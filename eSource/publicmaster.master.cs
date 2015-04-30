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



    protected void Page_Init(object sender, EventArgs e)
    {
        this.Page.LoadComplete += new EventHandler(Page_LoadComplete);
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {

        if (HttpContext.Current.Session["clientid"] != null)
        {
            stylesheet.Href = "~/web/themes/" + HttpContext.Current.Session["configTheme"].ToString() + "/css/style.css";
            stylesheet2.Href = "~/web/themes/" + HttpContext.Current.Session["configTheme"].ToString() + "/css/style_front.css";
        }
        else
        {
            stylesheet.Href = "~/web/themes/default/css/style.css";
            stylesheet2.Href = "~/web/themes/default/css/style_front.css";
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
					case "1":
						Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["BuyerHomePage"]);
						break;
					case "2":
						Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["VendorHomePage"]);
						break;
					case "3":
						Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["PurchasingHomePage"]);
						break;
					case "4":
						Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["AdminHomePage"]);
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
