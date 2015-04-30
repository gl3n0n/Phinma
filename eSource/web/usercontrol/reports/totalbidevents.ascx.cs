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

public partial class web_usercontrol_reports_totalbidevents : System.Web.UI.UserControl
{
    private string connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            clndrStartDate.Attributes.Add("style", "text-align:center;");
            clndrEndDate.Attributes.Add("style", "text-align:center;");
            clndrStartDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            clndrEndDate.Text = DateTime.Now.ToString("MM/dd/yyyy");

            Session["totalbidevents_BuyerId"] = null;
            Session["totalbidevents_FromDate"] = null;
            Session["totalbidevents_ToDate"] = null;
        }
    }

    protected void lnkViewReport_Click(object sender, EventArgs e)
    {
        /*
        TotalBidEventsReportParameter param = new TotalBidEventsReportParameter();

        DateTime start, end;

        if (DateTime.TryParse(clndrStartDate.Text + " 00:00:00", out start))
            param.StartDate = start;
        if (DateTime.TryParse(clndrEndDate.Text + " 23:59:59", out end))
            param.EndDate = end;

        param.Buyers = GetSelected(lbBuyers);

        Session[Constant.PARAMETER_TOTALBIDEVENTS] = param;
         */

        Session["totalbidevents_BuyerId"] = lbBuyers.SelectedValue;
        Session["totalbidevents_FromDate"] = clndrStartDate.Text;
        Session["totalbidevents_ToDate"] = clndrEndDate.Text;
    }

    protected void gvBids_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Details":
                //Session[Constant.SESSION_BIDREFNO] = e.CommandArgument.ToString();
                //Response.Redirect("bideventdetails.aspx");
                break;
        }
    }

    /*
    private int IsForOpening()
    {

        SqlConnection sqlConnect = new SqlConnection(connstring);

        int count = 0;

        sqlConnect.Open();

        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
        sqlParams[0].Value = int.Parse(Session[Constant.SESSION_BIDREFNO].ToString());

        count = Convert.ToInt32(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "sp_IsBidEventForOpening", sqlParams));

        sqlConnect.Close();

        return count;
    }
     */
}
