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
using EBid.lib.bid.data;
using EBid.lib.bid.trans;
using EBid.lib.user.trans;
using EBid.lib.user.data;
using EBid.lib;

public partial class web_usercontrol_auctionvendor_auctiondetail : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        pnlVendor.Visible = (Session[Constant.SESSION_USERTYPE].ToString() == "2");        
    }

    protected void lnkContactBuyer_Click(object sender, EventArgs e)
    {
        Session["CB_BuyerID"] = dvEventDetails.DataKey["BuyerId"].ToString();
        Session["CB_Subject"] = String.Format("BID EVENT INQUIRY: {0}", dvEventDetails.DataKey["ItemDesc"].ToString());
        Response.Redirect("contactbuyer.aspx");
    }

}
