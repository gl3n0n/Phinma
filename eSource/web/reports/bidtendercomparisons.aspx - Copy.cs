using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using EBid.lib;
using EBid.lib.constant;

public partial class bidtendercomparisons : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageTitle.Text = String.Format(Constant.TITLEFORMAT, "Bid Event Tenders Comparison");

        if (Session[Constant.SESSION_USERID] == null)
            Response.Redirect("sessionexpired.aspx");

        if (!IsPostBack)
        {
            int biddetailno = 0;

            rvBidTenderComparisons.LocalReport.ReportPath = Request.PhysicalApplicationPath + @"web\reports\bidtendercomparisons.rdlc";
            rvBidTenderComparisons.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("BidTenderComparisons", sdsBidTenderComparisons));
            rvBidTenderComparisons.ShowReportBody = false;

            if (!string.IsNullOrEmpty(Request.QueryString["bdn"]))
            {
                if (int.TryParse(Request.QueryString["bdn"].Trim(), out biddetailno))
                {
                    ReportParameter[] RequestorParameter = new ReportParameter[3];
                    RequestorParameter[0] = new ReportParameter("BidDetailNo", Request.QueryString["bdn"].Trim());                    
                    RequestorParameter[1] = new ReportParameter("UseAlias", "0");
                    RequestorParameter[2] = new ReportParameter("ClientId", "0");
                    rvBidTenderComparisons.LocalReport.SetParameters(RequestorParameter);
                }
                rvBidTenderComparisons.ShowReportBody = true;
            }
        }
    }

    protected void chkShowVendorName_CheckedChanged(object sender, EventArgs e)
    {
        int biddetailno = 0;
        string showVendorName = chkShowVendorName.Checked ? "0" : "1";

        if (!string.IsNullOrEmpty(Request.QueryString["bdn"]))
        {
            if (int.TryParse(Request.QueryString["bdn"].Trim(), out biddetailno))
            {
                ReportParameter[] RequestorParameter = new ReportParameter[3];
                RequestorParameter[0] = new ReportParameter("BidDetailNo", Request.QueryString["bdn"].Trim());
                RequestorParameter[1] = new ReportParameter("UseAlias", "0");
                RequestorParameter[2] = new ReportParameter("ClientId", "0");
                rvBidTenderComparisons.LocalReport.SetParameters(RequestorParameter);
            }
            rvBidTenderComparisons.ShowReportBody = true;
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
