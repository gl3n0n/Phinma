using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for GridViewTemplate2
/// </summary>

public class GridViewTemplate2 : ITemplate
{
    private DataControlRowType templateType;
    private string columnName;

    public GridViewTemplate2(DataControlRowType type, string colname)
    {
        templateType = type;
        columnName = colname;
    }

    public void InstantiateIn(System.Web.UI.Control container)
    {
        // Create the content for the different row types.
        switch (templateType)
        {
            case DataControlRowType.Header:
                // Create the controls to put in the header
                // section and set their properties.
                Literal lc = new Literal();
                lc.Text = "<B>" + columnName + "</B>";

                // Add the controls to the Controls collection
                // of the container.
                container.Controls.Add(lc);
                break;
            case DataControlRowType.DataRow:
                // Create the controls to put in a data row
                // section and set their properties.
                Label firstName = new Label();
                Label lastName = new Label();

                Literal spacer = new Literal();
                spacer.Text = " ";

                // To support data binding, register the event-handling methods
                // to perform the data binding. Each control needs its own event
                // handler.
                firstName.DataBinding += new EventHandler(this.FirstName_DataBinding);
                lastName.DataBinding += new EventHandler(this.LastName_DataBinding);

                // Add the controls to the Controls collection
                // of the container.
                container.Controls.Add(firstName);
                container.Controls.Add(spacer);
                container.Controls.Add(lastName);
                break;

            // Insert cases to create the content for the other 
            // row types, if desired.

            default:
                // Insert code to handle unexpected values.
                break;
        }
    }

    private void FirstName_DataBinding(Object sender, EventArgs e)
    {
        // Get the Label control to bind the value. The Label control
        // is contained in the object that raised the DataBinding 
        // event (the sender parameter).
        Label l = (Label)sender;

        // Get the GridViewRow object that contains the Label control. 
        GridViewRow row = (GridViewRow)l.NamingContainer;

        // Get the field value from the GridViewRow object and 
        // assign it to the Text property of the Label control.
        l.Text = "Test";//DataBinder.Eval(row.DataItem, "au_fname").ToString();
    }

    private void LastName_DataBinding(Object sender, EventArgs e)
    {
        // Get the Label control to bind the value. The Label control
        // is contained in the object that raised the DataBinding 
        // event (the sender parameter).
        Label l = (Label)sender;

        // Get the GridViewRow object that contains the Label control.
        GridViewRow row = (GridViewRow)l.NamingContainer;

        // Get the field value from the GridViewRow object and 
        // assign it to the Text property of the Label control.
        l.Text = "Test"; //DataBinder.Eval(row.DataItem, "au_lname").ToString();
    }
} 
