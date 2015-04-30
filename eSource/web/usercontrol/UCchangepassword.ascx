<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCchangepassword.ascx.cs"
    Inherits="web_usercontrol_UCchangepassword" %>
<div>
    <table style="font-family: Arial; font-size: 11px;" width="100%" border="0" cellpadding="0" cellspacing="0"> 
        <tr>
            <td colspan="2">
                Password must be:
                <br />
                &nbsp;» Atleast 7 characters<br />
                &nbsp;» Contains atleast 1 letter (a-z / A-Z)<br />
                &nbsp;» Contains atleast 1 number (0-9)<br />
                &nbsp;» Contains atleast 1 symbol (-,+,?,*,$,.,|,!,@,#,%,&,_,=,:,)<br />
                <br />
            </td>
        </tr>       
        <tr>
            <td valign="bottom" style="width: 120px;">
                Current Password:</td>
            <td valign="bottom">
                <label>
                    <asp:TextBox ID="txtCurrentPwd" runat="server" CausesValidation="True" TabIndex="1"
                        Width="150px" TextMode="Password"></asp:TextBox>&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCurrentPwd"
                        ErrorMessage="*" SetFocusOnError="True" Width="16px"></asp:RequiredFieldValidator></label></td>
        </tr>
        <tr>
            <td valign="bottom">
                <label>
                    New Password:</label></td>
            <td valign="bottom">
                <asp:TextBox ID="txtNewPwd" runat="server" CausesValidation="True" TabIndex="1" Width="150px"
                    TextMode="Password"></asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNewPwd"
                    ErrorMessage="*" SetFocusOnError="True" Width="11px"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td valign="bottom">
                <label>
                    Confirm Password:</label></td>
            <td valign="bottom">
                <asp:TextBox ID="txtConfirmPwd" runat="server" CausesValidation="True" TabIndex="1"
                    Width="150px" TextMode="Password"></asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtConfirmPwd"
                    ErrorMessage="*" SetFocusOnError="True" Width="14px"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td align="center" colspan="2" style="text-align:left; padding-left:15px;">
                <br />
                <asp:CompareValidator ID="cvPasswordCompare" runat="server" ErrorMessage="Passwords doesn't match.<br />"
                    Display="Dynamic" ControlToValidate="txtConfirmPwd" ControlToCompare="txtNewPwd"></asp:CompareValidator>
                <asp:CustomValidator ID="cvVerifyPasswords" runat="server" ControlToValidate="txtCurrentPwd"
                    Display="Dynamic" ErrorMessage="Invalid current password.<br />" OnServerValidate="cvVerifyPasswords_ServerValidate"
                    SetFocusOnError="True"></asp:CustomValidator>
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                <br />
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
        <tr>
            <td style="height: 19px">
                <asp:LinkButton ID="lnkchange" runat="server" OnClick="lnkchange_Click" Text="Change Password"
                    Width="120px"></asp:LinkButton>
                <asp:LinkButton ID="lnkBack" runat="server" OnClick="lnkBack_Click" Text="Back" Width="120px" CausesValidation="False"></asp:LinkButton>
            </td>
        </tr>
    </table>
</div>
