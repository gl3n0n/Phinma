<%@ Page Language="C#" AutoEventWireup="true" CodeFile="changepassword.aspx.cs" Inherits="admin_changePassword" %>

<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminTopNav" Src="~/usercontrol/admin/adminTopNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Admin_TopNav_Home" Src="~/usercontrol/admin/Admin_TopNav_Home.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminLeftNavUser" Src="~/usercontrol/admin/AdminLeftNavUser.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminLeftNav" Src="~/usercontrol/admin/AdminLeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/usercontrol/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>.:| Trans-Asia | Change Password |:.</title>
    <meta http-equiv="Content-Language" content="en-us">
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/style.css" rel="stylesheet" type="text/css" /> 
    <link rel="stylesheet" type="text/css" href="../css/style_ua.css" />
    <script type="text/javascript" src="../include/util.js"></script>
    <script type="text/javascript" src="../include/generaljsfunctions.js"  ></script>
    <script type="text/javascript" src="../include/supplierregistration.js"></script>               
</head>
<body onload="SetStatus();ChangePasswordFocus();">
    <div align="left">
        <form id="form1" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
                <tr>
                    <td valign="top" height="137px">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <div align="left" id="masthead">
                                        <ebid:admintopnav runat="server" id="GlobalLinksNav" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <ebid:admin_topnav_home runat="server" id="Admin_TopNav_Home" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <ebid:topdate runat="server" id="TopDate" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="height: 100%;">
                            <tr>
                                <td id="relatedInfo">
                                    <h2>
                                        Admin Functions</h2>
                                    <ebid:adminleftnav runat="server" id="AdminLeftNav" />
                                </td>
                                <td id="content">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page0">
                                        <tr>
                                            <td valign="top">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td id="content0">
															<br />
                                                            <h1>Change Password</h1>                                                                                                                                                                                    
                                                            <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%" id="pageDetails"
                                                                bordercolor="white">
                                                                <tr>
																	<td colspan="2" height="15px">
																		<asp:Label runat="server" ID="lblError" Font-Names="Arial" Font-Size="11px" ForeColor="Red" Visible="false" Text="" />
																	</td>
                                                                </tr>
                                                                <tr class="value">
                                                                    <td style="width: 120px;padding-top: 7px" >
                                                                        Current Password:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbPassword" name="tbPassword" runat="server" Width="150px" MaxLength="20" TextMode="Password" />
                                                                        <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="tbPassword"
                                                                            ErrorMessage="<br />» Current Password is required." Font-Size="11px" SetFocusOnError="True"
                                                                            Text="* Required" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                        <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="tbPassword"
                                                                            ErrorMessage="<br />» Current Password is invalid." OnServerValidate="CheckPassword"
                                                                            Text="* Invalid Password"></asp:CustomValidator></td>
                                                                </tr>
                                                                <tr class="value">
                                                                    <td style="width: 120px;padding-top: 7px; height: 25px;" >
                                                                        New Password:</td>
                                                                    <td style="height: 25px">
                                                                        <asp:TextBox ID="tbNewPassword" name="tbNewPassword" runat="server" Width="150px" MaxLength="20" TextMode="Password" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbNewPassword"
                                                                            ErrorMessage="<br />» New Password is required." Font-Size="11px" SetFocusOnError="True"
                                                                            Text="* Required" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                        <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="tbNewPassword"
                                                                            OnServerValidate="CheckPasswordLength" Text="* Password is too weak"></asp:CustomValidator></td>
                                                                </tr>
                                                                <tr class="value">
                                                                    <td style="width: 120px;padding-top: 7px">
                                                                        Confirm New Password:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbConfNewPassword" name="tbConfNewPassword" runat="server" Width="150px" MaxLength="20" TextMode="Password" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbConfNewPassword"
                                                                            ErrorMessage="<br />» New Password Confirmation is required." Font-Size="11px"
                                                                            SetFocusOnError="True" Text="* Required" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="tbNewPassword" ControlToValidate="tbConfNewPassword" 
                                                                            ErrorMessage="<br />» Password confirmation doesn't match.">* Password Mismatch</asp:CompareValidator></td>
                                                                </tr>   
                                                                <tr>
                                                                    <td colspan="2" align="left">
                                                                        <br />
                                                                        <div style="text-align: justify; width: 350px; padding-left: 10px;">
                                                                            <asp:ValidationSummary id="ValidationSummary1" runat="server" Font-Size="11px" 
                                                                                HeaderText="<b>Please review the following:</b>" DisplayMode="SingleParagraph" Font-Names="Arial">
                                                                            </asp:ValidationSummary>
                                                                        </div>
                                                                    </td>
                                                                </tr>                                                             
                                                            </table>
                                                            <br />
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                                <tr>
                                                                    <td>
                                                                        <asp:LinkButton ID="lnkSave" runat="server" Text="Save" 
                                                                            OnClick="lnkSave_Click"></asp:LinkButton><a href="index.aspx">Cancel</a>
                                                                    </td>                                                                            
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 20px;">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td id="footer" height="50px">
                        <ebid:footer runat="server" id="Footer1" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
