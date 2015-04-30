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
using EBid.lib;
using EBid.lib.constant;

public partial class web_usercontrol_bids_biddetails_suppliers_boc : System.Web.UI.UserControl
{
	public int prev_vendor_id = 0;
	
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Write("RequestFORM:" + (Request.Form["__EVENTTARGET"])); 
	if ((Request.Form["__EVENTTARGET"] != "Biddetails_suppliers1$gvInvitedSuppliers") && (IsPostBack))
	{
	     if (Request.Form["__EVENTTARGET"] != "") { 
	     gvInvitedSuppliers.DataBind(); }
	}
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
			
			if (gvInvitedSuppliers.DataKeys[e.Row.RowIndex].Values[2].ToString() == "")
				e.Row.Cells[3].Text = "&nbsp;&nbsp;No Attachment";
			
			if (Int32.Parse(prev_vendor_id.ToString()) == Int32.Parse(gvInvitedSuppliers.DataKeys[e.Row.RowIndex].Values[0].ToString()))
			{
				
				e.Row.Cells[0].Text = "&nbsp;";
				e.Row.Cells[1].Text = "&nbsp;";
				e.Row.Cells[2].Text = "&nbsp;";
			}	
			
			prev_vendor_id = Int32.Parse(gvInvitedSuppliers.DataKeys[e.Row.RowIndex].Values[0].ToString());
			
			//e.Row.Cells[0].Text = gvInvitedSuppliers.DataKeys[e.Row.RowIndex].Values[0].ToString();	
			//e.Row.Cells[1].Text=Convert.ToString(gvInvitedSuppliers.DataKeys[e.Row.RowIndex].Values[0]);
		}	
	}
	
	protected void gvFileAttachments_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Download":
                { 
					string[] args = e.CommandArgument.ToString().Split(new char[] { '|' });
					string path = Constant.FILEATTACHMENTSFOLDERDIR;
					string[] folder = args[0].Split(new char[] { '_' });
					path = path + folder[1].ToString() + '\\' + folder[2].ToString() + '\\';
					FileHelper.DownloadFile(this.Page, path, args[0], args[1]);
                } break;
        }
    }
}
