<%@ Control Language="C#" AutoEventWireup="true" CodeFile="savingsbybiditem.ascx.cs" Inherits="web_usercontrol_reports_savingsbybiditem" %>
<table border="0" cellpadding="0" cellspacing="0" style="font-size: 11px; font-family: Arial; width: 100%">    
    <tr>
        <td valign="top" style="padding-top: 5px; width: 115px;">
            <%--Internal / External--%></td>
        <td>
            <asp:RadioButtonList ID="rblInternalExternal" runat="server" RepeatColumns="1" Font-Size="11px" Visible="False">
                <asp:ListItem Selected="True">External</asp:ListItem>
                <asp:ListItem>Internal</asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td valign="top" style="width: 115px; padding-top: 5px;">
            <%--Bid Event--%>
        </td>
        <td>
            <%--<asp:TextBox ID="tbBidEvent" runat="server" Width="200px"></asp:TextBox>
	    <asp:Button ID="btnSearch" runat="server" Text="Search" />--%>
        </td>
    </tr> 
 </table>
 <table border="0" cellpadding="0" cellspacing="0" style="font-size: 11px; font-family: Arial; width: 100%">    
    <tr>
        <td valign="top" style="padding-top: 5px" colspan="2">
            <asp:GridView runat="server" ID="gvBids" AllowPaging="True" SkinID="BidEvents"
                AllowSorting="True" AutoGenerateColumns="False"
                DataKeyNames="BidRefNo" DataSourceID="sdsBidEvents" EmptyDataText="No Bid Event to Display.">
                <Columns>
                    <asp:TemplateField>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                        <HeaderTemplate>
                            <asp:CheckBox ID="CheckAll" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbSelected" runat="server" />
                            <asp:HiddenField ID="hdBidRefNo" runat="server" Value='<%# Eval("BidRefNo") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Reference No." SortExpression="BidRefNo">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="90px" />
                        <ItemTemplate>
                            &nbsp;<asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("BidRefNo") %>' Width="95%"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bid Event" SortExpression="ItemDesc">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            &nbsp;<asp:Label ID="lblBidEvent" runat="server" Text='<%# Eval("ItemDesc") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <asp:SqlDataSource ID="sdsBidEvents" runat="server" ConnectionString='<%$ Code: (string) HttpContext.Current.Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetAwardedBidEventsB" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:SessionParameter Name="UserId" SessionField="UserId" Type="Int32" />
                    <asp:SessionParameter Name="UserType" SessionField="UserType" Type="Int32" />
                </SelectParameters>
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