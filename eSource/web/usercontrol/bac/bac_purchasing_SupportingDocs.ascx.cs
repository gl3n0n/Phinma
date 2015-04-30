using System;
using System.Web;
using EBid.ConnectionString;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using EBid.lib;
using System.Data.SqlClient;

public partial class web_usercontrol_bac_bac_purchasing_SupportingDocs : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        count_attachements();
    }
    protected void count_attachements()
    {
        string sCommand;
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        SqlDataReader oReader;

        //SUPPORTING DOCUMENTS ATTACHED COUNTS
        //Approved_PR
        sCommand = "SELECT Count(*) as Count FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["UserId"] + " AND BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND DocuName='Approved_PR'";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            SDA_APR_count.Text = oReader["Count"].ToString();
        } oReader.Close();

        //Commercial_Evaluation
        sCommand = "SELECT Count(*) as Count FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["UserId"] + " AND BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND DocuName='Commercial_Evaluation'";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            SDA_CE_count.Text = oReader["Count"].ToString();
        } oReader.Close();

        //Approved_Business_Case
        sCommand = "SELECT Count(*) as Count FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["UserId"] + " AND BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND DocuName='Approved_Business_Case'";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            SDA_ABC_count.Text = oReader["Count"].ToString();
        } oReader.Close();

        //Board_Resolution
        sCommand = "SELECT Count(*) as Count FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["UserId"] + " AND BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND DocuName='Board_Resolution'";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            SDA_BR_count.Text = oReader["Count"].ToString();
        } oReader.Close();

        //Technical_Evaluation
        sCommand = "SELECT Count(*) as Count FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["UserId"] + " AND BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND DocuName='Technical_Evaluation'";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            SDA_TE_count.Text = oReader["Count"].ToString();
        } oReader.Close();

        //Negotiation_Results
        sCommand = "SELECT Count(*) as Count FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["UserId"] + " AND BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND DocuName='Negotiation_Results'";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            SDA_NR_count.Text = oReader["Count"].ToString();
        } oReader.Close();

        //Others
        sCommand = "SELECT Count(*) as Count FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["UserId"] + " AND BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND DocuName='Others'";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            SDA_Oth_count.Text = oReader["Count"].ToString();
        } oReader.Close();

        //BASIS FOR AWARDING
        //bfa_LowestPr
        sCommand = "SELECT Count(*) as Count FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["UserId"] + " AND BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND DocuName='bfa_LowestPr'";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            bfa_LowestPr_count.Text = oReader["Count"].ToString();
        } oReader.Close();
        //bfa_SoleLOA
        sCommand = "SELECT Count(*) as Count FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["UserId"] + " AND BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND DocuName='bfa_SoleLOA'";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            bfa_SoleLOA_count.Text = oReader["Count"].ToString();
        } oReader.Close();
        //bfa_SoleCSD
        sCommand = "SELECT Count(*) as Count FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["UserId"] + " AND BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND DocuName='bfa_SoleCSD'";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            bfa_SoleCSD_count.Text = oReader["Count"].ToString();
        } oReader.Close();
        //bfa_TechSpec
        sCommand = "SELECT Count(*) as Count FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["UserId"] + " AND BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND DocuName='bfa_TechSpec'";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            bfa_TechSpec_count.Text = oReader["Count"].ToString();
        } oReader.Close();
        //bfa_LeadTime
        sCommand = "SELECT Count(*) as Count FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["UserId"] + " AND BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND DocuName='bfa_LeadTime'";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            bfa_LeadTime_count.Text = oReader["Count"].ToString();
        } oReader.Close();
        //bfa_BFAOth
        sCommand = "SELECT Count(*) as Count FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["UserId"] + " AND BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND DocuName='bfa_BFAOth'";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            bfa_BFAOth_count.Text = oReader["Count"].ToString();
        } oReader.Close();


        //Label1.Text = Repeater_SDA_APR.Items.Count.ToString();
    }
}