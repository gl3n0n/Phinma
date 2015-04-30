using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Ava.lib;
using Ava.lib.constant;

public partial class vmofficer_NewApplicants_List : System.Web.UI.Page
{
    //SqlDataReader oReader;
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
    //string query, sCommand;
    //SqlCommand cmd;
    //SqlConnection conn;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null) Response.Redirect("login.aspx");
        if (Session["SESSION_USERTYPE"].ToString() != ((int)Constant.USERTYPE.VMOFFICER).ToString()) Response.Redirect("login.aspx"); 

        Session["VendorEmail"] = "";
        Session["VendorCompany"] = "";
        Session["VendorApplicantId"] = "";
    }
    protected void gvBids_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Details"))
        {
            //Session["ViewOption"] = "AsBuyer";
            string sArg = e.CommandArgument.ToString().Trim();
            char[] mySeparator = new char[] { ';' };
            string[] Arr = sArg.Split(mySeparator);
            //Session["VendorEmail"] = "";
            Session["VendorEmail"] = Arr[0].ToString();
            Session["VendorCompany"] = Arr[1].ToString();
            Session["VendorApplicantId"] = Arr[2].ToString();

            //Session["VendorEmail"] = e.CommandArgument.ToString().Trim();

            Session["PrevUrl"] = HttpContext.Current.Request.Url.AbsoluteUri;
            Session["vendorDetails"] = "vmofficer_VendorDetails.aspx";
            Response.Redirect("vmofficer_VendorDetails.aspx");

        }
    }
}