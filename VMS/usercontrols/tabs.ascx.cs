using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class usercontrols_tabs : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //onclick=\"window.location='vendor_Home.aspx'\"
        //class=\"current\"
        Tablists3.Text = "<li><span>Vendor Admin</span></li>";
        Tablists1.Text = "<li><span>Vendor</span></li>";
        Tablists2.Text = "<li><span>Commodity Buyer</span></li>";
        //Tablists4.Text = "<li><span>Legal</span></li>";
        //Tablists5.Text = "<li><span>Technical Review</span></li>";
        //Tablists6.Text = "<li><span>Issue Management</span></li>";
        Tablists7.Text = "<li><span>MMD Manager</span></li>";
        Tablists8.Text = "<li><span>MMD VP</span></li>";
        //Tablists9.Text = "<li><span>VP</span></li>";

        //Response.Write(HttpContext.Current.Session["SESSION_USERTYPE"].ToString());
        if (HttpContext.Current.Session["SESSION_USERTYPE"].ToString() == "10")
        {
            Tablists3.Text = "<li class=\"rounded-corners-top-left current\" onclick=\"window.location='vmofficer_Home.aspx'\" ><span class=\"rounded-corners-top-left\">Vendor Admin</span></li>";
        }
        if (HttpContext.Current.Session["SESSION_USERTYPE"].ToString() == "11")
        {
            Tablists1.Text = "<li class=\"current\" onclick=\"window.location='vendor_Home.aspx'\" ><span>Vendor</span></li>";
        }
        if (HttpContext.Current.Session["SESSION_USERTYPE"].ToString() == "12")
        {
            Tablists2.Text = "<li class=\"current\" onclick=\"window.location='dnb_Home.aspx'\" ><span>Commodity Buyer</span></li>";
        }
        if (HttpContext.Current.Session["SESSION_USERTYPE"].ToString() == "13")
        {
            //Tablists4.Text = "<li class=\"current\" onclick=\"window.location='legal_Home.aspx'\" ><span>Legal</span></li>";
        }
        if (HttpContext.Current.Session["SESSION_USERTYPE"].ToString() == "14")
        {
            //Tablists5.Text = "<li class=\"current\" onclick=\"window.location='vmtech_Home.aspx'\" ><span>Technical Review</span></li>";
        }
        if (HttpContext.Current.Session["SESSION_USERTYPE"].ToString() == "15")
        {
            //Tablists6.Text = "<li class=\"current\" onclick=\"window.location='vmissue_Home.aspx'\" ><span>Issue Management</span></li>";
        }
        if (HttpContext.Current.Session["SESSION_USERTYPE"].ToString() == "16")
        {
            Tablists7.Text = "<li class=\"current\" onclick=\"window.location='vmhead_Home.aspx'\" ><span >MMD Manager</span></li>";
        }
        if (HttpContext.Current.Session["SESSION_USERTYPE"].ToString() == "17")
        {
            //Tablists8.Text = "<li class=\"current\" onclick=\"window.location='pvmd_Home.aspx'\" ><span >VP</span></li>";
        }
        if (HttpContext.Current.Session["SESSION_USERTYPE"].ToString() == "18")
        {
            Tablists8.Text = "<li class=\"current\" onclick=\"window.location='cfo_Home.aspx'\"  style=\"border-right:none;\"><span >MMD VP</span></li>";
        }
    }
}