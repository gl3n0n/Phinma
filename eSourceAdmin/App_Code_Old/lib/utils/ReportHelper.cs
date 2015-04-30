using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Text;
using EBid.lib.report;
using EBid.lib.constant;

namespace EBid.lib
{
    public static class ReportHelper
    {
        public static void ExportToPDF(Page page, ReportViewer rptViewer, string fileName)
        {
            PDFDeviceInfo deviceInfo = new PDFDeviceInfo();

            ExportToPDF(page, rptViewer, fileName, deviceInfo);
        }

        public static void ExportToPDF(Page page, ReportViewer rptViewer, string fileName, PDFDeviceInfo deviceInfo)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytes = rptViewer.LocalReport.Render(
               deviceInfo.OutputFormat, deviceInfo.ToString(),
               out mimeType, out encoding,
               out extension, out streamids, out warnings);

            FileInfo fInfo = new FileInfo(Path.GetTempFileName());

            FileStream fs = new FileStream(fInfo.FullName, FileMode.Create);

            fs.Write(bytes, 0, bytes.Length);
            fs.Close();

            FileHelper.DownloadFile(page, fInfo.DirectoryName, fInfo.Name, fileName);
        }

        public static void ExportToExcel(Page page, ReportViewer rptViewer, string fileName)
        {
            ExcelDeviceInfo deviceInfo = new ExcelDeviceInfo();

            ExportToExcel(page, rptViewer, fileName, deviceInfo);
        }

        public static void ExportToExcel(Page page, ReportViewer rptViewer, string fileName, ExcelDeviceInfo deviceInfo)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytes = rptViewer.LocalReport.Render(
               deviceInfo.OutputFormat, deviceInfo.ToString(),
               out mimeType, out encoding,
               out extension, out streamids, out warnings);

            FileInfo fInfo = new FileInfo(Path.GetTempFileName());

            FileStream fs = new FileStream(fInfo.FullName, FileMode.Create);

            fs.Write(bytes, 0, bytes.Length);
            fs.Close();

            FileHelper.DownloadFile(page, fInfo.DirectoryName, fInfo.Name, fileName);
        }        
    }
        
    public class ExcelDeviceInfo
    {
        string _omitDocumentMap, _omitFormulas, _removeSpace, _simplePageHeaders;

        public ExcelDeviceInfo()
        { 
            _omitDocumentMap = "false";
            _omitFormulas = "false";
            _removeSpace = "0.125in";
            _simplePageHeaders = "false";
        }

        public ExcelDeviceInfo(string omitDocumentMap, string omitFormulas, string removeSpace, string simplePageHeaders)
        { 
            _omitDocumentMap = omitDocumentMap;
            _omitFormulas = omitFormulas;
            _removeSpace = removeSpace;
            _simplePageHeaders = simplePageHeaders;
        }

        public string OutputFormat
        {
            get { return "EXCEL"; }
        }

        public string OmitDocumentMap
        {
            get { return _omitDocumentMap; }            
            set { _omitDocumentMap = value;}
        }

        public string OmitFormulas
        {
            get { return _omitFormulas; }
            set { _omitFormulas = value; }
        }

        public string RemoveSpace
        {
            get { return _removeSpace; }
            set { _removeSpace = value; }
        }

        public string SimplePageHeaders
        {
            get { return _simplePageHeaders; }
            set { _simplePageHeaders = value; }
        }

        public override string ToString()
        {
            return String.Format("<DeviceInfo><OutputFormat>{0}</OutputFormat><OmitDocumentMap>{1}</OmitDocumentMap><OmitFormulas>{2}</OmitFormulas><RemoveSpace>{3}</RemoveSpace><SimplePageHeaders>{4}</SimplePageHeaders></DeviceInfo>",
                this.OutputFormat, this.OmitDocumentMap, this.OmitFormulas, this.RemoveSpace, this.SimplePageHeaders);
        }
    }

    public class PDFDeviceInfo
    {
        string _width, _height, _top, _bottom, _left, _right;

        public PDFDeviceInfo()
        {
            // load defaults
            _width = "8.5in";
            _height = "11in";
            _top = "0.5in";
            _bottom = "0.5in";
            _left = "0.5in";
            _right = "0.5in";
        }

        public PDFDeviceInfo(string pageWidth, string pageHeight, string marginTop, string marginBottom, string marginLeft, string marginRight)
        {            
            _width = pageWidth;
            _height = pageHeight;
            _top = marginTop;
            _bottom = marginBottom;
            _left = marginLeft;
            _right = marginRight;
        }

        public string OutputFormat
        {
            get { return "PDF"; }
        }

        public string PageWidth
        {
            get { return _width; }
            set { _width = value; }
        }

        public string PageHeight
        {
            get { return _height; }
            set { _height = value; }
        }

        public string MarginTop
        {
            get { return _top; }
            set { _top = value; } 
        }

        public string MarginBottom
        {
            get { return _bottom; }
            set { _bottom = value; }
        }

        public string MarginLeft
        {
            get { return _left; }
            set { _left = value; }
        }

        public string MarginRight
        {
            get { return _right; }
            set { _right = value; }
        }

        public override string ToString()
        {
            return String.Format("<DeviceInfo><OutputFormat>{0}</OutputFormat><PageWidth>{1}</PageWidth><PageHeight>{2}</PageHeight><MarginTop>{3}</MarginTop><MarginLeft>{4}</MarginLeft><MarginRight>{5}</MarginRight><MarginBottom>{6}</MarginBottom></DeviceInfo>",
                this.OutputFormat, this.PageWidth, this.PageHeight, this.MarginTop, this.MarginLeft, this.MarginRight, this.MarginBottom);
        }
    }
}