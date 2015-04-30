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
using EBid.lib.constant;
using EBid.lib.bid.data;
using EBid.lib.report;

public partial class usercontrol_reports_vendorlist : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            clndrStartDate.Attributes.Add("style", "text-align:center;");
            clndrEndDate.Attributes.Add("style", "text-align:center;");
            clndrStartDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            clndrEndDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
        }
    }

    protected void lnkViewReport_Click(object sender, EventArgs e)
    {

        VendorListReportParameter param = new VendorListReportParameter();

        param.StartDate = DateTime.Parse(clndrStartDate.Text);
        param.EndDate = DateTime.Parse(clndrEndDate.Text);

        Session[Constant.PARAMETER_VENDORLIST] = param;

        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SHOWWINDOW", "<script type='text/javascript'> window.open('../reports/vendorlist.aspx','r1', 'toolbar=no,width=960, menubar=no, resizable=yes , scrollbars=yes'); </script>");

    }

}
