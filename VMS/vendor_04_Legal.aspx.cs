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

public partial class vendor_04_Legal : System.Web.UI.Page
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
            repeaterShareHolders.DataBind(); repeaterShareHolders_Lbl.Visible = false;
            repeaterBoardMembers.DataBind(); repeaterBoardMembers_Lbl.Visible = false;
            repeaterRegulatoryRequirements.DataBind(); repeaterRegulatoryRequirements_Lbl.Visible = false;
            tbl01_Lbl.Visible = tbl02_Lbl.Visible = tbl04_Lbl.Visible = tbl03_Lbl.Visible = false;
            tbl04a_Lbl.Visible = false;

        }
        else
        {
            PopulateFields_Lbl();
            repeaterShareHolders_Lbl.DataBind(); repeaterShareHolders.Visible = false;
            repeaterBoardMembers_Lbl.DataBind(); repeaterBoardMembers.Visible = false;
            repeaterRegulatoryRequirements_Lbl.DataBind(); repeaterRegulatoryRequirements.Visible = false;
            tbl01.Visible = tbl02.Visible = tbl03.Visible =  tbl04.Visible = false;


            add4a.Visible = add4b.Visible = add5.Visible = false;
            createBt.Visible = createBt1.Visible = false;
            tbl04a.Visible = false; 
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
        query = "SELECT * FROM tblVendorInformation WHERE VendorId = @VendorId";
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
                        legalStrucOrgType.Value = oReader["legalStrucOrgType"].ToString();
                        legalStrucDateReg.Value = oReader["legalStrucDateReg"].ToString() != "1/1/1900 12:00:00 AM" && oReader["legalStrucDateReg"].ToString() != "" ? string.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(oReader["legalStrucDateReg"].ToString())) : "";
                        legalStrucRegNo.Value = oReader["legalStrucRegNo"].ToString();
                        //fileuploaded1.Text = "<a href='" + oReader["legalStrucSECAttachement"].ToString() + "' target='_blank'>" + oReader["legalStrucSECAttachement"].ToString() + "</a>";
                        //legalStrucSECAttachement.Value = oReader["legalStrucSECAttachement"].ToString();
                        legalStrucDateStartedOp.Value = oReader["legalStrucDateStartedOp"].ToString() != "1/1/1900 12:00:00 AM" && oReader["legalStrucDateStartedOp"].ToString() != "" ? string.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(oReader["legalStrucDateStartedOp"].ToString())) : "";
                        legalStrucPrevBusName.Value = oReader["legalStrucPrevBusName"].ToString();
                        legalStrucDateChanged.Value = oReader["legalStrucDateChanged"].ToString() != "1/1/1900 12:00:00 AM" && oReader["legalStrucDateChanged"].ToString() != "" ? string.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(oReader["legalStrucDateChanged"].ToString())) : "";
                        legalStrucTIN.Value = oReader["legalStrucTIN"].ToString();

                        busPermitDateReg.Value = oReader["busPermitDateReg"].ToString() != "1/1/1900 12:00:00 AM" && oReader["busPermitDateReg"].ToString() != "" ? string.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(oReader["busPermitDateReg"].ToString())) : "";
                        busPermitNo.Value = oReader["busPermitNo"].ToString();

                        fileuploaded2.Text = oReader["busPermitAttachement"].ToString()!="" ?  "<a href='" + oReader["busPermitAttachement"].ToString() + "' target='_blank'>Attached file</a>" : "Attach file";
                        busPermitAttachement.Value = oReader["busPermitAttachement"].ToString();

                        if (oReader["busPermitAttachementOthers"].ToString() != "")
                        {
                            string[] busPermitAttachementOthers1 = oReader["busPermitAttachementOthers"].ToString().Split(';');
                            foreach (string busPermitAttachementOther1 in busPermitAttachementOthers1)
                            {
                                busPermitAttachementOthersLbl.Text = busPermitAttachementOther1.Trim() != "" ? busPermitAttachementOthersLbl.Text + "<div><a href='" + busPermitAttachementOther1.Trim() + "' target='_blank'>Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"busPermitAttachementOthersx\" onclick=\"$(this).parent(\'div\').html(\'\');FileattchValues($(\'#ContentPlaceHolder1_busPermitAttachementOthers\').val(),\'" + busPermitAttachementOther1.Trim() + "\',\'busPermitAttachementOthers\');\" /><br></div>" : "";
                            }
                        }
                        else
                        {
                            busPermitAttachementOthersLbl.Text = "Attach file<br>";
                        }
                        legalStrucCorpAttch_geninfo4Text.Value = oReader["legalStrucCorpAttch_geninfo4Text"].ToString();

                        birRegTIN.Value = oReader["birRegTIN"].ToString();

                        //fileuploaded3.Text = oReader["birRegAttachement"].ToString() != "" ? "<a href='" + oReader["birRegAttachement"].ToString() + "' target='_blank'>" + oReader["birRegAttachement"].ToString() + "</a>" : "Attach file";
                        //birRegAttachement.Value = oReader["birRegAttachement"].ToString();
                        if (oReader["birRegAttachement"].ToString() != "")
                        {
                            string[] birRegAttachements1 = oReader["birRegAttachement"].ToString().Split(';');
                            foreach (string birRegAttachement1 in birRegAttachements1)
                            {
                                birRegAttachementLbl.Text = birRegAttachement1.Trim() != "" ? birRegAttachementLbl.Text + "<div><a href='" + birRegAttachement1.Trim() + "' target='_blank'>Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"birRegAttachementx\" onclick=\"$(this).parent(\'div\').html(\'\');FileattchValues($(\'#ContentPlaceHolder1_birRegAttachement\').val(),\'" + birRegAttachement1.Trim() + "\',\'birRegAttachement\');\" /><br></div>" : "";
                            }
                        }
                        else
                        {
                            birRegAttachementLbl.Text = "Attach file<br>";
                        }

                        corpAuthorizedCapital.Value = oReader["corpAuthorizedCapital"].ToString();
                        corpSubscribedCapital.Value = oReader["corpSubscribedCapital"].ToString();
                        corpPaidUpCapital.Value = oReader["corpPaidUpCapital"].ToString();
                        corpPerValue.Value = oReader["corpPerValue"].ToString();
                        corpAsOfDate.Value = oReader["corpAsOfDate"].ToString() != "1/1/1900 12:00:00 AM" && oReader["corpAsOfDate"].ToString() != "" ? string.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(oReader["corpAsOfDate"].ToString())) : "";

                        legalStrucCorpAttch_geninfoLbl.Text = oReader["legalStrucCorpAttch_geninfo"].ToString() != "" ? "<a href='" + oReader["legalStrucCorpAttch_geninfo"].ToString() + "' target='_blank'>General Information Sheet</a>" : "General Information Sheet";
                        legalStrucCorpAttch_geninfo.Value = oReader["legalStrucCorpAttch_geninfo"].ToString();

                        legalStrucCorpAttch_geninfo2Lbl.Text = oReader["legalStrucCorpAttch_geninfo2"].ToString() != "" ? "<a href='" + oReader["legalStrucCorpAttch_geninfo2"].ToString() + "' target='_blank'>SEC Certificate</a>" : "SEC Certificate";
                        legalStrucCorpAttch_geninfo2.Value = oReader["legalStrucCorpAttch_geninfo2"].ToString();

                        legalStrucCorpAttch_geninfo3Lbl.Text = oReader["legalStrucCorpAttch_geninfo3"].ToString() != "" ? "<a href='" + oReader["legalStrucCorpAttch_geninfo3"].ToString() + "' target='_blank'>Articles of Incorporation and By Laws</a>" : "Articles of Incorporation and By Laws";
                        legalStrucCorpAttch_geninfo3.Value = oReader["legalStrucCorpAttch_geninfo3"].ToString();

                        legalStrucCorpAttch_geninfo4Lbl.Text = oReader["legalStrucCorpAttch_geninfo3"].ToString() != "" ? "<a href='" + oReader["legalStrucCorpAttch_geninfo4"].ToString() + "' target='_blank'>Others</a>" : "Others";
                        legalStrucCorpAttch_geninfo4.Value = oReader["legalStrucCorpAttch_geninfo4"].ToString();

                        legalStrucCorpAttch_IdentityAuthorizdSignaLbl.Text = oReader["legalStrucCorpAttch_IdentityAuthorizdSigna"].ToString() != "" ? "<a href='" + oReader["legalStrucCorpAttch_IdentityAuthorizdSigna"].ToString() + "' target='_blank'>Authorized signatories - Any government issued ID with picture</a>" : "Authorized signatories - Any government issued ID with picture";
                        legalStrucCorpAttch_IdentityAuthorizdSigna.Value = oReader["legalStrucCorpAttch_IdentityAuthorizdSigna"].ToString();

                        legalStrucCorpAttch_IdentitytaxcertLbl.Text = oReader["legalStrucCorpAttch_Identitytaxcert"].ToString() != "" ? "<a href='" + oReader["legalStrucCorpAttch_Identitytaxcert"].ToString() + "' target='_blank'>Company – Community Tax Certificate</a>" : "Company – Community Tax Certificate";
                        legalStrucCorpAttch_Identitytaxcert.Value = oReader["legalStrucCorpAttch_Identitytaxcert"].ToString();

                        legalStrucCorpAttch_BoardAuthorizdSignaLbl.Text = oReader["legalStrucCorpAttch_BoardAuthorizdSigna"].ToString() != "" ? "<a href='" + oReader["legalStrucCorpAttch_BoardAuthorizdSigna"].ToString() + "' target='_blank'>Board Resolution / Secretary certificate of authorized signatories</a>" : "Board Resolution / Secretary certificate of authorized signatories";
                        legalStrucCorpAttch_BoardAuthorizdSigna.Value = oReader["legalStrucCorpAttch_BoardAuthorizdSigna"].ToString();

                        legalStrucSoleAttch_DTIRegLbl.Text = oReader["legalStrucSoleAttch_DTIReg"].ToString() != "" ? "<a href='" + oReader["legalStrucSoleAttch_DTIReg"].ToString() + "' target='_blank'>DTI Registration – attach letter of certification from Proprietor stating registered / authorized capital</a>" : "DTI Registration – attach letter of certification from Proprietor stating registered / authorized capital";
                        legalStrucSoleAttch_DTIReg.Value = oReader["legalStrucSoleAttch_DTIReg"].ToString();

                        legalStrucSoleAttch_OwnersId1Lbl.Text = oReader["legalStrucSoleAttch_OwnersId1"].ToString() != "" ? "<a href='" + oReader["legalStrucSoleAttch_OwnersId1"].ToString() + "' target='_blank'>Owner’s Identification ** valid IDs include: Driver’s License, Passport or NBI Clearance</a>" : "Owner’s Identification ** valid IDs include: Driver’s License, Passport or NBI Clearance";
                        legalStrucSoleAttch_OwnersId1.Value = oReader["legalStrucSoleAttch_OwnersId1"].ToString();

                    legalStrucSoleAttch_CTCLbl.Text = oReader["legalStrucSoleAttch_CTC"].ToString() != "" ? "<a href='" + oReader["legalStrucSoleAttch_CTC"].ToString() + "' target='_blank'>Community Tax Certificate of the owner (CTC)</a>" : "Community Tax Certificate of the owner (CTC)";
                    legalStrucSoleAttch_CTC.Value = oReader["legalStrucSoleAttch_CTC"].ToString();
                    }
                }
            }
        }


    }




    void PopulateFields_Lbl()
    {
        query = "SELECT * FROM tblVendorInformation WHERE VendorId = @VendorId";
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
                        //"<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader[""].ToString() + "' target='_blank'>Attachment</a>";
                        legalStrucOrgType_Lbl.Text = oReader["legalStrucOrgType"].ToString();
                        legalStrucDateReg_Lbl.Text = oReader["legalStrucDateReg"].ToString() != "1/1/1900 12:00:00 AM" && oReader["legalStrucDateReg"].ToString() != "" ? string.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(oReader["legalStrucDateReg"].ToString())) : "";
                        legalStrucRegNo_Lbl.Text = oReader["legalStrucRegNo"].ToString();
                        //legalStrucSECAttachement_Lbl.Text = "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["legalStrucSECAttachement"].ToString() + "' target='_blank'>Attachment</a>";
                        legalStrucDateStartedOp_Lbl.Text = oReader["legalStrucDateStartedOp"].ToString() != "1/1/1900 12:00:00 AM" && oReader["legalStrucDateStartedOp"].ToString() != "" ? string.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(oReader["legalStrucDateStartedOp"].ToString())) : "";
                        legalStrucPrevBusName_Lbl.Text = oReader["legalStrucPrevBusName"].ToString();
                        legalStrucDateChanged_Lbl.Text = oReader["legalStrucDateChanged"].ToString()!="" ? string.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(oReader["legalStrucDateChanged"].ToString())) : "";
                        legalStrucTIN_Lbl.Text = oReader["legalStrucTIN"].ToString();

                        busPermitDateReg_Lbl.Text = oReader["busPermitDateReg"].ToString() != "1/1/1900 12:00:00 AM" && oReader["busPermitDateReg"].ToString() != "" ? string.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(oReader["busPermitDateReg"].ToString())) : "";
                        busPermitNo_Lbl.Text = oReader["busPermitNo"].ToString();
                        busPermitAttachement_Lbl.Text = oReader["busPermitAttachement"].ToString() != "" ? "<div style=\"float:left; width:24px;\"><img src=\"images/attachment.png\" /></div><a href='" + oReader["busPermitAttachement"].ToString() + "' target='_blank'>Attached file</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> No attached file";
                        if (oReader["busPermitAttachementOthers"].ToString() != "")
                        {
                            string[] busPermitAttachementOthers1 = oReader["busPermitAttachementOthers"].ToString().Split(';');
                            foreach (string busPermitAttachementOther1 in busPermitAttachementOthers1)
                            {
                                busPermitAttachementOthers_Lbl.Text = busPermitAttachementOther1.Trim() != "" ? busPermitAttachementOthers_Lbl.Text + "<div><img src=\"images/attachment.png\" /> <a href='" + busPermitAttachementOther1.Trim() + "' target='_blank'>Attached file</a> <br></div>" : "";
                            }
                        }
                        else
                        {
                            busPermitAttachementOthers_Lbl.Text = "No attached file<br>";
                        }
                        birRegTIN_Lbl.Text = oReader["birRegTIN"].ToString();
                        birRegTINVAT_Lbl.Text = oReader["birRegTINVAT"].ToString();
                        //birRegAttachement_Lbl.Text = oReader["birRegAttachement"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["birRegAttachement"].ToString() + "' target='_blank'>Attachment</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> No attached file";
                        if (oReader["birRegAttachement"].ToString() != "")
                        {
                            string[] birRegAttachements1 = oReader["birRegAttachement"].ToString().Split(';');
                            foreach (string birRegAttachement1 in birRegAttachements1)
                            {
                                birRegAttachement_Lbl.Text = birRegAttachement1.Trim() != "" ? birRegAttachement_Lbl.Text + "<div><img src=\"images/attachment.png\" /> <a href='" + birRegAttachement1.Trim() + "' target='_blank'>Attached file</a> <br></div>" : "";
                            }
                        }
                        else
                        {
                            birRegAttachement_Lbl.Text = "No attached file<br>";
                        }

                        corpAuthorizedCapital_Lbl.Text = oReader["corpAuthorizedCapital"].ToString();
                        corpSubscribedCapital_Lbl.Text = oReader["corpSubscribedCapital"].ToString();
                        corpPaidUpCapital_Lbl.Text = oReader["corpPaidUpCapital"].ToString();
                        //corpPerValue_Lbl.Text = oReader["corpPerValue"].ToString();
                        corpAsOfDate_Lbl.Text = oReader["corpAsOfDate"].ToString() != "1/1/1900 12:00:00 AM" && oReader["corpAsOfDate"].ToString() != "" || oReader["corpAsOfDate"].ToString() != "" ? string.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(oReader["corpAsOfDate"].ToString())) : "";

                        legalStrucCorpAttch_geninfo_Lbl.Text = oReader["legalStrucCorpAttch_geninfo"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["legalStrucCorpAttch_geninfo"].ToString() + "' target='_blank'>General Information Sheet</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> General Information Sheet";

                        legalStrucCorpAttch_geninfo2_Lbl.Text = oReader["legalStrucCorpAttch_geninfo2"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["legalStrucCorpAttch_geninfo2"].ToString() + "' target='_blank'>SEC Certificate</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> SEC Certificate";

                        legalStrucCorpAttch_geninfo3_Lbl.Text = oReader["legalStrucCorpAttch_geninfo3"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["legalStrucCorpAttch_geninfo3"].ToString() + "' target='_blank'>Articles of Incorporation and By Laws</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> Articles of Incorporation and By Laws";

                        legalStrucCorpAttch_geninfo4_Lbl.Text = oReader["legalStrucCorpAttch_geninfo4"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["legalStrucCorpAttch_geninfo4"].ToString() + "' target='_blank'>Others</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> Others";

                        legalStrucCorpAttch_geninfo4Txt_Lbl.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>" + oReader["legalStrucCorpAttch_geninfo4Text"].ToString()+"</b>";

                        legalStrucCorpAttch_IdentityAuthorizdSigna_Lbl.Text = oReader["legalStrucCorpAttch_IdentityAuthorizdSigna"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["legalStrucCorpAttch_IdentityAuthorizdSigna"].ToString() + "' target='_blank'>Authorized signatories - Any government issued ID with picture</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> Authorized signatories - Any government issued ID with picture";

                        legalStrucCorpAttch_Identitytaxcert_Lbl.Text = oReader["legalStrucCorpAttch_Identitytaxcert"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["legalStrucCorpAttch_Identitytaxcert"].ToString() + "' target='_blank'>Company – Community Tax Certificate</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> Company – Community Tax Certificate";

                        legalStrucCorpAttch_BoardAuthorizdSigna_Lbl.Text = oReader["legalStrucCorpAttch_BoardAuthorizdSigna"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["legalStrucCorpAttch_BoardAuthorizdSigna"].ToString() + "' target='_blank'>Board Resolution / Secretary certificate of authorized signatories</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> Board Resolution / Secretary certificate of authorized signatories";

                        legalStrucSoleAttch_DTIReg_Lbl.Text = oReader["legalStrucSoleAttch_DTIReg"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["legalStrucSoleAttch_DTIReg"].ToString() + "' target='_blank'>DTI Registration – attach letter of certification from Proprietor stating registered / authorized capital</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> DTI Registration – attach letter of certification from Proprietor stating registered / authorized capital";

                        legalStrucSoleAttch_OwnersId1_Lbl.Text = oReader["legalStrucSoleAttch_OwnersId1"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["legalStrucSoleAttch_OwnersId1"].ToString() + "' target='_blank'>Owner’s Identification ** valid IDs include: Driver’s License, Passport or NBI Clearance</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> Owner’s Identification ** valid IDs include: Driver’s License, Passport or NBI Clearance";

                        legalStrucSoleAttch_CTC_Lbl.Text = oReader["legalStrucSoleAttch_CTC"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["legalStrucSoleAttch_CTC"].ToString() + "' target='_blank'>Community Tax Certificate of the owner (CTC)</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> Community Tax Certificate of the owner (CTC)";
                    }
                }
            }
        }


    }


    void SaveToDB()
    {
        string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
        string sCommand = "";

        query = "UPDATE tblVendorInformation SET legalStrucOrgType=@legalStrucOrgType, legalStrucDateReg=@legalStrucDateReg, legalStrucRegNo=@legalStrucRegNo, legalStrucDateStartedOp=@legalStrucDateStartedOp, legalStrucPrevBusName=@legalStrucPrevBusName, legalStrucDateChanged=@legalStrucDateChanged, legalStrucTIN=@legalStrucTIN, busPermitDateReg=@busPermitDateReg, busPermitNo=@busPermitNo, busPermitAttachement=@busPermitAttachement, busPermitAttachementOthers=@busPermitAttachementOthers, birRegTIN=@birRegTIN, birRegAttachement=@birRegAttachement, corpAuthorizedCapital=@corpAuthorizedCapital, corpSubscribedCapital=@corpSubscribedCapital, corpPaidUpCapital=@corpPaidUpCapital, corpPerValue=@corpPerValue, corpAsOfDate=@corpAsOfDate, legalStrucCorpAttch_geninfo=@legalStrucCorpAttch_geninfo, legalStrucCorpAttch_geninfo2=@legalStrucCorpAttch_geninfo2, legalStrucCorpAttch_geninfo3=@legalStrucCorpAttch_geninfo3, legalStrucCorpAttch_geninfo4=@legalStrucCorpAttch_geninfo4, legalStrucCorpAttch_geninfo4Text=@legalStrucCorpAttch_geninfo4Text, legalStrucCorpAttch_IdentityAuthorizdSigna=@legalStrucCorpAttch_IdentityAuthorizdSigna, legalStrucCorpAttch_Identitytaxcert=@legalStrucCorpAttch_Identitytaxcert, legalStrucCorpAttch_BoardAuthorizdSigna=@legalStrucCorpAttch_BoardAuthorizdSigna, legalStrucSoleAttch_DTIReg=@legalStrucSoleAttch_DTIReg, legalStrucSoleAttch_OwnersId1=@legalStrucSoleAttch_OwnersId1, legalStrucSoleAttch_CTC=@legalStrucSoleAttch_CTC, birRegTINVAT=@birRegTINVAT  WHERE VendorId = @VendorId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@legalStrucOrgType", legalStrucOrgType.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@legalStrucDateReg", legalStrucDateReg.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@legalStrucRegNo", legalStrucRegNo.Value.Trim().ToString());
                //cmd.Parameters.AddWithValue("@legalStrucSECAttachement", legalStrucSECAttachement.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@legalStrucDateStartedOp", legalStrucDateStartedOp.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@legalStrucPrevBusName", legalStrucPrevBusName.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@legalStrucDateChanged", legalStrucDateChanged.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@legalStrucTIN", legalStrucTIN.Value.Trim().ToString());

                cmd.Parameters.AddWithValue("@busPermitDateReg", busPermitDateReg.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@busPermitNo", busPermitNo.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@busPermitAttachement", busPermitAttachement.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@busPermitAttachementOthers", busPermitAttachementOthers.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@birRegTIN", birRegTIN.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@birRegTINVAT", birRegTINVAT.Value.Trim());
                cmd.Parameters.AddWithValue("@birRegAttachement", birRegAttachement.Value.Trim().ToString());

                cmd.Parameters.AddWithValue("@corpAuthorizedCapital", corpAuthorizedCapital.Value.Trim().ToString() != "" ? Convert.ToDouble(corpAuthorizedCapital.Value.Trim().ToString()) : 0);
                cmd.Parameters.AddWithValue("@corpSubscribedCapital", corpSubscribedCapital.Value.Trim().ToString() != "" ? Convert.ToDouble(corpSubscribedCapital.Value.Trim().ToString()) : 0);
                cmd.Parameters.AddWithValue("@corpPaidUpCapital", corpPaidUpCapital.Value.Trim().ToString() != "" ? Convert.ToDouble(corpPaidUpCapital.Value.Trim().ToString()) : 0);
                cmd.Parameters.AddWithValue("@corpPerValue", corpPerValue.Value.Trim().ToString() != "" ? Convert.ToDouble(corpPerValue.Value.Trim().ToString()) : 0);
                cmd.Parameters.AddWithValue("@corpAsOfDate", corpAsOfDate.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@legalStrucCorpAttch_geninfo", legalStrucCorpAttch_geninfo.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@legalStrucCorpAttch_geninfo2", legalStrucCorpAttch_geninfo2.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@legalStrucCorpAttch_geninfo3", legalStrucCorpAttch_geninfo3.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@legalStrucCorpAttch_geninfo4", legalStrucCorpAttch_geninfo4.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@legalStrucCorpAttch_geninfo4Text", legalStrucCorpAttch_geninfo4Text.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@legalStrucCorpAttch_IdentityAuthorizdSigna", legalStrucCorpAttch_IdentityAuthorizdSigna.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@legalStrucCorpAttch_Identitytaxcert", legalStrucCorpAttch_Identitytaxcert.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@legalStrucCorpAttch_BoardAuthorizdSigna", legalStrucCorpAttch_BoardAuthorizdSigna.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@legalStrucSoleAttch_DTIReg", legalStrucSoleAttch_DTIReg.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@legalStrucSoleAttch_OwnersId1", legalStrucSoleAttch_OwnersId1.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@legalStrucSoleAttch_CTC", legalStrucSoleAttch_CTC.Value.Trim().ToString());
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }


        //CLEAR tblVendorShareHolders FROM USER
        sCommand = "DELETE FROM tblVendorShareHolders WHERE VendorId = " + Session["VendorId"];
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        numRowsTbl = Convert.ToInt32(Request.Form["ShareHoldersCounter"].ToString());
        for (int i = 1; i <= numRowsTbl; i++)
        {
            query = "INSERT INTO tblVendorShareHolders (VendorId, shShareHolderName, shNationality, shSubsribedCapital, shPaidupCapital, DateCreated) VALUES (@VendorId, @shShareHolderName, @shNationality, @shSubsribedCapital, @shPaidupCapital, @DateCreated)";
            //query = "sp_GetVendorInformation"; //##storedProcedure
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                    cmd.Parameters.AddWithValue("@VendorId", VendorId);
                    cmd.Parameters.AddWithValue("@shShareHolderName", Request.Form["shShareHolderName" + i].ToString());
                    cmd.Parameters.AddWithValue("@shNationality", Request.Form["shNationality" + i].ToString());
                    cmd.Parameters.AddWithValue("@shSubsribedCapital", Request.Form["shSubsribedCapital" + i].ToString() != "" ? Convert.ToDouble(Request.Form["shSubsribedCapital" + i].ToString()) : 0 );
                    cmd.Parameters.AddWithValue("@shPaidupCapital", Request.Form["shPaidupCapital" + i].ToString() != "" ? Convert.ToDouble(Request.Form["shPaidupCapital" + i].ToString()) : 0);
                    cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                    conn.Open(); cmd.ExecuteNonQuery();
                }
            }
        }



        //CLEAR tblVendorBoardMembers FROM USER
        sCommand = "DELETE FROM tblVendorBoardMembers WHERE VendorId = " + Session["VendorId"];
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        numRowsTbl = Convert.ToInt32(Request.Form["BoardMembersCounter"].ToString());
        for (int i = 1; i <= numRowsTbl; i++)
        {
            query = "INSERT INTO tblVendorBoardMembers (VendorId, bmMemberOfTheBoard, bmNationality, bmPostion, DateCreated) VALUES (@VendorId, @bmMemberOfTheBoard, @bmNationality, @bmPostion, @DateCreated)";
            //query = "sp_GetVendorInformation"; //##storedProcedure
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                    cmd.Parameters.AddWithValue("@VendorId", VendorId);
                    cmd.Parameters.AddWithValue("@bmMemberOfTheBoard", Request.Form["bmMemberOfTheBoard" + i].ToString());
                    cmd.Parameters.AddWithValue("@bmNationality", Request.Form["bmNationality" + i].ToString());
                    cmd.Parameters.AddWithValue("@bmPostion", Request.Form["bmPostion" + i].ToString());
                    cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                    conn.Open(); cmd.ExecuteNonQuery();
                }
            }
        }



        //CLEAR tblVendorRegulatoryRequirements FROM USER
        sCommand = "DELETE FROM tblVendorRegulatoryRequirements WHERE VendorId = " + Session["VendorId"];
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        numRowsTbl = Convert.ToInt32(Request.Form["RegulatoryRequirementsCounter"].ToString());
        for (int i = 1; i <= numRowsTbl; i++)
        {
            query = "INSERT INTO tblVendorRegulatoryRequirements (VendorId, RegulatoryRequirement, DateRegistered, PermitNo, DateCreated) VALUES (@VendorId, @RegulatoryRequirement, @DateRegistered, @PermitNo, @DateCreated)";
            //query = "sp_GetVendorInformation"; //##storedProcedure
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                    cmd.Parameters.AddWithValue("@VendorId", VendorId);
                    cmd.Parameters.AddWithValue("@RegulatoryRequirement", Request.Form["RegulatoryRequirement" + i].ToString());
                    cmd.Parameters.AddWithValue("@DateRegistered", Request.Form["DateRegistered" + i].ToString());
                    cmd.Parameters.AddWithValue("@PermitNo", Request.Form["PermitNo" + i].ToString());
                    cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                    conn.Open(); cmd.ExecuteNonQuery();
                }
            }
        }



        string control1 = Request.Form["__EVENTTARGET"];
        if (control1 == "continueStp")
        {
            Response.Redirect("vendor_05_financialInfo.aspx");
        }
    }

    //#####################################################3

    protected void rptGeneral_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        e.Command.Parameters["@VendorId"].Value = VendorId;
    }
}