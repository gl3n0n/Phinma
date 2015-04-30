<%@ Page Language="C#" AutoEventWireup="true" CodeFile="finishedauctionevents.aspx.cs" Inherits="web_onlineAuction_UpcomingAuctionEvents" Theme="default" %>

<%@ Register Src="../usercontrol/AuctionVendor/AuctionVendor_TopNav_Finished.ascx" TagName="AuctionVendor_TopNav_Finished" TagPrefix="uc1" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/web/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AuctionVendor_TopNav_Upcoming" Src="~/web/usercontrol/AuctionVendor/AuctionVendor_TopNav_Upcoming.ascx" %>
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
        <form id="frmFinished" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
                <tr>
                    <td valign="top" style="height: 137px">
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
                                    <uc1:AuctionVendor_TopNav_Finished ID="AuctionVendor_TopNav_Finished1" runat="server"></uc1:AuctionVendor_TopNav_Finished>
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
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td>
                                                <h1>
                                                    <br />
                                                    Finished Auction Events</h1>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <div id='pnl_buyerMenu' runat="server">
                                                    <asp:CheckBoxList ID="chkbuyeropts" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="11px" RepeatDirection="Horizontal" RepeatLayout="Table"
                                                        Width="330px">
                                                        <asp:ListItem Selected="True" Value='0'>For Endorsement</asp:ListItem>
                                                        <asp:ListItem Selected="False" Value='1'>Elapsed</asp:ListItem>
                                                        <asp:ListItem Selected="False" Value='2'>Awarded</asp:ListItem>
                                                        <asp:ListItem Selected="False" Value='3'>Failed</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </div>
                                                <div id='pnl_Menu' runat="server">
                                                    <asp:CheckBoxList ID="chkauctiontype" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="11px" RepeatDirection="Horizontal" RepeatLayout="Table" Width="288px">
                                                        <asp:ListItem Selected="true" Value='0'>Forward Auction</asp:ListItem>
                                                        <asp:ListItem Selected="true" Value='1'>Reverse Auction</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </div>
                                                <asp:GridView ID="gvAuctionEvents" runat="server" AutoGenerateColumns="False" SkinID="AuctionEvents" DataKeyNames="AuctionRefNo" AllowPaging="True" AllowSorting="True" EmptyDataText="There are no finished auctions at this moment."
                                                    DataSourceID="dsFinishedAuctions" OnRowCreated="gvAuctionEvents_RowCreated">
                                                    <EmptyDataRowStyle HorizontalAlign="Center" Height="25px" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Reference No." SortExpression="AuctionStartDateTime">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle Width="90px" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                &nbsp;<asp:LinkButton ID="lblrefno" runat="server" Text='<%# Bind("AuctionRefNo") %>' CommandName="SelectAuctionItem" CommandArgument='<%#Bind("AuctionRefNo") %>' OnCommand="lblAuctionEvents_Command">'></asp:LinkButton><br />
                                                                <asp:Label ID="Label1" runat="server" ForeColor="gray" Text='<%# Bind("AuctionType1") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="&#160;Auction Events&#160;" SortExpression="ItemDesc">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                &nbsp;<asp:LinkButton ID="lblAuctionEvents" runat="server" Text='<%# Bind("ItemDesc") %>' CommandName="SelectAuctionItem" CommandArgument='<%#Bind("AuctionRefNo") %>' OnCommand="lblAuctionEvents_Command">'></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="&#160;Start Date and Time&#160;" SortExpression="AuctionStartDateTime">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle Width="180px" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStartDate" runat="server" Text='<%# Eval("AuctionStartDateTime", "{0:D}<br />{0:T}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="&#160;End Date and Time&#160;" SortExpression="AuctionEndDateTime">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle Width="180px" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEndDate" runat="server" Text='<%# Eval("AuctionEndDateTime", "{0:D}<br />{0:T}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status">
                                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="Center" />
                                                            <ItemStyle Width="190px" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <div>
                                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                                        <tr>
                                                                            <td>
                                                                                <p style="padding-left: 3px">
                                                                                    Pending:&nbsp;
                                                                                    <asp:Label ID="lblPending" runat="server" Font-Bold="true" Text='<%# Eval("PendingCount", "{0}").ToString() + "/" + Eval("DetailsCount", "{0}")  %>'></asp:Label>
                                                                                </p>
                                                                            </td>
                                                                            <td>
                                                                                <p style="padding-right: 3px">
                                                                                    Approved:&nbsp;
                                                                                    <asp:Label ID="lblApproved" runat="server" Font-Bold="true" Text='<%# Eval("ApprovedCount", "{0}").ToString() + "/" + Eval("DetailsCount", "{0}")  %>'></asp:Label>
                                                                                </p>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <p style="padding-left: 3px">
                                                                                    Disapproved:&nbsp;
                                                                                    <asp:Label ID="lblDisapproved" runat="server" Font-Bold="true" Text='<%# Eval("DisapprovedCount", "{0}").ToString() + "/" + Eval("DetailsCount", "{0}")  %>'></asp:Label>
                                                                                </p>
                                                                            </td>
                                                                            <td>
                                                                                <p style="padding-right: 3px">
                                                                                    Re-Edit:&nbsp;
                                                                                    <asp:Label ID="lblReedit" runat="server" Font-Bold="true" Text='<%# Eval("ReeditCount", "{0}").ToString() + "/" + Eval("DetailsCount", "{0}")  %>'></asp:Label>
                                                                                </p>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEndorse" runat="server" CommandArgument='<%# Eval("AuctionRefNo") %>' OnCommand="lnkEndorse_Command" Enabled='<%# IsEndorsement(Eval("StatusDesc").ToString()) %>' Visible='<%# IsEndorsement(Eval("StatusDesc").ToString()) %>'>Endorse</asp:LinkButton>
                                                                <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("StatusDesc").ToString() %>' Visible='<%# !IsEndorsement(Eval("StatusDesc").ToString()) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:SqlDataSource ID="dsFinishedAuctions" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetFinishedAuctions" SelectCommandType="StoredProcedure">
                                                    <SelectParameters>
                                                        <asp:SessionParameter Name="UserId" SessionField="UserId" Type="Int32" />
                                                        <asp:SessionParameter Name="UserType" SessionField="UserType" Type="Int32" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            </td>
                                        </tr>
                                    </table>
                                    <td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblMessage" runat="server" Text="" Font-Size="11px" ForeColor="red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
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
