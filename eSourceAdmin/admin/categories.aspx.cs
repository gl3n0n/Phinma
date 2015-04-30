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
        if (Session["CategoryMessage"] != null)
        {
            lblMsg.Text = Session["CategoryMessage"].ToString() + "<br /><br />";
            lblMsg.Visible = true;
            Session["CategoryMessage"] = null;
        }
        else
            lblMsg.Visible = false;
    }

    protected void dsCategories_Deleted(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows > 0)
        {
            Session["CategoryMessage"] = "Category was successfully deleted.";
            Response.Redirect("categories.aspx");
        }
        else
        {
            Session["CategoryMessage"] = "Category was not deleted.";
            Response.Redirect("categories.aspx");
        }
    }

    protected void dsCategories_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows > 0)
        {
            Session["CategoryMessage"] = "Category was successfully added.";
            Response.Redirect("categories.aspx");
        }
        else
        {
            Session["CategoryMessage"] = "Category was not added.";
            Response.Redirect("categories.aspx");
        }
    }

    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            dsCategories.InsertParameters.Clear();
            dsCategories.InsertParameters.Add("CategoryId", TypeCode.String, txtCategoryId.Text.Trim());
            dsCategories.InsertParameters.Add("CategoryName", TypeCode.String, txtCategoryName.Text.Trim());
            dsCategories.InsertParameters.Add("CategoryDesc", TypeCode.String, string.Empty.ToString());
            dsCategories.Insert();
        }
    }

    protected void dsCategories_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows > 0)
        {
            Session["CategoryMessage"] = "Category was successfully updated.";
            Response.Redirect("Categories.aspx");
        }
        else
        {
            Session["CategoryMessage"] = "Category was not updated.";
            Response.Redirect("Categories.aspx");
        }
    }

    protected void dsCategories_Updating(object sender, SqlDataSourceCommandEventArgs e)
    {
        if (e.Command.Parameters["@CategoryCount"] != null)
            e.Command.Parameters.RemoveAt("@CategoryCount");
    }

    protected void dsCategories_Deleting(object sender, SqlDataSourceCommandEventArgs e)
    {
        if (e.Command.Parameters["@CategoryCount"] != null)
            e.Command.Parameters.RemoveAt("@CategoryCount");
    }

    protected void gvCategories_RowDataBound(object sender, GridViewRowEventArgs e)
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

            if (e.Row.Controls[3].Controls.Count > 0)
            {
                LinkButton delBtn = (LinkButton)e.Row.Controls[3].Controls[0];
                if (delBtn.Text == "Delete")
                {
                    int locCount = (int)gvCategories.DataKeys[e.Row.RowIndex].Values[1];
                    if (locCount > 0)
                       delBtn.Enabled = false;
                    else
                    {
                        delBtn.Enabled = true;
                        delBtn.OnClientClick = "return confirm('Are you sure you want to delete this category?');";
                    }
                }
            }

            if (e.Row.Controls[1].Controls.Count > 0)
            {
                LinkButton updateBtn = (LinkButton)e.Row.Controls[2].Controls[0];
                if (updateBtn.Text == "Update")
                {
                    e.Row.Cells[2].Width = Unit.Parse("90px");
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                    updateBtn.CausesValidation = false;
                    updateBtn.OnClientClick = "return confirm('Are you sure you want to update this category?');";
                }
                else
                {
                    e.Row.Cells[2].Width = Unit.Parse("40px");
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                }
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
                    FileUpload1.SaveAs(csvPath + "rfcProductCategory.csv");
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
        if (File.Exists(csvPath + "rfcProductCategory.csv"))
        {
            StreamReader csvContent = new StreamReader(csvPath + "rfcProductCategory.csv");
            string line = "";
            string[] row;
            string CategoryId = ""; string CategoryName = ""; string ErrCategoryId = "";
            int updateSuccessCnt = 0; int updateFailedCnt = 0;

            Console.WriteLine(csvContent.ReadLine());
            while ((line = csvContent.ReadLine()) != null)
            {
                row = line.Split('^');
                for (int i = 0; i < row.Length; i++)
                {
                    if (i == 0)
                    {
                        CategoryId = row[i].ToString().Trim();
                    }
                    else
                    {
                        CategoryName = row[i].ToString().Trim();
                    }
                }
                Response.Write(CategoryId + " : " + CategoryName + "<br>");
                if (UpdateCategoryFromCsv(CategoryId, CategoryName))
                {
                    updateSuccessCnt++;
                }
                else
                {
                    updateFailedCnt++;
                    if (ErrCategoryId.Length == 0) {
                       ErrCategoryId = CategoryId;
                    }
                    else {
                       ErrCategoryId = ErrCategoryId + '^' + CategoryId;
                    }
                }

            }
            csvContent.Close();
            File.Delete(csvPath + "rfcProductCategory.csv");

            if (ErrCategoryId.Length == 0) { 
               Session["SubCategoryMessage"] = "[" + updateSuccessCnt.ToString() + "] Sub-Categories were successfully updated.  [" + updateFailedCnt.ToString() + "] Failed.";
            }
            else {
               Session["SubCategoryMessage"] = "[" + updateSuccessCnt.ToString() + "] Sub-Categories were successfully updated.  [" + updateFailedCnt.ToString() + "] Failed. <br>List of Failed [" + ErrCategoryId.ToString() + "]";
            }
            Session["CategoryMessage"] = "[" + updateSuccessCnt.ToString() + "] Categories were successfully updated.  [" + updateFailedCnt.ToString() + "] Failed.";
            Response.Redirect("categories.aspx");
        }
        else
        {
            Session["CategoryMessage"] = "No uploaded CVS file to process.";
            Response.Redirect("categories.aspx");
        } 
    }

    void ExportCSVfromDB()
    {
        //string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        string sCommand, CategoryId, CategoryName, CategoryDesc;
        string csvContent = "";
        SqlDataReader oReader;

        csvContent = "\"CategoryId\"^\"CategoryName\"" + "\n";

        sCommand = "SELECT * FROM rfcProductCategory ORDER BY CategoryId";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        while (oReader.Read())
        {
            CategoryId = oReader["CategoryId"].ToString();
            CategoryName = oReader["CategoryName"].ToString();
            csvContent = csvContent + CategoryId + "^" + CategoryName + "\n";
        }
        oReader.Close();

        //Download csv
        StringWriter oStringWriter = new StringWriter();
        oStringWriter.WriteLine(csvContent);
        Response.ContentType = "text/plain";
        Response.AddHeader("content-disposition", "attachment;filename=" + string.Format("rfcProductCategory_{0}.csv", string.Format("{0:yyyy-MM-dd}", DateTime.Today)));
        Response.Clear();
        using (StreamWriter writer = new StreamWriter(Response.OutputStream, Encoding.UTF8))
        {
            writer.Write(oStringWriter.ToString());
        }
        Response.End();
    }

    private bool UpdateCategoryFromCsv(string CategoryId, string CategoryName)
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;
        bool success = false;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@CategoryId", SqlDbType.Int);
            sqlParams[1] = new SqlParameter("@CategoryName", SqlDbType.VarChar);
            sqlParams[2] = new SqlParameter("@CategoryDesc", SqlDbType.VarChar);

            sqlParams[0].Value = CategoryId.ToString().Trim();
            sqlParams[1].Value = CategoryName.ToString().Trim();
            sqlParams[2].Value = "";

            SqlHelper.ExecuteNonQuery(sqlTransact, "sp_UpdateCategoryFromCsv", sqlParams);

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
