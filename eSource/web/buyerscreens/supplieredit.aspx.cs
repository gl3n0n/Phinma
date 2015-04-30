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
using EBid.lib;
using EBid.ConnectionString;
using EBid.lib.constant;

public partial class web_buyerscreens_supplierEdit : System.Web.UI.Page
{

    private void InitJavascript()
    {
        btnSelectCategory.Attributes.Add("onClick", "copyToCategoryList();");
        btnSelectAllCategories.Attributes.Add("onClick", "copyAllCategories();");
        btnDeSelectCategory.Attributes.Add("onClick", "removeAllCategories();");
        btnDeSelectAllCategories.Attributes.Add("onClick", "removeFromCategoryList();");
        lstCategories.Attributes.Add("onDblClick", "copyACategory();");
        lstSelectedCategories.Attributes.Add("onDblClick", "removeACategory();");

        btnSelectSubCategory.Attributes.Add("onClick", "copyToSubCategoryList();");
        btnSelectAllSubCategories.Attributes.Add("onClick", "copyAllSubCategories();");
        btnDeSelectAllSubCategories.Attributes.Add("onClick", "removeAllSubCategories();");
        btnDeSelectSubCategory.Attributes.Add("onClick", "removeFromSubCategoryList();");
        lstSubCategory.Attributes.Add("onDblClick", "copyASubCategory();");
        lstSelectedSubCategories.Attributes.Add("onDblClick", "removeASubCategory();");
    }

    private string connstring = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (!IsPostBack)
        {
            InitJavascript();
            if (Session["VendorId"] != null)
                ViewState["VendorId"] = Session["VendorId"].ToString().Trim();
            FillDropDownLists();
            SupplierTransaction st = new SupplierTransaction();
            Supplier s = st.QuerySuppliers(connstring, ViewState["VendorId"].ToString().Trim());
            lblCompanyName.Text = s.VendorName.Trim();
            lblAddressHeadOffice.Text = s.VendorAddress.Trim() + " " + s.VendorAddress1.Trim();
            lblTelephone.Text = s.TelephoneNumber.Trim();
            lblFax.Text = s.Fax.Trim();
            lblExtension.Text = s.Extension.Trim();
            lblPOBox.Text = s.POBOX.Trim();
            lblPostalCode.Text = s.PostalCode.Trim();
            lblEmail.Text = s.VendorEmail.Trim();
            ddlSupplierType.SelectedIndex = ddlSupplierType.Items.IndexOf(ddlSupplierType.Items.FindByValue(s.Accredited.Trim()));
            ddlPCABClass.SelectedIndex = ddlPCABClass.Items.IndexOf(ddlPCABClass.Items.FindByValue(s.PCABClass.Trim()));
            switch (s.ISOStandard.Trim())
            {
                case "01":
                    chkISOStandard_white.Items[1].Selected = true;
                    break;
                case "10":
                    chkISOStandard_white.Items[0].Selected = true;
                    break;
                case "11":
                    chkISOStandard_white.Items[0].Selected = true;
                    chkISOStandard_white.Items[1].Selected = true;
                    break;
            }

            FillCategoriesSubCategories();
            FillListBoxes();
        }
    }

    private void FillCategoriesSubCategories()
    {
        SupplierTransaction st = new SupplierTransaction();
        DataSet ds = st.QueryCategoryAndSubCategory(connstring, ViewState["VendorId"].ToString().Trim());
        string strcategory = "";
        string strcategorynoapostrophe = ""; //for general purposes
        string strsubcategory = "";
        
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0] != null)
            {
                DataTable dtCategory = ds.Tables[0];
                if (dtCategory.Rows.Count > 0)
                {

                    for (int i = 0; i < dtCategory.Rows.Count; i++)
                    {
                        strcategory = (strcategory == "" ? "'" + dtCategory.Rows[i]["CategoryId"].ToString() + "'" : strcategory + ",'" + dtCategory.Rows[i]["CategoryId"].ToString() + "'");
                        strcategorynoapostrophe = (strcategorynoapostrophe == "" ? dtCategory.Rows[i]["CategoryId"].ToString() : strcategorynoapostrophe + "," + dtCategory.Rows[i]["CategoryId"].ToString());
                    }
                }
            }
            if (ds.Tables[1] != null)
            {
                DataTable dtSubCategory = ds.Tables[1];
                if (dtSubCategory.Rows.Count > 0)
                {

                    for (int i = 0; i < dtSubCategory.Rows.Count; i++)
                    {
                        strsubcategory = (strsubcategory == "" ? dtSubCategory.Rows[i]["SubCategoryId"].ToString() : strsubcategory + "," + dtSubCategory.Rows[i]["SubCategoryId"].ToString());
                    }
                }
            }
        }
        //for general purposes
        hdnCategories.Text = strcategorynoapostrophe.Trim();
        hdnSubCategory.Text = strsubcategory.Trim();

        
            SupplierTransaction su = new SupplierTransaction();
            DataSet ds1 = su.QuerySelectedAndUnselectedCategories(connstring, strcategory.Trim());

            if (strcategory != "")
            {
                
                if (ds1.Tables.Count > 0)
             {
                if (ds1.Tables[0] != null)
                {
                    DataTable selectedcategories = ds1.Tables[0];
                    lstSelectedCategories.DataSource = selectedcategories;
                    lstSelectedCategories.DataTextField = "CategoryName";
                    lstSelectedCategories.DataValueField = "CategoryId";
                    lstSelectedCategories.DataBind();
                }
                if (ds1.Tables.Count == 2)
                {
                    if (ds1.Tables[1] != null)
                    {
                        DataTable availcategories = ds1.Tables[1];
                        lstCategories.DataSource = availcategories;
                        lstCategories.DataTextField = "CategoryName";
                        lstCategories.DataValueField = "CategoryId";
                        lstCategories.DataBind();
                    }
                }
            }
        }
        else
        {
            if (ds1.Tables.Count > 0)
            {
                DataTable availcategories = ds1.Tables[0];
                lstCategories.DataSource = availcategories;
                lstCategories.DataTextField = "CategoryName";
                lstCategories.DataValueField = "CategoryId";
                lstCategories.DataBind();
            }

        }
        if (strsubcategory.Trim() != "")
        {
            //SupplierTransaction su = new SupplierTransaction();
            DataSet ds2 = su.QuerySelectedAndUnselectedSubCategories(connstring, strsubcategory.Trim(), strcategory.Trim());
            if (ds2.Tables.Count > 0)
            {
                if (ds2.Tables[0] != null)
                {
                    DataTable selectedsubcategories = ds2.Tables[0];
                    lstSelectedSubCategories.DataSource = selectedsubcategories;
                    lstSelectedSubCategories.DataTextField = "SubCategoryName";
                    lstSelectedSubCategories.DataValueField = "SubCategoryId";
                    lstSelectedSubCategories.DataBind();
                }
                if (ds2.Tables[1] != null)
                {
                    DataTable availsubcategories = ds2.Tables[1];
                    lstSubCategory.DataSource = availsubcategories;
                    lstSubCategory.DataTextField = "SubCategoryName";
                    lstSubCategory.DataValueField = "SubCategoryId";
                    lstSubCategory.DataBind();
                }
            }
        }
    }

    private void FillListBoxes()
    {
        SupplierTransaction s = new SupplierTransaction();
        DataTable dtBrands = s.QueryBrandsByVendorId(connstring, ViewState["VendorId"].ToString().Trim());
        string vBrandIds="";
        for( int j=0; j < dtBrands.Rows.Count; j++)
            vBrandIds = (vBrandIds == "") ? dtBrands.Rows[j]["BrandId"].ToString().Trim() : vBrandIds + "," + dtBrands.Rows[j]["BrandId"].ToString().Trim();
        hdnBrands.Text = vBrandIds.Trim();
        DataTable dtServices = s.QueryServicesByVendorId(connstring, ViewState["VendorId"].ToString().Trim());
        string vServiceIds = "";
        for (int j = 0; j < dtServices.Rows.Count; j++)
            vServiceIds = (vServiceIds == "") ? dtServices.Rows[j]["ServiceId"].ToString().Trim() : vServiceIds + "," + dtServices.Rows[j]["ServiceId"].ToString().Trim();
        hdnServices.Text = vServiceIds.Trim();
        DataTable dtLocation = s.QueryLocationsByVendorId(connstring, ViewState["VendorId"].ToString().Trim());
        string vLocationIds = "";
        for (int j = 0; j < dtLocation.Rows.Count; j++)
            vLocationIds = (vLocationIds == "") ? dtLocation.Rows[j]["LocationId"].ToString().Trim() : vLocationIds + "," + dtLocation.Rows[j]["LocationId"].ToString().Trim();
        hdnLocation.Text = vLocationIds.Trim();
        DataTable dtItem = s.QueryItemsCarriedByVendorId(connstring, ViewState["VendorId"].ToString().Trim());
        string vItemIds = "";
        for (int j = 0; j < dtItem.Rows.Count; j++)
            vItemIds = (vItemIds == "") ? dtItem.Rows[j]["ProdItemNo"].ToString().Trim() : vItemIds + "," + dtItem.Rows[j]["ProdItemNo"].ToString().Trim();
        hdnItems.Text = vItemIds.Trim();
        BrandsTransaction b = new BrandsTransaction();
        lstBrandsCarried.DataSource = b.GetAllUnSelectedBrands(connstring, vBrandIds.Trim());
        lstBrandsCarried.DataValueField = "BrandId";
        lstBrandsCarried.DataTextField = "BrandName";
        lstBrandsCarried.DataBind();

        lstSelectedBrandsCarried.DataSource = b.GetAllSelectedBrands(vBrandIds.Trim());
        lstSelectedBrandsCarried.DataValueField = "BrandId";
        lstSelectedBrandsCarried.DataTextField = "BrandName";
        lstSelectedBrandsCarried.DataBind();

        ItemsCarriedTransaction i = new ItemsCarriedTransaction();
        lstItemsCarried.DataSource = i.GetAllUnSelectedItemsCarried(connstring, vItemIds.Trim());
        lstItemsCarried.DataValueField = "ItemNo";
        lstItemsCarried.DataTextField = "ItemsCarried";
        lstItemsCarried.DataBind();

        lstSelectedItemsCarried.DataSource = i.GetAllSelectedItemsCarried(vItemIds.Trim());
        lstSelectedItemsCarried.DataValueField = "ItemNo";
        lstSelectedItemsCarried.DataTextField = "ItemsCarried";
        lstSelectedItemsCarried.DataBind();

        ServicesTransaction se = new ServicesTransaction();
        lstServicesOffered.DataSource = se.GetAllUnSelectedServices(connstring, vServiceIds.Trim());
        lstServicesOffered.DataValueField = "ServiceId";
        lstServicesOffered.DataTextField = "ServiceName";
        lstServicesOffered.DataBind();

        lstSelectedServicesOffered.DataSource = se.GetAllSelectedServices(vServiceIds.Trim());
        lstSelectedServicesOffered.DataValueField = "ServiceId";
        lstSelectedServicesOffered.DataTextField = "ServiceName";
        lstSelectedServicesOffered.DataBind();

        LocationsTransaction lo = new LocationsTransaction();
        lstLocation.DataSource = lo.GetAllUnSelectedLocations(connstring, vLocationIds.Trim());
        lstLocation.DataValueField = "LocationId";
        lstLocation.DataTextField = "LocationName";
        lstLocation.DataBind();

        lstSelectedLocation.DataSource = lo.GetAllSelectedLocations(vLocationIds.Trim());
        lstSelectedLocation.DataValueField = "LocationId";
        lstSelectedLocation.DataTextField = "LocationName";
        lstSelectedLocation.DataBind();

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string vISOStandard = "";
        if ((chkISOStandard_white.Items[0].Selected == true) && (chkISOStandard_white.Items[1].Selected == true))
        {
            vISOStandard = "11";
        }
        else if ((chkISOStandard_white.Items[0].Selected == true) && (chkISOStandard_white.Items[1].Selected == false))
        {
            vISOStandard = "10";
        }
        else if ((chkISOStandard_white.Items[0].Selected == false) && (chkISOStandard_white.Items[1].Selected == true))
        {
            vISOStandard = "01";
        }
        else
        {
            vISOStandard = "00";
        }


        SupplierTransaction s = new SupplierTransaction();
        int i = s.UpdateSupplier(connstring, ViewState["VendorId"].ToString().Trim(), ddlSupplierType.SelectedItem.Value.Trim(), ddlPCABClass.SelectedItem.Value.Trim(), vISOStandard);
        s.DeleteVendorCategorySubCategory(connstring, ViewState["VendorId"].ToString().Trim());
        string vSubCategory = hdnSubCategory.Text.Trim();
        string[] VendorSubCategory = vSubCategory.Split(Convert.ToChar(","));
        s.InsertSubCategory(connstring, ViewState["VendorId"].ToString().Trim(), VendorSubCategory);
        string vCategory = hdnCategories.Text.Trim();
        string[] VendorCategory = vCategory.Split(Convert.ToChar(","));
        s.InsertCategory(connstring, ViewState["VendorId"].ToString().Trim(), VendorCategory);
        s.DeleteBSLP(connstring, ViewState["VendorId"].ToString().Trim());
        s.InsertBrandsMain(connstring, Session["VendorId"].ToString().Trim(), hdnBrands.Text.Trim());
        s.InsertItemsCarriedMain(connstring, Session["VendorId"].ToString().Trim(), hdnItems.Text.Trim());
        s.InsertServicesOfferedMain(connstring, Session["VendorId"].ToString().Trim(), hdnServices.Text.Trim());
        s.InsertLocationMain(connstring, Session["VendorId"].ToString().Trim(), hdnLocation.Text.Trim());

        Session["VendorId"] = ViewState["VendorId"].ToString().Trim();
        Response.Redirect("supplierdetails.aspx");

    }

    private void FillDropDownLists()
    {
        //CategoryTransaction c = new CategoryTransaction();
        //ddlCategory.DataSource = c.GetAllCategories();
        //ddlCategory.DataTextField = "CategoryName";
        //ddlCategory.DataValueField = "CategoryId";
        //ddlCategory.DataBind();
        ListItem l = new ListItem();
        //l.Value = "";
        //l.Text = "---Category---";
        //ddlCategory.Items.Insert(0, l);

        //SubCategory s = new SubCategory();
        //ddlSubCategory.DataSource = s.GetAllSubCategories(connstring);
        //ddlSubCategory.DataTextField = "SubCategoryName";
        //ddlSubCategory.DataValueField = "SubCategoryId";
        //ddlSubCategory.DataBind();
        //l = new ListItem();
        //l.Value = "";
        //l.Text = "---Sub-Category---";
        //ddlSubCategory.Items.Insert(0, l);

        SupplierType sty = new SupplierType();
        ddlSupplierType.DataSource = sty.GetAllSupplierTypes(connstring);
        ddlSupplierType.DataTextField = "SupplierTypeDesc";
        ddlSupplierType.DataValueField = "SupplierTypeId";
        ddlSupplierType.DataBind();
        l = new ListItem();
        l.Value = "";
        l.Text = "---Supplier Type---";
        ddlSupplierType.Items.Insert(0, l);

        PCABClassTransaction p = new PCABClassTransaction();
        ddlPCABClass.DataSource = p.GetAllPCABClass(connstring);
        ddlPCABClass.DataValueField = "PCABClassId";
        ddlPCABClass.DataTextField = "PCABClassName";
        ddlPCABClass.DataBind();
        l = new ListItem();
        l.Value = "";
        l.Text = "-- PCAB Class --";
        ddlPCABClass.Items.Insert(0, l);

        ISOStandard iso = new ISOStandard();
        chkISOStandard_white.DataSource = iso.GetISOStandard(connstring);
        chkISOStandard_white.DataTextField = "ISOStandardName";
        chkISOStandard_white.DataValueField = "ISOStandardId";
        chkISOStandard_white.DataBind();
    }

   
}
