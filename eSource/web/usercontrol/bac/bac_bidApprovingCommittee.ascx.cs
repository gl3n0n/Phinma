using System;
using System.Web;
using EBid.ConnectionString;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Configuration;
using EBid.lib;
using System.Data.SqlClient;

public partial class web_usercontrol_bac_bac_bidApprovingCommittee : System.Web.UI.UserControl
{
    int iApproverCount = 0;

    protected void Page_Load(object sender, System.EventArgs e)
    {
        iApproverCount = 0;
    }

    //protected void 
    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        string sCommand;
        Session["ClarifyDt"] = "";
        Session["ApprovedDt"] = "";
        SqlDataReader oReader;
        //get position of approving user
        //Session["approverPosition"] = "";
        //Session["approverNext"] = "";
        sCommand = "SELECT * FROM tblBacBidItems WHERE BacRefNo=" + Session["BuyerBacRefNo"];
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();

            //get position of clarifying user
            if (oReader["ClarifyDt_0"].ToString() != "") Session["ClarifyDt"] = "0";
            if (oReader["ClarifyDt_1"].ToString() != "") Session["ClarifyDt"] = "1";
            if (oReader["ClarifyDt_2"].ToString() != "") Session["ClarifyDt"] = "2";
            if (oReader["ClarifyDt_3"].ToString() != "") Session["ClarifyDt"] = "3";
            if (oReader["ClarifyDt_4"].ToString() != "") Session["ClarifyDt"] = "4";
            if (oReader["ClarifyDt_5"].ToString() != "") Session["ClarifyDt"] = "5";
            if (oReader["ClarifyDt_6"].ToString() != "") Session["ClarifyDt"] = "6";
            if (oReader["ClarifyDt_7"].ToString() != "") Session["ClarifyDt"] = "7";
            if (oReader["ClarifyDt_8"].ToString() != "") Session["ClarifyDt"] = "8";
            if (oReader["ClarifyDt_9"].ToString() != "") Session["ClarifyDt"] = "9";
            if (oReader["ClarifyDt_10"].ToString() != "") Session["ClarifyDt"] = "10";

            //get position of clarifying user
            if (oReader["ApprovedDt_0"].ToString() != "") Session["ApprovedDt"] = "0";
            if (oReader["ApprovedDt_1"].ToString() != "") Session["ApprovedDt"] = "1";
            if (oReader["ApprovedDt_2"].ToString() != "") Session["ApprovedDt"] = "2";
            if (oReader["ApprovedDt_3"].ToString() != "") Session["ApprovedDt"] = "3";
            if (oReader["ApprovedDt_4"].ToString() != "") Session["ApprovedDt"] = "4";
            if (oReader["ApprovedDt_5"].ToString() != "") Session["ApprovedDt"] = "5";
            if (oReader["ApprovedDt_6"].ToString() != "") Session["ApprovedDt"] = "6";
            if (oReader["ApprovedDt_7"].ToString() != "") Session["ApprovedDt"] = "7";
            if (oReader["ApprovedDt_8"].ToString() != "") Session["ApprovedDt"] = "8";
            if (oReader["ApprovedDt_9"].ToString() != "") Session["ApprovedDt"] = "9";
            if (oReader["ApprovedDt_10"].ToString() != "") Session["ApprovedDt"] = "10";
        }

        oReader.Close();
    }

    protected void RepeaterApprover1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            iApproverCount++;
            int iApproverTotal = 0;
            string sCommand;
            SqlDataReader oReader;
            string connstring = HttpContext.Current.Session["ConnectionString"].ToString();

            // check number of approvers
            sCommand = "IF EXISTS (SELECT 1 FROM tblBACSupportingDocuments WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND DocuName='bfa_LowestPr') BEGIN SELECT COUNT(t1.BACId) count1 FROM tblBidAwardingCommittee t1  WHERE t1.ApprovingLimitOnLowestPrice <= (SELECT EstItemValue FROM tblBidItems WHERE BidRefNo=" + Session["BuyerBidForBac"] + ") END ELSE BEGIN SELECT COUNT(t1.BACId) count1 FROM tblBidAwardingCommittee t1 WHERE t1.ApprovingLimitOnNonLowestPrice <= (SELECT EstItemValue FROM tblBidItems WHERE BidRefNo=" + Session["BuyerBidForBac"] + ") END";
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                oReader.Read();
                iApproverTotal = int.Parse(oReader["count1"].ToString());
            }
            oReader.Close();
            if (iApproverCount > iApproverTotal) iApproverCount = 1;

            // update checkboxes
            //((Literal)e.Item.FindControl("lblApprover")).Text = "Approver " + (iApproverCount-1).ToString();

            sCommand = "SELECT Approver_" + (iApproverCount).ToString() + " ApproverID FROM tblBACBidItems WHERE BacRefNo=" + Session["BuyerBacRefNo"];
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);

            ((HtmlInputCheckBox)e.Item.FindControl("chkApproverLevel")).Checked = false;

            if (oReader.HasRows)
            {
                oReader.Read();
                string sApproverID = oReader["ApproverID"].ToString();
                if (sApproverID.Trim() != "" && sApproverID.Trim() != "0")
                {
                    //((HtmlInputCheckBox)e.Item.FindControl("chkApproverLevel")).Checked = true;
                }
            }
            oReader.Close();
        }
    }

    protected void RepeaterApprover1_Load(object sender, EventArgs e)
    {
        iApproverCount = 0;
    }
}