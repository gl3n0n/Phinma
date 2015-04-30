using System;
using System.Web;
using EBid.ConnectionString;
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
using EBid.lib.bid.data;
using EBid.lib.bid.trans;
using EBid.lib.user.trans;
using EBid.lib.user.data;
using EBid.lib;

public partial class web_usercontrol_bids_biddetails_w_bidtenderdetails : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        pnlVendor.Visible = (Session[Constant.SESSION_USERTYPE].ToString() == "2");
    }

    protected void gvBidItemDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "BidTenderDetails":
                {
                    string[] args = e.CommandArgument.ToString().Split(new char[] { '|' });
                    Session[Constant.SESSION_BIDREFNO] = args[0];
                    Session[Constant.SESSION_BIDTENDERNO] = args[1];
                    Session[Constant.SESSION_BIDDETAILNO] = args[2];

                    Response.Redirect("biditemdetails.aspx");
                    
                } break;
        }
    }

    protected void lnkContactBuyer_Click(object sender, EventArgs e)
    {
        Session["CB_BuyerID"] = dView.DataKey["BuyerId"].ToString();
        Session["CB_Subject"] = String.Format("BID EVENT INQUIRY: {0}", dView.DataKey["ItemDesc"].ToString());
        Response.Redirect("contactbuyer.aspx");
    }
}
