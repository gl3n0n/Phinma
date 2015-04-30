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


public partial class admin_vendorBlacklisting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
    }

    protected void gvUsersRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("blacklist"))
        {
            string connstring = HttpContext.Current.Session["ConnectionString"].ToString();

            //HiddenField hdStatus = (HiddenField)e.Row.FindControl("hdStatus");

            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@vendorID", SqlDbType.Int);
            sqlParams[1] = new SqlParameter("@blackList", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(e.CommandArgument.ToString().Trim());
            sqlParams[1].Value = 0;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_UpdateBlackListStatus", sqlParams);

            Response.Redirect("vendorBlacklisting.aspx");
        }
        else if (e.CommandName.Equals("restore"))
        {
            string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
                        
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@vendorID", SqlDbType.Int);
            sqlParams[1] = new SqlParameter("@blackList", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(e.CommandArgument.ToString().Trim());
            sqlParams[1].Value = 1;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_UpdateBlackListStatus", sqlParams);

            Response.Redirect("vendorBlacklisting.aspx");
        }
    }

    protected void gvRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdStatus = (HiddenField)e.Row.FindControl("hdStatus");
            LinkButton lnkOption = (LinkButton)e.Row.FindControl("lnkOption");
            Label lblStatus = (Label)e.Row.FindControl("lblStatus");

            if(hdStatus.Value == "0")
            {
                lnkOption.Text = "Blacklist";
                lnkOption.CommandName = "blacklist";
                lblStatus.ForeColor = System.Drawing.Color.Black;
                lnkOption.OnClientClick = "return confirm('Are you sure you want to blacklist this user?')";
            }
            else
            {
                lnkOption.Text = "Remove Blacklist";
                lnkOption.CommandName = "restore";
                lblStatus.ForeColor = System.Drawing.Color.Red;
                lnkOption.OnClientClick = "return confirm('Are you sure you want to remove this user from blacklist?')";
            }
        }
    }
}
