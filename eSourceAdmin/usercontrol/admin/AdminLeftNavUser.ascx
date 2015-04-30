<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminLeftNavUser.ascx.cs"
    Inherits="web_usercontrol_admin_AdminLeftNavUser" %>
<link type="text/css" href="../../css/style.css" rel="stylesheet" />
<table border="0" cellpadding="0" cellspacing="0" width="100%" id="related">
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="lnkViewUsers" NavigateUrl="~/admin/users.aspx">View Users</asp:HyperLink></td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="lnkAddUser" NavigateUrl="~/admin/adduser.aspx">Add New User</asp:HyperLink></td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="lnlDeletedUsers" NavigateUrl="~/admin/deletedusers.aspx">Deleted Users</asp:HyperLink>
        </td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="lnkBlackListing" NavigateUrl="~/admin/vendorblacklisting.aspx">Vendor Blacklisting</asp:HyperLink>
        </td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="lnkSearchUsers" NavigateUrl="~/admin/searchuser.aspx">User Search</asp:HyperLink>
        </td>
    </tr>
</table>
