using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class usercontrols_list_ongoing : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void gvBids_RowCommand(object sender, GridViewCommandEventArgs e)
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
            Session["vendorDetails"] = "cfo_vendorDetails_View.aspx";
            Response.Redirect("cfo_vendorDetails_View.aspx");

        }
    }
}