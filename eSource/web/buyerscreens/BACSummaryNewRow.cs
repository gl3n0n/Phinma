using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;

public class MyTemplate : System.Web.UI.ITemplate
{
    System.Web.UI.WebControls.ListItemType templateType;
    public MyTemplate(System.Web.UI.WebControls.ListItemType type)
    {
        templateType = type;
    }

    public void InstantiateIn(System.Web.UI.Control container)
    {
        PlaceHolder ph = new PlaceHolder();
        TextBox oCrit1 = new TextBox();
        oCrit1.ID = "item" ;
        oCrit1.Width = 120;

        switch (templateType)
        {
            case ListItemType.Header:
                ph.Controls.Add(new LiteralControl("<td runat='server'><span style='width: 150px;'>"));
                //ph.Controls.Add(new LiteralControl("<td><table border=\"0\"><tbody><tr>"));
                break;
            case ListItemType.Item:
                ph.Controls.Add(new LiteralControl("<td runat='server'>"));
                ph.Controls.Add(oCrit1);
                ph.Controls.Add(new LiteralControl("</td>"));
                ph.DataBinding += new EventHandler(Item_DataBinding);
                break;
            case ListItemType.AlternatingItem:
                ph.Controls.Add(new LiteralControl("<td runat='server'>"));
                ph.Controls.Add(oCrit1);
                ph.Controls.Add(new LiteralControl("</td>"));
                ph.DataBinding += new EventHandler(Item_DataBinding);
                break;
            case ListItemType.Footer:
                ph.Controls.Add(new LiteralControl("</span></td>"));
                //ph.Controls.Add(new LiteralControl("</tr></tbody></table></td>"));
                break;
        }
        container.Controls.Add(ph);
    }

    static void Item_DataBinding(object sender, System.EventArgs e)
    {
        PlaceHolder ph = (PlaceHolder)sender;
        RepeaterItem ri = (RepeaterItem)ph.NamingContainer;
        //Int32 item1Value = (Int32)DataBinder.Eval(ri.DataItem, "CategoryID");
        //String item2Value = (String)DataBinder.Eval(ri.DataItem, "CategoryName");
        //((Label)ph.FindControl("item1")).Text = item1Value.ToString();
        //((Label)ph.FindControl("item2")).Text = item2Value;
    }
}