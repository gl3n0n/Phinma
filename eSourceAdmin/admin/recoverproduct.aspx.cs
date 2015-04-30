using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EBid.lib;
using EBid.lib.constant;
using EBid.lib.bid.trans;
using EBid.lib.utils;

public partial class web_buyerscreens_recoverproduct : System.Web.UI.Page
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
            if (Session["message"] != null)
            {
                lblMessage.Text = Session["message"].ToString().Trim();
            }
        }
    }

    protected void gvProducts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("recoverProduct"))
        {
            ProductsTransaction P = new ProductsTransaction();
            int result = P.UpdateProductStatus(e.CommandArgument.ToString(), 0, connstring);
            if (result == 0)
            {
                Session["message"] = "Product successfully recovered.";
                Response.Redirect("recoverproduct.aspx");
            }
            else
            {
                Session["message"] = "Recover failed.";
            }

        }

    }
}
