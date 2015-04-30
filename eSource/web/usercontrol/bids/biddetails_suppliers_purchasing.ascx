<%@ Control Language="C#" AutoEventWireup="true" CodeFile="biddetails_suppliers_purchasing.ascx.cs" Inherits="web_usercontrol_bids_biddetails_suppliers_purchasing" %>
<div align="left">
    <asp:GridView AutoGenerateColumns="False" runat="server" ID="gvInvitedSuppliers"
		DataKeyNames="VendorId,TenderCount"
        OnRowDataBound="gvInvitedSuppliers_RowDataBound"
		SkinID="AuctionedItems" DataSourceID="dsParticipants" AllowSorting="true">
        <Columns>
            <asp:TemplateField HeaderText="Invited Suppliers" SortExpression="Supplier">
                <ItemStyle BackColor="White" />
                <HeaderStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    &nbsp;<asp:Label runat="server" ID="lblAccType" Text='<%#Bind("AccType") %>'></asp:Label><asp:Label runat="server" ID="lblVendors" Text='<%#Bind("Supplier") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Email" SortExpression="VendorEmail">
                <ItemStyle BackColor="White" HorizontalAlign="Center" Width="30%" />
                <HeaderStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("VendorEmail") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
			<asp:TemplateField HeaderText="Sumbission Status" SortExpression="TenderCount">
				<ItemStyle BackColor="White" HorizontalAlign="Center" Width="40%" />
                <HeaderStyle HorizontalAlign="Center" />
                <ItemTemplate>	
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("TenderCount") %>'></asp:Label>
                </ItemTemplate>
			</asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="dsParticipants" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
        SelectCommand="s3p_EBid_GetSuppliers" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter Name="BidRefNo" SessionField="BidRefNo" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</div>