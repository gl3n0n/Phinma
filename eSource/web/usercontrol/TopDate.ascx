<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopDate.ascx.cs" Inherits="web_user_control_TopDate" %>
<div id="loader"></div>
<table border="0" cellpadding="0" cellspacing="0" class="tasks">
    <tr>       
        <td style="padding-left: 10px; text-align: left; color:#fff;">
            <iframe src="/clock.aspx" border="0" frameborder="0" scrolling="no" height="20" width="350"></iframe>
            <asp:Label runat="server" ID="lblDate" Font-Bold="True" Font-Size="12px" style="display:none" ></asp:Label>
        </td>        
    </tr>
</table>