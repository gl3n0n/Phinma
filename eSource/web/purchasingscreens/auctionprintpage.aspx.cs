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
using EBid.lib.auction.data;
using EBid.lib.auction.trans;
using EBid.lib.bid.data;
using EBid.lib.bid.trans;
using EBid.lib.user.trans;
using EBid.lib.user.data;
using EBid.ConnectionString;

public partial class web_purchasingscreens_auctionprintpage : System.Web.UI.Page
{
    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (!(Page.IsPostBack))
        {
            if (Request.QueryString["AuctionRefNo"] != null)
            {
                ViewState[Constant.SESSION_AUCTIONREFNO] = Request.QueryString["AuctionRefNo"].ToString().Trim();
                DisplayDetails();

                if (Request.QueryString["AuctionEventType"] != null)
                {
                    int status = Int32.Parse(Request.QueryString["AuctionEventType"].ToString().Trim());
                    string auctLabel = "Auction Event";

                    switch (status)
                    {
                        case 0:
                            auctLabel = "Auction Draft";
                            break;
                        case 1:
                            auctLabel = "Submitted Auction Event";
                            break;
                        case 2:
                            auctLabel = "Rejected Auction Event";
                            break;
                        case 3:
                            auctLabel = "Auction Event For Re-Editing";
                            break;
                        case 4:
                            auctLabel = "Approved Auction Event";
                            break;
                        case 5:
                            auctLabel = "Cancelled Auction Event";
                            break;
                        case 6:
                            auctLabel = "Finished Auction Event";
                            break;
                    }

                    lblAuctEventType.Text = auctLabel;
                }
            }
        }
    }

    private void DisplayDetails()
    {
        AuctionItem auctionItem = AuctionItemTransaction.QueryAuctionItemInfo(ViewState[Constant.SESSION_AUCTIONREFNO].ToString().Trim());

        CategoryTransaction category = new CategoryTransaction();
        GroupDeptSecTransaction grp = new GroupDeptSecTransaction();
        CompanyTransaction cmp = new CompanyTransaction();

        OtherTransaction dte = new OtherTransaction();

        lblCompany.Text = cmp.GetCompanyName(connstring, auctionItem.CompanyId.ToString().Trim());

        lblRequestor.Text = auctionItem.Requestor.ToString().Trim();
        lblPRNumber.Text = auctionItem.PRRefNo.ToString().Trim();
        lblPRDate.Text = auctionItem.PRDate.ToString().Trim();
        lblGroup.Text = grp.GetGroupDeptSecNameById(connstring, auctionItem.GroupDeptSec.ToString().Trim());
        lblAuctionReferenceNumber.Text = auctionItem.AuctionRefNo.ToString().Trim();
        lblSubCategory.Text = category.GetCategoryNameById(connstring, auctionItem.Category.ToString().Trim());
        // lblDeadline.Text = auctionItem.Deadline.ToString().Trim();
        lblDeliveryDate.Text = auctionItem.DeliveryDate.ToString().Trim();
        lblItemDesc.Text = auctionItem.ItemDescription.ToString().Trim();

        lblAuctionType.Text = AuctionItemTransaction.GetAuctionTypeById(auctionItem.AuctionType.ToString().Trim());
        lblAuctionDeadline.Text = auctionItem.AuctionDeadline.ToString().Trim();
        lblAuctionDate.Text = auctionItem.AuctionStartDate.ToString().Trim();
        lblAuctionStartTime.Text = auctionItem.AuctionStartTime.ToString().Trim();
        lblAuctionEndTime.Text = auctionItem.AuctionEndTime.ToString().Trim();
        lblBidCurrency.Text = AuctionItemTransaction.getBidCurrency(auctionItem.BidCurrency.ToString().Trim());
    }
}
