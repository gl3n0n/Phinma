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

namespace EBid.WEB.bac
{
    public partial class index : System.Web.UI.Page
    {
        private static string connstring = "";
        protected void Page_Load(object sender, System.EventArgs e)
        {
            connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
			foreach (string key in Session.Keys)
            {   //TEST SHOW ALL SESSIONS
				//Response.Write(key + " - " + Session[key].ToString() + "<br />");
			}

            FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
            if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BIDAWARDINGCOMMITTEE)
                Response.Redirect("../unauthorizedaccess.aspx");

            PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Home");

            if (!(Page.IsPostBack))
            {
                if ((Session[Constant.SESSION_USERID] != null) && (Session[Constant.SESSION_USERTYPE] != null))
                {                 
                    lblName.Text = String.Format("Welcome {0}!", Session[Constant.SESSION_USERFULLNAME].ToString());
                    DisplayCount();                
                }
            }
        
        }

	private void DisplayCount() 
	{
            // get all counters for this buyer
            DataRow dr = GetBacCounters(int.Parse(Session[Constant.SESSION_USERID].ToString()));
            
            // BAC BID EVENTS
            lblBacEndorsed.Text = String.Format("({0})", dr["BacEndorsedCount"].ToString());
            lblBacAwarded.Text = String.Format("({0})", dr["BacAwardedCount"].ToString());
            lblBacClarifications.Text = String.Format("({0})", dr["BacClarificationsCount"].ToString());
            lblBacRejected.Text = String.Format("({0})", dr["BacRejectedCount"].ToString());
            lblBacForAwarding.Text = String.Format("({0})", dr["BacForAwardingCount"].ToString());

	}

        public static DataRow GetBacCounters(int BACid)
        {
            DataRow dr = null;

            SqlConnection sqlConnect = new SqlConnection(connstring);

            using (sqlConnect)
            {
                sqlConnect.Open();

                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@BACid", SqlDbType.Int);
                sqlParams[0].Value = BACid;

                dr = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "sp_GetBacCounters", sqlParams).Tables[0].Rows[0];

            }
            return dr;
        }

    }
}
