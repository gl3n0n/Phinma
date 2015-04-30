<%@ Control Language="C#" AutoEventWireup="true" CodeFile="totalbidevents.ascx.cs" Inherits="web_usercontrol_reports_totalbidevents" %>
<%@ Register Assembly="Calendar" Namespace="CalendarControl" TagPrefix="cc1" %>
<table border="0" cellpadding="0" cellspacing="0" style="font-size: 11px; font-family: Arial; width: 100%">
    <tr>
        <td valign="top" style="padding-top: 5px;">
            Buyer</td>
        <td>
            <asp:ListBox ID="lbBuyers" runat="server" SelectionMode="Single" Width="300px" DataSourceID="sdsBuyers" DataTextField="BuyerFullName" DataValueField="BuyerId" Rows="1"></asp:ListBox><asp:SqlDataSource
                ID="sdsBuyers" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetAllBuyers" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
        </td>
    </tr>
    <tr>
        <td style="width: 113px;padding-top: 5px;" valign="top">
            Date</td>
        <td>
            <table style="width: 272px; font-size: 11px; font-family: Arial">
                <tr>
                    <td style="width: 44px">
                        From</td>
                    <td>
                        <cc1:JSCalendar ID="clndrStartDate" runat="server" ImageURL="../../calendar/img.gif" EnableViewState="true" ScriptsBasePath="../../calendar/" DateFormat="MM/dd/yyyy" Width="178px" ReadOnly="false" MaxLength="10"
                            TabIndex="-1"></cc1:JSCalendar></td>
                </tr>
                <tr>
                    <td style="width: 44px; height: 16px;">
                        To</td>
                    <td style="height: 16px">
                        <cc1:JSCalendar ID="clndrEndDate" runat="server" ImageURL="../../calendar/img.gif" EnableViewState="true" ScriptsBasePath="../../calendar/" DateFormat="MM/dd/yyyy" Width="178px" ReadOnly="false" MaxLength="10"
                            TabIndex="-1"></cc1:JSCalendar>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="height: 10px;">
        </td>
    </tr>
    <tr>
        <td colspan="2" id="actions">
            <asp:LinkButton ID="lnkViewReport" runat="server" OnClick="lnkViewReport_Click" Width="100px">View Details</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="height: 10px;">
        </td>
    </tr>
    <tr>
        <td colspan="2" style="height: 10px;">
            <asp:GridView ID="gvBids" runat="server" SkinID="BidEvents" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False" DataKeyNames="CompanyId" DataSourceID="dsTotalBidEvents"
                OnRowCommand="gvBids_RowCommand" Width="450">
                <Columns>
                    <asp:TemplateField HeaderText="Company" InsertVisible="False" SortExpression="Company">
                        <HeaderStyle HorizontalAlign="Center" Width="250" />
                        <ItemTemplate>
                            &nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Bind("Company") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Number of Bid Events" SortExpression="CntBidEvents">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            &nbsp;<asp:LinkButton ID="lnkCntBidEvents" runat="server" Text='<%# Bind("CntBidEvents") %>'
                                CommandArgument='<%# Bind("CompanyId") %>' CommandName="Details"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="dsTotalBidEvents" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                SelectCommand="sp_GetBuyerCntApprovedBidEventsByCompany" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:SessionParameter Name="BuyerId" SessionField="totalbidevents_BuyerId" Type="Int32" />
                    <asp:SessionParameter Name="FromDate" SessionField="totalbidevents_FromDate" Type="DateTime" />
                    <asp:SessionParameter Name="ToDate" SessionField="totalbidevents_ToDate" Type="DateTime" />
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="height: 10px;">
        </td>
    </tr>
</table>
