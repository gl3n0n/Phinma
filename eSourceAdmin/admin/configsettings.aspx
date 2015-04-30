<%@ Page language="C#" %>
<%@ Import Namespace="System.Configuration" %>
<%@ Import Namespace="System.Web.Configuration" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="System.Collections" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="EBid" %>
<%@ Import Namespace="EBid.lib" %>
<%@ Import Namespace="System.Data.Sql" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.IO" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminTopNav" Src="~/usercontrol/admin/adminTopNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Admin_TopNav_Home" Src="~/usercontrol/admin/Admin_TopNav_Home.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminLeftNavUser" Src="~/usercontrol/admin/AdminLeftNavUser.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminLeftNav" Src="~/usercontrol/admin/AdminLeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/usercontrol/Footer.ascx" %>

<script runat="server" language="C#">
private string connstring = "";
	public void Page_Load(object source, EventArgs e)
	{
        connstring = ConfigurationManager.ConnectionStrings["EBIDConnectionString"].ConnectionString;
        
		if (!IsPostBack)
		{
			UpdateUI();
            PopulateFields();		
		}
		if (Session["Message"] != null)
		{
			lblMessage.Text = Session["Message"].ToString();
			Session["Message"] = null;
		}
        
	}

    void PopulateFields()
    {
        connstring = ConfigurationManager.ConnectionStrings["EBIDConnectionString"].ConnectionString;
        string sCommand;

        sCommand = "SELECT * FROM tblClients WHERE clientid=" + HttpContext.Current.Session["clientid"].ToString();
        SqlDataReader oReader;
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            configCompanyName.Text = oReader["configCompanyName"].ToString();
            contactName.Text = oReader["contactName"].ToString();
            contactEmail.Text = oReader["contactEmail"].ToString();
            contactNumber.Text = oReader["contactNumber"].ToString();
            AdminEmailName.Text = oReader["AdminEmailName"].ToString();
            AdminEmailAddress.Text = oReader["AdminEmailAddress"].ToString();
            
        }
        oReader.Close();
        string ImgUrl = "/clients/" + HttpContext.Current.Session["clientid"] + "/images/logo.jpg";
        string ImgUrlAbs = @"C:\\EBID\clients\" + HttpContext.Current.Session["clientid"] + @"\images\logo.jpg";
        if (File.Exists(ImgUrlAbs))
        {
            configLogoLbl.Text = "<a href='/clients/" + HttpContext.Current.Session["clientid"].ToString() + "/images/logo.jpg' target='_blank'>View</a>";
        }
        else
        {
            configLogoLbl.Text = "/images/logo.JPG";
        }
    }

	void Save_OnClick(Object source, EventArgs e)
	{
        
        string query;
        SqlCommand cmd;
        SqlConnection conn;
        connstring = ConfigurationManager.ConnectionStrings["EBIDConnectionString"].ConnectionString;

        query = "UPDATE tblClients SET contactName=@contactName, contactEmail=@contactEmail, contactNumber=@contactNumber, configTheme=@configTheme, AdminEmailName=@AdminEmailName, AdminEmailAddress=@AdminEmailAddress, dateModified=getdate() WHERE clientid=@clientid";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@contactName", Request.Form["contactName"].ToString());
                cmd.Parameters.AddWithValue("@contactEmail", Request.Form["contactEmail"].ToString());
                cmd.Parameters.AddWithValue("@contactNumber", Request.Form["contactNumber"].ToString());
                cmd.Parameters.AddWithValue("@configTheme", Request.Form["configTheme"].ToString());
                cmd.Parameters.AddWithValue("@AdminEmailName", Request.Form["AdminEmailName"].ToString());
                cmd.Parameters.AddWithValue("@AdminEmailAddress", Request.Form["AdminEmailAddress"].ToString());
                cmd.Parameters.AddWithValue("@clientid", Convert.ToInt32(HttpContext.Current.Session["clientid"].ToString()));
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }
        //PopulateFields();
        Response.Redirect("../logout.aspx");
	}

	void UpdateUI()
	{
        //Configuration config = WebConfigurationManager.OpenWebConfiguration(Request.ApplicationPath);
        //ConfigurationSection section = config.Sections["connectionStrings"];

        //// Set protect button appropriately.
        //if (section.SectionInformation.IsProtected)
        //{
        //    rbtnProtected.Checked = true;
        //}
        //else
        //{
        //    rbtnUnprotected.Checked = true;
        //}
	}

    protected void UploadLogo(object sender, EventArgs e)
    {
        if (configLogo.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(configLogo.FileName);

            if (fileExt == ".jpg" || fileExt == ".JPG")
            {
                try
                {
                        string logoPath =  "/clients/" + HttpContext.Current.Session["clientid"].ToString() + "/images/logo.jpg";
                        File.Delete(Server.MapPath(logoPath));
                        configLogo.SaveAs(Server.MapPath(logoPath));
                    Label1.Text = "File uploaded: " + configLogo.PostedFile.FileName + " <br />File size: " + configLogo.PostedFile.ContentLength + " kb<br>" + "Content type: " + configLogo.PostedFile.ContentType;
                }
                catch (Exception ex)
                {
                    Label1.Text = "ERROR: " + ex.Message.ToString();
                }
            }
            else
            {
                Label1.Text = "Only .jpg & .JPG files allowed!";
            }
        }
        else
        {
            Label1.Text = "You have not specified a file.";
        }
    }

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<title>.:| Trans-Asia | Configuration Settings |:.</title>
	<meta http-equiv="Content-Language" content="en-us" />
	<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
	<link href="../css/style.css" rel="stylesheet" type="text/css" />
	<link rel="stylesheet" type="text/css" href="../css/style_ua.css" />
	<script type="text/javascript" src="../include/util.js"></script>
    <style type="text/css">
        .auto-style1
        {
            height: 14px;
        }
        .auto-style2 {
            height: 37px;
        }
        .auto-style3 {
            height: 37px;
            width: 167px;
        }
        .auto-style4 {
            width: 167px;
        }
    </style>
</head>
<body onload="SetStatus();">
	<div align="left">
		<form id="Form2" runat="server">
			<table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
				<tr>
					<td valign="top" height="137px">
						<table border="0" cellpadding="0" cellspacing="0" width="100%">
							<tr>
								<td>
									<div align="left" id="masthead">
										<EBid:AdminTopNav runat="server" ID="GlobalLinksNav" />
									</div>
								</td>
							</tr>
							<tr>
								<td>
									<EBid:Admin_TopNav_Home runat="server" ID="Admin_TopNav_Home" />
								</td>
							</tr>
							<tr>
								<td>
									<EBid:TopDate runat="server" ID="TopDate" />
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table border="0" cellpadding="0" cellspacing="0" width="100%" style="height: 100%;">
							<tr>
								<td id="relatedInfo">									
									<h2>
										Admin Functions</h2>
									<EBid:AdminLeftNav runat="server" ID="AdminLeftNav" />
								</td>
								<td id="content">
									<table border="0" cellpadding="0" cellspacing="0" width="100%" id="page0">
										<tr>
											<td valign="top" colspan="2">
												<h1>
													<br />
													Configuration Settings</h1>
												<br />
											</td>
										</tr>
										<%--<tr>
											<td>
												<p>
													<b>Connection String</b><br />
													<asp:RadioButton ID="rbtnProtected" runat="server" GroupName="Protection" Text="Protected" />
													<asp:RadioButton ID="rbtnUnprotected" runat="server" GroupName="Protection" Text="Unprotected" />													
												</p>
											</td>
										</tr>	--%>
										<tr>
											<td class="auto-style2" colspan="2">
												<p>
													<h3><asp:Label ID="configCompanyName" runat="server"></asp:Label>	</h3>									
												</p>
                                                <br />
											</td>
										</tr>
										<tr>
											<td class="auto-style3">
												<p>
													<b>Contact Name</b><br />
												</p>
											</td>
											<td class="auto-style2">
                                                    <asp:TextBox ID="contactName" runat="server"></asp:TextBox>										
											</td>
										</tr>			
										<tr>
											<td class="auto-style4">
												<p><br />	
													<b>Contact Email</b><br />
												</p>
											</td>
											<td>
                                                    <asp:TextBox ID="contactEmail" runat="server"></asp:TextBox>										
											</td>
										</tr>		
										<tr>
											<td class="auto-style4">
												<p><br />	
													<b>Contact Number</b><br />
												</p>
											</td>
											<td>
                                                    <asp:TextBox ID="contactNumber" runat="server"></asp:TextBox>						
											</td>
										</tr>				
										<tr>
											<td class="auto-style4">
												<p><br />
													<b>Theme</b><br />			
                                                    &nbsp;</p>
											</td>
											<td>
                                                    <select id="configTheme" runat="server" name="D1">
                                                        <option value="default">Default</option>
                                                        <option value="green">Green</option>

                                                    </select></td>
										</tr>		
										<tr>
											<td class="auto-style4">
												<p><br />	
													<b>Admin Name *</b><br />
												</p>
											</td>
											<td>
                                                    <asp:TextBox ID="AdminEmailName" runat="server"></asp:TextBox>						
											</td>
										</tr>	
										<tr>
											<td class="auto-style4">
												<p><br />	
													<b>Admin Email *</b><br />
												</p>
											</td>
											<td>
                                                    <asp:TextBox ID="AdminEmailAddress" runat="server"></asp:TextBox>						
											</td>
										</tr>
                                        
                                        
                                        	
										<tr>
											<td align="left" class="auto-style1" colspan="2">
												<hr />
											</td>
										</tr>

                                        
										<tr>
											<td class="auto-style4">
												<p><br />	
                                                    <b>Company Logo *</b> <asp:Label ID="configLogoLbl" runat="server" Text="Label"></asp:Label>
                                                        
                                                        <br />							
												</p>
											</td>
											<td>
													<asp:FileUpload ID="configLogo" runat="server"  /><asp:LinkButton ID="Button1" runat="server"  OnClick="UploadLogo" CausesValidation="False"  Width="100px">Upload Image</asp:LinkButton>
                                                        <br /><asp:Label ID="Label1" runat="server" Font-Size="12px"></asp:Label>
                                                        </td>
										</tr>	
                                        	
										<tr>
											<td align="left" class="auto-style1" colspan="2">
												&nbsp;
											</td>
										</tr>

                                        	
										<tr>
											<td align="left" class="auto-style1" colspan="2">
												<asp:Label ID="lblMessage" runat="server" Font-Size="11px" ForeColor="red"></asp:Label>
											</td>
										</tr>
										<tr>
											<td colspan="2">
													<br />
													&nbsp;&nbsp;<font color="red" size="2">NOTE: Upon successful update, you will be prompted to
														login again </font>
											</td>
										</tr>
										<tr>
											<td colspan="2" style="padding-left: 20px; padding-right: 10px" id="actions">
												<asp:LinkButton ID="lnkSave" runat="server" OnClick="Save_OnClick">Save</asp:LinkButton>
											</td>
										</tr>
										<tr>
											<td style="height: 20px;" colspan="2">
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td id="footer" height="50px">
						<EBid:Footer runat="server" ID="Footer1" />
					</td>
				</tr>
			</table>
		</form>
	</div>
</body>
</html>
