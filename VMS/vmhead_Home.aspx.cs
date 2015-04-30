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

public partial class vmhead_Home : System.Web.UI.Page
{
    //SqlDataReader oReader;
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
    //string query;
    //SqlCommand cmd;
    //SqlConnection conn;

    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null || Session["UserId"].ToString() == "") Response.Redirect("login.aspx");
        if (Session["SESSION_USERTYPE"].ToString() != "16") Response.Redirect("login.aspx");
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
        string sCommand = "";

        sCommand = "SELECT count(*) AS totalUsers FROM (SELECT t1.VendorId FROM tblVendor t1 WHERE t1.IsAuthenticated = 1 AND t1.Status = 3) t2";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            countVendors.Text = oReader["totalUsers"].ToString();
        } oReader.Close();

        sCommand = "SELECT count(*) AS totalUsers FROM (SELECT t1.VendorId FROM tblVendor t1 WHERE t1.IsAuthenticated = 1 AND t1.Status = 4) t2";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            countEndorsed.Text = oReader["totalUsers"].ToString();
        } oReader.Close();

        sCommand = "SELECT count(*) AS totalUsers FROM (SELECT t1.VendorId FROM tblVendor t1 WHERE t1.IsAuthenticated = 1 AND t1.Status = 7 AND (clarifiedtoVMRecoDate IS NOT NULL OR clarifiedtoVMRecoDate != '')) t2";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            countClarify.Text = oReader["totalUsers"].ToString();
        } oReader.Close();

        sCommand = "SELECT count(*) AS totalUsers FROM (SELECT t1.VendorId FROM tblVendor t1 WHERE t1.IsAuthenticated = 1 AND t1.Status > 3) t2";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            countApproved.Text = oReader["totalUsers"].ToString();
        } oReader.Close();

        sCommand = "SELECT count(*) AS totalUsers FROM (SELECT t1.VendorId FROM tblVendor t1 WHERE t1.IsAuthenticated = 1 AND t1.Status = 8) t2";
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
            vmofficer_VendorOngoing_List_Count.Text = oReader["totalUsers"].ToString();
        } oReader.Close();
        //query = "SELECT count(*) AS totalUsers FROM (SELECT t1.UserId FROM tblUsers t1 , tblUserTypes t2 WHERE (t1.AuthenticationTicket IS NOT NULL OR t1.AuthenticationTicket!='') AND t2.UserType = 11 AND t1.UserId = t2.UserId AND t1.IsAuthenticated = 1) t3";
        ////query = "sp_GetVendorInformation"; //##storedProcedure
        //using (conn = new SqlConnection(connstring))
        //{
        //    using (cmd = new SqlCommand(query, conn))
        //    {
        //        //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
        //        //cmd.Parameters.AddWithValue("@CompanyName", CompanyName.Value.Trim());
        //        conn.Open();
        //        //Process results
        //        oReader = cmd.ExecuteReader();
        //        if (oReader.HasRows)
        //        {
        //            //errAuthTicket = true;
        //            countVendors.Text = oReader["totalUsers"].ToString();
        //        }
        //    }
        //}
    }
    void SaveToDB()
    {
        

    }
}