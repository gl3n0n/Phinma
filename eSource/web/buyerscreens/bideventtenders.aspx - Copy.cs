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

public partial class web_buyerscreens_bideventtendersx : System.Web.UI.Page
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
            fillGrid();
            lnkComparison.NavigateUrl = "javascript://";
            lnkComparison.Attributes.Add("onclick", "javascript:__doPostBack('lnkSave',''); window.open('../reports/eventtenderscanvass.aspx?brn=' + " + Session[Constant.SESSION_BIDREFNO].ToString() + " , 'x', 'toolbar=no, menubar=no, width=950; height=790, top=80, left=80, resizable=yes, scrollbars=yes'); ");
        }
        else
        {
            GvTenders.DataBind();
            GvTendersAddedCosts.DataBind();
            fillGrid();
        }
        
        lnkComparison.Enabled = !isEnabledComparison();

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

    protected void fillGrid()
    {
        string str = "SELECT TOP 10 VendorName, VendorEmail, MobileNo, VendorAddress FROM tblVendors";

        myConnection = new SqlConnection(conn1);
        myConnection.Open();
        myCommand = new SqlCommand(str, myConnection);
        SqlDataAdapter mySQLDataAdapter;
        myDataSet = new DataTable();
        mySQLDataAdapter = new SqlDataAdapter(myCommand);
        mySQLDataAdapter.Fill(myDataSet);
        //GridView1.DataSource = myDataSet;
        //GridView1.DataBind();
        ViewState["dtList"] = myDataSet;
    }
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt1 = (DataTable)ViewState["dtList"];
            if (dt1 == null)
            {
                throw new Exception("No Records to Export");
            }
            string Path = "C:\\eSourcePOtoACCPAC\\eSourceForPO_" + Session["BidRefNo"] + ".xls";
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

    protected void lnkSave_Click(object sender, EventArgs e)
    {
        string control1 = Request.Form["__EVENTTARGET"];
        string biddetail;
        //Response.Write(control1 + GvTenders.Rows.Count.ToString());
        foreach (GridViewRow oGrid in GvTenders.Rows)
        {

            biddetail = oGrid.Cells[0].Text.Trim();
            //RadioButton vendori = (RadioButton)oGrid.biddetail;
            //if (oGrid.HasControls("16101"))
            //{
            //}
            for (int i = 0; i < oGrid.Cells.Count; i++)
            {
                if (i >= 3)
                {
                    char[] delimiterChars = { '(' };
                    string valu = GvTenders.HeaderRow.Cells[i].Text.Replace(")", "");
                    string[] vendori = valu.Split(delimiterChars);
          
                    //HtmlInputRadioButton vendori = Directcast(oGrid.FindControl("2042"),HtmlInputRadioButton);
                    //Response.Write(oGrid.Cells[0].Text);
                    //Response.Write(vendori[1].ToString());
                }
            }
                    string bidItem = oGrid.Cells[0].Text.Trim() != "" ? oGrid.Cells[0].Text.Trim() : "";

                    TextBox biditemF = (TextBox)oGrid.FindControl(bidItem);
                    if (biditemF.Text != "0")
                    {

                        sCommand = "UPDATE tblBidTenders SET Status=1 WHERE BidDetailNo=" + bidItem;
                        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                        sCommand = "UPDATE tblBidTenders SET Status=2 WHERE BidDetailNo=" + bidItem + " AND VendorId=" + biditemF.Text.Trim();
                        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                        //Response.Write(sCommand + "<br>");
                    }
        }

        foreach (GridViewRow oGrid in GvTendersAddedCosts.Rows)
        {
            for (int i = 0; i < oGrid.Cells.Count; i++)
            {
                string vendori = GvTendersAddedCosts.HeaderRow.Cells[i].Text;
                TextBox txtFld = (TextBox)oGrid.FindControl(vendori);

                //Update Duties and Taxes
                if (oGrid.RowType == DataControlRowType.DataRow)
                {
                    if (i >= 1 && oGrid.RowIndex == 0)
                    {
                        sCommand = "UPDATE tblBidTendersAddedCosts SET BidRefNo=" + Session["BidRefNo"] + ", VendorId=" + vendori.Trim() + ", DutiesTaxes=" + txtFld.Text.Trim() + " WHERE BidRefNo=" + Session["BidRefNo"] + " AND VendorId=" + vendori.Trim();
                        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                    }
                }
                //Update Brokerage
                if (oGrid.RowType == DataControlRowType.DataRow)
                {
                    if (i >= 1 && oGrid.RowIndex == 1)
                    {
                        sCommand = "UPDATE tblBidTendersAddedCosts SET BidRefNo=" + Session["BidRefNo"] + ", VendorId=" + vendori.Trim() + ", Brokerage=" + txtFld.Text.Trim() + " WHERE BidRefNo=" + Session["BidRefNo"] + " AND VendorId=" + vendori.Trim();
                        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                    }
                }
                //Update Freight
                if (oGrid.RowType == DataControlRowType.DataRow)
                {
                    if (i >= 1 && oGrid.RowIndex == 2)
                    {
                        sCommand = "UPDATE tblBidTendersAddedCosts SET BidRefNo=" + Session["BidRefNo"] + ", VendorId=" + vendori.Trim() + ", Freight=" + txtFld.Text.Trim() + " WHERE BidRefNo=" + Session["BidRefNo"] + " AND VendorId=" + vendori.Trim();
                        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                    }
                }
                //Update Insurance
                if (oGrid.RowType == DataControlRowType.DataRow)
                {
                    if (i >= 1 && oGrid.RowIndex == 3)
                    {
                        sCommand = "UPDATE tblBidTendersAddedCosts SET BidRefNo=" + Session["BidRefNo"] + ", VendorId=" + vendori.Trim() + ", Insurance=" + txtFld.Text.Trim() + " WHERE BidRefNo=" + Session["BidRefNo"] + " AND VendorId=" + vendori.Trim();
                        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                    }
                }
            }
            //string bidItem = oGrid.Cells[0].Text.Trim() != "" ? oGrid.Cells[0].Text.Trim() : "";

            //TextBox biditemF = (TextBox)oGrid.FindControl(bidItem);
            //Response.Write(biditemF.Text + "<br>");
        }
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
                                        head.Attributes.Add("VendorId", head.Text);
                                        head.Text = oReader["VendorName"].ToString() + " (" + head.Text + ")";
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

                if (i == 1)
                {
                    TextBox BidItemx = new TextBox();
                    BidItemx.ID = e.Row.Cells[0].Text.ToString();
                    BidItemx.Text = "0";

                    Label lblR = new Label();
                    lblR.Text = e.Row.Cells[i].Text.ToString();
                    BidItemx.Attributes.Add("class", e.Row.Cells[0].Text.ToString());
                    BidItemx.Attributes.Add("style", "display:none");
                    cell.Controls.Add(BidItemx);
                    cell.Controls.Add(lblR);
                }
                if (i >= 3)
                {

                    //e.Row.Cells[i].Width = new Unit("140");
                    e.Row.Cells[i].Attributes.Add("style", "width:140px;");
                    HtmlInputRadioButton chk = new HtmlInputRadioButton();
                    chk.EnableViewState = true;
                    //chk.Enabled = true;
                    //chk.l = e.Row.Cells[i].Text;
                    chk.Value = e.Row.Cells[0].Text.ToString();

                    Label lblR = new Label();
                    lblR.Text = e.Row.Cells[i].Text.ToString().Replace("| ", "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style='font-size:9px;font-style:italic'>").Replace("^", "</span>");

                    char[] delimiterChars2 = { '|' };
                    string valu2 = e.Row.Cells[i].Text;
                    string[] valus2 = valu2.Trim().Split(delimiterChars2);
                    char[] delimiterChars3 = { ' ' };
                    string[] txtAmount = valus2[0].Trim().Split(delimiterChars3);
                    if (txtAmount.Length > 1)
                    {
                        //Response.Write(txtAmount[1].Trim() + "<br>");
                    }

                    char[] delimiterChars = { '(' };
                    string valu = GvTenders.HeaderRow.Cells[i].Text.Replace(")", "");
                    string[] valus = valu.Split(delimiterChars);
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
                            chk.Attributes.Add("Amount", txtAmount[1].ToString());
                        }
                        e.Row.Cells[i].Controls.Add(chk);
                        e.Row.Cells[i].Controls.Add(lblR);
                        //cell.Text = "<input type='radio' id='" + e.Row.Cells[0].Text.ToString() + "' name='" + e.Row.Cells[0].Text.ToString() + "' value='" + valus[1].Trim() + "' >&nbsp;&nbsp;" + cell.Text;
                        //cell.Text = cell.Text;
                    }
                    else
                    {
                        chk.Disabled = true;
                        e.Row.Cells[i].Controls.Add(chk);
                        //cell.Text = "<input type='radio' disabled='disabled' name='" + e.Row.Cells[0].Text.ToString() + "' value='" + valus[1].Trim() + "' >&nbsp;&nbsp;" + cell.Text;
                        //cell.Text =  cell.Text;
                    }
                }
            }
        }
    }
    protected void GvTendersAddedCosts_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        TableCell cell;
        TableCell head;

        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            cell = e.Row.Cells[i];
            //e.Row.Cells[0].Width = new Unit("338px");
            //e.Row.Cells[1].Width = new Unit("220px");
            //e.Row.Cells[2].Width = new Unit("35px");

            if (e.Row.RowType == DataControlRowType.Header)
            {
                //e.Row.Visible = false;
                e.Row.ForeColor = Color.White;
                e.Row.BackColor = Color.White;
                e.Row.Style.Add("display", "none");
            }


            //CHANGE HEADER FROM VendorId To VendorName
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (i == 0)
                {
                    e.Row.Cells[i].HorizontalAlign =  HorizontalAlign.Right;
                    e.Row.Cells[i].BackColor = Color.FromArgb(245, 186, 34);
                    e.Row.Cells[i].Font.Bold = true;
                    e.Row.Cells[i].ForeColor = Color.White;
                    //e.Row.Cells[i].Attributes.Add("style", "width:290px;display:block;margin:0px;height:18px;padding:10px 5px;");
                    if (GvTendersAddedCosts.Rows.Count == 4)
                    {
                        e.Row.Cells[i].BackColor = Color.Gray;
                    }
                }
                if (i >= 1)
                {

                    TextBox txtField = new TextBox();
                    txtField.EnableViewState = true;
                    //chk.Enabled = true;
                    //chk.l = e.Row.Cells[i].Text;
                    //txtField.Text = e.Row.Cells[0].Text.ToString();

                    //Label lblR = new Label();
                    //lblR.Text = e.Row.Cells[i].Text.ToString();

                    
                    txtField.ID = GvTendersAddedCosts.HeaderRow.Cells[i].Text.ToString();
                    txtField.Attributes.Add("style", "width:100px");
                    txtField.Text = e.Row.Cells[i].Text.ToString();
                    txtField.Attributes.Add("onblur", "if($(this).val()==''){ $(this).val('0.0') }");
                    txtField.Attributes.Add("class", GvTendersAddedCosts.HeaderRow.Cells[i].Text.ToString().Trim());
                    //txtField.Attributes.Add("onclick", "/*alert('" + txtField.ClientID + "')*/ $('." + e.Row.Cells[0].Text.ToString() + "').val($(this).val())");
                    //chk.GroupName = e.Row.Cells[0].Text.ToString();
                    e.Row.Cells[i].Controls.Add(txtField);
                    //e.Row.Cells[i].Width = new Unit("140");
                    e.Row.Cells[i].Attributes.Add("style", "width:140px;");
                    if (GvTendersAddedCosts.Rows.Count != 4)
                    {
                        e.Row.Cells[i].Controls.Add(txtField);
                    }
                    else
                    {
                        e.Row.Cells[i].Font.Bold = true;
                        e.Row.Cells[i].BackColor = Color.Gray;
                        e.Row.Cells[i].Attributes.Add("class", "total" + GvTendersAddedCosts.HeaderRow.Cells[i].Text.ToString().Trim());
                    }
                    //e.Row.Cells[i].Controls.Add(lblR);
                    //cell.Text = "<input type='radio' id='" + e.Row.Cells[0].Text.ToString() + "' name='" + e.Row.Cells[0].Text.ToString() + "' value='" + valus[1].Trim() + "' >&nbsp;&nbsp;" + cell.Text;
                    //cell.Text = cell.Text;
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
        //Response.Write(GvTenders.HeaderRow.Cells.Count.ToString());
        //Response.Write(GvTendersAddedCosts.Rows.Count.ToString());

    }
}

