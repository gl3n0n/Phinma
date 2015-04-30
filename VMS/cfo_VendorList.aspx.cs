using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ava.lib;
using Ava.lib.constant;

public partial class cfo_VendorList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null || Session["UserId"].ToString() == "") Response.Redirect("login.aspx");
        if (Session["SESSION_USERTYPE"].ToString() != ((int)Constant.USERTYPE.CFO).ToString()) Response.Redirect("login.aspx");
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
            Response.Redirect("cfo_vendorDetails.aspx");

        }
    }
}