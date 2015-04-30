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

public partial class vmofficer_Home : System.Web.UI.Page
{
    SqlDataReader oReader;
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
    //string query; 
    string sCommand;
    //SqlCommand cmd;
    //SqlConnection conn;

    protected void TestShowAllSessions()
    {  //test show all session
        string str = null;
        foreach (string key in HttpContext.Current.Session.Keys)
        { str += string.Format("<b>{0}</b>: {1};  ", key, HttpContext.Current.Session[key].ToString()); }
        Response.Write("<span style='font-size:12px'>" + str + "</span>");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //TestShowAllSessions();
        if (Session["UserId"] == null) Response.Redirect("login.aspx");
        if (Session["SESSION_USERTYPE"].ToString() != ((int)Constant.USERTYPE.VMOFFICER).ToString()) Response.Redirect("login.aspx");

        sCommand = "SELECT * FROM tblUserProcurementType WHERE ProcurementId = "+Session["UserId"];
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            if (oReader["ProcurementType"].ToString() == "3") 
            {
                //Response.Redirect("procurementApprover_Home.aspx");
            }
        } oReader.Close();


        if (IsPostBack)
        {
            SaveToDB();
        }
        PopulateFields();
    }

    void PopulateFields()
    {
        sCommand = "SELECT count(*) AS totalUsers FROM (SELECT t1.CompanyName, t1.EmailAdd, t1.LOIFileName, t1.DateCreated FROM tblVendorApplicants t1 WHERE (t1.Status = 1 OR t1.Status = 0)) t2";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            countVendors.Text = oReader["totalUsers"].ToString();
        } oReader.Close();

        sCommand = "SELECT count(*) AS totalUsers FROM (SELECT t1.CompanyName, t1.EmailAdd, t1.LOIFileName, t1.DateCreated FROM tblVendorApplicants t1  WHERE (t1.Status = 2)) t2";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            countApproved.Text = oReader["totalUsers"].ToString();
        } oReader.Close();

        sCommand = "SELECT count(*) AS totalUsers FROM (SELECT t1.CompanyName, t1.EmailAdd, t1.LOIFileName, t1.DateCreated FROM tblVendorApplicants t1   WHERE (t1.Status = 3)) t2";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            countRejected.Text = oReader["totalUsers"].ToString();
        } oReader.Close();

        sCommand = "SELECT count(*) AS totalUsers FROM (SELECT t1.CompanyName FROM tblVendor t1  WHERE t1.Status = 2) t2";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            vmofficer_VendorSIR_List_Count.Text = oReader["totalUsers"].ToString();
        } oReader.Close();



        sCommand = "SELECT count(*) AS totalUsers FROM (SELECT t1.CompanyName FROM tblVendor t1  WHERE t1.Status = 3) t2";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            vmofficer_VendorEndorsed_List_Count.Text = oReader["totalUsers"].ToString();
        } oReader.Close();


        sCommand = "SELECT count(*) AS totalUsers FROM (SELECT t1.CompanyName FROM tblVendor t1 WHERE t1.Status >= 1 AND t1.Status <> 7 AND t1.Status <> 9 AND t1.Status <> 8 AND t1.Status <> 6) t2";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            vmofficer_VendorOngoing_List_Count.Text = oReader["totalUsers"].ToString();
        } oReader.Close();


        sCommand = "SELECT count(*) AS totalUsers FROM (SELECT t1.CompanyName FROM tblVendor t1  WHERE t1.Status = 6) t2";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            vmofficer_VendorAcrredited_List_Count.Text = oReader["totalUsers"].ToString();
        } oReader.Close();


        sCommand = "SELECT count(*) AS totalUsers FROM (SELECT t1.CompanyName FROM tblVendor t1  WHERE t1.Status = 8) t2";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            vmofficer_VendorDisapproved_List_Count.Text = oReader["totalUsers"].ToString();
        } oReader.Close();

        sCommand = "SELECT count(*) AS totalUsers FROM (SELECT t1.CompanyName FROM tblVendor t1  WHERE (t1.NotificationSent is null OR t1.NotificationSent = '') AND (t1.Status = 6)) t2";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            vmofficer_VendorForNotification_List_Count.Text = oReader["totalUsers"].ToString();
        } oReader.Close();

        sCommand = "SELECT count(*) AS totalUsers FROM (SELECT t1.CompanyName FROM tblVendor t1  WHERE (t1.SendToSAP_Status = 1 AND t1.NotificationSent is not null) AND (t1.Status = 6)) t2";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            vmofficer_VendorForNotification_List_Sent_Count.Text = oReader["totalUsers"].ToString();
        } oReader.Close();
    }

    void SaveToDB()
    {
    }
}