using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using EBid.lib.constant;
using EBid.lib.auction.data;
using EBid.lib.auction.trans;
using EBid.lib.bid.data;
using EBid.lib.bid.trans;
using EBid.lib.user.trans;
using EBid.lib.user.data;
using EBid.lib;
using EBid.ConnectionString;

public partial class web_purchasing_screens_auctionDetails : System.Web.UI.Page
{
    private static string connstring = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (Request.QueryString["arn"] != null)
        {
            Session["AuctionRefNo"] = Request.QueryString["arn"].ToString().Trim();
        }

        if (Session[Constant.SESSION_AUCTIONREFNO] == null)
            Response.Redirect("auctions.aspx");

        if (!(Page.IsPostBack))
        {
            lnkExport.Attributes.Add("onclick", "window.open('../reports/auctiondetails.aspx?arn=' + " + Session[Constant.SESSION_AUCTIONREFNO].ToString() + " , 'x', 'toolbar=no, menubar=no, width=600; height=800, top=80, left=80, resizable=yes, scrollbars=yes');");

            PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Auction Event Details");

        }
        if (Session[Constant.SESSION_LASTPAGE] != null)
        {
            if (Session[Constant.SESSION_LASTPAGE].ToString() == "~/web/purchasingscreens/auctioninvitations.aspx")
            {
                pnlComments.Visible = true;
                lnkCancelAuction.Visible = true;
                lnkCancelAuction.Enabled = IsEnabled();
                if (!IsEnabled())
                {
                    lnkCancelAuction.OnClientClick = "javascript:void(0)";
                }
            }
        }

    }    

    protected void lnkBack_Click(object sender, EventArgs e)
    {
        if (Session[Constant.SESSION_LASTPAGE] != null)
            Response.Redirect(Session[Constant.SESSION_LASTPAGE].ToString());
        else
            Response.Redirect("auctions.aspx");
    }
    protected void lnkCancelAuction_Click(object sender, EventArgs e)
    {
        if (Session[Constant.SESSION_AUCTIONREFNO] != null)
        {
            AuctionItemTransaction.UpdateAuctionStatus(connstring, Session[Constant.SESSION_AUCTIONREFNO].ToString(),
                Session[Constant.SESSION_USERID].ToString(),
                Constant.AUCTION_STATUS_CANCELLED, txtComment.Text.Trim());

            try
            {
                ArrayList list = GetSelectedSuppliers(int.Parse(Session[Constant.SESSION_AUCTIONREFNO].ToString()));
                AuctionDetails details = GetAuctionItemDetails(int.Parse(Session[Constant.SESSION_AUCTIONREFNO].ToString()));
                int failedcount = 0, successcount = 0;
                SendEmailInvitation(details, list, ref failedcount, ref successcount);
            }
            catch
            {
                // failed
                //Session["Message"] = "Failed to send invitations. Please try again or contact adminitrator for assistance.";
            }

            Response.Redirect("auctioninvitations.aspx");
        }        
    }

   private bool IsEnabled()
    {
       String sDate = ((Label)((DetailsView)Auctiondetail1.FindControl("dvEventDetails")).Rows[0].Cells[0].FindControl("Label3")).Text;
       
       DateTime datenow = DateTime.Now;
       DateTime rdeadline = DateTime.Parse(sDate);

       SqlParameter[] sqlParams = new SqlParameter[1];
       sqlParams[0] = new SqlParameter("@AuctionRefNo", SqlDbType.Int);
       sqlParams[0].Value = Int32.Parse(Session[Constant.SESSION_AUCTIONREFNO].ToString());

       int  status = int.Parse(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "sp_GetAuctionStatus", sqlParams).ToString().Trim());

       if ((DateTime.Compare(datenow, rdeadline) < 0) & !(status > 4))
       {
           return true;
       }
       else
       {
           return false;
       }

    }

    private ArrayList GetSelectedSuppliers(int vAuctionRefNo)
    {
        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@AuctionRefNo", SqlDbType.Int);
        sqlParams[0].Value = vAuctionRefNo;

        DataTable dtParticipants = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "sp_GetAuctionParticipants", sqlParams).Tables[0];

        ArrayList suppliersList = new ArrayList();

        foreach (DataRow dr in dtParticipants.Rows)
        {
            AuctionParticipant participant = new AuctionParticipant();
            participant.ID = int.Parse(dr["ParticipantId"].ToString());
            participant.Username = dr["Username"].ToString();
            participant.EncryptedTicket = dr["Ticket"].ToString();
            participant.Alias = dr["Alias"].ToString();
            participant.Name = dr["VendorName"].ToString();
            participant.EmailAddress = dr["VendorEmail"].ToString();
            participant.EmailSent = int.Parse(dr["EmailSent"].ToString());
            participant.MobileNo = dr["MobileNo"].ToString();

            suppliersList.Add(participant);
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

    #region Email
    private bool SendEmailInvitation(AuctionDetails auctiondetails, ArrayList recipients, ref int failedcount, ref int successcount)
    {
        bool success = false;
        string subject = "Trans-Asia / Commnunications : Auction Event Cancelled";
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
                        CreateCancelAuctionBody(auctiondetails, p),
                        MailTemplate.GetTemplateLinkedResources(this)))
                {	// if sending failed					
                    failedcount++;
                    LogHelper.EventLogHelper.Log("Auction > Cancel Auction : Sending Failed to " + p.EmailAddress, System.Diagnostics.EventLogEntryType.Error);
                }
                else
                {	// if sending successful
                    successcount++;
                    LogHelper.EventLogHelper.Log("Auction > Cancel Auction : Email Sent to " + p.EmailAddress, System.Diagnostics.EventLogEntryType.Information);
                    // update sent mail count
                    SqlHelper.ExecuteNonQuery(connstring, "sp_SendEmailInvitation", new SqlParameter[] { new SqlParameter("@ParticipantId", p.ID) });
                }

                //if (SMSHelper.AreValidMobileNumbers(p.MobileNo.Trim()))
                //{
                //    SMSHelper.SendSMS(new SMSMessage(CreateSMSInvitationBody(auctiondetails, p).Trim(), p.MobileNo.Trim())).ToString();
                //}
            }
            success = true;
        }
        catch (Exception ex)
        {
            success = false;
            LogHelper.EventLogHelper.Log("Auction > Cancel Auction : " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
        }

        return success;
    }

    //private string CreateSMSInvitationBody(AuctionDetails auctiondetails, AuctionParticipant participant)
    //{
    //    return String.Format("You are invited to participate in an auction event;Ref. No.:{0}, initiated by Trans-Asia . Start Date: {1}", auctiondetails.ID, auctiondetails.StartDateTime.ToString("MM/dd/yyyy hh:mm:ss tt"));
    //}

    private string CreateCancelAuctionBody(AuctionDetails auctiondetails, AuctionParticipant participant)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<tr><td style='width: 1px'></td><td style='width: auto' colspan=''></td><td style='width: 1px'></td></tr>");
        sb.Append("<tr><td style='width: auto; height: 635px'></td>");
        sb.Append("<td style='width: 100%; height: auto; text-align: justify;'>");
        sb.Append("<br /><br /><br />");
        sb.Append("" + DateTime.Now.ToLongDateString() + "");
        sb.Append("<br /><br /><br /><strong>");
        sb.Append(participant.Name);
        sb.Append("<br /></strong>");
        sb.Append("<br /><br />");
        sb.Append("<table style='width: 100%'><tr><td style='width: 12px'>");
        sb.Append("Auction Event:");
        sb.Append("</td><td style='width: auto'>");
        sb.Append(auctiondetails.Description);
        sb.Append("</td></tr></table>");
        sb.Append("<br /><br />");
        sb.Append("Dear Sir/Madame:");
        sb.Append("<br /><br />");
        sb.Append("Thank you for confirming your participation on  Trans-Asia / Commnunications Auction Invitation for ");
        sb.Append("" + auctiondetails.Description + ", that is scheduled to start on ");
        sb.Append("" + Convert.ToDateTime(auctiondetails.StartDateTime).ToLongDateString() + " " + Convert.ToDateTime(auctiondetails.StartDateTime).ToShortTimeString() + ", We regret to inform you, ");
        sb.Append("however, that the subject Auction event has been cancelled.");
        sb.Append("We will keep in mind your cooperation and commitment in helping us on this endeavor.");
        sb.Append("<br /><br />");
        sb.Append("We sincerely appreciate the time and effort you dedicated for the completion ");
        sb.Append("of your response and we look forward to working with you again in the future.");
        sb.Append("<br /><br /><br />");
        sb.Append("Sincerely,");
        sb.Append("<br /><br /><br /><br />");
        sb.Append(auctiondetails.Sender);
        sb.Append("<br /><br /><br /><br /></td><td style='width: auto; height: auto'></td></tr><tr><td style='width: auto'></td><td style='width: auto'></td><td style='width: auto'></td></tr>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }

    #endregion
}
