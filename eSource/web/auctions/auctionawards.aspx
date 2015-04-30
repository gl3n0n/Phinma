<%@ Page Language="C#" AutoEventWireup="true" CodeFile="auctionawards.aspx.cs" Inherits="web_onlineAuction_AuctionAwards" Theme="default" %>

<%@ Register Src="../usercontrol/AuctionVendor/auctiondetail.ascx" TagName="auctiondetail" TagPrefix="uc4" %>
<%@ Register Src="../usercontrol/commentlist.ascx" TagName="commentlist" TagPrefix="uc1" %>
<%@ Register Src="../usercontrol/commentlist_auction.ascx" TagName="commentlist_auction" TagPrefix="uc3" %>
<%@ Register Src="../usercontrol/AuctionVendor/AuctionVendor_TopNav_Finished.ascx" TagName="AuctionVendor_TopNav_Finished" TagPrefix="uc2" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/web/usercontrol/TopDate.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
	<link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_oa.css" %>' />
</head>
<body style="height: 100%;">
    <div>
        <form runat="server">
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
                                    <uc2:AuctionVendor_TopNav_Finished ID="AuctionVendor_TopNav_Finished1" runat="server" />
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
                    <td valign="top">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td valign="top" id="content">
                                    <h1>
                                        <br />
                                        Auction Awards</h1>
                                    <uc4:auctiondetail ID="Auctiondetail1" runat="server" />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" colspan="2" style="padding-left: 20px; padding-right: 10px">
                                    <asp:Label ID="Label6" runat="server" Font-Size="12px" Font-Bold="true" Text="Last Bid(s)"></asp:Label>
                                    <asp:GridView ID="gvAuctionLastBids" runat="server" AllowSorting="True" DataSourceID="dsAuctionLatestBids" OnRowCreated="gvAuctionLastBids_RowCreated" Width="100%" OnRowDataBound="gvAuctionLastBids_RowDataBound"
                                        Font-Size="11px">
                                        <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#10659E" Height="25px" HorizontalAlign="Center" />
                                        <RowStyle Height="20px" />
                                    </asp:GridView>
                                    <asp:Label ID="Label5" runat="server" Font-Size="11px" ForeColor="red" Text="&nbsp;&nbsp;NOTE: Click an item above to view its bid history."></asp:Label>
                                    <asp:SqlDataSource ID="dsAuctionLatestBids" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetAuctionLastBids" SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:SessionParameter DefaultValue="0" Name="AuctionRefNo" SessionField="AuctionRefNo" Type="Int32" />
                                            <asp:SessionParameter DefaultValue="2" Name="UserType" SessionField="UserType" Type="Int32" />
                                            <asp:SessionParameter DefaultValue="0" Name="UserId" SessionField="UserId" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" colspan="2" style="padding-left: 20px; padding-right: 10px">
                                    <asp:Label ID="lblApproved" runat="server" Font-Size="12px" Font-Bold="true" Text="Auction Winner(s)"></asp:Label>
                                    <asp:GridView ID="GridView1" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="dsApprovedEndorsements" Font-Names="Arial" Font-Size="11px" EmptyDataText="No winner(s) for this auction yet.">
                                        <EmptyDataRowStyle HorizontalAlign="Center" Height="25px" />
                                        <HeaderStyle Font-Bold="True" Font-Size="11px" ForeColor="White" BackColor="#10659E" Height="25px" HorizontalAlign="Center" />
                                        <RowStyle Height="20px" />
                                        <AlternatingRowStyle BackColor="#DBEAF5" Height="20px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Item Name" SortExpression="ItemName">
                                                <ItemTemplate>
                                                    &nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Bind("ItemDesc") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Winner" SortExpression="Winner">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    &nbsp;<asp:Label ID="Label2" runat="server" Text='<%# Bind("Winner") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Winning Bid" SortExpression="Winning Bid">
                                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("[Winning Bid]") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date Approved" SortExpression="Date Approved">
                                                <ItemStyle Width="120px" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("[Date Approved]") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:SqlDataSource ID="dsApprovedEndorsements" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetApprovedEndorsements" SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:SessionParameter DefaultValue="0" Name="AuctionRefNo" SessionField="AuctionRefNo" Type="Int32" />
                                            <asp:SessionParameter DefaultValue="2" Name="UserType" SessionField="UserType" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    <br />
                                    <uc3:commentlist_auction ID="Commentlist_auction1" runat="server" />
                                    <br />
                                </td>
                            </tr>
                            <tr runat="server" id="trHistory">
                                <td valign="top" colspan="2" style="padding-left: 20px; padding-right: 10px">
                                    <asp:Label ID="lblHistory" runat="server" Font-Size="12px" Font-Bold="true" Text="Bid History"></asp:Label>
                                    <input type="hidden" runat="server" id="hdnLblHistory" />
                                    <asp:GridView ID="gvBidHistory" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="11px" DataSourceID="dsBidHistory" AllowPaging="True" EmptyDataText="No History for this item.">
                                        <EmptyDataRowStyle HorizontalAlign="Center" Height="25px" />
                                        <HeaderStyle Font-Bold="True" Font-Size="11px" ForeColor="White" BackColor="#10659E" Height="25px" HorizontalAlign="Center" />
                                        <RowStyle Height="20px" />
                                        <AlternatingRowStyle BackColor="#DBEAF5" Height="20px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Supplier/Bidder" SortExpression="Supplier">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    &nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Bind("Supplier") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bid" SortExpression="Bid">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Bid") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date Submitted" SortExpression="DateSubmitted">
                                                <ItemStyle Width="120px" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("DateSubmitted") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:SqlDataSource ID="dsBidHistory" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetAuctionedItemBidHistory" SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:QueryStringParameter DefaultValue="0" Name="Auctiondetailno" QueryStringField="a" Type="Int32" />
                                            <asp:SessionParameter DefaultValue="0" Name="Userid" SessionField="UserId" Type="Int32" />
                                            <asp:SessionParameter DefaultValue="0" Name="UserType" SessionField="UserType" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <p>
                                        &nbsp;</p>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding-left: 20px; padding-right: 10px" id="actions">
                                   <asp:LinkButton ID="lnkEndorse" runat="server" Width="100px" OnClick="lnkEndorse_Click" OnClientClick="return confirm('Are you sure you want to endorse this item?');" Visible="False">Endorse Item</asp:LinkButton>
                                   <asp:LinkButton ID="lnkOk" runat="server" OnClick="lnkOk_Click" Width="100px">OK</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <p>
                                        &nbsp;</p>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td id="footer" height="50px">
                        <EBid:Footer runat="server" ID="Footer" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
