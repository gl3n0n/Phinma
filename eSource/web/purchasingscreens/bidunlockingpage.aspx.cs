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
using EBid.lib;
using EBid.lib.user.trans;
using EBid.lib.constant;
using EBid.ConnectionString;

public partial class web_purchasing_screens_BidUnlockingPage : System.Web.UI.Page
{
    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (!(Page.IsPostBack))
        {
            if (Session[Constant.SESSION_USERID] != null)
            {
                ViewState[Constant.SESSION_USERID] = Session[Constant.SESSION_USERID].ToString().Trim();
                if (Session["message"] != null)
                {
                    txtNote.Visible = true;
                    txtNote.Text = Session["message"].ToString();
                }
                else
                    txtNote.Visible = false;

                if (Session[Constant.SESSION_PASSWORD] != null)
                {
                    ViewState[Constant.SESSION_PASSWORD] = Session[Constant.SESSION_PASSWORD].ToString();
                }
                else
                {
                    HttpCookie cookie = Request.Cookies["tempCookie"];
                    if (cookie.Values[Constant.SESSION_PASSWORD] != null)
                        Session[Constant.SESSION_PASSWORD] = cookie.Values[Constant.SESSION_PASSWORD];
                }

                if(Session["AuthDepID"] != null)
                {
                    ViewState["AuthDepID"] = Session["AuthDepID"].ToString().Trim();
                }

                Session.Remove("message");
                BidForOpenDataGet();
                PurchTableDataGet();
            }
            
        }
    }

    protected void BidForOpenDataGet()
    {
        PurchasingTransaction bid = new PurchasingTransaction();
        //Response.Write(Session["AuthDepID"].ToString());
        try
        {
            DataTable dt = bid.QuerySingleBidTenderStatus(connstring, ViewState["AuthDepID"].ToString());
            DataView dv = new DataView(dt);
            GridView1.DataSource = dv;
            GridView1.DataBind();
        }
        catch (System.NullReferenceException)
        {
            Response.Redirect("bidsforopening.aspx");
        }
    }

    protected void PurchTableDataGet()
    {
        PurchasingTransaction bid = new PurchasingTransaction();
        if (ViewState[Constant.SESSION_USERID] != null)
        {
            DataTable dt = bid.QueryPurchasingTable(connstring, ViewState[Constant.SESSION_USERID].ToString().Trim());
            DataView dv = new DataView(dt);
            Gv1.DataSource = dv;
            Gv1.DataBind();
        }
    }

    
    private bool CheckPassword()
    {
        string password = null;

        foreach(GridViewRow gvr in Gv1.Rows)
        {
            password = ((TextBox)gvr.FindControl("txtPassword")).Text;
        }

        if (password == EncryptionHelper.Decrypt(ViewState[Constant.SESSION_PASSWORD].ToString().Trim()))
            return true;

        return false;
    }

    protected void Back_Btn_Click(object sender, EventArgs e)
    {
        Response.Redirect("bidsforopening.aspx");
    }

    protected void Unlock_Btn_Click(object sender, EventArgs e)
    {
        string dept=null;
        string bidTenderGenNo = ViewState["AuthDepID"].ToString();

        foreach (GridViewRow gvr in Gv1.Rows)
        {
            dept = ((Label)gvr.FindControl("lblDeptID")).Text;
        }

        if (CheckPassword())
        {
            PurchasingTransaction bid = new PurchasingTransaction();
            bid.UpdateAuthTable(connstring, dept, bidTenderGenNo);
            Response.Redirect("bidunlockingpage.aspx");
        }
        else
        {
            Session["message"] = "Incorrect Password, Please Try Again.";
            Response.Redirect("bidunlockingpage.aspx", true);
        }
     }
}
