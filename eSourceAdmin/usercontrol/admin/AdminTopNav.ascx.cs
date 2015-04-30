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
using EBid.lib.constant;
using System.IO;

public partial class web_user_control_GlobalLinksNav : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
		// Check if session still alive
		if ((Session[Constant.SESSION_USERID] == null) || (String.IsNullOrEmpty(Session[Constant.SESSION_USERTYPE].ToString())))
			Response.Redirect(FormsAuthentication.LoginUrl);

        if (HttpContext.Current.Session["clientid"] != "")
        {
            string ImgUrl = "/clients/" + HttpContext.Current.Session["clientid"] + "/images/logo.jpg";
            string ImgUrlAbs = @"C:\\WWW\ESOURCE_TRANS\clients\" + HttpContext.Current.Session["clientid"] + @"\images\logo.jpg";
            if (File.Exists(ImgUrlAbs)) LogoImg.ImageUrl = ImgUrl;
        }
    }
    
    protected void btnSignOut_Click(object sender, EventArgs e)
    {
		Session.Abandon();
		Session.Clear();
        FormsAuthentication.SignOut();
		Response.Redirect(FormsAuthentication.LoginUrl);
    }
}
