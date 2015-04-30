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
using EBid.lib.constant;
using EBid.lib.utils;

public partial class web_usercontrol_admin_AdminLeftNav : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void lnkEditProfile_Click(object sender, EventArgs e)
    {
        Response.Redirect("addadmin.aspx?userid=" + Session[Constant.SESSION_USERID].ToString().Trim() + "&usertype=4");
    }
}
