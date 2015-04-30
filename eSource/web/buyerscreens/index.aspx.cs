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
using EBid.lib.bid.trans;
using EBid.lib.auction.trans;
using EBid.lib.constant;
using EBid.lib.user.trans;
using EBid.lib.user.data;
using System.Web.Security;
using EBid.lib;
using System.Data.SqlClient;
using System.Configuration;
using EBid.ConnectionString;
using System.Web.UI.WebControls.WebParts;

namespace EBid.WEB.buyer_screens
{	
	public partial class index : System.Web.UI.Page
	{
        private static string connstring = "";

		protected void Page_Load(object sender, System.EventArgs e)
		{
            FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
            if (HttpContext.Current.Session["clientid"] != null)
            {
                connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
            }
            else 
            {
                Response.Redirect("../../login.aspx");
            }
            if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
                Response.Redirect("../unauthorizedaccess.aspx");

            if (!(Page.IsPostBack))
			{
                PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Home");
                lblUserName.Text = String.Format("Welcome {0}!", Session[Constant.SESSION_USERFULLNAME].ToString());
                DisplayCount();                
			}

            //stylesheet3.href = "~/web/themes/" + HttpContext.Current.Session["configTheme"].ToString() + "/css/style.css";
            //stylesheet4.href = "~/web/themes/" + HttpContext.Current.Session["configTheme"].ToString() + "/css/style_buyer.css";
		}

        protected void Page_LoadComplete(object sender, System.EventArgs e)
        {

            // register css
            //System.Web.UI.HtmlControls.HtmlHead head2 = (System.Web.UI.HtmlControls.HtmlHead)Page.Header;
            //System.Web.UI.HtmlControls.HtmlLink link = new System.Web.UI.HtmlControls.HtmlLink();
            //link.Attributes.Add("href", Page.ResolveClientUrl("~/web/themes/" + HttpContext.Current.Session["configTheme"].ToString() + "/css/style.css"));
            //link.Attributes.Add("type", "text/css");
            //link.Attributes.Add("rel", "stylesheet");
            //head2.Controls.Add(link);


            //System.Web.UI.HtmlControls.HtmlLink link2 = new System.Web.UI.HtmlControls.HtmlLink();
            //link2.Attributes.Add("href", Page.ResolveClientUrl("~/web/themes/" + HttpContext.Current.Session["configTheme"].ToString() + "/css/style_buyer.css"));
            //link2.Attributes.Add("type", "text/css");
            //link2.Attributes.Add("rel", "stylesheet");
            //head2.Controls.Add(link2);

            //System.Web.UI.HtmlControls. NewRow;
            //NewRow = ((System.Web.UI.HtmlControls.HtmlInputHidden)Form.FindControl("txtNewRow"));
        }
		
		private void DisplayCount() 
		{
            // get all counters for this buyer
            DataRow dr = GetBuyerCounters(int.Parse(Session[Constant.SESSION_USERID].ToString()));
            
            // BID EVENT
            lblCountDrafts.Text = String.Format("({0})", dr["DraftEventsCount"].ToString());
            lblCountSubmitted.Text = String.Format("({0})", dr["SubmittedEventsCount"].ToString());
            lblCountApproved.Text = String.Format("({0})", dr["ApprovedEventsCount"].ToString());
            lblCountReedit.Text = String.Format("({0})", dr["ReeditEventsCount"].ToString());
            lblCountRejected.Text = String.Format("({0})", dr["RejectedEventsCount"].ToString());            
            // BID ITEM
            lblCountConvertedBidItems.Text = String.Format("({0})", dr["ConvertedItemsCount"].ToString());
            lblCountWithdrawnedBidItems.Text = String.Format("({0})", dr["WithdrawnedItemsCount"].ToString());
            lblCountAwardedBidItems.Text = String.Format("({0})", dr["AwardedItemsCount"].ToString());            
            // BID TENDER
            lblCountReceivedBidTenders.Text = String.Format("({0})", dr["ReceivedTendersCount"].ToString());
            lblCountEndorsedBidTenders.Text = String.Format("({0})", dr["EndorsedTendersCount"].ToString());
            lblCountBidsTendersForRenegotiation.Text = String.Format("({0})", dr["RenegotiatedTendersCount"].ToString());
            lblCountBidsForOpening.Text = String.Format("({0})", dr["BidsForOpeningCount"].ToString());
            lblCountBidsOpened.Text = String.Format("({0})", dr["BidsOpenedCount"].ToString());
            // AUCTION EVENT

            // AUCTION ITEM                                  
            // TODO: COUNTERS FOR AUCTIONS
            lblCountDraftsAuction.Text = String.Format("({0})", dr["DraftAuctionsCount"].ToString());
            lblCountAuctionEventsForApproval.Text = String.Format("({0})", dr["SubmittedAuctionsCount"].ToString());
            lblCountApprovedAuctionEvents.Text = String.Format("({0})", dr["ApprovedAuctionsCount"].ToString());
            lblCountRejectedAuctionEvents.Text = String.Format("({0})", dr["RejectedAuctionsCount"].ToString());
            lblCountConfirmedAuctionInvitations.Text = String.Format("({0})", dr["ConfirmedAuctionCount"].ToString());
            lblCountAuctionsForReedit.Text = String.Format("({0})", dr["ReeditAuctionsCount"].ToString());
            
            lblAwardedAuctionItems.Text = String.Format("({0})", dr["AwardedAuctionCount"].ToString());

            lblCountOngoingAuctionEvents.Text = String.Format("({0})", dr["OngoingAuctionCount"].ToString());
            lblCountUpcomingAuctionEvents.Text = String.Format("({0})", dr["UpcomingAuctionCount"].ToString());
            lblCountFinishedAuctions.Text = String.Format("({0})", dr["FinishedAuctionCount"].ToString());            

            lblCountBacDrafts.Text = String.Format("({0})", dr["BacDrafts"].ToString());            
            lblCountBacEndorsed.Text = String.Format("({0})", dr["BacEndorsed"].ToString());            
            lblCountBacAwarded.Text = String.Format("({0})", dr["BacAwarded"].ToString());            
            lblCountBacClarifications.Text = String.Format("({0})", dr["BacClarifications"].ToString());
            lblCountBacRejected.Text = String.Format("({0})", dr["BacRejected"].ToString());          

            LabelVSF1.Text = String.Format("({0})", dr["VSFDrafts"].ToString());            
            LabelVSF2.Text = String.Format("({0})", dr["VSFEndorsed"].ToString());            
            LabelVSF3.Text = String.Format("({0})", dr["VSFClarify"].ToString());
            LabelVSF4.Text = String.Format("({0})", dr["VSFApproved"].ToString());

            //PR
            lblCountPR.Text = String.Format("({0})", dr["PRCount"].ToString());

        }

        public static DataRow GetBuyerCounters(int buyerid)
        {
            DataRow dr = null;

            SqlConnection sqlConnect = new SqlConnection(connstring);

            using (sqlConnect)
            {
                sqlConnect.Open();

                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@BuyerId", SqlDbType.Int);
                sqlParams[0].Value = buyerid;

                dr = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "sp_GetBuyerCounters", sqlParams).Tables[0].Rows[0];

            }
            return dr;
        }
    }
}
