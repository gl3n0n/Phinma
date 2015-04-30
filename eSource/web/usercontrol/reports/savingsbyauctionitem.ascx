<%@ Control Language="C#" AutoEventWireup="true" CodeFile="savingsbyauctionitem.ascx.cs" Inherits="web_usercontrol_reports_savingsbyauctionitem" %>
<table border="0" cellpadding="0" cellspacing="0" style="font-size: 11px; font-family: Arial; width: 100%">    
    <tr>
        <td valign="top" style="padding-top: 5px; width: 115px;">
            Internal / External</td>
        <td>
            <asp:RadioButtonList ID="rblInternalExternal" runat="server" RepeatColumns="1" Font-Size="11px">
                <asp:ListItem Selected="True">External</asp:ListItem>
                <asp:ListItem>Internal</asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td valign="top" style="width: 115px; padding-top: 5px;">
            Auction Event
        </td>
        <td>
            <asp:TextBox ID="tbAuctionEvent" runat="server" Width="200px"></asp:TextBox><asp:Button ID="btnSearch" runat="server" Text="Search" />
        </td>
    </tr> 
 </table>
 <table border="0" cellpadding="0" cellspacing="0" style="font-size: 11px; font-family: Arial; width: 100%">    
    <tr>
        <td valign="top" style="padding-top: 5px" colspan="2">
            <asp:GridView runat="server" ID="gvAuctions" AllowPaging="True" SkinID="BidEvents"
                AllowSorting="True" AutoGenerateColumns="False"
                DataKeyNames="AuctionRefNo" DataSourceID="sdsAuctionEvents" EmptyDataText="No Auction Event to Display.">
                <Columns>
                    <asp:TemplateField>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                        <HeaderTemplate>
                            <asp:CheckBox ID="CheckAll" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbSelected" runat="server" />
                            <asp:HiddenField ID="hdAuctionRefNo" runat="server" Value='<%# Eval("AuctionRefNo") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Reference No." SortExpression="AuctionRefNo">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="90px" />
                        <ItemTemplate>
                            &nbsp;<asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("AuctionRefNo") %>' Width="95%"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Auction Event" SortExpression="ItemDesc">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            &nbsp;<asp:Label ID="lblAuctionEvent" runat="server" Text='<%# Eval("ItemDesc") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="sdsAuctionEvents" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="exec sp_GetAwardedAuctionEvents" FilterExpression="ItemDesc like '%{0}%'">
                <FilterParameters>
                    <asp:ControlParameter ControlID="tbAuctionEvent" Name="newparameter" PropertyName="Text" />
                </FilterParameters>
            </asp:SqlDataSource>
    </tr>
    <tr>
        <td colspan="2" style="height: 10px;">
        </td>
    </tr>        
    <tr>
        <td colspan="2" id="actions">
            <asp:LinkButton ID="lnkViewReport" runat="server" OnClick="lnkViewReport_Click" Width="100px">View Report</asp:LinkButton>
        </td>
    </tr>
</table>