<%@ Control Language="C#" AutoEventWireup="true" CodeFile="buyer_bidtenderdetails.ascx.cs" Inherits="web_usercontrol_Buyer_buyer_bidtenderdetails" %>
<%@ Register Src="bidtender_attachments.ascx" TagName="bidtender_attachments" TagPrefix="uc1" %>

	
<asp:GridView runat="server" ID="dvBidTender" SkinID="BidEvents" AllowPaging="True" AllowSorting="true" DataSourceID="dsTenderDetails" AutoGenerateColumns="false" HeaderText="Bid Tender Details">
	<Columns>
		<asp:TemplateField HeaderText="Bidder" SortExpression="VendorName">
			<HeaderStyle HorizontalAlign="Center" Width="80px" />
			<ItemTemplate>
				&nbsp;<asp:Label ID="lblVendor" runat="server" Text='<%# Bind("VendorName")%>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>		
		<asp:TemplateField HeaderText="Quantity">
			<HeaderStyle HorizontalAlign="Center" />
			<ItemTemplate>
				&nbsp;<asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("Qty", "{0:#,##0}")%>'></asp:Label>
                &nbsp;<asp:Label ID="lblUOM" runat="server" Text='<%# Bind("UnitOfMeasure")%>'></asp:Label>(s) 
			</ItemTemplate>
		</asp:TemplateField>		
		<asp:TemplateField HeaderText="Unit Price">
			<HeaderStyle HorizontalAlign="Center" />
			<ItemTemplate>
				<asp:Label ID="lblMount" runat="server" Text='<%# Bind("Amount", "{0:#,##0.00}")%>'></asp:Label>
			</ItemTemplate>
			<ItemStyle HorizontalAlign="Center" />
		</asp:TemplateField>		
		<asp:TemplateField HeaderText="Discount">
			<HeaderStyle HorizontalAlign="Center" />
			<ItemStyle HorizontalAlign="Center" Width="70px" />
			<ItemTemplate>
				&nbsp;
				<asp:Label ID="lblDiscount" runat="server" Text='<%# Bind("Discount", "{0:#,##0.00}") %>'></asp:Label>  
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Delivery Cost">
			<HeaderStyle HorizontalAlign="Center" />
			<ItemStyle HorizontalAlign="Center" />
			<ItemTemplate>
				&nbsp;
                <asp:Label ID="lblDeliveryCost" runat="server" Text='<%# Bind("DeliveryCost", "{0:#,##0.00}") %>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>		
		<asp:TemplateField HeaderText="Total Bid Tender Price">
			<HeaderStyle HorizontalAlign="Center" Width="120px" />
			<ItemTemplate>
				&nbsp;
                <asp:Label ID="Label1" runat="server" Text='<%# Bind("BidPrice", "{0:#,##0.00}") %>' Font-Bold="true"></asp:Label>  
			</ItemTemplate>
			<ItemStyle HorizontalAlign="Center" />
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Warranty">
			<HeaderStyle HorizontalAlign="Center" />
			<ItemStyle HorizontalAlign="Center" Width="180px" />
			<ItemTemplate>
				&nbsp;
                <asp:Label ID="lblWarranty" runat="server" Text='<%# Bind("Warranty")%>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Remarks">
			<HeaderStyle HorizontalAlign="Center" />
			<ItemStyle HorizontalAlign="Center" Width="100px" />
			<ItemTemplate>
				&nbsp;
                <asp:Label ID="Label8" runat="server" Text='<%# Eval("Remarks").ToString().Trim().Length > 0 ? Eval("Remarks").ToString() : "No Remarks" %>'
                                                                            ForeColor='<%# Eval("Remarks").ToString().Trim().Length > 0 ? System.Drawing.Color.Black : System.Drawing.Color.Gray %>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>		
	</Columns>
</asp:GridView>
<asp:SqlDataSource ID="dsTenderDetails" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' 
	SelectCommand="sp_GetBidTenderDetailsAll" SelectCommandType="StoredProcedure">
	<SelectParameters>
		<asp:SessionParameter Name="BidRefNo" SessionField="BidRefNo" DefaultValue="0" Type="Int32" />
	</SelectParameters>
</asp:SqlDataSource>
<uc1:bidtender_attachments ID="Bidtender_attachments1" runat="server" />
