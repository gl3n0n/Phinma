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
using EBid.lib.constant;
using EBid.lib.bid.trans;
using EBid.lib.user.trans;
using EBid.lib;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public partial class web_bac_bacapprovedpurchasing : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        string sCommand;
        SqlDataReader oReader;
        sCommand = "SELECT t1.BacRefNo, t1.BidRefNo, t1.ItemDesc, t1.ApprovedDt_0, t1.ApprovedDt_1, t1.ApprovedDt_2, t1.ApprovedDt_3, t1.ApprovedDt_4, t1.ApprovedDt_5, t1.ApprovedDt_6, t1.ApprovedDt_7, t1.ApprovedDt_8, t1.ApprovedDt_9, t1.Approver_1, t1.Approver_2, t1.Approver_3, t1.Approver_4, t1.Approver_5, t1.Approver_6, t1.Approver_7, t1.Approver_8, t1.Approver_9, t1.Approver_10 from tblBacBidItems t1 where (t1.Approver_1=" + Session["UserId"] + " OR t1.Approver_2=" + Session["UserId"] + " OR t1.Approver_3=" + Session["UserId"] + " OR t1.Approver_4=" + Session["UserId"] + " OR t1.Approver_5=" + Session["UserId"] + " OR t1.Approver_6=" + Session["UserId"] + " OR t1.Approver_7=" + Session["UserId"] + " OR t1.Approver_8=" + Session["UserId"] + " OR t1.Approver_9=" + Session["UserId"] + " OR t1.Approver_10=" + Session["UserId"] + ") ORDER BY t1.BacRefNo DESC";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        Hashtable ApproverList = new Hashtable();
        while (oReader.Read())
        {
            //string i = oReader["ItemDesc"].ToString();
            if (oReader["ApprovedDt_0"].ToString() != "") ApproverList.Add("1_" + oReader["BidRefNo"].ToString(), oReader["Approver_1"].ToString());
            if (oReader["ApprovedDt_1"].ToString() != "") ApproverList.Add("2_" + oReader["BidRefNo"].ToString(), oReader["Approver_2"].ToString());
            if (oReader["ApprovedDt_2"].ToString() != "") ApproverList.Add("3_" + oReader["BidRefNo"].ToString(), oReader["Approver_3"].ToString());
            if (oReader["ApprovedDt_3"].ToString() != "") ApproverList.Add("4_" + oReader["BidRefNo"].ToString(), oReader["Approver_4"].ToString());
            if (oReader["ApprovedDt_4"].ToString() != "") ApproverList.Add("5_" + oReader["BidRefNo"].ToString(), oReader["Approver_5"].ToString());
            if (oReader["ApprovedDt_5"].ToString() != "") ApproverList.Add("6_" + oReader["BidRefNo"].ToString(), oReader["Approver_6"].ToString());
            if (oReader["ApprovedDt_6"].ToString() != "") ApproverList.Add("7_" + oReader["BidRefNo"].ToString(), oReader["Approver_7"].ToString());
            if (oReader["ApprovedDt_7"].ToString() != "") ApproverList.Add("8_" + oReader["BidRefNo"].ToString(), oReader["Approver_8"].ToString());
            if (oReader["ApprovedDt_8"].ToString() != "") ApproverList.Add("9_" + oReader["BidRefNo"].ToString(), oReader["Approver_9"].ToString());
            if (oReader["ApprovedDt_9"].ToString() != "") ApproverList.Add("10_" + oReader["BidRefNo"].ToString(), oReader["Approver_10"].ToString());
        }
        foreach (DictionaryEntry Item in ApproverList)
        {
            //Response.Write(Item.Key.ToString() + ": " + Item.Value.ToString() + "<br/>");

        }


        //string str = null;
        //foreach (string key in HttpContext.Current.Session.Keys)
        //{
        //    str += string.Format("{0}: {1}<br />", key, HttpContext.Current.Session[key].ToString());
        //}
        //Response.Write(str);
        //Response.Write(Session["UserId"]);
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BIDAWARDINGCOMMITTEE)
            Response.Redirect("../unauthorizedaccess.aspx");

        Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;

        if (!(Page.IsPostBack))
        {
        }
        if (Session[Constant.SESSION_USERID] == null)
        {
            Session.Abandon();
            Session.Clear();
            FormsAuthentication.SignOut();

            string returnUrl = string.Empty;
            if (Request.RawUrl.Trim() != "")
                returnUrl = "?ReturnUrl=" + Request.RawUrl.Trim().Replace("~/", "");
            Response.Redirect(FormsAuthentication.LoginUrl + returnUrl);
        }


        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "BAC Approved by Purchasing");

    }


    protected void gvBids_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Details"))
        {
            Session["ViewOption"] = "AsCommittee";
            string sArg = e.CommandArgument.ToString().Trim();
            char[] mySeparator = new char[] { ';' };
            string[] Arr = sArg.Split(mySeparator);
            Session["BuyerBidForBac"] = "";
            Session["BuyerBidForBac"] = Arr[0].ToString();
            Session["BuyerBacRefNo"] = Arr[1].ToString();
            Session["BuyerBuyerId"] = Arr[2].ToString();
            Session["pageDetails"] = "bacapprovedbypurchasing.aspx";

            Server.Transfer("bacapprovedbypurchasing.aspx");
        }

    }
}