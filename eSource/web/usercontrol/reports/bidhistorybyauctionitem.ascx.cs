using System;
using System.Web;
using EBid.ConnectionString;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EBid.lib.constant;
using EBid.lib.report;

public partial class web_usercontrol_reports_bidhistorybyauctionitem : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void gvBids_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("ViewReport"))
        {
            AuctionBidHistoryReportParameter param = new AuctionBidHistoryReportParameter();

            param.AuctionEvent = e.CommandArgument.ToString();
            param.IsExternal = rblInternalExternal.Items[0].Selected;

            Session[Constant.PARAMETER_BIDHISTORYBYAUCTIONITEM] = param;                  
         
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SHOWWINDOW", "<script type='text/javascript'> window.open('../reports/bidhistorybyauctionevent.aspx','r1', 'toolbar=no,width=630, menubar=no, resizable=yes , scrollbars=yes'); </script>");
        }
    }
}
