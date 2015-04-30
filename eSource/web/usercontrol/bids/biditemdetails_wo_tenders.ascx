<%@ Control Language="C#" AutoEventWireup="true" CodeFile="biditemdetails_wo_tenders.ascx.cs" Inherits="web_usercontrol_bids_biditem_details" %>
<asp:DetailsView id="dvBidItem" Width="100%" SkinID="BidDetails" DataSourceID="dsBidItemDetails"
    runat="server" DataKeyNames="BidDetailNo,BidRefNo" HeaderText="Bid Item Details">    
    <Fields>
        <asp:TemplateField HeaderText="Detail No." InsertVisible="False" SortExpression="BidDetailNo">
            <HeaderStyle Width="120px" />            
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# Bind("BidDetailNo") %>'></asp:Label>                     
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Description" SortExpression="DetailDesc">                        
            <ItemTemplate>
                <asp:Label ID="Label2" runat="server" Text='<%# Bind("DetailDesc") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="SKU" SortExpression="SKU">            
            <ItemTemplate>
                <asp:Label ID="Label3" runat="server" Text='<%# Bind("SKU") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Quantity" SortExpression="Qty">            
            <ItemTemplate>
                <asp:Label ID="Label4" runat="server" Text='<%# Bind("Qty", "{0:#,##0}") %>'></asp:Label>
                <asp:Label ID="Label5" runat="server" Text='<%# Bind("UnitofMeasure") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>        
        <asp:TemplateField HeaderText="DeliveryDate" SortExpression="DeliveryDate">            
            <ItemTemplate>
                <asp:Label ID="Label6" runat="server" Text='<%# Bind("DeliveryDate", "{0:D}") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Bid Event" InsertVisible="False" SortExpression="BidRefNo">            
            <ItemTemplate>
                <asp:Label ID="Label7" runat="server" Text='<%# Bind("BidRefNo") %>'></asp:Label> - 
                <asp:Label ID="Label8" runat="server" Text='<%# Bind("ItemDesc") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Fields>
</asp:DetailsView>
<asp:SqlDataSource ID="dsBidItemDetails" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
    SelectCommand="sp_GetBidItemDetailsWoTenders" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="0" Name="BidDetailNo" SessionField="BidDetailNo"
            Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
