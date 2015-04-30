using System;
using System.Web;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Web;
using System.Web.SessionState;
using EBid.lib;
using EBid.ConnectionString;
using EBid.lib.utils;
using EBid.lib.constant;
using EBid.lib.auction.data;

namespace EBid.lib.auction.trans
{
    public static class AuctionItemTransaction
    {
        public static DataTable QuerySubmittedAuctions(string connstring, string orderby)
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);
            DataSet dsQueryResult;
            sqlConnect.Open();
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@Status", SqlDbType.Int);
            sqlParams[0].Value = Constant.AUCTION_STATUS_SUBMITTED.ToString().Trim();
            dsQueryResult = SqlHelper.ExecuteDataset(sqlConnect, "s3p_EBid_QuerySubmittedAuctions", sqlParams);
            sqlConnect.Close();
            return dsQueryResult.Tables[0];
        }

        public static DataTable QueryApprovedAuctions(string connstring, string orderby)
        {
            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryApprovedAuctions").Tables[0];
        }

        public static DataTable QueryRejectedAuctions(string connstring, string orderby)
        {
            string query = "SELECT [AuctionRefNo], [ItemDesc], [DateRejected], " +
                           "DateName(month, [DateRejected]) + ' ' + convert(varchar(2),(DatePart(dd, [DateRejected]))) + ', ' + convert(varchar(4),(DatePart(yyyy, [DateRejected]))) as [DateRejected1] " +
                           "FROM [tblAuctionItems] " +
                           "WHERE [Status]=" + Constant.AUCTION_STATUS_REJECTED.ToString().Trim();

            if (orderby != "")
                query = query + " ORDER BY [DateRejected] " + orderby; //changed from DateCreated to DateSubmitted
            else
                query = query + " ORDER BY [DateRejected]";
            
            DataSet auctionData = new DataSet();
            SqlParameter[] sqlparam = new SqlParameter[1];
            sqlparam[0] = new SqlParameter("@query", SqlDbType.NText);
            sqlparam[0].Value = query;
            auctionData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_GenericQueryProcedure]", sqlparam);
            return auctionData.Tables[0];
        }

        public static int GetAuctionCountSubmitted(string connstring)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@status", SqlDbType.Int);
            sqlParams[0].Value = Constant.AUCTION_STATUS_SUBMITTED;

            return Convert.ToInt32(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetSubmittedAuctionsCount", sqlParams));
        }

        public static int GetCountApproved(string connstring)
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);

            int count = 0;

            sqlConnect.Open();

            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@status", SqlDbType.Int);
            sqlParams[0].Value = Constant.AUCTION_STATUS_APPROVED;

            count = Convert.ToInt32(SqlHelper.ExecuteScalar(sqlConnect, "s3p_EBid_GetApprovedAuctionsCount", sqlParams));

            sqlConnect.Close();
            return count;
        }

        public static string GetConfVendorCnt(string connstring, string auctRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@auctionRefNo", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(auctRefNo);

            return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetConfVendorCnt", sqlParams).ToString().Trim();
        }

        public static AuctionItem QueryAuctionItemInfo(string connstring, string AuctionRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@auctionRefNo", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(AuctionRefNo.Trim());


            DataTable auctionDataTable = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryAuctionItemInfo", sqlParams).Tables[0];
            AuctionItem auction = new AuctionItem();

            if (auctionDataTable.Rows.Count > 0)
            {
                DataRow auctionDataRow = auctionDataTable.Rows[0];
                auction.AuctionRefNo = Int32.Parse(auctionDataRow["AuctionRefNo"].ToString().Trim());
                auction.BidRefNo = Int32.Parse(auctionDataRow["BidRefNo"].ToString().Trim());
                auction.PRRefNo = Int32.Parse(auctionDataRow["PRRefNo"].ToString().Trim());
                auction.Requestor = auctionDataRow["Requestor"].ToString().Trim();
                auction.ItemDescription = auctionDataRow["ItemDesc"].ToString().Trim();
                auction.DeliveryDate = auctionDataRow["DeliveryDate"].ToString().Trim();
                auction.GroupDeptSec = Int32.Parse(auctionDataRow["GroupDeptSec"].ToString().Trim());
                auction.Category = auctionDataRow["Category"].ToString().Trim();
                auction.CompanyId = Int32.Parse(auctionDataRow["CompanyId"].ToString().Trim());
                auction.FileAttachments = auctionDataRow["FileAttachments"].ToString().Trim();
                auction.PRDate = auctionDataRow["PRDate"].ToString().Trim();
                auction.AuctionType = auctionDataRow["AuctionType"].ToString().Trim();
                //                auction.AuctionStartDate = auctionDataRow["AuctionDate"].ToString().Trim();
                auction.AuctionDeadline = auctionDataRow["AuctionDeadline"].ToString().Trim();
                auction.AuctionStartDate = auctionDataRow["AuctionStartDate"].ToString().Trim();
                auction.AuctionEndDate = auctionDataRow["AuctionEndDate"].ToString().Trim();
                auction.AuctionStartTime = auctionDataRow["AuctionStartTime"].ToString().Trim();
                auction.AuctionEndTime = auctionDataRow["AuctionEndTime"].ToString().Trim();
                auction.BidCurrency = auctionDataRow["BidCurrency"].ToString().Trim();
                auction.AuctionStatus = Int32.Parse(auctionDataRow["Status"].ToString().Trim());
            }

            return auction;
        }

        public static string GetAuctionTypeById(string connstring, string vAuctionType)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@auctionType", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vAuctionType);

            return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetAuctionTypeById", sqlParams).ToString().Trim();
        }

        public static string getBidCurrency(string vBidCurrency)
        {
            switch (vBidCurrency)
            {
                case "0":
                    return "US Dollar";                    
                case "1":
                    return "Philippine Peso";
                default:
                    return "Not Specified";
            }            
        }

        public static void UpdateAuctionStatusNoComment(string connstring, string vAuctionRefNo, string vUser, int vStatus)
        {
            if ((vUser != null) && (vUser != ""))
            {
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@AuctionRefNo", SqlDbType.Int);
                sqlParams[0].Value = Int32.Parse(vAuctionRefNo.Trim());
                sqlParams[1] = new SqlParameter("@UserId", SqlDbType.Int);
                sqlParams[1].Value = Int32.Parse(vUser.Trim());
                sqlParams[2] = new SqlParameter("@Status", SqlDbType.Int);
                sqlParams[2].Value = vStatus;

                SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_UpdateAuctionStatus", sqlParams);
            }
        }

        public static void UpdateAuctionStatus(string connstring, string vAuctionRefNo, string vPurchasingId, int vStatus, string vComment)
        {
            if ((!String.IsNullOrEmpty(vPurchasingId)) && (!String.IsNullOrEmpty(vComment)))
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@AuctionRefNo", SqlDbType.Int);
                sqlParams[0].Value = Int32.Parse(vAuctionRefNo.Trim());
                sqlParams[1] = new SqlParameter("@PurchasingId", SqlDbType.Int);
                sqlParams[1].Value = Int32.Parse(vPurchasingId.Trim());
                sqlParams[2] = new SqlParameter("@Status", SqlDbType.Int);
                sqlParams[2].Value = vStatus;
                sqlParams[3] = new SqlParameter("@Comment", SqlDbType.NVarChar);
                sqlParams[3].Value = vComment;

                SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "sp_UpdateAuctionStatus", sqlParams);
            }
        }

        public static void InsertAuctionParticipants(string connstring, string vAuctionRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@AuctionRefNo", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vAuctionRefNo.Trim());
            
            DataTable dtVendors = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetAuctionItemSupplierID", sqlParams).Tables[0];

			// Modified By: GA S. 06292006
            if (dtVendors.Rows.Count > 0)
            {
                foreach (DataRow row in dtVendors.Rows)
                {
                    SqlParameter[] sqlParams2 = new SqlParameter[3];
                    sqlParams2[0] = new SqlParameter("@AuctionRefNo", SqlDbType.Int);
                    sqlParams2[0].Value = Int32.Parse(vAuctionRefNo.Trim());
                    sqlParams2[1] = new SqlParameter("@VendorId", SqlDbType.Int);
                    sqlParams2[1].Value = Int32.Parse(row["VendorID"].ToString().Trim());
                    sqlParams2[2] = new SqlParameter("@Ticket", SqlDbType.NVarChar);
                    sqlParams2[2].Value = EncryptionHelper.Encrypt(RandomPasswordGenerator.GenerateRandomPassword());

                    SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_InsertAuctionParticipants", sqlParams2);
                }
            }
        }

        public static DataTable QueryConfirmedVendors(string connstring, string orderby, string vAuctionRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@auctionRefNo", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vAuctionRefNo.Trim());

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryConfirmedVendors", sqlParams).Tables[0];
        }

        public static DataTable QueryAllAwardedAuctionItems(string connstring, string auctionEvent)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@auctionEvent", SqlDbType.NVarChar);
            sqlParams[0].Value = auctionEvent;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_Ebid_QueryAllAwardedAuctionItems", sqlParams).Tables[0];
        }

        public static int GetAwardedAuctionsCount(string connstring)
        {
            try
            {
                string query = "SELECT Count(*) as AwardedCnt " +
                               "FROM [tblBidItems] t1, " +
                               "[tblBidItemDetails] t2, " +
                               "[tblBidTenders] t3 " +
                               "WHERE t2.[BidDetailNo] = t3.[BidDetailNo] " +
                               "AND t1.[BidRefNo] = t2.[BidRefNo] " +
                               "AND t1.[ForAuction] = 2 " +
                               "AND t3.[Status] = 4";

                DataSet auctionData = new DataSet();
                SqlParameter[] sqlparam = new SqlParameter[1];
                sqlparam[0] = new SqlParameter("@query", SqlDbType.NText);
                sqlparam[0].Value = query;
                auctionData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_GenericQueryProcedure]", sqlparam);
                DataTable dtTable =  new DataTable();
                if (auctionData.Tables.Count > 0)
                {
                    dtTable = auctionData.Tables[0];
                    foreach (DataRow row in dtTable.Rows)
                        return Int32.Parse(row["AwardedCnt"].ToString());   
                }
            }
            catch
            {
                
            }
            return 0;
        }

        public static DataTable QueryAwardedAuctionItems(string connstring, string company, string category, string month, string day, string year)
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

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_Ebid_QueryAwardedAuctionItem", sqlParams).Tables[0];
        }

        public static string GetAuctionRefNobyBidRefNo(string connstring, string BidRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(BidRefNo);

            return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryConfirmedVendors", sqlParams).ToString().Trim();
        }

        public static void InsertAuctionItemDetail(string connstring, AuctionItemDetail auctionItemDetail)
        {
            
            string query = "INSERT INTO [tblAuctionItemDetails] " +
                "([AuctionRefNo], [Item], [Description], [Quantity], [UnitOfMeasure], [DateCreated], [Status]) " +
                "VALUES (" + auctionItemDetail.AuctionRefNo + ", '" + auctionItemDetail.Item + "', '" + auctionItemDetail.Description + "', " + auctionItemDetail.Quantity + ", '" + auctionItemDetail.UnitOfMeasure + "', '" + auctionItemDetail.DateCreated + "', " + auctionItemDetail.Status + ")";
            SqlParameter[] sqlparam = new SqlParameter[1];
            sqlparam[0] = new SqlParameter("@query", SqlDbType.NText);
            sqlparam[0].Value = query;
            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "[s3p_EBid_GenericQueryProcedure]", sqlparam);
        }

        public static ArrayList GetAuctionParticipants(string connstring, int vAuctionRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@AuctionRefNo", SqlDbType.Int);
            sqlParams[0].Value = vAuctionRefNo;

            DataTable dtParticipants = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "sp_GetAuctionParticipants", sqlParams).Tables[0];
            
            ArrayList suppliersList = new ArrayList();
            
            foreach (DataRow dr in dtParticipants.Rows)
            {
                AuctionParticipant participant = new AuctionParticipant();
                participant.ID = int.Parse(dr["ParticipantId"].ToString());
                participant.Username = dr["Username"].ToString();
                participant.EncryptedTicket = dr["Ticket"].ToString();
                participant.Alias = dr["Alias"].ToString();
                participant.Name = dr["VendorName"].ToString();
                participant.EmailAddress = dr["VendorEmail"].ToString();
                participant.EmailSent = int.Parse(dr["EmailSent"].ToString());
                participant.MobileNo = dr["MobileNo"].ToString();

                suppliersList.Add(participant);
            }
            return suppliersList;
        }
    }
}