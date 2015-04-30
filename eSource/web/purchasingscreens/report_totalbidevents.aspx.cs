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

public partial class web_purchasingscreens_report_totalbidevents : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            clndrStartDate.Attributes.Add("style", "text-align:center;");
            clndrEndDate.Attributes.Add("style", "text-align:center;");

            if (Request.QueryString.Get("back") == "1")
            {
                lbBuyers.SelectedValue = Session["totalbidevents_BuyerId"].ToString();
                clndrStartDate.Text = Session["totalbidevents_FromDate"].ToString();
                clndrEndDate.Text = Session["totalbidevents_ToDate"].ToString();
                Panel1.Visible = true;
            }
            else
            {
                clndrStartDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                clndrEndDate.Text = DateTime.Now.ToString("MM/dd/yyyy");

                Session["totalbidevents_BuyerId"] = null;
                Session["totalbidevents_BuyerName"] = null;
                Session["totalbidevents_FromDate"] = null;
                Session["totalbidevents_ToDate"] = null;
                Session["totalbidevents_CompanyId"] = null;
                Session["totalbidevents_CompanyName"] = null;
                Panel1.Visible = false;
            }
        }
        else
        {
            Panel1.Visible = true;
        }
    }

    protected void lnkShowDetails_Click(object sender, EventArgs e)
    {
        Session["totalbidevents_BuyerId"] = lbBuyers.SelectedValue;
        Session["totalbidevents_BuyerName"] = lbBuyers.SelectedItem.Text;
        Session["totalbidevents_FromDate"] = clndrStartDate.Text;
        Session["totalbidevents_ToDate"] = clndrEndDate.Text;
    }

    protected void lnkViewReport_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SHOWWINDOW", "<script type='text/javascript'> window.open('../reports/totalbidevents.aspx','r1', 'toolbar=no,width=960, menubar=no, resizable=yes , scrollbars=yes'); </script>");
    }

    protected void gvBids_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Session["totalbidevents_CompanyId"] = Convert.ToInt32("0" + e.CommandArgument.ToString());
        Session["totalbidevents_CompanyName"] = e.CommandName;
        Response.Redirect("report_totalbidevents_items.aspx");
    }
}
