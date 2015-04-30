<%@ Page Language="C#" AutoEventWireup="true" CodeFile="otlsdetails_add.aspx.cs"
    Inherits="web_otlsdetails_add" %>

<%@ Register Src="../usercontrol/mobileno.ascx" TagName="mobileno" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <script src="../include/util.js" type="text/javascript"></script>
    <script src="../include/events.js" type="text/javascript"></script>
</head>
<body oncontextmenu="return false;">
    <form id="Form1" runat="server" defaultfocus="tbUserName" defaultbutton="lnkOK">
        <div align="left">
            <table cellpadding="10" cellspacing="0" border="0" width="100%">
                <tr>
                    <td>
                        <br />
                        <h1>
                            &nbsp;Add One-Time Supplier</h1>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails">
                            <tr>
                                <th width="152">&nbsp;
                                    </th>
                                <th>&nbsp;
                                    </th>
                            </tr>
                            <tr>
                                <td width="149">
                                    Category</td>
                                <td class="value">
                                    <asp:Label ID="lblCategory" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Sub Category</td>
                                <td class="value">
                                    <asp:Label ID="lblSubCategory" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: 10px;">
                                    Username</td>
                                <td class="value">
                                    <asp:TextBox ID="tbUserName" runat="server" MaxLength="100" Width="250px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ErrorMessage="*" ControlToValidate="tbUserName"
                                        Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: 10px;">
                                    Company</td>
                                <td class="value">
                                    <asp:TextBox runat="server" ID="txtVendorName" Width="350px" MaxLength="120"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvVendorName" runat="server" ControlToValidate="txtVendorName"
                                        Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: 10px;">
                                    Address
                                </td>
                                <td class="value">
                                    <asp:TextBox runat="server" ID="txtAddress1" Width="380px" MaxLength="120"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ErrorMessage="*"
                                        ControlToValidate="txtAddress1" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <br />
                                    <asp:TextBox runat="server" ID="txtAddress2" Width="380px" MaxLength="120"></asp:TextBox>                                    
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: 10px;">
                                    Contact Person</td>
                                <td class="value">
                                    <asp:TextBox runat="server" ID="txtContactPerson" Width="250px" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvContactPerson" runat="server" ErrorMessage="*"
                                        ControlToValidate="txtContactPerson" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: 10px;">
                                    Telephone Number
                                </td>
                                <td class="value">
                                    <asp:TextBox runat="server" ID="txtTelephoneNumber" Width="150px" MaxLength="12"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvTelephoneNo" runat="server" ErrorMessage="*"
                                        ControlToValidate="txtTelephoneNumber" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: 10px;">
                                    Mobile Number
                                </td>
                               <td class="value">
                                    <uc1:mobileno id="uctrlMobileNo" runat="server">
                                    </uc1:mobileno></td>
                            </tr>
                            <tr>
                                <td style="padding-top: 10px;">
                                    Email Address
                                </td>
                                <td class="value">
                                    <asp:TextBox runat="server" ID="txtEmailAddress" Width="300px" MaxLength="120"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvEmailAddress" runat="server" ErrorMessage="*"
                                        ControlToValidate="txtEmailAddress" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                            </tr>
                        </table>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="Table1">
                            <tr>
                                <td align="left" width="70%" class="value">
                                    <asp:Label runat="server" ID="lblMessage" CssClass="discount"></asp:Label>
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="False" ShowSummary="False" Height="1px" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <div align="left">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                <tr>
                                    <td align="right" width="70%">
                                        <asp:LinkButton runat="server" ID="lnkOK" Text="Save" OnClick="lnkOK_Click" OnClientClick="return confirm('Are you sure you want to add this One-Time Supplier?');"></asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lnkCancel" CausesValidation="false" Text="Cancel"
                                            OnClientClick="Close();" OnClick="lnkCancel_Click"></asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lnkClose" Text="Close" Visible="false"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <input type="hidden" runat="server" id="hdnCategoryId" name="hdnCategoryId" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
