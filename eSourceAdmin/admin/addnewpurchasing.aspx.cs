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
using System.Diagnostics;

public partial class admin_addNewPurchasing : System.Web.UI.Page
{
    private string connstring;

    protected void Page_Load(object sender, EventArgs e)
    {
        connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();

        if (!Page.IsPostBack)
        {
            ddlUserTypes.SelectedIndex = 2;
            rbDepartments.SelectedIndex = 0;
        }
    }

    private int SavePurchasing()
    {
        string query;
        SqlCommand cmd;
        SqlConnection conn;
        SqlTransaction sqlTransact = null;
        int value = 0;
        string randomPwd = RandomPasswordGenerator.GenerateRandomPassword();

        try
        {
            query = "sp_AddNewPurchasing"; //##storedProcedure
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                    cmd.Parameters.AddWithValue("@Username", tbUserName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", EncryptionHelper.Encrypt(randomPwd));
                    cmd.Parameters.AddWithValue("@FirstName", tbFirstName.Text.Trim());
                    cmd.Parameters.AddWithValue("@LastName", tbLastName.Text.Trim());
                    cmd.Parameters.AddWithValue("@MidName", tbMidInitial.Text.Trim());
                    cmd.Parameters.AddWithValue("@DeptId", Int32.Parse(rbDepartments.SelectedValue.Trim()));
                    cmd.Parameters.AddWithValue("@EmailAdd", tbEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@TempPwd", EncryptionHelper.Encrypt(randomPwd));
                    cmd.Parameters.AddWithValue("@ClientId", Int32.Parse(HttpContext.Current.Session["clientid"].ToString()));
                    conn.Open();
                    sqlTransact = conn.BeginTransaction();
                    sqlTransact.Commit();
                    value = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                }
            }
            
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);
            sqlTransact.Rollback();
            value = 0;
            LogHelper.EventLogHelper.Log("Admin > Add User : " + ex.Message, EventLogEntryType.Error);
        }
        finally
        {
            //conn.Close();
        }

        return value;
    }

    protected void lnkSave_Click(object sender, EventArgs e)
    {
        lblUserError.Visible = false;
        
        if (Page.IsValid)
        {
            int result = SavePurchasing();

            if (result != 0)
            {
                if (result == -1)
                {
                    lblUserError.Visible = true;
                }
                else
                {
                    if (SendEmailInvitation(result))
                        Session["Message"] = "An e-mail has been sent to the new user.";

                    Response.Redirect("users.aspx");
                }
            }        
        }
    }
    
    #region E-mail Creation
    private bool SendEmailInvitation(int userid)
    {
        bool success = false;
        
        string subject = "Trans-Asias : New User Profile Created";

        if (MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                MailHelper.ChangeToFriendlyName(ConfigurationManager.AppSettings["AdminEmailName"], ConfigurationManager.AppSettings["AdminEmailAddress"]),
                MailHelper.ChangeToFriendlyName(tbFirstName.Text.Trim() + " " + tbLastName.Text.Trim(), tbEmail.Text.Trim()),
                subject,
                CreateInvitationBody(userid),
                MailTemplate.GetTemplateLinkedResources(this)))
            success = true;

        return success;
    }

    private string CreateInvitationBody(int userid)
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
        sb.Append("Your new profile has been successfully created.");
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
}
