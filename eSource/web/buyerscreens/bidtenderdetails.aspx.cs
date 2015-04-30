using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EBid.lib.bid.data;
using EBid.lib.bid.trans;
using EBid.lib.constant;
using EBid.lib;
using EBid.ConnectionString;
using System.IO;
using System.Data.Sql;
using System.Data.SqlClient;
using EBid.ConnectionString;

public partial class web_buyerscreens_bidtenderdetails : System.Web.UI.Page
{    
    private string connstring = "";

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (HttpContext.Current.Session["clientid"] != null)
        {
            connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        }
        else
        {
            Response.Redirect("../../login.aspx");
        }
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (Session[Constant.SESSION_BIDTENDERNO] == null)
            Response.Redirect("bids.aspx");

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Bid Tender Details");
    }    

    #region Actions
    protected void btnEndorse_Click(object sender, EventArgs e)
    {
        // update bid tender status to "endorsed"
        BidTransaction.UpdateBidTenderStatus(connstring, Convert.ToInt32(Session[Constant.SESSION_BIDTENDERNO].ToString()), Constant.BID_TENDER_STATUS_ENDORSED);
        // save comment
        BidTransaction.SaveBidTenderComment(connstring, int.Parse(Session[Constant.SESSION_BIDTENDERNO].ToString()),
            int.Parse(Session[Constant.SESSION_USERID].ToString()), txtComment.Text.Trim(), Constant.BIDTENDERCOMMENT_BUYER_TO_PURCHASING);
        Response.Redirect("endorsedbidtenders.aspx");
    }

    protected void btnRenegotiate_Click(object sender, EventArgs e)
    {
        // update bid tender status to "for renegotiation"
        BidTransaction.UpdateBidTenderStatus(connstring, Convert.ToInt32(Session[Constant.SESSION_BIDTENDERNO].ToString()), Constant.BID_TENDER_STATUS_RE_NEGOTIATE);
        // save comment
        BidTransaction.SaveBidTenderComment(connstring, int.Parse(Session[Constant.SESSION_BIDTENDERNO].ToString()),
            int.Parse(Session[Constant.SESSION_USERID].ToString()), txtComment.Text.Trim(), Constant.BIDTENDERCOMMENT_BUYER_TO_VENDOR);
        Response.Redirect("biditemsforrenegotiation.aspx");
    }
    #endregion        
    protected void lnkBack_Click(object sender, EventArgs e)
    {
        if (Session[Constant.SESSION_LASTPAGE] != null)
            Response.Redirect(Session[Constant.SESSION_LASTPAGE].ToString());
        else
            Response.Redirect("bids.aspx");
    }
}