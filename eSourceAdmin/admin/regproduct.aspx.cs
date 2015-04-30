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
using EBid.lib.constant;

public partial class web_buyer_screens_RegProduct : System.Web.UI.Page
{
    private string connstring;
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.ADMINISTRATOR)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (!(Page.IsPostBack))
        {
            if (Session["SKU"] != null)
                ViewState["SKU"] = Session["SKU"].ToString().Trim();

            BrandsTransaction brand = new BrandsTransaction();
            ServicesTransaction serv = new ServicesTransaction();
            SubCategory sub = new SubCategory();
            CategoryTransaction c = new CategoryTransaction();
            ServicesTransaction s = new ServicesTransaction();
            UnitOfMeasurementTransaction u = new UnitOfMeasurementTransaction();

            ddlUnitOfMeasure.DataSource = u.GetAllUnitsOfMeasurement(connstring);
            ddlUnitOfMeasure.DataTextField = "UnitOfMeasureName";
            ddlUnitOfMeasure.DataValueField = "UnitOfMeasureID";
            ddlUnitOfMeasure.DataBind();
            ddlUnitOfMeasure.Items.Insert(0, "");


            ddlCategory.DataSource = c.GetAllCategories(connstring);
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "CategoryId";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, "");

            ddlSubCategory.DataSource = sub.GetAllSubCategories(connstring);
            ddlSubCategory.DataTextField = "SubCategoryName";
            ddlSubCategory.DataValueField = "SubCategoryId";
            ddlSubCategory.DataBind();
            ddlSubCategory.Items.Insert(0, "");

            ddlBrand.DataSource = brand.GetAllBrands(connstring);
            ddlBrand.DataTextField = "BrandName";
            ddlBrand.DataValueField = "BrandId";
            ddlBrand.DataBind();
            ddlBrand.Items.Insert(0, "");

            ddlServiceType.DataSource = s.GetAllServices(connstring);
            ddlServiceType.DataTextField = "ServiceName";
            ddlServiceType.DataValueField = "ServiceId";
            ddlServiceType.DataBind();
            ddlServiceType.Items.Insert(0, "");

            if (ViewState["SKU"] != null)
            {
                ProductsTransaction p = new ProductsTransaction();
                Product po = new Product();
                po = p.QueryProductBySKU(ViewState["SKU"].ToString().Trim(), connstring);
                txtSKU.Enabled = false;
                txtSKU.Text = po.SKU.Trim();
                txtItemName.Text = po.ItemName.Trim();
                txtProductDescription.Text = po.ProductDescription.Trim();
                ddlUnitOfMeasure.SelectedIndex = ddlUnitOfMeasure.Items.IndexOf(ddlUnitOfMeasure.Items.FindByValue(po.UnitOfMeasure.Trim()));
                ddlCategory.SelectedIndex = ddlCategory.Items.IndexOf(ddlCategory.Items.FindByValue(po.Category.Trim()));
                ddlSubCategory.SelectedIndex = ddlSubCategory.Items.IndexOf(ddlSubCategory.Items.FindByValue(po.SubCategory.Trim()));
                ddlBrand.SelectedIndex = ddlBrand.Items.IndexOf(ddlBrand.Items.FindByValue(po.Brand.Trim()));
                ddlServiceType.SelectedIndex = ddlServiceType.Items.IndexOf(ddlServiceType.Items.FindByValue(po.ServiceType.Trim()));
                btnDelete.Visible = true;
            }
        }

    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
            SubCategory s = new SubCategory();
            ddlSubCategory.DataSource = s.GetSubCategoryByCategoryId(ddlCategory.SelectedItem.Value.Trim(), connstring);
            ddlSubCategory.DataTextField = "SubCategoryName";
            ddlSubCategory.DataValueField = "SubCategoryId";
            ddlSubCategory.DataBind();
            ddlSubCategory.Items.Insert(0, "");
    }
    


    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtSKU.Text = "";
        txtItemName.Text = "";
        txtProductDescription.Text = "";
        ddlUnitOfMeasure.SelectedIndex = 0;
        ddlCategory.SelectedIndex = 0;
        ddlSubCategory.SelectedIndex = 0;
        ddlServiceType.SelectedIndex = 0;
        ddlBrand.SelectedIndex = 0;
        lblSKUAlreadyExists.Text = "";
        lblMessage.Text = "";
        Session["SKU"] = null;
    }

    private void InsertProduct()
    {
        ProductsTransaction P = new ProductsTransaction();
        int count = P.InsertProduct(txtSKU.Text.Trim(), txtItemName.Text.Trim(), txtProductDescription.Text.Trim(), ddlUnitOfMeasure.SelectedItem.Value.Trim(), ddlCategory.SelectedItem.Value.Trim(), ddlSubCategory.SelectedItem.Value.Trim(), ddlBrand.SelectedItem.Value.Trim(), ddlServiceType.SelectedItem.Value.Trim(), connstring);
        if (count > 0)
        {
            lblSKUAlreadyExists.Text = "Product SKU already exists. Please enter another SKU.";
            lblMessage.Text = "";
        }
        else
        {
            lblSKUAlreadyExists.Text = "";
            lblMessage.Text = "Insert Successful.";
            
        }
    }

    private void UpdateProduct()
    {
        ProductsTransaction P = new ProductsTransaction();
        int result = P.UpdateProduct(txtSKU.Text.Trim(), txtItemName.Text.Trim(), txtProductDescription.Text.Trim(), ddlUnitOfMeasure.SelectedItem.Value.Trim(), ddlCategory.SelectedItem.Value.Trim(), ddlSubCategory.SelectedItem.Value.Trim(), ddlBrand.SelectedItem.Value.Trim(), ddlServiceType.SelectedItem.Value.Trim(), connstring);
        if (result == 0)
        {
            lblMessage.Text = "Update Successful.";            
        }
        else
        {
            lblMessage.Text = "Update Failed.";
        }
        
    }

    private void DeleteProduct()
    {
        ProductsTransaction P = new ProductsTransaction();
        int result = P.UpdateProductStatus(ViewState["SKU"].ToString(), 1, connstring);
        if (result == 0)
        {
            lblMessage.Text = "Product successfully deleted.";
            Response.Redirect("product.aspx");
        }
        else
        {
            lblMessage.Text = "Delete failed.";
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ViewState["SKU"] != null)
        {
            UpdateProduct();
        }
        else
        {
            InsertProduct();
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        DeleteProduct();
    }
}
