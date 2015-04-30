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

public partial class web_usercontrol_admin_AdminLeftNavList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void lnkRegisterProduct_Click(object sender, EventArgs e)
    {
        Session["SKU"] = null;
        Response.Redirect("regproduct.aspx");
    }
    protected void lnkDeletedProducts_Click(object sender, EventArgs e)
    {
        Session["SKU"] = null;
        Session["message"] = null;
        Response.Redirect("recoverproduct.aspx");
    }
}
