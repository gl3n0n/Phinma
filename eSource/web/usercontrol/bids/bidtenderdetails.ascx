<%@ Control Language="C#" AutoEventWireup="true" CodeFile="bidtenderdetails.ascx.cs" Inherits="web_usercontrol_bids_bidtenderdetails" %>
<div align="left">
    <asp:DetailsView ID="dvEventDetails" runat="server" AutoGenerateRows="False" DataSourceID="dsEventDetails"
        Width="100%" SkinID="BidDetails" HeaderText="Bid Item Details">
        <Fields>            
            <asp:BoundField DataField="BidTenderNo" HeaderText="Tender No." InsertVisible="False" ReadOnly="True" SortExpression="BidTenderNo" >
                <ItemStyle BackColor="White" />
            </asp:BoundField>
            <asp:BoundField DataField="SKU" HeaderText="SKU" ReadOnly="True" SortExpression="SKU" >
                <ItemStyle BackColor="White" />
            </asp:BoundField>
            <asp:BoundField DataField="DetailDesc" HeaderText="Description" SortExpression="DetailDesc" >
                <ItemStyle BackColor="White" />
            </asp:BoundField>
            <asp:BoundField DataField="Qty" HeaderText="Quantity" SortExpression="Qty"  >
                <ItemStyle BackColor="White" />
            </asp:BoundField>
            <asp:BoundField DataField="UnitOfMeasure" HeaderText="Unit of Measure" SortExpression="UnitOfMeasure" >
                <ItemStyle BackColor="White" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Delivery Date" SortExpression="DeliveryDate">                                
                <ItemStyle BackColor="White" />
                <ItemTemplate>
                    <asp:Label ID="lbldeliverydate" runat="server" Text='<%# Bind("DeliveryDate", "{0:D}") %>'></asp:Label>                    
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Currency" HeaderText="Currency" SortExpression="Currency" >
                <ItemStyle BackColor="White" />
            </asp:BoundField>
        </Fields>
        <HeaderStyle BackColor="#10659E" Font-Bold="True" ForeColor="White" />
        <FieldHeaderStyle Width="120px" />
    </asp:DetailsView>
    <asp:SqlDataSource ID="dsEventDetails" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
        SelectCommand="sp_GetBidTenderDetails" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter Name="BidTenderNo" SessionField="BidTenderNo" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>    
</div>