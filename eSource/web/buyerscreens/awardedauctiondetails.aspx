<%@ Page Language="C#" AutoEventWireup="true" CodeFile="awardedauctiondetails.aspx.cs" Inherits="web_buyerscreens_AwardedAuctionDetails" %>

<%@ Register TagPrefix="EBid" TagName="TopNavAuction" Src="~/WEB/usercontrol/Buyer/TopNavAuctions.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNav_Auctions" Src="~/web/usercontrol/Buyer/TopNav2_Auction.ascx" %>
<%@ Register TagPrefix="EBid" TagName="LeftNavAuctions1" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="LeftNavNotifications" Src="~/web/usercontrol/Buyer/LeftNavNotifications.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
    <%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_buyer.css" %>' />
</head>
<body style="height: 100%;">
    <div align="left">
        <form id="Form1" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
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
                                    <EBid:TopNav_Auctions runat="server" ID="TopNav_Auctions" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td id="relatedInfo">
                                    <div align="left">
                                        <EBid:LeftNavAuctions1 runat="server" ID="LeftNavAuctions1" />
                                    </div>
                                    <div align="left">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table4">
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <p>
                                        &nbsp;</p>
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
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails">
                                                                <tr>
                                                                    <th colspan="2">
                                                                        Auction Event&nbsp;<asp:Label runat="server" ID="lblRefNo"></asp:Label></th>
                                                                </tr>
                                                            </table>
                                                            <div align="left">
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails">
                                                                    <tr>
                                                                        <th>
                                                                            Status</th>
                                                                        <td class="value">
                                                                            <asp:LinkButton runat="server" ID="btnAwarded" OnClick="btnAwarded_Click">Awarded</asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails">
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
                                                                        Reference Number:</td>
                                                                    <td class="value">
                                                                        <asp:Label runat="server" ID="lblReferenceNumber"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="133">
                                                                        Sub-category:</td>
                                                                    <td class="value">
                                                                        <asp:Label runat="server" ID="lblSubCategory"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="133">
                                                                        Deadline:</td>
                                                                    <td class="value">
                                                                        <asp:Label runat="server" ID="lblDeadline"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="133">
                                                                        Delivery Date</td>
                                                                    <td class="value">
                                                                        <asp:Label runat="server" ID="lblDeliveryDate"></asp:Label>6</td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="133">
                                                                        Item Description</td>
                                                                    <td class="value">
                                                                        <asp:Label runat="server" ID="lblItemDescription"></asp:Label>
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails">
                                                                <tr>
                                                                    <th colspan="2">
                                                                        Auction Event details</th>
                                                                </tr>
                                                                <tr>
                                                                    <td width="25%">
                                                                        Auction type:
                                                                    </td>
                                                                    <td class="value" width="79%">
                                                                        <asp:Label runat="server" ID="lblAuctionType"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="25%">
                                                                        Auction Confirmation Deadline:</td>
                                                                    <td class="value" width="79%">
                                                                        <asp:Label runat="server" ID="lblAuctionConfirmationDeadline"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="25%">
                                                                        Auction Event Date:</td>
                                                                    <td class="value" width="79%">
                                                                        <asp:Label runat="server" ID="lblAuctionEventDate"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="25%">
                                                                        Auction Start Time:</td>
                                                                    <td class="value" width="79%">
                                                                        <asp:Label runat="server" ID="lblAuctionStartTime"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="25%">
                                                                        Auction End Time:</td>
                                                                    <td class="value" width="79%">
                                                                        <asp:Label runat="server" ID="lblAuctionEndTime"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="25%">
                                                                        Bid Currency:</td>
                                                                    <td class="value" width="79%">
                                                                        <asp:Label runat="server" ID="lblBidCurrency"></asp:Label></td>
                                                                </tr>
                                                            </table>
                                                            <asp:GridView runat="server" ID="gvAuctionItems" AutoGenerateColumns="false" CssClass="itemDetails">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="itemDetails_th" ItemStyle-CssClass="itemDetails_td">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblItem" Text='<%# Bind("PItemName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="itemDetails_th" ItemStyle-CssClass="value">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblDesc" Text='<%# Bind("Description") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Quantity" HeaderStyle-CssClass="itemDetails_th" ItemStyle-CssClass="value">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblQuantity" Text='<%# Bind("Quantity") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Unit of Measure" HeaderStyle-CssClass="itemDetails_th" ItemStyle-CssClass="value">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblUnitOfMeasure" Text='<%# Bind("UnitOfMeasure") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <asp:GridView runat="server" ID="gvFiles" AutoGenerateColumns="false" CssClass="itemDetails">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="File Attachments" HeaderStyle-CssClass="itemDetails_th" ItemStyle-CssClass="value">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblFiles" Text='<% #Bind("FileAttachment")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <asp:GridView runat="server" ID="gvSuppliers" AutoGenerateColumns="false" CssClass="itemDetails">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Invited Suppliers" HeaderStyle-CssClass="itemDetails_th" ItemStyle-CssClass="value">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblSuppliers" Text='<% #Bind("Supplier")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <br />
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                                <tr>
                                                                    <td>
                                                                        <asp:LinkButton runat="server" ID="btnOK" OnClick="btnOK_Click">OK</asp:LinkButton></td>
                                                                </tr>
                                                            </table>
                                                            <p>
                                                                &nbsp;</p>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td id="footer">
                        <EBid:Footer runat="server" ID="Footer1" />
                        <input type="hidden" runat="server" id="hdnAuctionRefNo" name="hdnAuctionRefNo" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
