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
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using EBid.lib;
using EBid.lib.constant;

public partial class web_usercontrol_commentlist : System.Web.UI.UserControl
{
    private string connstring = "";
    private SqlConnection sqlConnection;
    private SqlDataAdapter sqlAdapter;
    private SqlCommand sqlCommand;

    protected void Page_Load(object sender, EventArgs e)
    {
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (!IsPostBack)
        {
            lblPageSize.Text = ConfigurationManager.AppSettings["CommentsPerPage"];
            Bind();
        }
    }

    private void Bind()
    {
        DataSet dSet = new DataSet();

        sqlConnection = new SqlConnection(connstring);

        sqlCommand = new SqlCommand("sp_GetBidEventComments", sqlConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.Parameters.Add("@BidRefNo", SqlDbType.Int);
        if (Session[Constant.SESSION_BIDREFNO] != null)
            sqlCommand.Parameters[0].Value = int.Parse(Session[Constant.SESSION_BIDREFNO].ToString());
        else
            sqlCommand.Parameters[0].Value = 0;
        sqlCommand.Parameters.Add("@UserType", SqlDbType.Int);
        sqlCommand.Parameters[1].Value = Session[Constant.SESSION_USERTYPE].ToString();
        sqlCommand.Parameters.Add("@VendorId", SqlDbType.Int);
        sqlCommand.Parameters[2].Value = Session[Constant.SESSION_USERID].ToString();

        sqlAdapter = new SqlDataAdapter(sqlCommand);

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
        Bind();
    }
    
    protected void btnPreviousPage_Click(object sender, ImageClickEventArgs e)
    {
        lblCurrentIndex.Text = (int.Parse(lblCurrentIndex.Text) - int.Parse(lblPageSize.Text)) + "";
        if (int.Parse(lblCurrentIndex.Text) < 0)
            lblCurrentIndex.Text = "0";
        Bind();
    }
    
    protected void btnNextPage_Click(object sender, ImageClickEventArgs e)
    {
        if ((int.Parse(lblCurrentIndex.Text) + int.Parse(lblPageSize.Text)) < int.Parse(lblRecordCount.Text))
            lblCurrentIndex.Text = (int.Parse(lblCurrentIndex.Text) + int.Parse(lblPageSize.Text)) + "";
        Bind();
    }
    
    protected void btnLastPage_Click(object sender, ImageClickEventArgs e)
    {
        int intMod = int.Parse(lblRecordCount.Text) % int.Parse(lblPageSize.Text);

        if (intMod > 0)
            lblCurrentIndex.Text = (int.Parse(lblRecordCount.Text) - intMod) + "";
        else
            lblCurrentIndex.Text = (int.Parse(lblRecordCount.Text) - int.Parse(lblPageSize.Text)) + "";
        Bind();
    }
}
