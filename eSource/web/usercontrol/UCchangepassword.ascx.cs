using System;
using System.Web;
using EBid.ConnectionString;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EBid.lib.utils;
using EBid.lib.user.trans;
using EBid.lib.constant;
using EBid.lib;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Net.Mail;
using EBid;
using EBid.ConnectionString;

public partial class web_usercontrol_UCchangepassword : System.Web.UI.UserControl
{
    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (!IsPostBack)
        {
            // show confirmation message
            if (Session["Message"] != null)
            {
                lblMessage.Text = Session["Message"].ToString();
                Session["Message"] = null;
            }
        }
    }
    protected void lnkchange_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int userid, usertype;
            string username = Session[Constant.SESSION_USERNAME].ToString();

            int.TryParse(Session[Constant.SESSION_USERID].ToString(), out userid);
            int.TryParse(Session[Constant.SESSION_USERTYPE].ToString(), out usertype);

            // change password
            if (UserTransaction.ChangePasswordAndAuthenticate(connstring, userid, EncryptionHelper.Encrypt(txtConfirmPwd.Text.Trim()), true, Session[Constant.SESSION_USERNAME].ToString()))
            {
                Session["Message"] = "Password successfully changed.<br /><br />";
                
                // redirect user
                Response.Redirect("changepwd.aspx");
            }
        }
    }
    protected void cvVerifyPasswords_ServerValidate(object source, ServerValidateEventArgs args)
    {
        // check if current password is correct
        if (EncryptionHelper.Encrypt(args.Value) == Session[Constant.SESSION_PASSWORD].ToString())
        {
            // check if new password is a strong password
            cvVerifyPasswords.ErrorMessage = "Password is weak.<br />";
            args.IsValid = (PasswordChecker.IsStrongPassword(txtNewPwd.Text.Trim()));
        }
        else
        {
            cvVerifyPasswords.ErrorMessage = "Invalid current password.<br />";
            args.IsValid = false;
        }        
    }

    private void RedirectUser()
    {
        switch (int.Parse(Session[Constant.SESSION_USERTYPE].ToString()))
        {
            case 1:
                Response.Redirect(ConfigurationManager.AppSettings["BuyerHomePage"]);
                break;
            case 2:
                Response.Redirect(ConfigurationManager.AppSettings["VendorHomePage"]);
                break;
            case 3:
                Response.Redirect(ConfigurationManager.AppSettings["PurchasingHomePage"]);
                break;
            case 5:
                Response.Redirect(ConfigurationManager.AppSettings["BidsOpeningCommitteeHomePage"]);
                break;  
			case 6:
                Response.Redirect(ConfigurationManager.AppSettings["BidsAwardingCommitteeHomePage"]);
                break;           
        }        
    }

    protected void lnkBack_Click(object sender, EventArgs e)
    {
        RedirectUser();
    }
}
