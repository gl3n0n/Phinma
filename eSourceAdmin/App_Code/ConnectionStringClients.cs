using System;
using System.Web;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using EBid.libs;

/// <summary>
/// Summary description for ClientsConnectionString
/// </summary>
namespace EBid.ConnectionString
{
    public static class Client
    {
        public static string ConnectionString(string clientid)
        {
            string databaseName = "ebid";
            string query;
            SqlCommand cmd;
            SqlConnection conn;
            string connstring = ConfigurationManager.ConnectionStrings["EBidConnectionString"].ConnectionString;
            SqlDataReader oReader;
            
            if (clientid != null || clientid != "")
            {
                query = "SELECT * FROM tblClients WHERE clientid=@clientid AND status=1";
                //query = "sp_GetVendorInformation"; //##storedProcedure
                using (conn = new SqlConnection(connstring))
                {
                    using (cmd = new SqlCommand(query, conn))
                    {
                        //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                        cmd.Parameters.AddWithValue("@clientid", Convert.ToInt32(clientid));
                        conn.Open();
                        //Process results
                        oReader = cmd.ExecuteReader();
                        if (oReader.HasRows)
                        {
                            while (oReader.Read())
                            {
                                databaseName = oReader["databaseName"].ToString();
                            }
                        }
                    }
                }
            }

            //databaseName = "ebid_yiucon";
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "10.10.20.61\\SQLEXPRESS";
            builder.InitialCatalog = databaseName;
            builder.UserID = "sa";
            builder.Password = "P@ssw0rd123";
            return (builder.ConnectionString);
        }
	}
}