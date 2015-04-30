<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CommentBox.ascx.cs" Inherits="web_usercontrol_CommentBox"  %>
<link rel="stylesheet" type="text/css" href="../css/style.css">

<br>
<table border="0" cellpadding="0" cellspacing="0" id="pageDetails">
    <tr>
        <th>
            Comments</th>
    </tr>
    <tr>
        <td>
            <p>
                <asp:TextBox ID="CommentBox" runat="server" TextMode="MultiLine" Height="132px" Width="190px"
                    MaxLength="200" /></p>
            <p>
                <asp:Button ID="PostButton" runat="server" Height="21px" Width="103px" OnClick="PostButton_Click"
                    Text="Post Comment" CausesValidation="false"/></p>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="gvCommentArea" runat="server" AllowPaging="True" PageSize="4" AutoGenerateColumns="false"
                OnPageIndexChanging="gridView_PageIndexChanging" CssClass="pageDetails" BorderWidth="0">
                <HeaderStyle CssClass="pageDetails_th" ForeColor="#FFFFFF" />
                <Columns>
                    <asp:TemplateField HeaderText="Recent Comments" ItemStyle-Width="190px">
                        <ItemTemplate>
                            <p class="date">
                                <asp:Label ID="lblDate" runat="server" Text='<%# Bind("DatePosted1") %>'></asp:Label></p>
                            <p style="word-wrap: break-word">
                                <asp:Label ID="lblComments" runat="server" Text='<%# Bind("Comment") %>'></asp:Label></p>
                            <h4>
                                <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label></h4>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
<input type="hidden" id="hdnIsPosted" name="hdnIsPosted" runat="server" />
