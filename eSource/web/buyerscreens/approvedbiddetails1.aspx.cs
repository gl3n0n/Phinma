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
using System.Data.SqlClient;
using EBid.lib.constant;
using EBid.lib.bid.data;
using EBid.lib.bid.trans;
using EBid.lib.user.trans;
using EBid.lib.user.data;
using EBid.lib.auction.data;
using EBid.lib;
using EBid.ConnectionString;

public partial class web_purchasing_screens_approvedbiddetails1 : System.Web.UI.Page
{
    DateTime _newDeadline;
    DateTime _currentDeadline;
    string connstring = "";

    protected void Page_Load(object sender, EventArgs e)
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

        if (Request.QueryString["brn"] != null)
        {
            Session["BidRefNo"] = Request.QueryString["brn"].ToString().Trim();
        }

        if (Session[Constant.SESSION_BIDREFNO] == null)
            Response.Redirect("approvedbidevents.aspx");

        if (!IsPostBack)
        {
            PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Approved Bid Details");
            InitializeControls();

            if (Session["MESSAGE"] != null)
            {
                lblMessage.Text = Session["MESSAGE"].ToString();
                Session["MESSAGE"] = null;
            }
        }

        if (QueryCountEndorsedBidTenders() != "0")
        {
            lnkShowHideChangeDeadline.Enabled = false;
        }

        SqlDataReader oReader;
        string query;
        SqlCommand cmd;
        SqlConnection conn;

        query = "SELECT * FROM tblBidItems WHERE BidRefno= " + Session["BidRefno"].ToString(); //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.Parameters.AddWithValue("@BidRefNo", Convert.ToInt32(Session["BidRefno"].ToString()));
                conn.Open(); oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        if (oReader["Status"].ToString() == "4")
                        {
                            lnkShowHideChangeDeadline.Visible = true;
                        }
                        else
                        {
                            lnkShowHideChangeDeadline.Visible = false;
                        }
                    }
                }
            }
        } 
    }

    private void InitializeControls()
    {
        txtDeadlineHH.Attributes.Add("onfocus", txtDeadlineHH.ClientID + ".select();");
        txtDeadlineMM.Attributes.Add("onfocus", txtDeadlineMM.ClientID + ".select();");
        txtDeadlineSS.Attributes.Add("onfocus", txtDeadlineSS.ClientID + ".select();");
        txtDeadlineHH.Attributes.Add("style", "text-align:center;");
        txtDeadlineMM.Attributes.Add("style", "text-align:center;");
        txtDeadlineSS.Attributes.Add("style", "text-align:center;");
        ddlDeadline.SelectedValue = "PM";

        clndrDeadline.Attributes.Add("style", "text-align:center;");

        txtDeadlineHH.Attributes.Add("onkeydown", "return DigitsOnly(event);");
        txtDeadlineMM.Attributes.Add("onkeydown", "return DigitsOnly(event);");
        txtDeadlineSS.Attributes.Add("onkeydown", "return DigitsOnly(event);");

        txtDeadlineHH.Attributes.Add("onfocusout", "ResetIfEmpty(this);");
        txtDeadlineMM.Attributes.Add("onfocusout", "ResetIfEmpty(this);");
        txtDeadlineSS.Attributes.Add("onfocusout", "ResetIfEmpty(this);");

        ResetValues();
    }

    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        if (Session[Constant.SESSION_LASTPAGE] != null)
            Response.Redirect(Session[Constant.SESSION_LASTPAGE].ToString());
        else
            Response.Redirect("bids.aspx");
    }

    protected void lnkShowHideChangeDeadline_Click(object sender, EventArgs e)
    {
        if (tblDeadline.Visible)
            tblDeadline.Visible = lnkChangeDeadline.Visible = false;
        else
        {
            Session["MESSAGE"] = null;
            lblMessage.Text = "";
            tblDeadline.Visible = lnkChangeDeadline.Visible = true;
            ResetValues();
        }
    }

    private void ResetValues()
    {
        _currentDeadline = GetBidEventSubmissionDeadline(Session[Constant.SESSION_BIDREFNO].ToString());

        clndrDeadline.Text = _currentDeadline.ToShortDateString();
        txtDeadlineHH.Text = (_currentDeadline.ToString("hh"));
        txtDeadlineMM.Text = _currentDeadline.Minute.ToString("00");
        txtDeadlineSS.Text = _currentDeadline.Second.ToString("00");
        ddlDeadline.SelectedValue = (_currentDeadline.ToString("tt"));
    }

    protected void cfvDeadline_ServerValidate(object source, ServerValidateEventArgs args)
    {
        string deadline = String.Format("{0} {1}:{2}:{3} {4}",
                    clndrDeadline.Text, txtDeadlineHH.Text,
                    txtDeadlineMM.Text, txtDeadlineSS.Text, ddlDeadline.SelectedValue);

        _currentDeadline = GetBidEventSubmissionDeadline(Session[Constant.SESSION_BIDREFNO].ToString());

        if (DateTime.TryParse(deadline, out _newDeadline))
        {
            if (_newDeadline > _currentDeadline)
                args.IsValid = true;
            else
            {
                cfvDeadline.ErrorMessage = "<br />&nbsp;&nbsp;The new bid submission deadline must be more than the current submission deadline.";
                args.IsValid = false;
            }
        }
        else
        {
            cfvDeadline.ErrorMessage = "<br />&nbsp;&nbsp;Invalid date or time. Please enter a valid date and time.";
            args.IsValid = false;
        }
    }

    protected void lnkChangeDeadline_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            string comment = String.Format("[Bid submission deadline changed from {0:D} {0:T} to {1:D} {1:T}] {2}", _currentDeadline, _newDeadline, txtComment.Text.Trim());

            if (SetNewBidEventSubmissionDeadline(Session[Constant.SESSION_BIDREFNO].ToString(),
                    Session[Constant.SESSION_USERID].ToString(), _newDeadline, comment))
            {
                Session["MESSAGE"] = String.Format("Bid submission deadline successfully changed from {0:D} {0:T} to {1:D} {1:T}.", _currentDeadline, _newDeadline);
                Response.Redirect("approvedbiddetails.aspx");
            }
            else
            {
                Session["MESSAGE"] = "Bid submission deadline was unsuccessfully changed.";
            }
        }
    }

    private DateTime GetBidEventSubmissionDeadline(string bidrefno)
    {
        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
        sqlParams[0].Value = int.Parse(bidrefno);

        return (DateTime)SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "sp_GetBidEventSubmissionDeadline", sqlParams);
    }

    private bool SetNewBidEventSubmissionDeadline(string bidrefno, string purchasingId, DateTime newDeadline, string comment)
    {
        SqlTransaction sqlTransact = null;
        SqlConnection sqlConn = new SqlConnection(connstring);
        bool success = false;

        try
        {
            sqlConn.Open();
            sqlTransact = sqlConn.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[1] = new SqlParameter("@Deadline", SqlDbType.DateTime);
            sqlParams[2] = new SqlParameter("@Comment", SqlDbType.NVarChar);
            sqlParams[3] = new SqlParameter("@PurchasingId", SqlDbType.Int);

            sqlParams[0].Value = int.Parse(bidrefno);
            sqlParams[1].Value = newDeadline;
            sqlParams[2].Value = comment;
            sqlParams[3].Value = int.Parse(purchasingId);

            SqlHelper.ExecuteNonQuery(sqlTransact, CommandType.StoredProcedure, "sp_SetBidEventSubmissionDeadline", sqlParams);
            sqlTransact.Commit();
            success = true;

            ArrayList list = GetSelectedSuppliers();
            BidDetails details = GetBidItemDetails(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()));

            if (list.Count > 0)
            {
                SendEmail_ChangeDeadline(details, list);
            }
        }
        catch
        {
            sqlTransact.Rollback();
            success = false;
        }
        finally
        {
            sqlConn.Close();
        }
        return success;
    }

    #region SEND EMAIL

    private ArrayList GetSelectedSuppliers()
    {
        ArrayList suppliersList = new ArrayList();

        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
        sqlParams[0].Value = Int32.Parse(Session[Constant.SESSION_BIDREFNO].ToString());
        DataTable dt = new DataTable();

        dt = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "sp_GetEmailAddresses", sqlParams).Tables[0];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            BidParticipant participant = new BidParticipant();
            participant.ID = int.Parse(dt.Rows[i]["VendorId"].ToString().Trim());
            participant.Name = dt.Rows[i]["VendorName"].ToString().Trim();
            participant.EmailAddress = dt.Rows[i]["VendorEmail"].ToString().Trim();
            participant.MobileNo = dt.Rows[i]["MobileNo"].ToString().Trim();

            suppliersList.Add(participant);
        }

        return suppliersList;
    }

    private bool SendEmail_ChangeDeadline(BidDetails biddetails, ArrayList recipients)
    {
        //String b_Company = ((Label)((DetailsView)Biddetails_details1.FindControl("dvEventDetails")).Rows[5].Cells[1].FindControl("lblCompany")).Text;

        bool success = false;
        string subject = "Trans-Asia / Commnunications : Changed Submission Deadline Notification";
        int failedcount = 0;
        int successcount = 0;
        
        try
        {
            #region NOTIFY VENDORS

            for (int i = 0; i < recipients.Count; i++)
            {
                BidParticipant p = (BidParticipant)recipients[i];

                if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                       MailHelper.ChangeToFriendlyName(Session[Constant.SESSION_USERFULLNAME].ToString(), Session[Constant.SESSION_USEREMAIL].ToString()),
                       MailHelper.ChangeToFriendlyName(p.Name, p.EmailAddress),
                       subject,
                       CreateNotificationBody(biddetails, p),
                       MailTemplate.GetTemplateLinkedResources(this)))
                {	// if sending failed					
                    failedcount++;
                    LogHelper.EventLogHelper.Log("Bids > Send Notification : Sending Failed to " + p.EmailAddress, System.Diagnostics.EventLogEntryType.Error);
                }
                else
                {	// if sending successful
                    successcount++;
                    LogHelper.EventLogHelper.Log("Bids > Send Notification : Email Sent to " + p.EmailAddress, System.Diagnostics.EventLogEntryType.Information);

                    #region add 1 to emailsent field based on vendorID and BidRefNo

                    //SqlParameter[] sqlparams = new SqlParameter[2];
                    //sqlparams[0] = new SqlParameter("@Vendorid", SqlDbType.Int);
                    //sqlparams[0].Value = p.ID;
                    //sqlparams[1] = new SqlParameter("@BidRefNo", SqlDbType.VarChar);
                    //sqlparams[1].Value = Int32.Parse(Session["BidRefNo"].ToString());
                    //SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "sp_BidInvitationAddEmailSent", sqlparams);
                    #endregion
                }
            }

            #region SMS SENDER
            try
            {
                for (int j = 0; j < recipients.Count; j++)
                {
                    BidParticipant BP = (BidParticipant)recipients[j];

                    if (SMSHelper.AreValidMobileNumbers(BP.MobileNo.Trim()))
                    {
                        SMSHelper.SendSMS(new SMSMessage(CreateInvitationSmsBody(biddetails, BP).Trim(), BP.MobileNo.Trim())).ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.EventLogHelper.Log("Bid Event > Send SMS Invitation : " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            #endregion

            #endregion

            #region NOTIFY BUYER

            if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                   MailHelper.ChangeToFriendlyName(Session[Constant.SESSION_USERFULLNAME].ToString(), Session[Constant.SESSION_USEREMAIL].ToString()),
                   MailHelper.ChangeToFriendlyName(biddetails.Creator, biddetails.CreatorEmail),
                   subject,
                   CreateNotificationBody(biddetails),
                   MailTemplate.GetTemplateLinkedResources(this)))
            {
                failedcount++;
                LogHelper.EventLogHelper.Log("Bids > Send Notification : Sending Failed to " + Session[Constant.SESSION_USEREMAIL].ToString(), System.Diagnostics.EventLogEntryType.Error);
            }
            else
            {
                successcount++;
                LogHelper.EventLogHelper.Log("Bids > Send Notification : Email Sent to " + Session[Constant.SESSION_USEREMAIL].ToString(), System.Diagnostics.EventLogEntryType.Information);
            }

            #endregion

            success = true;
        }
        catch (Exception ex)
        {
            success = false;
            LogHelper.EventLogHelper.Log("Bid Item > Send ??? Change Notice : " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
        }

        return success;
    }

    private string CreateInvitationSmsBody(BidDetails biddetails, BidParticipant participant)
    {
        //return String.Format("The Deadline for the: ", biddetails.Description, biddetails.ID, biddetails.SubmissionDeadline.ToString("MM/dd/yyyy hh:mm:ss tt"));

        DetailsView dv = Biddetails_details1.FindControl("dvEventDetails") as DetailsView;
        Label lblPreviousDeadline = dv.FindControl("lblDeadline") as Label;

        String textMessage = "‘Submission Deadline of Bid reference Number: " + biddetails.ID + " has been changed from " + Convert.ToDateTime(lblPreviousDeadline.Text).ToString("MMMM dd, yyyy, hh:mm tt") + " to " + biddetails.SubmissionDeadline.ToString("MMMM dd, yyyy, hh:mm tt") + ".’";

        return textMessage;
    }

    private string createNotificationSmsBody()
    {
        //String b_Company = ((Label)((DetailsView)Biddetails_details1.FindControl("dvEventDetails")).Rows[5].Cells[1].FindControl("lblCompany")).Text;

        return String.Format("You are invited to participate in an online bidding event[Ref No.:{0}] w/c was initiated by Trans-Asia . Please visit the portal.", "TODO:::SHOULD BE ID");
    }

    private string CreateNotificationBody(BidDetails biddetails, BidParticipant participant)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<table style='width: 100%'><tr><td style='width: 1px'></td><td style='width: auto' colspan=''></td><td style='width: 1px'></td></tr>");
        sb.Append("<tr><td style='width: auto; height: 635px'></td>");
        sb.Append("<td style='width: 100%; height: auto; text-align: justify;'>");
        sb.Append("<br />");
        sb.Append("<br />");
        sb.Append(DateTime.Now.ToLongDateString());
        sb.Append("<br /><br /><br /><strong>");
        sb.Append(participant.Name);
        sb.Append("<br /></strong>");
        sb.Append("<br />");
        sb.Append("<table style='width: 745px'>");
        sb.Append("<tr>");
        sb.Append("<td style='width: 50px'>");
        sb.Append("Attention &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; :</td>");
        sb.Append("<td style='width: 225px'>");
        sb.Append("<strong>");
        sb.Append(participant.Username);
        sb.Append("</strong></td></tr>");
        sb.Append("<tr>");
        sb.Append("<td style='width: 26px'>");
        sb.Append("</td>");
        sb.Append("<td style='width: 225px'>");
        sb.Append("</td></tr>");
        sb.Append("<tr>");
        sb.Append("<td style='width: 26px'>");
        sb.Append("</td>");
        sb.Append("<td style='width: 225px'>");
        sb.Append("</td></tr>");
        sb.Append("</table>");
        sb.Append("<br />");
        sb.Append("Re: ");
        sb.Append("&nbsp; &nbsp; &nbsp; &nbsp;&nbsp; ");
        sb.Append(String.Format("Changed Submission Deadline Notification : {0}", biddetails.Description));
        sb.Append("<br /><br />");
        sb.Append("Dear Sir/Madame:&nbsp;<br /><br />");
        sb.Append("We would like to inform you that the Submission Deadline &nbsp;for ");
        sb.Append(String.Format("{2} has been moved from {0:D} {0:T} to {1:D} {1:T}.<br />", _currentDeadline, _newDeadline, biddetails.Description));
        sb.Append("<br /><br /><br />");
        sb.Append("");
        sb.Append("Sincerely,");
        sb.Append("<br /><br />");
        sb.Append(Session[Constant.SESSION_USERFULLNAME].ToString());
        sb.Append("<br /><br /><br /><br /></td><td style='width: auto; height: auto'></td></tr><tr><td style='width: auto'></td><td style='width: auto'></td><td style='width: auto'></td></tr></table>");


        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }

    private string CreateNotificationBody(BidDetails biddetails)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<table style='width: 100%'><tr><td style='width: 1px'></td><td style='width: auto' colspan=''></td><td style='width: 1px'></td></tr>");
        sb.Append("<tr><td style='width: auto; height: 635px'></td>");
        sb.Append("<td style='width: 100%; height: auto; text-align: justify;'>");
        sb.Append("<br />");
        sb.Append("<br />");
        sb.Append(DateTime.Now.ToLongDateString());
        sb.Append("<br /><br /><br /><strong>");
        sb.Append(biddetails.Creator);
        sb.Append("<br /></strong>");
        sb.Append("<br />");
        sb.Append("<table style='width: 745px'>");
        sb.Append("<tr>");
        sb.Append("<td style='width: 26px'>");
        sb.Append("Attention &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; :</td>");
        sb.Append("<td style='width: 225px'>");
        sb.Append("<strong>");
        sb.Append(biddetails.Creator);
        sb.Append("</strong></td></tr>");
        sb.Append("<tr>");
        sb.Append("<td style='width: 26px'>");
        sb.Append("</td>");
        sb.Append("<td style='width: 225px'>");
        sb.Append("</td></tr>");
        sb.Append("<tr>");
        sb.Append("<td style='width: 26px'>");
        sb.Append("</td>");
        sb.Append("<td style='width: 225px'>");
        sb.Append("</td></tr>");
        sb.Append("</table>");
        sb.Append("<br />");
        sb.Append("Re: ");
        sb.Append("&nbsp; &nbsp; &nbsp; &nbsp;&nbsp; ");
        sb.Append(String.Format("Changed Submission Deadline Notification : {0}", biddetails.Description));
        sb.Append("<br /><br />");
        sb.Append("Dear Sir/Madame:&nbsp;<br /><br />");
        sb.Append("We would like to inform you that the Submission Deadline &nbsp;for ");
        sb.Append(String.Format("{2} has been moved from {0:D} {0:T} to {1:D} {1:T}.<br />", _currentDeadline, _newDeadline, biddetails.Description));
        sb.Append("<br /><br /><br />");
        sb.Append("");
        sb.Append("Sincerely,");
        sb.Append("<br /><br />");
        sb.Append(Session[Constant.SESSION_USERFULLNAME].ToString());
        sb.Append("<br /><br /><br /><br /></td><td style='width: auto; height: auto'></td></tr><tr><td style='width: auto'></td><td style='width: auto'></td><td style='width: auto'></td></tr></table>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }

    private BidDetails GetBidItemDetails(int bidrefno)
    {
        DataTable dt = SqlHelper.ExecuteDataset(connstring, "sp_GetBidInvitationInfo", new SqlParameter[] { new SqlParameter("@BidRefNo", bidrefno) }).Tables[0];
        BidDetails item = new BidDetails();

        if (dt.Rows.Count > 0)
            item = new BidDetails(dt.Rows[0]);

        return item;
    }

    #endregion

    protected void gvBidItemDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "withdraw":
                {
                    GridViewRow row = ((Control)e.CommandSource).NamingContainer as GridViewRow;
                    Panel tmp_pnlcommand = (Panel)row.FindControl("pnlLinks");
                    Panel tmp_pnlcomment = (Panel)row.FindControl("pnlComments");

                    tmp_pnlcommand.Visible = false;
                    tmp_pnlcomment.Visible = true;

                } break;
            case "continuewithdraw":
                {
                    if (BidItemTransaction.WithdrawBidItem(connstring, int.Parse(e.CommandArgument.ToString()), Constant.BIDITEM_STATUS.WITHDRAWAL_STATUS.WITHDRAWNED))
                    {
                        GridViewRow row = ((Control)e.CommandSource).NamingContainer as GridViewRow;
                        TextBox tmp_txtComment = (TextBox)row.FindControl("txtComment");
                        CheckBox tmp_chkBox = (CheckBox)row.FindControl("chkAlowVendorView");

                        InsertWithdrawnedItemComments(int.Parse(e.CommandArgument.ToString()), tmp_txtComment.Text.ToString().Trim(), Session[Constant.SESSION_USERID].ToString(), tmp_chkBox.Checked);
                        GetAllVendorsInfoByBidRef(int.Parse(e.CommandArgument.ToString()));
                        SendEmailToVendors();

                        #region Dev Richard - Send SMS Notification

                        ArrayList recipients = GetSelectedSuppliers();

                        GridViewRow lblItemRow = ((LinkButton)e.CommandSource).Parent.Parent as GridViewRow;
                        Label lblItem = (Label)gvBidItemDetails.Rows[row.RowIndex].FindControl("lblItem");

                        for (int a = 0; a < recipients.Count; a++)
                        {
                            BidParticipant p = (BidParticipant)recipients[a];
                            String mobileNo = p.MobileNo;

                            if (SMSHelper.AreValidMobileNumbers(mobileNo.Trim()))
                            {
                                String textMessage = "“We wish to inform you that '" + lblItem.Text + "' has been withdrawn. Kindly go to your portal account under Withdrawn bid Items for details.”";
                                
                                try
                                {
                                    SMSHelper.SendSMS(new SMSMessage(textMessage, mobileNo));
                                }
                                catch
                                { }
                            }
                        }

                        #endregion

                        Response.Redirect("withdrawnedbiditems.aspx");
                    }

                } break;
            case "cancelwithdraw":
                {
                    GridViewRow row = ((Control)e.CommandSource).NamingContainer as GridViewRow;
                    Panel tmp_pnlcommand = (Panel)row.FindControl("pnlLinks");
                    Panel tmp_pnlcomment = (Panel)row.FindControl("pnlComments");
                    TextBox tmp_txtComment = (TextBox)row.FindControl("txtComment");

                    tmp_pnlcommand.Visible = true;
                    tmp_pnlcomment.Visible = false;
                    tmp_txtComment.Text = "";

                } break;
        }
    }

    protected bool IsVisible(string status)
    {
        return (status.ToUpper() == "NONE");
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
        //String b_Company = ((Label)((DetailsView)Biddetails_details1.FindControl("dvEventDetails")).Rows[5].Cells[1].FindControl("lblCompany")).Text;

        DataTable dtVendors = (DataTable)ViewState["AllVendorsInBidDetailNo"];

        if (dtVendors.Rows.Count > 0)
        {
            for (int i = 0; i < dtVendors.Rows.Count; i++)
            {
                Response.Write(
                SendEmail(dtVendors.Rows[i]["VendorName"].ToString(),
                            dtVendors.Rows[i]["VendorEmail"].ToString(),
                            dtVendors.Rows[i]["VendorAddress"].ToString(),
                            dtVendors.Rows[i]["VendorAddress1"].ToString(),
                            dtVendors.Rows[i]["VendorAddress2"].ToString(),
                            dtVendors.Rows[i]["VendorAddress3"].ToString(),
                            dtVendors.Rows[i]["ContactPerson"].ToString(),
                            dtVendors.Rows[i]["TelephoneNo"].ToString(),
                            dtVendors.Rows[i]["Fax"].ToString(),
                             "Trans-Asia / Commnunications : Withdrawn Item").ToString()
                             )
                             ;
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
        String b_Event = ((Label)((DetailsView)Biddetails_details1.FindControl("dvEventDetails")).Rows[1].Cells[1].FindControl("lblItemDesc")).Text;
        String b_Item = ((Label)(gvBidItemDetails.Rows[0].Cells[0].FindControl("lblDescription"))).Text;

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
        sb.Append("Thank you for your interest to help Trans-Asia / Commnunications in finding a solution for ");
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
        sb.Append(Session[Constant.SESSION_USERFULLNAME].ToString());
        sb.Append("<br />");
        sb.Append("Head, Corporate Procurement Department");
        sb.Append("<br /><br /><br /><br /></td><td style='width: auto; height: auto'></td></tr><tr><td style='width: auto'></td><td style='width: auto'></td><td style='width: auto'></td></tr>");
        #endregion

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }
    #endregion

    private void InsertWithdrawnedItemComments(int detailNo, string comment, string userId, bool allowVendorView)
    {
        SqlParameter[] sqlParams = new SqlParameter[4];
        sqlParams[0] = new SqlParameter("@DetailNo", SqlDbType.Int);
        sqlParams[0].Value = detailNo;
        sqlParams[1] = new SqlParameter("@UserId", SqlDbType.Int);
        sqlParams[1].Value = Int32.Parse(userId);
        sqlParams[2] = new SqlParameter("@Comment", SqlDbType.NVarChar);
        sqlParams[2].Value = comment;
        sqlParams[3] = new SqlParameter("@AllowVendorView", SqlDbType.Int);

        if (!(allowVendorView))
        {
            sqlParams[3].Value = 0;
        }
        else
        {
            sqlParams[3].Value = 1;
        }

        //SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "sp_InsertBidItemDetailComments", sqlParams);
    }

    private string QueryCountEndorsedBidTenders()
    {

        SqlParameter[] sqlParams = new SqlParameter[2];
        sqlParams[0] = new SqlParameter("@PurchasingId", SqlDbType.Int);
        sqlParams[0].Value = int.Parse(Session[Constant.SESSION_USERID].ToString());
        sqlParams[1] = new SqlParameter("@BidRefNo", SqlDbType.Int);
        sqlParams[1].Value = int.Parse(Session[Constant.SESSION_BIDREFNO].ToString());

        return Convert.ToString(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "[sp_GetEndorsedTendersCount]", sqlParams));
    }
}
