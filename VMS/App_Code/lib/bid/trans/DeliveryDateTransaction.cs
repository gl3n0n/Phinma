using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;

/// <summary>
/// Summary description for DeliveryDateTransaction
/// </summary>
/// 

namespace Ava.lib.bid.trans
{
    public class DeliveryDateTransaction
    {
        public DeliveryDateTransaction()
        {
            //
            // TODO: Add constructor logic here
            //
        }

    public ListItemCollection GetMonth() 
    {
        ListItemCollection lst = new ListItemCollection();
        ListItem lstitem = new ListItem();
        lstitem.Value = "";
        lstitem.Text = "";
        lst.Add(lstitem);
        ListItem lstitem1 = new ListItem();
        lstitem1.Value = "1";
        lstitem1.Text = "Jan";
        lst.Add(lstitem1);
        ListItem lstitem2 = new ListItem();
        lstitem2.Value = "2";
        lstitem2.Text = "Feb";
        lst.Add(lstitem2);
        ListItem lstitem3 = new ListItem();
        lstitem3.Value = "3";
        lstitem3.Text = "Mar";
        lst.Add(lstitem3);
        ListItem lstitem4 = new ListItem();
        lstitem4.Value = "4";
        lstitem4.Text = "Apr";
        lst.Add(lstitem4);
        ListItem lstitem5 = new ListItem();
        lstitem5.Value = "5";
        lstitem5.Text = "May";
        lst.Add(lstitem5);
        ListItem lstitem6 = new ListItem();
        lstitem6.Value = "6";
        lstitem6.Text = "Jun";
        lst.Add(lstitem6);
        ListItem lstitem7 = new ListItem();
        lstitem7.Value = "7";
        lstitem7.Text = "Jul";
        lst.Add(lstitem7);
        ListItem lstitem8 = new ListItem();
        lstitem8.Value = "8";
        lstitem8.Text = "Aug";
        lst.Add(lstitem8);
        ListItem lstitem9 = new ListItem();
        lstitem9.Value = "9";
        lstitem9.Text = "Sep";
        lst.Add(lstitem9);
        ListItem lstitem10 = new ListItem();
        lstitem10.Value = "10";
        lstitem10.Text = "Oct";
        lst.Add(lstitem10);
        ListItem lstitem11 = new ListItem();
        lstitem11.Value = "11";
        lstitem11.Text = "Nov";
        lst.Add(lstitem11);
        ListItem lstitem12 = new ListItem();
        lstitem12.Value = "12";
        lstitem12.Text = "Dec";
        lst.Add(lstitem12);

        return lst;
    }
        
    }
}