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
using EBid.lib.user.data;
using EBid.lib.user.trans;
using EBid.lib.auction.trans;
using EBid.lib;

public partial class web_vendorscreens_ConfirmedInvitations : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.VENDOR)
            Response.Redirect("../unauthorizedaccess.aspx");

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Confirmed Auction Invitations");

        Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;
    }

    protected void lnkAuctionEvent_Command(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewDetails")
        {
            Session[Constant.SESSION_AUCTIONREFNO] = e.CommandArgument;
            Response.Redirect("auctiondetails.aspx");
        }
    }
}
