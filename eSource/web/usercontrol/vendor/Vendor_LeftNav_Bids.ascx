<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Vendor_LeftNav_Bids.ascx.cs"
	Inherits="web_user_control_Vendor_Vendor_LeftNav_Bids" %>
<link type="text/css" href="../../css/style.css" rel="stylesheet" />


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
    <h3> <a href="javascript:void(0)" onclick="window.location ='/web/vendorscreens/index.aspx';" > Home</a></h3>
<div></div>
     <h3 style="margin-top:15px;">Bids</h3>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="related">
	<tr>
		<td style="height: 19px">
			<asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="~/web/vendorscreens/bids.aspx">New Bid Events</asp:HyperLink>
		</td>
	</tr>
	<tr>
		<td style="height: 19px">
			<asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="~/web/vendorscreens/declinedbidevents.aspx">Declined Bid Events</asp:HyperLink>
		</td>
	</tr>
	<tr>
		<td style="height: 19px">
			<asp:HyperLink ID="HyperLink11" runat="server" NavigateUrl="~/web/vendorscreens/finishedbidevents.aspx">Finished Bid Events</asp:HyperLink>
		</td>
	</tr>
</table>
    </div>
     <h3 >Bid Status</h3>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="related">
	<tr>
		<td>			
			<asp:HyperLink ID="HyperLink12" runat="server" NavigateUrl="~/web/vendorscreens/awardedbids.aspx">Awarded Bid Items</asp:HyperLink>
		</td>
	</tr>
	<tr>
		<td>			
			<asp:HyperLink ID="HyperLink13" runat="server" NavigateUrl="~/web/vendorscreens/withdrawnedbiditems.aspx">Withdrawn Bid Items</asp:HyperLink>
		</td>
	</tr>		
</table>
    </div>
     <h3>Tenders</h3>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="related">
	<tr>
		<td>
			<asp:HyperLink ID="HyperLink14" runat="server" NavigateUrl="~/web/vendorscreens/tenderdrafts.aspx">Bid Tender Drafts</asp:HyperLink>
		</td>
	</tr>
	<tr>
		<td>			
			<asp:HyperLink ID="HyperLink15" runat="server" NavigateUrl="~/web/vendorscreens/submittedtenders.aspx">Submitted Bid Tenders</asp:HyperLink>
		</td>
	</tr>
	<!--<tr>
		<td>			
			<asp:HyperLink ID="HyperLink16" runat="server" NavigateUrl="~/web/vendorscreens/bidsforrenegotiation.aspx">Bid Tenders For Clarification</asp:HyperLink>
		</td>
	</tr>-->
</table>
    </div>
    <%--<div style="display:none;">
     <h3 style="margin-top:15px;">Auctions</h3>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="related">
	<tr>
		<td>			
			<asp:HyperLink ID="lnk1" runat="server" NavigateUrl="~/web/vendorscreens/auctions.aspx">New Auction Events</asp:HyperLink>
		</td>
	</tr>
	<tr>
		<td>			
			<asp:HyperLink ID="lnk2" runat="server" NavigateUrl="~/web/vendorscreens/confirmedinvitations.aspx">Confirmed Invitations</asp:HyperLink>
		</td>
	</tr>
	<tr>
		<td>			
			<asp:HyperLink ID="lnk4" runat="server" NavigateUrl="~/web/vendorscreens/auctioncalendar.aspx">Auction Calendar</asp:HyperLink>
		</td>
	</tr>
</table>
    </div>
     <h3>Auction Status</h3>
    <div>
        <table cellSpacing="0" cellPadding="0" width="100%" border="0" class="related">
<tr>
	<td>			
		<asp:HyperLink ID="lnk3" runat="server" NavigateUrl="~/web/vendorscreens/awardedauctions.aspx">Awarded Auction Items</asp:HyperLink>
	</td>
</tr>
</table>
    </div>
     <h3>Auction Events</h3>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="related">
	<tr>
		<td>			
			<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/web/auctions/ongoingauctionevents.aspx">Ongoing Auction Events</asp:HyperLink>
		</td>
	</tr>
	<tr>
		<td>			
			<asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/web/auctions/upcomingauctionevents.aspx">Upcoming Auction Events</asp:HyperLink>
		</td>
	</tr>
	<tr>
		<td style="height: 19px">			
			<asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/web/auctions/finishedauctionevents.aspx">Finished Auction Events</asp:HyperLink>
		</td>
	</tr>
</table>
        </div>
    </div>--%>
     <h3 style="margin-top:15px;">My Profile</h3>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="related">
	<tr>
		<td style="height: 19px">			
			<asp:HyperLink ID="HyperLink4"  runat="server" NavigateUrl="~/web/vendorscreens/profile.aspx">My Details</asp:HyperLink>
		</td>
	</tr>
	<tr>
		<td>			
			<asp:HyperLink ID="HyperLink17" runat="server" NavigateUrl="~/web/vendorscreens/changepassword.aspx">Change Password</asp:HyperLink>
		</td>
	</tr>
</table>
    </div>
</div>



<br />




