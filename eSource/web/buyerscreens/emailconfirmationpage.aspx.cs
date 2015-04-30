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
using EBid.lib;
using EBid.ConnectionString;
using EBid.lib.constant;

public partial class web_buyerscreens_EmailConfirmationPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (!IsPostBack)
        {
            if (Session["successEmail"] != null)
                ViewState["successEmail"] = Session["successEmail"].ToString().Trim();
            lblAddresses.Text = ViewState["successEmail"].ToString().Trim();
        }
    }
    protected void btnHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("index.aspx");
    }
}
