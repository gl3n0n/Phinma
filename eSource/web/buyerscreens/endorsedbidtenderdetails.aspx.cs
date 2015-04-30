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

public partial class WEB_buyer_screens_SentBidItemsForRenegotiationDetails : System.Web.UI.Page
{
    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (Request.QueryString["brn"] != null)
        {
            Session[Constant.SESSION_BIDREFNO] = Request.QueryString["brn"].ToString();
        }

        if (!(Page.IsPostBack))
        {
			if (Request.QueryString[Constant.SESSION_BIDREFNO] != null)
            {
				Session[Constant.SESSION_BIDREFNO] = Request.QueryString[Constant.SESSION_BIDREFNO].ToString().Trim();
            }

			if (Session["ORDERBY"] == null)
			{
				Session["ORDERBY"] = "DESC";
			}

            BidItem bid = BidItemTransaction.QueryBidItemInfo(connstring, Session[Constant.SESSION_BIDREFNO].ToString());
            BuyerTransaction buyer = new BuyerTransaction();
            CategoryTransaction category = new CategoryTransaction();
            GroupDeptSecTransaction grp = new GroupDeptSecTransaction();
            CompanyTransaction cmp = new CompanyTransaction();
            VendorTransaction vnd = new VendorTransaction();
            IncotermTransaction inc = new IncotermTransaction();
            OtherTransaction obj = new OtherTransaction();

            lblBidReferenceNumber.Text = bid.BidRefNo.ToString().Trim();
            
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

            DisplayDetails();
			Bind();
        }
    }

    private void DisplayDetails()
    {
        BidItemDetailTransaction biddetail = new BidItemDetailTransaction();
        ArrayList biddetailarray = biddetail.GetBidDetails(connstring, Session[Constant.SESSION_BIDREFNO].ToString().Trim());
        OtherTransaction dte = new OtherTransaction();
        foreach (BidItemDetail b in biddetailarray)
        {
            HtmlTableRow row = new HtmlTableRow();
            HtmlTableCell[] cell = new HtmlTableCell[5];
            cell[0] = new HtmlTableCell();
            cell[0].Controls.Add(new LiteralControl(b.Item));
            row.Cells.Add(cell[0]);
            cell[1] = new HtmlTableCell();
            cell[1].Controls.Add(new LiteralControl(b.DetailDesc));
            cell[1].Attributes.Add("class", "value");
            row.Cells.Add(cell[1]);
            cell[2] = new HtmlTableCell();
            cell[2].Controls.Add(new LiteralControl(b.Qty.ToString().Trim()));
            cell[2].Attributes.Add("class", "value");
            row.Cells.Add(cell[2]);
            cell[3] = new HtmlTableCell();
            cell[3].Controls.Add(new LiteralControl(b.UnitOfMeasure.ToString().Trim()));
            cell[3].Attributes.Add("class", "value");
            row.Cells.Add(cell[3]);
            itemDetails1.Rows.Add(row);
        }
    }

    protected void lnkOk_Click(object sender, EventArgs e)
    {
		Response.Redirect("endorsedbidtenders.aspx");
    }

	private void Bind()
	{
		DataTable dt = CreateDataSource();
		if (dt != null)
		{
			DataView m_DataView = new DataView(dt);
			m_DataView.Sort = "[DateRenegotiated]" + " " + Session["ORDERBY"].ToString().Trim();
			gvBids.DataSource = m_DataView;
			gvBids.DataBind();
		}
	}

	private DataTable CreateDataSource()
	{
		BidTenderTransaction tenderTransaction = new BidTenderTransaction();
		DataTable dtDrafts = tenderTransaction.QueryEndorsedBidTenderDetails(connstring, Session[Constant.SESSION_USERID].ToString(), Session[Constant.SESSION_BIDREFNO].ToString(), Session["ORDERBY"].ToString());
		return dtDrafts;
	}

	protected void gvBids_RowCommand(object sender, GridViewCommandEventArgs e)
	{
		if (e.CommandName.Equals("Select"))
			Response.Redirect("biditemsforrenegotiationdetails.aspx?bidrefno=" + e.CommandArgument.ToString().Trim());
	}

	protected void gvBids_Sorting(object sender, GridViewSortEventArgs e)
	{
		Session["ORDERBY"] = ((Session["ORDERBY"].ToString().Trim() == "DESC") ? "ASC" : "DESC");
		Bind();
	}

	protected void gvBids_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		DataTable dt = CreateDataSource();
		if (dt != null)
		{
			DataView m_DataView = new DataView(dt);
			m_DataView.Sort = "[DateRenegotiated]" + " " + Session["ORDERBY"].ToString().Trim();

			gvBids.DataSource = m_DataView;
			gvBids.PageIndex = e.NewPageIndex;
			gvBids.DataBind();
		}
	}
}
