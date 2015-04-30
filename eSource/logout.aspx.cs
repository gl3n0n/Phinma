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
using System.Data.Sql;
using System.Data.SqlClient;
using EBid.lib;
using EBid.lib.constant;

public partial class logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
	if (Session[Constant.SESSION_USERID] != null)
	{
           UpdateUserLoginStatus(Session[Constant.SESSION_USERID].ToString(), 0, Session.SessionID.ToString());
	}

        FormsAuthenticationHelper.SignOutAndRedirectToLogin();

		//Response.Buffer= true;
		//Response.ExpiresAbsolute=DateTime.Now.AddDays(-1d);
		//Response.Expires =-1500;
		//Response.CacheControl = "no-cache";		
		//Response.AppendHeader("Refresh", "1; URL=login.aspx");
		//RegisterStartupScript("disableback", "<script language='text/javascript'><!-- javascript:window.history.forward(1); //--></script>");
	}

    private void UpdateUserLoginStatus(string vUserId, int vLoginStatus, string vSessionId)
    {
        SqlParameter[] sqlParams = new SqlParameter[3];
        sqlParams[0] = new SqlParameter("@UserId", SqlDbType.Int);
        sqlParams[0].Value = Int32.Parse(vUserId);
        sqlParams[1] = new SqlParameter("@SessionId", SqlDbType.NVarChar);
        sqlParams[1].Value = vSessionId;
        sqlParams[2] = new SqlParameter("@LoginStatus", SqlDbType.Int);
        sqlParams[2].Value = vLoginStatus;

        SqlHelper.ExecuteNonQuery(HttpContext.Current.Session["ConnectionString"].ToString() , CommandType.StoredProcedure, "sp_UpdateUserLoginStatus", sqlParams);
    }
}
