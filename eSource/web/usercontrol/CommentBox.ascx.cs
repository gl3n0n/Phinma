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

public partial class web_usercontrol_CommentBox : System.Web.UI.UserControl
{
    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        web_usercontrol_commentlist commentslist_bid = (web_usercontrol_commentlist)FindControl("Commentlist_bid");
        web_usercontrol_commentlist_auction commentslist_auction = (web_usercontrol_commentlist_auction)FindControl("Commentlist_auction1");

        if (Session[Constant.SESSION_BIDREFNO] != null)
        {
            commentslist_bid.Visible = true;
            commentslist_auction.Visible = false;
        }

        if (Session[Constant.SESSION_AUCTIONREFNO] != null)
        {
            commentslist_bid.Visible = false;
            commentslist_auction.Visible = true;
        }

        if (!(Page.IsPostBack))
        {
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
                                dtComments = UserTransaction.QueryItemCommentsbyRefNo(connstring, ViewState[Constant.SESSION_BIDREFNO].ToString().Trim(), false);
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
                                dtComments = UserTransaction.QueryItemCommentsbyRefNo(connstring, ViewState[Constant.SESSION_AUCTIONREFNO].ToString().Trim(), true);
                                dvComments = new DataView(dtComments);
                            }
                        }
                    }
                }
            }

           
            if (Session[Constant.SESSION_USERTYPE].Equals("1"))
            {
                chkAlowVendorView.Visible = true;
            }
            else
            {
                chkAlowVendorView.Visible = false;
            }

            CommentBox.Attributes.Add("maxLength", "50");
            hdnIsPosted.Value = "0";
        }
    }

    protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataTable dtComments = null;
        DataView dvComments = null;

        if (ViewState[Constant.SESSION_COMMENT_TYPE] != null)
        {
            if (Int32.Parse(ViewState[Constant.SESSION_COMMENT_TYPE].ToString().Trim()) == 0)
            {
                if (ViewState[Constant.SESSION_BIDREFNO] != null)
                {
                    dtComments = UserTransaction.QueryItemCommentsbyRefNo(connstring, ViewState[Constant.SESSION_BIDREFNO].ToString().Trim(), false);
                    dvComments = new DataView(dtComments);
                }
            }
            else
            {
                if (ViewState[Constant.SESSION_AUCTIONREFNO] != null)
                {
                    dtComments = UserTransaction.QueryItemCommentsbyRefNo(connstring, ViewState[Constant.SESSION_AUCTIONREFNO].ToString().Trim(), true);
                    dvComments = new DataView(dtComments);
                }
            }
        }

    }

    protected void PostButton_Click(object sender, EventArgs e)
    {
        if (CommentBox.Text.Trim() != string.Empty)
        {
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
                            UserTransaction.InsertItemComments(connstring, ViewState[Constant.SESSION_BIDREFNO].ToString().Trim(), CommentBox.Text, ViewState[Constant.SESSION_USERID].ToString().Trim(), Int32.Parse(ViewState[Constant.SESSION_USERTYPE].ToString().Trim()), false, chkAlowVendorView.Checked);

                            hdnIsPosted.Value = "1";

                            dtComments = UserTransaction.QueryItemCommentsbyRefNo(connstring, ViewState[Constant.SESSION_BIDREFNO].ToString().Trim(), false);
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
                            UserTransaction.InsertItemComments(connstring, ViewState[Constant.SESSION_AUCTIONREFNO].ToString().Trim(), CommentBox.Text, ViewState[Constant.SESSION_USERID].ToString().Trim(), Int32.Parse(ViewState[Constant.SESSION_USERTYPE].ToString().Trim()), true, chkAlowVendorView.Checked);

                            hdnIsPosted.Value = "1";

                            dtComments = UserTransaction.QueryItemCommentsbyRefNo(connstring, Session[Constant.SESSION_AUCTIONREFNO].ToString().Trim(), true);
                            dvComments = new DataView(dtComments);
                        }
                    }
                }
            }

            if (dvComments != null)
            {
                CommentBox.Text = "";
                Response.Redirect(Request.AppRelativeCurrentExecutionFilePath);
            }
        }
    }
}
