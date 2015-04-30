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
                ReadCsvAndUpdateDB();
            }
            if (control1 == "lnkDownloadCSVfromDB")
            {
                ExportCSVfromDB();
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        if (Session["BrandMessage"] != null)
        {
            lblMsg.Text = Session["BrandMessage"].ToString() + "<br /><br />";
            lblMsg.Visible = true;
            Session["BrandMessage"] = null;
        }
        else
            lblMsg.Visible = false;

        if (!IsPostBack)
        {
            IEnumerator items = dsBrands.Select(DataSourceSelectArguments.Empty).GetEnumerator();
            SortedList sl = new SortedList();

            while (items.MoveNext())
            {
                DataRowView dv = (DataRowView)items.Current;
                string s = dv.Row["BrandName"].ToString().Trim();
                sl.Add(s, s);                
            }
            ViewState["BrandsList"] = sl;      

        }
    }

    protected void gvBrands_RowDataBound(object sender, GridViewRowEventArgs e)
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

            if (e.Row.Controls[2].Controls.Count > 0)
            {
                LinkButton delBtn = (LinkButton)e.Row.Controls[2].Controls[0];
                if (delBtn.Text == "Delete")
                {
                    int locCount = (int)gvBrands.DataKeys[e.Row.RowIndex].Values[1];
                    if (locCount > 0)
                        delBtn.Enabled = false;
                    else
                    {
                        delBtn.Enabled = true;
                        delBtn.OnClientClick = "return confirm('Are you sure you want to delete this brand?');";
                    }
                }
            }

            if (e.Row.Controls[1].Controls.Count > 0)
            {
                LinkButton updateBtn = (LinkButton)e.Row.Controls[2].Controls[0];
                if (updateBtn.Text == "Update")
                {
                    e.Row.Cells[1].Width = Unit.Parse("90px");
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                    updateBtn.CausesValidation = false;
                    updateBtn.OnClientClick = "return confirm('Are you sure you want to update this brand?');";
                }
                else
                {
                    e.Row.Cells[1].Width = Unit.Parse("40px");
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                }
            }
        }
    }

    protected void dsBrands_Updating(object sender, SqlDataSourceCommandEventArgs e)
    {
        if (e.Command.Parameters["@BrandCount"] != null)
            e.Command.Parameters.RemoveAt("@BrandCount");

    }

    protected void dsBrands_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows > 0)
        {
            Session["BrandMessage"] = "Brand was successfully updated.";
            Response.Redirect("brands.aspx");
        }
        else
        {
            Session["BrandMessage"] = "Brand was not updated.";
            Response.Redirect("brands.aspx");
        }
    }

    protected void dsBrands_Deleting(object sender, SqlDataSourceCommandEventArgs e)
    {
        if (e.Command.Parameters["@BrandCount"] != null)
            e.Command.Parameters.RemoveAt("@BrandCount");
    }

    protected void dsBrands_Deleted(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows > 0)
        {
            Session["BrandMessage"] = "Brand was successfully deleted.";
            Response.Redirect("brands.aspx");
        }
        else
        {
            Session["BrandMessage"] = "Brand was not deleted.";
            Response.Redirect("brands.aspx");
        }
    }

    protected void dsBrands_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows > 0)
        {
            Session["BrandMessage"] = "Brand was successfully added.";
            Response.Redirect("brands.aspx");
        }
        else
        {
            Session["BrandMessage"] = "Brand was not added.";
            Response.Redirect("brands.aspx");
        }
    }

    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            dsBrands.InsertParameters.Clear();
            dsBrands.InsertParameters.Add("BrandName", TypeCode.String, txtBrandName.Text.Trim());
            dsBrands.InsertParameters.Add("SubCategoryId", TypeCode.String, ddlSubCategories.SelectedValue.Trim());
            dsBrands.Insert();
        }
    }

    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = !Contains(txtBrandName.Text.Trim());
    }

    private bool Contains(string name)
    {
        if (ViewState["BrandsList"] != null)
        {
            SortedList sl = (SortedList)ViewState["BrandsList"];

            return sl.Contains(name);
        }
        else
            return false;
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
                    FileUpload1.SaveAs(csvPath + "rfcProductBrands.csv");
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
        if (File.Exists(csvPath + "rfcProductBrands.csv"))
        {
            StreamReader csvContent = new StreamReader(csvPath + "rfcProductBrands.csv");
            string line = "";
            string[] row;
            string BrandId = ""; string BrandName = ""; string SubCategoryId = ""; string ErrBrandId = "";
            int updateSuccessCnt = 0; int updateFailedCnt = 0;

            Console.WriteLine(csvContent.ReadLine());
            while ((line = csvContent.ReadLine()) != null)
            {
                row = line.Split('^');
                for (int i = 0; i < row.Length; i++)
                {
                    if (i == 0)
                    {
                        BrandId = row[i].ToString().Trim();
                    }
                    if (i == 1)
                    {
                        BrandName = row[i].ToString().Trim();
                    }
                    if (i == 2)
                    {
                        SubCategoryId = row[i].ToString().Trim();
                    }
                }
                Response.Write(BrandId + " : " + BrandName  + " : " + SubCategoryId + "<br>");
                if (SubCategoryId.Length == 0) {SubCategoryId = "0"; }
                if (UpdateBrandFromCsv(BrandId, BrandName, SubCategoryId))
                {
                    updateSuccessCnt++;
                }
                else
                { 
                    updateFailedCnt++;
                    if (ErrBrandId.Length == 0) {
                       ErrBrandId = BrandId + ' ';
                    }
                    else {
                       ErrBrandId = ErrBrandId + '^' + BrandId + ' ';
                    }
                }

            }
            csvContent.Close();

            File.Delete(csvPath + "rfcProductBrands.csv");
            if (ErrBrandId.Length == 0) { 
               Session["BrandMessage"] = "[" + updateSuccessCnt.ToString() + "] Brands were successfully updated.  [" + updateFailedCnt.ToString() + "] Failed.";
            }
            else {
               Session["BrandMessage"] = "[" + updateSuccessCnt.ToString() + "] Brands were successfully updated.  [" + updateFailedCnt.ToString() + "] Failed. <br>List of Failed [" + ErrBrandId.ToString() + "]";
            }
            Response.Redirect("brands.aspx");
        }
        else
        {
            Session["BrandMessage"] = "File does not exist.";
            Response.Redirect("brands.aspx");
        } 
    }

    void ExportCSVfromDB()
    {
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        string sCommand, BrandId, BrandName, SubCategoryId, SubCategoryName, CategoryId, CategoryName;
        string csvContent = "";
        SqlDataReader oReader;

        csvContent = "\"BrandId\"^\"BrandName\"^\"SubCategoryId\"^\"SubCategoryName\"^\"CategoryID\"^\"CategoryName\"" + "\n";

        sCommand = "SELECT t1.BrandID, t1.BrandName, t2.SubCategoryId, t2.SubCategoryName, t3.CategoryId, t3.CategoryName ";
        sCommand = sCommand + "FROM rfcProductBrands t1 ";
        sCommand = sCommand + "LEFT OUTER JOIN rfcProductSubCategory t2 ON t1.SubCategoryId = t2.SubCategoryId ";
        sCommand = sCommand + "LEFT OUTER JOIN rfcProductCategory t3 ON t2.CategoryId = t3.CategoryId ";
        sCommand = sCommand + "ORDER BY t1.SubCategoryId";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        while (oReader.Read())
        {
            BrandId = oReader["BrandId"].ToString();
            BrandName = oReader["BrandName"].ToString();
            SubCategoryId = oReader["SubCategoryId"].ToString();
            SubCategoryName = oReader["SubCategoryName"].ToString();
            CategoryId = oReader["CategoryId"].ToString();
            CategoryName = oReader["CategoryName"].ToString();
            csvContent = csvContent + BrandId + "^" + BrandName + "^" + SubCategoryId + "^" + SubCategoryName + "^" + CategoryId  + "^" + CategoryName + "\n";
        }
        oReader.Close();

        //Download csv
        StringWriter oStringWriter = new StringWriter();
        oStringWriter.WriteLine(csvContent);
        Response.ContentType = "text/plain";
        Response.AddHeader("content-disposition", "attachment;filename=" + string.Format("rfcProductBrands_{0}.csv", string.Format("{0:yyyy-MM-dd}", DateTime.Today)));
        Response.Clear();
        using (StreamWriter writer = new StreamWriter(Response.OutputStream, Encoding.UTF8))
        {
            writer.Write(oStringWriter.ToString());
        }
        Response.End();
    }

    private bool UpdateBrandFromCsv(string BrandId, string BrandName, string SubCategoryId)
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;
        bool success = false;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@BrandId", SqlDbType.Int);
            sqlParams[1] = new SqlParameter("@BrandName", SqlDbType.VarChar);
            sqlParams[2] = new SqlParameter("@SubCategoryId", SqlDbType.Int);

            sqlParams[0].Value = Int32.Parse(BrandId.ToString().Trim());
            sqlParams[1].Value = BrandName.ToString().Trim();
            sqlParams[2].Value = Int32.Parse(SubCategoryId.ToString().Trim());

            SqlHelper.ExecuteNonQuery(sqlTransact, "sp_UpdateBrandFromCsv", sqlParams);

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

}
