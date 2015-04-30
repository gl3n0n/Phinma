<%@ Page Language="C#" AutoEventWireup="true" CodeFile="supplierdetails.aspx.cs" Inherits="web_buyer_screens_SupplierDetails" %>

<%@ Register TagPrefix="EBid" TagName="TopNavSuppliers" Src="~/WEB/usercontrol/Buyer/TopNavSuppliers.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNav2" Src="~/WEB/usercontrol/Buyer/TopNav2.ascx" %>
<%@ Register TagPrefix="EBid" TagName="LeftNavSuppliers" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_buyer.css" %>' />
</head>
<body style="height: 100%;">
    <div align="left">
        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
            <form runat="server">
                <tr>
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
                                    <EBid:TopNav2 runat="server" ID="TopNav2" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr height="100%">
                    <td>
                        <br />
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td id="relatedInfo" style="width:250px;">
                                    <EBid:LeftNavSuppliers runat="server" ID="LeftNavSuppliers" />
                                    <div align="left">                                        
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table3">
                                            <tr>
                                                <td>
                                                    <div align="left">
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table4">
                                                            <tr>
                                                                <td>&nbsp;
                                                                    </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>                                        
                                    </div>
                                </td>
                                <td id="content" >                                    
                                    <h1>
                                        <br />
                                        Supplier Profile</h1>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails">
                                        <tr>
                                            <th>
                                                Company information</th>
                                        </tr>
                                        <tr>
                                            <td class="value">
                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                    <tr>
                                                        <td style="border-width: 0" width="20%">
                                                            Company Name:
                                                        </td>
                                                        <td style="border-width: 0" width="80%">
                                                            <asp:Label runat="server" ID="lblCompanyName"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="evenCells">
                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                    <tr>
                                                        <td style="border-width: 0" width="20%">
                                                            Address: (Head Office)
                                                        </td>
                                                        <td style="border-width: 0">
                                                            <asp:Label runat="server" ID="lblAddressHeadOffice"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="value">
                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                    <tr>
                                                        <td style="border-width: 0" width="20%">
                                                            Telephone:</td>
                                                        <td style="border-width: 0">
                                                            <asp:Label runat="server" ID="lblTelephone"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="evenCells">
                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                    <tr>
                                                        <td style="border-width: 0" width="20%">
                                                            Fax:</td>
                                                        <td style="border-width: 0" width="30%">
                                                            <asp:Label runat="server" ID="lblFax"></asp:Label></td>
                                                        <td style="border-width: 0" width="20%">
                                                            Extension:</td>
                                                        <td style="border-width: 0">
                                                            <asp:Label runat="server" ID="lblExtension"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="value">
                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                    <tr>
                                                        <td style="border-width: 0" width="20%">
                                                            Address: (Branch)</td>
                                                        <td style="border-width: 0">
                                                            <asp:Label runat="server" ID="lblAddressBranch"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="evenCells">
                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                    <tr>
                                                        <td style="border-width: 0" width="20%">
                                                            Telephone:</td>
                                                        <td style="border-width: 0">
                                                            <asp:Label runat="server" ID="lblTelephone1"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="value">
                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                    <tr>
                                                        <td style="border-width: 0" width="20%">
                                                            Fax:</td>
                                                        <td style="border-width: 0" width="30%">
                                                            <asp:Label runat="server" ID="lblFax1"></asp:Label></td>
                                                        <td style="border-width: 0" width="20%">
                                                            Extension:</td>
                                                        <td style="border-width: 0">
                                                            <asp:Label runat="server" ID="lblExtension1"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="evenCells" colspan="4">
                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                    <tr>
                                                        <td style="border-width: 0" width="20%">
                                                            VAT Reg. No.:
                                                        </td>
                                                        <td style="border-width: 0" width="20%">
                                                            <asp:Label runat="server" ID="lblVatRegNo"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="border-width: 0" width="20%">
                                                            TIN:
                                                        </td>
                                                        <td style="border-width: 0" width="20%">
                                                            <asp:Label runat="server" ID="lblTin"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="value">
                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                    <tr>
                                                        <td style="border-width: 0" width="20%">
                                                            P.O. Box:
                                                        </td>
                                                        <td style="border-width: 0" width="30%">
                                                            <asp:Label runat="server" ID="lblPOBox"></asp:Label>
                                                        </td>
                                                        <td style="border-width: 0" width="20%">
                                                            Postal Code:
                                                        </td>
                                                        <td style="border-width: 0">
                                                            <asp:Label runat="server" ID="lblPostalCode"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="border-width: 0" width="20%">
                                                            Email:
                                                        </td>
                                                        <td style="border-width: 0">
                                                            <asp:Label runat="server" ID="lblEmail"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="evenCells">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td style="border-width: 0" width="20%">
                                                            Standard Terms of Payment:
                                                        </td>
                                                        <td style="border-width: 0">
                                                            <asp:Label runat="server" ID="lblStandardTerms"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="border-width: 0" width="20%">
                                                            Special Terms:
                                                        </td>
                                                        <td style="border-width: 0">
                                                            <asp:Label runat="server" ID="lblSpecialTerms"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="border-width: 0" width="20%">
                                                            Minimum Order Value:
                                                        </td>
                                                        <td style="border-width: 0">
                                                            <asp:Label runat="server" ID="lblMinimumOrderValue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="value">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td style="border-width: 0" width="20%">
                                                            Sales Person:
                                                        </td>
                                                        <td style="border-width: 0" width="30%">
                                                            <asp:Label runat="server" ID="lblSalesPerson"></asp:Label>
                                                        </td>
                                                        <td style="border-width: 0" width="20%">
                                                            Telephone:
                                                        </td>
                                                        <td style="border-width: 0">
                                                            <asp:Label runat="server" ID="lblTelephone2"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="evenCells">
                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                    <tr>
                                                        <td style="border-width: 0" width="20%">
                                                            Type of Business Organization:
                                                        </td>
                                                        <td style="border-width: 0">
                                                            <asp:Label runat="server" ID="lblTypeOfBusinessOrganization"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="value">
                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                    <tr>
                                                        <td style="border-width: 0" width="20%">
                                                            Ownership:
                                                        </td>
                                                        <td style="border-width: 0" width="3%">
                                                            <asp:Label runat="server" ID="lblFilipino"></asp:Label>
                                                        </td>
                                                        <td style="border-width: 0" width="27%">
                                                            % Filipino
                                                        </td>
                                                        <td style="border-width: 0" width="3%">
                                                            <asp:Label runat="server" ID="lblOtherNationality"></asp:Label>
                                                        </td>
                                                        <td style="border-width: 0">
                                                            % Other Nationality
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="evenCells">
                                                <table cellpadding="0" border="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td style="border-width: 0" width="20%">
                                                            Company Classification:
                                                        </td>
                                                        <td style="border-width: 0">
                                                            <asp:Label runat="server" ID="lblCompanyClassification"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="value">
                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                    <tr>
                                                        <td style="border-width: 0" width="20%">
                                                            Sole Supplier:
                                                        </td>
                                                        <td style="border-width: 0">
                                                            <asp:Label runat="server" ID="lblSoleSupplier"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="evenCells">
                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                    <tr>
                                                        <td style="border-width: 0" width="20%">
                                                            Specialization:
                                                        </td>
                                                        <td style="border-width: 0">
                                                            <asp:Label runat="server" ID="lblSpecialization"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="vendorDetails_all">
                                        <tr>
                                            <th colspan="3" class="itemDetails_th">
                                                Key Personell</th>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:GridView Width='100%' ID="gvKeyPersonnel" runat="server" AutoGenerateColumns="False" CssClass="vendorDetails" BorderWidth="0px">
                                                    <AlternatingRowStyle CssClass="evenCells" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width='50%' HeaderText="Names">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtName" runat="server" Text='<%# Bind("Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="vendorDetails_td" />
                                                            <HeaderStyle CssClass="vendorDetails_th" Font-Bold="False" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width='50%' HeaderText="Position">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtTitlePosition" runat="server" Text='<%# Bind("Position") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="vendorDetails_td" />
                                                            <HeaderStyle CssClass="vendorDetails_th" Font-Bold="False" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <!--<table border="0" cellpadding="0" cellspacing="0" width="100%" class="vendorDetails_all">
                                        <tr>
                                            <th colspan="3" class="itemDetails_th">
                                                Present Services availed from Trans-Asia </th>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:GridView Width='100%' ID="gvPresentServices" runat="server" AutoGenerateColumns="False" CssClass="vendorDetails" BorderWidth="0">
                                                    <AlternatingRowStyle CssClass="evenCells" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width='33%' HeaderText="Type Of Plan" ItemStyle-CssClass="vendorDetails_td" HeaderStyle-CssClass="vendorDetails_th" HeaderStyle-Font-Bold="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtTypeOfPlan" runat="server" Text='<%# Bind("Plan") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width='33%' HeaderText="Acct No." ItemStyle-CssClass="vendorDetails_td" HeaderStyle-CssClass="vendorDetails_th" HeaderStyle-Font-Bold="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtAcctNo" runat="server" Text='<%# Bind("AcctNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width='33%' HeaderText="Credit Limit" ItemStyle-CssClass="vendorDetails_td" HeaderStyle-CssClass="vendorDetails_th" HeaderStyle-Font-Bold="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtCreditLimit" runat="server" Text='<%# Bind("CreditLimit") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />-->
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="vendorDetails_all">
                                        <tr>
                                            <th colspan="3" class="itemDetails_th">
                                                References</th>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:GridView Width='100%' ID="gvMajorCustomers" runat="server" AutoGenerateColumns="False" CssClass="vendorDetails" BorderWidth="0">
                                                    <AlternatingRowStyle CssClass="evenCells" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width='50%' HeaderText="Major Customers" ItemStyle-CssClass="vendorDetails_td" HeaderStyle-CssClass="vendorDetails_th" HeaderStyle-Font-Bold="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtMajorCust" runat="server" Text='<%# Bind("Customer")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width='50%' HeaderText="Average Monthly Sales" ItemStyle-CssClass="vendorDetails_td" HeaderStyle-CssClass="vendorDetails_th" HeaderStyle-Font-Bold="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtAveMonthlySales" runat="server" Text='<%# Bind("Sale")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:GridView Width='100%' ID="gvBanks" runat="server" AutoGenerateColumns="False" CssClass="vendorDetails" BorderWidth="0">
                                                    <AlternatingRowStyle CssClass="evenCells" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width='50%' HeaderText="Banks" ItemStyle-CssClass="vendorDetails_td" HeaderStyle-CssClass="vendorDetails_th" HeaderStyle-Font-Bold="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtBanks" runat="server" Text='<%# Bind("Bank") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width='50%' HeaderText="Credit Line" ItemStyle-CssClass="vendorDetails_td" HeaderStyle-CssClass="vendorDetails_th" HeaderStyle-Font-Bold="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtCreditLine" runat="server" Text='<%# Bind("CreditLine") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:GridView Width='100%' ID="gvAffiliatedCompanies" runat="server" AutoGenerateColumns="False" CssClass="vendorDetails" BorderWidth="0">
                                                    <AlternatingRowStyle CssClass="evenCells" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width='50%' HeaderText="Affiliated Companies" ItemStyle-CssClass="vendorDetails_td" HeaderStyle-CssClass="vendorDetails_th" HeaderStyle-Font-Bold="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtAffiliatedCompanies" runat="server" Text='<%# Bind("Company") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width='50%' HeaderText="Kind Of Business" ItemStyle-CssClass="vendorDetails_td" HeaderStyle-CssClass="vendorDetails_th" HeaderStyle-Font-Bold="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtKindOfBusiness" runat="server" Text='<%# Bind("Business") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:GridView Width='100%' ID="gvExternalAuditors" runat="server" AutoGenerateColumns="False" CssClass="vendorDetails" BorderWidth="0">
                                                    <AlternatingRowStyle CssClass="evenCells" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width='50%' HeaderText="External Auditors" ItemStyle-CssClass="vendorDetails_td" HeaderStyle-CssClass="vendorDetails_th" HeaderStyle-Font-Bold="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtExternalAuditors" runat="server" Text='<%# Bind("Auditor") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width='50%' HeaderText="Legal Counsel" ItemStyle-CssClass="vendorDetails_td" HeaderStyle-CssClass="vendorDetails_th" HeaderStyle-Font-Bold="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtLegalCounsel" runat="server" Text='<%# Bind("Counsel") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="vendorDetails_all">
                                        <tr>
                                            <th colspan="3" class="itemDetails_th">
                                                Add Equipment</th>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:GridView Width='100%' ID="gvEquipment" runat="server" AutoGenerateColumns="False" CssClass="vendorDetails" BorderWidth="0">
                                                    <AlternatingRowStyle CssClass="evenCells" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width='33%' HeaderText="Type" ItemStyle-CssClass="vendorDetails_td" HeaderStyle-CssClass="vendorDetails_th" HeaderStyle-Font-Bold="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtType" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width='33%' HeaderText="Units" ItemStyle-CssClass="vendorDetails_td" HeaderStyle-CssClass="vendorDetails_th" HeaderStyle-Font-Bold="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtUnit" runat="server" Text='<%# Bind("Unit") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width='33%' HeaderText="Remarks" ItemStyle-CssClass="vendorDetails_td" HeaderStyle-CssClass="vendorDetails_th" HeaderStyle-Font-Bold="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="vendorDetails_all">
                                        <tr>
                                            <th colspan="3" class="itemDetails_th">
                                                Relative Working in Trans-Asia </th>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:GridView Width='100%' ID="gvRelatives" runat="server" AutoGenerateColumns="False" CssClass="vendorDetails" BorderWidth="0">
                                                    <AlternatingRowStyle CssClass="evenCells" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Name" ItemStyle-Width='33%' ItemStyle-CssClass="vendorDetails_td" HeaderStyle-CssClass="vendorDetails_th" HeaderStyle-Font-Bold="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Title/Position" ItemStyle-Width='33%' ItemStyle-CssClass="vendorDetails_td" HeaderStyle-CssClass="vendorDetails_th" HeaderStyle-Font-Bold="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtTitlePosition" runat="server" Text='<%# Bind("TitlePosition") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Relationship" ItemStyle-Width='33%' ItemStyle-CssClass="vendorDetails_td" HeaderStyle-CssClass="vendorDetails_th" HeaderStyle-Font-Bold="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtRelationship" runat="server" Text='<%# Bind("Relationship") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                    <div align="left">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="ClearGrid">
                                            <tr>
                                                <th colspan="2">
                                                    Supplier Type:</th>
                                                <th colspan="2">
                                                    Category:</th>
                                                <th colspan="2">
                                                    Sub Category:</th>
                                            </tr>
                                            <tr>
                                                <td class="value" colspan="2">
                                                    <asp:Label runat="server" ID="lblSupplierType" /></td>
                                                <td class="value" colspan="2">
                                                    <asp:Label runat="server" ID="lblCategory" /></td>
                                                <td class="value" colspan="2">
                                                    <asp:Label runat="server" ID="lblSubCategory" /></td>
                                            </tr>
                                            <tr>
                                                <th colspan="3">
                                                    ISO Classification:</th>
                                                <th colspan="3">
                                                    PCAB Class:</th>
                                            </tr>
                                            <tr>
                                                <td class="value" colspan="3">
                                                    <asp:Label runat="server" ID="lblISOStandard"></asp:Label>
                                                </td>
                                                <td class="value" colspan="3">
                                                    <asp:Label runat="server" ID="lblPCABClass" /></td>
                                            </tr>
                                            <tr>
                                                <th colspan="3">
                                                    Brands:</th>
                                                <th colspan="3">
                                                    Services:</th>
                                            </tr>
                                            <tr>
                                                <td class="value" colspan="3" bordercolor="#FFFFFF">
                                                    <asp:GridView ShowHeader="false" ID="gvBrands" runat="server" AutoGenerateColumns="false" CssClass="ClearGrid">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblBrands" Text='<%# Bind("BrandName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="ClearGrid" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <br />
                                                </td>
                                                <td class="value" colspan="3">
                                                    <asp:GridView ShowHeader="false" ID="gvServices" runat="server" AutoGenerateColumns="false" CssClass="ClearGrid">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblServices" Text='<%# Bind("ServiceName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="ClearGrid" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <th colspan="3">
                                                    Locations</th>
                                                <th colspan="3">
                                                    Items:</th>
                                            </tr>
                                            <tr>
                                                <td class="value" colspan="3">
                                                    <asp:GridView ShowHeader="false" ID="gvLocations" runat="server" AutoGenerateColumns="false" CssClass="ClearGrid">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblLocations" Text='<%# Bind("LocationName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="ClearGrid" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <br />
                                                </td>
                                                <td class="value" colspan="3">
                                                    <asp:GridView ShowHeader="false" ID="gvItems" runat="server" AutoGenerateColumns="false" CssClass="ClearGrid">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblItems" Text='<%# Bind("ItemsCarried")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="ClearGrid" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <br />
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                        <tr>
                                            <td>
                                                <%--<asp:LinkButton runat="server" ID="btnEdit" Text="Edit" OnClick="btnEdit_Click"></asp:LinkButton>--%>
                                                <%--<asp:LinkButton runat="server" ID="btnOK" Text="Ok" PostBackUrl="~/web/buyerscreens/index.aspx"></asp:LinkButton>--%>
                                                <a href="javascript:history.back()" ID="btnOK">OK</a>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td id="footer">
                        <EBid:Footer runat="server" ID="Footer" />
                    </td>
                </tr>
            </form>
        </table>
    </div>
</body>
</html>
