<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bidinvitations.aspx.cs" Inherits="web_buyerscreens_bidinvitations" %>

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
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_buyer.css" %>' />
</head>
<body style="height: 100%;">
    <div>
        <form id="form1" runat="server">
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
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
                            <tr>
                                <td id="relatedInfo">
                                   
                                    <div align="left">
                                        <EBid:LeftNav ID="LeftNav" runat="server" />
                                    </div>
                                    <p>&nbsp;
                                        </p>
                                </td>
                                <td id="content">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td valign="top">
                                                <h1>
                                                    <br />
                                                    Bid Event Invitations</h1>
                                                <p> 
                                                    These are bid events that has been confirmed, declined and not responded yet.
                                                   
                                                </p>
                                                <asp:GridView ID="gvBids" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                                    SkinID="BidEvents" EmptyDataText="None" DataKeyNames="BidRefNo" DataSourceID="sdsInvitations" OnRowCommand="gvBids_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Reference No." InsertVisible="False" SortExpression="BidRefNo">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle Width="90px" />
                                                            <ItemTemplate>
                                                                &nbsp;<asp:LinkButton ID="Label5" runat="server" Text='<%# Bind("BidRefNo") %>' CommandName="ViewDetails" CommandArgument='<%# Bind("BidRefNo") %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bid Event" SortExpression="ItemDesc">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                &nbsp;<asp:LinkButton ID="Label1" runat="server" Text='<%# Bind("ItemDesc") %>' CommandName="ViewDetails" CommandArgument='<%# Bind("BidRefNo") %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Pending" SortExpression="PendingCount">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="Label3" runat="server" Text='<%# String.Format("{0}/{1}", Eval("PendingCount"),Eval("TotalCount")) %>' Enabled='<%# IsEnabled(Eval("PendingCount").ToString()) %>' CommandName="Pending" CommandArgument='<%# Bind("BidRefNo") %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Confirmed" SortExpression="ConfirmedCount">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="Label4" runat="server" Text='<%# String.Format("{0}/{1}", Eval("ConfirmedCount"),Eval("TotalCount")) %>' Enabled='<%# IsEnabled(Eval("ConfirmedCount").ToString()) %>' CommandName="Confirmed" CommandArgument='<%# Bind("BidRefNo") %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DeclinedCount" SortExpression="DeclinedCount">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="Label6" runat="server" Text='<%# String.Format("{0}/{1}", Eval("DeclinedCount"),Eval("TotalCount")) %>' Enabled='<%# IsEnabled(Eval("DeclinedCount").ToString()) %>' CommandName="Declined" CommandArgument='<%# Bind("BidRefNo") %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>                                                
                                                <asp:SqlDataSource ID="sdsInvitations" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetBuyerBidInvitations" SelectCommandType="StoredProcedure">
                                                    <SelectParameters>
                                                        <asp:SessionParameter DefaultValue="0" Name="BuyerId" SessionField="UserId" Type="Int32" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                                <br />
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                    <tr>                                                        
                                                        <td>&nbsp;
                                                                                                                        
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
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