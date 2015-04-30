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

public partial class web_purchasing_screens_BidsforEval : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        //if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
        //    Response.Redirect("../unauthorizedaccess.aspx");

        Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Received Endorsements");
    }

    protected void gvBids_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("SelectItem"))
        {
            Session["BidDetailNo"] = e.CommandArgument;
            HiddenField VendorId = (HiddenField)gvReceivedTenders.FindControl("VendorID");

            #region BEGIN RICHARD

            GridViewRow row = ((LinkButton)e.CommandSource).Parent.Parent as GridViewRow;
            Label lblbuyer = (Label)gvReceivedTenders.Rows[row.RowIndex].FindControl("lblbuyer");
            Session["Buyer"] = lblbuyer.Text;

            #endregion
            
            Response.Redirect("endorsementsummary.aspx");
        }
        else if (e.CommandName.Equals("SelectEvent"))
        {
            Session["BidRefNo"] = e.CommandArgument;
            Response.Redirect("biddetails.aspx");
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
