<%@ Page Language="C#" AutoEventWireup="true" CodeFile="report_totalauctionevents_items.aspx.cs" Inherits="web_purchasingscreens_report_totalauctionevents_items" %>

<%@ Register Src="~/web/usercontrol/Purchasing/Purchasing_LeftNav_Bids.ascx" TagName="Purchasing_LeftNav_Reports" TagPrefix="uc1" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/WEB/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_TopNav_Reports" Src="~/web/usercontrol/Purchasing/Purchasing_TopNav_Reports.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Purchasing/Footer.ascx"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_ph.css" %>' />
    <script type="text/javascript" src="../include/util.js"></script>
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
                    <td class="content">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="height: 100%">
                            <tr>
                                <td id="relatedInfo">
                                    <uc1:Purchasing_LeftNav_Reports ID="Purchasing_LeftNav_Reports1" runat="server" />
                                </td>
                                <td id="content">
                                    <div align="left">
                                        <br />
                                        <h1>
                                            Auction Events (<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>)</h1>
                                        <div>
                                        <!-- contents here //-->
                                        
                                        
                                        <br />
                                        <asp:GridView ID="gvBids" runat="server" SkinID="BidEvents" AllowPaging="True" AllowSorting="True"
                                            AutoGenerateColumns="False" DataKeyNames="AuctionRefNo" DataSourceID="dsTotalAuctionEventsItems"
                                            OnRowCommand="gvBids_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Reference No." InsertVisible="False" SortExpression="AuctionRefNo">
                                                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                                    <ItemTemplate>
                                                        &nbsp;<asp:LinkButton ID="lnkRefNo" runat="server" Text='<%# Bind("AuctionRefNo") %>'
                                                            CommandArgument='<%# Bind("AuctionRefNo") %>' CommandName="Details"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Auction Event" SortExpression="ItemDesc">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        &nbsp;<asp:LinkButton ID="lnkDesc" runat="server" Text='<%# Bind("ItemDesc") %>'
                                                            CommandArgument='<%# Bind("AuctionRefNo") %>' CommandName="Details"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date Created" SortExpression="DateCreated">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="180px" />
                                                    <ItemTemplate>
                                                        &nbsp;<asp:Label ID="Label3" runat="server" Text='<%# Bind("DateCreated", "{0:D}<br />{0:T}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date Approved" SortExpression="DateApproved">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="180px" />
                                                    <ItemTemplate>
                                                        &nbsp;<asp:Label ID="Label4" runat="server" Text='<%# Bind("DateApproved", "{0:D}<br />{0:T}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="dsTotalAuctionEventsItems" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                            SelectCommand="sp_GetBuyerApprovedAuctionEventsByCompany" SelectCommandType="StoredProcedure">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="BuyerId" SessionField="totalauctionevents_BuyerId" Type="Int32" />
                                                <asp:SessionParameter Name="CompanyId" SessionField="totalauctionevents_CompanyId" Type="Int32" />
                                                <asp:SessionParameter Name="FromDate" SessionField="totalauctionevents_FromDate" Type="DateTime" />
                                                <asp:SessionParameter Name="ToDate" SessionField="totalauctionevents_ToDate" Type="DateTime" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                        <br />

                                        <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td id="actions">
                                                <asp:LinkButton ID="LinkButton1" runat="server" Width="100px" OnClick="LinkButton1_Click">Back</asp:LinkButton>
                                                <asp:LinkButton ID="lnkViewReport" runat="server" OnClick="lnkViewReport_Click" Width="100px">View Report</asp:LinkButton>
                                            </td>
                                        </tr>
                                        </table>
                                        <br />

                                        <!-- contents here //-->
                                        </div>
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
