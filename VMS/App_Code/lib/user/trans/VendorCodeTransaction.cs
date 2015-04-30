using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using Ava.lib;
using Ava.lib.utils;
using Ava.lib.constant;
using System.Text.RegularExpressions;

namespace Ava.lib.user.trans
{

    public class VendorCodeTransaction
    {
        public string GetMaxVendorCodeFromAccpac(string CompanyName)
        {
            string connstring3 = ConfigurationManager.ConnectionStrings["ACCPACConnectionString"].ConnectionString;
            SqlDataReader oReader;
            string query;
            SqlCommand cmd;
            SqlConnection conn;
            string VendorCode="";

            query = "declare @VendorCode varchar(12) declare @FirstVendorChar char(1) declare @PreFix varchar(12) declare @LastNumberCodes char(12) declare @LastNumberInt int SET @PreFix  = 'H-' + UPPER(SUBSTRING(@CompanyName, 1, 1)) BEGIN SELECT @LastNumberCodes = MAX(SUBSTRING(VENDORID, 4, 99)) FROM [SHIDAT].dbo.APVEN WHERE VENDORID LIKE '%'+@PreFix+'%' END BEGIN SELECT @LastNumberInt = @LastNumberCodes END BEGIN SELECT @PreFix + RIGHT('000'+ CONVERT(VARCHAR,@LastNumberInt),4) AS MaxVendorCode END";

            using (conn = new SqlConnection(connstring3))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CompanyName", CompanyName);
                    conn.Open();  oReader = cmd.ExecuteReader();
                    if (oReader.HasRows)
                    {
                        while (oReader.Read())
                        {
                            //if (oReader["Status"].ToString() == "6") { Response.Redirect("cfo_vendorDetails_View.aspx"); }
                            VendorCode = oReader["MaxVendorCode"].ToString()+"99";
                            string codePrefix = VendorCode.Substring(0, 3);
                            Regex digitsOnly = new Regex(@"[^\d]");
                            int codeDigits = digitsOnly.Replace(VendorCode, "").Length;
                            int VendorCode1 = Convert.ToInt32(digitsOnly.Replace(VendorCode, "")) + 1;
                            string codeLeading = "D"+codeDigits.ToString();
                            VendorCode = codePrefix + VendorCode1.ToString(codeLeading);
                        }
                    }
                }
            }
            conn.Close();

            return VendorCode;
        }
    }
}