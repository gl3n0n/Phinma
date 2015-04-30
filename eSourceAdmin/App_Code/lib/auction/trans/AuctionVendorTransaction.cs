using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EBid.lib;
using EBid.lib.auction.data;
using EBid.lib.constant;
using EBid.lib.utils;

namespace EBid.lib.auction.trans
{
	public class AuctionVendorTransaction
	{
        private static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["EBidConnectionString"].ConnectionString;

		public AuctionVendorTransaction()
		{
		}

		//NOTE: MISSING PA YUNG DATE COMPARISON
		public DataTable GetOngoingAuctions(string vendorId, string orderBy)
		{
			string query = "SELECT [AuctionRefNo], ISNULL([ItemDesc], '') as [ItemDesc], " +
				"ISNULL(DATENAME(month, [AuctionDate]) + ' ' + CONVERT(varchar(2),(DATEPART(dd, [AuctionDate]))) + ', ' + CONVERT(varchar(4),(DATEPART(yyyy, [AuctionDate]))), '') as [AuctionDate], " +
				"ISNULL(DATENAME(month, [AuctionDate]) + ' ' + CONVERT(varchar(2),(DATEPART(dd, [AuctionDate]))) + ', ' + CONVERT(varchar(4),(DATEPART(yyyy, [AuctionDate]))) + ' ' + " +
				"CONVERT(VARCHAR(2),DatePart(hh, [AuctionStartTime])) + ':' + CONVERT(VARCHAR(2),DATEPART(mi, [AuctionStartTime])) + ' ' + " +
				"CASE WHEN DATEPART(HH,[AuctionStartTime]) < 13 THEN 'AM' ELSE 'PM' END,'') AS [AuctionStartTime], " +
				"ISNULL(DATENAME(month, [AuctionDate]) + ' ' + CONVERT(varchar(2),(DATEPART(dd, [AuctionDate]))) + ', ' + CONVERT(varchar(4),(DATEPART(yyyy, [AuctionDate]))) + ' ' + " +
				"CONVERT(VARCHAR(2),DatePart(hh, [AuctionEndTime])) + ':' + CONVERT(VARCHAR(2),DATEPART(mi, [AuctionEndTime])) + ' ' + " +
				"CASE WHEN DATEPART(HH,[AuctionEndTime]) < 13 THEN 'AM' ELSE 'PM' END,'') AS [AuctionEndTime] " +
                "FROM [tblAuctionItems] WHERE [Status]=" + Constant.AUCTION_STATUS_APPROVED + " AND ([Suppliers] LIKE '" + vendorId + "' OR [Suppliers] LIKE '" + vendorId + ",%' OR  [Suppliers] like '%," + vendorId + ",%' OR [Suppliers] LIKE '%," + vendorId + "') " +
				"ORDER BY [AuctionDate] " + orderBy;

			DataSet dataSet = new DataSet();
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@query", SqlDbType.NText);
            sqlParams[0].Value = query;
            dataSet = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_GenericQueryProcedure]", sqlParams);
			DataTable dataTable = dataSet.Tables[0];

			return dataTable;
		}

		public DataTable GetAuctionItemDetailsByAuctionRefNo(string auctionRefNo)
		{
			string query = "SELECT [AuctionDetailNo], [AuctionRefNo], [Item], [Description], [Quantity], [UnitOfMeasure], [DateCreated], [Status] FROM [tblAuctionItemDetails] WHERE [AuctionRefNo] = " + auctionRefNo;
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@query", SqlDbType.NText);
            sqlParams[0].Value = query;
            DataSet dataSet = new DataSet();
            dataSet = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_GenericQueryProcedure]", sqlParams);
			DataTable dataTable = dataSet.Tables[0];
			return dataTable;
		}

		public string[] GetAuctionParticipantsId(string auctionRefNo)
		{
			string query = "SELECT [Suppliers] FROM [tblAuctionItems] WHERE [AuctionRefNo] = " + auctionRefNo;
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@query", SqlDbType.NText);
            sqlParams[0].Value = query;
			DataSet bidData = new DataSet();
            bidData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_GenericQueryProcedure]", sqlParams);
			string suppliers = bidData.Tables[0].Rows[0]["Suppliers"].ToString().Trim();
			return suppliers.Split(new char[] { ',' });
		}

		public ArrayList GetAuctionItemsDetail(string auctionRefNo)
		{
			ArrayList arrayList = new ArrayList();
			string query = "SELECT [AuctionDetailNo] FROM [tblAuctionItems] A, [tblAuctionItemDetails] B WHERE A.[AuctionRefNo] = B.[AuctionRefNo] AND B.[AuctionRefNo] = " + auctionRefNo;
			SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@query", SqlDbType.NText);
            sqlParams[0].Value = query;
            DataSet dataSet = new DataSet();
            dataSet = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_GenericQueryProcedure]", sqlParams);
            if (dataSet.Tables["tblAuctionItems"].Rows.Count > 0)
			{
				foreach (DataRow row in dataSet.Tables["tblAuctionItems"].Rows)
				{
					arrayList.Add(GetAuctionItemDetail(row["AuctionDetailNo"].ToString()));
				}
			}

			return arrayList;
		}

		public AuctionItemDetail GetAuctionItemDetail(string auctionDetailNo)
		{
			AuctionItemDetail auctionItemDetail = new AuctionItemDetail();
            string query = "SELECT [AuctionDetailNo], [AuctionRefNo], [Item], [Description], [Quantity], [UnitOfMeasure], [DateCreated], [Status], [CategoryId], [SubCategoryId] FROM [tblAuctionItemDetails] WHERE [AuctionDetailNo] = " + auctionDetailNo;
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@query", SqlDbType.NText);
            sqlParams[0].Value = query;
            DataSet dataSet = new DataSet();
            dataSet = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_GenericQueryProcedure]", sqlParams);
			
            if (dataSet.Tables[0].Rows.Count > 0)
			{
				auctionItemDetail.AuctionDetailNo = Int32.Parse(dataSet.Tables[0].Rows[0]["AuctionDetailNo"].ToString());
				auctionItemDetail.AuctionRefNo = Int32.Parse(dataSet.Tables[0].Rows[0]["AuctionRefNo"].ToString());
				auctionItemDetail.CategoryId = dataSet.Tables[0].Rows[0]["CategoryId"].ToString();
				auctionItemDetail.DateCreated = dataSet.Tables[0].Rows[0]["DateCreated"].ToString();
				auctionItemDetail.Description = dataSet.Tables[0].Rows[0]["Description"].ToString();
				auctionItemDetail.Item = dataSet.Tables[0].Rows[0]["Item"].ToString();
				auctionItemDetail.Quantity = Int32.Parse(dataSet.Tables[0].Rows[0]["Quantity"].ToString());
				auctionItemDetail.Status = Int32.Parse(dataSet.Tables[0].Rows[0]["Status"].ToString());
				auctionItemDetail.SubCategoryId = Int32.Parse(dataSet.Tables[0].Rows[0]["SubCategoryId"].ToString());
				auctionItemDetail.UnitOfMeasure = dataSet.Tables[0].Rows[0]["UnitOfMeasure"].ToString();
			}
			return auctionItemDetail;
		}

        public DataTable QueryAuctionEventsByVendorID(string vendorID, string orderby)
        {
            string query = "SELECT t1.[AuctionRefNo], [ItemDesc], " +
                           "ISNULL(DATENAME(month, [AuctionDate]) + ' ' + CONVERT(varchar(2),(DATEPART(dd, [AuctionDate]))) + ', ' + CONVERT(varchar(4),(DATEPART(yyyy, [AuctionDate]))), '') as [AuctionDate1] " +
                           "FROM [tblAuctionItems] t1, " +
                           "[tblAuctionParticipants] t2 " +
                           "WHERE t1.[AuctionRefNo] = t2.[AuctionRefNo] " +
                           "AND t2.Status = " + Constant.AUCTION_PARTICIPANT_STATUS_WAIT + " AND t2.[VendorId] = " + vendorID;

            if (orderby != "")
                query = query + " ORDER BY [AuctionDate] " + orderby; 
            else
                query = query + " ORDER BY [AuctionDate] DESC";

            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@query", SqlDbType.NText);
            sqlParams[0].Value = query; 
            DataSet dataSet = new DataSet();
            dataSet = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_GenericQueryProcedure]", sqlParams);
            DataTable dataTable = dataSet.Tables[0];
            return dataTable;
        }

        public DataTable QueryConfirmedAuctionByVendorID(string vendorID)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@vendorID", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vendorID.Trim());

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryConfirmedAuctionByVendorID", sqlParams).Tables[0];
        }

		public string GetAuctionTenders(int auctionDetailNo, int vendorId)
		{
            string bid = null;
			string query = "SELECT [Bid] FROM [tblAuctionItemTrail] WHERE [AuctionDetailNo]=" + auctionDetailNo + " AND [VendorId]=" + vendorId;
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@query", SqlDbType.NText);
            sqlParams[0].Value = query;
			
			DataSet dataSet = new DataSet();
            dataSet = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_GenericQueryProcedure]", sqlParams);

			if (dataSet.Tables[0].Rows.Count > 0)
			    bid = dataSet.Tables[0].Rows[0]["Bid"].ToString();
			return bid;
		}

		public void InsertAuctionTender(int auctionDetailNo, int vendorId, string tender)
		{
			string statement = "INSERT INTO [tblAuctionItemTrail]([AuctionDetailNo], [VendorId], [Bid], [LastBid]) VALUES(" + auctionDetailNo + ", " + vendorId + ", '" + tender + "', '" + tender + "')";
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@query", SqlDbType.NText);
            sqlParams[0].Value = statement;
            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "[s3p_EBid_GenericQueryProcedure]", sqlParams);
		}

		public void UpdateAuctionTender(int auctionDetailNo, int vendorId, string tender)
		{
			string bids = GetAuctionTenders(auctionDetailNo, vendorId);

			if (bids == null)
			{
				InsertAuctionTender(auctionDetailNo, vendorId, tender);
			}
			else
			{
				string[] arrBids = bids.Split(new char[] {'|'});

				for (int i = 0; i < arrBids.Length; i++)
				{
					if (!arrBids[i].Equals("") && arrBids[i] != null)
					{
                        string statement = "UPDATE [tblAuctionItemTrail] SET [Bid]='" + bids + "|" + tender + "', [LastBid]='" + tender + "' WHERE [AuctionDetailNo]=" + auctionDetailNo + " AND [VendorId]=" + vendorId;
                        SqlParameter[] sqlParams = new SqlParameter[1];
                        sqlParams[0] = new SqlParameter("@query", SqlDbType.NText);
                        sqlParams[0].Value = statement;
                        SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "[s3p_EBid_GenericQueryProcedure]", sqlParams);
					}
				}
			}
		}

        public void UpdateAuctionInvitationStatus(string vAuctionRefNo, string vUser, int vStatus)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@auctionRefNo", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vAuctionRefNo.Trim());
            sqlParams[1] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlParams[1].Value = Int32.Parse(vUser.Trim());
            sqlParams[2] = new SqlParameter("@status", SqlDbType.Int);
            sqlParams[2].Value = vStatus;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_UpdateAuctionInvitationStatus", sqlParams);
        }

        public static string GetNewAuctionEventCountviaVendorId(string vendorId)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlParams[1] = new SqlParameter("@status", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vendorId.Trim());
            sqlParams[1].Value = Constant.AUCTION_PARTICIPANT_STATUS_WAIT;

            return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetNewAuctionEventCountviaVendorId", sqlParams).ToString().Trim();
        }

        public DataTable QueryAwardedVendorAuctionItem(int vendorID, int status)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlParams[0].Value = vendorID;
            DataSet vendorData = new DataSet();
            vendorData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_QueryVendorAuctionItems]", sqlParams);
            DataTable dt = new DataTable();
            if (vendorData.Tables.Count > 0)
                dt = vendorData.Tables[0];
            return dt;
        }

        public DataTable QueryAuctionEventDates(string vendorId)
        {
            string query = "SELECT [AuctionRefNo], [ItemDesc], " +
                           " CONVERT(CHAR(19),[])  " +
                            "FROM [tblAuctionItems] " +
                            "WHERE t1.[AuctionRefNo] = t2.[AuctionRefNo] " +
                            "AND t2.Status = " + Constant.AUCTION_PARTICIPANT_STATUS_WAIT +
                            " AND t2.[VendorId] = " + vendorId;
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@query", SqlDbType.NText);
            sqlParams[0].Value = query;
            DataSet auctionData = new DataSet();
            auctionData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_GenericQueryProcedure]", sqlParams);
            return auctionData.Tables[0];
        }

        
        public DataTable QueryVendorAuctionsViaDate(string vendorId, string status, string date)
        {
            string query =  "SELECT t1.[AuctionRefNo], [ItemDesc], " +
                            "ISNULL(DATENAME(month, [AuctionDate]) + ' ' + CONVERT(varchar(2),(DATEPART(dd, [AuctionDate]))) + ', ' + CONVERT(varchar(4),(DATEPART(yyyy, [AuctionDate]))), '') as [AuctionDate1] " +
                            "FROM [tblAuctionItems] t1, [tblVendorsInAuctions] t2 " +
                            "WHERE t1.[AuctionRefNo] = t2.[AuctionRefNo] " +
                            "AND t1.[Status] = " + status +
                            " AND t2.[VendorID] = " + vendorId +
                            " AND [AuctionDate] = " + date;
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@query", SqlDbType.NText);
            sqlParams[0].Value = query;
            DataSet auctionData = new DataSet();
            auctionData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_GenericQueryProcedure]", sqlParams);
            return auctionData.Tables[0];
        }
	}
}