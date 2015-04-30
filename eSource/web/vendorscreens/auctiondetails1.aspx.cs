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
using EBid.lib.auction.data;
using EBid.lib.bid.trans;
using EBid.lib;
using EBid.ConnectionString;

public partial class web_vendorscreens_AuctionDetails1 : System.Web.UI.Page
{
    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.VENDOR)
            Response.Redirect("../unauthorizedaccess.aspx");

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Auction Details");

        if (Request.QueryString["arn"] != null)
        {
            Session["AuctionRefNo"] = Request.QueryString["arn"].ToString().Trim();
        }

        if (!(Page.IsPostBack))
        {
            if (Session[Constant.SESSION_USERID] == null)
            {
                Session.Abandon();
                Session.Clear();
                FormsAuthentication.SignOut();

                string returnUrl = string.Empty;
                if (Request.RawUrl.Trim() != "")
                    returnUrl = "?ReturnUrl=" + Request.RawUrl.Trim().Replace("~/", "");
                Response.Redirect(FormsAuthentication.LoginUrl + returnUrl);
            }
            else
            {
                Session[Constant.SESSION_COMMENT_TYPE] = "1";

                if (Request.QueryString["AuctionRefno"] != null)
                    Session[Constant.SESSION_AUCTIONREFNO] = Request.QueryString["AuctionRefno"].ToString().Trim();

                if (Session[Constant.SESSION_AUCTIONREFNO] != null)
                    ViewState[Constant.SESSION_AUCTIONREFNO] = Session[Constant.SESSION_AUCTIONREFNO].ToString().Trim();

                displayDetails();
            }
        }
    }

    private void displayDetails()
    {
        if (Request.QueryString[Constant.SESSION_AUCTIONREFNO] != null)
        {
            Session[Constant.SESSION_AUCTIONREFNO] = Request.QueryString[Constant.SESSION_AUCTIONREFNO];
        }

        AuctionItem auctionItem = AuctionItemTransaction.QueryAuctionItemInfo(Session[Constant.SESSION_AUCTIONREFNO].ToString().Trim());

        CategoryTransaction category = new CategoryTransaction();

        lblAuctionReferenceNumber.Text = auctionItem.AuctionRefNo.ToString().Trim();
        lblSubCategory.Text = category.GetCategoryNameById(auctionItem.Category.ToString().Trim());
        lblDeliveryDate.Text = auctionItem.DeliveryDate.ToString().Trim();
        lblItemDesc.Text = auctionItem.ItemDescription.ToString().Trim();
        lblAuctionDeadline.Text = auctionItem.AuctionDeadline.ToString().Trim();
        lblAuctionDate.Text = auctionItem.AuctionStartDate.ToString().Trim();
        lblAuctionStartTime.Text = auctionItem.AuctionStartTime.ToString().Trim();
        lblAuctionEndTime.Text = auctionItem.AuctionEndTime.ToString().Trim();

        DisplayItemDetails(auctionItem.BidRefNo.ToString().Trim());
    }

    private void DisplayItemDetails(string bidRefNo)
    {
        BidItemDetailTransaction bid = new BidItemDetailTransaction();
        gvAuctionItemDetails.DataSource = bid.GetBidItemDetails(connstring, bidRefNo);
        gvAuctionItemDetails.DataBind();
    }
}
