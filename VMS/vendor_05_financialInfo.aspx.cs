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

public partial class vendor_05_financialInfo : System.Web.UI.Page
{
    SqlDataReader oReader;
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
    string query;
    SqlCommand cmd;
    SqlConnection conn;
    //int numRowsTbl;
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
            tbl01_Lbl.Visible = false;
        }
        else
        {
            PopulateFields_Lbl();
            tbl01.Visible = false;
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
        SqlDataReader oReader;
        string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
        string sCommand = "";


        query = "SELECT * FROM tblVendorInformation WHERE VendorId = @VendorId";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                conn.Open();oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        finanInfo_Type.SelectedValue = oReader["finanInfo_Type"].ToString();
                    }
                }
            }
        }

        sCommand = "SELECT * FROM tblVendorFinancialInformation WHERE VendorId= " + VendorId.ToString() + " AND Year = '1'";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            yr1Revenue.Value = oReader["Revenue"].ToString();
            yr1NetIncome.Value = oReader["NetIncome"].ToString();
            yr1TotalAssets.Value = oReader["TotalAssets"].ToString();
            yr1TotalLiabilities.Value = oReader["TotalLiabilities"].ToString();
            yr1CurrentAssets.Value = oReader["CurrentAssets"].ToString();
            yr1CurrentLiabilities.Value = oReader["CurrentLiabilities"].ToString();
            yr1FileNameLbl.Text = oReader["FileName"].ToString() != "" ? "<a href='" + oReader["FileName"].ToString() + "' target='_blank'> Attached file</a>" : "Attach file";
            yr1FileName.Value = oReader["FileName"].ToString();
        }
        oReader.Close();

        sCommand = "SELECT * FROM tblVendorFinancialInformation WHERE VendorId= " + VendorId.ToString() + " AND Year = '2'";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            yr2Revenue.Value = oReader["Revenue"].ToString();
            yr2NetIncome.Value = oReader["NetIncome"].ToString();
            yr2TotalAssets.Value = oReader["TotalAssets"].ToString();
            yr2TotalLiabilities.Value = oReader["TotalLiabilities"].ToString();
            yr2CurrentAssets.Value = oReader["CurrentAssets"].ToString();
            yr2CurrentLiabilities.Value = oReader["CurrentLiabilities"].ToString();
            yr2FileNameLbl.Text = oReader["FileName"].ToString() != "" ? "<a href='" + oReader["FileName"].ToString() + "' target='_blank'> Attached file</a>" : "Attach file";
            yr2FileName.Value = oReader["FileName"].ToString();
        }
        oReader.Close();

        sCommand = "SELECT * FROM tblVendorFinancialInformation WHERE VendorId= " + VendorId.ToString() + " AND Year = '3'";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            yr3.Value = oReader["YearInfo"].ToString();
            yr3Revenue.Value = oReader["Revenue"].ToString();
            yr3NetIncome.Value = oReader["NetIncome"].ToString();
            yr3TotalAssets.Value = oReader["TotalAssets"].ToString();
            yr3TotalLiabilities.Value = oReader["TotalLiabilities"].ToString();
            yr3CurrentAssets.Value = oReader["CurrentAssets"].ToString();
            yr3CurrentLiabilities.Value = oReader["CurrentLiabilities"].ToString();
            yr3FileNameLbl.Text = oReader["FileName"].ToString()!="" ? "<a href='" + oReader["FileName"].ToString() + "' target='_blank'> Attached file</a>" : "Attach file";
            yr3FileName.Value = oReader["FileName"].ToString();
        }
        oReader.Close();
    }


    void PopulateFields_Lbl()
    {
        SqlDataReader oReader;
        string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
        string sCommand = "";


        query = "SELECT * FROM tblVendorInformation WHERE VendorId = @VendorId";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                conn.Open(); oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        finanInfo_Type.SelectedValue = oReader["finanInfo_Type"].ToString();
                        finanInfo_Type.Enabled = false;
                    }
                }
            }
        }

        sCommand = "SELECT * FROM tblVendorFinancialInformation WHERE VendorId= " + VendorId.ToString() + " AND Year = '1'";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            yr1YearInfo_Lbl.Text = oReader["YearInfo"].ToString();
            yr1Revenue_Lbl.Text = oReader["Revenue"].ToString();
            yr1NetIncome_Lbl.Text = oReader["NetIncome"].ToString();
            yr1TotalAssets_Lbl.Text = oReader["TotalAssets"].ToString();
            yr1TotalLiabilities_Lbl.Text = oReader["TotalLiabilities"].ToString();
            yr1CurrentAssets_Lbl.Text = oReader["CurrentAssets"].ToString();
            yr1CurrentLiabilities_Lbl.Text = oReader["CurrentLiabilities"].ToString();
            //yr1FileName_Lbl.Text = oReader["FileName"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["FileName"].ToString() + "' target='_blank'> Attached file</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> No attached file";
            yr1FileNameLbl_Lbl.Text = oReader["FileName"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["FileName"].ToString() + "' target='_blank'> Attached file</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> No attached file";
        }
        oReader.Close();

        sCommand = "SELECT * FROM tblVendorFinancialInformation WHERE VendorId= " + VendorId.ToString() + " AND Year = '2'";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            yr2YearInfo_Lbl.Text = oReader["YearInfo"].ToString();
            yr2Revenue_Lbl.Text = oReader["Revenue"].ToString();
            yr2NetIncome_Lbl.Text = oReader["NetIncome"].ToString();
            yr2TotalAssets_Lbl.Text = oReader["TotalAssets"].ToString();
            yr2TotalLiabilities_Lbl.Text = oReader["TotalLiabilities"].ToString();
            yr2CurrentAssets_Lbl.Text = oReader["CurrentAssets"].ToString();
            yr2CurrentLiabilities_Lbl.Text = oReader["CurrentLiabilities"].ToString();
            //yr2FileName_Lbl.Text = oReader["FileName"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["FileName"].ToString() + "' target='_blank'> Attached file</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> No attached file";
        }
        oReader.Close();

        sCommand = "SELECT * FROM tblVendorFinancialInformation WHERE VendorId= " + VendorId.ToString() + " AND Year = '3'";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            yr3YearInfo_Lbl.Text = oReader["YearInfo"].ToString();
            yr3Revenue_Lbl.Text = oReader["Revenue"].ToString();
            yr3NetIncome_Lbl.Text = oReader["NetIncome"].ToString();
            yr3TotalAssets_Lbl.Text = oReader["TotalAssets"].ToString();
            yr3TotalLiabilities_Lbl.Text = oReader["TotalLiabilities"].ToString();
            yr3CurrentAssets_Lbl.Text = oReader["CurrentAssets"].ToString();
            yr3CurrentLiabilities_Lbl.Text = oReader["CurrentLiabilities"].ToString();
            //yr3FileName_Lbl.Text = oReader["FileName"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["FileName"].ToString() + "' target='_blank'> Attached file</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> No attached file";
        }
        oReader.Close();
    }





    void SaveToDB()
    {

        //string status1 = Request.Form["status"].ToString();
        string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
        string sCommand = "";

        query = "UPDATE tblVendorInformation SET finanInfo_Type=@finanInfo_Type  WHERE VendorId = @VendorId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@finanInfo_Type", finanInfo_Type.SelectedValue.Trim().ToString());
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }


        //CLEAR tblVendorFinancialInformation FROM USER AND Year='1'"
        sCommand = "DELETE FROM tblVendorFinancialInformation WHERE VendorId = " + VendorId.ToString() + "AND Year='1'";
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        query = "INSERT INTO tblVendorFinancialInformation (VendorId, Year, YearInfo, Revenue, NetIncome, CurrentAssets, TotalAssets,  CurrentLiabilities, TotalLiabilities, FileName, DateCreated) VALUES (@VendorId, @Year, @YearInfo, @Revenue, @NetIncome, @CurrentAssets, @TotalAssets,  @CurrentLiabilities, @TotalLiabilities, @FileName, @DateCreated)";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@Year", "1");
                cmd.Parameters.AddWithValue("@YearInfo", yr3.Value != "" ? (Convert.ToInt32(yr3.Value.Trim().ToString()) - 2).ToString() : "");
                cmd.Parameters.AddWithValue("@Revenue", yr1Revenue.Value.Trim().ToString() != "" ? Convert.ToInt32(yr1Revenue.Value.Trim().ToString().Replace(",", "")) : 0);
                cmd.Parameters.AddWithValue("@NetIncome", yr1NetIncome.Value.Trim().ToString() != "" ? Convert.ToInt32(yr1NetIncome.Value.Trim().ToString().Replace(",", "")) : 0);
                cmd.Parameters.AddWithValue("@CurrentAssets", yr1CurrentAssets.Value.Trim().ToString() != "" ? Convert.ToInt32(yr1CurrentAssets.Value.Trim().ToString().Replace(",", "")) : 0);
                cmd.Parameters.AddWithValue("@TotalAssets", yr1TotalAssets.Value.Trim().ToString() != "" ? Convert.ToInt32(yr1TotalAssets.Value.Trim().ToString().Replace(",", "")) : 0);
                cmd.Parameters.AddWithValue("@CurrentLiabilities", yr1CurrentLiabilities.Value.Trim().ToString() != "" ? Convert.ToInt32(yr1CurrentLiabilities.Value.Trim().ToString().Replace(",", "")) : 0);
                cmd.Parameters.AddWithValue("@TotalLiabilities", yr1TotalLiabilities.Value.Trim().ToString() != "" ? Convert.ToInt32(yr1TotalLiabilities.Value.Trim().ToString().Replace(",", "")) : 0);
                cmd.Parameters.AddWithValue("@FileName", yr1FileName.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }

        //CLEAR tblVendorFinancialInformation FROM USER AND Year='1'"
        sCommand = "DELETE FROM tblVendorFinancialInformation WHERE VendorId = " + VendorId.ToString() + "AND Year='2'";
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        query = "INSERT INTO tblVendorFinancialInformation (VendorId, Year, YearInfo, Revenue, NetIncome, CurrentAssets, TotalAssets,  CurrentLiabilities, TotalLiabilities, FileName, DateCreated) VALUES (@VendorId, @Year, @YearInfo, @Revenue, @NetIncome, @CurrentAssets, @TotalAssets,  @CurrentLiabilities, @TotalLiabilities, @FileName, @DateCreated)";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@Year", "2");
                cmd.Parameters.AddWithValue("@YearInfo", yr3.Value != "" ? (Convert.ToInt32(yr3.Value.Trim().ToString()) - 1).ToString() : "");
                cmd.Parameters.AddWithValue("@Revenue", yr2Revenue.Value.Trim().ToString() != "" ? Convert.ToInt32(yr2Revenue.Value.Trim().ToString().Replace(",", "")) : 0);
                cmd.Parameters.AddWithValue("@NetIncome", yr2NetIncome.Value.Trim().ToString() != "" ? Convert.ToInt32(yr2NetIncome.Value.Trim().ToString().Replace(",", "")) : 0);
                cmd.Parameters.AddWithValue("@CurrentAssets", yr2CurrentAssets.Value.Trim().ToString() != "" ? Convert.ToInt32(yr2CurrentAssets.Value.Trim().ToString().Replace(",", "")) : 0);
                cmd.Parameters.AddWithValue("@TotalAssets", yr2TotalAssets.Value.Trim().ToString() != "" ? Convert.ToInt32(yr2TotalAssets.Value.Trim().ToString().Replace(",", "")) : 0);
                cmd.Parameters.AddWithValue("@CurrentLiabilities", yr2CurrentLiabilities.Value.Trim().ToString() != "" ? Convert.ToInt32(yr2CurrentLiabilities.Value.Trim().ToString().Replace(",", "")) : 0);
                cmd.Parameters.AddWithValue("@TotalLiabilities", yr2TotalLiabilities.Value.Trim().ToString() != "" ? Convert.ToInt32(yr2TotalLiabilities.Value.Trim().ToString().Replace(",", "")) : 0);
                cmd.Parameters.AddWithValue("@FileName", yr2FileName.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }



        //CLEAR tblVendorFinancialInformation FROM USER AND Year='3'"
        sCommand = "DELETE FROM tblVendorFinancialInformation WHERE VendorId = " + VendorId.ToString() + "AND Year='3'";
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        query = "INSERT INTO tblVendorFinancialInformation (VendorId, Year, YearInfo, Revenue, NetIncome, CurrentAssets, TotalAssets,  CurrentLiabilities, TotalLiabilities, FileName, DateCreated) VALUES (@VendorId, @Year, @YearInfo, @Revenue, @NetIncome, @CurrentAssets, @TotalAssets,  @CurrentLiabilities, @TotalLiabilities, @FileName, @DateCreated)";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@Year", "3");
                cmd.Parameters.AddWithValue("@YearInfo", yr3.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@Revenue", yr3Revenue.Value.Trim().ToString() != "" ? Convert.ToInt32(yr3Revenue.Value.Trim().ToString().Replace(",", "")) : 0);
                cmd.Parameters.AddWithValue("@NetIncome", yr3NetIncome.Value.Trim().ToString() != "" ? Convert.ToInt32(yr3NetIncome.Value.Trim().ToString().Replace(",", "")) : 0);
                cmd.Parameters.AddWithValue("@CurrentAssets", yr3CurrentAssets.Value.Trim().ToString() != "" ? Convert.ToInt32(yr3CurrentAssets.Value.Trim().ToString().Replace(",", "")) : 0);
                cmd.Parameters.AddWithValue("@TotalAssets", yr3TotalAssets.Value.Trim().ToString() != "" ? Convert.ToInt32(yr3TotalAssets.Value.Trim().ToString().Replace(",", "")) : 0);
                cmd.Parameters.AddWithValue("@CurrentLiabilities", yr3CurrentLiabilities.Value.Trim().ToString() != "" ? Convert.ToInt32(yr3CurrentLiabilities.Value.Trim().ToString().Replace(",", "")) : 0);
                cmd.Parameters.AddWithValue("@TotalLiabilities", yr3TotalLiabilities.Value.Trim().ToString() != "" ? Convert.ToInt32(yr3TotalLiabilities.Value.Trim().ToString().Replace(",", "")) : 0);
                cmd.Parameters.AddWithValue("@FileName", yr3FileName.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }


        
        string control1 = Request.Form["__EVENTTARGET"];
        if (control1 == "continueStp")
        {
            Response.Redirect("vendor_07_Conflict.aspx");
        }
    }

    //#####################################################3

    protected void rptGeneral_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        e.Command.Parameters["@VendorId"].Value = VendorId;
    }
}