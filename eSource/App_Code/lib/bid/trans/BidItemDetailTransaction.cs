using System;
using System.Web;
using System.Data.OleDb;
using System.Web;
using EBid.lib.utils;
using EBid.lib.bid.trans;
using EBid.lib.bid.data;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using EBid.lib;
using EBid.ConnectionString;


namespace EBid.lib.bid.trans
{
	/// <summary>
	/// Summary description for BidItemDetail.
	/// </summary>
	public class BidItemDetailTransaction
	{
        public string InsertBidDetail(
			string connstring, 
			string vSKU, 
            string vBidRefNo,
            string vDetailDesc,
			string vQty, 
            string vUnitOfMeasure, 
            string vDeliveryDate,
            string vUnitPrice,
            string vEstItemValue)
		{
            SqlParameter[] sqlParams = new SqlParameter[9];
            sqlParams[0] = new SqlParameter("@Item", SqlDbType.VarChar);
            sqlParams[0].Value = vSKU;
            sqlParams[1] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[1].Value = vBidRefNo;
            sqlParams[2] = new SqlParameter("@DetailDesc", SqlDbType.VarChar);
            sqlParams[2].Value = vDetailDesc;
            sqlParams[3] = new SqlParameter("@Qty", SqlDbType.Float);
            sqlParams[3].Value = vQty;
            sqlParams[4] = new SqlParameter("@UnitOfMeasure", SqlDbType.VarChar);
            sqlParams[4].Value = vUnitOfMeasure;
            sqlParams[5] = new SqlParameter("@DeliveryDate", SqlDbType.DateTime);
            sqlParams[5].Value = vDeliveryDate;
            sqlParams[6] = new SqlParameter("@UnitPrice", SqlDbType.Float);
            sqlParams[6].Value = vUnitPrice;
            sqlParams[7] = new SqlParameter("@EstItemValue", SqlDbType.Float);
            sqlParams[7].Value = vEstItemValue;
            sqlParams[8] = new SqlParameter("@BidDetailNo", SqlDbType.Int);
            sqlParams[8].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "[s3p_Ebid_InsertBidDetail]", sqlParams);
            return sqlParams[8].Value.ToString().Trim();
		}


		public int UpdateBidDetail(
			string connstring, 
			string vSKU,
            string vDetailDesc,
			string vQty, 
            string vUnitOfMeasure, 
            string vUnitPrice, 
            string vEstItemValue, 
            string vDeliveryDate, 
            string vBidDetailNo, 
            string vBidRefNo)
		{
            SqlParameter[] sqlParams = new SqlParameter[10];
            sqlParams[0] = new SqlParameter("@Item", SqlDbType.VarChar);
            sqlParams[0].Value = vSKU;
            sqlParams[1] = new SqlParameter("@DetailDesc", SqlDbType.VarChar);
            sqlParams[1].Value = vDetailDesc;
            sqlParams[2] = new SqlParameter("@Qty", SqlDbType.Float);
            sqlParams[2].Value = vQty;
            sqlParams[3] = new SqlParameter("@UnitOfMeasure", SqlDbType.VarChar);
            sqlParams[3].Value = vUnitOfMeasure;
            sqlParams[4] = new SqlParameter("@DeliveryDate", SqlDbType.DateTime);
            sqlParams[4].Value = vDeliveryDate;
            sqlParams[5] = new SqlParameter("@BidDetailNo", SqlDbType.Int);
            sqlParams[5].Value = vBidDetailNo;
            sqlParams[6] = new SqlParameter("@UnitPrice", SqlDbType.Float);
            sqlParams[6].Value = vUnitPrice;
            sqlParams[7] = new SqlParameter("@EstItemValue", SqlDbType.Float);
            sqlParams[7].Value = vEstItemValue;
            sqlParams[8] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[8].Value = vBidRefNo;
            sqlParams[9] = new SqlParameter("@Result", SqlDbType.Int);
            sqlParams[9].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "[s3p_Ebid_UpdateBidDetail]", sqlParams);
            return Convert.ToInt32(sqlParams[9].Value.ToString().Trim());
		}

        public int DeleteBidDetail(string connstring, string vBidRefNo, string vBidDetailNos)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.VarChar);
            sqlParams[0].Value = vBidRefNo;
            sqlParams[1] = new SqlParameter("@BidDetailNo", SqlDbType.NVarChar);
            sqlParams[1].Value = vBidDetailNos;
            sqlParams[2] = new SqlParameter("@Result", SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "[s3p_EBid_DeleteBidDetail]", sqlParams);
            return Convert.ToInt32(sqlParams[2].Value.ToString().Trim());
        }


        public BidItemDetail GetBidItemDetailsByRefNo(string connstring, int bidRefNo)
        {
            
            BidItemDetail bidDetail = new BidItemDetail();
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
                sqlParams[0].Value = bidRefNo;

                DataSet bidData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetItemDetailsByRefNo", sqlParams);
                if (bidData.Tables[0].Rows.Count > 0)
                {
                    DataRow row = bidData.Tables[0].Rows[0];
                    bidDetail.BidDetailNo = Int32.Parse(row["BidDetailNo"].ToString());
                    bidDetail.BidRefNo = Int32.Parse(row["BidRefNo"].ToString());
                    bidDetail.CategoryId = row["CategoryId"].ToString();
                    bidDetail.DeliveryDate = row["DeliveryDate"].ToString();
                    bidDetail.DetailDesc = row["DetailDesc"].ToString();
                    bidDetail.EstItemValue = Double.Parse(row["EstItemValue"].ToString());
                    bidDetail.Item = row["Item"].ToString();
                    bidDetail.Qty = Int32.Parse(row["Qty"].ToString());
                    bidDetail.UnitOfMeasure = row["UnitOfMeasure"].ToString();        
                }
                
            }
            catch
            {
                
            }
            return bidDetail;
        }

		public  ArrayList GetBidDetails(string connstring, string vBidRefNo)
		{	
            ArrayList biditemdetails = new ArrayList();
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vBidRefNo);

            DataSet bidDetailData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetBidDetails", sqlParams);
            

			foreach(DataTable table in bidDetailData.Tables)
			{
				foreach(DataRow row in table.Rows) 
				{
					BidItemDetail bidItem = new BidItemDetail();
					bidItem.BidDetailNo = Int32.Parse(row["BidDetailNo"].ToString().Trim());
					bidItem.BidRefNo = Int32.Parse(row["BidRefNo"].ToString().Trim());
					bidItem.DetailDesc = row["DetailDesc"].ToString();
					bidItem.EstItemValue = Double.Parse(row["EstItemValue"].ToString().Trim());
                    bidItem.UnitPrice = Double.Parse(row["UnitPrice"].ToString().Trim());
					bidItem.Qty = Int32.Parse(row["Qty"].ToString().Trim());
					bidItem.DeliveryDate = row["DeliveryDate"].ToString();
					bidItem.Item = row["Item"].ToString();
					bidItem.UnitOfMeasure = row["UnitOfMeasure"].ToString().Trim();
                    bidItem.DeliveryDateMonth = row["DeliveryDateMonth"].ToString().Trim();
                    bidItem.DeliveryDateDay = row["DeliveryDateDay"].ToString().Trim();
                    bidItem.DeliveryDateYear = row["DeliveryDateYear"].ToString().Trim();
					biditemdetails.Add(bidItem);
				}
			}
			
			return biditemdetails;
		}


        public DataView GetBidItemDetails(string connstring, string vBidRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vBidRefNo);

            DataTable dt = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetBidItemDetails", sqlParams).Tables[0];
            
            DataView dv = new DataView(dt);
            return dv; 
        }

        public DataTable GetBidItemDetailsByBidRefNo(string connstring, string BidRefNo)
        {
            SqlParameter[] sqlparams = new SqlParameter[1];
            sqlparams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlparams[0].Value = Int32.Parse(BidRefNo);

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetBidItemDetailsByBidRefNo", sqlparams).Tables[0];
        }

        public DataTable QueryBidItemDetail_Items_ByBidRefNo(string connstring, string BidRefNo)
        {
            SqlParameter[] sqlparams = new SqlParameter[1];
            sqlparams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlparams[0].Value = Int32.Parse(BidRefNo);

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryBidItemDetail_Items_ByBidRefNo", sqlparams).Tables[0];
        }
	}
}
