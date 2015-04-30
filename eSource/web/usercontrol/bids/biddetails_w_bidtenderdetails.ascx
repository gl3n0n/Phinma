<%@ Control Language="C#" AutoEventWireup="true" CodeFile="biddetails_w_bidtenderdetails.ascx.cs" Inherits="web_usercontrol_bids_biddetails_w_bidtenderdetails" %>
<div align="left">
    <asp:Panel ID="pnlVendor" runat="server" Width="125px">
        <asp:LinkButton ID="lnkContactBuyer" runat="server" OnClick="lnkContactBuyer_Click">Contact Buyer</asp:LinkButton>
    </asp:Panel>    
    <asp:DetailsView ID="dView" runat="server" AutoGenerateRows="False"
        DataSourceID="dsEventDetails" 
        HeaderText="Details" SkinID="BidDetails" DataKeyNames="BidRefNo,Buyer,ItemDesc,BuyerId">        
        <Fields>
            <asp:TemplateField HeaderText="Bid Reference No." InsertVisible="False" SortExpression="BidRefNo">
                <HeaderStyle Width="133px" />
                <ItemStyle BackColor="White" />
                <ItemTemplate>
                    <asp:Label ID="Label14" runat="server" Text='<%# Bind("BidRefNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description" SortExpression="ItemDesc">
                <HeaderStyle Width="133px" />
                <ItemStyle BackColor="White" />
                <ItemTemplate>
                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("ItemDesc") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>            
            <asp:TemplateField HeaderText="PR Ref No." SortExpression="PRRefNo" Visible="false">
                <HeaderStyle Width="133px" />
                <ItemStyle BackColor="White" />
                <ItemTemplate>
                    <asp:Label ID="Label13" runat="server" Text='<%# Bind("PRRefNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PR Date" SortExpression="PRDate" Visible="false">
                <HeaderStyle Width="133px" />
                <ItemStyle BackColor="White" />
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("PRDate", "{0:D}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Requestor" SortExpression="Requestor">
                <HeaderStyle Width="133px" />
                <ItemStyle BackColor="White" />
                <ItemTemplate>
                    <asp:Label ID="Label12" runat="server" Text='<%# Bind("Requestor") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:TemplateField HeaderText="Company" SortExpression="Company">
                <HeaderStyle Width="133px" />
                <ItemStyle BackColor="White" />
                <ItemTemplate>
                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("Company") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Group / Department" SortExpression="GroupDeptSecName">
                <HeaderStyle Width="133px" />
                <ItemStyle BackColor="White" />
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("GroupDeptSecName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="Currency" SortExpression="Currency" Visible="false">
                <HeaderStyle Width="133px" />
                <ItemStyle BackColor="White" />
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("Currency") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:TemplateField HeaderText="MainCategory" SortExpression="MainCategoryName">
                <HeaderStyle Width="133px" />
                <ItemStyle BackColor="White" />
                <ItemTemplate>
                    <asp:Label ID="Label61" runat="server" Text='<%# Bind("MainCategoryName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="Category" SortExpression="CategoryName">
                <HeaderStyle Width="133px" />
                <ItemStyle BackColor="White" />
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="DeliverTo" SortExpression="DeliverTo" Visible="false">
                <HeaderStyle Width="133px" />
                <ItemStyle BackColor="White" />
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("DeliverTo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Incoterm" SortExpression="Incoterm" Visible="false">
                <HeaderStyle Width="133px" />
                <ItemStyle BackColor="White" />
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("Incoterm") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Buyer / Creator" SortExpression="Buyer">
                <HeaderStyle Width="133px" />
                <ItemStyle BackColor="White" />
                <ItemTemplate>
                    <asp:Label ID="Label10" runat="server" Text='<%# Bind("Buyer") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Date Created" SortExpression="DateCreated">
                <HeaderStyle Width="133px" />
                <ItemStyle BackColor="White" />
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("DateCreated", "{0:D}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Bid Submission Deadline" SortExpression="Deadline">
                <HeaderStyle Width="133px" />
                <ItemStyle BackColor="White" />
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Deadline", "{0:D}  {0:T}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Fields>
    </asp:DetailsView>
    <asp:SqlDataSource ID="dsEventDetails" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
        SelectCommand="sp_GetBidEventDetails" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter Name="BidRefNo" SessionField="BidRefNo" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <asp:GridView AutoGenerateColumns="False" AllowSorting="true" runat="server" ID="gvBidItemDetails" SkinID="AuctionedItems"
        DataSourceID="dsItemDetails" EmptyDataText="No Bid Items" OnRowCommand="gvBidItemDetails_RowCommand">        
        <Columns>
            <asp:TemplateField HeaderText="Item" SortExpression="Item">
                <ItemStyle CssClass="itemDetails_td" Width="65px" />
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDetailDesc" runat="server" Text='<%# Bind("Item") %>' CommandName="BidTenderDetails" 
                    CommandArgument='<%# Bind("TenderNo", "{0}") %>'></asp:LinkButton>                                                                
                </ItemTemplate>
                <ItemStyle BackColor="White" Width="250px" />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description" SortExpression="DetailDesc">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblDescription" Text='<%#Bind("DetailDesc") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"/>
                <HeaderStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quantity" SortExpression="Qty">
                <ItemStyle BackColor="White" HorizontalAlign="Center" Width="120px" />
                <HeaderStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblQuantity" Text='<%#Bind("Qty", "{0:#,##0}") %>'></asp:Label>
                    <asp:Label runat="server" ID="lblUnitOfMeasure" Text='<%#Bind("UnitOfMeasure") %>'></asp:Label>
                </ItemTemplate>                
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delivery Date" SortExpression="DeliveryDate">
                <ItemStyle BackColor="White" HorizontalAlign="Center" Width="180px" />
                <HeaderStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblDeliveryDate" Text='<%#Bind("DeliveryDate", "{0:D}") %>'></asp:Label>
                </ItemTemplate>                
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status" SortExpression="ItemStatus">
                <ItemStyle HorizontalAlign="Center" Width="100px" />
                <HeaderStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblStatus" Text='<%#Bind("ItemStatus") %>'></asp:Label>
                </ItemTemplate>                
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="dsItemDetails" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
        SelectCommand="sp_GetBidItemDetailsStatus" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter Name="BidRefNo" SessionField="BidRefNo" Type="Int32" />
            <asp:SessionParameter Name="VendorId" SessionField="UserId" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>    
</div>
