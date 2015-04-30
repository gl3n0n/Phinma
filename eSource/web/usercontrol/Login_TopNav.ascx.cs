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


public partial class web_user_control_Login_TopNav : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void lnkHome_Click(object sender, EventArgs e)
    {
        switch (Session[Constant.SESSION_USERTYPE].ToString().Trim()) 
        {
            case "1":
                Response.Redirect(ConfigurationManager.AppSettings["BuyerHomePage"].Trim());
            break;

            case "2":
                Response.Redirect(ConfigurationManager.AppSettings["VendorHomePage"].Trim());
            break;

            case "3":
                Response.Redirect(ConfigurationManager.AppSettings["PurchasingHomePage"].Trim());
            break;  
            default:
                Response.Redirect(ConfigurationManager.AppSettings["LoginPage"].Trim());
                break;
        }
    }
}
