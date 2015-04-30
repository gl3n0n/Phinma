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
using System.Text;
using EBid.lib;
using EBid.lib.constant;
using EBid.lib.utils;
using EBid.ConnectionString;
using EBid.Configurations;

public partial class admin_addadmin : System.Web.UI.Page
{
    private string connstring = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        connstring = HttpContext.Current.Session["ConnectionString"].ToString();

        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();

        if (!Page.IsPostBack)
        {
            ddlUserTypes.SelectedIndex = 3;
            //rbCommittee.SelectedIndex = -1;
            if ((Request.QueryString["userID"] != null) && (Request.QueryString["userType"] != null))
            {
                ViewState["sUserID"] = Request.QueryString["userID"];
                ViewState["sUserType"] = Request.QueryString["userType"];
                lblTitle.Text = "Edit Profile";
                FillTextboxData();
                FillAdminDetails();
                lnkSave.Text = "Update";
                lnkSave.OnClientClick = "return confirm('Save details?');";
            }
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
            ViewState["AdminPwd"] = dtUserData.Rows[0]["UserPassword"].ToString().Trim();
            tbEmail.Text = dtUserData.Rows[0]["EmailAdd"].ToString().Trim();
        }
    }

    private void FillAdminDetails()
    {
        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@userID", SqlDbType.Int);
        sqlParams[0].Value = Int32.Parse(ViewState["sUserID"].ToString().Trim());

        DataTable dtBOCDetails = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "sp_QueryAdminDetails", sqlParams).Tables[0];

        if (dtBOCDetails.Rows.Count > 0)
        {
            tbUserName.Text = dtBOCDetails.Rows[0]["UserName"].ToString().Trim();
            tbFirstName.Text = dtBOCDetails.Rows[0]["FirstName"].ToString().Trim();
            tbLastName.Text = dtBOCDetails.Rows[0]["LastName"].ToString().Trim();
            tbMidInitial.Text = dtBOCDetails.Rows[0]["MiddleName"].ToString().Trim();
            tbEmail.Text = dtBOCDetails.Rows[0]["EmailAdd"].ToString().Trim();
            //rbCommittee.SelectedValue = dtBOCDetails.Rows[0]["CommitteeId"].ToString().Trim();

        }
    }

    private bool UpdateAdminDetails()
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;
        bool success = false;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@UserId", SqlDbType.Int);
            sqlParams[1] = new SqlParameter("@FirstName", SqlDbType.VarChar);
            sqlParams[2] = new SqlParameter("@LastName", SqlDbType.VarChar);
            sqlParams[3] = new SqlParameter("@MidName", SqlDbType.VarChar);
            //sqlParams[4] = new SqlParameter("@CommitteeId", SqlDbType.Int);

            sqlParams[0].Value = Int32.Parse(ViewState["sUserID"].ToString().Trim());
            sqlParams[1].Value = tbFirstName.Text.Trim();
            sqlParams[2].Value = tbLastName.Text.Trim();
            sqlParams[3].Value = tbMidInitial.Text.Trim();
            //sqlParams[4].Value = Int32.Parse(rbCommittee.SelectedValue);

            SqlHelper.ExecuteNonQuery(sqlTransact, "sp_UpdateAdminDetails", sqlParams);

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
            sqlParams[3].Value = ViewState["AdminPwd"].ToString();//EncryptionHelper.Encrypt(tbPassword.Attributes["value"]);
            sqlParams[4].Value = tbEmail.Text.Trim();

            int tempInt = Convert.ToInt32(SqlHelper.ExecuteScalar(sqlTransact, "s3p_EBid_UpdateSpecificUser", sqlParams));

            sqlTransact.Commit();

            if (tempInt == 1)
            {
                success = false;
            }
            else
            {
                success = true;
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
            success = UpdateAdminDetails();

        return success;
    }

    private int SaveAdmin()
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;
        int value = 0;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[8];
            sqlParams[0] = new SqlParameter("@UserName", SqlDbType.VarChar);
            sqlParams[1] = new SqlParameter("@Password", SqlDbType.VarChar);
            sqlParams[2] = new SqlParameter("@FirstName", SqlDbType.VarChar);
            sqlParams[3] = new SqlParameter("@LastName", SqlDbType.VarChar);
            sqlParams[4] = new SqlParameter("@MidName", SqlDbType.VarChar);
            //sqlParams[5] = new SqlParameter("@CommitteeId", SqlDbType.Int);
            sqlParams[5] = new SqlParameter("@EmailAdd", SqlDbType.VarChar);
            sqlParams[6] = new SqlParameter("@TempPwd", SqlDbType.VarChar);
            sqlParams[7] = new SqlParameter("@Clientid", SqlDbType.Int);

            string randomPwd = RandomPasswordGenerator.GenerateRandomPassword();

            sqlParams[0].Value = tbUserName.Text.Trim();
            sqlParams[1].Value = EncryptionHelper.Encrypt(randomPwd);
            sqlParams[2].Value = tbFirstName.Text.Trim();
            sqlParams[3].Value = tbLastName.Text.Trim();
            sqlParams[4].Value = tbMidInitial.Text.Trim();
            //sqlParams[5].Value = Int32.Parse(rbCommittee.SelectedValue.Trim());
            sqlParams[5].Value = tbEmail.Text.Trim();
            sqlParams[6].Value = EncryptionHelper.Encrypt(randomPwd);
            sqlParams[7].Value = Int32.Parse(HttpContext.Current.Session["clientid"].ToString());

            value = Convert.ToInt32(SqlHelper.ExecuteScalar(sqlTransact, "sp_AddNewAdmin", sqlParams));

            sqlTransact.Commit();
        }
        catch
        {
            sqlTransact.Rollback();
            value = 0;
        }
        finally
        {
            sqlConnect.Close();
        }

        return value;
    }

    protected void lnkSave_Click(object sender, EventArgs e)
    {
        if (lnkSave.Text == "Update")
        {
            if (Page.IsValid)
            {
                if (UpdateUserData())
                {
                    SendEmail(Int32.Parse(ViewState["sUserID"].ToString().Trim()));
                    Response.Redirect("users.aspx");
                }
                else
                {
                    lblUserError.Visible = true;
                    lblUserError.Text = "* Username Already Exists";
                }
            }
        }
        else
        {
            lblUserError.Visible = false;

            if (Page.IsValid)
            {
                int result = SaveAdmin();

                if (result != 0)
                {
                    if (result == -1)
                    {
                        lblUserError.Visible = true;
                        lblUserError.Text = "* Username Already Exists";
                    }
                    else
                    {
                        SendEmail(result);
                        Response.Redirect("users.aspx");
                    }
                }
                else
                {
                    lblUserError.Visible = true;
                    lblUserError.Text = "* Failed to add administrator";
                }
            }
        }
    }

    protected void ddlUserTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Cookies.Add(new HttpCookie("selectedusertype", ddlUserTypes.SelectedIndex.ToString().Trim()));

        if (ddlUserTypes.SelectedIndex == 0)
        {
            Response.Redirect("adduser.aspx");
        }
        else if (ddlUserTypes.SelectedIndex == 1)
        {
            Response.Redirect("aevendor.aspx");
        }
        else if (ddlUserTypes.SelectedIndex == 2)
        {
            Response.Redirect("addnewpurchasing.aspx");
        }
        else if (ddlUserTypes.SelectedIndex == 3)
        {
            Response.Redirect("addadmin.aspx");
        }
        else if (ddlUserTypes.SelectedIndex == 4)
        {
            Response.Redirect("addboc.aspx");
        }
        {
            Response.Redirect("addbac.aspx");
        }
    }

    #region Email
    private bool SendEmail(int userid)
    {
        bool success = false;
        string fullname = tbFirstName.Text.Trim() + " " + tbLastName.Text.Trim();

        string subject = "";

        if ((Request.QueryString["userID"] != null) && (Request.QueryString["userType"] != null))
        {
            subject = "Trans-Asias : User Profile Changed";
        }
        else
        {
            subject = "Trans-Asias : User Profile Created";
        }

        if (MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                MailHelper.ChangeToFriendlyName(ConfigurationManager.AppSettings["AdminEmailName"], ConfigurationManager.AppSettings["AdminEmailAddress"]),
                MailHelper.ChangeToFriendlyName(fullname, tbEmail.Text.Trim()),
                subject,
                CreateBody(userid),
                MailTemplate.GetTemplateLinkedResources(this)))
            success = true;

        return success;
    }

    private string CreateBody(int userid)
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
        if ((Request.QueryString["userID"] != null) && (Request.QueryString["userType"] != null))
        {
            sb.Append("Your profile has been successfully changed.");
        }
        else
        {
            sb.Append("Your profile has been successfully created.");
        }
        sb.Append("<br />&nbsp;&nbsp;&nbsp;Username: " + tbUserName.Text.Trim());
        sb.Append("<br />&nbsp;&nbsp;&nbsp;Password: " + HttpUtility.HtmlEncode(EncryptionHelper.Decrypt(GetUserPassword(userid))));
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
