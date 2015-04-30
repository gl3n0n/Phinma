using System;
using System.Collections;
using System.Data;
using System.Configuration;
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
using EBid.lib;

public partial class web_onlineAuction_AuctionDetails : System.Web.UI.Page
{

	protected void Page_Load(object sender, EventArgs e)
	{
		FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Auction Details");

		// Get status of auctionrefno
		if (!String.IsNullOrEmpty(Request.QueryString["aid"]))
		{
			string q = string.Empty;
			try
			{
				q = EncryptionHelper.Decrypt(Request.QueryString["aid"]);
			}
			catch
			{ }
			Session[Constant.SESSION_AUCTIONREFNO] = q;
		}
	}

	public string Format(string date)
	{
		return FormattingHelper.FormatDateToString(Convert.ToDateTime(date));
	}
	
	protected void lnkOk_Click(object sender, EventArgs e)
	{
		if (Session["CurrentTab"] != null)
		{
			switch (Session["CurrentTab"].ToString())
			{
				case "1": Response.Redirect("ongoingauctionevents.aspx"); break;
				case "2": Response.Redirect("upcomingauctionevents.aspx"); break;
				case "3": Response.Redirect("finishedauctionevents.aspx"); break;
				default: Response.Redirect("ongoingauctionevents.aspx"); break;
			}
		}
	}
}