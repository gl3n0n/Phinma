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
using EBid.ConnectionString;

public partial class web_buyerscreens_auctionCalendar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");
        
        Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;
        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Auction Calendar");

        if (!IsPostBack)
        {
            Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;

            auctCalendar.SelectedDate = DateTime.Today;
        }
    }

    protected void lblAuctionItems_Command(object sender, CommandEventArgs e)
    {
        Session[Constant.SESSION_AUCTIONREFNO] = e.CommandArgument.ToString().Trim();
        Response.Redirect("auctiondetailssubmitted.aspx");
    }

    protected void auctCalendar_SelectionChanged(object sender, EventArgs e)
    {
        Session["SelectedDate"] = auctCalendar.SelectedDate.Date;
    }
}
