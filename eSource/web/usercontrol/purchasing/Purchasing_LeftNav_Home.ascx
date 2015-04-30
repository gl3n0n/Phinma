<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Purchasing_LeftNav_Home.ascx.cs"
    Inherits="web_user_control_Purchasing_Purchasing_LeftNav_Home" %>
<link type="text/css" href="../../css/style_ph.css" rel="stylesheet" />
<table border="0" cellpadding="0" cellspacing="0" width="100%" id="related">
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="btnBidsForApproval" NavigateUrl="~/web/purchasingscreens/bids.aspx">
								Bid Events For Approval</asp:HyperLink></td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="btnApprovedBidItems" NavigateUrl="~/web/purchasingscreens/approvedbidevents.aspx">
								Approved Bid Events</asp:HyperLink></td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="btnRejectedBids" NavigateUrl="~/web/purchasingscreens/RejectedBidEvents.aspx">
								Rejected Bid Events</asp:HyperLink></td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="btnBidsForReediting" NavigateUrl="~/web/purchasingscreens/BidEventsforRe-Editing.aspx">
								Bid Events For Re-editing</asp:HyperLink></td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="btnEndorsedBidTenders" NavigateUrl="~/web/purchasingscreens/BidsforEval.aspx">
								Received Tenders</asp:HyperLink></td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="btnBidsForConv" NavigateUrl="~/web/purchasingscreens/bidsForConversion.aspx">
								Bids For Conversion</asp:HyperLink></td>
    </tr>
    <tr>
        <td style="height: 19px">
            <asp:HyperLink runat="server" ID="btnBidsForReneg" NavigateUrl="~/web/purchasingscreens/BidsForRenegotiation.aspx">
								Bids For Clarification</asp:HyperLink></td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="btnBidsForOpening" NavigateUrl="~/web/purchasingscreens/bidsForOpening.aspx">
								Bids For Opening</asp:HyperLink></td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="btnAwardedBidItems" NavigateUrl="~/web/purchasingscreens/awardedBidItems.aspx">
								Awarded Bid Items</asp:HyperLink></td>
    </tr>
</table>
