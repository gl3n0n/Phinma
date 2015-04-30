<%@ Page Language="C#" AutoEventWireup="true" CodeFile="biddetails1.aspx.cs" Inherits="web_vendorscreens_bidDetails1" %>

<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="CommentArea" Src="~/web/usercontrol/CommentArea.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Vendor/Footer.ascx"  %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/web/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_TopNav_Bids" Src="~/web/usercontrol/Vendor/Vendor_TopNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_LeftNav_Bids" Src="~/web/usercontrol/Vendor/Vendor_LeftNav_Bids.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
    <%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
	<title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_v.css" %>' />
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
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <td>
                                                <EBid:Vendor_LeftNav_Bids runat="server" ID="Vendor_LeftNav_Bids" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <EBid:CommentArea runat="server" ID="CommentArea" />
                                                <p>
                                                    &nbsp;</p>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td id="content" style="width: 100%">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table5">
                                        <tr>
                                            <td id="content0">
                                                <h1>
                                                    <br />
                                                    Bid Details</h1>
                                                <table id="itemDetails" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tr>
                                                        <th colspan="2">
                                                            Details</th>
                                                    </tr>
                                                    <tr>
                                                        <td width="133">
                                                            Company:</td>
                                                        <td class="value">
                                                            <asp:Label runat="server" ID="lblCompany"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td width="133">
                                                            Category:</td>
                                                        <td class="value">
                                                            <asp:Label runat="server" ID="lblCategory"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td width="133">
                                                            Bid reference Number:</td>
                                                        <td class="value">
                                                            <asp:Label runat="server" ID="lblBidReferenceNumber"></asp:Label></td>
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
                                                            Deliver to:
                                                        </td>
                                                        <td class="value">
                                                            <asp:Label runat="server" ID="lblDeliverTo"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td width="132">
                                                            Preferred Incoterm</td>
                                                        <td class="value">
                                                            <asp:Label runat="server" ID="lblPreferredIncoterm"></asp:Label></td>
                                                    </tr>
                                                </table>
                                                <asp:GridView AutoGenerateColumns="false" runat="server" ID="gvBidItemDetails" CssClass="itemDetails">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="itemDetails_th" ItemStyle-CssClass="itemDetails_td">
                                                            <ItemStyle CssClass="itemDetails_td" />
                                                            <ItemTemplate>
                                                                &nbsp;<asp:Label runat="server" ID="lblItem" Text='<%#Bind("Item") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="60px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Description" ItemStyle-CssClass="valueGridItem" HeaderStyle-CssClass="itemDetails_th">
                                                            <ItemTemplate>
                                                                &nbsp;<asp:Label runat="server" ID="lblDescription" Text='<%#Bind("DetailDesc") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Unit Price" ItemStyle-CssClass="valueGridItem" HeaderStyle-CssClass="itemDetails_th">
                                                            <ItemTemplate>
                                                                &nbsp;<asp:Label runat="server" ID="lblUnitPrice" Text='<%#Bind("UnitPrice", "{0:F}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="60px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Quantity" ItemStyle-CssClass="valueGridItem" HeaderStyle-CssClass="itemDetails_th">
                                                            <ItemTemplate>
                                                                &nbsp;<asp:Label runat="server" ID="lblQuantity" Text='<%#Bind("Qty") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="60px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Unit Of Measure" ItemStyle-CssClass="valueGridItem"
                                                            HeaderStyle-CssClass="itemDetails_th">
                                                            <ItemTemplate>
                                                                &nbsp;<asp:Label runat="server" ID="lblUnitOfMeasure" Text='<%#Bind("UnitOfMeasure") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="60px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Item Price" ItemStyle-CssClass="valueGridItem"
                                                            HeaderStyle-CssClass="itemDetails_th">
                                                            <ItemTemplate>
                                                                &nbsp;<asp:Label runat="server" ID="lbTotalPrice" Text='<%#Bind("TotalUnitCost", "{0:F}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="60px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delivery Date" ItemStyle-CssClass="valueGridItemRight"
                                                            HeaderStyle-CssClass="itemDetails_th_right">
                                                            <ItemTemplate>
                                                                &nbsp;<asp:Label runat="server" ID="lblDeliveryDate" Text='<%#Bind("DeliveryDate1") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="100px" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
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
                        <input type="hidden" runat="server" id="hdnBidRefNo" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
