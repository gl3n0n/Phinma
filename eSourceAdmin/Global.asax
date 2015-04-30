<%@ Application Language="C#" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Collections" %>
<%@ Import Namespace="System.ComponentModel" %>
<%@ Import Namespace="System.Configuration" %>
<%@ Import Namespace="System.Security.Principal" %>
<%@ Import Namespace="System.Web.Security" %>
<%@ Import Namespace="EBid.lib" %>
<%@ Import Namespace="EBid.lib.constant" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Web.SessionState" %>
<%@ Import Namespace="System.IO" %>

<script runat="server">
	
	// Created By: GA Sacramento, 07312006
    // Last Update: GA Sacramento, 09152006
	private string _tempDirectory = Constant.TEMPDIR + "\\" + 
		(DateTime.Now.Month < 10 ? "0" + DateTime.Now.Month : DateTime.Now.Month.ToString()) + "_" +
		(DateTime.Now.Day < 10 ? "0" + DateTime.Now.Day : DateTime.Now.Day.ToString()) + "_" +
		DateTime.Now.Year.ToString();
    
    public enum UseSecureConnection
    {
        On = 0,
        Off = 1,
        Custom = 2
    };

    private class SecuredPages : ArrayList
    {
        public SecuredPages() { }
    }
    
    void Application_Start(object sender, EventArgs e) 
    {        
        LogHelper.TextLogHelper.Log("EBidAdmin Application Started", LogHelper.TextLogHelper.LogType.Information);
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        LogHelper.TextLogHelper.Log("EBidAdmin Application Ended", LogHelper.TextLogHelper.LogType.Information);		
    }
        
    void Application_Error(object sender, EventArgs e) 
    {
		Exception ex = Server.GetLastError();
        LogHelper.TextLogHelper.Log("0: " + ex.Message, LogHelper.TextLogHelper.LogType.Error);
        if (ex.InnerException != null)
            LogHelper.TextLogHelper.Log("1: " + ex.InnerException.Message, LogHelper.TextLogHelper.LogType.Error);        
    }    

    protected void Application_BeginRequest(Object sender, EventArgs e)
    {
        if (Request.AppRelativeCurrentExecutionFilePath == "~/")
        {
            HttpContext.Current.RewritePath("ebidadmin/login.aspx");
        }
        
        // By default, UseSecureConnection is off
        UseSecureConnection _useSecureConnection = UseSecureConnection.Off;
        // Check configurations if any
        string _fromConfig = ConfigurationManager.AppSettings["UseSecureConnection"].Trim().ToUpper();
        string _fromConfig2 = ConfigurationManager.AppSettings["SecuredPages"].Trim();
        SecuredPages _securedPages = new SecuredPages();
        if (!String.IsNullOrEmpty(_fromConfig))
        {
            switch (_fromConfig)
            {
                case "ON": _useSecureConnection = UseSecureConnection.On; break;
                case "OFF": _useSecureConnection = UseSecureConnection.Off; break;
                case "CUSTOM": _useSecureConnection = UseSecureConnection.Custom; break;
                default: _useSecureConnection = UseSecureConnection.Off; break;                    
            }
        }
        if (!String.IsNullOrEmpty(_fromConfig2))
        {
            string[] _pages = _fromConfig2.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in _pages)
            {
                _securedPages.Add(s.Trim());
            }
        }

        if (_useSecureConnection != UseSecureConnection.Off)
        {
            string redir = string.Empty;
            if (Request.ServerVariables["HTTPS"] == "off")
            {
                redir = "https://" + Request.ServerVariables["SERVER_NAME"] + Request.ServerVariables["SCRIPT_NAME"];
                if (Request.ServerVariables["QUERY_STRING"] != "")
                    redir += "?" + Request.ServerVariables["QUERY_STRING"];
                if (_useSecureConnection == UseSecureConnection.Custom)
                {
                    foreach (object o in _securedPages)
                    {
                        if (redir.Contains(o.ToString()))
                            Response.Redirect(redir);
                    }
                }
                else
                {
                    // if set to on, then all pages will use secure connection
                    Response.Redirect(redir);
                }
            }
            else
            {
                redir = "http://" + Request.ServerVariables["SERVER_NAME"] + Request.ServerVariables["SCRIPT_NAME"];
                if (Request.ServerVariables["QUERY_STRING"] != "")
                    redir += "?" + Request.ServerVariables["QUERY_STRING"];
                
                // do not use secure connection on pages not set on securedpages configuration
                if (_useSecureConnection == UseSecureConnection.Custom)
                {
                    bool matchFound = false;
                    
                    foreach (object o in _securedPages)
                    {
                        if (redir.Contains(o.ToString()))
                            matchFound = true;
                    }
                    
                    if (!matchFound)
                        Response.Redirect(redir);
                }                
            }
        }        
    }

	void Application_AuthenticateRequest(Object sender, EventArgs e)
	{
		HttpCookie authCookie;
		FormsAuthenticationTicket ticket;
		string[] roles;
		FormsIdentity identity;
		GenericPrincipal principal;

		// acquire the authentication cookie
		authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

		// process the authentication cookie if it is available
		if (authCookie != null)
		{
			// decrypt authentication cookie
			ticket = FormsAuthentication.Decrypt(authCookie.Value);

			// make sure the authentication cookie has not expired yet
			if (!ticket.Expired)
			{
				if (ticket.UserData != null)
					roles = ticket.UserData.Trim().Split(new char[] { '|' });
				else
					roles = new string[0];

				identity = new FormsIdentity(ticket);
				principal = new GenericPrincipal(identity, roles);
				Context.User = principal;
			}
		}
	}

    void Session_Start(object sender, EventArgs e) 
    {
		// Create Temp Folder for today
		if (!(Directory.Exists(_tempDirectory)))
		{
			Directory.CreateDirectory(_tempDirectory);

            LogHelper.TextLogHelper.Log("Temp Folder Created : " + _tempDirectory, LogHelper.TextLogHelper.LogType.Information);			
		}		
    }

    void Session_End(object sender, EventArgs e) 
    {   
		// Clear Temp Files/Folders
		if (Directory.Exists(_tempDirectory))
		{
			string[] tempDirs = Directory.GetDirectories(Constant.TEMPDIR);
			foreach (string tempDir in tempDirs)
			{
				if (tempDir != _tempDirectory)
					if (Directory.Exists(tempDir))
					{
						Directory.Delete(tempDir);
                        LogHelper.TextLogHelper.Log("Temp Folder Deleted : " + tempDir, LogHelper.TextLogHelper.LogType.Information);						
					}
			}
		}		
    }       
</script>
