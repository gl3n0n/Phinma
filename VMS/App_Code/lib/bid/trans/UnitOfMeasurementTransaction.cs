using System;
using System.Data;
using Ava.lib.utils;
using System.Data.OleDb;
using System.Data.SqlClient;
using Ava.lib.bid.data;
using System.Configuration;

namespace Ava.lib.bid.trans
{
    public class UnitOfMeasurementTransaction
    {
        private string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;

        public DataTable GetAllUnitsOfMeasurement()
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);
            DataSet dsQueryResult;
            sqlConnect.Open();
            dsQueryResult = SqlHelper.ExecuteDataset(sqlConnect, CommandType.StoredProcedure, "s3p_EBid_GetAllUnitsOfMeasurement");
            sqlConnect.Close();
            return dsQueryResult.Tables[0];
        }


        public ProductUnit GetUnitOfMeasureById(int id)
        {
            ProductUnit productUnit = new ProductUnit();

            SqlParameter[] sqlparams = new SqlParameter[1];
            sqlparams[0] = new SqlParameter("@unitId", SqlDbType.Int);
            sqlparams[0].Value = id;
            DataSet bidData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetUnitOfMeasureById", sqlparams);

            if (bidData.Tables[0].Rows.Count > 0)
            {

                foreach (DataTable table in bidData.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        productUnit = new ProductUnit();
                        productUnit.UnitId = Int32.Parse(row["UnitId"].ToString());
                        productUnit.UnitName = row["UnitName"].ToString();
                    }
                }
            }

            return productUnit;
        }

        public int InsertNewUnitOfMeasurement(string vUnitName)
        {
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@UnitName", SqlDbType.VarChar);
            sqlparams[0].Value = vUnitName;
            sqlparams[1] = new SqlParameter("@UnitId", SqlDbType.Int);
            sqlparams[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_Ebid_InsertNewUOM_GetId", sqlparams);

            return Convert.ToInt32(sqlparams[1].Value);
        }

        public string GetUnitNameById(string unitid)
        {
            SqlParameter[] sqlparams = new SqlParameter[1];
            sqlparams[0] = new SqlParameter("@unitId", SqlDbType.Int);

            return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetUnitNameById", sqlparams).ToString().Trim();
        }
    }
}
