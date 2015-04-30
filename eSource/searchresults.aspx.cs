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
using EBid.lib.constant;
using EBid.lib.user.trans;
using EBid.lib;
using EBid.ConnectionString;

public partial class SearchResults : System.Web.UI.Page
{   
    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Search Result");
        if (!(IsPostBack))
        {
            if ((Session[Constant.SESSION_USERID] != null) && (Session[Constant.SESSION_USERTYPE] != null))
            {
                ViewState[Constant.SESSION_USERID] = Int32.Parse(Session[Constant.SESSION_USERID].ToString().Trim());
                int userID = Int32.Parse(ViewState[Constant.SESSION_USERID].ToString().Trim());

		int usertype = Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim());
		string searchstring = "";
		int searchType = 1;
		if (Request.QueryString["searchstring"] != null)
                	searchstring = Request.QueryString["searchstring"].ToString().Trim();
		if (Request.QueryString["searchType"] != null)
			searchType = Int32.Parse(Request.QueryString["searchType"].ToString().Trim());

                if ((searchType >= 1) && (searchType <= 6))
                {
                    if ((searchType >= 1) && (searchType <= 3))
                    {
                        gvSearchResultForItems.Columns[0].HeaderText = "Bid Reference No.";
                        gvSearchResultForItems.Columns[1].HeaderText = "Bid Items";
                    }
                    else
                    {
                        gvSearchResultForItems.Columns[0].HeaderText = "Auction Reference No.";
                        gvSearchResultForItems.Columns[1].HeaderText = "Auction Items";
                    }

                    gvSearchResultForItems.Visible = true;

//                    BidItemTransaction items = new BidItemTransaction();

                    DataTable dtItems = BidItemTransaction.QuerySearchedItemByItemDesc(connstring, searchType, searchstring, usertype, userID);

                    if (dtItems.Rows.Count > 0)
                    {
                        DataView dvItems = new DataView(dtItems);

                        gvSearchResultForItems.DataSource = dvItems;
                        gvSearchResultForItems.DataBind();
                    }
                    else
                    {
                        lblIsEmpty.Visible = true;
                        lblIsEmpty.Text = "No matches for '" + searchstring + "' found.";
                    }
                }
                else if (searchType == 7)
                {
                    ProductsTransaction products = new ProductsTransaction();

                    DataTable dtProducts = products.QueryProductsViaName(connstring, searchstring);
                    // DataView dvProducts = new DataView(dtProducts);

                    if (dtProducts.Rows.Count > 0)
                    {
                        gvProducts.Visible = true;

                        gvProducts.DataSource = dtProducts;
                        gvProducts.DataBind();
                    }
                    else
                    {
                        lblIsEmpty.Visible = true;
                        lblIsEmpty.Text = "No matches for '" + searchstring + "' found.";
                    }
                }
                else if (searchType == 8)
                {
                    CompanyTransaction vendor = new CompanyTransaction();

                    DataTable dtVendor = vendor.QueryVendorSearch(connstring, searchstring);

                    if (dtVendor.Rows.Count > 0)
                    {
                        DataView dvVendor = new DataView(dtVendor);

                        gvSuppliers.Visible = true;
                        gvSuppliers.DataSource = dvVendor;
                        gvSuppliers.DataBind();
                    }
                    else
                    {
                        lblIsEmpty.Visible = true;
                        lblIsEmpty.Text = "No matches for '" + searchstring + "' found.";
                    }
                }
            }
        }
    }

    protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ViewState[Constant.SESSION_USERID] = Int32.Parse(Session[Constant.SESSION_USERID].ToString().Trim());
        int userID = Int32.Parse(ViewState[Constant.SESSION_USERID].ToString().Trim());
        string usertype = Session[Constant.SESSION_USERTYPE].ToString().Trim();
	if (Request.QueryString["usertype"] != null)
        	usertype = Request.QueryString["usertype"].ToString().Trim();
        string searchstring = "";
	if (Request.QueryString["searchstring"] != null)
        	searchstring = Request.QueryString["searchstring"].ToString().Trim();
        string searchType = "1";
	if (Request.QueryString["searchType"] != null)
        	searchType = Request.QueryString["searchType"].ToString().Trim();

        if ((Int32.Parse(searchType) >= 1) && (Int32.Parse(searchType) <= 6))
        {
            if ((Int32.Parse(searchType) >= 1) && (Int32.Parse(searchType) <= 3))
            {
                gvSearchResultForItems.Columns[0].HeaderText = "Bid Reference No.";
                gvSearchResultForItems.Columns[1].HeaderText = "Bid Items";
            }
            else
            {
                gvSearchResultForItems.Columns[0].HeaderText = "Auction Reference No.";
                gvSearchResultForItems.Columns[1].HeaderText = "Auction Items";
            }

            gvSearchResultForItems.Visible = true;            

            DataTable dtItems = BidItemTransaction.QuerySearchedItemByItemDesc(connstring, Int32.Parse(searchType), searchstring, Int32.Parse(usertype), userID);
            DataView dvItems = new DataView(dtItems);

            gvSearchResultForItems.DataSource = dvItems;
            gvSearchResultForItems.PageIndex = e.NewPageIndex;
            gvSearchResultForItems.DataBind();
        }
        else if (Int32.Parse(searchType) == 7)
        {
            ProductsTransaction products = new ProductsTransaction();

            DataTable dtProducts = products.QueryProductsViaName(connstring, searchstring);          

            gvProducts.Visible = true;

            gvProducts.DataSource = dtProducts;
            gvProducts.PageIndex = e.NewPageIndex;
            gvProducts.DataBind();
        }
        else if (Int32.Parse(searchType) == 8)
        {
            CompanyTransaction vendor = new CompanyTransaction();

            DataTable dtVendor = vendor.QueryVendorSearch(connstring, searchstring);
            DataView dvVendor = new DataView(dtVendor);

            gvSuppliers.Visible = true;

            gvSuppliers.DataSource = dvVendor;
            gvSuppliers.PageIndex = e.NewPageIndex;
            gvSuppliers.DataBind();
        }
    }

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // string usertype = Request.QueryString["usertype"].ToString().Trim();
            // string searchType = Request.QueryString["searchType"].ToString().Trim();

        	string usertype = Session[Constant.SESSION_USERTYPE].ToString().Trim();
        	if (Request.QueryString["usertype"] != null)
			usertype = Request.QueryString["usertype"].ToString().Trim();
	        string searchType = "1";
        	if (Request.QueryString["searchType"] != null)
			searchType = Request.QueryString["searchType"].ToString().Trim();

            string url = null;
            
            if ((Int32.Parse(searchType) >= 1) && (Int32.Parse(searchType) <= 6))
            {
                LinkButton lbRefNo = (LinkButton)e.Row.FindControl("lblRefNo");
                Label lblStatusID = (Label)e.Row.FindControl("lblStatusID");
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");

                string refNo = lbRefNo.Text;

                if ((Int32.Parse(searchType) >= 1) && (Int32.Parse(searchType) <= 3))
                {
                    switch (Int32.Parse(usertype))
                    {
                        case 1:
                            url = "";
                            break;
                        case 2:
                            url = "web/vendorscreens/biddetails.aspx";
                            break;
                        case 3:
                            url = "web/purchasingscreens/biddetails.aspx";
                            break;
                        case 5:
                            url = "web/boc/biddetails.aspx";
                            break;
                        case 6:
                            url = "web/bac/bidawardingchecklistsearch.aspx";
                            break;
                    }

                    switch (Int32.Parse(lblStatusID.Text))
                    {
                        case 0:
                            lblStatus.Text = "Draft";
                            if (Int32.Parse(usertype) == 1)
                            
                                url = "web/buyerscreens/draftbiddetails.aspx";
                            
                           
                            break;
                        case 1:
                            lblStatus.Text = "Submitted";
                            if (Int32.Parse(usertype) == 1)
                            
                                url = "web/buyerscreens/bideventdetails.aspx";
                            
                            break;
                        case 2:
                            lblStatus.Text = "Rejected";
                            if (Int32.Parse(usertype) == 1)
                            
                                url = "web/buyerscreens/bideventdetails.aspx";
                            
                            break;
                        case 3:
                            lblStatus.Text = "Re-edit";
                            if (Int32.Parse(usertype) == 1)
                            
                                url = "web/buyerscreens/draftbiddetails.aspx";
                            
                            break;
                        case 4:
                            lblStatus.Text = "Approved";
                            if (Int32.Parse(usertype) == 1)
                            
                                url = "web/buyerscreens/approvedbiddetails.aspx";
                            
                            break;
                        case 5:
                            lblStatus.Text = "Endorsed";
                            if (Int32.Parse(usertype) == 1)
                            
                                url = "web/buyerscreens/bideventdetails.aspx";
                            
                            break;
                        case 6:
                            lblStatus.Text = "Renegotiate";
                            if (Int32.Parse(usertype) == 1)
                            
                                url = "web/buyerscreens/bideventdetails.aspx";
                            
                            break;
                        case 7:
                            lblStatus.Text = "Declined";
                            if (Int32.Parse(usertype) == 1)
                            
                                url = "web/buyerscreens/bideventdetails.aspx";
                            
                            break;
                        case 8:
                            lblStatus.Text = "Cancelled";
                            if (Int32.Parse(usertype) == 1)
                            
                                url = "web/buyerscreens/bideventdetails.aspx";
                            
                            break;
                    }

                    lbRefNo.Attributes.Add("onClick", "RedirectToItemDetails('" + url + "','" + refNo + "',0)");
                }
                else
                {
                    switch (Int32.Parse(usertype))
                    {
                        case 1:
                            url = "";
                            break;
                        case 2:
                            url = "web/vendorscreens/auctiondetails.aspx";
                            break;
                        case 3:
                            url = "web/purchasingscreens/auctiondetails.aspx";
                            break;
                    }

                    switch (Int32.Parse(lblStatusID.Text))
                    {
                        case 0:
                            lblStatus.Text = "Draft";
                            if (Int32.Parse(usertype) == 1)
                                url = "web/buyerscreens/draftauctiondetails.aspx";
                            break;
                        case 1:
                            lblStatus.Text = "Submitted";
                            if (Int32.Parse(usertype) == 1)
                                url = "web/buyerscreens/auctiondetailssubmitted.aspx";
                            break;
                        case 2:
                            lblStatus.Text = "Rejected";
                            if (Int32.Parse(usertype) == 1)
                                url = "web/buyerscreens/auctiondetails.aspx";
                            break;
                        case 3:
                            lblStatus.Text = "Re-edit";
                            if (Int32.Parse(usertype) == 1)
                                url = "web/buyerscreens/draftauctiondetails.aspx";
                            break;
                        case 4:
                            lblStatus.Text = "Approved";
                            if (Int32.Parse(usertype) == 1)
                                url = "web/buyerscreens/approvedauctiondetails.aspx";
                            break;
                        case 5:
                            lblStatus.Text = "Cancelled";
                            if (Int32.Parse(usertype) == 1)
                                url = "web/buyerscreens/auctiondetails.aspx";
                            break;
                        case 6:
                            lblStatus.Text = "Finished";
                            if (Int32.Parse(usertype) == 1)
                                url = "web/buyerscreens/auctiondetails.aspx";
                            break;
                    }

                    lbRefNo.Attributes.Add("onClick", "RedirectToItemDetails('" + url + "','" + refNo + "',1)");
                }
            }
           else if (Int32.Parse(searchType) == 8)
            {
                LinkButton lbVendor = (LinkButton)e.Row.FindControl("lbVendorName");
                Label lbVendorId = (Label)e.Row.FindControl("lblVendorID");
                
                switch (Int32.Parse(usertype))
                {
                    case 1:
                        url = "web/buyerscreens/supplierdetails.aspx";
                        break;
                    case 2:
                        url = "web/vendorscreens/supplierdetails.aspx";
                        break;
                    case 3:
                        url = "web/purchasingscreens/supplierdetails.aspx";
                        break;
                }

                lbVendor.Attributes.Add("onClick", "RedirectToSupplierDetails('" + url + "', '" + lbVendorId.Text.Trim() + "')");
            }
        }
    }
}
