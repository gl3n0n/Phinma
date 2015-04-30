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

public partial class admin_searchUser : System.Web.UI.Page
{
    private string connstring;

    protected void Page_Load(object sender, EventArgs e)
    {
        connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();

        if (!IsPostBack)
            if (Request.Cookies["selectedusertype"] != null)
                ddlUserTypes.SelectedIndex = (!String.IsNullOrEmpty(Request.Cookies["selectedusertype"].Value)) ? int.Parse(Request.Cookies["selectedusertype"].Value) : 0;

        if (ddlUserTypes.SelectedIndex == 1)
            lblNote.Visible = true;
        else
            lblNote.Visible = false;
    }

    private int GetUserType(string userId)
    {
        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@Userid", SqlDbType.Int);
        sqlParams[0].Value = Int32.Parse(userId);

        return Convert.ToInt32(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "sp_GetUserType", sqlParams));
    }

    protected void gvRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {            
            Label lblUsername = (Label)e.Row.FindControl("lblUsername");
            LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            LinkButton lnkActDeact = (LinkButton)e.Row.FindControl("lnkActDeact");
            HiddenField hdStatus = (HiddenField)e.Row.FindControl("hdStatus");
            HiddenField hdUserId = (HiddenField)e.Row.FindControl("hdUserId");
            
            if (hdStatus.Value == "0")
            {
                lnkActDeact.Text = "Activate";
                lnkActDeact.CommandName = "Activate";
                lnkActDeact.OnClientClick = "return confirm('Are you sure you want to activate this user?')";
            }
            else if (hdStatus.Value == "1")
            {
                lnkActDeact.Text = "Deactivate";
                lnkActDeact.CommandName = "Deactivate";
                lnkActDeact.OnClientClick = "return confirm('Are you sure you want to deactivate this user?')";
            }
            else if (hdStatus.Value.Trim() == "2")
            {
                lnkEdit.Enabled = false;
                lnkDelete.Enabled = false;
            }

            if (ddlUserTypes.SelectedValue == "2")
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
                sqlParams[0].Value = Int32.Parse(hdUserId.Value);

                if (Convert.ToInt32(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_CheckVendorStatus", sqlParams)) == 1)
                    lblUsername.ForeColor = System.Drawing.Color.Red;
            }

        }
    }

    protected void gvUsersRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("EditUser"))
        {
            string userType = GetUserType(e.CommandArgument.ToString().Trim()).ToString().Trim();

            int tempInt = Int32.Parse(userType);
            tempInt -= 1;

            Response.Cookies.Add(new HttpCookie("selectedusertype", tempInt.ToString()));

            switch (userType)
            {
                case "1":
                    Response.Redirect("edituser.aspx?userid=" + e.CommandArgument.ToString().Trim() + "&usertype=" + userType);
                    break;
                case "2":
                    Response.Redirect("aevendor.aspx?t=1&vid=" + e.CommandArgument.ToString().Trim());
                    break;
                case "3":
                    Response.Redirect("editpurchasing.aspx?userid=" + e.CommandArgument.ToString().Trim() + "&usertype=" + userType);
                    break;
                case "5":
                    Response.Redirect("editboc.aspx?userid=" + e.CommandArgument.ToString().Trim() + "&usertype=" + userType);
                    break;
            }
        }
        else if (e.CommandName.Equals("DeleteUser"))
        {
            string userType = GetUserType(e.CommandArgument.ToString().Trim()).ToString().Trim();

            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@UserID", SqlDbType.Int);
            sqlParams[1] = new SqlParameter("@UserType", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(e.CommandArgument.ToString().Trim());
            sqlParams[1].Value = Int32.Parse(userType);

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_DeleteUser", sqlParams);

            int tempInt = Int32.Parse(userType);
            tempInt -= 1;
            Response.Cookies.Add(new HttpCookie("selectedusertype", tempInt.ToString()));

            Server.Transfer("searchuser.aspx");
        }
        else if (e.CommandName.Equals("Activate"))
        {
            string userType = GetUserType(e.CommandArgument.ToString().Trim()).ToString().Trim();

            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@userID", SqlDbType.Int);
            sqlParams[1] = new SqlParameter("@userType", SqlDbType.Int);
            sqlParams[2] = new SqlParameter("@status", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(e.CommandArgument.ToString().Trim());
            sqlParams[1].Value = Int32.Parse(userType);
            sqlParams[2].Value = 1;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_ChangeUserStatus", sqlParams);

            int tempInt = Int32.Parse(userType);
            tempInt -= 1;

            Response.Cookies.Add(new HttpCookie("selectedusertype", tempInt.ToString()));
            Server.Transfer("searchuser.aspx");
        }
        else if (e.CommandName.Equals("Deactivate"))
        {
            string userType = GetUserType(e.CommandArgument.ToString().Trim()).ToString().Trim();

            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@userID", SqlDbType.Int);
            sqlParams[1] = new SqlParameter("@userType", SqlDbType.Int);
            sqlParams[2] = new SqlParameter("@status", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(e.CommandArgument.ToString().Trim());
            sqlParams[1].Value = Int32.Parse(userType);
            sqlParams[2].Value = 0;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_ChangeUserStatus", sqlParams);

            int tempInt = Int32.Parse(userType);
            tempInt -= 1;

            Response.Cookies.Add(new HttpCookie("selectedusertype", tempInt.ToString()));
            Server.Transfer("searchuser.aspx");
        }
    }
}

