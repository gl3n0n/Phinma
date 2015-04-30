<%@ Page Language="C#" AutoEventWireup="true" CodeFile="auctionitemsforawarding.aspx.cs"
    Inherits="web_purchasingscreens_auctionItemsForAwarding" %>

<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/WEB/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_TopNav_Auct" Src="~/web/usercontrol/Purchasing/Purchasing_TopNav_Auct.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_LeftNav" Src="~/web/usercontrol/Purchasing/Purchasing_LeftNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_LeftNav_Not" Src="~/web/usercontrol/Purchasing/Purchasing_LeftNav_Auct_Not.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Purchasing/Footer.ascx" %>
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
                <tr valign="top">
                    <td valign="top">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="height: 100%">
                            <tr height="100%" valign="top">
                                <td id="relatedInfo" valign="top">
                                    <div align="left">
                                        <EBid:Purchasing_LeftNav runat="server" ID="Purchasing_LeftNav" />
                                        
                                    </div>
                                </td>
                                <td id="content" valign="top" style="height: 100%">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page0">
                                        <tr>
                                            <td valign="top">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td id="content0">
                                                            <h1>
                                                                <br />
                                                                Endorsed Auction Items</h1>
                                                            <br />
                                                            <asp:GridView runat="server" ID="gvAuctionItems" AllowPaging="True"
                                                                AllowSorting="True" AutoGenerateColumns="False" SkinID="bidevents"
                                                                DataSourceID="dsEndorsedAuctionItems" OnRowCommand="gvAuctions_RowCommand">                                                                
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Reference No." SortExpression="AuctionRefNo">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle Width="90px" />
                                                                        <ItemTemplate>
                                                                            &nbsp;<asp:LinkButton CommandName="AuctionEvent" ID="lblRefNo" runat="server"
                                                                                Text='<%# Bind("AuctionRefNo") %>' CommandArgument='<%# Bind("AuctionRefNo", "{0}") %>' Width="95%"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Auction Event" SortExpression="ItemDesc">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            &nbsp;<asp:LinkButton CommandName="AuctionEvent" ID="lblAuctItems" runat="server"
                                                                                Text='<%# Bind("ItemDesc") %>' CommandArgument='<%# Bind("AuctionRefNo", "{0}") %>'></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Start Date" SortExpression="AuctionStartDateTime" >
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" Width="180px" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblStartDateTime" runat="server" Text='<%# Bind("AuctionStartDateTime", "{0:D}<br />{0:T}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="End Date" SortExpression="AuctionEndDateTime">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" Width="180px" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblEndDateTime" runat="server" Text='<%# Bind("AuctionEndDateTime", "{0:D}<br />{0:T}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Status">
                                                                        <HeaderStyle HorizontalAlign="center" />
                                                                        <ItemStyle HorizontalAlign="center" Width="190px" />
                                                                        <ItemTemplate>
                                                                            <div>
                                                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <p style="padding-left: 3px">
                                                                                                Pending:&nbsp;
                                                                                                <asp:Label ID="lblPending" runat="server" Font-Bold="true" Text='<%# Eval("PendingCount", "{0}").ToString() + "/" + Eval("TotalCount", "{0}")  %>'></asp:Label>
                                                                                            </p>
                                                                                        </td>
                                                                                        <td>
                                                                                            <p style="padding-right: 3px">
                                                                                                Approved:&nbsp;
                                                                                                <asp:Label ID="lblApproved" runat="server" Font-Bold="true" Text='<%# Eval("ApprovedCount", "{0}").ToString() + "/" + Eval("TotalCount", "{0}")  %>'></asp:Label>
                                                                                            </p>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <p style="padding-left: 3px">
                                                                                                Disapproved:&nbsp;
                                                                                                <asp:Label ID="lblDisapproved" runat="server" Font-Bold="true" Text='<%# Eval("DisapprovedCount", "{0}").ToString() + "/" + Eval("TotalCount", "{0}")  %>'></asp:Label>
                                                                                            </p>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EmptyDataRowStyle ForeColor="Black" />
                                                            </asp:GridView>
                                                            <asp:SqlDataSource ID="dsEndorsedAuctionItems" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                SelectCommand="s3p_EBid_QueryAuctionItemsForAwarding" SelectCommandType="StoredProcedure">
                                                            </asp:SqlDataSource>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 20px;">
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
