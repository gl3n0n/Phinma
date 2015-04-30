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

public partial class web_onlineAuction_OngoingAuctionEvents : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
	{
		FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Ongoing Auction Events");

		if (!Page.IsPostBack)
		{
			if (Session["CONFIRMATIONMESSAGE"] != null)
			{
				lblMessage.Text = Session["CONFIRMATIONMESSAGE"].ToString();
				Session["CONFIRMATIONMESSAGE"] = null;
			}
		}
    }	

	protected void lblAuctionEvents_Command(object sender, CommandEventArgs e)
	{
		if (e.CommandName.Equals("SelectAuctionItem"))
		{
			Session["CurrentTab"] = "1";
			Session[Constant.SESSION_AUCTIONREFNO] = e.CommandArgument.ToString();
			Response.Redirect("auctiondetails.aspx");
		}
	}

	public bool ShowParticipateLink(string status)
	{
		if ((status == "1") || (status == "3"))
			return true;
		else
			return false;
	}

	public bool ShowConfirmDeclineLink(string status, string deadlinediff)
	{
		if (status == "0")
		{
			if (Int32.Parse(deadlinediff) <= 0)
				return true;
			else
				return false;
		}
		else
			return false;
	}	

	public string ConfirmationStatus(string status, string deadlinediff)
	{
		if (status == "0")
		{
			if (Int32.Parse(deadlinediff) <= 0)
				return "";
			else
				return "Deadline already reached";
		}
		else if (status == "2")
			return "Declined";
		else
			return "";
	}

	protected void lnkConfirm_Command(object sender, CommandEventArgs e)
	{
		Session["CurrentTab"] = "1";              
        string aid = FormattingHelper.EncryptQueryString(e.CommandArgument.ToString().Trim());
		Response.Redirect("~/web/auctions/confirmauctionevent.aspx?aid=" + aid);
	}    
 
    protected void gvAuctionEvents_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkARN = (LinkButton)e.Row.FindControl("lblAuctionEvents");
            HyperLink lnkParticipate = (HyperLink)e.Row.FindControl("lnkParticipate");
                        
            string qs = FormattingHelper.EncryptQueryString(lnkARN.CommandArgument);

            string url = String.Format("javascript:onclick=showWindow('onlineauctionpopup.aspx?qs={0}', '{1}');", qs, lnkARN.CommandArgument);
            lnkParticipate.NavigateUrl = url;
        }
    }
}