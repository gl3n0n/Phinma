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

public partial class web_buyerscreens_RegProductCategory : System.Web.UI.Page
{
    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Add Category");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtCategoryId.Text = "";
        txtCategoryName.Text = "";
        txtCategoryDesc.Text = "";
        lblMessage.Text = "";
        lblCategoryId.Text = "";
        lblCategoryName.Text = "";

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        lblCategoryId.Text = "";
        lblCategoryName.Text = "";
        ProductsTransaction P = new ProductsTransaction();
        Category cat = P.InsertCategory(connstring, txtCategoryId.Text.Trim(), txtCategoryName.Text.Trim(), txtCategoryDesc.Text.Trim());
        if ((cat.IdCount == "0") && (cat.NameCount == "0"))
            lblMessage.Text = "Insert successful.";
        else
        {
            if (cat.IdCount.ToString().Trim() != "0")
                lblCategoryId.Text = "Category Id already exists!";
            if (cat.NameCount.ToString().Trim() != "0")
                lblCategoryName.Text = "Category Name already exists!";
        }  
    }
}
