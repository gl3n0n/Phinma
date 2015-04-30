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
using EBid.lib.bid.trans;
using EBid.lib.auction.trans;
using EBid.lib.user.trans;
using EBid.lib;

public partial class web_purchasing_screens_AwardAuctionItem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (!(Page.IsPostBack))
        {
            if (Session[Constant.SESSION_USERID] != null)
            {
                if (Session[Constant.SESSION_AUCTIONREFNO] != null)
                {
                    ViewState[Constant.SESSION_AUCTIONREFNO] = Session[Constant.SESSION_AUCTIONREFNO].ToString().Trim();

                    lnkAuctionItem.Text = "Auction Item " + ViewState[Constant.SESSION_AUCTIONREFNO].ToString().Trim();

                    //PurchasingTransaction auction = new PurchasingTransaction();

                    //DataTable dtAwardedItems = auction.QueryAwardedItemsbyBidRefNo(connstring, ViewState[Constant.SESSION_AUCTIONREFNO].ToString().Trim());
                    //DataView dvAwardedItems = new DataView(dtAwardedItems);

                    //gvAwardedItems.DataSource = dvAwardedItems;
                    //gvAwardedItems.DataBind();
                }
            }
           
        }
    }

    protected void lnkAuctionItem_Click(object sender, EventArgs e)
    {
        if (ViewState[Constant.SESSION_AUCTIONREFNO] != null)
        {
            Session[Constant.SESSION_AUCTIONREFNO] = ViewState[Constant.SESSION_AUCTIONREFNO].ToString().Trim();
            Response.Redirect("auctiondetails.aspx");
        }
    }

}
