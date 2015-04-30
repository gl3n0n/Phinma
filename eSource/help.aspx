<%@ Page Language="C#" MasterPageFile="~/publicmaster.master" AutoEventWireup="true" CodeFile="help.aspx.cs" Inherits="help" Title=".:| Trans-Asia  eSourcing System | Help |:." %>

<%@ Register Assembly="KeySortDropDownList" Namespace="KeySortDropDownList.Thycotic.Web.UI.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%" visible="false">
        <tr> <td><font size="20" color="black">&nbsp;</td></tr>
        <tr> <td><font size="20" color="black">&nbsp;</td></tr>
        <tr>
            <td>
                <p>
                <font size="10" color="black"><center><b><asp:Label ID="CtrDisplay" runat="server" Text="UNDER&nbsp;&nbsp;CONSTRUCTION" Visible="true"></asp:Label></b></center>
                </p>
            </td>
        </tr>
    </table>
    <%--<table cellpadding="0" cellspacing="0" border="0" width="100%" visible="false">
        <tr>
            <td>
                <p>
                    &nbsp; &nbsp;&nbsp;
                    <asp:Label ID="lblClientInfo" runat="server" Text="" Visible="false"></asp:Label>
                </p>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <p style="padding-left: 15px;">
                    Recipients:
                    <br />
                    &nbsp;<asp:TextBox ID="txtRecipients" runat="server" Width="200px" Visible="false"></asp:TextBox><br />
                    Message:
                    <br />
                    &nbsp;<asp:TextBox ID="txtMessage" runat="server" Width="200px" TextMode="MultiLine" Rows="4" Visible="false" Font-Names="Arial" Font-Size="11px"></asp:TextBox><br />
                    <asp:Button ID="btnSend" runat="server" OnClick="btnSend_Click" Text="SEND" Visible="false" />
                </p>
                <br />
            </td>
        </tr>
        <tr>
            <td valign="top">
                <p style="padding-left: 15px;">
                    Password:
                    <br />
                    &nbsp;<asp:TextBox ID="txtPassword" runat="server" Visible="false" Width="200px"></asp:TextBox>
                    <asp:Label ID="lblIsStrong" runat="server" Visible="false"></asp:Label>
                    <br />
                    <asp:Button ID="btnCheckPassword" runat="server" OnClick="btnCheckPassword_Click" Visible="false" Text="CHECK PASSWORD" />
                </p>
            </td>
        </tr>
        <tr>
            <td>
                <br />
                &nbsp; &nbsp;&nbsp;
                <cc1:KeySortDropDownList ID="KeySortDropDownList1" runat="server" CaseSensitiveKeySort="False" 
                    DataSourceID="dsProducts" DataTextField="ItemName1" DataValueField="SKU" Width="456px"
                    Height="47px">
                </cc1:KeySortDropDownList>
                <asp:SqlDataSource ID="dsProducts" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="SELECT DISTINCT SKU, SKU + ' - ' + ItemName AS ItemName1 FROM tblProducts ORDER BY ItemName1">
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>--%>
</asp:Content>
