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

public partial class vmofficer_VendorDetails_View : System.Web.UI.Page
{
    SqlDataReader oReader;
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
    string query;
    SqlCommand cmd;
    SqlConnection conn;
    //bool UserNameExists;
    //bool VendorCompanyNameExists;
    //string sCommand;

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

        if (IsPostBack)
        {
            SaveToDB();
        }
        else 
        {
            PopulateFields();
        }

    }


    void PreCreateUserAcct()
    {
            
    }


    void PopulateFields()
    {
        //SqlDataReader oReader;
        //string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
        //string sCommand = "SELECT * FROM tblUsers WHERE EmailAdd= '" + Session["VendorEmail"].ToString() + "' ";
        //oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        //if (oReader.HasRows)
        //{
        //    oReader.Read();
        //    UserName.Value = oReader["UserName"].ToString();
        //    UserPassword.Value = EncryptionHelper.Decrypt(oReader["UserPassword"].ToString());
        //    CompanyName.Value = oReader["CompanyName"].ToString();
        //    EmailAdd.Value = oReader["EmailAdd"].ToString();
        //    UserName.Value = oReader["UserName"].ToString();
        //    FirstName.Value = oReader["FirstName"].ToString();
        //    MiddleName.Value = oReader["MiddleName"].ToString();
        //    LastName.Value = oReader["LastName"].ToString();
        //    UserName.Disabled = true;
        //    UserPassword.Disabled = true;
        //    CompanyName.Disabled = true;
        //    EmailAdd.Disabled = true;
        //    UserName.Disabled = true;
        //    FirstName.Disabled = true;
        //    MiddleName.Disabled = true;
        //    LastName.Disabled = true;

        //} oReader.Close();
    }

    protected void DetailsView1_OnDataBound(object sender, EventArgs e)
    {
        if (DetailsView1.Rows.Count > 0)
        {
            string DateStarted1 = DetailsView1.Rows[1].Cells[1].Text.ToString() != "" ? DetailsView1.Rows[1].Cells[1].Text.ToString() : "";
            //DetailsView1.Rows[1].Cells[1].Text = DateStarted1 != "1/1/1900 12:00:00 AM"  || DateStarted1 != "" ? string.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(DateStarted1)) : "";

            string FinancialStatement1 = DetailsView1.Rows[2].Cells[1].Text;
            if (FinancialStatement1.ToString() == "Yes")
            {
                DetailsView1.Rows[2].Cells[1].Text = "Covers 12 months";
            }
            else
            {
                DetailsView1.Rows[2].Cells[1].Text = "Covers less than 12 months, or no FS available";
            }

            string coreBusiness1 = DetailsView1.Rows[5].Cells[1].Text + ";";
            DetailsView1.Rows[5].Cells[1].Text = "";
            string[] coreBusiness_arr = Regex.Split(coreBusiness1, ";");
            foreach (string coreBusiness in coreBusiness_arr)
            {
                if (coreBusiness != "")
                {
                    query = "IF EXISTS(select 1 from tblVendorProductsAndServices t1, tblVendor t2 where t1.VendorId = t2.VendorId and t2.Status = 6 and CategoryId = @CategoryId) BEGIN SELECT count(*) AS totalUsers, ta.CategoryId, tb.CategoryName FROM (select t1.VendorId, t1.CategoryId from tblVendorProductsAndServices t1, tblVendor t2 where t1.VendorId = t2.VendorId and t2.Status = 6 and CategoryId = @CategoryId) ta, rfcProductCategory tb WHERE tb.CategoryId=ta.CategoryId GROUP BY ta.CategoryId, tb.CategoryName END ELSE BEGIN SELECT 0 AS totalUsers, ta.CategoryId, tb.CategoryName FROM (select 0 as VendorId, @CategoryId as CategoryId from rfcProductCategory t1 where t1.CategoryId = @CategoryId) ta, rfcProductCategory tb WHERE tb.CategoryId=ta.CategoryId GROUP BY ta.CategoryId, tb.CategoryName END";
                    using (conn = new SqlConnection(connstring))
                    {
                        using (cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@CategoryId", coreBusiness.Trim());
                            conn.Open(); oReader = cmd.ExecuteReader();
                            if (oReader.HasRows)
                            {
                                while (oReader.Read())
                                {
                                    DetailsView1.Rows[5].Cells[1].Text = DetailsView1.Rows[5].Cells[1].Text + "[" + oReader["totalUsers"].ToString() + "] " + oReader["CategoryName"].ToString() + "<br>";
                                }
                            }
                            else
                            {
                                DetailsView1.Rows[5].Cells[1].Text = DetailsView1.Rows[5].Cells[1].Text + "[0] " + coreBusiness + "<br>";
                            }
                        }
                    }
                }
            }
        }
    }

    void SaveToDB() 
    {
        
    }
}