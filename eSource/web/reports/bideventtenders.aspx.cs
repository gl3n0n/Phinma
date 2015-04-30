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
using EBid.lib;
using EBid.lib.constant;

public partial class web_reports_bideventtenders : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[Constant.SESSION_USERID] == null)
            Response.Redirect("sessionexpired.aspx");

        if (!IsPostBack)
        {
            int biddetailno = 0;

            rvBidTenderComparisons.LocalReport.ReportPath = Request.PhysicalApplicationPath + @"web\reports\bideventtenders.rdlc";
            rvBidTenderComparisons.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("BidTenderComparisons", sdsBidEventTenders));
            rvBidTenderComparisons.ShowReportBody = false;

            if (!string.IsNullOrEmpty(Request.QueryString["brn"]))
            {
                if (int.TryParse(Request.QueryString["brn"].Trim(), out biddetailno))
                {
                    ReportParameter[] RequestorParameter = new ReportParameter[1];
                    RequestorParameter[0] = new ReportParameter("BidRefNo", Request.QueryString["brn"].Trim());                    
                    rvBidTenderComparisons.LocalReport.SetParameters(RequestorParameter);
                }
                rvBidTenderComparisons.ShowReportBody = true;
            }
        }
    }

    protected void lnkExportToPdf_Click(object sender, EventArgs e)
    {
        PDFDeviceInfo deviceInfo = new PDFDeviceInfo("14in", "8.5in", "0.5in", "0.5in", "1in", "1in");
        ReportHelper.ExportToPDF(this, rvBidTenderComparisons, "Bid Tender Comparison Report.pdf", deviceInfo);
    }

    protected void lnkExportToExcel_Click(object sender, EventArgs e)
    {
        ReportHelper.ExportToExcel(this, rvBidTenderComparisons, "Bid Tender Comparison Report.xls");
    }

    protected void lnkRefresh_Click(object sender, EventArgs e)
    {
        rvBidTenderComparisons.LocalReport.Refresh();
    }
}
