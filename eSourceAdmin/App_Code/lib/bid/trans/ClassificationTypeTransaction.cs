using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using EBid.lib.utils;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClassificationTypeTransaction
/// </summary>
/// 
namespace EBid.lib.bid.trans
{
    public class ClassificationTypeTransaction
    {
        private string connstring = ConfigurationManager.ConnectionStrings["EBidConnectionString"].ConnectionString;

        public DataTable GetClassificationType()
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);
            DataSet dsQueryResult;
            sqlConnect.Open();

            dsQueryResult = SqlHelper.ExecuteDataset(sqlConnect, CommandType.StoredProcedure, "s3p_EBid_CompanyClassification");
            sqlConnect.Close();
            return dsQueryResult.Tables[0];
        }
    }

    
}