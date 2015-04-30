using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Text;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EBid.lib.bid.data;
using EBid.lib.bid.trans;
using EBid.lib.constant;
using EBid.lib;
using EBid.ConnectionString;
using System.Data.Sql;
using System.Data.SqlClient;
using EBid.ConnectionString;
using System.Reflection;
using System.IO;
using System.Drawing;
using System.Web.Security;
using System.Collections.Generic;
using Microsoft.Reporting.WebForms;
using System.Data.Odbc;

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using System.IO;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Web.UI.WebControls.WebParts;

public partial class web_buyerscreens_bideventtenders : System.Web.UI.Page
{
    private string connstring = "";
    private string sCommand;
    SqlDataReader oReader; 
    string query;
    SqlCommand cmd;
    SqlConnection conn;
    public string jquery = "";
    SqlCommand myCommand;
    DataTable myDataSet;
    SqlConnection myConnection;
    string conn1 = ConfigurationManager.ConnectionStrings["EBidConnectionString1"].ConnectionString;
    public List<string> BidDetailNos = new List<string>();
    public List<string> VendorIds = new List<string>();

    protected void Page_Load(object sender, System.EventArgs e)
    {

        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (Session[Constant.SESSION_BIDREFNO] == null)
            Response.Redirect("bidsforeval.aspx");

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Bid Item Details");



        if (!IsPostBack)
        {
            //Response.Write("not postback");
            int bidrefno = 0;
            rvBidEventTendersComparisons.LocalReport.ReportPath = Request.PhysicalApplicationPath + @"web\reports\eventtenderscanvass.rdlc";
            rvBidEventTendersComparisons.ShowReportBody = false;

            if (!string.IsNullOrEmpty(Session["BidRefNo"].ToString()))
            {
                if (int.TryParse(Session["BidRefNo"].ToString(), out bidrefno))
                {
                    ReportParameter[] RequestorParameter = new ReportParameter[1];
                    RequestorParameter[0] = new ReportParameter("BidRefNo", bidrefno.ToString());
                    //RequestorParameter[1] = new ReportParameter("UseAlias", "0");
                    rvBidEventTendersComparisons.LocalReport.SetParameters(RequestorParameter);
                }
                rvBidEventTendersComparisons.ShowReportBody = true;
            }

            //rvBidTendersForPOFileImport.ProcessingMode = ProcessingMode.Local;
            //rvBidTendersForPOFileImport.LocalReport.ReportPath = Request.PhysicalApplicationPath + @"web\reports\bidtendersforpofileimport.rdlc";
            //rvBidTendersForPOFileImport.ShowReportBody = false;

            //if (!string.IsNullOrEmpty(Session["BidRefNo"].ToString()))
            //{
            //    if (int.TryParse(Session["BidRefNo"].ToString(), out bidrefno))
            //    {
            //        ReportParameter[] RequestorParameter2 = new ReportParameter[1];
            //        RequestorParameter2[0] = new ReportParameter("BidRefNo", bidrefno.ToString());
            //        rvBidTendersForPOFileImport.LocalReport.SetParameters(RequestorParameter2);
            //    }
            //    rvBidTendersForPOFileImport.ShowReportBody = true;
            //}

        }
        else
        {
            GvTenders.DataBind();
        }
        
        //lnkComparison.Enabled = !isEnabledComparison();

        if (Session["EndorsementMessage"] != null)
        {
            lblMessage.Text = Session["EndorsementMessage"].ToString().Trim();
            Session["EndorsementMessage"] = null;
        }


    }



    protected void lnkBack_Click(object sender, EventArgs e)
    {
        if (Session[Constant.SESSION_LASTPAGE] != null)
            Response.Redirect(Session[Constant.SESSION_LASTPAGE].ToString());
        else
            Response.Redirect("bidsforeval.aspx");
    }

    

    //public static void WriteAttachment(string FileName, string FileType, string content)
    //{
    //    HttpResponse Response = System.Web.HttpContext.Current.Response;
    //    Response.ClearHeaders();
    //    Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
    //    Response.ContentType = FileType;
    //    Response.Write(content);
    //    Response.End();

    //}
    
    
    private void CreatePDF(string fileName)
    {
        // Variables
        Warning[] warnings;
        string[] streamIds;
        string mimeType = string.Empty;
        string encoding = string.Empty;
        string extension = string.Empty;


        // Setup the report viewer object and get the array of bytes
        ReportViewer viewer = new ReportViewer();
        viewer.ProcessingMode = ProcessingMode.Local;
        viewer.LocalReport.ReportPath = "../ESOURCE_TRANS/web/reports/eventtenderscanvass.rdlc";


        byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);


        // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
        Response.Buffer = true;
        Response.Clear();
        Response.ContentType = mimeType;
        Response.AddHeader("content-disposition", "attachment; filename=" + fileName + "." + extension);
        Response.BinaryWrite(bytes); // create the file
        Response.Flush(); // send it to the client to download
    }


    protected void lnkSave_Click(object sender, EventArgs e)
    {
        string control1 = Request.Form["__EVENTTARGET"];
        string biddetail;

        foreach (GridViewRow oGrid in GvTenders.Rows)
        {
            int Num;
            bool isNum = int.TryParse(oGrid.Cells[0].Text.Trim(), out Num);
            if (isNum && oGrid.Cells[0].Text.Trim() != "0")
            {
                biddetail = oGrid.Cells[0].Text.Trim();
                string bidItem = oGrid.Cells[0].Text.Trim() != "" ? oGrid.Cells[0].Text.Trim() : "";

                TextBox biditemF = (TextBox)oGrid.FindControl(bidItem);
                if (biditemF.Text != "0")
                {
                    sCommand = "UPDATE tblBidTenders SET Status=1 WHERE BidDetailNo=" + bidItem;
                    SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                    sCommand = "UPDATE tblBidTenders SET Status=2 WHERE BidDetailNo=" + bidItem + " AND VendorId=" + biditemF.Text.Trim();
                    SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                }
            }
        }

        foreach (GridViewRow oGrid in GvTenders.Rows)
        {
            for (int i = 0; i < oGrid.Cells.Count; i++)
            {
                if (i >= 3 && oGrid.RowIndex > BidDetailNos.Count)
                {
                    string vendori = VendorIds[i - 3].ToString();
                    TextBox txtFld = (TextBox)oGrid.FindControl(vendori);

                    //Update InLandFreight
                    if (oGrid.RowType == DataControlRowType.DataRow)
                    {
                        if (oGrid.Cells[1].Text == "InLandFreight")
                        {
                            double Num;
                            bool isNum = double.TryParse(txtFld.Text.Trim(), out Num);
                            string txtFldnew;
                            if (isNum)
                            {
                                txtFldnew = txtFld.Text.Trim().Replace(",", "");
                            }
                            else { txtFldnew = "0"; }
                            sCommand = "UPDATE tblBidTendersAddedCosts SET BidRefNo=" + Session["BidRefNo"] + ", VendorId=" + vendori.Trim() + ", InLandFreight=" + txtFldnew + " WHERE BidRefNo=" + Session["BidRefNo"] + " AND VendorId=" + vendori.Trim();
                            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                            //Response.Write(sCommand + "<br>");
                        }
                    }
                    //Update SeaAirFreight
                    if (oGrid.RowType == DataControlRowType.DataRow)
                    {
                        if (oGrid.Cells[1].Text == "SeaAirFreight")
                        {
                            double Num;
                            bool isNum = double.TryParse(txtFld.Text.Trim(), out Num);
                            string txtFldnew;
                            if (isNum)
                            {
                                txtFldnew = txtFld.Text.Trim().Replace(",", "");
                            }
                            else { txtFldnew = "0"; }
                            sCommand = "UPDATE tblBidTendersAddedCosts SET BidRefNo=" + Session["BidRefNo"] + ", VendorId=" + vendori.Trim() + ", SeaAirFreight=" + txtFldnew + " WHERE BidRefNo=" + Session["BidRefNo"] + " AND VendorId=" + vendori.Trim();
                            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                        }
                    }
                    //Update Brokerage
                    if (oGrid.RowType == DataControlRowType.DataRow)
                    {
                        if (oGrid.Cells[1].Text == "FowardingBrokerage")
                        {
                            double Num;
                            bool isNum = double.TryParse(txtFld.Text.Trim(), out Num);
                            string txtFldnew;
                            if (isNum)
                            {
                                txtFldnew = txtFld.Text.Trim().Replace(",", "");
                            }
                            else { txtFldnew = "0"; }
                            sCommand = "UPDATE tblBidTendersAddedCosts SET BidRefNo=" + Session["BidRefNo"] + ", VendorId=" + vendori.Trim() + ", FowardingBrokerage=" + txtFldnew + " WHERE BidRefNo=" + Session["BidRefNo"] + " AND VendorId=" + vendori.Trim();
                            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                        }
                    }
                    //Update Duties and Taxes
                    if (oGrid.RowType == DataControlRowType.DataRow)
                    {
                        if (oGrid.Cells[1].Text == "DutiesTaxes")
                        {
                            double Num;
                            bool isNum = double.TryParse(txtFld.Text.Trim(), out Num);
                            string txtFldnew;
                            if (isNum)
                            {
                                txtFldnew = txtFld.Text.Trim().Replace(",", "");
                            }
                            else { txtFldnew = "0"; }
                            //Response.Write(txtFld.Text.Trim());
                            sCommand = "UPDATE tblBidTendersAddedCosts SET BidRefNo=" + Session["BidRefNo"] + ", VendorId=" + vendori.Trim() + ", DutiesTaxes=" + txtFldnew + " WHERE BidRefNo=" + Session["BidRefNo"] + " AND VendorId=" + vendori.Trim();
                            //Response.Write(VendorIds[i - 3].ToString() + " " + i.ToString() + "<br>");
                            //Response.Write(sCommand + "<br>");
                            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                        }
                    }
                    //Update Insurance
                    if (oGrid.RowType == DataControlRowType.DataRow)
                    {
                        if (oGrid.Cells[1].Text == "Insurance")
                        {
                            double Num;
                            bool isNum = double.TryParse(txtFld.Text.Trim(), out Num);
                            string txtFldnew;
                            if (isNum)
                            {
                                txtFldnew = txtFld.Text.Trim().Replace(",", "");
                            }
                            else { txtFldnew = "0"; }
                            sCommand = "UPDATE tblBidTendersAddedCosts SET BidRefNo=" + Session["BidRefNo"] + ", VendorId=" + vendori.Trim() + ", Insurance=" + txtFldnew + " WHERE BidRefNo=" + Session["BidRefNo"] + " AND VendorId=" + vendori.Trim();
                            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                        }
                    }
                    //Update DeliveryCostToSite
                    if (oGrid.RowType == DataControlRowType.DataRow)
                    {
                        if (oGrid.Cells[1].Text == "DeliveryCostToSite")
                        {
                            double Num;
                            bool isNum = double.TryParse(txtFld.Text.Trim(), out Num);
                            string txtFldnew;
                            if (isNum)
                            {
                                txtFldnew = txtFld.Text.Trim().Replace(",", "");
                            }
                            else { txtFldnew = "0"; }
                            sCommand = "UPDATE tblBidTendersAddedCosts SET BidRefNo=" + Session["BidRefNo"] + ", VendorId=" + vendori.Trim() + ", DeliveryCostToSite=" + txtFldnew + " WHERE BidRefNo=" + Session["BidRefNo"] + " AND VendorId=" + vendori.Trim();
                            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                        }
                    }
                }
            }


        }

        GvTenders.DataBind();
        Response.Redirect("bideventtenders.aspx");
    }


    protected void lnkEndorse_Click(object sender, EventArgs e)
    {
        string control1 = Request.Form["__EVENTTARGET"];

        ExportToExcel();

        if (control1 == "lnkEndorse")
        {
            //Response.Redirect("bidsforeval.aspx");
            Response.Redirect("endorsedbidtenders.aspx");
        }
    }


    protected void lnkExportToExcel_Click(object sender, EventArgs e)
    {
        ReportHelper.ExportToExcel(this, rvBidEventTendersComparisons, "Bid Event Tenders Comparison Report_" + Session["BidRefNo"].ToString() + ".xls");
    }



    protected void lnkExportToExcel2_Click(object sender, EventArgs e)
    {
        //string ACCPAC = Constant.ACCPACUNPROC;
        //ReportHelper.SaveToExcel(this, rvBidTendersForPOFileImport, ACCPAC + "PO Import-" + Session["BidRefNo"].ToString() + ".xls");
        //Response.Redirect("bidsforeval.aspx");
    }


    protected void lnkExportToPdf_Click(object sender, EventArgs e)
    {
        PDFDeviceInfo deviceInfo = new PDFDeviceInfo("13in", "8.5in", "0.5in", "0.5in", "1in", "1in");
        ReportHelper.ExportToPDF(this, rvBidEventTendersComparisons, "Bid Event Tenders Comparison Report" + Session["BidRefNo"].ToString() + ".pdf", deviceInfo);
    }


    public string getVendorIds()
    {
        jquery = "";
        for (int i = 0; i < GvTenders.HeaderRow.Cells.Count; i++)
        {
            if (i >= 3)
            {
                char[] delimiterChars = { '(' };
                string valu = GvTenders.HeaderRow.Cells[i].Text.Replace(")", "");
                string[] valus = valu.Split(delimiterChars);
                //Response.Write(valus[1].Trim());
                jquery = jquery + "'" + valus[1].Trim() + "',";
            }
        }
        jquery = jquery.Substring(0, jquery.Length -1);
        return jquery;
    }




    protected void GvTenders_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        TableCell cell;
        TableCell head;

        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                head = e.Row.Cells[i];
                if (i >= 3)
                {
                    double Num;
                    bool isNum = double.TryParse(head.Text.ToString().Trim(), out Num);
                    if (isNum)
                    {
                        //Page.Controls.Add(hyp);

                        VendorIds.Add(head.Text.ToString().Trim());
                        query = "s3p_EBid_GetVendorNameById";
                        using (conn = new SqlConnection(connstring))
                        {
                            using (cmd = new SqlCommand(query, conn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@vendorid", Int32.Parse(head.Text.ToString().Trim()));
                                conn.Open(); oReader = cmd.ExecuteReader();
                                if (oReader.HasRows)
                                {
                                    while (oReader.Read())
                                    {

                                        //TextBox txtVendorId = new TextBox();
                                        //txtVendorId.EnableViewState = true;
                                        //txtVendorId.ID = "txtVendorId" + i;
                                        ////txtVendorId. = "txtVendorId" + i;
                                        //txtVendorId.Text = head.Text.ToString().Trim();

                                        head.Attributes.Add("VendorId", head.Text);
                                        //head.Controls.Add(txtVendorId);
                                        head.Text = oReader["VendorName"].ToString() + "<br><a href='downloadattachments.aspx?vendor=" + head.Text + "&bid=" + Session[Constant.SESSION_BIDREFNO].ToString() + "' target='_blank'>Download All Attachments</a><br>" + "(" + head.Text + ")";
                                        //head.Text = oReader["VendorName"].ToString() + "<br" + "(" + head.Text + ")";
                                    }
                                }
                            }
                        }
                    }
                }
            }


            //CHANGE HEADER FROM VendorId To VendorName
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Width = new Unit("30px");
                e.Row.Cells[1].Width = new Unit("220px");
                e.Row.Cells[2].Width = new Unit("35px");
                cell = e.Row.Cells[i];
                if (i <= 2)
                {
                    cell.BackColor = System.Drawing.Color.LightGray;
                    cell.Font.Bold = true;
                }

                if (i == 1 && e.Row.Cells[0].Text != "0")
                {
                    TextBox BidItemx = new TextBox();
                    BidItemx.ID = e.Row.Cells[0].Text.ToString();
                    BidItemx.Text = "0";

                    Label lblR = new Label();
                    lblR.Text = cell.Text.ToString();
                    BidItemx.Attributes.Add("class", e.Row.Cells[0].Text.ToString());
                    BidItemx.Attributes.Add("style", "display:none");
                    cell.Controls.Add(BidItemx);
                    cell.Controls.Add(lblR);

                    BidDetailNos.Add(e.Row.Cells[0].Text.ToString());
                }

                //VendorId = valus[1];
                char[] delimiterChars = { '(' };
                string valu = GvTenders.HeaderRow.Cells[i].Text.Replace(")", "");
                string[] valus = valu.Split(delimiterChars);
                //Response.Write(GvTenders.HeaderRow.Cells[i].HasAttributes); //.Attributes["vendorid"].ToString()); //.Attributes.("vendorid"));


                if (i >= 3 && e.Row.Cells[1].Text == "Total")
                {
                    e.Row.Cells[i].Attributes.Add("class", "total" + valus[1].ToString());
                }

                if (i >= 3 && e.Row.Cells[1].Text == "SubTotal")
                {
                    e.Row.Cells[i].Attributes.Add("class", "subtotal" + valus[1].ToString());
                }

                string curr = "";

                if (i >= 3 && e.Row.Cells[0].Text != "0")
                {

                    //cell.Width = new Unit("140");
                    cell.Attributes.Add("style", "width:140px;");
                    HtmlInputRadioButton chk = new HtmlInputRadioButton();
                    chk.EnableViewState = true;
                    chk.Value = e.Row.Cells[0].Text.ToString();

                    TextBox txtPrice = new TextBox();

                    char[] delimiterChars2 = { '|' };
                    string valu2 = cell.Text;
                    string[] valus2 = valu2.Trim().Split(delimiterChars2);
                    char[] delimiterChars3 = { ' ' };
                    string[] txtAmount = valus2[0].Trim().Split(delimiterChars3);

                    Label lblR = new Label();
                    lblR.Attributes.Add("style", "font-weight:bold");
                    lblR.Text = cell.Text.ToString().Replace("| ", "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style='font-size:9px;font-style:italic;font-weight:normal'>").Replace("^", "</span>");

                    //Tender Attachments
                    //sCommand = "SELECT status FROM tblBidTenders WHERE BidDetailNo=" + e.Row.Cells[0].Text.ToString().Trim() + " AND VendorId=" + valus[1].Trim();
                    //oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
                    //if (oReader.HasRows)
                    //{
                    //    oReader.Read();
                    //    statusTender = oReader["Status"].ToString();
                    //} oReader.Close();
                    //lblR.Text = lblR.Text + ("<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a  href='#' style='font-size:9px;font-style:italic;font-weight:normal'>[Attachment]</a>");

                    //SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

                    if (cell.Text.ToString().Trim() != "&nbsp;")
                    {
                        string statusTender = "";
                        sCommand = "SELECT status FROM tblBidTenders WHERE BidDetailNo=" + e.Row.Cells[0].Text.ToString().Trim() + " AND VendorId=" + valus[1].Trim();
                        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
                        if (oReader.HasRows)
                        {
                            oReader.Read();
                            statusTender = oReader["Status"].ToString();
                        } oReader.Close();
                        //Response.Write(statusTender + "<br>");
                        if (statusTender == "2")
                        {
                            chk.Checked = true;
                        }
                        chk.ID = valus[1].Trim();
                        chk.Value = valus[1].Trim();
                        chk.Attributes.Add("onclick", "$('." + e.Row.Cells[0].Text.ToString().Trim() + "').val($(this).val())");
                        chk.Attributes.Add("class", valus[1].Trim());
                        if (txtAmount.Length > 1)
                        {
                            chk.Attributes.Add("Amount", txtAmount[1].ToString().Replace(",",""));
                            txtPrice.Text = txtAmount[1].ToString();
                        }

                        txtPrice.ID = "amount" + valus[1].Trim();
                        txtPrice.Attributes.Add("style", "display:none");


                        cell.Controls.Add(chk);
                        cell.Controls.Add(lblR);
                        cell.Controls.Add(txtPrice);
                        //cell.Text = "<input type='radio' id='" + e.Row.Cells[0].Text.ToString() + "' name='" + e.Row.Cells[0].Text.ToString() + "' value='" + valus[1].Trim() + "' >&nbsp;&nbsp;" + cell.Text;
                        //cell.Text = cell.Text;
                    }
                    else
                    {
                        chk.Disabled = true;
                        cell.Controls.Add(chk);
                        //cell.Text = "<input type='radio' disabled='disabled' name='" + e.Row.Cells[0].Text.ToString() + "' value='" + valus[1].Trim() + "' >&nbsp;&nbsp;" + cell.Text;
                        //cell.Text =  cell.Text;
                    }
                }
                else if(i >= 3)
                {
                    //e.Row.Cells[0].Text = e.Row.Cells[1].Text;
                    //e.Row.Cells[0].ColumnSpan = 3;
                    //e.Row.Cells[1].Visible = false;
                    //e.Row.Cells[2].Visible = false;

                    Label lblCurr = new Label();
                    lblCurr.Text = "";

                    TextBox txtField = new TextBox();
                    txtField.EnableViewState = true;
                    txtField.ID = valus[1].ToString();
                    txtField.Attributes.Add("style", "width:100px");
                    txtField.Text = cell.Text.ToString() != "" && cell.Text.ToString()!="&nbsp;" ? cell.Text.ToString() : "0";
                    txtField.Attributes.Add("onblur", "if($(this).val()=='' || $(this).val()=='&nbsp;'){ $(this).val('0.0') }");
                    txtField.Attributes.Add("class", valus[1].ToString());
                    if (e.Row.Cells[1].Text != "SubTotal" && e.Row.Cells[1].Text != "Total")
                    {
                        cell.Controls.Add(lblCurr);
                        cell.Controls.Add(txtField);
                    }
                    else
                    {
                        cell.BackColor = Color.FromArgb(211, 211, 211);
                        cell.Font.Bold = true;
                        cell.Font.Size = 8;
                    }
                }
            }
        }
    }





    //################################################################
    //################################################################

    protected void gvBidItemTenders_DataBound(object sender, EventArgs e)
    {
        //Session[Constant.SESSION_BIDREFNO] = gvBidItemTenders.DataKeys[gvBidItemTenders.SelectedIndex].Values[0].ToString();
        // load comments control
        LoadComments();
    }

    private void LoadComments()
    {
        phComments.Controls.Clear();
        web_usercontrol_commentlist_tender uc = (web_usercontrol_commentlist_tender)Page.LoadControl(@"~/web/usercontrol/commentlist_tender.ascx");
        phComments.Controls.Add(uc);
    }

    protected bool isEndorsed(Object itemStatus)
    {
        int stat = int.Parse(itemStatus.ToString());

        return (stat == Constant.BIDTENDER_STATUS.STATUS.ENDORSED);
    }

    protected bool isEnabled(object itemStatus, object rdeadline)
    {
        int istat = int.Parse(itemStatus.ToString());

        if ((istat == Constant.BIDTENDER_STATUS.STATUS.RENEGOTIATED) && (rdeadline.ToString() == ""))
        {
            return true;
        }
        else if ((istat == Constant.BIDTENDER_STATUS.STATUS.RENEGOTIATED) && (rdeadline.ToString() != ""))
        {
            int stat = int.Parse(itemStatus.ToString());
            DateTime rdate = DateTime.Parse(rdeadline.ToString());
            DateTime dtnow = DateTime.Now;

            if (DateTime.Compare(rdate, dtnow) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    protected bool isEnabledComparison()
    {
        if ((GetBidEventRenegotiationDeadline().ToString() != ""))
        {
            DateTime rdate = DateTime.Parse(GetBidEventRenegotiationDeadline().ToString());
            DateTime dtnow = DateTime.Now;

            if (DateTime.Compare(rdate, dtnow) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private string GetBidEventRenegotiationDeadline()
    {
        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
        sqlParams[0].Value = Int32.Parse(Session[Constant.SESSION_BIDREFNO].ToString());

        return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "sp_GetBidEventRenegotiationDeadline", sqlParams).ToString().Trim();
    }






    protected void Page_LoadComplete(object sender, System.EventArgs e)
    {
        foreach (string Biddetailno1 in BidDetailNos) // Loop through List with foreach
        {
            //Response.Write(Biddetailno1+"<br>");
        }
        foreach (string Vendorids1 in VendorIds) // Loop through List with foreach
        {
            //Response.Write(Vendorids1 + "<br>");
        }
        //Response.Write(GvTenders.HeaderRow.Cells.Count.ToString());
        //Response.Write(GvTenders.HeaderRow.Cells.Count.ToString());
        for (int k = 0; k < GvTenders.HeaderRow.Cells.Count; k++)
        {
            //Response.Write(GvTenders.HeaderRow.Cells[k].Text);
        }
        for (int i = 0; i < GvTenders.Rows.Count; i++)
        {
            for (int j = 0; j < GvTenders.HeaderRow.Cells.Count; j++)
            {
                if (j <= 2 && GvTenders.Rows[i].Cells[0].Text == "0")
                {
                    GvTenders.Rows[i].Cells[0].Text = GvTenders.Rows[i].Cells[1].Text;
                    GvTenders.Rows[i].Cells[0].ColumnSpan = 3;
                    GvTenders.Rows[i].Cells[0].Style.Add("text-align", "right");
                    GvTenders.Rows[i].Cells[0].BackColor = Color.FromArgb(245, 186, 34);
                    GvTenders.Rows[i].Cells[0].Font.Bold = true;
                    GvTenders.Rows[i].Cells[0].ForeColor = Color.Black;
                    GvTenders.Rows[i].Cells[1].Visible = false;
                    GvTenders.Rows[i].Cells[2].Visible = false;

                    if (GvTenders.Rows[i].Cells[0].Text == "SubTotal" || GvTenders.Rows[i].Cells[0].Text == "Total")
                    {
                        GvTenders.Rows[i].Cells[0].BackColor = Color.FromArgb(211, 211, 211);
                    }
                    if (GvTenders.Rows[i].Cells[0].Text == "Total")
                    {
                        GvTenders.Rows[i].Cells[0].Text = "Grand Total";
                    }
                    if (GvTenders.Rows[i].Cells[0].Text == "InLandFreight")
                    {
                        GvTenders.Rows[i].Cells[0].Text = "In Land Freight";
                    }
                    if (GvTenders.Rows[i].Cells[0].Text == "SeaAirFreight")
                    {
                        GvTenders.Rows[i].Cells[0].Text = "Sea/Air Freight";
                    }
                    if (GvTenders.Rows[i].Cells[0].Text == "FowardingBrokerage")
                    {
                        GvTenders.Rows[i].Cells[0].Text = "Forwarding Brokerage";
                    }
                    if (GvTenders.Rows[i].Cells[0].Text == "DutiesTaxes")
                    {
                        GvTenders.Rows[i].Cells[0].Text = "Duties and Taxes";
                    }
                    if (GvTenders.Rows[i].Cells[0].Text == "DeliveryCostToSite")
                    {
                        GvTenders.Rows[i].Cells[0].Text = "Delivery cost to site";
                    }
                }
            }
        }
    }




    //#############################################
    //#############################################
    protected void fillGrid(string xbidrefno)
    {
        string str = "sp_GetBidTendersForPOFileImport";

        myConnection = new SqlConnection(connstring);
        myConnection.Open();
        myCommand = new SqlCommand(str, myConnection);
        myCommand.CommandType = CommandType.StoredProcedure;
        myCommand.Parameters.AddWithValue("@BidRefNo", Convert.ToInt32(xbidrefno));
        //myCommand.Parameters.AddWithValue("@SeqNo", "");
        SqlDataAdapter mySQLDataAdapter;
        myDataSet = new DataTable();
        mySQLDataAdapter = new SqlDataAdapter(myCommand);
        mySQLDataAdapter.Fill(myDataSet);
        //GridView1.DataSource = myDataSet;
        //GridView1.DataBind();
        ViewState["dtList"] = myDataSet;
        //Response.Write(xbidrefno);
    }
    //protected void btnExportToExcel_Click(object sender, EventArgs e)
    protected void ExportToExcel()
    {
        try
        {
            //Response.Write(Session["BidRefNo"].ToString());
            fillGrid(Session["BidRefNo"].ToString());
            DataTable dt1 = (DataTable)ViewState["dtList"];
            if (dt1 == null)
            {
                throw new Exception("No Records to Export");
            }
            else
            {
                DataGrid DataGrd = new DataGrid();
                DataGrd.DataSource = dt1;
                DataGrd.DataBind();


                //#######################
                string CompanyCode = "";
                sCommand = @"select CompanyCode from rfcCompany where CompanyId in
(SELECT CompanyId FROM tblBidItems where BidRefNo = " + Session["BidRefNo"].ToString() + ")";
                oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
                if (oReader.HasRows)
                {
                    oReader.Read();
                    CompanyCode = oReader["CompanyCode"].ToString();
                }
                oReader.Close();


                string ACCPAC = Constant.ACCPACUNPROC;
                string Path1 = ACCPAC + "\\" + CompanyCode + "\\PO Import-" + Session["BidRefNo"].ToString() + ".xls";

                int i = 1;
                foreach (DataRow row in dt1.Rows)
                {
                    if (i == 2)
                    {
                        //Path1 = ACCPAC + row["5a"].ToString() + "\\PO Import-" + Session["BidRefNo"].ToString() + ".xlsx";
                    }
                    i++;
                }
                var newFile = new FileInfo(Path1);
                if (newFile.Exists)
                {
                    newFile.Delete();
                }

                using (ExcelPackage pck = new ExcelPackage(newFile))
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add(Session["BidRefNo"].ToString());

                    i = 1;
                    foreach (DataRow row in dt1.Rows)
                    {
                        //ws.Cells["A" + i].Value = row["1a"].ToString(); //HEADER H, DEATAILS D
                        //ws.Cells["B" + i].Value = row["2a"].ToString(); //SEQUENCE NO
                        //ws.Cells["C" + i].Value = row["3a"].ToString(); //PO DATE, ITEM No
                        //ws.Cells["D" + i].Value = row["4a"].ToString(); //PO NUMBER, ITEM Desc
                        //ws.Cells["E" + i].Value = row["5a"].ToString(); //VENDOR ID, COMPANY/LOCATION
                        //ws.Cells["F" + i].Value = row["6a"].ToString(); //TERMS, EXPECTED ARRIVAL/DELIVERY DATE
                        //ws.Cells["G" + i].Value = row["7a"].ToString(); //EXPECTED ARRIVAL/DELIVERY DATE, QUANTITY
                        //ws.Cells["H" + i].Value = row["8a"].ToString(); //PO DESCRIPTION-CHANGED TO BUYERCODE, UNIT COST
                        //ws.Cells["I" + i].Value = row["8o"].ToString(); //WORKFLOW, NULL
                        //ws.Cells["J" + i].Value = row["8b"].ToString(); //PR NUMBER, NULL
                        //ws.Cells["K" + i].Value = row["9a"].ToString(); //REFERENCE, NULL
                        //ws.Cells["L" + i].Value = row["10a"].ToString(); //Bid Event Remarks, NULL
                        //ws.Cells["M" + i].Value = row["11a"].ToString(); //InLandFreight, NULL
                        //ws.Cells["N" + i].Value = row["12a"].ToString(); //SeaAirFreight, NULL
                        //ws.Cells["O" + i].Value = row["13a"].ToString(); //FowardingBrokerage, NULL
                        //ws.Cells["P" + i].Value = row["14a"].ToString(); //DutiesTaxes, NULL
                        //ws.Cells["Q" + i].Value = row["15a"].ToString(); //Insurance, NULL
                        //ws.Cells["R" + i].Value = row["16a"].ToString(); //DeliveryCostToSite, NULL

                        ws.Cells["A" + i].Value = row["1a"].ToString(); //HEADER H, DEATAILS D
                        ws.Cells["B" + i].Value = row["2a"].ToString(); //SEQUENCE NO
                        ws.Cells["C" + i].Value = row["3a"].ToString(); //PO DATE, ITEM No
                        ws.Cells["D" + i].Value = row["4a"].ToString(); //PO NUMBER, ITEM Desc
                        ws.Cells["E" + i].Value = row["5a"].ToString(); //VENDOR ID, COMPANY/LOCATION
                        ws.Cells["F" + i].Value = row["6a"].ToString(); //TERMS, EXPECTED ARRIVAL/DELIVERY DATE
                        ws.Cells["G" + i].Value = row["7a"].ToString(); //EXPECTED ARRIVAL/DELIVERY DATE, QUANTITY
                        ws.Cells["H" + i].Value = row["8a"].ToString(); //PO DESCRIPTION-CHANGED TO BUYERCODE, UNIT COST
                        ws.Cells["I" + i].Value = row["8o"].ToString(); //WORKFLOW, NULL
                        ws.Cells["J" + i].Value = row["8b"].ToString(); //PR NUMBER, NULL
                        ws.Cells["K" + i].Value = row["9a"].ToString(); //REFERENCE, NULL
                        ws.Cells["L" + i].Value = row["10b"].ToString(); //Incoterm, NULL
                        ws.Cells["M" + i].Value = row["11a"].ToString(); //InLandFreight, NULL
                        ws.Cells["N" + i].Value = row["12a"].ToString(); //SeaAirFreight, NULL
                        ws.Cells["O" + i].Value = row["13a"].ToString(); //FowardingBrokerage, NULL
                        ws.Cells["P" + i].Value = row["14a"].ToString(); //DutiesTaxes, NULL
                        ws.Cells["Q" + i].Value = row["15a"].ToString(); //Insurance, NULL
                        ws.Cells["R" + i].Value = row["16a"].ToString(); //DeliveryCostToSite, NULL
                        i++;
                    }

                    pck.Save();
                }
            }




            //ExcelPackage pck = new ExcelPackage(newFile);
            //var ws = pck.Workbook.Worksheets.Add("Sample1");

            ////string Path = "C:\\eSourcePOtoACCPAC\\eSourceForPO_" + Session["BidRefNo"] + ".xls";
            ////string Path = "C:\\eSourcePOtoACCPAC\\2eSourceForPO_" + Session["BidRefNo"] + ".xls";
            //FileInfo FI = new FileInfo(Path);
            //StringWriter stringWriter = new StringWriter();
            //HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWriter);
            //DataGrid DataGrd = new DataGrid();
            //DataGrd.DataSource = dt1;
            //DataGrd.DataBind();

            //DataGrd.RenderControl(htmlWrite);
            //string directory = Path.Substring(0, Path.LastIndexOf("\\"));// GetDirectory(Path);
            //if (!Directory.Exists(directory))
            //{
            //    Directory.CreateDirectory(directory);
            //}

            //System.IO.StreamWriter vw = new System.IO.StreamWriter(Path, true);
            //stringWriter.ToString().Normalize();
            //vw.Write(stringWriter.ToString());
            //vw.Flush();
            //vw.Close();
            //WriteAttachment(FI.Name, "application/vnd.ms-excel", stringWriter.ToString());
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
        //Response.End();

    }

    
    //int asdfxxx = 9835;
//DownloadZip(asdfxxx);

    protected void lnkDownloadAll_Click(object sender, EventArgs e)
    {
        Response.Write("adfasdf");
        //Response.Clear();
        //Response.BufferOutput = false;
        //Response.ContentType = "application/zip";
        //Response.AddHeader("content-disposition", "attachment; filename=BidRefNo_" + Session["BidRefNo"].ToString() + ".zip"); // File name of a zip file

        //using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
        //{
        //    string fileNameActual = String.Empty;
        //    string fileNameOrig = String.Empty;
        //    string vendorName = String.Empty;
        //    string path2tmp = String.Empty;

        //    //sCommand = "SELECT * FROM tblVendorFileUploads WHERE VendorID=" + VendorId.ToString() + " AND AsDraft=0";
        //    //string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        //    //SqlDataReader oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        //    //if (oReader.HasRows)
        //    //{
        //    //    oReader.Read();
        //    //    string path = Constant.FILEATTACHMENTSFOLDERDIR;
        //    //}
        //    //oReader.Close();
        //    //Response.Write(sCommand);

        //    //foreach (GridViewRow row1 in gvInvitedSuppliers.Rows)
        //    //{
        //    //    if (row1.FindControl("lnkDownload") != null && row1.FindControl("txtFileAttachment") != null)
        //    //    {
        //    //        vendorName = (row1.FindControl("txtVendorNme") as HiddenField).Value.ToString().Replace("'", "").Replace(",", " ").Replace(".", " ");
        //    //        fileNameActual = (row1.FindControl("lnkDownload") as LinkButton).Text;
        //    //        path2tmp = (row1.FindControl("txtFileAttachment") as HiddenField).Value.ToString();
        //    //        string[] args = path2tmp.Split(new char[] { '|' });
        //    //        string path = Constant.FILEATTACHMENTSFOLDERDIR;
        //    //        string[] folder = args[0].Split(new char[] { '_' });
        //    //        path = path + folder[1].ToString() + '\\' + folder[2].ToString() + '\\';
        //    //        fileNameActual = path + args[0];
        //    //        //fileNameOrig = folder[1].ToString() + '\\' + args[1];
        //    //        fileNameOrig = vendorName + '\\' + args[1];
        //    //        if (File.Exists(fileNameActual))
        //    //        {
        //    //            zip.AddFile(fileNameActual).FileName = fileNameOrig;
        //    //        }
        //    //    }
        //    //}
        //    //zip.Save(Response.OutputStream);
        //}

        //Response.Close();
    }
}
