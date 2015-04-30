using System;
using System.Web;
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
            String ex1 = @"
                            ^                                   # anchor at the start
                            (?=.*\d)                            # must contain at least one digit
                            (?=.*[a-z])                         # must contain at least one lowercase
                            (?=.*[\-\+\?\*\$\.\|!@#%&_=:])      # must contain at least one 
                            .{7,20}                             # min, max length
                            $                                   # anchor at the end";
                        
            return (IsMatch(password, ex1, RegexOptions.IgnorePatternWhitespace));
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
            char[] tempChar = text.Trim().ToCharArray();

            foreach (char c in tempChar)
            {
                if (c == ' ')
                    return true;
            }

            return false;
        }
    }
}