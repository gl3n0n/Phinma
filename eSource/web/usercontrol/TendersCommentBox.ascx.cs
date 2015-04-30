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

public partial class web_usercontrol_TendersCommentBox : System.Web.UI.UserControl
{
    private string connstring = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (!(Page.IsPostBack))
        {
            CommentBox.Attributes.Add("maxLength","250");
            
            DataTable dtComments = null;
            DataView dvComments = null;

            int userType = Int32.Parse(hdnUserType.Value.ToString().Trim()),
                bidRefno = Int32.Parse(hdnBidRefNo.Value.ToString().Trim()),
                tenderStat = Int32.Parse(hdnTenderStat.Value.Trim());

            dtComments = UserTransaction.QueryTenderCommentsbyRefNoAndStatus(connstring, userType, bidRefno, tenderStat);
            dvComments = new DataView(dtComments);
                    
            gvCommentArea.DataSource = dvComments;
            gvCommentArea.DataBind();
            
            hdnIsPosted.Value = "0";
        }
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

    protected void PostButton_Click(object sender, EventArgs e)
    {
        if (CommentBox.Text.Trim() != string.Empty)
        {
            DataTable dtComments = null;
            DataView dvComments = null;

            int userType = Int32.Parse(hdnUserType.Value.ToString().Trim()),
                bidRefno = Int32.Parse(hdnBidRefNo.Value.ToString().Trim()),
                tenderStat = Int32.Parse(hdnTenderStat.Value.Trim()),
                userID = Int32.Parse(hdnUserID.Value.Trim());

            if (CommentBox.Text.Trim().Length > 0)
            {
                UserTransaction.InsertTenderComments(connstring, userType, userID, bidRefno, tenderStat, CommentBox.Text.Trim());

                hdnIsPosted.Value = "1";

                dtComments = UserTransaction.QueryTenderCommentsbyRefNoAndStatus(connstring, userType, bidRefno, tenderStat);
                dvComments = new DataView(dtComments);


                gvCommentArea.DataSource = dvComments;
                gvCommentArea.DataBind();

                CommentBox.Text = "";
            }
        }
    }
}
