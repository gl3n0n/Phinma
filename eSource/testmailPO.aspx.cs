using System;
using System.Configuration;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EBid.lib;
using EBid.lib.constant;
using EBid.lib.report;
using EBid.lib.bid.trans;
using EBid.lib.bid.data;
using EBid.lib.auction.data;
using System.Text;
using System.Data.Common;

public partial class testmailPO : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {
        if (SendEmailNotification())
        {
            Response.Write("Mail sent successfully.");
        }
        else
        {
            Response.Write("Mail sent failed.");
        }
        //SendEmailNotificationReject("From Test", "fromTest@esource.com", "To Test", "rickylenon@gmail.com", "13412341234", "Test CompanyName", "1000999");
    }


    //############################################################
    //############################################################
    // SEND EMAIL NOTIFICATION TO VENDOR IF REJECTED

    private bool SendEmailNotification()
    {
        //"From Test", "fromTest@esource.com", "To Test", "rickylenon@gmail.com", "13412341234", "Test CompanyName", "1000999"
        //"Judith Mercado. Ponce" <email@globetel.com.ph>
        //"Consolacion G.. Lim" <email@globetel.com.ph>
        //string sCommand;
        //string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        SqlDataReader oReader;
        bool success = false;


        string fromName = "From TestName";
        string fromEmail = "noreply@globetel.com.ph";
        string from = '"' + "From TestName" + '"' + "<noreply@globetel.com.ph>";
        string toName = "To Testname";
        string toEmail = "rickylenon@gmail.com>";
        string to = '"' + "To Testname" + '"' + "<rickylenon@gmail.com>";
        string subject = "Test eMail";


        try
        {
            if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                    from,
                    to,
                    subject,
                    CreateNotificationBody(),
                    MailTemplate.GetTemplateLinkedResources(this)))
            {	//if sending failed					
                LogHelper.EventLogHelper.Log("Bid > Send Notification : Sending Failed to " + from, System.Diagnostics.EventLogEntryType.Error);
            }
            else
            {	//if sending successful
                LogHelper.EventLogHelper.Log("Bid > Send Notification : Email Sent to " + from, System.Diagnostics.EventLogEntryType.Information);

            }
            success = true;
        }
        catch (Exception ex)
        {
            success = false;
            LogHelper.EventLogHelper.Log("Bid > Send Notification : " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
        }

        return success;
    }
    private string CreateNotificationBody()
    {
        StringBuilder sb = new StringBuilder();

        //sb.Append("<tr><td align='right'><h5>" + DateTime.Now.ToLongDateString() + "</h5></td></tr>");
        sb.Append("<tr><td><p><strong>BAC for Approval</strong> <br> Sent: " + DateTime.Now.ToLongDateString() + "</p> <p>From: From TestName<br><br> To: To Testname<br><br> Subject: Test eMail<br><br> Dear Bid Award Approvers, <br><br> Re: Request for Bid Award Approval – <strong>Test email only.</strong><br><br> This is to request for your Bid Award Approval of the ff:<br><br> <b>Bid Reference Number:</b> 999999<br> <b>Bid Event Name:</b> Just a test email<br></p>  <p>Very truly yours,<br><br><br> <strong>e-Sourcing Procurement</strong></p><p>&nbsp;</p> <p><strong>Instructions:</strong></p> <ol> <li>Go to <a href='https://e-sourcing.Trans-Asia.com.ph/'>https://e-sourcing.Trans-Asia.com.ph</a></li> <li>Enter your Username and Password then  click Login</li> <li>Click Received Bid Events for Awarding</li> <li>Click Bid Events Name</li> <li>Review / Endorse / Approve Bid event  for Awarding</li> <li>Click Clarify if you have clarification  or click Approved to award Bid Events</li> </ol> Please do not reply to this auto-generated  message.&nbsp;</td></tr>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }
}