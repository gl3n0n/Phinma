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

public partial class web_buyerscreens_AwardedAuctions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (!(Page.IsPostBack))
        {
            Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;
            PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Awarded Auction Items");
            
            hdnUserId.Value = Session[Constant.SESSION_USERID].ToString().Trim();
            Session.Add("BuyerId", Int32.Parse(Session[Constant.SESSION_USERID].ToString().Trim()));
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("index.aspx");
    }

    protected void gvAuctions_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName.ToString())
        {
            case "SelectItem":
                Session["AuctionDetailNo"] = e.CommandArgument.ToString().Trim();
                Response.Redirect("AwardedAuctionItemDetails.aspx");
                break;
            case "SelectEvent":
                Session["AuctionRefNo"] = e.CommandArgument.ToString().Trim();
                Response.Redirect("AwardedAuctionEventDetails.aspx");
                break;
        }
        //Session[Constant.SESSION_AUCTIONREFNO] = e.CommandArgument;
        //Response.Redirect("auctiondetails.aspx");
        //Session[Constant.SESSION_AUCTIONDETAILNO] = e.CommandArgument;
        //Response.Redirect("awardedauctiondetails.aspx");
    }

    protected void chkbuyeropts_SelectedIndexChange(object sender, EventArgs e)
    {
        setFilter();
    }

    private void setFilter()
    {
        string filter = string.Empty;

        for (int ind = 0; ind < chkbuyeropts.Items.Count; ind++)
        {
            if (chkbuyeropts.Items[ind].Selected == true)
            {
                switch (chkbuyeropts.Items[ind].Value)
                {
                    case "0"://Trans-Asia
                        {
                            if (string.IsNullOrEmpty(filter))
                                filter = "0";
                            else
                                filter += ",0";
                        }
                        break;
                    case "1"://
                        {
                            if (string.IsNullOrEmpty(filter))
                                filter = "1";
                            else
                                filter += ",1";
                        }
                        break;
                    case "2"://2
                        {
                            if (string.IsNullOrEmpty(filter))
                                filter = "2";
                            else
                                filter += ",2";
                        }
                        break;
                }
            }
        }

        if (string.IsNullOrEmpty(filter))
            filter = "-1";

        dsAwardedAuctions.FilterExpression = String.Format("CompanyId IN ({0})", filter); ;
        gvAuctions.DataBind();
    }
}
