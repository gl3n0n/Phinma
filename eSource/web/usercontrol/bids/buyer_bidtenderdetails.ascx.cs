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
using EBid.lib.bid.trans;
using EBid.lib.bid.data;
using EBid.lib.constant;
using EBid.lib.user.trans;
using EBid.lib.auction.data;
using EBid.lib;
using EBid.ConnectionString;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;

public partial class web_usercontrol_Buyer_buyer_bidtenderdetails : System.Web.UI.UserControl
{
    private string connstring = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["clientid"] == null) Response.Redirect("../unauthorizedaccess.aspx");
        connstring = HttpContext.Current.Session["ConnectionString"].ToString();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string sCommand;

        Label lblMessage = (Label)dvBidTender.FindControl("lblMessage1");
        DataList dlStatusComments1 = (DataList)dvBidTender.FindControl("dlStatusComments");

        if (Session["Usertype"].ToString() == "2")
        {
            DropDownList ddlStatus = (DropDownList)dvBidTender.FindControl("ddlStatus1");
            TextBox txtComment1 = (TextBox)dvBidTender.FindControl("txtComment");
            if ((ddlStatus.SelectedValue != "" && ddlStatus.SelectedValue != "Select status") && txtComment1.Text!="")
            {
                sCommand = "UPDATE tblBidTenders Set AwardedStatus = '" + ddlStatus.SelectedValue.Trim() + "' WHERE BidTenderNo=" + Session["BidTenderNo"].ToString();
                SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);


                sCommand = @"INSERT INTO tblBidTenderComments (BidTenderNo, UserId, Comment, CommentType) VALUES(" + Session["BidTenderNo"].ToString() + ", " + Session["UserId"].ToString() + ", 'Status: " + ddlStatus.SelectedValue + "<br>" + txtComment1.Text.Trim().Replace("'", "''") + "', 'ST')";
                SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

                lblMessage.Text = "Status successfully updated.<br>";
                lblMessage.ForeColor = Color.Blue;
                lblMessage.Font.Bold = true;
                txtComment1.Text = "";
                dlStatusComments1.DataBind();
            }
            else
            {
                lblMessage.Text = "Select a status and comment is required.<br>";
                lblMessage.ForeColor = Color.Red;
                lblMessage.Font.Bold = true;
            }
        }
        else
        {
            TextBox txtComment1 = (TextBox)dvBidTender.FindControl("txtComment");
            if (txtComment1.Text != "")
            {
                sCommand = @"INSERT INTO tblBidTenderComments (BidTenderNo, UserId, Comment, CommentType) VALUES(" + Session["BidTenderNo"].ToString() + ", " + Session["UserId"].ToString() + ", '" + txtComment1.Text.Trim().Replace("'", "''") + "', 'ST')";
                SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

                lblMessage.Text = "Comment successfully posted.<br>";
                lblMessage.ForeColor = Color.Blue;
                lblMessage.Font.Bold = true;
                txtComment1.Text = "";
                dlStatusComments1.DataBind();
            }
            else
            {
                lblMessage.Text = "Comment is required.<br>";
                lblMessage.ForeColor = Color.Red;
                lblMessage.Font.Bold = true;
            }
        }

    }

    protected void dvBidTender_DataBound(object sender, EventArgs e)
    {
        DropDownList ddlStatus = (DropDownList)dvBidTender.FindControl("ddlStatus1");
        Label lblStatus = (Label)dvBidTender.FindControl("lblStatus1");
        Button lblButton = (Button)dvBidTender.FindControl("Button1");
        if (Session["Usertype"].ToString() != "2")
        {
            ddlStatus.Visible = false;
            lblStatus.Visible = true;
            lblButton.Text = "Comment";
        }
        else
        {
            //ddlStatus.Visible = true;
            //lblStatus.Visible = false;
        }

    }
}
