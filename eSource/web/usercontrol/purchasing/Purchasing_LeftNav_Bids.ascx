<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Purchasing_LeftNav_Bids.ascx.cs"
    Inherits="web_user_control_Purchasing_Purchasing_LeftNav" %>
<link type="text/css" href="../../css/style_ph.css" rel="stylesheet" />


<script src="http://code.jquery.com/jquery-1.9.1.js"></script>
<script src="http://code.jquery.com/ui/1.10.1/jquery-ui.js"></script>
<script>
    $(function () {
        $("#accordion").accordion({
            heightStyle: "content"
        });
    });
</script>
<style type="text/css">
    #accordion h3 {
        margin-bottom:2px;
        color:black;
        font-size:14px;
        background-color:#67893F;
        color:#fff;
        padding: 5px 2px;
        font-weight:bold;
        cursor:pointer;
        margin-bottom:1px;
        margin-top:0px;
    }
        #accordion h3 a {
       text-decoration:none;
       color:#fff;

        }
       
</style>

<div id="accordion">
     <h3> <a href="javascript:void(0)" onclick="window.location ='/web/purchasingscreens/index.aspx';" > Home</a></h3>
    <div>

    </div>
    <h3 style="margin-top:15px;">Bid</h3>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="related">
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="HyperLink1" NavigateUrl="~/web/purchasingscreens/bids.aspx">
								Bid Events For Approval</asp:HyperLink></td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="HyperLink2" NavigateUrl="~/web/purchasingscreens/approvedbidevents.aspx">
								Approved Bid Events</asp:HyperLink></td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="HyperLink3" NavigateUrl="~/web/purchasingscreens/bidinvitations.aspx">
								Bid Event Invitations</asp:HyperLink></td>
    </tr> 
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="HyperLink4" NavigateUrl="~/web/purchasingscreens/rejectedbidevents.aspx">
								Rejected Bid Events</asp:HyperLink></td>
    </tr>
    <!--<tr>
        <td>
            <asp:HyperLink runat="server" ID="HyperLink5" NavigateUrl="~/web/purchasingscreens/bideventsforre-editing.aspx">
								Bid Events For Re-Editing</asp:HyperLink></td>
    </tr>-->
</table>
    </div>
    <h3>Bid Status</h3>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="related">
    <!--<tr>
        <td>
            <asp:HyperLink runat="server" ID="HyperLink6" NavigateUrl="~/web/purchasingscreens/bidsforconversion.aspx">
								Converted Bid Items</asp:HyperLink></td>
    </tr>-->
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="HyperLink7" NavigateUrl="~/web/purchasingscreens/awardedbiditems.aspx">
								Awarded Bid Items</asp:HyperLink></td>
    </tr>
     <tr>
        <td>
            <asp:HyperLink runat="server" ID="HyperLink8" NavigateUrl="~/web/purchasingscreens/withdrawnedbiditems.aspx">
								Withdrawn Bid Items</asp:HyperLink></td>
    </tr>
</table>
    </div>
    <h3>Tenders</h3>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="related">
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="HyperLink9" NavigateUrl="~/web/purchasingscreens/bidsforeval.aspx">
								Received Endorsements</asp:HyperLink></td>
    </tr>
    <tr>
        <td style="height: 19px">
            <asp:HyperLink runat="server" ID="HyperLink10" NavigateUrl="~/web/purchasingscreens/bidsforrenegotiation.aspx">
								Bid Tenders For Clarification</asp:HyperLink></td>
    </tr>
</table>
    </div>
    <!--<h3 style="margin-top:15px;">Auction</h3>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="related">
    
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="btnConfirmedAuction" NavigateUrl="~/web/purchasingscreens/auctioninvitations.aspx">
								Auction Invitations</asp:HyperLink></td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="lnkAuctionForReedit" NavigateUrl="~/web/purchasingscreens/auctionitemsforre-editing.aspx">
								Auction Events For Re-Editing</asp:HyperLink></td>
    </tr>    
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="lnkAuctionCalendar" NavigateUrl="~/web/purchasingscreens/auctioncalendar.aspx">
                                Auction Calendar</asp:HyperLink>
        </td>
    </tr>
</table>
    </div>
    <h3>Auction Status</h3>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="related">
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="btnAuctForApproval" NavigateUrl="~/web/purchasingscreens/auctions.aspx">
								Auction Events For Approval</asp:HyperLink></td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="btnApprovedAuction" NavigateUrl="~/web/purchasingscreens/approvedauctionevents.aspx">
								Approved Auction Events</asp:HyperLink></td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="btnRejectedAuction" NavigateUrl="~/web/purchasingscreens/rejectedauctionevents.aspx">
								Rejected Auction Events</asp:HyperLink></td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="btnEndorsedAuctItems" NavigateUrl="~/web/purchasingscreens/auctionitemsforawarding.aspx">
								Endorsed Auction Items</asp:HyperLink></td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="btnAwardedAuction" NavigateUrl="~/web/purchasingscreens/awardedauctions.aspx">
								Awarded Auction Items</asp:HyperLink></td>
    </tr>
</table>
    </div>
    <h3>Auction Events</h3>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="related">
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="btnOngoingAuction" NavigateUrl="~/web/auctions/ongoingauctionevents.aspx">
								Ongoing Auction Events</asp:HyperLink></td>
    </tr>
    <tr>
        <td style="height: 19px">
            <asp:HyperLink runat="server" ID="btnUpcomingAuction" NavigateUrl="~/web/auctions/upcomingauctionevents.aspx">
								Upcoming Auction Events</asp:HyperLink></td>
    </tr>
    <tr>
        <td style="height: 19px">
            <asp:HyperLink runat="server" ID="btnFinishedAuction" NavigateUrl="~/web/auctions/finishedauctionevents.aspx">
								Finished Auction Events</asp:HyperLink></td>
    </tr>    
</table>
    </div>-->
    <h3 style="margin-top:15px;">Bid Event Report</h3>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="related">
        <tr>
            <td>
                <asp:HyperLink runat="server" ID="lnkAwardedBidItemsByItem" NavigateUrl="~/web/purchasingscreens/report_awardedbiditemsbyitem.aspx">
								Awarded Bid Items By Item</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HyperLink runat="server" ID="lnkAwardedBidItemsByCategory" NavigateUrl="~/web/purchasingscreens/report_awardedbiditemsbycategory.aspx">
								Awarded Bid Items By Category</asp:HyperLink>
            </td>
        </tr>
       <tr>
            <td>
                <asp:HyperLink runat="server" ID="HyperLink16" NavigateUrl="~/web/purchasingscreens/report_eventtenderscomparison.aspx">
								Bid Event Tenders Comparison</asp:HyperLink>
            </td>
        </tr> 
        <tr>
            <td>
                <asp:HyperLink runat="server" ID="HyperLink17" NavigateUrl="~/web/purchasingscreens/report_savingsbybidevent.aspx">
								Savings by Bid Event</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HyperLink runat="server" ID="HyperLink18" NavigateUrl="~/web/purchasingscreens/report_totalbidevents.aspx">
								Total Bid Events</asp:HyperLink>
            </td>
        </tr>
    </table>
    </div>
    <h3>Auction Event Report</h3>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="related">
        <tr>
            <td>
                <asp:HyperLink runat="server" ID="HyperLink19" NavigateUrl="~/web/purchasingscreens/report_bidhistorybyauctionevent.aspx">
								Bid History By Auction Event</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HyperLink runat="server" ID="HyperLink20" NavigateUrl="~/web/purchasingscreens/report_savingsbyauctionevent.aspx">
								Savings by Auction Event</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HyperLink runat="server" ID="HyperLink21" NavigateUrl="~/web/purchasingscreens/report_totalauctionevents.aspx">
								Total Auction Events</asp:HyperLink>
            </td>
        </tr>
    </table>
    </div>
   

</div>


<br />


