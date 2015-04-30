<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bidsforconversiondetails.aspx.cs" Inherits="web_purchasing_screens_BidsForConversionDetails" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/WEB/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_TopNav_Bids" Src="~/web/usercontrol/Purchasing/Purchasing_TopNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_LeftNav" Src="~/web/usercontrol/Purchasing/Purchasing_LeftNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Purchasing/Footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title>.:| Trans-Asia  eSourcing System |  |:.</title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_ph.css" %>' />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
</head>
<body style="height: 100%;">
    <div align="left">
        <form id="Form1" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
                <tr>
                    <td valign="top" height="137px">
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
                                    <EBid:Purchasing_LeftNav runat="server" ID="LeftNav" />
                                </td>
                                <td id="content">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page0">
                                        <tr>
                                            <td valign="top">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td id="content0">
                                                            <h1>
                                                                <br />
                                                                Bid Event for Conversion</h1>
                                                            <div align="left">
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails" height="85">
                                                                    <tr>
                                                                        <th colspan="2">
                                                                            Details</tr>
                                                                    <tr>
                                                                        <td width="133">
                                                                            Company:</td>
                                                                        <td class="value">
                                                                            <asp:Label runat="server" ID="lblCompany"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="133">
                                                                            Requestor:</td>
                                                                        <td class="value">
                                                                            <asp:Label runat="server" ID="lblRequestor"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="133">
                                                                            PR Number:</td>
                                                                        <td class="value">
                                                                            <asp:Label runat="server" ID="lblPRNumber"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="133">
                                                                            PR Date</td>
                                                                        <td class="value">
                                                                            <asp:Label runat="server" ID="lblPRDate"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="133">
                                                                            Group/Department/Section</td>
                                                                        <td class="value">
                                                                            <asp:Label runat="server" ID="lblGroup"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="133">
                                                                            Bid reference Number:</td>
                                                                        <td class="value">
                                                                            <asp:Label runat="server" ID="lblBidReferenceNumber"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="133">
                                                                            Sub Category:</td>
                                                                        <td class="value">
                                                                            <asp:Label runat="server" ID="lblSubCategory"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="133">
                                                                            Bid Submission Deadline:</td>
                                                                        <td class="value">
                                                                            <asp:Label runat="server" ID="lblBidSubmissionDeadline"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="133">
                                                                            Bid Item Description</td>
                                                                        <td class="value">
                                                                            <asp:Label runat="server" ID="lblBidItemDescription"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="133">
                                                                            Deliver To:
                                                                        </td>
                                                                        <td class="value">
                                                                            <asp:Label runat="server" ID="lblDeliverTo"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="132">
                                                                            Incoterm</td>
                                                                        <td class="value">
                                                                            <asp:Label runat="server" ID="lblIncoterm"></asp:Label></td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <div align="left">
                                                                <asp:GridView AutoGenerateColumns="false" runat="server" ID="gvBidItemDetails" CssClass="itemDetails">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="itemDetails_th" ItemStyle-CssClass="itemDetails_td"
                                                                            ItemStyle-Width="65px">
                                                                            <ItemStyle CssClass="itemDetails_td" />
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblItem" Text='<%#Bind("Item") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Description" ItemStyle-CssClass="valueGridItem" HeaderStyle-CssClass="itemDetails_th">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblDescription" Text='<%#Bind("DetailDesc") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Quantity" ItemStyle-CssClass="valueGridItem" HeaderStyle-CssClass="itemDetails_th"
                                                                            ItemStyle-Width="65px">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblQuantity" Text='<%#Bind("Qty") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Unit Of Measure" ItemStyle-CssClass="valueGridItem"
                                                                            HeaderStyle-CssClass="itemDetails_th" ItemStyle-Width="65px">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblUnitOfMeasure" Text='<%#Bind("UnitOfMeasure") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Delivery Date" ItemStyle-CssClass="valueGridItemRight"
                                                                            HeaderStyle-CssClass="itemDetails_th_right" ItemStyle-Width="65px">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblDeliveryDate" Text='<%#Bind("DeliveryDate1") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                                 <asp:GridView AutoGenerateColumns="false" runat="server" ID="gvInvitedSuppliers"
                                                                    CssClass="itemDetails">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Invited Suppliers" ItemStyle-BackColor="#FFFFFF">
                                                                            <ItemStyle CssClass="itemDetails_td" />
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblVendors" Text='<%#Bind("Supplier") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                        <tr>
                                            <td>
                                                <asp:LinkButton runat="server" ID="btnApprove" OnClick="btnApprove_Click">Approve</asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="btnDeny" OnClick="btnDeny_Click">Deny</asp:LinkButton>
                                                <a href="bidsForConversion.aspx">Cancel</a>
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
