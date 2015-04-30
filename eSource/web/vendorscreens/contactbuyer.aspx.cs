using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Configuration;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EBid.lib;
using EBid.lib.bid.trans;
using EBid.lib.auction.trans;
using EBid.lib.constant;
using EBid.lib.user.data;
using EBid.lib.user.trans;
using EBid.lib.utils;
using EBid.ConnectionString;

namespace EBid.web.vendor_screens
{
    public partial class contactbuyer : System.Web.UI.Page
    {
        private string connstring = "";

        protected void Page_Load(object sender, System.EventArgs e)
        {
            connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
            FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
            if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.VENDOR)
                Response.Redirect("../unauthorizedaccess.aspx");

            if (!IsPostBack)
            {
                PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Contact Buyer");
                                
                if (Session["CB_Message"] != null)
                {
                    lblMessage.Text = Session["CB_Message"].ToString();
                    Session["CB_Message"] = null;
                }
                
                // TODO: check if default value is provided (CB_BuyerID, CB_Subject)

                //Session["CB_BuyerID"] = "189";
                //Session["CB_Subject"] = "Test Subject";
                if ((Session["CB_BuyerID"] != null) && (Session["CB_Subject"] != null))
                {
                    ddlRecipient.SelectedValue = Session["CB_BuyerID"].ToString();
                    txtSubject.Text = Session["CB_Subject"].ToString();
                    txtSubject.Enabled = ddlRecipient.Enabled = false;

                    Session["CB_BuyerID"] = null;
                    Session["CB_Subject"] = null;
                }               
            }
        }

        protected void lnkSend_Click(object sender, EventArgs e)
        {
            string smtpServer = ConfigurationManager.AppSettings["SMTPServer"].Trim();

            string from = MailHelper.ChangeToFriendlyName(Session[Constant.SESSION_USERFULLNAME].ToString(), Session[Constant.SESSION_USEREMAIL].ToString());

            try
            {
                MailHelper.SendEmail(smtpServer, from, GetEmailAddress(ddlRecipient.SelectedIndex), txtSubject.Text.Trim(), txtMessage.Text.Trim());

                Session["CB_Message"] = String.Format("Message to {0} was successfully sent.", ddlRecipient.SelectedItem.Text);
                Response.Redirect("contactbuyer.aspx");
            }
            catch
            {
                lblMessage.Text =String.Format("Sending of message to {0} was not successful. Please try again later.", ddlRecipient.SelectedItem.Text);
            }
        }

        private string GetEmailAddress(int id)
        {
            DataView dv = (DataView)sdsBuyers.Select(DataSourceSelectArguments.Empty);

            return dv.Table.Rows[id]["EmailAdd"].ToString();
        }
    }
}
