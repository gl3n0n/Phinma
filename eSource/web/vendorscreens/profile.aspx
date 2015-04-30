<%@ Page Language="C#" AutoEventWireup="true" CodeFile="profile.aspx.cs" Inherits="web_vendorscreens_Profile" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Vendor/Footer.ascx"  %>
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
                                    
                                        <EBid:Vendor_LeftNav_Profile runat="server" ID="LeftNav" />
                                   
                                </td>
                                <td id="content">
                                    <h1>
                                        <br />
                                        My Profile</h1>
                                    <p>
                                        Your profile is displayed below. Click "Edit" to make changes then click "Save"
                                        to apply changes.</p>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails">
                                        <tr>
                                            <th colspan="2">
                                                Company information</th>
                                        </tr>
                                        <tr class="value">
                                            <td style="width: 19%">
                                                Company Name:</td>
                                            <td width="101%" colspan="2">
                                                <asp:Label ID="lblCompanyName" runat="server" /></td>
                                        </tr>
                                        <tr class="evenCells">
                                            <td style="width: 19%">
                                                Address: (Head Office)</td>
                                            <td>
                                                <asp:Label ID="lblHeadOfficeAddress" runat="server" /></td>
                                        </tr>
                                        <tr class="value">
                                            <td style="width: 19%">
                                                Telephone:<br /><br />Mobile No.:</td>
                                            <td width="101%" colspan="2">
                                                <asp:Label ID="lblTelephone" runat="server" /><br /><br />
                                                <asp:Label ID="lblMobileNo" runat="server" /></td>
                                        </tr>
                                        <tr class="evenCells">
                                            <td style="width: 19%">
                                                Fax:<br />
                                                <br />
                                                Extension:</td>
                                            <td>
                                                <asp:Label ID="lblFax" runat="server" /><br />
                                                <br />
                                                <asp:Label ID="lblExtension" runat="server" /></td>
                                        </tr>
                                        <tr class="value">
                                            <td style="width: 19%">
                                                Address: (Branch)</td>
                                            <td>
                                                <asp:Label ID="lblBranchAddress" runat="server" /></td>
                                        </tr>
                                        <tr class="evenCells">
                                            <td style="width: 19%">
                                                Telephone:</td>
                                            <td>
                                                <asp:Label ID="lblBranchPhone" runat="server" /></td>
                                        </tr>
                                        <tr class="value">
                                            <td style="width: 19%">
                                                Fax:<br />
                                                <br />
                                                Extension:</td>
                                            <td>
                                                <asp:Label ID="lblBranchFax" runat="server" /><br />
                                                <br />
                                                <asp:Label ID="lblBranchExtension" runat="server" /></td>
                                        </tr>
                                        <tr class="evenCells">
                                            <td style="width: 19%">
                                                VAT Reg. No.:<br />
                                                <br />
                                                TIN:</td>
                                            <td>
                                                <asp:Label ID="lblVatRegNo" runat="server" /><br />
                                                <br />
                                                <asp:Label ID="lblTin" runat="server" /></td>
                                        </tr>
                                        <tr class="value">
                                            <td style="width: 19%">
                                                P.O. Box:<br />
                                                <br />
                                                Email:</td>
                                            <td>
                                                <asp:Label ID="lblPOBox" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;Postal Code:&nbsp;&nbsp;&nbsp;&nbsp;
                                                &nbsp; &nbsp;&nbsp; &nbsp;<asp:Label ID="lblPostalCode" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                <br />
                                                <br />
                                                <asp:Label ID="lblEmail" runat="server" /></td>
                                        </tr>
                                        <tr class="evenCells">
                                            <td width="85%" colspan="2">
                                                Standard Terms of Payment: &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;<asp:Label ID="lblTermsofPayment"
                                                    runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                <br />
                                                <br />
                                                Special Terms:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                &nbsp; &nbsp; &nbsp; &nbsp;
                                                <asp:Label ID="lblSpecialTerms" runat="server" />
                                                <br />
                                                <br />
                                                Minimum Order Value:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                &nbsp; &nbsp; &nbsp;
                                                <asp:Label ID="lblMinOrderValue" runat="server" /></td>
                                        </tr>
                                        <tr class="value">
                                            <td style="width: 19%">
                                                Sales Person:<br />
                                                <br />
                                                Telephone:</td>
                                            <td>
                                                <asp:Label ID="lblSalesPerson" runat="server" /><br />
                                                <br />
                                                <asp:Label ID="lblSalesPersonPhone" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td width="85%" class="evenCells" colspan="2">
                                                Type of Business Organization: &nbsp; &nbsp;<asp:Label ID="lblOrgType" runat="server" /></td>
                                        </tr>
                                        <tr class="value">
                                            <td style="width: 19%">
                                                Ownership:</td>
                                            <td>
                                                <asp:Label ID="lblOwnershipFilipino" runat="server" />
                                                Filipino&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:Label
                                                    ID="lblOwnershipOther" runat="server" />
                                                Other Nationality</td>
                                        </tr>
                                        <tr class="evenCells">
                                            <td style="width: 19%">
                                                Company Classification:</td>
                                            <td>
                                                <asp:Label ID="lblClassification" runat="server" />
                                            </td>
                                        </tr>
                                        <tr class="value">
                                            <td style="width: 19%">
                                                Sole Supplier:</td>
                                            <td>
                                                <asp:Label ID="lblSoleSupplier" runat="server" /></td>
                                        </tr>
                                        <tr class="evenCells">
                                            <td style="width: 19%">
                                                Specialization:</td>
                                            <td>
                                                <asp:Label ID="lblSpecialization" runat="server" /></td>
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
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetailsV"
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
                                            <th colspan="3">
                                                Supplier Type:</th>
                                            <th colspan="3">
                                                Category:</th>
                                            <%--     <th colspan="2">
                                                    Sub Category:</th>--%>
                                        </tr>
                                        <tr>
                                            <td class="value" colspan="3">
                                                &nbsp;<asp:Label runat="server" ID="lblSupplierType" /></td>
                                            <td class="value" colspan="3">
                                                &nbsp;<asp:Label runat="server" ID="lblCategory" /></td>
                                            <%-- <td class="value" colspan="2">
                                                    <asp:Label runat="server" ID="lblSubCategory" /></td>--%>
                                        </tr>
                                        <tr>
                                            <th colspan="3">
                                                ISO Classification:</th>
                                            <th colspan="3">
                                                PCAB Class:</th>
                                        </tr>
                                        <tr>
                                            <td class="value" colspan="3">
                                                &nbsp;<asp:Label runat="server" ID="lblISOStandard"></asp:Label>
                                            </td>
                                            <td class="value" colspan="3">
                                                &nbsp;<asp:Label runat="server" ID="lblPCABClass" /></td>
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
                                    <p>&nbsp;
                                        </p>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" OnClick="lnkEdit_Click" Visible="false"></asp:LinkButton><a
                                                    href="index.aspx">OK</a></td>
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
