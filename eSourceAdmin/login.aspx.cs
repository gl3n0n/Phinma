using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
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

public partial class login : System.Web.UI.Page
{	
	string connstring = "";
	int userid = 0;
	int usertype = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Request.QueryString["clientid"] != null && Request.QueryString["clientid"] != "") || (Request.QueryString["client"] != null && Request.QueryString["client"] != ""))
        {
            if (Request.QueryString["clientid"] != null && Request.QueryString["clientid"] != "")
            {
                Response.Cookies["clientidCookie"].Value = System.Web.HttpContext.Current.Request.QueryString["clientid"].ToString();
                Response.Cookies["clientidCookie"].Expires = DateTime.Now.AddDays(30);

                Session["client"] = ClientConfig.ConfigurationsId(HttpContext.Current.Request.QueryString["clientid"].ToString(), "slug");
                Session["clientid"] = System.Web.HttpContext.Current.Request.QueryString["clientid"].ToString();
                Session["configTheme"] = ClientConfig.ConfigurationsId(HttpContext.Current.Request.QueryString["clientid"].ToString(), "configTheme");
                Session["configCompanyName"] = ClientConfig.ConfigurationsId(HttpContext.Current.Request.QueryString["clientid"].ToString(), "configCompanyName");
                Session["ConnectionString"] = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());

                Response.Cookies["clientid"].Value = System.Web.HttpContext.Current.Session["clientid"].ToString();
                Response.Cookies["clientid"].Expires = DateTime.Now.AddDays(30);
            }
            else if (Request.QueryString["client"] != null && Request.QueryString["client"] != "")
            {
                Response.Cookies["clientCookie"].Value = System.Web.HttpContext.Current.Request.QueryString["client"].ToString();
                Response.Cookies["clientCookie"].Expires = DateTime.Now.AddDays(30);

                Session["client"] = System.Web.HttpContext.Current.Request.QueryString["client"].ToString();
                Session["clientid"] = ClientConfig.ConfigurationsSlug(HttpContext.Current.Request.QueryString["client"].ToString(), "clientid");
                Session["configTheme"] = ClientConfig.ConfigurationsSlug(HttpContext.Current.Session["client"].ToString(), "configTheme");
                Session["configCompanyName"] = ClientConfig.ConfigurationsSlug(HttpContext.Current.Session["client"].ToString(), "configCompanyName");
                Session["ConnectionString"] = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());

                Response.Cookies["clientidCookie"].Value = System.Web.HttpContext.Current.Session["clientid"].ToString();
                Response.Cookies["clientidCookie"].Expires = DateTime.Now.AddDays(30);
            }


        }
        else
        {
            if (Request.Cookies["clientidCookie"] != null)
            {
                Response.Redirect("login.aspx?clientid=" + Request.Cookies["clientidCookie"].Value.ToString());
            }
            else if (Session["clientid"] == null || Session["clientid"].ToString() == "")
            {
                Response.Redirect("err_default.htm");
            }
        }

        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
		//connstring = ConfigurationManager.ConnectionStrings["EBidConnectionString"].ConnectionString;
        //Response.Write(connstring + ":" + ConfigurationManager.ConnectionStrings["EBidConnectionString"].ConnectionString);
        if (connstring == "Data Source=COMPSERVER;Initial Catalog=ebid;User ID=sa;Password=Sqldbo@2012")
        {
            Response.Redirect("err_default.htm");
        }

        if (!IsPostBack)
		{            
			if (!String.IsNullOrEmpty(User.Identity.Name))
			{
				FormsAuthenticationHelper.SignOut();
			}            
		}

		if (Request.QueryString["t"] != null)
		{
			if (Request.QueryString["t"] != "")
			{
				int i = int.Parse(Request.QueryString["t"]);
				mView.ActiveViewIndex = i < mView.Views.Count ? i : 0;

				if ((IsPostBack) && (i == 1))
					btnSend_Click(null, null);
			}
			else
				mView.ActiveViewIndex = 0;
		}
		else
		{
			mView.ActiveViewIndex = 0;
		}

		if (!IsPostBack)
		{
			if (Session["msg"] != null)
			{
				txtNote2.Text = Session["msg"].ToString();
				Session["msg"] = null;
			}			
		}		
    }	

	protected void btnLogin_ServerClick(object sender, EventArgs e)
	{
		string username = txtUserName.Text.Trim();
		string password = EncryptionHelper.Encrypt(txtPassword.Text.Trim()); 

		// check user credentials
		if (CheckUserCredentials(username, password))
		// if ok, 
		{
			SqlParameter[] sqlparams = new SqlParameter[1];
			sqlparams[0] = new SqlParameter("@Userid", SqlDbType.Int);
			sqlparams[0].Value = userid;

			// get user info
			Session[Constant.SESSION_USERNAME] = username;
			Session[Constant.SESSION_PASSWORD] = password;
			Session[Constant.SESSION_USERTYPE] = usertype = (int)SqlHelper.ExecuteScalar(connstring, "sp_GetUserType", sqlparams);

			switch (usertype)
			{
				// admin				
				case 4:
					{						
						// Create authentication ticket/cookie
						FormsAuthenticationHelper.CreateAuthenticationTicket(username, false, usertype.ToString());

						// Get the Web application configuration.
						//System.Configuration.Configuration configuration = WebConfigurationManager.OpenWebConfiguration("/EBid");

						// Get the external Authentication section.
						//AuthenticationSection authenticationSection = (AuthenticationSection)configuration.GetSection("system.web/authentication");
                        AuthenticationSection authenticationSection = (AuthenticationSection)System.Configuration.ConfigurationManager.GetSection("system.web/authentication");

						// Get the external Forms section .
						//FormsAuthenticationConfiguration formsAuthentication = authenticationSection.Forms;

						//formsAuthentication.DefaultUrl = System.Configuration.ConfigurationManager.AppSettings["AdminHomePage"];
                        string DefaultUrl = System.Configuration.ConfigurationManager.AppSettings["AdminHomePage"];

						if (String.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
						{                                                        
							//Response.Redirect(formsAuthentication.DefaultUrl);
                            Response.Redirect(DefaultUrl);
                        }
						else
						{
							string redirectUrl = FormsAuthentication.GetRedirectUrl(username, true);

                            LogHelper.TextLogHelper.Log("User Login : " + username, LogHelper.TextLogHelper.LogType.Information);

							if (redirectUrl.Contains("admin/"))
								Response.Redirect(redirectUrl);
							else
								//Response.Redirect(formsAuthentication.DefaultUrl);
                                Response.Redirect(DefaultUrl);
                        }
						break;
					}
				default:
					txtNote.Text = "Only Administrators can use this site.";
					break;
			}						
		}
		// if not, prompt incorrect username/password
		else
		{
			txtNote.Text = "Invalid username or password.";
		}
	}	

	private bool CheckUserCredentials(string username, string password)
	{		
		if ((!String.IsNullOrEmpty(username)) && (!String.IsNullOrEmpty(password)))
		{
			SqlParameter[] sqlparams = new SqlParameter[2];
			sqlparams[0] = new SqlParameter("@Username", SqlDbType.NVarChar);
			sqlparams[0].Value = username.Replace("'", "''");
			sqlparams[1] = new SqlParameter("@Password", SqlDbType.NVarChar);
			sqlparams[1].Value = password.Replace("'", "''");

			try
			{
				userid = (int)SqlHelper.ExecuteScalar(connstring, "sp_GetUserId", sqlparams);
			}
			catch (SqlException ex)
			{
                LogHelper.TextLogHelper.Log("User Login : " + ex.Message, LogHelper.TextLogHelper.LogType.Error);
			}
			// check if username exists, check password
			if (userid != 0)
			{
				// if yes, return true
				Session[Constant.SESSION_USERID] = userid;
				return true;
			}
			// if no, return false
			else
				return false;
		}
		else
			return false;
	}

	protected void btnSend_Click(object sender, EventArgs e)
	{		
		SqlParameter[] sqlparams = new SqlParameter[1];
		sqlparams[0] = new SqlParameter("@UserName", SqlDbType.NVarChar, 100);
		sqlparams[0].Value = txtUserName2.Text.Trim();
		
		DataTable dt;

		try
		{
			dt = SqlHelper.ExecuteDataset(connstring, "sp_GetUserPasswordAndEmail", sqlparams).Tables[0];

			string ePwd = dt.Rows[0]["Password"].ToString();
			string eAdd = dt.Rows[0]["EmailAddress"].ToString();

			if (ePwd.Equals("NONE") || eAdd.Equals("NONE"))
			{
				txtNote2.Text = "Username not found.";
			}
			else
			{
				string from = System.Configuration.ConfigurationManager.AppSettings["AdminEmailAddress"];
				string to = eAdd;

				string subject = "Trans - Asia : Password Request";

				try
				{
					if (MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(), from, to, subject,
							MailTemplate.IntegrateBodyIntoTemplate(CreateRequestPasswordBody(ePwd)),
							MailTemplate.GetTemplateLinkedResources(this)))
					{
						Session["msg"] = "Password was sent to your email!";
                        LogHelper.TextLogHelper.Log("User Login > Send Password : Email Sent to " + to, LogHelper.TextLogHelper.LogType.Information);						
					}
					else
					{
						Session["msg"] = "Password not sent this time.";
                        LogHelper.TextLogHelper.Log("User Login > Send Password : Sending Failed to " + to, LogHelper.TextLogHelper.LogType.Error);						
					}
				}
				catch (Exception ex)
				{
					Session["msg"] = "Password not sent this time.";
                    LogHelper.TextLogHelper.Log("User Login > Send Password : " + ex.Message, LogHelper.TextLogHelper.LogType.Error);					
				}

				Response.Redirect("login.aspx?t=1");
			}
		}
		catch (Exception ex)
		{
            LogHelper.TextLogHelper.Log("User Login : " + ex.Message, LogHelper.TextLogHelper.LogType.Error);			
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
}
