using System;
using System.Web;
using EBid.ConnectionString;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EBid.lib;
using EBid.lib.constant;

public partial class web_usercontrol_bac_bac_purchasingApprover : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //string sCommand;
        //string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        //SqlDataReader oReader;

        //// prepared date
        //sCommand = "SELECT DateSubmitted as PreparedDt, ApprovedDt_0 AS ApprovedDt FROM tblBACBidItems WHERE BacRefNo=" + Session["BuyerBacRefNo"] + " AND Status=1";
        //oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        //if (oReader.HasRows)
        //{
        //    oReader.Read();
        //    //System.Web.UI.WebControls.Literal lblPreparedDate1;
        //    //System.Web.UI.WebControls.Literal lblApprovedDate1;

        //    //lblPreparedDate1 = bac_purchasingApprover1.FindControl("lblPreparedDate");
        //    //lblApprovedDate1 = (System.Web.UI.WebControls.Literal)bac_purchasingApprover1.FindControl("lblApprovedDt");
        //    DateTime date1 = DateTime.Parse(oReader["PreparedDt"].ToString());
        //    lblPreparedDate.Text = String.Format("{0:MMMM dd, yyyy}", date1);
        //    DateTime date2 = DateTime.Parse(oReader["ApprovedDt"].ToString());
        //    lblApprovedDt.Text = String.Format("{0:MMMM dd, yyyy}", date2);


        //}
        //oReader.Close();

    }
    protected void Page_LoadComplete(object sender, EventArgs e)
    {
       
    }
}