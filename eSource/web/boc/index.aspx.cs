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
using System.Data.SqlClient;
using System.Configuration;
using EBid.ConnectionString;

namespace EBid.WEB.boc
{
    public partial class index : System.Web.UI.Page
    {
        private static string connstring = "";
        protected void Page_Load(object sender, System.EventArgs e)
        {
            connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
            FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
            if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BIDOPENINGCOMMITTEE)
                Response.Redirect("../unauthorizedaccess.aspx");

            PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Home");

            if (!(Page.IsPostBack))
            {
                if ((Session[Constant.SESSION_USERID] != null) && (Session[Constant.SESSION_USERTYPE] != null))
                {                 
                    lblName.Text = String.Format("Welcome {0}!", Session[Constant.SESSION_USERFULLNAME].ToString());
                    lblBidForOpening.Text = "(" + BOCTransaction.GetCountBidEventsForOpening(connstring).ToString().Trim() + ")";
                    lblBidsOpened.Text = "(" + BOCTransaction.GetCountBidEventsOpened(connstring).ToString().Trim() + ")";
                    DisplayCount();                
                }
            }
        
        }

	private void DisplayCount() 
	{
            // get all counters for this buyer
            DataRow dr = GetBocCounters(int.Parse(Session[Constant.SESSION_USERID].ToString()));
            
            // BID TENDER
            lblCountReceivedBidTenders.Text = String.Format("({0})", dr["ReceivedTendersCount"].ToString());

	}

        public static DataRow GetBocCounters(int BOCid)
        {
            DataRow dr = null;

            SqlConnection sqlConnect = new SqlConnection(connstring);

            using (sqlConnect)
            {
                sqlConnect.Open();

                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@BOCid", SqlDbType.Int);
                sqlParams[0].Value = BOCid;

                dr = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "sp_GetBocCounters", sqlParams).Tables[0].Rows[0];

            }
            return dr;
        }

    }
}
