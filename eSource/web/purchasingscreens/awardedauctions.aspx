<%@ Page Language="C#" AutoEventWireup="true" CodeFile="awardedauctions.aspx.cs"
    Inherits="web_purchasing_screens_awardedAuctionItems" %>

<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/WEB/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_TopNav_Auct" Src="~/web/usercontrol/Purchasing/Purchasing_TopNav_Auct.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_LeftNav" Src="~/web/usercontrol/Purchasing/Purchasing_LeftNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_LeftNav_Not" Src="~/web/usercontrol/Purchasing/Purchasing_LeftNav_Auct_Not.ascx" %>
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
                                                <EBid:Purchasing_LeftNav runat="server" ID="Purchasing_LeftNav" />
                                              
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
                                                                <div align="left">
                                                                    <h1>
                                                                        <br />
                                                                        Awarded Auction Items</h1>
                                                                    <br />
                                                                    <div id='pnl_buyerMenu' runat="server">
                                                                    <asp:CheckBoxList ID="chkbuyeropts" runat="server" AutoPostBack="True" 
                                                                        Font-Names="Arial" Font-Size="11px" OnSelectedIndexChanged="chkbuyeropts_SelectedIndexChange" 
                                                                        RepeatDirection="Horizontal" RepeatLayout="Table" Width="330px">
                                                                        <asp:ListItem Selected="True" Value='0'>Trans-Asia</asp:ListItem>
                                                                        <asp:ListItem Selected="True" Value='1'></asp:ListItem>
                                                                        <asp:ListItem Selected="True" Value='2'>2</asp:ListItem>
                                                                    </asp:CheckBoxList>
											                        </div>
                                                                    <asp:GridView ID="gvAuctions" runat="server" SkinID="BidEvents" AllowPaging="True"
                                                                        AllowSorting="True" AutoGenerateColumns="False" OnRowCommand="gvAuctions_RowCommand"
                                                                        DataSourceID="dsAwardedAuctions">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Detail No." SortExpression="AuctionDetailNo">
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <ItemTemplate>
                                                                                    &nbsp;<asp:LinkButton CommandName="SelectItem" ID="lnkRefNo" runat="server" Text='<%# Bind("AuctionDetailNo") %>'
                                                                                        CommandArgument='<%# Bind("AuctionDetailNo") %>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="90px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Auction Items" SortExpression="Description">
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <ItemTemplate>
                                                                                    &nbsp;<asp:LinkButton CommandName="SelectItem" ID="lnkItemDesc" runat="server" Text='<%# Bind("Description") %>'
                                                                                        CommandArgument='<%# Bind("AuctionDetailNo") %>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Auction Event" SortExpression="Item">
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <ItemTemplate>
                                                                                    &nbsp;<asp:LinkButton CommandName="SelectEvent" ID="lnkevent" runat="server" Text='<%# Eval("Item").ToString() + " - " + Eval("ItemDesc").ToString() %>'
                                                                                        CommandArgument='<%# Bind("AuctionRefNo") %>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Company" SortExpression="Company">
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCompany" runat="server" Text='<%# Bind("Company") %>'></asp:Label>                                                                     
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Winner" SortExpression="VendorName">
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <ItemTemplate>
                                                                                    &nbsp;<asp:Label ID="lblWinner" runat="server" Text='<%# Bind("VendorName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Date Approved" SortExpression="DateApproved">
                                                                                <HeaderStyle HorizontalAlign="Center" />                                                                                
                                                                                <ItemStyle HorizontalAlign="Center" Width="180px" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDateApproved" runat="server" Text='<%# Bind("DateApproved", "{0:D}<br />{0:T}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="False">
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAuctionRefNo" runat="server" Text='<%# Bind("AuctionRefNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                    <asp:SqlDataSource ID="dsAwardedAuctions" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                        SelectCommand="sp_GetPurchasingAwardedAuctionItems" SelectCommandType="StoredProcedure">
                                                                        <SelectParameters>
                                                                            <asp:SessionParameter Name="PurchasingId" SessionField="PurchasingId" Type="Int32" />
                                                                        </SelectParameters>
                                                                    </asp:SqlDataSource>
                                                                    <br />                                                                    
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
                        <EBid:Footer runat="server" ID="Footer1" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
