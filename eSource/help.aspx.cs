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
using System.Text;
using System.Net.Mail;
using EBid.lib;
using EBid.lib.constant;

public partial class help : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            Title = String.Format(Constant.TITLEFORMAT, "Help");

        //lblClientInfo.Text = String.Format("You are logged on at: {0}", Request.UserHostAddress);        
    }    

    //protected void btnSend_Click(object sender, EventArgs e)
    //{
    //    if (SMSHelper.AreValidMobileNumbers(txtRecipients.Text.Trim()))
    //    {
    //        if (SMSHelper.SendSMS(new SMSMessage(txtMessage.Text.Trim(), txtRecipients.Text.Trim())))
    //            Response.Redirect("help.aspx");
    //    }
    //}

    //protected void btnCheckPassword_Click(object sender, EventArgs e)
    //{
    //    lblIsStrong.Text = PasswordChecker.IsStrongPassword(txtPassword.Text.Trim()) ? "Password is strong" : "Password is weak";
    //}
}
