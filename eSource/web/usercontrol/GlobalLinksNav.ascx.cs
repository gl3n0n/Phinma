using System;
using System.Web;
using EBid.ConnectionString;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EBid.lib.constant;
using EBid.lib;

public partial class web_user_control_GlobalLinksNav : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
		FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();

        if ((Session[Constant.SESSION_USERTYPE] != null) && (Session[Constant.SESSION_USERTYPE].ToString() != ""))
        {
            btnSearch.Attributes.Add("OnClick", "ShowResults(" + Session[Constant.SESSION_USERTYPE].ToString().Trim() + ")");
            //Response.Write(Session[Constant.SESSION_USERTYPE].ToString().Trim());
            if (Session[Constant.SESSION_USERTYPE].ToString().Trim() == "1") //BUYER
            {
                //ddlSearchOpt.Items[0].Enabled = false;
                //ddlSearchOpt.Items[1].Enabled = false;
                //ddlSearchOpt.Items[2].Enabled = false;
                //ddlSearchOpt.Items[3].Enabled = false;
                //ddlSearchOpt.Items[4].Enabled = false;
                //ddlSearchOpt.Items[5].Enabled = false;
                //ddlSearchOpt.Items[6].Enabled = false;
                //ddlSearchOpt.Items[7].Enabled = false;
            }
            if (Session[Constant.SESSION_USERTYPE].ToString().Trim() == "2") //VENDOR
            {
                //ddlSearchOpt.Items[0].Enabled = false;
                //ddlSearchOpt.Items[1].Enabled = false;
                //ddlSearchOpt.Items[2].Enabled = false;
                //ddlSearchOpt.Items[3].Enabled = false;
                //ddlSearchOpt.Items[4].Enabled = false;
                //ddlSearchOpt.Items[5].Enabled = false;
                //ddlSearchOpt.Items[6].Enabled = false;
                ddlSearchOpt.Items[7].Enabled = false;
            }
            if (Session[Constant.SESSION_USERTYPE].ToString().Trim() == "3") //PURCHASING
            {
                //ddlSearchOpt.Items[0].Enabled = false;
                //ddlSearchOpt.Items[1].Enabled = false;
                //ddlSearchOpt.Items[2].Enabled = false;
                //ddlSearchOpt.Items[3].Enabled = false;
                //ddlSearchOpt.Items[4].Enabled = false;
                //ddlSearchOpt.Items[5].Enabled = false;
                //ddlSearchOpt.Items[6].Enabled = false;
                //ddlSearchOpt.Items[7].Enabled = false;
            }
            if (Session[Constant.SESSION_USERTYPE].ToString().Trim() == "5") //BOC
            {
                //ddlSearchOpt.Items[0].Enabled = false;
                //ddlSearchOpt.Items[1].Enabled = false;
                //ddlSearchOpt.Items[2].Enabled = false;
                ddlSearchOpt.Items[3].Enabled = false;
                ddlSearchOpt.Items[4].Enabled = false;
                ddlSearchOpt.Items[5].Enabled = false;
                //ddlSearchOpt.Items[6].Enabled = false;
                ddlSearchOpt.Items[7].Enabled = false;
            }
            if (Session[Constant.SESSION_USERTYPE].ToString().Trim() == "6") //BAC
            {
                //ddlSearchOpt.Items[0].Enabled = false;
                //ddlSearchOpt.Items[1].Enabled = false;
                //ddlSearchOpt.Items[2].Enabled = false;
                ddlSearchOpt.Items[3].Enabled = false;
                ddlSearchOpt.Items[4].Enabled = false;
                ddlSearchOpt.Items[5].Enabled = false;
                //ddlSearchOpt.Items[6].Enabled = false;
                ddlSearchOpt.Items[7].Enabled = false;
            }
        }
        else
        {
            HttpCookie cookie = Request.Cookies["tempCookie"];
            Session[Constant.SESSION_USERTYPE] = cookie.Values[Constant.SESSION_USERTYPE];
            btnSearch.Attributes.Add("OnClick", "ShowResults(" + Session[Constant.SESSION_USERTYPE].ToString().Trim() + ")");
        }        
    }    
}
