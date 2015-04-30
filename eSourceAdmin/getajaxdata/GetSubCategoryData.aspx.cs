using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;

public partial class GetAjaxData_GetSubCategoryData : System.Web.UI.Page
{
    private string connString = ConfigurationManager.ConnectionStrings["EBidConnectionStringTrans"].ToString().Trim();
    //private string connString = ConfigurationManager.ConnectionStrings["EBidConnectionString"].ToString().Trim();
    //private string connString = HttpContext.Current.Session["ConnectionString"].ToString();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        string category = "";
        if (Request.Params["cat"] != null)
            category = Request.Params["cat"].ToString().Trim();
        string remcategory = "";
        if (Request.Params["remcat"] != null)
            remcategory = Request.Params["remcat"].ToString().Trim();
        
        try
        {
            #region insert sub categories
            if ((!(category == null)) && (category != String.Empty))
            {
                category = this.GetCategorySet(category);

                DataSet ds = this.GetData(category);
                string s = "";
                // Populate ddlSubCategory
                string t = "";
                string v = "";
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    t += "\""
                                + (r["SubCategoryName"].ToString().Trim() + "\",");
                    v += "\""
                                + (r["SubCategoryId"].ToString().Trim() + "\",");
                }
                if ((t.Length > 0))
                {
                    t = t.Substring(0, (t.Length - 1));
                }
                if ((v.Length > 0))
                {
                    v = v.Substring(0, (v.Length - 1));
                }
                s = ("populateDDL(new Array("
                            + (v + ("), new Array("
                            + (t + "),\'Wizard1_lstSubCategory\');"))));
                Response.Write(s);
            }
            #endregion
            #region remove sub categories
            if ((!(remcategory == null)) && (remcategory != String.Empty))
            {
                remcategory = this.GetCategorySet(remcategory);

                DataSet ds = this.GetData(remcategory);
                string s = "";
                // Populate ddlSubCategory
                string t = "";
                string v = "";
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    t += "\""
                                + (r["SubCategoryName"].ToString().Trim() + "\",");
                    v += "\""
                                + (r["SubCategoryId"].ToString().Trim() + "\",");
                }
                if ((t.Length > 0))
                {
                    t = t.Substring(0, (t.Length - 1));
                }
                if ((v.Length > 0))
                {
                    v = v.Substring(0, (v.Length - 1));
                }
                s = ("RemoveFromList(new Array("
                            + (v + ("), new Array("
                            + (t + "),\'lstTempRemoveSubCategory\');"))));
                Response.Write(s);
            }
            #endregion
        }
        catch (Exception ex)
        {
            string x = ex.Message;
        }
    }

    private string GetCategorySet(string category)
    {
        string[] categoryA = category.Split(Convert.ToChar(","));
        if (categoryA.Length > 1)
        {
            string sb = "";
            foreach (string cat in categoryA)
            {
                if (sb.Trim() == "")
                    sb = "'" + cat + "'";
                else
                
                    sb =  sb + ",'" + cat + "'"; 
           }
            category = sb.ToString().Trim();
        }
        else
        {
            category = "'" + category.Trim() + "'";
        }
        return category;
    }

    private DataSet GetData(string catID)
    {
        SqlConnection cnn = new SqlConnection(connString);
        try
        {
            SqlCommand cmd = new SqlCommand("s3p_EBid_GetProductSubCategoryByMultipleCategoryId");
            cmd.CommandType = CommandType.StoredProcedure;
            //returns all subcategory if catID = ""
            cmd.Parameters.Add(new SqlParameter("@CategoryId", catID));
            cmd.Connection = cnn;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            string a = ex.Message.ToString().Trim();
            return null;
        }
        finally
        {
            if (!(cnn == null))
            {
                cnn.Dispose();
            }
        }
    }
}
