<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Purchasing_LeftNav_Suppliers.ascx.cs"
    Inherits="web_user_control_Purchasing_Purchasing_LeftNav" %>

<table border="0" cellpadding="0" cellspacing="0" width="100%" id="related">
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="btnVSFEndorsed" NavigateUrl="~/web/purchasingscreens/vsfendorsed.aspx">
								Endorsed VSF</asp:HyperLink></td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="btnVSFApproval" NavigateUrl="~/web/purchasingscreens/vsfapproved.aspx">
								Approved VSF</asp:HyperLink></td>
    </tr>
</table>

