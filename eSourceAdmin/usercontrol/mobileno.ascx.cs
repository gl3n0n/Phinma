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

public partial class usercontrol_mobileno : System.Web.UI.UserControl
{
    private string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["EBidConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateMobilePrefix();
            txtMobileNo.Attributes.Add("onkeydown", "return DigitsOnly(event);");
        }
    }

    private void PopulateMobilePrefix()
    {
        ddlMobilePrefix.Items.Insert(0, "");

        string[] s = ValidMobilePrefixes;

        for (int i = 0; i < s.Length; i++)
        {
            ddlMobilePrefix.Items.Add(new ListItem(prefix(i)));
        }

    }
    private string prefix(int p)
    {
        string[] s = ValidMobilePrefixes;

        if (p >= 0 && p < s.Length) return s[p];
        return null;

    }

    private static string[] ValidMobilePrefixes
    {
        get
        {
            string[] validPrefixes = ConfigurationManager.AppSettings["ValidMobilePhonePrefixes"].Trim().Split(new char[] { '|' });
            return validPrefixes;
        }
    }

    public string MobileNumber
    {
        get
        {
            return String.Format("{0}{1}", ddlMobilePrefix.SelectedValue, txtMobileNo.Text);
        }
    }
}

