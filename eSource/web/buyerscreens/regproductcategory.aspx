<%@ Page Language="C#" AutoEventWireup="true" CodeFile="regproductcategory.aspx.cs" Inherits="web_buyerscreens_RegProductCategory" %>

<%@ Register TagPrefix="EBid" TagName="TopNavProducts" Src="~/WEB/usercontrol/Buyer/TopNavProducts.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNav2" Src="~/WEB/usercontrol/Buyer/TopNav2.ascx" %>
<%@ Register TagPrefix="EBid" TagName="LeftNavProducts" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_buyer.css" %>' />

    <script language="javascript" type="text/javascript" src="../include/customValidation.js"></script>

</head>
<body style="height: 100%;">
    <div align="left" height="100%">
        <form id="Form1" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
                <tr>
                    <td valign="top" width="100%">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <div align="left" id="masthead">
                                        <EBid:GlobalLinksNav runat="server" ID="GlobalLinksNav" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <EBid:TopNav2 runat="server" ID="TopNav2_1" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td height="100%">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
                            <tr>
                                <td id="relatedInfo">
                                    <div align="left">
                                        <EBid:LeftNavProducts runat="server" ID="LeftNavProducts" />
                                    </div>
                                    <br />
                                </td>
                                <td id="content">
                                    <h1>
                                        <br />
                                        Add Category</h1>
                                    <div align="left">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails">
                                            <tr>
                                                <th colspan="2">
                                                    Details</th>
                                                
                                            </tr>
                                            <tr>
                                                <td style="width: 130px">
                                                    Category Id:</td>
                                                <td class="value">
                                                    <asp:TextBox runat="server" ID="txtCategoryId" MaxLength="7"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvCategoryId" runat="server" ControlToValidate="txtCategoryId" Display="None" ErrorMessage="Category Id is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    <asp:Label ID="lblCategoryId" runat="server" CssClass="messagelabels" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Category Name:</td>
                                                <td class="value">
                                                    <asp:TextBox runat="server" ID="txtCategoryName" MaxLength="50" Width="200px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvCategoryName" runat="server" ControlToValidate="txtCategoryName" Display="None" ErrorMessage="Category Name is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    <asp:Label ID="lblCategoryName" runat="server" CssClass="messagelabels" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Category Description:</td>
                                                <td class="value">
                                                    <asp:TextBox runat="server" ID="txtCategoryDesc" Width="200px" Rows="5" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </div>
                                    <div align="left">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                            <tr>
                                                <td align="left" width="75%" style="height: 34px">
                                                    <asp:Label ID="lblMessage" runat="server" CssClass="messagelabels"></asp:Label>
                                                </td>
                                                <td align="right" width="25%" style="height: 34px">
                                                    <asp:LinkButton runat="server" ID="btnSave" Width="100px" OnClick="btnSave_Click">Save</asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="btnClear" Width="100px" CausesValidation="False" OnClick="btnClear_Click">Clear</asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td id="footer">
                        <EBid:Footer runat="server" ID="Footer1" />
                        <asp:CustomValidator ID="cuvValidate" runat="server" ClientValidationFunction="ValidatorIndividualAlert(this, args);" Display="None"></asp:CustomValidator>
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
