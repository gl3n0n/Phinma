using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Ava.lib;
using Ava.lib.constant;

public partial class vendor_02_productServices : System.Web.UI.Page
{
    SqlDataReader oReader;
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
    string query;
    SqlCommand cmd;
    SqlConnection conn;
    int numRowsTbl;
    public int VendorId;
    public string queryString;


    protected void TestShowAllSessions()
    {  //test show all session
        string str = null;
        foreach (string key in HttpContext.Current.Session.Keys)
        { str += string.Format("<b>{0}</b>: {1};  ", key, HttpContext.Current.Session[key].ToString()); }
        Response.Write("<span style='font-size:12px'>" + str + "</span>");
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        queryString = (Request.QueryString["VendorId"] != null && Request.QueryString["VendorId"] != "") ? "?VendorId=" + Request.QueryString["VendorId"] : "";
        if (Request.QueryString["VendorId"] != null && Request.QueryString["VendorId"] != "")
        {
            VendorId = Convert.ToInt32(Request.QueryString["VendorId"]);
        }
        else if (Session["VendorId"] != null && Session["VendorId"].ToString() != "")
        {
            VendorId = Convert.ToInt32(Session["VendorId"].ToString());
        }


        //TestShowAllSessions();
        if (
            Session["UserId"] == null ||
            Session["UserId"].ToString() == "" ||
            VendorId.ToString() == ""
            )
        {
            Response.Redirect("login.aspx");
        }

        if (editable())
        {
            if (IsPostBack) SaveToDB();
            PopulateFields();
            repeaterVendorNatureOfBusiness.DataBind(); repeaterVendorNatureOfBusiness_Lbl.Visible = false;
            repeaterVendorProductsAndServices.DataBind(); repeaterVendorProductsAndServices_Lbl.Visible = false;
            repeaterSupplierReferences.DataBind(); repeaterSupplierReferences_Lbl.Visible = false;
            repeaterTopCompetitors.DataBind(); repeaterTopCompetitors_Lbl.Visible = false;
            repeaterCustomerReferences.DataBind(); repeaterCustomerReferences_Lbl.Visible = false;
            prodServ_DescLineOfBusiness_Lbl.Visible = false;
        }
        else
        {
            PopulateFields_Lbl();
            repeaterVendorNatureOfBusiness.Visible = false; repeaterVendorNatureOfBusiness_Lbl.DataBind();
            repeaterVendorProductsAndServices.Visible = false; repeaterVendorProductsAndServices_Lbl.DataBind();
            repeaterSupplierReferences.Visible = false; repeaterSupplierReferences_Lbl.DataBind();
            repeaterTopCompetitors.Visible = false; repeaterTopCompetitors_Lbl.DataBind();
            repeaterCustomerReferences.Visible = false; repeaterCustomerReferences_Lbl.DataBind();
            prodServ_DescLineOfBusiness.Visible = false;
            add1.Visible = add2.Visible = add3.Visible = add4.Visible = add5.Visible = false;
            createBt.Visible = createBt1.Visible = false;
        }
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
    }

    private bool editable()
    {
        bool EditMode = false;
        query = "SELECT * FROM tblVendor WHERE VendorId =  @VendorId";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                conn.Open();
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        if (
                            (Session["SESSION_USERTYPE"].ToString() == "11" && oReader["Status"].ToString() == "0") ||
                            (Session["SESSION_USERTYPE"].ToString() == "11" && oReader["Status"].ToString() == "9")
                            )
                        {
                            EditMode = true;
                        }
                    }
                }
            }
        }

        return EditMode;
    }


    void PopulateFields()
    {
        //query = "INSERT INTO tblVendorProductsAndServices (VendorId, CategoryId, SubCategoryId, BrandId, NoYears, MajorClients) VALUES (@VendorId, @CategoryId, @SubCategoryId, @BrandId, @NoYears, @MajorClients)";
        ////query = "sp_GetVendorInformation"; //##storedProcedure
        //using (conn = new SqlConnection(connstring))
        //{
        //    using (cmd = new SqlCommand(query, conn))
        //    {
        //        //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
        //        cmd.Parameters.AddWithValue("@VendorId", VendorId);
        //        cmd.Parameters.AddWithValue("@CategoryId", Request.Form["CategoryId" + i].ToString());
        //        cmd.Parameters.AddWithValue("@SubCategoryId", Request.Form["SubCategoryId" + i] != "" ? Convert.ToInt32(Request.Form["SubCategoryId" + i].ToString().Replace(",", "")) : 0);
        //        cmd.Parameters.AddWithValue("@BrandId", Request.Form["BrandId" + i].ToString() != "" ? Convert.ToInt32(Request.Form["BrandId" + i].ToString().Replace(",", "")) : 0);
        //        cmd.Parameters.AddWithValue("@NoYears", Request.Form["NoYears" + i].ToString() != "" ? Convert.ToInt32(Request.Form["NoYears" + i].ToString().Replace(",", "")) : 0);
        //        cmd.Parameters.AddWithValue("@MajorClients", Request.Form["MajorClients" + i].ToString());
        //        //cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
        //        conn.Open(); cmd.ExecuteNonQuery();
        //    }
        //}
        //query = "IF NOT EXISTS (SELECT 1 FROM tblVendorProductsAndServices WHERE VendorId = @VendorId) BEGIN INSERT INTO tblVendorInformation (VendorId, prodServ_DescLineOfBusiness, prodServ_DAC_Attachment) VALUES (@VendorId, @prodServ_DescLineOfBusiness, @prodServ_DAC_Attachment) END ELSE BEGIN UPDATE tblVendorInformation SET prodServ_DescLineOfBusiness=@prodServ_DescLineOfBusiness, prodServ_DAC_Attachment=@prodServ_DAC_Attachment WHERE VendorId=@VendorId END";
        ////query = "sp_GetVendorInformation"; //##storedProcedure
        //using (conn = new SqlConnection(connstring))
        //{
        //    using (cmd = new SqlCommand(query, conn))
        //    {
        //        cmd.Parameters.AddWithValue("@VendorId", VendorId);
        //        cmd.Parameters.AddWithValue("@prodServ_DescLineOfBusiness", prodServ_DescLineOfBusiness.InnerText);
        //        cmd.Parameters.AddWithValue("@prodServ_DAC_Attachment", prodServ_DAC_Attachment.Value);
        //        conn.Open(); cmd.ExecuteNonQuery();
        //    }
        //}

        query = "SELECT * FROM tblVendorInformation WHERE VendorId=  @VendorId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        prodServ_DescLineOfBusiness.InnerText = oReader["prodServ_DescLineOfBusiness"].ToString();
                        prodServ_DAC_Attachment.Value = oReader["prodServ_DAC_Attachment"].ToString();
                        //prodServ_DAC_AttachmentLbl.Text = oReader["prodServ_DAC_Attachment"].ToString()!="" ? "<a href='" + oReader["prodServ_DAC_Attachment"].ToString() + "' target='_blank'>" + oReader["prodServ_DAC_Attachment"].ToString() + "</a>" : "Attach file";
                        if (oReader["prodServ_DAC_Attachment"].ToString() != "")
                        {
                            string[] prodServ_DAC_Attachments1 = oReader["prodServ_DAC_Attachment"].ToString().Split(';');
                            foreach (string prodServ_DAC_Attachment1 in prodServ_DAC_Attachments1)
                            {
                                prodServ_DAC_AttachmentLbl.Text = prodServ_DAC_Attachment1.Trim() != "" ? prodServ_DAC_AttachmentLbl.Text + "<div><a href='" + prodServ_DAC_Attachment1.Trim() + "' target='_blank'> Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"prodServ_DAC_Attachmentx\" onclick=\"$(this).parent(\'div\').html(\'\');FileattchValues($(\'#ContentPlaceHolder1_prodServ_DAC_Attachment\').val(),\'" + prodServ_DAC_Attachment1.Trim() + "\',\'prodServ_DAC_Attachment\');\" /><br></div>" : "";
                            }
                        }
                        else
                        {
                            prodServ_DAC_AttachmentLbl.Text = "Attach file<br>";
                        }
                    }
                }
            }
        }  
    }


    void PopulateFields_Lbl()
    {
        query = "SELECT * FROM tblVendorInformation WHERE VendorId=  @VendorId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        prodServ_DescLineOfBusiness_Lbl.Text = oReader["prodServ_DescLineOfBusiness"].ToString();
                        prodServ_DAC_AttachmentFile.Visible = false;
                        //prodServ_DAC_AttachmentLbl.Text = oReader["prodServ_DAC_Attachment"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["prodServ_DAC_Attachment"].ToString() + "' target='_blank'>" + oReader["prodServ_DAC_Attachment"].ToString() + "</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div>  No attached file";
                        if (oReader["prodServ_DAC_Attachment"].ToString() != "")
                        {
                            string[] prodServ_DAC_Attachments1 = oReader["prodServ_DAC_Attachment"].ToString().Split(';');
                            foreach (string prodServ_DAC_Attachment1 in prodServ_DAC_Attachments1)
                            {
                                prodServ_DAC_AttachmentLbl.Text = prodServ_DAC_Attachment1.Trim() != "" ? prodServ_DAC_AttachmentLbl.Text + "<div><a href='" + prodServ_DAC_Attachment1.Trim() + "' target='_blank'><img src=\"images/attachment.png\" /> Attached file</a> <br></div>" : "";
                            }
                        }
                        else
                        {
                            prodServ_DAC_AttachmentLbl.Text = "Attach file<br>";
                        }
                    }
                }
            }
        }  

    }



    void SaveToDB()
    {
        //Response.Write(Request.Form["NatureOfBusinessId1"].ToString()+"<br>");
        string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
        string sCommand = "";


        query = "IF NOT EXISTS (SELECT 1 FROM tblVendorInformation WHERE VendorId = @VendorId) BEGIN INSERT INTO tblVendorInformation (VendorId, prodServ_DescLineOfBusiness, prodServ_DAC_Attachment) VALUES (@VendorId, @prodServ_DescLineOfBusiness, @prodServ_DAC_Attachment) END ELSE BEGIN UPDATE tblVendorInformation SET prodServ_DescLineOfBusiness=@prodServ_DescLineOfBusiness, prodServ_DAC_Attachment=@prodServ_DAC_Attachment WHERE VendorId=@VendorId END";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@prodServ_DescLineOfBusiness", prodServ_DescLineOfBusiness.InnerText);
                cmd.Parameters.AddWithValue("@prodServ_DAC_Attachment", prodServ_DAC_Attachment.Value);
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }

        //CLEAR tblVendorNatureOfBusiness FROM USER
        sCommand = "DELETE FROM tblVendorNatureOfBusiness WHERE VendorId = " + VendorId.ToString();
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        numRowsTbl = Convert.ToInt32(Request.Form["NatureOfBusinessCounter"].ToString());
        for (int i = 1; i <= numRowsTbl; i++)
        {
            query = "INSERT INTO tblVendorNatureOfBusiness (VendorId, NatureOfBusinessId) VALUES (@VendorId, @NatureOfBusinessId)";
            //Response.Write(query + "<br>");
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@VendorId", VendorId);
                    cmd.Parameters.AddWithValue("@NatureOfBusinessId", Convert.ToInt32(Request.Form["NatureOfBusinessId" + i].ToString().Replace(",", "")));
                    conn.Open(); cmd.ExecuteNonQuery();
                }
            }
        }


        //CLEAR tblVendorProductsAndServices FROM USER
        sCommand = "DELETE FROM tblVendorProductsAndServices WHERE VendorId = " + VendorId.ToString();
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        numRowsTbl = Convert.ToInt32(Request.Form["ProductsAndServicesCounter"].ToString());
        //Response.Write(Request.Form["ProductsAndServicesCounter"].ToString());
        if (numRowsTbl > 0)
        {
            for (int i = 1; i <= numRowsTbl; i++)
            {
                try
                {
                    query = "INSERT INTO tblVendorProductsAndServices (VendorId, CategoryId, SubCategoryId, BrandId,  NoYears, BrandTxt, MajorClients, Exclusive) VALUES (@VendorId, @CategoryId, @SubCategoryId, @BrandId, @NoYears,@BrandTxt, @MajorClients, @Exclusive)";
                    using (conn = new SqlConnection(connstring))
                    {
                        using (cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@VendorId", VendorId);
                            cmd.Parameters.AddWithValue("@CategoryId", Request.Form["CategoryId" + i].ToString());
                            cmd.Parameters.AddWithValue("@SubCategoryId", Request.Form["SubCategoryId" + i] != "" ? Convert.ToInt32(Request.Form["SubCategoryId" + i].ToString().Replace(",", "")) : (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@BrandId", Request.Form["BrandId" + i].ToString() != "" ? Convert.ToInt32(Request.Form["BrandId" + i].ToString().Replace(",", "")) : (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@BrandTxt", Request.Form["BrandTxt" + i].ToString());
                            cmd.Parameters.AddWithValue("@NoYears", Request.Form["NoYears" + i].ToString() != "" ? Convert.ToInt32(Request.Form["NoYears" + i].ToString().Replace(",", "")) : (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@MajorClients", Request.Form["MajorClients" + i].ToString());
                            cmd.Parameters.AddWithValue("@Exclusive", Request.Form["NoYears" + i].ToString() != "" ? Request.Form["NoYears" + i].ToString() : (object)DBNull.Value);
                            //cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                            conn.Open(); cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception e)
                {
                    //Response.Write(e.StackTrace);
                }
            }
        }



        //CLEAR tblVendorSupplierReferences FROM USER
        sCommand = "DELETE FROM tblVendorSupplierReferences WHERE VendorId = " + VendorId.ToString();
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        numRowsTbl = Convert.ToInt32(Request.Form["SupplierReferencesCounter"].ToString());
        for (int i = 1; i <= numRowsTbl; i++)
        {
            query = "INSERT INTO tblVendorSupplierReferences (VendorId, SupplierName, ContactPerson,majaddress, ContactNo, Terms, DateCreated) VALUES (@VendorId, @SupplierName, @ContactPerson, @majaddress, @ContactNo, @Terms,  @DateCreated)";
            //query = "sp_GetVendorInformation"; //##storedProcedure
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                    cmd.Parameters.AddWithValue("@VendorId", VendorId);
                    cmd.Parameters.AddWithValue("@SupplierName", Request.Form["SupplierName" + i].ToString());
                    cmd.Parameters.AddWithValue("@ContactPerson", Request.Form["ContactPerson" + i].ToString());
                    cmd.Parameters.AddWithValue("@majaddress", Request.Form["majaddress" + i].ToString());
                    cmd.Parameters.AddWithValue("@ContactNo", Request.Form["ContactNo" + i].ToString());
                    cmd.Parameters.AddWithValue("@Terms", Request.Form["Terms" + i].ToString());
                    cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                    conn.Open(); cmd.ExecuteNonQuery();
                }
            }
        }




        //CLEAR tblVendorTopCompetitors FROM USER
        sCommand = "DELETE FROM tblVendorTopCompetitors WHERE VendorId = " + VendorId.ToString();
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        numRowsTbl = Convert.ToInt32(Request.Form["TopCompetitorsCounter"].ToString());
        for (int i = 1; i <= numRowsTbl; i++)
        {
            query = "INSERT INTO tblVendorTopCompetitors (VendorId, CompanyName, DateCreated) VALUES (@VendorId, @CompanyName, @DateCreated)";
            //query = "sp_GetVendorInformation"; //##storedProcedure
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                    cmd.Parameters.AddWithValue("@VendorId", VendorId);
                    cmd.Parameters.AddWithValue("@CompanyName", Request.Form["CompanyName" + i].ToString());
                    cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                    conn.Open(); cmd.ExecuteNonQuery();
                }
            }
        }

        

        //CLEAR tblVendorSupplierReferences FROM USER
        sCommand = "DELETE FROM tblVendorCustomerReferences WHERE VendorId = " + VendorId.ToString();
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        numRowsTbl = Convert.ToInt32(Request.Form["CustomerReferencesCounter"].ToString());
        for (int i = 1; i <= numRowsTbl; i++)
        {
            query = "INSERT INTO tblVendorCustomerReferences (VendorId, custrefCustomerName, custrefContactPerson,custrefAddress, custrefContactNo, custrefTerms, DateCreated) VALUES (@VendorId, @custrefCustomerName, @custrefContactPerson, @custrefAddress, @custrefContactNo, @custrefTerms,  @DateCreated)";
            //query = "sp_GetVendorInformation"; //##storedProcedure
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                    cmd.Parameters.AddWithValue("@VendorId", VendorId);
                    cmd.Parameters.AddWithValue("@custrefCustomerName", Request.Form["custrefCustomerName" + i].ToString());
                    cmd.Parameters.AddWithValue("@custrefContactPerson", Request.Form["custrefContactPerson" + i].ToString());
                    cmd.Parameters.AddWithValue("@custrefAddress", Request.Form["custrefAddress" + i].ToString());
                    cmd.Parameters.AddWithValue("@custrefContactNo", Request.Form["custrefContactNo" + i].ToString());
                    cmd.Parameters.AddWithValue("@custrefTerms", Request.Form["custrefTerms" + i].ToString());
                    cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                    conn.Open(); cmd.ExecuteNonQuery();
                }
            }
        }


        string control1 = Request.Form["__EVENTTARGET"];
        if (control1 == "continueStp")
        {
            Response.Redirect("vendor_03_businessOperational.aspx");
        }
    }

    //#####################################################3

    protected void rptGeneral_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        e.Command.Parameters["@VendorId"].Value = VendorId;
    }
}