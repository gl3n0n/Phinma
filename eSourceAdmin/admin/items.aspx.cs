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

public partial class admin_items : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ItemMessage"] != null)
        {
            lblMsg.Text = Session["ItemMessage"].ToString() + "<br /><br />";
            lblMsg.Visible = true;
            Session["ItemMessage"] = null;
        }
        else
            lblMsg.Visible = false;

        if (!IsPostBack)
        {
            IEnumerator items = dsItems.Select(DataSourceSelectArguments.Empty).GetEnumerator();
            SortedList sl = new SortedList();

            while (items.MoveNext())
            {
                DataRowView dv = (DataRowView)items.Current;
                string s = dv.Row["ItemsCarried"].ToString().Trim();
                sl.Add(s, s);
            }
            ViewState["ItemsList"] = sl;
        }
    }

    protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Controls[2].Controls.Count > 0)
            {
                LinkButton delBtn = (LinkButton)e.Row.Controls[2].Controls[0];
                if (delBtn.Text == "Delete")
                {
                    int locCount = (int)gvItems.DataKeys[e.Row.RowIndex].Values[1];
                    if (locCount > 0)
                        delBtn.Enabled = false;
                    else
                    {
                        delBtn.Enabled = true;
                        delBtn.OnClientClick = "return confirm('Are you sure you want to delete this item?');";
                    }
                }
            }

            if (e.Row.Controls[1].Controls.Count > 0)
            {
                LinkButton updateBtn = (LinkButton)e.Row.Controls[1].Controls[0];
                if (updateBtn.Text == "Update")
                {
                    e.Row.Cells[1].Width = Unit.Parse("90px");
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                    updateBtn.CausesValidation = false;
                    updateBtn.OnClientClick = "return confirm('Are you sure you want to update this item?');";
                }
                else
                {
                    e.Row.Cells[1].Width = Unit.Parse("40px");
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                }
            }
        }
    }

    protected void dsItems_Updating(object sender, SqlDataSourceCommandEventArgs e)
    {
        if (e.Command.Parameters["@ItemCount"] != null)
            e.Command.Parameters.RemoveAt("@ItemCount");
    }

    protected void dsItems_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows > 0)
        {
            Session["ItemMessage"] = "Item was successfully updated.";
            Response.Redirect("items.aspx");
        }
        else
        {
            Session["ItemMessage"] = "Item was not updated.";
            Response.Redirect("items.aspx");
        }
    }

    protected void dsItems_Deleting(object sender, SqlDataSourceCommandEventArgs e)
    {
        if (e.Command.Parameters["@ItemCount"] != null)
            e.Command.Parameters.RemoveAt("@ItemCount");
    }

    protected void dsItems_Deleted(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows > 0)
        {
            Session["ItemMessage"] = "Item was successfully deleted.";
            Response.Redirect("items.aspx");
        }
        else
        {
            Session["ItemMessage"] = "Item was not deleted.";
            Response.Redirect("items.aspx");
        }
    }

    protected void dsItems_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows > 0)
        {
            Session["ItemMessage"] = "Item was successfully added.";
            Response.Redirect("items.aspx");
        }
        else
        {
            Session["ItemMessage"] = "Item was not added.";
            Response.Redirect("items.aspx");
        }
    }

    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            dsItems.InsertParameters.Clear();
            dsItems.InsertParameters.Add("ItemsCarried", TypeCode.String, txtItemName.Text.Trim());
            dsItems.Insert();
        }
    }

    protected void cvItem_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = !Contains(txtItemName.Text.Trim());
    }

    private bool Contains(string name)
    {
        if (ViewState["ItemsList"] != null)
        {
            SortedList sl = (SortedList)ViewState["ItemsList"];

            return sl.Contains(name);
        }
        else
            return false;
    }
}