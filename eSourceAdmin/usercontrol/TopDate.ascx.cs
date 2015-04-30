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
using EBid.lib;
using EBid.ConnectionString;
using EBid.Configurations;

public partial class web_user_control_TopDate : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
		string serverTimeInSeconds = SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "sp_GetServerTimeInSeconds").ToString();

		this.Page.ClientScript.RegisterStartupScript(GetType(), "onload", "<script>var tis = " + serverTimeInSeconds + ";</script>");
		lblDate.Text = DateTime.Now.ToString("MMMM dd, yyyy");
    }	
}
