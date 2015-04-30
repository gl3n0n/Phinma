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
using EBid.lib.bid.data;
using EBid.lib.report;

public partial class web_usercontrol_reports_savingsbybiditem : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (gvBids.Rows.Count > 0)
        {
            CheckBox checkall = (CheckBox)gvBids.HeaderRow.FindControl("CheckAll");
            checkall.Attributes["onclick"] = "ChangeAllCheckBoxStates(this.checked);";
            Page.ClientScript.RegisterArrayDeclaration("CheckBoxIDs", String.Concat("'", checkall.ClientID, "'"));

            foreach (GridViewRow gr in gvBids.Rows)
            {
                CheckBox cbSelected = (CheckBox)gr.FindControl("cbSelected");
                cbSelected.Attributes["onclick"] = "ChangeHeaderAsNeeded();";
                Page.ClientScript.RegisterArrayDeclaration("CheckBoxIDs", String.Concat("'", cbSelected.ClientID, "'"));
            }
        }
    }

    protected void lnkViewReport_Click(object sender, EventArgs e)
    {

        BidSavingsReportParameter param = new BidSavingsReportParameter();

        param.BidEvents = GetBidRefNos();
        param.IsExternal = rblInternalExternal.Items[0].Selected;

        Session[Constant.PARAMETER_SAVINGSBYBIDITEM] = param;

        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SHOWWINDOW", "<script type='text/javascript'> window.open('../reports/savingsbybidevent.aspx','r1', 'toolbar=no,width=960, menubar=no, resizable=yes , scrollbars=yes'); </script>");

    }

    private ArrayList GetBidRefNos()
    {
        ArrayList biditemlist = new ArrayList();

        for (int i = 0; i < gvBids.Rows.Count; i++)
        {
            if (((CheckBox)gvBids.Rows[i].Cells[0].FindControl("cbSelected")).Checked)
            {
                biditemlist.Add(((HiddenField)gvBids.Rows[i].Cells[0].FindControl("hdBidRefNo")).Value.ToString());
            }

        }

        return biditemlist;
    }
}
