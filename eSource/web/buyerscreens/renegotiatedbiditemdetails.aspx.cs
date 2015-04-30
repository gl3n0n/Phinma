using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EBid.lib.bid.data;
using EBid.lib.bid.trans;
using EBid.lib.auction.data;
using EBid.lib.constant;
using EBid.lib;
using EBid.ConnectionString;
using System.IO;
using System.Data.Sql;
using System.Data.SqlClient;
using EBid.ConnectionString;

public partial class web_buyerscreens_renegotiatedbiditemdetails : System.Web.UI.Page
{
    private const string BR = "<br />";
    private const string BULLET = "&#187;";
    private const string BR_BULLET = "<br />&#187;";
    private string connstring = "";
    private SqlConnection sqlConnection;
    private SqlDataAdapter sqlAdapter;
    private SqlCommand sqlCommand;

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (HttpContext.Current.Session["clientid"] != null)
        {
            connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        }
        else
        {
            Response.Redirect("../../login.aspx");
        }
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Clarified Bid Item Details");

        if (Session[Constant.SESSION_BIDDETAILNO] == null)
            Response.Redirect("bidsforeval.aspx");

        if (!IsPostBack)
        {
            lblPageSize.Text = ConfigurationManager.AppSettings["CommentsPerPage"];
            gvBidItemTenders.SelectedIndex = 0;
            InitializeRowsForGridViewAttachments();
        }

        if (Session["Message"] != null)
        {
            lblMessage.Text = Session["Message"].ToString().Trim();
            Session["Message"] = null;
        }
    }

    protected void lnkBack_Click(object sender, EventArgs e)
    {
        if (Session[Constant.SESSION_LASTPAGE] != null)
            Response.Redirect(Session[Constant.SESSION_LASTPAGE].ToString());
        else
            Response.Redirect("bidsforeval.aspx");
    }

    protected void gvBidItemTenders_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = ((Control)e.CommandSource).NamingContainer as GridViewRow;
        Panel pnlLinks = (Panel)gvr.FindControl("pnlLinks");
        Panel pnlComments = (Panel)gvr.FindControl("pnlComments");
        LinkButton lnkOK = (LinkButton)gvr.FindControl("lnkOK");
        LinkButton lnkReendorsed = (LinkButton)gvr.FindControl("lnkEndorse");
        LinkButton lnkRenegotiate = (LinkButton)gvr.FindControl("lnkRenegotiate");
        HiddenField hdnVendorId = (HiddenField)gvr.FindControl("hdnVendorId");

        switch (e.CommandName)
        {
            case "Clarify":
                {
                    pnlLinks.Visible = false;
                    pnlComments.Visible = true;
                    gvBidItemTenders.Columns[5].ControlStyle.Width = Unit.Pixel(150);
                    ViewState["Command"] = "clarify";
                    lnkOK.Attributes.Add("onclick", "return confirm('Are you sure you want to clarify this item?');");
                    pnlFileUpload.Visible = true;
                } break;
            case "Renegotiate":
                {
                    pnlLinks.Visible = false;
                    pnlComments.Visible = true;
                    gvBidItemTenders.Columns[5].ControlStyle.Width = Unit.Pixel(150);
                    ViewState["Command"] = "renegotiate";
                    lnkOK.Attributes.Add("onclick", "return confirm('Are you sure you want to renegotiate this item?');");
                    pnlFileUpload.Visible = true;
                } break;
            case "Re-endorse":
                {
                    pnlLinks.Visible = false;
                    pnlComments.Visible = true;
                    gvBidItemTenders.Columns[5].ControlStyle.Width = Unit.Pixel(150);
                    ViewState["Command"] = "re-endorse";
                    lnkOK.Attributes.Add("onclick", "return confirm('Are you sure you want to re-endorse this item?');");

                } break;
            case "ContinueEndorsement":
                {
                    if (ViewState["Command"] != null)
                    {
                        TextBox tbComments = (TextBox)pnlComments.FindControl("txtComment");

                        // save comment
                        if (ViewState["Command"].ToString() == "clarify")
                        {
                            // update bid tender status to "renegotiated"
                            bool updateOk = BidTransaction.UpdateBidTenderStatus(connstring, Convert.ToInt32(e.CommandArgument.ToString()), Constant.BIDTENDER_STATUS.STATUS.RENEGOTIATED);
                            bool updateOk2 = BidTransaction.UpdateBidTenderRenegotiationStatus(connstring, Convert.ToInt32(e.CommandArgument.ToString()), Constant.BIDTENDER_STATUS.RENEGOTIATION_STATUS.BUYER_TO_VENDOR);
                            bool updateOk3 = BidTransaction.UpdateAsClarifiedStatus(connstring, Convert.ToInt32(e.CommandArgument.ToString()), 1);

                            bool saveCommentOk = BidTransaction.SaveBidTenderComment(connstring, int.Parse(e.CommandArgument.ToString()),
                            int.Parse(Session[Constant.SESSION_USERID].ToString()), tbComments.Text.Trim(), Constant.BIDTENDERCOMMENT_BUYER_TO_VENDOR);

                            if (updateOk && updateOk2 && updateOk3 && saveCommentOk)
                            {
                                //save attachments
                                DeleteExistingBidEventAttachments(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()));
                                SaveBidEventAttachments(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()));
                                Session["Message"] = "Bid item was successfully clarified.";

                                //send sms
                                try
                                {
                                    if (SMSHelper.AreValidMobileNumbers(GetVendorMobileNo(int.Parse(hdnVendorId.Value))))
                                    {
                                        SMSHelper.SendSMS(new SMSMessage(CreateSmsBody(e.CommandArgument.ToString().Trim()), GetVendorMobileNo(int.Parse(hdnVendorId.Value))));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    LogHelper.EventLogHelper.Log("Bid Event > Send SMS Notification : " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                                }
                                //end of sms sending

                                Response.Redirect("renegotiatedbiditemdetails.aspx");

                            }
                            else
                            {
                                Session["Message"] = "Bid item was not clarified.";
                                pnlLinks.Visible = true;

                            }
                        }
                        else if (ViewState["Command"].ToString() == "renegotiate")
                        {
                            // update bid tender status to "renegotiated"
                            bool updateOk = BidTransaction.UpdateBidTenderStatus(connstring, Convert.ToInt32(e.CommandArgument.ToString()), Constant.BIDTENDER_STATUS.STATUS.RENEGOTIATED);
                            bool updateOk2 = BidTransaction.UpdateBidTenderRenegotiationStatus(connstring, Convert.ToInt32(e.CommandArgument.ToString()), Constant.BIDTENDER_STATUS.RENEGOTIATION_STATUS.BUYER_TO_VENDOR);
                            bool updateOk3 = BidTransaction.UpdateAsClarifiedStatus(connstring, Convert.ToInt32(e.CommandArgument.ToString()), 0);

                            bool saveCommentOk = BidTransaction.SaveBidTenderComment(connstring, int.Parse(e.CommandArgument.ToString()),
                            int.Parse(Session[Constant.SESSION_USERID].ToString()), tbComments.Text.Trim(), Constant.BIDTENDERCOMMENT_BUYER_TO_VENDOR);

                            if (updateOk && updateOk2 && updateOk3 && saveCommentOk)
                            {
                                //save attachments
                                DeleteExistingBidEventAttachments(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()));
                                SaveBidEventAttachments(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()));
                                Session["Message"] = "Bid item was successfully clarified.";

                                //send sms
                                try
                                {
                                    if (SMSHelper.AreValidMobileNumbers(GetVendorMobileNo(int.Parse(hdnVendorId.Value))))
                                    {
                                        SMSHelper.SendSMS(new SMSMessage(CreateSmsBody(e.CommandArgument.ToString().Trim()), GetVendorMobileNo(int.Parse(hdnVendorId.Value))));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    LogHelper.EventLogHelper.Log("Bid Event > Send SMS Notification : " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                                }
                                //end of sms sending
                                Response.Redirect("renegotiatedbiditemdetails.aspx");

                            }
                            else
                            {
                                Session["Message"] = "Bid item was not clarified.";
                                pnlLinks.Visible = true;

                            }
                        }
                        else if (ViewState["Command"].ToString() == "re-endorse")
                        {
                            // update bid tender status to "endorsed"
                            bool updateOk = BidTransaction.UpdateBidTenderStatus(connstring, Convert.ToInt32(e.CommandArgument.ToString()), Constant.BIDTENDER_STATUS.STATUS.ENDORSED);
                            bool updateOk2 = BidTransaction.UpdateBidTenderRenegotiationStatus(connstring, Convert.ToInt32(e.CommandArgument.ToString()), Constant.BIDTENDER_STATUS.RENEGOTIATION_STATUS.BUYER_TO_PURCHASING);

                            bool saveCommentOk = BidTransaction.SaveBidTenderComment(connstring, int.Parse(e.CommandArgument.ToString()),
                            int.Parse(Session[Constant.SESSION_USERID].ToString()), tbComments.Text.Trim(), Constant.BIDTENDERCOMMENT_BUYER_TO_PURCHASING);

                            if (updateOk && updateOk2 && saveCommentOk)
                            {
                                Session["Message"] = "Bid item was successfully endorsed.";
                                Response.Redirect("renegotiatedbiditemdetails.aspx");

                            }
                            else
                            {
                                Session["Message"] = "Bid item was not endorsed.";
                                pnlLinks.Visible = true;
                                pnlComments.Visible = false;
                            }
                        }
                    }

                } break;
            case "CancelEndorsement":
                {
                    pnlLinks.Visible = true;
                    pnlComments.Visible = false;
                    dvTenderDetails.PageIndex = gvBidItemTenders.SelectedIndex;
                    Session[Constant.SESSION_BIDTENDERNO] = gvBidItemTenders.DataKeys[gvBidItemTenders.SelectedIndex].Values[0].ToString();
                    BindComments();
                    gvBidItemTenders.Columns[5].ControlStyle.Width = Unit.Pixel(90);
                    pnlFileUpload.Visible = false;
                } break;
            case "Select":
                {
                    string[] args = e.CommandArgument.ToString().Split(new char[] { '|' });
                    Session[Constant.SESSION_BIDTENDERNO] = args[0];
                    Session["TVendorId"] = args[1];
                    Session[Constant.SESSION_BIDREFNO] = args[2];
                    Session["ViewOption"] = "AsBuyer";
                    lblCurrentIndex.Text = "0";
                    BindComments();
                    gvBidItemTenders.Columns[5].ControlStyle.Width = Unit.Pixel(90);
                    dvTenderDetails.Visible = true;
                    pnlTenderAttachments.Visible = true;
                } break;
        }
    }

    protected void gvBidItemTenders_SelectedIndexChanged(object sender, EventArgs e)
    {
        dvTenderDetails.PageIndex = gvBidItemTenders.SelectedIndex;
    }

    protected void gvBidItemTenders_DataBound(object sender, EventArgs e)
    {
        Session[Constant.SESSION_BIDTENDERNO] = gvBidItemTenders.DataKeys[gvBidItemTenders.SelectedIndex].Values[0].ToString();
        // load comments control        
        BindComments();
    }

    protected bool isEndorsed(Object itemStatus)
    {
        int stat = int.Parse(itemStatus.ToString());

        return (stat == Constant.BIDTENDER_STATUS.STATUS.ENDORSED);
    }

    protected bool showRenegotiateButton(Object itemStatus, Object renegotiationDeadline)
    {
        int stat = int.Parse(itemStatus.ToString());

        if ((renegotiationDeadline.ToString().Length != 0) && (stat == Constant.BIDTENDER_STATUS.RENEGOTIATION_STATUS.PURCHASING_TO_BUYER))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected bool showBidPrice(Object itemStatus, Object renegotiationDeadline)
    {
        int istat = int.Parse(itemStatus.ToString());

        if ((istat == Constant.BIDTENDER_STATUS.RENEGOTIATION_STATUS.VENDOR_TO_BUYER) && (renegotiationDeadline.ToString() == ""))
        {
            return false;
        }
        else if ((istat == Constant.BIDTENDER_STATUS.RENEGOTIATION_STATUS.VENDOR_TO_BUYER) && (renegotiationDeadline.ToString() != ""))
        {

            DateTime renegotiationdeadline = DateTime.Parse(renegotiationDeadline.ToString());
            DateTime datenow = DateTime.Now;

            if (DateTime.Compare(renegotiationdeadline, datenow) < 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        else
        {
            return true;
        }
    }

    protected bool showUnderRenegotiationLabel(Object itemStatus)
    {
        int stat = int.Parse(itemStatus.ToString());

        // show under renegotiation label if already sent to vendor
        //return false; 
        return (stat == Constant.BIDTENDER_STATUS.RENEGOTIATION_STATUS.BUYER_TO_VENDOR);
    }

    protected bool showReEndorseButton(Object itemStatus, Object renegotiationDeadline)
    {

        if (renegotiationDeadline.ToString() != "")
        {
            DateTime renegotiationdeadline = DateTime.Parse(renegotiationDeadline.ToString());
            DateTime datenow = DateTime.Now;

            if ((DateTime.Compare(renegotiationdeadline, datenow) < 0) &&
                    ((int.Parse(itemStatus.ToString()) == Constant.BIDTENDER_STATUS.RENEGOTIATION_STATUS.VENDOR_TO_BUYER) ||
                      (int.Parse(itemStatus.ToString()) == Constant.BIDTENDER_STATUS.RENEGOTIATION_STATUS.BUYER_TO_VENDOR)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {

            return false;
        }

    }

    protected bool showReEndorsedLabel(Object itemStatus)
    {
        int stat = int.Parse(itemStatus.ToString());

        // show re-endorsed label if already resent to purchasing for endorsement
        return (stat == Constant.BIDTENDER_STATUS.RENEGOTIATION_STATUS.BUYER_TO_PURCHASING);
    }

    #region Comments
    // bidtender comments usercontrol was not used because of some binding issues.
    // bidtender comments need to be rebinded when a tender is selected.
    public void BindComments()
    {
        DataSet dSet = new DataSet();

        sqlConnection = new SqlConnection(connstring);

        sqlCommand = new SqlCommand("sp_GetBidTenderComments", sqlConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.Parameters.Add("@BidTenderNo", SqlDbType.Int);
        sqlCommand.Parameters[0].Value = int.Parse(Session[Constant.SESSION_BIDTENDERNO].ToString());
        sqlCommand.Parameters.Add("@UserType", SqlDbType.Int);
        sqlCommand.Parameters[1].Value = int.Parse(Session[Constant.SESSION_USERTYPE].ToString());

        sqlAdapter = new SqlDataAdapter(sqlCommand);

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

    protected void lnkWithdraw_Click(object sender, EventArgs e)
    {
        if (Session[Constant.SESSION_BIDDETAILNO] != null)
        {
            int biddetailno = int.Parse(Session[Constant.SESSION_BIDDETAILNO].ToString());

            if (BidItemTransaction.WithdrawBidItem(connstring, biddetailno, Constant.BIDITEM_STATUS.WITHDRAWAL_STATUS.APPROVED))
            {
                GetAllVendorsInfoByBidRef(biddetailno);
                SendEmailToVendors();

                Response.Redirect("withdrawnedbiditems.aspx");
            }
        }
    }

    //for sms sending
    private string CreateSmsBody(string bidtenderno)
    {
        return String.Format("Tender No.:{0}, Renegotiation Deadline: {1}", bidtenderno, GetBidEventRenegotiationDeadline());
    }

    private string GetBidEventRenegotiationDeadline()
    {
        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
        sqlParams[0].Value = Int32.Parse(Session[Constant.SESSION_BIDREFNO].ToString());

        return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "sp_GetBidEventRenegotiationDeadline", sqlParams).ToString().Trim();
    }

    private string GetVendorMobileNo(int UserId)
    {
        
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@UserId", SqlDbType.Int);
            sqlParams[0].Value = UserId;

            return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "sp_GetVendorMobileNo", sqlParams).ToString().Trim();    
       
    }

    #region EMAIL ALL PARTICIPANTS ABOUT WITHDRAWN ITEM

    private void GetAllVendorsInfoByBidRef(int vDetailNo)
    {
        SqlParameter[] sqlparams = new SqlParameter[1];
        sqlparams[0] = new SqlParameter("@BidDetailNo", SqlDbType.Int);
        sqlparams[0].Value = vDetailNo;
        DataSet vendorData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "sp_GetAllBidEventParticipantsByBidDetailNo", sqlparams);

        DataTable vendorDataTable = vendorData.Tables[0];
        ViewState["AllVendorsInBidDetailNo"] = vendorDataTable;
    }

    private void SendEmailToVendors()
    {
        DataTable dtVendors = (DataTable)ViewState["AllVendorsInBidDetailNo"];

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
                             "Trans-Asia  : Withdrawn Item");
            }
        }
    }

    private bool SendEmail
        (
        string vVendorname,
        string vVendoremail,
        string vVendoraddress,
        string vVendoraddress1,
        string vVendoraddress2,
        string vVendoraddress3,
        string vVendorcontactperson,
        string vVendorphone,
        string vVendorfax,
        string subj
        )
    {
        bool success = false;
        string fullname = vVendorname;
        string subject;

        subject = subj;


        //vVendorname = "Jason Pineda";
        //vVendoremail = "jpineda@asiaonline.net.ph";


        if (MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                MailHelper.ChangeToFriendlyName(ConfigurationManager.AppSettings["AdminEmailName"], ConfigurationManager.AppSettings["AdminEmailAddress"]),
                MailHelper.ChangeToFriendlyName(vVendorname, vVendoremail),
                subject,
                CreateBodyWithdrawn(vVendorname,
                                     vVendoraddress,
                                     vVendoraddress1,
                                     vVendoraddress2,
                                     vVendoraddress3,
                                     vVendorcontactperson,
                                     vVendorphone,
                                     vVendorfax),

                MailTemplate.GetTemplateLinkedResources(this)))
            success = true;

        return success;
    }

    private string CreateBodyWithdrawn(
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
        String b_Event = ((Label)((DetailsView)ctrlBidItemDetails.FindControl("dvBidItem")).Rows[5].Cells[1].FindControl("Label8")).Text;
        String b_Item = ((Label)((DetailsView)ctrlBidItemDetails.FindControl("dvBidItem")).Rows[1].Cells[1].FindControl("Label2")).Text;

        StringBuilder sb = new StringBuilder();

        #region WITHDRAWN EMAIL BODY
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
        sb.Append(b_Event);
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
        sb.Append("We regret to inform you that " + b_Item + " has been withdrawn. We will, however, keep in mind your cooperation ");
        sb.Append("and commitment when we have the opportunity to implement other projects. ");
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

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }
    #endregion

    #region FILE ATTACHMENTS
    private void InitializeRowsForGridViewAttachments()
    {
        DataTable dtAttachments = CreateAttachmentsTable();
        // add empty row
        AddEmptyAttachmentRow(ref dtAttachments);
        // save to viewstate
        ViewState["BidEventAttachments"] = dtAttachments;
        // bind to gridview
        gvFileAttachment.DataSource = dtAttachments;
        gvFileAttachment.DataBind();
        FillBidEventAttachments(ref dtAttachments);
        // save to viewstate
        ViewState["BidEventAttachments"] = dtAttachments;
        ViewState["BidEventExistingAttachments"] = dtAttachments;
        // bind to gridview
        gvFileAttachment.DataSource = dtAttachments;
        gvFileAttachment.DataBind();


    }

    private DataTable CreateAttachmentsTable()
    {
        DataTable dtAttachments = new DataTable();
        DataColumn dc;
        dc = new DataColumn("ID", typeof(System.Int32));
        dtAttachments.Columns.Add(dc);
        dc = new DataColumn("Original", typeof(System.String));
        dtAttachments.Columns.Add(dc);
        dc = new DataColumn("Actual", typeof(System.String));
        dtAttachments.Columns.Add(dc);
        dc = new DataColumn("Attached", typeof(System.Int32));
        dtAttachments.Columns.Add(dc);
        dc = new DataColumn("IsDetachable", typeof(System.Int32));
        dtAttachments.Columns.Add(dc);
        dc = new DataColumn("FileAttachment", typeof(System.String));
        dtAttachments.Columns.Add(dc);

        return dtAttachments;
    }

    private void AddEmptyAttachmentRow(ref DataTable dtAttachments)
    {
        DataRow dr = dtAttachments.NewRow();
        int nxtCounter = 0;
        if (dtAttachments.Rows.Count > 0)
            nxtCounter = int.Parse(dtAttachments.Rows[dtAttachments.Rows.Count - 1]["ID"].ToString()) + 1;

        dr["ID"] = nxtCounter;
        dr["Original"] = "";
        dr["Actual"] = "";
        dr["Attached"] = 0;
        dr["IsDetachable"] = 0;
        dr["FileAttachment"] = "";
        dtAttachments.Rows.Add(dr);
    }

    private void FillBidEventAttachments(ref DataTable dtAttachments)
    {
        IEnumerator iEnum = dsAttachments.Select(DataSourceSelectArguments.Empty).GetEnumerator();

        while (iEnum.MoveNext())
        {
            DataRow dr = dtAttachments.NewRow();
            DataRowView dRView = (DataRowView)iEnum.Current;
            dr["ID"] = dRView["FileUploadId"].ToString();
            dr["Original"] = dRView["OriginalFileName"].ToString();
            dr["Actual"] = dRView["ActualFileName"].ToString();
            dr["Attached"] = 1;
            dr["IsDetachable"] = 0;
            dr["FileAttachment"] = dRView["FileAttachment"].ToString();
            dtAttachments.Rows.Add(dr);
        }
    }

    protected void gvFileAttachment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Attach":
                {
                    if (gvFileAttachment.FooterRow != null)
                    {
                        FileUpload fu = (FileUpload)gvFileAttachment.FooterRow.FindControl("fileUpload");
                        Literal litMsg = (Literal)gvFileAttachment.FooterRow.FindControl("addAttachmentMsg");
                        if (fu.HasFile)
                        {
                            FileInfo fInfo = new FileInfo(fu.PostedFile.FileName);

                            if (!FileUploadHelper.IsFileForbidden(fInfo.Extension))
                            {
                                int fileSize = (fu.PostedFile.ContentLength + 512) / 1024;
                                int maxFileSize = int.Parse(ConfigurationManager.AppSettings["MaxFileSize"].ToString());

                                if (fileSize < maxFileSize)
                                {
                                    string original = fu.FileName.ToString().Trim();
                                    string actual = FileUploadHelper.GetAlternativeFileName(fInfo.Extension);

                                    try
                                    {
                                        fu.SaveAs((Constant.FILEATTACHMENTSFOLDERDIR) + actual);
                                        Attach(original, actual);
                                    }
                                    catch
                                    {
                                        litMsg.Text = BR + "&nbsp;&nbsp;&nbsp;" + BULLET + " File cannot be uploaded.";
                                    }
                                }
                                else
                                {
                                    litMsg.Text = BR + "&nbsp;&nbsp;&nbsp;" + BULLET + " File size exceeds limit(" + maxFileSize + "KB).";
                                }
                            }
                            else
                            {
                                litMsg.Text = BR + "&nbsp;&nbsp;&nbsp;" + BULLET + " File cannot be uploaded. The file type is forbidden to be uploaded.";
                            }
                        }
                    }
                } break;
            case "Remove":
                {
                    Remove(int.Parse(e.CommandArgument.ToString()));
                } break;
            case "Download":
                {
                    string[] args = e.CommandArgument.ToString().Split(new char[] { '|' });
                    string path = Constant.FILEATTACHMENTSFOLDERDIR;
                    FileHelper.DownloadFile(this.Page, path, args[0], args[1]);
                } break;
        }
    }

    private void DeleteExistingBidEventAttachments(int bidrefno)
    {
        try
        {
            DataTable dtAttachments = (DataTable)ViewState["BidEventExistingAttachments"];

            for (int i = 0; i < dtAttachments.Rows.Count; i++)
            {
                string fid = dtAttachments.Rows[i]["ID"].ToString();

                if (fid != "")
                {
                    DeleteBidEventAttachments(bidrefno, int.Parse(fid));
                    //DeleteAttachmentsFiles(int.Parse(fid));
                }
            }

        }
        catch
        {

        }

    }

    private void DeleteBidEventAttachments(int bidrefno, int fileuploadid)
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[0].Value = bidrefno;
            sqlParams[1] = new SqlParameter("@FileUploadID", SqlDbType.Int);
            sqlParams[1].Value = fileuploadid;
            sqlParams[2] = new SqlParameter("@Result", SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "sp_DeleteBidEventAttachments", sqlParams);

            sqlTransact.Commit();
            sqlConnect.Close();
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

    private bool SaveBidEventAttachments(int bidrefno)
    {
        bool isSuccessful = false;
        try
        {
            DataTable dtAttachments = (DataTable)ViewState["BidEventAttachments"];
            string s = string.Empty;

            for (int i = 1; i < dtAttachments.Rows.Count; i++)
            {
                s += SaveBidEventAttachments(bidrefno, int.Parse(Session[Constant.SESSION_USERID].ToString()), dtAttachments.Rows[i]["Original"].ToString(), dtAttachments.Rows[i]["Actual"].ToString());
            }
            isSuccessful = s.IndexOf("0") >= 0 ? false : true;
        }
        catch
        {
            isSuccessful = false;
        }
        return isSuccessful;
    }

    private string SaveBidEventAttachments(int bidrefno, int buyerid, string originalfilename, string actualfilename)
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;
        string isSuccessful = string.Empty;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[0].Value = bidrefno;
            sqlParams[1] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[1].Value = buyerid;
            sqlParams[2] = new SqlParameter("@OriginalFileName", SqlDbType.VarChar);
            sqlParams[2].Value = originalfilename;
            sqlParams[3] = new SqlParameter("@ActualFileName", SqlDbType.VarChar);
            sqlParams[3].Value = actualfilename;
            sqlParams[4] = new SqlParameter("@FileUploadID", SqlDbType.Int);
            sqlParams[4].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_Ebid_InsertBidAttachment", sqlParams);

            sqlTransact.Commit();

            int r = int.Parse(sqlParams[4].Value.ToString().Trim());
            isSuccessful = (r <= 0 ? "0" : "1");
        }
        catch
        {
            sqlTransact.Rollback();
            isSuccessful = "0";
        }
        finally
        {
            sqlConnect.Close();
        }
        return isSuccessful;
    }

    private void Attach(string original, string actual)
    {
        DataTable dtAttachments = (DataTable)ViewState["BidEventAttachments"];

        DataRow dr = dtAttachments.NewRow();
        int nxtCounter = 0;
        if (dtAttachments.Rows.Count > 0)
            nxtCounter = int.Parse(dtAttachments.Rows[dtAttachments.Rows.Count - 1]["ID"].ToString()) + 1;

        dr["ID"] = nxtCounter;
        dr["Original"] = original;
        dr["Actual"] = actual;
        dr["Attached"] = 1;
        dr["IsDetachable"] = 1;
        dr["FileAttachment"] = actual + "|" + original;
        dtAttachments.Rows.Add(dr);

        ViewState["BidEventAttachments"] = dtAttachments;
        gvFileAttachment.DataSource = dtAttachments;
        gvFileAttachment.DataBind();
    }

    private void Remove(int id)
    {
        DataTable dtAttachments = (DataTable)ViewState["BidEventAttachments"];
        int toBeRemoved = -1;
        string actualfilepath = Constant.FILEATTACHMENTSFOLDERDIR;

        for (int i = 0; i < dtAttachments.Rows.Count; i++)
        {
            if (dtAttachments.Rows[i]["ID"].ToString() == id.ToString())
            {
                toBeRemoved = i;
                actualfilepath += dtAttachments.Rows[i]["Actual"].ToString();
                break;
            }
        }
        if (toBeRemoved > -1)
        {
            // remove from datatable
            dtAttachments.Rows.RemoveAt(toBeRemoved);

            // remove the actual file
            FileInfo fInfo = new FileInfo(actualfilepath);
            if (fInfo.Exists)
                fInfo.Delete();
        }

        ViewState["BidEventAttachments"] = dtAttachments;

        gvFileAttachment.DataSource = dtAttachments;
        gvFileAttachment.DataBind();
    }

    protected bool IsAttached(string isattached)
    {
        return isattached == "1" ? true : false;
    }

    protected bool IsRemovable(string isattached, string isdetachable)
    {
        if ((isattached == "1") && (isdetachable == "1"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion


    protected void dvTenderDetails_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
    {

    }
}

