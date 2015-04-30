<%@ Page Language="C#" AutoEventWireup="true" CodeFile="auctioncalendar.aspx.cs"
    Inherits="web_vendorscreens_auctionCalendar" Theme="default" MaintainScrollPositionOnPostback="true"%>
<%@ Register Src="../usercontrol/Vendor/Vendor_LeftNav_Notifications.ascx" TagName="Vendor_LeftNav_Notifications" TagPrefix="uc1" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Vendor/Footer.ascx"  %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/web/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_TopNav_Auction" Src="~/web/usercontrol/Vendor/Vendor_TopNav_Auction.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_LeftNav_Bids" Src="~/web/usercontrol/Vendor/Vendor_LeftNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_LeftNav_Auctions" Src="~/web/usercontrol/Vendor/Vendor_LeftNav_Bids.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
    <%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
	<title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_v.css" %>' />
    <script type="text/javascript" src="../include/util.js"></script>
</head>
<body onload="SetStatus();">
    <div align="left">
        <form id="Form1" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" id="page">
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
                                                <EBid:Vendor_LeftNav_Auctions runat="server" ID="Vendor_LeftNav_Auctions" />
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <h2>
                                                    Auction Calendar</h2>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Calendar runat="server" ID="auctCalendar" OnSelectionChanged="auctCalendar_SelectionChanged"
                                                    SkinID="Calendar"></asp:Calendar>
                                                <p>
                                                    &nbsp;</p>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td id="content">
                                    <h1>
                                        <br />
                                        Auction Calendar</h1>
                                    <br />                                    
                                    <asp:GridView runat="server" ID="gvAuctionEvents" AllowPaging="True"
                                        AllowSorting="True" AutoGenerateColumns="False" SkinID="BidEvents"
                                        EmptyDataText="There Are No Auction Event(s) On This Date." DataSourceID="dsAuctionEvent">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Reference No." SortExpression="AuctionRefNo">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="90px" />
                                                <ItemTemplate>
                                                    &nbsp;<asp:LinkButton CommandName="Select" ID="lblRefNo" runat="server" Text='<%# Bind("AuctionRefNo") %>'
                                                        CommandArgument='<%# Bind("AuctionRefNo") %>' OnCommand="lblAuctionItems_Command" Width="95%"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Auction Events" SortExpression="ItemDesc">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    &nbsp;<asp:LinkButton CommandName="Select" ID="lblAuctionItems" runat="server" Text='<%# Bind("ItemDesc") %>'
                                                        CommandArgument='<%# Bind("AuctionRefNo") %>' OnCommand="lblAuctionItems_Command"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Start Date" SortExpression="AuctionStartDateTime">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Width="180px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStartDate" runat="server" Text='<%# Bind("AuctionStartDateTime", "{0:D}<br />{0:T}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="End Date" SortExpression="AuctionEndDateTime">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Width="180px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEndDate" runat="server" Text='<%# Bind("AuctionEndDateTime", "{0:D}<br />{0:T}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:SqlDataSource ID="dsAuctionEvent" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                        SelectCommand="s3p_EBid_ApprovedAuctionsByDate" SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="auctCalendar" DefaultValue="" Name="AuctionDate"
                                                PropertyName="SelectedDate" Type="DateTime" />
                                            <asp:SessionParameter DefaultValue="" Name="Vendorid" SessionField="UserId" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
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
