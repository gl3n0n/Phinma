using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EBid.lib;
using EBid.lib.constant;
using EBid.lib.auction.trans;

public partial class web_vendorscreens_awardedAuctions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.VENDOR)
            Response.Redirect("../unauthorizedaccess.aspx");

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Awarded Auction Items");

        Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;
    }

    protected void lnkAuctionEvent_Command(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "ViewEventDetails":
                {
                    Session[Constant.SESSION_AUCTIONREFNO] = e.CommandArgument;
                    Response.Redirect("auctiondetails.aspx");
                } break;
            case "ViewItemDetails":
                {
                    Session[Constant.SESSION_AUCTIONDETAILNO] = e.CommandArgument;
                    Response.Redirect("awardedauctiondetails.aspx");
                } break;
        }
    }
}
