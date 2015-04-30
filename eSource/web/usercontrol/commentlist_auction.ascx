<%@ Control Language="C#" AutoEventWireup="true" CodeFile="commentlist_auction.ascx.cs" Inherits="web_usercontrol_commentlist_auction" %>
<div align="left" style="padding-left: 3px; text-align: justify;">
    <asp:DataList ID="dlComments" runat="server" Width="98%">
        <ItemStyle Font-Names="Arial" Font-Size="11px" />
        <SeparatorStyle HorizontalAlign="Center" />
        <FooterStyle HorizontalAlign="Center" />
        <ItemTemplate>
            <asp:Label ID="Label3" runat="server" Text="Date Posted : " ForeColor="DimGray"></asp:Label>
            <asp:Label ID="DatePostedLabel" runat="server" Text='<%# Eval("DatePosted") %>'></asp:Label><br />
            <asp:Label ID="Label2" runat="server" Text="Author : " ForeColor="DimGray"></asp:Label>
            <asp:Label ID="AuthorLabel" runat="server" Text='<%# Eval("Author") %>'></asp:Label><br />
            <asp:Label ID="Label1" runat="server" Text="Comment : " ForeColor="DimGray"></asp:Label><br />
            &nbsp;&nbsp;<asp:Label ID="CommentLabel" runat="server" Text='<%# Eval("Comment") %>'></asp:Label>
            <br />
            <br />
        </ItemTemplate>
        <SeparatorTemplate>
            - - -
        </SeparatorTemplate>
        <FooterTemplate>
            - - - - -
        </FooterTemplate>
    </asp:DataList>    
</div>
<div align="center" style="width: 100%; font-family: arial; font-size: 11px;">
    <table width="100%">
        <tr>
            <td colspan="3" align="center">
                <asp:Label ID="lblCounts" Font-Names="Arial" Font-Size="11px" runat="server" />
            </td>
        </tr>
        <tr id="trPagers" runat="server">
            <td>&nbsp;</td>
            <td valign="bottom" align="center">
                <asp:ImageButton ID="btnFirstPage" ImageUrl="~/web/themes/images/pager/firstpage.gif" BorderWidth="0"
                    runat="server" OnClick="btnFirstPage_Click" CausesValidation="false" ToolTip="First Page" />            
                <asp:ImageButton ID="btnPreviousPage" ImageUrl="~/web/themes/images/pager/prevpage.gif"
                    BorderWidth="0" runat="server" OnClick="btnPreviousPage_Click" CausesValidation="false" ToolTip="Previous Page" />            
                <asp:ImageButton ID="btnNextPage" ImageUrl="~/web/themes/images/pager/nextpage.gif" BorderWidth="0"
                    runat="server" OnClick="btnNextPage_Click" CausesValidation="false" ToolTip="Next Page" />            
                <asp:ImageButton ID="btnLastPage" ImageUrl="~/web/themes/images/pager/lastpage.gif" BorderWidth="0"
                    runat="server" OnClick="btnLastPage_Click" CausesValidation="false" ToolTip="Last Page" />
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
</div>
<div>
    <asp:Label ID="lblRecordCount" Visible="False" runat="server" />
    <asp:Label ID="lblCurrentIndex" Visible="False" Text="0" runat="server" />
    <asp:Label ID="lblPageSize" Visible="False" Text="5" runat="server" />
</div>
<br />

