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

public partial class dnb_Home : System.Web.UI.Page
{
    SqlDataReader oReader;
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
    //string query;
    //SqlCommand cmd;
    //SqlConnection conn;
    string sCommand;

    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null) Response.Redirect("login.aspx");
        if (Session["SESSION_USERTYPE"].ToString() != "12") Response.Redirect("login.aspx");
        if (IsPostBack) 
        {
            SaveToDB();
        }
        PopulateFields();
    }

    void PopulateFields() 
    {
        sCommand = "SELECT count(*) AS totalUsers FROM (SELECT t1.VendorId FROM tblVendor t1 WHERE (t1.AuthenticationTicket IS NOT NULL OR t1.AuthenticationTicket != '') AND (t1.IsAuthenticated IS NULL OR t1.IsAuthenticated = 0) AND (t1.Status = 1 OR t1.Status = 9) AND EndorsedTo=" + Session["UserId"].ToString() + ") t2";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            countAuthentication.Text = oReader["totalUsers"].ToString();
        } oReader.Close();

        sCommand = "SELECT count(*) AS totalUsers FROM (SELECT t1.VendorId FROM tblVendor t1 WHERE (t1.AuthenticationTicket IS NOT NULL OR t1.AuthenticationTicket != '') AND (t1.IsAuthenticated IS NOT NULL OR t1.IsAuthenticated = 1) AND (t1.Status = 1) AND EndorsedTo=" + Session["UserId"].ToString() + ") t2";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            countVendors.Text = oReader["totalUsers"].ToString();
        } oReader.Close();

        sCommand = "SELECT count(*) AS totalUsers FROM (SELECT t1.VendorId FROM tblVendor t1 WHERE t1.IsAuthenticated = 1 AND t1.Status > 1 AND t1.Status <> 9 AND EndorsedTo=" + Session["UserId"].ToString() + ") t2";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            countApproved.Text = oReader["totalUsers"].ToString();
        } oReader.Close();

    }


    void SaveToDB()
    {
        //string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
        //bool errAuthTicket;

        //if (Request.Form["__EVENTTARGET"] == "AuthTicket") 
        //{
        //    query = "SELECT * FROM tblVendor WHERE AuthenticationTicket = @AuthenticationTicket AND (IsAuthenticated IS NULL OR IsAuthenticated = 0)";
        //    //query = "sp_GetVendorInformation"; //##storedProcedure
        //    using (conn = new SqlConnection(connstring))
        //    {
        //        using (cmd = new SqlCommand(query, conn))
        //        {
        //            //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
        //            cmd.Parameters.AddWithValue("@AuthenticationTicket", AuthenticationTicket.Value.Trim());
        //            conn.Open();
        //            //Process results
        //            oReader = cmd.ExecuteReader();
        //            if (oReader.HasRows)
        //            {
        //                errAuthTicket = true;
        //                errNotificationAuthTicket.Text = "";
        //            }
        //            else
        //            {
        //                errAuthTicket = false;
        //                errNotificationAuthTicket.Text = "Vendor Authentication failed.";
        //                errNotificationAuthTicket.ForeColor = Color.Red;
        //            }
        //        }
        //    }

        //    if (errAuthTicket)
        //    {
        //        query = "UPDATE tblVendor SET IsAuthenticated = 1 WHERE AuthenticationTicket = @AuthenticationTicket";
        //        //query = "sp_GetVendorInformation"; //##storedProcedure
        //        using (conn = new SqlConnection(connstring))
        //        {
        //            using (cmd = new SqlCommand(query, conn))
        //            {
        //                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
        //                cmd.Parameters.AddWithValue("@dnbUserId", Convert.ToInt32(Session["UserId"]));
        //                cmd.Parameters.AddWithValue("@AuthenticationTicket", AuthenticationTicket.Value.Trim());
        //                conn.Open(); cmd.ExecuteNonQuery();
        //            }
        //        }
        //        errNotificationAuthTicket.Text = "Vendor successfully authenticated.";
        //        errNotificationAuthTicket.ForeColor = Color.Blue;
        //        AuthenticationTicket.Value = "";
        //    }
            

        //} 
        //else if (Request.Form["__EVENTTARGET"] == "CompanyName") 
        //{
        //    query = "SELECT * FROM tblVendor WHERE CompanyName = @CompanyName AND (IsAuthenticated IS NULL OR IsAuthenticated = 0)";
        //    //query = "sp_GetVendorInformation"; //##storedProcedure
        //    using (conn = new SqlConnection(connstring))
        //    {
        //        using (cmd = new SqlCommand(query, conn))
        //        {
        //            //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
        //            cmd.Parameters.AddWithValue("@CompanyName", CompanyName.Value.Trim());
        //            conn.Open();
        //            //Process results
        //            oReader = cmd.ExecuteReader();
        //            if (oReader.HasRows)
        //            {
        //                errAuthTicket = true;
        //                errNotificationCompanyName.Text = "";
        //            }
        //            else
        //            {
        //                errAuthTicket = false;
        //                errNotificationCompanyName.Text = "Vendor Authentication failed.";
        //                errNotificationCompanyName.ForeColor = Color.Red;
        //            }
        //        }
        //    }

        //    if (errAuthTicket)
        //    {
        //        query = "UPDATE tblVendor SET IsAuthenticated = 1 WHERE CompanyName = @CompanyName";
        //        //query = "sp_GetVendorInformation"; //##storedProcedure
        //        using (conn = new SqlConnection(connstring))
        //        {
        //            using (cmd = new SqlCommand(query, conn))
        //            {
        //                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
        //                cmd.Parameters.AddWithValue("@CompanyName", CompanyName.Value.Trim());
        //                conn.Open(); cmd.ExecuteNonQuery();
        //            }
        //        }
        //        CompanyName.Value = "";
        //        errNotificationCompanyName.Text = "Vendor successfully authenticated.";
        //        errNotificationCompanyName.ForeColor = Color.Blue;
        //    }
        //}

    }
}