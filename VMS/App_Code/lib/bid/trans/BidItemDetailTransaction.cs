using System;
using System.Data.OleDb;
using Ava.lib.utils;
using Ava.lib.bid.trans;
using Ava.lib.bid.data;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Ava.lib;


namespace Ava.lib.bid.trans
{
	/// <summary>
	/// Summary description for BidItemDetail.
	/// </summary>
	public class BidItemDetailTransaction
	{
        private string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
	
        public string InsertBidDetail(string vSKU, 
            string vBidRefNo,
			string vCategoryId, 
            string vSubCategoryId,
            string vDetailDesc,
			string vQty, 
            string vUnitOfMeasure, 
            string vDeliveryDate,
            string vUnitPrice,
            string vEstItemValue)
		{
            SqlParameter[] sqlParams = new SqlParameter[11];
            sqlParams[0] = new SqlParameter("@Item", SqlDbType.VarChar);
            sqlParams[0].Value = vSKU;
            sqlParams[1] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[1].Value = vBidRefNo;
            sqlParams[2] = new SqlParameter("@CategoryId", SqlDbType.NVarChar);
            sqlParams[2].Value = vCategoryId;
            sqlParams[3] = new SqlParameter("@SubCategoryId", SqlDbType.Int);
            if (vSubCategoryId.Trim() == "")
                sqlParams[3].Value = System.DBNull.Value;
            else
                sqlParams[3].Value = vSubCategoryId;
            sqlParams[4] = new SqlParameter("@DetailDesc", SqlDbType.VarChar);
            sqlParams[4].Value = vDetailDesc;
            sqlParams[5] = new SqlParameter("@Qty", SqlDbType.Float);
            sqlParams[5].Value = vQty;
            sqlParams[6] = new SqlParameter("@UnitOfMeasure", SqlDbType.VarChar);
            sqlParams[6].Value = vUnitOfMeasure;
            sqlParams[7] = new SqlParameter("@DeliveryDate", SqlDbType.DateTime);
            sqlParams[7].Value = vDeliveryDate;
            sqlParams[8] = new SqlParameter("@UnitPrice", SqlDbType.Float);
            sqlParams[8].Value = vUnitPrice;
            sqlParams[9] = new SqlParameter("@EstItemValue", SqlDbType.Float);
            sqlParams[9].Value = vEstItemValue;
            sqlParams[10] = new SqlParameter("@BidDetailNo", SqlDbType.Int);
            sqlParams[10].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "[s3p_Ebid_InsertBidDetail]", sqlParams);
            return sqlParams[10].Value.ToString().Trim();
		}


		public int UpdateBidDetail(string vSKU, 
            string vCategoryId, 
            string vSubCategoryId, 
            string vDetailDesc,
			string vQty, 
            string vUnitOfMeasure, 
            string vUnitPrice, 
            string vEstItemValue, 
            string vDeliveryDate, 
            string vBidDetailNo, 
            string vBidRefNo)
		{
            SqlParameter[] sqlParams = new SqlParameter[12];
            sqlParams[0] = new SqlParameter("@Item", SqlDbType.VarChar);
            sqlParams[0].Value = vSKU;
            sqlParams[1] = new SqlParameter("@CategoryId", SqlDbType.NVarChar);
            sqlParams[1].Value = vCategoryId;
            sqlParams[2] = new SqlParameter("@SubCategoryId", SqlDbType.Int);
            if (vSubCategoryId.Trim() == "")
                sqlParams[2].Value = System.DBNull.Value;
            else
                sqlParams[2].Value = vSubCategoryId;
            sqlParams[3] = new SqlParameter("@DetailDesc", SqlDbType.VarChar);
            sqlParams[3].Value = vDetailDesc;
            sqlParams[4] = new SqlParameter("@Qty", SqlDbType.Float);
            sqlParams[4].Value = vQty;
            sqlParams[5] = new SqlParameter("@UnitOfMeasure", SqlDbType.VarChar);
            sqlParams[5].Value = vUnitOfMeasure;
            sqlParams[6] = new SqlParameter("@DeliveryDate", SqlDbType.DateTime);
            sqlParams[6].Value = vDeliveryDate;
            sqlParams[7] = new SqlParameter("@BidDetailNo", SqlDbType.Int);
            sqlParams[7].Value = vBidDetailNo;
            sqlParams[8] = new SqlParameter("@UnitPrice", SqlDbType.Float);
            sqlParams[8].Value = vUnitPrice;
            sqlParams[9] = new SqlParameter("@EstItemValue", SqlDbType.Float);
            sqlParams[9].Value = vEstItemValue;
            sqlParams[10] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[10].Value = vBidRefNo;
            sqlParams[11] = new SqlParameter("@Result", SqlDbType.Int);
            sqlParams[11].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "[s3p_Ebid_UpdateBidDetail]", sqlParams);
            return Convert.ToInt32(sqlParams[11].Value.ToString().Trim());
		}

        public int DeleteBidDetail(string vBidRefNo, string vBidDetailNos)
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


        public BidItemDetail GetBidItemDetailsByRefNo(int bidRefNo)
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

		public  ArrayList GetBidDetails(string vBidRefNo)
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
					bidItem.CategoryId = row["CategoryId"].ToString().Trim();
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


        public DataView GetBidItemDetails(string vBidRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vBidRefNo);

            DataTable dt = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetBidItemDetails", sqlParams).Tables[0];
            
            DataView dv = new DataView(dt);
            return dv; 
        }

        public DataTable GetBidItemDetailsByBidRefNo(string BidRefNo)
        {
            SqlParameter[] sqlparams = new SqlParameter[1];
            sqlparams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlparams[0].Value = Int32.Parse(BidRefNo);

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetBidItemDetailsByBidRefNo", sqlparams).Tables[0];
        }

        public DataTable QueryBidItemDetail_Items_ByBidRefNo(string BidRefNo)
        {
            SqlParameter[] sqlparams = new SqlParameter[1];
            sqlparams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlparams[0].Value = Int32.Parse(BidRefNo);

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryBidItemDetail_Items_ByBidRefNo", sqlparams).Tables[0];
        }
	}
}
