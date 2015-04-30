<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BAC_LeftNav_Bids.ascx.cs" Inherits="web_user_control_BAC_LeftNav" %>
<table border="0" cellpadding="0" cellspacing="0" width="100%" id="related">
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="btnBidsForAwarding" NavigateUrl="~/web/bac/bacapprovedpurchasing.aspx">
								Endorsed BAC</asp:HyperLink></td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="btnAwardedBidItems" NavigateUrl="~/web/bac/bacawarded.aspx">
								Awarded BAC</asp:HyperLink></td>
    </tr>  
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="btnBidsForClarification" NavigateUrl="~/web/bac/bacforclarifications.aspx">
								BAC for Clarification</asp:HyperLink></td>
    </tr>  
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="btnBacRejected" NavigateUrl="~/web/bac/bacrejected.aspx">
								Rejected BAC</asp:HyperLink></td>
    </tr>  
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="btnBacForApproval" NavigateUrl="~/web/bac/bacforawarding.aspx">
								Ongoing BAC</asp:HyperLink></td>
    </tr>  
</table>
