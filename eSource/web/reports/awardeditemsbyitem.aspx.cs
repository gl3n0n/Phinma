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

public partial class AwardedItemsByItem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[Constant.SESSION_USERID] == null)
            Response.Redirect("sessionexpired.aspx");

        PageTitle.Text = String.Format(Constant.TITLEFORMAT, "Awarded Bid Items By Item");

        if (!IsPostBack)
        {
            if (Session[Constant.PARAMETER_AWARDEDBIDITEMSBYITEM] == null)
            {
                return;
            }

            AwardedBidItemsReportParameter param = (AwardedBidItemsReportParameter)Session[Constant.PARAMETER_AWARDEDBIDITEMSBYITEM];

            rvAwardedItems.LocalReport.ReportPath = Request.PhysicalApplicationPath + @"web\reports\awardeditemsbyitem.rdlc";
            rvAwardedItems.ShowReportBody = false;
            ReportParameter[] RequestorParameter = new ReportParameter[6];

            RequestorParameter[0] = new ReportParameter("startDate", param.StartDate.ToString());
            RequestorParameter[1] = new ReportParameter("endDate", param.EndDate.ToString());
            RequestorParameter[2] = new ReportParameter("Item", ToJoinedString(param.Items));
            RequestorParameter[3] = new ReportParameter("Category", ToJoinedString(param.Categories));
            RequestorParameter[4] = new ReportParameter("Vendor", ToJoinedString(param.Vendors));
            RequestorParameter[5] = new ReportParameter("Company", ToJoinedString(param.Companies));

            rvAwardedItems.LocalReport.SetParameters(RequestorParameter);

            rvAwardedItems.ShowReportBody = true;
            rvAwardedItems.LocalReport.Refresh();
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
        ReportHelper.ExportToPDF(this, rvAwardedItems, "Awarded Items By Item Report.pdf", deviceInfo);
    }

    protected void lnkExportToExcel_Click(object sender, EventArgs e)
    {
        ReportHelper.ExportToExcel(this, rvAwardedItems, "Awarded Items By Item Report.xls");
    }

    protected void lnkRefresh_Click(object sender, EventArgs e)
    {
        rvAwardedItems.LocalReport.Refresh();
    }
}
