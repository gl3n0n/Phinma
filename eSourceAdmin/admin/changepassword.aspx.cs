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

public partial class admin_changePassword : System.Web.UI.Page
{
    private static string connstring;

    protected void Page_Load(object sender, EventArgs e)
    {
        connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (!IsPostBack)
        {
            tbPassword.Attributes.Add("onkeypress", "return NoSpaces(event);");
            tbNewPassword.Attributes.Add("onkeypress", "return NoSpaces(event);");
            tbConfNewPassword.Attributes.Add("onkeypress", "return NoSpaces(event);");            
        }
        CustomValidator1.ErrorMessage = "<br />» Password should be within " +
                ConfigurationManager.AppSettings["MinimumPasswordSize"] + " to " +
                ConfigurationManager.AppSettings["MaximumPasswordSize"] + " characters." +
                "<br />» Password should contain atleast (1)one number and (1)one symbol.";
    }

    private string GetPassword()
    {
        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@userID", SqlDbType.Int);
		sqlParams[0].Value = Int32.Parse(Session[Constant.SESSION_USERID].ToString().Trim());

        return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetUserPassword", sqlParams).ToString().Trim();
    }

    private int UpdatePassword(int userID, string newPassword)
    {
        SqlParameter[] sqlParams = new SqlParameter[3];
        sqlParams[0] = new SqlParameter("@userID", SqlDbType.Int);
        sqlParams[1] = new SqlParameter("@password", SqlDbType.VarChar);
        sqlParams[2] = new SqlParameter("@UserName", SqlDbType.VarChar);
		sqlParams[0].Value = userID;
        sqlParams[1].Value = newPassword;
        sqlParams[2].Value = Session[Constant.SESSION_USERNAME].ToString();

        return SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_UpdateUserPassword", sqlParams);
    }

    protected void lnkSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            return;

		int userid = Int32.Parse(Session[Constant.SESSION_USERID].ToString());
		// encrypted,current, correct password
		string pwd = Session[Constant.SESSION_PASSWORD].ToString();
		
		// inputed values
		string currentpwd = tbPassword.Text.Trim();
		string newpwd = tbNewPassword.Text.Trim();
		string confirmnewpwd = tbConfNewPassword.Text.Trim();

        lblError.Visible = false;
        if ((currentpwd.Length > 0) && (newpwd.Length > 0) && (confirmnewpwd.Length > 0))
        {
            lblError.Visible = true;
            if (UpdatePassword(userid, EncryptionHelper.Encrypt(newpwd)) > 0)
            {
                Session[Constant.SESSION_PASSWORD] = EncryptionHelper.Encrypt(newpwd);
                lblError.Text = "Password successfully changed.";
            }
        }
        else
        {
            lblError.Text = "Please input required fields";
            lblError.Visible = false;
        }
    }

    protected void CheckPasswordLength(object source, ServerValidateEventArgs args)
    {
        string password = args.Value.Trim();
        bool isLengthOK = false;

        isLengthOK = ((password.Length >= int.Parse(ConfigurationManager.AppSettings["MinimumPasswordSize"])) &&
            (password.Length <= int.Parse(ConfigurationManager.AppSettings["MaximumPasswordSize"]))) ? true : false;
               
        args.IsValid = isLengthOK && PasswordChecker.IsStrongPassword(password);
    }

    protected void CheckPassword(object source, ServerValidateEventArgs args)
    {
        string currentpwd = args.Value.Trim();

        args.IsValid = (EncryptionHelper.Encrypt(currentpwd) == Session[Constant.SESSION_PASSWORD].ToString().Trim());
    }
}