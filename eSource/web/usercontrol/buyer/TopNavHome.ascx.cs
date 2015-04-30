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

public partial class WEB_user_control_TopNavHome : System.Web.UI.UserControl
{       
    private void ClearVars()
    {
        Session["ItemType"] = "";
        Session["Company"] = "";
        Session["Requestor"] = "";
        Session["PRNumber"] = "";
        Session["PRDate"] = "";
        Session["GroupDeptSec"] = "";
        Session["BidRefNo"] = "";
        Session["CategoryId"] = "";
        Session["BidSubmissionDeadline"] = "";
        Session["BidItemDescription"] = "";
        Session["DeliverTo"] = "";
        Session["Incoterm"] = "";
        Session["SubItems"] = null;
        Session["mode"] = null;
        Session["FileNames"] = null;
        Session["moreSubItems"] = null;
        Session["SubItemId"] = null;
        Session["Suppliers"] = null;
    }   
}
