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
using EBid.lib.bid.data;
using EBid.lib.bid.trans;
using EBid.lib.auction.data;
using EBid.lib.auction.trans;
using EBid.lib.user.trans;
using EBid.lib.user.data;
using EBid.lib;

public partial class web_purchasing_screens_BidsForConversionDetails : System.Web.UI.Page
{
    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (!(Page.IsPostBack))
        {
            if (Session[Constant.SESSION_USERID] != null)
            {
                if (Request.QueryString[Constant.SESSION_BIDREFNO] != null)
                {
                    Session[Constant.SESSION_BIDREFNO] = Request.QueryString[Constant.SESSION_BIDREFNO].ToString().Trim();
                }

                if (Session[Constant.SESSION_BIDREFNO] != null)
                    ViewState[Constant.SESSION_BIDREFNO] = Session[Constant.SESSION_BIDREFNO].ToString().Trim();

                displayDetails();
            }
            
        }
    }

    private void displayDetails()
    {
//        BidItemTransaction obj = new BidItemTransaction();

        BidItem biditem = BidItemTransaction.QueryBidItemInfo(connstring, Session[Constant.SESSION_BIDREFNO].ToString().Trim());
        CategoryTransaction category = new CategoryTransaction();
        GroupDeptSecTransaction grp = new GroupDeptSecTransaction();
        CompanyTransaction cmp = new CompanyTransaction();
        IncotermTransaction inc = new IncotermTransaction();
        OtherTransaction dte = new OtherTransaction();

        lblCompany.Text = cmp.GetCompanyName(connstring, biditem.CompanyId.ToString().Trim());

        lblRequestor.Text = biditem.Requestor.ToString().Trim();
        lblPRNumber.Text = biditem.PRRefNo.ToString().Trim();
        lblPRDate.Text = biditem.PRDate.ToString().Trim();
        lblGroup.Text = grp.GetGroupDeptSecNameById(connstring, biditem.GroupDeptSec.ToString().Trim());
        lblBidReferenceNumber.Text = biditem.BidRefNo.ToString().Trim();
        lblSubCategory.Text = category.GetCategoryNameById(biditem.Category.ToString().Trim());
        lblBidSubmissionDeadline.Text = biditem.Deadline.ToString().Trim();
        lblBidItemDescription.Text = biditem.ItemDescription;
        lblDeliverTo.Text = biditem.DeliverTo;
        lblIncoterm.Text = inc.GetIncotermName(connstring, biditem.Incoterm);

        displayItemDetails();
        displaySuppliers();
    }

    private void displayItemDetails()
    {
        BidItemDetailTransaction bid = new BidItemDetailTransaction();
		gvBidItemDetails.DataSource = bid.GetBidItemDetails(connstring, ViewState[Constant.SESSION_BIDREFNO].ToString().Trim());
        gvBidItemDetails.DataBind();
    }

    private void displaySuppliers()
    {
        //BidItemTransaction vnd = new BidItemTransaction();

        gvInvitedSuppliers.DataSource = BidItemTransaction.GetSuppliers(connstring, ViewState[Constant.SESSION_BIDREFNO].ToString().Trim());
        // gvInvitedSuppliers.DataSource = dtVendors;
        gvInvitedSuppliers.DataBind();
    }

	protected void btnApprove_Click(object sender, EventArgs e)
	{
		UpdateBidForAuctionStatus(Constant.BID_STATUS_APPROVED_FOR_AUCTION);
	}

	protected void btnDeny_Click(object sender, EventArgs e)
	{
		UpdateBidForAuctionStatus(Constant.BID_STATUS_DENIED_FOR_CONVERSION);
	}

	private void UpdateBidForAuctionStatus(int vStatus) 
	{
		// get bid item
        BidItem bidItem = BidItemTransaction.QueryBidInfo(connstring, Session[Constant.SESSION_BIDREFNO].ToString());
		
		if (vStatus == Constant.BID_STATUS_APPROVED_FOR_AUCTION)
		{
            BidItemTransaction.UpdateBidForAuctionStatus(connstring, bidItem.BidRefNo.ToString(), vStatus);

			string dummyVar = Constant.BLANK;
			// save a copy to tblAuctionItem
            string vAuctionRefNo = "";
			AuctionTransaction auctionTransaction = new AuctionTransaction();
			auctionTransaction.InsertAuctionItem(connstring,
				bidItem.PRRefNo.ToString(),
				bidItem.Requestor,
				bidItem.ItemDescription,
				bidItem.BuyerId.ToString(),
				bidItem.GroupDeptSec.ToString(),
				bidItem.Category.ToString(),
                bidItem.SubCategory.ToString(),
				Constant.AUCTION_STATUS_APPROVED.ToString(),
				bidItem.DeliveryDate.ToString(),
				bidItem.CompanyId.ToString(),
				Constant.AUCTION_TYPE_REVERSE.ToString(),
				Constant.BLANK,
				Constant.BLANK,
				Constant.BLANK,
                bidItem.BidCurrency.ToString(),
				bidItem.BidRefNo.ToString(),
                bidItem.PRDate.ToString().Trim(),
                //Constant.BLANK,
                //Constant.BLANK,
                ref vAuctionRefNo);

			// copy the bid item details to auction item details
			BidItemDetailTransaction detailTransaction = new BidItemDetailTransaction();
			ArrayList arrBidItemDetails = detailTransaction.GetBidDetails(connstring, Session[Constant.SESSION_BIDREFNO].ToString());
			foreach (BidItemDetail bidDetail in arrBidItemDetails)
			{
				string vAuctionDetailNo = "";

				// insert auction detail to database
				auctionTransaction.InsertAuctionItemDetails(
					connstring,
					bidDetail.Item,
					bidDetail.DetailDesc,
					bidDetail.Qty.ToString(),
					bidDetail.UnitOfMeasure,
					Constant.AUCTION_STATUS_APPROVED.ToString(),
					vAuctionRefNo,
                    bidItem.Category.ToString(),
                    bidItem.SubCategory.ToString(),
                    "",
                    "",
                    "0",
                    "0",
					ref vAuctionDetailNo);
			}
		}

		Response.Redirect("bidsforconversion.aspx");
	}
}
