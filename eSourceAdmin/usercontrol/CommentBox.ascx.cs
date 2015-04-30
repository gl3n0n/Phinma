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

public partial class web_usercontrol_CommentBox : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(Page.IsPostBack))
        {
            UserTransaction commentDetails = new UserTransaction();

            DataTable dtComments = null;
            DataView dvComments = null;

            if (Session[Constant.SESSION_USERID] != null)
            {
                ViewState[Constant.SESSION_USERID] = Session[Constant.SESSION_USERID].ToString();
                ViewState[Constant.SESSION_USERTYPE] = Session[Constant.SESSION_USERTYPE].ToString().Trim();
            }

            if (Session[Constant.SESSION_COMMENT_TYPE] != null)
            {
                ViewState[Constant.SESSION_COMMENT_TYPE] = Session[Constant.SESSION_COMMENT_TYPE].ToString().Trim();

                if (Int32.Parse(ViewState[Constant.SESSION_COMMENT_TYPE].ToString().Trim()) == 0)
                {
                    if (Session[Constant.SESSION_BIDREFNO] != null)
                    {
                        ViewState[Constant.SESSION_BIDREFNO] = Session[Constant.SESSION_BIDREFNO].ToString().Trim();
                        if (ViewState[Constant.SESSION_BIDREFNO] != null)
                        {
                            if (ViewState[Constant.SESSION_BIDREFNO].ToString().Trim() != "")
                            {
                                dtComments = commentDetails.QueryItemCommentsbyRefNo(ViewState[Constant.SESSION_BIDREFNO].ToString().Trim(), false);
                                dvComments = new DataView(dtComments);
                            }
                        }
                    }
                }
                else
                {
                    if (Session[Constant.SESSION_AUCTIONREFNO] != null)
                    {
                        ViewState[Constant.SESSION_AUCTIONREFNO] = Session[Constant.SESSION_AUCTIONREFNO].ToString().Trim();
                        if (ViewState[Constant.SESSION_AUCTIONREFNO] != null)
                        {
                            if (ViewState[Constant.SESSION_AUCTIONREFNO].ToString().Trim() != "")
                            {
                                dtComments = commentDetails.QueryItemCommentsbyRefNo(ViewState[Constant.SESSION_AUCTIONREFNO].ToString().Trim(), true);
                                dvComments = new DataView(dtComments);
                            }
                        }
                    }
                }
            }

            if (dvComments != null)
            {
                gvCommentArea.DataSource = dvComments;
                gvCommentArea.DataBind();
            }

            CommentBox.Attributes.Add("maxLength", "50");
            hdnIsPosted.Value = "0";
        }
    }

    protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataTable dtComments = null;
        DataView dvComments = null;

        UserTransaction commentDetails = new UserTransaction();

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

    protected void PostButton_Click(object sender, EventArgs e)
    {
        if (CommentBox.Text.Trim() != string.Empty)
        {
            UserTransaction commentDetails = new UserTransaction();

            DataTable dtComments = null;
            DataView dvComments = null;

            if (ViewState[Constant.SESSION_COMMENT_TYPE] != null)
            {
                if (Int32.Parse(ViewState[Constant.SESSION_COMMENT_TYPE].ToString().Trim()) == 0)
                {
                    if ((ViewState[Constant.SESSION_BIDREFNO] != null) && (ViewState[Constant.SESSION_USERID] != null) && (ViewState[Constant.SESSION_USERTYPE] != null)
                        && (ViewState[Constant.SESSION_USERID].ToString() != ""))
                    {
                        if (ViewState[Constant.SESSION_BIDREFNO].ToString().Trim() != "")
                        {
                            commentDetails.InsertItemComments(ViewState[Constant.SESSION_BIDREFNO].ToString().Trim(), CommentBox.Text,
                                                              ViewState[Constant.SESSION_USERID].ToString().Trim(), Int32.Parse(ViewState[Constant.SESSION_USERTYPE].ToString().Trim()), false);

                            hdnIsPosted.Value = "1";

                            dtComments = commentDetails.QueryItemCommentsbyRefNo(ViewState[Constant.SESSION_BIDREFNO].ToString().Trim(), false);
                            dvComments = new DataView(dtComments);
                        }
                    }
                }
                else
                {
                    if ((ViewState[Constant.SESSION_AUCTIONREFNO] != null) && (ViewState[Constant.SESSION_USERID] != null) && (ViewState[Constant.SESSION_USERTYPE] != null) &&
                        (ViewState[Constant.SESSION_USERID].ToString() != ""))
                    {
                        if (ViewState[Constant.SESSION_AUCTIONREFNO].ToString().Trim() != "")
                        {
                            commentDetails.InsertItemComments(ViewState[Constant.SESSION_AUCTIONREFNO].ToString().Trim(), CommentBox.Text,
                                                              ViewState[Constant.SESSION_USERID].ToString().Trim(), Int32.Parse(ViewState[Constant.SESSION_USERTYPE].ToString().Trim()), true);

                            hdnIsPosted.Value = "1";

                            dtComments = commentDetails.QueryItemCommentsbyRefNo(Session[Constant.SESSION_AUCTIONREFNO].ToString().Trim(), true);
                            dvComments = new DataView(dtComments);
                        }
                    }
                }
            }

            if (dvComments != null)
            {
                gvCommentArea.DataSource = dvComments;
                gvCommentArea.DataBind();

                CommentBox.Text = "";
            }
        }
    }
}
