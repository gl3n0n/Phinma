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

public partial class pvmd_Home : System.Web.UI.Page
{
    //SqlDataReader oReader;
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
    //string query;
    //SqlCommand cmd;
    //SqlConnection conn;

    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null || Session["UserId"].ToString() == "") Response.Redirect("login.aspx");
        if (Session["SESSION_USERTYPE"].ToString() != "17") Response.Redirect("login.aspx");
        if (IsPostBack) 
        {
            SaveToDB();
        }
        PopulateFields();
    }

    void PopulateFields() 
    {
        SqlDataReader oReader;
        string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;

        string sCommand = "SELECT count(*) AS totalUsers FROM (SELECT t1.VendorId FROM tblVendor t1 WHERE t1.IsAuthenticated = 1 AND t1.Status = 4 ) t2";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            countVendors.Text = oReader["totalUsers"].ToString();
        } oReader.Close();

        sCommand = "SELECT count(*) AS totalUsers FROM (SELECT t1.VendorId FROM tblVendor t1 WHERE t1.IsAuthenticated = 1 AND t1.Status = 5 ) t2";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            countVendorsEndorsed.Text = oReader["totalUsers"].ToString();
        } oReader.Close();

        sCommand = "SELECT count(*) AS totalUsers FROM (SELECT t1.VendorId FROM tblVendor t1 WHERE t1.IsAuthenticated = 1 AND t1.Status = 6 ) t2";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            countApproved.Text = oReader["totalUsers"].ToString();
        } oReader.Close();

        sCommand = "SELECT count(*) AS totalUsers FROM (SELECT t1.VendorId FROM tblVendor t1 WHERE t1.IsAuthenticated = 1 AND t1.Status = 8 ) t2";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            countDisapproved.Text = oReader["totalUsers"].ToString();
        } oReader.Close();

        sCommand = "SELECT count(*) AS totalUsers FROM (SELECT t1.CompanyName FROM tblVendor t1 WHERE t1.Status >= 1 AND t1.Status <> 7 AND t1.Status <> 9 AND t1.Status <> 8 AND t1.Status <> 6) t2";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            pvmd_VendorOngoing_List_Count.Text = oReader["totalUsers"].ToString();
        } oReader.Close();

    }
    void SaveToDB()
    {
        

    }
}