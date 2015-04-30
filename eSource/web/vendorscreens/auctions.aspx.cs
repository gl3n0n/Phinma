using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using EBid.lib.constant;
using EBid.lib.user.data;
using EBid.lib.user.trans;
using EBid.lib.auction.trans;
using EBid.lib;

namespace EBid.web.vendor_screens
{
	public partial class auctions : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
            FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
            if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.VENDOR)
                Response.Redirect("../unauthorizedaccess.aspx");

            PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "New Auction Events");

            Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;
		}

        public bool ShowConfirmDeclineLink(string status, string deadlinediff)
        {		
	    string deadlinediff2;
	    deadlinediff2 = deadlinediff != "" ? deadlinediff : "0";
            if (status == "0")
            {
                if (Int32.Parse(deadlinediff2) <= 0)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public string ConfirmationStatus(string status, string deadlinediff)
        {
            if (status == "0")
            {
                if (Int32.Parse(deadlinediff) <= 0)
                    return "";
                else
                    return "Deadline already reached";
            }
            else if (status == "2")
                return "Declined";
            else
                return "";
        }
        protected void gvAuctionsForApproval_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Session[Constant.SESSION_AUCTIONREFNO] = e.CommandArgument;
                Response.Redirect("confirmauctionparticipation.aspx");
            }
            if (e.CommandName == "Select2")
            {
                Session[Constant.SESSION_AUCTIONREFNO] = e.CommandArgument;
                Response.Redirect("auctiondetail.aspx");
            }
        }
}

}
