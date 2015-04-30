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
using System.Reflection;
using System.IO;

public partial class cfo_vendorDetails : System.Web.UI.Page
{
    SqlDataReader oReader;
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
    string query;
    SqlCommand cmd;
    SqlConnection conn;
    //int numRowsTbl;
    SqlCommand myCommand;
    DataTable myDataSet;
    SqlConnection myConnection;

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
        if (Session["SESSION_USERTYPE"].ToString() != "18") Response.Redirect("login.aspx");
        if (IsPostBack)
        {
            SaveToDB();
        }
        else
        {
            //fillGrid();
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
                        if (oReader["Status"].ToString() == "6") { Response.Redirect("cfo_vendorDetails_View.aspx"); }
                    }
                }
            }
        }
        conn.Close();



        //query = "SELECT * FROM tblVendorApprovalbyPVMDHead WHERE VendorId = @VendorId";
        query = "select t1.*, t2.FirstName +' '+t2.LastName as Name from tblVendorApprovalbyPVMDHead t1, tblUsers t2, tblVendor t3 where t2.UserId = t3.approvedbyFAALogistics and t3.VendorId = t1.VendorId and t1.VendorId=@VendorId";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                conn.Open(); oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        if (oReader["vendorApproved"].ToString() == "1")
                        {
                            recommendation2.Text = "APPROVE";
                        }
                        else if (oReader["vendorApproved"].ToString() == "2")
                        {
                            recommendation2.Text = "CONDITIONALLY APPROVE";
                        }
                        else
                        {
                            recommendation2.Text = "DISAPPROVE";
                        }
                        recoby2.Text = oReader["Name"].ToString();
                        recodate2.Text = oReader["DateCreated"].ToString();
                    }
                }
            }
        }
        conn.Close();


        //query = "SELECT * FROM tblVendorApprovalbyVmReco WHERE VendorId = @VendorId";
        query = "select t1.*, t2.FirstName +' '+t2.LastName as Name from tblVendorApprovalbyVmReco t1, tblUsers t2, tblVendor t3 where t2.UserId = t3.approvedbyVMReco and t3.VendorId = t1.VendorId and t1.VendorId=@VendorId";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                conn.Open(); oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        AccreDuration.SelectedValue = oReader["AccreDuration"].ToString();
                        Others.Text = oReader["Others"].ToString();
                        FileAttachementLbl.Text = oReader["FileAttachement"].ToString() != "" ? "<a href='" + oReader["FileAttachement"].ToString() + "' target='_blank'>" + oReader["FileAttachement"].ToString() + "</a>" : "<h3>n/a</h3>";

                        if (oReader["Recommendation"].ToString() == "1")
                        {
                            recommendation.Text = "APPROVE";
                        }
                        else if (oReader["Recommendation"].ToString() == "2")
                        {
                            recommendation.Text = "CONDITIONALLY APPROVE";
                        }
                        else
                        {
                            recommendation.Text = "DISAPPROVE";
                        }
                        OverallEvalRemarks.Text = oReader["OverallEvalRemarks"].ToString().Replace("\n", "<br>");
                        recodate.Text = oReader["DateCreated"].ToString();
                        recoby.Text = oReader["Name"].ToString();
                    }
                }
            }
        }
        conn.Close();


        query = "SELECT t2.CompanyName, t1.* FROM tblDnbReport t1, tblVendor t2 WHERE t1.VendorId = @VendorId AND t2.VendorId = @VendorId";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"].ToString()));
                conn.Open(); oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {

                        int odnbScore, ovmoGTPerf_Eval;
                        CompanyNameLbl.Text = oReader["CompanyName"].ToString();
                        dnbDuns.Text = oReader["dnbDuns"].ToString();
                        dnbFinCapScore.Text = oReader["dnbFinCapScore"].ToString();
                        dnbFinCapScore_Remarks.Text = oReader["dnbFinCapScore_Remarks"].ToString().Replace("\n", "<br>");
                        dnbLegalConfScore.Text = oReader["dnbLegalConfScore"].ToString();
                        dnbLegalConfScore_Remarks.Text = oReader["dnbLegalConfScore_Remarks"].ToString().Replace("\n", "<br>");
                        dnbTechCompScore.Text = oReader["dnbTechCompScore"].ToString();
                        dnbTechCompScore_Remarks.Text = oReader["dnbTechCompScore_Remarks"].ToString().Replace("\n", "<br>");
                        dnbMaxExposureLimit.Text = oReader["dnbMaxExposureLimit"].ToString();
                        vmoNo_POs.Text = oReader["vmoNo_POs"].ToString();
                        vmoSpend.Text = oReader["vmoSpend"].ToString();
                        vmoWith_Existing_Frame_Arg.SelectedValue = oReader["vmoWith_Existing_Frame_Arg"].ToString() == "0" ? "0" : "1";
                        vmoIssues_bond_claims.Checked = oReader["vmoIssues_bond_claims"].ToString() == "1" ? true : false;
                        vmoIssue_risk_to_Globe.Checked = oReader["vmoIssue_risk_to_Globe"].ToString() == "1" ? true : false;
                        vmoConflict_of_Interest.Checked = oReader["vmoConflict_of_Interest"].ToString() == "1" ? true : false;
                        vmoWith_Type_Approved_Products.Checked = oReader["vmoWith_Type_Approved_Products"].ToString() == "1" ? true : false;
                        vmoWith_Approved_Proof_of_Concept.Checked = oReader["vmoWith_Approved_Proof_of_Concept"].ToString() == "1" ? true : false;
                        vmoGTPerf_Eval.Text = oReader["vmoGTPerf_Eval"].ToString();
                        vmoIssues_ISR_involvement.Checked = oReader["vmoIssues_ISR_involvement"].ToString() == "1" ? true : false;
                        vmoIssues_Loss_Incidents.Checked = oReader["vmoIssues_Loss_Incidents"].ToString() == "1" ? true : false;
                        vmoIssues_Others.Checked = oReader["vmoIssues_Others"].ToString() == "1" ? true : false;
                        vmoIssues_Remarks.Text = oReader["vmoIssues_Remarks"].ToString();
                        vmoGTPerf_Eval.Text = oReader["vmoGTPerf_Eval"].ToString();

                        vmoNew_Vendor.SelectedValue = oReader["vmoNew_Vendor"].ToString() == "0" ? "0" : "1";
                        odnbScore = oReader["dnbScore"].ToString() != "" ? Convert.ToInt32(oReader["dnbScore"].ToString()) : Convert.ToInt32(oReader["dnbFinCapScore"].ToString()) + Convert.ToInt32(oReader["dnbLegalConfScore"].ToString()) + Convert.ToInt32(oReader["dnbTechCompScore"].ToString());
                        dnbScore.Text = odnbScore.ToString();
                        if (oReader["vmoNew_Vendor"].ToString() == "0")
                        {
                            ovmoGTPerf_Eval = oReader["vmoGTPerf_Eval"].ToString() != "" ? Convert.ToInt32(oReader["vmoGTPerf_Eval"].ToString()) : 0;
                            vmoOverallScore.Text = ((odnbScore + ovmoGTPerf_Eval) / 2).ToString();
                        }
                        else
                        {
                            vmoOverallScore.Text = odnbScore.ToString();
                        }
                        dnbSupplierInfoReport.Text = oReader["dnbSupplierInfoReport"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["dnbSupplierInfoReport"].ToString() + "' target='_blank'>" + oReader["dnbSupplierInfoReport"].ToString() + "</a>" : "No attach file";
                        dnbOtherDocumentsLbl.Text = oReader["dnbOtherDocuments"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["dnbOtherDocuments"].ToString() + "' target='_blank'>" + oReader["dnbOtherDocuments"].ToString() + "</a>" : "No attach file";
                    }
                }
            }
        }
        conn.Close();



        legalStrucOrgType.Text = "n/a";
        query = "SELECT * FROM tblVendorInformation WHERE VendorId = @VendorId";
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
                        legalStrucOrgType.Text = oReader["legalStrucOrgType"].ToString() != "" ? oReader["legalStrucOrgType"].ToString() : "n/a";
                    }
                }
            }
        }
        conn.Close();

        string NatureOfBusiness1 = "";
        query = "SELECT t2.NatureOfBusinessName FROM tblVendorNatureOfBusiness t1, rfcNatureOfBusiness t2  WHERE t1.VendorId = @VendorId AND t1.NatureOfBusinessId = t2.NatureOfBusinessId";
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
                        NatureOfBusiness1 = NatureOfBusiness1 + oReader["NatureOfBusinessName"].ToString() + ", ";
                    }
                }
            }
        }
        conn.Close();
        NatureOfBusiness.Text = NatureOfBusiness1 != "" ? NatureOfBusiness1.Substring(0, NatureOfBusiness1.Length - 2) : "n/a";


        string Category1 = "";
        query = "SELECT t2.CategoryName FROM tblVendorProductsAndServices t1, rfcProductCategory t2  WHERE t1.VendorId = @VendorId AND t1.CategoryId = t2.CategoryId GROUP BY t2.CategoryName";
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
                        Category1 = Category1 + oReader["CategoryName"].ToString() + ", ";
                    }
                }
            }
        }
        conn.Close();
        Category.Text = Category1 != "" ? Category1.Substring(0, Category1.Length - 2) : "n/a";



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
                        CompanyNameLbl.Text = oReader["CompanyName"].ToString() != "" ? "<a href='vendor_Home.aspx?VendorId=" + Session["VendorId"] + "' target='_blank'>" + oReader["CompanyName"].ToString() + "</a>" : "n/a";
                        //AuthenticationTicketLbl.Text = oReader["AuthenticationTicket"].ToString();
                    }
                }
            }
        }
        conn.Close();


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
                            TypeOfCase.Text = oReader["TypeOfCase"].ToString() != "" ? oReader["TypeOfCase"].ToString() : "n/a";
                            //DateFiled.Text = oReader["DateFiled"].ToString() != "" ? oReader["DateFiled"].ToString() : "n/a";
                            fileuploaded_1.Text = oReader["Attachment"].ToString() != "" ? "<a href='" + oReader["Attachment"].ToString() + "' target='_blank'>" + oReader["Attachment"].ToString() + "</a>" : "<h3>n/a</h3>";
                        }
                    }
                }
            }
            conn.Close();
        
    }


    void SaveToDB()
    {
        //Response.Write(Request.Form["__EVENTTARGET"].ToString());
        string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
        string sCommand = "";

        if (Comment.Value != "")
        {
            query = "INSERT INTO tblComments (VendorId, UserId, Comment, DateCreated) VALUES (@VendorId, @UserId, @Comment, @DateCreated)";
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                    cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(Session["UserId"]));
                    cmd.Parameters.AddWithValue("@Comment", Comment.Value.ToString());
                    cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                    conn.Open(); cmd.ExecuteNonQuery();
                }
            }
            conn.Close();
        }



        if (Request.Form["__EVENTTARGET"] == "Approve")
        {
            //string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
            //string sCommand = "";

            //CLEAR tblVendorApprovalbyFAALFinance FROM VendorId
            sCommand = "DELETE FROM tblVendorApprovalbyFAALFinance WHERE VendorId = " + Session["VendorId"];
            query = "INSERT INTO tblVendorApprovalbyFAALFinance (VendorId, vendorApproved, DateCreated) VALUES (@VendorId, @vendorApproved, @DateCreated)";
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                    cmd.Parameters.AddWithValue("@vendorApproved", 1);
                    cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                    conn.Open(); cmd.ExecuteNonQuery();
                }
            }
            conn.Close();


            sCommand = "UPDATE tblVendor SET approvedbyFAAFinance = " + Session["UserId"] + ", approvedbyFAAFinanceDate = getdate(), Status = " + Constant.VENDOR_STATUS_APPROVED + " WHERE VendorId = " + Session["VendorId"];
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);


            query = "sp_UpdateVendorCode"; 
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure; 
                    cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                    conn.Open(); cmd.ExecuteNonQuery();
                }
            }
            conn.Close();

            // SEND EMAIL NOTIFICATION TO VM OFFICER
            //query = "SELECT t3.FirstName + ' ' + t3.LastName as fromName, t3.EmailAdd as fromEmail, t1.FirstName + ' ' + t1.LastName as toName, t1.EmailAdd as toEmail, t4.CompanyName, t4.AuthenticationTicket, t1.UserName, t1.UserPassword FROM tblUsers t1, tblUsersForVendors t2, tblUsers t3, tblVendor t4 WHERE t1.UserId = t2.UserId AND t2.VendorId = @VendorId AND t3.UserId = @UserId AND t4.VendorId = @VendorId";
            query = "SELECT t3.FirstName + ' ' + t3.LastName as fromName, t3.EmailAdd as fromEmail, t1.FirstName + ' ' + t1.LastName as toName, t1.EmailAdd as toEmail, t4.CompanyName, t4.AuthenticationTicket FROM tblUsers t1, tblUserTypes t2, tblUsers t3, tblVendor t4 WHERE t1.UserId = t2.UserId AND t1.Status = 1 AND t3.Status = 1 AND t2.UserType = 10 AND t3.UserId = @UserId AND t4.VendorId = @VendorId";
            string fromName = "", fromEmail = "", toName = "", toEmail = "", AuthenticationTicket = "", VendorName = "", Username = "", Password = "";
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                    cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(Session["UserId"]));
                    conn.Open(); oReader = cmd.ExecuteReader();
                    if (oReader.HasRows)
                    {
                        while (oReader.Read())
                        {
                            fromName = oReader["fromName"].ToString();
                            fromEmail = oReader["fromEmail"].ToString();
                            toName = oReader["toName"].ToString();
                            toEmail = oReader["toEmail"].ToString();
                            AuthenticationTicket = oReader["AuthenticationTicket"].ToString();
                            VendorName = oReader["CompanyName"].ToString();
                            //Username = oReader["UserName"].ToString();
                            //Password = EncryptionHelper.Decrypt(oReader["UserPassword"].ToString());
                            //SendEmailNotification(fromName, fromEmail, toName, toEmail, AuthenticationTicket, VendorName, Session["VendorId"].ToString(), Username, Password);
                            SendEmailNotificationForImport(fromName, fromEmail, toName, toEmail, AuthenticationTicket, VendorName);

                        }
                    }
                }
            }
            conn.Close();
            // SEND EMAIL NOTIFICATION TO VM OFFICER ENDS

            // SEND EMAIL NOTIFICATION TO VENDOR
            //query = "SELECT t3.FirstName + ' ' + t3.LastName as fromName, t3.EmailAdd as fromEmail, t1.FirstName + ' ' + t1.LastName as toName, t1.EmailAdd as toEmail, t4.CompanyName, t4.AuthenticationTicket, t1.UserName, t1.UserPassword FROM tblUsers t1, tblUsersForVendors t2, tblUsers t3, tblVendor t4 WHERE t1.UserId = t2.UserId AND t2.VendorId = @VendorId AND t3.UserId = @UserId AND t4.VendorId = @VendorId";
            //string fromName = "", fromEmail = "", toName = "", toEmail = "", AuthenticationTicket = "", VendorName = "", Username="", Password="";
            //using (conn = new SqlConnection(connstring))
            //{
            //    using (cmd = new SqlCommand(query, conn))
            //    {
            //        cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
            //        cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(Session["UserId"]));
            //        conn.Open(); oReader = cmd.ExecuteReader();
            //        if (oReader.HasRows)
            //        {
            //            while (oReader.Read())
            //            {
            //                fromName = oReader["fromName"].ToString();
            //                fromEmail = oReader["fromEmail"].ToString();
            //                toName = oReader["toName"].ToString();
            //                toEmail = oReader["toEmail"].ToString();
            //                AuthenticationTicket = oReader["AuthenticationTicket"].ToString();
            //                VendorName = oReader["CompanyName"].ToString();
            //                Username = oReader["UserName"].ToString();
            //                Password = EncryptionHelper.Decrypt(oReader["UserPassword"].ToString());
            //                //SendEmailNotification(fromName, fromEmail, toName, toEmail, AuthenticationTicket, VendorName, Session["VendorId"].ToString(), Username, Password);
            //            }
            //        }
            //    }
            //}
            //conn.Close();
            // SEND EMAIL NOTIFICATION TO VM VENDOR ENDS

            //fillGrid();
            //SaveVendorDetailsToESource();
            errNotification.Text = "Vendor has been accredited.";
            errNotification.ForeColor = Color.Blue;
            createBt.Visible = false;
            //createBt1.Visible = false;
            Session["PrevUrl"] = "cfo_VendorList.aspx";
            //Response.Redirect("cfo_vendorDetails_View.aspx");
        }
        else if (Request.Form["__EVENTTARGET"] == "clarifyThis")
        {
            //string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
            //string sCommand = "";
            //CLEAR tblVendorApprovalbyVmTech FROM VendorId
            sCommand = "UPDATE tblVendor SET clarifiedtoVMRecoBy = " + Session["UserId"] + ", clarifiedtoVMRecoDate = getdate(), Status = " + Constant.VENDOR_STATUS_CLARIFYTOVMHEAD + " WHERE VendorId = " + Session["VendorId"];
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

            // SEND EMAIL NOTIFICATION TO VM RECO
            query = "SELECT t3.FirstName + ' ' + t3.LastName as fromName, t3.EmailAdd as fromEmail, t1.FirstName + ' ' + t1.LastName as toName, t1.EmailAdd as toEmail, t4.CompanyName, t4.AuthenticationTicket FROM tblUsers t1, tblUserTypes t2, tblUsers t3, tblVendor t4 WHERE t1.UserId = t2.UserId AND t2.UserType = 16 AND t3.UserId = @UserId AND t4.Status = 7 AND t4.VendorId = @VendorId";
            string fromName = "", fromEmail = "", toName = "", toEmail = "", AuthenticationTicket = "", VendorName = "";
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                    cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(Session["UserId"]));
                    conn.Open(); oReader = cmd.ExecuteReader();
                    if (oReader.HasRows)
                    {
                        while (oReader.Read())
                        {
                            fromName = oReader["fromName"].ToString();
                            fromEmail = oReader["fromEmail"].ToString();
                            toName = oReader["toName"].ToString();
                            toEmail = oReader["toEmail"].ToString();
                            AuthenticationTicket = oReader["AuthenticationTicket"].ToString();
                            VendorName = oReader["CompanyName"].ToString();
                            SendEmailNotificationClarify(fromName, fromEmail, toName, toEmail, AuthenticationTicket, VendorName);
                        }
                    }
                }
            }
            conn.Close();
            // SEND EMAIL NOTIFICATION TO VM RECO ENDS

            errNotification.Text = "Vendor clarification has been endorsed.";
            errNotification.ForeColor = Color.Blue;
            createBt.Visible = false;
            //createBt1.Visible = false;
            Session["PrevUrl"] = "cfo_VendorList.aspx";
            Response.Redirect("cfo_VendorList.aspx");
        }
        else if (Request.Form["__EVENTTARGET"] == "Disapprove")
        {
            //string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
            //string sCommand = "";
            //CLEAR tblVendorApprovalbyVmTech FROM VendorId
            sCommand = "UPDATE tblVendor SET Status = " + Constant.VENDOR_STATUS_DISAPPROVED + " WHERE VendorId = " + Session["VendorId"];
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

            // SEND EMAIL NOTIFICATION TO VENDOR
            query = "SELECT 'Trans-Asia Admin' as fromName, 'noreply@phinma.com.ph' as fromEmail, t1.FirstName + ' ' + t1.LastName as toName, t1.EmailAdd as toEmail, t4.CompanyName, t4.AuthenticationTicket FROM tblUsers t1, tblUsersForVendors t2, tblUsers t3, tblVendor t4 WHERE t1.UserId = t2.UserId AND t2.VendorId = @VendorId AND t3.UserId = @UserId AND t4.Status = 8 AND t4.VendorId = @VendorId";
            string fromName = "", fromEmail = "", toName = "", toEmail = "", AuthenticationTicket = "", VendorName = "";
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                    cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(Session["UserId"]));
                    conn.Open(); oReader = cmd.ExecuteReader();
                    if (oReader.HasRows)
                    {
                        while (oReader.Read())
                        {
                            fromName = oReader["fromName"].ToString();
                            fromEmail = oReader["fromEmail"].ToString();
                            toName = oReader["toName"].ToString();
                            toEmail = oReader["toEmail"].ToString();
                            AuthenticationTicket = oReader["AuthenticationTicket"].ToString();
                            VendorName = oReader["CompanyName"].ToString();
                            SendEmailNotificationReject(fromName, fromEmail, toName, toEmail, AuthenticationTicket, VendorName, Session["VendorId"].ToString());
                        }
                    }
                }
            }
            conn.Close();
            // SEND EMAIL NOTIFICATION TO VM RECO ENDS

            errNotification.Text = "Vendor clarification has been endorsed.";
            errNotification.ForeColor = Color.Blue;
            createBt.Visible = false;
            //createBt1.Visible = false;
            Session["PrevUrl"] = "cfo_VendorList.aspx";
            Response.Redirect("cfo_VendorList.aspx");
        }

    }










    private int SaveVendorDetailsToESource()
    {

        string connstring2 = ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString;
        SqlConnection sqlConnect = new SqlConnection(connstring2);
        SqlTransaction sqlTransact = null;
        int value = 0;

        query = "SELECT t1.*, t3.UserName, t3.UserPassword FROM tblVendorInformation t1, tblUsersForVendors t2, tblUsers t3  WHERE t2.VendorId = @VendorId AND t3.UserId = t2.UserId AND t1.VendorId = @VendorId";
        string fromName = "", fromEmail = "", toName = "", toEmail = "", AuthenticationTicket = "", VendorName = "";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                conn.Open();
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {

                        string username = oReader["UserName"].ToString();
                        //string password = EncryptionHelper.Encrypt(randomPwd);
                        string password = oReader["UserPassword"].ToString();
                        string CompanyName = oReader["CompanyName"].ToString();
                        string tbHeadOfficeAddress1 = "";
                        string tbHeadOfficeAddress2 = "";
                        string tbTelephone = oReader["conBidTelNo"].ToString();
                        string tbMobileNo = oReader["conBidMobile"].ToString();
                        string tbFax = oReader["conBidFaxNo"].ToString();
                        string tbExtension = "";
                        string tbBranchAddress1 = "";
                        string tbBranchAddress2 = "";
                        string tbBranchPhone = "";
                        string tbBranchFax = "";
                        string tbBranchExtension = "";
                        string tbVatRegNo = "";string tbTIN = "";
                        string tbPOBox = "";
                        string tbPostalCode = "";
                        string tbTermsofPayment = "";
                        string tbSpecialTerms = "";
                        string tbMinOrderValue = "";
                        string tbSalesPerson = "";
                        string tbSalesPersonPhone = "";
                        string tbEmail = "";
                        string tbSoleSupplier1 = "";
                        string tbSoleSupplier2 = "";
                        string tbKeyPersonnel = "";
                        string tbKpPosition = "";
                        string tbSpecialization = "";
                        string ddlSupplierType = "";
                        string rbOrganizationType = "";
                        string tbOwnershipFilipino = "";
                        string tbOwnershipOther = "";
                        string SaveSelectedISO = "";
                        string ddlPCABClass = "";

                        tbHeadOfficeAddress1 = oReader["regBldgBldg"].ToString() != "" ? tbHeadOfficeAddress1 + "Bldg. " + oReader["regBldgBldg"].ToString() + ", " : tbHeadOfficeAddress1 + "";
                        tbHeadOfficeAddress1 = oReader["regBldgUnit"].ToString() != "" ? tbHeadOfficeAddress1 + "Rm. " + oReader["regBldgUnit"].ToString() + ", " : tbHeadOfficeAddress1 + "";
                        tbHeadOfficeAddress1 = oReader["regBldgLotNo"].ToString() != "" ? tbHeadOfficeAddress1 + oReader["regBldgLotNo"].ToString() + ", " : tbHeadOfficeAddress1 + "";
                        tbHeadOfficeAddress1 = oReader["regBldgBlock"].ToString() != "" ? tbHeadOfficeAddress1 + oReader["regBldgBlock"].ToString() + ", " : tbHeadOfficeAddress1 + "";
                        tbHeadOfficeAddress1 = oReader["regBldgPhase"].ToString() != "" ? tbHeadOfficeAddress1 + oReader["regBldgPhase"].ToString() + ", " : tbHeadOfficeAddress1 + "";
                        tbHeadOfficeAddress1 = oReader["regBldgHouseNo"].ToString() != "" ? tbHeadOfficeAddress1 + "No. " + oReader["regBldgHouseNo"].ToString() + ", " : tbHeadOfficeAddress1 + "";
                        tbHeadOfficeAddress1 = oReader["regBldgStreet"].ToString() != "" ? tbHeadOfficeAddress1 + oReader["regBldgStreet"].ToString() + ", " : tbHeadOfficeAddress1 + "";
                        tbHeadOfficeAddress1 = oReader["regBldgSubd"].ToString() != "" ? tbHeadOfficeAddress1 + oReader["regBldgSubd"].ToString() + ", " : tbHeadOfficeAddress1 + "";
                        tbHeadOfficeAddress1 = oReader["regBrgy"].ToString() != "" ? tbHeadOfficeAddress1 + oReader["regBrgy"].ToString() + ", " : tbHeadOfficeAddress1 + "";

                        tbHeadOfficeAddress2 = oReader["regCity"].ToString() != "" ? tbHeadOfficeAddress2 + oReader["regCity"].ToString() + ", " : tbHeadOfficeAddress2 + "";
                        tbHeadOfficeAddress2 = oReader["regProvince"].ToString() != "" ? tbHeadOfficeAddress2 + oReader["regProvince"].ToString() + ", " : tbHeadOfficeAddress2 + "";
                        tbHeadOfficeAddress2 = oReader["regCountry"].ToString() != "" ? tbHeadOfficeAddress2 + oReader["regCountry"].ToString() + ", " : tbHeadOfficeAddress2 + "";
                        tbHeadOfficeAddress2 = oReader["regPostal"].ToString() != "" ? tbHeadOfficeAddress2 + oReader["regPostal"].ToString() + " " : tbHeadOfficeAddress2 + "";

                        try
                        {
                            sqlConnect.Open();
                            sqlTransact = sqlConnect.BeginTransaction();

                            //string randomPwd = RandomPasswordGenerator.GenerateRandomPassword();

                            SqlParameter[] sqlParams = new SqlParameter[38];
                            sqlParams[0] = new SqlParameter("@userName", SqlDbType.VarChar);
                            sqlParams[0].Value = username;
                            sqlParams[1] = new SqlParameter("@password", SqlDbType.VarChar);
                            sqlParams[1].Value = password; 
                            sqlParams[2] = new SqlParameter("@vendorName", SqlDbType.VarChar);
                            sqlParams[2].Value = CompanyName;
                            sqlParams[3] = new SqlParameter("@vendorAddress", SqlDbType.VarChar);
                            sqlParams[3].Value = (tbHeadOfficeAddress1.Trim().Length > 0) ? tbHeadOfficeAddress1.Trim() : null;
                            sqlParams[4] = new SqlParameter("@vendorAddress1", SqlDbType.VarChar);
                            sqlParams[4].Value = (tbHeadOfficeAddress2.Trim().Length > 0) ? tbHeadOfficeAddress2.Trim() : null;
                            sqlParams[5] = new SqlParameter("@vendorPhone", SqlDbType.VarChar);
                            sqlParams[5].Value = (tbTelephone.Trim().Length > 0) ? tbTelephone.Trim() : null;
                            sqlParams[6] = new SqlParameter("@vendorMobile", SqlDbType.VarChar);
                            sqlParams[6].Value = (tbMobileNo.Trim().Length > 0) ? tbMobileNo.Trim() : null;
                            sqlParams[7] = new SqlParameter("@vendorFax", SqlDbType.VarChar);
                            sqlParams[7].Value = (tbFax.Trim().Length > 0) ? tbFax.Trim() : null;
                            sqlParams[8] = new SqlParameter("@vendorExt", SqlDbType.VarChar);
                            sqlParams[8].Value = (tbExtension.Trim().Length > 0) ? tbExtension.Trim() : null;
                            sqlParams[9] = new SqlParameter("@branchAddress", SqlDbType.VarChar);
                            sqlParams[9].Value = (tbBranchAddress1.Trim().Length > 0) ? tbBranchAddress1.Trim() : null;
                            sqlParams[10] = new SqlParameter("@branchAddress1", SqlDbType.VarChar);
                            sqlParams[10].Value = (tbBranchAddress2.Trim().Length > 0) ? tbBranchAddress2.Trim() : null;
                            sqlParams[11] = new SqlParameter("@branchPhone", SqlDbType.VarChar);
                            sqlParams[11].Value = (tbBranchPhone.Trim().Length > 0) ? tbBranchPhone.Trim() : null;
                            sqlParams[12] = new SqlParameter("@branchFax", SqlDbType.VarChar);
                            sqlParams[12].Value = (tbBranchFax.Trim().Length > 0) ? tbBranchFax.Trim() : null;
                            sqlParams[13] = new SqlParameter("@branchExt", SqlDbType.VarChar);
                            sqlParams[13].Value = (tbBranchExtension.Trim().Length > 0) ? tbBranchExtension.Trim() : null;
                            sqlParams[14] = new SqlParameter("@vatRegNo", SqlDbType.VarChar);
                            sqlParams[14].Value = (tbVatRegNo.Trim().Length > 0) ? tbVatRegNo.Trim() : null;
                            sqlParams[15] = new SqlParameter("@TIN", SqlDbType.VarChar);
                            sqlParams[15].Value = (tbTIN.Trim().Length > 0) ? tbTIN.Trim() : null;
                            sqlParams[16] = new SqlParameter("@POBox", SqlDbType.VarChar);
                            sqlParams[16].Value = (tbPOBox.Trim().Length > 0) ? tbPOBox.Trim() : null;
                            sqlParams[17] = new SqlParameter("@postalCode", SqlDbType.VarChar);
                            sqlParams[17].Value = (tbPostalCode.Trim().Length > 0) ? tbPostalCode.Trim() : null;
                            sqlParams[18] = new SqlParameter("@standardTerms", SqlDbType.VarChar);
                            sqlParams[18].Value = (tbTermsofPayment.Trim().Length > 0) ? tbTermsofPayment.Trim() : null;
                            sqlParams[19] = new SqlParameter("@specialTerms", SqlDbType.VarChar);
                            sqlParams[19].Value = (tbSpecialTerms.Trim().Length > 0) ? tbSpecialTerms.Trim() : null;
                            sqlParams[20] = new SqlParameter("@minOrderVal", SqlDbType.Float);
                            sqlParams[20].Value = (tbMinOrderValue.Trim().Length > 0) ? float.Parse(tbMinOrderValue.Trim()) : 0;
                            sqlParams[21] = new SqlParameter("@salesPerson", SqlDbType.VarChar);
                            sqlParams[21].Value = (tbSalesPerson.Trim().Length > 0) ? tbSalesPerson.Trim() : null;
                            sqlParams[22] = new SqlParameter("@salesPersonPhone", SqlDbType.VarChar);
                            sqlParams[22].Value = (tbSalesPersonPhone.Trim().Length > 0) ? tbSalesPersonPhone.Trim() : null;
                            sqlParams[23] = new SqlParameter("@emailAdd", SqlDbType.VarChar);
                            sqlParams[23].Value = (tbEmail.Trim().Length > 0) ? tbEmail.Trim() : null;
                            sqlParams[24] = new SqlParameter("@soleSupplier1", SqlDbType.VarChar);
                            sqlParams[24].Value = (tbSoleSupplier1.Trim().Length > 0) ? tbSoleSupplier1.Trim() : null;
                            sqlParams[25] = new SqlParameter("@soleSupplier2", SqlDbType.VarChar);
                            sqlParams[25].Value = (tbSoleSupplier2.Trim().Length > 0) ? tbSoleSupplier2.Trim() : null;
                            sqlParams[26] = new SqlParameter("@keyPersonnel", SqlDbType.VarChar);
                            sqlParams[26].Value = (tbKeyPersonnel.Trim().Length > 0) ? tbKeyPersonnel.Trim() : null;
                            sqlParams[27] = new SqlParameter("@kpPosition", SqlDbType.VarChar);
                            sqlParams[27].Value = (tbKpPosition.Trim().Length > 0) ? tbKpPosition.Trim() : null;
                            sqlParams[28] = new SqlParameter("@specialization", SqlDbType.VarChar);
                            sqlParams[28].Value = (tbSpecialization.Trim().Length > 0) ? tbSpecialization.Trim() : null;
                            sqlParams[29] = new SqlParameter("@category", SqlDbType.VarChar);
                            sqlParams[29].Value = "0";
                            sqlParams[30] = new SqlParameter("@accredited", SqlDbType.Int);
                            sqlParams[30].Value = ddlSupplierType.Trim().Length > 0 ? Int32.Parse(ddlSupplierType) : 1;
                            sqlParams[31] = new SqlParameter("@orgTypeId", SqlDbType.Int);
                            sqlParams[31].Value = rbOrganizationType.Trim().Length > 0 ? Int32.Parse(rbOrganizationType) : 1; 
                            sqlParams[32] = new SqlParameter("@ownershipFil", SqlDbType.Int);
                            sqlParams[32].Value = (tbOwnershipFilipino.Trim().Length > 0) ? Int32.Parse(tbOwnershipFilipino.Trim()) : 0;
                            sqlParams[33] = new SqlParameter("@ownershipOther", SqlDbType.Int);
                            sqlParams[33].Value = (tbOwnershipOther.Trim().Length > 0) ? Int32.Parse(tbOwnershipOther.Trim()) : 0;
                            sqlParams[34] = new SqlParameter("@classification", SqlDbType.Int);
                            sqlParams[34].Value = 1;
                            sqlParams[35] = new SqlParameter("@isoStandard", SqlDbType.NVarChar);
                            sqlParams[35].Value = SaveSelectedISO.Trim();
                            sqlParams[36] = new SqlParameter("@pcabClass", SqlDbType.Int);
                            sqlParams[36].Value = (ddlPCABClass.Trim().Length > 0) ? Int32.Parse(ddlPCABClass.Trim()) : 1; 
                            sqlParams[37] = new SqlParameter("@Clientid", SqlDbType.Int);
                            sqlParams[37].Value = 1;

                            value = Convert.ToInt32(SqlHelper.ExecuteScalar(sqlTransact, "s3p_EBid_SaveVendorDetails", sqlParams));

                            sqlTransact.Commit();
                        }
                        catch (Exception ex)
                        {
                            sqlTransact.Rollback();
                            value = 0;
                            Response.Write(ex.ToString());
                        }
                        finally
                        {
                            sqlConnect.Close();
                        }

                        query = "INSERT INTO tblVendorCategoriesAndSubcategories (VendorId, CategoryId, IncludesAllSubCategories, MainCategoryId) VALUES ((select MAX(VendorId) from tblVendors), @CategoryId, @IncludesAllSubCategories, @MainCategoryId)";
                        //query = "sp_GetVendorInformation"; //##storedProcedure
                        using (conn = new SqlConnection(connstring2))
                        {
                            using (cmd = new SqlCommand(query, conn))
                            {
                                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                                //cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                                cmd.Parameters.AddWithValue("@CategoryId", "CAT0000");
                                cmd.Parameters.AddWithValue("@IncludesAllSubCategories", 1);
                                cmd.Parameters.AddWithValue("@MainCategoryId", "MC000");
                                conn.Open(); cmd.ExecuteNonQuery();
                            }
                        }
                        conn.Close();
                    }
                }
            }
        }
        conn.Close();

        btnExportToExcel_Click();
        

        return value;
    }


    protected void fillGrid()
    {
        //string str = "SELECT TOP 10 VendorName, VendorEmail, MobileNo, VendorAddress FROM tblVendors";
        string str = "sp_GetVendorInformation";

        myConnection = new SqlConnection(connstring);
        myConnection.Open();
        myCommand = new SqlCommand(str, myConnection);
        myCommand.CommandType = CommandType.StoredProcedure;
        myCommand.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"].ToString()));
        SqlDataAdapter mySQLDataAdapter;
        myDataSet = new DataTable();
        mySQLDataAdapter = new SqlDataAdapter(myCommand);
        mySQLDataAdapter.Fill(myDataSet);
        //GridView1.DataSource = myDataSet;
        //GridView1.DataBind();
        ViewState["dtList"] = myDataSet;
    }
    void btnExportToExcel_Click()
    {
        try
        {
            DataTable dt1 = (DataTable)ViewState["dtList"];
            if (dt1 == null)
            {
                throw new Exception("No Records to Export");
            }
            //string Path = "C:\\eSourcePOtoACCPAC\\eSourceForPO_" + Session["BidRefNo"] + ".xls";
            string Path = "C:\\eSourcePOtoACCPAC\\VendorImportFile_" + Session["VendorId"] + ".xls";
            FileInfo FI = new FileInfo(Path);
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWriter);
            DataGrid DataGrd = new DataGrid();
            DataGrd.DataSource = dt1;
            DataGrd.DataBind();

            DataGrd.RenderControl(htmlWrite);
            string directory = Path.Substring(0, Path.LastIndexOf("\\"));// GetDirectory(Path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            System.IO.StreamWriter vw = new System.IO.StreamWriter(Path, true);
            stringWriter.ToString().Normalize();
            vw.Write(stringWriter.ToString());
            vw.Flush();
            vw.Close();
            WriteAttachment(FI.Name, "application/vnd.ms-excel", stringWriter.ToString());
        }
        catch (Exception ex)
        {
            //throw new Exception(ex.Message);
        }
    }


    public static void WriteAttachment(string FileName, string FileType, string content)
    {
        HttpResponse Response = System.Web.HttpContext.Current.Response;
        Response.ClearHeaders();
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
        Response.ContentType = FileType;
        Response.Write(content);
        Response.End();

    }

















    //############################################################
    //############################################################
    // SEND EMAIL NOTIFICATION TO VENDOR IF APPROVED
    private bool SendEmailNotification(string sfromName, string sfromEmail, string stoName, string stoEmail, string sAuthenticationTicket, string sVendorName, string VendorIdx, string Username, string Password)
    {
        //Response.Write(VendorIdx);
        bool success = false;

        string from = sfromName + "<" + sfromEmail + ">";
        string to = stoName + "<" + stoEmail + ">";
        string subject = "";

        //Response.Write(from + "<br>" + to);

        try
        {
            subject = "Trans-Asia VMS Accreditation application approved.";
            if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                    from,
                    to,
                    subject,
                    CreateNotificationBody(sfromName, stoName, sAuthenticationTicket, sVendorName, VendorIdx, Username, Password),
                    MailTemplate.GetTemplateLinkedResources(this)))
            {	//if sending failed					
                LogHelper.EventLogHelper.Log("Bid > Send Notification : Sending Failed to " + from, System.Diagnostics.EventLogEntryType.Error);
            }
            else
            {	//if sending successful
                LogHelper.EventLogHelper.Log("Bid > Send Notification : Email Sent to " + from, System.Diagnostics.EventLogEntryType.Information);
            }
            success = true;
        }
        catch (Exception ex)
        {
            success = false;
            LogHelper.EventLogHelper.Log("Bid > Send Notification : " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            Response.Write(ex.ToString());
        }
        return success;
    }

    private string CreateNotificationBody(string cfromName, string ctoName, string cAuthenticationNumber, string cVendorName, string VendorIdx, string Username, string Password)
    {
        SqlDataReader oReader;
        string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
        string cCeo = "", cCeoEmail = "", cAddress = "", cServices = "", cAccreDuration = "";
        query = "SELECT * FROM tblVendorInformation WHERE VendorId = @VendorId";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(VendorIdx));
                conn.Open(); oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        cCeo = oReader["conBidName"].ToString();
                        cCeoEmail = oReader["conBidEmail"].ToString();
                        cAddress = oReader["regBldgBldg"].ToString() != "" ? cAddress + "Bldg. " + oReader["regBldgBldg"].ToString() + ", " : cAddress + "";
                        cAddress = oReader["regBldgUnit"].ToString() != "" ? cAddress + "Rm. " + oReader["regBldgUnit"].ToString() + ", " : cAddress + "";
                        cAddress = oReader["regBldgLotNo"].ToString() != "" ? cAddress + oReader["regBldgLotNo"].ToString() + ", " : cAddress + "";
                        cAddress = oReader["regBldgBlock"].ToString() != "" ? cAddress + oReader["regBldgBlock"].ToString() + ", " : cAddress + "";
                        cAddress = oReader["regBldgPhase"].ToString() != "" ? cAddress + oReader["regBldgPhase"].ToString() + ", " : cAddress + "";
                        cAddress = oReader["regBldgHouseNo"].ToString() != "" ? cAddress + "No. " + oReader["regBldgHouseNo"].ToString() + ", " : cAddress + "";
                        cAddress = oReader["regBldgStreet"].ToString() != "" ? cAddress + oReader["regBldgStreet"].ToString() + ", " : cAddress + "";
                        cAddress = oReader["regBldgSubd"].ToString() != "" ? cAddress + oReader["regBldgSubd"].ToString() + ", " : cAddress + "";
                        cAddress = oReader["regBrgy"].ToString() != "" ? cAddress + oReader["regBrgy"].ToString() + ", " : cAddress + "";
                        cAddress = cAddress + "<br>";
                        cAddress = oReader["regCity"].ToString() != "" ? cAddress + oReader["regCity"].ToString() + ", " : cAddress + "";
                        cAddress = oReader["regProvince"].ToString() != "" ? cAddress + oReader["regProvince"].ToString() + ", " : cAddress + "";
                        cAddress = cAddress + "<br>";
                        cAddress = oReader["regCountry"].ToString() != "" ? cAddress + oReader["regCountry"].ToString() + ", " : cAddress + "";
                        cAddress = oReader["regPostal"].ToString() != "" ? cAddress + oReader["regPostal"].ToString() + " " : cAddress + "";
                    }
                }
            }
        }
        conn.Close();

        //Response.Write(VendorIdx);
        //query = "SELECT t1.*, t2.CategoryName FROM tblVendorProductsAndServices t1, rfcProductCategory t2 WHERE t2.CategoryId = t1.CategoryId AND t1.VendorId = @VendorId";
        query = "SELECT t1.*, t2.CategoryName, t3.SubCategoryName FROM tblVendorProductsAndServices t1, rfcProductCategory t2, rfcProductSubcategory t3 WHERE t2.CategoryId = t1.CategoryId AND t3.SubCategoryId = t1.SubCategoryId AND t1.VendorId = @VendorId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(VendorIdx));
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        //cServices = cServices + "&bull; " + oReader["CategoryName"].ToString() + "<br>";
                        cServices = cServices + "&bull; " + oReader["CategoryName"].ToString() + " - " + oReader["SubCategoryName"].ToString() + "<br>";
                    }
                }
            }
        }
        conn.Close();

        query = "SELECT * FROM tblVendorApprovalbyVmReco  WHERE VendorId = @VendorId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(VendorIdx));
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        cAccreDuration = oReader["AccreDuration"].ToString();
                        //Response.Write(cAccreDuration);
                    }
                }
            }
        }
        conn.Close();



        StringBuilder sb = new StringBuilder();
        string sTxt = "<table border='1' style='font-size:12px'>";
        sTxt = sTxt + "<tr>";
        sTxt = sTxt + "<td><strong>&nbsp;UserName</strong></td>";
        sTxt = sTxt + "<td>&nbsp;" + Username + "&nbsp;</td>";
        sTxt = sTxt + "</tr>";
        sTxt = sTxt + "<tr>";
        sTxt = sTxt + "<td><strong>&nbsp;Temporary Password</strong></td>";
        sTxt = sTxt + "<td>&nbsp;" + Password + "&nbsp;</td>";
        sTxt = sTxt + "</tr>";
        //sTxt = sTxt + "<tr>";
        //sTxt = sTxt + "<td><strong>&nbsp;Authentication Ticket</strong></td>";
        //sTxt = sTxt + "<td>&nbsp;" + cAuthenticationNumber + "&nbsp;</td>";
        //sTxt = sTxt + "</tr>";
        sTxt = sTxt + "</table>";

        //sb.Append("<tr><td><p>Sent: " + DateTime.Now.ToLongDateString() + "<br>From: " + cfromName + "<br> To: " + ctoName + "<br><br> Congratulations!<br><br> This is to inform you that application for vendor accreditation has been approved.<br></p><br>" + sTxt + "<p>Very truly yours,<br><br><br> <strong>" + cfromName + "</strong></p><p>&nbsp;</p> <span style='font-size:10px; font-style:italic;'>Please do not reply to this auto-generated  message.&nbsp;</span></td></tr>");
        sb.Append("<tr><td>");
        sb.Append("<p>");
        sb.Append("Date: " + DateTime.Now.ToLongDateString() + "<br><br>");
        sb.Append(cCeo + "<br>");
        sb.Append("<b>" + cVendorName + "</b><br>");
        sb.Append(cAddress + "<br><br>");
        sb.Append("</p>");
        sb.Append("<tr><td>");
        sb.Append("<p>");
        sb.Append("Dear " + cCeo + ":<br><br>");
        sb.Append("We are pleased to inform you that Trans-Asia Vendor Management has approved your accreditation.<br><br>");
        //sb.Append("We are pleased to inform you that Trans-Asia Vendor Management has approved your accreditation for the following:<br><br>");
        //sb.Append("<b>PRODUCTS/SERVICES OFFERED   :  </b><br>");
        //sb.Append(cServices + "<br><br><br>");
        sb.Append("Please inform us immediately should there be any change in your products and services, organization, management, ownership, contact numbers, address and other material information which will affect our business relationship.<br><br>");
        sb.Append("This certification is valid for " + cAccreDuration + " from date of issuance. Trans-Asia reserves the right to invite and select suppliers, determine allocation and volume of orders in accordance with the company's procurement policies and objectives. Trans-Asia is also not committed to place an order upon accreditation. <br><br>");
        sb.Append("Your continuing status as an accredited supplier of Trans-Asia depends on your performance as a vendor/service provider subject to regular review and your compliance to company requirements. Consequently, Trans-Asia reserves the right to invite and require you to undergo an accreditation renewal process 60 days prior to the lapse of the " + cAccreDuration + " period.<br><br>");
        sb.Append("As part of Trans-Asia's program for good governance, we would like to take this opportunity to remind you, our business partner, of Trans-Asia's Gifts and Inducement Policy.  Our policy strongly prohibits Trans-Asia employees from soliciting gifts from business partners; and conversely prohibits business partners from giving Trans-Asia employees gifts of any kind in consideration of business, or as an inducement for the award of business.  <br><br>");
        sb.Append("We congratulate you and we look forward to a mutually beneficial and long-lasting business relationship with you. <br><br>");
        sb.Append("Please clic the link below and login using the provided username and password for Trans-Asia e-Sourcing System. <br><br>");
        sb.Append("<a href='http://vm2008:300/'>http://vm2008:300/</a><br><br>");
        sb.Append(sTxt);
        sb.Append("</p>");
        sb.Append("<br><br><br><br>");
        sb.Append("Very truly yours,<br><br>");
        sb.Append("<b>Honesto P. Oliva</b><br>");
        sb.Append("Head - Vendor Management<br><br>");
        //sb.Append("<b>Trans-Asia Admin<br><br>");
        sb.Append("</td></tr>");
        sb.Append("<tr><td>");
        sb.Append("<p>&nbsp;</p><span style='font-size:10px; font-style:italic;'>Please do not reply to this auto-generated  message.&nbsp;</span>");
        sb.Append("</td></tr>");
        //Response.Write(sb.ToString());
        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }





    //############################################################
    //############################################################
    // SEND EMAIL NOTIFICATION TO VENDOR IF REJECTED
    private bool SendEmailNotificationReject(string sfromName, string sfromEmail, string stoName, string stoEmail, string sAuthenticationTicket, string sVendorName, string VendorIdx)
    {
        bool success = false;

        string from = sfromName + "<" + sfromEmail + ">";
        string to = stoName + "<" + stoEmail + ">";
        string subject = "";

        try
        {
            subject = "Trans-Asia VMS Accreditation application rejected.";
            if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                    from,
                    to,
                    subject,
                    CreateNotificationBodyReject(sfromName, stoName, sAuthenticationTicket, sVendorName, VendorIdx),
                    MailTemplate.GetTemplateLinkedResources(this)))
            {	//if sending failed					
                LogHelper.EventLogHelper.Log("Bid > Send Notification : Sending Failed to " + from, System.Diagnostics.EventLogEntryType.Error);
            }
            else
            {	//if sending successful
                LogHelper.EventLogHelper.Log("Bid > Send Notification : Email Sent to " + from, System.Diagnostics.EventLogEntryType.Information);
            }
            success = true;
        }
        catch (Exception ex)
        {
            success = false;
            LogHelper.EventLogHelper.Log("Bid > Send Notification : " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            //Response.Write(ex.ToString());
        }
        return success;
    }

    private string CreateNotificationBodyReject(string cfromName, string ctoName, string cAuthenticationNumber, string cVendorName, string VendorIdx)
    {
        StringBuilder sb = new StringBuilder();
        string sTxt = "<table border='0' cellpadding='5' style='font-size:12px'>";
        sTxt = sTxt + "<tr>";
        sTxt = sTxt + "<td><strong>&nbsp;Vendor ID</strong></td>";
        sTxt = sTxt + "<td>&nbsp;" + VendorIdx + "&nbsp;</td>";
        sTxt = sTxt + "</tr>";
        sTxt = sTxt + "<tr>";
        sTxt = sTxt + "<td><strong>&nbsp;Company Name</strong></td>";
        sTxt = sTxt + "<td>&nbsp;" + cVendorName + "&nbsp;</td>";
        sTxt = sTxt + "</tr>";
        //sTxt = sTxt + "<tr>";
        //sTxt = sTxt + "<td><strong>&nbsp;Authentication Ticket</strong></td>";
        //sTxt = sTxt + "<td>&nbsp;" + cAuthenticationNumber + "&nbsp;</td>";
        //sTxt = sTxt + "</tr>";
        sTxt = sTxt + "</table>";

        sb.Append("<tr><td>");
        sb.Append("<p>");
        sb.Append("Sent: " + DateTime.Now.ToLongDateString() + "<br>From: " + cfromName + "<br>");
        sb.Append("To: " + cVendorName + "<br><br>");
        sb.Append("</p>");
        sb.Append("<tr><td>");
        sb.Append("<p>");
        sb.Append("Dear " + cVendorName + ":<br><br>");
        sb.Append("Be advised that your application to be an Accredited Trans-Asia Vendor was evaluated and found to have not met the requirements, we regret that you will not be accredited at this time.<br><br>");
        sb.Append("If you so wish, we encourage you to schedule a meeting with us to discuss the result of your application for accreditation.<br><br>");
        sb.Append("We would like to thank you for the time afforded in completing and submitting your application.  We will keep your details on record for consideration of future business opportunities.<br><br>");
        //sb.Append("Please access the link below using your username and password to start your application for Trans-Asia accreditation. <br><br>");
        //sb.Append("<a href='http://'<br><br>");
        sb.Append(sTxt);
        sb.Append("</p>");
        sb.Append("<br><br><br>");
        sb.Append("Sincerely,<br><br>");
        sb.Append("Trans-Asia<br><br>");
        sb.Append("</td></tr>");
        sb.Append("<tr><td>");
        sb.Append("<p>&nbsp;</p><span style='font-size:10px; font-style:italic;'>Please do not reply to this auto-generated  message.&nbsp;</span>");
        sb.Append("</td></tr>");

        //sb.Append("<tr><td><p>Sent: " + DateTime.Now.ToLongDateString() + "<br>From: " + cfromName + "<br> To: " + ctoName + "<br><br> Good day!<br><br> This is to inform you that application for vendor accreditation has been rejected.<br></p><br>" + sTxt + "<p>Very truly yours,<br><br><br> <strong>" + cfromName + "</strong></p><p>&nbsp;</p> <span style='font-size:10px; font-style:italic;'>Please do not reply to this auto-generated  message.&nbsp;</span></td></tr>");
        //Response.Write(sb.ToString());
        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }





    //############################################################
    //############################################################
    // SEND EMAIL NOTIFICATION TO VM OFFICER FOR IMPORT
    private bool SendEmailNotificationForImport(string sfromName, string sfromEmail, string stoName, string stoEmail, string sAuthenticationTicket, string sVendorName)
    {
        bool success = false;

        string from = sfromName + "<" + sfromEmail + ">";
        string to = stoName + "<" + stoEmail + ">";
        string subject = "";

        try
        {
            //subject = "Clarification - Vendor Accreditation Approval -- " + sVendorName;
            subject = "Vendor Accreditation: For Import <" + sVendorName + ">";
            if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                    from,
                    to,
                    subject,
                    CreateNotificationBodyForImport(sfromName, stoName, sAuthenticationTicket, sVendorName),
                    MailTemplate.GetTemplateLinkedResources(this)))
            {	//if sending failed					
                LogHelper.EventLogHelper.Log("Bid > Send Notification : Sending Failed to " + from, System.Diagnostics.EventLogEntryType.Error);
            }
            else
            {	//if sending successful
                LogHelper.EventLogHelper.Log("Bid > Send Notification : Email Sent to " + from, System.Diagnostics.EventLogEntryType.Information);
            }
            success = true;
        }
        catch (Exception ex)
        {
            success = false;
            LogHelper.EventLogHelper.Log("Bid > Send Notification : " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            //Response.Write(ex.ToString());
        }
        return success;
    }

    private string CreateNotificationBodyForImport(string cfromName, string ctoName, string cAuthenticationNumber, string cVendorName)
    {
        SqlDataReader oReader;
        string cServices = "";
        query = "SELECT t1.*, t2.CategoryName FROM tblVendorProductsAndServices t1, rfcProductCategory t2 WHERE t2.CategoryId = t1.CategoryId AND t1.VendorId = @VendorId";
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
                        cServices = cServices + "&bull; " + oReader["CategoryName"].ToString() + "<br>";
                    }
                }
            }
        }
        conn.Close();
        StringBuilder sb = new StringBuilder();
        string sTxt = "<table border='1' style='font-size:12px'>";
        sTxt = sTxt + "<tr>";
        sTxt = sTxt + "<td><strong>&nbsp;Vendor ID</strong></td>";
        sTxt = sTxt + "<td>&nbsp;" + Session["VendorId"] + "&nbsp;</td>";
        sTxt = sTxt + "</tr>";
        sTxt = sTxt + "<tr>";
        sTxt = sTxt + "<td><strong>&nbsp;Company Name</strong></td>";
        sTxt = sTxt + "<td>&nbsp;" + cVendorName + "&nbsp;</td>";
        sTxt = sTxt + "</tr>";
        sTxt = sTxt + "<tr>";
        sTxt = sTxt + "<td><strong>&nbsp;Category</strong></td>";
        sTxt = sTxt + "<td>&nbsp;" + cServices + "&nbsp;</td>";
        sTxt = sTxt + "</tr>";
        sTxt = sTxt + "</table>";


        sb.Append("<tr><td>");
        sb.Append("<p>");
        sb.Append("Sent: " + DateTime.Now.ToLongDateString() + "<br>From: " + cfromName + "<br>");
        sb.Append("To: " + ctoName + "<br><br>");
        sb.Append("</p>");
        sb.Append("<tr><td>");
        sb.Append("<p>");
        sb.Append("Dear " + ctoName + ":<br><br>");
        sb.Append("Re: For PO - Vendor Accreditation For Import -- " + cVendorName + "<br><br>");
        sb.Append("This is to request the generation of import file for the ff approved vendor: <br><br>");
        //sb.Append("<a href='http://'<br><br>");
        sb.Append(sTxt);
        sb.Append("</p><br><br>");
        //sb.Append("We are happy to be doing business with you. Thank you and God bless your dealings.<br><br><br>");
        sb.Append("Very truly yours,<br><br>");
        sb.Append("Trans-Asia<br><br>");
        sb.Append("</td></tr>");
        sb.Append("<tr><td>");
        sb.Append("<p>&nbsp;</p>");
        sb.Append("<b>Instructions:</b><br>");
        sb.Append("&nbsp;&nbsp;1. Go to <a href='" + System.Configuration.ConfigurationManager.AppSettings["ServerUrl"] + "' target='_blank'>" + System.Configuration.ConfigurationManager.AppSettings["ServerUrl"] + "</a><br>");
        sb.Append("&nbsp;&nbsp;2. Enter your Username and Password then click Login<br>");
        sb.Append("&nbsp;&nbsp;3. Click Vendors for Generation of Vendor Import file<br>");
        sb.Append("&nbsp;&nbsp;4. Click Send<br>");
        sb.Append("</td></tr>");
        sb.Append("<tr><td>");
        sb.Append("<p>&nbsp;</p><span style='font-size:10px; font-style:italic;'>Please do not reply to this auto-generated  message.&nbsp;</span>");
        sb.Append("</td></tr>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }



    //############################################################
    //############################################################
    // SEND EMAIL NOTIFICATION TO VM RECO FOR CLARIFY
    private bool SendEmailNotificationClarify(string sfromName, string sfromEmail, string stoName, string stoEmail, string sAuthenticationTicket, string sVendorName)
    {
        bool success = false;

        string from = sfromName + "<" + sfromEmail + ">";
        string to = stoName + "<" + stoEmail + ">";
        string subject = "";

        try
        {
            //subject = "Clarification - Vendor Accreditation Approval -- " + sVendorName;
            subject = "Vendor Accreditation: For Clarification <" + sVendorName + ">";
            if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                    from,
                    to,
                    subject,
                    CreateNotificationBodyClarify(sfromName, stoName, sAuthenticationTicket, sVendorName),
                    MailTemplate.GetTemplateLinkedResources(this)))
            {	//if sending failed					
                LogHelper.EventLogHelper.Log("Bid > Send Notification : Sending Failed to " + from, System.Diagnostics.EventLogEntryType.Error);
            }
            else
            {	//if sending successful
                LogHelper.EventLogHelper.Log("Bid > Send Notification : Email Sent to " + from, System.Diagnostics.EventLogEntryType.Information);
            }
            success = true;
        }
        catch (Exception ex)
        {
            success = false;
            LogHelper.EventLogHelper.Log("Bid > Send Notification : " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            //Response.Write(ex.ToString());
        }
        return success;
    }

    private string CreateNotificationBodyClarify(string cfromName, string ctoName, string cAuthenticationNumber, string cVendorName)
    {
        SqlDataReader oReader;
        string cServices = "";
        query = "SELECT t1.*, t2.CategoryName FROM tblVendorProductsAndServices t1, rfcProductCategory t2 WHERE t2.CategoryId = t1.CategoryId AND t1.VendorId = @VendorId";
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
                        cServices = cServices + "&bull; " + oReader["CategoryName"].ToString() + "<br>";
                    }
                }
            }
        }
        conn.Close();
        StringBuilder sb = new StringBuilder();
        string sTxt = "<table border='1' style='font-size:12px'>";
        sTxt = sTxt + "<tr>";
        sTxt = sTxt + "<td><strong>&nbsp;Vendor ID</strong></td>";
        sTxt = sTxt + "<td>&nbsp;" + Session["VendorId"] + "&nbsp;</td>";
        sTxt = sTxt + "</tr>";
        sTxt = sTxt + "<tr>";
        sTxt = sTxt + "<td><strong>&nbsp;Company Name</strong></td>";
        sTxt = sTxt + "<td>&nbsp;" + cVendorName + "&nbsp;</td>";
        sTxt = sTxt + "</tr>";
        sTxt = sTxt + "<tr>";
        sTxt = sTxt + "<td><strong>&nbsp;Category</strong></td>";
        sTxt = sTxt + "<td>&nbsp;" + cServices + "&nbsp;</td>";
        sTxt = sTxt + "</tr>";
        sTxt = sTxt + "</table>";

        //sb.Append("<tr><td><p>Sent: " + DateTime.Now.ToLongDateString() + "<br>From: " + cfromName + "<br> To: " + ctoName + "<br><br> Good day!<br><br> This is to inform you that application for vendor accreditation has been endorsed for clarification.<br></p><br>" + sTxt + "<p>Very truly yours,<br><br><br> <strong>" + cfromName + "</strong></p><p>&nbsp;</p> <span style='font-size:10px; font-style:italic;'>Please do not reply to this auto-generated  message.&nbsp;</span></td></tr>");

        sb.Append("<tr><td>");
        sb.Append("<p>");
        sb.Append("Sent: " + DateTime.Now.ToLongDateString() + "<br>From: " + cfromName + "<br>");
        sb.Append("To: " + ctoName + "<br><br>");
        sb.Append("</p>");
        sb.Append("<tr><td>");
        sb.Append("<p>");
        sb.Append("Dear " + ctoName + ":<br><br>");
        sb.Append("Re: For Clarification - Vendor Accreditation Approval -- " + cVendorName + "<br><br>");
        sb.Append("This is to request for your clarification of the ff: <br><br>");
        //sb.Append("<a href='http://'<br><br>");
        sb.Append(sTxt);
        sb.Append("</p><br><br>");
        //sb.Append("We are happy to be doing business with you. Thank you and God bless your dealings.<br><br><br>");
        sb.Append("Very truly yours,<br><br>");
        sb.Append("Trans-Asia<br><br>");
        sb.Append("</td></tr>");
        sb.Append("<tr><td>");
        sb.Append("<p>&nbsp;</p>");
        sb.Append("<b>Instructions:</b><br>");
        sb.Append("&nbsp;&nbsp;1. Go to <a href='" + System.Configuration.ConfigurationManager.AppSettings["ServerUrl"] + "' target='_blank'>" + System.Configuration.ConfigurationManager.AppSettings["ServerUrl"] + "</a><br>");
        sb.Append("&nbsp;&nbsp;2. Enter your Username and Password then click Login<br>");
        sb.Append("&nbsp;&nbsp;3. Click Vendors for Clarifications<br>");
        sb.Append("&nbsp;&nbsp;4. Click View<br>");
        sb.Append("</td></tr>");
        sb.Append("<tr><td>");
        sb.Append("<p>&nbsp;</p><span style='font-size:10px; font-style:italic;'>Please do not reply to this auto-generated  message.&nbsp;</span>");
        sb.Append("</td></tr>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }

}