<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AwardedAuctionVendordetails.ascx.cs" Inherits="web_usercontrol_AuctionVendor_AwardedAuctionVendordetails" %>
<asp:DetailsView ID="dvAuctionWinner" runat="server" AllowPaging="True" AutoGenerateRows="False"
    DataSourceID="dsAuctionWinnerDetails" SkinID="BidDetails" Width="100%" HeaderText="Winning Bid">    
    <EmptyDataRowStyle BackColor="white" HorizontalAlign="Center" />    
    <Fields>
        <asp:TemplateField>
            <HeaderStyle Width="120px" />
            <HeaderTemplate>
                &nbsp;Bidder
            </HeaderTemplate>
            <ItemStyle BackColor="White" />
            <ItemTemplate>
                &nbsp;
                <asp:Label ID="lblVendor" runat="server" Text='<%# Bind("VendorName")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>            
            <HeaderTemplate>
                &nbsp;Bid
            </HeaderTemplate>
            <ItemStyle BackColor="White" />
            <ItemTemplate>
                &nbsp;
                <asp:Label ID="lblMount" runat="server" Text='<%# Bind("Bid", "{0:#,##0.00}")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>            
            <HeaderTemplate>
                &nbsp;Date Submitted
            </HeaderTemplate>
            <ItemStyle BackColor="White" />
            <ItemTemplate>
                &nbsp;
                <asp:Label ID="lblDateSubmitted" runat="server" Text='<%# Bind("DateSubmitted","{0:D}&nbsp;{0:T}") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Fields>
    <PagerSettings Visible="False" />    
</asp:DetailsView>
<asp:SqlDataSource ID="dsAuctionWinnerDetails" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
    SelectCommand="sp_GetAuctionWinnerDetails" SelectCommandType="StoredProcedure">
    <SelectParameters>
            <asp:SessionParameter DefaultValue="0" Name="AuctionDetailNo" SessionField="AuctionDetailNo" Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>