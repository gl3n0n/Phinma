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

public partial class web_purchasingscreens_report_totalbidevents_items : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text = Session["totalbidevents_CompanyName"].ToString();
    }

    protected void gvBids_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Details":
                Session[Constant.SESSION_BIDREFNO] = e.CommandArgument.ToString();
                Response.Redirect("report_totalbidevents_itemdetails.aspx");
                break;
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("report_totalbidevents.aspx?back=1");
    }
    
    protected void lnkViewReport_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SHOWWINDOW", "<script type='text/javascript'> window.open('../reports/totalbideventsitems.aspx','r1', 'toolbar=no,width=960, menubar=no, resizable=yes , scrollbars=yes'); </script>");
    }
}
