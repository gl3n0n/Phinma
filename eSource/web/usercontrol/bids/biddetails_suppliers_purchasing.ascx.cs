using System;
using System.Web;
using EBid.ConnectionString;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class web_usercontrol_bids_biddetails_suppliers_purchasing : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
	
	public void gvInvitedSuppliers_RowDataBound(object sender, GridViewRowEventArgs e)
	{
	    if (e.Row.RowType == DataControlRowType.DataRow)
		{	
			//e.Row.Cells[2].Text =  
			int status = Int32.Parse(gvInvitedSuppliers.DataKeys[e.Row.RowIndex].Values[1].ToString());
			if (status > 0)
			{
				e.Row.Cells[2].Text = "with bid tenders";
			}
			else if (status == 0)
			{
				e.Row.Cells[2].Text = "no remarks";
			}
			//e.Row.Cells[1].Text=Convert.ToString(gvInvitedSuppliers.DataKeys[e.Row.RowIndex].Values[0]);
		}
	}
}
