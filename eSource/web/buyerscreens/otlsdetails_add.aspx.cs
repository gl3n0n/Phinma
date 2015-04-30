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
using System.Text;
using System.Data.SqlClient;
using EBid.ConnectionString;

public partial class web_otlsdetails_add : System.Web.UI.Page
{
    private string connstring = "";

    protected void Page_Load(object sender, EventArgs e)
    {
		connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString[Constant.QS_CATEGORYID]))
            {
                ViewState[Constant.QS_CATEGORYID] = Request.QueryString[Constant.QS_CATEGORYID];
                ViewState[Constant.QS_SUBCATEGORYID] = Request.QueryString[Constant.QS_SUBCATEGORYID];

                CategoryTransaction ct = new CategoryTransaction();
                lblCategory.Text = ct.GetCategoryNameById(connstring, Request.QueryString[Constant.QS_CATEGORYID]);
                SubCategory st = new SubCategory();
                lblSubCategory.Text = st.GetSubCategoryNameById(connstring, Request.QueryString[Constant.QS_SUBCATEGORYID]).Trim() == "" ? "ALL" : st.GetSubCategoryNameById(Request.QueryString[Constant.QS_SUBCATEGORYID]);

            }
            PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Add One-Time Supplier");
            
        }
    }

    private void ClearFields()
    {
        tbUserName.Text = "";
        txtVendorName.Text = "";
        txtAddress1.Text = "";
        txtAddress2.Text = "";
        txtContactPerson.Text = "";
        txtEmailAddress.Text = "";
        txtTelephoneNumber.Text = "";
        //txtMobileNo.Text = "";
    }

    protected void lnkOK_Click(object sender, EventArgs e)
    {
        SupplierTransaction supp = new SupplierTransaction();
        OtherTransaction oth = new OtherTransaction();

        if (!(supp.VendorExists(oth.Replace(txtVendorName.Text.Trim()))))
        {
            string vUserName = tbUserName.Text.Replace(" ", "");
            //if (vUserName.Length > 8)
            //    vUserName = vUserName.Substring(0, 8).ToUpper();
            //else
            //    vUserName = vUserName.ToUpper();

            if (supp.CheckUser(tbUserName.Text.Trim()))
            {
                string randomPwd = RandomPasswordGenerator.GenerateRandomPassword();

                int vVendorId = supp.InsertUser(connstring, vUserName, EncryptionHelper.Encrypt(randomPwd), ((int)Constant.USERTYPE.VENDOR).ToString().Trim());
                ViewState["sUserID"] = vVendorId;

                web_usercontrol_mobileno uctrlMobileNo1 = (web_usercontrol_mobileno)FindControl("uctrlMobileNo");
                
                supp.InsertOneTimeSupplier(vVendorId.ToString().Trim(), txtVendorName.Text.Trim(),
                     ((int)Constant.SUPPLIERTYPE.OneTimeSupplier).ToString().Trim(),
                     txtEmailAddress.Text.Trim(),
                    txtAddress1.Text.Trim(),
                     txtAddress2.Text.Trim(),
                     txtContactPerson.Text.Trim(),
                     txtTelephoneNumber.Text.Trim(), uctrlMobileNo1.MobileNumber);

                supp.SaveCategoriesAndSubCategories(vVendorId, ViewState[Constant.QS_CATEGORYID].ToString(), ViewState[Constant.QS_SUBCATEGORYID].ToString());

                string emailOk = "";
                if (SendEmail())
                    emailOk = " and notified";
                lblMessage.Text = txtVendorName.Text.Trim() + " has been successfully added" + emailOk + ".";
                lnkClose.Attributes.Add("onclick", "AddSuppliersB('" + vVendorId + "', '" + txtVendorName.Text.Trim() + "');");
                DisableFields();

            }
            else
            {
                lblMessage.Text = tbUserName.Text.Trim() + " already exists.";
            }
        }
        else
        {
            lblMessage.Text = txtVendorName.Text.Trim() + " already exists in the vendor list.";

        }
        
    }    

    private bool SendEmail()
    {
        bool success = false;
        string fullname = txtVendorName.Text.Trim();
        string subject;
       
        subject = "Trans-Asia  : User Profile Created";

        if(MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                MailHelper.ChangeToFriendlyName(ConfigurationManager.AppSettings["AdminEmailName"], ConfigurationManager.AppSettings["AdminEmailAddress"]),
                MailHelper.ChangeToFriendlyName(fullname, txtEmailAddress.Text.Trim()),
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
        sb.Append("<b>TO&nbsp;&nbsp;:&nbsp;&nbsp;<u>" + txtVendorName.Text.Trim() + "</u></b>");
        sb.Append("<br /><br /><br />");
        sb.Append("Good Day!");
        sb.Append("<br /><br />");
        sb.Append("Your profile has been successfully created.");
        sb.Append("<br />&nbsp;&nbsp;&nbsp;Username: " + tbUserName.Text.Trim());
        sb.Append("<br />&nbsp;&nbsp;&nbsp;Password: " + HttpUtility.HtmlEncode(EncryptionHelper.Decrypt(GetUserPassword(Int32.Parse(ViewState["sUserID"].ToString().Trim())))));
        sb.Append("<br /><br />");
        sb.Append("You can access the site using <a href='" + ConfigurationManager.AppSettings["ServerUrl"] + "' target='_blank'>https://e-sourcing.Trans-Asia.com.ph</a>");
        sb.Append("<br /><br /><br />");
        sb.Append("Sincerely Yours,");
        sb.Append("<br /><br />");
        sb.Append("</p>");
        sb.Append("</td>");
        sb.Append("</tr>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }

    private string GetUserPassword(int userid)
    {
        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@UserID", SqlDbType.Int);
        sqlParams[0].Value = userid;

        string s = (string)SqlHelper.ExecuteScalar(connstring, "s3p_EBid_GetUserPassword", sqlParams);

        return s;
    }
    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        Session["vendorid"] = null;
        Session["vendorname"] = null;
    }

    private void DisableFields()
    {
        tbUserName.Enabled = false;
        txtVendorName.Enabled = false;
        txtAddress1.Enabled = false;
        txtAddress2.Enabled = false;
        txtContactPerson.Enabled = false;
        txtTelephoneNumber.Enabled = false;
        txtEmailAddress.Enabled = false;
        lnkOK.Visible = false;
        lnkCancel.Visible = false;
        lnkClose.Visible = true;
    }

}
