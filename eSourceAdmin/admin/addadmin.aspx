<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addadmin.aspx.cs" Inherits="admin_addadmin" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminTopNav" Src="~/usercontrol/admin/adminTopNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Admin_TopNav_User" Src="~/usercontrol/admin/Admin_TopNav_User.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminLeftNavUser" Src="~/usercontrol/admin/AdminLeftNavUser.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminLeftNav" Src="~/usercontrol/admin/AdminLeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/usercontrol/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>.:| Trans-Asia | Add Administrator |:.</title>
    <meta http-equiv="Content-Language" content="en-us">
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/style_ua.css" />
    <script type="text/javascript" src="../include/util.js"></script>
</head>
<body onload="SetStatus();">
    <div align="left">
        <form id="Form1" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
                <tr>
                    <td valign="top" height="137px">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <div align="left" id="masthead">
                                        <EBid:AdminTopNav runat="server" ID="GlobalLinksNav" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <EBid:Admin_TopNav_User runat="server" ID="Admin_TopNav_User" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <EBid:TopDate runat="server" ID="TopDate" />
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
                                        User Functions</h2>
                                    <EBid:AdminLeftNavUser runat="server" ID="AdminLeftNavUser" />
                                    <p>
                                        &nbsp;</p>
                                </td>
                                <td id="content">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page0">
                                        <tr>
                                            <td valign="top">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td id="Td1">
                                                            <h1>
                                                                <br />
                                                                <asp:Label ID="lblTitle" runat="server" Text="Add New Administrator"></asp:Label></h1>
                                                        </td>
                                                    </tr>
                                                    <tr align="right">
                                                        <td valign="top" height="30px">                                                            
                                                            <asp:DropDownList ID="ddlUserTypes" runat="server" DataSourceID="dsUserTypes" DataTextField="UserType"
                                                                DataValueField="UserTypeId" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlUserTypes_SelectedIndexChanged">
                                                            </asp:DropDownList>&nbsp;
                                                            <asp:SqlDataSource ID="dsUserTypes" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                SelectCommand="s3p_EBid_UserTypes" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td id="content0">
                                                                <table align="center" border="0" cellpadding="0" cellspacing="0" width="300px" id="itemDetails">
                                                                <tr>
                                                                    <th colspan="2">
                                                                        User information</th>
                                                                </tr>
                                                                <tr class="value">
                                                                    <td style="width: 19%">
                                                                        User Name:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbUserName" runat="server" Width="35%" MaxLength="50" />                                                                        
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbUserName" EnableClientScript="False" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                                                        <asp:Label ID="lblUserError" runat="server" ForeColor="red" Visible="false"></asp:Label></td>
                                                                </tr>
                                                                <tr class="value">
                                                                    <td style="width: 19%; height: 30px;">
                                                                        Email Address:</td>
                                                                    <td style="height: 30px">
                                                                        <asp:TextBox ID="tbEmail" runat="server" Width="35%" MaxLength="100" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbEmail"
                                                                            EnableClientScript="False" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbEmail"
                                                                            EnableClientScript="False" ErrorMessage="* Invalid Email Address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
                                                                </tr>
                                                            </table>
                                                            <table align="center" border="0" cellpadding="0" cellspacing="0" width="300px" id="itemDetails1">
                                                                <tr>
                                                                    <th colspan="2">
                                                                        &nbsp;Administrator Details</th>
                                                                </tr>
                                                                <tr class="value">
                                                                    <td style="width: 19%; height: 30px;">
                                                                        First Name:</td>
                                                                    <td style="height: 30px">
                                                                        <asp:TextBox ID="tbFirstName" runat="server" Width="35%" MaxLength="70" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" EnableClientScript="False"
                                                                            ErrorMessage="* Required" ControlToValidate="tbFirstName" Display="Dynamic"></asp:RequiredFieldValidator></td>
                                                                </tr>
                                                                <tr class="evenCells">
                                                                    <td style="width: 19%; height: 30px;">
                                                                        Middle Name:</td>
                                                                    <td style="height: 30px">
                                                                        <asp:TextBox ID="tbMidInitial" runat="server" Width="35%" MaxLength="50" />
                                                                    </td>
                                                                </tr>
                                                                <tr class="value">
                                                                    <td style="width: 19%; height: 30px;">
                                                                        Last Name:</td>
                                                                    <td style="height: 30px">
                                                                        <asp:TextBox ID="tbLastName" runat="server" Width="35%" MaxLength="70" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" EnableClientScript="False"
                                                                            ErrorMessage="* Required" ControlToValidate="tbLastName"></asp:RequiredFieldValidator></td>
                                                                </tr>
                                                              </table>
                                                            <br />
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                                <tr>
                                                                    <td>
                                                                        <asp:LinkButton ID="lnkSave" runat="server" Text="Save" OnClientClick="return confirm('Add New Administrator?');" OnClick="lnkSave_Click" ></asp:LinkButton>
                                                                        <a href="addboc.aspx">Cancel</a>
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
                        <EBid:Footer runat="server" ID="Footer1" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>