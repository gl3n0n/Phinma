using System;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EBid.lib.auction.trans;
using EBid.lib.constant;
using EBid.lib;
using System.Data.SqlClient;

public partial class web_onlineAuction_participateauctionevent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Session Expired");

        Session.Abandon();
        Session.Clear();
    }	
}
