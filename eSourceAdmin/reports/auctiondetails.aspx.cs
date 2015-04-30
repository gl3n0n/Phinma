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

public partial class auctiondetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[Constant.SESSION_USERID] == null)
            Response.Redirect("sessionexpired.aspx");

        if (!IsPostBack)
        {
            int auctionrefno = 0;

            rvAuctionDetails.LocalReport.ReportPath = Request.PhysicalApplicationPath + @"web\reports\auctiondetails.rdlc";
            this.rvAuctionDetails.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("AuctionDetails", sdsAuctionDetails));
            this.rvAuctionDetails.ShowReportBody = false;

            if (!string.IsNullOrEmpty(Request.QueryString["arn"]))
            {
                if (int.TryParse(Request.QueryString["arn"].Trim(), out auctionrefno))
                {
                    ReportParameter[] RequestorParameter = new ReportParameter[1];
                    RequestorParameter[0] = new ReportParameter("AuctionRefNo", Request.QueryString["arn"].Trim());
                    rvAuctionDetails.LocalReport.SetParameters(RequestorParameter);
                }
                rvAuctionDetails.ShowReportBody = true;
            }
        }
    }

    protected void lnkExportToPdf_Click(object sender, EventArgs e)
    {
        PDFDeviceInfo deviceInfo = new PDFDeviceInfo("13in", "8.5in", "0.5in", "0.5in", "1in", "1in");
        ReportHelper.ExportToPDF(this, rvAuctionDetails, "Auction Details Report.pdf", deviceInfo);
    }

    protected void lnkExportToExcel_Click(object sender, EventArgs e)
    {
        ReportHelper.ExportToExcel(this, rvAuctionDetails, "Auction Details Report.xls");
    }

    protected void lnkRefresh_Click(object sender, EventArgs e)
    {
        rvAuctionDetails.LocalReport.Refresh();
    }
}
