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

public partial class admin_addbac : System.Web.UI.Page
{
    private string connstring;

    protected void Page_Load(object sender, EventArgs e)
    {
        connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();

        if (!Page.IsPostBack)
        {

            ddlUserTypes.SelectedIndex = 5;
        }
    }

    private int SaveBAC()
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;
        int value = 0;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[14];
            sqlParams[0] = new SqlParameter("@UserName", SqlDbType.VarChar);
            sqlParams[1] = new SqlParameter("@Password", SqlDbType.VarChar);
            sqlParams[2] = new SqlParameter("@FirstName", SqlDbType.VarChar);
            sqlParams[3] = new SqlParameter("@LastName", SqlDbType.VarChar);
            sqlParams[4] = new SqlParameter("@MidName", SqlDbType.VarChar);
            sqlParams[5] = new SqlParameter("@EmailAdd", SqlDbType.VarChar);
            sqlParams[6] = new SqlParameter("@TempPwd", SqlDbType.VarChar);
            sqlParams[7] = new SqlParameter("@ApprovingLimitOnLowestPrice", SqlDbType.Float);
            sqlParams[8] = new SqlParameter("@ApprovingLimitOnLowestPriceTo", SqlDbType.Float);
            sqlParams[9] = new SqlParameter("@ApprovingLimitOnNonLowestPrice", SqlDbType.Float);
            sqlParams[10]= new SqlParameter("@ApprovingLimitOnNonLowestPriceTo", SqlDbType.Float);
            sqlParams[11]= new SqlParameter("@Committee", SqlDbType.Int);
            sqlParams[12] = new SqlParameter("@Approver", SqlDbType.Int);
            sqlParams[13] = new SqlParameter("@Clientid", SqlDbType.Int);

            string randomPwd = RandomPasswordGenerator.GenerateRandomPassword();

            sqlParams[0].Value = tbUserName.Text.Trim();
            sqlParams[1].Value = EncryptionHelper.Encrypt(randomPwd);
            sqlParams[2].Value = tbFirstName.Text.Trim();
            sqlParams[3].Value = tbLastName.Text.Trim();
            sqlParams[4].Value = tbMidInitial.Text.Trim();
            sqlParams[5].Value = tbEmail.Text.Trim();
            sqlParams[6].Value = EncryptionHelper.Encrypt(randomPwd);
            sqlParams[7].Value = ApprovingLimitOnLowestPrice.Text.Trim();
            sqlParams[8].Value = ApprovingLimitOnLowestPriceTo.Text.Trim();
            sqlParams[9].Value = ApprovingLimitOnNonLowestPrice.Text.Trim();
            sqlParams[10].Value= ApprovingLimitOnNonLowestPriceTo.Text.Trim();
            sqlParams[11].Value= Committee.Text.Trim();
            sqlParams[12].Value = Approver.Text.Trim();
            sqlParams[13].Value = Int32.Parse(HttpContext.Current.Session["clientid"].ToString());
            //Response.Write(sqlParams[10].Value);

            value = Convert.ToInt32(SqlHelper.ExecuteScalar(sqlTransact, "sp_AddNewBAC", sqlParams));
            Response.Write(value);

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
        lblUserError.Visible = false;

        if (Page.IsValid)
        {
            int result = SaveBAC();

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
                lblUserError.Text = "* Failed to add bac";
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
        else if (ddlUserTypes.SelectedIndex == 5)
        {
            Response.Redirect("addbac.aspx");
        }
    }

    #region Email
    private bool SendEmail(int userid)
    {
        bool success = false;
        string fullname = tbFirstName.Text.Trim() + " " + tbLastName.Text.Trim();

        string subject = "Trans-Asias : User Profile Created";

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
        sb.Append("Your profile has been successfully created.");
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
