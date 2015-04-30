using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EBid.lib.bid.data;
using EBid.lib.bid.trans;
using EBid.lib.user.trans;
using EBid.lib.auction.data;
using EBid.lib.constant;
using EBid.lib;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using EBid.ConnectionString;

public partial class web_purchasing_screens_renegotiatedsummary : System.Web.UI.Page
{
    private string connstring = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (Session[Constant.SESSION_BIDDETAILNO] == null)
            Response.Redirect("bidsforrenegotiation.aspx");

        if (!IsPostBack)
        {
            gvTenders.SelectedIndex = 0;
            lblPageSize.Text = ConfigurationManager.AppSettings["CommentsPerPage"];
        }

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Endorsement Summary");
    }

    private void ChangeStatus(int bidtenderno, int status)
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("Status", SqlDbType.Int);
            sqlParams[0].Value = status;
            sqlParams[1] = new SqlParameter("@BidTenderNo", SqlDbType.Int);
            sqlParams[1].Value = bidtenderno;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "sp_UpdateBidTenderStatus", sqlParams);
            sqlTransact.Commit();
        }
        catch
        {
            sqlTransact.Rollback();
        }
        finally
        {
            sqlConnect.Close();
        }
    }

    private void ChangeRenegotiationStatus(int bidtenderno, int status)
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@Status", SqlDbType.Int);
            sqlParams[0].Value = status;
            sqlParams[1] = new SqlParameter("@BidTenderNo", SqlDbType.Int);
            sqlParams[1].Value = bidtenderno;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "sp_UpdateBidTenderRenegotiationStatus", sqlParams);
            sqlTransact.Commit();
        }
        catch
        {
            sqlTransact.Rollback();
        }
        finally
        {
            sqlConnect.Close();
        }
    }

    private void InsertNewComment(String NewComment,int BidTenderNo)
    {
        //@BidTenderNo int, @UserId int, @Comment varchar(1000), @CommentType char(2)
        SqlConnection sqlConnect = new SqlConnection(connstring);

        sqlConnect.Open();

        SqlParameter[] sqlparams = new SqlParameter[4];
        sqlparams[0] = new SqlParameter("@BidTenderNo",SqlDbType.Int);
        sqlparams[0].Value = BidTenderNo;

        sqlparams[1] = new SqlParameter("@UserId",SqlDbType.Int);
        sqlparams[1].Value = int.Parse(Session[Constant.SESSION_USERID].ToString());

        sqlparams[2] = new SqlParameter("@Comment",SqlDbType.VarChar);
        sqlparams[2].Value = NewComment;

        sqlparams[3] = new SqlParameter("@CommentType",SqlDbType.Char);
        sqlparams[3].Value = Constant.BIDTENDERCOMMENT_PURCHASING_TO_BUYER;

        SqlHelper.ExecuteNonQuery(sqlConnect, "sp_AddBidTenderComment", sqlparams);
        sqlConnect.Close();
    }

    protected void gvTenders_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        switch (e.CommandName)
        {
            case "AwardItem":
                {
                    int awardstatus = Constant.BIDTENDER_STATUS.STATUS.AWARDED;
                    int notawardedstatus = Constant.BIDTENDER_STATUS.STATUS.NOT_AWARDED;

                    string vendorname = GetVendorInfo(int.Parse(e.CommandArgument.ToString()), "name");
                    string vendoremail = GetVendorInfo(int.Parse(e.CommandArgument.ToString()), "email");

                    ChangeStatus(int.Parse(e.CommandArgument.ToString()), awardstatus);
                    SendEmail(vendorname, vendoremail, awardstatus, "Trans-Asia / Commnunications : Congratulations");

                    GridViewRow row = ((Control)e.CommandSource).NamingContainer as GridViewRow;
                    TextBox tmp_txtComment = (TextBox)row.FindControl("txtComment");
                    InsertNewComment(tmp_txtComment.Text, int.Parse(e.CommandArgument.ToString()));
                                      
                    GetVendorsInfoNotAwardedOrConverted(int.Parse(dvEventDetails.Rows[0].Cells[1].Text.ToString()));
                    UpdateStatus(notawardedstatus);

                    GetVendorsInfoNotAwardedOrConvertedByBidRef(int.Parse(((HiddenField)dvEventDetails.Rows[5].Cells[1].FindControl("hdn_bidrefno")).Value.ToString()), int.Parse(dvEventDetails.Rows[0].Cells[1].Text.ToString()));
                    //SendEmailToNotAwardedOrConverted(notawardedstatus);
                    //gvTenders.DataBind();
                    Response.Redirect("awardedbiditems.aspx");
                } break;
            case "RenegotiateItem":
                {   
                    int renegotiatestatus = Constant.BIDTENDER_STATUS.STATUS.RENEGOTIATED;
                    int purchasingtobuyer = Constant.BIDTENDER_STATUS.RENEGOTIATION_STATUS.PURCHASING_TO_BUYER;

                    string vendorname = GetVendorInfo(int.Parse(e.CommandArgument.ToString()), "name");
                    string vendoremail = GetVendorInfo(int.Parse(e.CommandArgument.ToString()), "email");

                    ChangeStatus(int.Parse(e.CommandArgument.ToString()), renegotiatestatus);
                    ChangeRenegotiationStatus(int.Parse(e.CommandArgument.ToString()), purchasingtobuyer);
                    SendEmail(vendorname, vendoremail, renegotiatestatus, "Trans-Asia / Commnunications : For Clarification");

                    GridViewRow row = ((Control)e.CommandSource).NamingContainer as GridViewRow;                    
                    TextBox tmp_txtComment = (TextBox)row.FindControl("txtComment");
                    InsertNewComment(tmp_txtComment.Text, int.Parse(e.CommandArgument.ToString()));
                    //gvTenders.DataBind();                    
                    Response.Redirect("bidsforrenegotiation.aspx");                    
                } break;
            case "SetComments":
                {
                    GridViewRow row = ((Control)e.CommandSource).NamingContainer as GridViewRow;

                    Panel tmp_pnlcommand = (Panel)row.FindControl("pnlCommands");
                    Panel tmp_pnlcomment = (Panel)row.FindControl("pnlComments");
                    LinkButton btnaward = (LinkButton)row.FindControl("btnOkAward");
                    LinkButton btnrenegotiate = (LinkButton)row.FindControl("btnOkRenegotiate");
                    LinkButton btnDetails = (LinkButton)row.FindControl("lblDetails");
                    

                    tmp_pnlcommand.Visible = false;
                    tmp_pnlcomment.Visible = true;

                    btnDetails.Visible = false;

                    string[] args = e.CommandArgument.ToString().Split(new char[]{ '|' });

                    switch (args[0].ToUpper())
                    {
                        case "AWARD":
                            btnaward.Visible = true;
                            btnrenegotiate.Visible = false;
                            break;
                        case "RENEGOTIATE":
                            btnaward.Visible = false;
                            btnrenegotiate.Visible = true;
                            break;
                    }
                    gvTenders.SelectedIndex = row.RowIndex;
                    Session[Constant.SESSION_BIDTENDERNO] = args[1];
                    BindComments();
                } break;
            case "CancelComments":
                {
                    GridViewRow row = ((Control)e.CommandSource).NamingContainer as GridViewRow;
                    Panel tmp_pnlcommand = (Panel)row.FindControl("pnlCommands");
                    Panel tmp_pnlcomment = (Panel)row.FindControl("pnlComments");
                    TextBox tmp_txtComment = (TextBox)row.FindControl("txtComment");
                    LinkButton btnDetails = (LinkButton)row.FindControl("lblDetails");

                    tmp_pnlcommand.Visible = true;
                    tmp_pnlcomment.Visible = false;
                    tmp_txtComment.Text = "";
                    btnDetails.Visible = true;
                } break;
            case "Select":
                {
                    string[] args = e.CommandArgument.ToString().Split(new char[] { '|' });
                    Session[Constant.SESSION_BIDTENDERNO] = args[0];
                    Session["TVendorId"] = args[1];
                    Session[Constant.SESSION_BIDREFNO] = args[2];
                    Session["ViewOption"] = "AsBuyer";
                    pnlDetails.Visible = true;
                    //Session[Constant.SESSION_BIDTENDERNO] = e.CommandArgument.ToString();
                    BindComments();
                } break;
        }
    }

    protected String GetRenegotiateDesc(String rstatus)
    {
        String rstatus_desc = "";
        switch (Int32.Parse(rstatus))
        { 
            case 0:
                rstatus_desc = "For Clarification";
                break;
            case 1:
                rstatus_desc = "For Clarification";
                break;
            case 2: case 3:
                rstatus_desc = "Under Clarification";
                break;
            
            case 4:
                rstatus_desc = "";
                break;
        }
        return rstatus_desc;
    }
        
    protected bool IsPanelVisible(string status)
    {
        bool isVisible = false;

        switch (status)
        {
            case "0": isVisible = true; break;
            case "1": isVisible = false; break;
            case "2": isVisible = false; break;
            case "3": isVisible = false; break;
            case "4": isVisible = true; break;            
        }

        return isVisible;
    }    

    private void GetVendorsInfoNotAwardedOrConverted(int vDetailNo)
    {
        SqlParameter[] sqlparams = new SqlParameter[1];
        sqlparams[0] = new SqlParameter("@BidDetailNo", SqlDbType.Int);
        sqlparams[0].Value = vDetailNo;
        DataSet vendorData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "sp_QueryVendordetailsNotAwardedOrConverted", sqlparams);

        DataTable vendorDataTable = vendorData.Tables[0];
        ViewState["NotAwardedOrConvertedVendors"] = vendorDataTable;
    }

    private void GetVendorsInfoNotAwardedOrConvertedByBidRef(int bidrefno, int vDetailNo)
    {
        SqlParameter[] sqlparams = new SqlParameter[2];
        sqlparams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
        sqlparams[0].Value = bidrefno;
        sqlparams[1] = new SqlParameter("@BidDetailNo", SqlDbType.Int);
        sqlparams[1].Value = vDetailNo;
        DataSet vendorData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "sp_GetBidEventParticipants", sqlparams);

        DataTable vendorDataTable = vendorData.Tables[0];
        ViewState["NotAwardedOrConvertedVendors"] = vendorDataTable;
    }

    private void UpdateStatus(int vStatus)
    {
        DataTable dtVendors = (DataTable)ViewState["NotAwardedOrConvertedVendors"];

        if (dtVendors.Rows.Count > 0)
        {
            for (int i = 0; i < dtVendors.Rows.Count; i++)
            {
                ChangeStatus(int.Parse(dtVendors.Rows[i]["BidTenderNo"].ToString()), vStatus);

            }
        }
    }

    private BidDetails GetBidItemDetails(int bidrefno)
    {
        DataTable dt = SqlHelper.ExecuteDataset(connstring, "sp_GetBidInvitationInfo", new SqlParameter[] { new SqlParameter("@BidRefNo", bidrefno) }).Tables[0];
        BidDetails item = new BidDetails();

        if (dt.Rows.Count > 0)
            item = new BidDetails(dt.Rows[0]);

        return item;
    }

    private string GetVendorInfo(int vTenderNo, string vInfo)
    {
        SqlParameter[] sqlparams = new SqlParameter[1];
        sqlparams[0] = new SqlParameter("@BidTenderNo", SqlDbType.Int);
        sqlparams[0].Value = vTenderNo;
        DataSet vendorData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "sp_GetSupplierInfoByTenderNo", sqlparams);

        DataTable vendorDataTable = vendorData.Tables[0];
        DataRow vendorRow = vendorDataTable.Rows[0];

        if (vInfo == "name")
        {
            return vendorRow["VendorName"].ToString().Trim();
        }
        else
            return vendorRow["VendorEmail"].ToString().Trim();
    }    

    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        if (Session[Constant.SESSION_LASTPAGE] != null)
            Response.Redirect(Session[Constant.SESSION_LASTPAGE].ToString());
        else
            Response.Redirect("bids.aspx");
    }

    protected void gvTenders_SelectedIndexChanged(object sender, EventArgs e)
    {
        dvTenderDetails.PageIndex = gvTenders.SelectedIndex;
        // load comments control
        lblCurrentIndex.Text = "0";
        BindComments();
    }    

    protected void gvTenders_DataBound(object sender, EventArgs e)
    {
        if (gvTenders.DataKeys.Count > 0)
        {
            Session[Constant.SESSION_BIDTENDERNO] = gvTenders.DataKeys[gvTenders.SelectedIndex].Values[0].ToString();
            // load comments control
            BindComments();
        }
    }

    private void SendEmailToNotAwardedOrConverted(int vStatus)
    {
        DataTable dtVendors = (DataTable)ViewState["NotAwardedOrConvertedVendors"];

        if (dtVendors.Rows.Count > 0)
        {
            for (int i = 0; i < dtVendors.Rows.Count; i++)
            {
                SendEmail(dtVendors.Rows[i]["VendorName"].ToString(),
                            dtVendors.Rows[i]["VendorEmail"].ToString(),
                            dtVendors.Rows[i]["VendorAddress"].ToString(),
                            dtVendors.Rows[i]["VendorAddress1"].ToString(),
                            dtVendors.Rows[i]["VendorAddress2"].ToString(),
                            dtVendors.Rows[i]["VendorAddress3"].ToString(),
                            dtVendors.Rows[i]["ContactPerson"].ToString(),
                            dtVendors.Rows[i]["TelephoneNo"].ToString(),
                            dtVendors.Rows[i]["Fax"].ToString(),
                            vStatus, "Trans-Asia  : Thank You");
            }
        }
    }

    #region SendEmail OVERLOADED

    private bool SendEmail(string vVendorname, string vVendoremail, int vStatus, String subj)
    {
        bool success = false;
        string fullname = vVendorname;
        string subject;

        //vVendoremail = "jpineda@asiaonline.net.ph";

        subject = subj;

        if (MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                MailHelper.ChangeToFriendlyName(ConfigurationManager.AppSettings["AdminEmailName"], ConfigurationManager.AppSettings["AdminEmailAddress"]),
                MailHelper.ChangeToFriendlyName(vVendorname, vVendoremail),
                subject,
                CreateBody(vStatus, vVendorname, "", "", "", "", "", "", ""),
                MailTemplate.GetTemplateLinkedResources(this)))
            success = true;

        return success;
    }

    private bool SendEmail(
        string vVendorname,
        string vVendoremail,
        string vVendoraddress,
        string vVendoraddress1,
        string vVendoraddress2,
        string vVendoraddress3,
        string vVendorcontactperson,
        string vVendorphone,
        string vVendorfax,
        int vStatus,
        string subj
                            )
    {
        bool success = false;
        string fullname = vVendorname;
        string subject;

        //vVendoremail = "jpineda@asiaonline.net.ph";

        subject = subj;

        if (MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                MailHelper.ChangeToFriendlyName(ConfigurationManager.AppSettings["AdminEmailName"], ConfigurationManager.AppSettings["AdminEmailAddress"]),
                MailHelper.ChangeToFriendlyName(vVendorname, vVendoremail),
                subject,
                CreateBody(vStatus, vVendorname, vVendoraddress, vVendoraddress1, vVendoraddress2, vVendoraddress3, vVendorcontactperson, vVendorphone, vVendorfax),
                MailTemplate.GetTemplateLinkedResources(this)))
            success = true;

        return success;
    }

    #endregion

    #region EMAIL BODY
    private string CreateBody(
        int status,
        string vendorname,
        string vendorAddress,
        string vendorAddress1,
        string vendorAddress2,
        string vendorAddress3,
        string vendorcontactperson,
        string vendorphone,
        string vendorfax
        )
    {
        BidDetails details = GetBidItemDetails(int.Parse(((HiddenField)dvEventDetails.Rows[5].Cells[1].FindControl("hdn_bidrefno")).Value.ToString()));
        String b_Event = ((HiddenField)dvEventDetails.Rows[5].Cells[1].FindControl("hdn_bidevent")).Value.ToString();
        String b_Item = dvEventDetails.Rows[2].Cells[1].Text.ToString();

        StringBuilder sb = new StringBuilder();
        if (status == 3) //FOR RENEGOTIATION
        {
            #region CLARIFY BODY

            sb.Append("<tr><td style='width: 5%; height: 13px;'></td><td style='width: 90%; height: 13px;' colspan=''></td><td style='width: 5%; height: 13px;'></td></tr><tr><td style='width: 5%; height: 635px'></td><td style='width: 90%'><br />");
            sb.Append(DateTime.Now.ToLongDateString());
            sb.Append("<br />");
            sb.Append("<br />");
            sb.Append("<br /><strong>");
            sb.Append(vendorname);
            sb.Append("</strong><br />");
            sb.Append("<br /><br />");
            sb.Append("Attention:");
            sb.Append("<br />");
            sb.Append("Re: Request for Clarification: " + b_Item + "</p>");
            sb.Append("<br /><br />");
            sb.Append("Dear Mr/Madame,");
            sb.Append("<br /><br />");
            sb.Append("We would like to request for a clarification on your commercial tender and technical response for Bid Invitation: " + b_Item + ".<br />");
            sb.Append("Kindly refer to this item posted under <strong>Bid Tenders for Clarification</strong> on the e-Sourcing Portal for the details.");
            sb.Append("<br /><br /><br />");
            sb.Append("Very truly yours,<br /><br />");
            sb.Append(details.Sender);
            sb.Append("<br />    ");
            sb.Append("</p></td><td style='width: 5%; height: auto'></td></tr><tr><td style='width: 5%'></td><td style='width: 90%'></td><td style='width: 5%'></td></tr>");

            #endregion
        }
        else if (status == 4) //AWARDED
        {
            #region AWARDED BODY

            sb.Append("<tr><td style='width: 5%; height: 13px;'></td><td style='width: 90%; height: 13px;' colspan=''></td><td style='width: 5%; height: 13px;'></td></tr><tr><td style='width: 5%; height: 635px'></td><td style='width: 90%'><br />");
            sb.Append(DateTime.Now.ToLongDateString());
            sb.Append("<br /><br />");
            sb.Append("Managing Director");
            sb.Append("<br /><strong>");
            sb.Append(vendorname);
            sb.Append("</strong><br />");
            sb.Append("<br /><br />");
            sb.Append("Dear Mr/Madame " + vendorcontactperson + ",");
            sb.Append("<br /><br />");
            sb.Append("Re: Letter of Award - " + b_Item + "");
            sb.Append("<br /><br />");
            sb.Append("Trans-Asia / Commnunications is pleased to inform you that your bid tender has been chosen for " + b_Item + ".");
            sb.Append("Kindly get in touch with " + details.Creator + " for further details");
            sb.Append("<br /><br />");
            sb.Append("We sincerely appreciate the time and effort you have dedicated to conform with our requirements and we look forward to working with you on the fulfillment of " + b_Item + ".");
            sb.Append("<br /><br />");
            sb.Append("Very truly yours,");
            sb.Append("<br /><br />");
            sb.Append("T. Jefferson M. de Leon");
            sb.Append("<br />");
            sb.Append("<i>Head-Logistics</i>");
            sb.Append("<br /><br />");
            sb.Append("Cc:&nbsp; EC Santiago");
            sb.Append("</td><td style='width: 5%; height: auto'></td></tr><tr><td style='width: 5%'></td><td style='width: 90%'></td><td style='width: 5%'></td></tr>");

            #endregion
        }
        else if (status == 5) //NOT AWARDED
        {
            #region NOT AWARDED BODY
            sb.Append("<tr><td style='width: 1px'></td><td style='width: auto' colspan=''></td><td style='width: 1px'></td></tr>");
            sb.Append("<tr><td style='width: auto; height: 635px'></td>");
            sb.Append("<td style='width: 100%; height: auto; text-align: justify;'>");
            sb.Append("<br /><br /><br />");
            sb.Append("" + DateTime.Now.ToLongDateString() + "");
            sb.Append("<br /><br /><br /><strong>");
            sb.Append(vendorname.Trim());
            sb.Append("<br /></strong>");
            sb.Append(vendorAddress.Trim());
            sb.Append("<br />");
            sb.Append(vendorAddress1.Trim());
            sb.Append("<br />");
            sb.Append(vendorAddress2.Trim());
            sb.Append("<br />");
            sb.Append(vendorAddress3.Trim());
            sb.Append("<br /><br />");
            sb.Append("<table style='width: 100%'><tr><td style='width: 1px; height: 8px'>Attention&nbsp; :</td><td style='width: 548px; height: 8px'><strong>");
            sb.Append(vendorcontactperson.Trim());
            sb.Append("</strong></td><td style='width: 1px; height: 8px'></td></tr><tr><td style='width: 1px'></td><td style='width: 548px'>");
            sb.Append(vendorAddress.Trim());
            sb.Append("</td><td style='width: 1px'></td></tr><tr><td style='width: 1px'></td><td style='width: 548px'>");
            sb.Append(vendorAddress1.Trim());
            sb.Append("</td><td style='width: 1px'></td></tr><tr><td style='width: 1px'></td><td style='width: 548px'>");
            sb.Append(vendorAddress2.Trim());
            sb.Append("</td><td style='width: 1px'></td></tr><tr><td style='width: 1px'></td><td style='width: 548px'>");
            sb.Append(vendorAddress3.Trim());
            sb.Append("</td><td style='width: 1px'></td></tr></table>");
            sb.Append("<br /><br />");
            sb.Append("<table style='width: 100%'><tr><td style='width: 12px'>");
            sb.Append("Bid Event:");
            sb.Append("</td><td style='width: auto'>");
            sb.Append("RFP Results for " + b_Event);
            sb.Append("</td></tr><tr><td style='width: 12px'>");
            sb.Append("Bid Item:");
            sb.Append("</td><td style='width: auto'>");
            sb.Append(b_Item);
            sb.Append("</td></tr></table>");
            sb.Append("<br /><br />");
            sb.Append("Dear Sir:");
            sb.Append("<br /><br />");
            sb.Append("Thank you for your interest to help Trans-Asia  in finding a solution for ");
            sb.Append("" + b_Event + ". It certainly was a pleasure to ");
            sb.Append("have worked with your company during the RFP and the succeeding clarificatory discussions.");
            sb.Append("<br /><br />");
            sb.Append("After careful evaluation of your solution, we regret to inform you that we are unable ");
            sb.Append("to award the project to " + vendorname + ". We will, however, keep in mind your cooperation ");
            sb.Append("and commitment when we have the opportunity to implement other projects.");
            sb.Append("<br /><br />");
            sb.Append("We sincerely appreciate the time and effort you dedicated for the completion of ");
            sb.Append("this RFP and we look forward to working with you again in the future.");
            sb.Append("<br /><br /><br />");
            sb.Append("Sincerely,");
            sb.Append("<br /><br /><br /><br />");
            sb.Append("Ma. Corazon V. Martin");
            sb.Append("<br />");
            sb.Append("Head, Corporate Procurement Department");
            sb.Append("<br /><br /><br /><br /></td><td style='width: auto; height: auto'></td></tr><tr><td style='width: auto'></td><td style='width: auto'></td><td style='width: auto'></td></tr>");
            #endregion
        }
        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }

    #endregion    

    #region Comments
    // bidtender comments usercontrol was not used because of some binding issues.
    // bidtender comments need to be rebinded when a tender is selected.
    public void BindComments()
    {
        DataSet dSet = new DataSet();

        SqlConnection sqlConnection = new SqlConnection(connstring);

        SqlCommand sqlCommand = new SqlCommand("sp_GetBidTenderComments", sqlConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.Parameters.Add("@BidTenderNo", SqlDbType.Int);
        sqlCommand.Parameters[0].Value = int.Parse(Session[Constant.SESSION_BIDTENDERNO].ToString());
        sqlCommand.Parameters.Add("@UserType", SqlDbType.Int);
        sqlCommand.Parameters[1].Value = int.Parse(Session[Constant.SESSION_USERTYPE].ToString());

        SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);

        using (sqlConnection)
        {
            sqlAdapter.Fill(dSet);
            lblRecordCount.Text = dSet.Tables[0].Rows.Count.ToString();
            dSet = new DataSet();

            sqlAdapter.Fill(dSet, int.Parse(lblCurrentIndex.Text), int.Parse(lblPageSize.Text), "Comments");
            dlComments.DataSource = dSet.Tables["Comments"].DefaultView;
            dlComments.DataBind();
        }
        ShowCounts();
    }

    protected void ShowCounts()
    {
        int recordCount = int.Parse(lblRecordCount.Text);
        int pageSize = int.Parse(lblPageSize.Text);
        int currentIndex = int.Parse(lblCurrentIndex.Text);

        if (recordCount > 0)
        {
            lblCounts.Text = (currentIndex + 1) + " to ";
            if ((currentIndex + pageSize) <= recordCount)
                lblCounts.Text += (currentIndex + pageSize);
            else
                lblCounts.Text += recordCount;

            lblCounts.Text += " out of " + recordCount + " comments";
            // show or hide page buttons        
            trPagers.Visible = (recordCount > pageSize);
        }
        else
        {
            lblCounts.Text = "No Comments";
            trPagers.Visible = false;
        }
    }

    protected void btnFirstPage_Click(object sender, ImageClickEventArgs e)
    {
        lblCurrentIndex.Text = "0";
        BindComments();
    }

    protected void btnPreviousPage_Click(object sender, ImageClickEventArgs e)
    {
        lblCurrentIndex.Text = (int.Parse(lblCurrentIndex.Text) - int.Parse(lblPageSize.Text)) + "";
        if (int.Parse(lblCurrentIndex.Text) < 0)
            lblCurrentIndex.Text = "0";
        BindComments();
    }

    protected void btnNextPage_Click(object sender, ImageClickEventArgs e)
    {
        if ((int.Parse(lblCurrentIndex.Text) + int.Parse(lblPageSize.Text)) < int.Parse(lblRecordCount.Text))
            lblCurrentIndex.Text = (int.Parse(lblCurrentIndex.Text) + int.Parse(lblPageSize.Text)) + "";
        BindComments();
    }

    protected void btnLastPage_Click(object sender, ImageClickEventArgs e)
    {
        int intMod = int.Parse(lblRecordCount.Text) % int.Parse(lblPageSize.Text);

        if (intMod > 0)
            lblCurrentIndex.Text = (int.Parse(lblRecordCount.Text) - intMod) + "";
        else
            lblCurrentIndex.Text = (int.Parse(lblRecordCount.Text) - int.Parse(lblPageSize.Text)) + "";
        BindComments();
    }

    #endregion
}