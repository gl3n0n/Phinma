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
using EBid.lib.auction.trans;
using EBid.lib.constant;
using EBid.lib;
using EBid.ConnectionString;

public partial class web_buyer_screens_approvedAuctionEvents : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (!(Page.IsPostBack))        
            Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;
        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Approved Auction Events");                    
    }

    protected void gvAuctions_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Select"))
        {
            Session[Constant.SESSION_AUCTIONREFNO] = e.CommandArgument.ToString().Trim();
            Response.Redirect("approvedauctiondetails.aspx");
        }
    }
}
