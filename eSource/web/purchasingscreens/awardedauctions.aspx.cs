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
using EBid.lib.user.trans;
using EBid.lib.auction.trans;
using EBid.lib.bid.trans;
using EBid.lib;

public partial class web_purchasing_screens_awardedAuctionItems : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (!IsPostBack)
        {
            Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;
            Session.Add("PurchasingId", Int32.Parse(Session[Constant.SESSION_USERID].ToString().Trim()));
           
        }
        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Awarded Auctions Items");
    }
    
    protected void gvAuctions_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName.ToString())
        {
            case "SelectItem":
                Session["AuctionDetailNo"] = e.CommandArgument.ToString().Trim();
                Response.Redirect("awardedauctionitemdetails.aspx");
                break;
            case "SelectEvent":
                Session["AuctionRefNo"] = e.CommandArgument.ToString().Trim();
                Response.Redirect("awardedauctioneventdetails.aspx");
                break;
        }
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
