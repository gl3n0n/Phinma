using System;
using System.Data;
using System.Web;
using System.Configuration;
using System.Web.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ava;
using Ava.lib.utils;
using System.Data.Sql;
using System.Data.SqlClient;
using Ava.lib;

public partial class forgotPwd : System.Web.UI.Page
{
    private string connString = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnForgotPwd_Click(object sender, EventArgs e)
    {
        DataTable dt;
        SqlParameter[] sqlparams = new SqlParameter[1];
        sqlparams[0] = new SqlParameter("@UserName", SqlDbType.NVarChar, 100);
        sqlparams[0].Value = txtUserName.Text.Trim();

        try
        {
            dt = SqlHelper.ExecuteDataset(connString, "sp_GetUserPasswordAndEmail", sqlparams).Tables[0];

            string Pwd = dt.Rows[0]["Password"].ToString();
            string EmailAdd = dt.Rows[0]["EmailAddress"].ToString();

            if (Pwd.Equals("NONE") || EmailAdd.Equals("NONE"))
            {
                txtNote.Text = "Username not found.";
            }
            else
            {
                string from = MailHelper.ChangeToFriendlyName(ConfigurationManager.AppSettings["AdminEmailName"], ConfigurationManager.AppSettings["AdminEmailAddress"]);
                string to = EmailAdd;

                string subject = "Globe Telecom Vendor Accreditation : Password Request";
                try
                {
                    if (MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(), from, to, subject, MailTemplate.IntegrateBodyIntoTemplate(CreateRequestPasswordBody(Pwd)), MailTemplate.GetTemplateLinkedResources(this)))
                    {
                        txtNote.Text = "Password sent!";
                    }
                    else
                    {
                        txtNote.Text = "Password not sent this time.";
                    }
                }
                catch
                {
                    txtNote.Text = "Password not sent this time.";
                }
                txtUserName.Text = "";
            }
        }
        catch (Exception ex)
        {
            txtNote.Text = ex.Message.ToString();
        }
    }

    private string CreateRequestPasswordBody(string password)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        sb.Append("<tr><td align='center'><h3>Password Request</h3></td></tr>");
        sb.Append("<tr><td height='100%' valign='top'><p>");
        sb.Append("Your password is <b>" + EncryptionHelper.Decrypt(password) + "</b>.<br />");
        sb.Append("<font color='red'>NOTE: Password is case sensitive.</font><br />");
        sb.Append("</p></td></tr>");

        return sb.ToString();
    }
}