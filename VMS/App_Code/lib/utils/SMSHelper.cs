using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

namespace Ava.lib
{
    public class SMSHelper
    {
        // private constructor
        private SMSHelper() { }

        // Created By: GA S. 10192006

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
