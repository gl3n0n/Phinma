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
using EBid.lib.report;
using EBid.lib.constant;
using EBid.lib;

public partial class AwardedItemsByCategory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageTitle.Text = String.Format(Constant.TITLEFORMAT, "Awarded Bid Items By Category");

        if (Session[Constant.SESSION_USERID] == null)
            Response.Redirect("sessionexpired.aspx");

        if (!IsPostBack)
        {
            if (Session[Constant.PARAMETER_AWARDEDBIDITEMSBYCATEGORY] == null)
            {
                return;
            }

            AwardedBidItemsReportParameter param = (AwardedBidItemsReportParameter)Session[Constant.PARAMETER_AWARDEDBIDITEMSBYCATEGORY];

            rvAwardedItems.LocalReport.ReportPath = Request.PhysicalApplicationPath + @"web\reports\awardeditemsbycategory.rdlc";
            this.rvAwardedItems.ShowReportBody = false;
            ReportParameter[] RequestorParameter = new ReportParameter[6];

            RequestorParameter[0] = new ReportParameter("startDate", param.StartDate.ToString());
            RequestorParameter[1] = new ReportParameter("endDate", param.EndDate.ToString());
            RequestorParameter[2] = new ReportParameter("Item", ToJoinedString(param.Items));
            RequestorParameter[3] = new ReportParameter("Category", ToJoinedString(param.Categories));
            RequestorParameter[4] = new ReportParameter("Vendor", ToJoinedString(param.Vendors));
            RequestorParameter[5] = new ReportParameter("Company", ToJoinedString(param.Companies));

            rvAwardedItems.LocalReport.SetParameters(RequestorParameter);

            this.rvAwardedItems.ShowReportBody = true;
            this.rvAwardedItems.LocalReport.Refresh();

        }
    }

    private String ToJoinedString(ArrayList tmparr)
    {
        String[] tmpstrarr;
        String retstr = "";
        if (tmparr.Count > 0)
        {
            tmpstrarr = new String[tmparr.Count];
            for (int tmpind = 0; tmpind < tmparr.Count; tmpind++)
            {
                tmpstrarr[tmpind] = tmparr[tmpind].ToString();
            }
            retstr = "'" + String.Join("','", tmpstrarr) + "'";
        }
        else
        {
            tmpstrarr = new String[1];
            tmpstrarr[0] = "";
            retstr = "«NOVALUE»";
        }
        return retstr;
    }

    protected void lnkExportToPdf_Click(object sender, EventArgs e)
    {
        PDFDeviceInfo deviceInfo = new PDFDeviceInfo("13in", "8.5in", "0.5in", "0.5in", "1in", "1in");
        ReportHelper.ExportToPDF(this, rvAwardedItems, "Awarded Items By Category Report.pdf", deviceInfo);
    }

    protected void lnkExportToExcel_Click(object sender, EventArgs e)
    {
        ReportHelper.ExportToExcel(this, rvAwardedItems, "Awarded Items By Category Report.xls");
    }

    protected void lnkRefresh_Click(object sender, EventArgs e)
    {
        rvAwardedItems.LocalReport.Refresh();
    }
}