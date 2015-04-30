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
using EBid.lib.auction.trans;
using EBid.lib.constant;
using EBid.lib;

public partial class web_onlineAuction_UpcomingAuctionEvents : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Upcoming Auction Events");

		if (!Page.IsPostBack)
		{
			if (Session["CONFIRMATIONMESSAGE"] != null)
			{
				lblMessage.Text = Session["CONFIRMATIONMESSAGE"].ToString();
				Session["CONFIRMATIONMESSAGE"] = null;
			}
		}

		gvAuctionEvents.Columns[4].Visible = IsVendor();		
    }

	public string Format(string date)
	{
		return FormattingHelper.FormatDateToString(Convert.ToDateTime(date));
	}

	public string ConfirmationStatus(string status)
	{
		if (status == "0")
			return "Deadline already reached.";
		else if (status == "1")
			return "Confirmed";
		else if (status == "2")
			return "Declined";
		else
			return "Participated";
	}

	public bool ShowConfirmation(string status, string deadlinereached)
	{
		if (deadlinereached == "1")
			return false;
		else
		{
			if (status == "0") // still pending
				return true;
			else
				return false;
		}
	}

	public bool IsVendor()
	{
		if (Session[Constant.SESSION_USERTYPE].ToString() != ((int)Constant.USERTYPE.VENDOR).ToString())
			return false;
		else
			return true;
	}

	protected void lblAction_Command(object sender, CommandEventArgs e)
	{
		Session["CurrentTab"] = "2";
		Response.Redirect("~/web/auctions/confirmauctionevent.aspx?aid=" + EncryptionHelper.Encrypt(e.CommandArgument.ToString()));
	}

	protected void lblAuctionEvents_Command(object sender, CommandEventArgs e)
	{
		if (e.CommandName.Equals("SelectAuctionItem"))
		{
			Session["CurrentTab"] = "2";
			Session[Constant.SESSION_AUCTIONREFNO] = e.CommandArgument.ToString();
			Response.Redirect("auctiondetails.aspx");
		}
	}
}
