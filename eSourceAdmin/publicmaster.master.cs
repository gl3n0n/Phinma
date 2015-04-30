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
using EBid.lib.bid.trans;
using EBid.lib.auction.data;
using EBid.lib.auction.trans;
using EBid.lib.auction.constant;
using EBid.lib.constant;
using EBid.lib.user.trans;
using System.IO;

public partial class publicmaster : System.Web.UI.MasterPage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["clientid"] != "")
        {
            string ImgUrl = "/clients/" + HttpContext.Current.Session["clientid"] + "/images/logo.jpg";
            string ImgUrlAbs = @"C:\\EBID\clients\" + HttpContext.Current.Session["clientid"] + @"\images\logo.jpg";
            if (File.Exists(ImgUrlAbs)) LogoImg.ImageUrl = ImgUrl;
        }
		if (Session[Constant.SESSION_USERID] != null)
		{
			if (Session[Constant.SESSION_USERID].ToString().Trim() == "")
			{
				lnkHome.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["LoginPage"] + "?client=" + HttpContext.Current.Session["client"];
			}
			else
			{
				switch (Session["userType"].ToString())
				{
					case "4":
						lnkHome.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["AdminHomePage"];
						break;
					default:
                        lnkHome.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["LoginPage"] + "?client=" + HttpContext.Current.Session["client"];
						break;
				}
			}
		}
		else
		{
            lnkHome.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["LoginPage"] + "?client=" + HttpContext.Current.Session["client"];
		}
    }	

	protected void lnkHome_Click(object sender, EventArgs e)
	{
		if (Session[Constant.SESSION_USERID] != null)
		{
			if (Session[Constant.SESSION_USERID].ToString().Trim() == "")
			{
				Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["LoginPage"]);
			}
			else
			{
				switch (Session["userType"].ToString())
				{					
					case "4":
						Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["AdminHomePage"]);
						break;
					default:
						Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["LoginPage"]);
						break;
				}
			}
		}
		else
		{
			Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["LoginPage"]);
		}
	}	
}
