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
using EBid.lib.user.trans;
using EBid.lib.user.data;
using EBid.ConnectionString;

public partial class web_purchasingscreens_bidprintpage : System.Web.UI.Page
{
    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (!(Page.IsPostBack))
        {
            if (Request.QueryString["BidRefNo"] != null)
            {
                ViewState[Constant.SESSION_BIDREFNO] = Request.QueryString["BidRefNo"].ToString().Trim();

                if (Request.QueryString["BidEventType"] != null)
                {
                    int status = Int32.Parse(Request.QueryString["BidEventType"].ToString().Trim());
                    string bidLabel = "Bid Event";

                    switch (status)
                    {
                        case 0:
                            bidLabel = "Bid Draft";
                            break;
                        case 1:
                            bidLabel = "Submitted Bid Event";
                            break;
                        case 2:
                            bidLabel = "Rejected Bid Event";
                            break;
                        case 3:
                            bidLabel = "Bid Event For Re-Editing";
                            break;
                        case 4:
                            bidLabel = "Approved Bid Event";
                            break;
                        case 5:
                            bidLabel = "Endorsed Bid Event";
                            break;
                        case 6:
                            bidLabel = "Bid Event for Renegotiation";
                            break;
                        case 7:
                            bidLabel = "Declined Bid Event";
                            break;
                        case 8:
                            bidLabel = "Cancelled Bid Event";
                            break;
                    }

                    lblBidEventType.Text = bidLabel;
                }
                displayDetails();
            }
        }
    }

    private void displayDetails()
    {
        if (ViewState[Constant.SESSION_BIDREFNO] != null)
        {
            BidItem biditem = BidItemTransaction.QueryBidItemInfo(connstring, ViewState[Constant.SESSION_BIDREFNO].ToString().Trim());
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
            lblSubCategory.Text = category.GetCategoryNameById(connstring, biditem.Category.ToString().Trim());
            lblBidSubmissionDeadline.Text = biditem.Deadline.ToString().Trim();
            lblBidItemDescription.Text = biditem.ItemDescription;
            lblDeliverTo.Text = biditem.DeliverTo;
            lblIncoterm.Text = inc.GetIncotermName(connstring, biditem.Incoterm);
        }
    }
}
