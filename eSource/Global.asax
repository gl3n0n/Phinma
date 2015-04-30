<%@ Application Language="C#" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Collections" %>
<%@ Import Namespace="System.ComponentModel" %>
<%@ Import Namespace="System.Configuration" %>
<%@ Import Namespace="System.Security.Principal" %>
<%@ Import Namespace="System.Web.Security" %>
<%@ Import Namespace="EBid.lib" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Web.SessionState" %>
<%@ Import Namespace="System.Web.Caching" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="EBid.lib.constant" %>
<%@ Import Namespace="EBid.lib" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.Sql" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Net.Mail" %>
<%@ Import Namespace="EBid.ConnectionString" %>

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
        LogHelper.TextLogHelper.Log("EBid Application Started", LogHelper.TextLogHelper.LogType.Information);
        DoRegisterCacheEntry();

    }

    void Application_End(object sender, EventArgs e)
    {
        LogHelper.TextLogHelper.Log("EBid Application Ended", LogHelper.TextLogHelper.LogType.Information);
    }

    void Application_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        LogHelper.TextLogHelper.Log("0: " + ex.Message, LogHelper.TextLogHelper.LogType.Error);
        if (ex.InnerException != null)
            LogHelper.TextLogHelper.Log("1: " + ex.InnerException.Message, LogHelper.TextLogHelper.LogType.Error);
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

    protected void Application_BeginRequest(Object sender, EventArgs e)
    {
        //LogHelper.TextLogHelper.Log("EBid Application_BeginRequest", LogHelper.TextLogHelper.LogType.Information);
        
        // If the dummy page is hit, then it means we want to add another item in cache
        if (HttpContext.Current.Request.Url.ToString().Contains(DummyPage))
        {
            // Add the item in cache and when succesful, do the work.
            DoRegisterCacheEntry();
        }

        if (Request.AppRelativeCurrentExecutionFilePath == "~/")
        {
            HttpContext.Current.RewritePath("login.aspx");
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

    //================================================================================================
    //================================================================================================

    private const string DummyPage = "CacheTimeout.aspx";
    private const string DummyCacheItemKey = "EBID_CacheTimeoutKey";
    private const string DummyCacheItemValue = "EBID_CacheTimeoutValue";

    private void DoRegisterCacheEntry()
    {
        LogHelper.TextLogHelper.Log("EBid DoRegisterCacheEntry", LogHelper.TextLogHelper.LogType.Information);
        //RegisterCacheEntry();
    }

    
    private void HitPage()
    {
        LogHelper.TextLogHelper.Log("EBid HitPage", LogHelper.TextLogHelper.LogType.Information);

        try
        {
            string serverBaseDir = AppDomain.CurrentDomain.BaseDirectory.TrimEnd("\\".ToCharArray());
            string appBaseDir = serverBaseDir.Substring(serverBaseDir.LastIndexOf("\\") + 1);
            string webRoot = ConfigurationManager.AppSettings["WebRoot"].Trim();

            //string dummyPageUrl = "http://localhost/" + appBaseDir + "/" + DummyPage;
            string dummyPageUrl = webRoot + "/" + DummyPage;
            LogHelper.TextLogHelper.Log("EBid HitPage: dummyPageUrl: " + dummyPageUrl, LogHelper.TextLogHelper.LogType.Information);

            System.Net.WebClient client = new System.Net.WebClient();
            client.Credentials = System.Net.CredentialCache.DefaultCredentials;
            client.DownloadData(dummyPageUrl);
        }
        catch (Exception ex)
        {
            // An error has occurred!!!
        }
    }

    private void DoWork()
    {
        ProcessScheduledTasks();
    }

    public void CacheItemRemovedCallback(string key,
                object value, CacheItemRemovedReason reason)
    {
        HitPage();
        DoWork();
    }

    private bool RegisterCacheEntry()
    {
        if (null != HttpContext.Current.Cache[DummyCacheItemKey]) return false;

        HttpContext.Current.Cache.Add(DummyCacheItemKey, DummyCacheItemValue, null,
            DateTime.MaxValue, TimeSpan.FromMinutes(1),
            CacheItemPriority.Normal,
            new CacheItemRemovedCallback(CacheItemRemovedCallback));

        return true;
    }

    //================================================================================================

    private string connString;

    private DataTable GetBidEventsPassDeadline()
    {
        connString = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        DataSet dataSet = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "sp_GetBidEventsPassDeadline");
        DataTable dataTable = dataSet.Tables[0];
        return dataTable;
    }

    private DataTable GetBOCMembersInfo()
    {
        connString = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        DataSet dataSet = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "sp_GetBocEmailAddresses");
        DataTable dataTable = dataSet.Tables[0];
        return dataTable;
    }

    private int UpdateSentEmailToBoc(int bidrefno)
    {
        connString = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        SqlParameter[] sqlparams = new SqlParameter[1];
        sqlparams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
        sqlparams[0].Value = bidrefno;
        return SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "sp_UpdateSentEmailToBoc", sqlparams);
    }

    private bool SendEmail(string From, string To, string Cc, string Bcc, string Subject, string Body)
    {
        bool success = false;
        LinkedResource[] linkresources = new LinkedResource[0];

        if (MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(), From, To, Subject, Body, linkresources))
        {
            success = true;
        }
        return success;
    }

    private string CreateBOCEmailBody(DataRow drBidEvent)
    {
        StringBuilder sb = new StringBuilder();

        string bidRefNo = drBidEvent["BidRefNo"].ToString();
        string bidEvent = drBidEvent["ItemDesc"].ToString();
        string buyerName = drBidEvent["BuyerName"].ToString();
        string deadline = drBidEvent["Deadline"].ToString();

        sb.Append("<tr><td style='width: 5%; height: 13px;'></td><td style='width: 90%; height: 13px;' colspan=''></td><td style='width: 5%; height: 13px;'></td></tr><tr><td style='width: 5%; height: 635px'></td><td style='width: 90%'><br />");
        sb.Append(DateTime.Now.ToLongDateString());
        sb.Append("<br /><br />");
        sb.Append("Bid Opening Committee Member");
        sb.Append("<br />");
        sb.Append("Trans-Asia ");
        sb.Append("<br /><br />");
        sb.Append("Dear BOC Member,");
        sb.Append("<br /><br />");
        sb.Append("Re: Request for Opening - " + bidEvent);
        sb.Append("<br /><br />");
        sb.Append("This is to request the Opening of Bid tenders received for the Bid event: " + bidEvent);
        sb.Append("<br />");
        sb.Append("Details are as follows:");
        sb.Append("<br />");
        sb.Append("Bid Reference Number: " + bidRefNo + "<br>");
        sb.Append("Bid Event Name: " + bidEvent + "<br>");
        sb.Append("Buyer: " + buyerName + "<br>");
        sb.Append("Bid Submission Deadline: " + deadline + "<br>");
        sb.Append("<br /><br />");
        sb.Append("Very truly yours,");
        sb.Append("<br /><br />");
        sb.Append(buyerName);
        sb.Append("<br />");
        sb.Append("</td><td style='width: 5%; height: auto'></td></tr><tr><td style='width: 5%'></td><td style='width: 90%'></td><td style='width: 5%'></td></tr>");
        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }

    private void SendBOCEmailNotification()
    {
        LogHelper.TextLogHelper.Log("EBid SendBOCEmailNotification", LogHelper.TextLogHelper.LogType.Information);

        string From = MailHelper.ChangeToFriendlyName(ConfigurationManager.AppSettings["AdminEmailName"], ConfigurationManager.AppSettings["AdminEmailAddress"]);
        string Subject = "Trans-Asia /Innove Commnunications : Request for Opening";

        DataTable dtBidEvents = GetBidEventsPassDeadline();
        DataTable dtBOCMembers = GetBOCMembersInfo();

        for (int i = 0; i < dtBidEvents.Rows.Count; i++)
        {
            DataRow drBidEvent = dtBidEvents.Rows[i];
            int bidrefno = int.Parse(drBidEvent["BidRefNo"].ToString());

            for (int j = 0; j < dtBOCMembers.Rows.Count; j++)
            {
                DataRow drBOCMember = dtBOCMembers.Rows[j];

                string bocName = drBOCMember["BocName"].ToString();
                string bocEmail = drBOCMember["EmailAdd"].ToString();

                string To = MailHelper.ChangeToFriendlyName(bocName, bocEmail);
                string Body = CreateBOCEmailBody(drBidEvent);

                //-- send notification...
                SendEmail(From, To, "", "", Subject, Body);
            }
            UpdateSentEmailToBoc(bidrefno);
        }
    }

    //================================================================================================

    private void ProcessScheduledTasks()
    {
        SendBOCEmailNotification();
    }

    //================================================================================================
    //================================================================================================
   
</script>
