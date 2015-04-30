using System;
using System.Web;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;
using EBid.lib.constant;
using System.Data.Sql;
using System.Data.SqlClient;
using EBid.ConnectionString;

/// Created By : GA S. 07212006
namespace EBid.lib
{
	public static class FormsAuthenticationHelper
	{

		/// <summary>
		/// Creates authentication ticket for this user
		/// </summary>
		/// <param name="username">User who owns this ticket</param>
		/// <param name="ispersistent">True, if ticket never expires, otherwise, False</param>
		/// <param name="userdata">User information to be stored in the ticket/cookie</param>
		public static void SignIn(string username, bool ispersistent, string userdata)
		{
			CreateAuthenticationTicket(username, ispersistent, userdata);
		}

		/// <summary>
		/// Signs out current user
		/// </summary>
		public static void SignOut()
		{			
			FormsAuthentication.Initialize();
			HttpContext context = HttpContext.Current;

			// clear session
			context.Session.Abandon();
			context.Session.Clear();

			// clear cookie
			HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
			cookie.Path = FormsAuthentication.FormsCookiePath;
			cookie.Expires = new DateTime(0x7cf, 1, 1);
			cookie.Secure = FormsAuthentication.RequireSSL;
			context.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
			context.Response.Cookies.Add(cookie);
		}

		/// <summary>
		/// Signs out current user and redirect to login page
		/// </summary>
		public static void SignOutAndRedirectToLogin()
        {
			HttpContext context = HttpContext.Current;
            context.Response.Redirect(FormsAuthentication.LoginUrl);
		}

		/// <summary>
		/// Signs out current user and redirect to login page with return url
		/// </summary>
		public static void SignOutAndRedirectToLoginWithReturnUrl()
		{
			SignOut();
			HttpContext context = HttpContext.Current;
			string returnUrl = string.Empty;
			if (context.Request.RawUrl.Trim() != "")
                returnUrl = "?ReturnUrl=" + context.Request.RawUrl.Trim().Replace("~/", "");
                returnUrl = "";
			//context.Response.Redirect(FormsAuthentication.LoginUrl + returnUrl);
			context.Response.Redirect(FormsAuthentication.LoginUrl);
		}

		/// <summary>
		/// Checks if user has already logged in
		/// if not, redirects back to login page
		/// if so, continue
		/// </summary>
		public static void AuthenticateUser()
		{
			HttpContext context = HttpContext.Current;
			if (context.Session["UserId"] == null)
			{
				FormsAuthenticationHelper.SignOutAndRedirectToLogin();
			}
		}

		/// <summary>
		/// Checks if user has already logged in.
		/// if not, redirects back to login page with return url;
		/// if so, continue.
		/// </summary>
		public static void AuthenticateUserWithReturnUrl()
		{
            HttpContext context = HttpContext.Current;
			if (context.Session["UserId"] == null)
			{
				FormsAuthenticationHelper.SignOutAndRedirectToLoginWithReturnUrl();
			}

            if (context.Session[Constant.SESSION_USERID] != null)
            {
                string connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());

                SqlParameter[] sqlparams = new SqlParameter[1];
                sqlparams[0] = new SqlParameter("@UserId", SqlDbType.NVarChar);
                sqlparams[0].Value = int.Parse(context.Session[Constant.SESSION_USERID].ToString());

                String SessionId = (string)SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "sp_GetUserSessionID", sqlparams);

                if ((context.Session["SesId"] == null) || (context.Session["SesId"].ToString() == "") || (context.Session["SesId"].ToString().Trim() != SessionId.Trim()))
                {
                    FormsAuthenticationHelper.SignOutAndRedirectToLogin();
                }
            }
		}

		/// <summary>
		/// Creates authentication ticket for this user
		/// </summary>
		/// <param name="username">User who owns this ticket</param>
		/// <param name="ispersistent">True, if ticket never expires, otherwise, False</param>
		/// <param name="userdata">User information to be stored in the ticket/cookie</param>
		public static void CreateAuthenticationTicket(string username, bool ispersistent, string userdata)
		{
			HttpContext context = HttpContext.Current;
			// declarations
			int version = 1;
			DateTime issueDate = DateTime.Now;
			DateTime expirationDate;
			string cookiePath = "/";

			// set the expiration date of the ticket
			if (ispersistent)
				expirationDate = DateTime.Now.AddYears(5);
			else
				expirationDate = DateTime.Now.AddMinutes(double.Parse(context.Session.Timeout.ToString()));

			// set up the authentication ticket
			FormsAuthenticationTicket ticket = new
				FormsAuthenticationTicket(version, username, issueDate, expirationDate, ispersistent, userdata, cookiePath);

			// encrypt the ticket 
			string encryptedTicket = FormsAuthentication.Encrypt(ticket);

			// place the encrypted ticket in a cookie
			HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

			// set cookie settings
			authCookie.Path = FormsAuthentication.FormsCookiePath;
			authCookie.Expires = expirationDate;
			authCookie.Secure = FormsAuthentication.RequireSSL;

			// send cookie back to user/client
			context.Response.Cookies.Add(authCookie);
		}
	}
}
