using System;
using System.Data;
using System.Data.SqlClient;
using EBid.lib.utils;
using EBid.lib;

namespace EBid.lib.utils
{
	/// <summary>
	/// Summary description for AuditLogger.
	/// </summary>
	public class AuditLogger
	{
        private static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["EBidConnectionString"].ConnectionString;

		public AuditLogger()
		{
		}

        public static void Log(string log)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@log", SqlDbType.VarChar);
            sqlParams[0].Value = log;
            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_Log", sqlParams);
        }
	}
}
