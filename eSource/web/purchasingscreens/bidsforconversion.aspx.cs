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
using EBid.lib.bid.data;
using EBid.lib.bid.trans;
using EBid.lib.constant;
using EBid.lib;

public partial class web_purchasing_screens_BidsforConversion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
            Response.Redirect("../unauthorizedaccess.aspx");

		if (!(IsPostBack))
		{
            Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;
		}

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Converted Bid Items");
    }

	protected void gvBidsForConversion_RowCommand(object sender, GridViewCommandEventArgs e)
	{
        GridViewRow gvr = ((Control)e.CommandSource).NamingContainer as GridViewRow;

		if (e.CommandName.Equals("SelectBidItem"))
		{
            Session[Constant.SESSION_BIDREFNO] = gvBidsForConversion.DataKeys[gvr.RowIndex].Values[0].ToString();
			Session[Constant.SESSION_BIDDETAILNO] = e.CommandArgument.ToString().Trim();
			Response.Redirect("biditemdetail.aspx");
		}
        else if (e.CommandName.Equals("SelectBidEvent"))
        {
            Session[Constant.SESSION_BIDTENDERNO] = null;
            Session[Constant.SESSION_BIDREFNO] = e.CommandArgument.ToString().Trim();
            Response.Redirect("biddetails.aspx");
        }
	}


}
