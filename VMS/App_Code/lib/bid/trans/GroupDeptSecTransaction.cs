using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using Ava.lib;
using Ava.lib.utils;

namespace Ava.lib.bid.trans
{
	public class GroupDeptSecTransaction
	{
        private static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;



        public DataTable GetGroupDeptSec(string CompanyId)
        {

            string query = "SELECT isnull([GroupDeptSecId], '') as [GroupDeptSecId], isnull([GroupDeptSecName], '') as [GroupDeptSecName], convert(varchar(5), isnull([CompanyId], '') )+'|'+ convert(varchar(5),isnull([GroupDeptSecId], '')) as [value] " +
                    "FROM [rfcGroupDeptSec]";

            if (CompanyId != "")
                query = query + " WHERE [CompanyId]=" + CompanyId;

            SqlParameter[] sqlparam = new SqlParameter[1];
            sqlparam[0] = new SqlParameter("@query", SqlDbType.NText);
            sqlparam[0].Value = query;
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_GenericQueryProcedure]", sqlparam);
            DataTable dt = new DataTable();
            if (ds.Tables.Count > 0)
                dt = ds.Tables[0];
            return dt;
        }

		public string GetGroupDeptSecNameById(string vGroupId)
		{
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@groupId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vGroupId);

            return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetGroupDeptName", sqlParams).ToString().Trim();
		}
	}
}
