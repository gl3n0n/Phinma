<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Purchasing_LeftNav_Reports.ascx.cs" Inherits="web_user_control_Purchasing_Purchasing_LeftNav_Reports" %>
<h2>
    Bid Event Reports
</h2>
<div align="left">
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
                <asp:HyperLink runat="server" ID="HyperLink3" NavigateUrl="~/web/purchasingscreens/report_eventtenderscomparison.aspx">
								Bid Event Tenders Comparison</asp:HyperLink>
            </td>
        </tr> 
        <tr>
            <td>
                <asp:HyperLink runat="server" ID="HyperLink4" NavigateUrl="~/web/purchasingscreens/report_savingsbybidevent.aspx">
								Savings by Bid Event</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HyperLink runat="server" ID="HyperLink5" NavigateUrl="~/web/purchasingscreens/report_totalbidevents.aspx">
								Total Bid Events</asp:HyperLink>
            </td>
        </tr>
    </table>
</div>
<h2>
    Auction Event Reports
</h2>
<div align="left">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="related">
        <tr>
            <td>
                <asp:HyperLink runat="server" ID="HyperLink1" NavigateUrl="~/web/purchasingscreens/report_bidhistorybyauctionevent.aspx">
								Bid History By Auction Event</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HyperLink runat="server" ID="HyperLink2" NavigateUrl="~/web/purchasingscreens/report_savingsbyauctionevent.aspx">
								Savings by Auction Event</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HyperLink runat="server" ID="HyperLink6" NavigateUrl="~/web/purchasingscreens/report_totalauctionevents.aspx">
								Total Auction Events</asp:HyperLink>
            </td>
        </tr>
    </table>
</div>
