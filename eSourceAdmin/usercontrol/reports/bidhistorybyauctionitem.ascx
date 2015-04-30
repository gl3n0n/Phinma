<%@ Control Language="C#" AutoEventWireup="true" CodeFile="bidhistorybyauctionitem.ascx.cs" Inherits="web_usercontrol_reports_bidhistorybyauctionitem" %>
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
        <td>
            <asp:GridView runat="server" ID="gvAuctions" AllowPaging="True" SkinID="BidEvents"
                AllowSorting="True" AutoGenerateColumns="False" OnRowCommand="gvBids_RowCommand" CssClass="pageDetails"
                DataKeyNames="AuctionRefNo" DataSourceID="sdsAuctionEvents" EmptyDataText="No Auction Event to Display.">
                <Columns>
                    <asp:TemplateField HeaderText="Reference No." SortExpression="AuctionRefNo">
                        <HeaderStyle HorizontalAlign="Center" CssClass="pageDetails_th" ForeColor="#FFFFFF"  />
                        <ItemStyle Width="90px" />
                        <ItemTemplate>
                            &nbsp;<asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("AuctionRefNo") %>' Width="95%"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Auction Event" SortExpression="ItemDesc">
                        <HeaderStyle HorizontalAlign="Center" CssClass="pageDetails_th" ForeColor="#FFFFFF"   />
                        <ItemTemplate>
                            &nbsp;<asp:Label ID="lblAuctionEvent" runat="server" Text='<%# Eval("ItemDesc") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <HeaderStyle HorizontalAlign="Center" CssClass="pageDetails_th" ForeColor="#FFFFFF"  />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                        <ItemTemplate>
                            &nbsp;<asp:LinkButton ID="lnkViewReport" runat="server" CommandArgument='<%# Eval("AuctionRefNo") %>' CommandName="ViewReport" Width="100px">View Report</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="sdsAuctionEvents" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="exec sp_GetFinishedAuctionEvents" FilterExpression="ItemDesc like '{0}%'">
                <FilterParameters>
                    <asp:ControlParameter ControlID="tbAuctionEvent" Name="newparameter" PropertyName="Text" />
                </FilterParameters>
            </asp:SqlDataSource>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="height: 10px;">
        </td>
    </tr>        
    <tr>
        <td colspan="2" id="actions">
            &nbsp;
        </td>
    </tr>   
</table>