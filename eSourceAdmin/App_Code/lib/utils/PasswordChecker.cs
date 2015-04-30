using System;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace EBid.lib
{
    public static class PasswordChecker
    {        
        public static bool IsStrongPassword(String password)
        {
			String pattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[\- \+ \\ \? \* \$ \[ \] \^ \. \( \) \| `~!@#%&_={}:;',/]).{8,25}$";			
			
			return (IsMatch(password, pattern, RegexOptions.IgnorePatternWhitespace)); 
        }

        public static bool IsMatch(String input, String pattern, RegexOptions options)
        {
            Regex regex = new Regex(pattern, options);
            Match m = regex.Match(input);
            if (m.Success)
                return true;
            else
                return false;
        }

        public static bool HasSpaces(String text)
        {
			if (text.IndexOf(' ') > -1)
				return true;
			else
				return false;			
        }
    }	
}