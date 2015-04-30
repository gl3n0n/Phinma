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

public partial class web_usercontrol_bids_biddetails_attachments : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void gvFileAttachments_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Download":
                {
                    if (Session["ViewOption"] != null)
                    {
                        if (Session["ViewOption"] == "AsBuyer")
                        {
                            string[] args = e.CommandArgument.ToString().Split(new char[] { '|' });
                            string path = Constant.FILEATTACHMENTSFOLDERDIR;
                            FileHelper.DownloadFile(this.Page, path, args[0], args[1]);
                        }
                        else if (Session["ViewOption"] == "AsVendor")
                        {
                            string[] args = e.CommandArgument.ToString().Split(new char[] { '|' });
                            string path = Constant.FILEATTACHMENTSFOLDERDIR + Session["VendorId"].ToString() + "\\" + Session[Constant.SESSION_BIDREFNO.ToString()].ToString() + "\\";
                            FileHelper.DownloadFile(this.Page, path, args[0], args[1]);
                        }
                    }
                    else
                    {
						string[] args = e.CommandArgument.ToString().Split(new char[] { '|' });
						string path = Constant.FILEATTACHMENTSFOLDERDIR;
                        FileHelper.DownloadFile(this.Page, path, args[0], args[1]);
                    }               
                } break;
        }
    }
}
