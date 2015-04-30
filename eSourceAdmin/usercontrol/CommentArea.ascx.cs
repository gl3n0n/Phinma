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
using EBid.lib.user.trans;
using EBid.lib.constant;

public partial class web_usercontrol_CommentArea : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserTransaction commentDetails = new UserTransaction();

        DataTable dtComments = new DataTable();
        DataView dvComments = new DataView();

        if(Session[Constant.SESSION_COMMENT_TYPE] != null)
        {
            ViewState[Constant.SESSION_COMMENT_TYPE] = Session[Constant.SESSION_COMMENT_TYPE].ToString().Trim();

            if (Int32.Parse(Session[Constant.SESSION_COMMENT_TYPE].ToString().Trim()) == 0)
            {
                if (Session[Constant.SESSION_BIDREFNO] != null)
                {
                    ViewState[Constant.SESSION_BIDREFNO] = Session[Constant.SESSION_BIDREFNO].ToString().Trim();
                    dtComments = commentDetails.QueryItemCommentsbyRefNo(ViewState[Constant.SESSION_BIDREFNO].ToString().Trim(), false);
                    if (dtComments.Rows.Count == 0)
                        dtComments = CreateNewCommentTable();
                    dvComments = new DataView(dtComments);
                }
            }
            else
            {
                if (Session[Constant.SESSION_AUCTIONREFNO] != null)
                {
                    ViewState[Constant.SESSION_AUCTIONREFNO] = Session[Constant.SESSION_AUCTIONREFNO].ToString().Trim();
                    dtComments = commentDetails.QueryItemCommentsbyRefNo(ViewState[Constant.SESSION_AUCTIONREFNO].ToString().Trim(), true);
                    if (dtComments.Rows.Count == 0)
                        dtComments = CreateNewCommentTable();
                    dvComments = new DataView(dtComments);
                }
            }
        }

        if (dvComments != null)
        {

            gvCommentArea.DataSource = dvComments;
            gvCommentArea.DataBind();
        }
    }

    private DataTable CreateNewCommentTable()
    {
        DataTable dt = new DataTable();
        DataColumn dc = new DataColumn("Comment", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("DatePosted1", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("Name", typeof(System.String));
        dt.Columns.Add(dc);
        DataRow dr = dt.NewRow();
        dr["Comment"] = "";
        dr["DatePosted1"] = "";
        dr["Name"] = "";
        dt.Rows.Add(dr);
        return dt;
    }

    protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        UserTransaction commentDetails = new UserTransaction();
        DataTable dtComments = null;
        DataView dvComments = null;

        if (ViewState[Constant.SESSION_COMMENT_TYPE] != null)
        {
            if (Int32.Parse(ViewState[Constant.SESSION_COMMENT_TYPE].ToString().Trim()) == 0)
            {
                if (ViewState[Constant.SESSION_BIDREFNO] != null)
                {
                    dtComments = commentDetails.QueryItemCommentsbyRefNo(ViewState[Constant.SESSION_BIDREFNO].ToString().Trim(), false);
                    dvComments = new DataView(dtComments);
                }
            }
            else
            {
                if (ViewState[Constant.SESSION_AUCTIONREFNO] != null)
                {
                    dtComments = commentDetails.QueryItemCommentsbyRefNo(ViewState[Constant.SESSION_AUCTIONREFNO].ToString().Trim(), true);
                    dvComments = new DataView(dtComments);
                }
            }
        }

        if (dvComments != null)
        {
            gvCommentArea.DataSource = dvComments;
            gvCommentArea.PageIndex = e.NewPageIndex;
            gvCommentArea.DataBind();
        }
    }
}
