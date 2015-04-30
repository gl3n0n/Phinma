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
using System.IdentityModel;
using System.Data.OleDb;


/// <summary>
/// Summary description for ClientsConnectionString
/// </summary>
namespace EBid.libs
{
    public static class Class1
    {

        public static string Class1String(string clientid)
        {


            string query;
            SqlCommand cmd;
            SqlConnection conn;
            string connstring;
            connstring = ConfigurationManager.ConnectionStrings["EBidConnectionString"].ConnectionString;
            SqlDataReader oReader;
            string databaseName = "";

            if (clientid != null || clientid != "")
            {
                query = "Select * FROM tblClients WHERE clientid=@clientid";
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
                return (databaseName);
            }
            else
            {
                return ("ebid");
            }
            
        }


    }
}