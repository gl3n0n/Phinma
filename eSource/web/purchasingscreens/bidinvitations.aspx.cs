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

public partial class web_purchasingscreens_bidinvitations : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (!(IsPostBack))
        {
            Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;
            PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Bid Event Invitations");
        }
    }

    protected void gvAuctionInvitations_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "ViewDetails":
                {
                    Session[Constant.SESSION_BIDREFNO] = e.CommandArgument;
                    Response.Redirect("biddetails.aspx");
                } break;
            case "Pending":
                {
                    Session[Constant.SESSION_BIDREFNO] = e.CommandArgument;
                    Session["BIStatus"] = 0;
                    Response.Redirect("bidinvitationdetails.aspx");
                } break;
            case "Confirmed":
                {
                    Session[Constant.SESSION_BIDREFNO] = e.CommandArgument;
                    Session["BIStatus"] = 1;
                    Response.Redirect("bidinvitationdetails.aspx");
                } break;
            case "Declined":
                {
                    Session[Constant.SESSION_BIDREFNO] = e.CommandArgument;
                    Session["BIStatus"] = 2;
                    Response.Redirect("bidinvitationdetails.aspx");
                } break;
        }
    }

    protected bool IsEnabled(string count)
    {
        return (count.Trim() != "0");
    }
}
