using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EBid.lib;
using EBid.lib.constant;
using EBid.lib.utils;

public partial class admin_editPurchasing : System.Web.UI.Page
{
    private string connstring;

    protected void Page_Load(object sender, EventArgs e)
    {
        connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();

        if (!(IsPostBack))
        {            
            if ((Request.QueryString["userID"] != null) && (Request.QueryString["userType"] != null))
            {
                ViewState["sUserID"] = Request.QueryString["userID"];
                ViewState["sUserType"] = Request.QueryString["userType"];

                FillTextboxData();
                FillPurchasingDetails();
            }
            else
                Response.Redirect("users.aspx");
        }
    }

    private void FillTextboxData()
    {
        SqlParameter[] sqlParams = new SqlParameter[2];
        sqlParams[0] = new SqlParameter("@userID", SqlDbType.Int);
        sqlParams[1] = new SqlParameter("@userType", SqlDbType.Int);
        sqlParams[0].Value = Int32.Parse(ViewState["sUserID"].ToString().Trim());
        sqlParams[1].Value = Int32.Parse(ViewState["sUserType"].ToString().Trim());

        DataTable dtUserData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_ViewSpecificUser", sqlParams).Tables[0];

        if (dtUserData.Rows.Count > 0)
        {
            tbUserName.Text = dtUserData.Rows[0]["UserName"].ToString().Trim();            
            ViewState["PurchasingPwd"] = dtUserData.Rows[0]["UserPassword"].ToString().Trim();
            tbEmail.Text = dtUserData.Rows[0]["EmailAdd"].ToString().Trim();
        }
    }

    private void FillPurchasingDetails()
    {
        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@userID", SqlDbType.Int);
        sqlParams[0].Value = Int32.Parse(ViewState["sUserID"].ToString().Trim());

        DataTable dtPurchasingDetails = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetPurchasingDetails", sqlParams).Tables[0];

        if (dtPurchasingDetails.Rows.Count > 0)
        {
            tbFirstName.Text = dtPurchasingDetails.Rows[0]["FirstName"].ToString().Trim();
            tbLastName.Text = dtPurchasingDetails.Rows[0]["LastName"].ToString().Trim();
            tbMidInitial.Text = dtPurchasingDetails.Rows[0]["MiddleName"].ToString().Trim();

            try
            {
                int tempInt = Int32.Parse(dtPurchasingDetails.Rows[0]["DeptID"].ToString().Trim());
                tempInt -= 1;
                rbDepartments.SelectedIndex = tempInt;
            }
            catch
            {
                rbDepartments.SelectedIndex = 0;
            }
        }
    }

    private bool UpdatePurchasingDetails()
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;
        bool success = false;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@userId", SqlDbType.Int);
            sqlParams[1] = new SqlParameter("@firstName", SqlDbType.VarChar);
            sqlParams[2] = new SqlParameter("@lastName", SqlDbType.VarChar);
            sqlParams[3] = new SqlParameter("@midName", SqlDbType.VarChar);
            sqlParams[4] = new SqlParameter("@deptId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(ViewState["sUserID"].ToString().Trim());
            sqlParams[1].Value = tbFirstName.Text.Trim();
            sqlParams[2].Value = tbLastName.Text.Trim();
            sqlParams[3].Value = tbMidInitial.Text.Trim();
            sqlParams[4].Value = Int32.Parse(rbDepartments.SelectedValue);

            SqlHelper.ExecuteNonQuery(sqlTransact, "s3p_EBid_UpdatePurchasingDetails", sqlParams);

            sqlTransact.Commit();

            success = true;
        }
        catch
        {
            sqlTransact.Rollback();
            success = false;
        }
        finally
        {
            sqlConnect.Close();
        }

        return success;
    }

    private bool UpdateUserData()
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;
        bool success = false;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@userID", SqlDbType.Int);
            sqlParams[1] = new SqlParameter("@userType", SqlDbType.Int);
            sqlParams[2] = new SqlParameter("@userName", SqlDbType.Int);
            sqlParams[3] = new SqlParameter("@password", SqlDbType.Int);
            sqlParams[4] = new SqlParameter("@email", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(ViewState["sUserID"].ToString().Trim());
            sqlParams[1].Value = Int32.Parse(ViewState["sUserType"].ToString().Trim());
            sqlParams[2].Value = tbUserName.Text.Trim();
            sqlParams[3].Value = ViewState["PurchasingPwd"].ToString();
            sqlParams[4].Value = tbEmail.Text.Trim();

            int tempInt = Convert.ToInt32(SqlHelper.ExecuteScalar(sqlTransact, "s3p_EBid_UpdateSpecificUser", sqlParams));

            sqlTransact.Commit();

            if (tempInt == 0)
            {
                success = true;
                lblUsrError.Visible = false;
            }
            else
            {
                success = false;
                lblUsrError.Visible = true;
                lblUsrError.Text = "* Username already exists.";
            }
        }
        catch
        {
            sqlTransact.Rollback();
            success = false;
        }
        finally
        {
            sqlConnect.Close();
        }

        if (success)
            success = UpdatePurchasingDetails();

        return success;
    }

    protected void lnkSave_Click(object sender, EventArgs e)
    {
        lblUsrError.Visible = false;

        if (Page.IsValid)
        {
            if (UpdateUserData())
            {
                SendEmail();
                Response.Redirect("users.aspx");
            }            
        }
    }

    #region Email
    private bool SendEmail()
    {
        bool success = false;
        string fullname = tbFirstName.Text.Trim() + " " + tbLastName.Text.Trim();

        string subject = "Trans-Asias : User Profile Changed";

        if (MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                MailHelper.ChangeToFriendlyName(ConfigurationManager.AppSettings["AdminEmailName"], ConfigurationManager.AppSettings["AdminEmailAddress"]),
                MailHelper.ChangeToFriendlyName(fullname, tbEmail.Text.Trim()),
                subject,
                CreateBody(),
                MailTemplate.GetTemplateLinkedResources(this)))
            success = true;

        return success;
    }

    private string CreateBody()
    {
        StringBuilder sb = new StringBuilder();
                
        sb.Append("<tr>");
        sb.Append("<td valign='top'>");
        sb.Append("<p>");
        sb.Append("<br /><b>" + DateTime.Now.ToLongDateString() + "</b><br />");
        sb.Append("<b>TO&nbsp&nbsp;:&nbsp&nbsp;<u>" + tbFirstName.Text.Trim() + " " + tbLastName.Text.Trim() + "</u></b>");
        sb.Append("<br /><br /><br />");
        sb.Append("Good Day!");
        sb.Append("<br /><br />");
        sb.Append("Your profile has been successfully changed.");
        sb.Append("<br />&nbsp;&nbsp;&nbsp;Username: " + tbUserName.Text.Trim());
        sb.Append("<br />&nbsp;&nbsp;&nbsp;Password: " + HttpUtility.HtmlEncode(EncryptionHelper.Decrypt(GetUserPassword(Int32.Parse(ViewState["sUserID"].ToString().Trim())))));
        sb.Append("<br /><br /><br />");
        sb.Append("Sincerely Yours,");
        sb.Append("<br /><br />");
        sb.Append(ConfigurationManager.AppSettings["AdminEmailName"]);
        sb.Append("<br /><br />");
        sb.Append("</p>");
        sb.Append("</td>");
        sb.Append("</tr>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }

    public string GetUserPassword(int userid)
    {
        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@UserID", SqlDbType.Int);
        sqlParams[0].Value = userid;

        string s = (string)SqlHelper.ExecuteScalar(connstring, "s3p_EBid_GetUserPassword", sqlParams);

        return s;
    }
    #endregion
}
