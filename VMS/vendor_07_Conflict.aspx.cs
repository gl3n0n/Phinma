using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Ava.lib;
using Ava.lib.constant;

public partial class vendor_07_Conflict : System.Web.UI.Page
{
    SqlDataReader oReader;
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
    string query;
    SqlCommand cmd;
    SqlConnection conn;
    int numRowsTbl;
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
            //repeaterConflictOfInterest1.DataBind(); repeaterConflictOfInterest1_Lbl.Visible = false;
            //repeaterConflictOfInterest2.DataBind(); repeaterConflictOfInterest2_Lbl.Visible = false;
            repeaterConflictOfInterest3.DataBind(); repeaterConflictOfInterest3_Lbl.Visible = false;
            //repeaterConflictOfInterest4.DataBind(); repeaterConflictOfInterest4_Lbl.Visible = false;
            YesNo1_Lbl.Visible = YesNo2_Lbl.Visible = YesNo3_Lbl.Visible  = false;
        }
        else
        {
            PopulateFields_Lbl();
            //repeaterConflictOfInterest1_Lbl.DataBind(); repeaterConflictOfInterest1.Visible = false;
            //repeaterConflictOfInterest2_Lbl.DataBind(); repeaterConflictOfInterest2.Visible = false;
            repeaterConflictOfInterest3_Lbl.DataBind(); repeaterConflictOfInterest3.Visible = false;
            //repeaterConflictOfInterest4_Lbl.DataBind(); repeaterConflictOfInterest4.Visible = false;
            YesNo1.Visible = YesNo2.Visible = YesNo3.Visible = false;
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
        query = dsVendorConflictOfInterest1.SelectCommand;
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
                        YesNo1.SelectedValue = oReader["YesNo"].ToString();
                    }
                }
            }
        }


        query = dsVendorConflictOfInterest2.SelectCommand;
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
                        YesNo2.SelectedValue = oReader["YesNo"].ToString();
                    }
                }
            }
        }


        query = dsVendorConflictOfInterest3.SelectCommand;
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
                        YesNo3.SelectedValue = oReader["YesNo"].ToString();
                    }
                }
            }
        }


        //query = dsVendorConflictOfInterest4.SelectCommand;
        //using (conn = new SqlConnection(connstring))
        //{
        //    using (cmd = new SqlCommand(query, conn))
        //    {
        //        cmd.Parameters.AddWithValue("@VendorId", VendorId);
        //        conn.Open();
        //        oReader = cmd.ExecuteReader();
        //        if (oReader.HasRows)
        //        {
        //            while (oReader.Read())
        //            {
        //                YesNo4.SelectedValue = oReader["YesNo"].ToString();
        //            }
        //        }
        //    }
        //}
    }



    void PopulateFields_Lbl()
    {
        query = dsVendorConflictOfInterest1.SelectCommand;
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
                        YesNo1_Lbl.Text = oReader["YesNo"].ToString() == "1" ? "Yes" : "No";
                    }
                }
            }
        }


        query = dsVendorConflictOfInterest2.SelectCommand;
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
                        YesNo2_Lbl.Text = oReader["YesNo"].ToString() == "1" ? "Yes" : "No";
                    }
                }
            }
        }


        query = dsVendorConflictOfInterest3.SelectCommand;
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
                        YesNo3_Lbl.Text = oReader["YesNo"].ToString() == "1" ? "Yes" : "No";
                    }
                }
            }
        }


        //query = dsVendorConflictOfInterest4.SelectCommand;
        //using (conn = new SqlConnection(connstring))
        //{
        //    using (cmd = new SqlCommand(query, conn))
        //    {
        //        cmd.Parameters.AddWithValue("@VendorId", VendorId);
        //        conn.Open();
        //        oReader = cmd.ExecuteReader();
        //        if (oReader.HasRows)
        //        {
        //            while (oReader.Read())
        //            {
        //                YesNo4_Lbl.Text = oReader["YesNo"].ToString() == "1" ? "Yes" : "No";
        //            }
        //        }
        //    }
        //}
    }


    void SaveToDB()
    {
        string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
        string sCommand = "";

        //CLEAR tblVendorSupplierReferences FROM USER
        sCommand = "DELETE FROM tblVendorConflictOfInterest WHERE VendorId = " + VendorId.ToString() + " AND Description = 'Q1'";
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        numRowsTbl = Convert.ToInt32(Request.Form["ConflictOfInterestCounter1"].ToString());
        for (int i = 1; i <= numRowsTbl; i++)
        {
            query = "INSERT INTO tblVendorConflictOfInterest (VendorId, Description, YesNo, NatureOfBusinessId, CompetitorName, NoYears, DateCreated) VALUES (@VendorId, @Description, @YesNo, @NatureOfBusinessId, @CompetitorName, @NoYears, @DateCreated)";
            //query = "sp_GetVendorInformation"; //##storedProcedure
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                    cmd.Parameters.AddWithValue("@VendorId", VendorId);
                    cmd.Parameters.AddWithValue("@Description", "Q1");
                    cmd.Parameters.AddWithValue("@YesNo", Convert.ToInt32(YesNo1.SelectedValue));
                    cmd.Parameters.AddWithValue("@NatureOfBusinessId", Convert.ToInt32(Request.Form["1NatureOfBusinessId" + i].ToString()));
                    cmd.Parameters.AddWithValue("@CompetitorName", Request.Form["1CompetitorName" + i].ToString().Replace(",", ""));
                    cmd.Parameters.AddWithValue("@NoYears", Request.Form["1NoYears" + i].ToString() != "" ? Convert.ToInt32(Request.Form["1NoYears" + i].ToString().Replace(",", "")) : 0);
                    cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                    conn.Open(); cmd.ExecuteNonQuery();
                }
            }
        }

        //CLEAR tblVendorSupplierReferences FROM USER
        sCommand = "DELETE FROM tblVendorConflictOfInterest WHERE VendorId = " + VendorId.ToString() + " AND Description = 'Q2'";
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        numRowsTbl = Convert.ToInt32(Request.Form["ConflictOfInterestCounter2"].ToString());
        for (int i = 1; i <= numRowsTbl; i++)
        {
            query = "INSERT INTO tblVendorConflictOfInterest (VendorId, Description, YesNo, NatureOfBusinessId, CompetitorName, Position, DateCreated) VALUES (@VendorId, @Description, @YesNo, @NatureOfBusinessId, @CompetitorName, @Position, @DateCreated)";
            //query = "sp_GetVendorInformation"; //##storedProcedure
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                    cmd.Parameters.AddWithValue("@VendorId", VendorId);
                    cmd.Parameters.AddWithValue("@Description", "Q2");
                    cmd.Parameters.AddWithValue("@YesNo", Convert.ToInt32(YesNo2.SelectedValue));
                    cmd.Parameters.AddWithValue("@NatureOfBusinessId", Convert.ToInt32(Request.Form["2NatureOfBusinessId" + i].ToString().Replace(",", "")));
                    cmd.Parameters.AddWithValue("@CompetitorName", Request.Form["2CompetitorName" + i].ToString());
                    cmd.Parameters.AddWithValue("@Position", Request.Form["Position" + i].ToString());
                    cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                    conn.Open(); cmd.ExecuteNonQuery();
                }
            }
        }


        //CLEAR tblVendorSupplierReferences FROM USER
        sCommand = "DELETE FROM tblVendorConflictOfInterest WHERE VendorId = " + VendorId.ToString() + " AND Description = 'Q3'";
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        numRowsTbl = Convert.ToInt32(Request.Form["ConflictOfInterestCounter3"].ToString());
        for (int i = 1; i <= numRowsTbl; i++)
        {
            query = "INSERT INTO tblVendorConflictOfInterest (VendorId, Description, YesNo, Position, GTEmployee, Relationship, DateCreated, NatureOfBusinessId, NoYears) VALUES (@VendorId, 'Q3', 1, @Position, @GTEmployee, @Relationship, @DateCreated, 0, 0)";
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@VendorId", VendorId);
                    //cmd.Parameters.AddWithValue("@Description", "Q3");
                    //cmd.Parameters.AddWithValue("@YesNo", 1);
                    //cmd.Parameters.AddWithValue("@CompetitorName", Request.Form["3CompetitorName" + i].ToString());
                    cmd.Parameters.AddWithValue("@Position", Request.Form["3Position" + i].ToString());
                    cmd.Parameters.AddWithValue("@GTEmployee", Request.Form["3GTEmployee" + i].ToString());
                    cmd.Parameters.AddWithValue("@Relationship", Request.Form["3Relationship" + i].ToString());
                    cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                    conn.Open(); cmd.ExecuteNonQuery();
                }
            }
        }


        //CLEAR tblVendorSupplierReferences FROM USER
        //sCommand = "DELETE FROM tblVendorConflictOfInterest WHERE VendorId = " + VendorId.ToString() + " AND Description = 'Q4'";
        //SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        //numRowsTbl = Convert.ToInt32(Request.Form["ConflictOfInterestCounter4"].ToString());
        //for (int i = 1; i <= numRowsTbl; i++)
        //{
        //    query = "INSERT INTO tblVendorConflictOfInterest (VendorId, Description, YesNo, NatureOfBusinessId, CompetitorName, NoYears, Position, DateCreated) VALUES (@VendorId, @Description, @YesNo, @NatureOfBusinessId, @CompetitorName, @NoYears, @Position, @DateCreated)";
        //    //query = "sp_GetVendorInformation"; //##storedProcedure
        //    using (conn = new SqlConnection(connstring))
        //    {
        //        using (cmd = new SqlCommand(query, conn))
        //        {
        //            //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
        //            cmd.Parameters.AddWithValue("@VendorId", VendorId);
        //            cmd.Parameters.AddWithValue("@Description", "Q4");
        //            cmd.Parameters.AddWithValue("@YesNo", Convert.ToInt32(YesNo4.SelectedValue));
        //            cmd.Parameters.AddWithValue("@NatureOfBusinessId", Convert.ToInt32(Request.Form["4NatureOfBusinessId" + i].ToString().Replace(",", "")));
        //            cmd.Parameters.AddWithValue("@CompetitorName", Request.Form["4CompetitorName" + i].ToString());
        //            cmd.Parameters.AddWithValue("@Position", Request.Form["4Position" + i].ToString());
        //            cmd.Parameters.AddWithValue("@NoYears", Request.Form["4NoYears" + i].ToString() != "" ? Convert.ToInt32(Request.Form["4NoYears" + i].ToString().Replace(",", "")) : 0);
        //            cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
        //            conn.Open(); cmd.ExecuteNonQuery();
        //        }
        //    }
        //}


        string control1 = Request.Form["__EVENTTARGET"];
        if (control1 == "continueStp")
        {
            Response.Redirect("vendor_08_Undertakings.aspx");
        }
        
    }


    //#####################################################3

    protected void rptGeneral_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        e.Command.Parameters["@VendorId"].Value = VendorId;
    }
}