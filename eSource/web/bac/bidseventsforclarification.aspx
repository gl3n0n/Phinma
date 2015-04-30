<%@ Page Language="c#" EnableEventValidation="true" Inherits="EBid.WEB.bac.bidseventsforclarification" CodeFile="bidseventsforclarification.aspx.cs" %>
<%@ Register Src="../usercontrol/bac/BAC_LeftNav_Bids.ascx" TagName="BAC_LeftNav_Bids" TagPrefix="uc2" %>
<%@ Register Src="../usercontrol/bac/BAC_TopNav_Bids.ascx" TagName="BAC_TopNav_Bids" TagPrefix="uc1" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/WEB/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_LeftNav" Src="~/web/usercontrol/Purchasing/Purchasing_LeftNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style_ph.css" %>' rel="stylesheet" type="text/css" />
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
                                    <uc1:BAC_TopNav_Bids ID="BAC_TopNav_Bids1" runat="server" />
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
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="height: 100%;">
                            <tr>
                                <td id="relatedInfo">
                                    <h2>
                                        Bid Events</h2>
                                    <uc2:BAC_LeftNav_Bids ID="BAC_LeftNav_Bids1" runat="server" />
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
                                                                Bid Events For Clarification</h1>
                                                            <br />
                                                            <asp:GridView runat="server" ID="gvBidEventsForOpening" SkinID="BidEvents" AllowPaging="True"
                                                                AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="BidRefNo,CommitteeId,IsApprovedByPurchasing,IsApprovedByFinance,IsApprovedByAudit,Deadline" DataSourceID="dsBidEventsForOpening"
                                                                OnRowCommand="gvBidEventsForOpening_RowCommand" OnRowDataBound="gvBidEventsForOpening_RowDataBound">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Reference No." SortExpression="BidRefNo">
                                                                        <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                                                        <ItemTemplate>
                                                                            &nbsp;<asp:LinkButton ID="lnkBidRefNo" runat="server" Text='<%# Eval("BidRefNo") %>'
                                                                                CommandName="bidEvent" CommandArgument='<%# Eval("BidRefNo", "{0}") %>'></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Bid Event" SortExpression="ItemDesc">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            &nbsp;<asp:LinkButton ID="lnkBidEvent" runat="server" Text='<%# Eval("ItemDesc") %>'
                                                                                CommandName="bidEvent" CommandArgument='<%# Eval("BidRefNo", "{0}") %>'></asp:LinkButton><br />	
                                                                                &nbsp;&nbsp;<asp:Label ID="LabelTenderCount" runat="server" ForeColor="gray" Text='<%# showBidTenderCount(Eval("SubmittedTenderCount")) %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Date Received" SortExpression="Deadline">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" Width="180px" />
                                                                        <ItemTemplate>
                                                                            &nbsp;<asp:Label ID="Label2" runat="server" Text='<%# Bind("Deadline", "{0:D}") %>'></asp:Label><br />
                                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Deadline", "{0:T}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <asp:SqlDataSource ID="dsBidEventsForOpening" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                SelectCommand="sp_QueryBidsEventsForOpening" SelectCommandType="StoredProcedure">
                                                                <SelectParameters>
                                                                    <asp:SessionParameter DefaultValue="0" Name="BOCId" SessionField="UserId" Type="Int32" />
                                                                </SelectParameters>
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
        </form>
    </div>
</body>
</html>
