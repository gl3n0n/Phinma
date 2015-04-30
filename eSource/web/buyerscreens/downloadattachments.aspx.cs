using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.IO;
using EBid.lib;
using EBid.lib.constant;
using Ionic.Zip;

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
using EBid.lib.bid.data;
using EBid.lib.bid.trans;
using EBid.lib.constant;
using EBid.lib;
using EBid.ConnectionString;
using System.Data.Sql;
using System.Data.SqlClient;
using EBid.ConnectionString;
using System.Reflection;
using System.IO;
using System.Drawing;
using System.Web.Security;
using System.Collections.Generic;
using Microsoft.Reporting.WebForms;
using System.Data.Odbc;


public partial class web_buyerscreens_downloadattachments : System.Web.UI.Page
{
    private string connstring = "";
    private string sCommand;
    SqlDataReader oReader;
    string query;
    SqlCommand cmd;
    SqlConnection conn;
    public string jquery = "";
    SqlCommand myCommand;
    DataTable myDataSet;
    SqlConnection myConnection;
    string conn1 = ConfigurationManager.ConnectionStrings["EBidConnectionString1"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        string txtVendorId = Request.QueryString["vendor"];
        string txtBidRefNo = Request.QueryString["bid"];
        Response.Clear();
        Response.BufferOutput = false;
        Response.ContentType = "application/zip";
        Response.AddHeader("content-disposition", "attachment; filename=TenderAttachments_Bid_" + txtBidRefNo.ToString() + "_Vendor_" + txtVendorId.ToString() + ".zip"); // File name of a zip file

        using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
        {
            string vendorName = String.Empty;
            string path2tmp = String.Empty;

            sCommand = "SELECT * FROM tblVendorFileUploads WHERE BidRefNo=" + txtBidRefNo.ToString() + " AND VendorID=" + txtVendorId.ToString() + " AND AsDraft=0";
            string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
            SqlDataReader oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                string fileNameActual = String.Empty;
                string fileNameOrig = String.Empty;
                oReader.Read();
                string path = Constant.FILEATTACHMENTSFOLDERDIR;
                fileNameActual = path + '\\' + txtVendorId.ToString() + '\\' + txtBidRefNo.ToString() + '\\' + oReader["ActualFileName"].ToString();
                fileNameOrig = oReader["OriginalFileName"].ToString();
                if (File.Exists(fileNameActual))
                {
                    zip.AddFile(fileNameActual).FileName = fileNameOrig;
                }
            }
            oReader.Close();
            //Response.Write(sCommand);

            //foreach (GridViewRow row1 in gvInvitedSuppliers.Rows)
            //{
            //    if (row1.FindControl("lnkDownload") != null && row1.FindControl("txtFileAttachment") != null)
            //    {
            //        vendorName = (row1.FindControl("txtVendorNme") as HiddenField).Value.ToString().Replace("'", "").Replace(",", " ").Replace(".", " ");
            //        fileNameActual = (row1.FindControl("lnkDownload") as LinkButton).Text;
            //        path2tmp = (row1.FindControl("txtFileAttachment") as HiddenField).Value.ToString();
            //        string[] args = path2tmp.Split(new char[] { '|' });
            //        string path = Constant.FILEATTACHMENTSFOLDERDIR;
            //        string[] folder = args[0].Split(new char[] { '_' });
            //        path = path + folder[1].ToString() + '\\' + folder[2].ToString() + '\\';
            //        fileNameActual = path + args[0];
            //        //fileNameOrig = folder[1].ToString() + '\\' + args[1];
            //        fileNameOrig = vendorName + '\\' + args[1];
            //        if (File.Exists(fileNameActual))
            //        {
            //            zip.AddFile(fileNameActual).FileName = fileNameOrig;
            //        }
            //    }
            //}
            zip.Save(Response.OutputStream);
        }

        Response.Close();
    }
}