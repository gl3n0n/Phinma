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

public partial class vendor_01_vendorInfo : System.Web.UI.Page
{
    SqlDataReader oReader;
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
    string query;
    SqlCommand cmd;
    SqlConnection conn;
    int numRowsTbl;
    public int VendorId;
    public string queryString;
    public bool success;

    protected void TestShowAllSessions()
    {  //test show all session
        string str = null;
        foreach (string key in HttpContext.Current.Session.Keys)
        { str += string.Format("<b>{0}</b>: {1};  ", key, HttpContext.Current.Session[key].ToString()); }
        Response.Write("<span style='font-size:12px'>" + str + "</span>");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        success = false;
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
            if (IsPostBack) {
                string alertos = "";
                if (
                    regBldgUnit.Value.ToString().Trim() == "" &&
                    regBldgBldg.Value.ToString().Trim() == "" &&
                    regBldgLotNo.Value.ToString().Trim() == "" &&
                    regBldgBlock.Value.ToString().Trim() == "" &&
                    regBldgPhase.Value.ToString().Trim() == "" &&
                    regBldgHouseNo.Value.ToString().Trim() == "" &&
                    regBldgStreet.Value.ToString().Trim() == "" &&
                    regBldgSubd.Value.ToString().Trim() == "" &&
                    regBrgy.Value.ToString().Trim() == "" &&
                    regCity.Value.ToString().Trim() == "" &&
                    regProvince.Value.ToString().Trim() == "" &&
                    regCountry.Value.ToString().Trim() == "" &&
                    regPostal.Value.ToString().Trim() == ""
                    )
                {
                    SaveToDB();
                    success = false;
                    alertos = alertos + "Complete address is required.\n";
                }
                else if (
                    conBidName.Value.ToString().Trim() == "" || 
                    conBidPosition.Value.ToString().Trim() == "" || 
                    conBidEmail.Value.ToString().Trim() == "" || 
                    conBidMobile.Value.ToString().Trim() == "" || 
                    conBidTelNo.Value.ToString().Trim() == "" || 
                    conSalName.Value.ToString().Trim() == "" || 
                    conSalPosition.Value.ToString().Trim() == "" || 
                    conSalEmail.Value.ToString().Trim() == "" || 
                    conSalMobile.Value.ToString().Trim() == "" || 
                    conSalTelNo.Value.ToString().Trim() == ""
                    )
                {
                    SaveToDB();
                    success = false;
                    alertos = alertos + "CEO & Sales/Marketing Representative details are required.\n";
                }
                else
                {
                    SaveToDB();
                    success = true;
                    string control1 = Request.Form["__EVENTTARGET"];
                    if (control1 == "continueStp")
                    {
                        Response.Redirect("vendor_02_productServices.aspx");
                        Response.Write("asdfasdfasf");
                    }
                }
                if (!success)
                {
                    Response.Write(@"<script>alert('" + alertos.Trim() + "');</script>");
                }
            }
            PopulateFields();
            repeaterBranches.DataBind();
            repeaterSubsidiary.DataBind();
            repeaterBranches_Lbl.Visible=false;
            repeaterSubsidiary_Lbl.Visible = false;
            tbl01_Lbl.Visible = false;
            tbl02_Lbl.Visible = false;
            tbl03_Lbl.Visible = false;
            tbl04_Lbl.Visible = false;
        }
        else //otherwise viewing only
        {
            PopulateFieldsLbl();
            repeaterSubsidiary_Lbl.DataBind();
            repeaterBranches_Lbl.DataBind();
            repeaterBranches.Visible=false;
            repeaterSubsidiary.Visible = false;
            tbl01.Visible = false;
            tbl02.Visible = false;
            tbl03.Visible = false;
            tbl04.Visible = false;
            add5.Visible = add6.Visible = false;
            createBt.Visible = createBt1.Visible = false;
            //(HtmlInput)VendorBranchesCounter.Visible = false;
        }
        //dstblVendorSubsidiary.SelectParameters.Add("
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
        CompanyName.Disabled = true;

        query = "SELECT * FROM tblVendor WHERE VendorId=@VendorId";
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
                        CompanyName.Value = oReader["CompanyName"].ToString();
                    }
                }
            }
        }  

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
                        //CompanyName.Value = oReader["CompanyName"].ToString();
                        regBldgUnit.Value = oReader["regBldgUnit"].ToString();
                        regBldgBldg.Value = oReader["regBldgBldg"].ToString();
                        regBldgLotNo.Value = oReader["regBldgLotNo"].ToString();
                        regBldgBlock.Value = oReader["regBldgBlock"].ToString();
                        regBldgPhase.Value = oReader["regBldgPhase"].ToString();
                        regBldgHouseNo.Value = oReader["regBldgHouseNo"].ToString();
                        regBldgStreet.Value = oReader["regBldgStreet"].ToString();
                        regBldgSubd.Value = oReader["regBldgSubd"].ToString();
                        regBrgy.Value = oReader["regBrgy"].ToString();
                        regCity.Value = oReader["regCity"].ToString();
                        regProvince.Value = oReader["regProvince"].ToString();
                        regCountry.Value = oReader["regCountry"].ToString();
                        regPostal.Value = oReader["regPostal"].ToString();
                        regArea.Value = oReader["regArea"].ToString();
                        regOwned.SelectedValue = oReader["regOwned"].ToString();
                        if (oReader["regOwnedAttachment"].ToString() != "")
                        {
                            string[] regOwnedAttachments1 = oReader["regOwnedAttachment"].ToString().Split(';');
                            foreach (string regOwnedAttachment1 in regOwnedAttachments1)
                            {
                                regOwnedAttachmentLbl.Text = regOwnedAttachment1.Trim() != "" ? regOwnedAttachmentLbl.Text + "<div><a href='" + regOwnedAttachment1.Trim() + "' target='_blank'>Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"regOwnedAttachmentx\" onclick=\"$(this).parent(\'div\').html(\'\');FileattchValues($(\'#ContentPlaceHolder1_regOwnedAttachment\').val(),\'" + regOwnedAttachment1.Trim() + "\');\" /><br></div>" : "";
                            }
                        }
                        else
                        {
                            regOwnedAttachmentLbl.Text = "Attach file<br>";
                        }
                        
                        regOwnedAttachment.Value = oReader["regOwnedAttachment"].ToString();
                        conBidName.Value = oReader["conBidName"].ToString();
                        conBidPosition.Value = oReader["conBidPosition"].ToString();
                        conBidEmail.Value = oReader["conBidEmail"].ToString();
                        conBidMobile.Value = oReader["conBidMobile"].ToString();
                        conBidTelNo.Value = oReader["conBidTelNo"].ToString();
                        conBidFaxNo.Value = oReader["conBidFaxNo"].ToString();
                        conSalName.Value = oReader["conSalName"].ToString();
                        conSalPosition.Value = oReader["conSalPosition"].ToString();
                        conSalEmail.Value = oReader["conSalEmail"].ToString();
                        conSalMobile.Value = oReader["conSalMobile"].ToString();
                        conSalMobile.Value = oReader["conSalMobile"].ToString();
                        conSalTelNo.Value = oReader["conSalTelNo"].ToString();
                        conSalFaxNo.Value = oReader["conSalFaxNo"].ToString();
                        conTecName.Value = oReader["conTecName"].ToString();
                        conTecPosition.Value = oReader["conTecPosition"].ToString();
                        conTecEmail.Value = oReader["conTecEmail"].ToString();
                        conTecMobile.Value = oReader["conTecMobile"].ToString();
                        conTecTelNo.Value = oReader["conTecTelNo"].ToString();
                        conTecFaxNo.Value = oReader["conTecFaxNo"].ToString();
                        conLegName.Value = oReader["conLegName"].ToString();
                        conLegPosition.Value = oReader["conLegPosition"].ToString();
                        conLegEmail.Value = oReader["conLegEmail"].ToString();
                        conLegMobile.Value = oReader["conLegMobile"].ToString();
                        conLegTelNo.Value = oReader["conLegTelNo"].ToString();
                        conLegFaxNo.Value = oReader["conLegFaxNo"].ToString();
                        conBonName.Value = oReader["conBonName"].ToString();
                        conBonPosition.Value = oReader["conBonPosition"].ToString();
                        conBonEmail.Value = oReader["conBonEmail"].ToString();
                        conBonMobile.Value = oReader["conBonMobile"].ToString();
                        conBonTelNo.Value = oReader["conBonTelNo"].ToString();
                        conBonFaxNo.Value = oReader["conBonFaxNo"].ToString();
                        parentCompanyName.Value = oReader["parentCompanyName"].ToString();
                        parentCompanyAddr.Value = oReader["parentCompanyAddr"].ToString();
                        repOfcCompanyName.Value = oReader["repOfcCompanyName"].ToString();
                        repOfcCompanyAddr.Value = oReader["repOfcCompanyAddr"].ToString();
                        repOfcEmail.Value = oReader["repOfcEmail"].ToString();
                        repOfcTelNo.Value = oReader["repOfcTelNo"].ToString();
                        repOfcFaxNo.Value = oReader["repOfcFaxNo"].ToString();

                    }
                }
            }
        }        
    }


    void PopulateFieldsLbl()
    {
        //CompanyName.Value = oReader["CompanyName"].ToString();
        //CompanyName.Disabled = true;

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
                        CompanyName_Lbl.Text = oReader["CompanyName"].ToString();
                        regBldgUnit_Lbl.Text = oReader["regBldgUnit"].ToString();
                        regBldgBldg_Lbl.Text = oReader["regBldgBldg"].ToString();
                        regBldgLotNo_Lbl.Text = oReader["regBldgLotNo"].ToString();
                        regBldgBlock_Lbl.Text = oReader["regBldgBlock"].ToString();
                        regBldgPhase_Lbl.Text = oReader["regBldgPhase"].ToString();
                        regBldgHouseNo_Lbl.Text = oReader["regBldgHouseNo"].ToString();
                        regBldgStreet_Lbl.Text = oReader["regBldgStreet"].ToString();
                        regBldgSubd_Lbl.Text = oReader["regBldgSubd"].ToString();
                        regBrgy_Lbl.Text = oReader["regBrgy"].ToString();
                        regCity_Lbl.Text = oReader["regCity"].ToString();
                        regProvince_Lbl.Text = oReader["regProvince"].ToString();
                        regCountry_Lbl.Text = oReader["regCountry"].ToString();
                        regPostal_Lbl.Text = oReader["regPostal"].ToString();
                        regArea_Lbl.Text = oReader["regArea"].ToString();
                        regOwned_Lbl.Text = oReader["regOwned"].ToString();
                        //regOwnedAttachment_Lbl.Text = oReader["regOwnedAttachment"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <div style='float:left;'>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div>  No attached file";
                        if (oReader["regOwnedAttachment"].ToString() != "")
                        {
                            string[] regOwnedAttachments1 = oReader["regOwnedAttachment"].ToString().Split(';');
                            foreach (string regOwnedAttachment1 in regOwnedAttachments1)
                            {
                                if (regOwnedAttachment1 != "")
                                {
                                    regOwnedAttachment_Lbl.Text = regOwnedAttachment_Lbl.Text + regOwnedAttachment1.Trim() != "" ? regOwnedAttachment_Lbl.Text + "<a href='" + regOwnedAttachment1.Trim() + "' target='_blank'><img src=\"images/attachment.png\" /> Attached file</a><br>" : "";
                                }
                            }
                        }
                        regOwnedAttachment_Lbl.Text = oReader["regOwnedAttachment"].ToString() != "" ? regOwnedAttachment_Lbl.Text + "</div>" : regOwnedAttachment_Lbl.Text + "";
                        //regOwnedAttachment_Lbl.Text = oReader["regOwnedAttachment"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["regOwnedAttachment"].ToString() + "' target='_blank'>Attached file</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div>  No attached file";
                        conBidName_Lbl.Text = oReader["conBidName"].ToString();
                        conBidPosition_Lbl.Text = oReader["conBidPosition"].ToString();
                        conBidEmail_Lbl.Text = oReader["conBidEmail"].ToString();
                        conBidMobile_Lbl.Text = oReader["conBidMobile"].ToString();
                        conBidTelNo_Lbl.Text = oReader["conBidTelNo"].ToString();
                        conBidFaxNo_Lbl.Text = oReader["conBidFaxNo"].ToString();
                        conSalName_Lbl.Text = oReader["conSalName"].ToString();
                        conSalPosition_Lbl.Text = oReader["conSalPosition"].ToString();
                        conSalEmail_Lbl.Text = oReader["conSalEmail"].ToString();
                        conSalMobile_Lbl.Text = oReader["conSalMobile"].ToString();
                        conSalMobile_Lbl.Text = oReader["conSalMobile"].ToString();
                        conSalTelNo_Lbl.Text = oReader["conSalTelNo"].ToString();
                        conSalFaxNo_Lbl.Text = oReader["conSalFaxNo"].ToString();
                        conTecName_Lbl.Text = oReader["conTecName"].ToString();
                        conTecPosition_Lbl.Text = oReader["conTecPosition"].ToString();
                        conTecEmail_Lbl.Text = oReader["conTecEmail"].ToString();
                        conTecMobile_Lbl.Text = oReader["conTecMobile"].ToString();
                        conTecTelNo_Lbl.Text = oReader["conTecTelNo"].ToString();
                        conTecFaxNo_Lbl.Text = oReader["conTecFaxNo"].ToString();
                        conLegName_Lbl.Text = oReader["conLegName"].ToString();
                        conLegPosition_Lbl.Text = oReader["conLegPosition"].ToString();
                        conLegEmail_Lbl.Text = oReader["conLegEmail"].ToString();
                        conLegMobile_Lbl.Text = oReader["conLegMobile"].ToString();
                        conLegTelNo_Lbl.Text = oReader["conLegTelNo"].ToString();
                        conLegFaxNo_Lbl.Text = oReader["conLegFaxNo"].ToString();
                        conBonName_Lbl.Text = oReader["conBonName"].ToString();
                        conBonPosition_Lbl.Text = oReader["conBonPosition"].ToString();
                        conBonEmail_Lbl.Text = oReader["conBonEmail"].ToString();
                        conBonMobile_Lbl.Text = oReader["conBonMobile"].ToString();
                        conBonTelNo_Lbl.Text = oReader["conBonTelNo"].ToString();
                        conBonFaxNo_Lbl.Text = oReader["conBonFaxNo"].ToString();

                        parentCompanyName_Lbl.Text = oReader["parentCompanyName"].ToString();
                        parentCompanyAddr_Lbl.Text = oReader["parentCompanyAddr"].ToString();
                        repOfcCompanyName_Lbl.Text = oReader["repOfcCompanyName"].ToString();
                        repOfcCompanyAddr_Lbl.Text = oReader["repOfcCompanyAddr"].ToString();
                        repOfcEmail_Lbl.Text = oReader["repOfcEmail"].ToString();
                        repOfcTelNo_Lbl.Text = oReader["repOfcTelNo"].ToString();
                        repOfcFaxNo_Lbl.Text = oReader["repOfcFaxNo"].ToString();
                    }
                }
            }
        }
        
    }

    void SaveToDB()
    {
        string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
        string sCommand = "";

        query = 

            @" IF NOT EXISTS (SELECT 1 FROM tblVendorInformation WHERE VendorId = @VendorId) 
                BEGIN INSERT INTO tblVendorInformation (VendorId, VendorCode, CompanyName, regBldgUnit, regBldgBldg, regBldgLotNo, regBldgBlock, regBldgPhase, regBldgHouseNo, regBldgStreet, regBldgSubd, regBrgy, regCity, regProvince, regCountry, regPostal, regArea, regOwned, regOwnedAttachment, conBidName, conBidPosition, conBidEmail, conBidMobile, conBidTelNo, conBidFaxNo, conLegName, conLegPosition, conLegEmail, conLegMobile, conLegTelNo, conLegFaxNo, conSalName, conSalPosition, conSalEmail, conSalMobile, conSalTelNo, conSalFaxNo, conTecName, conTecPosition, conTecEmail, conTecMobile, conTecTelNo, conTecFaxNo, conBonName, conBonPosition, conBonEmail, conBonMobile, conBonTelNo, conBonFaxNo, parentCompanyName, parentCompanyAddr, repOfcCompanyName, repOfcCompanyAddr, repOfcEmail, repOfcTelNo, repOfcFaxNo) VALUES (@VendorId, @VendorCode, @CompanyName, @regBldgUnit, @regBldgBldg, @regBldgLotNo, @regBldgBlock, @regBldgPhase, @regBldgHouseNo, @regBldgStreet, @regBldgSubd, @regBrgy, @regCity, @regProvince, @regCountry, @regPostal, @regArea, @regOwned, @regOwnedAttachment, @conBidName, @conBidPosition, @conBidEmail, @conBidMobile, @conBidTelNo, @conBidFaxNo, @conLegName, @conLegPosition, @conLegEmail, @conLegMobile, @conLegTelNo, @conLegFaxNo, @conSalName, @conSalPosition, @conSalEmail, @conSalMobile, @conSalTelNo, @conSalFaxNo, @conTecName, @conTecPosition, @conTecEmail, @conTecMobile, @conTecTelNo, @conTecFaxNo, @conBonName, @conBonPosition, @conBonEmail, @conBonMobile, @conBonTelNo, @conBonFaxNo,  @parentCompanyName, @parentCompanyAddr, @repOfcCompanyName, @repOfcCompanyAddr, @repOfcEmail, @repOfcTelNo, @repOfcFaxNo) END ELSE BEGIN UPDATE tblVendorInformation SET VendorCode=@VendorCode, CompanyName=@CompanyName, regBldgUnit=@regBldgUnit, regBldgBldg=@regBldgBldg, regBldgLotNo=@regBldgLotNo, regBldgBlock=@regBldgBlock, regBldgPhase=@regBldgPhase, regBldgHouseNo=@regBldgHouseNo, regBldgStreet=@regBldgStreet, regBldgSubd=@regBldgSubd, regBrgy=@regBrgy, regCity=@regCity, regProvince=@regProvince, regCountry=@regCountry, regPostal=@regPostal, regArea=@regArea, regOwned=@regOwned, regOwnedAttachment=@regOwnedAttachment, conBidName=@conBidName, conBidPosition=@conBidPosition, conBidEmail=@conBidEmail, conBidMobile=@conBidMobile, conBidTelNo=@conBidTelNo, conBidFaxNo=@conBidFaxNo, conLegName=@conLegName, conLegPosition=@conLegPosition, conLegEmail=@conLegEmail, conLegMobile=@conLegMobile, conLegTelNo=@conLegTelNo, conLegFaxNo=@conLegFaxNo, conSalName=@conSalName, conSalPosition=@conSalPosition, conSalEmail=@conSalEmail, conSalMobile=@conSalMobile, conSalTelNo=@conSalTelNo, conSalFaxNo=@conSalFaxNo, conTecName=@conTecName, conTecPosition=@conTecPosition, conTecEmail=@conTecEmail, conTecMobile=@conTecMobile, conTecTelNo=@conTecTelNo, conTecFaxNo=@conTecFaxNo, conBonName=@conBonName, conBonPosition=@conBonPosition, conBonEmail=@conBonEmail, conBonMobile=@conBonMobile, conBonTelNo=@conBonTelNo, conBonFaxNo=@conBonFaxNo, parentCompanyName=@parentCompanyName, parentCompanyAddr=@parentCompanyAddr, repOfcCompanyName=@repOfcCompanyName, repOfcCompanyAddr=@repOfcCompanyAddr, repOfcEmail=@repOfcEmail, repOfcTelNo=@repOfcTelNo, repOfcFaxNo=@repOfcFaxNo WHERE VendorId=@VendorId END";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@VendorCode", "");
                cmd.Parameters.AddWithValue("@CompanyName", CompanyName.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@regBldgUnit", regBldgUnit.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@regBldgBldg", regBldgBldg.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@regBldgLotNo", regBldgLotNo.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@regBldgBlock", regBldgBlock.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@regBldgPhase", regBldgPhase.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@regBldgHouseNo", regBldgHouseNo.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@regBldgStreet", regBldgStreet.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@regBldgSubd", regBldgSubd.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@regBrgy", regBrgy.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@regCity", regCity.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@regProvince", regProvince.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@regCountry", regCountry.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@regPostal", regPostal.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@regArea", regArea.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@regOwned", regOwned.SelectedValue.ToString().Trim());
                cmd.Parameters.AddWithValue("@regOwnedAttachment", regOwnedAttachment.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conBidName", conBidName.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conBidPosition", conBidPosition.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conBidEmail", conBidEmail.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conBidMobile", conBidMobile.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conBidTelNo", conBidTelNo.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conBidFaxNo", conBidFaxNo.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conLegName", conLegName.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conLegPosition", conLegPosition.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conLegEmail", conLegEmail.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conLegMobile", conLegMobile.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conLegTelNo", conLegTelNo.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conLegFaxNo", conLegFaxNo.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conSalName", conSalName.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conSalPosition", conSalPosition.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conSalEmail", conSalEmail.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conSalMobile", conSalMobile.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conSalTelNo", conSalTelNo.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conSalFaxNo", conSalFaxNo.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conTecName", conTecName.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conTecPosition", conTecPosition.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conTecEmail", conTecEmail.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conTecMobile", conTecMobile.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conTecTelNo", conTecTelNo.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conTecFaxNo", conTecFaxNo.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conBonName", conBonName.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conBonPosition", conBonPosition.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conBonEmail", conBonEmail.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conBonMobile", conBonMobile.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conBonTelNo", conBonTelNo.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conBonFaxNo", conBonFaxNo.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@parentCompanyName", parentCompanyName.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@parentCompanyAddr", parentCompanyAddr.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@repOfcCompanyName", repOfcCompanyName.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@repOfcCompanyAddr", repOfcCompanyAddr.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@repOfcEmail", repOfcEmail.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@repOfcTelNo", repOfcTelNo.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@repOfcFaxNo", repOfcFaxNo.Value.ToString().Trim());
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }
        


        //CLEAR tblVendorBranches FROM USER
        sCommand = "DELETE FROM tblVendorBranches WHERE VendorId = " + VendorId.ToString();        
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        numRowsTbl = Convert.ToInt32(Request.Form["VendorBranchesCounter"].ToString());
        for (int i = 1; i <= numRowsTbl; i++)
        {
            query = "INSERT INTO tblVendorBranches (VendorId, brAddr, brUsedAs, brEmplNo, brArea, brOwned) VALUES (@VendorId, @brAddr, @brUsedAs, @brEmplNo, @brArea, @brOwned)";
            //query = "sp_GetVendorInformation"; //##storedProcedure
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                    cmd.Parameters.AddWithValue("@VendorId", VendorId);
                    cmd.Parameters.AddWithValue("@brAddr", Request.Form["brAddr" + i].ToString());
                    cmd.Parameters.AddWithValue("@brUsedAs", Request.Form["brUsedAs" + i].ToString());
                    cmd.Parameters.AddWithValue("@brEmplNo", Request.Form["brEmplNo" + i].ToString());
                    cmd.Parameters.AddWithValue("@brArea", Request.Form["brArea" + i].ToString());
                    cmd.Parameters.AddWithValue("@brOwned", Request.Form["brOwned" + i].ToString());
                    conn.Open(); cmd.ExecuteNonQuery();
                }
            }
        }




        //CLEAR tblVendorBranches FROM USER
        sCommand = "DELETE FROM tblVendorSubsidiaries WHERE VendorId = " + VendorId.ToString();
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        numRowsTbl = Convert.ToInt32(Request.Form["SubsidiaryCounter"].ToString());
        for (int i = 1; i <= numRowsTbl; i++)
        {
            query = "INSERT INTO tblVendorSubsidiaries (VendorId, subCompanyName, subAddr, SubContact, SubFax, SubEmailAdd, subOwned) VALUES (@VendorId, @subCompanyName, @subAddr,@SubContact, @SubFax, @SubEmailAdd, @subOwned)";
            //query = "sp_GetVendorInformation"; //##storedProcedure
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                    cmd.Parameters.AddWithValue("@VendorId", VendorId);
                    cmd.Parameters.AddWithValue("@subCompanyName", Request.Form["subCompanyName" + i].ToString());
                    cmd.Parameters.AddWithValue("@subAddr", Request.Form["subAddr" + i].ToString());
                    cmd.Parameters.AddWithValue("@SubContact", Request.Form["SubContact" + i].ToString());
                    cmd.Parameters.AddWithValue("@SubFax", Request.Form["SubFax" + i].ToString());
                    cmd.Parameters.AddWithValue("@SubEmailAdd", Request.Form["SubEmailAdd" + i].ToString());
                    //cmd.Parameters.AddWithValue("@subEquity", Request.Form["subEquity" + i].ToString()!="" ? Convert.ToDouble(Request.Form["subEquity" + i].ToString()) : 0);
                    cmd.Parameters.AddWithValue("@subOwned", Request.Form["subOwned" + i].ToString());
                    conn.Open(); cmd.ExecuteNonQuery();
                }
            }
        }
        
        
        //foreach (RepeaterItem oRptItem in repeaterSubsidiary.Items)
        //{
        //    if (oRptItem.ItemType == ListItemType.Item || oRptItem.ItemType == ListItemType.AlternatingItem)
        //    {
        //        //HtmlInputHidden osubSubsidiaryId = (System.Web.UI.HtmlControls.HtmlInputHidden)oRptItem.FindControl("subSubsidiaryId");
        //        //HtmlInputText osubCompanyName = (System.Web.UI.HtmlControls.HtmlInputText)oRptItem.FindControl("subCompanyName");
        //        //HtmlInputText osubAddr = (System.Web.UI.HtmlControls.HtmlInputText)oRptItem.FindControl("subAddr");
        //        //HtmlInputText osubEquity = (System.Web.UI.HtmlControls.HtmlInputText)oRptItem.FindControl("subEquity");
        //        //HtmlInputText osubOwned = (System.Web.UI.HtmlControls.HtmlInputText)oRptItem.FindControl("subOwned");
        //        //sCommand = "INSERT INTO tblVendorSubsidiaries (VendorId, subCompanyName, subAddr, subEquity, subOwned) VALUES (";
        //        //sCommand = sCommand + Session["UserId"] + ", '";
        //        //sCommand = sCommand + osubCompanyName.Value.Trim().ToString() + "', '";
        //        //sCommand = sCommand + osubAddr.Value.Trim().ToString() + "', ";
        //        //sCommand = sCommand + osubEquity.Value.Trim().ToString() + ", '";
        //        //sCommand = sCommand + osubOwned.Value.Trim().ToString();
        //        //sCommand = sCommand + "')";
        //        ////Response.Write(sCommand);
        //        //SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        //    }
        //}

    }


    //#####################################################3

    protected void rptGeneral_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        e.Command.Parameters["@VendorId"].Value = VendorId;
    }
}