<%@ Page Language="C#" AutoEventWireup="true" CodeFile="profileedit.aspx.cs" Inherits="web_vendorscreens_profileEdit"
    EnableViewState="true" MaintainScrollPositionOnPostback="true" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Vendor/Footer.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/web/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_TopNav_Profile" Src="~/web/usercontrol/Vendor/Vendor_TopNav_Profile.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_LeftNav_Profile" Src="~/web/usercontrol/Vendor/Vendor_LeftNav_Bids.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
    <link href='../themes/<%= Session["configTheme"] %>/css/style_v.css' rel="stylesheet" type="text/css" />
    <style>
    #innerTable {
	    width: 100%;
	    margin-right: 10px;
	    margin-top: 10px;
	    margin-left: 0px;
	    background-color: #DBEAF5; 
	  
	    }
    #innerTable_th {
	    text-align: left;
	    padding: 0px;
	    padding-left: 2px;
	    font-size: 11px;
	    background-color: #B1CADC;
	    color: #fff;
	    font-weight: bold;
	  /*  border-left: solid 1px #FFF; */
	    }
    #innerTable_td {
	    color: #000;
	    background-color: #000;
	    font-size: 11px;
	    padding: 0px;
	    padding-left: 2px;
	    vertical-align: middle;
	 /*   border-bottom: solid 1px #B1CADC; */
	    }
    #innerTable_th_a {
	    color: #FFFFFF;
	    }
    #innerTable_th_a:hover {
    color: #fff;
	    text-decoration: none;
	    width: 100%;
	    }
    #innerTable_a {
	    color: blue;
	    padding: 0px;
	    width: 100%;
	    }
    #innerTable_a:hover {
	    padding: 0px;
	    background-color:Transparent;
	    width: 100%;
	    color: blue;
	    text-decoration: none;
	}
    </style>
    <script type="text/javascript" src="../include/profileedit.js"></script>
</head>
<body style="height: 100%;">
    <div align="left">
        <form id="Form1" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
                <tr height="137px">
                    <td valign="top">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <div align="left" id="masthead">
                                        <EBid:GlobalLinksNav runat="server" ID="GlobalLinksNav" />
                                    </div>
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
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
                            <tr>
                                <td id="relatedInfo">
                                    <div align="left">
                                    <EBid:Vendor_LeftNav_Profile runat="server" ID="LeftNav" />
                                    </div>
                                </td>
                                <td id="content">
                                    <h1>
                                        <br />
                                        Edit Profile</h1>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails">
                                        <tr>
                                            <th colspan="2">
                                                Company information</th>
                                        </tr>
                                        <tr class="value">
                                            <td style="width: 19%">
                                                Company Name:</td>
                                            <td>
                                                <asp:TextBox ID="tbCompanyName" runat="server" Width="35%" ReadOnly="True" /></td>
                                        </tr>
                                        <tr class="evenCells">
                                            <td style="width: 19%" align="center" valign="middle">
                                                Address: (Head Office)</td>
                                            <td>
                                                <asp:TextBox ID="tbHeadOfficeAddress1" runat="server" Width="56%" /><br />
                                                <asp:TextBox ID="tbHeadOfficeAddress2" runat="server" Width="56%" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="101%" class="value" colspan="2">
                                                Telephone:&nbsp;
                                                <asp:TextBox ID="tbTelephone" runat="server" Width="20%" /><br />
                                                 Mobile No:&nbsp;&nbsp;                                                   
                                                    <asp:TextBox ID="tbMobileNo" runat="server" Width="20%" MaxLength="11" />
                                                    <asp:RequiredFieldValidator ID="rfvMobileNo" runat="server" ControlToValidate="tbMobileNo"
                                                        ErrorMessage="&lt;br /&gt;&#187; Mobile No. is required." EnableTheming="True" SetFocusOnError="True">* Required</asp:RequiredFieldValidator>
                                                    <asp:CustomValidator ID="cvMobileNo" runat="server" ControlToValidate="tbMobileNo" Text="* Mobile No. is invalid" ErrorMessage="&lt;br /&gt;&#187; Mobile No. is invalid." Display="Dynamic" OnServerValidate="CheckMobileNo" SetFocusOnError="True"></asp:CustomValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="101%" class="evenCells" colspan="2">
                                                Fax:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:TextBox ID="tbFax" runat="server" Width="20%" />
                                                &nbsp; Extension:
                                                <asp:TextBox ID="tbExtension" runat="server" Width="20%" />
                                            </td>
                                        </tr>
                                        <tr class="value">
                                            <td style="width: 198px">
                                                Address: (Branch)</td>
                                            <td>
                                                <asp:TextBox ID="tbBranchAddress1" runat="server" Width="56%" /><br />
                                                <asp:TextBox ID="tbBranchAddress2" runat="server" Width="56%" /></td>
                                        </tr>
                                        <tr class="evenCells">
                                            <td width="101%" colspan="2">
                                                Telephone:&nbsp;
                                                <asp:TextBox ID="tbBranchPhone" runat="server" Width="20%" /></td>
                                        </tr>
                                        <tr class="value">
                                            <td width="101%" colspan="2">
                                                Fax:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:TextBox ID="tbBranchFax" runat="server" Width="20%" />
                                                &nbsp; Extension:
                                                <asp:TextBox ID="tbBranchExtension" runat="server" Width="20%" /></td>
                                        </tr>
                                        <tr class="evenCells">
                                            <td width="85%" colspan="2">
                                                VAT Reg. No.:&nbsp;
                                                <asp:TextBox ID="tbVatRegNo" runat="server" Width="30%" /><br />
                                                <br />
                                                TIN:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:TextBox ID="tbTIN" runat="server" Width="30%" /></td>
                                        </tr>
                                        <tr class="value">
                                            <td width="85%" colspan="2">
                                                P.O. Box:
                                                <asp:TextBox ID="tbPOBox" runat="server" Width="15%" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                Postal Code:
                                                <asp:TextBox ID="tbPostalCode" runat="server" Width="10%" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                <br />
                                                <br />
                                                Email:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:TextBox ID="tbEmail" runat="server" Width="28%" /></td>
                                        </tr>
                                        <tr class="evenCells">
                                            <td width="85%" colspan="2">
                                                Standard Terms of Payment:
                                                <asp:TextBox ID="tbTermsofPayment" runat="server" Width="20%" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                <br />
                                                <br />
                                                Special Terms:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:TextBox ID="tbSpecialTerms" runat="server" Width="20%" />
                                                %PPD
                                                <br />
                                                <br />
                                                Minimum Order Value:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:TextBox ID="tbMinOrderValue" runat="server" Width="20%" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="85%" class="value" colspan="2">
                                                Sales Person:
                                                <asp:TextBox ID="tbSalesPerson" runat="server" Width="30%" ReadOnly="True" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                Telephone:
                                                <asp:TextBox ID="tbSalesPersonPhone" runat="server" Width="20%" /></td>
                                        </tr>
                                        <tr>
                                            <td width="85%" class="evenCells" colspan="2">
                                                Type of Business Organization:<br />
                                                <br />
                                                <asp:RadioButtonList ID="rbOrganizationType" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1">Individual</asp:ListItem>
                                                    <asp:ListItem Value="2">Partnership</asp:ListItem>
                                                    <asp:ListItem Value="3">Corporation</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="85%" class="value" colspan="2">
                                                Ownership:&nbsp;
                                                <asp:Label ID="lblOwnershipFilipino" runat="server" /><br />
                                                <br />
                                                <asp:TextBox ID="tbOwnershipFilipino" runat="server" Width="30px" />% Filipino&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:TextBox ID="tbOwnershipOther" runat="server" Width="30px" />% Other Nationality</td>
                                        </tr>
                                        <tr>
                                            <td width="85%" class="evenCells" colspan="2">
                                                Company Classification (for goods Supplier Only):<br />
                                                <br />
                                                <table id="chkCompanyClassification" border="0">
                                                    <tr>
                                                        <td>
                                                            <input runat="server" name="chkCompanyClass" id="chkCompanyClassification_0" type="checkbox"
                                                                onclick="CheckCompanyClass();" /><label for="chkCompanyClassification_0">Manufacturer</label></td>
                                                        <td>
                                                            <input runat="server" name="chkCompanyClass" id="chkCompanyClassification_1" type="checkbox"
                                                                onclick="CheckCompanyClass();" /><label for="chkCompanyClassification_1">Importer</label></td>
                                                        <td>
                                                            <input runat="server" name="chkCompanyClass" id="chkCompanyClassification_2" type="checkbox"
                                                                onclick="CheckCompanyClass();" /><label for="chkCompanyClassification_2">Dealer</label></td>
                                                        <td>
                                                            <input runat="server" name="chkCompanyClass" id="chkCompanyClassification_3" type="checkbox"
                                                                onclick="CheckCompanyClass();" /><label for="chkCompanyClassification_3">Trader</label></td>
                                                        <td>
                                                            <input runat="server" name="chkCompanyClass" id="chkCompanyClassification_4" type="checkbox"
                                                                onclick="CheckCompanyClass();" /><label for="chkCompanyClassification_4">N/A</label></td>
                                                    </tr>
                                                </table>
                                                <asp:CustomValidator ID="cvCompanyClass" runat="server" ClientValidationFunction="ValidateCompanyClass(this, args);"
                                                    ErrorMessage="Select at least 1 Company Classification." SetFocusOnError="true"></asp:CustomValidator>
                                            </td>
                                        </tr>
                                        <tr class="value">
                                            <td style="width: 19%">
                                                Sole Supplier:</td>
                                            <td>
                                                <asp:TextBox ID="tbSoleSupplier1" runat="server" Width="56%" /><br />
                                                <asp:TextBox ID="tbSoleSupplier2" runat="server" Width="56%" /></td>
                                        </tr>
                                        <tr class="evenCells">
                                            <td style="width: 19%">
                                                Specialization:</td>
                                            <td>
                                                <asp:TextBox ID="tbSpecialization" runat="server" Width="56%" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails1">
                                        <tr>
                                            <th colspan="2">
                                                Key Personell</th>
                                        </tr>
                                        <tr>
                                            <td width="55%">
                                                Names</td>
                                            <td width="45%">
                                                Position</td>
                                        </tr>
                                        <tr>
                                            <td class="value">
                                                <asp:TextBox ID="tbKeyPersonnel" runat="server" Width="80%" /></td>
                                            <td class="value">
                                                <asp:TextBox ID="tbKpPosition" runat="server" Width="80%" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails5"
                                        runat="server">
                                        <tr>
                                            <th>
                                                Present Services availed from Trans-Asia 
                                            </th>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:GridView runat="server" ID="gvPresentSvc" AutoGenerateColumns="false" ShowHeader="true"
                                                    CssClass="innerTable" OnRowDataBound="gvPresentSvc_RowDataBound" OnRowCommand="gvPresentSvc_RowCommand"
                                                    BorderWidth="0" Width="100%">
                                                    <EditRowStyle CssClass="value" />
                                                    <AlternatingRowStyle CssClass="evenCells" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Type of Plan" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="false"
                                                            HeaderStyle-BackColor="#DBEAF5" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <asp:DropDownList runat="server" ID="ddlPlans">
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="hdPlanID" runat="server" Value='<% #Bind("PlanID")%>' />
                                                                <asp:HiddenField ID="hdPresentSvcID" runat="server" Value='<% #Bind("PresentServiceID")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Acct No." HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="false"
                                                            HeaderStyle-BackColor="#DBEAF5" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="lblAcctNo" Text='<% #Bind("AccountNo")%>' onkeypress="return PhoneAndNoValidator(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Credit Limit" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="false"
                                                            HeaderStyle-BackColor="#DBEAF5" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="lblCreditLimit" Text='<% #Bind("CreditLimit")%>'
                                                                    onkeypress="return(currencyFormat(this,',','.',event))"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-BackColor="#DBEAF5" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lnkRemove" Text="Remove" CausesValidation="false"
                                                                    CommandName="Remove" CommandArgument='<% #Bind("index")%>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:LinkButton ID="lnkAddServices" runat="server" Text="Add Services" OnClick="lnkAddServices_Click" />
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails8"
                                        runat="server">
                                        <tr>
                                            <th colspan="2">
                                                References</th>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:GridView runat="server" ID="gvMajCustomers" AutoGenerateColumns="false" ShowHeader="true"
                                                    CssClass="innerTable" OnRowDataBound="gvMajCustomers_RowDataBound" OnRowCommand="gvMajCustomers_RowCommand"
                                                    BorderWidth="0" Width="100%">
                                                    <EditRowStyle CssClass="value" />
                                                    <HeaderStyle HorizontalAlign="left" />
                                                    <AlternatingRowStyle CssClass="evenCells" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Major Customers" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="false"
                                                            HeaderStyle-BackColor="#DBEAF5" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdReferenceNo" runat="server" Value='<% #Bind("ReferencesNo")%>' />
                                                                <asp:TextBox runat="server" ID="lblName" Text='<% #Bind("CompanyName")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Average Monthly Sales" HeaderStyle-ForeColor="black"
                                                            HeaderStyle-Font-Bold="false" HeaderStyle-BackColor="#DBEAF5" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="lblAveMonthly" Text='<% #Bind("AveMonthlySales")%>'
                                                                    onkeypress="return(currencyFormat(this,',','.',event))"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-BackColor="#DBEAF5" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lnkRemove" Text="Remove" CausesValidation="false"
                                                                    CommandName="Remove" CommandArgument='<% #Bind("index")%>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:GridView runat="server" ID="gvBanks" AutoGenerateColumns="false" ShowHeader="true"
                                                    CssClass="innerTable" OnRowDataBound="gvBanks_RowDataBound" OnRowCommand="gvBanks_RowCommand"
                                                    BorderWidth="0" Width="100%">
                                                    <EditRowStyle CssClass="value" />
                                                    <AlternatingRowStyle CssClass="evenCells" />
                                                    <HeaderStyle HorizontalAlign="left" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Banks" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="false"
                                                            HeaderStyle-BackColor="#DBEAF5" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdReferenceNo" runat="server" Value='<% #Bind("ReferencesNo")%>' />
                                                                <asp:TextBox runat="server" ID="lblName" Text='<% #Bind("CompanyName")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Credit Line" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="false"
                                                            HeaderStyle-BackColor="#DBEAF5" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="lblCreditLine" Text='<% #Bind("CreditLine")%>' onkeypress="return(currencyFormat(this,',','.',event))"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-BackColor="#DBEAF5" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lnkRemove" Text="Remove" CausesValidation="false"
                                                                    CommandName="Remove" CommandArgument='<% #Bind("index")%>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:GridView runat="server" ID="gvAffiliate" AutoGenerateColumns="false" ShowHeader="true"
                                                    CssClass="innerTable" OnRowDataBound="gvAffiliate_RowDataBound" OnRowCommand="gvAffiliate_RowCommand"
                                                    BorderWidth="0" Width="100%">
                                                    <EditRowStyle CssClass="value" />
                                                    <AlternatingRowStyle CssClass="evenCells" />
                                                    <HeaderStyle HorizontalAlign="left" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Affiliated Companies" HeaderStyle-ForeColor="black"
                                                            HeaderStyle-Font-Bold="false" HeaderStyle-BackColor="#DBEAF5" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdReferenceNo" runat="server" Value='<% #Bind("ReferencesNo")%>' />
                                                                <asp:TextBox runat="server" ID="lblName" Text='<% #Bind("CompanyName")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kind of Business" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="false"
                                                            HeaderStyle-BackColor="#DBEAF5" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="lblBusiness" Text='<% #Bind("KindOfBusiness")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-BackColor="#DBEAF5" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lnkRemove" Text="Remove" CausesValidation="false"
                                                                    CommandName="Remove" CommandArgument='<% #Bind("index")%>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:GridView runat="server" ID="gvExtAuditors" AutoGenerateColumns="false" ShowHeader="true"
                                                    CssClass="innerTable" OnRowDataBound="gvExtAuditors_RowDataBound" OnRowCommand="gvExtAuditors_RowCommand"
                                                    BorderWidth="0" Width="100%">
                                                    <HeaderStyle HorizontalAlign="left" />
                                                    <EditRowStyle CssClass="value" />
                                                    <AlternatingRowStyle CssClass="evenCells" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="External Auditors" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="false"
                                                            HeaderStyle-BackColor="#DBEAF5" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdReferenceNo" runat="server" Value='<% #Bind("ReferencesNo")%>' />
                                                                <asp:TextBox runat="server" ID="lblName" Text='<% #Bind("CompanyName")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Legal Counsel" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="false"
                                                            HeaderStyle-BackColor="#DBEAF5" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="lblLegalCounsel" Text='<% #Bind("LegalCounsel")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-BackColor="#DBEAF5" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lnkRemove" Text="Remove" CausesValidation="false"
                                                                    CommandName="Remove" CommandArgument='<% #Bind("index")%>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:LinkButton ID="lnkReferences" runat="server" Text="Add References" OnClick="lnkReferences_Click" />
                                    <asp:DropDownList ID="ddlReference" runat="server">
                                        <asp:ListItem Selected="True" Value="1">Major Customers</asp:ListItem>
                                        <asp:ListItem Value="2">Banks</asp:ListItem>
                                        <asp:ListItem Value="3">Affiliated Companies</asp:ListItem>
                                        <asp:ListItem Value="4">External Auditors</asp:ListItem>
                                    </asp:DropDownList>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails6"
                                        runat="server">
                                        <tr>
                                            <th colspan="3">
                                                Equipment</th>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:GridView runat="server" ID="gvEquipments" AutoGenerateColumns="false" ShowHeader="true"
                                                    CssClass="innerTable" OnRowDataBound="gvEquipments_RowDataBound" OnRowCommand="gvEquipments_RowCommand"
                                                    BorderWidth="0" Width="100%">
                                                    <EditRowStyle CssClass="value" />
                                                    <AlternatingRowStyle CssClass="evenCells" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Type" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="false"
                                                            HeaderStyle-BackColor="#DBEAF5" HeaderStyle-BorderWidth="0" ControlStyle-Width="80%"
                                                            ItemStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="lblEqpmntType" Text='<% #Bind("EquipmentType")%>'></asp:TextBox>
                                                                <asp:HiddenField ID="hdEquipmentID" runat="server" Value='<% #Bind("EquipmentID")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Units" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="false"
                                                            HeaderStyle-BackColor="#DBEAF5" HeaderStyle-BorderWidth="0" ControlStyle-Width="80%">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="lblUnits" Text='<% #Bind("Units")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="false"
                                                            HeaderStyle-BackColor="#DBEAF5" HeaderStyle-BorderWidth="0" ControlStyle-Width="80%"
                                                            ItemStyle-Width="60%">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="lblRemarks" Text='<% #Bind("Remarks")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-BackColor="#DBEAF5" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lnkRemove" Text="Remove" CausesValidation="false"
                                                                    CommandName="Remove" CommandArgument='<% #Bind("index")%>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:LinkButton ID="lnkAddEquipment" runat="server" Text="Add Equipment" OnClick="lnkAddEquipment_Click" />
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails7"
                                        runat="server">
                                        <tr>
                                            <th colspan="3">
                                                Relative Working in Trans-Asia </th>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:GridView runat="server" ID="gvRelatives" AutoGenerateColumns="false" ShowHeader="true"
                                                    CssClass="innerTable" OnRowDataBound="gvRelatives_RowDataBound" OnRowCommand="gvRelatives_RowCommand"
                                                    BorderWidth="0" Width="100%">
                                                    <EditRowStyle CssClass="value" />
                                                    <AlternatingRowStyle CssClass="evenCells" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Name:" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="false"
                                                            HeaderStyle-BackColor="#DBEAF5" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="lblRelative" Text='<% #Bind("VendorRelative")%>'></asp:TextBox>
                                                                <asp:HiddenField ID="hdRelativeID" runat="server" Value='<% #Bind("VendorRelativeID")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Title/Position" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="false"
                                                            HeaderStyle-BackColor="#DBEAF5" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="lblTitle" Text='<% #Bind("Title")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Relationship" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="false"
                                                            HeaderStyle-BackColor="#DBEAF5" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="lblRelationship" Text='<% #Bind("Relationship")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-BackColor="#DBEAF5" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lnkRemove" Text="Remove" CausesValidation="false"
                                                                    CommandName="Remove" CommandArgument='<% #Bind("index")%>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:LinkButton ID="lnkAddRelative" runat="server" Text="Add Relative" OnClick="lnkAddRelative_Click" />
                                    <br />
                                    <p>&nbsp;
                                        </p>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="lnkNext" runat="server" Text="Save" OnClick="lnkNext_Click"></asp:LinkButton><a
                                                    href="index.aspx">Cancel</a></td>
                                        </tr>
                                    </table>
                                    <p>&nbsp;
                                        </p>
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
