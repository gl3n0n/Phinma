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
using EBid.lib.user.trans;
using EBid.lib.auction.trans;
using EBid.lib.bid.trans;
using EBid.lib;
using EBid.ConnectionString;

public partial class web_purchasing_screens_awardedAuctionItems : System.Web.UI.Page
{
    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (!IsPostBack)
        {
            if (Session[Constant.SESSION_USERID] != null)
            {
                FillDropDownList();
                d3Search.Visible = false;

                DataTable dtSearchAuctions = DtSearchedItems();
                
                if (dtSearchAuctions.Rows.Count > 0)
                {
                    d3Search.Visible = true;

                    DataView dvSearchAuctions = new DataView(dtSearchAuctions);
                   
                    gvSearchedItems.DataSource = dvSearchAuctions;
                    gvSearchedItems.DataBind();
                }
                else
                {
                    lblDataIsEmpty.Text = "There are no searchable auction events.";
                }
            }
           
        }
    }

    private void FillDropDownList()
    {
        SupplierTransaction vnd = new SupplierTransaction();
        CategoryTransaction subCat = new CategoryTransaction();

        DataSet dsVendors = vnd.GetAllVendors(connstring);
        DataTable dsSubCategories = subCat.GetAllCategories(connstring);

        ddlCompanies.DataSource = dsVendors.Tables[0];
        ddlCompanies.DataTextField = "VendorName";
        ddlCompanies.DataValueField = "VendorID";
        ddlCompanies.DataBind();
        ddlCompanies.Items.Insert(0, new ListItem("[Company]", "0"));

        ddlSubCat.DataSource = dsSubCategories;
        //ddlSubCat.DataTextField = "SubCategoryName";
        //ddlSubCat.DataValueField = "SubCategoryId";
        ddlSubCat.DataTextField = "CategoryName";
        ddlSubCat.DataValueField = "CategoryId";
        ddlSubCat.DataBind();
        ddlSubCat.Items.Insert(0, new ListItem("[Sub-category]", "9999"));
    }

    protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string auctionEventName = tbSearch.Value.ToString().Trim() + "%";

        DataView dvSearchAuctions = new DataView(DtSearchedItems());

        gvSearchedItems.DataSource = dvSearchAuctions;
        gvSearchedItems.PageIndex = e.NewPageIndex;
        gvSearchedItems.DataBind();
    }

    private DataTable DtSearchedItems()
    {
        string day = null,
        year = null,
        month = ddlMonth.Value.ToString().Trim(),
        vendor = ddlCompanies.SelectedItem.Value.ToString().Trim(),
        category = ddlSubCat.SelectedItem.Value.ToString().Trim(),
        auctionEventname = tbSearch.Value.ToString().Trim() + "%";
                
        DataTable dtSearchAuctions = null;

        if (tbDay.Value.ToString().Trim().Equals("dd"))
            day = "0";
        else
            day = tbDay.Value.ToString().Trim();

        if (tbYear.Value.ToString().Trim().Equals("yyyy"))
            year = "0";
        else
            year = tbYear.Value.ToString().Trim();

        if ((Int32.Parse(vendor) == 0) &&
            (category == "9999") &&
            (Int32.Parse(month) == 0) &&
            (Int32.Parse(year) == 0))
        {
            dtSearchAuctions = AuctionItemTransaction.QueryAllAwardedAuctionItems(auctionEventname);
        }
        else
        {
            dtSearchAuctions = AuctionItemTransaction.QueryAwardedAuctionItems(vendor, category, month, day, year);
        }

        return dtSearchAuctions;
    }

    private void showSearchedItems(DataTable dtSearchAuctions)
    {
        d3Search.Visible = true;

        if (dtSearchAuctions.Rows.Count > 0)
        {
            DataView dvSearchAuctions = new DataView(dtSearchAuctions);

            gvSearchedItems.Visible = true;
            lblDataIsEmpty.Visible = false;

            gvSearchedItems.DataSource = dvSearchAuctions;
            gvSearchedItems.DataBind();
        }
        else
        {
            gvSearchedItems.Visible = false;
            lblDataIsEmpty.Visible = true;

            if (tbSearch.Value.ToString().Trim().Length > 0)
                lblDataIsEmpty.Text = "No Matches for \"" + tbSearch.Value.ToString().Trim() + "\" found. Please try another search.";
            else
                lblDataIsEmpty.Text = "No Matches found. Please try another search.";
        }
    }

    protected void gvBids_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Select"))
        {
            Session[Constant.SESSION_AUCTIONREFNO] = e.CommandArgument;
            Response.Redirect("awardauctionitem.aspx");
        }
    }

    protected void lbSearch_Click(object sender, EventArgs e)
    {
        showSearchedItems(DtSearchedItems());
    }
}
