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
using EBid.lib.bid.trans;
using EBid.lib.auction.trans;
using EBid.lib.bid.data;
using EBid.lib.user.trans;
using EBid.lib.constant;
using EBid.lib;

public partial class web_purchasing_screens_EndorsedConversionConfirmation : System.Web.UI.Page
{
    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
            Response.Redirect("../unauthorizedaccess.aspx");

        if(!IsPostBack)
        {
            if (Session[Constant.SESSION_USERID] != null)
            {
                if (Session[Constant.SESSION_BIDREFNO] != null)
                {
                    ViewState[Constant.SESSION_BIDTENDERNOS] = Session["BidTenderNos"].ToString().Trim();
                    ViewState[Constant.SESSION_BIDREFNO] = Session[Constant.SESSION_BIDREFNO].ToString().Trim();
                    ViewState[Constant.SESSION_USERTYPE] = Session[Constant.SESSION_USERTYPE].ToString().Trim();
                    ViewState[Constant.SESSION_USERID] = Session[Constant.SESSION_USERID].ToString().Trim();

                    InitializeHiddenFields();

                    string bidTenderNos = ViewState[Constant.SESSION_BIDTENDERNOS].ToString().Trim();
                    string bidRefNo = ViewState[Constant.SESSION_BIDREFNO].ToString().Trim();

                    lblBidRefNo.Text = bidRefNo;

                    DataTable dtEndorsed = PurchasingTransaction.QuerySelectedEndorsedItems(connstring, bidRefNo, bidTenderNos);
                    DataView dvEndorsed = new DataView(dtEndorsed);

                    gvEndorsedItems.DataSource = dvEndorsed;
                    gvEndorsedItems.DataBind();
                }
            }
            
        }
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
		if (Session[Constant.SESSION_BIDTENDERNOS] != null)
        {
            string bidTenderNos = Session[Constant.SESSION_BIDTENDERNOS].ToString().Trim();
			
            UpdateBidForAuctionStatus(Constant.BID_STATUS_APPROVED_FOR_AUCTION);
			Response.Redirect("bidsforeval.aspx");
        }
    }

	private void UpdateBidForAuctionStatus(int vStatus)
	{
		// Convert the bid item first before the tenders
		
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
                Constant.BLANK,
				Constant.AUCTION_STATUS_DRAFT.ToString(),
				bidItem.DeliveryDate.ToString(),
				bidItem.CompanyId.ToString(),
				Constant.AUCTION_TYPE_REVERSE.ToString(),
				Constant.BLANK,
				Constant.BLANK,
				Constant.BLANK,
				Constant.BLANK,
				bidItem.BidRefNo.ToString(),
				bidItem.PRDate.ToString().Trim(),
                //Constant.BLANK,
                //Constant.BLANK,
                ref vAuctionRefNo);
		}

		// convert and create a copy of the bid tender to auction tenders
		// start here
	}

    private void InitializeHiddenFields()
    {
        HiddenField hdnTenderStat = (HiddenField)TendersCommentBox.FindControl("hdnTenderStat");
        hdnTenderStat.Value = Constant.BID_TENDER_STATUS_CONVERTED.ToString().Trim();
        HiddenField hdnBidRefNo = (HiddenField)TendersCommentBox.FindControl("hdnBidRefNo");
        hdnBidRefNo.Value = ViewState[Constant.SESSION_BIDREFNO].ToString().Trim();
        HiddenField hdnUserType = (HiddenField)TendersCommentBox.FindControl("hdnUserType");
        hdnUserType.Value = ViewState[Constant.SESSION_USERTYPE].ToString().Trim();
        HiddenField hdnUserID = (HiddenField)TendersCommentBox.FindControl("hdnUserID");
        hdnUserID.Value = ViewState[Constant.SESSION_USERID].ToString().Trim();
    }
}