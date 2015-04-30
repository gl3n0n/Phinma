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
using EBid.lib.auction.data;
using EBid.lib.auction.trans;
using EBid.lib.bid.data;
using EBid.lib.bid.trans;
using EBid.lib.user.trans;
using EBid.lib.user.data;
using EBid.lib;
using EBid.ConnectionString;

public partial class web_purchasing_screens_auctionDetailsSubmitted : System.Web.UI.Page
{
    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (Session[Constant.SESSION_AUCTIONREFNO] == null)
            Response.Redirect("auctions.aspx");

        if (!(Page.IsPostBack))
        {
            DisplaySuppliers();
        }
        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Auction Event Details");
    }

    private void DisplaySuppliers()
    {
        AuctionTransaction vnd = new AuctionTransaction();

        gvInvitedSuppliers.DataSource = vnd.GetSuppliers(connstring, Session[Constant.SESSION_AUCTIONREFNO].ToString().Trim());        
        gvInvitedSuppliers.DataBind();
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (Session[Constant.SESSION_AUCTIONREFNO] != null)
        {            
            AuctionItemTransaction.UpdateAuctionStatus(connstring, Session[Constant.SESSION_AUCTIONREFNO].ToString(),
                Session[Constant.SESSION_USERID].ToString(),
                Constant.AUCTION_STATUS_APPROVED, txtComment.Text.Trim());

            AuctionItemTransaction.InsertAuctionParticipants(connstring, Session[Constant.SESSION_AUCTIONREFNO].ToString());

            Response.Redirect("approvedauctionevents.aspx");
        }        
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        if (Session[Constant.SESSION_AUCTIONREFNO] != null)
        {
            AuctionItemTransaction.UpdateAuctionStatus(connstring, Session[Constant.SESSION_AUCTIONREFNO].ToString().Trim(),
                Session[Constant.SESSION_USERID].ToString(),
                Constant.AUCTION_STATUS_REJECTED, txtComment.Text.Trim());

            Response.Redirect("rejectedauctionevents.aspx");
        }
    }

    protected void lnkBack_Click(object sender, EventArgs e)
    {
        if (Session[Constant.SESSION_LASTPAGE] != null)
            Response.Redirect(Session[Constant.SESSION_LASTPAGE].ToString());
        else
            Response.Redirect("auctions.aspx");
    }

    protected void btnReedit_Click(object sender, EventArgs e)
    {
        if (Session[Constant.SESSION_AUCTIONREFNO] != null)
        {
            AuctionItemTransaction.UpdateAuctionStatus(connstring, Session[Constant.SESSION_AUCTIONREFNO].ToString().Trim(),
                Session[Constant.SESSION_USERID].ToString(),
                Constant.AUCTION_STATUS_RE_EDIT, txtComment.Text.Trim());

            Response.Redirect("auctionitemsforre-editing.aspx");
        }
    }
}
