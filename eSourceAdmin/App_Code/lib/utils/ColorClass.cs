using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Color
/// </summary>
public class ColorClass
{
    public ColorClass()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public System.Drawing.Color stringToColor(System.String paramValue)
    {
        int red;
        int green;
        int blue;
        red = (System.Int32.Parse(paramValue.Substring(0, (2) - (0)), System.Globalization.NumberStyles.AllowHexSpecifier));
        green = (System.Int32.Parse(paramValue.Substring(2, (4) - (2)), System.Globalization.NumberStyles.AllowHexSpecifier));
        blue = (System.Int32.Parse(paramValue.Substring(4, (6) - (4)), System.Globalization.NumberStyles.AllowHexSpecifier));
        return System.Drawing.Color.FromArgb(red, green, blue);
    }

}
