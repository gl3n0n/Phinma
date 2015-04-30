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

public partial class web_buyerscreens_AwardedAuctionEventDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (!(IsPostBack))
        {
            if (Request.QueryString["AuctionRefNo"] != null)
                Session["AuctionRefNo"] = Request.QueryString["AuctionRefNo"].ToString().Trim();

            if (Session["AuctionRefNo"] != null)
                hdnAuctionRefNo.Value = Session["AuctionRefNo"].ToString().Trim();
            //AuctionDetailNo
        }

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Auction Event Details");
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        Response.Redirect(Session[Constant.SESSION_LASTPAGE].ToString());
    }
    protected void btnAwarded_Click(object sender, EventArgs e)
    {
        Response.Redirect("awardedsuppliers.aspx");
    }
}
