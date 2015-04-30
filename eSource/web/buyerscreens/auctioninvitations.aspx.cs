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

public partial class WEB_buyer_screens_AuctionInvitations : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (!(IsPostBack))
        {
            Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;
            PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Confirmed Auction Invitations");
        }
    }

    protected bool IsEnabled(string count)
    {
        return (count.Trim() != "0");
    }    

    protected void gvAuctionInvitations_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "ViewDetails":
                {
                    Session[Constant.SESSION_AUCTIONREFNO] = e.CommandArgument;
                    Response.Redirect("auctiondetails.aspx");
                } break;
            case "Pending":
                {
                    Session[Constant.SESSION_AUCTIONREFNO] = e.CommandArgument;
                    Session["IStatus"] = 0;
                    Response.Redirect("auctioninvitationdetails.aspx");
                } break;
            case "Confirmed":
                {
                    Session[Constant.SESSION_AUCTIONREFNO] = e.CommandArgument;
                    Session["IStatus"] = 1;
                    Response.Redirect("auctioninvitationdetails.aspx");
                } break;
            case "Declined":
                {
                    Session[Constant.SESSION_AUCTIONREFNO] = e.CommandArgument;
                    Session["IStatus"] = 2;
                    Response.Redirect("auctioninvitationdetails.aspx");
                } break;            
        }
    }    
}
