using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Web;
using System.Web.SessionState;
using EBid.lib;
using EBid.lib.utils;
using EBid.lib.bid.data;
using EBid.lib.constant;

namespace EBid.lib.bid.trans
{
    public class BidItemTransaction
    {
        private static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["EBidConnectionString"].ConnectionString;

        public string GetBidStatus(int status)
        {
            string strStatus = "";

            if (status == Constant.BID_STATUS_APPROVED)
            {
                strStatus = Constant.APPROVED;
            }
            else if (status == Constant.BID_STATUS_DRAFT)
            {
                strStatus = Constant.DRAFT;
            }
            else if (status == Constant.BID_STATUS_RE_EDIT)
            {
                strStatus = Constant.RE_EDIT;
            }
            else if (status == Constant.BID_STATUS_RE_NEGOTIATE)
            {
                strStatus = Constant.RE_NEGOTIATE;
            }
            else if (status == Constant.BID_STATUS_REJECTED)
            {
                strStatus = Constant.REJECTED;
            }
            else if (status == Constant.BID_STATUS_SUBMITTED)
            {
                strStatus = Constant.SUBMITTED;
            }

            return strStatus;
        }

        public BidItem GetBidDetailsByRefNo(string bidRefNo)
        {
            BidItem bidItem = null;

            SqlParameter[] sqlparams = new SqlParameter[1];
            sqlparams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlparams[0].Value = Int32.Parse(bidRefNo);

            DataSet bidData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetBidDetailsByRefNo", sqlparams);

            if (bidData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in bidData.Tables[0].Rows)
                {
                    bidItem = new BidItem();
                    bidItem.PRRefNo = Int32.Parse(row["PRRefNo"].ToString());
                    bidItem.BidRefNo = Int32.Parse(row["BidRefNo"].ToString());
                    bidItem.Category = row["Category"].ToString();
                    bidItem.Deadline = row["Deadline"].ToString();
                    bidItem.DeliveryDate = row["DeliveryDate"].ToString();
                    bidItem.CompanyId = Int32.Parse(row["CompanyId"].ToString());
                    bidItem.DeliverTo = row["DeliverTo"].ToString();
                    bidItem.Incoterm = row["Incoterm"].ToString();
                    bidItem.ItemDescription = row["ItemDesc"].ToString();
                }
            }

            return bidItem;
        }

        public ArrayList GetBidItemDetailsByRefNo(int bidRefNo)
        {
            ArrayList bidArray = new ArrayList();

            try
            {

                SqlParameter[] sp = new SqlParameter[1];
                sp[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
                sp[0].Value = bidRefNo;
                DataSet bidData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_QueryBidItemDetailsByRefNo]", sp);

                if (bidData.Tables[0].Rows.Count > 0)
                {
                    foreach (DataTable table in bidData.Tables)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            BidItemDetail bidDetail = new BidItemDetail();
                            bidDetail.BidDetailNo = Int32.Parse(row["BidDetailNo"].ToString());
                            bidDetail.BidRefNo = Int32.Parse(row["BidRefNo"].ToString());
                            bidDetail.CategoryId = row["CategoryId"].ToString();
                            bidDetail.DeliveryDate = row["DeliveryDate"].ToString();
                            bidDetail.DetailDesc = row["DetailDesc"].ToString();
                            bidDetail.EstItemValue = Double.Parse(row["EstItemValue"].ToString());
                            bidDetail.Item = row["Item"].ToString();
                            bidDetail.Qty = Int32.Parse(row["Qty"].ToString());
                            bidDetail.UnitOfMeasure = row["UnitOfMeasure"].ToString();
                            bidArray.Add(bidDetail);
                        }
                    }
                }

            }
            catch
            {

            }
            return bidArray;
        }

        public BidItem QueryBidInfo(string BidRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(BidRefNo);
            
            DataRow bidDataRow = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryBidInfo", sqlParams).Tables[0].Rows[0];
            BidItem bidItem = new BidItem();

            bidItem.BidRefNo = Int32.Parse(bidDataRow["BidRefNo"].ToString().Trim());
            bidItem.PRRefNo = Int32.Parse(bidDataRow["PRRefNo"].ToString().Trim());
            bidItem.Requestor = bidDataRow["Requestor"].ToString().Trim();
            bidItem.ItemDescription = bidDataRow["ItemDesc"].ToString().Trim();
            bidItem.EstItemValue = Double.Parse(bidDataRow["EstItemValue"].ToString().Trim());
            bidItem.Deadline = bidDataRow["Deadline"].ToString().Trim();
            bidItem.DateCreated = bidDataRow["DateCreated"].ToString().Trim();
            bidItem.BuyerId = Int32.Parse(bidDataRow["BuyerId"].ToString().Trim());
            bidItem.ApprovedBy = Int32.Parse(bidDataRow["ApprovedBy"].ToString().Trim());
            bidItem.GroupDeptSec = Int32.Parse(bidDataRow["GroupDeptSec"].ToString().Trim());
            bidItem.Category = bidDataRow["Category"].ToString().Trim();
            bidItem.ForAuction = Int32.Parse(bidDataRow["ForAuction"].ToString().Trim());
            bidItem.BidStatus = Int32.Parse(bidDataRow["Status"].ToString().Trim());
            bidItem.DeliveryDate = bidDataRow["DeliveryDate"].ToString().Trim();
            bidItem.CompanyId = Int32.Parse(bidDataRow["CompanyId"].ToString().Trim());
            //  bidItem.Suppliers = bidDataRow["Suppliers"].ToString().Trim();
            bidItem.Incoterm = bidDataRow["Incoterm"].ToString().Trim();
            bidItem.PRDate = bidDataRow["PRDate"].ToString().Trim();
            bidItem.DeliverTo = bidDataRow["DeliverTo"].ToString().Trim();

            return bidItem;
        }

        public void UpdateBidStatus(string vBidRefNo, string vStatus)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vBidRefNo);
            sqlParams[1] = new SqlParameter("@status", SqlDbType.Int);
            sqlParams[1].Value = Int32.Parse(vStatus);

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_UpdateBidStatus", sqlParams);
        }

        public int DeleteSupplierForABid(string vBidRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vBidRefNo);
            sqlParams[1] = new SqlParameter("@RetVal", SqlDbType.Int);
            sqlParams[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_DeleteSuppliersInBids", sqlParams);
            return Int32.Parse(sqlParams[1].Value.ToString().Trim());
        }

        public BidItem QueryBidItemInfo(string BidRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(BidRefNo);

            DataTable bidDataTable = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryBidItemInfo", sqlParams).Tables[0];
            BidItem bid = new BidItem();

            if (bidDataTable.Rows.Count > 0)
            {
                DataRow bidDataRow = bidDataTable.Rows[0];
                bid.BidRefNo = Int32.Parse(bidDataRow["BidRefNo"].ToString().Trim());
                bid.PRRefNo = Int32.Parse(bidDataRow["PRRefNo"].ToString().Trim());
                bid.Requestor = bidDataRow["Requestor"].ToString().Trim();
                bid.ItemDescription = bidDataRow["ItemDesc"].ToString().Trim();
                bid.Deadline = bidDataRow["Deadline"].ToString().Trim();
                bid.DeadlineMonth = bidDataRow["DeadlineMonth"].ToString().Trim();
                bid.DeadlineDay = bidDataRow["DeadlineDay"].ToString().Trim();
                bid.DeadlineYear = bidDataRow["DeadlineYear"].ToString().Trim();
                bid.GroupDeptSec = Int32.Parse(bidDataRow["GroupDeptSec"].ToString().Trim());
                bid.Category = bidDataRow["Category"].ToString().Trim();
                bid.SubCategory = Int32.Parse(bidDataRow["SubCategory"].ToString().Trim());
                bid.CompanyId = Int32.Parse(bidDataRow["CompanyId"].ToString().Trim());
                bid.FileAttachments = bidDataRow["FileAttachments"].ToString().Trim();
                bid.ActualFileNames = bidDataRow["ActualFileNames"].ToString().Trim();
                bid.DeliverTo = bidDataRow["DeliverTo"].ToString().Trim();
                bid.Incoterm = bidDataRow["Incoterm"].ToString().Trim();
                bid.PRDate = bidDataRow["PRDate"].ToString().Trim();
                bid.PRDateMonth = bidDataRow["PRDateMonth"].ToString().Trim();
                bid.PRDateDay = bidDataRow["PRDateDay"].ToString().Trim();
                bid.PRDateYear = bidDataRow["PRDateYear"].ToString().Trim();
                bid.BidStatus = Int32.Parse(bidDataRow["Status"].ToString().Trim());
                bid.FormattedEstItemValue = bidDataRow["EstItemValue1"].ToString().Trim();
            }

            //obj.DBClose();
            return bid;
        }

        public BidItem GetFileAttachment(string vBidRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vBidRefNo);

            DataTable bidDataTable = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetFileAttachment", sqlParams).Tables[0];
            BidItem bid = new BidItem();

            if (bidDataTable.Rows.Count > 0)
            {
                DataRow bidDataRow = bidDataTable.Rows[0];
                bid.FileAttachments = bidDataRow["FileAttachments"].ToString().Trim();
                bid.ActualFileNames = bidDataRow["ActualFileNames"].ToString().Trim();
            }
            else
            {
                bid.FileAttachments = "";
                bid.ActualFileNames = "";
            }

            return bid;
        }

        public DataTable GetVendorFileAttachments(int vendorId, int bidRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlParams[0].Value = vendorId;
            sqlParams[1] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlParams[1].Value = bidRefNo;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryVendorFileAttachments", sqlParams).Tables[0];
        }

        public void DeleteRemovedFileAttachment(string files)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@files", SqlDbType.NVarChar);
            sqlParams[0].Value = files;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_DeleteRemovedFiles", sqlParams);
        }

        public DataTable GetSuppliers(string vBidRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vBidRefNo);

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetSuppliers", sqlParams).Tables[0];
        }



        public void UpdateFileAttachment(string vBidRefNo, string newFileAttachments, string actualfilenames)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@NewFileAttachments", SqlDbType.VarChar);
            sqlParams[0].Value = newFileAttachments;
            sqlParams[1] = new SqlParameter("@ActualFileNames", SqlDbType.Int);
            sqlParams[1].Value = actualfilenames;
            sqlParams[2] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[2].Value = actualfilenames;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_UpdateFileAttachments", sqlParams);
        }

        public string InsertBidItem(string vPRRefNo,
            string vRequestor,
            string vItemDesc,
            string vDeadline,
            string vPRDate,
            string vBuyerId,
            string vGroupDeptSec,
            string vCategory,
            string vSubCategory,
            string vStatus,
            string vCompanyId,
            string vForAuction,
            string vFileAttachments,
            string vActualFileNames,
            string vDeliverTo,
            string vIncoterm,
            double vEstItemValue)
        {
            SqlParameter[] sqlParams = new SqlParameter[18];
            sqlParams[0] = new SqlParameter("@PRRefNo", SqlDbType.Int);
            sqlParams[0].Value = vPRRefNo;
            sqlParams[1] = new SqlParameter("@Requestor", SqlDbType.VarChar);
            sqlParams[1].Value = vRequestor;
            sqlParams[2] = new SqlParameter("@ItemDesc", SqlDbType.VarChar);
            sqlParams[2].Value = vItemDesc;
            sqlParams[3] = new SqlParameter("@Deadline", SqlDbType.DateTime);
            sqlParams[3].Value = vDeadline;
            sqlParams[4] = new SqlParameter("@PRDate", SqlDbType.DateTime);
            sqlParams[4].Value = vPRDate;
            sqlParams[5] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[5].Value = vBuyerId;
            sqlParams[6] = new SqlParameter("@GroupDeptSec", SqlDbType.Int);
            sqlParams[6].Value = vGroupDeptSec;
            sqlParams[7] = new SqlParameter("@Category", SqlDbType.NVarChar);
            sqlParams[7].Value = vCategory;
            sqlParams[8] = new SqlParameter("@SubCategory", SqlDbType.Int);
            if (vSubCategory == "")
                sqlParams[8].Value = System.DBNull.Value;
            else
                sqlParams[8].Value = vSubCategory;
            sqlParams[9] = new SqlParameter("@Status", SqlDbType.Int);
            sqlParams[9].Value = vStatus;
            sqlParams[10] = new SqlParameter("@CompanyId", SqlDbType.Int);
            sqlParams[10].Value = vCompanyId;
            sqlParams[11] = new SqlParameter("@ForAuction", SqlDbType.Int);
            sqlParams[11].Value = vForAuction;
            sqlParams[12] = new SqlParameter("@FileAttachments", SqlDbType.VarChar);
            sqlParams[12].Value = vFileAttachments;
            sqlParams[13] = new SqlParameter("@ActualFileNames", SqlDbType.VarChar);
            sqlParams[13].Value = vActualFileNames;
            sqlParams[14] = new SqlParameter("@DeliverTo", SqlDbType.VarChar);
            sqlParams[14].Value = vDeliverTo;
            sqlParams[15] = new SqlParameter("@Incoterm", SqlDbType.Int);
            sqlParams[15].Value = vIncoterm;
            sqlParams[16] = new SqlParameter("@EstItemValue", SqlDbType.Float);
            sqlParams[16].Value = vEstItemValue;
            sqlParams[17] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[17].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "[s3p_Ebid_InsertBid]", sqlParams);
            return sqlParams[17].Value.ToString().Trim();      
        }

        public bool InsertSuppliersForBid(string vBidRefNo, string vSuppliers)
        {
            string[] SuppliersA = vSuppliers.Split(Convert.ToChar(","));
            bool completed = false;
            int lastindex = 0;
            while (completed == false)
            {
                string strSQL = CreateInsertSuppliersStmt(SuppliersA, vBidRefNo, ref lastindex, ref completed);
                //execute strSQL; retval should be 0; no error
                string retVal = InsertSuppliers(strSQL);
                if (Int32.Parse(retVal) > 0)
                    return false;
            }
            return true;
        }

        private string CreateInsertSuppliersStmt(string[] SuppliersA, string vBidRefNo, ref int lastindex, ref bool completed)
        {
            string strSQL = "";

            for (int i = lastindex; i < SuppliersA.Length; i++)
            {
                if (SuppliersA[i] != null)
                {
                    if (SuppliersA[i] != "")
                    {
                        string newStrSQL = "";
                        if (strSQL == "")
                        {
                            strSQL = "INSERT INTO [tblVendorsInBids]([BidRefNo], [VendorId]) " +
                                        "VALUES(" + vBidRefNo + ", " + SuppliersA[i].ToString().Trim() + ")";
                        }
                        else
                        {
                            newStrSQL = "INSERT INTO [tblVendorsInBids]([BidRefNo], [VendorId]) " +
                                        "VALUES(" + vBidRefNo + ", " + SuppliersA[i].ToString().Trim() + ")";
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
            }
            completed = true;
            return strSQL;
        }

        private string InsertSuppliers(string strSQL)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@SQLStatement", SqlDbType.NText);
            sqlParams[0].Value = strSQL;
            sqlParams[1] = new SqlParameter("@retVal", SqlDbType.Int);
            sqlParams[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "[s3p_EBid_ExecuteSqlStatement]", sqlParams);
            return sqlParams[1].Value.ToString().Trim();      
        }


        public string UpdateBidItem(string vPRRefNo,
            string vRequestor,
            string vItemDesc,
            string vDeadline,
            string vPRDate,
            string vBuyerId,
            string vGroupDeptSec,
            string vCategory,
            string vSubCategory,
            string vStatus,
            string vCompanyId,
            string vForAuction,
            string vFileAttachments,
            string vActualFileNames,
            string vDeliverTo,
            string vIncoterm,
            double vEstItemValue,
            string vBidRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[19];
            sqlParams[0] = new SqlParameter("@PRRefNo", SqlDbType.Int);
            sqlParams[0].Value = vPRRefNo;
            sqlParams[1] = new SqlParameter("@Requestor", SqlDbType.VarChar);
            sqlParams[1].Value = vRequestor;
            sqlParams[2] = new SqlParameter("@ItemDesc", SqlDbType.VarChar);
            sqlParams[2].Value = vItemDesc;
            sqlParams[3] = new SqlParameter("@Deadline", SqlDbType.DateTime);
            sqlParams[3].Value = vDeadline;
            sqlParams[4] = new SqlParameter("@PRDate", SqlDbType.DateTime);
            sqlParams[4].Value = vPRDate;
            sqlParams[5] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[5].Value = vBuyerId;
            sqlParams[6] = new SqlParameter("@GroupDeptSec", SqlDbType.Int);
            sqlParams[6].Value = vGroupDeptSec;
            sqlParams[7] = new SqlParameter("@Category", SqlDbType.NVarChar);
            sqlParams[7].Value = vCategory;
            sqlParams[8] = new SqlParameter("@SubCategory", SqlDbType.Int);
            if (vSubCategory=="")
                sqlParams[8].Value = System.DBNull.Value;
            else
                sqlParams[8].Value = vSubCategory;
            sqlParams[9] = new SqlParameter("@Status", SqlDbType.Int);
            sqlParams[9].Value = vStatus;
            sqlParams[10] = new SqlParameter("@CompanyId", SqlDbType.Int);
            sqlParams[10].Value = vCompanyId;
            sqlParams[11] = new SqlParameter("@ForAuction", SqlDbType.Int);
            sqlParams[11].Value = vForAuction;
            sqlParams[12] = new SqlParameter("@FileAttachments", SqlDbType.VarChar);
            sqlParams[12].Value = vFileAttachments;
            sqlParams[13] = new SqlParameter("@ActualFileNames", SqlDbType.VarChar);
            sqlParams[13].Value =vActualFileNames;
            sqlParams[14] = new SqlParameter("@DeliverTo", SqlDbType.VarChar);
            sqlParams[14].Value = vDeliverTo;
            sqlParams[15] = new SqlParameter("@Incoterm", SqlDbType.Int);
            sqlParams[15].Value = vIncoterm;
            sqlParams[16] = new SqlParameter("@EstItemValue", SqlDbType.Float);
            sqlParams[16].Value = vEstItemValue;
            sqlParams[17] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[17].Value = vBidRefNo;
            sqlParams[18] = new SqlParameter("@Result", SqlDbType.Int);
            sqlParams[18].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "[s3p_Ebid_UpdateBid]", sqlParams);
            return sqlParams[18].Value.ToString().Trim();   
        }

        public string DeleteBidItem(
            string vBidRefNo)
        {

            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[0].Value = vBidRefNo;
            sqlParams[1] = new SqlParameter("@RetVal", SqlDbType.Int);
            sqlParams[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "[s3p_Ebid_DeleteBid]", sqlParams);
            return sqlParams[1].Value.ToString().Trim();    
        }

        public string QueryCountApproved(string vBuyerId)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@Bid_Status_Approved", SqlDbType.Int);
            sqlParams[0].Value = Constant.BID_STATUS_APPROVED.ToString().Trim();
            sqlParams[1] = new SqlParameter("@Bid_Status_Bid_Item", SqlDbType.Int);
            sqlParams[1].Value = Constant.BID_STATUS_BID_ITEM.ToString().Trim();
            sqlParams[2] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[2].Value = vBuyerId.Trim();
            return Convert.ToString(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "[s3p_EBid_QueryCountApproved]", sqlParams));
        }

        public string QueryCountAllApproved()
        {

            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@BID_STATUS_APPROVED", SqlDbType.Int);
            sqlParams[0].Value = Constant.BID_STATUS_APPROVED.ToString().Trim();
            sqlParams[1] = new SqlParameter("@BID_STATUS_BID_ITEM", SqlDbType.Int);
            sqlParams[1].Value = Constant.BID_STATUS_BID_ITEM.ToString().Trim();

            return Convert.ToString(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "[s3p_EBid_QueryCountAllApproved]", sqlParams));
        }

        public DataTable QueryBidsForEvaluationAndEndorsement(string vBuyerId, string vForAuction, string vBidStatusApproved, string vBidTenderStatus, string vBidStatusRenegotiated, ref string vCount)
        {

            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[0].Value = vBuyerId;
            sqlParams[1] = new SqlParameter("@ForAuction", SqlDbType.Int);
            sqlParams[1].Value = vForAuction;
            sqlParams[2] = new SqlParameter("@BidStatusApproved", SqlDbType.Int);
            sqlParams[2].Value = vBidStatusApproved;
            sqlParams[3] = new SqlParameter("@BidTenderStatus", SqlDbType.Int);
            sqlParams[3].Value = vBidTenderStatus;
            sqlParams[4] = new SqlParameter("@BidStatusRenegotiated", SqlDbType.Int);
            sqlParams[4].Value = vBidStatusRenegotiated;

            DataSet ds = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryBidsForEvaluationAndEndorsement", sqlParams);
            DataTable dt = new DataTable();
            if (ds.Tables.Count > 0)
                dt = ds.Tables[0];

            vCount = dt.Rows.Count.ToString().Trim();
            return dt;
        }

        public string QueryCountReedit(string vBuyerId)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@BID_STATUS_RE_EDIT", SqlDbType.Int);
            sqlParams[0].Value = Constant.BID_STATUS_RE_EDIT.ToString().Trim();
            sqlParams[1] = new SqlParameter("@BID_STATUS_BID_ITEM", SqlDbType.Int);
            sqlParams[1].Value = Constant.BID_STATUS_BID_ITEM.ToString().Trim();
            sqlParams[2] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[2].Value = vBuyerId.Trim();
            return Convert.ToString(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "[s3p_EBid_QueryCountReeditByBuyerId]", sqlParams));
        }

        public DataTable QueryBidsForReedit(string vUserId, string orderby)
        {

            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@BID_STATUS_RE_EDIT", SqlDbType.Int);
            sqlParams[0].Value = Constant.BID_STATUS_RE_EDIT.ToString().Trim();
            sqlParams[1] = new SqlParameter("@BID_STATUS_BID_ITEM", SqlDbType.Int);
            sqlParams[1].Value = Constant.BID_STATUS_BID_ITEM.ToString().Trim();
            sqlParams[2] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[2].Value = vUserId;

            DataSet ds = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryBidsForReedit", sqlParams);
            DataTable dt = new DataTable();
            if (ds.Tables.Count > 0)
                dt = ds.Tables[0];
            return dt;
            
        }


        public DataTable QueryAllBidsForReedit(string orderby)
        {
            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryAllBidsForReedit").Tables[0];
        }

        
        public DataTable QueryAllApprovedBids(string orderby)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@status", SqlDbType.Int);
            sqlParams[0].Value = Constant.BID_STATUS_APPROVED;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryAllApprovedBids", sqlParams).Tables[0];
        }

        

        public BidItem QueryFactsAboutABidItemByBidRefNo(string BidRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(BidRefNo);

            DataSet bidData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryBidItemFacts", sqlParams);
 
            BidItem bid = new BidItem();

            if (bidData.Tables[0].Rows.Count > 0)
            {
                DataRow row = bidData.Tables[0].Rows[0];
                bid.BidRefNo = Int32.Parse(row["BidRefNo"].ToString().Trim());
                bid.PRRefNo = Int32.Parse(row["PRRefNo"].ToString().Trim());
                bid.ItemDescription = row["ItemDesc"].ToString().Trim();
                bid.Category = row["Category"].ToString().Trim();
                bid.Deadline = row["Deadline"].ToString().Trim();
                bid.DeliveryDate = row["DeliveryDate"].ToString().Trim();
                bid.DeliverTo = row["DeliverTo"].ToString().Trim();
            }

            return bid;
        }

        
        public DataTable GetBidItemsForConversion(string strOrderBy)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@status", SqlDbType.Int);
            sqlParams[0].Value = Constant.BID_STATUS_REQUEST_TO_CONVERT;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_UpdateBidStatus", sqlParams).Tables[0];
        }

        public string GetBidName(string vBidRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vBidRefNo);

            return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetBidName", sqlParams).ToString().Trim();
        }

        
        public int UpdateBidForAuctionStatus(string vBidRefNo, int vStatus)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@ForAuction", SqlDbType.Int);
            sqlParams[0].Value = vStatus;
            sqlParams[1] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlParams[1].Value = Int32.Parse(vBidRefNo);
            sqlParams[2] = new SqlParameter("@Result", SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_Ebid_UpdateBidAuctionStatus", sqlParams);

            return Convert.ToInt32(sqlParams[2].Value);
        }

       
        public DataTable QueryAwardedBidTenders(string vVendorId, string vOrderBy)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vVendorId);

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryAwardedBidTenders", sqlParams).Tables[0];
        }

        public DataTable QuerySearchedItemByItemDesc(int queryType, string queryString, int userType, int userId)
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);
            DataSet dsQueryResult;

            sqlConnect.Open();

            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@userType", SqlDbType.Int);
            sqlParams[1] = new SqlParameter("@query", SqlDbType.NVarChar);
            sqlParams[2] = new SqlParameter("@queryType", SqlDbType.Int);
            sqlParams[3] = new SqlParameter("@userId", SqlDbType.Int);
            sqlParams[0].Value = userType;
            sqlParams[1].Value = queryString;
            sqlParams[2].Value = queryType;
            sqlParams[3].Value = userId;

            dsQueryResult = SqlHelper.ExecuteDataset(sqlConnect, "s3p_EBid_SearchAuctionAndBids", sqlParams);

            sqlConnect.Close();

            return dsQueryResult.Tables[0];
        }



        public DataTable QueryDrafts(string vBuyerId, string orderby)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@BID_STATUS_DRAFT", SqlDbType.Int);
            sqlParams[0].Value = Constant.BID_STATUS_DRAFT.ToString().Trim();
            sqlParams[1] = new SqlParameter("@BID_STATUS_BID_ITEM", SqlDbType.Int);
            sqlParams[1].Value = Constant.BID_STATUS_BID_ITEM.ToString().Trim();
            sqlParams[2] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[2].Value = Int32.Parse(vBuyerId);

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryDrafts", sqlParams).Tables[0];
        }


        public DataTable QueryApprovedBids(string vBuyerId, string orderby)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@BID_STATUS_APPROVED", SqlDbType.Int);
            sqlParams[0].Value = Constant.BID_STATUS_APPROVED.ToString().Trim();
            sqlParams[1] = new SqlParameter("@BID_STATUS_BID_ITEM", SqlDbType.Int);
            sqlParams[1].Value = Constant.BID_STATUS_BID_ITEM.ToString().Trim();
            sqlParams[2] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[2].Value = Int32.Parse(vBuyerId);

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryApprovedBids", sqlParams).Tables[0];
        }

        public DataTable QuerySubmittedBids(string vBuyerId, string orderby)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@BID_STATUS_SUBMITTED", SqlDbType.Int);
            sqlParams[0].Value = Constant.BID_STATUS_SUBMITTED.ToString().Trim();
            sqlParams[1] = new SqlParameter("@BID_STATUS_BID_ITEM", SqlDbType.Int);
            sqlParams[1].Value = Constant.BID_STATUS_BID_ITEM.ToString().Trim();
            sqlParams[2] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[2].Value = Int32.Parse(vBuyerId);

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QuerySubmittedBids", sqlParams).Tables[0];
        }


        public DataTable QueryAllSubmittedBids(string orderby)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@BID_STATUS_SUBMITTED", SqlDbType.Int);
            sqlParams[0].Value = Constant.BID_STATUS_SUBMITTED.ToString().Trim();
            sqlParams[1] = new SqlParameter("@BID_STATUS_BID_ITEM", SqlDbType.Int);
            sqlParams[1].Value = Constant.BID_STATUS_BID_ITEM.ToString().Trim();
            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryAllSubmittedBids", sqlParams).Tables[0];
        }

        public DataTable GetSuppliersByBidRefNo(string vBidRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[0].Value = vBidRefNo;
            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetSuppliersByBidRefNo", sqlParams).Tables[0];
        }

        public DataTable QueryDeniedBidConversions(string vBuyerId, string vOrderBy)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@BID_STATUS_DENIED_FOR_CONVERSION", SqlDbType.Int);
            sqlParams[0].Value = Constant.BID_STATUS_DENIED_FOR_CONVERSION.ToString().Trim();
            sqlParams[1] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[1].Value = vBuyerId;
            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryDeniedBidConversions", sqlParams).Tables[0];
        }

        public DataTable QueryRejectedBidItems(string vBuyerId, string vOrderBy)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@BID_STATUS_REJECTED", SqlDbType.Int);
            sqlParams[0].Value = Constant.BID_STATUS_REJECTED.ToString().Trim();
            sqlParams[1] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[1].Value = vBuyerId;
            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryRejectedBidItems", sqlParams).Tables[0];
        }

        public DataTable QueryBidEventsForConversion(string vBuyerId, string vOrderBy)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@BID_STATUS_REQUEST_TO_CONVERT", SqlDbType.Int);
            sqlParams[0].Value = Constant.BID_STATUS_REQUEST_TO_CONVERT.ToString().Trim();
            sqlParams[1] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[1].Value = vBuyerId;
            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryBidEventsForConversion", sqlParams).Tables[0];
            
        }

        public DataTable QueryAwardedBidItems(string vBuyerId, string vOrderBy)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@BID_TENDER_STATUS_AWARD", SqlDbType.Int);
            sqlParams[0].Value = Constant.BID_TENDER_STATUS_AWARD.ToString().Trim();
            sqlParams[1] = new SqlParameter("@BID_STATUS_BID_ITEM", SqlDbType.Int);
            sqlParams[1].Value = Constant.BID_STATUS_BID_ITEM.ToString().Trim();
            sqlParams[2] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[2].Value = Int32.Parse(vBuyerId);

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryAwardedBidItems", sqlParams).Tables[0];
        }

        public DataTable QueryAwardedSuppliers(string vBuyerId, string vBidRefNo, string vOrderBy)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@BID_TENDER_STATUS_AWARD", SqlDbType.Int);
            sqlParams[0].Value = Constant.BID_TENDER_STATUS_AWARD.ToString().Trim();
            sqlParams[1] = new SqlParameter("@BID_STATUS_BID_ITEM", SqlDbType.Int);
            sqlParams[1].Value = Constant.BID_STATUS_BID_ITEM.ToString().Trim();
            sqlParams[2] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[2].Value = vBidRefNo;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryAwardedSuppliers", sqlParams).Tables[0];
        }

       
        public DataTable QueryReceivedTenders(string vBuyerId)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@BID_STATUS_APPROVED", SqlDbType.Int);
            sqlParams[0].Value = Constant.BID_STATUS_APPROVED.ToString().Trim();
            sqlParams[1] = new SqlParameter("@BID_TENDER_STATUS_SUBMITTED", SqlDbType.Int);
            sqlParams[1].Value = Constant.BID_TENDER_STATUS_SUBMITTED.ToString().Trim();
            sqlParams[2] = new SqlParameter("@BID_STATUS_BID_ITEM", SqlDbType.Int);
            sqlParams[2].Value = Constant.BID_STATUS_BID_ITEM.ToString().Trim();
            sqlParams[3] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[3].Value = vBuyerId;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryReceivedTenders_2", sqlParams).Tables[0];
        }

        
        public DataTable QueryRenegotiateBidItems(string vBuyerId, string vOrderBy)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@BID_TENDER_STATUS_WAIT_FOR_RE_NEGOTIATE", SqlDbType.Int);
            sqlParams[0].Value = Constant.BID_TENDER_STATUS_WAIT_FOR_RE_NEGOTIATE.ToString().Trim();
            sqlParams[1] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[1].Value = vBuyerId.Trim();
            sqlParams[2] = new SqlParameter("@BID_STATUS_BID_ITEM", SqlDbType.Int);
            sqlParams[2].Value = Constant.BID_STATUS_BID_ITEM.ToString().Trim();

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryRenegotiateBidItems", sqlParams).Tables[0];
        }

        public DataTable QueryRenegotiateBidItemsSent(string vBuyerId)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@BID_TENDER_STATUS_WAIT_FOR_RE_NEGOTIATE", SqlDbType.Int);
            sqlParams[0].Value = Constant.BID_TENDER_STATUS_WAIT_FOR_RE_NEGOTIATE.ToString().Trim();
            sqlParams[1] = new SqlParameter("@BID_STATUS_BID_ITEM", SqlDbType.Int);
            sqlParams[1].Value = Constant.BID_STATUS_BID_ITEM.ToString().Trim();
            sqlParams[2] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[2].Value = vBuyerId.Trim();

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryRenegotiateBidItemsSent", sqlParams).Tables[0];
        }

        public DataTable QueryEndorsedBidTendersItems(string vBuyerId)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@BID_TENDER_STATUS_ENDORSED", SqlDbType.Int);
            sqlParams[0].Value = Constant.BID_TENDER_STATUS_ENDORSED.ToString().Trim();
            sqlParams[1] = new SqlParameter("@BID_STATUS_BID_ITEM", SqlDbType.Int);
            sqlParams[1].Value = Constant.BID_STATUS_BID_ITEM.ToString().Trim();
            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryEndorsedBidTendersItems", sqlParams).Tables[0];
        }


        public string QueryCountDrafts(string vBuyerId)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@BID_STATUS_DRAFT", SqlDbType.Int);
            sqlParams[0].Value = Constant.BID_STATUS_DRAFT.ToString().Trim();
            sqlParams[1] = new SqlParameter("@BID_STATUS_BID_ITEM", SqlDbType.Int);
            sqlParams[1].Value = Constant.BID_STATUS_BID_ITEM.ToString().Trim();
            sqlParams[2] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[2].Value = vBuyerId.Trim();
            return Convert.ToString(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "[s3p_EBid_QueryCountDrafts]", sqlParams));
        }

        public string QueryCountRejectedBidEvents(string vBuyerId)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@BID_STATUS_REJECTED", SqlDbType.Int);
            sqlParams[0].Value = Constant.BID_STATUS_REJECTED.ToString().Trim();
            sqlParams[1] = new SqlParameter("@TRUE", SqlDbType.Int);
            sqlParams[1].Value = Constant.TRUE.ToString().Trim();
            sqlParams[2] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[2].Value = vBuyerId.Trim();
            return Convert.ToString(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "[s3p_EBid_QueryCountRejectedBidEvents]", sqlParams));
        }


        public int GetSubmittedBidsCount(string vBuyerId)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@BID_STATUS_BID_ITEM", SqlDbType.Int);
            sqlParams[0].Value = Constant.BID_STATUS_BID_ITEM.ToString().Trim();
            sqlParams[1] = new SqlParameter("@BID_STATUS_SUBMITTED", SqlDbType.Int);
            sqlParams[1].Value = Constant.BID_STATUS_SUBMITTED.ToString().Trim();
            sqlParams[2] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[2].Value = vBuyerId.Trim();
            return Convert.ToInt32(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "[s3p_EBid_GetSubmittedBidsCount_1]", sqlParams));
        }


        public string QueryCountBidEventsForConversion(string vBuyerId)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@BID_STATUS_REQUEST_TO_CONVERT", SqlDbType.Int);
            sqlParams[0].Value = Constant.BID_STATUS_REQUEST_TO_CONVERT.ToString().Trim();
            sqlParams[1] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[1].Value = vBuyerId.Trim();
            return Convert.ToString(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "[s3p_EBid_QueryCountBidEventsForConversion]", sqlParams));
        }

        public string QueryCountAwardedBidItems(string vBuyerId)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@BID_TENDER_STATUS_AWARD", SqlDbType.Int);
            sqlParams[0].Value = Constant.BID_TENDER_STATUS_AWARD.ToString().Trim();
            sqlParams[1] = new SqlParameter("@BID_STATUS_BID_ITEM", SqlDbType.Int);
            sqlParams[1].Value = Constant.BID_STATUS_BID_ITEM.ToString().Trim();
            sqlParams[2] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[2].Value = vBuyerId.Trim();
            return Convert.ToString(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "[s3p_EBid_QueryCountAwardedBidItems]", sqlParams));
        }

        public string QueryCountAwardedBidItems(string vBuyerId, string vBidRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@BID_TENDER_STATUS_AWARD", SqlDbType.Int);
            sqlParams[0].Value = Constant.BID_TENDER_STATUS_AWARD.ToString().Trim();
            sqlParams[1] = new SqlParameter("@BID_STATUS_BID_ITEM", SqlDbType.Int);
            sqlParams[1].Value = Constant.BID_STATUS_BID_ITEM.ToString().Trim();
            sqlParams[2] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[2].Value = vBuyerId.Trim();
            sqlParams[3] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[3].Value = vBidRefNo.Trim();
            return Convert.ToString(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "[s3p_EBid_QueryCountAwardedBidItems1]", sqlParams));
        }

        public string QueryCountForRenegotiateBidItems(string vBuyerId)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@BID_TENDER_STATUS_WAIT_FOR_RE_NEGOTIATE", SqlDbType.Int);
            sqlParams[0].Value = Constant.BID_TENDER_STATUS_WAIT_FOR_RE_NEGOTIATE.ToString().Trim();
            sqlParams[1] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[1].Value = vBuyerId.Trim();
            sqlParams[2] = new SqlParameter("@BID_STATUS_BID_ITEM", SqlDbType.Int);
            sqlParams[2].Value = Constant.BID_STATUS_BID_ITEM.ToString().Trim();
            return Convert.ToString(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "[s3p_EBid_QueryCountForRenegotiateBidItems]", sqlParams));
        }

        public string QueryCountReceivedTenders(string vBuyerId)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@BID_TENDER_STATUS_SUBMITTED", SqlDbType.Int);
            sqlParams[0].Value = Constant.BID_TENDER_STATUS_SUBMITTED.ToString().Trim();
            sqlParams[1] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[1].Value = vBuyerId.Trim();
            sqlParams[2] = new SqlParameter("@BID_STATUS_BID_ITEM", SqlDbType.Int);
            sqlParams[2].Value = Constant.BID_STATUS_BID_ITEM.ToString().Trim();
            return Convert.ToString(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "[s3p_EBid_QueryCountReceivedTenders]", sqlParams));
        }

        public void UpdateBidStatusToEndorsed(string vBidRefNo)
        {

            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@BID_STATUS_ENDORSED", SqlDbType.Int);
            sqlParams[0].Value = Constant.BID_STATUS_ENDORSED.ToString().Trim();
            sqlParams[1] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[1].Value = Int32.Parse(vBidRefNo);
            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "[s3p_EBid_UpdateBidStatusToEndorsed]", sqlParams);
        }

        public void ConvertBidToAuction(string vBidRefNo, string vStatus)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@Status", SqlDbType.Int);
            sqlParams[0].Value = vStatus;
            sqlParams[1] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[1].Value = Int32.Parse(vBidRefNo);
            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "[s3p_EBid_ConvertBidToAuction]", sqlParams);
        }
        
    }
}