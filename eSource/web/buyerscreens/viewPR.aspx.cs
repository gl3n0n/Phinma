using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EBid.lib.bid.trans;
using EBid.lib.bid.data;
using EBid.lib.constant;
using EBid.lib;
using EBid.ConnectionString;
using System.IO;
using System.Xml;
using System.Text;
using CalendarControl;
using System.Data.Sql;
using System.Data.SqlClient;
using EBid.ConnectionString;
using System.Text.RegularExpressions;

public partial class web_buyerscreens_viewPR : System.Web.UI.Page
{
   


    private string connstring = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = HttpContext.Current.Session["ConnectionString"].ToString();

        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;
        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "View PR");



        string connCo1 = ConfigurationManager.ConnectionStrings["ACCPACConnectionString"].ConnectionString;
        string connCo2 = ConfigurationManager.ConnectionStrings["ACCPACConnectionString_0a"].ConnectionString;
        string connCo3 = ConfigurationManager.ConnectionStrings["ACCPACConnectionString_0b"].ConnectionString;

        ArrayList Connections = new ArrayList();
        Connections.Add(connCo1);
        Connections.Add(connCo2);
        Connections.Add(connCo3);

        foreach (string Connx in Connections)
        {
            //ExtractPRfromACCPAC(Connx);
        }
        
    }

    public static int GetQuarter(DateTime date)
    {
        if (date.Month >= 4 && date.Month <= 6)
            return 1;
        else if (date.Month >= 7 && date.Month <= 9)
            return 2;
        else if (date.Month >= 10 && date.Month <= 12)
            return 3;
        else
            return 4;

    }

    private float getBudget(string ItemCode1)
    {

        SqlDataReader oReader3;
        string connstring3 = ConfigurationManager.ConnectionStrings["ACCPACConnectionString2"].ConnectionString;
        string query3;
        SqlCommand cmd3;
        SqlConnection conn3;
        float BudgetAmt = 0;

        ItemCode1 = ItemCode1.Substring(0, 3) + "-" + ItemCode1.Substring(3, 20).Trim();
        //Response.Write(ItemCode1 + "<br>");

        query3 = "SELECT BUDGET_AMT FROM ICBUDGET WHERE ITEMNO = @ITEMNO AND FISCYEAR=@FISCYEAR AND FISCPERIOD=@FISCPERIOD";
        //Response.Write(DateTime.Now.Year.ToString() + "," + DateTime.Now.Month.ToString() + "<br>");
        using (conn3 = new SqlConnection(connstring3))
        {
            using (cmd3 = new SqlCommand(query3, conn3))
            {
                cmd3.Parameters.AddWithValue("@ITEMNO", ItemCode1.Trim());
                cmd3.Parameters.AddWithValue("@FISCYEAR", DateTime.Now.Year.ToString());
                cmd3.Parameters.AddWithValue("@FISCPERIOD", DateTime.Now.Month.ToString());
                conn3.Open();
                oReader3 = cmd3.ExecuteReader();
                if (oReader3.HasRows)
                {
                    while (oReader3.Read())
                    {
                        if (oReader3["BUDGET_AMT"].ToString() != null && oReader3["BUDGET_AMT"].ToString() != "")
                        {
                            BudgetAmt = (float)System.Convert.ToSingle(oReader3["BUDGET_AMT"].ToString());
                        }
                    }
                }
            }
        }
        return BudgetAmt;
    }


    private string getCompanyId(string CompanyCode1)
    {
        SqlDataReader oReader;
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        string query;
        SqlCommand cmd;
        SqlConnection conn;
        string CompanyId = "0";

        query = "SELECT CompanyId FROM rfcCompany WHERE CompanyCode = @CompanyCode";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@CompanyCode", CompanyCode1.Trim());
                conn.Open();
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        if (oReader["CompanyId"].ToString() != null)
                        {
                            CompanyId = oReader["CompanyId"].ToString();
                        }
                    }
                }
            }
        }
        return CompanyId;
    }
    
    void ExtractPRfromACCPAC(string connx)
    {
		SqlDataReader oReader, oReader1, oReader2;
		//string connstring1 = ConfigurationManager.ConnectionStrings["ACCPACConnectionString"].ConnectionString;
        string connstring1 = ConfigurationManager.ConnectionStrings["ACCPACConnectionString"].ConnectionString;
        string connstring2 = ConfigurationManager.ConnectionStrings["ACCPACConnectionString2"].ConnectionString;
		string query, query1, query2, query3;
		SqlCommand cmd, cmd1, cmd2, cmd3;
		SqlConnection conn, conn1, conn2, conn3;
		Session["BuyerCode"] = "";
		
		query = "SELECT BuyerCode FROM tblBuyers WHERE BuyerId = @BuyerId";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@BuyerId", Convert.ToInt32(Session["UserId"].ToString()));
                conn.Open();
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
						Session["BuyerCode"] = oReader["BuyerCode"].ToString();
                    }
                 }
             }
         }


        query1 = @"SELECT t1.RQNNUMBER as PRNo, t2.RQNHSEQ as PRLineNo, t2.ITEMNO as ItemCode, t2.ITEMDESC as PRDescription, 
                    t1.REQDATE as PRDate, t1.AUDTORG as CompanyCode, '' as SubCategory, t2.RQRDDATE as DeliveryDate, 
                    t2.ORDERUNIT as UOM, t2.REQQTY as Qty, t2.UNITCOST as UnitPrice, t2.CURRCODE as Currency, '' as Groupname, 
                    '0' as Commodity, t1.REFERENCE as BuyerCode, COMMENT1, t3.WORKFLOW, t2.LOCATION
                    FROM PTPRH t1, PTPRD t2 
                    LEFT JOIN PTUWF t3 ON t3.USERID = @BuyerCode AND t3.WRKFLWTYPE = 20
                    WHERE t2.RQNNUMBER = t1.RQNNUMBER and t1.REFERENCE = @BuyerCode --and t1.ISCOMPLETE = 1";
        using (conn1 = new SqlConnection(connx))
        {
            using (cmd1 = new SqlCommand(query1, conn1))
            {
                cmd1.Parameters.AddWithValue("@BuyerCode", Session["BuyerCode"].ToString());
                conn1.Open();
                oReader1 = cmd1.ExecuteReader();
                if (oReader1.HasRows)
                {
                    while (oReader1.Read())
                    {
                        //Response.Write(getBudget(oReader1["ItemCode"].ToString()) + "<br>");
                        //Response.Write(oReader1["DeliveryDate"].ToString() + "<br>");
                        string CompanyId = getCompanyId(oReader1["CompanyCode"].ToString());
                        DateTime dt = DateTime.Now; //.AddDays(1); 
                        string dateNowStr = dt.ToString("yyyyMMdd");
                        //Response.Write(dateNowStr+"<br>");
                        string DeliveryDate = dateNowStr;
                        if (oReader1["DeliveryDate"].ToString() != "" && oReader1["DeliveryDate"].ToString() != "0")
                        {
                            DeliveryDate= oReader1["DeliveryDate"].ToString();
                        }

                        query2 = "IF NOT EXISTS (SELECT 1 FROM tblPR WHERE PRNo = @PRNo AND ItemCode=@ItemCode AND CompanyCode=@CompanyCode) BEGIN INSERT INTO tblPR (PRNo, PRLineNo, ItemCode, PRDescription, PRDate, Company, CompanyCode, SubCategory, DeliveryDate, UOM, Qty, UnitPrice, Currency, Groupname, Commodity, BuyerCode, Budget, Remarks, Workflow, Location) VALUES (@PRNo, @PRLineNo, @ItemCode, @PRDescription, @PRDate, @Company, @CompanyCode, @SubCategory, @DeliveryDate, @UOM, @Qty, @UnitPrice, @Currency, @Groupname, @Commodity, @BuyerCode, @Budget, @Remarks, @Workflow, @Location) END";
						using (conn2 = new SqlConnection(connstring))
						{
							using (cmd2 = new SqlCommand(query2, conn2))
							{
                                //Response.Write(getCompanyId(oReader1["CompanyCode"].ToString()));
								//cmd.Parameters.AddWithValue("@", Convert.ToInt32(oReader1[""].ToString()));
								//cmd.Parameters.AddWithValue("@", oReader1[""].ToString());
								//cmd2.Parameters.AddWithValue("@BuyerCode", Session["BuyerCode"].ToString());
								cmd2.Parameters.AddWithValue("@PRNo", Convert.ToInt32(oReader1["PRNo"].ToString().Replace("RQN","")));
								cmd2.Parameters.AddWithValue("@PRLineNo", Convert.ToInt32(oReader1["PRLineNo"].ToString()));
                                cmd2.Parameters.AddWithValue("@ItemCode", oReader1["ItemCode"].ToString());
                                cmd2.Parameters.AddWithValue("@PRDescription", oReader1["PRDescription"].ToString());
								cmd2.Parameters.AddWithValue("@PRDate", oReader1["PRDate"].ToString());
                                cmd2.Parameters.AddWithValue("@Company", getCompanyId(oReader1["CompanyCode"].ToString()));
                                cmd2.Parameters.AddWithValue("@CompanyCode", oReader1["CompanyCode"].ToString());
								cmd2.Parameters.AddWithValue("@SubCategory", oReader1["SubCategory"].ToString());
                                cmd2.Parameters.AddWithValue("@DeliveryDate", DeliveryDate);
								cmd2.Parameters.AddWithValue("@UOM", oReader1["UOM"].ToString());
								cmd2.Parameters.AddWithValue("@Qty", oReader1["Qty"].ToString()!="" ? Convert.ToDouble(oReader1["Qty"].ToString()):0);
								cmd2.Parameters.AddWithValue("@UnitPrice", oReader1["UnitPrice"].ToString()!="" ? Convert.ToDouble(oReader1["UnitPrice"].ToString()):0);
								cmd2.Parameters.AddWithValue("@Currency", oReader1["Currency"].ToString());
								cmd2.Parameters.AddWithValue("@Groupname", oReader1["Groupname"].ToString());
								cmd2.Parameters.AddWithValue("@Commodity", oReader1["Commodity"].ToString());
								cmd2.Parameters.AddWithValue("@BuyerCode", oReader1["BuyerCode"].ToString());
                                cmd2.Parameters.AddWithValue("@Budget", getBudget(oReader1["ItemCode"].ToString()));
                                cmd2.Parameters.AddWithValue("@Remarks", oReader1["COMMENT1"].ToString());
                                cmd2.Parameters.AddWithValue("@Workflow", oReader1["WORKFLOW"].ToString());
                                cmd2.Parameters.AddWithValue("@Location", oReader1["LOCATION"].ToString());
								conn2.Open(); cmd2.ExecuteNonQuery();
							}
						}
                    }
                }
            }
        }
        

            
            
     }

    protected void gvPR_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Details":
                {
                    Session["PrRefNo"] = e.CommandArgument.ToString();
                    Response.Redirect("prDetails.aspx");
                } break;
        }
    }
    protected void lnkSave_Click(object sender, EventArgs e)
    {
        DateTime DteNow = DateTime.Now;
        string GroupName = DteNow.ToString("yyyyMMddHHmmss");
        //Response.Write(GroupName);
        for (int i = 0; i < gvPR.Rows.Count; i++)
        {
            CheckBox chkPR = (CheckBox)gvPR.Rows[i].Cells[0].FindControl("CheckBox1");
            HiddenField PRRefNoFld = (HiddenField)gvPR.Rows[i].Cells[0].FindControl("HiddenField1");
            if (chkPR.Checked)
            {
                //Response.Write(PRRefNoFld.Value + " " + bidrefno.ToString()+"<br>");
                UpdatePR(int.Parse(PRRefNoFld.Value), GroupName);
            }
            // SaveBidEventItems();
        }
        int BidRefNo = int.Parse(CreateBidEventFromPRGroupName(GroupName));
        if (BidRefNo > 0)
        {
            Session[Constant.SESSION_BIDREFNO] = BidRefNo.ToString();
            Response.Redirect("createnewevent.aspx?" + Constant.QS_TASKTYPE + "=" + 2);
        }
        //Response.Write(a);
        //creatit(GroupName);
        
    }

    private void UpdatePR(int PrRefNo, string groupname)
    {
        SqlConnection sqlConn = new SqlConnection(connstring);
        SqlTransaction sqlTrans = null;

        try
        {
            sqlConn.Open();
            sqlTrans = sqlConn.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@PrRefNo", SqlDbType.Int);
            sqlParams[1] = new SqlParameter("@GroupName", SqlDbType.VarChar);

            sqlParams[0].Value = PrRefNo;
            sqlParams[1].Value = groupname;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "sp_UpdatePRGroup", sqlParams);
            sqlTrans.Commit();

            //lblMessage.Text = "Changes was saved successfully";
        }
        catch (Exception ex)
        {
            sqlTrans.Rollback();
            //lblMessage.Text = "Saving of changes was unsuccessful Error: " + ex.Message;
        }
    }


    private string CreateBidEventFromPRGroupName(string GroupNameStr)
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;
        string isSuccessful = string.Empty;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@GroupName", SqlDbType.VarChar);
            sqlParams[0].Value = GroupNameStr;
            sqlParams[1] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[1].Value = int.Parse(Session[Constant.SESSION_USERID].ToString());
            sqlParams[2] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.Output;

            //s3p_Ebid_InsertBidFromPRGroupName
            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_Ebid_InsertBidFromPRGroupName", sqlParams);

            sqlTransact.Commit();

            //Response.Write(sqlParams[0].Value.ToString().Trim());
            int r = int.Parse(sqlParams[2].Value.ToString().Trim());
            isSuccessful = (r <= 0 ? "0" : sqlParams[2].Value.ToString().Trim());
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            LogHelper.EventLogHelper.Log("Bid Event > View PR Create New Event : " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            sqlTransact.Rollback();
            isSuccessful = "0";
        }
        finally
        {
            sqlConnect.Close();
        }
        return isSuccessful;
    }


}