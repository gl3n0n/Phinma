using System;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using EBid.lib;
using EBid.ConnectionString;
using EBid.lib.user.data;
using EBid.lib.user.trans;
using EBid.lib.utils;
using EBid.lib.bid.data;
using EBid.ConnectionString;
using System.Web;

namespace EBid.lib.user.trans
{
	public static class UserTransaction
	{
        public static DataTable QueryItemCommentsbyRefNo(string connstring, string RefNo, bool isAuction)
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

        public static void InsertItemComments(string connstring, string RefNo, string comment, string userId, int userType, bool isAuction, bool allowVendorview)
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@refNo", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(RefNo);
            sqlParams[1] = new SqlParameter("@userId", SqlDbType.Int);
            sqlParams[1].Value = Int32.Parse(userId);
            sqlParams[2] = new SqlParameter("@userType", SqlDbType.Int);
            sqlParams[2].Value = userType;
            sqlParams[3] = new SqlParameter("@isAuction", SqlDbType.Int);

            if (!(isAuction))
            {
                sqlParams[3].Value = 0;
            }
            else
            {
                sqlParams[3].Value = 1;
            }

            sqlParams[4] = new SqlParameter("@comment", SqlDbType.NVarChar);
            sqlParams[4].Value = comment;
            sqlParams[5] = new SqlParameter("@allowvendorview", SqlDbType.Int);

            if (!(allowVendorview))
            {
                sqlParams[5].Value = 0;
            }
            else
            {
                sqlParams[5].Value = 1;
            }

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_InsertItemComments", sqlParams);
        }

        public static DataTable QueryTenderCommentsbyRefNoAndStatus(string connstring, int userType, int refNo, int status)
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

        public static void InsertTenderComments(string connstring, int userType, int userId, int refNo, int status, string comment)
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

        public static bool ChangePassword(string connstring, int userid, string password)
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);
            SqlTransaction sqlTransact = null;
            bool isSuccessful = false;

            try
            {
                sqlConnect.Open();
                sqlTransact = sqlConnect.BeginTransaction();


                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@UserID", SqlDbType.Int);
                sqlParams[1] = new SqlParameter("@Password", SqlDbType.VarChar);
                sqlParams[0].Value = userid;
                sqlParams[1].Value = password;

                SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_UpdateUserPassword", sqlParams);
                
                isSuccessful = true;
            }
            catch
            {
                sqlTransact.Rollback();
                isSuccessful = false;
            }
            finally
            {
                sqlConnect.Close();
            }
            return isSuccessful;
        }

        public static bool ChangePasswordAndAuthenticate(string connstring, int userid, string password, bool authenticate, string username)
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);
            SqlTransaction sqlTransact = null;
            bool isSuccessful = false;

            try
            {
                sqlConnect.Open();
                sqlTransact = sqlConnect.BeginTransaction();


                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@UserID", SqlDbType.Int);
                sqlParams[1] = new SqlParameter("@Password", SqlDbType.VarChar);
                sqlParams[2] = new SqlParameter("@UserName", SqlDbType.VarChar);
                sqlParams[3] = new SqlParameter("@AuthenticateUser", SqlDbType.VarChar);
                sqlParams[0].Value = userid;
                sqlParams[1].Value = password;
                sqlParams[2].Value = username;
                sqlParams[3].Value = authenticate ? 1 : 0; 

                SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_UpdateUserPassword", sqlParams);

                isSuccessful = true;
            }
            catch
            {
                sqlTransact.Rollback();
                isSuccessful = false;
            }
            finally
            {
                sqlConnect.Close();
            }
            return isSuccessful;
        }
	}
}
