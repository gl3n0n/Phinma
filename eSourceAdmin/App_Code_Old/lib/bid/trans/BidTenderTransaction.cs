using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using EBid.lib;
using EBid.lib.bid.data;
using EBid.lib.constant;
using EBid.lib.utils;

namespace EBid.lib.bid.trans
{
    public class BidTenderTransaction
    {
        private static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["EBidConnectionString"].ConnectionString;

        public ArrayList GetBidItemTenders(string refNo, int userId)
        {
            ArrayList arrBidItemTenders = new ArrayList();

            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@refNo", SqlDbType.Int);
            sqlparams[0].Value = Int32.Parse(refNo);
            sqlparams[1] = new SqlParameter("@userId", SqlDbType.Int);
            sqlparams[1].Value = userId;

            DataSet bidData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetBidTendersNos", sqlparams);

            if (bidData.Tables[0].Rows.Count > 0)
            {
                BidTender bidTender = new BidTender();
                foreach (DataTable table in bidData.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        arrBidItemTenders.Add(GetBidTender(Int32.Parse(row["BidTenderNo"].ToString())));
                    }
                }
            }

            return arrBidItemTenders;
        }

        public void ConfirmBidTender(string vBidRefNo, string vPassword, string vVendorId)
        {
            SqlParameter[] sqlparams = new SqlParameter[4];
            sqlparams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlparams[0].Value = Int32.Parse(vBidRefNo);
            sqlparams[1] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlparams[1].Value = Int32.Parse(vVendorId);
            sqlparams[2] = new SqlParameter("@status", SqlDbType.Int);
            sqlparams[2].Value = Constant.TRUE;
            sqlparams[3] = new SqlParameter("@password", SqlDbType.NVarChar);
            sqlparams[3].Value = EncryptionHelper.Encrypt(vPassword);

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_ConfirmBidTender", sqlparams);
        }

        public void UpdateBidTenderStatus(string bidTenderNo, int status)
        {
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@bidTenderNo", SqlDbType.Int);
            sqlparams[0].Value = Int32.Parse(bidTenderNo);
            sqlparams[1] = new SqlParameter("@status", SqlDbType.Int);
            sqlparams[1].Value = status;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_UpdateVendBidTenderStatus", sqlparams);
        }

        public BidTenderGeneral GetBidTenderGeneral(string refNo, int vendorId)
        {
            BidTenderGeneral bidTenderGeneral = new BidTenderGeneral();

            ArrayList arrBidRefNo = new ArrayList();

            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@refNo", SqlDbType.Int);
            sqlparams[0].Value = Int32.Parse(refNo);
            sqlparams[1] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlparams[1].Value = vendorId;

            DataSet bidData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetBidTendersPassword", sqlparams);

            if (bidData.Tables[0].Rows.Count > 0)
            {
                foreach (DataTable table in bidData.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        bidTenderGeneral.Password = row["Password"].ToString();
                    }
                }
            }

            return bidTenderGeneral;
        }

        public string InsertBidTenderToDataBase(string vBidDetailNo,
                        string vAmount, string vTenderDate, string vVendorId, string vTotalItemCost)
        {
            SqlParameter[] sqlparams = new SqlParameter[7];
            sqlparams[0] = new SqlParameter("@BidDetailNo", SqlDbType.Int);
            sqlparams[0].Value = Int32.Parse(vBidDetailNo);
            sqlparams[1] = new SqlParameter("@Amount", SqlDbType.Float);
            sqlparams[1].Value = Convert.ToDouble(vAmount);
            sqlparams[2] = new SqlParameter("@TenderDate", SqlDbType.DateTime);
            sqlparams[2].Value = DateTime.Parse(vTenderDate);
            sqlparams[3] = new SqlParameter("@VendorId", SqlDbType.Int);
            sqlparams[3].Value = Int32.Parse(vVendorId);
            sqlparams[4] = new SqlParameter("@TotalItemCost", SqlDbType.Float);
            sqlparams[4].Value = Convert.ToDouble(vTotalItemCost);
            sqlparams[5] = new SqlParameter("@RetVal", SqlDbType.Int);
            sqlparams[5].Direction = ParameterDirection.Output;
            sqlparams[6] = new SqlParameter("@BidTenderNo", SqlDbType.Int);
            sqlparams[6].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_InsertBidTenderDetails", sqlparams);

            return sqlparams[6].Value.ToString().Trim();
        }

        public int UpdateBidTenderToDataBase(string vBidDetailNo,
                        string vAmount, string vTenderDate, string vVendorId, string vTotalItemCost)
        {
            SqlParameter[] sqlparams = new SqlParameter[6];
            sqlparams[0] = new SqlParameter("@BidDetailNo", SqlDbType.Int);
            sqlparams[0].Value = Int32.Parse(vBidDetailNo);
            sqlparams[1] = new SqlParameter("@Amount", SqlDbType.Float);
            sqlparams[1].Value = Convert.ToDouble(vAmount);
            sqlparams[2] = new SqlParameter("@TenderDate", SqlDbType.DateTime);
            sqlparams[2].Value = DateTime.Parse(vTenderDate);
            sqlparams[3] = new SqlParameter("@VendorId", SqlDbType.Int);
            sqlparams[3].Value = Int32.Parse(vVendorId);
            sqlparams[4] = new SqlParameter("@TotalItemCost", SqlDbType.Float);
            sqlparams[4].Value = Convert.ToDouble(vTotalItemCost);
            sqlparams[5] = new SqlParameter("@RetVal", SqlDbType.Int);
            sqlparams[5].Direction = ParameterDirection.Output;
            
            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_UpdateBidTenderDetails", sqlparams);

            return Int32.Parse(sqlparams[5].Value.ToString().Trim());
        }

        

        public void MarkIf2MAbove(string vBidRefNo,
                        string vVendorId)
        {
            SqlParameter[] sqlparams = new SqlParameter[3];
            sqlparams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlparams[0].Value = Int32.Parse(vBidRefNo);
            sqlparams[1] = new SqlParameter("@VendorId", SqlDbType.Int);
            sqlparams[1].Value = Int32.Parse(vVendorId);
            sqlparams[2] = new SqlParameter("@RetVal", SqlDbType.Int);
            sqlparams[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_CheckIf2MAbove", sqlparams);

        }



        public int InsertBidTenderGeneralToDataBase(string vBidRefNo, string vVendorId, string vCurrency, string vDiscount, string vTotalCost, string vDeliveryCost, string vTotalExtendedCost, string vIncoterm, string vPaymentTerms, string vWarranty, string vRemarks, string vStatus)
        {
            SqlParameter[] sqlparams = new SqlParameter[13];
            sqlparams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlparams[0].Value = Int32.Parse(vBidRefNo);
            sqlparams[1] = new SqlParameter("@VendorId", SqlDbType.Int);
            sqlparams[1].Value = Int32.Parse(vVendorId);
            sqlparams[2] = new SqlParameter("@Currency", SqlDbType.VarChar);
            sqlparams[2].Value = vCurrency;
            sqlparams[3] = new SqlParameter("@Discount", SqlDbType.Float);
            sqlparams[3].Value = float.Parse(vDiscount);
            sqlparams[4] = new SqlParameter("@TotalCost", SqlDbType.Float);
            sqlparams[4].Value = float.Parse(vTotalCost);
            sqlparams[5] = new SqlParameter("@DeliveryCost", SqlDbType.Float);
            sqlparams[5].Value = float.Parse(vDeliveryCost);
            sqlparams[6] = new SqlParameter("@TotalExtendedCost", SqlDbType.Float);
            sqlparams[6].Value = float.Parse(vTotalExtendedCost);
            sqlparams[7] = new SqlParameter("@Incoterm", SqlDbType.VarChar);
            sqlparams[7].Value = vIncoterm;
            sqlparams[8] = new SqlParameter("@PaymentTerms", SqlDbType.VarChar);
            sqlparams[8].Value = vPaymentTerms;
            sqlparams[9] = new SqlParameter("@Warranty", SqlDbType.VarChar);
            sqlparams[9].Value = vWarranty;
            sqlparams[10] = new SqlParameter("@Remarks", SqlDbType.VarChar);
            sqlparams[10].Value = vRemarks;
            sqlparams[11] = new SqlParameter("@Status", SqlDbType.Int);
            sqlparams[11].Value = Int32.Parse(vStatus);
            sqlparams[12] = new SqlParameter("@RetVal", SqlDbType.Int);
            sqlparams[12].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_InsertBidTenderGeneral", sqlparams);

            return Convert.ToInt32(sqlparams[12].Value);
        }

        public int UpdateBidTenderGeneralToDataBase(string vBidRefNo,
            string vVendorId,
            string vCurrency,
            string vDiscount,
            string vTotalCost,
            string vDeliveryCost,
            string vTotalExtendedCost,
            string vIncoterm,
            string vPaymentTerms,
            string vWarranty,
            string vRemarks,
            string vStatus)
        {
            
            SqlParameter[] sqlparams = new SqlParameter[13];
            sqlparams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlparams[0].Value = Int32.Parse(vBidRefNo);
            sqlparams[1] = new SqlParameter("@VendorId", SqlDbType.Int);
            sqlparams[1].Value = Int32.Parse(vVendorId);
            sqlparams[2] = new SqlParameter("@Currency", SqlDbType.VarChar);
            sqlparams[2].Value = vCurrency;
            sqlparams[3] = new SqlParameter("@Discount", SqlDbType.Float);
            sqlparams[3].Value = float.Parse(vDiscount);
            sqlparams[4] = new SqlParameter("@TotalCost", SqlDbType.Float);
            sqlparams[4].Value = float.Parse(vTotalCost);
            sqlparams[5] = new SqlParameter("@DeliveryCost", SqlDbType.Float);
            sqlparams[5].Value = float.Parse(vDeliveryCost);
            sqlparams[6] = new SqlParameter("@TotalExtendedCost", SqlDbType.Float);
            sqlparams[6].Value = float.Parse(vTotalExtendedCost);
            sqlparams[7] = new SqlParameter("@Incoterm", SqlDbType.VarChar);
            sqlparams[7].Value = vIncoterm;
            sqlparams[8] = new SqlParameter("@PaymentTerms", SqlDbType.VarChar);
            sqlparams[8].Value = vPaymentTerms;
            sqlparams[9] = new SqlParameter("@Warranty", SqlDbType.VarChar);
            sqlparams[9].Value = vWarranty;
            sqlparams[10] = new SqlParameter("@Remarks", SqlDbType.VarChar);
            sqlparams[10].Value = vRemarks;
            sqlparams[11] = new SqlParameter("@Status", SqlDbType.Int);
            sqlparams[11].Value = Int32.Parse(vStatus);
            sqlparams[12] = new SqlParameter("@RetVal", SqlDbType.Int);
            sqlparams[12].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_UpdateBidTenderGeneral", sqlparams);

            return Convert.ToInt32(sqlparams[12].Value);
        }

        public BidTenderGeneral QueryBidTendersGeneral(string BidRefNo, string VendorId)
        {
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlparams[0].Value = Int32.Parse(BidRefNo);
            sqlparams[1] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlparams[1].Value = Int32.Parse(VendorId);

            DataSet bidData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryBidTendersGeneral", sqlparams);

            BidTenderGeneral bid = new BidTenderGeneral();

            if (bidData.Tables[0].Rows.Count > 0)
            {
                DataRow row = bidData.Tables[0].Rows[0];
                bid.Currency = row["Currency"].ToString().Trim();
                bid.DeliveryCost = (row["DeliveryCost"].ToString().Trim().Length > 0) ? String.Format("{0:f}", float.Parse(row["DeliveryCost"].ToString().Trim())) : "0.00";
                bid.Discount = (row["Discount"].ToString().Trim().Length > 0) ?  String.Format("{0:f}", float.Parse(row["Discount"].ToString().Trim())) : "0.00";
                bid.TotalCost = (row["TotalCost"].ToString().Trim().Length > 0) ? String.Format("{0:f}", float.Parse(row["TotalCost"].ToString().Trim())) : "0.00";
                bid.Incoterm = row["Incoterm"].ToString().Trim();
                bid.PaymentTerms = row["PaymentTerms"].ToString().Trim();
                bid.Warranty = row["Warranty"].ToString().Trim();
                bid.Remarks = row["Remarks"].ToString().Trim();
                bid.TotalExtendedCost = (row["TotalExtendedCost"].ToString().Trim().Length > 0) ? String.Format("{0:f}", float.Parse(row["TotalExtendedCost"].ToString().Trim())) : "0.00";
                bid.Mode_BidTendersGeneral = "Edit";
            }
            else
            {
                bid.Currency = "";
                bid.DeliveryCost = "0.00";
                bid.Discount = "0.00";
                bid.TotalCost = "0.00";
                bid.Incoterm = "";
                bid.PaymentTerms = "";
                bid.Warranty = "";
                bid.Remarks = "";
                bid.TotalExtendedCost = "0.00";
                bid.Mode_BidTendersGeneral = "Add";
            }
            return bid;
        }

        

        public BidTender QueryBidTenderAmount(string BidDetailNo, string VendorId)
        {
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@bidDetailNo", SqlDbType.Int);
            sqlparams[0].Value = Int32.Parse(BidDetailNo);
            sqlparams[1] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlparams[1].Value = Int32.Parse(VendorId);

            DataSet bidData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryBidTenderAmount", sqlparams);

            BidTender bt = new BidTender();

            if (bidData.Tables[0].Rows.Count > 0)
            {
                bt.Amount = Convert.ToDouble(bidData.Tables[0].Rows[0]["TotalItemCost"].ToString().Trim());
                bt.BidTenderNo = Int32.Parse(bidData.Tables[0].Rows[0]["BidTenderNo"].ToString().Trim());
            }
            else
            {
                bt.Amount = 0;
                bt.BidTenderNo = 0;
            }
            return bt;
        }

        public BidTender QueryLowestAndHighestBidTender(string BidDetailNo)
        {
            SqlParameter[] sqlparams = new SqlParameter[1];
            sqlparams[0] = new SqlParameter("@bidDetailNo", SqlDbType.Int);
            sqlparams[0].Value = Int32.Parse(BidDetailNo);

            DataSet bidData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetOrderOfBidTenderAmounts", sqlparams);
            DataTable dtMin = new DataTable();
            DataTable dtMax = new DataTable();
            BidTender bt = new BidTender();
            //initial amount in case no vendor is returned
            bt.VendorWithLowestAmt = "-1";
            bt.VendorWithHighestAmt = "-1";
            if (bidData.Tables.Count > 0)
            {
                dtMin = bidData.Tables[0];
                if (dtMin.Rows.Count > 0)
                    bt.VendorWithLowestAmt = dtMin.Rows[0]["VendorId"].ToString().Trim();
                if (bidData.Tables.Count > 1)
                {
                    dtMax = bidData.Tables[1];
                    if (dtMax.Rows.Count > 0)
                        bt.VendorWithHighestAmt = dtMax.Rows[0]["VendorId"].ToString().Trim();
                }
            }
            return bt;
        }

        public Double GetDiscount(string BidRefNo, string VendorId)
        {
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlparams[0].Value = Int32.Parse(BidRefNo);
            sqlparams[1] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlparams[1].Value = Int32.Parse(VendorId);

            return Convert.ToDouble(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetDiscount", sqlparams));
        }


        public Double GetTotalCost(string BidRefNo, string VendorId)
        {
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlparams[0].Value = Int32.Parse(BidRefNo);
            sqlparams[1] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlparams[1].Value = Int32.Parse(VendorId);

            return Convert.ToDouble(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetTotalCost", sqlparams));
        }

        public Double GetDeliveryCost(string BidRefNo, string VendorId)
        {
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlparams[0].Value = Int32.Parse(BidRefNo);
            sqlparams[1] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlparams[1].Value = Int32.Parse(VendorId);

            return Convert.ToDouble(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetDeliveryCost", sqlparams));
        }

        public Double GetTotalExtendedCost(string BidRefNo, string VendorId)
        {
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlparams[0].Value = Int32.Parse(BidRefNo);
            sqlparams[1] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlparams[1].Value = Int32.Parse(VendorId);

            return Convert.ToDouble(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetTotalExtendedCost", sqlparams));
        }

        public string GetIncoterm(string BidRefNo, string VendorId)
        {
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlparams[0].Value = Int32.Parse(BidRefNo);
            sqlparams[1] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlparams[1].Value = Int32.Parse(VendorId);

            return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetIncoterm", sqlparams).ToString().Trim();
        }

        public string GetPaymentTerms(string BidRefNo, string VendorId)
        {
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlparams[0].Value = Int32.Parse(BidRefNo);
            sqlparams[1] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlparams[1].Value = Int32.Parse(VendorId);

            return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetPaymentTerms", sqlparams).ToString().Trim();
        }

        public string GetWarranty(string BidRefNo, string VendorId)
        {
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlparams[0].Value = Int32.Parse(BidRefNo);
            sqlparams[1] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlparams[1].Value = Int32.Parse(VendorId);

            return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetWarranty", sqlparams).ToString().Trim();
        }

        public string GetRemarks(string BidRefNo, string VendorId)
        {
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlparams[0].Value = Int32.Parse(BidRefNo);
            sqlparams[1] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlparams[1].Value = Int32.Parse(VendorId);

            return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetRemarks", sqlparams).ToString().Trim();
        }

        public DataTable GetBidTenders(string vBidRefNo, string vVendorId, ref int vCount)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[1] = new SqlParameter("@VendorId", SqlDbType.Int);
            sqlParams[2] = new SqlParameter("@count", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vBidRefNo);
            sqlParams[1].Value = Int32.Parse(vVendorId);
            sqlParams[2].Direction = ParameterDirection.Output;

            DataTable dt = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryBidTenderDetailsByBidRefNo", sqlParams).Tables[0];
            vCount = Convert.ToInt32(sqlParams[2].Value);

            return dt;
        }

        public DataTable GetBidTendersForRenegotiation(string vendorId)
        {
            SqlParameter[] sqlparams = new SqlParameter[1];
            sqlparams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlparams[0].Value = Int32.Parse(vendorId);

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetBidTendersForRenegotiation", sqlparams).Tables[0];
        }

        public DataTable GetBidTendersByStatus(string vVendorId, string vStatus)
        {
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlparams[0].Value = Int32.Parse(vVendorId);
            sqlparams[1] = new SqlParameter("@status", SqlDbType.Int);
            sqlparams[1].Value = Int32.Parse(vStatus);

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetBidTendersByStatus", sqlparams).Tables[0];
        }

       

        public void UpdateBidTenderStatus(string vBidRefNo, string vVendorId, string vStatus)
        {
            SqlParameter[] sqlparams = new SqlParameter[3];
            sqlparams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlparams[0].Value = Int32.Parse(vBidRefNo);
            sqlparams[1] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlparams[1].Value = Int32.Parse(vVendorId);
            sqlparams[2] = new SqlParameter("@status", SqlDbType.Int);
            sqlparams[2].Value = Int32.Parse(vStatus);

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_UpdateBidTenderStatus2", sqlparams);
        }

        public DataTable GetAwardedBidTenders(string vBidRefNo, string vVendorId)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[1] = new SqlParameter("@VendorId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vBidRefNo.Trim());
            sqlParams[1].Value = Int32.Parse(vVendorId.Trim());

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryAwardedBidTenderDetailsByBidRefNo", sqlParams).Tables[0];
        }

        public string QueryCountRenegotiations(string vendorId)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@VendorId", SqlDbType.Int);
            sqlParams[1] = new SqlParameter("@Status", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vendorId.Trim());
            sqlParams[1].Value = Constant.BID_TENDER_STATUS_RE_NEGOTIATE;

            return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetVendorRenegotiateCount", sqlParams).ToString().Trim();
        }

        public string GetNewBidEventsCount(int vendorId, int status, int isAuction)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@VendorId", SqlDbType.Int);
            sqlParams[1] = new SqlParameter("@ForAuction", SqlDbType.Int);
            sqlParams[2] = new SqlParameter("@Status", SqlDbType.Int);
            sqlParams[0].Value = vendorId;
            sqlParams[1].Value = isAuction;
            sqlParams[2].Value = status;

            return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetNewBidEventsCount", sqlParams).ToString().Trim();
        }


        public BidTender GetBidTender(int bidTenderNo)
        {
            BidTender bidTender = new BidTender();

            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@BidTenderNo", SqlDbType.Int);
            sqlParams[0].Value = bidTenderNo;

            DataSet ds = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_GetBidTender]", sqlParams);
            DataTable dt = new DataTable();

            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    bidTender.Amount = Double.Parse(row["Amount"].ToString());
                    bidTender.BidDetailNo = Int32.Parse(row["BidDetailNo"].ToString());
                    bidTender.BidItem = row["Item"].ToString();
                    bidTender.BidTenderNo = Int32.Parse(row["BidTenderNo"].ToString());
                    bidTender.DetailDesc = row["DetailDesc"].ToString();
                    bidTender.Status = Int32.Parse(row["Status"].ToString());
                    bidTender.TenderDate = row["TenderDate"].ToString();
                    bidTender.VendorID = Int32.Parse(row["VendorID"].ToString());
                }
                else
                {
                    bidTender.Amount = 0;
                    bidTender.BidDetailNo = -1;
                    bidTender.BidItem = "";
                    bidTender.BidTenderNo = -1;
                    bidTender.DetailDesc = "";
                    bidTender.Status = -1;
                    bidTender.TenderDate = "";
                    bidTender.VendorID = -1;
                }
            }

            else
            {
                bidTender.Amount = 0;
                bidTender.BidDetailNo = -1;
                bidTender.BidItem = "";
                bidTender.BidTenderNo = -1;
                bidTender.DetailDesc = "";
                bidTender.Status = -1;
                bidTender.TenderDate = "";
                bidTender.VendorID = -1;
            }
            return bidTender;
        }


        public ArrayList GetSubmittedBidTendersByUserId(int VendorId)
        {

            ArrayList arrBidItemTenders = new ArrayList();
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@VendorId", SqlDbType.Int);
            sqlParams[0].Value = VendorId;
            sqlParams[1] = new SqlParameter("@BID_STATUS_SUBMITTED", SqlDbType.Int);
            sqlParams[1].Value = Constant.BID_TENDER_STATUS_SUBMITTED.ToString().Trim();

            DataSet ds = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_GetSubmittedBidTendersByUserId]", sqlParams);
            DataTable dt = new DataTable();

            BidItemTransaction bidItemTransaction = new BidItemTransaction();
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        arrBidItemTenders.Add(bidItemTransaction.GetBidDetailsByRefNo(row["BidRefNo"].ToString()));
                    }
                }
            }

            return arrBidItemTenders;  
        }

        public ArrayList GetSubmittedBidTenderDraftsByUserId(int userId)
        {
            ArrayList arrBidItemTenders = new ArrayList();

            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@UserId", SqlDbType.Int);
            sqlParams[0].Value = userId;
            sqlParams[1] = new SqlParameter("@BID_STATUS_DRAFT", SqlDbType.Int);
            sqlParams[1].Value = Constant.BID_STATUS_DRAFT.ToString().Trim();

            DataSet ds = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_GetSubmittedBidTenderDraftsByUserId]", sqlParams);
            DataTable dt = new DataTable();
            BidItemTransaction bidItemTransaction = new BidItemTransaction();

            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    arrBidItemTenders.Add(bidItemTransaction.GetBidDetailsByRefNo(row["BidRefNo"].ToString()));
                }
            }
            return arrBidItemTenders;
        }

        public void SaveBidTender(BidTender bidTender)
        {
            SqlParameter[] sqlparams = new SqlParameter[5];
            sqlparams[0] = new SqlParameter("@BidDetailNo", SqlDbType.Int);
            sqlparams[0].Value = bidTender.BidDetailNo;
            sqlparams[1] = new SqlParameter("@Amount", SqlDbType.Float);
            sqlparams[1].Value = bidTender.Amount;
            sqlparams[2] = new SqlParameter("@TenderDate", SqlDbType.DateTime);
            sqlparams[2].Value = bidTender.TenderDate;
            sqlparams[3] = new SqlParameter("@VendorID", SqlDbType.Int);
            sqlparams[3].Value = bidTender.VendorID;
            sqlparams[4] = new SqlParameter("@Status", SqlDbType.Int);
            sqlparams[4].Value = bidTender.Status;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "[s3p_EBid_SaveBidTender]", sqlparams);
        }

        public void UpdateBidTender(BidTender bidTender)
        {
            SqlParameter[] sqlparams = new SqlParameter[6];
            sqlparams[0] = new SqlParameter("@BidDetailNo", SqlDbType.Int);
            sqlparams[0].Value = bidTender.BidDetailNo;
            sqlparams[1] = new SqlParameter("@Amount", SqlDbType.Float);
            sqlparams[1].Value = bidTender.Amount;
            sqlparams[2] = new SqlParameter("@TenderDate", SqlDbType.DateTime);
            sqlparams[2].Value = bidTender.TenderDate;
            sqlparams[3] = new SqlParameter("@VendorID", SqlDbType.Int);
            sqlparams[3].Value = bidTender.VendorID;
            sqlparams[4] = new SqlParameter("@Status", SqlDbType.Int);
            sqlparams[4].Value = bidTender.Status;
            sqlparams[5] = new SqlParameter("@BidTenderNo", SqlDbType.Int);
            sqlparams[5].Value = bidTender.BidTenderNo;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "[s3p_EBid_UpdateBidTender]", sqlparams);
        }

        public int CountBidTenders(string BidRefNo, string VendorId)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[0].Value = BidRefNo;
            sqlParams[1] = new SqlParameter("@VendorId", SqlDbType.Int);
            sqlParams[1].Value = VendorId;
            
            return Convert.ToInt32(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_CountBidTenders", sqlParams).ToString().Trim());
        }

        public string CountVendorId(string vBidRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[0].Value = vBidRefNo;
            sqlParams[1] = new SqlParameter("@BID_TENDER_STATUS_SUBMITTED", SqlDbType.Int);
            sqlParams[1].Value = Constant.BID_TENDER_STATUS_SUBMITTED.ToString().Trim();

            return Convert.ToString(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "[s3p_EBid_CountVendorId]", sqlParams));
        }

        public DataTable GetEndorsedBidTenders(string vBidTenderNos)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@BidTenderNos", SqlDbType.VarChar);
            sqlParams[0].Value = vBidTenderNos;
            DataSet ds = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_GetEndorsedBidTenders]", sqlParams);
            DataTable dt = new DataTable();
            if (ds.Tables.Count > 0)
                dt = ds.Tables[0];
            return dt;
        }

        public DataTable QueryBidTendersGeneral2(string BidRefNo, string VendorId)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[0].Value = BidRefNo;
            sqlParams[1] = new SqlParameter("@VendorId", SqlDbType.Int);
            sqlParams[1].Value = VendorId;
            DataSet ds = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_QueryBidTendersGeneral2]", sqlParams);
            DataTable dt = new DataTable();
            if (ds.Tables.Count > 0)
                dt = ds.Tables[0];
            return dt;
        }

        public void EndorseBidTender(string vBidTenderNo)
        {
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@BID_TENDER_STATUS_ENDORSED", SqlDbType.Int);
            sqlparams[0].Value = Constant.BID_TENDER_STATUS_ENDORSED.ToString().Trim();
            sqlparams[1] = new SqlParameter("@BidTenderNo", SqlDbType.Int);
            sqlparams[1].Value = vBidTenderNo;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "[s3p_EBid_EndorseBidTender]", sqlparams);
        }


        public void EndorseBidItem(string vBidRefNo)
        {
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlparams[0].Value = vBidRefNo;
            sqlparams[1] = new SqlParameter("@BID_STATUS_ENDORSED", SqlDbType.Int);
            sqlparams[1].Value = Constant.BID_TENDER_STATUS_ENDORSED.ToString().Trim();

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "[s3p_EBid_EndorseBidItem]", sqlparams);
        }

        public int UpdateBidTenderForApproval(string vBidTenderNo)
        {
            SqlParameter[] sqlparams = new SqlParameter[3];
            sqlparams[0] = new SqlParameter("@Status", SqlDbType.Int);
            sqlparams[0].Value = Constant.BID_TENDER_STATUS_AWARD.ToString().Trim();
            sqlparams[1] = new SqlParameter("@BidTenderNo", SqlDbType.Int);
            sqlparams[1].Value = vBidTenderNo;
            sqlparams[2] = new SqlParameter("@Result", SqlDbType.Int);
            sqlparams[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "[s3p_Ebid_UpdateBidTenderStatus]", sqlparams);
            return Int32.Parse(sqlparams[2].Value.ToString().Trim());
        }

        public DataTable QueryRenegotiateBidTenderDetails(string vBuyerId, string vBidRefNo, string vOrderBy)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@BID_TENDER_STATUS_RE_NEGOTIATE", SqlDbType.Int);
            sqlParams[0].Value = Constant.BID_TENDER_STATUS_RE_NEGOTIATE.ToString().Trim();
            sqlParams[1] = new SqlParameter("@BID_STATUS_BID_ITEM", SqlDbType.Int);
            sqlParams[1].Value = Constant.BID_STATUS_BID_ITEM.ToString().Trim();
            sqlParams[2] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[2].Value = vBuyerId;
            sqlParams[3] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[3].Value = vBidRefNo;
            DataSet ds = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_QueryRenegotiateBidTenderDetails]", sqlParams);
            DataTable dt = new DataTable();
            if (ds.Tables.Count > 0)
                dt = ds.Tables[0];
            return dt;
        }

        public DataTable QuerySentRenegotiateBidTenderDetails(string vBuyerId, string vBidRefNo, string vOrderBy)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@BID_TENDER_STATUS_WAIT_FOR_RE_NEGOTIATE", SqlDbType.Int);
            sqlParams[0].Value = Constant.BID_TENDER_STATUS_WAIT_FOR_RE_NEGOTIATE.ToString().Trim();
            sqlParams[1] = new SqlParameter("@BID_STATUS_BID_ITEM", SqlDbType.Int);
            sqlParams[1].Value = Constant.BID_STATUS_BID_ITEM.ToString().Trim();
            sqlParams[2] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[2].Value = vBuyerId;
            sqlParams[3] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[3].Value = vBidRefNo;
            DataSet ds = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_QuerySentRenegotiateBidTenderDetails]", sqlParams);
            DataTable dt = new DataTable();
            if (ds.Tables.Count > 0)
                dt = ds.Tables[0];
            return dt;
        }

        public void UpdateBidTenderStatusWaitingRenegotiation(string vBidRefNo)
        {
            SqlParameter[] sqlparams = new SqlParameter[4];
            sqlparams[0] = new SqlParameter("@BID_TENDER_STATUS_RE_NEGOTIATE", SqlDbType.Int);
            sqlparams[0].Value = Constant.BID_TENDER_STATUS_RE_NEGOTIATE.ToString().Trim();
            sqlparams[1] = new SqlParameter("@BID_TENDER_STATUS_WAIT_FOR_RE_NEGOTIATE", SqlDbType.Int);
            sqlparams[1].Value = Constant.BID_TENDER_STATUS_WAIT_FOR_RE_NEGOTIATE.ToString().Trim();
            sqlparams[2] = new SqlParameter("@BID_STATUS_BID_ITEM", SqlDbType.Int);
            sqlparams[2].Value = Constant.BID_STATUS_BID_ITEM.ToString().Trim();
            sqlparams[3] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlparams[3].Value = vBidRefNo;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "[s3p_EBid_UpdateBidTenderStatusWaitingRenegotiation]", sqlparams);
        }

        public DataTable QueryEndorsedBidTenderDetails(string vBuyerId, string vBidRefNo, string vOrderBy)
        {
            SqlParameter[] sqlparams = new SqlParameter[4];
            sqlparams[0] = new SqlParameter("@BID_TENDER_STATUS_ENDORSED", SqlDbType.Int);
            sqlparams[0].Value = Constant.BID_TENDER_STATUS_ENDORSED.ToString().Trim();
            sqlparams[1] = new SqlParameter("@BID_STATUS_BID_ITEM", SqlDbType.Int);
            sqlparams[1].Value = Constant.BID_STATUS_BID_ITEM.ToString().Trim();
            sqlparams[2] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlparams[2].Value = vBuyerId;
            sqlparams[3] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlparams[3].Value = vBidRefNo;

            DataSet ds = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_QueryEndorsedBidTenderDetails]", sqlparams);
            DataTable dt = new DataTable();
            if (ds.Tables.Count > 0)
                dt = ds.Tables[0];
            return dt;
        }



    }
}