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
using EBid.lib;

public partial class web_purchasing_screens_bidDetailsGeneric : System.Web.UI.Page
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
                if (Request.QueryString["BidRefNo"] != null)
                {
                    Session[Constant.SESSION_BIDREFNO] = Request.QueryString["BidRefNo"].ToString().Trim();
                }

                if (Session[Constant.SESSION_BIDREFNO] != null)
                    ViewState[Constant.SESSION_BIDREFNO] = Session[Constant.SESSION_BIDREFNO].ToString().Trim();

                hdBidRefNo.Value = ViewState[Constant.SESSION_BIDREFNO].ToString().Trim();

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

            hdBidEventType.Value = biditem.BidStatus.ToString().Trim();

            displayItemDetails();
            displaySuppliers();
        }
    }

    private void displayItemDetails()
    {
        BidItemDetailTransaction bid = new BidItemDetailTransaction();
        gvBidItemDetails.DataSource = bid.GetBidItemDetails(connstring, ViewState[Constant.SESSION_BIDREFNO].ToString().Trim());
        gvBidItemDetails.DataBind();
    }

    private void displaySuppliers()
    {
        gvInvitedSuppliers.DataSource = BidItemTransaction.GetSuppliers(connstring, ViewState[Constant.SESSION_BIDREFNO].ToString().Trim());
        gvInvitedSuppliers.DataBind();
    }
}
