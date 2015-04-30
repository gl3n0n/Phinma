<%@ Control Language="C#" AutoEventWireup="true" CodeFile="auctionitemdetail.ascx.cs" Inherits="web_usercontrol_auctionvendor_auctionitemdetail" %>
<div align="left">
    <br />
    <asp:DetailsView ID="dvAuctionItemDetails" runat="server" AutoGenerateRows="False"
        Width="100%" SkinID="BidDetails" HeaderText="Auction Item Details" DataSourceID="dsAuctionItemDetails">
        <HeaderStyle BackColor="#10659E" Font-Bold="True" ForeColor="White" />
        <FieldHeaderStyle Width="120px" />
        <Fields>
            <asp:TemplateField HeaderText="Detail No." InsertVisible="False" >
               <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("AuctionDetailNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item" >
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Item") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description" >
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Currency" >
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("Currency") %>'></asp:Label>                    
                </ItemTemplate>
            </asp:TemplateField>    
            <asp:TemplateField HeaderText="Quantity" SortExpression="Quantity">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("Quantity", "{0:#,##0}") %>'></asp:Label>
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("UnitOfMeasure")%>'></asp:Label>(s)
                </ItemTemplate>
            </asp:TemplateField>            
            <asp:TemplateField HeaderText="Starting Price" >
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("StartingPrice", "{0:#,##0.00}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Increment / Decrement" >
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("IncrementDecrement", "{0:#,##0.00}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Fields>
    </asp:DetailsView>    
    <asp:SqlDataSource ID="dsAuctionItemDetails" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetAuctionItemDetails" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="0" Name="AuctionDetailNo" SessionField="AuctionDetailNo" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</div>