using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class GetAJAXData : System.Web.UI.Page
{
    private string connString = ConfigurationManager.ConnectionStrings["EBidConnectionString"].ToString().Trim();
    protected void Page_Load(object sender, EventArgs e)
    {
        string company = "";
        if (Request.Params["com"] != null)
            company = Request.Params["com"].ToString().Trim();
        //for supplier edit
        if (company == "null")
            company = null;

        string category = "";
        if (Request.Params["c"] !=null)
            category = Request.Params["c"].ToString().Trim();
        try
        {
            if (!(company == null))
            {
                DataSet ds = this.GetGroupData(company);
                string s = "";


                // Populate ddlGroupDeptSec
                string t = "";
                string v = "";
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    t += "\""
                                + (r["GroupDeptSecName"].ToString().Trim() + "\",");
                    v += "\""
                                + (r["GroupDeptSecId"].ToString().Trim() + "\",");
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
                            + (t + "),\'ddlGroupDeptSec\',\'__groupID\');"))));
                Response.Write(s);
            }

            if (!(category == null))
            {
                DataSet ds = this.GetData(category);
                string s = "";
                
                
                // Populate ddlSubCategory
                string t="";
                string v="";
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    t +="\""
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
                            + (t + "),\'ddlSubCategory\',\'__subcatID\');"))));
                Response.Write(s);
            }

            
        }
        catch (Exception ex)
        {
            string x = ex.Message;
        }
    }

    private DataSet GetData(string catID)
    {
        SqlConnection cnn = new SqlConnection(connString);
        try
        {
            SqlCommand cmd = new SqlCommand("s3p_EBid_GetProductSubCategoryByCategoryId");
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


    private DataSet GetGroupData(string company)
    {
        SqlConnection cnn = new SqlConnection(connString);
        try
        {
            SqlCommand cmd = new SqlCommand("s3p_EBid_GetGroupDeptSecByCompany");
            cmd.CommandType = CommandType.StoredProcedure;
            if ((company == ""))
                company = "-1";
            //returns all group if company = -1
            cmd.Parameters.Add(new SqlParameter("@CompanyId", company));
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
