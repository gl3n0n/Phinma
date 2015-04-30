using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EBid.lib;
using EBid.lib.constant;
using EBid.lib.user.trans;
using EBid.lib.auction.trans;

public partial class web_purchasing_screens_confirmedauctionInvitations : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (!(IsPostBack))
            Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Confirmed Auction Invitations");
    }

    protected void gvRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdConfirmedCnt = (HiddenField)e.Row.FindControl("hdConfirmedCnt");
            LinkButton lnkConfirmedVendors = (LinkButton)e.Row.FindControl("lnkConfirmedVendors");

            if (hdConfirmedCnt.Value == "0")
                lnkConfirmedVendors.Enabled = false;
            else
                lnkConfirmedVendors.Enabled = true;
        }
    }

    protected void lblAuctionItems_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            Session[Constant.SESSION_AUCTIONREFNO] = e.CommandArgument;
            if (e.CommandName == "Select")
                Response.Redirect("auctiondetails.aspx");
            else
                Response.Redirect("confirmedinvitations.aspx");
        }
    }
}
