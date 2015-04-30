<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vendorBlacklisting.aspx.cs" Inherits="admin_vendorBlacklisting" MaintainScrollPositionOnPostback="true" Theme="default" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminTopNav" Src="~/usercontrol/admin/adminTopNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Admin_TopNav_User" Src="~/usercontrol/admin/Admin_TopNav_User.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminLeftNavUser" Src="~/usercontrol/admin/AdminLeftNavUser.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminLeftNav" Src="~/usercontrol/admin/AdminLeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/usercontrol/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
	<title>.:| Trans-Asia | Supplier Blacklisting |:.</title>
    <meta http-equiv="Content-Language" content="en-us">
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/style_ua.css" />
    <script type="text/javascript" src="../include/util.js"></script>
</head>
<body onload="SetStatus();">
    <div align="left">
        <form id="Form1" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
                <tr>
                    <td valign="top" height="137px">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <div align="left" id="masthead">
                                        <EBid:AdminTopNav runat="server" ID="GlobalLinksNav" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <EBid:Admin_TopNav_User runat="server" ID="Admin_TopNav_User" />
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
                                        User Functions</h2>
                                    <EBid:AdminLeftNavUser runat="server" ID="AdminLeftNavUser" />
                                    <p>
                                        &nbsp;</p>
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
                                                                Vendor Blacklisting</h1>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="25px">
                                                            <p>
                                                                Click on 'Blacklist' to include the user in the blacklisted users.</p>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" height="30px"></td>
                                                    </tr>                                                        
                                                    <tr>
														<td>                                                            
                                                            <asp:GridView ID="gvSearchResults" runat="server" AllowPaging="True"
                                                                PageSize="20" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="VendorId"
                                                                DataSourceID="dsViewVendors" OnRowDataBound="gvRowDataBound"
                                                                OnRowCommand="gvUsersRowCommand" SkinID="AuctionEvents">                                                                
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Username" SortExpression="UserName">
																		<HeaderStyle HorizontalAlign="Center" />
																		<ItemStyle HorizontalAlign="left" />
                                                                        <ItemTemplate>
                                                                            &nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Supplier Name" SortExpression="VendorName">
																		<HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            &nbsp;<asp:Label ID="lblVendorName" runat="server" Text='<%# Bind("VendorName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Email Address" SortExpression="VendorEmail">																		
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("VendorEmail") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Status" SortExpression="Vendor Status">                                                                        
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("[Vendor Status]") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Option">
                                                                        <HeaderStyle HorizontalAlign="Center" />                                                                        
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkOption" runat="server" CommandArgument='<%# Eval("VendorId", "{0}") %>'></asp:LinkButton>
                                                                            <asp:HiddenField ID="hdStatus" runat="server" Value='<%# Eval("isBlackListed", "{0}") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>                                                                
                                                            </asp:GridView>
                                                            <asp:SqlDataSource ID="dsViewVendors" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                SelectCommand="s3p_EBid_ViewVendors" SelectCommandType="StoredProcedure"></asp:SqlDataSource>                                                            
                                                            <br />
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                                <tr><td></td></tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr><td style="height: 20px;"></td></tr>
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
