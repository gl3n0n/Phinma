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

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using System.IO;
using OfficeOpenXml.Style;
using System.Drawing;

public partial class vmofficer_VendorForNotification_List : System.Web.UI.Page
{
    SqlDataReader oReader;
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
    string query;
    SqlCommand cmd;
    SqlConnection conn;
    string VendorIdstr;
    SqlCommand myCommand;
    DataTable myDataSet;
    SqlConnection myConnection;




    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null) Response.Redirect("login.aspx");
        if (Session["SESSION_USERTYPE"].ToString() != ((int)Constant.USERTYPE.VMOFFICER).ToString()) Response.Redirect("login.aspx");

        if (IsPostBack)
        {
            if (Request["__EVENTTARGET"].ToString() == "Details" && Request["__EVENTARGUMENT"].ToString() != "")
            {
                //Response.Write(Request["__EVENTARGUMENT"].ToString());
                VendorIdstr = Request["__EVENTARGUMENT"].ToString().Trim();
                string sArg = VendorIdstr.Trim();
                char[] mySeparator = new char[] { '|' };
                string[] Arr = sArg.Split(mySeparator);
                string VendorAlias = "";

                query = "SELECT CompanyName FROM tblVendor WHERE VendorId=@VendorId AND NotificationSent IS NULL";
                using (conn = new SqlConnection(connstring))
                {
                    using (cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Arr[0].ToString()));
                        conn.Open();
                        oReader = cmd.ExecuteReader();
                        if (oReader.HasRows)
                        {
                            while (oReader.Read())
                            {
                                VendorAlias = oReader["CompanyName"].ToString().Replace(" ", "").Substring(0, 4).ToLower();

                            }
                            //Response.Write(VendorAlias);
                            SendMail(Arr[0].ToString(), Arr[1].ToString());
                            query = "UPDATE tblVendor SET NotificationSent=@NotificationSent, SendToSAP_Status=@SendToSAP_Status, AccGroup=@AccGroup, VendorAlias=@VendorAlias, VendorCode=@VendorCode, PurchasingOrg=@PurchasingOrg, CountryCode=@CountryCode, Currency=@Currency  WHERE VendorId=@VendorId";
                            using (conn = new SqlConnection(connstring))
                            {
                                using (cmd = new SqlCommand(query, conn))
                                {
                                    cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Arr[0].ToString()));
                                    cmd.Parameters.AddWithValue("@NotificationSent", DateTime.Now);
                                    cmd.Parameters.AddWithValue("@SendToSAP_Status", 1);
                                    cmd.Parameters.AddWithValue("@AccGroup", Arr[2].Trim().ToString());
                                    cmd.Parameters.AddWithValue("@VendorAlias", VendorAlias);
                                    cmd.Parameters.AddWithValue("@VendorCode", Arr[3].Trim().ToString());
                                    cmd.Parameters.AddWithValue("@PurchasingOrg", Arr[4].Trim().ToString());
                                    cmd.Parameters.AddWithValue("@CountryCode", Arr[5].Trim().ToString());
                                    cmd.Parameters.AddWithValue("@Currency", Arr[6].Trim().ToString());
                                    conn.Open(); cmd.ExecuteNonQuery();
                                }
                            }
                            conn.Close();

                            if (Arr[1].ToString() == "6")
                            {
                                //fillGrid(Arr[0].ToString());
                                Session["VendorId"] = Arr[0].ToString();
                                SaveVendorDetailsToESource(Arr[0].ToString());
                                //fillGrid(Arr[0].ToString()); ExportToExcel(Arr[0].ToString());
                                //Response.Write("<script type='text/javascript'>window.location.href='vmofficer_VendorForNotification_List.aspx';</script>");
                                Response.Write("<script type='text/javascript'>window.open('exportVendorInformation.aspx?vendorid=" + Session["VendorId"].ToString() + "', '_blank', 'location=yes,height=570,width=520,scrollbars=yes,status=yes');;</script>");
                            }
                        }
                    }
                }
                conn.Close();



                GridView1.DataBind();
                //Response.Redirect("vmofficer_VendorForNotification_List.aspx");
            }
            else
            {
            }
        }
        else
        {
        }
    }










    private int SaveVendorDetailsToESource(string VendorIdx)
    {
        string connstring2 = ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString;
        SqlConnection sqlConnect = new SqlConnection(connstring2);
        SqlTransaction sqlTransact = null;
        int value = 0;

        query = "SELECT t1.*, t3.UserName, t3.UserPassword, t0.VendorCode Vendor_Code FROM tblVendor t0, tblVendorInformation t1, tblUsersForVendors t2, tblUsers t3  WHERE t2.VendorId = @VendorId AND t3.UserId = t2.UserId AND t1.VendorId = @VendorId AND t0.VendorId=@VendorId";
        string fromName = "", fromEmail = "", toName = "", toEmail = "", AuthenticationTicket = "", VendorName = "";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(VendorIdx));
                conn.Open();
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {

                        string username = oReader["Vendor_Code"].ToString();
                        string vendor_code = oReader["Vendor_Code"].ToString();
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
                        string tbVatRegNo = ""; string tbTIN = "";
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
                        tbHeadOfficeAddress1 = tbHeadOfficeAddress1.Length > 200 ? tbHeadOfficeAddress1.Substring(0, 200) : tbHeadOfficeAddress1;

                        tbHeadOfficeAddress2 = oReader["regCity"].ToString() != "" ? tbHeadOfficeAddress2 + oReader["regCity"].ToString() + ", " : tbHeadOfficeAddress2 + "";
                        tbHeadOfficeAddress2 = oReader["regProvince"].ToString() != "" ? tbHeadOfficeAddress2 + oReader["regProvince"].ToString() + ", " : tbHeadOfficeAddress2 + "";
                        tbHeadOfficeAddress2 = oReader["regCountry"].ToString() != "" ? tbHeadOfficeAddress2 + oReader["regCountry"].ToString() + ", " : tbHeadOfficeAddress2 + "";
                        tbHeadOfficeAddress2 = oReader["regPostal"].ToString() != "" ? tbHeadOfficeAddress2 + oReader["regPostal"].ToString() + " " : tbHeadOfficeAddress2 + "";
                        tbHeadOfficeAddress2 = tbHeadOfficeAddress2.Length > 200 ? tbHeadOfficeAddress2.Substring(0, 200) : tbHeadOfficeAddress2;

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
                            //Response.Write(ex.ToString());
                        }
                        finally
                        {
                            sqlConnect.Close();
                        }

                        Response.Write(vendor_code.ToString());

                        // query = "INSERT INTO tblVendorCategoriesAndSubcategories (VendorId, CategoryId, IncludesAllSubCategories, MainCategoryId) VALUES ((select MAX(VendorId) from tblVendors), @CategoryId, @IncludesAllSubCategories, @MainCategoryId)";

                        query = "UPDATE tblVendors SET VendorCode=@VendorCode, Vendor_Code=@VendorCode WHERE VendorId=@VendorId";
                        using (conn = new SqlConnection(connstring2))
                        {
                            using (cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(value.ToString()));
                                cmd.Parameters.AddWithValue("@VendorCode", vendor_code.ToString());
                                conn.Open(); cmd.ExecuteNonQuery();
                            }
                        }
                        conn.Close();

                        query = "IF NOT EXISTS (SELECT 1 FROM tblVendorCategoriesAndSubcategories WHERE VendorId=@VendorId AND CategoryId=@CategoryId) BEGIN INSERT INTO tblVendorCategoriesAndSubcategories (VendorId, CategoryId, IncludesAllSubCategories) VALUES (@VendorId, 'CAT000', 1) END";
                        using (conn = new SqlConnection(connstring2))
                        {
                            using (cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(value.ToString()));
                                cmd.Parameters.AddWithValue("@CategoryId", "CAT000");
                                cmd.Parameters.AddWithValue("@IncludesAllSubCategories", 1);
                                cmd.Parameters.AddWithValue("@MainCategoryId", "");
                                conn.Open(); cmd.ExecuteNonQuery();
                            }
                        }
                        conn.Close();
                    }
                }
            }
        }
        conn.Close();

        //ExportToExcel(VendorIdx);


        return value;
    }












    //protected void gvVendors_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName.Equals("Details"))
    //    {

    //        VendorIdstr = e.CommandArgument.ToString().Trim();
    //        //Response.Write(e.CommandArgument.ToString().Trim());
    //        //Session["PrevUrl"] = HttpContext.Current.Request.Url.AbsoluteUri;
    //        //Response.Redirect("cfo_vendorDetails_View.aspx?VendorId=" + e.CommandArgument.ToString().Trim());
    //        //Response.Write(Constant.VENDOR_STATUS_APPROVED.ToString());
    //        string sArg = e.CommandArgument.ToString().Trim();
    //        char[] mySeparator = new char[] { '|' };
    //        string[] Arr = sArg.Split(mySeparator);
    //        //Session["BuyerBidForBac"] = Arr[0].ToString();
    //        //Session["BuyerBacRefNo"] = Arr[1].ToString();

    //        query = "UPDATE tblVendor SET NotificationSent=@NotificationSent WHERE VendorId=@VendorId";
    //        //query = "sp_GetVendorInformation"; //##storedProcedure
    //        using (conn = new SqlConnection(connstring))
    //        {
    //            using (cmd = new SqlCommand(query, conn))
    //            {
    //                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
    //                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Arr[0].ToString()));
    //                cmd.Parameters.AddWithValue("@NotificationSent", DateTime.Now);
    //                conn.Open(); cmd.ExecuteNonQuery();
    //            }
    //        }

            
    //    }
    //}



    void SendMail(string VendorIdx, string Statusx)
    {

        // SEND EMAIL NOTIFICATION TO VENDOR
        query = "SELECT t3.FirstName + ' ' + t3.LastName as fromName, t3.EmailAdd as fromEmail, t1.FirstName + ' ' + t1.LastName as toName, t1.EmailAdd as toEmail, t4.CompanyName, t4.AuthenticationTicket, t1.UserName, t1.UserPassword, t4.VendorCode  FROM tblUsers t1, tblUsersForVendors t2, tblUsers t3, tblVendor t4 WHERE t1.UserId = t2.UserId AND t2.VendorId = @VendorId AND t3.UserId = @UserId AND t4.VendorId = @VendorId";
        string fromName = "", fromEmail = "", toName = "", toEmail = "", AuthenticationTicket = "", VendorName = "", Username = "", Password = "";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(VendorIdx));
                cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(Session["UserId"]));
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        //fromName = "Trans-Asia Admin";
                        //fromEmail = oReader["fromEmail"].ToString();
                        fromName = ConfigurationManager.AppSettings["AdminEmailName"].ToString();
                        fromEmail = ConfigurationManager.AppSettings["AdminNoReplyEmail"].ToString();
                        toName = oReader["toName"].ToString();
                        toEmail = oReader["toEmail"].ToString();
                        AuthenticationTicket = oReader["AuthenticationTicket"].ToString();
                        VendorName = oReader["CompanyName"].ToString();
                        Username = oReader["VendorCode"].ToString();
                        Password = EncryptionHelper.Decrypt(oReader["UserPassword"].ToString());
                        if (Statusx == "6")
                        {
                            try
                            {
                                SendEmailNotification(fromName, fromEmail, toName, toEmail, AuthenticationTicket, VendorName, VendorIdx, Username, Password);
                            }
                            catch (Exception ex)
                            {
                                Response.Write(ex.Message);
                            }
                        }
                        else
                        {
                            SendEmailNotificationReject(fromName, fromEmail, toName, toEmail, AuthenticationTicket, VendorName, VendorIdx);
                        }

                    }
                }
            }
        }
        conn.Close();
        // SEND EMAIL NOTIFICATION TO VM VENDOR ENDS
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
        sb.Append("Please click the link below and login using the provided username and password for Trans-Asia e-Sourcing System. <br><br>");
        sb.Append("<a href='http://vm2008:300/'>http://vm2008:300/</a><br><br>");
        sb.Append(sTxt);
        sb.Append("</p>");
        sb.Append("<br><br><br><br>");
        sb.Append("Very truly yours,<br><br>");
        sb.Append("<b>Marife </b><br>");
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
}