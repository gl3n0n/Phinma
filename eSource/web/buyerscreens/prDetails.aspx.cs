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
using EBid.lib.constant;
using EBid.lib;
using EBid.ConnectionString;
using System.Text.RegularExpressions;
using System.Data.Sql;
using System.Data.SqlClient;

public partial class web_buyerscreens_prDetails : System.Web.UI.Page
{
    private string connstring = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        //connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;
        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "View PR");

    }

    //BACK ACTION
    protected void lnkBack_Click(object sender, EventArgs e)
    {
       
            Response.Redirect("viewPR.aspx");
    }

    //SAVE CHANGES ACTION
    protected void lnkSave_Click(object sender, EventArgs e)
    {
        //Label lbPRNo = (Label)DetailsView1.FooterRow.FindControl("lblPRNo");
        //Label lbPRLineNo = (Label)DetailsView1.FooterRow.FindControl("lblPRLineNo");
        //Label lbPRDate = (Label)DetailsView1.FooterRow.FindControl("lblPRDate");
        //TextBox tbItemCode = (TextBox)DetailsView1.FooterRow.FindControl("txtItemCode");
        //TextBox tbPRDescription = (TextBox)DetailsView1.FooterRow.FindControl("txtPRDescription");
        //TextBox tbDeliveryDate = (TextBox)DetailsView1.FooterRow.FindControl("txtDeliveryDate");
        //TextBox tbUOM = (TextBox)DetailsView1.FooterRow.FindControl("txtUOM");
        //TextBox tbQty = (TextBox)DetailsView1.FooterRow.FindControl("txtQty");
        //TextBox tbUnitPrice = (TextBox)DetailsView1.FooterRow.FindControl("txtUnitPrice");
        //TextBox tbCurrency = (TextBox)DetailsView1.FooterRow.FindControl("txtCurrency");
        //TextBox tbCommodity = (TextBox)DetailsView1.FooterRow.FindControl("txtCommodity");
        //TextBox tbGroupName = (TextBox)DetailsView1.FooterRow.FindControl("txtGroupName");
        //Label lbPRNo = (Label)DetailsView1.FooterRow.FindControl("lblPRNo");

        //Response.Write(lbPRNo.Text.ToString());

        //UpdatePR(int.Parse(lbPRNo.Text.ToString()), tbGroupName.Text.ToString());
    }


    //UPDATE PR
    //private void UpdatePR(int prno, string groupname)
    //{
    //    SqlConnection sqlConn = new SqlConnection(connstring);
    //    SqlTransaction sqlTrans = null;

    //    try
    //    {
    //        sqlConn.Open();
    //        sqlTrans = sqlConn.BeginTransaction();

    //        SqlParameter[] sqlParams = new SqlParameter[2];
    //        sqlParams[0] = new SqlParameter("@PRNo", SqlDbType.Int);
    //        sqlParams[1] = new SqlParameter("@GroupName", SqlDbType.VarChar);

    //        sqlParams[0].Value = prno;
    //        sqlParams[1].Value = groupname;

    //        SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "sp_UpdatePRGroup", sqlParams);
    //        sqlTrans.Commit();

    //        lblMessage.Text = "Changes was saved successfully";
    //    }
    //    catch (Exception ex)
    //    {
    //        sqlTrans.Rollback();
    //        lblMessage.Text = "Saving of changes was unsuccessful Error: " + ex.Message;
    //    }
    //}
}