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
using EBid.lib;
using EBid.ConnectionString;

public partial class web_buyerscreens_ProductItems : System.Web.UI.Page
{
    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        //if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
        //    Response.Redirect("../unauthorizedaccess.aspx");

        if (!(IsPostBack))
        {
            if (Request.QueryString[Constant.QS_CATEGORYID]!=null)
                hdnCategoryId.Value = Request.QueryString[Constant.QS_CATEGORYID].Trim();
            if (Request.QueryString[Constant.QS_SUBCATEGORYID] != null)
                hdnSubCategoryId.Value = Request.QueryString[Constant.QS_SUBCATEGORYID].Trim();
            if (Request.QueryString[Constant.QS_SEARCHTEXT] != null)
                txtSearch.Text = Request.QueryString[Constant.QS_SEARCHTEXT].Trim();
            if (Request.QueryString[Constant.QS_CONTROLID] != null)
                hdnControlId.Value = Request.QueryString[Constant.QS_CONTROLID].Trim();

            ViewState["SearchString"] = txtSearch.Text.Trim();
            ViewState["Mode"] = Constant.SHOWSKU.ToString().Trim();

            CategoryTransaction c = new CategoryTransaction();
            lblCategory.Text = c.GetCategoryNameById(connstring, hdnCategoryId.Value.Trim());

            SubCategory s = new SubCategory();
            lblSubCategory.Text = s.GetSubCategoryNameById(connstring, hdnSubCategoryId.Value.Trim());

            btnClose.Attributes.Add("onclick", "Close();");
            btnOK.Attributes.Add("onclick", "SelectProduct('" + hdnControlId.Value + "');");

            ProductsTransaction p = new ProductsTransaction();
            lstProducts.DataSource = p.QueryProducts(connstring, ViewState["SearchString"].ToString().Trim(), hdnSubCategoryId.Value.Trim(), hdnCategoryId.Value.Trim(), ViewState["Mode"].ToString().Trim());
            lstProducts.DataTextField = ((ViewState["Mode"].ToString().Trim() == Constant.HIDESKU.ToString().Trim()) ? "ItemName_NoSKU" : "ItemName");
            lstProducts.DataValueField = "VALUE";
            lstProducts.DataBind();            
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ViewState["SearchString"] = txtSearch.Text.Trim();
        ProductsTransaction p = new ProductsTransaction();
        lstProducts.DataSource = p.QueryProducts(connstring, ViewState["SearchString"].ToString().Trim(), hdnSubCategoryId.Value.Trim(), hdnCategoryId.Value.Trim(), ViewState["Mode"].ToString().Trim());        
        lstProducts.DataTextField = ((ViewState["Mode"].ToString().Trim() == Constant.HIDESKU.ToString().Trim()) ? "ItemName_NoSKU" : "ItemName");
        lstProducts.DataValueField = "VALUE";
        lstProducts.DataBind();
    }  

    protected void btnShowAll_Click(object sender, EventArgs e)
    {
        ViewState["SearchString"] = "";
        ProductsTransaction p = new ProductsTransaction();
        lstProducts.DataSource = p.QueryProducts(connstring, "", hdnSubCategoryId.Value.Trim(), hdnCategoryId.Value.Trim(), ViewState["Mode"].ToString().Trim());
        lstProducts.DataTextField = ((ViewState["Mode"].ToString().Trim() == Constant.HIDESKU.ToString().Trim()) ? "ItemName_NoSKU" : "ItemName");
        lstProducts.DataValueField = "VALUE";
        lstProducts.DataBind();
    }

    protected void btnSKU_Click(object sender, EventArgs e)
    {
        if (btnSKU.Text.ToUpper().Trim() == "HIDE SKU")
        {
            btnSKU.Text = "Show SKU";
            ViewState["Mode"] = Constant.HIDESKU.ToString().Trim();
        }
        else
        {
            btnSKU.Text = "Hide SKU";
            ViewState["Mode"] = Constant.SHOWSKU.ToString().Trim();
        }
        ProductsTransaction p = new ProductsTransaction();
        lstProducts.DataSource = p.QueryProducts(connstring, ViewState["SearchString"].ToString().Trim(), hdnSubCategoryId.Value.Trim(), hdnCategoryId.Value.Trim(), ViewState["Mode"].ToString().Trim());
        lstProducts.DataTextField = ((ViewState["Mode"].ToString().Trim()==Constant.HIDESKU.ToString().Trim())? "ItemName_NoSKU" : "ItemName");
        lstProducts.DataValueField = "VALUE";
        lstProducts.DataBind();
    }

}
