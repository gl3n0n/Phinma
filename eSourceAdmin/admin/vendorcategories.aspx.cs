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
using System.IO;
using System.Data.SqlClient;
using EBid.lib;
using EBid.lib.constant;
using System.ComponentModel;
using System.Drawing;
using System.Text;

public partial class admin_items : System.Web.UI.Page
{
    private string connstring;
    private string csvPath = (System.AppDomain.CurrentDomain.BaseDirectory + "admin\\rfcProductCategory\\");

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            string control1 = Request.Form["__EVENTTARGET"];
            if (control1 == "lnkUpdateDB")
            {
                //Response.Write(control1);
                //ExportCsv();
                //ReadExportedCsv();
                ReadCsvAndUpdateDB();
            }
            if (control1 == "lnkDownloadCSVfromDB")
            {
                //Response.Write(control1);
                //AttachMyFile();
                ExportCSVfromDB();
            }
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        //Response.Write(csvPath);
        if (Session["VendorCategoryMessage"] != null)
        {
            lblMsg.Text = Session["VendorCategoryMessage"].ToString() + "<br /><br />";
            lblMsg.Visible = true;
            Session["VendorCategoryMessage"] = null;
        }
        else
            lblMsg.Visible = false;


        if (!IsPostBack)
        {
            if (Session["VendorCurrentValue"] != null)
            {
                ddlVendor.SelectedValue = Session["VendorCurrentValue"].ToString();
            }
        }

    }


    protected void UploadCSV(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string fileExt =
               System.IO.Path.GetExtension(FileUpload1.FileName);

            if (fileExt == ".csv" || fileExt == ".txt")
            {
                try
                {
                    //FileUpload1.SaveAs("C:\\Uploads\\" + FileUpload1.FileName);
                    //Label1.Text = "File name: " + FileUpload1.PostedFile.FileName + "<br>" + FileUpload1.PostedFile.ContentLength + " kb<br>" + "Content type: " + FileUpload1.PostedFile.ContentType;
                    FileUpload1.SaveAs(csvPath + "tblVendorCategoriesAndSubcategories.csv");
                    Label1.Text = "File uploaded: " + FileUpload1.PostedFile.FileName + " <br />File size: " + FileUpload1.PostedFile.ContentLength + " kb<br>" + "Content type: " + FileUpload1.PostedFile.ContentType;
                }
                catch (Exception ex)
                {
                    Label1.Text = "ERROR: " + ex.Message.ToString();
                }
            }
            else
            {
                Label1.Text = "Only .csv & .txt files allowed!";
            }
        }
        else
        {
            Label1.Text = "You have not specified a file.";
        }
    }

    void ReadCsvAndUpdateDB()
    {
        if (File.Exists(csvPath + "tblVendorCategoriesAndSubcategories.csv"))
        {
            StreamReader csvContent = new StreamReader(csvPath + "tblVendorCategoriesAndSubcategories.csv");
            string line = "";
            string[] row;
            string vID = ""; string VendorId = ""; string CategoryId = ""; string BrandId = ""; 
            string IncludesAllSubCategories = ""; string SubCategoryId = ""; 
            string ErrSubCategoryId = "";
            int updateSuccessCnt = 0; int updateFailedCnt = 0;

            Console.WriteLine(csvContent.ReadLine());
            while ((line = csvContent.ReadLine()) != null)
            {
                row = line.Split('^');
                for (int i = 0; i < row.Length; i++)
                {
                    if (i == 0)
                    {
                        vID = row[i].ToString().Trim();
                    }
                    if (i == 1)
                    {
                        VendorId = row[i].ToString().Trim();
                    }
                    if (i == 3)
                    {
                        CategoryId = row[i].ToString().Trim();
                    }
                    if (i == 5)
                    {
                        SubCategoryId = row[i].ToString().Trim();
                    }
                    if (i == 7)
                    {
                        BrandId = row[i].ToString().Trim();
                    }
                }
                Response.Write(vID + " : " + VendorId + " : " + CategoryId + " : " + SubCategoryId + " : " + BrandId + "<br>");
                if (SubCategoryId.Length == 0) {SubCategoryId = "0"; }
                if (BrandId.Length == 0) { BrandId = "0"; }
                if (vID.Length == 0) { vID = "0"; }
                if (UpdateVendorCategoryFromCsv(vID, VendorId, CategoryId, SubCategoryId, BrandId))
                {
                    updateSuccessCnt++;
                }
                else
                {
                    updateFailedCnt++;
                    if (ErrSubCategoryId.Length == 0) {
                       ErrSubCategoryId = '[' + VendorId + ':' + CategoryId + ':' + SubCategoryId + ':' + BrandId + ']';
                    }
                    else {
                       ErrSubCategoryId = ErrSubCategoryId + '[' + VendorId + ':' + CategoryId + ':' + SubCategoryId + ':' + BrandId + ']';
                    }
                }

            }
            csvContent.Close();
            File.Delete(csvPath + "tblVendorCategoriesAndSubcategories.csv");

            if (ErrSubCategoryId.Length == 0) 
               Session["VendorCategoryMessage"] = "[" + updateSuccessCnt.ToString() + "] Vendor Categories were successfully updated.  [" + updateFailedCnt.ToString() + "] Failed.";
            else
               Session["VendorCategoryMessage"] = "[" + updateSuccessCnt.ToString() + "] Vendor Categories were successfully updated.  [" + updateFailedCnt.ToString() + "] Failed.<br>List of Failed [" + ErrSubCategoryId.ToString() + "]";
            Response.Redirect("vendorcategories.aspx");
        }
        else
        {
            Session["VendorCategoryMessage"] = "File does not exist.";
            Response.Redirect("vendorcategories.aspx");
        } 
    }

    void ExportCSVfromDB()
    {
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        string sCommand, vID, VendorId, VendorName, CategoryId, CategoryName, SubCategoryId, SubCategoryName, BrandId, BrandName;
        int    IncludesAllSubCategories;
        string csvContent = "";
        SqlDataReader oReader;

        csvContent = "\"ID\"^\"VendorId\"^\"VendorName\"^\"CategoryId\"^\"CategoryName\"^\"SubCategoryId\"^\"SubCategoryName\"^\"BrandId\"^\"BrandName\"" + "\n";

        sCommand = "select t1.ID, ";
        sCommand = sCommand + " t1.ID, "; 
        sCommand = sCommand + " t1.VendorId, "; 
        sCommand = sCommand + " t2.VendorName, ";
        sCommand = sCommand + " t1.CategoryId, ";
        sCommand = sCommand + " r1.CategoryName, ";
        sCommand = sCommand + " t1.IncludesAllSubCategories, ";
        sCommand = sCommand + " t1.SubCategoryId, ";
        sCommand = sCommand + " r2.SubCategoryName, ";
        sCommand = sCommand + " t1.BrandId, ";
        sCommand = sCommand + " r3.BrandName ";
        sCommand = sCommand + "from tblVendorCategoriesAndSubcategories t1 ";
        sCommand = sCommand + "     LEFT OUTER JOIN tblVendors t2 ON t1.VendorId = t2.VendorId ";
        sCommand = sCommand + "     LEFT OUTER JOIN rfcProductCategory r1 ON t1.CategoryId = r1.CategoryId ";
        sCommand = sCommand + "     LEFT OUTER JOIN rfcProductSubCategory r2 ON t1.SubCategoryId = r2.SubCategoryId ";
        sCommand = sCommand + "     LEFT OUTER JOIN rfcProductBrands r3 ON t1.BrandId = r3.BrandId ";
        sCommand = sCommand + "ORDER BY t1.ID, t1.VendorId, t1.CategoryId, t1.SubcategoryId, t1.BrandId";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        while (oReader.Read())
        {
            vID = oReader["ID"].ToString();
            VendorId = oReader["VendorId"].ToString();
            VendorName = oReader["VendorName"].ToString();
            CategoryId = oReader["CategoryId"].ToString();
            CategoryName = oReader["CategoryName"].ToString();
            IncludesAllSubCategories = Int32.Parse(oReader["IncludesAllSubCategories"].ToString());
            SubCategoryId = oReader["SubCategoryId"].ToString();
            SubCategoryName = oReader["SubCategoryName"].ToString();
            BrandId = oReader["BrandId"].ToString();
            BrandName= oReader["BrandName"].ToString();
            csvContent = csvContent + vID + "^" + VendorId + "^" + VendorName + "^" + CategoryId + "^" + CategoryName + "^" + SubCategoryId + "^" + SubCategoryName + "^" + BrandId + "^" + BrandName + "\n";
        }
        oReader.Close();

        //string filename = string.Format("rfcProductCategory.txt", string.Format("{0:yyyy-MM-dd}", DateTime.Today));
        //System.IO.StreamWriter StreamWriter1 = new System.IO.StreamWriter(Server.MapPath( "rfcProductCategory/"+filename));
        //StreamWriter1.WriteLine(csvContent);
        //StreamWriter1.Close();

        //Download csv
        StringWriter oStringWriter = new StringWriter();
        oStringWriter.WriteLine(csvContent);
        Response.ContentType = "text/plain";
        Response.AddHeader("content-disposition", "attachment;filename=" + string.Format("tblVendorCategoriesAndSubcategories_{0}.csv", string.Format("{0:yyyy-MM-dd}", DateTime.Today)));
        Response.Clear();
        using (StreamWriter writer = new StreamWriter(Response.OutputStream, Encoding.UTF8))
        {
            writer.Write(oStringWriter.ToString());
        }
        Response.End();
    }

    private bool UpdateVendorCategoryFromCsv(string Id, string VendorId, string CategoryId, string SubCategoryId, string BrandId)
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;
        bool success = false;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@Id", SqlDbType.Int);
            sqlParams[1] = new SqlParameter("@VendorId", SqlDbType.Int);
            sqlParams[2] = new SqlParameter("@CategoryId", SqlDbType.VarChar);
            sqlParams[3] = new SqlParameter("@SubCategoryId", SqlDbType.Int);
            sqlParams[4] = new SqlParameter("@BrandId", SqlDbType.Int);

            sqlParams[0].Value = Int32.Parse(Id.ToString().Trim());
            sqlParams[1].Value = Int32.Parse(VendorId.ToString().Trim());
            sqlParams[2].Value = CategoryId.ToString().Trim();
            sqlParams[3].Value = Int32.Parse(SubCategoryId.ToString().Trim());
            sqlParams[4].Value = Int32.Parse(BrandId.ToString().Trim());

            SqlHelper.ExecuteNonQuery(sqlTransact, "sp_UpdateVendorCategoryFromCsv", sqlParams);

            sqlTransact.Commit();

            success = true;
        }
        catch
        {
            sqlTransact.Rollback();
            success = false;
        }
        finally
        {
            sqlConnect.Close();
        }
        Response.Write(success);
        return success;
    }

    protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["VendorCurrentValue"] = ddlVendor.SelectedValue;
    }

    protected void gvVendorCategories_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Controls[0].Controls.Count > 0)
            {
                if (e.Row.Controls[0].Controls[1].GetType() == typeof(TextBox))
                {
                    TextBox tb = (TextBox)e.Row.Controls[0].Controls[1];
                    tb.Text = tb.Text.Trim();
                }
            }
        }
    }
}
