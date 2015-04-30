<%@ Page Language="C#" AutoEventWireup="true" CodeFile="deletedUsers.aspx.cs" Inherits="admin_deletedUsers" MaintainScrollPositionOnPostback="true" Theme="default" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminTopNav" Src="~/usercontrol/admin/adminTopNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Admin_TopNav_User" Src="~/usercontrol/admin/Admin_TopNav_User.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminLeftNavUser" Src="~/usercontrol/admin/AdminLeftNavUser.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminLeftNav" Src="~/usercontrol/admin/AdminLeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/usercontrol/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
	<title>.:| Trans-Asia | Deleted Users |:.</title>
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
                            <tr valign="top">
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
                                                    <tr id="content0">
                                                        <td>
                                                            <h1>
                                                                <br />
                                                                Deleted E-Bid Portal Users</h1>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="25px">
                                                            <p>
                                                                Please select the user type for the users you wish to view. Click on 'Recover' to
                                                                recover the user in the 'Option' column.</p>
                                                        </td>
                                                    </tr>
                                                    <tr align="right">
                                                        <td valign="top" height="30px">
                                                            <asp:Label ID="lblViewUsers" runat="server" Text="Select User Type:" Font-Names="Arial"
                                                                Font-Size="8pt" Width="89px" />
                                                            <asp:DropDownList ID="ddlUserTypes" runat="server" DataSourceID="dsUserTypes" DataTextField="UserType"
                                                                DataValueField="UserTypeId" Width="150px" AutoPostBack="True">
                                                            </asp:DropDownList>
                                                            <asp:SqlDataSource ID="dsUserTypes" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                SelectCommand="s3p_EBid_UserTypes" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False"
                                                                BorderWidth="1px" PageSize="15" AllowPaging="True" AllowSorting="True" DataSourceID="dsUsers"
                                                                OnRowCommand="gvRowCommand" EmptyDataText="None" SkinID="AuctionEvents">                                                                
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Username" SortExpression="UserName">
																		<HeaderStyle HorizontalAlign="Center" />
																		<ItemStyle HorizontalAlign="left" />																		
                                                                        <ItemTemplate>
                                                                            &nbsp;<asp:Label ID="lblUserName" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Email Address" SortExpression="EmailAdd">
																		<HeaderStyle HorizontalAlign="Center" />                                                                        
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("EmailAdd") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Status">
                                                                        <HeaderStyle HorizontalAlign="Center" />                                                                        
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("StatusType") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Option">
                                                                        <HeaderStyle HorizontalAlign="Center" />                                                                        
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkButton" runat="server" Text="Recover" OnClientClick="return confirm('Recover this user?')"
                                                                                CommandName="recoverUser" CommandArgument='<%# Eval("UserID") %>'></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <asp:SqlDataSource ID="dsUsers" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                SelectCommand="s3p_EBid_ViewDeletedUsers" SelectCommandType="StoredProcedure">
                                                                <SelectParameters>
                                                                    <asp:ControlParameter ControlID="ddlUserTypes" DefaultValue="3" Name="userType" PropertyName="SelectedValue"
                                                                        Type="Int32" />
                                                                </SelectParameters>
                                                            </asp:SqlDataSource>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                    <tr><td></td></tr>
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
