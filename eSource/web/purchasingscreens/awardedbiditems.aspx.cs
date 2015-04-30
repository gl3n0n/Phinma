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
using EBid.lib.bid.trans;
using EBid.lib.user.trans;
using EBid.lib;

public partial class web_purchasing_screens_awardedBidItems : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (!IsPostBack)
        {
            Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;
            if (Session[Constant.SESSION_USERID] != null)
            {
                //FillDropDownList();

                //DataTable dtSearchBids = DtSearchedItems();
                //d3Search.Visible = false;

                //if (dtSearchBids.Rows.Count > 0)
                //{
                //    DataView dvSearchBids = new DataView(dtSearchBids);

                //    gvSearchedItems.DataSource = dvSearchBids;
                //    gvSearchedItems.DataBind();
                //}
                //else
                //{
                //    lblDataIsEmpty.Text = "There are no searchable bid events.";
                //}
            }
            
        }
        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Awarded Bid Items");
    }

    //private void FillDropDownList()
    //{
    //    SupplierTransaction vnd = new SupplierTransaction();
    //    CategoryTransaction subCat = new CategoryTransaction();

    //    DataSet dsVendors = vnd.GetAllVendors(connstring);
    //    DataTable dsSubCategories = subCat.GetAllCategories();
               
    //    ddlCompanies.DataSource = dsVendors.Tables[0];
    //    ddlCompanies.DataTextField = "VendorName";
    //    ddlCompanies.DataValueField = "VendorId";
    //    ddlCompanies.DataBind();
    //    ddlCompanies.Items.Insert(0, new ListItem("[Company]", "0"));
       
    //    ddlSubCat.DataSource = dsSubCategories;
    //    //ddlSubCat.DataTextField = "SubCategoryName";
    //    //ddlSubCat.DataValueField = "SubCategoryId";
    //    ddlSubCat.DataTextField = "CategoryName";
    //    ddlSubCat.DataValueField = "CategoryId";
    //    ddlSubCat.DataBind();
    //    ddlSubCat.Items.Insert(0, new ListItem("[Sub-category]", "9999"));
    //}

    //protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    string bidEventName = tbSearch.Value.ToString().Trim() + "%";
        
    //    PurchasingTransaction bid = new PurchasingTransaction();

    //    DataView dvSearchBids = new DataView(DtSearchedItems());
        
    //    gvSearchedItems.DataSource = dvSearchBids;
    //    gvSearchedItems.PageIndex = e.NewPageIndex;
    //    gvSearchedItems.DataBind();
    //}

    protected void gvBids_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Select"))
        {
            Session[Constant.SESSION_BIDREFNO] = e.CommandArgument;
            //Response.Redirect("awarditem.aspx");
        }
        else if (e.CommandName.Equals("SelectItem"))
        {
            //String[] vals = e.CommandArgument.ToString().Split('»');

            //switch(vals.Length) 
            //{
            //    case 0:
            //        return;
            //    case 1:
            //    Session[Constant.SESSION_BIDTENDERNO] = vals[0];                
            //        break;
            //    case 2:
            //    Session[Constant.SESSION_BIDTENDERNO] = vals[0];
            //    Session[Constant.SESSION_BIDDETAILNO] = vals[1];
            //        break;
            //}
            string[] args = e.CommandArgument.ToString().Split(new char[] { '|' });
            Session[Constant.SESSION_BIDTENDERNO] = args[0];
            Session["TVendorId"] = args[1];
            Session[Constant.SESSION_BIDREFNO] = args[2];
            Session[Constant.SESSION_BIDDETAILNO] = args[3];
            Session["ViewOption"] = "AsVendor";

            Session[Constant.SESSION_LASTPAGE] = Request.Url.AbsolutePath;
            Response.Redirect("awardedbiditemdetails.aspx");
        }
        else if (e.CommandName.Equals("SelectEvent"))
        {
            string[] args = e.CommandArgument.ToString().Split(new char[] { '|' });
            Session[Constant.SESSION_BIDTENDERNO] = args[0];
            Session["TVendorId"] = args[1];
            Session[Constant.SESSION_BIDREFNO] = args[2];
            Session[Constant.SESSION_BIDDETAILNO] = args[3];
            Response.Redirect("biddetails.aspx");
        }

    }
    //protected void lbSearch_Click(object sender, EventArgs e)
    //{
    //    showSearchedItems(DtSearchedItems());
    //}

    //private void showSearchedItems(DataTable dtSearchBids)
    //{
    //    d3Search.Visible = true;

    //    if (dtSearchBids.Rows.Count > 0)
    //    {
    //        DataView dvSearchBids = new DataView(dtSearchBids);
                        
    //        gvSearchedItems.Visible = true;
    //        lblDataIsEmpty.Visible = false;
    //        gvSearchedItems.DataSource = dvSearchBids;
    //        gvSearchedItems.DataBind();
    //    }
    //    else
    //    {
    //        gvSearchedItems.Visible = false;
    //        lblDataIsEmpty.Visible = true;

    //        if (tbSearch.Value.ToString().Trim().Length > 0)
    //            lblDataIsEmpty.Text = "No Matches for \"" + tbSearch.Value.ToString().Trim() + "\" found. Please try another search.";
    //        else
    //            lblDataIsEmpty.Text = "No Matches found. Please try another search.";
    //       // actions.Visible = false;
    //    }
    //}

    //private DataTable DtSearchedItems()
    //{
    //   string day = null,
    //   year = null,
    //   month = ddlMonth.Value.ToString().Trim(),
    //   vendor = ddlCompanies.SelectedItem.Value.ToString().Trim(),
    //   category = ddlSubCat.SelectedItem.Value.ToString().Trim(),
    //   bidEventname = tbSearch.Value.ToString().Trim() + "%";

    //    PurchasingTransaction bid = new PurchasingTransaction();
    //    DataTable dtSearchBids = null;

    //    if (tbDay.Value.ToString().Trim().Equals("dd"))
    //        day = "0";
    //    else
    //        day = tbDay.Value.ToString().Trim();

    //    if (tbYear.Value.ToString().Trim().Equals("yyyy"))
    //        year = "0";
    //    else
    //        year = tbYear.Value.ToString().Trim();

    //    if ((Int32.Parse(vendor) == 0) &&
    //        (category == "9999") &&
    //        (Int32.Parse(month) == 0) &&
    //        (Int32.Parse(year) == 0))
    //    {
    //        dtSearchBids = bid.QueryAllAwardedItems(Int32.Parse(Session[Constant.SESSION_USERID].ToString()));
    //    }
    //    else
    //    {
    //        dtSearchBids = bid.QueryAwardedItems(Int32.Parse(Session[Constant.SESSION_USERID].ToString()), vendor, category, month, day, year);            
            
    //    }
    //    return dtSearchBids;
    //}

    protected String GetStringValue(Object tmbobj)
    {
        String retString = "";
        if ((tmbobj == DBNull.Value) || (tmbobj == null)){
            retString = "";
        }
        else {
            retString = tmbobj.ToString();
        }
        return retString;
        
    }

    protected void chkbuyeropts_SelectedIndexChange(object sender, EventArgs e)
    {
        setFilter();
    }

    private void setFilter()
    {
        string filter = string.Empty;

        for (int ind = 0; ind < chkbuyeropts.Items.Count; ind++)
        {
            if (chkbuyeropts.Items[ind].Selected == true)
            {
                switch (chkbuyeropts.Items[ind].Value)
                {
                    case "0"://Trans-Asia
                        {
                            if (string.IsNullOrEmpty(filter))
                                filter = "0";
                            else
                                filter += ",0";
                        }
                        break;
                    case "1"://
                        {
                            if (string.IsNullOrEmpty(filter))
                                filter = "1";
                            else
                                filter += ",1";
                        }
                        break;
                    case "2"://2
                        {
                            if (string.IsNullOrEmpty(filter))
                                filter = "2";
                            else
                                filter += ",2";
                        }
                        break;
                }
            }
        }

        if (string.IsNullOrEmpty(filter))
            filter = "-1";

        dsAwardedItems.FilterExpression = String.Format("CompanyId IN ({0})", filter); ;
        gvSearchedItems.DataBind();
    }

}
