<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminLeftNav.ascx.cs"
    Inherits="web_usercontrol_admin_AdminLeftNav" %>
<link type="text/css" href="../../css/style.css" rel="stylesheet" />
<table border="0" cellpadding="0" cellspacing="0" width="100%" id="related">
   <tr>
        <td>
            <asp:LinkButton runat="server" ID="lnkEditProfile" OnClick="lnkEditProfile_Click" CausesValidation="false">Edit Profile</asp:LinkButton>
        </td>
    </tr>  
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="lnkChangePassword" NavigateUrl="~/admin/changepassword.aspx">Change Password</asp:HyperLink>
        </td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="lnkConfigurationSettings" NavigateUrl="~/admin/configsettings.aspx">Configuration Settings</asp:HyperLink>
        </td>
    </tr>
</table>
