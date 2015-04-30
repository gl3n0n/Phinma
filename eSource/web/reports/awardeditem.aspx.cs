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

public partial class awardeditem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[Constant.SESSION_USERID] == null)
            Response.Redirect("sessionexpired.aspx");

        if (!IsPostBack)
        {
            int biddetailno = 0;

            rvAwardedItem.LocalReport.ReportPath = Request.PhysicalApplicationPath + @"web\reports\awardeditem.rdlc";
            this.rvAwardedItem.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("AwardedItem", sdsAwardedItem));
            this.rvAwardedItem.ShowReportBody = false;

            if (!string.IsNullOrEmpty(Request.QueryString["bdn"]))
            {
                if (int.TryParse(Request.QueryString["bdn"].Trim(), out biddetailno))
                {
                    ReportParameter[] RequestorParameter = new ReportParameter[1];
                    RequestorParameter[0] = new ReportParameter("BidDetailNo", Request.QueryString["bdn"].Trim());
                    rvAwardedItem.LocalReport.SetParameters(RequestorParameter);
                }
                rvAwardedItem.ShowReportBody = true;
            }
        }
    }

    protected void lnkExportToPdf_Click(object sender, EventArgs e)
    {
        PDFDeviceInfo deviceInfo = new PDFDeviceInfo("8.5in", "11in", "0.5in", "0.5in", "1in", "1in");
        ReportHelper.ExportToPDF(this, rvAwardedItem, "Awarded Item Details Report.pdf", deviceInfo);
    }

    protected void lnkExportToExcel_Click(object sender, EventArgs e)
    {
        ReportHelper.ExportToExcel(this, rvAwardedItem, "Awarded Item Details Report.xls");
    }

    protected void lnkRefresh_Click(object sender, EventArgs e)
    {
        rvAwardedItem.LocalReport.Refresh();
    }
}
