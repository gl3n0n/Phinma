using System;
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
        private static string connstring = ConfigurationManager.ConnectionStrings["EBidConnectionString"].ConnectionString;

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

        public static string GetPurchasingFirstName(int purchID)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@userId", SqlDbType.Int);
            sqlParams[0].Value = purchID;

            return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetPurchasingFirstName", sqlParams).ToString().Trim();
        }

        public DataTable QuerySubmittedBidsRemoveAbove2M(string orderby)
        {
            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetSubmittedBidEventsAbove2M").Tables[0];
        }

        public static int GetSubmittedBidsCountRemove2M()
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);
            
            int count = 0;

            sqlConnect.Open();

            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@status", SqlDbType.Int);
            sqlParams[0].Value = Constant.BID_STATUS_SUBMITTED;

            count = Convert.ToInt32(SqlHelper.ExecuteScalar(sqlConnect, "s3p_EBid_GetSubmittedBidsCount", sqlParams));

            sqlConnect.Close();
            return count;
        }

        public static int QueryCountAllApproved()
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

        public static DataTable QueryBidsForRenegotiation(string orderby)
        {
            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryBidsForRenegotiation").Tables[0];
        }

        public static DataTable QueryRejectedBids(string orderby)
        {
            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryAllRejectedBids").Tables[0];
        }

        
        public static DataTable QueryEndorseSummItems(string bidRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[0].Value = bidRefNo;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_Endorse_Summ_Items", sqlParams).Tables[0];
        }

        public static DataTable QuerySelectedEndorsedItems(string bidRefNo, string bidTenderNos)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlParams[1] = new SqlParameter("@bidTenderNos", SqlDbType.VarChar);
            sqlParams[0].Value = Int32.Parse(bidRefNo.Trim());
            sqlParams[1].Value = bidTenderNos;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QuerySelectedEndorsedItems", sqlParams).Tables[0];
        }

        public static DataTable QueryBidTendersStatus()
        {
            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryBidsForOpening").Tables[0];
        }

        
		public DataTable QueryBidTendersForEndorsement()
        {            
            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryEndorsedBidTenders").Tables[0];
        }

        public DataTable QuerySingleBidTenderStatus(string bidTenderGenNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@BidTenderGenNo", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(bidTenderGenNo);
            
            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_Ebid_authDeptDisp", sqlParams).Tables[0];
        }

        public static int GetBidsForEvalCount()
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);

            int count = 0;

            sqlConnect.Open();
            count = Convert.ToInt32(SqlHelper.ExecuteScalar(sqlConnect, CommandType.StoredProcedure, "s3p_EBid_GetBidsForEvalCount"));
            sqlConnect.Close();

            return count;
        }

        public static int GetForOpenBidsCount()
        {
            
            SqlConnection sqlConnect = new SqlConnection(connstring);

            int count = 0;

            sqlConnect.Open();
            count = Convert.ToInt32(SqlHelper.ExecuteScalar(sqlConnect, CommandType.StoredProcedure, "s3p_EBid_GetBidsForOpenCount"));
            sqlConnect.Close();

            return count;
        }

        public void UpdateAuthTable(string dept, string bidTenderGeneralNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@bidTenderGenNo", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(bidTenderGeneralNo);
            sqlParams[1] = new SqlParameter("@deptType", SqlDbType.Int);
            sqlParams[1].Value = Int32.Parse(dept);

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_UpdateAuthTable", sqlParams);
        }

        public void UpdateBidTenderStatus(string vBidTenderNos, string vStatus)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@bidTenderNos", SqlDbType.NVarChar);
            sqlParams[0].Value = vBidTenderNos;
            sqlParams[1] = new SqlParameter("@status", SqlDbType.Int);
            sqlParams[1].Value = Int32.Parse(vStatus);

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_UpdateBidTenderAwardedRenegStatus", sqlParams);
        }

        
        public DataTable QueryAllAwardedItems(string bidEvent)
        {
            if (String.IsNullOrEmpty(bidEvent))
                bidEvent = "%";

            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@bidEvent", SqlDbType.VarChar);
            sqlParams[0].Value = bidEvent;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_Ebid_QueryAllAwardedBidItems", sqlParams).Tables[0];
        }

        public static int GetAwardedBidsCount()
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);

            int count = 0;

            sqlConnect.Open();
            count = Convert.ToInt32(SqlHelper.ExecuteScalar(sqlConnect, CommandType.StoredProcedure, "s3p_EBid_GetAwardedBidItemsCount"));
            sqlConnect.Close();

            return count;
        }

        public DataTable QueryAwardedItems(string company, string category, string month, string day, string year)
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

        public string QueryBuyerEmailAddViaBidRefNo(string refNo, bool isAuction)
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

        public DataTable QueryAwardedItemsbyBidRefNo(string BidRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(BidRefNo);
            sqlParams[1] = new SqlParameter("@status", SqlDbType.Int);
            sqlParams[1].Value = Constant.BID_TENDER_STATUS_AWARD;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryAwardedItemsbyBidRefNo", sqlParams).Tables[0];
        }

        public DataTable QueryPurchasingTable(string userId)
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

        public static DataTable QueryReceivedTenders()
        {
            DataSet bidData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_Ebid_QueryReceivedTenders]");
            DataTable dt = new DataTable();
            if (bidData.Tables.Count > 0)
                dt = bidData.Tables[0];
            return dt;
        }
    }
}