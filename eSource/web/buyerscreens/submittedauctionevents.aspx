<%@ Page Language="C#" AutoEventWireup="true" CodeFile="submittedauctionevents.aspx.cs" Inherits="web_buyer_screens_SubmittedAuctionEvents" %>

<%@ Register TagPrefix="EBid" TagName="TopNavAuction" Src="~/WEB/usercontrol/Buyer/TopNavAuctions.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNav_Auctions" Src="~/web/usercontrol/Buyer/TopNav2_Auction.ascx" %>
<%@ Register TagPrefix="EBid" TagName="LeftNavAuctions1" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="LeftNavNotifications" Src="~/web/usercontrol/Buyer/LeftNavNotifications.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
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
                                    <EBid:TopNav_Auctions runat="server" ID="TopNav_Auctions" />
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
                                        <EBid:LeftNavAuctions1 runat="server" ID="LeftNavAuctions1" />
                                    </div>
                                    <h2>
                                        Notifications</h2>
                                    <div align="left">
                                        <EBid:LeftNavNotifications runat="server" ID="LeftNavNotifications" />
                                    </div>
                                    <p>&nbsp;
                                        </p>
                                </td>
                                <td id="content">
                                    <div align="left">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table5">
                                            <tr>
                                                <td>
                                                    <h1>
                                                        <br />
                                                        Auction Events For Approval
                                                    </h1>
                                                    <p>
                                                        These are your submitted auction events for approval. Click on the auction event reference number or the auction event description to view the details of that event.
                                                    </p>
                                                    <asp:GridView ID="gvAuctions" runat="server" SkinID="BidEvents" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" OnRowCommand="gvAuctions_RowCommand" OnSorting="gvAuctions_Sorting" OnPageIndexChanging="gvAuctions_PageIndexChanging">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Reference No." SortExpression="AuctionRefNo">
                                                                <HeaderStyle HorizontalAlign="center" />
                                                                <ItemStyle Width="90px" />
                                                                <ItemTemplate>
                                                                    &nbsp;<asp:LinkButton CommandName="Select" ID="lnkRefNo" runat="server" Text='<%# Bind("AuctionRefNo") %>' CommandArgument='<%# Bind("AuctionRefNo") %>' Width="95%"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Auction Event" SortExpression="ItemDesc">
                                                                <HeaderStyle HorizontalAlign="center" />
                                                                <ItemTemplate>
                                                                    &nbsp;<asp:LinkButton CommandName="Select" ID="lnkItemDesc" runat="server" Text='<%# Bind("ItemDesc") %>' CommandArgument='<%# Bind("AuctionRefNo") %>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date Submitted" SortExpression="DateSubmitted">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="180px" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDateSubmitted" runat="server" Text='<%# Bind("DateSubmitted", "{0:D}<br />{0:T}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBidRefNo" runat="server" Text='<%# Bind("AuctionRefNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <br />
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                        <tr>
                                                            <td align="left">&nbsp;
                                                                </td>
                                                        </tr>
                                                    </table>
                                                    <p>&nbsp;
                                                        </p>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td id="footer" height="50px">
                        <EBid:Footer runat="server" ID="Footer" />
                        <input type="hidden" runat="server" id="hdnUserId" name="hdnUserId" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
