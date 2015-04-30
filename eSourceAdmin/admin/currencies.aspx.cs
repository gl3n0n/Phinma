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
using EBid.lib;
using System.Data.SqlClient;

public partial class admin_currencies : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CurrencyMessage"] != null)
        {
            lblMsg.Text = Session["CurrencyMessage"].ToString() + "<br /><br />";
            lblMsg.Visible = true;
            Session["CurrencyMessage"] = null;
        }
        else
            lblMsg.Visible = false;

        if (!IsPostBack)
        {
            hdnPHPToUSD.Value = GetPHPToUSDRate().ToString();
            clndrRateAsOf.Text = DateTime.Now.ToShortDateString();
            txtRateToUSD.Attributes.Add("onkeydown", "return DigitsOnly(event, '" + txtRateToUSD.ClientID + "');");            
            txtRateToUSD.Attributes.Add("onkeyup", "Compute('" + txtRateToUSD.ClientID + "','" + txtRateToPHP.ClientID + "','" + hdnPHPToUSD.ClientID + "');");
        }
    }

    private object GetPHPToUSDRate()
    {
        return SqlHelper.ExecuteScalar(dsCurrencies.ConnectionString, CommandType.StoredProcedure, "sp_GetExchangeRateToPHP");
    }

    protected void gvCurrencies_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowState == DataControlRowState.Edit)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.Controls[2].Controls.Count > 0) && (e.Row.Controls[3].Controls.Count > 0))
                {
                    TextBox tb = (TextBox)e.Row.Controls[2].Controls[1];
                    Label lbl = (Label)e.Row.Controls[3].Controls[1];
                    string id = gvCurrencies.DataKeys[e.Row.RowIndex].Values[1].ToString();
                    tb.Attributes.Add("onkeydown", "return DigitsOnly(event, '" + tb.ClientID + "');");
                    if (id != "PHP")
                        tb.Attributes.Add("onkeyup", "Compute('" + tb.ClientID + "','" + lbl.ClientID + "','" + hdnPHPToUSD.ClientID + "');");
                }
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Controls[6].Controls.Count > 0)
            {
                LinkButton delBtn = (LinkButton)e.Row.Controls[6].Controls[0];
                if (delBtn.Text == "Delete")
                {
                    bool deletable = (bool)gvCurrencies.DataKeys[e.Row.RowIndex].Values[0];
                    if (deletable)
                    {
                        delBtn.Enabled = true;
                        delBtn.OnClientClick = "return confirm('Are you sure you want to delete this currency?');";
                    }
                    else
                        delBtn.Enabled = false;
                }
            }
            if (e.Row.Controls[5].Controls.Count > 0)
            {
                LinkButton editBtn = (LinkButton)e.Row.Controls[5].Controls[0];
                string id = gvCurrencies.DataKeys[e.Row.RowIndex].Values[1].ToString();
                if (id == "USD")
                    editBtn.Enabled = false;
            }
            if (e.Row.Controls[5].Controls.Count > 0)
            {
                LinkButton updateBtn = (LinkButton)e.Row.Controls[5].Controls[0];
                if (updateBtn.Text == "Update")
                {
                    e.Row.Cells[5].Width = Unit.Parse("90px");
                    e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
                    updateBtn.CausesValidation = false;
                    updateBtn.OnClientClick = "return confirm('Are you sure you want to update this currency?');";
                }
                else
                {
                    e.Row.Cells[5].Width = Unit.Parse("40px");
                    e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
                }
            }
        }
    }

    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            double d = double.Parse(hdnPHPToUSD.Value) / double.Parse(txtRateToUSD.Text.Trim());
            dsCurrencies.InsertParameters.Clear();
            dsCurrencies.InsertParameters.Add("ID", TypeCode.String, txtID.Text.Trim().ToUpper());
            dsCurrencies.InsertParameters.Add("Currency", TypeCode.String, txtCurrency.Text.Trim());
            dsCurrencies.InsertParameters.Add("RateToUSD", TypeCode.Decimal, txtRateToUSD.Text.Trim());
            dsCurrencies.InsertParameters.Add("RateToPHP", TypeCode.Decimal, d.ToString());
            dsCurrencies.InsertParameters.Add("AsOf", TypeCode.DateTime, clndrRateAsOf.Text.Trim());
            dsCurrencies.Insert();
        }
    }

    protected void dsCurrencies_Updating(object sender, SqlDataSourceCommandEventArgs e)
    {
        if (e.Command.Parameters["@Deletable"] != null)
            e.Command.Parameters.RemoveAt("@Deletable");
        if (e.Command.Parameters["@RateToPHP"] != null)
            e.Command.Parameters.RemoveAt("@RateToPHP");
    }

    protected void dsCurrencies_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows > 0)
        {
            Session["CurrencyMessage"] = "Currency was successfully updated.";
            Response.Redirect("currencies.aspx");
        }
        else
        {
            Session["CurrencyMessage"] = "Currency was not updated.";
            Response.Redirect("currencies.aspx");
        }
    }

    protected void dsCurrencies_Deleting(object sender, SqlDataSourceCommandEventArgs e)
    {
        if (e.Command.Parameters["@Deletable"] != null)
            e.Command.Parameters.RemoveAt("@Deletable");        
    }

    protected void dsCurrencies_Deleted(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows > 0)
        {
            Session["CurrencyMessage"] = "Currency was successfully deleted.";
            Response.Redirect("currencies.aspx");
        }
        else
        {
            Session["CurrencyMessage"] = "Currency was not deleted.";
            Response.Redirect("currencies.aspx");
        }
    }

    protected void dsCurrencies_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows > 0)
        {
            Session["CurrencyMessage"] = "Currency was successfully added.";
            Response.Redirect("currencies.aspx");
        }
        else
        {
            Session["CurrencyMessage"] = "Currency was not added.";
            Response.Redirect("currencies.aspx");
        }
    }
}
