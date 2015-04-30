using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using EBid.lib;
using EBid.ConnectionString;
using EBid.lib.constant;
using System.Web.SessionState;

public partial class web_buyer_screens_ApprovedBidItems : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;
        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Approved Bid Events");
    }

    protected void gvBids_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Details"))
        {
            Session["ViewOption"] = "AsBuyer";
            string sArg = e.CommandArgument.ToString().Trim();
            char[] mySeparator = new char[] { ';' };
            string[] Arr = sArg.Split(mySeparator);
            Session["BuyerBidForBac"] = "";
            Session["BuyerBidForBac"] = Arr[0].ToString();
            Session["BuyerBacRefNo"] = Arr[1].ToString();
            Session["pageDetails"] = "bidawardingchecklistdraft.aspx";

            string sCommand;
            string connstring = Session["ConnectionString"].ToString();

            sCommand = "DELETE FROM tblBACEvaluationDetails_tmp WHERE BidRefNo=" + Session["BuyerBidForBac"];
            //LogHelper.TextLogHelper.Log(Server.MapPath("ebid.log"), sCommand + "\\n");
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

            sCommand = "INSERT INTO tblBACEvaluationDetails_tmp (BidRefNo, BidDetailNo, BidTenderNo, SKU, ItemName, Chkd, Qty, UnitCost, TotalCost, ";
            sCommand = sCommand + "Ranking, FromDB, UnitMeasure, VendorName, Currenzy) ";
            sCommand = sCommand + "SELECT BidRefNo, BidDetailNo, BidTenderNo, SKU, ItemName, Chkd, Qty, UnitCost, TotalCost, ";
            sCommand = sCommand + "Ranking, FromDB, UnitMeasure, VendorName, Currenzy ";
            sCommand = sCommand + "FROM tblBACEvaluationDetails WHERE BidRefNo=" + Session["BuyerBidForBac"];
            //LogHelper.TextLogHelper.Log(Server.MapPath("ebid.log"), sCommand + "\\n");
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

            //Server.Transfer("bidawardingchecklist.aspx");
            Server.Transfer("bidawardingchecklistdraft.aspx");
            //System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('" + Session["Session_BidRefNoAsBac"] + "')</SCRIPT>");
            //Server.Transfer("bidawardingchecklist.aspx", true);
        }
    }

    protected void chkShowEventsBeforeDeadline_CheckedChanged(object sender, EventArgs e)
    {
        if (chkShowEventsBeforeDeadline.Checked)
        {
            dsAproved.FilterExpression = "IsPastDeadline = 'NO'";
        }
        else
        {
            dsAproved.FilterExpression = "IsPastDeadline IN ('YES','NO')";
        }
        dsAproved.DataBind();
    }
}
