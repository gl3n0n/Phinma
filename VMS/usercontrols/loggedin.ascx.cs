using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Configuration;
using System.Web.Configuration;

public partial class usercontrols_loggedin : System.Web.UI.UserControl
{
    protected void TestShowAllSessions()
    {  //test show all session
        string str = null;
        foreach (string key in HttpContext.Current.Session.Keys)
        { str += string.Format("<b>{0}</b>: {1};  ", key, System.Web.HttpContext.Current.Session[key].ToString()); }
        System.Web.HttpContext.Current.Response.Write("<span style='font-size:12px'>" + str + "</span>");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //TestShowAllSessions();
        if (System.Web.HttpContext.Current.Session["UserId"] != null && System.Web.HttpContext.Current.Session["UserId"].ToString() != "")
        {
            string UserLbl = System.Web.HttpContext.Current.Session["SESSION_FULLNAME"].ToString().Trim() != "" ? System.Web.HttpContext.Current.Session["SESSION_FULLNAME"].ToString() : "User";
            Label1.Text = "Welcome, " + UserLbl + " | <a href=\"logout.aspx\">Logout</a>";
        }
        else 
        { 
            Label1.Text = ""; 
        }
        //System.Web.UI.UserControl.Label1.Text = "sdfsdfsdf";
        //System.Web.HttpContext.Current.Response.Write("afdasfsdf");
    }
    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        //TestShowAllSessions();
        //Label1.Text = System.Web.HttpContext.Current.Session["SESSION_USERNAME"].ToString();
        //System.Web.UI.UserControl.Label1.Text = "sdfsdfsdf";
        //System.Web.HttpContext.Current.Response.Write("afdasfsdf");
    }
}