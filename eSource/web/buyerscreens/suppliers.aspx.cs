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
using EBid.lib;
using EBid.ConnectionString;
using EBid.lib.constant;

public partial class WEB_buyer_screens_suppliers : System.Web.UI.Page
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
            PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Suppliers");
            DisplaySuppliers();
            PutDataToComboBoxes();
        }
    }

    private void DisplaySuppliers()
    {
        SupplierTransaction s = new SupplierTransaction();
        s.GetAllVendors(connstring);
    }

    private void PutDataToComboBoxes()
    {
        BrandsTransaction brands = new BrandsTransaction();
        ItemsCarriedTransaction items = new ItemsCarriedTransaction();
        LocationsTransaction loc = new LocationsTransaction();
        ServicesTransaction serv = new ServicesTransaction();
        CategoryTransaction cat = new CategoryTransaction();
        SubCategory sub = new SubCategory();
        PCABClassTransaction pcab = new PCABClassTransaction();
        ISOStandard iso = new ISOStandard();

        ddlBrands.DataSource= brands.GetAllBrands(connstring);
        ddlBrands.DataTextField = "BrandName";
        ddlBrands.DataValueField = "BrandId";
        ddlBrands.DataBind();
        ListItem lst = new ListItem();
        lst.Text = "Select Brands";
        lst.Value = "";
        ddlBrands.Items.Insert(0,lst);

        ddlItemsCarried.DataSource = items.GetAllItemsCarried(connstring);
        ddlItemsCarried.DataTextField = "ItemsCarried";
        ddlItemsCarried.DataValueField = "ItemNo";
        ddlItemsCarried.DataBind();
        ListItem lst1 = new ListItem();
        lst1.Text = "Select Items";
        lst1.Value = "";
        ddlItemsCarried.Items.Insert(0,lst1);

        ddlLocations.DataSource = loc.GetAllLocations(connstring);
        ddlLocations.DataTextField = "LocationName";
        ddlLocations.DataValueField = "LocationId";
        ddlLocations.DataBind();
        ListItem lst2 = new ListItem();
        lst2.Text = "Select Locations";
        lst2.Value = "";
        ddlLocations.Items.Insert(0,lst2);

        ddlServices.DataSource = serv.GetAllServices(connstring);
        ddlServices.DataTextField = "ServiceName";
        ddlServices.DataValueField = "ServiceId";
        ddlServices.DataBind();
        ListItem lst3 = new ListItem();
        lst3.Text = "Select Services";
        lst3.Value = "";
        ddlServices.Items.Insert(0, lst3);

        ddlCategory.DataSource = cat.GetCategories(connstring);
        ddlCategory.DataTextField = "CategoryName";
        ddlCategory.DataValueField = "CategoryId";
        ddlCategory.DataBind();
        ListItem lst4 = new ListItem();
        lst4.Text = "Select Category";
        lst4.Value = "";
        ddlCategory.Items.Insert(0, lst4);

        ddlSubCategory.DataSource = sub.GetSubCategoryByCategoryId(connstring, ddlCategory.SelectedItem.Value);
        ddlSubCategory.DataTextField = "SubCategoryName";
        ddlSubCategory.DataValueField = "SubCategoryId";
        ddlSubCategory.DataBind();
        ListItem lst5 = new ListItem();
        lst5.Text = "Select Sub-category";
        lst5.Value = "";
        ddlSubCategory.Items.Insert(0, lst5);

        ddlPCAAB.DataSource = pcab.GetAllPCABClass(connstring);
        ddlPCAAB.DataTextField = "PCABClassName";
        ddlPCAAB.DataValueField = "PCABClassId";
        ddlPCAAB.DataBind();
        ListItem lst6 = new ListItem();
        lst6.Text = "PCAB Class";
        lst6.Value = "";
        ddlPCAAB.Items.Insert(0, lst6);

        chkISOStandard.DataSource = iso.GetISOStandard(connstring);
        //chkISOStandard.DataSource = iso.GetISOStandard(connstring);
        chkISOStandard.DataTextField = "ISOStandardName";
        chkISOStandard.DataValueField = "ISOStandardId";
        chkISOStandard.DataBind();
    }


    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        SubCategory sub = new SubCategory();
        ddlSubCategory.DataSource = sub.GetSubCategoryByCategoryId(connstring, ddlCategory.SelectedItem.Value);
        ddlSubCategory.DataTextField = "SubCategoryName";
        ddlSubCategory.DataValueField = "SubCategoryId";
        ddlSubCategory.DataBind();
        ListItem lst5 = new ListItem();
        lst5.Text = "Select Sub-category";
        lst5.Value = "";
        ddlSubCategory.Items.Insert(0, lst5);
    }

    private string GetISOStandard()
    {
        //Both ISO 9001 and ISO 9002 are selected 
        if ((chkISOStandard.Items[0].Selected) && (chkISOStandard.Items[1].Selected))
        {
            return "11";
        }
        //only ISO 9001 is selected and ISO 9002 is not selected
        else if ((chkISOStandard.Items[0].Selected) && (!(chkISOStandard.Items[1].Selected)))
        {
            return "10";
        }
        //ISO 9001 is not selected and ISO 9002 is selected
        else if ((!(chkISOStandard.Items[0].Selected)) && (chkISOStandard.Items[1].Selected))
        {
            return "01";
        }
        //Both ISO 9001 and ISO 9002 are not selected or other conditions
        else 
        {
            return "00";
        } 
    }

    protected void ddlSearch_Click(object sender, EventArgs e)
    {
        SupplierTransaction s = new SupplierTransaction();
        string ISOStandard = GetISOStandard();
        gvSuppliers.DataSource = s.SearchVendors(connstring, ddlCategory.SelectedItem.Value.Trim(), 
        ddlSubCategory.SelectedItem.Value.Trim(), ISOStandard, ddlPCAAB.SelectedItem.Value.Trim(), 
        ddlBrands.SelectedItem.Value.Trim(), ddlServices.SelectedItem.Value.Trim(), 
        ddlLocations.SelectedItem.Value.Trim(), ddlItemsCarried.SelectedItem.Value.Trim());
        gvSuppliers.DataBind();
    }

    protected void gvSuppliers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName.ToUpper().Trim())
        {
            case "SELECT":
                Session["VendorId"] = e.CommandArgument.ToString().Trim();
                Response.Redirect("supplierdetails.aspx");
                break;
        }
    }
    protected void gvSuppliers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SupplierTransaction s = new SupplierTransaction();
        string ISOStandard = GetISOStandard();
        DataTable dt = s.SearchVendors(connstring, ddlCategory.SelectedItem.Value.Trim(), 
            ddlSubCategory.SelectedItem.Value.Trim(), ISOStandard, ddlPCAAB.SelectedItem.Value.Trim(), 
            ddlBrands.SelectedItem.Value.Trim(), ddlServices.SelectedItem.Value.Trim(),
            ddlLocations.SelectedItem.Value.Trim(), ddlItemsCarried.SelectedItem.Value.Trim());
        if (dt != null)
        {
            DataView m_DataView = new DataView(dt);    
            gvSuppliers.DataSource = m_DataView;
            gvSuppliers.PageIndex = e.NewPageIndex;
            gvSuppliers.DataBind();
        }
    }
}
