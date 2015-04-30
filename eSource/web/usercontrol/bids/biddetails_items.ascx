<%@ Control Language="C#" AutoEventWireup="true" CodeFile="biddetails_items.ascx.cs"
    Inherits="web_usercontrol_bids_biddetails_items" %>
<div align="left">
    <asp:GridView AutoGenerateColumns="False" runat="server" ID="gvBidItemDetails" SkinID="AuctionedItems"
        DataSourceID="dsItemDetails" AllowSorting='true'>
        <Columns>
            <asp:TemplateField HeaderText="Item" SortExpression="Item">
                <ItemStyle CssClass="itemDetails_td" Width="65px" />
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblItem" Text='<%#Bind("Item") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="260px" />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description" SortExpression="DetailDesc">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblDescription" Text='<%#Bind("DetailDesc") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quantity Measure" SortExpression="Qty">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblQuantity" Text='<%#Bind("Qty", "{0:#,##0}") %>'></asp:Label>
                    <asp:Label runat="server" ID="lblUnitOfMeasure" Text='<%#Bind("UnitOfMeasure") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="140px" />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Unit Price" SortExpression="UnitPrice">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="120px" />
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblPrice" Text='<%#Bind("UnitPrice", "{0:#,##0}") %>'></asp:Label>                    
                </ItemTemplate>                
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total Cost" SortExpression="TotalUnitCost">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="120px" />
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblTotal" Text='<%#Bind("TotalUnitCost", "{0:#,##0}") %>'></asp:Label>                    
                </ItemTemplate>                
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delivery Date" SortExpression="DeliveryDate1">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblDeliveryDate" Text='<%#Bind("DeliveryDate1") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="120px" />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="dsItemDetails" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
        SelectCommand="s3p_EBid_GetBidItemDetails" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter Name="BidRefNo" SessionField="BidRefNo" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
</div>
