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
        if (Session["LocationMessage"] != null)
        {
            lblMsg.Text = Session["LocationMessage"].ToString() + "<br /><br />";
            lblMsg.Visible = true;
            Session["LocationMessage"] = null;
        }
        else        
            lblMsg.Visible = false;

        if (!IsPostBack)
        {
            IEnumerator items = dsLocations.Select(DataSourceSelectArguments.Empty).GetEnumerator();
            SortedList sl = new SortedList();

            while (items.MoveNext())
            {
                DataRowView dv = (DataRowView)items.Current;
                string s = dv.Row["LocationName"].ToString().Trim();
                sl.Add(s, s);
            }
            ViewState["LocationList"] = sl;
        }
    }    
    
    protected void gvLocations_RowDataBound(object sender, GridViewRowEventArgs e)
    {        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Controls[2].Controls.Count > 0)
            {
                LinkButton delBtn = (LinkButton)e.Row.Controls[2].Controls[0];
                if (delBtn.Text == "Delete")
                {
                    int locCount = (int)gvLocations.DataKeys[e.Row.RowIndex].Values[1];
                    if (locCount > 0)
                        delBtn.Enabled = false;
                    else
                    {
                        delBtn.Enabled = true;                          
                        delBtn.OnClientClick = "return confirm('Are you sure you want to delete this location?');";
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
                    updateBtn.OnClientClick = "return confirm('Are you sure you want to update this location?');";
                }
                else
                {
                    e.Row.Cells[1].Width = Unit.Parse("40px");
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                }
            }
        }
    }
    
    protected void dsLocations_Updating(object sender, SqlDataSourceCommandEventArgs e)
    {
        if (e.Command.Parameters["@LocationCount"] != null)
            e.Command.Parameters.RemoveAt("@LocationCount");
    }

    protected void dsLocations_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows > 0)
        {
            Session["LocationMessage"] = "Location was successfully updated.";
            Response.Redirect("locations.aspx");
        }
        else
        {
            Session["LocationMessage"] = "Location was not updated.";
            Response.Redirect("locations.aspx");
        }
    }

    protected void dsLocations_Deleting(object sender, SqlDataSourceCommandEventArgs e)
    {
        if (e.Command.Parameters["@LocationCount"] != null)
            e.Command.Parameters.RemoveAt("@LocationCount");
    }

    protected void dsLocations_Deleted(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows > 0)
        {
            Session["LocationMessage"] = "Location was successfully deleted.";
            Response.Redirect("locations.aspx");
        }
        else
        {
            Session["LocationMessage"] = "Location was not deleted.";
            Response.Redirect("locations.aspx");
        }
    }

    protected void dsLocations_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows > 0)
        {
            Session["LocationMessage"] = "Location was successfully added.";
            Response.Redirect("locations.aspx");
        }
        else
        {
            Session["LocationMessage"] = "Location was not added.";
            Response.Redirect("locations.aspx");
        }
    }

    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            dsLocations.InsertParameters.Clear();
            dsLocations.InsertParameters.Add("LocationName", TypeCode.String, txtLocationName.Text.Trim());
            dsLocations.Insert();
        }
    }

    protected void cvItem_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = !Contains(txtLocationName.Text.Trim());
    }

    private bool Contains(string name)
    {
        if (ViewState["LocationList"] != null)
        {
            SortedList sl = (SortedList)ViewState["LocationList"];

            return sl.Contains(name);
        }
        else
            return false;
    }
}
