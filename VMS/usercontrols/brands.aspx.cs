using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class usercontrols_brands : System.Web.UI.Page
{
    SqlDataReader oReader;
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
    string query;
    SqlCommand cmd;
    SqlConnection conn;

    protected void Page_Load(object sender, EventArgs e)
    {
        string SubCategoryId = Request.Form["SubCategoryId"] != null ? Request.Form["SubCategoryId"].ToString() : "";

        if (SubCategoryId != "")
        {
            query = "SELECT '' as BrandId, '' as SubCategoryId, 'Select a Brand' as BrandName UNION SELECT BrandId, SubCategoryId, BrandName FROM rfcProductBrands WHERE SubCategoryId = @SubCategoryId";
        }
        else
        {
            query = "SELECT '' as BrandId, '' as SubCategoryId, 'Select a Brand' as BrandName UNION SELECT BrandId, SubCategoryId, BrandName FROM rfcProductBrands";
        }
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@SubCategoryId", SubCategoryId);
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        //Response.Write(oReader["BrandId"].ToString() + ": (" + oReader["SubCategoryId"].ToString() + ") " + oReader["BrandName"].ToString() + "<br>");
                        Response.Write("<option value=\"" + oReader["BrandId"].ToString() + "\">" + oReader["BrandName"].ToString() + "</option>");
                    }
                }
            }
        }  
    }
}