<%@ Page Language="C#" AutoEventWireup="true" CodeFile="auctioncalendar.aspx.cs" Inherits="web_buyerscreens_auctionCalendar" Theme="default" MaintainScrollPositionOnPostback="true" %>

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
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <EBid:LeftNavAuctions1 runat="server" ID="LeftNavAuctions1" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                                <h2>
                                                    Auction Calendar</h2>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Calendar runat="server" ID="auctCalendar" OnSelectionChanged="auctCalendar_SelectionChanged" SkinID="Calendar" ShowGridLines="True"></asp:Calendar>
                                                <p>
                                                    &nbsp;</p>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td id="content">
                                    <div align="left">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page0">
                                            <tr>
                                                <td valign="top">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td id="content0">
                                                                <h1>
                                                                    <br />
                                                                    Auction Calendar</h1>
                                                                <br />
                                                                <asp:GridView runat="server" ID="gvAuctionEvents" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" EmptyDataText="No Auction Event to display at the moment." DataSourceID="dsAuctionEvent"
                                                                    SkinID="BidEvents">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Reference No." SortExpression="AuctionRefNo">
                                                                            <HeaderStyle HorizontalAlign="center" />
                                                                            <ItemStyle Width="90px" />
                                                                            <ItemTemplate>
                                                                                &nbsp;<asp:LinkButton CommandName="Select" ID="lblRefNo" runat="server" Text='<%# Bind("AuctionRefNo") %>' 
                                                                                    CommandArgument='<%# Bind("AuctionRefNo") %>' OnCommand="lblAuctionItems_Command" Width="95%"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Auction Events" SortExpression="ItemDesc">
                                                                            <HeaderStyle HorizontalAlign="center" />
                                                                            <ItemTemplate>
                                                                                &nbsp;<asp:LinkButton CommandName="Select" ID="lblAuctionItems" runat="server" Text='<%# Bind("ItemDesc") %>' CommandArgument='<%# Bind("AuctionRefNo") %>' OnCommand="lblAuctionItems_Command"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Start Time" SortExpression="AuctionStartTime">
                                                                            <HeaderStyle HorizontalAlign="center" />
                                                                            <ItemStyle HorizontalAlign="center" Width="150px" />
                                                                            <ItemTemplate>
                                                                                &nbsp;<asp:Label ID="lblStartDate" runat="server" Text='<%# Bind("AuctionStartDateTime", "{0:D}&nbsp;{0:t}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="End Time" SortExpression="AuctionEndDateTime">
                                                                            <HeaderStyle HorizontalAlign="center" />
                                                                            <ItemStyle HorizontalAlign="center" Width="150px" />
                                                                            <ItemTemplate>
                                                                                &nbsp;<asp:Label ID="lblEndDate" runat="server" Text='<%# Bind("AuctionEndDateTime", "{0:D}&nbsp;{0:t}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                                <asp:SqlDataSource ID="dsAuctionEvent" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="s3p_EBid_BuyerApprovedAuctionsByDate" SelectCommandType="StoredProcedure">
                                                                    <SelectParameters>
                                                                        <asp:ControlParameter ControlID="auctCalendar" Name="AuctionDate" PropertyName="SelectedDate" Type="DateTime" />
                                                                        <asp:SessionParameter Name="buyerId" SessionField="userid" Type="Int32" />
                                                                    </SelectParameters>
                                                                </asp:SqlDataSource>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    <br />
                                                    <table id="actions" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <p>
                                                        &nbsp;</p>
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
                        <EBid:Footer runat="server" ID="Footer1" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
