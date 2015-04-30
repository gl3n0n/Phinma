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
using EBid.lib.auction.trans;
using EBid.lib.auction.data;
using EBid.lib.bid.trans;
using EBid.lib.constant;
using EBid.lib;
using EBid.ConnectionString;

public partial class web_buyerscreens_AuctionDetailsConfirmed : System.Web.UI.Page
{
    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (!(IsPostBack))
        {
            PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Auction Event Details");

            Session[Constant.SESSION_COMMENT_TYPE] = "1";
        }
    }

    //protected void btnCancel_Click(object sender, EventArgs e)
    //{
    //    AuctionTransaction a = new AuctionTransaction();
    //    a.CancelAuction(connstring, Session[Constant.SESSION_AUCTIONREFNO].ToString());
        
    //    // TODO: Send notification

    //    Response.Redirect("auctioncancel.aspx");
    //}

    protected void lnkBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("confirmedauctioninvitations.aspx");
    }
}
