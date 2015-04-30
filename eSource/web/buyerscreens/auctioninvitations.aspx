<%@ Page Language="C#" AutoEventWireup="true" CodeFile="auctioninvitations.aspx.cs"
    Inherits="WEB_buyer_screens_AuctionInvitations" %>

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
                                    <p>
                                        &nbsp;</p>
                                </td>
                                <td id="content">
                                    <div align="left">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page0">
                                            <tr>
                                                <td valign="top">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td id="content0">
                                                                <div align="left">
                                                                    <h1>
                                                                        <br />
                                                                        Auction Invitations</h1>
                                                                    <p>
                                                                        These are auction events that has been confirmed, declined and not responded yet.</p>                                                                    
                                                                    <asp:GridView ID="gvAuctionInvitations" runat="server" SkinID="BidEvents"
                                                                        AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                                                        DataKeyNames="AuctionRefNo" DataSourceID="sdsInvitations" OnRowCommand="gvAuctionInvitations_RowCommand">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Reference No." InsertVisible="False" SortExpression="AuctionRefNo">
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <ItemStyle Width="90px" />
                                                                                <ItemTemplate>
                                                                                    &nbsp;<asp:LinkButton ID="Label1" runat="server" Text='<%# Bind("AuctionRefNo") %>' CommandName="ViewDetails" CommandArgument='<%# Bind("AuctionRefNo") %>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Auction Event" SortExpression="ItemDesc">
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <ItemTemplate>
                                                                                    &nbsp;<asp:LinkButton ID="Label2" runat="server" Text='<%# Bind("ItemDesc") %>' CommandName="ViewDetails" CommandArgument='<%# Bind("AuctionRefNo") %>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Pending" SortExpression="PendingCount">
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="Label3" runat="server" Text='<%# String.Format("{0}/{1}", Eval("PendingCount"),Eval("TotalCount")) %>' Enabled='<%# IsEnabled(Eval("PendingCount").ToString()) %>' CommandName="Pending" CommandArgument='<%# Bind("AuctionRefNo") %>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Confirmed" SortExpression="ConfirmedCount">
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="Label4" runat="server" Text='<%# String.Format("{0}/{1}", Eval("ConfirmedCount"),Eval("TotalCount")) %>' Enabled='<%# IsEnabled(Eval("ConfirmedCount").ToString()) %>' CommandName="Confirmed" CommandArgument='<%# Bind("AuctionRefNo") %>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Declined" SortExpression="DeclinedCount">
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="Label5" runat="server" Text='<%# String.Format("{0}/{1}", Eval("DeclinedCount"),Eval("TotalCount")) %>' Enabled='<%# IsEnabled(Eval("DeclinedCount").ToString()) %>' CommandName="Declined" CommandArgument='<%# Bind("AuctionRefNo") %>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                    <asp:SqlDataSource ID="sdsInvitations" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetAuctionInvitations" SelectCommandType="StoredProcedure">
                                                                        <SelectParameters>
                                                                            <asp:SessionParameter DefaultValue="0" Name="BuyerId" SessionField="UserID" Type="Int32" />
                                                                        </SelectParameters>
                                                                    </asp:SqlDataSource>
                                                                    <br />
                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                                        <tr>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <p>
                                                                        &nbsp;</p>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
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
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
