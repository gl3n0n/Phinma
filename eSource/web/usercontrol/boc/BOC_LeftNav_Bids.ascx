<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BOC_LeftNav_Bids.ascx.cs" Inherits="web_user_control_BOC_LeftNav" %>
<table border="0" cellpadding="0" cellspacing="0" width="100%" id="related">
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="btnBidsForApproval" NavigateUrl="~/web/boc/bidseventsforopening.aspx">
								Bid Events For Opening</asp:HyperLink></td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="btnApprovedBidItems" NavigateUrl="~/web/boc/bidsopened.aspx">
								Bid Events Opened</asp:HyperLink></td>
    </tr>    
</table>
