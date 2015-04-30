using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EBid.lib.constant;
using EBid.lib.bid.trans;
using EBid.lib.user.trans;
using EBid.lib;
using System.Data.SqlClient;
using System.Configuration;

namespace EBid.WEB.boc
{
    public partial class bidsopened : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
            if ( Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BIDOPENINGCOMMITTEE )
                Response.Redirect("../unauthorizedaccess.aspx");

            Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;
            PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Bid Opened");
        }
        protected void gvBidEventsOpened_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if ( e.CommandName.Equals("bidEvent") )
            {
                Session[Constant.SESSION_BIDREFNO] = e.CommandArgument.ToString().Trim();
                Response.Redirect("biddetails.aspx");
            }
        }
    }
}
