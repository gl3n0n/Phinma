using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EBid.lib;
using EBid.lib.constant;
using EBid.lib.utils;

public partial class admin_deletedUsers : System.Web.UI.Page
{
    private string connstring;

    protected void Page_Load(object sender, EventArgs e)
    {
        connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();

        if (!Page.IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["t"]))
                ddlUserTypes.SelectedIndex = int.Parse(Request.QueryString["t"]) < 5 ? int.Parse(Request.QueryString["t"]) : 0;
            else
                ddlUserTypes.SelectedIndex = Request.Cookies["selectedusertype"] != null ? int.Parse(Request.Cookies["selectedusertype"].Value) : 0;
        }
    }

    private int GetUserType(string userId)
    {
        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@Userid", SqlDbType.Int);
        sqlParams[0].Value = Int32.Parse(userId);

        return Convert.ToInt32(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "sp_GetUserType", sqlParams));
    }

    protected void gvRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("recoverUser"))
        {
            string userType = GetUserType(e.CommandArgument.ToString().Trim()).ToString().Trim();

            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@userID", SqlDbType.Int);
            sqlParams[1] = new SqlParameter("@userType", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(e.CommandArgument.ToString().Trim());
            sqlParams[1].Value = Int32.Parse(userType);

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_RecoverUser", sqlParams);

            int tempInt = Int32.Parse(userType);
            tempInt -= 1;

            Response.Cookies.Add(new HttpCookie("selectedusertype", ddlUserTypes.SelectedIndex.ToString()));

            Response.Redirect("deletedUsers.aspx");
        }
    }
}