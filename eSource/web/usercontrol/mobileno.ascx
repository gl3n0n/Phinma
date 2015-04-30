<%@ Control Language="C#" AutoEventWireup="true" CodeFile="mobileno.ascx.cs" Inherits="web_usercontrol_mobileno" %>

<asp:DropDownList ID="ddlMobilePrefix" runat="server">
</asp:DropDownList><asp:RequiredFieldValidator ID="rfvPrefix" runat="server" ErrorMessage="*" ControlToValidate="ddlMobilePrefix" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
<asp:TextBox ID="txtMobileNo" runat="server" MaxLength="7" Width="96px"></asp:TextBox>&nbsp;
<asp:RequiredFieldValidator ID="rfvMobileNo" runat="server" ErrorMessage="*" ControlToValidate="txtMobileNo" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
