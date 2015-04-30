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
using EBid.lib.bid.data;
using EBid.lib.user.trans;
using EBid.lib.constant;
using EBid.lib;
using EBid.ConnectionString;

public partial class WEB_buyer_screens_ConvertedToAuctionDetails : System.Web.UI.Page
{
    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (!(Page.IsPostBack))
        {
            Session[Constant.SESSION_COMMENT_TYPE] = "0";
            if (Request.QueryString["BidRefNo"] != null)
            {
                Session["BidRefNo"] = Request.QueryString["BidRefNo"].ToString().Trim();
            }

            BidItem bid = BidItemTransaction.QueryBidItemInfo(connstring, Request.QueryString["BidRefNo"].ToString().Trim());
            BuyerTransaction buyer = new BuyerTransaction();
            CategoryTransaction category = new CategoryTransaction();
            GroupDeptSecTransaction grp = new GroupDeptSecTransaction();
            CompanyTransaction cmp = new CompanyTransaction();
            VendorTransaction vnd = new VendorTransaction();
            IncotermTransaction inc = new IncotermTransaction();
            OtherTransaction obj = new OtherTransaction();

            lblCompany.Text =cmp.GetCompanyName(connstring, bid.CompanyId.ToString().Trim());
            lblRequestor.Text = bid.Requestor.ToString().Trim();
            lblPRNumber.Text = bid.PRRefNo.ToString().Trim();
            lblPRDate.Text = bid.PRDate.ToString().Trim();
            lblGroup.Text = grp.GetGroupDeptSecNameById(connstring, bid.GroupDeptSec.ToString().Trim());
            lblBidRefNo.Text = bid.BidRefNo.ToString().Trim();
            lblSubCategory.Text = category.GetCategoryNameById(connstring, bid.Category.ToString().Trim());
            lblBidSubmissionDeadline.Text = bid.Deadline.ToString().Trim();
            lblBidItemDesc.Text = bid.ItemDescription.ToString().Trim();
            lblDeliverTo.Text = bid.DeliverTo.ToString().Trim();
            lblIncoterm.Text = inc.GetIncotermName(connstring, bid.Incoterm.ToString().Trim());

            DataTable dtSuppliers = BidItemTransaction.GetSuppliers(connstring, Session["BidRefNo"].ToString().Trim());
            gvSuppliers.DataSource = dtSuppliers;
            gvSuppliers.DataBind();

            DataTable dtFileAttachment = new DataTable();
            DataColumn dcol1 = new DataColumn("FILES", typeof(System.String));
            dtFileAttachment.Columns.Add(dcol1);

            string Files = bid.FileAttachments;
            string[] Files1 = Files.Split(Convert.ToChar("|"));

            for (int i = 0; i < Files1.Length; i++)
            {
                //Create a new row
                DataRow drow = dtFileAttachment.NewRow();
                drow["FILES"] = Files1[i];
                dtFileAttachment.Rows.Add(drow);
            }

            gvFileAttachments.DataSource = dtFileAttachment;
            gvFileAttachments.DataBind();
        }
    }
    
    protected void lnkOk_Click(object sender, EventArgs e)
    {
		Response.Redirect("index.aspx");
    }
}
