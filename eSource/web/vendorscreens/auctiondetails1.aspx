<%@ Page Language="C#" AutoEventWireup="true" CodeFile="auctiondetails1.aspx.cs"
    Inherits="web_vendorscreens_AuctionDetails1" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Vendor/Footer.ascx"  %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/web/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_TopNav_Auction" Src="~/web/usercontrol/Vendor/Vendor_TopNav_Auction.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_LeftNav_Auctions" Src="~/web/usercontrol/Vendor/Vendor_LeftNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="CommentArea" Src="~/web/usercontrol/CommentArea.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='../themes/<%= Session["configTheme"] %>/css/style_v.css' />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
</head>
<body style="height: 100%;">
    <div>
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
                                            <tr>
                                                <td>
                                                    <EBid:Vendor_LeftNav_Auctions runat="server" ID="Vendor_LeftNav_Bids" />
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <EBid:CommentArea runat="server" ID="CommentArea" />
                                                    <p>&nbsp;
                                                        </p>
                                                </td>
                                            </tr>
                                        </table>
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
                                                                    Auction Event Details</h1>
                                                                <div align="left">
                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails" height="85">
                                                                        <tr>
                                                                            <th colspan="2">
                                                                                Details</tr>
                                                                        <tr>
                                                                            <td width="133">
                                                                                Auction reference Number:</td>
                                                                            <td class="value">
                                                                                <asp:Label runat="server" ID="lblAuctionReferenceNumber"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="133">
                                                                                Category:</td>
                                                                            <td class="value">
                                                                                <asp:Label runat="server" ID="lblSubCategory"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="25%">
                                                                                Auction Confirmation Deadline:</td>
                                                                            <td class="value">
                                                                                <asp:Label runat="server" ID="lblAuctionDeadline"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="25%">
                                                                                Auction Event Date:</td>
                                                                            <td class="value">
                                                                                <asp:Label runat="server" ID="lblAuctionDate"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="25%">
                                                                                Auction Start Time:</td>
                                                                            <td class="value">
                                                                                <asp:Label runat="server" ID="lblAuctionStartTime"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="25%">
                                                                                Auction End Time:</td>
                                                                            <td class="value">
                                                                                <asp:Label runat="server" ID="lblAuctionEndTime"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="133">
                                                                                Delivery Date:</td>
                                                                            <td class="value">
                                                                                <asp:Label runat="server" ID="lblDeliveryDate"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="133">
                                                                                Auction Item Description:</td>
                                                                            <td class="value">
                                                                                <asp:Label runat="server" ID="lblItemDesc"></asp:Label></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <div align="left">
                                                                    <asp:GridView AutoGenerateColumns="false" runat="server" ID="gvAuctionItemDetails"
                                                                        CssClass="itemDetails">
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
                                                    <%--<a href="Auctions.aspx">Ok</a>--%>
                                                </td>
                                            </tr>
                                        </table>
                                        <p>
                                            &nbsp;
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
        &nbsp;
    </div>
</body>
</html>
