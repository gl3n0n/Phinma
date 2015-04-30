using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class usercontrols_TopNav_vendor : System.Web.UI.UserControl
{
    string queryString;
    protected void Page_Load(object sender, EventArgs e)
    {
        queryString = Request.QueryString["VendorId"] == "" || Request.QueryString["VendorId"] == null ? "" : "?VendorId=" + Request.QueryString["VendorId"];
        //HomeLink.add
        //HomeLink.
        HomeLink.NavigateUrl = "../vendor_Home.aspx" + queryString;
    }
}