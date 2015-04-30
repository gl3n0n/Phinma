using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Configuration;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using EBid.lib.utils;
using EBid.lib.user.trans;
using EBid.lib.constant;
using EBid.lib;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Net.Mail;
using EBid;
using EBid.ConnectionString;
using EBid.Configurations;
using EBid.libs;

public partial class login : System.Web.UI.Page
{
    string connstring;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Write("<span>" + EncryptionHelper.Decrypt("2f9YfrY41yIPUjLY2dKVlKH+ZzbdkbVRrRolu9ejINY=") + "</span><br>");
        //Response.Write("<span>" + EncryptionHelper.Decrypt("38Wda9U8nuBfgyMx4ZVYzaufUUlz1/Z4ymX72tsBzxa2iHrTRELdTblJVOT06z6T") + "</span><br>");
        //Response.Write("<span>" + EncryptionHelper.Decrypt("vicpnTw0Pilb6TNu07q1Vc3DQC4AySM3TJshBETh0vw=") + "</span><br>");
        //Response.Write("<span>" + EncryptionHelper.Decrypt("fACwYAMPVyTb2NVhH8FuoapG2QJEJilvHXPAXbuu03g=") + "</span><br>");

        if (Request.QueryString["ReturnUrl"] != null)
        {
            Response.Redirect("login.aspx");
        }
        

        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(User.Identity.Name))
            {
                FormsAuthenticationHelper.SignOut();
            }
        }
        else 
        {
            if (HttpContext.Current.Session["clientid"] != null)
            {
                connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
            }
            else
            {
                connstring = ConfigurationManager.ConnectionStrings["EBidConnectionString"].ConnectionString;
            }
        }

        Title = String.Format(Constant.TITLEFORMAT, "Login");


    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        #region show/hide message
        //if (Session["msg"] != null)
        //{
        //    txtNote2.Text = Session["msg"].ToString();
        //    Session["msg"] = null;
        //}
        //else
        //{
        //    txtNote2.Text = "";
        //}
        if (HttpContext.Current.Session["clientid"] != null) 
        { 
            //LoadContents(HttpContext.Current.Session["clientid"].ToString());
        }
        #endregion
    }


	private void LoadContents(string clientid)
	{
	}


    private string GetClientId(string vUserName)
    {

        SqlDataReader oReader;
        //string connstring = ConfigurationManager.ConnectionStrings["EBidConnectionString"].ConnectionString;
        string query;
        string clientid = "";
        SqlCommand cmd;
        SqlConnection conn;

        try{
            query = "sp_GetClientId"; //##storedProcedure
            using (conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EBidConnectionString"].ConnectionString))
            {
                using (cmd = new SqlCommand(query, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@Username", vUserName.Replace("'", "''"));
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        clientid = oReader["clientid"].ToString();
                        return clientid;
                    }
                }
                else
                {
                    return "0";
                }
             } }
        }
        catch (SqlException ex)
        {
            LogHelper.EventLogHelper.Log("User Login : " + ex.Message, EventLogEntryType.Error);
            return "0";
        }  

        return clientid;
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

        SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "sp_UpdateUserLoginStatus", sqlParams);
    }

    private string GetSessionId()
    {
        SqlParameter[] sqlparams = new SqlParameter[1];
        sqlparams[0] = new SqlParameter("@UserId", SqlDbType.NVarChar);
        sqlparams[0].Value = int.Parse(Session[Constant.SESSION_USERID].ToString());

        String SessionId = (string)SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "sp_GetUserSessionID", sqlparams);

        return SessionId;
    }

	protected void btnLogin_Click(object sender, EventArgs e)
	{
		string username = txtUserName.Text.Trim();
		string password = EncryptionHelper.Encrypt(txtPassword.Text.Trim());
		//string password = EncryptionHelper.Encrypt("$january8");
        Session[Constant.SESSION_PASSWORD] = password;


        if (GetClientId(username).ToString() != "0")
        {
            Session["clientid"] = GetClientId(username).ToString();
            Session["client"] = ClientConfig.ConfigurationsId(HttpContext.Current.Session["clientid"].ToString(), "slug");
            Session["configTheme"] = ClientConfig.ConfigurationsId(HttpContext.Current.Session["clientid"].ToString(), "configTheme");
            Session["configCompanyName"] = ClientConfig.ConfigurationsId(HttpContext.Current.Session["clientid"].ToString(), "configCompanyName");
            Session["ConnectionString"] = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
            connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
            //Response.Write(Session["ConnectionString"]);

            //SqlDataReader oReader;
            ////string connstring = ConfigurationManager.ConnectionStrings["EBidConnectionString"].ConnectionString;
            //string query;
            //SqlCommand cmd;
            //SqlConnection conn;
            //query = "SELECT UserId FROM tblUsers WHERE UserName=@UserName AND UserPassword=@UserPassword"; //##storedProcedure
            //using (conn = new SqlConnection(Session["ConnectionString"].ToString()))
            //{
            //    using (cmd = new SqlCommand(query, conn))
            //    {
            //        //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
            //        cmd.Parameters.AddWithValue("@UserName", username.Replace("'", "''"));
            //        cmd.Parameters.AddWithValue("@UserPassword", password.Replace("'", "''"));
            //        conn.Open();
            //        //Process results
            //        oReader = cmd.ExecuteReader();
            //        if (oReader.HasRows)
            //        {
            //            while (oReader.Read())
            //            {
            //                Response.Write(oReader["UserId"].ToString());
            //            }
            //        }
            //    }
            //}

            //Response.Write(CheckUserCredentials(username, password, Session["ConnectionString"].ToString()));
		    // check user credentials
            if (CheckUserCredentials(username, password, Session["ConnectionString"].ToString()))
            // if ok, proceed
            {
                Session[Constant.SESSION_PASSWORD] = password;

                // check if already authenticated
                if (Session[Constant.SESSION_ISUSERAUTHENTICATED].ToString() == "YES")
                {
                    if (GetSessionId() == "")
                    {
                        UpdateUserLoginStatus(Session[Constant.SESSION_USERID].ToString(), 1, Session.SessionID.ToString());

                        Session["SesId"] = GetSessionId();

                        RedirectUser(Session[Constant.SESSION_USERNAME].ToString(), int.Parse(Session[Constant.SESSION_USERTYPE].ToString()));
                    }
                    else
                    {
                        txtNote.Text = "<div style='padding:5px; background-color:#f60; color:#fff; font-size:10px; margin-top:10px; width:166px;'>This user is currently logged-in. <br>Do you want to force login?</div>";
                        lnkForceLogout.Visible = true;
                        lnkForceLogoutNo.Visible = true;
                        lnkForceSeparator.Text = "&nbsp;&nbsp;|&nbsp;&nbsp;";
                        btnLogin.Visible = false;
                        btnClear.Visible = false;
                        //UpdateUserLoginStatus(Session[Constant.SESSION_USERID].ToString(), 1, Session.SessionID.ToString());

                        //Session["SesId"] = GetSessionId();

                        //RedirectUser(Session[Constant.SESSION_USERNAME].ToString(), int.Parse(Session[Constant.SESSION_USERTYPE].ToString()));
                    }
                }
                // if not yet authenticated
                else
                {
                    mView.ActiveViewIndex = 2;
                }
            }
            // if not, prompt incorrect username/password
            else
            {
                //txtUserName.Text = "";
                txtNote.Text = "Invalid username or password.";
            }
        }
        else
        {
            //txtUserName.Text = "";
            txtNote.Text = "Invalid username or password..";
        }
	}

    private void RedirectUser(string username, int usertype)
    {
        // Create authentication ticket/cookie
        FormsAuthenticationHelper.CreateAuthenticationTicket(username, false, usertype.ToString());

        bool isAdmin = false;
        string defaultUrl = string.Empty;

        #region get default page for user type
        switch (usertype)
        {
            case 1:
                defaultUrl = ConfigurationManager.AppSettings["BuyerHomePage"];
                break;
            case 2:
                defaultUrl = ConfigurationManager.AppSettings["VendorHomePage"];
                break;
            case 3:
                defaultUrl = ConfigurationManager.AppSettings["PurchasingHomePage"];
                break;
            case 5:
                defaultUrl = ConfigurationManager.AppSettings["BidsOpeningCommitteeHomePage"];
                break;
			case 6:
                defaultUrl = ConfigurationManager.AppSettings["BidsAwardingCommitteeHomePage"];
                break;
            default:
                txtNote.Text = "Invalid username or password.";
                isAdmin = true;
                break;
        }
        #endregion

        #region redirect user
        if (!isAdmin)
        {
            if (String.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
            {
                Response.Redirect(defaultUrl);
            }
            else
            {
                // Verify if user is authorized to go to the redirection url
                // if not, redirect to login page	
                string redirectUrl = FormsAuthentication.GetRedirectUrl(username, true);

                switch (usertype)
                {
                    // buyer
                    case 1:
                        {
                            if ((redirectUrl.Contains("boc")) || (redirectUrl.Contains("vendorscreens")) || (redirectUrl.Contains("purchasingscreens")))
                                redirectUrl = defaultUrl;

                            LogHelper.EventLogHelper.Log("User Login : " + username, EventLogEntryType.Information);
                            Response.Redirect(redirectUrl);
                        } break;
                    // vendor
                    case 2:
                        {
                            if ((redirectUrl.Contains("buyerscreens")) || (redirectUrl.Contains("boc")) || (redirectUrl.Contains("purchasingscreens")))
                                redirectUrl = defaultUrl;

                            LogHelper.EventLogHelper.Log("User Login : " + username, EventLogEntryType.Information);
                            Response.Redirect(redirectUrl);
                        } break;
                    // purchasing
                    case 3:
                        {
                            if ((redirectUrl.Contains("buyerscreens")) || (redirectUrl.Contains("vendorscreens")) || (redirectUrl.Contains("boc")))
                                redirectUrl = defaultUrl;

                            LogHelper.EventLogHelper.Log("User Login : " + username, EventLogEntryType.Information);
                            Response.Redirect(redirectUrl);
                        } break;
                    // bid opening committee
                    case 5:
                        {
                            if ((redirectUrl.Contains("buyerscreens")) || (redirectUrl.Contains("vendorscreens")) || (redirectUrl.Contains("purchasingscreens")))
                                redirectUrl = defaultUrl;

                            LogHelper.EventLogHelper.Log("User Login : " + username, EventLogEntryType.Information);
                            Response.Redirect(redirectUrl);
                        } break;
					case 6:
                        {
                            if ((redirectUrl.Contains("buyerscreens")) || (redirectUrl.Contains("vendorscreens")) || (redirectUrl.Contains("bac")))
                                redirectUrl = defaultUrl;

                            LogHelper.EventLogHelper.Log("User Login : " + username, EventLogEntryType.Information);
                            Response.Redirect(redirectUrl);
                        } break;
                    default:
                        //txtUserName.Text = "";
                        txtNote.Text = "Invalid username or password.";
                        break;
                }
            }
        }
        else
        {
            //txtUserName.Text = "";
            txtNote.Text = "Invalid username or password.";
        }
        #endregion
    }
        
    private bool CheckUserCredentials(string username, string password, string connstring)
    {
        DataSet ds = new DataSet();

        if ((!String.IsNullOrEmpty(username)) && (!String.IsNullOrEmpty(password)))
        {
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@Username", SqlDbType.NVarChar);
            sqlparams[0].Value = username.Replace("'", "''");
            sqlparams[1] = new SqlParameter("@Password", SqlDbType.NVarChar);
            sqlparams[1].Value = password.Replace("'", "''");

            try
            {                
                //userid = (int)SqlHelper.ExecuteScalar(connstring, "sp_GetUserId", sqlparams);
                ds = SqlHelper.ExecuteDataset(connstring, "sp_GetUserLoginDetails", sqlparams);
                                
                // check if username exists, check password
                if (ds.Tables[0].Rows[0]["UserId"].ToString() != "0")
                {
                    Session[Constant.SESSION_ISUSERAUTHENTICATED] = ds.Tables[0].Rows[0]["IsAuthenticated"].ToString();

                    // if user is already authenticated, then fill session variables
                    if (Session[Constant.SESSION_ISUSERAUTHENTICATED].ToString() == "YES")
                    {                        
                        Session[Constant.SESSION_USERID] = ds.Tables[0].Rows[0]["UserId"].ToString();
                        Session[Constant.SESSION_USERNAME] = username;
                        Session[Constant.SESSION_USERTYPE] = ds.Tables[0].Rows[0]["UserType"].ToString();
                        Session[Constant.SESSION_USEREMAIL] = ds.Tables[0].Rows[0]["UserEmail"].ToString();
                        Session[Constant.SESSION_USERFULLNAME] = ds.Tables[0].Rows[0]["UserFullName"].ToString();
                    }
                    // if not, do not put anything yet.. force user to change his password
                    else
                    {
                        ViewState[Constant.SESSION_USERID] = ds.Tables[0].Rows[0]["UserId"].ToString();
                        ViewState[Constant.SESSION_USERNAME] = username;
                        ViewState[Constant.SESSION_USERTYPE] = ds.Tables[0].Rows[0]["UserType"].ToString();
                        ViewState[Constant.SESSION_USEREMAIL] = ds.Tables[0].Rows[0]["UserEmail"].ToString();
                        ViewState[Constant.SESSION_USERFULLNAME] = ds.Tables[0].Rows[0]["UserFullName"].ToString();
                    }

                    // if yes, return true
                    return true;
                }
                // if no, return false
                else
                    return false;
            }
            catch (SqlException ex)
            {
                LogHelper.EventLogHelper.Log("User Login : " + ex.Message, EventLogEntryType.Error);
                return false;
            }            
        }
        else
            return false;
    }

    protected void btnContinue_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            int userid, usertype;
            string username = ViewState[Constant.SESSION_USERNAME].ToString();
            
            int.TryParse(ViewState[Constant.SESSION_USERID].ToString(), out userid);
            int.TryParse(ViewState[Constant.SESSION_USERTYPE].ToString(), out usertype);

            // change password
            if (UserTransaction.ChangePasswordAndAuthenticate(connstring, userid, EncryptionHelper.Encrypt(txtConfirmPwd.Text.Trim()), true, username))
            {
                // store session values
                Session[Constant.SESSION_USERID] = ViewState[Constant.SESSION_USERID];
                Session[Constant.SESSION_USERNAME] = ViewState[Constant.SESSION_USERNAME];
                Session[Constant.SESSION_USERTYPE] = ViewState[Constant.SESSION_USERTYPE];
                Session[Constant.SESSION_USEREMAIL] = ViewState[Constant.SESSION_USEREMAIL];
                Session[Constant.SESSION_USERFULLNAME] = ViewState[Constant.SESSION_USERFULLNAME];

                UpdateUserLoginStatus(Session[Constant.SESSION_USERID].ToString(), 1, Session.SessionID.ToString());
                Session["SesId"] = GetSessionId();
                // redirect user
                RedirectUser(Session[Constant.SESSION_USERNAME].ToString(), int.Parse(Session[Constant.SESSION_USERTYPE].ToString()));  
            }     
        }
    }    

    #region Forgot Password
    protected void lnkForgotPwd_Click(object sender, EventArgs e)
    {
        mView.ActiveViewIndex = 1;
    }

	protected void btnSend_Click(object sender, EventArgs e)
	{		
		SqlParameter[] sqlparams = new SqlParameter[1];
		sqlparams[0] = new SqlParameter("@UserName", SqlDbType.NVarChar, 100);
		sqlparams[0].Value = txtUserName2.Text.Trim();
		
		DataTable dt;

		try
        {
            SqlDataReader oReader;
            string query;
            string userPP = "", userEE = "";
            SqlCommand cmd;
            SqlConnection conn;
            query = "sp_GetUserPasswordAndEmail";
            using (conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EBidConnectionString"].ConnectionString))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", txtUserName2.Text.Trim());
                    conn.Open();
                    oReader = cmd.ExecuteReader();
                    if (oReader.HasRows)
                    {
                        while (oReader.Read())
                        {
                            userPP = oReader["Password"].ToString();
                            userEE = oReader["EmailAddress"].ToString();
                        }
                    }
                }
            }

            //dt = SqlHelper.ExecuteDataset(connstring, "sp_GetUserPasswordAndEmail", sqlparams).Tables[0];

            //string ePwd = dt.Rows[0]["Password"].ToString();
            //string eAdd = dt.Rows[0]["EmailAddress"].ToString();
            string ePwd = userPP;
            string eAdd = userEE;


            if (userPP == "" || userEE == "")
            {
                txtNote2.Text = "Username not found.";
            }
			else
			{
				string from = MailHelper.ChangeToFriendlyName(ConfigurationManager.AppSettings["AdminEmailName"], ConfigurationManager.AppSettings["AdminEmailAddress"]);
				string to = eAdd;

				string subject = "Trans-Asia  : Password Request";

				try
				{
					if (MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(), from, to, subject,
							MailTemplate.IntegrateBodyIntoTemplate(CreateRequestPasswordBody(ePwd)),
							MailTemplate.GetTemplateLinkedResources(this)))
					{
						Session["msg"] = "Password sent!";
						LogHelper.EventLogHelper.Log("User Login > Send Password : Email Sent to " + to, EventLogEntryType.Information);
					}
					else
					{
						Session["msg"] = "Password not sent this time.";
						LogHelper.EventLogHelper.Log("User Login > Send Password : Sending Failed to " + to, EventLogEntryType.Error);
					}
				}
				catch (Exception ex)
				{
					Session["msg"] = "Password not sent this time.";
					LogHelper.EventLogHelper.Log("User Login > Send Password : " + ex.Message, EventLogEntryType.Error);
				}
                txtUserName2.Text = "";
                if ( Session["msg"] != null )
                {
                    txtNote2.Text = Session["msg"].ToString();
                    Session["msg"] = null;
                }				
			}
		}
		catch (Exception ex)
		{
			LogHelper.EventLogHelper.Log("User Login : " + ex.Message, EventLogEntryType.Error);
		}
	}

	private string CreateRequestPasswordBody(string pPassword)
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
				
		sb.Append("<tr><td align='center'><h3>Password Request</h3></td></tr>");
		sb.Append("<tr><td height='100%' valign='top'><p>");
			sb.Append("Your password is <b>" + EncryptionHelper.Decrypt(pPassword) + "</b>.<br />");
			sb.Append("<font color='red'>NOTE: Password is case sensitive.</font><br />");
		sb.Append("</p></td></tr>");

		return sb.ToString();
    }

    protected void lnkBackToLogin_Click(object sender, EventArgs e)
    {
        mView.ActiveViewIndex = 0;
    }
    #endregion

    protected void cvVerifyPasswords_ServerValidate(object source, ServerValidateEventArgs args)
    {
        // check if current password is correct
        if (EncryptionHelper.Encrypt(args.Value) == Session[Constant.SESSION_PASSWORD].ToString())
        {
            // check if new password is a strong password
            cvVerifyPasswords.ErrorMessage = "Password is weak.<br />";
            args.IsValid = (PasswordChecker.IsStrongPassword(txtNewPwd.Text.Trim()));            
        }
        else
        {
            cvVerifyPasswords.ErrorMessage = "Invalid current password.<br />";
            args.IsValid = false;
        }        
    }
    protected void lnkForceLogout_Click(object sender, EventArgs e)
    {
        if (Session[Constant.SESSION_USERID] != null)
        {
            UpdateUserLoginStatus(Session[Constant.SESSION_USERID].ToString(), 0, Session.SessionID.ToString());
            UpdateUserLoginStatus(Session[Constant.SESSION_USERID].ToString(), 1, Session.SessionID.ToString());

            Session["SesId"] = GetSessionId();

            RedirectUser(Session[Constant.SESSION_USERNAME].ToString(), int.Parse(Session[Constant.SESSION_USERTYPE].ToString()));
            //Response.Redirect("login.aspx");
        }
    }
    protected void lnkForceLogoutNo_Click(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.SignOut();
        Response.Redirect("login.aspx");
    }
}
