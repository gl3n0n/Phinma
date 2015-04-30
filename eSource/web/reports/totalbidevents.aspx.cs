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

public partial class web_reports_totalbidevents : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["totalbidevents_BuyerId"] == null)
            {
                return;
            }

            rvBidEvent.LocalReport.ReportPath = Request.PhysicalApplicationPath + @"web\reports\totalbidevents.rdlc";

            rvBidEvent.ShowReportBody = false;
            ReportParameter[] RequestorParameter = new ReportParameter[4];

            RequestorParameter[0] = new ReportParameter("BuyerId", Session["totalbidevents_BuyerId"].ToString());
            RequestorParameter[1] = new ReportParameter("BuyerName", Session["totalbidevents_BuyerName"].ToString());
            RequestorParameter[2] = new ReportParameter("FromDate", Session["totalbidevents_FromDate"].ToString());
            RequestorParameter[3] = new ReportParameter("ToDate", Session["totalbidevents_ToDate"].ToString());

            rvBidEvent.LocalReport.SetParameters(RequestorParameter);

            rvBidEvent.ShowReportBody = true;
            rvBidEvent.LocalReport.Refresh();
        }
        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Total Bid Events Report");
    }

    protected void lnkExportToPdf_Click(object sender, EventArgs e)
    {
        PDFDeviceInfo deviceInfo = new PDFDeviceInfo("11in", "8.5in", "0.5in", "0.5in", "0.25in", "0.25in");
        ReportHelper.ExportToPDF(this, rvBidEvent, "Total Bid Events.pdf", deviceInfo);
    }

    protected void lnkExportToExcel_Click(object sender, EventArgs e)
    {
        ReportHelper.ExportToExcel(this, rvBidEvent, "Total Bid Events.xls");
    }

    protected void lnkRefresh_Click(object sender, EventArgs e)
    {
        rvBidEvent.LocalReport.Refresh();
    }
}
