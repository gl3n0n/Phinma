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
using EBid.lib.bid.trans;
using EBid.lib.user.trans;
using EBid.lib;

public partial class web_purchasing_screens_BidsforRenegotiation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        //if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
        //    Response.Redirect("../unauthorizedaccess.aspx");

        Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;

        if (!(Page.IsPostBack))
        {
            if (Session[Constant.SESSION_USERID] != null)
            {
            }
            
        }

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Bid Tenders For Renegotiation");
    }

    protected void gvBids_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("bidEvent"))
        {
            Session[Constant.SESSION_BIDREFNO] = e.CommandArgument;
            Session["BidEventType"] = "6";
            Response.Redirect("biddetails.aspx");
        }
        else if (e.CommandName.Equals("bidItem"))
        {
            Session[Constant.SESSION_BIDDETAILNO] = e.CommandArgument;
            Session["BidEventType"] = "6";
            Response.Redirect("biditemdetails.aspx");
        }
        else if (e.CommandName.Equals("SelectItem"))
        {
            Session[Constant.SESSION_BIDDETAILNO] = e.CommandArgument;
            Session["BidEventType"] = "6";
            Response.Redirect("renegotiatedsummary.aspx");
        }
    }
    protected String GetStringValue(Object myval)
    {
        if (DBNull.Value == myval)
        {
            return "";
        }
        else
        {
            return myval.ToString();
        }
    }
}
