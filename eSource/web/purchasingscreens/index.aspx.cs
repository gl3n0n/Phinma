using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EBid.lib.bid.trans;
using EBid.lib.auction.trans;
using EBid.lib.user.trans;
using EBid.lib.constant;
using EBid.lib;
using EBid.ConnectionString;

namespace EBid.WEB.purchasing_screens
{	
	public partial class index : System.Web.UI.Page
	{
        private static string connstring = "";

		protected void Page_Load(object sender, System.EventArgs e)
		{
            PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Home");

            FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
			connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
            if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
                Response.Redirect("../unauthorizedaccess.aspx");            

            if (!(Page.IsPostBack))
            {
                if ((Session[Constant.SESSION_USERID] != null) && (Session[Constant.SESSION_USERTYPE] != null))
                {
                    lblName.Text = String.Format("Welcome {0}!", Session[Constant.SESSION_USERFULLNAME].ToString());

                    int[] purchasing_values = PurchasingTransaction.QueryCountAll(connstring, Convert.ToInt32(Session[Constant.SESSION_USERID].ToString()));

                    lblBidEventsForApproval.Text = "(" + purchasing_values[0].ToString() + ")";
                    lblApprovedBidEvents.Text = "(" + purchasing_values[1].ToString() + ")";

                    lblConvertedBidItems.Text = "(" + purchasing_values[11].ToString() + ")";
                    lblAwardedBidItems.Text = "(" + purchasing_values[4].ToString() + ")";
                    lblWithdrawnItems.Text = "(" + purchasing_values[13].ToString() + ")";

                    lblRecievedEndorsements.Text = "(" + purchasing_values[3].ToString() + ")";
                    lblBidTendersForRenegotiation.Text = "(" + purchasing_values[2].ToString() + ")";

                    lblAuctionEventsForApproval.Text = "(" + purchasing_values[5].ToString() + ")";
                    //lblConfirmedAuctionInvitations.Text = "(" + purchasing_values[6].ToString() + ")";

                    lblAwardedAuctionItems.Text = "(" + purchasing_values[7].ToString() + ")";
                    lblEndorsedAuctionItems.Text = "(" + purchasing_values[12].ToString() + ")";

                    lblOngoingAuctionEvents.Text = "(" + purchasing_values[8].ToString() + ")";
                    lblUpcomingAuctionEvents.Text = "(" + purchasing_values[9].ToString() + ")";
                    lblFinishedAuctionEvents.Text = "(" + purchasing_values[10].ToString() + ")";

               

                }                
            }

            
		}		

        protected void lnkBids_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("bids.aspx");
		}
        
        protected void LinkRxTenders_Click(object sender, EventArgs e)
        {
            Response.Redirect("bidsforeval.aspx");
        }

        protected void LinkApprovedBids_Click(object sender, EventArgs e)
        {
            Response.Redirect("approvedbidevents.aspx");
        }

        protected void LinkAwardedBids_Click(object sender, EventArgs e)
        {
            Response.Redirect("awardedbiditems.aspx");
        }

        protected void LnkAuctionsForApproval_Click(object sender, EventArgs e)
        {
            Response.Redirect("auctions.aspx");
        }

        protected void LnkConfirmedAuction_Click(object sender, EventArgs e)
        {
            Response.Redirect("confirmedauctioninvitations.aspx");
        }

        protected void LnkAwardedAuctions_Click(object sender, EventArgs e)
        {
            Response.Redirect("awardedauctionitems.aspx");
        }
}
}
