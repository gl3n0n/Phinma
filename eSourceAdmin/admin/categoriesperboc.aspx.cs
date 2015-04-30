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

public partial class admin_categoriesperboc : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CategoryPerBOCMessage"] != null)
        {
            lblMsg.Text = Session["CategoryPerBOCMessage"].ToString() + "<br /><br />";
            lblMsg.Visible = true;
            Session["CategoryPerBOCMessage"] = null;
        }
        else
            lblMsg.Visible = false;
    }

    protected void gvBOCProcurement_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("EditBOCProcurement"))
        {
            Response.Redirect("editcategoriesperboc.aspx?BOCId=" + e.CommandArgument.ToString().Trim());
        }
    }
}
