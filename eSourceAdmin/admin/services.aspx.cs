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
        if (Session["ServiceMessage"] != null)
        {
            lblMsg.Text = Session["ServiceMessage"].ToString() + "<br /><br />";
            lblMsg.Visible = true;
            Session["ServiceMessage"] = null;
        }
        else
            lblMsg.Visible = false;

        if (!IsPostBack)
        {
            IEnumerator items = dsServices.Select(DataSourceSelectArguments.Empty).GetEnumerator();
            SortedList sl = new SortedList();

            while (items.MoveNext())
            {
                DataRowView dv = (DataRowView)items.Current;
                string s = dv.Row["ServiceName"].ToString().Trim();
                sl.Add(s, s);
            }
            ViewState["ServiceList"] = sl;
        }
    }

    protected void gvServices_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Controls[2].Controls.Count > 0)
            {
                LinkButton delBtn = (LinkButton)e.Row.Controls[2].Controls[0];
                if (delBtn.Text == "Delete")
                {
                    int locCount = (int)gvServices.DataKeys[e.Row.RowIndex].Values[1];
                    if (locCount > 0)
                        delBtn.Enabled = false;
                    else
                    {
                        delBtn.Enabled = true;
                        delBtn.OnClientClick = "return confirm('Are you sure you want to delete this service?');";
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
                    updateBtn.OnClientClick = "return confirm('Are you sure you want to update this service?');";
                }
                else
                {
                    e.Row.Cells[1].Width = Unit.Parse("40px");
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                }
            }
        }
    }

    protected void dsServices_Updating(object sender, SqlDataSourceCommandEventArgs e)
    {
        if (e.Command.Parameters["@ServiceCount"] != null)
            e.Command.Parameters.RemoveAt("@ServiceCount");
    }

    protected void dsServices_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows > 0)
        {
            Session["ServiceMessage"] = "Service was successfully updated.";
            Response.Redirect("services.aspx");
        }
        else
        {
            Session["ServiceMessage"] = "Service was not updated.";
            Response.Redirect("services.aspx");
        }
    }

    protected void dsServices_Deleting(object sender, SqlDataSourceCommandEventArgs e)
    {
        if (e.Command.Parameters["@ServiceCount"] != null)
            e.Command.Parameters.RemoveAt("@ServiceCount");
    }

    protected void dsServices_Deleted(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows > 0)
        {
            Session["ServiceMessage"] = "Service was successfully deleted.";
            Response.Redirect("services.aspx");
        }
        else
        {
            Session["ServiceMessage"] = "Service was not deleted.";
            Response.Redirect("services.aspx");
        }
    }

    protected void dsServices_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows > 0)
        {
            Session["ServiceMessage"] = "Service was successfully added.";
            Response.Redirect("services.aspx");
        }
        else
        {
            Session["ServiceMessage"] = "Service was not added.";
            Response.Redirect("services.aspx");
        }
    }

    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            dsServices.InsertParameters.Clear();
            dsServices.InsertParameters.Add("ServiceName", TypeCode.String, txtServiceName.Text.Trim());
            dsServices.Insert();
        }
    }

    protected void cvItem_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = !Contains(txtServiceName.Text.Trim());
    }

    private bool Contains(string name)
    {
        if (ViewState["ServiceList"] != null)
        {
            SortedList sl = (SortedList)ViewState["ServiceList"];

            return sl.Contains(name);
        }
        else
            return false;
    }
}
