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

public partial class WEB_buyer_screens_Product : System.Web.UI.Page
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
            PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Registered Products");
            lblMessage.Text = "";
            BrandsTransaction brand = new BrandsTransaction();
            ServicesTransaction serv = new ServicesTransaction();
            SubCategory sub = new SubCategory();

            ddlBrand.DataSource = brand.GetAllBrands(connstring);
            ddlBrand.DataTextField = "BrandName";
            ddlBrand.DataValueField = "BrandId";
            ddlBrand.DataBind();
            ListItem lst = new ListItem();
            lst.Text = "Select Brands";
            lst.Value = "";
            ddlBrand.Items.Insert(0, lst);

            ddlServices.DataSource = serv.GetAllServices(connstring);
            ddlServices.DataTextField = "ServiceName";
            ddlServices.DataValueField = "ServiceId";
            ddlServices.DataBind();
            ListItem lst1 = new ListItem();
            lst1.Text = "Select Services";
            lst1.Value = "";
            ddlServices.Items.Insert(0, lst1);

            ddlSubCategory.DataSource = sub.GetAllSubCategories(connstring);
            ddlSubCategory.DataTextField = "SubCategoryName";
            ddlSubCategory.DataValueField = "SubCategoryId";
            ddlSubCategory.DataBind();
            ListItem lst2 = new ListItem();
            lst2.Text = "Select Sub-category";
            lst2.Value = "";
            ddlSubCategory.Items.Insert(0, lst2);
            dvFooter.Visible = false;

        }

    }

    protected void gvProducts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Select"))
        {
            Session["SKU"] = e.CommandArgument.ToString().Trim();
            Response.Redirect("regproduct.aspx");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Bind();  
    }

    private DataTable CreateDataSource()
    {
        ProductsTransaction p = new ProductsTransaction();
        DataTable dtProducts = p.SearchProduct(connstring, txtSKU.Text.Trim(), ddlBrand.SelectedItem.Value.Trim(), ddlServices.SelectedItem.Value.Trim(), ddlSubCategory.SelectedItem.Value.Trim());
        return dtProducts;
    }

    private void Bind()
    {
        DataTable dt = CreateDataSource();
        if (dt.Rows.Count > 0)
        {
            DataView m_DataView = new DataView(dt);
            gvProducts.DataSource = m_DataView;
            gvProducts.DataBind();
            lblMessage.Text = "";
            dvFooter.Visible = true;
        }
        else
        {
            gvProducts.DataSource = dt;
            gvProducts.DataBind();
            lblMessage.Text = "No Search Results.";
            dvFooter.Visible = false;
        }
        
    }

    private DataTable CreateEmptyTable()
    {
        DataTable dt = new DataTable();
        DataColumn dc = new DataColumn("SKU", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("ItemName", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("ProductDescription", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("ServiceName", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("BrandName", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("SubCategoryName", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("UnitOfMeasure", typeof(System.String));
        dt.Columns.Add(dc);
        DataRow dr = dt.NewRow();
        dr["SKU"] = "";
        dr["ItemName"] = "";
        dr["ProductDescription"] = "";
        dr["ServiceName"] = "";
        dr["BrandName"] = "";
        dr["SubCategoryName"] = "";
        dr["UnitOfMeasure"] = "";
        dt.Rows.Add(dr);
        return dt;
    }

    protected void gvProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataTable dt = CreateDataSource();
        if (dt != null)
        {
            DataView m_DataView = new DataView(dt);

            gvProducts.DataSource = m_DataView;
            gvProducts.PageIndex = e.NewPageIndex;
            gvProducts.DataBind();
        }
    }
}
