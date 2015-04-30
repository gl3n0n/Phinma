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

public partial class login : System.Web.UI.Page
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
        //Response.Write(EncryptionHelper.Decrypt("IsA6nrcPsVVxFZwXqJYUrA=="));
       //TestShowAllSessions(); 
        Session["UserId"] = "";
        Session["VendorId"] = "";
        Session["SESSION_USERNAME"] = "";
        Session["SESSION_PASSWORD"] = "";
        Session["SESSION_USERTYPE"] = "";
        Session["SESSION_FULLNAME"] = "";
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
        string username = txtUserName.Text.Trim();
        string password = EncryptionHelper.Encrypt(txtPassword.Text.Trim());
        SqlDataReader oReader;
        
        string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
        string sCommand = "SELECT t1.*, t2.UserType FROM tblUsers t1, tblUserTypes t2  WHERE UserName='" + username + "' AND t2.UserId = t1.UserId AND t1.Status != 0";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            Response.Write("<span style='display:none'>" + EncryptionHelper.Decrypt(oReader["UserPassword"].ToString()).ToString() + "</span>");
        }

        connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
        sCommand = "SELECT t1.*, t2.UserType FROM tblUsers t1, tblUserTypes t2  WHERE UserName='" + username + "' AND UserPassword='" + password + "' AND t2.UserId = t1.UserId AND t1.Status != 0";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);

        if(oReader.HasRows)
        {
            oReader.Read();
            Session["UserId"] = oReader["UserId"].ToString();
            Session["SESSION_USERNAME"] = oReader["UserName"].ToString();
            Session["SESSION_PASSWORD"] = oReader["UserPassword"].ToString();
            Session["SESSION_USERTYPE"] = oReader["UserType"].ToString();
            Session["SESSION_FULLNAME"] = System.Globalization.CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(oReader["FirstName"].ToString() + " " + oReader["LastName"].ToString());
            txtNote.Text = "";
            //Session["SESSION_USERNAME"] = username;
            if (Session["SESSION_USERTYPE"].ToString() == "9") { Response.Redirect("admin_Home.aspx"); }
            else if (Session["SESSION_USERTYPE"].ToString() == "10") { Response.Redirect("vmofficer_Home.aspx"); }
            else if (Session["SESSION_USERTYPE"].ToString() == "11") {
                sCommand = "SELECT * FROM tblUsersForVendors WHERE UserId = " + Session["UserId"].ToString();
                oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
                if (oReader.HasRows)
                {
                    oReader.Read();
                    Session["VendorId"] = oReader["VendorId"].ToString(); 
                    Response.Redirect("vendor_Home.aspx");
                }
            }
            else if (Session["SESSION_USERTYPE"].ToString() == "12") { Response.Redirect("dnb_Home.aspx"); }
            else if (Session["SESSION_USERTYPE"].ToString() == "13") { Response.Redirect("legal_Home.aspx"); }
            else if (Session["SESSION_USERTYPE"].ToString() == "14") { Response.Redirect("vmtech_Home.aspx"); }
            else if (Session["SESSION_USERTYPE"].ToString() == "15") { Response.Redirect("vmissue_Home.aspx"); }
            else if (Session["SESSION_USERTYPE"].ToString() == "16") { Response.Redirect("vmhead_Home.aspx"); }
            else if (Session["SESSION_USERTYPE"].ToString() == "17") { Response.Redirect("pvmd_Home.aspx"); }
            else if (Session["SESSION_USERTYPE"].ToString() == "18") { Response.Redirect("cfo_Home.aspx"); }
        }
        else 
        {
            txtNote.Text = "Invalid Username/Password";
        }
        oReader.Close();
    }
}
