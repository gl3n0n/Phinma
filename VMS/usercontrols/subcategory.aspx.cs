using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class usercontrols_subcategory : System.Web.UI.Page
{
    SqlDataReader oReader;
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
    string query;
    SqlCommand cmd;
    SqlConnection conn;

    protected void Page_Load(object sender, EventArgs e)
    {
        string CategoryId = Request.Form["CategoryId"] != null ? Request.Form["CategoryId"].ToString() : "";

        if (CategoryId != "")
        {
            query = "SELECT '' as SubCategoryId, '' as CategoryId, 'Select SubCategory' as SubCategoryName UNION SELECT SubCategoryId, CategoryId, SubCategoryName FROM rfcProductSubCategory WHERE CategoryId = @CategoryId";
        }
        else
        {
            query = "SELECT '' as SubCategoryId, '' as CategoryId, 'Select SubCategory' as SubCategoryName UNION SELECT SubCategoryId, CategoryId, SubCategoryName FROM rfcProductSubCategory";
        }
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        //Response.Write(oReader["SubCategoryId"].ToString() + ": (" + oReader["CategoryId"].ToString() + ") " + oReader["SubCategoryName"].ToString() + "<br>");
                        Response.Write("<option value=\""+ oReader["SubCategoryId"].ToString() +"\">" + oReader["SubCategoryName"].ToString() + "</option>");
                    }
                }
            }
        }  
    }
}