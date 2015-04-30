<%@ Control Language="C#" AutoEventWireup="true" CodeFile="auctionparticipants.ascx.cs" Inherits="web_usercontrol_auctionvendor_auctionparticipants" %>
<div>
    <br />
    <asp:GridView ID="gvAuctionParticipants" runat="server" SkinID="AuctionedItems" 
        AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ParticipantId" DataSourceID="dsAuctionedParticipants">
        <Columns>
            <asp:TemplateField HeaderText="Participant Id" InsertVisible="False" SortExpression="ParticipantId">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle Width="90px" />
                <ItemTemplate>
                    &nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Bind("ParticipantId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>            
            <asp:TemplateField HeaderText="Name" SortExpression="VendorName">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("VendorName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Alias" SortExpression="Alias">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Alias") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Email" SortExpression="VendorEmail">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("VendorEmail") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Mobile No." SortExpression="MobileNo">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("MobileNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="No. Of Email Sent" SortExpression="EmailSent">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("EmailSent") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="dsAuctionedParticipants" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetAuctionParticipants" SelectCommandType="StoredProcedure">        
        <SelectParameters>
            <asp:SessionParameter DefaultValue="0" Name="AuctionRefNo" SessionField="AuctionRefNo" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</div>
