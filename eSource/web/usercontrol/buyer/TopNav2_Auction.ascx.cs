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
using System.IO;
using EBid.lib.constant;

public partial class web_user_control_Buyer_TopNav2_Auction : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(Page.IsPostBack))
        {
            DateTime dt = new DateTime();
            dt = System.DateTime.Now;
            lblDate.Text = "Today is " + dt.ToString("MMMM dd, yyyy hh:mm:ss tt");
        }
    }

    protected void lnkCreateNewItem_Click(object sender, EventArgs e)
    {
        Session[Constant.SESSION_AUCTIONREFNO] = null;
        Response.Redirect("createauction.aspx");
    }
    
}
