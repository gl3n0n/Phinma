<%@ Control Language="C#" AutoEventWireup="true" CodeFile="news_announcements_nav.ascx.cs"
    Inherits="web_usercontrol_news_announcements_nav" %>
<div>
    <h2>
        Top 10 Announcements</h2>
    <p style="padding: 0px; margin-left:10px " >
        <asp:DataList ID="dlAnnouncements" runat="server" DataKeyField="ID" DataSourceID="dsAnnouncements" OnItemCommand="dlAnnouncements_ItemCommand" ItemStyle-BackColor="#96c545 ">
            <ItemTemplate>                
                <asp:LinkButton ID="lnkAnnouncement" runat="server" Text='<%# Limit_title(Eval("Title", "» {0}")) %>'
                    ForeColor="white" Font-Size="11px" CommandArgument='<%# Bind("ID") %>' CommandName="ViewAnnouncementDetail" 
                    CausesValidation="false" ToolTip='<%# Bind("Title", "» {0}") %>' Width="253px" style="border-bottom:solid 2px #fff; text-decoration:none"></asp:LinkButton>
            </ItemTemplate>
        </asp:DataList><asp:SqlDataSource ID="dsAnnouncements" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
            SelectCommand="sp_GetTop10Announcement" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
    </p>
    <h2>
        Top 10 News</h2>
    <p style="padding: 0px; margin-left:10px " >
        <asp:DataList ID="dlNews" runat="server" DataKeyField="ID" DataSourceID="dsNews" OnItemCommand="dlNews_ItemCommand" ItemStyle-BackColor="#96c545 ">
            <ItemTemplate>
                <asp:LinkButton ID="lnkNews" runat="server" Text='<%# Limit_title(Eval("Title", "» {0}")) %>'
                    ForeColor="white" Font-Size="11px" CommandArgument='<%# Bind("ID") %>' CommandName="ViewNewsDetail" 
                    CausesValidation="false" ToolTip='<%# Bind("Title", "» {0}") %>' Width="253px" style="border-bottom:solid 2px #fff; text-decoration:none"></asp:LinkButton>
            </ItemTemplate>
        </asp:DataList><asp:SqlDataSource ID="dsNews" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
            SelectCommand="sp_GetTop10News" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
    </p>
</div>