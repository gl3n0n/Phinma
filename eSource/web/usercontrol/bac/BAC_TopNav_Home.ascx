<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BAC_TopNav_Home.ascx.cs" Inherits="web_user_control_BAC_TopNav_Home" %>
<table border="0" cellpadding="0" cellspacing="0" width="100%" class="tabs" id="table2">
    <tr>
        <td class="activeTab" width="200px">
            <asp:HyperLink runat="server" ID="lnkHome" NavigateUrl="~/web/bac/index.aspx">Home</asp:HyperLink></td>
        <td width="200px">
            <asp:HyperLink runat="server" ID="lnkBids" NavigateUrl="~/web/bac/bacapprovedpurchasing.aspx">Bids</asp:HyperLink></td>        
        <td>&nbsp;
            </td>
    </tr>
</table>