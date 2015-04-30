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

public partial class web_buyerscreens_RegProductSubCategory : System.Web.UI.Page
{

    private string connstring = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (!IsPostBack)
        {
            PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Add Sub Category");

            if (Session["CategoryId"] != null)
                ViewState["CategoryId"] = Session["CategoryId"].ToString().Trim();
            CategoryTransaction c = new CategoryTransaction();
            ddlCategory.DataSource = c.GetAllCategories(connstring);
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "CategoryId";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, "");
            if (ViewState["CategoryId"] != null)
                ddlCategory.SelectedIndex = ddlCategory.Items.IndexOf(ddlCategory.Items.FindByValue(ViewState["CategoryId"].ToString().Trim()));
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlCategory.SelectedIndex = 0;
        txtSubCategoryName.Text = "";
        txtSubCategoryDesc.Text = "";
        lblMessage.Text = "";
        lblSubCategoryName.Text = "";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        lblSubCategoryName.Text = "";
        ProductsTransaction P = new ProductsTransaction();
        int count = P.InsertSubCategory(connstring, ddlCategory.SelectedItem.Value.Trim(), txtSubCategoryName.Text.Trim(), txtSubCategoryDesc.Text.Trim());
        if (count > 0)
        {
            lblSubCategoryName.Text = "Sub-Category Name already exists. Please enter another Sub-Category Name.";
            lblMessage.Text = "";
        }
        else
        {
            lblSubCategoryName.Text = "";
            lblMessage.Text = "Insert Successful.";
        }

    }
}
