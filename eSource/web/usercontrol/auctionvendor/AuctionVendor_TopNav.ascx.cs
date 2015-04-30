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

public partial class AuctionVendor_TopNav : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
		// TODO: check session
		if (Session["CurrentTab"] != null)
		{
			//Response.Write(Session["CurrentTab"].ToString());
			switch(Session["CurrentTab"].ToString())
			{
				case "1": tdOngoing.Attributes.Add("class", "activeTab"); break;
				case "2": tdUpcoming.Attributes.Add("class", "activeTab"); break;
				case "3": tdFinished.Attributes.Add("class", "activeTab"); break;
				default: tdUpcoming.Attributes.Add("class", "activeTab"); break;
			}
		}
		else
			tdUpcoming.Attributes.Add("class", "activeTab");
    }

	protected void lnkHome_Click(object sender, EventArgs e)
	{
		switch (Session["userType"].ToString())
		{
			case "1":
				{
					Response.Redirect("~/web/buyerscreens/index.aspx");
				} break;
			case "2":
				{
					Response.Redirect("~/web/vendorscreens/index.aspx");
				} break;
			case "3":
				{
					Response.Redirect("~/web/purchasingscreens/index.aspx");
				} break;
		}
	}
	protected void lnkOngoingAuctions_Click(object sender, EventArgs e)
	{
		Session["CurrentTab"] = "1";
		Response.Redirect("ongoingauctionevents.aspx");
	}
	protected void lnkUpcomingAuctions_Click(object sender, EventArgs e)
	{
		Session["CurrentTab"] = "2";
		Response.Redirect("upcomingauctionevents.aspx");
	}
	protected void lnkFinishedAuctions_Click(object sender, EventArgs e)
	{
		Session["CurrentTab"] = "3";
		Response.Redirect("finishedauctionevents.aspx");
	}	
}
