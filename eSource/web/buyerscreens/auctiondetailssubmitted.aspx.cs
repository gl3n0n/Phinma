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
using EBid.lib.auction.data;
using EBid.lib.bid.trans;
using EBid.lib.constant;
using EBid.lib;
using EBid.ConnectionString;

public partial class web_buyer_screens_AuctionDetailsSubmitted : System.Web.UI.Page
{
    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");
        
        if (!(IsPostBack))
        {
            Session[Constant.SESSION_COMMENT_TYPE] = "1";

            if (Request.QueryString["arn"] != null)
            {
                Session["AuctionRefNo"] = Request.QueryString["arn"].ToString().Trim();
            }

            if (Session["AuctionRefNo"] != null)
            {
                hdnAuctionRefNo.Value = Session["AuctionRefNo"].ToString().Trim();
                DisplaySuppliers();
            }
            else
            {
                Response.Redirect("submittedauctionevents.aspx");
            }
        }

    }

    private void DisplaySuppliers()
    {
        AuctionTransaction vnd = new AuctionTransaction();

        gvInvitedSuppliers.DataSource = vnd.GetSuppliers(connstring, Session[Constant.SESSION_AUCTIONREFNO].ToString().Trim());
        gvInvitedSuppliers.DataBind();
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (Session[Constant.SESSION_LASTPAGE] != null)
            Response.Redirect(Session[Constant.SESSION_LASTPAGE].ToString());
        else
            Response.Redirect("auctions.aspx");
    }
}
