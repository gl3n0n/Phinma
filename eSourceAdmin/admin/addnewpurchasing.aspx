<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addnewpurchasing.aspx.cs"
    Inherits="admin_addNewPurchasing" MaintainScrollPositionOnPostback="true" %>

<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminTopNav" Src="~/usercontrol/admin/adminTopNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Admin_TopNav_User" Src="~/usercontrol/admin/Admin_TopNav_User.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminLeftNavUser" Src="~/usercontrol/admin/AdminLeftNavUser.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminLeftNav" Src="~/usercontrol/admin/AdminLeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/usercontrol/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>.:| Trans-Asia | Add User |:.</title>
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
                                <td id="relatedInfo" style="height: 290px">
                                    <h2>
                                        User Functions</h2>
                                    <EBid:AdminLeftNavUser runat="server" ID="AdminLeftNavUser" />
                                    <p>
                                        &nbsp;</p>
                                </td>
                                <td id="content" style="height: 290px">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page0">
                                        <tr>
                                            <td valign="top">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td id="content0">
                                                            <h1>
                                                                <br>
                                                                Add New Purchasing Officer</h1>
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
                                                        <td>
                                                            <table align="center" border="0" cellpadding="0" cellspacing="0" width="300px" class="itemDetails">
                                                                <tr>
                                                                    <th colspan="2">
                                                                        User information</th>
                                                                </tr>
                                                                <tr class="value">
                                                                    <td style="width: 120px;padding-top: 7px" >
                                                                        Username:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbUserName" runat="server" Width="250px" />
                                                                        <asp:Label runat="server" ID="lblUserError" Visible="false" ForeColor="red" Text="* Username already exists." />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* This is a Required Field"
                                                                            ControlToValidate="tbUserName" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                                                </tr>
                                                                <%--<tr class="evenCells">
                                                                    <td style="width: 120px;padding-top: 7px" >
                                                                        Password:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbPassword" runat="server" Width="250px" MaxLength="25" TextMode="Password" >*</asp:TextBox>
                                                                        <asp:Label runat="server" ID="lblPassError" Visible="false" ForeColor="red" Text="* Password should be at least 8 characters/digits." />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* This is a Required Field"
                                                                            ControlToValidate="tbPassword" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                        <asp:Label ID="lblCheckPassStrength" runat="server" ForeColor="Red" Visible="False"></asp:Label></td>
                                                                </tr>--%>
                                                                <tr class="evenCells">
                                                                    <td style="width: 120px;padding-top: 7px" >
                                                                        Email Address:</td>
                                                                    <td style="height: 46px">
                                                                        <asp:TextBox ID="tbEmail" runat="server" Width="250px" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="* This is a Required Field"
                                                                            ControlToValidate="tbEmail" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="* Invalid Email Address"
                                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="tbEmail" SetFocusOnError="True"></asp:RegularExpressionValidator></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table align="center" border="0" cellpadding="0" cellspacing="0" width="300px" class="itemDetails">
                                                                <tr>
                                                                    <th colspan="2">
                                                                        Purchasing Details</th>
                                                                </tr>
                                                                <tr class="value">
                                                                    <td style="width: 120px;padding-top: 7px" >
                                                                        First Name:</td>
                                                                    <td style="height: 30px">
                                                                        <asp:TextBox ID="tbFirstName" runat="server" Width="250px" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="* This is a Required Field"
                                                                            ControlToValidate="tbFirstName" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                                                                </tr>
                                                                <tr class="evenCells">
                                                                    <td style="width: 120px;padding-top: 7px" >
                                                                        Middle Name:</td>
                                                                    <td style="height: 33px">
                                                                        <asp:TextBox ID="tbMidInitial" runat="server" Width="250px" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="* This is a Required Field"
                                                                            ControlToValidate="tbMidInitial" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                                                                </tr>
                                                                <tr class="value">
                                                                    <td style="width: 120px;padding-top: 7px" >
                                                                        Last Name:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbLastName" runat="server" Width="250px" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="* This is a Required Field"
                                                                            ControlToValidate="tbLastName" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                                                                </tr>
                                                                <tr class="evenCells">
                                                                    <td style="width: 120px;padding-top: 7px" >
                                                                        Department:</td>
                                                                    <td>
                                                                        <asp:RadioButtonList ID="rbDepartments" runat="server" DataSourceID="dsDepartments"
                                                                            DataTextField="DeptName" DataValueField="DeptId" RepeatDirection="Horizontal">
                                                                        </asp:RadioButtonList><asp:SqlDataSource ID="dsDepartments" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                            SelectCommand="s3p_EBid_QueryDepartments" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br>
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                                <tr>
                                                                    <td>
                                                                        <asp:LinkButton ID="lnkSave" runat="server" Text="Save" OnClientClick="return confirm('Add New User?');"
                                                                            OnClick="lnkSave_Click"></asp:LinkButton>
                                                                        <a href="addnewpurchasing.aspx">Cancel</a>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr><td style="height: 20px;"></td></tr>
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
