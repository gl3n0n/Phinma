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

public partial class bidhistorybyauctionevent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {
            if (Session[Constant.PARAMETER_BIDHISTORYBYAUCTIONITEM] == null)
            { 
                return; 
            }

            AuctionBidHistoryReportParameter param = (AuctionBidHistoryReportParameter)Session[Constant.PARAMETER_BIDHISTORYBYAUCTIONITEM];

            rvAuctionEvent.LocalReport.ReportPath = Request.PhysicalApplicationPath + @"web\reports\bidhistorybyauctionevent.rdlc";
            rvAuctionEvent.ShowReportBody = false;
            ReportParameter[] RequestorParameter = new ReportParameter[2];

            RequestorParameter[0] = new ReportParameter("AuctionRefNo", param.AuctionEvent);
            RequestorParameter[1] = new ReportParameter("IsExternal", param.IsExternal.ToString());

            rvAuctionEvent.LocalReport.SetParameters(RequestorParameter);

            rvAuctionEvent.ShowReportBody = true;
            rvAuctionEvent.LocalReport.Refresh();
        }
        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Bid History by Auction Event Report");                        
    }

    private String ToJoinedString(ArrayList tmparr)
    {
        String[] tmpstrarr;
        String retstr="";
        if (tmparr.Count > 0){
            tmpstrarr = new String[tmparr.Count];
            for (int tmpind = 0; tmpind < tmparr.Count; tmpind++)
            {
                tmpstrarr[tmpind] = tmparr[tmpind].ToString();
            }
            retstr = "'" + String.Join("','", tmpstrarr) + "'";
        }
        else {
            tmpstrarr = new String[1];
            tmpstrarr[0] = "";
            retstr="«NOVALUE»";
        }
            return retstr ;
    }

    protected void lnkExportToPdf_Click(object sender, EventArgs e)
    {
        PDFDeviceInfo deviceInfo = new PDFDeviceInfo("8.5in", "11in", "0.5in", "0.5in", "1in", "1in");
        ReportHelper.ExportToPDF(this, rvAuctionEvent, "Awarded Items By Item Report.pdf", deviceInfo);
    }
    
    protected void lnkExportToExcel_Click(object sender, EventArgs e)
    {        
        ReportHelper.ExportToExcel(this, rvAuctionEvent, "Awarded Items By Item Report.xls");
    }

    protected void lnkRefresh_Click(object sender, EventArgs e)
    {
        rvAuctionEvent.LocalReport.Refresh();
    }
}
