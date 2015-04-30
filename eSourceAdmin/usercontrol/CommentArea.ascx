<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CommentArea.ascx.cs" Inherits="web_usercontrol_CommentArea" %>
<link rel="stylesheet" type="text/css" href="../css/style.css">

<asp:GridView ID="gvCommentArea" runat="server" AllowPaging="True" PageSize="4" AutoGenerateColumns="false"
    OnPageIndexChanging="gridView_PageIndexChanging" CssClass="pageDetails" BorderWidth="0">
    <HeaderStyle CssClass="pageDetails_th" ForeColor="#FFFFFF" />
    <Columns>
        <asp:TemplateField HeaderText="Recent Comments" ItemStyle-Width="190px" >
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
