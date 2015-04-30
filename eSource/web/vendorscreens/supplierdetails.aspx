<%@ Page Language="C#" AutoEventWireup="true" CodeFile="supplierdetails.aspx.cs" Inherits="web_vendorscreens_SupplierDetails" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Vendor/Footer.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/web/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_TopNav_Home" Src="~/web/usercontrol/Vendor/Vendor_TopNav_Home.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_LeftNav_Bids" Src="~/web/usercontrol/Vendor/Vendor_LeftNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_LeftNav_Auctions" Src="~/web/usercontrol/Vendor/Vendor_LeftNav_Auctions.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
    <%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
	<title>.:| Trans-Asia  eSourcing System |  |:.</title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_v.css" %>' />
</head>
<body style="height: 100%;">
    <div>
        <form id="Form1" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
                <tr>
                    <td valign="top" height="137">
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
                    <td class="content" height="715" valign="top">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td id="relatedInfo">
                                    <div align="left">
                                        <table id="table4" cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td>
                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0" id="table5">
                                                        <tr>
                                                            <td id="relatedInfo0">
                                                                <div align="left">
                                                                    <EBid:Vendor_LeftNav_Bids runat="server" ID="Vendor_LeftNav_Bids" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div align="left">
                                        <h2>
                                            Auctions</h2>
                                        <div align="left">
                                            <table id="table6" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                <tr>
                                                    <td>
                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0" id="table7">
                                                            <tr>
                                                                <td id="relatedInfo1">
                                                                    <div align="left">
                                                                        <EBid:Vendor_LeftNav_Auctions runat="server" ID="Vendor_LeftNav_Auctions" />
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div align="left">
                                            <h2>
                                                Bid bulletin</h2>
                                        </div>
                                    </div>
                                    <h6>
                                        Bid Bulletin Title</h6>
                                    <p class="date">
                                        February 10, 2006</p>
                                    <p>
                                        Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor
                                        incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud
                                        exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute
                                        irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla
                                        pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia
                                        deserunt mollit anim id est laborum</p>
                                    <h4>
                                        Published by: Group</h4>
                                    <p>
                                        &nbsp;</p>
                                </td>
                                <td id="content">
                                    <h1>
                                        <br />
                                        Supplier Details</h1>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails">
                                        <tr>
                                            <th colspan="2">
                                                Company information</th>
                                        </tr>
                                        <tr>
                                            <td width="101%" colspan="2" class="value">
                                                Company Name:
                                                <asp:Label ID="lblCompanyName" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td class="evenCells" style="width: 19%">
                                                Address: (Head Office)</td>
                                            <td class="evenCells">
                                                <asp:Label ID="lblHeadOfficeAddress" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td width="101%" class="value" colspan="2">
                                                Telephone:&nbsp;
                                                <asp:Label ID="lblTelephone" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td width="101%" class="evenCells" colspan="2">
                                                Fax:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lblFax" runat="server"></asp:Label>
                                                &nbsp; Extension:
                                                <asp:Label ID="lblExtension" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td class="value" style="width: 198px">
                                                Address: (Branch)</td>
                                            <td class="value" width="84%">
                                                <asp:Label ID="lblBranchAddress" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td width="101%" class="value" colspan="2">
                                                Telephone:&nbsp;
                                                <asp:Label ID="lblBranchPhone" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td width="101%" class="evenCells" colspan="2">
                                                Fax:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lblBranchFax" runat="server" />
                                                &nbsp; Extension:
                                                <asp:Label ID="lblBranchExtension" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td width="85%" class="evenCells" colspan="2">
                                                VAT Reg. No.:&nbsp;
                                                <asp:Label ID="lblVatRegNo" runat="server" /><br />
                                                <br />
                                                TIN:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lblTin" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td width="85%" class="value" colspan="2">
                                                P.O. Box:
                                                <asp:Label ID="lblPOBox" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp; Postal Code:
                                                <asp:Label ID="lblPostalCode" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                <br />
                                                <br />
                                                Email:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lblEmail" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td width="85%" class="evenCells" colspan="2">
                                                Standard Terms of Payment:
                                                <asp:Label ID="lblTermsofPayment" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                <br />
                                                <br />
                                                Special Terms:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lblSpecialTerms" runat="server" />
                                                <br />
                                                <br />
                                                Minimum Order Value:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lblMinOrderValue" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td width="85%" class="value" colspan="2">
                                                Sales Person:
                                                <asp:Label ID="lblSalesPerson" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp; Telephone:
                                                <asp:Label ID="lblSalesPersonPhone" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td width="85%" class="evenCells" colspan="2">
                                                Type of Business Organization:
                                                <asp:Label ID="lblOrgType" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td width="85%" class="value" colspan="2">
                                                Ownership:&nbsp;
                                                <asp:Label ID="lblOwnershipFilipino" runat="server" />
                                                Filipino&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lblOwnershipOther" runat="server" />
                                                Other Nationality</td>
                                        </tr>
                                        <tr>
                                            <td width="85%" class="evenCells" colspan="2">
                                                Company Classification :&nbsp;
                                                <asp:Label ID="lblClassification" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="85%" class="value" colspan="2">
                                                Sole Supplier:&nbsp;
                                                <asp:Label ID="lblSoleSupplier" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td width="85%" class="evenCells" colspan="2">
                                                Specialization:
                                                <asp:Label ID="lblSpecialization" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails1">
                                        <tr>
                                            <th colspan="2">
                                                Key Personell</th>
                                        </tr>
                                        <tr>
                                            <td width="60%">
                                                Names</td>
                                            <td width="38%">
                                                Position</td>
                                        </tr>
                                        <tr>
                                            <td class="value" width="48%">
                                                <asp:Label ID="lblKeyPersonnel" runat="server" /></td>
                                            <td class="value" width="51%">
                                                <asp:Label ID="lblKpPosition" runat="server" /></td>
                                        </tr>
                                    </table>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails2"
                                        runat="server">
                                        <tr>
                                            <th colspan="3">
                                                Present Services availed from Trans-Asia 
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                Type of Plan</td>
                                            <td>
                                                Acct No.</td>
                                            <td>
                                                Credit Limit</td>
                                        </tr>
                                    </table>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails3"
                                        runat="server">
                                        <tr>
                                            <th colspan="2">
                                                References</th>
                                        </tr>
                                    </table>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails4"
                                        runat="server">
                                        <tr>
                                            <th colspan="3">
                                                Equipment</th>
                                        </tr>
                                        <tr>
                                            <td width="40%">
                                                Type</td>
                                            <td>
                                                Units</td>
                                            <td width="40%">
                                                Remarks</td>
                                        </tr>
                                    </table>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails5"
                                        runat="server">
                                        <tr>
                                            <th colspan="3">
                                                Relative Working in Trans-Asia </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                Name:
                                            </td>
                                            <td>
                                                Title/Position</td>
                                            <td>
                                                Relationship</td>
                                        </tr>
                                    </table>
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
                                                <asp:GridView ShowHeader="false" ID="gvBrands" runat="server" AutoGenerateColumns="false"
                                                    CssClass="ClearGrid">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-CssClass="ClearGrid">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblBrands" Text='<%# Bind("BrandName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                            <td class="value" colspan="3">
                                                <asp:GridView ShowHeader="false" ID="gvServices" runat="server" AutoGenerateColumns="false"
                                                    CssClass="ClearGrid">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-CssClass="ClearGrid">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblServices" Text='<%# Bind("ServiceName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
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
                                                <asp:GridView ShowHeader="false" ID="gvLocations" runat="server" AutoGenerateColumns="false"
                                                    CssClass="ClearGrid">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-CssClass="ClearGrid">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblLocations" Text='<%# Bind("LocationName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                            <td class="value" colspan="3">
                                                <asp:GridView ShowHeader="false" ID="gvItems" runat="server" AutoGenerateColumns="false"
                                                    CssClass="ClearGrid">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-CssClass="ClearGrid">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblItems" Text='<%# Bind("ItemsCarried")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                        <tr>
                                            <td>
                                                <a href="Index.aspx">Ok</a></td>
                                        </tr>
                                    </table>
                                    <p>
                                        &nbsp;</p>
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
