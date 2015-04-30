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

public partial class vendor_Declarations_Business : System.Web.UI.Page
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
            tbl01_Lbl.Visible = tbl02_Lbl.Visible = tbl03_Lbl.Visible = tbl04_Lbl.Visible = tbl05_Lbl.Visible = tbl06_Lbl.Visible = tbl07_Lbl.Visible = tbl08_Lbl.Visible = tbl09_Lbl.Visible = false;
        }
        else
        {
            PopulateFields_Lbl();
            tbl01.Visible = tbl02.Visible = tbl03.Visible = tbl04.Visible = tbl05.Visible = tbl06.Visible = tbl07.Visible = tbl08.Visible = tbl09.Visible  = false;
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
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        CompanyName.Value = oReader["CompanyName"].ToString();
                    }
                }
            }
        }

        query = "SELECT * FROM tblVendorSupplierDeclarationOnBusiness WHERE VendorId=  @VendorId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        DateOfEvaluation.Value = oReader["DateOfEvaluation"].ToString();
                        A_Rating_Q1.Value = oReader["A_Rating_Q1"].ToString();
                        A_Remarks_Q1.Value = oReader["A_Remarks_Q1"].ToString();
                        A_Rating_Q2.Value = oReader["A_Rating_Q2"].ToString();
                        A_Remarks_Q2.Value = oReader["A_Remarks_Q2"].ToString();
                        A_Rating_Q3.Value = oReader["A_Rating_Q3"].ToString();
                        A_Remarks_Q3.Value = oReader["A_Remarks_Q3"].ToString();
                        A_Rating_Q4.Value = oReader["A_Rating_Q4"].ToString();
                        A_Remarks_Q4.Value = oReader["A_Remarks_Q4"].ToString();
                        A_Rating_Q5.Value = oReader["A_Rating_Q5"].ToString();
                        A_Remarks_Q5.Value = oReader["A_Remarks_Q5"].ToString();
                        A_Rating_Q6.Value = oReader["A_Rating_Q6"].ToString();
                        A_Remarks_Q6.Value = oReader["A_Remarks_Q6"].ToString();
                        A_Rating_Q7.Value = oReader["A_Rating_Q7"].ToString();
                        A_Remarks_Q7.Value = oReader["A_Remarks_Q7"].ToString();
                        A_Rating_Q8.Value = oReader["A_Rating_Q8"].ToString();
                        A_Remarks_Q8.Value = oReader["A_Remarks_Q8"].ToString();
                        B_Rating_Q1.Value = oReader["B_Rating_Q1"].ToString();
                        B_Remarks_Q1.Value = oReader["B_Remarks_Q1"].ToString();
                        B_Rating_Q2.Value = oReader["B_Rating_Q2"].ToString();
                        B_Remarks_Q2.Value = oReader["B_Remarks_Q2"].ToString();
                        B_Rating_Q3.Value = oReader["B_Rating_Q3"].ToString();
                        B_Remarks_Q3.Value = oReader["B_Remarks_Q3"].ToString();
                        B_Rating_Q4.Value = oReader["B_Rating_Q4"].ToString();
                        B_Remarks_Q4.Value = oReader["B_Remarks_Q4"].ToString();
                        B_Rating_Q5.Value = oReader["B_Rating_Q5"].ToString();
                        B_Remarks_Q5.Value = oReader["B_Remarks_Q5"].ToString();
                        B_Rating_Q6.Value = oReader["B_Rating_Q6"].ToString();
                        B_Remarks_Q6.Value = oReader["B_Remarks_Q6"].ToString();
                        B_Rating_Q7.Value = oReader["B_Rating_Q7"].ToString();
                        B_Remarks_Q7.Value = oReader["B_Remarks_Q7"].ToString();
                        B_Rating_Q8.Value = oReader["B_Rating_Q8"].ToString();
                        B_Remarks_Q8.Value = oReader["B_Remarks_Q8"].ToString();
                        B_Rating_Q9.Value = oReader["B_Rating_Q9"].ToString();
                        B_Remarks_Q9.Value = oReader["B_Remarks_Q9"].ToString();
                        C_Rating_Q1.Value = oReader["C_Rating_Q1"].ToString();
                        C_Rating_Q2.Value = oReader["C_Rating_Q2"].ToString();
                        C_Rating_Q3.Value = oReader["C_Rating_Q3"].ToString();
                        C_Remarks_Q1.Value = oReader["C_Remarks_Q1"].ToString();
                        C_Remarks_Q2.Value = oReader["C_Remarks_Q2"].ToString();
                        C_Remarks_Q3.Value = oReader["C_Remarks_Q3"].ToString();
                        D_Rating_Q1.Value = oReader["D_Rating_Q1"].ToString();
                        D_Rating_Q2.Value = oReader["D_Rating_Q2"].ToString();
                        D_Rating_Q3.Value = oReader["D_Rating_Q3"].ToString();
                        D_Rating_Q4.Value = oReader["D_Rating_Q4"].ToString();
                        D_Remarks_Q1.Value = oReader["D_Remarks_Q1"].ToString();
                        D_Remarks_Q2.Value = oReader["D_Remarks_Q2"].ToString();
                        D_Remarks_Q3.Value = oReader["D_Remarks_Q3"].ToString();
                        D_Remarks_Q4.Value = oReader["D_Remarks_Q4"].ToString();
                        E_Rating_Q1.Value = oReader["E_Rating_Q1"].ToString();
                        E_Remarks_Q1.Value = oReader["E_Remarks_Q1"].ToString();
                        E_Rating_Q2.Value = oReader["E_Rating_Q2"].ToString();
                        E_Remarks_Q2.Value = oReader["E_Remarks_Q2"].ToString();
                        F_Rating_Q1.Value = oReader["F_Rating_Q1"].ToString();
                        F_Remarks_Q1.Value = oReader["F_Remarks_Q1"].ToString();
                        F_Rating_Q2.Value = oReader["F_Rating_Q2"].ToString();
                        F_Remarks_Q2.Value = oReader["F_Remarks_Q2"].ToString();
                        G_Rating_Q1.Value = oReader["G_Rating_Q1"].ToString();
                        G_Remarks_Q1.Value = oReader["G_Remarks_Q1"].ToString();
                        G_Rating_Q2.Value = oReader["G_Rating_Q2"].ToString();
                        G_Remarks_Q2.Value = oReader["G_Remarks_Q2"].ToString();
                        G_Rating_Q3.Value = oReader["G_Rating_Q3"].ToString();
                        G_Remarks_Q3.Value = oReader["G_Remarks_Q3"].ToString();
                        ApprovedDate.Value = oReader["ApprovedDate"].ToString();
                        PrintedName.Value = oReader["PrintedName"].ToString();
                        Position.Value = oReader["Position"].ToString();
                    }
                }
            }
        }
    }

    void PopulateFields_Lbl()
    {
        query = "SELECT * FROM tblVendorInformation WHERE VendorId=  @VendorId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        CompanyName_Lbl.Text = oReader["CompanyName"].ToString();
                    }
                }
            }
        }

        query = "SELECT * FROM tblVendorSupplierDeclarationOnBusiness WHERE VendorId=  @VendorId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        DateOfEvaluation_Lbl.Text = oReader["DateOfEvaluation"].ToString();
                        A_Rating_Q1_Lbl.Text = oReader["A_Rating_Q1"].ToString();
                        A_Remarks_Q1_Lbl.Text = oReader["A_Remarks_Q1"].ToString();
                        A_Rating_Q2_Lbl.Text = oReader["A_Rating_Q2"].ToString();
                        A_Remarks_Q2_Lbl.Text = oReader["A_Remarks_Q2"].ToString();
                        A_Rating_Q3_Lbl.Text = oReader["A_Rating_Q3"].ToString();
                        A_Remarks_Q3_Lbl.Text = oReader["A_Remarks_Q3"].ToString();
                        A_Rating_Q4_Lbl.Text = oReader["A_Rating_Q4"].ToString();
                        A_Remarks_Q4_Lbl.Text = oReader["A_Remarks_Q4"].ToString();
                        A_Rating_Q5_Lbl.Text = oReader["A_Rating_Q5"].ToString();
                        A_Remarks_Q5_Lbl.Text = oReader["A_Remarks_Q5"].ToString();
                        A_Rating_Q6_Lbl.Text = oReader["A_Rating_Q6"].ToString();
                        A_Remarks_Q6_Lbl.Text = oReader["A_Remarks_Q6"].ToString();
                        A_Rating_Q7_Lbl.Text = oReader["A_Rating_Q7"].ToString();
                        A_Remarks_Q7_Lbl.Text = oReader["A_Remarks_Q7"].ToString();
                        A_Rating_Q8_Lbl.Text = oReader["A_Rating_Q8"].ToString();
                        A_Remarks_Q8_Lbl.Text = oReader["A_Remarks_Q8"].ToString();
                        B_Rating_Q1_Lbl.Text = oReader["B_Rating_Q1"].ToString();
                        B_Remarks_Q1_Lbl.Text = oReader["B_Remarks_Q1"].ToString();
                        B_Rating_Q2_Lbl.Text = oReader["B_Rating_Q2"].ToString();
                        B_Remarks_Q2_Lbl.Text = oReader["B_Remarks_Q2"].ToString();
                        B_Rating_Q3_Lbl.Text = oReader["B_Rating_Q3"].ToString();
                        B_Remarks_Q3_Lbl.Text = oReader["B_Remarks_Q3"].ToString();
                        B_Rating_Q4_Lbl.Text = oReader["B_Rating_Q4"].ToString();
                        B_Remarks_Q4_Lbl.Text = oReader["B_Remarks_Q4"].ToString();
                        B_Rating_Q5_Lbl.Text = oReader["B_Rating_Q5"].ToString();
                        B_Remarks_Q5_Lbl.Text = oReader["B_Remarks_Q5"].ToString();
                        B_Rating_Q6_Lbl.Text = oReader["B_Rating_Q6"].ToString();
                        B_Remarks_Q6_Lbl.Text = oReader["B_Remarks_Q6"].ToString();
                        B_Rating_Q7_Lbl.Text = oReader["B_Rating_Q7"].ToString();
                        B_Remarks_Q7_Lbl.Text = oReader["B_Remarks_Q7"].ToString();
                        B_Rating_Q8_Lbl.Text = oReader["B_Rating_Q8"].ToString();
                        B_Remarks_Q8_Lbl.Text = oReader["B_Remarks_Q8"].ToString();
                        B_Rating_Q9_Lbl.Text = oReader["B_Rating_Q9"].ToString();
                        B_Remarks_Q9_Lbl.Text = oReader["B_Remarks_Q9"].ToString();
                        C_Rating_Q1_Lbl.Text = oReader["C_Rating_Q1"].ToString();
                        C_Rating_Q2_Lbl.Text = oReader["C_Rating_Q2"].ToString();
                        C_Rating_Q3_Lbl.Text = oReader["C_Rating_Q3"].ToString();
                        C_Remarks_Q1_Lbl.Text = oReader["C_Remarks_Q1"].ToString();
                        C_Remarks_Q2_Lbl.Text = oReader["C_Remarks_Q2"].ToString();
                        C_Remarks_Q3_Lbl.Text = oReader["C_Remarks_Q3"].ToString();
                        D_Rating_Q1_Lbl.Text = oReader["D_Rating_Q1"].ToString();
                        D_Rating_Q2_Lbl.Text = oReader["D_Rating_Q2"].ToString();
                        D_Rating_Q3_Lbl.Text = oReader["D_Rating_Q3"].ToString();
                        D_Rating_Q4_Lbl.Text = oReader["D_Rating_Q4"].ToString();
                        D_Remarks_Q1_Lbl.Text = oReader["D_Remarks_Q1"].ToString();
                        D_Remarks_Q2_Lbl.Text = oReader["D_Remarks_Q2"].ToString();
                        D_Remarks_Q3_Lbl.Text = oReader["D_Remarks_Q3"].ToString();
                        D_Remarks_Q4_Lbl.Text = oReader["D_Remarks_Q4"].ToString();
                        E_Rating_Q1_Lbl.Text = oReader["E_Rating_Q1"].ToString();
                        E_Remarks_Q1_Lbl.Text = oReader["E_Remarks_Q1"].ToString();
                        E_Rating_Q2_Lbl.Text = oReader["E_Rating_Q2"].ToString();
                        E_Remarks_Q2_Lbl.Text = oReader["E_Remarks_Q2"].ToString();
                        F_Rating_Q1_Lbl.Text = oReader["F_Rating_Q1"].ToString();
                        F_Remarks_Q1_Lbl.Text = oReader["F_Remarks_Q1"].ToString();
                        F_Rating_Q2_Lbl.Text = oReader["F_Rating_Q2"].ToString();
                        F_Remarks_Q2_Lbl.Text = oReader["F_Remarks_Q2"].ToString();
                        G_Rating_Q1_Lbl.Text = oReader["G_Rating_Q1"].ToString();
                        G_Remarks_Q1_Lbl.Text = oReader["G_Remarks_Q1"].ToString();
                        G_Rating_Q2_Lbl.Text = oReader["G_Rating_Q2"].ToString();
                        G_Remarks_Q2_Lbl.Text = oReader["G_Remarks_Q2"].ToString();
                        G_Rating_Q3_Lbl.Text = oReader["G_Rating_Q3"].ToString();
                        G_Remarks_Q3_Lbl.Text = oReader["G_Remarks_Q3"].ToString();
                        ApprovedDate_Lbl.Text = oReader["ApprovedDate"].ToString();
                        PrintedName_Lbl.Text = oReader["PrintedName"].ToString();
                        Position_Lbl.Text = oReader["Position"].ToString();
                    }
                }
            }
        }
    }
    void SaveToDB()
    {
        string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
        string sCommand = "";

        sCommand = "DELETE FROM tblVendorSupplierDeclarationOnBusiness WHERE VendorId = " + VendorId.ToString();
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        query = "INSERT INTO tblVendorSupplierDeclarationOnBusiness (VendorId, DateOfEvaluation, A_Rating_Q1, A_Remarks_Q1, A_Rating_Q2, A_Remarks_Q2, A_Rating_Q3, A_Remarks_Q3, A_Rating_Q4, A_Remarks_Q4, A_Rating_Q5, A_Remarks_Q5, A_Rating_Q6, A_Remarks_Q6, A_Rating_Q7, A_Remarks_Q7, A_Rating_Q8, A_Remarks_Q8, B_Rating_Q1, B_Remarks_Q1, B_Rating_Q2, B_Remarks_Q2, B_Rating_Q3, B_Remarks_Q3, B_Rating_Q4, B_Remarks_Q4, B_Rating_Q5, B_Remarks_Q5, B_Rating_Q6, B_Remarks_Q6, B_Rating_Q7, B_Remarks_Q7, B_Rating_Q8, B_Remarks_Q8, B_Rating_Q9, B_Remarks_Q9, C_Rating_Q1, C_Rating_Q2, C_Rating_Q3, C_Remarks_Q1, C_Remarks_Q2, C_Remarks_Q3, D_Rating_Q1, D_Rating_Q2, D_Rating_Q3, D_Rating_Q4, D_Remarks_Q1, D_Remarks_Q2, D_Remarks_Q3, D_Remarks_Q4, E_Rating_Q1, E_Remarks_Q1, E_Rating_Q2, E_Remarks_Q2, F_Rating_Q1, F_Remarks_Q1, F_Rating_Q2, F_Remarks_Q2, G_Rating_Q1, G_Remarks_Q1, G_Rating_Q2, G_Remarks_Q2, G_Rating_Q3, G_Remarks_Q3, ApprovedDate, PrintedName, Position) VALUES (@VendorId, @DateOfEvaluation, @A_Rating_Q1, @A_Remarks_Q1, @A_Rating_Q2, @A_Remarks_Q2, @A_Rating_Q3, @A_Remarks_Q3, @A_Rating_Q4, @A_Remarks_Q4, @A_Rating_Q5, @A_Remarks_Q5, @A_Rating_Q6, @A_Remarks_Q6, @A_Rating_Q7, @A_Remarks_Q7, @A_Rating_Q8, @A_Remarks_Q8, @B_Rating_Q1, @B_Remarks_Q1, @B_Rating_Q2, @B_Remarks_Q2, @B_Rating_Q3, @B_Remarks_Q3, @B_Rating_Q4, @B_Remarks_Q4, @B_Rating_Q5, @B_Remarks_Q5, @B_Rating_Q6, @B_Remarks_Q6, @B_Rating_Q7, @B_Remarks_Q7, @B_Rating_Q8, @B_Remarks_Q8, @B_Rating_Q9, @B_Remarks_Q9, @C_Rating_Q1, @C_Rating_Q2, @C_Rating_Q3, @C_Remarks_Q1, @C_Remarks_Q2, @C_Remarks_Q3, @D_Rating_Q1, @D_Rating_Q2, @D_Rating_Q3, @D_Rating_Q4, @D_Remarks_Q1, @D_Remarks_Q2, @D_Remarks_Q3, @D_Remarks_Q4, @E_Rating_Q1, @E_Remarks_Q1, @E_Rating_Q2, @E_Remarks_Q2, @F_Rating_Q1, @F_Remarks_Q1, @F_Rating_Q2, @F_Remarks_Q2, @G_Rating_Q1, @G_Remarks_Q1, @G_Rating_Q2, @G_Remarks_Q2, @G_Rating_Q3, @G_Remarks_Q3, @ApprovedDate, @PrintedName, @Position)";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@DateOfEvaluation", DateOfEvaluation.Value);
                cmd.Parameters.AddWithValue("@A_Rating_Q1", A_Rating_Q1.Value!=""? Convert.ToInt32(A_Rating_Q1.Value) : 0);
                cmd.Parameters.AddWithValue("@A_Remarks_Q1", A_Remarks_Q1.Value);
                cmd.Parameters.AddWithValue("@A_Rating_Q2", A_Rating_Q2.Value!=""? Convert.ToInt32(A_Rating_Q2.Value) : 0);
                cmd.Parameters.AddWithValue("@A_Remarks_Q2", A_Remarks_Q2.Value);
                cmd.Parameters.AddWithValue("@A_Rating_Q3", A_Rating_Q3.Value!=""? Convert.ToInt32(A_Rating_Q3.Value) : 0);
                cmd.Parameters.AddWithValue("@A_Remarks_Q3", A_Remarks_Q3.Value);
                cmd.Parameters.AddWithValue("@A_Rating_Q4", A_Rating_Q4.Value!=""? Convert.ToInt32(A_Rating_Q4.Value) : 0);
                cmd.Parameters.AddWithValue("@A_Remarks_Q4", A_Remarks_Q4.Value);
                cmd.Parameters.AddWithValue("@A_Rating_Q5", A_Rating_Q5.Value!=""? Convert.ToInt32(A_Rating_Q5.Value) : 0);
                cmd.Parameters.AddWithValue("@A_Remarks_Q5", A_Remarks_Q5.Value);
                cmd.Parameters.AddWithValue("@A_Rating_Q6", A_Rating_Q6.Value!=""? Convert.ToInt32(A_Rating_Q6.Value) : 0);
                cmd.Parameters.AddWithValue("@A_Remarks_Q6", A_Remarks_Q6.Value);
                cmd.Parameters.AddWithValue("@A_Rating_Q7", A_Rating_Q7.Value!=""? Convert.ToInt32(A_Rating_Q7.Value) : 0);
                cmd.Parameters.AddWithValue("@A_Remarks_Q7", A_Remarks_Q7.Value);
                cmd.Parameters.AddWithValue("@A_Rating_Q8", A_Rating_Q8.Value!=""? Convert.ToInt32(A_Rating_Q8.Value) : 0);
                cmd.Parameters.AddWithValue("@A_Remarks_Q8", A_Remarks_Q8.Value);
                cmd.Parameters.AddWithValue("@B_Rating_Q1", B_Rating_Q1.Value!=""? Convert.ToInt32(B_Rating_Q1.Value) : 0);
                cmd.Parameters.AddWithValue("@B_Remarks_Q1", B_Remarks_Q1.Value);
                cmd.Parameters.AddWithValue("@B_Rating_Q2", B_Rating_Q2.Value!=""? Convert.ToInt32(B_Rating_Q2.Value) : 0);
                cmd.Parameters.AddWithValue("@B_Remarks_Q2", B_Remarks_Q2.Value);
                cmd.Parameters.AddWithValue("@B_Rating_Q3", B_Rating_Q3.Value!=""? Convert.ToInt32(B_Rating_Q3.Value) : 0);
                cmd.Parameters.AddWithValue("@B_Remarks_Q3", B_Remarks_Q3.Value);
                cmd.Parameters.AddWithValue("@B_Rating_Q4", B_Rating_Q4.Value!=""? Convert.ToInt32(B_Rating_Q4.Value) : 0);
                cmd.Parameters.AddWithValue("@B_Remarks_Q4", B_Remarks_Q4.Value);
                cmd.Parameters.AddWithValue("@B_Rating_Q5", B_Rating_Q5.Value!=""? Convert.ToInt32(B_Rating_Q5.Value) : 0);
                cmd.Parameters.AddWithValue("@B_Remarks_Q5", B_Remarks_Q5.Value);
                cmd.Parameters.AddWithValue("@B_Rating_Q6", B_Rating_Q6.Value!=""? Convert.ToInt32(B_Rating_Q6.Value) : 0);
                cmd.Parameters.AddWithValue("@B_Remarks_Q6", B_Remarks_Q6.Value);
                cmd.Parameters.AddWithValue("@B_Rating_Q7", B_Rating_Q7.Value!=""? Convert.ToInt32(B_Rating_Q7.Value) : 0);
                cmd.Parameters.AddWithValue("@B_Remarks_Q7", B_Remarks_Q7.Value);
                cmd.Parameters.AddWithValue("@B_Rating_Q8", B_Rating_Q8.Value!=""? Convert.ToInt32(B_Rating_Q8.Value) : 0);
                cmd.Parameters.AddWithValue("@B_Remarks_Q8", B_Remarks_Q8.Value);
                cmd.Parameters.AddWithValue("@B_Rating_Q9", B_Rating_Q9.Value!=""? Convert.ToInt32(B_Rating_Q9.Value) : 0);
                cmd.Parameters.AddWithValue("@B_Remarks_Q9", B_Remarks_Q9.Value);
                cmd.Parameters.AddWithValue("@C_Rating_Q1", C_Rating_Q1.Value!=""? Convert.ToInt32(C_Rating_Q1.Value) : 0);
                cmd.Parameters.AddWithValue("@C_Rating_Q2", C_Rating_Q2.Value!=""? Convert.ToInt32(C_Rating_Q2.Value) : 0);
                cmd.Parameters.AddWithValue("@C_Rating_Q3", C_Rating_Q3.Value!=""? Convert.ToInt32(C_Rating_Q3.Value) : 0);
                cmd.Parameters.AddWithValue("@C_Remarks_Q1", C_Remarks_Q1.Value);
                cmd.Parameters.AddWithValue("@C_Remarks_Q2", C_Remarks_Q2.Value);
                cmd.Parameters.AddWithValue("@C_Remarks_Q3", C_Remarks_Q3.Value);
                cmd.Parameters.AddWithValue("@D_Rating_Q1", D_Rating_Q1.Value!=""? Convert.ToInt32(D_Rating_Q1.Value) : 0);
                cmd.Parameters.AddWithValue("@D_Rating_Q2", D_Rating_Q2.Value!=""? Convert.ToInt32(D_Rating_Q2.Value) : 0);
                cmd.Parameters.AddWithValue("@D_Rating_Q3", D_Rating_Q3.Value!=""? Convert.ToInt32(D_Rating_Q3.Value) : 0);
                cmd.Parameters.AddWithValue("@D_Rating_Q4", D_Rating_Q4.Value!=""? Convert.ToInt32(D_Rating_Q4.Value) : 0);
                cmd.Parameters.AddWithValue("@D_Remarks_Q1", D_Remarks_Q1.Value);
                cmd.Parameters.AddWithValue("@D_Remarks_Q2", D_Remarks_Q2.Value);
                cmd.Parameters.AddWithValue("@D_Remarks_Q3", D_Remarks_Q3.Value);
                cmd.Parameters.AddWithValue("@D_Remarks_Q4", D_Remarks_Q4.Value);
                cmd.Parameters.AddWithValue("@E_Rating_Q1", E_Rating_Q1.Value!=""? Convert.ToInt32(E_Rating_Q1.Value) : 0);
                cmd.Parameters.AddWithValue("@E_Remarks_Q1", E_Remarks_Q1.Value);
                cmd.Parameters.AddWithValue("@E_Rating_Q2", E_Rating_Q2.Value!=""? Convert.ToInt32(E_Rating_Q2.Value) : 0);
                cmd.Parameters.AddWithValue("@E_Remarks_Q2", E_Remarks_Q2.Value);
                cmd.Parameters.AddWithValue("@F_Rating_Q1", F_Rating_Q1.Value!=""? Convert.ToInt32(F_Rating_Q1.Value) : 0);
                cmd.Parameters.AddWithValue("@F_Remarks_Q1", F_Remarks_Q1.Value);
                cmd.Parameters.AddWithValue("@F_Rating_Q2", F_Rating_Q2.Value!=""? Convert.ToInt32(F_Rating_Q2.Value) : 0);
                cmd.Parameters.AddWithValue("@F_Remarks_Q2", F_Remarks_Q2.Value);
                cmd.Parameters.AddWithValue("@G_Rating_Q1", G_Rating_Q1.Value!=""? Convert.ToInt32(G_Rating_Q1.Value) : 0);
                cmd.Parameters.AddWithValue("@G_Remarks_Q1", G_Remarks_Q1.Value);
                cmd.Parameters.AddWithValue("@G_Rating_Q2", G_Rating_Q2.Value!=""? Convert.ToInt32(G_Rating_Q2.Value) : 0);
                cmd.Parameters.AddWithValue("@G_Remarks_Q2", G_Remarks_Q2.Value);
                cmd.Parameters.AddWithValue("@G_Rating_Q3", G_Rating_Q3.Value!=""? Convert.ToInt32(G_Rating_Q3.Value) : 0);
                cmd.Parameters.AddWithValue("@G_Remarks_Q3", G_Remarks_Q3.Value);
                cmd.Parameters.AddWithValue("@ApprovedDate", ApprovedDate.Value);
                cmd.Parameters.AddWithValue("@PrintedName", PrintedName.Value);
                cmd.Parameters.AddWithValue("@Position", Position.Value);
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }
        


        
        
        
        
        string control1 = Request.Form["__EVENTTARGET"];
        if (control1 == "continueStp")
        {
            Response.Redirect("vendor_Home.aspx");
        }
    }
}