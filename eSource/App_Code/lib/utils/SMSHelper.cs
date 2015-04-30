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
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.Security;
using System.Collections;

namespace EBid.lib
{
    public class SMSHelper
    {
        #region TrustAllCertificatePolicy
        public class TrustAllCertificatePolicy : System.Net.ICertificatePolicy
        {
            public TrustAllCertificatePolicy()
            {}

            public bool CheckValidationResult(ServicePoint sp, X509Certificate cert, WebRequest req, int problem)
            {
                return true;
            }
        }
        #endregion

        // private constructor
        private SMSHelper() { }

        public static bool SendSMS(SMSMessage message)
        {
            bool useNewSendMethod = bool.Parse(ConfigurationManager.AppSettings["UseNewSendMethod"]);

            if (useNewSendMethod)
            {
                return SendNew(message);
            }
            else
            {
                return SendOld(message);
            }
        }

        // Created By: GA S. 10192006
        private static bool SendOld(SMSMessage message)
        {
            TxtConnect.TxtConnWS txtConn = new TxtConnect.TxtConnWS();
            string username = ConfigurationManager.AppSettings["TxtConnUsername"];
            string password = ConfigurationManager.AppSettings["TxtConnPassword"];
            string organization = ConfigurationManager.AppSettings["TxtConnOrganization"];
            string token = string.Empty;            
            string smsc_from = ConfigurationManager.AppSettings["Mask"].Trim();
            string cmts = ConfigurationManager.AppSettings["Mask"].Trim();
            bool isSuccessful = false;

            if (txtConn.Url.StartsWith("https:"))
            {
                bool useProxy = bool.Parse(ConfigurationManager.AppSettings["UseProxy"].Trim());
                if (useProxy)
                {
                    int proxyPort = int.Parse(ConfigurationManager.AppSettings["ProxyPort"].Trim());
                    string proxyHost = ConfigurationManager.AppSettings["ProxyHost"].Trim();
                    string proxyUsername = ConfigurationManager.AppSettings["ProxyUsername"].Trim();
                    string proxyPassword = ConfigurationManager.AppSettings["ProxyPassword"].Trim();
                    string proxyDomain = ConfigurationManager.AppSettings["ProxyDomain"].Trim();

                    WebProxy proxy = new WebProxy(proxyHost, proxyPort);
                    NetworkCredential netCred = new NetworkCredential(proxyUsername, proxyPassword, proxyDomain);

                    proxy.Credentials = netCred;
                    txtConn.Proxy = proxy;
                }
                #pragma warning disable
                ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
                #pragma warning restore
            }

            // validate user
            DataSet ds = txtConn.ValidateUser(username, password, organization, false);

            if ((ds != null) && (ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0))
            {
                // login ok
                token = ds.Tables[0].Rows[0]["token"].ToString().Trim();                
                
                DataSet mDs = new DataSet();
                mDs.Tables.Add();
                mDs.Tables[0].Columns.Add("mobile");
                mDs.Tables[0].Columns.Add("uniqueid");

                string[] recipients = message.Recipients.Split(new char[] { ',' });
                for (int i = 0; i < recipients.Length; i++)
                {
                    DataRow dr = mDs.Tables[0].NewRow();
                    dr["mobile"] = recipients[i];
                    dr["uniqueid"] = i + 1;
                    mDs.Tables[0].Rows.Add(dr);
                }

                isSuccessful = txtConn.SendMessageList(token,
                                                        username,
                                                        message.Recipients,
                                                        message.Subject,
                                                        message.IncludeSubject,
                                                        message.Content,
                                                        message.Footer,
                                                        message.IncludeFooter,
                                                        smsc_from,
                                                        cmts,
                                                        100,
                                                        DateTime.Now.ToString("MM/dd/yy HH:mm"),
                                                        mDs);

            }
            else
            {
                // login failed
                isSuccessful = false;
            }

            return isSuccessful;
        }

        // Created By: GA S. 12142006
        private static bool SendNew(SMSMessage message)
        {
            TxtConnWS.TCWS txtConn = new TxtConnWS.TCWS();
            string username = ConfigurationManager.AppSettings["TxtConnUsername"].Trim();
            string password = ConfigurationManager.AppSettings["TxtConnPassword"].Trim();
            string organization = ConfigurationManager.AppSettings["TxtConnOrganization"].Trim();
            string token = string.Empty;
            string smsc_from = ConfigurationManager.AppSettings["Mask"].Trim();
            string cmts = ConfigurationManager.AppSettings["Mask"].Trim();
            bool isSuccessful = false;

            txtConn.Url = ConfigurationManager.AppSettings["TxtConnWS.tcws"].Trim();

            #region Proxy and Certificate
            if (txtConn.Url.StartsWith("https:"))
            {
                bool useProxy = bool.Parse(ConfigurationManager.AppSettings["UseProxy"].Trim());
                if (useProxy)
                {
                    int proxyPort = int.Parse(ConfigurationManager.AppSettings["ProxyPort"].Trim());
                    string proxyHost = ConfigurationManager.AppSettings["ProxyHost"].Trim();                    
                    string proxyUsername = ConfigurationManager.AppSettings["ProxyUsername"].Trim();
                    string proxyPassword = ConfigurationManager.AppSettings["ProxyPassword"].Trim();
                    string proxyDomain = ConfigurationManager.AppSettings["ProxyDomain"].Trim();
                    
                    WebProxy proxy = new WebProxy(proxyHost, proxyPort);
                    NetworkCredential netCred = new NetworkCredential(proxyUsername, proxyPassword, proxyDomain);
                                        
                    proxy.Credentials = netCred;
                    txtConn.Proxy = proxy;
                }
                #pragma warning disable
                ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
                #pragma warning restore
            }
            #endregion

            #region Validate User
            try
            {
                token = txtConn.ValidateUser(username, password, organization);
            }
            catch
            {
                return false;
            }
            #endregion

            if (token.ToUpper().Trim() != "INVALID CREDENTIALS")
            {
                // login ok
                isSuccessful = txtConn.SendMessageList(token,
                                                        message.Recipients,
                                                        message.Content,
                                                        smsc_from,
                                                        cmts,
                                                        DateTime.Now.ToString("MM/dd/yy HH:mm"));

            }
            else
            {
                // login failed
                isSuccessful = false;
            }


            return isSuccessful;
        }        

        public static bool IsValidMobileNumber(string mobileNum)
        {
            if (!FormattingHelper.IsNaturalNumber(mobileNum))
                return false;
            else
            {
                string[] s = ValidMobilePrefixes;
                mobileNum = ConvertToLocalFormat(mobileNum);

                if (mobileNum.Length == 11)
                {
                    for (int i = 0; i < s.Length; i++)
                    {
                        if (mobileNum.StartsWith(s[i]))
                            return true;
                    }
                    return false;
                }
                else
                    return false;
            }
        }

        public static bool AreValidMobileNumbers(string mobileNums)
        {
            string[] s = mobileNums.Split(new char[] { ',' });

            for (int i = 0; i < s.Length; i++)
            {
                if (!IsValidMobileNumber(s[i].Trim()))
                    return false;
            }
            return true;
        }

        public static string GetValidMobileNumbers_Delimited(string mobileNums)
        {
            string[] s = mobileNums.Split(new char[] { ',' });
            StringBuilder validNums = new StringBuilder();

            for (int i = 0; i < s.Length; i++)
            {
                if (IsValidMobileNumber(s[i].Trim()))
                {
                    if (validNums.Length == 0)
                        validNums.Append(s[i].Trim());
                    else
                    {
                        validNums.Append(",");
                        validNums.Append(s[i].Trim());
                    }
                }
            }
            return validNums.ToString().Trim();
        }

        public static string[] GetValidMobileNumbers_Array(string mobileNums)
        {
            string validNums = GetValidMobileNumbers_Delimited(mobileNums);
            return validNums.Split(new char[] { ',' });
        }

        public static string[] ValidMobilePrefixes
        {
            get
            {
                string[] validPrefixes = ConfigurationManager.AppSettings["ValidMobilePhonePrefixes"].Trim().Split(new char[] { '|' });
                return validPrefixes;
            }
        }

        public static string ConvertToLocalFormat(string mobileNum)
        {
            if (mobileNum.Length > 2)
            {
                if (mobileNum.StartsWith("63"))
                    return "0" + mobileNum.Substring(2, mobileNum.Length - 2);
                else if (mobileNum.StartsWith("0"))
                    return mobileNum;
                else
                    return string.Empty;
            }
            else
                return string.Empty;
        }
    }

    public class SMSMessage
    {
        // Created By: GA S. 10192006
        #region Variables
        string _subject = string.Empty;
        string _content = string.Empty;
        string _recipients = string.Empty;
        string _footer = string.Empty;
        bool _includeSuject = false;
        bool _includeFooter = false;
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SMSMessage()
        {
            _subject = string.Empty;
            _content = string.Empty;
            _recipients = string.Empty;
            _footer = string.Empty;
            _includeSuject = false;
            _includeFooter = false;
        }

        /// <summary>
        /// Sends SMS Message.
        /// Subject not included. Footer not included.
        /// </summary>
        /// <param name="content">Content of the message.</param>
        /// <param name="recipients">Recipient(s) of the message. If more than one,then separate them by comma.</param>
        public SMSMessage(string content, string recipients)
        {
            _content = content;
            _recipients = recipients;
        }

        /// <summary>
        /// Sends SMS Message.
        /// Subject will be included if not null nor empty. Footer will be included if not null nor empty.
        /// </summary>
        /// <param name="subject">Subject of the message.</param>
        /// <param name="content">Content of the message.</param>
        /// <param name="recipients">Recipient(s) of the message. If more than one,then separate them by comma.</param>
        /// <param name="footer">Footer of the message.</param>
        public SMSMessage(string subject, string content, string recipients, string footer)
        {
            if (!string.IsNullOrEmpty(_subject))
            {
                _subject = subject;
                _includeSuject = true;
            }
            else
                _includeSuject = false;

            _content = content;
            _recipients = recipients;

            if (!string.IsNullOrEmpty(_footer))
            {
                _footer = footer;
                _includeFooter = true;
            }
            else
                _includeFooter = false;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or Sets subject of the message.
        /// </summary>
        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }

        /// <summary>
        /// Gets or Sets content of the message.
        /// </summary>
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        /// <summary>
        /// Gets or Sets recipients of the message. Comma separated.
        /// </summary>
        public string Recipients
        {
            get { return _recipients; }
            set { _recipients = value; }
        }

        /// <summary>
        /// Gets or Sets footer of the message.
        /// </summary>
        public string Footer
        {
            get { return _footer; }
            set { _footer = value; }
        }

        /// <summary>
        /// Gets or Sets if the message would include the subject in the message.
        /// </summary>
        public bool IncludeSubject
        {
            get { return _includeSuject; }
            set { _includeSuject = value; }
        }

        /// <summary>
        /// Gets or Sets if the message would include the footer in the message.
        /// </summary>
        public bool IncludeFooter
        {
            get { return _includeFooter; }
            set { _includeFooter = value; }
        }
        #endregion
    }
}
