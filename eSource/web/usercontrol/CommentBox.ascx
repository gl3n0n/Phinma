<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CommentBox.ascx.cs" Inherits="web_usercontrol_CommentBox" %>
<%@ Register Src="commentlist.ascx" TagName="commentlist" TagPrefix="uc2" %>
<%@ Register Src="commentlist_auction.ascx" TagName="commentlist_auction" TagPrefix="uc1" %>
<h2>
    Comments
</h2>
<div align="left" style="padding-left: 3px; text-align: justify;">
    <p>
        <asp:TextBox ID="CommentBox" runat="server" TextMode="MultiLine" Height="132px" Width="180px" MaxLength="200" Font-Names="Arial" />
        <br />
        <asp:CheckBox ID="chkAlowVendorView" runat="server" Checked="false" Text="Show To Vendors" />
    </p>
    <p>
        <asp:Button ID="PostButton" runat="server" Height="21px" Width="103px" OnClick="PostButton_Click" Text="Post Comment" CausesValidation="false" />&nbsp;</p>
</div>
<h2>
    Recent Comments
</h2>
<div style="padding-left: 3px;">    
    <input type="hidden" id="hdnIsPosted" name="hdnIsPosted" runat="server" />    
</div>
<div>
    <uc2:commentlist ID="Commentlist_bid" runat="server" />
    <uc1:commentlist_auction ID="Commentlist_auction1" runat="server" />
</div>
