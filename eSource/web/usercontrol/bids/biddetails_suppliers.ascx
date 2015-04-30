<%@ Control Language="C#" AutoEventWireup="true" CodeFile="biddetails_suppliers.ascx.cs" Inherits="web_usercontrol_bids_biddetails_suppliers" %>
<div align="left">
    <asp:GridView AutoGenerateColumns="False" runat="server" ID="gvInvitedSuppliers"
		DataKeyNames="VendorId,TenderCount,OriginalFileName"
        OnRowDataBound="gvInvitedSuppliers_RowDataBound" OnRowCommand="gvFileAttachments_RowCommand"
		SkinID="AuctionedItems" DataSourceID="dsParticipants" AllowSorting="true" EmptyDataText="No Attachments">
			    	
        <Columns>
            <asp:TemplateField HeaderText="Invited Suppliers" SortExpression="Supplier">
                <ItemStyle BackColor="White" />
                <HeaderStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    &nbsp;<asp:Label runat="server" ID="lblAccType" Text='<%#Bind("AccType") %>'></asp:Label><asp:Label runat="server" ID="lblVendors" Text='<%#Bind("Supplier") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Email" SortExpression="VendorEmail">
                <ItemStyle BackColor="White" HorizontalAlign="Center" Width="25%" />
                <HeaderStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("VendorEmail") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
			<asp:TemplateField HeaderText="Submission Status" SortExpression="TenderCount">
				<ItemStyle BackColor="White" HorizontalAlign="Center" Width="20%" />
                <HeaderStyle HorizontalAlign="Center" />
                <ItemTemplate>	
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("TenderCount") %>'></asp:Label>
                </ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="&nbsp;File Attachments" SortExpression="TenderCount">
				<ItemStyle BackColor="White" HorizontalAlign="Center" Width="25%" />
                <HeaderStyle HorizontalAlign="Center" />
				<ItemStyle HorizontalAlign="Left" />
                <ItemTemplate>	
					&nbsp;&nbsp;<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/web/themes/images/paperclip.gif" Width="15px" Height="15px" 
                        AlternateText="Click to download" CausesValidation="false" CommandArgument='<%# Bind("FileAttachment") %>' CommandName="Download" />
                    <asp:LinkButton ID="lnkDownload" runat="server" ToolTip="Click to download" CausesValidation="false" Text='<%# Bind("OriginalFileName") %>' CommandArgument='<%# Bind("FileAttachment") %>' CommandName="Download" Width="85%"></asp:LinkButton>
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