<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Admin_LeftNav_Reports.ascx.cs" Inherits="web_user_control_Admin_Admin_LeftNav_Reports" %>
<h2>
    Bid Event Reports
</h2>
<div align="left">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="related">        
        <tr>
            <td>
                <asp:HyperLink runat="server" ID="HyperLink4" NavigateUrl="~/admin/report_savingsbybidevent.aspx">
								Savings by Bid Event</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HyperLink runat="server" ID="HyperLink2" NavigateUrl="~/admin/report_vendorlist.aspx">
								Vendor List</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HyperLink runat="server" ID="HyperLink3" NavigateUrl="~/admin/report_totalbids.aspx">
								Total Bids</asp:HyperLink>
            </td>
        </tr>
    </table>
</div>
<!--<h2>
    Auction Event Reports
</h2>
<div align="left">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="related">
        <tr>
            <td>
                <asp:HyperLink runat="server" ID="HyperLink1" NavigateUrl="~/admin/report_bidhistorybyauctionevent.aspx">
								Bid History By Auction Event</asp:HyperLink>
            </td>
        </tr>        
    </table>
</div>-->
