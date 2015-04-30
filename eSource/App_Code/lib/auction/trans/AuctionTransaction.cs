using System;
using System.Web;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EBid.lib;
using EBid.ConnectionString;
using EBid.lib.constant;
using EBid.lib.auction.data;


/// <summary>
/// Summary description for AuctionTransaction
/// </summary>
/// 
namespace EBid.lib.auction.trans
{
    public class AuctionTransaction
    {

        public DataTable GetAuctionType(string connstring)
        {
            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetAuctionType").Tables[0];
        }

        public string GetAuctionTypeNameById(string connstring, string vAuctionId)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@auctionId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vAuctionId);

            return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetAuctionTypeNameById", sqlParams).ToString().Trim();
        }

        public DataTable GetApprovedAuctions(string connstring, string vBuyerId, string vOrderBy)
        {

            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@buyerId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vBuyerId);
            sqlParams[1] = new SqlParameter("@status", SqlDbType.Int);
            sqlParams[1].Value = Constant.AUCTION_STATUS_APPROVED;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetApprovedAuctions", sqlParams).Tables[0];
        }

        public string GetApprovedAuctionsCount(string connstring, string vBuyerId)
        {
            string cnt = "0";
            if (vBuyerId != "")
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@buyerId", SqlDbType.Int);
                sqlParams[0].Value = Int32.Parse(vBuyerId);
                sqlParams[1] = new SqlParameter("@status", SqlDbType.Int);
                sqlParams[1].Value = Constant.AUCTION_STATUS_APPROVED;

                cnt = SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetApprovedAuctionsCountByBuyer", sqlParams).ToString().Trim();
            }

            return cnt;
        }

        public DataTable GetRejectedAuctions(string connstring, string vBuyerId, string vOrderBy)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@buyerId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vBuyerId);
            sqlParams[1] = new SqlParameter("@status", SqlDbType.Int);
            sqlParams[1].Value = Constant.AUCTION_STATUS_REJECTED;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetRejectedAuctions", sqlParams).Tables[0];
        }


        public string GetRejectedAuctionsCount(string connstring, string vBuyerId)
        {
            string cnt = "0";

            if (vBuyerId != "")
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@buyerId", SqlDbType.Int);
                sqlParams[0].Value = Int32.Parse(vBuyerId);
                sqlParams[1] = new SqlParameter("@status", SqlDbType.Int);
                sqlParams[1].Value = Constant.AUCTION_STATUS_REJECTED;

                cnt = SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetRejectedAuctionsCount", sqlParams).ToString().Trim();
            }

            return cnt;
        }



        public DataTable GetAuctionDrafts(string connstring, string vBuyerId, string vOrderBy)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@buyerId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vBuyerId);
            sqlParams[1] = new SqlParameter("@status", SqlDbType.Int);
            sqlParams[1].Value = Constant.AUCTION_STATUS_DRAFT;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetAuctionDrafts", sqlParams).Tables[0];
        }

        public string GetAuctionDraftsCount(string connstring, string vBuyerId)
        {
            string cnt = "0";

            if (vBuyerId != "")
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@buyerId", SqlDbType.Int);
                sqlParams[0].Value = Int32.Parse(vBuyerId);
                sqlParams[1] = new SqlParameter("@status", SqlDbType.Int);
                sqlParams[1].Value = Constant.AUCTION_STATUS_DRAFT;

                cnt = SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetAuctionDraftsCount", sqlParams).ToString().Trim();
            }

            return cnt;
        }

        public DataTable GetSubmittedAuctions(string connstring, string vBuyerId, string vOrderBy)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@buyerId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vBuyerId);
            sqlParams[1] = new SqlParameter("@status", SqlDbType.Int);
            sqlParams[1].Value = Constant.AUCTION_STATUS_SUBMITTED;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetSubmittedAuctions", sqlParams).Tables[0];
        }

        public string GetSubmittedAuctionsCount(string connstring, string vBuyerId)
        {
            string cnt = "0";

            if (vBuyerId != "")
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@buyerId", SqlDbType.Int);
                sqlParams[0].Value = Int32.Parse(vBuyerId);
                sqlParams[1] = new SqlParameter("@status", SqlDbType.Int);
                sqlParams[1].Value = Constant.AUCTION_STATUS_SUBMITTED;

                cnt = SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetSubmittedAuctionsCountBuyer", sqlParams).ToString().Trim();
            }

            return cnt;
        }

        public AuctionItem GetAuctionByAuctionRefNo(string connstring, string vAuctionRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@auctionRefNo", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vAuctionRefNo);

            DataTable auctionDataTable = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetAuctionByAuctionRefNo", sqlParams).Tables[0];

            AuctionItem ai = new AuctionItem();
            ai.PRRefNo = Int64.Parse(auctionDataTable.Rows[0]["PRRefNo"].ToString().Trim());
            ai.Requestor = auctionDataTable.Rows[0]["Requestor"].ToString().Trim();
            ai.ItemDescription = auctionDataTable.Rows[0]["ItemDesc"].ToString().Trim();
            ai.GroupDeptSec = Int32.Parse(auctionDataTable.Rows[0]["GroupDeptSec"].ToString().Trim());
            ai.Category = auctionDataTable.Rows[0]["Category"].ToString().Trim();
            ai.SubCategory = auctionDataTable.Rows[0]["SubCategory"].ToString().Trim();
            ai.DeliveryDate = auctionDataTable.Rows[0]["DeliveryDate"].ToString().Trim();
            ai.DeliveryDateMonth = auctionDataTable.Rows[0]["DeliveryDateMonth"].ToString().Trim();
            ai.DeliveryDateYear = auctionDataTable.Rows[0]["DeliveryDateYear"].ToString().Trim();
            ai.DeliveryDateDay = auctionDataTable.Rows[0]["DeliveryDateDay"].ToString().Trim();
            ai.CompanyId = Int32.Parse(auctionDataTable.Rows[0]["CompanyId"].ToString().Trim());
            ai.AuctionType = auctionDataTable.Rows[0]["AuctionType"].ToString().Trim();
            ai.AuctionDeadline = auctionDataTable.Rows[0]["AuctionDeadline"].ToString().Trim();
            ai.AuctionDeadlineMonth = auctionDataTable.Rows[0]["AuctionDeadlineMonth"].ToString().Trim();
            ai.AuctionDeadlineDay = auctionDataTable.Rows[0]["AuctionDeadlineDay"].ToString().Trim();
            ai.AuctionDeadlineYear = auctionDataTable.Rows[0]["AuctionDeadlineYear"].ToString().Trim();

            ai.AuctionStartDate = auctionDataTable.Rows[0]["AuctionStartDate"].ToString().Trim();
            ai.AuctionStartMonth = auctionDataTable.Rows[0]["AuctionStartMonth"].ToString().Trim();
            ai.AuctionStartDay = auctionDataTable.Rows[0]["AuctionStartDay"].ToString().Trim();
            ai.AuctionStartYear = auctionDataTable.Rows[0]["AuctionStartYear"].ToString().Trim();

            ai.AuctionStartTime = auctionDataTable.Rows[0]["AuctionStartDateTime"].ToString().Trim();
            ai.AuctionStartTimeHour = auctionDataTable.Rows[0]["AuctionStartTimeHour"].ToString().Trim();
            ai.AuctionStartTimeMin = auctionDataTable.Rows[0]["AuctionStartTimeMin"].ToString().Trim();
            ai.AuctionStartTimeSec = auctionDataTable.Rows[0]["AuctionStartTimeSec"].ToString().Trim();
            ai.AuctionStartTimeAMPM = auctionDataTable.Rows[0]["AuctionStartTimeAMPM"].ToString().Trim();

            ai.AuctionEndDate = auctionDataTable.Rows[0]["AuctionEndDate"].ToString().Trim();
            ai.AuctionEndMonth = auctionDataTable.Rows[0]["AuctionEndMonth"].ToString().Trim();
            ai.AuctionEndDay = auctionDataTable.Rows[0]["AuctionEndDay"].ToString().Trim();
            ai.AuctionEndYear = auctionDataTable.Rows[0]["AuctionEndYear"].ToString().Trim();

            ai.AuctionEndTime = auctionDataTable.Rows[0]["AuctionEndTime"].ToString().Trim();
            ai.AuctionEndTimeHour = auctionDataTable.Rows[0]["AuctionEndTimeHour"].ToString().Trim();
            ai.AuctionEndTimeMin = auctionDataTable.Rows[0]["AuctionEndTimeMin"].ToString().Trim();
            ai.AuctionEndTimeSec = auctionDataTable.Rows[0]["AuctionEndTimeSec"].ToString().Trim();
            ai.AuctionEndTimeAMPM = auctionDataTable.Rows[0]["AuctionEndTimeAMPM"].ToString().Trim();
            ai.BidCurrency = auctionDataTable.Rows[0]["BidCurrency"].ToString().Trim();
            ai.PRDateMonth = auctionDataTable.Rows[0]["PRDateMonth"].ToString().Trim();
            ai.PRDateDay = auctionDataTable.Rows[0]["PRDateDay"].ToString().Trim();
            ai.PRDateYear = auctionDataTable.Rows[0]["PRDateYear"].ToString().Trim();
            ai.PRDate = auctionDataTable.Rows[0]["PRDate"].ToString().Trim();            
            return ai;
        }

        public AuctionItem GetAuctionFileAttachmentByAuctionRefNo(string connstring, string vAuctionRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@auctionRefNo", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vAuctionRefNo);

            DataTable auctionDataTable = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetAuctionFileAttachmentByAuctionRefNo", sqlParams).Tables[0];

            AuctionItem ai = new AuctionItem();
            ai.FileAttachments = auctionDataTable.Rows[0]["OriginalFileName"].ToString().Trim();
            ai.ActualFileNames = auctionDataTable.Rows[0]["ActualFileName"].ToString().Trim();
            return ai;
        }

        public void UpdateAuctionFileAttachment(string connstring, string vAuctionRefNo, string vFiles)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@auctionRefNo", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vAuctionRefNo);
            sqlParams[1] = new SqlParameter("@files", SqlDbType.Int);
            sqlParams[1].Value = vFiles;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_UpdateAuctionFileAttachment", sqlParams);
        }

        public void SubmitAnAuction(string connstring, string vAuctionRefNo)
        {
            string statement = "UPDATE [tblAuctionItems] " +
                                "SET [Status]= " + Constant.AUCTION_STATUS_SUBMITTED.ToString().Trim() + ", [DateSubmitted]=getdate() " +
                                "WHERE [AuctionRefNo] = " + vAuctionRefNo;
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@query", SqlDbType.NText);
            sqlParams[0].Value = statement;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_GenericQueryProcedure", sqlParams);
        }

        public void SubmitApprovedAuction(string connstring, string vAuctionRefNo)
        {
            string statement = "UPDATE [tblAuctionItems] " +
                                "SET [Status]= " + Constant.AUCTION_STATUS_APPROVED.ToString().Trim() + ", [DateSubmitted]=getdate() " +
                                "WHERE [AuctionRefNo] = " + vAuctionRefNo;
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@query", SqlDbType.NText);
            sqlParams[0].Value = statement;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_GenericQueryProcedure", sqlParams);
        }

        public int InsertAuctionItem(
            string connstring,
            string vPRRefNo,
            string vRequestor,
            string vItemDesc,
            string vBuyerId,
            string vGroupDeptSec,
            string vCategory,
            string vSubCategory,
            string vStatus,
            string vDeliveryDate,
            string vCompanyId,
            string vAuctionType,
            string vAuctionDeadline,
            string vAuctionStartDateTime,
            string vAuctionEndDateTime,
            string vBidCurrency,
            string vBidRefNo,
            string vPRDate,
            ref string vAuctionRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[19];
            sqlParams[0] = new SqlParameter("@PRRefNo", SqlDbType.BigInt);
            sqlParams[0].Value = Int64.Parse(vPRRefNo);
            sqlParams[1] = new SqlParameter("@Requestor", SqlDbType.VarChar);
            sqlParams[1].Value = vRequestor;
            sqlParams[2] = new SqlParameter("@ItemDesc", SqlDbType.VarChar);
            sqlParams[2].Value = vItemDesc;
            sqlParams[3] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[3].Value = Int32.Parse(vBuyerId);
            sqlParams[4] = new SqlParameter("@GroupDeptSec", SqlDbType.Int);
            sqlParams[4].Value = Int32.Parse(vGroupDeptSec);
            sqlParams[5] = new SqlParameter("@Category", SqlDbType.NVarChar);
            sqlParams[5].Value = vCategory;
            sqlParams[6] = new SqlParameter("@SubCategory", SqlDbType.Int);
            if (vSubCategory == "")
                sqlParams[6].Value = System.DBNull.Value;
            else
                sqlParams[6].Value = Int32.Parse(vSubCategory);
            sqlParams[7] = new SqlParameter("@Status", SqlDbType.Int);
            sqlParams[7].Value = Int32.Parse(vStatus);
            sqlParams[8] = new SqlParameter("@DeliveryDate", SqlDbType.DateTime);
            sqlParams[8].Value = DateTime.Parse(vDeliveryDate);
            sqlParams[9] = new SqlParameter("@CompanyId", SqlDbType.Int);
            sqlParams[9].Value = Int32.Parse(vCompanyId);
            sqlParams[10] = new SqlParameter("@AuctionType", SqlDbType.Int);
            sqlParams[10].Value = Int32.Parse(vAuctionType);
            sqlParams[11] = new SqlParameter("@AuctionDeadline", SqlDbType.DateTime);

            if (vAuctionDeadline != string.Empty)
                sqlParams[11].Value = DateTime.Parse(vAuctionDeadline);
            else
                sqlParams[11].Value = DBNull.Value;

            sqlParams[12] = new SqlParameter("@AuctionStartDateTime", SqlDbType.DateTime);

            if (vAuctionDeadline != string.Empty)
                sqlParams[12].Value = DateTime.Parse(vAuctionStartDateTime);
            else
                sqlParams[12].Value = DBNull.Value;
                        
            sqlParams[13] = new SqlParameter("@AuctionEndDateTime", SqlDbType.DateTime);

            if (vAuctionDeadline != string.Empty)
                sqlParams[13].Value = DateTime.Parse(vAuctionEndDateTime);
               
            else
                sqlParams[13].Value = DBNull.Value;
                        
            sqlParams[14] = new SqlParameter("@BidCurrency", SqlDbType.VarChar);
            sqlParams[14].Value = vBidCurrency;
            sqlParams[15] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[15].Value = Int32.Parse(vBidRefNo);
            sqlParams[16] = new SqlParameter("@PRDate", SqlDbType.DateTime);
            sqlParams[16].Value = DateTime.Parse(vPRDate);
            sqlParams[17] = new SqlParameter("@AuctionRefNo", SqlDbType.Int);
            sqlParams[17].Direction = ParameterDirection.Output;
            sqlParams[18] = new SqlParameter("@RetVal", SqlDbType.Int);
            sqlParams[18].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_Ebid_InsertAuction", sqlParams);

            vAuctionRefNo = sqlParams[17].Value.ToString().Trim();

            return Convert.ToInt32(sqlParams[18].Value);
        }


        public int DeleteSupplierForAnAuction(string connstring, string vAuctionRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@AuctionRefNo", SqlDbType.Int);
            sqlParams[0].Value = vAuctionRefNo;
            sqlParams[1] = new SqlParameter("@RetVal", SqlDbType.Int);
            sqlParams[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "[s3p_EBid_DeleteSuppliersInAuctions]", sqlParams);
            return Convert.ToInt32(sqlParams[1].Value.ToString().Trim());
        }

        public DataTable GetSuppliers(string connstring, string vAuctionRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@auctionRefno", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vAuctionRefNo);

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetAuctionItemSuppliers", sqlParams).Tables[0];
        }

        public bool InsertSuppliersForAnAuction(string connstring, string vAuctionRefNo, string vSuppliers)
        {
            string[] SuppliersA = vSuppliers.Split(Convert.ToChar(","));
            bool completed = false;
            int lastindex = 0;
            while (completed == false)
            {
                string strSQL = CreateInsertSuppliersStmt(SuppliersA, vAuctionRefNo, ref lastindex, ref completed);
                //execute strSQL; retval should be 0; no error
                string retVal = InsertSuppliers(connstring, strSQL);
                if (Int32.Parse(retVal) > 0)
                    return false;
            }
            return true;
        }

        private string CreateInsertSuppliersStmt(string[] SuppliersA, string vAuctionRefNo, ref int lastindex, ref bool completed)
        {
            string strSQL = "";

            for (int i = lastindex; i < SuppliersA.Length; i++)
            {
                if (SuppliersA[i] != null)
                {
                    string newStrSQL = "";
                    if (strSQL == "")
                    {
                        strSQL = "INSERT INTO [tblVendorsInAuctions]([AuctionRefNo], [VendorId]) " +
                                    "VALUES(" + vAuctionRefNo + ", " + SuppliersA[i].ToString().Trim() + ")";
                    }
                    else
                    {
                        newStrSQL = "INSERT INTO [tblVendorsInAuctions]([AuctionRefNo], [VendorId]) " +
                                    "VALUES(" + vAuctionRefNo + ", " + SuppliersA[i].ToString().Trim() + ")";
                        if (strSQL.Length + newStrSQL.Length < 8000)
                            strSQL = strSQL + " " + newStrSQL;
                        else
                        {
                            lastindex = i;
                            completed = false;
                            return strSQL;
                        }
                    }
                }
            }
            completed = true;
            return strSQL;
        }

        private string InsertSuppliers(string connstring, string strSQL)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@SQLStatement", SqlDbType.NText);
            sqlParams[0].Value = strSQL;
            sqlParams[1] = new SqlParameter("@RetVal", SqlDbType.Int);
            sqlParams[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "[s3p_EBid_ExecuteSqlStatement]", sqlParams);
            return sqlParams[1].Value.ToString().Trim();
        }

        public int DeleteAuctionDetail(string connstring, string vAuctionRefNo, string vAuctionDetailNos)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@AuctionRefNo", SqlDbType.VarChar);
            sqlParams[0].Value = vAuctionRefNo;
            sqlParams[1] = new SqlParameter("@AuctionDetailNo", SqlDbType.VarChar);
            sqlParams[1].Value = vAuctionDetailNos;
            sqlParams[2] = new SqlParameter("@Result", SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "[s3p_Ebid_DeleteAuctionDetail]", sqlParams);
            return Convert.ToInt32(sqlParams[2].Value.ToString().Trim());
        }


        public int InsertAuctionItemDetails(
            string connstring,
            string vItem,
            string vDescription,
            string vQuantity,
            string vUnitOfMeasure,
            string vStatus,
            string vAuctionRefNo,
            string vCategory,
            string vSubCategory,
            string vStartingPrice,
            string vIncrementDecrement,
            string vForConversion,
            string vBidDetailNo,
            ref string vAuctionDetailNo)
        {

            SqlParameter[] sqlParams = new SqlParameter[14];
            sqlParams[0] = new SqlParameter("@Item", SqlDbType.VarChar);
            sqlParams[0].Value = vItem;
            sqlParams[1] = new SqlParameter("@Description", SqlDbType.VarChar);
            sqlParams[1].Value = vDescription;
            sqlParams[2] = new SqlParameter("@Quantity", SqlDbType.Float);
            sqlParams[2].Value = float.Parse(vQuantity);
            sqlParams[3] = new SqlParameter("@UnitOfMeasure", SqlDbType.VarChar);
            sqlParams[3].Value = vUnitOfMeasure;
            sqlParams[4] = new SqlParameter("@Status", SqlDbType.Int);
            sqlParams[4].Value = vStatus;
            sqlParams[5] = new SqlParameter("@AuctionRefNo", SqlDbType.Int);
            sqlParams[5].Value = vAuctionRefNo;
            sqlParams[6] = new SqlParameter("@CategoryId", SqlDbType.VarChar);
            sqlParams[6].Value = vCategory;
            sqlParams[7] = new SqlParameter("@SubCategoryId", SqlDbType.Int);
            if (vSubCategory == "")
                sqlParams[7].Value = System.DBNull.Value;
            else
                sqlParams[7].Value = vSubCategory;
            sqlParams[8] = new SqlParameter("@StartingPrice", SqlDbType.Float);
            sqlParams[8].Value = float.Parse(vStartingPrice);
            sqlParams[9] = new SqlParameter("@IncrementDecrement", SqlDbType.Float);
            sqlParams[9].Value = float.Parse(vIncrementDecrement);
            sqlParams[10] = new SqlParameter("@ForConversion", SqlDbType.SmallInt);
            sqlParams[10].Value = vForConversion;
            sqlParams[11] = new SqlParameter("@BidDetailNo", SqlDbType.Int);
            sqlParams[11].Value = int.Parse(vBidDetailNo);
            sqlParams[12] = new SqlParameter("@AuctionDetailNo", SqlDbType.Int);
            sqlParams[12].Direction = ParameterDirection.Output;
            sqlParams[13] = new SqlParameter("@RetVal", SqlDbType.Int);
            sqlParams[13].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_InsertAuctionItemDetail", sqlParams);

            vAuctionDetailNo = sqlParams[12].Value.ToString().Trim();

            return Convert.ToInt32(sqlParams[13].Value);
        }

        public int UpdateAuctionItemDetails(
			string connstring,
            string vItem,
            string vDescription,
            string vQuantity,
            string vUnitOfMeasure,
            string vStatus, 
            string vAuctionDetailNo, 
            string vCategory,
            string vSubCategory,
            string vStartingPrice,
            string dIncrementDecrement,
            string vForConversion)
        {

            SqlParameter[] sqlParams = new SqlParameter[12];
            sqlParams[0] = new SqlParameter("@AuctionDetailNo", SqlDbType.Int);
            sqlParams[0].Value = vAuctionDetailNo;
            sqlParams[1] = new SqlParameter("@Item", SqlDbType.VarChar);
            sqlParams[1].Value = vItem;
            sqlParams[2] = new SqlParameter("@Description", SqlDbType.VarChar);
            sqlParams[2].Value = vDescription;
            sqlParams[3] = new SqlParameter("@Quantity", SqlDbType.Float);
            sqlParams[3].Value = float.Parse(vQuantity);
            sqlParams[4] = new SqlParameter("@UnitOfMeasure", SqlDbType.VarChar);
            sqlParams[4].Value = vUnitOfMeasure;
            sqlParams[5] = new SqlParameter("@Status", SqlDbType.Int);
            sqlParams[5].Value = vStatus;
            sqlParams[6] = new SqlParameter("@CategoryId", SqlDbType.VarChar);
            sqlParams[6].Value = vCategory;
            sqlParams[7] = new SqlParameter("@SubCategoryId", SqlDbType.Int);
            if (vSubCategory == "")
                sqlParams[7].Value = System.DBNull.Value;
            else
                sqlParams[7].Value = vSubCategory;
            sqlParams[8] = new SqlParameter("@StartingPrice", SqlDbType.Float);
            sqlParams[8].Value = float.Parse(vStartingPrice);
            sqlParams[9] = new SqlParameter("@IncrementDecrement", SqlDbType.Float);
            sqlParams[9].Value = float.Parse(dIncrementDecrement);
            sqlParams[10] = new SqlParameter("@ForConversion", SqlDbType.SmallInt);
            sqlParams[10].Value = vForConversion;
            sqlParams[11] = new SqlParameter("@RetVal", SqlDbType.Int);
            sqlParams[11].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_UpdateAuctionItemDetail", sqlParams);

            return Convert.ToInt32(sqlParams[11].Value);
        }

        public int UpdateAuctionItem(
            string connstring,
            string vPRRefNo,
            string vRequestor,
            string vItemDesc,
            string vBuyerId,
            string vGroupDeptSec,
            string vCategory,
            string vSubCategory,
            string vStatus,
            string vDeliveryDate,
            string vCompanyId,
            string vAuctionType,
            string vAuctionDeadline,
            string vAuctionStartDateTime,
            string vAuctionEndDateTime,
            string vBidCurrency,
            string vBidRefNo,
            string vPRDate,
           string vAuctionRefNo)
        {

            SqlParameter[] sqlParams = new SqlParameter[19];
            sqlParams[0] = new SqlParameter("@PRRefNo", SqlDbType.BigInt);
            sqlParams[0].Value = Int64.Parse(vPRRefNo);
            sqlParams[1] = new SqlParameter("@Requestor", SqlDbType.VarChar);
            sqlParams[1].Value = vRequestor;
            sqlParams[2] = new SqlParameter("@ItemDesc", SqlDbType.VarChar);
            sqlParams[2].Value = vItemDesc;
            sqlParams[3] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[3].Value = Int32.Parse(vBuyerId);
            sqlParams[4] = new SqlParameter("@GroupDeptSec", SqlDbType.Int);
            sqlParams[4].Value = Int32.Parse(vGroupDeptSec);
            sqlParams[5] = new SqlParameter("@Category", SqlDbType.NVarChar);
            sqlParams[5].Value = vCategory;
            sqlParams[6] = new SqlParameter("@SubCategory", SqlDbType.Int);
            if (vSubCategory == "")
                sqlParams[6].Value = System.DBNull.Value;
            else
                sqlParams[6].Value = Int32.Parse(vSubCategory);
            sqlParams[7] = new SqlParameter("@Status", SqlDbType.Int);
            sqlParams[7].Value = Int32.Parse(vStatus);
            sqlParams[8] = new SqlParameter("@DeliveryDate", SqlDbType.DateTime);
            sqlParams[8].Value = DateTime.Parse(vDeliveryDate);
            sqlParams[9] = new SqlParameter("@CompanyId", SqlDbType.Int);
            sqlParams[9].Value = Int32.Parse(vCompanyId);
            sqlParams[10] = new SqlParameter("@AuctionType", SqlDbType.Int);
            sqlParams[10].Value = Int32.Parse(vAuctionType);
            sqlParams[11] = new SqlParameter("@AuctionDeadline", SqlDbType.DateTime);
            sqlParams[11].Value = DateTime.Parse(vAuctionDeadline);
            sqlParams[12] = new SqlParameter("@AuctionStartDateTime", SqlDbType.DateTime);
            sqlParams[12].Value = DateTime.Parse(vAuctionStartDateTime);
            sqlParams[13] = new SqlParameter("@AuctionEndDateTime", SqlDbType.DateTime);
            sqlParams[13].Value = DateTime.Parse(vAuctionEndDateTime);
            sqlParams[14] = new SqlParameter("@BidCurrency", SqlDbType.VarChar);
            sqlParams[14].Value = vBidCurrency;
            sqlParams[15] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[15].Value = Int32.Parse(vBidRefNo);
            sqlParams[16] = new SqlParameter("@PRDate", SqlDbType.DateTime);
            sqlParams[16].Value = DateTime.Parse(vPRDate);
            sqlParams[17] = new SqlParameter("@AuctionRefNo", SqlDbType.Int);
            sqlParams[17].Value = vAuctionRefNo;
            sqlParams[18] = new SqlParameter("@RetVal", SqlDbType.Int);
            sqlParams[18].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_Ebid_UpdateAuction", sqlParams);

            vAuctionRefNo = sqlParams[17].Value.ToString().Trim();

            return Convert.ToInt32(sqlParams[18].Value);
        }

        public DataTable GetAuctionItems(string connstring, string vAuctionRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@auctionRefno", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vAuctionRefNo);
            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetAuctionItems", sqlParams).Tables[0];
        }

        public string GetAuctionTitle(string connstring, string vAuctionRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@auctionRefno", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vAuctionRefNo);

            return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetAuctionTitle", sqlParams).ToString().Trim();
        }

        public static int DeleteAuctionItem(string connstring, string vAuctionRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@AuctionRefNo", SqlDbType.Int);
            sqlParams[0].Value = vAuctionRefNo;
            sqlParams[1] = new SqlParameter("@RetVal", SqlDbType.Int);
            sqlParams[1].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_Ebid_DeleteAuction", sqlParams);
            return Int32.Parse(sqlParams[1].Value.ToString().Trim());
        }

        public DataTable QueryVendorsThatConfirmed(string connstring, string vAuctionRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@auctionRefNo", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vAuctionRefNo);
            sqlParams[1] = new SqlParameter("@status", SqlDbType.Int);
            sqlParams[1].Value = Constant.AUCTION_PARTICIPANT_STATUS_APPROVED;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryVendorsThatConfirmed", sqlParams).Tables[0];
        }


        public DataTable QueryConfirmedAuctionInvitations(string connstring, string vBuyerId)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@buyerId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vBuyerId);
            sqlParams[1] = new SqlParameter("@status", SqlDbType.Int);
            sqlParams[1].Value = Constant.AUCTION_PARTICIPANT_STATUS_APPROVED;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryConfirmedAuctionInvitations", sqlParams).Tables[0];

        }

        public string QueryCountConfirmedAuctionInvitations(string connstring, string vBuyerId)
        {
            string query = "SELECT count(distinct(p.[AuctionRefNo])) as count " +
                            "FROM [tblAuctionParticipants] p  " +
                            "INNER JOIN [tblAuctionItems] a  " +
                            "ON a.[AuctionRefNo] = p.AuctionRefNo  " +
                            "WHERE p.[STATUS]=" + Constant.AUCTION_PARTICIPANT_STATUS_APPROVED.ToString().Trim() + " AND a.[BuyerId] = " + vBuyerId;


            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@query", SqlDbType.NText);
            sqlParams[0].Value = query;
            DataSet auctionData = new DataSet();
            auctionData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_GenericQueryProcedure]", sqlParams);
            string confirmedinvitationscount = "";
            if (auctionData.Tables.Count > 0)
                confirmedinvitationscount = auctionData.Tables[0].Rows[0]["count"].ToString().Trim();
            return confirmedinvitationscount;
        }

        public int QueryCountFinishedAuctionEvents(string connstring)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@Status", SqlDbType.Int);
            sqlParams[0].Value = Constant.AUCTION_STATUS_APPROVED.ToString().Trim();
            return Convert.ToInt32(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_CountFinishedAuctions", sqlParams));
        }

        public int QueryUpcomingAuctionEvents(string connstring)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@Status", SqlDbType.Int);
            sqlParams[0].Value = Constant.AUCTION_STATUS_APPROVED.ToString().Trim();
            return Convert.ToInt32(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_CountUpcomingAuctions", sqlParams));

        }

        public int QueryOngoingAuctionEvents(string connstring)
        {

            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@Status", SqlDbType.Int);
            sqlParams[0].Value = Constant.AUCTION_STATUS_APPROVED.ToString().Trim();
            return Convert.ToInt32(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_CountOngoingAuctions", sqlParams));

        }

        public void CancelAuction(string connstring, string vAuctionRefNo)
        {
            try
            {
                string statement = "UPDATE [tblAuctionItems] " +
                                "SET [Status]= " + Constant.AUCTION_STATUS_CANCELLED.ToString().Trim() + ",  [DateCancelled]=getdate() " +
                                "WHERE [AuctionRefNo] = " + vAuctionRefNo;

                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@query", SqlDbType.NText);
                sqlParams[0].Value = statement;
                SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "[s3p_EBid_GenericQueryProcedure]", sqlParams);
            }
            catch
            {

            }
        }

        public static bool UpdateStatus(string connstring, int auctionrefno, int vendorid, int task, string ticket)
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);
            SqlTransaction sqlTransact = null;
            bool success = false;

            try
            {
                sqlConnect.Open();
                sqlTransact = sqlConnect.BeginTransaction();

                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@AuctionRefNo", SqlDbType.Int);
                sqlParams[1] = new SqlParameter("@Vendorid", SqlDbType.Int);
                sqlParams[2] = new SqlParameter("@Task", SqlDbType.Int);
                sqlParams[3] = new SqlParameter("@Ticket", SqlDbType.NVarChar);
                sqlParams[0].Value = auctionrefno;
                sqlParams[1].Value = vendorid;
                sqlParams[2].Value = task;
                sqlParams[3].Value = ticket;

                int i = (int)SqlHelper.ExecuteScalar(sqlTransact, CommandType.StoredProcedure, "sp_SetVendorAuctionParticipationStatus", sqlParams);

                sqlTransact.Commit();

                if (i == 1)
                    success = true;
            }
            catch
            {
                sqlTransact.Rollback();
                success = false;
            }
            finally
            {
                sqlConnect.Close();
            }
            return success;
        }
    }
}