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
using EBid.lib.bid.data;
using EBid.lib.bid.trans;
using EBid.lib.user.trans;
using EBid.lib.user.data;
using EBid.lib.constant;
using System.IO;
using EBid.lib;
using EBid.ConnectionString;
using System.Data.Sql;
using System.Data.SqlClient;
using EBid.ConnectionString;

namespace EBid.WEB.buyer_screens
{
    public partial class DraftBidDetails : System.Web.UI.Page
    {
        private string connstring = "";

        protected void Page_Load(object sender, System.EventArgs e)
        {
            FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
            connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
            if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
                Response.Redirect("../unauthorizedaccess.aspx");

            PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Bid Event Details");

            if (Request.QueryString["brn"] != null)
            {
                Session[Constant.SESSION_BIDREFNO] = Request.QueryString["brn"].ToString();
            }

            if (Session[Constant.SESSION_BIDREFNO] == null)
            {
                Response.Redirect("bids.aspx");
            }
            CheckVSF();
        }

        void CheckVSF()
        {
            int bidrefno = int.Parse(Session[Constant.SESSION_BIDREFNO].ToString());
            double totalcost = GetTotalBidEventCost(bidrefno);
            string sCommand = "SELECT * FROM tblBidItems WHERE BidRefNo=" + bidrefno;
            string oQualifiedSourcing;
            string oVSFId;
            //Response.Write(sCommand);
            SqlDataReader oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                oReader.Read();
                oVSFId = oReader["VSFId"].ToString();
                oQualifiedSourcing = oReader["QualifiedSourcing"].ToString();

                if (totalcost > Constant.BIDLIMIT_BEFOREAPPROVAL & oVSFId == "0" & oQualifiedSourcing=="False")
                {
                    //Response.Write(oVSFId + oQualifiedSourcing);
                    btnSubmit.Visible = false;
                }
            } 
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            Response.Redirect(Session[Constant.SESSION_LASTPAGE].ToString());
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {            
            int bidrefno = int.Parse(Session[Constant.SESSION_BIDREFNO].ToString());
            double totalcost = GetTotalBidEventCost(bidrefno);
            int invitedvendors = CountBidEventInvitedVendors(bidrefno);

            int stat = ((totalcost < Constant.BIDLIMIT_BEFOREAPPROVAL) && (invitedvendors >= 3)) ? Constant.BID_STATUS_APPROVED : Constant.BID_STATUS_SUBMITTED;
            
            if (UpdateBidEventStatus(bidrefno, stat ,
                int.Parse(Session[Constant.SESSION_USERID].ToString()), 0, txtComment.Text.Trim()))
            {
                if (stat == Constant.BID_STATUS_SUBMITTED)
                    Response.Redirect("submittedbiditems.aspx");
                else
                    Response.Redirect("approvedbiditems.aspx");
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {          
            Response.Redirect("createnewevent.aspx?" + Constant.QS_TASKTYPE + "=" + 2);
        }        

        private bool UpdateBidEventStatus(int bidrefno, int status, int buyerid, int purchasingid, string comments)
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);
            SqlTransaction sqlTransact = null;
            bool isSuccessful = false;

            try
            {
                sqlConnect.Open();
                sqlTransact = sqlConnect.BeginTransaction();

                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
                sqlParams[0].Value = bidrefno;
                sqlParams[1] = new SqlParameter("@Status", SqlDbType.Int);
                sqlParams[1].Value = status;
                sqlParams[2] = new SqlParameter("@BuyerId", SqlDbType.Int);
                sqlParams[2].Value = buyerid;
                sqlParams[3] = new SqlParameter("@PurchasingId", SqlDbType.Int);
                sqlParams[3].Value = purchasingid;
                sqlParams[4] = new SqlParameter("@Comment", SqlDbType.VarChar);
                sqlParams[4].Value = comments;

                SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "sp_UpdateBidEventStatus", sqlParams);

                sqlTransact.Commit();
                isSuccessful = true;
            }
            catch
            {
                sqlTransact.Rollback();
                isSuccessful = false;
            }
            finally
            {
                sqlConnect.Close();
            }
            return isSuccessful;
        }
        
        private double GetTotalBidEventCost(int bidrefno)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@BidRefNo", bidrefno);
            return double.Parse(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "sp_GetBidEventTotalCost", sqlParams).ToString());
        }

        private int CountBidEventInvitedVendors(int bidrefno)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@BidRefNo", bidrefno);
            return int.Parse(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "sp_CountBidEventInvitedVendors", sqlParams).ToString());
        }
    }
}
