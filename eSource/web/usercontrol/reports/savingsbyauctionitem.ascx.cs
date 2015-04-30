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
using EBid.lib.auction.data;
using EBid.lib.report;

public partial class web_usercontrol_reports_savingsbyauctionitem : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (gvAuctions.Rows.Count > 0)
        {
            CheckBox checkall = (CheckBox)gvAuctions.HeaderRow.FindControl("CheckAll");
            checkall.Attributes["onclick"] = "ChangeAllCheckBoxStates(this.checked);";
            Page.ClientScript.RegisterArrayDeclaration("CheckBoxIDs", String.Concat("'", checkall.ClientID, "'"));

            foreach (GridViewRow gr in gvAuctions.Rows)
            {
                CheckBox cbSelected = (CheckBox)gr.FindControl("cbSelected");
                cbSelected.Attributes["onclick"] = "ChangeHeaderAsNeeded();";
                Page.ClientScript.RegisterArrayDeclaration("CheckBoxIDs", String.Concat("'", cbSelected.ClientID, "'"));
            }
        }
    }

    protected void lnkViewReport_Click(object sender, EventArgs e)
    {
       
        AuctionSavingsReportParameter param = new AuctionSavingsReportParameter();

        param.AuctionEvents = GetAuctionRefNos();
        param.IsExternal = rblInternalExternal.Items[0].Selected;

        Session[Constant.PARAMETER_SAVINGSBYAUCTIONITEM] = param;

        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SHOWWINDOW", "<script type='text/javascript'> window.open('../reports/savingsbyauctionevent.aspx','r1', 'toolbar=no,width=960, menubar=no, resizable=yes , scrollbars=yes'); </script>");
     
    }

    private ArrayList GetAuctionRefNos()
    {
        ArrayList auctionitemlist = new ArrayList();

        for (int i = 0; i < gvAuctions.Rows.Count; i++)
        {
            if (((CheckBox)gvAuctions.Rows[i].Cells[0].FindControl("cbSelected")).Checked)
            {
                auctionitemlist.Add(((HiddenField)gvAuctions.Rows[i].Cells[0].FindControl("hdAuctionRefNo")).Value.ToString());
            }

        }

        return auctionitemlist;
    }
}
