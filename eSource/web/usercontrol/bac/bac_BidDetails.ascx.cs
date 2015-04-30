using System;
using System.Web;
using EBid.ConnectionString;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using EBid.lib;
using System.Data.SqlClient;

public partial class web_usercontrol_bac_BidDetails : System.Web.UI.UserControl
{
    // private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
	if (Request.QueryString["brn"] != null)
		Session["BuyerBacRefNo"] = HttpContext.Current.Request.QueryString["brn"];
        // main
        string sCommand = "SELECT Budgeted, CompanyID FROM tblBACBidItems WHERE BacRefNo=" + Session["BuyerBacRefNo"];
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        //connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        SqlDataReader oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            string sBudgeted = oReader["Budgeted"].ToString();
            switch (sBudgeted)
            {
                case "0": UnBudgeted.Checked = true; break;
                case "1": Budgeted.Checked = true; break;
            }
            string sCompanyID = oReader["CompanyId"].ToString();
            switch (sCompanyID)
            {
                case "0": CompanyIdGT.Checked = true; break;
                case "1": CompanyIdIC.Checked = true; break;
                case "2": CompanyIdGXI.Checked = true; break;
                case "3": CompanyIdEGG.Checked = true; break;
            }
        }
        oReader.Close(); 
    }
}