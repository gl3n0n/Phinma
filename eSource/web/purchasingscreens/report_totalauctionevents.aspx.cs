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

public partial class web_purchasingscreens_report_totalauctionevents : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            clndrStartDate.Attributes.Add("style", "text-align:center;");
            clndrEndDate.Attributes.Add("style", "text-align:center;");

            if (Request.QueryString.Get("back") == "1")
            {
                lbBuyers.SelectedValue = Session["totalauctionevents_BuyerId"].ToString();
                clndrStartDate.Text = Session["totalauctionevents_FromDate"].ToString();
                clndrEndDate.Text = Session["totalauctionevents_ToDate"].ToString();
                Panel1.Visible = true;
            }
            else
            {
                clndrStartDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                clndrEndDate.Text = DateTime.Now.ToString("MM/dd/yyyy");

                Session["totalauctionevents_BuyerId"] = null;
                Session["totalauctionevents_BuyerName"] = null;
                Session["totalauctionevents_FromDate"] = null;
                Session["totalauctionevents_ToDate"] = null;
                Session["totalauctionevents_CompanyId"] = null;
                Session["totalauctionevents_CompanyName"] = null;
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
        Session["totalauctionevents_BuyerId"] = lbBuyers.SelectedValue;
        Session["totalauctionevents_BuyerName"] = lbBuyers.SelectedItem.Text;
        Session["totalauctionevents_FromDate"] = clndrStartDate.Text;
        Session["totalauctionevents_ToDate"] = clndrEndDate.Text;
    }

    protected void lnkViewReport_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SHOWWINDOW", "<script type='text/javascript'> window.open('../reports/totalauctionevents.aspx','r1', 'toolbar=no,width=960, menubar=no, resizable=yes , scrollbars=yes'); </script>");
    }

    protected void gvBids_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Session["totalauctionevents_CompanyId"] = Convert.ToInt32("0" + e.CommandArgument.ToString());
        Session["totalauctionevents_CompanyName"] = e.CommandName;
        Response.Redirect("report_totalauctionevents_items.aspx");
    }
}
