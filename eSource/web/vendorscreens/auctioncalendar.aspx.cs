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
using EBid.lib.constant;
using EBid.lib;

public partial class web_vendorscreens_auctionCalendar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.VENDOR)
            Response.Redirect("../unauthorizedaccess.aspx");

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Auction Calendar");

        Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;

        if (!IsPostBack)
        {
            if (Session["SelectedDate"] != null)
                auctCalendar.SelectedDate = DateTime.Parse(Session["SelectedDate"].ToString());
            else
                auctCalendar.SelectedDate = DateTime.Today;            
        }
    }

    protected void auctCalendar_SelectionChanged(object sender, EventArgs e)
    {
        Session["SelectedDate"] = auctCalendar.SelectedDate.Date;        
    }

    protected void lblAuctionItems_Command(object sender, CommandEventArgs e)
    {
        Session[Constant.SESSION_AUCTIONREFNO] = e.CommandArgument.ToString().Trim();
        Response.Redirect("auctiondetails.aspx");
    }
}
