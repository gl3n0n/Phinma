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
namespace EBid.Configurations
{
    public static class ClientConfig
    {
        public static string ConfigurationsId(string clientid, string configRequest)
        {
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
                                configRequest = oReader[configRequest].ToString();
                            }
                        }
                    }
                }
            }

            return (configRequest);
        }



        public static string ConfigurationsSlug(string slug, string configRequest)
        {
            string query;
            SqlCommand cmd;
            SqlConnection conn;
            string connstring = ConfigurationManager.ConnectionStrings["EBidConnectionString"].ConnectionString;
            SqlDataReader oReader;

            if (slug != null || slug != "")
            {
                query = "SELECT * FROM tblClients WHERE slug=@slug";
                //query = "sp_GetVendorInformation"; //##storedProcedure
                using (conn = new SqlConnection(connstring))
                {
                    using (cmd = new SqlCommand(query, conn))
                    {
                        //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                        cmd.Parameters.AddWithValue("@slug", slug);
                        conn.Open();
                        //Process results
                        oReader = cmd.ExecuteReader();
                        if (oReader.HasRows)
                        {
                            while (oReader.Read())
                            {
                                configRequest = oReader[configRequest].ToString();
                            }
                        }
                    }
                }
            }

            return (configRequest);
        }

    }


}