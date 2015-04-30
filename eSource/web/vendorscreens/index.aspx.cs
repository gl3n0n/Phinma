using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EBid.lib;
using EBid.lib.bid.trans;
using EBid.lib.auction.trans;
using EBid.lib.constant;
using EBid.lib.user.data;
using EBid.lib.user.trans;
using EBid.ConnectionString;

namespace EBid.web.vendor_screens
{
    public partial class index : System.Web.UI.Page
    {
        private string connstring = "";

        protected void Page_Load(object sender, System.EventArgs e)
        { 
            FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
            connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
            if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.VENDOR)
                Response.Redirect("../unauthorizedaccess.aspx");

            PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Home");

            if (!(IsPostBack))
            {
                lblVendorName.Text = String.Format("Welcome {0}", Session[Constant.SESSION_USERFULLNAME]);
                DisplayCount();
            }
        }

        private void DisplayCount()
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@VendorId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(Session[Constant.SESSION_USERID].ToString().Trim());

            Int32[] LabelCounter = GetAllCounters();
            
            lblBidEventsCount.Text = String.Format("&nbsp;({0})", LabelCounter[0].ToString());
            lblDraftBidTendersCount.Text = String.Format("&nbsp;({0})", LabelCounter[2].ToString());
            lblSubmittedBidTendersCount.Text = String.Format("&nbsp;({0})", LabelCounter[3].ToString());
            lblForRenegotiationBidTendersCount.Text = String.Format("&nbsp;({0})", LabelCounter[4].ToString());
            lblAwardedBidItemsCnt.Text = String.Format("&nbsp;({0})", LabelCounter[5].ToString());
            lblNewAuctionCount.Text = String.Format("&nbsp;({0})", LabelCounter[6].ToString());
            lblAwardedAuctionsCnt.Text = String.Format("&nbsp;({0})", LabelCounter[7].ToString());
            lblOngoingAuctions.Text = String.Format("&nbsp;({0})", LabelCounter[8].ToString());
            lblUpcomingAuctions.Text = String.Format("&nbsp;({0})", LabelCounter[9].ToString());
            lblFinishedAuctions.Text = String.Format("&nbsp;({0})", LabelCounter[10].ToString());
        }

        private Int32[] GetAllCounters()
        {
            const int arrlength = 11;

            Int32[] tmpvalues = new Int32[arrlength];
            Array.Clear(tmpvalues, 0, arrlength);

            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@VendorId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(Session[Constant.SESSION_USERID].ToString().Trim());


            DataSet ds = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "sp_VendorScreenAllCount", sqlParams);

            for (int indx = 0; indx < ds.Tables[0].Rows.Count; indx++)
            {
                object[] obj = new object[2];
                obj = ds.Tables[0].Rows[indx].ItemArray;
                tmpvalues[indx] = Convert.ToInt32("0" + obj[0].ToString());
            }
            return tmpvalues;
        }

        protected void lnkContactBuyer_Click(object sender, EventArgs e)
        {

            Response.Redirect("contactbuyer.aspx");
        }
}
}
