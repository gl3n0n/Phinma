using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Configuration;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Ava;
using Ava.lib.utils;
using Ava.lib.user.trans;
using Ava.lib.constant;
using Ava.lib;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Net.Mail;

public partial class logout : System.Web.UI.Page
{
    protected void TestShowAllSessions()
    {  //test show all session
        string str = null;
        foreach (string key in HttpContext.Current.Session.Keys)
        { str += string.Format("<b>{0}</b>: {1};  ", key, HttpContext.Current.Session[key].ToString()); }
        Response.Write("<span style='font-size:12px'>" + str + "</span>");
    }
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString; 


    protected void Page_Load(object sender, EventArgs e)
    {
        //TestShowAllSessions(); 
        Session["SESSION_USERNAME"] = "";
        Session["SESSION_PASSWORD"] = "";
        Session["SESSION_FULLNAME"] = "";
        Session["SESSION_USERTYPE"] = "";
        Session["UserId"] = "";
        Session["VendorId"] = "";
        Session["UserIdDetails"] = "";
    }


    private void UpdateUserLoginStatus(string vUserId, int vLoginStatus, string vSessionId)
    {
        SqlParameter[] sqlParams = new SqlParameter[3];
        sqlParams[0] = new SqlParameter("@UserId", SqlDbType.Int);
        sqlParams[0].Value = Int32.Parse(vUserId);
        sqlParams[1] = new SqlParameter("@SessionId", SqlDbType.NVarChar);
        sqlParams[1].Value = vSessionId;
        sqlParams[2] = new SqlParameter("@LoginStatus", SqlDbType.Int);
        sqlParams[2].Value = vLoginStatus;

        //SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "sp_UpdateUserLoginStatus", sqlParams);
    }


    protected void btnLogin_Click(object sender, EventArgs e)
    {
        //string username = txtUserName.Text.Trim();
        //string password = EncryptionHelper.Encrypt(txtPassword.Text.Trim());
        //SqlDataReader oReader;

        //string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
        //string sCommand = "SELECT * FROM tblUsers WHERE UserName='" + username + "' AND UserPassword='" + password + "' ";
        //oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        //if (oReader.HasRows)
        //{
        //    oReader.Read();
        //    Session["SESSION_USERNAME"] = oReader["UserName"].ToString();
        //    Session["SESSION_PASSWORD"] = oReader["UserPassword"].ToString();
        //    Session["SESSION_FULLNAME"] = oReader["FirstName"].ToString() + " " + oReader["LastName"].ToString();
        //    txtNote.Text = "";
        //    //Session["SESSION_USERNAME"] = username;
        //    Response.Redirect("vendor_Home.aspx");
        //}
        //else
        //{
        //    Session["SESSION_USERNAME"] = "";
        //    Session["SESSION_PASSWORD"] = "";
        //    Session["SESSION_FULLNAME"] = "";
        //    txtNote.Text = "Invalid Username/Password";
        //}
        //oReader.Close();
    }
}
