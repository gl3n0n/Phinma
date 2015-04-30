<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Admin_TopNav_List.ascx.cs"
    Inherits="web_usercontrol_admin_Admin_TopNav_HomePageMngmnt" %>
<table border="0" cellpadding="0" cellspacing="0" width="100%" class="tabs" id="table2" style="background-color: #517130;">
    <tr>
        <td width="200px" style="height: 19px">
            <asp:HyperLink runat="server" ID="lnkHome" NavigateUrl="~/admin/index.aspx">Home</asp:HyperLink></td>
        <td width="200px">
            <asp:HyperLink runat="server" ID="lnkUsers" NavigateUrl="~/admin/users.aspx">User Management</asp:HyperLink></td>
        <td class="activeTab" width="200px">
            <asp:HyperLink runat="server" ID="lnkSite" NavigateUrl="~/admin/categories.aspx">List Management</asp:HyperLink></td>
        <td width="200px">
            <asp:HyperLink runat="server" ID="lnkReports" NavigateUrl="~/admin/report_savingsbybidevent.aspx">Reports</asp:HyperLink></td>			
        <td>
            &nbsp;</td>
    </tr>
</table>