<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopNav5.ascx.cs" Inherits="WEB_user_control_TopNav5" %>
<table border="0" cellpadding="0" cellspacing="0" class="tasks">
    <tr>
        <td style="padding-left: 5px; text-align: left;">
            <iframe src="/clock.aspx" border="0" frameborder="0" scrolling="false" height="20" width="350"></iframe>
            <asp:Label runat="server" ID="lblDate" Font-Bold="True" Font-Size="12px" style="display:none" ></asp:Label>
            <asp:Label runat="server" ID="lblTime" Font-Bold="True" Font-Size="12px" style="display:none" ></asp:Label>
        </td>
        <td style="width: 10px;">
        </td>
        <td style="padding-right: 10px; text-align: right;">
            <asp:LinkButton runat="server" ID="lnkCreateNewItem" OnClick="lnkCreateNewItem_Click"
                CausesValidation="False">Create New VSF</asp:LinkButton>
        </td>
    </tr>
</table>
