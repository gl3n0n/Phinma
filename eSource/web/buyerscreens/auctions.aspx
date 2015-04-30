<%@ Page Language="C#" AutoEventWireup="true" CodeFile="auctions.aspx.cs" Inherits="WEB_buyer_screens_auctions" %>

<%@ Register TagPrefix="EBid" TagName="TopNavAuction" Src="~/WEB/usercontrol/Buyer/TopNavAuctions.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNav_Auctions" Src="~/web/usercontrol/Buyer/TopNav2_Auction.ascx" %>
<%@ Register TagPrefix="EBid" TagName="LeftNavAuctions1" Src="~/WEB/usercontrol/Buyer/leftnav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="LeftNavNotifications" Src="~/web/usercontrol/Buyer/LeftNavNotifications.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
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
        <div align="left">
            <form id="Form1" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
                    <tr height="137px">
                        <td valign="top">
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
                                                                            Auction Event Drafts</h1>
                                                                        <p>
                                                                            These are your drafted auction events. 
                                                                            Click <b>"Delete"</b> to delete a drafted auction event. 
                                                                            Click on the auction event reference number or the auction event description to re-edit or submit that auction event.</p>
                                                                        <div align="left">
                                                                            <asp:GridView ID="gvAuctions" runat="server" SkinID="BidEvents" 
                                                                                AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" OnRowCommand="gvAuctions_RowCommand"                                                                                 
                                                                                DataSourceID="dsAuctionDrafts" DataKeyNames="AuctionRefNo" 
>
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Reference No.">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <ItemStyle Width="90px" />
                                                                                        <ItemTemplate>
                                                                                            &nbsp;<asp:LinkButton ID="lnkRefNo" runat="server" Text='<%# Bind("AuctionRefNo") %>' 
                                                                                                CommandArgument='<%# Bind("AuctionRefNo") %>' CommandName="ViewDetails"></asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Auction Event" SortExpression="ItemDesc">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <ItemTemplate>
                                                                                            &nbsp;<asp:LinkButton ID="lnkItemDesc" runat="server" Text='<%# Bind("ItemDesc") %>' 
                                                                                                CommandArgument='<%# Bind("AuctionRefNo") %>' CommandName="ViewDetails"></asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Date Created" SortExpression="DateSavedAsDraft">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <ItemStyle HorizontalAlign="Center" Width="180px" />
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblDateCreated" runat="server" Text='<%# Bind("DateSavedAsDraft", "{0:D}<br />{0:T}") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField>
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Bind("AuctionRefNo") %>' CommandName="Delete"
                                                                                                OnClientClick="return confirm('Are you sure you want to delete this auction event?');">Delete</asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                            <asp:SqlDataSource ID="dsAuctionDrafts" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="s3p_EBid_GetAuctionDrafts" SelectCommandType="StoredProcedure">
                                                                                <SelectParameters>
                                                                                    <asp:SessionParameter DefaultValue="0" Name="BuyerId" SessionField="UserId" Type="Int32" />
                                                                                    <asp:Parameter DefaultValue="0" Name="Status" Type="Int32" />
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
                                                                        </div>
                                                                    </div>
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
    </div>
</body>
</html>
