using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;
using EBid.lib.bid.trans;
using EBid.lib.bid.data;
using EBid.lib.user.trans;
using EBid.lib.constant;
using EBid.lib.auction.data;
using EBid.lib;
using EBid.ConnectionString;

public partial class web_buyerscreens_ApprovedBidDetails : System.Web.UI.Page
{
    private string connstring = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();

        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");
        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Bid Event Details");
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());

        Session[Constant.SESSION_COMMENT_TYPE] = "0";

        if (Request.QueryString["brn"] != null)
        {
            Session[Constant.SESSION_BIDREFNO] = Request.QueryString["brn"].ToString();
        }

        if (Session[Constant.SESSION_BIDREFNO] == null)
            Response.Redirect("approvedbiditems.aspx");

        if (gvSuppliers.Rows.Count > 0)
        {
            CheckBox chkHeader = (CheckBox)gvSuppliers.HeaderRow.FindControl("chkHeader");
            chkHeader.Attributes["onclick"] = "ChangeAllCheckBoxStates(this.checked);";
            ClientScript.RegisterArrayDeclaration("CheckBoxIDs", String.Concat("'", chkHeader.ClientID, "'"));

            foreach (GridViewRow gr in gvSuppliers.Rows)
            {
                CheckBox chkRow = (CheckBox)gr.FindControl("chkRow");
                chkRow.Attributes["onclick"] = "ChangeHeaderAsNeeded();";
                ClientScript.RegisterArrayDeclaration("CheckBoxIDs", String.Concat("'", chkRow.ClientID, "'"));
            }
        }
       
        if (!Page.IsPostBack)
        {
            if (Session["Message"] != null)
            {
                lblMessage.Text = Session["Message"].ToString();
                Session["Message"] = null;
            }

            if (DateTime.Now >= Convert.ToDateTime(((Label)((DetailsView)Biddetails_details1.FindControl("dvEventDetails")).Rows[12].Cells[1].FindControl("lblDeadline")).Text))
            {
                btnSendEmailToVendors.Visible = false;
                gvSuppliers.Columns[0].Visible = false;
            }
        }
    }

    protected void lnkOk_Click(object sender, EventArgs e)
    {
        if (Session[Constant.SESSION_LASTPAGE] != null)
            Response.Redirect(Session[Constant.SESSION_LASTPAGE].ToString());
        else
            Response.Redirect("approvedbiditems.aspx");
    }

    protected void btnSendEmailToVendors_Click(object sender, EventArgs e)
    {
        try
        {
            ArrayList list = GetSelectedSuppliersFromGv();
            BidDetails details = GetBidItemDetails(int.Parse(Session["BidRefNo"].ToString()));
            int failedcount = 0, successcount = 0;

            if (list.Count > 0)
            {
                if (SendEmailInvitation(details, list, ref failedcount, ref successcount))
                {
                    if ((failedcount == 0) && (successcount > 0))
                    {
                        // success
                        Session["Message"] = (successcount == 1 ? "Invitation" : "Invitations") + " were sent successfully.";
                    }
                    else
                        // failed
                        Session["Message"] = "Failed to send " + (list.Count == 1 ? "invitation" : "invitations") + " to " + failedcount + " out of " + list.Count + (list.Count == 1 ? " recipient" : " recipients") + ". Please try again or contact adminitrator for assistance.";
                }
                else
                {
                    // failed
                    Session["Message"] = "Failed to send invitations. Please try again or contact adminitrator for assistance.";
                }
            }
            else
            {
                Session["Message"] = "No invitations sent. Please select suppliers from the given list.";
            }
        }
        catch
        {
            // failed
            Session["Message"] = "Failed to send invitations. Please try again or contact adminitrator for assistance.";
        }

         Response.Redirect("approvedbiddetails.aspx");
    }    

    #region Email
    private BidDetails GetBidItemDetails(int bidrefno)
    {
        DataTable dt = SqlHelper.ExecuteDataset(connstring, "sp_GetBidInvitationInfo", new SqlParameter[] { new SqlParameter("@BidRefNo", bidrefno) }).Tables[0];
        BidDetails item = new BidDetails();

        if (dt.Rows.Count > 0)
            item = new BidDetails(dt.Rows[0]);

        return item;
    }

    private ArrayList GetSelectedSuppliers()
    {
        ArrayList suppliersList = new ArrayList();

        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
        sqlParams[0].Value = Int32.Parse(Session["BidRefNo"].ToString());
        DataTable dt = new DataTable();

        dt = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "sp_GetEmailAddresses", sqlParams).Tables[0];

        for (int i = 0; i < dt.Rows.Count; i++)
        {            
            BidParticipant participant = new BidParticipant();
            participant.ID = int.Parse(dt.Rows[i]["VendorId"].ToString().Trim());
            participant.Name = dt.Rows[i]["VendorName"].ToString().Trim();
			participant.Username = dt.Rows[i]["UserName"].ToString().Trim();
            participant.EmailAddress = dt.Rows[i]["VendorEmail"].ToString().Trim();
            participant.MobileNo = dt.Rows[i]["MobileNo"].ToString().Trim();

            suppliersList.Add(participant);
        }

        return suppliersList;
    }

    private ArrayList GetSelectedSuppliersFromGv()
    {
        ArrayList suppliersList = new ArrayList();

        foreach (GridViewRow gr in gvSuppliers.Rows)
        {
            // TODO: Check if checkbox is checked, if yes, add supplier in the sendlist
            CheckBox chkRow = (CheckBox)gr.FindControl("chkRow");

            if (chkRow.Checked)
            {
                BidParticipant participant = new BidParticipant();
                int i = gr.DataItemIndex;

                participant.ID = int.Parse(((HiddenField)gvSuppliers.Rows[i].Cells[0].FindControl("hdnvendorid")).Value.ToString());
                participant.Name = ((HiddenField)gvSuppliers.Rows[i].Cells[0].FindControl("hdnname")).Value.ToString();
				participant.Username = ((HiddenField)gvSuppliers.Rows[i].Cells[0].FindControl("hdnusername")).Value.ToString();
                participant.EmailAddress = ((HiddenField)gvSuppliers.Rows[i].Cells[0].FindControl("hdnemail")).Value.ToString();
                participant.MobileNo = ((HiddenField)gvSuppliers.Rows[i].Cells[0].FindControl("hdnmobileno")).Value.ToString();

                suppliersList.Add(participant);
            }
        }

        return suppliersList;
    }

    private bool SendEmailInvitation(BidDetails biddetails, ArrayList recipients, ref int failedcount, ref int successcount)
    {
        bool success = false;
        string subject = "Trans-Asia  Incorporated/ Commnunications : Invitation to Bid";
        failedcount = 0;
        successcount = 0;

        try
        {
            for (int i = 0; i < recipients.Count; i++)
            {
                BidParticipant p = (BidParticipant)recipients[i];

                if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                        MailHelper.ChangeToFriendlyName(biddetails.Creator, biddetails.CreatorEmail),
                        MailHelper.ChangeToFriendlyName(p.Name, p.EmailAddress),
                        subject,
                        CreateInvitationBody(biddetails, p),
                        MailTemplate.GetTemplateLinkedResources(this)))
                {	// if sending failed					
                    failedcount++;
                    LogHelper.EventLogHelper.Log("Bid Event > Send Invitation : Sending Failed to " + p.EmailAddress, System.Diagnostics.EventLogEntryType.Error);
                }
                else
                {	// if sending successful
                    successcount++;
                    LogHelper.EventLogHelper.Log("Bid Event > Send Invitation : Email Sent to " + p.EmailAddress, System.Diagnostics.EventLogEntryType.Information);
                    
                    //add 1 to emailsent field based on vendorID and BidRefNo
                    SqlParameter[] sqlparams = new SqlParameter[2];
                    sqlparams[0] = new SqlParameter("@Vendorid", SqlDbType.Int);
                    sqlparams[0].Value = p.ID;
                    sqlparams[1] = new SqlParameter("@BidRefNo", SqlDbType.VarChar);
                    sqlparams[1].Value = Int32.Parse(Session["BidRefNo"].ToString());
                    SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "sp_BidInvitationAddEmailSent", sqlparams);   
                }
            }

            success = true;
        }
        catch (Exception ex)
        {
            success = false;
            LogHelper.EventLogHelper.Log("Bid Event > Send Invitation : " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
        }

        try
        {
            for (int j = 0; j < recipients.Count; j++)
            {
                BidParticipant p = (BidParticipant)recipients[j];

                if (SMSHelper.AreValidMobileNumbers(p.MobileNo.Trim()))
                {
                    SMSHelper.SendSMS(new SMSMessage(CreateInvitationSmsBody(biddetails, p).Trim(), p.MobileNo.Trim())).ToString();
                }
            }
        }
         
       catch (Exception ex)
        {
            LogHelper.EventLogHelper.Log("Bid Event > Send SMS Invitation : " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
        }

        return success;
    }

    private string CreateInvitationSmsBody(BidDetails biddetails, BidParticipant participant)
    {
        return String.Format("You are invited to participate in a bid event;Ref. No.:{0}, initiated by Trans-Asia . Deadline: {1}", biddetails.ID, biddetails.SubmissionDeadline.ToString("MM/dd/yyyy hh:mm:ss tt"));
    }
    
    private string CreateInvitationBody(BidDetails biddetails, BidParticipant participant)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<tr><td align='left'><h5>" + DateTime.Now.ToLongDateString() + "</h5></td></tr>");
        sb.Append("<tr><td align='left'><h3>INVITATION TO BID</h3></td></tr>");
        sb.Append("<tr>");
        sb.Append("<td valign='top'>");
        sb.Append("<p>");
        sb.Append("<b>TO: &nbsp;&nbsp;<u>" + participant.Name + "</u></b>");
        sb.Append("<br /><br />");
        sb.Append("Good Day!");
        sb.Append("<br /><br />");
        sb.Append("We are glad to inform you that you have been invited to participate in an online bidding event which was initiated by Trans-Asia");
        sb.Append("</p>");

        sb.Append("<table style='font-size: 12px;width:100%;'>");
        sb.Append("<tr>");
        sb.Append("<td width='10px'></td>");
        sb.Append("<td style='font-weight:bold;width:20px;'>1.</td>");
        sb.Append("<td style='font-weight:bold;'>Bid Description</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td width='30px' colspan='2'></td>");
        sb.Append("<td>" + biddetails.Description + "</td>");
        sb.Append("</tr>");
        sb.Append("<tr><td height='10px' colspan='3'></td></tr>");

        sb.Append("<tr>");
        sb.Append("<td width='10px'></td>");
        sb.Append("<td style='font-weight:bold;width:20px;'>2.</td>");
        sb.Append("<td style='font-weight:bold;'>Schedule of Bid</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td width='30px' colspan='2'></td>");
        sb.Append("<td>");
        sb.Append("Submission Deadline : " + biddetails.SubmissionDeadline + "<br />");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("<tr><td height='10px' colspan='3'></td></tr>");

        sb.Append("<tr>");
        sb.Append("<td width='10px'></td>");
        sb.Append("<td style='font-weight:bold;width:20px;'>3.</td>");
        sb.Append("<td style='font-weight:bold;'>Payment Details</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td width='30px' colspan='2'></td>");
        sb.Append("<td>");
        sb.Append("<ul>");
        sb.Append("<li>Payment Terms - indicate applicable terms.</li>");
        // Commented by Angel 10/22/2008 ::  requested by Sir Seth
        //sb.Append("<br />");
        //sb.Append("<li>Billing Details</li>");
        //sb.Append("<ul>");
        //sb.Append("<li>Contact Person: Rose Soteco T# 730 2413</li>");
        //sb.Append("<li>Contact Details: 2F GT Plaza Tower 1, Pioneer cor Madison Sts., Mandaluyong City</li>");
        //sb.Append("</ul>");
        sb.Append("</ul>");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("<tr><td height='10px' colspan='3'></td></tr>");

        sb.Append("<tr>");
        sb.Append("<td width='10px'></td>");
        sb.Append("<td style='font-weight:bold;width:20px;'>4.</td>");
        sb.Append("<td style='font-weight:bold;'>Bid Price Details</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td width='30px' colspan='2'></td>");
        sb.Append("<td>The bid price submitted by the supplier shall be exclusive of VAT.</td>");
        sb.Append("</tr>");
        sb.Append("<tr><td height='10px' colspan='3'></td></tr>");

        sb.Append("<tr>");
        sb.Append("<td width='10px'></td>");
        sb.Append("<td style='font-weight:bold;width:20px;'>5.</td>");
        sb.Append("<td style='font-weight:bold;'>Price Validity</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td width='30px' colspan='2'></td>");
        sb.Append("<td>");
        sb.Append("The price quoted must be valid and firm for a period of 90 days.");
        sb.Append("No change in price quoted shall be allowed after bid submission unless negotiated by Trans-Asia");
        sb.Append("</td.");
        sb.Append("</tr>");
        sb.Append("<tr><td height='10px' colspan='3'></td></tr>");

        sb.Append("<tr>");
        sb.Append("<td width='10px'></td>");
        sb.Append("<td style='font-weight:bold;width:20px;'>6.</td>");
        sb.Append("<td style='font-weight:bold;'>Price Confirmation</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td width='30px' colspan='2'></td>");
        sb.Append("<td>");
        sb.Append("Responses to the Invitation to Bid/Tender shall be sent by the vendors using the e-Sourcing Portal.");
        sb.Append("Price schedules (details) and other attachments shall be in Acrobat Format(i.e. PDF),"); ;
        sb.Append("or in any password-protected file (e.g. MS Word, Excel)");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("<tr><td height='10px' colspan='3'></td></tr>");

        sb.Append("<tr>");
        sb.Append("<td width='10px'></td>");
        sb.Append("<td style='font-weight:bold;width:20px;'>7.</td>");
        sb.Append("<td style='font-weight:bold;'>Grounds for Invalidating Bids</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td width='30px' colspan='2'></td>");
        sb.Append("<td>");
        sb.Append("A supplier's bid may be invalidated under any of the following circumstances:");
        sb.Append("<ul>");
        sb.Append("<li>Incomplete bid documents</li>");
        sb.Append("<li>Scanned Summary documents without bidder's signature</li>");
        sb.Append("</ul>");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("<tr><td height='10px' colspan='3'></td></tr>");

        sb.Append("<tr>");
        sb.Append("<td width='10px'></td>");
        sb.Append("<td style='font-weight:bold;width:20px;'>8.</td>");
        sb.Append("<td style='font-weight:bold;'>Awarding of Bid</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td width='30px' colspan='2'></td>");
        sb.Append("<td>");
        sb.Append("The lowest/highest bidder is not necessarily the winning bidder. Trans-Asia shall not be bound to assign any reason for not accepting any bid or accepting it in part.");
        sb.Append("Bids are still subject to further ecaluation. Trans-Asia shall award the winning supplier through a Purchase Order.");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("<tr><td height='10px' colspan='3'></td></tr>");

        sb.Append("<tr>");
        sb.Append("<td width='10px'></td>");
        sb.Append("<td style='font-weight:bold;width:20px;'>9.</td>");
        sb.Append("<td style='font-weight:bold;'>Penalties (depends on the items to be purchased)</td>");
        sb.Append("</tr>");
        sb.Append("<tr><td height='10px' colspan='3'></td></tr>");
        sb.Append("</table>");

        sb.Append("<p>");
        sb.Append("<br /><br />");
        sb.Append("######################################################################################<br />");
        sb.Append("&nbsp;Credentials:<br />");
        sb.Append("&nbsp;&nbsp;&nbsp;Username: " + participant.Username + "<br /><br />");        
        sb.Append("&nbsp;Notes:<br />");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Password is for login.<br />");
        sb.Append("&nbsp;&nbsp;&nbsp;Username is NOT CASE SENSITIVE.<br />");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;If you don't know or forgot your password, go to eBid login page and click forgot password.<br />");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Use the username provided. Click Send. Your password will be sent to this email address.<br />");
        sb.Append("######################################################################################<br />");
        sb.Append("<br /><br /><br />");
        sb.Append("Sincerely Yours,");
        sb.Append("<br /><br />");
        sb.Append(biddetails.Creator);
        sb.Append("<br /><br />");
        sb.Append("</p>");
        sb.Append("</td>");
        sb.Append("</tr>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }
    #endregion
}

