using System;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Collections;
using System.Configuration;

namespace EBid.lib
{
    public static class MailHelper
    {
        public static void SendEmail(string pSMTPServer, string pFrom, string pTo, string pSubject, string pBody)
        {
            SmtpClient smtpClient = new SmtpClient();
            MailMessage message = new MailMessage();

            MailAddress fromAddress = new MailAddress(pFrom);

            smtpClient.Host = pSMTPServer;
            smtpClient.Port = 25;
            message.From = fromAddress;

            message.To.Add(pTo);
            message.Subject = pSubject;

            message.IsBodyHtml = true;            
            message.Body = pBody;

            smtpClient.Send(message);
        }

		public static bool SendEmail(SmtpClient pSMTPServer, 
									string pFrom, string pTo, string pSubject, 
									string pHtmlBody, 
									LinkedResource[] linkresources)
		{
			bool success = false;
			try
			{
				MailMessage message = new MailMessage();

				// From address
				message.From = new MailAddress(pFrom);
				// To address
				message.To.Add(pTo);
				// Subject
				message.Subject = pSubject;

				// Html body text
				AlternateView htmlview = AlternateView.CreateAlternateViewFromString(pHtmlBody, null, "text/html");

                foreach (LinkedResource link in linkresources)
                {
                    //htmlview.LinkedResources.Add(link);
                }

				// Add view			
				message.AlternateViews.Add(htmlview);

                //var smtpClient = new SmtpClient();
                //smtpClient.Send(message);
                //pSMTPServer.EnableSsl = true;
				pSMTPServer.Send(message);

				success = true;
			}
			catch { success = false; }
			return success;
		}

		/// <summary>
		/// Creates email friendly names
		/// </summary>
		/// <param name="pName">GA Sacramento</param>
		/// <param name="pEmail">gsacramento@asiaonline.net.ph</param>
		/// <returns>"GA Sacramento" &lt;gsacramento@asiaonline.net.ph&gt;</returns>
		public static string ChangeToFriendlyName(string pName, string pEmail)
		{
			return "\"" + pName + "\" <" + pEmail + ">";
		}

		/// <summary>
		/// Creates one string of addresses from a list of addresses
		/// </summary>
		/// <param name="pRecipients">List of recipients addresses</param>
		/// <returns>gsacramento@asiaonline.net.ph;rcollantes@asiaonline.net.ph</returns>
		public static string ChangeToMultipleRecipients(ArrayList pRecipients)
		{
			StringBuilder recipients = new StringBuilder();

			for (int i = 0; i < pRecipients.Count; i++)
			{
				if (i != 0)
					recipients.Append(pRecipients[i].ToString() + ";");
				else
					recipients.Append(pRecipients[i].ToString());
			}

			return recipients.ToString();
		}
    }

	public static class MailTemplate
	{
		public static string IntegrateBodyIntoTemplate(string pBody)
		{
            //string parent = System.Web.HttpContext.Current;
			StringBuilder sb = new StringBuilder();
			sb.Append("<html>");
			sb.Append("<head>");
            sb.Append("<meta http-equiv='Content-Language' content='en-us'><meta http-equiv='Content-Type' content='text/html; charset=windows-1252'>");
            sb.Append("<style type='text/css'>");
            sb.Append("body{font-family:Arial;font-size:11px;margin:0px;background-color:#fff;height:250px;}");
            sb.Append("p{font:Arial;font-size:11px;text-align:left;padding-right:10px;padding-left:0px;}");
            sb.Append(".tabs a{display: visible;background-image:url(" + ConfigurationManager.AppSettings["ServerUrl"]+"web/themes/images/TabBGactive.jpg" + ");background-repeat:repeat-x;width:100%;color:#ffffff;font-weight:bold;font-size:11px;padding:10px;background-color:#E0DFE3;border-right:solid 1px #fff;border-top:solid 1px gray;border-bottom:solid 3px #fff;}");
            sb.Append(".tabs a:hover{background-image:url(" + ConfigurationManager.AppSettings["ServerUrl"] + "web/themes/images/TabBGhover.jpg" + ");background-repeat:repeat-x;font-size:11px;width: 100%;color: #000000;padding:10px;background-color:#778899;}");
            sb.Append(".activeTab a{background-image:url(" + ConfigurationManager.AppSettings["ServerUrl"] + "web/themes/images/TabBG.jpg" + ");background-repeat:repeat-x;width:100%;color:#fff;font-weight:bold;padding:10px;background-color:#3399CC;border-top:#778899;}");
            sb.Append(".activeTab a:hover{background-image: url(" + ConfigurationManager.AppSettings["ServerUrl"]+"web/themes/images/TabBGactive.jpg" + ");background-repeat: repeat-x;background-color: #3399cc;width: 100%;color: #ffffff;padding: 10px;border-top: #778899;}");
            sb.Append(".table {border-width: 0px;}");
            sb.Append("#page{position:absolute;width:100%;vertical-align:top;height:100%;border:solid 1px #999999;background-color:#FFFFFF;}");
            sb.Append("#content{background-image:url(" + ConfigurationManager.AppSettings["ServerUrl"] + "web/themes/images/contentCorner.jpg" + ");background-repeat:no-repeat;vertical-align:top;padding-left:20px;padding-right:10px;}");
            sb.Append("#content a{font-size:11px;color:#003399;}");
            sb.Append("#content a:hover{background-color:none;font-size:11px;text-decoration:none;}");
            sb.Append("#masthead{padding-top:10px;}");
            sb.Append("#masthead h1{font-size:16px;color:#3399CC;padding-left:20px;margin-bottom:-10px;}");
            sb.Append("#masthead a{font-size:11px;font-weight:bold;color:#cc0000;padding-left:2px;padding-right:2px;}");
            sb.Append("#footer{color:white;font-weight:bold;padding-top:10px;padding-left:10px;padding-bottom:10px;font-size:10px;border:solid 1px #999999;background-color:gray;}");
            sb.Append("#footer a{color:white;}");
            sb.Append("</style>");
			sb.Append("</head>");
			sb.Append("<body>");
				sb.Append("<div>");
					sb.Append("<table border='0' cellpadding='0' cellspacing='0' id='page'>");
						sb.Append("<tr>");
							sb.Append("<td valign='top' style='height: 135px'>");
								sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='100%'>");
                                sb.Append("<tr><td><div align='left' id='masthead'><table cellpadding='0' cellspacing='0' id='table1'><tr><td><h1><img border='0' src='" + ConfigurationManager.AppSettings["ServerUrl"] + "web/themes/images/logo_mailer.jpg" + "' ></h1><br /></td></tr></table></div></td></tr>");
                                    //sb.Append("<tr><td><div align='left' id='masthead'><table cellpadding='0' cellspacing='0' id='table1'><tr><td><h1><img border='0' src='" + System.Configuration.ConfigurationManager.AppSettings["ServerUrl"] + "web/images/logo.jpg' width='151' height='63'></h1><br /></td></tr></table></div></td></tr>");
                                    //sb.Append("<tr><td height='100%'><table cellpadding='0' cellspacing='0' width='100%' id='table2'><tr><td class='tabs'><a href='" + System.Configuration.ConfigurationManager.AppSettings["ServerUrl"] + "login.aspx' target='_blank' title='Trans-Asia EBid Home'>Home</a></td></tr></table></td></tr>");
								sb.Append("</table>");
							sb.Append("</td>");
						sb.Append("</tr>");
						sb.Append("<tr>");
							sb.Append("<td class='content' height='100%' style='padding-left: 15px; padding-right: 15px;'>");
								sb.Append("<table cellpadding='0' cellspacing='0' height='100%' width='100%'>");
								// CONTENT STARTS HERE
								sb.Append(pBody);
								// CONTENT ENDS HERE
								sb.Append("</table>");
							sb.Append("</td>");
						sb.Append("</tr>");
						sb.Append("<tr><td height='10px'></td></tr>");
						sb.Append("<tr>");
							sb.Append("<td id='footer' height='50px'>");
								sb.Append("Trans-Asia E-Sourcing System<br />Copyright © 2006 Trans-Asia , Inc. All rights reserved.");
							sb.Append("</td>");
						sb.Append("</tr>");
					sb.Append("</table>");
				sb.Append("</div>");
			sb.Append("</body>");
			sb.Append("</html>");

			return sb.ToString();
		}

		public static LinkedResource[] GetTemplateLinkedResources(System.Web.UI.Page parent)
		{
			LinkedResource[] resources = new LinkedResource[5];
            //resources[0] = new LinkedResource(parent.Server.MapPath("~/web/themes/images/logo.jpg"));
            //resources[0].ContentId = "logo";
            //resources[1] = new LinkedResource(parent.Server.MapPath("~/web/themes/images/TabBGactive.jpg"));
            //resources[1].ContentId = "activebg";
            //resources[2] = new LinkedResource(parent.Server.MapPath("~/web/themes/images/TabBGhover.jpg"));
            //resources[2].ContentId = "hoverbg";
            //resources[3] = new LinkedResource(parent.Server.MapPath("~/web/themes/images/TabBG.jpg"));
            //resources[3].ContentId = "bg";
            //resources[4] = new LinkedResource(parent.Server.MapPath("~/web/themes/images/contentCorner.jpg"));
            //resources[4].ContentId = "cornerbg";

			return resources;
		}

		/// <summary>
		/// Gets the default SMTP Server.
		/// This can be configured in the web.config file
		/// &lt;add key="SMTPServer" value="192.21.200.51" /&gt;
		/// &lt;add key="SMTPServerPort" value="25" /&gt;
		/// </summary>
		/// <returns></returns>
		public static SmtpClient GetDefaultSMTPServer()
		{
			SmtpClient client = new SmtpClient();
			client.Host = System.Configuration.ConfigurationManager.AppSettings["SMTPServer"];
			client.Port = int.Parse(System.Configuration.ConfigurationManager.AppSettings["SMTPServerPort"]);
			
			return client;
		}
	}
}
