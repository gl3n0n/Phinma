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
using EBid.lib.user.trans;
using EBid.lib.constant;
using EBid.ConnectionString;

public partial class web_usercontrol_TendersCommentArea : System.Web.UI.UserControl
{
    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        DataTable dtComments = null;
        DataView dvComments = null;

        int userType = Int32.Parse(hdnUserType.Value.ToString().Trim()),
            bidRefno = Int32.Parse(hdnBidRefNo.Value.ToString().Trim()),
            tenderStat = Int32.Parse(hdnTenderStat.Value.Trim());

        dtComments = UserTransaction.QueryTenderCommentsbyRefNoAndStatus(connstring, userType, bidRefno, tenderStat);
        dvComments = new DataView(dtComments);

        gvCommentArea.DataSource = dvComments;
        gvCommentArea.DataBind();
    }

    protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataTable dtComments = null;
        DataView dvComments = null;

        int userType = Int32.Parse(hdnUserType.Value.ToString().Trim()),
            bidRefno = Int32.Parse(hdnBidRefNo.Value.ToString().Trim()),
            tenderStat = Int32.Parse(hdnTenderStat.Value.Trim());

        dtComments = UserTransaction.QueryTenderCommentsbyRefNoAndStatus(connstring, userType, bidRefno, tenderStat);
        dvComments = new DataView(dtComments);

        gvCommentArea.DataSource = dvComments;
        gvCommentArea.PageIndex = e.NewPageIndex;
        gvCommentArea.DataBind();
    }
}
