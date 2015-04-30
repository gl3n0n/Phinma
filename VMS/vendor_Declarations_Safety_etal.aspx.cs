using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Ava.lib;
using Ava.lib.constant;

public partial class vendor_Declarations_Safety_etal : System.Web.UI.Page
{
    SqlDataReader oReader;
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
    string query;
    SqlCommand cmd;
    SqlConnection conn;
    public int VendorId;
    public string queryString;

    protected void TestShowAllSessions()
    {  //test show all session
        string str = null;
        foreach (string key in HttpContext.Current.Session.Keys)
        { str += string.Format("<b>{0}</b>: {1};  ", key, HttpContext.Current.Session[key].ToString()); }
        Response.Write("<span style='font-size:12px'>" + str + "</span>");
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        queryString = (Request.QueryString["VendorId"] != null && Request.QueryString["VendorId"] != "") ? "?VendorId=" + Request.QueryString["VendorId"] : "";
        if (Request.QueryString["VendorId"] != null && Request.QueryString["VendorId"] != "")
        {
            VendorId = Convert.ToInt32(Request.QueryString["VendorId"]);
        }
        else if (Session["VendorId"] != null && Session["VendorId"].ToString() != "")
        {
            VendorId = Convert.ToInt32(Session["VendorId"].ToString());
        }


        //TestShowAllSessions();
        if (
            Session["UserId"] == null ||
            Session["UserId"].ToString() == "" ||
            VendorId.ToString() == ""
            )
        {
            Response.Redirect("login.aspx");
        }


        if (editable())
        {
            if (IsPostBack) SaveToDB();
            PopulateFields();
            tbl01_Lbl.Visible = tbl02_Lbl.Visible = tbl03_Lbl.Visible = false;
        }
        else
        {
            PopulateFields_Lbl();
            tbl01.Visible = tbl02.Visible = tbl03.Visible = false;
            createBt.Visible = createBt1.Visible = false;
        }

    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
    }

    private bool editable()
    {
        bool EditMode = false;
        query = "SELECT * FROM tblVendor WHERE VendorId =  @VendorId";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                conn.Open();
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        if (
                            (Session["SESSION_USERTYPE"].ToString() == "11" && oReader["Status"].ToString() == "0") ||
                            (Session["SESSION_USERTYPE"].ToString() == "11" && oReader["Status"].ToString() == "9")
                            )
                        {
                            EditMode = true;
                        }
                    }
                }
            }
        }

        return EditMode;
    }


    void PopulateFields()
    {
        query = "SELECT * FROM tblVendorInformation WHERE VendorId=  @VendorId";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                conn.Open();
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        CompanyName.Value = oReader["CompanyName"].ToString();
                    }
                } conn.Close();
            }
        }
        query = "SELECT * FROM tblVendorSupplierDeclarationOnSafety WHERE VendorId=  @VendorId";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                conn.Open();
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        DateOfEvaluation.Value = oReader["DateOfEvaluation"].ToString();
                        Q1.SelectedValue = oReader["Q1"].ToString();
                        Q2.SelectedValue = oReader["Q2"].ToString();
                        Q3.SelectedValue = oReader["Q3"].ToString();
                        Q4.SelectedValue = oReader["Q4"].ToString();
                        Q5.SelectedValue = oReader["Q5"].ToString();
                        Q6.SelectedValue = oReader["Q6"].ToString();
                        Q7.SelectedValue = oReader["Q7"].ToString();
                        Q8.SelectedValue = oReader["Q8"].ToString();
                        Q9.SelectedValue = oReader["Q9"].ToString();
                        Q10.SelectedValue = oReader["Q10"].ToString();
                        Q11.SelectedValue = oReader["Q11"].ToString();
                        Q12a.Value = oReader["Q12a"].ToString();
                        Q12b.Value = oReader["Q12b"].ToString();
                        Q12c.Value = oReader["Q12c"].ToString();
                        Q12d.Value = oReader["Q12d"].ToString();
                        ApprovedDate.Value = oReader["ApprovedDate"].ToString();
                        PrintedName.Value = oReader["PrintedName"].ToString();
                        Position.Value = oReader["Position"].ToString();
                    }
                } conn.Close();
            }
        }
    }



    void PopulateFields_Lbl()
    {
        query = "SELECT * FROM tblVendorInformation WHERE VendorId=  @VendorId";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                conn.Open();
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        CompanyName_Lbl.Text = oReader["CompanyName"].ToString();
                    }
                } conn.Close();
            }
        }
        query = "SELECT * FROM tblVendorSupplierDeclarationOnSafety WHERE VendorId=  @VendorId";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                conn.Open();
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        DateOfEvaluation_Lbl.Text = oReader["DateOfEvaluation"].ToString();
                        Q1_Lbl.Text = oReader["Q1"].ToString();
                        Q2_Lbl.Text = oReader["Q2"].ToString();
                        Q3_Lbl.Text = oReader["Q3"].ToString();
                        Q4_Lbl.Text = oReader["Q4"].ToString();
                        Q5_Lbl.Text = oReader["Q5"].ToString();
                        Q6_Lbl.Text = oReader["Q6"].ToString();
                        Q7_Lbl.Text = oReader["Q7"].ToString();
                        Q8_Lbl.Text = oReader["Q8"].ToString();
                        Q9_Lbl.Text = oReader["Q9"].ToString();
                        Q10_Lbl.Text = oReader["Q10"].ToString();
                        Q11_Lbl.Text = oReader["Q11"].ToString();
                        Q12a_Lbl.Text = oReader["Q12a"].ToString();
                        Q12b_Lbl.Text = oReader["Q12b"].ToString();
                        Q12c_Lbl.Text = oReader["Q12c"].ToString();
                        Q12d_Lbl.Text = oReader["Q12d"].ToString();
                        ApprovedDate_Lbl.Text = oReader["ApprovedDate"].ToString();
                        PrintedName_Lbl.Text = oReader["PrintedName"].ToString();
                        Position_Lbl.Text = oReader["Position"].ToString();
                    }
                } conn.Close();
            }
        }
    }



    void SaveToDB()
    {
        string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
        string sCommand = "";

        sCommand = "DELETE FROM tblVendorSupplierDeclarationOnSafety WHERE VendorId = " + VendorId.ToString() ;
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        query = "INSERT INTO tblVendorSupplierDeclarationOnSafety (VendorId, DateOfEvaluation, Q1, Q2, Q3, Q4, Q5, Q6, Q7, Q8, Q9, Q10, Q11, Q12a, Q12b, Q12c, Q12d, ApprovedDate, PrintedName, Position) VALUES (@VendorId, @DateOfEvaluation, @Q1, @Q2, @Q3, @Q4, @Q5, @Q6, @Q7, @Q8, @Q9, @Q10, @Q11, @Q12a, @Q12b, @Q12c, @Q12d, @ApprovedDate, @PrintedName, @Position)";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@DateOfEvaluation", DateOfEvaluation.Value);
                cmd.Parameters.AddWithValue("@Q1", Q1.SelectedValue);
                cmd.Parameters.AddWithValue("@Q2", Q2.SelectedValue);
                cmd.Parameters.AddWithValue("@Q3", Q3.SelectedValue);
                cmd.Parameters.AddWithValue("@Q4", Q4.SelectedValue);
                cmd.Parameters.AddWithValue("@Q5", Q5.SelectedValue);
                cmd.Parameters.AddWithValue("@Q6", Q6.SelectedValue);
                cmd.Parameters.AddWithValue("@Q7", Q7.SelectedValue);
                cmd.Parameters.AddWithValue("@Q8", Q8.SelectedValue);
                cmd.Parameters.AddWithValue("@Q9", Q9.SelectedValue);
                cmd.Parameters.AddWithValue("@Q10", Q10.SelectedValue);
                cmd.Parameters.AddWithValue("@Q11", Q11.SelectedValue);
                cmd.Parameters.AddWithValue("@Q12a", Q12a.Value);
                cmd.Parameters.AddWithValue("@Q12b", Q12b.Value);
                cmd.Parameters.AddWithValue("@Q12c", Q12c.Value);
                cmd.Parameters.AddWithValue("@Q12d", Q12d.Value);
                cmd.Parameters.AddWithValue("@ApprovedDate", ApprovedDate.Value);
                cmd.Parameters.AddWithValue("@PrintedName", PrintedName.Value);
                cmd.Parameters.AddWithValue("@Position", Position.Value);
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }
        conn.Close();


        
        
        
        
        string control1 = Request.Form["__EVENTTARGET"];
        if (control1 == "continueStp")
        {
            Response.Redirect("vendor_Declarations_Business.aspx");
        }
    }
}