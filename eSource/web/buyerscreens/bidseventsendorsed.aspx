<%@ page language="c#" enableeventvalidation="true" inherits="EBid.WEB.boc.bideventsforopening" CodeFile="bidseventsforopening.aspx.cs" theme="default" %>

<%@ Register TagPrefix="EBid" TagName="LeftNav" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNavBids" Src="~/WEB/usercontrol/Buyer/TopNavBids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNav2" Src="~/WEB/usercontrol/Buyer/TopNav2.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
    <link href='../themes/<%= Session["configTheme"] %>/css/style_buyer.css' rel="stylesheet" type="text/css" />
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
									<EBid:TopNav2 ID="TopNav2" runat="server" />
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
										<EBid:LeftNav ID="LeftNav" runat="server" />
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
                                                                Bid Events For Opening</h1>
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
                                                                                CommandName="bidEvent" CommandArgument='<%# Eval("BidRefNo", "{0}") %>'></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Buyer" SortExpression="Creator">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                                        <ItemTemplate>
                                                                            &nbsp;<asp:Label ID="Label3" runat="server" Text='<%# Bind("Creator") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText=" ">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Panel ID="pnlOpeningCommittee" runat="server" Width="200" visible="false">
                                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="font-size:11px; font-family:Arial;">
                                                                                    <tr>
                                                                                        <td align="center" style="width: 70px;">
                                                                                            Purchasing</td>
                                                                                        <td align="center" style="width: 70px;">
                                                                                            Finance</td>
                                                                                        <td align="center" style="width: 70px;">
                                                                                            Audit</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="center" style="width: 70px">
                                                                                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# (Eval("IsApprovedByPurchasing").ToString() != "1") ? "../../web/images/stop.jpg" : "../../web/images/go.jpg"%>'>
                                                                                            </asp:Image>
                                                                                        </td>
                                                                                        <td align="center" style="width: 70px">
                                                                                            <asp:Image ID="Image2" runat="server" ImageUrl='<%# (Eval("IsApprovedByFinance").ToString() != "1") ? "../../web/images/stop.jpg" : "../../web/images/go.jpg"%>'>
                                                                                            </asp:Image>
                                                                                        </td>
                                                                                        <td align="center" style="width: 70px;">
                                                                                            <asp:Image ID="Image3" runat="server" ImageUrl='<%# (Eval("IsApprovedByAudit").ToString() != "1") ? "../../web/images/stop.jpg" : "../../web/images/go.jpg" %>'>
                                                                                            </asp:Image>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                            <asp:Panel ID="pnlOpenEvent" runat="server" Width="200" visible="false">
                                                                                <asp:LinkButton ID="lnkOpenEvent" runat="server" Text='Open'
                                                                                CommandName="openEvent" CommandArgument='<%# Eval("BidRefNo", "{0}") %>' >
                                                                            </asp:LinkButton>
                                                                            </asp:Panel>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Submission Deadline" SortExpression="Deadline">
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
