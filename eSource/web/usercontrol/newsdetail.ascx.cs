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

public partial class web_usercontrol_newsdetail : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void lnkBack_Click(object sender, EventArgs e)
    {
        switch (int.Parse(Session[Constant.SESSION_USERTYPE].ToString()))
        {
            case 1:
                Response.Redirect(ConfigurationManager.AppSettings["BuyerHomePage"]);
                break;
            case 2:
                Response.Redirect(ConfigurationManager.AppSettings["VendorHomePage"]);
                break;
            case 3:
                Response.Redirect(ConfigurationManager.AppSettings["PurchasingHomePage"]);
                break;
            case 5:
                Response.Redirect(ConfigurationManager.AppSettings["BidsOpeningCommitteeHomePage"]);
                break;
	    case 6:
                Response.Redirect(ConfigurationManager.AppSettings["BidsAwardingCommitteeHomePage"]);
                break;
        }
    }
}
