using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class vmhead_EndorsedVendorList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null || Session["UserId"].ToString() == "") Response.Redirect("login.aspx");
        if (Session["SESSION_USERTYPE"].ToString() != "16") Response.Redirect("login.aspx");
        Session["VendorId"] = "";
    }
    protected void gvVendors_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Details"))
        {
            //Session["ViewOption"] = "AsBuyer";
            //string sArg = e.CommandArgument.ToString().Trim();
            //char[] mySeparator = new char[] { ';' };
            //string[] Arr = sArg.Split(mySeparator);
            //Session["VendorEmail"] = "";
            //Session["BuyerBidForBac"] = Arr[0].ToString();
            //Session["BuyerBacRefNo"] = Arr[1].ToString();

            Session["VendorId"] = e.CommandArgument.ToString().Trim();

            Session["PrevUrl"] = HttpContext.Current.Request.Url.AbsoluteUri;
            //Response.Redirect("dnb_vendorDetails.aspx?VendorId=" + e.CommandArgument.ToString().Trim());
            Response.Redirect("vmhead_vendorDetails_View.aspx");

        }
    }
}