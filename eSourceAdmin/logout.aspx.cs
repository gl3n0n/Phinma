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
using EBid.lib;

public partial class logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		FormsAuthenticationHelper.SignOut();
		//Response.Buffer= true;
		//Response.ExpiresAbsolute=DateTime.Now.AddDays(-1d);
		//Response.Expires =-1500;
		//Response.CacheControl = "no-cache";		
		//Response.AppendHeader("Refresh", "1; URL=login.aspx");
		//RegisterStartupScript("disableback", "<script language='text/javascript'><!-- javascript:window.history.forward(1); //--></script>");
	}
}
