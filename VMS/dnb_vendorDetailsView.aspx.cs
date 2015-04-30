using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Ava.lib;
using Ava.lib.constant;
using System.Text;
using System.Text.RegularExpressions;

public partial class dnb_vendorDetailsView : System.Web.UI.Page
{
    SqlDataReader oReader;
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
    string query;
    SqlCommand cmd;
    SqlConnection conn;
    //int numRowsTbl;

    protected void TestShowAllSessions()
    {  //test show all session
        string str = null;
        foreach (string key in HttpContext.Current.Session.Keys)
        { str += string.Format("<b>{0}</b>: {1};  ", key, HttpContext.Current.Session[key].ToString()); }
        Response.Write("<span style='font-size:12px'>" + str + "</span>");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //TestShowAllSessions();
        if (Session["UserId"] == null) Response.Redirect("login.aspx");
        if (Session["SESSION_USERTYPE"].ToString() != "12") Response.Redirect("login.aspx");
        if (IsPostBack)
        {
            SaveToDB();
        }
        if (Request.QueryString["VendorId"] != null)
        {
            Session["VendorId"] = Request.QueryString["VendorId"].ToString();
        }
        PopulateFields();
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        
    }


    void PopulateFields()
    {
        query = "SELECT * FROM tblVendor WHERE VendorId = @VendorId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        //if (oReader["Status"].ToString() == "2") { Response.Redirect("dnb_vendorDetailsView.aspx"); }
                        AuthenticationTicketLbl.Text = oReader["AuthenticationTicket"].ToString();
                        CompanyNameLbl.Text = oReader["CompanyName"].ToString() != "" ? "<a href='vendor_Home.aspx?VendorId=" + Session["VendorId"] + "' target='_blank'>" + oReader["CompanyName"].ToString() + "</a>" : "n/a";
                        VendorGroupCode_Lbl.Text = oReader["AccGroup"].ToString() != "" ? oReader["AccGroup"].ToString() : "n/a";


                        if (oReader["TradeNonTrade"].ToString() == "H")
                        {
                            TradeNonTrade_Lbl.Text ="Non-Trade";
                        }
                        else if (oReader["TradeNonTrade"].ToString() == "I")
                        {
                            TradeNonTrade_Lbl.Text = "Trade";
                        }
                        else
                        {
                            TradeNonTrade_Lbl.Text = "n/a";
                        }

                    }
                }
            }
        }

        query = "SELECT * FROM tblDnbReport WHERE VendorId = @VendorId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        dnbDuns.Text = oReader["dnbDuns"].ToString();
                        dnbFinCapScore.Text = oReader["dnbFinCapScore"].ToString();
                        dnbFinCapScore_Remarks.Text = oReader["dnbFinCapScore_Remarks"].ToString().Replace("\n","<br>");
                        dnbLegalConfScore.Text = oReader["dnbLegalConfScore"].ToString();
                        dnbLegalConfScore_Remarks.Text = oReader["dnbLegalConfScore_Remarks"].ToString() != "" ? oReader["dnbLegalConfScore_Remarks"].ToString().Replace("\n", "<br>") : "no remarks";
                        dnbTechCompScore.Text = oReader["dnbTechCompScore"].ToString();
                        dnbTechCompScore_Remarks.Text = oReader["dnbTechCompScore_Remarks"].ToString().Replace("\n", "<br>");
                        dnbCurrentRevenue.Text = oReader["dnbCurrentRevenue"].ToString();
                        dnbSupplierInfoReport.Value = oReader["dnbSupplierInfoReport"].ToString();
                        dnbSupplierInfoReportLbl.Text = oReader["dnbSupplierInfoReport"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["dnbSupplierInfoReport"].ToString() + "' target='_blank'>" + oReader["dnbSupplierInfoReport"].ToString() + "</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> No attach file";
                        dnbOtherDocuments.Value = oReader["dnbOtherDocuments"].ToString();
                        dnbOtherDocumentsLbl.Text = oReader["dnbOtherDocuments"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["dnbOtherDocuments"].ToString() + "' target='_blank'>" + oReader["dnbOtherDocuments"].ToString() + "</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> No attach file";

                        dnbScore.Text = oReader["dnbScore"].ToString() != "" ? oReader["dnbScore"].ToString() : "0";

                        MaxExposureLimit.Text = oReader["dnbMaxExposureLimit"].ToString() != "" ? oReader["dnbMaxExposureLimit"].ToString() : "0";
                    }
                }
            }
        }


        query = "SELECT * FROM tblDnbLegalReport WHERE VendorId = @VendorId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        //.Value = oReader[""].ToString();
                        //TypeOfCase.Text = oReader["TypeOfCase"].ToString();
                        //DateFiled.Text = oReader["DateFiled"].ToString()!="1/1/1900 12:00:00 AM" ? oReader["DateFiled"].ToString(): "";
                        //fileuploaded_1.Text = oReader["Attachment"].ToString()!="" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["Attachment"].ToString() + "' target='_blank'>" + oReader["Attachment"].ToString() + "</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> No attached file";
                    }
                }
            }
        }
    }


    void SaveToDB()
    {
        
    }


    
}