<%@ Page Language="C#" AutoEventWireup="true" CodeFile="awardedbiditems.aspx.cs" Inherits="web_purchasing_screens_awardedBidItems" %>

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
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_ph.css" %>' />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../include/awardedbiditems.js"></script>

</head>
<body style="height: 100%;">
    <form id="Form1" runat="server">
        <div align="left">
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
                                                <EBid:Purchasing_LeftNav runat="server" ID="Purchasing_LeftNav" />
                                                
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
                                                                Awarded Bid Items</h1>
                                                            <br />
                                                            <div id='pnl_buyerMenu' runat="server" style="display:none;">
                                                                <asp:CheckBoxList ID="chkbuyeropts" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="11px" OnSelectedIndexChanged="chkbuyeropts_SelectedIndexChange" RepeatDirection="Horizontal" RepeatLayout="Table"
                                                                    Width="330px">
                                                                    <asp:ListItem Selected="True" Value='0'>Trans-Asia</asp:ListItem>
                                                                    <asp:ListItem Selected="True" Value='1'></asp:ListItem>
                                                                    <asp:ListItem Selected="True" Value='2'>2</asp:ListItem>
                                                                </asp:CheckBoxList>
                                                            </div>
                                                            <asp:GridView runat="server" ID="gvSearchedItems" SkinID="BidEvents" AllowPaging="True" AllowSorting="true" DataSourceID="dsAwardedItems" AutoGenerateColumns="false" OnRowCommand="gvBids_RowCommand">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Detail No." SortExpression="BidDetailNo">
                                                                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                                        <ItemTemplate>
                                                                            &nbsp;<asp:LinkButton ID="lblDetailNo" runat="server" Text='<%# Bind("BidDetailNo") %>' CommandName="SelectItem" Width="95%" CommandArgument='<%# Bind("BidTenRef") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Bid Item" SortExpression="DetailDesc">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            &nbsp;<asp:LinkButton CommandName="SelectItem" ID="lbBidItems" runat="server" Text='<%# Bind("DetailDesc") %>' CommandArgument='<%# Bind("BidTenRef")%>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Bid Event" SortExpression="ItemDesc">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkBidEvent" CommandName="SelectEvent" runat="server" CommandArgument='<%# Bind("BidTenRef")%>' Text='<%# Bind("ItemDesc") %>' />
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Company" SortExpression="Company">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCompany1" runat="server" Text='<%# Bind("Company") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Bid Item Winner" SortExpression="VendorName">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCompany" runat="server" Text='<%# Bind("VendorName") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Event Creator" SortExpression="BidEventCreator">
                                                                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblbidcreator" runat="server" Text='<%# Bind("BidEventCreator") %>' />
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Date Awarded" SortExpression="DateAwarded">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" Width="180px" />
                                                                        <ItemTemplate>
                                                                            &nbsp;<asp:Label ID="lblDate" runat="server" Text='<%# Eval("DateAwarded", "{0:D}<br />{0:T}") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <asp:SqlDataSource ID="dsAwardedItems" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetPurchasingAwardedItems" SelectCommandType="StoredProcedure">
                                                                <SelectParameters>
                                                                    <asp:SessionParameter Name="PurchasingId" SessionField="UserId" Type="Int32" />
                                                                </SelectParameters>
                                                            </asp:SqlDataSource>
                                                            <br />
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
                    <td id="footer" height="50px">
                        <EBid:Footer runat="server" ID="Footer1" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
