using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class usercontrols_StepNav_vendor : System.Web.UI.UserControl
{
    string queryString;
    protected void Page_Load(object sender, EventArgs e)
    {
        //class=\"stepnav_current\" 
        queryString =  Request.QueryString["VendorId"]=="" ||  Request.QueryString["VendorId"]==null ? "" : "?VendorId=" + Request.QueryString["VendorId"];
        StepNav1.Text = "<li title=\"Vendor Information\" onclick=\"window.location='vendor_01_vendorInfo.aspx" + queryString + "'\">1</li>";
        StepNav2.Text = "<li title=\"Products &amp; Services\" onclick=\"window.location='vendor_02_productServices.aspx" + queryString + "'\">2</li>";
        StepNav3.Text = "<li title=\"Business Operational\" onclick=\"window.location='vendor_03_businessOperational.aspx" + queryString + "'\">3</li>";
        StepNav4.Text = "<li title=\"Legal\" onclick=\"window.location='vendor_04_Legal.aspx" + queryString + "'\">4</li>";
        StepNav5.Text = "<li title=\"Financial\" onclick=\"window.location='vendor_05_financialInfo.aspx" + queryString + "'\">5</li>";
        //StepNav6.Text = "<li title=\"Others\" onclick=\"window.location='vendor_06_Others.aspx" + queryString + "'\">6</li>";
        StepNav7.Text = "<li title=\"Conflict of interest\" onclick=\"window.location='vendor_07_Conflict.aspx" + queryString + "'\">6</li>";
        StepNav8.Text = "<li title=\"Undertakings\" onclick=\"window.location='vendor_08_Undertakings.aspx" + queryString + "'\">7</li>";

        if (HttpContext.Current.Request.Url.AbsoluteUri.ToLower().Contains("vendor_01"))
        {
            StepNav1.Text = "<li class=\"stepnav_current\" title=\"Vendor Information\" onclick=\"window.location='vendor_01_vendorInfo.aspx" + queryString + "'\">1</li>";
        }
        if (HttpContext.Current.Request.Url.AbsoluteUri.ToLower().Contains("vendor_02"))
        {
            StepNav2.Text = "<li class=\"stepnav_current\" title=\"Products &amp; Services\" onclick=\"window.location='vendor_02_productServices.aspx" + queryString + "'\">2</li>";
        }
        if (HttpContext.Current.Request.Url.AbsoluteUri.ToLower().Contains("vendor_03"))
        {
            StepNav3.Text = "<li class=\"stepnav_current\" title=\"Business Operational\" onclick=\"window.location='vendor_03_businessOperational.aspx" + queryString + "'\">3</li>";
        }
        if (HttpContext.Current.Request.Url.AbsoluteUri.ToLower().Contains("vendor_04"))
        {
            StepNav4.Text = "<li  class=\"stepnav_current\" title=\"Legal\" onclick=\"window.location='vendor_04_Legal.aspx" + queryString + "'\">4</li>";
        }
        if (HttpContext.Current.Request.Url.AbsoluteUri.ToLower().Contains("vendor_05"))
        {
            StepNav5.Text = "<li class=\"stepnav_current\" title=\"Financial\" onclick=\"window.location='vendor_05_financialInfo.aspx" + queryString + "'\">5</li>";
        }
        if (HttpContext.Current.Request.Url.AbsoluteUri.ToLower().Contains("vendor_06"))
        {
            //StepNav6.Text = "<li class=\"stepnav_current\" title=\"Others\" onclick=\"window.location='vendor_06_Others.aspx" + queryString + "'\">6</li>";
        }
        if (HttpContext.Current.Request.Url.AbsoluteUri.ToLower().Contains("vendor_07"))
        {
            StepNav7.Text = "<li class=\"stepnav_current\" title=\"Conflict of interest\" onclick=\"window.location='vendor_07_Conflict.aspx" + queryString + "'\">6</li>";
        }
        if (HttpContext.Current.Request.Url.AbsoluteUri.ToLower().Contains("vendor_08"))
        {
            StepNav8.Text = "<li class=\"stepnav_current\" title=\"Undertaking\" onclick=\"window.location='vendor_08_Undertakings.aspx" + queryString + "'\">7</li>";
        }
    }
}