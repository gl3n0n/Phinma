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

public partial class reports_vendorlist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session[Constant.PARAMETER_VENDORLIST] == null)
            {
                return;
            }

            VendorListReportParameter param = (VendorListReportParameter)Session[Constant.PARAMETER_VENDORLIST];

            ObjectDataSource1.SelectParameters[0].DefaultValue = param.StartDate.ToString();
            ObjectDataSource1.SelectParameters[1].DefaultValue = param.EndDate.ToString();

            ReportParameter[] RequestorParameter = new ReportParameter[2];
            RequestorParameter[0] = new ReportParameter("StartDate", param.StartDate.ToString());
            RequestorParameter[1] = new ReportParameter("EndDate", param.EndDate.ToString());

            rvVendorList.LocalReport.ReportPath = Request.PhysicalApplicationPath + @"reports\vendorlist.rdlc";
            rvVendorList.LocalReport.SetParameters(RequestorParameter);
            rvVendorList.ShowReportBody = true;
            //rvVendorList.LocalReport.Refresh();
        }
        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Vendor List Report");
    }

    protected void lnkExportToPdf_Click(object sender, EventArgs e)
    {
        PDFDeviceInfo deviceInfo = new PDFDeviceInfo("11in", "8.5in", "0.5in", "0.5in", "0.25in", "0.25in");
        ReportHelper.ExportToPDF(this, rvVendorList, "Vendor List Report.pdf", deviceInfo);
    }

    protected void lnkExportToExcel_Click(object sender, EventArgs e)
    {
        ReportHelper.ExportToExcel(this, rvVendorList, "Vendor List Report.xls");
    }

    protected void lnkRefresh_Click(object sender, EventArgs e)
    {
        rvVendorList.LocalReport.Refresh();
    }

}
