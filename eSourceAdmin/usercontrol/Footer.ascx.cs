using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class web_user_control_Footer : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {		
    }
    //protected string TestShowAllSessions()
    //{
    //    //test show all session
    //    string str = null;
    //    foreach (string key in HttpContext.Current.Session.Keys)
    //    {
    //        str += string.Format("<b>{0}</b>: {1};  ", key, HttpContext.Current.Session[key].ToString());
    //    }
    //    return ("<span style='font-size:9px; color: #fff'>" + str + "</span>");
    //}


    //protected void TestShowAllCookies()
    //{
    //    Response.Write("<br><span style='font-size:12px; color: #3366CC'>");

    //    //test show all cookies
    //    int loop1, loop2;
    //    HttpCookieCollection MyCookieColl;
    //    HttpCookie MyCookie;

    //    MyCookieColl = Request.Cookies;

    //    // Capture all cookie names into a string array.
    //    String[] arr1 = MyCookieColl.AllKeys;

    //    // Grab individual cookie objects by cookie name.
    //    for (loop1 = 0; loop1 < arr1.Length; loop1++)
    //    {
    //        MyCookie = MyCookieColl[arr1[loop1]];
    //        Response.Write("Cookie: " + MyCookie.Name + "; ");
    //        Response.Write("Secure:" + MyCookie.Secure + "; ");

    //        //Grab all values for single cookie into an object array.
    //        String[] arr2 = MyCookie.Values.AllKeys;

    //        //Loop through cookie Value collection and print all values.
    //        for (loop2 = 0; loop2 < arr2.Length; loop2++)
    //        {
    //            Response.Write("Value" + loop2 + ": " + Server.HtmlEncode(arr2[loop2]) + "  |  ");
    //        }
    //    }
    //    Response.Write("</span>");
    //}


    //protected void Page_Init(object sender, EventArgs e)
    //{
    //    this.Page.LoadComplete += new EventHandler(Page_LoadComplete);
    //}
    //protected void Page_LoadComplete(object sender, EventArgs e)
    //{
    //    //Label1.Text = TestShowAllSessions();
    //    //TestShowAllCookies();
    //}
}
