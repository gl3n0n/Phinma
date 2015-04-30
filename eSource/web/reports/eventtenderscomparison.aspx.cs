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

public partial class web_reports_eventtenderscomparison : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[Constant.SESSION_USERID] == null)
            Response.Redirect("sessionexpired.aspx");

        PageTitle.Text = String.Format(Constant.TITLEFORMAT, "Bid Event Tenders Comparison");

        if (!IsPostBack)
        {
            int bidrefno = 0;
            rvBidEventTendersComparisons.LocalReport.ReportPath = Request.PhysicalApplicationPath + @"web\reports\eventtenderscomparison.rdlc";
            rvBidEventTendersComparisons.ShowReportBody = false;

            if (!string.IsNullOrEmpty(Request.QueryString["brn"]))
            {
                if (int.TryParse(Request.QueryString["brn"].Trim(), out bidrefno))
                {
                    ReportParameter[] RequestorParameter = new ReportParameter[1];
                    RequestorParameter[0] = new ReportParameter("BidRefNo", bidrefno.ToString());
                    //RequestorParameter[1] = new ReportParameter("UseAlias", "0");
                    rvBidEventTendersComparisons.LocalReport.SetParameters(RequestorParameter);
                }
                rvBidEventTendersComparisons.ShowReportBody = true;
            }
        }
    }

    protected void chkShowVendorName_CheckedChanged(object sender, EventArgs e)
    {
        int bidrefno = 0;
        string showVendorName = chkShowVendorName.Checked ? "0" : "1";

        if (!string.IsNullOrEmpty(Request.QueryString["brn"]))
        {
            if (int.TryParse(Request.QueryString["brn"].Trim(), out bidrefno))
            {
                ReportParameter[] RequestorParameter = new ReportParameter[1];
                RequestorParameter[0] = new ReportParameter("BidRefNo", Request.QueryString["brn"].Trim());
                //RequestorParameter[1] = new ReportParameter("UseAlias", "0");
                rvBidEventTendersComparisons.LocalReport.SetParameters(RequestorParameter);
            }
            rvBidEventTendersComparisons.ShowReportBody = true;
        }
    }
        
    protected void lnkExportToPdf_Click(object sender, EventArgs e)
    {
        PDFDeviceInfo deviceInfo = new PDFDeviceInfo("14in", "8.5in", "0.5in", "0.5in", "1in", "1in");
        ReportHelper.ExportToPDF(this, rvBidEventTendersComparisons, "Bid Event Tenders Comparison Report.pdf", deviceInfo);
    }

    protected void lnkExportToExcel_Click(object sender, EventArgs e)
    {
        ReportHelper.ExportToExcel(this, rvBidEventTendersComparisons, "Bid Event Tenders Comparison Report.xls");
    }

    protected void lnkRefresh_Click(object sender, EventArgs e)
    {
        rvBidEventTendersComparisons.LocalReport.Refresh();
    }
}
