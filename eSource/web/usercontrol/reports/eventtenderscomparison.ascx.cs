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
using EBid.lib;
using EBid.lib.report;
using EBid.lib.constant;
using System.Data.Sql;
using System.Data.SqlClient;

public partial class web_usercontrol_reports_eventtenderscomparison : System.Web.UI.UserControl
{
    private string connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void lnkViewReport_Click(object sender, EventArgs e)
    {
        //Session[Constant.PARAMETER_EVENTTENDERSCOMPARISON] = lbEvents.SelectedValue;
        if (IsForOpening() == 0)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SHOWWINDOW", "<script type='text/javascript'> window.open('../reports/eventtenderscomparison.aspx?brn=" + lbEvents.SelectedValue + "','r1', 'toolbar=no, menubar=no, width=800; height=600, top=80, left=80, resizable=yes , scrollbars=yes'); </script>");

        }
    }

    private int IsForOpening()
    {

        SqlConnection sqlConnect = new SqlConnection(connstring);

        int count = 0;

		if (lbEvents.SelectedIndex > 0)
		{
		
        sqlConnect.Open();

        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
        sqlParams[0].Value = int.Parse(lbEvents.SelectedValue);

		count = Convert.ToInt32(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "sp_IsBidEventForOpening", sqlParams));

        sqlConnect.Close();
		}
        return count;
    }

}
