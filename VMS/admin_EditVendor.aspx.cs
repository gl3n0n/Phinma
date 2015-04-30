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
using System.Text;
using System.Text.RegularExpressions;

public partial class admin_EditVendor : System.Web.UI.Page
{
    SqlDataReader oReader;
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
    string query;
    SqlCommand cmd;
    SqlConnection conn;
    bool UserNameExists;
    string sCommand;

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
        if (Session["SESSION_USERTYPE"].ToString() != "9") Response.Redirect("login.aspx");
        deleteBt1.Visible = false;

        if (Request.QueryString["UserId"] == "0") { Session["UserIdVendor"] = ""; }
        if (Session["UserIdVendor"] == null || Session["UserIdVendor"].ToString() == "")
        {
            Response.Redirect("admin_VendorUsersList.aspx");
        }
        if (IsPostBack)
        {
            string control1 = Request.Form["__EVENTTARGET"];
            if (Session["UserIdVendor"] == null || Session["UserIdVendor"].ToString() == "")
            {
                Response.Redirect("admin_VendorUsersList.aspx");
            }
            else
            {
                if (control1 == "saveUser")
                {
                    UpdateUser();
                }
                else if (control1 == "deleteUser")
                {
                    DeleteUser();
                }
            }
        }
        else
        {
            PopulateFields();
        } 
    }

    void PopulateFields()
    {
        if (Session["UserIdVendor"].ToString() != "" && Session["UserIdVendor"] != null)
        {
            Label1.Text = "Edit Vendor User";
            Label2.Text = "UPDATE";
            deleteBt1.Visible = true;
            string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
            //string sCommand = "";

            query = "SELECT t1.* FROM tblUsers t1 WHERE t1.UserId=@UserId ";
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@UserId", Session["UserIdVendor"]);
                    oReader = cmd.ExecuteReader();
                    if (oReader.HasRows)
                    {
                        while (oReader.Read())
                        {
                            UserName.Value = oReader["UserName"].ToString();
                            UserPassword.Value = EncryptionHelper.Decrypt(oReader["UserPassword"].ToString());
                            EmailAdd.Value = oReader["EmailAdd"].ToString();
                            //FirstName.Value = oReader["FirstName"].ToString();
                            //LastName.Value = oReader["LastName"].ToString();
                            //CompanyName.Value = oReader["CompanyName"].ToString();
                            CompanyNameTxt.Text = oReader["CompanyName"].ToString();
                            //UserType.SelectedValue = oReader["UserType"].ToString();
                            //CompanyName1.SelectedValue = oReader["VendorId"].ToString();
                        }
                    } conn.Close();
                }
            }
            UserType.DataBind();
            query = "SELECT t1.* FROM tblUserTypes t1 WHERE t1.UserId=@UserId ";
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@UserId", Session["UserIdVendor"]);
                    oReader = cmd.ExecuteReader();
                    if (oReader.HasRows)
                    {
                        while (oReader.Read())
                        {
                            //UserType.SelectedValue = oReader["UserType"].ToString();
                            foreach (ListItem listItem in UserType.Items)
                            {
                                if (listItem.Value == oReader["UserType"].ToString())
                                {
                                    listItem.Selected = true;
                                }
                            }
                        }
                    } conn.Close();
                }
            }
        }
    }

    //DELETE User
    void DeleteUser()
    {
        string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
        query = "UPDATE tblUsers SET Status = 0 WHERE UserId = " + Session["UserIdVendor"];
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                conn.Open(); cmd.ExecuteNonQuery(); conn.Close();
            }
        }
        conn.Close();
        //sCommand = "DELETE FROM tblUserTypes WHERE UserId = " + Session["UserIdVendor"];
        //SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        errNotification.Text = "User successfully deleted";
        errNotification.ForeColor = Color.Red;
        UserName.Value = "";
        UserPassword.Value = "";
        UserType.SelectedIndex = 0;
        EmailAdd.Value = "";
        //FirstName.Value = "";
        //LastName.Value = "";
        //CompanyName.Value = "";
        Session["UserIdVendor"] = "";
    }

    //UPDATE User
    void UpdateUser()
    {
        string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
        //string sCommand = "";
        query = "SELECT UserName FROM tblUsers WHERE (UserName = @UserName) AND UserId != " + Session["UserIdVendor"];
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@UserName", UserName.Value.Trim());
                //cmd.Parameters.AddWithValue("@EmailAdd", EmailAdd.Value.Trim());
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        if (oReader["UserName"].ToString() != "")
                        {
                            UserNameExists = true;
                        }
                        else
                        {
                            UserNameExists = false;
                        }
                    }
                } conn.Close();
            }
        }
        
        if (UserNameExists)
        {
            errNotification.Text = "User already exists";
            errNotification.ForeColor = Color.Red;
        }
        else if (
            UserName.Value == "" ||
            UserPassword.Value == "" ||
            EmailAdd.Value == "" ||
            //FirstName.Value == "" ||
            //LastName.Value == "" ||
            //CompanyName.Value == "" ||
            //!UserTypeIsChecked(Session["UserIdVendor"].ToString()) || 
            !IsEmail(EmailAdd.Value)
            )
        {
            errNotification.Text = "Required fields.";
            errNotification.ForeColor = Color.Red;
        }
        else
        {
            
            if (UserPassword.Value != "")
            {
                query = "UPDATE tblUsers SET UserName=@UserName, UserPassword=@UserPassword, EmailAdd=@EmailAdd, Status=@Status WHERE UserId =@UserId";
            }
            else
            {
                query = "UPDATE tblUsers SET UserName=@UserName, EmailAdd=@EmailAdd, Status=@Status WHERE UserId =@UserId";
            }
            //query = "sp_GetVendorInformation"; //##storedProcedure
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                    cmd.Parameters.AddWithValue("@UserName", UserName.Value.Trim());
                    cmd.Parameters.AddWithValue("@UserPassword", EncryptionHelper.Encrypt(UserPassword.Value.Trim()));
                    cmd.Parameters.AddWithValue("@EmailAdd", EmailAdd.Value.Trim());
                    //cmd.Parameters.AddWithValue("@FirstName", FirstName.Value.Trim());
                    //cmd.Parameters.AddWithValue("@LastName", LastName.Value.Trim());
                    //cmd.Parameters.AddWithValue("@CompanyName", CompanyName.Value.Trim());
                    cmd.Parameters.AddWithValue("@Status", 1);
                    cmd.Parameters.AddWithValue("@UserId", Session["UserIdVendor"].ToString());
                    conn.Open(); cmd.ExecuteNonQuery(); conn.Close();
                }
            }
            //Response.Write(CompanyName.Value.Trim());


            //sCommand = "DELETE FROM tblUserTypes WHERE UserId = " + Session["UserIdVendor"];
            //SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
            //foreach (ListItem listItem in UserType.Items)
            //{
            //    if (listItem.Selected)
            //    {
            //        sCommand = "INSERT INTO tblUserTypes (UserId, UserType) VALUES (" + Session["UserIdVendor"] + ", " + listItem.Value + ")";
            //        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
            //    }
            //}

            errNotification.Text = "User successfully updated";
            errNotification.ForeColor = Color.Blue;
            UserName.Value = "";
            UserPassword.Value = "";
            UserType.SelectedIndex = 0;
            EmailAdd.Value = "";
            //FirstName.Value = "";
            //LastName.Value = "";
            //CompanyName.Value = "";
            Session["UserIdVendor"] = "";
        }
    }



    //CREATE User
    void SaveToDB()
    {
        
    }



    //CHECK IS THERE'S A USERTYPE SELECTED
    private bool UserTypeIsChecked(string idx)
    {
        bool thereIsChecked = false;
        int countSelected = 0;
        foreach (ListItem listItem in UserType.Items)
        {
            if (listItem.Selected)
            {
                countSelected++;
            }
        }
        //Response.Write(countSelected);
        if (countSelected > 0)
        {
            thereIsChecked = true;
        }

        ////VENDORS
        //string sCommand;
        ////Response.Write(Session["UserDetails"]);
        //sCommand = "select 1 from tblUsersForVendors WHERE UserId=" + idx;
        //Response.Write(sCommand);
        //SqlDataReader oReader;
        //oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        //if (oReader.HasRows)
        //{
        //    oReader.Read();
        //    //thereIsChecked = true;
        //}
        //oReader.Close();

        return thereIsChecked;
    }

    // CHECK EMAIL
    public static bool IsEmail(string Email)
    {
        string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
        @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
        @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        Regex re = new Regex(strRegex);
        if (re.IsMatch(Email))
            return (true);
        else
            return (false);
    }

}