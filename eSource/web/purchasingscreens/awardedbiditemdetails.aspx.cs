using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EBid.lib.bid.data;
using EBid.lib.bid.trans;
using EBid.lib.user.trans;
using EBid.lib.constant;
using EBid.lib;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using EBid.ConnectionString;

public partial class web_purchasingscreens_awardedbiditemdetails : System.Web.UI.Page
{
    private string connstring = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
            Response.Redirect("../unauthorizedaccess.aspx");
        BindComments();


        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Awarded Bid Item Details");
    }
    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        if (Session[Constant.SESSION_LASTPAGE] != null)
            Response.Redirect(Session[Constant.SESSION_LASTPAGE].ToString());
        else
            Response.Redirect("bids.aspx");
    }

    #region Comments
    // bidtender comments usercontrol was not used because of some binding issues.
    // bidtender comments need to be rebinded when a tender is selected.
    public void BindComments()
    {
        if (Session[Constant.SESSION_BIDTENDERNO] != null)
        {
            DataSet dSet = new DataSet();

            SqlConnection sqlConnection = new SqlConnection(connstring);

            SqlCommand sqlCommand = new SqlCommand("sp_GetBidTenderComments", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@BidTenderNo", SqlDbType.Int);
            sqlCommand.Parameters[0].Value = int.Parse(Session[Constant.SESSION_BIDTENDERNO].ToString());
            sqlCommand.Parameters.Add("@UserType", SqlDbType.Int);
            sqlCommand.Parameters[1].Value = int.Parse(Session[Constant.SESSION_USERTYPE].ToString());

            SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);

            using (sqlConnection)
            {
                sqlAdapter.Fill(dSet);
                lblRecordCount.Text = dSet.Tables[0].Rows.Count.ToString();
                dSet = new DataSet();

                sqlAdapter.Fill(dSet, int.Parse(lblCurrentIndex.Text), int.Parse(lblPageSize.Text), "Comments");
                dlComments.DataSource = dSet.Tables["Comments"].DefaultView;
                dlComments.DataBind();
            }
            ShowCounts();
        }
    }

    protected void ShowCounts()
    {
        int recordCount = int.Parse(lblRecordCount.Text);
        int pageSize = int.Parse(lblPageSize.Text);
        int currentIndex = int.Parse(lblCurrentIndex.Text);

        if (recordCount > 0)
        {
            lblCounts.Text = (currentIndex + 1) + " to ";
            if ((currentIndex + pageSize) <= recordCount)
                lblCounts.Text += (currentIndex + pageSize);
            else
                lblCounts.Text += recordCount;

            lblCounts.Text += " out of " + recordCount + " comments";
            // show or hide page buttons        
            trPagers.Visible = (recordCount > pageSize);
        }
        else
        {
            lblCounts.Text = "No Comments";
            trPagers.Visible = false;
        }
    }

    protected void btnFirstPage_Click(object sender, ImageClickEventArgs e)
    {
        lblCurrentIndex.Text = "0";
        BindComments();
    }

    protected void btnPreviousPage_Click(object sender, ImageClickEventArgs e)
    {
        lblCurrentIndex.Text = (int.Parse(lblCurrentIndex.Text) - int.Parse(lblPageSize.Text)) + "";
        if (int.Parse(lblCurrentIndex.Text) < 0)
            lblCurrentIndex.Text = "0";
        BindComments();
    }

    protected void btnNextPage_Click(object sender, ImageClickEventArgs e)
    {
        if ((int.Parse(lblCurrentIndex.Text) + int.Parse(lblPageSize.Text)) < int.Parse(lblRecordCount.Text))
            lblCurrentIndex.Text = (int.Parse(lblCurrentIndex.Text) + int.Parse(lblPageSize.Text)) + "";
        BindComments();
    }

    protected void btnLastPage_Click(object sender, ImageClickEventArgs e)
    {
        int intMod = int.Parse(lblRecordCount.Text) % int.Parse(lblPageSize.Text);

        if (intMod > 0)
            lblCurrentIndex.Text = (int.Parse(lblRecordCount.Text) - intMod) + "";
        else
            lblCurrentIndex.Text = (int.Parse(lblRecordCount.Text) - int.Parse(lblPageSize.Text)) + "";
        BindComments();
    }
    #endregion

}
