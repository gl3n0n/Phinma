
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LeftNav.ascx.cs" Inherits="LeftNav" %>
 <link rel="stylesheet" href="jquery/themes/base/jquery-ui.css" />
<%--<script src="/Scripts/jquery-1.5.1.js"></script>--%>
<script src="../../jquery/ui/jquery-ui.js"></script>
        <script>
            $(function () {
                $("#accordion").accordion({
                    heightStyle: "content",
                    collapsible: true,
                });
            });
        </script>
<asp:Label ID="Label1" runat="server" Text=""></asp:Label>
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

<h3> <a href="javascript:void(0)" onclick="window.location ='/web/buyerscreens/index.aspx';" > Home</a></h3>
<div></div>
<h3 style="margin-top:15px;" class="active" id="accrdn-Bid">Bid</h3>
<div>
<table border="0" cellpadding="0" cellspacing="0" width="100%" id="related">
	<tr>
		<td>
			<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/web/buyerscreens/bids.aspx">Bid Event Drafts</asp:HyperLink>		
		</td>
	</tr>
	<tr>
		<td>
			<asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="~/web/buyerscreens/bidinvitations.aspx">Bid Event Invitations</asp:HyperLink>		
		</td>
	</tr>
	<!--<tr>
		<td>
			<asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/web/buyerscreens/bidsreedit.aspx">Bid Events For Re-Editing</asp:HyperLink>		
		</td>
	</tr>-->
</table>
</div>
<h3 id="accrdn-BidStat">Bid Status</h3>
<div>
   <table border="0" cellpadding="0" cellspacing="0" width="100%" class="related">
    <tr>
		<td>
			<asp:HyperLink ID="HyperLink12" runat="server" NavigateUrl="~/web/buyerscreens/submittedbiditems.aspx">Bid Events For Approval</asp:HyperLink>		
		</td>
	</tr>
	<tr>
		<td>
			<asp:HyperLink ID="HyperLink13" runat="server" NavigateUrl="~/web/buyerscreens/approvedbiditems.aspx">Approved Bid Events</asp:HyperLink>		
		</td>
	</tr>
	<!--<tr>
		<td>
			<asp:HyperLink ID="HyperLink14" runat="server" NavigateUrl="~/web/buyerscreens/rejectedbiditems.aspx">Rejected Bid Events</asp:HyperLink>		
		</td>
	</tr>-->
    <!--<tr>
		<td>
			<asp:HyperLink ID="HyperLink15" runat="server" NavigateUrl="~/web/buyerscreens/biditemsforconversion.aspx">Converted Bid Items</asp:HyperLink>		
		</td>
	</tr>-->
	<tr>
		<td>
			<asp:HyperLink ID="HyperLink16" runat="server" NavigateUrl="~/web/buyerscreens/awardedbiditems.aspx">Awarded Bid Items</asp:HyperLink>		
		</td>
	</tr>
	<tr>
		<td>
			<asp:HyperLink ID="HyperLink17" runat="server" NavigateUrl="~/web/buyerscreens/withdrawnedbiditems.aspx">Withdrawn Bid Items</asp:HyperLink>		
		</td>
	</tr>
</table>
</div>
<h3 id="accrdn-Tenders">Tenders</h3>
<div>
   <table border="0" cellpadding="0" cellspacing="0" width="100%" class="related">
    <tr>
		<td>
			<asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/web/buyerscreens/bidsforeval.aspx">Received Bid Tenders</asp:HyperLink>		
		</td>
	</tr>
	<tr>
		<td>
			<asp:HyperLink ID="HyperLink11" runat="server" NavigateUrl="~/web/buyerscreens/endorsedbidtenders.aspx">Endorsed Bid Tenders</asp:HyperLink>		
		</td>
	</tr>
	<!--<tr>
		<td>
			<asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="~/web/buyerscreens/bidsforrenegotiation.aspx">Bid Tenders For Clarification</asp:HyperLink>		
		</td>
	</tr>-->		
</table> 
</div>
    <!--
<h3 style="margin-top:15px;" id="accrdn-Auction">Auction</h3>
<div>
    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="related">
                            	<tr>
		<td>
			<asp:HyperLink ID="HyperLink28" runat="server" NavigateUrl="~/web/buyerscreens/auctions.aspx">Auction Event Drafts</asp:HyperLink>
		</td>
	</tr>
                            	
	                            <tr>
		<td>
			<asp:HyperLink ID="HyperLink32" runat="server" NavigateUrl="~/web/buyerscreens/auctioninvitations.aspx">Auction Invitations</asp:HyperLink>			
		</td>
	</tr>
	                            <tr>
		<td>
			<asp:HyperLink ID="HyperLink33" runat="server" NavigateUrl="~/web/buyerscreens/auctionitemsforre-editing.aspx">Auction Events For Re-Editing</asp:HyperLink>			
		</td>
	</tr>	
	                            <tr>
		                            <td>
		                            	<asp:HyperLink ID="HyperLink34" runat="server" NavigateUrl="~/web/buyerscreens/auctioncalendar.aspx">Auction Calendar</asp:HyperLink>			
		                            </td>
	                            </tr>		
                            </table>
</div>
<h3 id="accrdn-AuctionStat">Auction Status</h3>
<div>
    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="related">
                                <tr>
		<td>
			<asp:HyperLink ID="HyperLink29" runat="server" NavigateUrl="~/web/buyerscreens/submittedauctionevents.aspx">Auction Events For Approval</asp:HyperLink>			
		</td>
	</tr>
                            	<tr>
		<td>
			<asp:HyperLink ID="HyperLink30" runat="server" NavigateUrl="~/web/buyerscreens/approvedauctionevents.aspx">Approved Auction Events</asp:HyperLink>			
		</td>
	</tr>
                            	<tr>
		<td>
			<asp:HyperLink ID="HyperLink31" runat="server" NavigateUrl="~/web/buyerscreens/rejectedauctionevents.aspx">Rejected Auction Events</asp:HyperLink>
		</td>
	</tr>
                                <tr>
		<td>
			<asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/web/buyerscreens/endorseditems.aspx">Endorsed Auction Items</asp:HyperLink>
		</td>
	</tr>
	                            <tr>
		<td>
			<asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/web/buyerscreens/awardedauctions.aspx">Awarded Auction Items</asp:HyperLink>			
		</td>
	</tr>
                                    </table>
</div>
<h3 id="accrdn-AuctionEvents">Auction Events</h3>
<div>
    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="related">
                                    	<tr>
		<td style="height: 19px">			
			<asp:HyperLink ID="HyperLink21" runat="server" NavigateUrl="~/web/auctions/ongoingauctionevents.aspx">Ongoing Auction Events</asp:HyperLink>
		</td>
	</tr>
                                    	<tr>
		<td>			
			<asp:HyperLink ID="HyperLink22" runat="server" NavigateUrl="~/web/auctions/upcomingauctionevents.aspx">Upcoming Auction Events</asp:HyperLink>
		</td>
	</tr>
                                    	<tr>
		<td>			
			<asp:HyperLink ID="HyperLink23" runat="server" NavigateUrl="~/web/auctions/finishedauctionevents.aspx">Finished Auction Events</asp:HyperLink>
		</td>
	</tr>	
                                    </table>
</div>-->
<h3 style="margin-top:15px;" id="accrdn-Sup">Suppliers/Products</h3>
<div>
    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="related">
                    	<tr>
		<td>
			<asp:HyperLink ID="HyperLink24" runat="server" NavigateUrl="~/web/buyerscreens/suppliers.aspx">Accredited Suppliers</asp:HyperLink>			
		</td>
    </tr> 
                        <tr>
		                    <td>
			<asp:HyperLink ID="HyperLink25" runat="server" NavigateUrl="~/web/buyerscreens/product.aspx">Registered Products</asp:HyperLink>			
		</td>
                         </tr>   
                    </table>
</div>
<h3 style="margin-top:15px;" id="accrdn-Rep">Reports</h3>
<div>
    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="related">
                        <tr>
		                    <td>
			<asp:HyperLink runat="server" ID="HyperLink26" NavigateUrl="~/web/buyerscreens/report_savingsbybidevent.aspx">Savings by Bid Event</asp:HyperLink>			
		</td>
                         </tr> 
                        <!--<tr>
		                    <td>
			<asp:HyperLink runat="server" ID="HyperLink27" NavigateUrl="~/web/buyerscreens/report_bidhistorybyauctionevent.aspx">Bid History By Auction Event</asp:HyperLink>			
		</td>
                         </tr>--> 
                    </table>
</div>
</div>
