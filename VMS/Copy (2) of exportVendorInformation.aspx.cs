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
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.IO;

public partial class exportVendorInformation : System.Web.UI.Page
{
    SqlDataReader oReader;
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
    string query;
    SqlCommand cmd;
    SqlConnection conn;
    string VendorIdstr;
    SqlCommand myCommand;
    DataTable myDataSet;
    SqlConnection myConnection;


    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (Request.QueryString["vendorid"] != null && Request.QueryString["vendorid"].ToString() != "")
        {
            Session["VendorId"] = Request.QueryString["vendorid"].ToString().Trim();
            if (Session["VendorId"] != null)
            {
                if (Session["VendorId"].ToString() != "")
                {
                    fillGrid(Session["VendorId"].ToString()); ExportToExcel(Session["VendorId"].ToString());
                }
            }
        }
        if (!IsPostBack)
        {
            
        }
    }
    protected void fillGrid(string VendorIdx)
    {
        //string str = "SELECT TOP 10 VendorName, VendorEmail, MobileNo, VendorAddress FROM tblVendors";
        string str = "sp_GetVendorInformation";

        myConnection = new SqlConnection(connstring);
        myConnection.Open();
        myCommand = new SqlCommand(str, myConnection);
        myCommand.CommandType = CommandType.StoredProcedure;
        myCommand.Parameters.AddWithValue("@VendorId", Convert.ToInt32(VendorIdx));
        SqlDataAdapter mySQLDataAdapter;
        myDataSet = new DataTable();
        mySQLDataAdapter = new SqlDataAdapter(myCommand);
        mySQLDataAdapter.Fill(myDataSet);
        //GridView1.DataSource = myDataSet;
        //GridView1.DataBind();
        ViewState["dtList"] = myDataSet;
    }
    //GridView1.DataSource = myDataSet;
    protected void ExportToExcel(string VendorIdx)
    {
        try
        {
            DataTable dt1 = (DataTable)ViewState["dtList"];
            if (dt1 == null)
            {
                throw new Exception("No Records to Export");
            }
            //string Path = "C:\\eSourcePOtoACCPAC\\eSourceForPO_" + Session["BidRefNo"] + ".xls";
            string Path = "C:\\eSourcePOtoACCPAC\\VendorImportFile_" + VendorIdx + ".xls";
            FileInfo FI = new FileInfo(Path);
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWriter);
            DataGrid DataGrd = new DataGrid();
            DataGrd.DataSource = dt1;
            DataGrd.DataBind();

            DataGrd.RenderControl(htmlWrite);
            string directory = Path.Substring(0, Path.LastIndexOf("\\"));// GetDirectory(Path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            Session["VendorId"] = "";
            System.IO.StreamWriter vw = new System.IO.StreamWriter(Path, true);
            stringWriter.ToString().Normalize();
            vw.Write(stringWriter.ToString());
            vw.Flush();
            vw.Close();
            WriteAttachment(FI.Name, "application/vnd.ms-excel", stringWriter.ToString());
        }
        catch (Exception ex)
        {
            //throw new Exception(ex.Message);
        }
    }


    public static void WriteAttachment(string FileName, string FileType, string content)
    {
        HttpResponse Response = System.Web.HttpContext.Current.Response;
        Response.ClearHeaders();
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
        Response.ContentType = FileType;
        Response.Write(content);
        Response.End();

    }

}