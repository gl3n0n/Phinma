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
using EBid.lib.report;
using EBid.lib.constant;

public partial class web_usercontrol_reports_awardedbiditemsbyitem : System.Web.UI.UserControl
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
        AwardedBidItemsReportParameter param = new AwardedBidItemsReportParameter();

        DateTime start, end;

        if (DateTime.TryParse(clndrStartDate.Text + " 00:00:00", out start))
            param.StartDate = start;
        if (DateTime.TryParse(clndrEndDate.Text + " 23:59:59", out end))
            param.EndDate = end;

        param.Categories = GetSelected(lbCategories);
        param.Companies = GetSelected(lbCompanies);
        param.Vendors = GetSelected(lbVendors);
        param.Items = GetSelected(lbItems);

        Session[Constant.PARAMETER_AWARDEDBIDITEMSBYITEM] = param;

        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SHOWWINDOW", "<script type='text/javascript'> window.open('../reports/awardeditemsbyitem.aspx','r1', 'toolbar=no, menubar=no, resizable=yes , scrollbars=yes'); </script>");
    }

    public ArrayList GetSelected(ListBox lb)
    {
        ArrayList list = new ArrayList();

        for (int i = 0; i < lb.Items.Count; i++)
        {
            if (lb.Items[i].Selected)
                list.Add(lb.Items[i].Value);
        }

        return list;
    }
}
