using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using Ava.lib;
using Ava.lib.user.data;
using Ava.lib.user.trans;
using Ava.lib.utils;
using Ava.lib.bid.data;

namespace Ava.lib.user.trans
{
	public class UserTransaction
	{
        private static string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;

		public UserTransaction()
		{
		}

        public DataTable QueryItemCommentsbyRefNo(string RefNo, bool isAuction)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@refNo", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(RefNo);
            sqlParams[1] = new SqlParameter("@isAuction", SqlDbType.Int);
            if (!(isAuction))
                sqlParams[1].Value = 0;
            else
                sqlParams[1].Value = 1;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryItemCommentsbyRefNo", sqlParams).Tables[0];
        }

        public void InsertItemComments(string RefNo, string comment, string userId, int userType, bool isAuction)
        {
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@refNo", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(RefNo);
            sqlParams[1] = new SqlParameter("@userId", SqlDbType.Int);
            sqlParams[1].Value = Int32.Parse(userId);
            sqlParams[2] = new SqlParameter("@userType", SqlDbType.Int);
            sqlParams[2].Value = userType;
            sqlParams[3] = new SqlParameter("@isAuction", SqlDbType.Int);

            if (!(isAuction))
                sqlParams[3].Value = 0;
            else
                sqlParams[3].Value = 1;

            sqlParams[4] = new SqlParameter("@comment", SqlDbType.NVarChar);
            sqlParams[4].Value = comment;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_InsertItemComments", sqlParams);
        }

        public DataTable QueryTenderCommentsbyRefNoAndStatus(int userType, int refNo, int status)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@userType", SqlDbType.Int);
            sqlParams[0].Value = userType;
            sqlParams[1] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlParams[1].Value = refNo;
            sqlParams[2] = new SqlParameter("@status", SqlDbType.Int);
            sqlParams[2].Value = status;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_Ebid_QueryTendersComments", sqlParams).Tables[0];
        }

        public void InsertTenderComments(int userType, int userId, int refNo, int status, string comment)
        {
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@userType", SqlDbType.Int);
            sqlParams[0].Value = userType;
            sqlParams[1] = new SqlParameter("@userID", SqlDbType.Int);
            sqlParams[1].Value = userId;
            sqlParams[2] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlParams[2].Value = refNo;
            sqlParams[3] = new SqlParameter("@status", SqlDbType.Int);
            sqlParams[3].Value = status;
            sqlParams[4] = new SqlParameter("@comment", SqlDbType.NVarChar);
            sqlParams[4].Value = comment;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_Ebid_InsertTendersComments", sqlParams);
        }

        public static string GetUserType(int userId)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@Userid", SqlDbType.Int);
            sqlParams[0].Value = userId;

            return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "sp_GetUserType", sqlParams).ToString().Trim();
        }
	}
}
