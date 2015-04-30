<%@ Control Language="C#" AutoEventWireup="true" CodeFile="announcementdetail.ascx.cs" Inherits="web_usercontrol_announcementdetail" %>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td valign="top">
            <br />
            <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" DataKeyNames="ID" DataSourceID="dsAnnouncementDetails" BorderWidth="0">
                <Fields>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <h1>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                            </h1>
                            <br />
                            <p>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Content", "&nbsp;&nbsp;{0}") %>'></asp:Label>
                            </p>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Fields>
            </asp:DetailsView>
            <asp:SqlDataSource ID="dsAnnouncementDetails" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetContentDetails" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:SessionParameter DefaultValue="0" Name="ID" SessionField="ContentId" Type="Int32" />
                    <asp:Parameter DefaultValue="2" Name="Type" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
</table>
<table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
    <tr>
        <td style="height: 19px">            
            <asp:LinkButton ID="lnkBack" runat="server" Text="Back" Width="100px" CausesValidation="False" OnClick="lnkBack_Click"></asp:LinkButton>
        </td>
    </tr>
</table>
