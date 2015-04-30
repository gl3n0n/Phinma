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

public partial class vendor_06_Others : System.Web.UI.Page
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
            repeaterVendorCourtCases.DataBind(); repeaterVendorCourtCases_Lbl.Visible = false;
            othersQltyMangmtSys_Lbl.Visible = false;
            othersQltyMangmtSys_File_Lbl.Visible = false;
            othersQltyMangmtSys_File2_Lbl.Visible = false;
        }
        else
        {
            PopulateFields_Lbl();
            repeaterVendorCourtCases_Lbl.DataBind(); repeaterVendorCourtCases.Visible = false;
            add1.Visible = false;
            othersQltyMangmtSys.Visible = false;
            othersQltyMangmtSys_File_Div.Visible = false;
            createBt.Visible = createBt1.Visible = false;
        }
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

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
    }

    void PopulateFields()
    {
        string sCommand = "SELECT * FROM tblVendorInformation WHERE VendorId= " + VendorId.ToString();;
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            othersQltyMangmtSys.SelectedValue = oReader["othersQltyMangmtSys"].ToString();
            othersQltyMangmtSys_FileLbl.Text = oReader["othersQltyMangmtSys_File"].ToString() != "" ? "<a href='" + oReader["othersQltyMangmtSys_File"].ToString() + "' target='_blank'>" + oReader["othersQltyMangmtSys_File"].ToString() + "</a>" : "Attach file";
            othersQltyMangmtSys_File.Value = oReader["othersQltyMangmtSys_File"].ToString();
            othersQltyMangmtSys_File2Lbl.Text = oReader["othersQltyMangmtSys_File2"].ToString() != "" ? "<a href='" + oReader["othersQltyMangmtSys_File2"].ToString() + "' target='_blank'>" + oReader["othersQltyMangmtSys_File"].ToString() + "</a>" : "Attach file";
            othersQltyMangmtSys_File2.Value = oReader["othersQltyMangmtSys_File2"].ToString();
                
        }
        oReader.Close();
    }

    void PopulateFields_Lbl()
    {
        string sCommand = "SELECT * FROM tblVendorInformation WHERE VendorId= " + VendorId.ToString();;
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            othersQltyMangmtSys_Lbl.Text = oReader["othersQltyMangmtSys"].ToString()!="" ? oReader["othersQltyMangmtSys"].ToString() : "No";
            othersQltyMangmtSys_File_Lbl.Text = oReader["othersQltyMangmtSys_File"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["othersQltyMangmtSys_File"].ToString() + "' target='_blank'>Attachment</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> No attached file";
            othersQltyMangmtSys_File2_Lbl.Text = oReader["othersQltyMangmtSys_File2"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["othersQltyMangmtSys_File2"].ToString() + "' target='_blank'>Attachment</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> No attached file";
                
        }
        oReader.Close();
    }

    void SaveToDB()
    {
        //string status1 = Request.Form["status"].ToString();
        string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
        string sCommand = "";

        query = "UPDATE tblVendorInformation SET othersQltyMangmtSys = @othersQltyMangmtSys, othersQltyMangmtSys_File=@othersQltyMangmtSys_File, othersQltyMangmtSys_File2=@othersQltyMangmtSys_File2 WHERE VendorId = @VendorId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@othersQltyMangmtSys", othersQltyMangmtSys.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@othersQltyMangmtSys_File", othersQltyMangmtSys_File.Value.Trim());
                cmd.Parameters.AddWithValue("@othersQltyMangmtSys_File2", othersQltyMangmtSys_File2.Value.Trim());
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }


        //CLEAR tblVendorLegalCompliance FROM USER
        sCommand = "DELETE FROM tblVendorCourtCases WHERE VendorId = " + VendorId.ToString(); ;
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        numRowsTbl = Convert.ToInt32(Request.Form["VendorCourtCaseCounter"].ToString());
        for (int i = 1; i <= numRowsTbl; i++)
        {
            query = "INSERT INTO tblVendorCourtCases (VendorId, TypeOfCase, DateRegistered, Status, Attachment) VALUES (@VendorId,  @TypeOfCase, @DateRegistered, @Status, @Attachment)";
            //query = "sp_GetVendorInformation"; //##storedProcedure
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                    cmd.Parameters.AddWithValue("@VendorId", VendorId);
                    cmd.Parameters.AddWithValue("@TypeOfCase", Request.Form["TypeOfCase" + i].ToString());
                    cmd.Parameters.AddWithValue("@DateRegistered", Request.Form["DateRegistered" + i].ToString());
                    cmd.Parameters.AddWithValue("@Status", Request.Form["Status" + i].ToString());
                    cmd.Parameters.AddWithValue("@Attachment", Request.Form["Attachment" + i].ToString());
                    conn.Open(); cmd.ExecuteNonQuery();
                }
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