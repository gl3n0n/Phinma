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
using EBid.lib.constant;
using EBid.lib.user.trans;

public partial class publicmaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
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
					case "1":
						Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["BuyerHomePage"]);
						break;
					case "2":
						Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["VendorHomePage"]);
						break;
					case "3":
						Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["PurchasingHomePage"]);
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
