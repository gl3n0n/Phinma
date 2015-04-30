using System;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using EBid.lib;
using EBid.ConnectionString;
using EBid.lib.utils;
using EBid.lib.bid.data;
using EBid.lib.constant;

/// <summary>
/// Summary description for PurchasingTransaction
/// </summary>
/// 
namespace EBid.lib.user.trans
{
    public class PurchasingTransaction
    {
        public PurchasingTransaction()
        {  
        }

        private bool ColumnEqual(object A, object B)
        {
            if (A == DBNull.Value && B == DBNull.Value)
                return true;
            if (A == DBNull.Value || B == DBNull.Value)
                return false;
            return (A.Equals(B));
        }

        public static string GetPurchasingFirstName(string connstring, int purchID)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@userId", SqlDbType.Int);
            sqlParams[0].Value = purchID;

            return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetPurchasingFirstName", sqlParams).ToString().Trim();
        }

        public DataTable QuerySubmittedBidsRemoveAbove2M(string connstring, string orderby)
        {
            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetSubmittedBidEventsAbove2M").Tables[0];
        }

        public static int GetSubmittedBidsCountRemove2M(string connstring)
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);
            
            int count = 0;

            sqlConnect.Open();

            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@Status", SqlDbType.Int);
            sqlParams[0].Value = Constant.BID_STATUS_SUBMITTED;

            count = Convert.ToInt32(SqlHelper.ExecuteScalar(sqlConnect, "s3p_EBid_GetSubmittedBidsCount", sqlParams));

            sqlConnect.Close();
            return count;
        }

        public static int GetSubmittedBidsCountRemove2M(string connstring, int purchasingId)
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);

            int count = 0;

            sqlConnect.Open();

            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@Status", SqlDbType.Int);
            sqlParams[0].Value = Constant.BID_STATUS_SUBMITTED;
            sqlParams[1] = new SqlParameter("@PurchasingId", SqlDbType.Int);
            sqlParams[1].Value = purchasingId;

            count = Convert.ToInt32(SqlHelper.ExecuteScalar(sqlConnect, "s3p_EBid_GetSubmittedBidsCount", sqlParams));

            sqlConnect.Close();
            return count;
        }

        public static int QueryCountAllApproved(string connstring)
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);

            int count = 0;

            sqlConnect.Open();

            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@status", SqlDbType.Int);
            sqlParams[0].Value = Constant.BID_STATUS_APPROVED;

            count = Convert.ToInt32(SqlHelper.ExecuteScalar(sqlConnect, "s3p_EBid_GetAllApprovedBidsCount", sqlParams));

            sqlConnect.Close();
            return count;
        }

        public static DataTable QueryBidsForRenegotiation(string connstring, string orderby)
        {
            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryBidsForRenegotiation").Tables[0];
        }

        public static DataTable QueryRejectedBids(string connstring, string orderby)
        {
            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryAllRejectedBids").Tables[0];
        }

        
        public static DataTable QueryEndorseSummItems(string connstring, string bidRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[0].Value = bidRefNo;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_Endorse_Summ_Items", sqlParams).Tables[0];
        }

        public static DataTable QuerySelectedEndorsedItems(string connstring, string bidRefNo, string bidTenderNos)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlParams[1] = new SqlParameter("@bidTenderNos", SqlDbType.VarChar);
            sqlParams[0].Value = Int32.Parse(bidRefNo.Trim());
            sqlParams[1].Value = bidTenderNos;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QuerySelectedEndorsedItems", sqlParams).Tables[0];
        }

        public static DataTable QueryBidTendersStatus(string connstring)
        {
            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryBidsForOpening").Tables[0];
        }

        
		public DataTable QueryBidTendersForEndorsement(string connstring)
        {            
            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryEndorsedBidTenders").Tables[0];
        }

        public DataTable QuerySingleBidTenderStatus(string connstring, string bidTenderGenNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@BidTenderGenNo", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(bidTenderGenNo);
            
            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_Ebid_authDeptDisp", sqlParams).Tables[0];
        }

        public static int GetBidsForEvalCount(string connstring)
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);

            int count = 0;

            sqlConnect.Open();
            count = Convert.ToInt32(SqlHelper.ExecuteScalar(sqlConnect, CommandType.StoredProcedure, "s3p_EBid_GetBidsForEvalCount"));
            sqlConnect.Close();

            return count;
        }

        public static int GetForOpenBidsCount(string connstring)
        {
            
            SqlConnection sqlConnect = new SqlConnection(connstring);

            int count = 0;

            sqlConnect.Open();
            count = Convert.ToInt32(SqlHelper.ExecuteScalar(sqlConnect, CommandType.StoredProcedure, "s3p_EBid_GetBidsForOpenCount"));
            sqlConnect.Close();

            return count;
        }

        public void UpdateAuthTable(string connstring, string dept, string bidTenderGeneralNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@bidTenderGenNo", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(bidTenderGeneralNo);
            sqlParams[1] = new SqlParameter("@deptType", SqlDbType.Int);
            sqlParams[1].Value = Int32.Parse(dept);

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_UpdateAuthTable", sqlParams);
        }

        public void UpdateBidTenderStatus(string connstring, string vBidTenderNos, string vStatus)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@bidTenderNos", SqlDbType.NVarChar);
            sqlParams[0].Value = vBidTenderNos;
            sqlParams[1] = new SqlParameter("@status", SqlDbType.Int);
            sqlParams[1].Value = Int32.Parse(vStatus);

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_UpdateBidTenderAwardedRenegStatus", sqlParams);
        }

        
        public DataTable QueryAllAwardedItems(string connstring, string bidEvent)
        {
            if (String.IsNullOrEmpty(bidEvent))
                bidEvent = "%";

            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@bidEvent", SqlDbType.VarChar);
            sqlParams[0].Value = bidEvent;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_Ebid_QueryAllAwardedBidItems", sqlParams).Tables[0];
        }

        public DataTable QueryAllAwardedItems(string connstring, Int32 userid)
        {            
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@PurchasingId", SqlDbType.Int);
            sqlParams[0].Value = userid;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "sp_GetPurchasingAwardedItems", sqlParams).Tables[0];
        }

        public static int GetAwardedBidsCount(string connstring)
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);

            int count = 0;

            sqlConnect.Open();
            count = Convert.ToInt32(SqlHelper.ExecuteScalar(sqlConnect, CommandType.StoredProcedure, "s3p_EBid_GetAwardedBidItemsCount"));
            sqlConnect.Close();

            return count;
        }

        public DataTable QueryAwardedItems(string connstring, string company, string category, string month, string day, string year)
        {
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(company);
            sqlParams[1] = new SqlParameter("@categoryId", SqlDbType.NVarChar);
            sqlParams[1].Value = category;
            sqlParams[2] = new SqlParameter("@month", SqlDbType.Int);
            sqlParams[2].Value = Int32.Parse(month);
            sqlParams[3] = new SqlParameter("@day", SqlDbType.Int);
            sqlParams[3].Value = Int32.Parse(day);
            sqlParams[4] = new SqlParameter("@year", SqlDbType.Int);
            sqlParams[4].Value = Int32.Parse(year);

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_Ebid_QueryAwardedItem", sqlParams).Tables[0];
        }

        public DataTable QueryAwardedItems(string connstring, int UserID,string company, string category, string month, string day, string year)
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(company);
            sqlParams[1] = new SqlParameter("@categoryId", SqlDbType.NVarChar);
            sqlParams[1].Value = category;
            sqlParams[2] = new SqlParameter("@month", SqlDbType.Int);
            sqlParams[2].Value = Int32.Parse(month);
            sqlParams[3] = new SqlParameter("@day", SqlDbType.Int);
            sqlParams[3].Value = Int32.Parse(day);
            sqlParams[4] = new SqlParameter("@year", SqlDbType.Int);
            sqlParams[4].Value = Int32.Parse(year);
            sqlParams[5] = new SqlParameter("@PurchasingID", SqlDbType.Int);
            sqlParams[5].Value = UserID;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "sp_QueryAwardedItem", sqlParams).Tables[0];
        }

        public string QueryBuyerEmailAddViaBidRefNo(string connstring, string refNo, bool isAuction)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@refNo", SqlDbType.Int);
            sqlParams[0].Value = refNo;
            sqlParams[1] = new SqlParameter("@isAuction", SqlDbType.Int);            

            if(!isAuction)
                sqlParams[1].Value = 0;
            else
                sqlParams[1].Value = 1;

            return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryBuyerEmailAddViaRefNo", sqlParams).ToString().Trim();
        }

        public DataTable QueryAwardedItemsbyBidRefNo(string connstring, string BidRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(BidRefNo);
            sqlParams[1] = new SqlParameter("@status", SqlDbType.Int);
            sqlParams[1].Value = Constant.BID_TENDER_STATUS_AWARD;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryAwardedItemsbyBidRefNo", sqlParams).Tables[0];
        }

        public DataTable QueryPurchasingTable(string connstring, string userId)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@UserId", SqlDbType.Int);
            sqlParams[0].Value = userId;
            DataSet bidData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryPurchasingTable", sqlParams);
            DataTable dt = new DataTable();
            if (bidData.Tables.Count > 0)
                dt = bidData.Tables[0];
            return dt;
        }

        public static DataTable QueryReceivedTenders(string connstring)
        {
            DataSet bidData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_Ebid_QueryReceivedTenders]");
            DataTable dt = new DataTable();
            if (bidData.Tables.Count > 0)
                dt = bidData.Tables[0];
            return dt;
        }


        /// <summary>
        /// Returns the count of all Bids Events and Auctions Events on the purchasing main page...
        /// </summary>
        public static int[] QueryCountAll(string connstring, int purchID) 
        {
            int[] count_values = new int[22];
            Array.Clear(count_values, 0, 21);
            
            SqlConnection sqlConnect = new SqlConnection(connstring);
            sqlConnect.Open();

            SqlParameter[] sqlParams = new SqlParameter[4];
            
            sqlParams[0] = new SqlParameter("@PurchasingId", SqlDbType.Int);
            sqlParams[0].Value = purchID;

            sqlParams[1] = new SqlParameter("@status", SqlDbType.Int);
            sqlParams[1].Value = Constant.BID_STATUS_APPROVED;
            
            sqlParams[2] = new SqlParameter("@auctionstatus", SqlDbType.Int);
            sqlParams[2].Value = Constant.AUCTION_STATUS_SUBMITTED;
            
            sqlParams[3] = new SqlParameter("@clientid", SqlDbType.Int);
            sqlParams[3].Value = HttpContext.Current.Session["clientid"].ToString();

            DataSet dset = new DataSet();
            dset = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure
                                            , "sp_GetAllPurchasingCount", sqlParams);

            for (int ind = 0; ind < count_values.Length ; ind++)
            {
                object[] tmpvals = dset.Tables[0].Rows[ind].ItemArray;
                count_values[ind] = Convert.ToInt32("0" + tmpvals[0].ToString());
            }
            dset.Dispose();
            sqlConnect.Close();

            return count_values;
        }

    }
}