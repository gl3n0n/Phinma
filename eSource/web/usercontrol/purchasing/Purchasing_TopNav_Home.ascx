<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Purchasing_TopNav_Home.ascx.cs"
    Inherits="web_user_control_Purchasing_Purchasing_TopNav_Home" %>
<link type="text/css" href="../../css/style_ph.css" rel="stylesheet" />
<table border="0" cellpadding="0" cellspacing="0" width="100%" class="tabs" id="table2">
    <tr>
        <td class="activeTab" width="200px">
            <asp:HyperLink runat="server" ID="lnkHome" NavigateUrl="~/web/purchasingscreens/index.aspx">Home</asp:HyperLink></td>
        <td width="200px">
            <asp:HyperLink runat="server" ID="lnkBids" NavigateUrl="~/web/purchasingscreens/bids.aspx">Bids</asp:HyperLink></td>
        <td width="200px">
            <asp:HyperLink runat="server" ID="lnkAuction" NavigateUrl="~/web/purchasingscreens/auctions.aspx">Auctions</asp:HyperLink></td>
        <td width="200px">
            <asp:HyperLink runat="server" ID="lnkSuppliers" NavigateUrl="~/web/purchasingscreens/vsfendorsed.aspx">Suppliers</asp:HyperLink></td>
        <td width="200px">
            <asp:HyperLink runat="server" ID="lnkReports" NavigateUrl="~/web/purchasingscreens/report_awardedbiditemsbyitem.aspx">Reports</asp:HyperLink></td>
        <td>
            &nbsp;</td>
    </tr>
</table>