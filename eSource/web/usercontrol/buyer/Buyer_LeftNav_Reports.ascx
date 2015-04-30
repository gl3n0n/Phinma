<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Buyer_LeftNav_Reports.ascx.cs" Inherits="web_user_control_Buyer_Buyer_LeftNav_Reports" %>
<h2>
    Bid Event Reports
</h2>
<div align="left">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="related">        
        <tr>
            <td>
                <asp:HyperLink runat="server" ID="HyperLink4" NavigateUrl="~/web/buyerscreens/report_savingsbybidevent.aspx">
								Savings by Bid Event</asp:HyperLink>
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
                <asp:HyperLink runat="server" ID="HyperLink1" NavigateUrl="~/web/buyerscreens/report_bidhistorybyauctionevent.aspx">
								Bid History By Auction Event</asp:HyperLink>
            </td>
        </tr>        
    </table>
</div>
