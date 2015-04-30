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
    bool isEdit = false;    
    private string connstring;
    private string csvPath = (System.AppDomain.CurrentDomain.BaseDirectory + "admin\\rfcProductCategory\\");

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        connstring = HttpContext.Current.Session["ConnectionString"].ToString();
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
        if (Session["SubCategoryMessage"] != null)
        {
            lblMsg.Text = Session["SubCategoryMessage"].ToString() + "<br /><br />";
            lblMsg.Visible = true;
            Session["SubCategoryMessage"] = null;
        }
        else
            lblMsg.Visible = false;

        if (!IsPostBack)
        {
            if (Session["SubCategoryCurrentValue"] != null)
            {
                ddlCategory.SelectedValue = Session["SubCategoryCurrentValue"].ToString();
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
                    FileUpload1.SaveAs(csvPath + "rfcProductSubCategory.csv");
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
        if (File.Exists(csvPath + "rfcProductSubCategory.csv"))
        {
            StreamReader csvContent = new StreamReader(csvPath + "rfcProductSubCategory.csv");
            string line = "";
            string[] row;
            string SubCategoryId = ""; string SubCategoryName = ""; string CategoryId = ""; string ErrSubCategoryId = "";
            int updateSuccessCnt = 0; int updateFailedCnt = 0;

            Console.WriteLine(csvContent.ReadLine());
            while ((line = csvContent.ReadLine()) != null)
            {
                row = line.Split('^');
                for (int i = 0; i < row.Length; i++)
                {
                    if (i == 0)
                    {
                        SubCategoryId = row[i].ToString().Trim();
                    }
                    if (i == 1)
                    {
                        SubCategoryName = row[i].ToString().Trim();
                    }
                    if (i == 2)
                    {
                        CategoryId = row[i].ToString().Trim();
                    }
                }
                Response.Write(SubCategoryId + " : " + SubCategoryName  + " : " + CategoryId + "<br>");
                if (UpdateSubCategoryFromCsv(SubCategoryId, SubCategoryName, CategoryId))
                {
                    updateSuccessCnt++;
                }
                else
                { 
                    updateFailedCnt++;
                    if (ErrSubCategoryId.Length == 0) {
                       ErrSubCategoryId = SubCategoryId;
                    }
                    else {
                       ErrSubCategoryId = ErrSubCategoryId + '^' + SubCategoryId;
                    }
                }

            }
            csvContent.Close();

            File.Delete(csvPath + "rfcProductSubCategory.csv");
            if (ErrSubCategoryId.Length == 0) { 
               Session["SubCategoryMessage"] = "[" + updateSuccessCnt.ToString() + "] Sub-Categories were successfully updated.  [" + updateFailedCnt.ToString() + "] Failed.";
            }
            else {
               Session["SubCategoryMessage"] = "[" + updateSuccessCnt.ToString() + "] Sub-Categories were successfully updated.  [" + updateFailedCnt.ToString() + "] Failed. <br>List of Failed [" + ErrSubCategoryId.ToString() + "]";
            }
            Response.Redirect("subcategories.aspx");
        }
        else
        {
            Session["SubCategoryMessage"] = "File does not exist.";
            Response.Redirect("subcategories.aspx");
        } 
    }

    void ExportCSVfromDB()
    {
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        string sCommand, CategoryId, SubCategoryName, SubCategoryDesc, CategoryName;
        string SubCategoryId;
        string csvContent = "";
        SqlDataReader oReader;

        csvContent = "\"SubCategoryId\"^\"SubCategoryName\"^\"CategoryID\"^\"CategoryName\"" + "\n";

        sCommand = "SELECT t1.*, t2.CategoryName FROM rfcProductSubCategory t1 LEFT OUTER JOIN rfcProductCategory t2 ON t1.CategoryId = t2.CategoryId ORDER BY t1.SubCategoryId";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        while (oReader.Read())
        {
            SubCategoryId = oReader["SubCategoryId"].ToString();
            SubCategoryName = oReader["SubCategoryName"].ToString();
            CategoryId = oReader["CategoryId"].ToString();
            CategoryName = oReader["CategoryName"].ToString();
            csvContent = csvContent + SubCategoryId + "^" + SubCategoryName + "^" + CategoryId  + "^" + CategoryName + "\n";
        }
        oReader.Close();

        //Download csv
        StringWriter oStringWriter = new StringWriter();
        oStringWriter.WriteLine(csvContent);
        Response.ContentType = "text/plain";
        Response.AddHeader("content-disposition", "attachment;filename=" + string.Format("rfcProductSubCategory_{0}.csv", string.Format("{0:yyyy-MM-dd}", DateTime.Today)));
        Response.Clear();
        using (StreamWriter writer = new StreamWriter(Response.OutputStream, Encoding.UTF8))
        {
            writer.Write(oStringWriter.ToString());
        }
        Response.End();
    }

    private bool UpdateSubCategoryFromCsv(string SubCategoryId, string SubCategoryName, string CategoryId)
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;
        bool success = false;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@SubCategoryId", SqlDbType.Int);
            sqlParams[1] = new SqlParameter("@SubCategoryName", SqlDbType.VarChar);
            sqlParams[2] = new SqlParameter("@SubCategoryDesc", SqlDbType.VarChar);
            sqlParams[3] = new SqlParameter("@CategoryId", SqlDbType.VarChar);

            sqlParams[0].Value = Int32.Parse(SubCategoryId.ToString().Trim());
            sqlParams[1].Value = SubCategoryName.ToString().Trim();
            sqlParams[2].Value = "";
            sqlParams[3].Value = CategoryId.ToString().Trim();

            SqlHelper.ExecuteNonQuery(sqlTransact, "sp_UpdateSubCategoryFromCsv", sqlParams);

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

    protected void gvSubCategories_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        isEdit = (e.CommandName == "Edit");         
    }

    protected void gvSubCategories_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (isEdit)
            {
                DropDownList ddl = e.Row.FindControl("ddlCategories") as DropDownList;

                if (ddl != null)
                {
                    ddl.SelectedValue = ddlCategory.SelectedValue;
                }                
            }

            if (e.Row.RowState == DataControlRowState.Edit)
            {
                LinkButton updateBtn = (LinkButton)e.Row.Controls[2].Controls[0];
                updateBtn.CausesValidation = false;
                updateBtn.OnClientClick = "return confirm('Are you sure you want to update this sub category?');";
            }

            if (e.Row.Controls[3].Controls.Count > 0)
            {
                LinkButton delBtn = (LinkButton)e.Row.Controls[3].Controls[0];
                if (delBtn.Text == "Delete")
                {
                    
                    delBtn.Enabled = true;
                    delBtn.OnClientClick = "return confirm('Are you sure you want to delete this sub category?');";
                   
                }
            }
        }
    }

    public static Control FindControl(string id, ControlCollection col)
    {
        foreach (Control c in col)
        {
            Control child = FindControlRecursive(c, id);
            if (child != null)
                return child;
        }
        return null;
    }

    private static Control FindControlRecursive(Control root, string id)
    {
        if (root.ID != null && root.ID == id)
            return root;

        foreach (Control c in root.Controls)
        {
            Control rc = FindControlRecursive(c, id);
            if (rc != null)
                return rc;
        }
        return null;
    }

    protected void dsSubCategories_Updating(object sender, SqlDataSourceCommandEventArgs e)
    {
        if (!e.Command.Parameters.Contains("@CategoryId"))
        {
            DropDownList ddl = (DropDownList)FindControl("ddlCategories", gvSubCategories.Controls);
            SqlParameter param = new SqlParameter("@CategoryId", SqlDbType.NVarChar, 7);
            param.Value = ddl.SelectedValue;
            e.Command.Parameters.Add(param);
        }
    }

    protected void dsSubCategories_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows > 0)
        {
            Session["SubCategoryMessage"] = "Sub Category was successfully updated.";
            Response.Redirect("subcategories.aspx");
        }
        else
        {
            Session["SubCategoryMessage"] = "Sub Category was not updated.";
            Response.Redirect("subcategories.aspx");
        }
    }

    protected void dsSubCategories_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows > 0)
        {
            Session["SubCategoryMessage"] = "Sub Category was successfully added.";
            Response.Redirect("subcategories.aspx");
        }
        else
        {
            Session["SubCategoryMessage"] = "Sub Category was not added.";
            Response.Redirect("subcategories.aspx");
        }
    }

    protected void dsSubCategories_Deleted(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows > 0)
        {
            Session["SubCategoryMessage"] = "Sub Category was successfully deleted.";
            Response.Redirect("subcategories.aspx");
        }
        else
        {
            Session["SubCategoryMessage"] = "Sub Category was not deleted.";
            Response.Redirect("subcategories.aspx");
        }
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["SubCategoryCurrentValue"] = ddlCategory.SelectedValue;
    }

    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            dsSubCategories.InsertParameters.Clear();
            dsSubCategories.InsertParameters.Add("SubCategoryName", TypeCode.String, txtSubCategoryName.Text.Trim());
            dsSubCategories.InsertParameters.Add("SubCategoryDesc", TypeCode.String, string.Empty.ToString());
            dsSubCategories.InsertParameters.Add("CategoryId", TypeCode.String, ddlCategory.SelectedValue.ToString());
            dsSubCategories.Insert();
        }
    }
}
