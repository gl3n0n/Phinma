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
using EBid.lib;
using EBid.lib.report;
using EBid.lib.constant;

public partial class reports_totalbids : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session[Constant.PARAMETER_TOTALBIDS] == null)
            {
                return;
            }

            TotalBidsReportParameter param = (TotalBidsReportParameter)Session[Constant.PARAMETER_TOTALBIDS];

            ObjectDataSource1.SelectParameters[0].DefaultValue = param.StartDate.ToString();
            ObjectDataSource1.SelectParameters[1].DefaultValue = param.EndDate.ToString();

            ReportParameter[] RequestorParameter = new ReportParameter[2];
            RequestorParameter[0] = new ReportParameter("StartDate", param.StartDate.ToString());
            RequestorParameter[1] = new ReportParameter("EndDate", param.EndDate.ToString());

            rvTotalBids.LocalReport.ReportPath = Request.PhysicalApplicationPath + @"reports\totalbids.rdlc";
            rvTotalBids.LocalReport.SetParameters(RequestorParameter);
            rvTotalBids.ShowReportBody = true;
            //rvTotalBids.LocalReport.Refresh();
        }
        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Total Bids Report");
    }

    protected void lnkExportToPdf_Click(object sender, EventArgs e)
    {
        PDFDeviceInfo deviceInfo = new PDFDeviceInfo("11in", "8.5in", "0.5in", "0.5in", "0.25in", "0.25in");
        ReportHelper.ExportToPDF(this, rvTotalBids, "Total Bids Report.pdf", deviceInfo);
    }

    protected void lnkExportToExcel_Click(object sender, EventArgs e)
    {
        ReportHelper.ExportToExcel(this, rvTotalBids, "Total Bids Report.xls");
    }

    protected void lnkRefresh_Click(object sender, EventArgs e)
    {
        rvTotalBids.LocalReport.Refresh();
    }

}
