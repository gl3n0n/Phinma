using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using EBid.lib;
using EBid.lib.constant;
using EBid.lib.utils;

public partial class admin_editbac : System.Web.UI.Page
{
    private string connstring;
  
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = HttpContext.Current.Session["ConnectionString"].ToString();

        if (!(IsPostBack))
        {
            if ((Request.QueryString["userID"] != null) && (Request.QueryString["userType"] != null))
            {
                ViewState["sUserID"] = Request.QueryString["userID"];
                ViewState["sUserType"] = Request.QueryString["userType"];

                FillTextboxData();
                FillBOCDetails();
            }
            else
                Response.Redirect("users.aspx");
                
        }
    }

    private void FillBOCDetails()
    {
        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@userID", SqlDbType.Int);
        sqlParams[0].Value = Int32.Parse(ViewState["sUserID"].ToString().Trim());

        DataTable dtBOCDetails = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "sp_QueryBACDetails", sqlParams).Tables[0];

        if (dtBOCDetails.Rows.Count > 0)
        {
            tbUserName.Text = dtBOCDetails.Rows[0]["UserName"].ToString().Trim();
            tbFirstName.Text = dtBOCDetails.Rows[0]["FirstName"].ToString().Trim();
            tbLastName.Text = dtBOCDetails.Rows[0]["LastName"].ToString().Trim();
            tbMidInitial.Text = dtBOCDetails.Rows[0]["MiddleName"].ToString().Trim();
            tbEmail.Text = dtBOCDetails.Rows[0]["EmailAdd"].ToString().Trim();
            ApprovingLimitOnLowestPrice.Text = dtBOCDetails.Rows[0]["ApprovingLimitOnLowestPrice"].ToString().Trim();
            ApprovingLimitOnLowestPriceTo.Text = dtBOCDetails.Rows[0]["ApprovingLimitOnLowestPriceTo"].ToString().Trim();
            ApprovingLimitOnNonLowestPrice.Text = dtBOCDetails.Rows[0]["ApprovingLimitOnNonLowestPrice"].ToString().Trim();
            ApprovingLimitOnNonLowestPriceTo.Text = dtBOCDetails.Rows[0]["ApprovingLimitOnNonLowestPriceTo"].ToString().Trim();
            Committee.SelectedValue = dtBOCDetails.Rows[0]["Committee"].ToString().Trim();
            Approver.SelectedValue = dtBOCDetails.Rows[0]["Approver"].ToString().Trim();
            OIC.SelectedValue = dtBOCDetails.Rows[0]["OIC"].ToString().Trim();
            initOIC.Value = dtBOCDetails.Rows[0]["OIC"].ToString().Trim();
            
        }
    }

    private bool UpdateBOCDetails()
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;
        bool success = false;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[11];
            sqlParams[0] = new SqlParameter("@UserId", SqlDbType.Int);
            sqlParams[1] = new SqlParameter("@FirstName", SqlDbType.VarChar);
            sqlParams[2] = new SqlParameter("@LastName", SqlDbType.VarChar);
            sqlParams[3] = new SqlParameter("@MidName", SqlDbType.VarChar);
            sqlParams[4] = new SqlParameter("@ApprovingLimitOnLowestPrice", SqlDbType.Float);
            sqlParams[5] = new SqlParameter("@ApprovingLimitOnLowestPriceTo", SqlDbType.Float);
            sqlParams[6] = new SqlParameter("@ApprovingLimitOnNonLowestPrice", SqlDbType.Float);
            sqlParams[7] = new SqlParameter("@ApprovingLimitOnNonLowestPriceTo", SqlDbType.Float);
            sqlParams[8] = new SqlParameter("@Committee", SqlDbType.Int);
            sqlParams[9] = new SqlParameter("@Approver", SqlDbType.Int);
            sqlParams[10] = new SqlParameter("@OIC", SqlDbType.Int);
 
            sqlParams[0].Value = Int32.Parse(ViewState["sUserID"].ToString().Trim());
            sqlParams[1].Value = tbFirstName.Text.Trim();
            sqlParams[2].Value = tbLastName.Text.Trim();
            sqlParams[3].Value = tbMidInitial.Text.Trim();
            sqlParams[4].Value = Double.Parse(ApprovingLimitOnLowestPrice.Text.Trim());
            sqlParams[5].Value = Double.Parse(ApprovingLimitOnLowestPriceTo.Text.Trim());
            sqlParams[6].Value = Double.Parse(ApprovingLimitOnNonLowestPrice.Text.Trim());
            sqlParams[7].Value = Double.Parse(ApprovingLimitOnNonLowestPriceTo.Text.Trim());
            sqlParams[8].Value = Int32.Parse(Committee.SelectedValue);
            sqlParams[9].Value = Int32.Parse(Approver.SelectedValue);
            sqlParams[10].Value = Int32.Parse(OIC.SelectedValue);

            SqlHelper.ExecuteNonQuery(sqlTransact, "sp_UpdateBacDetails", sqlParams);

            sqlTransact.Commit();

            success = true;
        }
        catch (Exception ex)
        {

            sqlTransact.Rollback();
            success = false;
            Response.Write(ex.ToString() +"<br>");
        }
        finally
        {
            sqlConnect.Close();

        }
        //Response.Write(success);
        return success;
    }

    private bool UpdateUserData()
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;
        bool success = false;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@userID", SqlDbType.Int);
            sqlParams[1] = new SqlParameter("@userType", SqlDbType.Int);
            sqlParams[2] = new SqlParameter("@userName", SqlDbType.Int);
            sqlParams[3] = new SqlParameter("@password", SqlDbType.Int);
            sqlParams[4] = new SqlParameter("@email", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(ViewState["sUserID"].ToString().Trim());
            sqlParams[1].Value = Int32.Parse(ViewState["sUserType"].ToString().Trim());
            sqlParams[2].Value = tbUserName.Text.Trim();
            sqlParams[3].Value = ViewState["BOCPwd"].ToString();//EncryptionHelper.Encrypt(tbPassword.Attributes["value"]);
            sqlParams[4].Value = tbEmail.Text.Trim();

            int tempInt = Convert.ToInt32(SqlHelper.ExecuteScalar(sqlTransact, "s3p_EBid_UpdateSpecificUser", sqlParams));

            sqlTransact.Commit();

            if (tempInt == 1)
            {
                success = false;
            }
            else
            {
                success = true;
            }
        }
        catch (Exception ex)
        {
            sqlTransact.Rollback();
            success = false; Response.Write(ex.ToString() + "<br>");
        }
        finally
        {
            sqlConnect.Close();
        }

        if (success)
            success = UpdateBOCDetails();

        return success;
    }

    private void FillTextboxData()
    {
        SqlParameter[] sqlParams = new SqlParameter[2];
        sqlParams[0] = new SqlParameter("@userID", SqlDbType.Int);
        sqlParams[1] = new SqlParameter("@userType", SqlDbType.Int);
        sqlParams[0].Value = Int32.Parse(ViewState["sUserID"].ToString().Trim());
        sqlParams[1].Value = Int32.Parse(ViewState["sUserType"].ToString().Trim());

        DataTable dtUserData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_ViewSpecificUser", sqlParams).Tables[0];

        if (dtUserData.Rows.Count > 0)
        {
            tbUserName.Text = dtUserData.Rows[0]["UserName"].ToString().Trim();
            ViewState["BOCPwd"] = dtUserData.Rows[0]["UserPassword"].ToString().Trim();
            tbEmail.Text = dtUserData.Rows[0]["EmailAdd"].ToString().Trim();
        }
    }

    protected void lnkSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (UpdateUserData())
            {
                SendEmail();
                Response.Redirect("users.aspx");
            }
        }
    }

    private string ToAsterisk(string input)
    {
        return "";
    }

    #region Email
    private bool SendEmail()
    {
        bool success = false;
        string fullname = tbFirstName.Text.Trim() + " " + tbLastName.Text.Trim();

        string subject = "Trans-Asias : User Profile Changed";

        if (MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                MailHelper.ChangeToFriendlyName(ConfigurationManager.AppSettings["AdminEmailName"], ConfigurationManager.AppSettings["AdminEmailAddress"]),
                MailHelper.ChangeToFriendlyName(fullname, tbEmail.Text.Trim()),
                subject,
                CreateBody(),
                MailTemplate.GetTemplateLinkedResources(this)))
            success = true;

        return success;
    }

    private string CreateBody()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<tr>");
        sb.Append("<td valign='top'>");
        sb.Append("<p>");
        sb.Append("<br /><b>" + DateTime.Now.ToLongDateString() + "</b><br />");
        sb.Append("<b>TO&nbsp&nbsp;:&nbsp&nbsp;<u>" + tbFirstName.Text.Trim() + " " + tbLastName.Text.Trim() + "</u></b>");
        sb.Append("<br /><br /><br />");
        sb.Append("Good Day!");
        sb.Append("<br /><br />");
        sb.Append("Your profile has been successfully changed.");
        sb.Append("<br />&nbsp;&nbsp;&nbsp;Username: " + tbUserName.Text.Trim());
        sb.Append("<br />&nbsp;&nbsp;&nbsp;Password: " + HttpUtility.HtmlEncode(EncryptionHelper.Decrypt(GetUserPassword(Int32.Parse(ViewState["sUserID"].ToString().Trim())))));
        sb.Append("<br /><br /><br />");
        sb.Append("Sincerely Yours,");
        sb.Append("<br /><br />");
        sb.Append(ConfigurationManager.AppSettings["AdminEmailName"]);
        sb.Append("<br /><br />");
        sb.Append("</p>");
        sb.Append("</td>");
        sb.Append("</tr>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }

    public string GetUserPassword(int userid)
    {
        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@UserID", SqlDbType.Int);
        sqlParams[0].Value = userid;

        string s = (string)SqlHelper.ExecuteScalar(connstring, "s3p_EBid_GetUserPassword", sqlParams);

        return s;
    }
    #endregion
}