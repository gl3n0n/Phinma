<%@ Control Language="C#" AutoEventWireup="true" CodeFile="auctiondetail.ascx.cs" Inherits="web_usercontrol_auctionvendor_auctiondetail" %>
<div align="left">
    <br />
   <asp:Panel ID="pnlVendor" runat="server" Width="125px">
        <asp:LinkButton ID="lnkContactBuyer" runat="server" OnClick="lnkContactBuyer_Click">Contact Buyer</asp:LinkButton>
    </asp:Panel>     
    <asp:DetailsView ID="dvEventDetails" runat="server" DataSourceID="dsAuctionDetails" DataKeyNames="AuctionRefNo,ItemDesc,BuyerId" 
        AutoGenerateRows="False" HeaderText="Auction Event Details" SkinID="BidDetails">
        <Fields>
            <asp:TemplateField HeaderText="Reference No." SortExpression="AuctionRefNo">
                <ItemTemplate>
                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("AuctionRefNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description" SortExpression="ItemDesc">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("ItemDesc") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Type" SortExpression="Type">
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Confirmation Deadline" SortExpression="AuctionDeadline">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("AuctionDeadline", "{0:D}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Start Date &amp; Time" SortExpression="AuctionStartDateTime">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("AuctionStartDateTime", "{0:D}&nbsp;{0:T}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="End Date &amp; Time" SortExpression="AuctionEndDateTime">
                <ItemTemplate>
                    <asp:Label ID="lblAuctionEndTime" runat="server" Text='<%# Eval("AuctionEndDateTime", "{0:D}&nbsp;{0:T}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Requestor" SortExpression="Requestor">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Requestor") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>            
            <asp:TemplateField HeaderText="Buyer" SortExpression="Buyer">
               <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("Buyer") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Currency" SortExpression="Currency">
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("Currency") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PR Ref No." SortExpression="PRRefNo">
                <ItemTemplate>
                    <asp:Label ID="Label14" runat="server" Text='<%# Bind("PRRefNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PR Date" SortExpression="PRDate">
                <ItemTemplate>
                    <asp:Label ID="Label13" runat="server" Text='<%# Bind("PRDate", "{0:D}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Category" SortExpression="CategoryName">
                <ItemTemplate>
                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Company" SortExpression="Company">
                <ItemTemplate>
                    <asp:Label ID="Label10" runat="server" Text='<%# Bind("Company") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>            
        </Fields>
    </asp:DetailsView>
    <asp:SqlDataSource ID="dsAuctionDetails" runat="server" SelectCommandType="StoredProcedure" SelectCommand="sp_GetAuctionDetails" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'>
        <SelectParameters>
            <asp:SessionParameter DefaultValue="0" Name="AuctionRefNo" SessionField="AuctionRefNo" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</div>

