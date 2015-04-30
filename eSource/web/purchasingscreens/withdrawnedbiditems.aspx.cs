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
using EBid.lib.bid.trans;
using EBid.lib.constant;
using EBid.lib;
using EBid.ConnectionString;

public partial class web_purchasingscreens_withdrawnedbiditems : System.Web.UI.Page
{
    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
            Response.Redirect("../unauthorizedaccess.aspx");
        Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Withdrawn Bid Items");        
    }
    

    protected void gvBids_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = ((Control)e.CommandSource).NamingContainer as GridViewRow;

        switch (e.CommandName)
        {
            case "ViewBidEventDetails":
                {
                    Session[Constant.SESSION_BIDREFNO] = e.CommandArgument.ToString();
                    Response.Redirect("biddetails.aspx");
                } break;
            case "ViewBidItemDetails":
                {
                    string[] args = e.CommandArgument.ToString().Split(new char[] { '|' });

                    Session[Constant.SESSION_BIDDETAILNO] = args[1];
                    Session[Constant.SESSION_BIDREFNO] = args[0];

                    Response.Redirect("withdrawnbiditemdetails.aspx");
                } break;
            case "Approve":
                {
                    int biddetailno = int.Parse(e.CommandArgument.ToString());
                    
                    if (BidItemTransaction.WithdrawBidItem(connstring, biddetailno, Constant.BIDITEM_STATUS.WITHDRAWAL_STATUS.WITHDRAWNED))
                        Response.Redirect("withdrawnedbiditems.aspx");
                } break;
        }
    }
}
