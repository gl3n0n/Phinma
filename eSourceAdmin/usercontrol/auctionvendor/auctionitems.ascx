<%@ Control Language="C#" AutoEventWireup="true" CodeFile="auctionitems.ascx.cs" Inherits="web_usercontrol_auctionvendor_auctionitems" %>
<div>
    <br />
    <asp:GridView ID="gvAuctionedItems" runat="server" SkinID="AuctionedItems" 
        AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" 
        DataKeyNames="AuctionDetailNo" DataSourceID="dsAuctionedItems">
        <Columns>
            <asp:TemplateField HeaderText="Detail No." SortExpression="AuctionDetailNo">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle Width="90px" />
                <ItemTemplate>
                    &nbsp;<asp:Label ID="lblAuctionDetailNo" runat="server" Text='<%# Eval("AuctionDetailNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item" SortExpression="ItemName">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    &nbsp;<asp:Label ID="lblItemName" runat="server" Text='<%# Eval("PItemName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description" SortExpression="Description">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    &nbsp;<asp:Label ID="lblItemDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quantity" SortExpression="Quantity">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle Width="100px" HorizontalAlign="Center" />
                <ItemTemplate>
                    &nbsp;<asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Unit Of Measure" SortExpression="UnitOfMeasure">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle Width="94px" HorizontalAlign="Center" />
                <ItemTemplate>
                    &nbsp;<asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UnitOfMeasure") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Starting Price" SortExpression="StartingPrice">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle Width="94px" HorizontalAlign="Center" />
                <ItemTemplate>
                    &nbsp;<asp:Label ID="lblStartingPrice" runat="server" Text='<%# Bind("StartingPrice", "{0:#,##0.00}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField  HeaderText="Increment / Decrement" SortExpression="IncrementDecrement">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle Width="94px" HorizontalAlign="Center" />
                <ItemTemplate>
                    &nbsp;<asp:Label ID="lblIncDec" runat="server" Text='<%# Bind("IncrementDecrement", "{0:#,##0.00}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="dsAuctionedItems" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetAuctionedItemsByAuctionRefNo" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter Name="AuctionRefNo" SessionField="AuctionRefNo" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</div>
