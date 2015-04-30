<%@ Page Language="C#" AutoEventWireup="true" CodeFile="endorseauction.aspx.cs" Inherits="web_onlineAuction_EndorseAuction" Theme="default" %>

<%@ Register Src="../usercontrol/auctionvendor/auctiondetail.ascx" TagName="auctiondetail" TagPrefix="uc4" %>

<%@ Register Src="../usercontrol/CommentBox.ascx" TagName="CommentBox" TagPrefix="uc3" %>
<%@ Register Src="../usercontrol/commentlist_auction.ascx" TagName="commentlist_auction" TagPrefix="uc1" %>
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
                                        Auction Endorsement</h1>
                                    <uc4:auctiondetail ID="Auctiondetail1" runat="server" />
                                    <br />
                                </td>
                            </tr>
                            <tr runat="server" id="trApprove">
                                <td style="padding-left: 20px; padding-right: 10px" height="30px">
                                    <asp:Label ID="lblApproved" runat="server" Font-Size="12px" Font-Bold="true" Text="Approved Endorsements"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" id="trApprove2">
                                <td style="padding-left: 20px; padding-right: 10px">
                                    <asp:GridView ID="GridView1" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="dsApprovedEndorsements" Font-Names="Arial" Font-Size="11px">
                                        <HeaderStyle Font-Bold="True" Font-Size="11px" ForeColor="White" BackColor="#10659E" Height="25px" HorizontalAlign="center" />
                                        <RowStyle Height="20px" />
                                        <AlternatingRowStyle BackColor="#DBEAF5" Height="20px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Item Name" SortExpression="ItemName">
                                                <ItemTemplate>
                                                    &nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Bind("ItemName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Winner" SortExpression="Winner">
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
                                    <asp:SqlDataSource ID="dsApprovedEndorsements" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetApprovedEndorsements" SelectCommandType="StoredProcedure"
                                        OnSelected="dsApprovedEndorsements_Selected">
                                        <SelectParameters>
                                            <asp:SessionParameter DefaultValue="0" Name="AuctionRefNo" SessionField="AuctionRefNo" Type="Int32" />
                                            <asp:SessionParameter DefaultValue="2" Name="UserType" SessionField="UserType" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    <asp:SqlDataSource ID="dsVendorsTenderDetails" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetAllVendorsTender" SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:SessionParameter DefaultValue="0" Name="AuctionRefNo" SessionField="AuctionRefNo" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    <br />
                                </td>
                            </tr>
                            <tr runat="server" id="trDisapprove">
                                <td style="padding-left: 20px; padding-right: 10px" height="12px">
                                    <asp:Label ID="Label5" runat="server" Font-Size="12px" Font-Bold="true" Text="Pending and Disapproved Endorsements"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" id="trDisapprove2">
                                <td valign="top" colspan="2" style="padding-left: 20px; padding-right: 10px">
                                    <asp:GridView ID="gvAuctionLastBids" runat="server" AllowSorting="True" CssClass="itemDetails" DataSourceID="dsAuctionLatestBids" OnRowCreated="gvAuctionLastBids_RowCreated" Width="100%" OnRowDataBound="gvAuctionLastBids_RowDataBound">
                                        <HeaderStyle ForeColor="White" />
                                    </asp:GridView>
                                    <asp:SqlDataSource ID="dsAuctionLatestBids" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetFinishedAuctionLastBids" SelectCommandType="StoredProcedure"
                                        OnSelected="dsAuctionLatestBids_Selected">
                                        <SelectParameters>
                                            <asp:SessionParameter DefaultValue="0" Name="AuctionRefNo" SessionField="AuctionRefNo" Type="Int32" />
                                            <asp:SessionParameter DefaultValue="2" Name="UserType" SessionField="UserType" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    <font style="font-size: 11px; color: Red; font-family: Arial;">&nbsp;&nbsp;<b>NOTE:</b> Select one bidder for each auctioned item.</font>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" colspan="2" style="padding-left: 20px; padding-right: 10px">
                                    <asp:GridView ID="GridView2" runat="server">
                                    </asp:GridView>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 20px; padding-right: 10px" align="center">
                                    <asp:Label ID="lblMessage" runat="server" Font-Size="11px" ForeColor="Red"></asp:Label>
                                    <br />
                                    <br />
                                    <uc3:CommentBox ID="CommentBox1" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding-left: 20px; padding-right: 10px" id="actions">
                                    <asp:LinkButton ID="lnkEndorse" runat="server" OnClick="lnkEndorse_Click">Endorse</asp:LinkButton>
                                    <asp:LinkButton ID="lnkReset" runat="server" OnClick="lnkReset_Click">Reset</asp:LinkButton>
                                    <asp:LinkButton ID="lnkCancel" runat="server" OnClick="lnkCancel_Click">Cancel</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
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
