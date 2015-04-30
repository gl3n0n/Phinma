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
using EBid.lib.auction.trans;
using EBid.lib.auction.data;
using EBid.lib.bid.trans;
using EBid.lib.constant;
using EBid.lib;
using EBid.ConnectionString;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Text;
using EBid.ConnectionString;

public partial class web_buyerscreens_ApprovedAuctionDetails : System.Web.UI.Page
{
    private string connstring = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
	connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (Request.QueryString["arn"] != null)
        {
            Session["AuctionRefNo"] = Request.QueryString["arn"].ToString().Trim();
        }

        if (Session[Constant.SESSION_AUCTIONREFNO] == null)
            Response.Redirect("approvedauctionevents.aspx");
        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Auction Event Details");

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

            if (DateTime.Now >= Convert.ToDateTime(((Label)((DetailsView)Auctiondetail1.FindControl("dvEventDetails")).Rows[4].Cells[1].FindControl("lblAuctionEndTime")).Text))
            {
                btnSendEmailToVendors.Visible = false;
                gvSuppliers.Columns[0].Visible = false;
            }
        }

		if (!Page.IsPostBack)
		{
			if (Session["Message"] != null)
			{
				lblMessage.Text = Session["Message"].ToString();
				Session["Message"] = null;
			}

            Session[Constant.SESSION_COMMENT_TYPE] = "1";

		}
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (Session[Constant.SESSION_LASTPAGE] != null)
            Response.Redirect(Session[Constant.SESSION_LASTPAGE].ToString());
        else
            Response.Redirect("index.aspx");
    }
	    
	private ArrayList GetSelectedSuppliers()
	{
		ArrayList suppliersList = new ArrayList();

		foreach (GridViewRow gr in gvSuppliers.Rows)
		{
			// TODO: Check if checkbox is checked, if yes, add supplier in the sendlist
			CheckBox chkRow = (CheckBox)gr.FindControl("chkRow");

			if (chkRow.Checked)
			{

				int i = gr.DataItemIndex;

				AuctionParticipant participant = new AuctionParticipant();
				participant.ID = int.Parse(gvSuppliers.DataKeys[i].Values[0].ToString());
				participant.Username = gvSuppliers.DataKeys[i].Values[1].ToString();
				participant.EncryptedTicket = gvSuppliers.DataKeys[i].Values[2].ToString();
				participant.Alias = gvSuppliers.DataKeys[i].Values[3].ToString();
				participant.Name = gvSuppliers.DataKeys[i].Values[4].ToString();
				participant.EmailAddress = gvSuppliers.DataKeys[i].Values[5].ToString();
                participant.MobileNo = gvSuppliers.DataKeys[i].Values[6].ToString();

				suppliersList.Add(participant);
			}
		}

		return suppliersList;
	}

	private AuctionDetails GetAuctionItemDetails(int auctionrefno)
	{
		DataTable dt = SqlHelper.ExecuteDataset(connstring, "sp_GetAuctionInvitationInfo", new SqlParameter[] { new SqlParameter("@AuctionRefNo", auctionrefno) }).Tables[0];
		AuctionDetails item = new AuctionDetails();

		if (dt.Rows.Count > 0)
			item = new AuctionDetails(dt.Rows[0]);
		
		return item;
    }

    #region Email/SMS Invitation

    private bool SendEmailInvitation(AuctionDetails auctiondetails, ArrayList recipients, ref int failedcount, ref int successcount)
	{
		bool success = false;
        string subject = "Trans-Asia / Commnunications : Invitation to Auction";
		failedcount = 0;
		successcount = 0;

		try
		{
			for (int i = 0; i < recipients.Count; i++)
			{
				AuctionParticipant p = (AuctionParticipant)recipients[i];

				if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
						MailHelper.ChangeToFriendlyName(auctiondetails.Creator, auctiondetails.CreatorEmail),
						MailHelper.ChangeToFriendlyName(p.Name, p.EmailAddress),
						subject,
						CreateInvitationBody(auctiondetails, p),
						MailTemplate.GetTemplateLinkedResources(this)))
				{	// if sending failed					
					failedcount++;
					LogHelper.EventLogHelper.Log("Auction > Send Invitation : Sending Failed to " + p.EmailAddress, System.Diagnostics.EventLogEntryType.Error);
				}
				else
				{	// if sending successful
					successcount++;					
					LogHelper.EventLogHelper.Log("Auction > Send Invitation : Email Sent to " + p.EmailAddress, System.Diagnostics.EventLogEntryType.Information);
					// update sent mail count
					SqlHelper.ExecuteNonQuery(connstring, "sp_SendEmailInvitation", new SqlParameter[] { new SqlParameter("@ParticipantId", p.ID) });
				}
			}

			success = true;
		}
		catch(Exception ex)
		{
			success = false;
			LogHelper.EventLogHelper.Log("Auction > Send Invitation : " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
		}

        try
        {
            for (int j = 0; j < recipients.Count; j++)
            {
                AuctionParticipant p = (AuctionParticipant)recipients[j];
                
                if (SMSHelper.AreValidMobileNumbers(p.MobileNo.Trim()))
                {
                    SMSHelper.SendSMS(new SMSMessage(CreateSMSInvitationBody(auctiondetails, p).Trim(), p.MobileNo.Trim())).ToString();
                }
            }
        }
        catch (Exception ex)
        {
            LogHelper.EventLogHelper.Log("Auction > Send SMS Invitation : " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
        }

		return success;
	}

    private string CreateSMSInvitationBody(AuctionDetails auctiondetails, AuctionParticipant participant)
    {
        return String.Format("You are invited to participate in an auction event;Ref. No.:{0},initiated by Trans-Asia . Deadline: {2} Start Date: {1}", auctiondetails.ID,auctiondetails.StartDateTime.ToString("MM/dd/yyyy hh:mm tt"), auctiondetails.ConfirmationDeadline.ToString("MM/dd/yyyy"));
    }

	private string CreateInvitationBody(AuctionDetails auctiondetails, AuctionParticipant participant)
	{
		StringBuilder sb = new StringBuilder();
		
		sb.Append("<tr><td align='right'><h5>" + DateTime.Now.ToLongDateString() + "</h5></td></tr>");
		sb.Append("<tr><td align='center'><h3>INVITATION TO AUCTION</h3></td></tr>");
		sb.Append("<tr>");
			sb.Append("<td valign='top'>");
				sb.Append("<p>");
					sb.Append("<b>TO&nbsp&nbsp;:&nbsp&nbsp;<u>" + participant.Name + "</u></b>");
					sb.Append("<br /><br />");
					sb.Append("Good Day!");
					sb.Append("<br /><br />");
                    sb.Append("We are glad to inform you that you have been invited to participate in an online auction event which was initiated by Trans-Asia / Commnunications.");
				sb.Append("</p>");

				sb.Append("<table style='font-size: 12px;width:100%;'>");
				sb.Append("<tr>");
				sb.Append("<td width='10px'></td>");
				sb.Append("<td style='font-weight:bold;width:20px;'>1.</td>");
				sb.Append("<td style='font-weight:bold;'>Auction Description</td>");
				sb.Append("</tr>");
				sb.Append("<tr>");
				sb.Append("<td width='30px' colspan='2'></td>");
				sb.Append("<td>" + auctiondetails.Description + "</td>");
				sb.Append("</tr>");
				sb.Append("<tr><td height='10px' colspan='3'></td></tr>");

				sb.Append("<tr>");
				sb.Append("<td width='10px'></td>");
				sb.Append("<td style='font-weight:bold;width:20px;'>2.</td>");
				sb.Append("<td style='font-weight:bold;'>Schedule of Auction Event</td>");
				sb.Append("</tr>");
				sb.Append("<tr>");
				sb.Append("<td width='30px' colspan='2'></td>");
				sb.Append("<td>");
                sb.Append("Confirmation Deadline : " + FormattingHelper.FormatDateToString(auctiondetails.ConfirmationDeadline) + "<br />");
				sb.Append("Start Date & Time : " + FormattingHelper.FormatDateToLongString(auctiondetails.StartDateTime) +"<br />");
				sb.Append("End Date & Time : " + FormattingHelper.FormatDateToLongString(auctiondetails.EndDateTime) + "<br />");
				sb.Append("Duration : " + auctiondetails.Duration + "<br />");
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
				sb.Append("<li>Payment Terms - depends on the item to be purchased</li>");
				sb.Append("<br />");
				sb.Append("<li>Billing Details</li>");
				sb.Append("<ul>");
				sb.Append("<li>Contact Person: Rose Soteco T# 730 2413</li>");
				sb.Append("<li>Contact Details: 2F GT Plaza Tower 1, Pioneer cor Madison Sts., Mandaluyong City</li>");
				sb.Append("</ul>");
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
                sb.Append("No change in price quoted shall be allowed after bid submission unless negotiated by Trans-Asia / Commnunications.");
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
                sb.Append("All bidders shall e-mail the price breakdown in Acrobat Format (i.e. PDF) according to");
                sb.Append("the final bid price submitted during the auction event not later than 24 hours after the");
                sb.Append("event has ended.  The sum of the breakdown must be equal to the supplier’s final bid");
                sb.Append("price submitted during the actual auction event.  Any attempt to submit a breakdown");
                sb.Append("which totals significantly higher, or lower than the final bid price during the event");
                sb.Append("may incur sanctions from Trans-Asia / Commnunications.");
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
                sb.Append("<li>Late or non-transmittal of the price breakdown of the final auction tender</li>");
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
                sb.Append("The lowest/highest bidder is not necessarily the winning bidder. Trans-Asia / Commnunications shall not be bound to assign any reason for not accepting any bid or accepting it in part.");
                sb.Append("Bids are still subject to further ecaluation. Trans-Asia / Commnunications shall award the winning supplier through a Purchase Order/Sales Order.");
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
				sb.Append("To know more about this auction, click <a href='" + ConfigurationManager.AppSettings["ServerUrl"] + "web/auctions/auctiondetails.aspx?aid=" + HttpUtility.UrlEncode(EncryptionHelper.Encrypt(auctiondetails.ID.ToString())) + "' target='_blank'>here</a>. ");
				sb.Append("<br />");
				sb.Append("To confirm/decline your invitation, click <a href='" + ConfigurationManager.AppSettings["ServerUrl"] + "web/auctions/confirmauctionevent.aspx?aid=" + HttpUtility.UrlEncode(EncryptionHelper.Encrypt(auctiondetails.ID.ToString())) + "' target='_blank'>here</a>.");
				sb.Append("<br />");
				sb.Append("<a href='" + ConfigurationManager.AppSettings["ServerUrl"] + "rules.htm' target='_blank' title='Click here or copy the link'>Rules and Regulations</a> : " + ConfigurationManager.AppSettings["ServerUrl"] + "rules.htm");
				sb.Append("<br /><br />");
				sb.Append("######################################################################################<br />");
				sb.Append("&nbsp;Credentials:<br />");
				sb.Append("&nbsp;&nbsp;&nbsp;Username: " + participant.Username + "<br />");
				sb.Append("&nbsp;&nbsp;&nbsp;Ticket: " + EncryptionHelper.Decrypt(participant.EncryptedTicket) + "<br /><br />");				
				sb.Append("&nbsp;Notes:<br />");
				sb.Append("&nbsp;&nbsp;&nbsp;Ticket and password are CASE SENSITIVE.<br />");
				sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Ticket is for confirming/declining/participating an auction.<br />");
				sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Ticket is different for each supplier for each auction event.<br />");
				sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Password is for login.<br />");
				sb.Append("&nbsp;&nbsp;&nbsp;Username is NOT CASE SENSITIVE.<br />");
				sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;If you don't know or forgot your password, go to eBid login page and click forgot password.<br />");
				sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Use the username provided. Click Send. Your password will be sent to this email address.<br />");
				sb.Append("######################################################################################<br />");
				sb.Append("<br /><br /><br />");
				sb.Append("Sincerely Yours,");
				sb.Append("<br /><br />");
				sb.Append(auctiondetails.Creator);
				sb.Append("<br /><br />");
				sb.Append("</p>");
			sb.Append("</td>");
		sb.Append("</tr>");

		return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
	}
	
	protected void btnSendEmailToVendors_Command(object sender, CommandEventArgs e)
	{
		try
		{
			ArrayList list = GetSelectedSuppliers();
			AuctionDetails details = GetAuctionItemDetails(int.Parse(Session[Constant.SESSION_AUCTIONREFNO].ToString()));
			int failedcount = 0, successcount = 0;

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
		catch
		{
			// failed
			Session["Message"] = "Failed to send invitations. Please try again or contact adminitrator for assistance.";
		}

		Response.Redirect("approvedauctiondetails.aspx");
    }
    #endregion
}
