using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Text;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Reflection;
using System.IO;
using System.Drawing;
using System.Web.Security;
using System.Collections.Generic;
//using Microsoft.Reporting.WebForms;
using System.Data.Odbc;

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using System.IO;
using OfficeOpenXml.Style;
using System.Drawing;

using Ava.lib;
using Ava.lib.constant;

using CarlosAg.ExcelXmlWriter;

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
                    fillGrid(); 
					ExportToExcel();
                }
            }
        }
        if (!IsPostBack)
        {
            
        }
    }

    protected void fillGrid()
    {
        //string str = "SELECT TOP 10 VendorName, VendorEmail, MobileNo, VendorAddress FROM tblVendors";
        string str = "sp_GetVendorInformation";

        myConnection = new SqlConnection(connstring);
        myConnection.Open();
        myCommand = new SqlCommand(str, myConnection);
        myCommand.CommandType = CommandType.StoredProcedure;
        myCommand.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"].ToString()));
        SqlDataAdapter mySQLDataAdapter;
        myDataSet = new DataTable();
        mySQLDataAdapter = new SqlDataAdapter(myCommand);
        mySQLDataAdapter.Fill(myDataSet);
        //GridView1.DataSource = myDataSet;
        //GridView1.DataBind();
        ViewState["dtList"] = myDataSet;
    }
    //GridView1.DataSource = myDataSet;
    protected void ExportToExcel()
    {
        try
        {

            fillGrid();
            DataTable dt1 = (DataTable)ViewState["dtList"];
            if (dt1 == null)
            {
                throw new Exception("No Records to Export");
            }
            else
            {
                DataGrid DataGrd = new DataGrid();
                DataGrd.DataSource = dt1;
                DataGrd.DataBind();


                string Path1 = "C:\\eSourcePOtoACCPAC\\VendorImportFile_" + Session["VendorId"].ToString() + ".xlsx";
				

                int i = 1;
                //foreach (DataRow row in dt1.Rows)
                //{
                //    if (i == 2)
                //    {
                //        Path1 = ACCPAC + row["5a"].ToString() + "\\PO Import-" + Session["BidRefNo"].ToString() + ".xlsx";
                //    }
                //    i++;
                //}
                
				FileInfo newFile = new FileInfo(Path1);
                if (newFile.Exists)
                {
                    newFile.Delete();
                }
				
				

                using (ExcelPackage pck = new ExcelPackage(newFile))
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add(Session["VendorId"].ToString());

                    //HEADER
                    i = 1;
                    ws.Cells["A" + i].Value = "VENDORID";
                    ws.Cells["B" + i].Value = "VENDNAME";
                    ws.Cells["C" + i].Value = "TEXTSTRE1";
                    ws.Cells["D" + i].Value = "TEXTSTRE2";
                    ws.Cells["E" + i].Value = "CITY";
                    ws.Cells["F" + i].Value = "PROVINCE";
                    ws.Cells["G" + i].Value = "ZIPCODE";
                    ws.Cells["H" + i].Value = "COUNTRY";
                    ws.Cells["I" + i].Value = "TEXTPHON1";
                    ws.Cells["J" + i].Value = "CONTACT";
                    ws.Cells["K" + i].Value = "CONTACTPHONE";
                    ws.Cells["L" + i].Value = "CTACTFAX";
                    ws.Cells["M" + i].Value = "EMAIL1";
                    ws.Cells["N" + i].Value = "EMAIL2";
                    ws.Cells["O" + i].Value = "WEBSITE";
                    ws.Cells["P" + i].Value = "GROUPID";

                    i = 2;
                    foreach (DataRow row in dt1.Rows)
                    {
                        ws.Cells["A" + i].Value = row["VENDORID"].ToString(); 
                        ws.Cells["B" + i].Value = row["VENDNAME"].ToString(); 
                        ws.Cells["C" + i].Value = row["TEXTSTRE1"].ToString(); 
                        ws.Cells["D" + i].Value = row["TEXTSTRE2"].ToString(); 
                        ws.Cells["E" + i].Value = row["CITY"].ToString(); 
                        ws.Cells["F" + i].Value = row["PROVINCE"].ToString(); 
                        ws.Cells["G" + i].Value = row["ZIPCODE"].ToString(); 
                        ws.Cells["H" + i].Value = row["COUNTRY"].ToString(); 
                        ws.Cells["I" + i].Value = row["TEXTPHON1"].ToString(); 
                        ws.Cells["J" + i].Value = row["CONTACT"].ToString(); 
                        ws.Cells["K" + i].Value = row["CONTACTPHONE"].ToString(); 
                        ws.Cells["L" + i].Value = row["CTACTFAX"].ToString();
                        ws.Cells["M" + i].Value = row["EMAIL1"].ToString();
                        ws.Cells["N" + i].Value = row["EMAIL2"].ToString();
                        ws.Cells["O" + i].Value = row["WEBSITE"].ToString();
                        ws.Cells["P" + i].Value = row["GROUPID"].ToString();
                        //Response.Write(row[1].ToString());
                        i++;
                    }

                    //SAVE TO {Path1}
                    //pck.Save();

                    //SEND Download file to client
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;  filename=VendorImportFile_" + Session["VendorId"].ToString() + ".xlsx");
                    Response.BinaryWrite(pck.GetAsByteArray());
                }			
								
				//book1.Save(Path1);
				
            }


        }
        catch (Exception ex)
        {
            //throw new Exception(ex.Message);
        }
    }

}