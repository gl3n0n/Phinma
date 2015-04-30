<%@ Control Language="C#" AutoEventWireup="true" CodeFile="biditemdetails.ascx.cs" Inherits="web_usercontrol_bids_biditemdetails" %>
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
        <%--<asp:TemplateField HeaderText="Currency" SortExpression="Currency">            
            <ItemTemplate>
                <asp:Label ID="Label10" runat="server" Text='<%# Bind("Currency") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>--%>
        <asp:TemplateField HeaderText="Bid Event" InsertVisible="False" SortExpression="BidRefNo">            
            <ItemTemplate>
                <asp:Label ID="Label7" runat="server" Text='<%# Bind("BidRefNo") %>'></asp:Label> - 
                <asp:Label ID="Label8" runat="server" Text='<%# Bind("ItemDesc") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Bid Tenders" SortExpression="BidTenderCount">            
            <ItemTemplate>
                <asp:Label ID="Label9" runat="server" Text='<%# Bind("BidTenderCount") %>'></asp:Label>                
            </ItemTemplate>
        </asp:TemplateField>
    </Fields>
</asp:DetailsView>
<asp:SqlDataSource ID="dsBidItemDetails" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
    SelectCommand="sp_GetBidItemDetails" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="0" Name="BidDetailNo" SessionField="BidDetailNo"
            Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
