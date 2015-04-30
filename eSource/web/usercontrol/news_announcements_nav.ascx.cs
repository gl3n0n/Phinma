using System;
using System.Web;
using EBid.ConnectionString;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EBid.lib.constant;

public partial class web_usercontrol_news_announcements_nav : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void dlNews_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "ViewNewsDetail")
        {
            Session[Constant.SESSION_CONTENTID] = e.CommandArgument.ToString();
            Response.Redirect("newsdetails.aspx");
        }
    }

    protected void dlAnnouncements_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "ViewAnnouncementDetail")
        {
            Session[Constant.SESSION_CONTENTID] = e.CommandArgument.ToString();
            Response.Redirect("announcementdetails.aspx");
        }
    }
    protected String Limit_title(object tmp_obj)
    {
        int title_length_limit = 38;
        String tmpval = tmp_obj.ToString();
        if (tmpval.Length > title_length_limit) 
        {
            tmpval = tmp_obj.ToString().Substring(0, title_length_limit - 3) + "...";
        }
        return tmpval;
    }
    protected String Limit_content(object tmp_obj)
    {
        int title_length_limit = 75;
        String tmpval = tmp_obj.ToString();
        if (tmpval.Length > title_length_limit)
        {
            tmpval = tmp_obj.ToString().Substring(0, title_length_limit - 3) + "...";
        }
        return tmpval;
    }
}
