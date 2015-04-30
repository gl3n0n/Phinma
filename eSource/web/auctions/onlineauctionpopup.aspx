<%@ Page EnableViewState="true" Language="C#" AutoEventWireup="true" CodeFile="onlineauctionpopup.aspx.cs" Inherits="web_onlineAuction_onlineauctionpopup" Theme="default" MaintainScrollPositionOnPostback="true"%>

<%@ Register Src="../usercontrol/AuctionDate.ascx" TagName="AuctionDate" TagPrefix="uc4" %>
<%@ Register Src="../usercontrol/auctionvendor/auctionfooter.ascx" TagName="auctionfooter" TagPrefix="uc3" %>
<%@ Register Src="../usercontrol/AuctionVendor/AuctionVendor_TopNav_Ongoing.ascx" TagName="AuctionVendor_TopNav_Ongoing" TagPrefix="uc2" %>
<%@ Register Src="../usercontrol/Vendor/Vendor_TopNav_Home.ascx" TagName="Vendor_TopNav_Home" TagPrefix="uc1" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
	<link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_oa.css" %>' />
    <script type="text/javascript" src="../include/auctions.js"></script>
    <script type="text/javascript" src="../include/util.js"></script>
    <script type="text/javascript" src="../include/generalJSFunctions.js"></script>
</head>
<body onload='Focus();Flash();DisplayTime();' ononcontextmenu="return false;" onfocus="window.status = ''; return false;" onbeforepaste="return false;" onpaste="return false;"
    ondragstart="return false;">
    <div>
        <form runat="server" name="form1">
        <asp:ScriptManager ID="scriptMngr" runat="server" /> 
            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
                <tr>
                    <td valign="top" style="height: 21px">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <uc4:AuctionDate ID="AuctionDate1" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
                            <tr>
                                <td id="relatedInfo" runat="server">
                                    <table id="vendorPanel" runat="server" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td><h2>My Workspace</h2></td>                                          
                                        </tr>
                                        <tr>
                                            <td align="center" >
                                                    <asp:GridView runat="server" ID="gvBidInput" Width="500px" AutoGenerateColumns="False" DataSourceID="dsAuctionedItemDetails" BorderColor="Gainsboro" BorderWidth="1px" DataKeyNames="AuctionDetailNo,NextBid,IncrementDecrement"
                                                        OnRowCreated="gvBidInput_RowCreated">
                                                        <RowStyle BackColor="White" Font-Names="Arial" Font-Size="11px" />
                                                        <HeaderStyle BackColor="LightSlateGray" Font-Names="Arial" Font-Size="13px" ForeColor="White" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Item(s)" SortExpression="Description">
                                                                <ItemTemplate>
                                                                    &nbsp;<asp:LinkButton ID="lnkBidHistory" runat="server" Text='<%# Eval("Description") %>' ToolTip="Click to view this item's bid history" CommandArgument='<%# GetStringValue(Eval("AuctionDetailNo", "{0}")) + "»" + GetStringValue(Eval("Description", "{0}")) %>'
                                                                        OnCommand="lnkBidHistory_Command" ForeColor='<%# ItemColor(Eval("AuctionDetailNo", "{0}").ToString()) %>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Your Bid(s)">
                                                                <ItemStyle Width="115px" />
                                                                <ItemTemplate>
                                                                    &nbsp;<asp:TextBox ID="txtLeft" runat="server" Width="80px" MaxLength="12"></asp:TextBox>.<asp:TextBox ID="txtRight" runat="server" Width="20px" Text="00" MaxLength="2"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:SqlDataSource ID="dsAuctionedItemDetails" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetAuctionedItemBidDetails" SelectCommandType="StoredProcedure">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="hdnAID" DefaultValue="0" Type="Int32" Name="AuctionRefNo" PropertyName="Value" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>  
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 18px;" align="center">
                                                <asp:Label ID="hdnDetailNo" runat="server" Visible="False"></asp:Label>&nbsp;<asp:Label ID="lblMessage" runat="server" Font-Size="11px" ForeColor="Red"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td align="center" >
                                                <asp:Button ID="btnSubmit" runat="server" Text="Enter Bids" OnClientClick="return confirm('Are you sure you want to submit your bid(s)?');"
                                                    OnClick="btnSubmit_Click" />
                                                <input type="reset" value="Reset" name="B3" onclick="Focus();" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td >
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td >
                                                <h2>
                                                    My Bid History</h2>
                                            </td>
                                        </tr>
                                        <tr id="trHistory" runat="server">
                                            <td align="center" >
                                                <table cellpadding="0" cellspacing="0" border="0" width="95%" style="font-family: Arial; font-size: 11px;">
                                                    <tr height="19px">
                                                        <td colspan="2">
                                                            <asp:Label ID="lblItemName" runat="server" Visible="true" Font-Bold="true" />
                                                            <hr />
                                                            &nbsp;Latest Bid:
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="left">
                                                            <p style="text-align: right; font-weight: bold; font-size: 13px; color: Red;">
                                                                <asp:Literal ID="lblLatestBid" runat="server" Text="0.00"></asp:Literal>
                                                            </p>
                                                            <hr />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" width="75px" colspan="2">
                                                            &nbsp;Previous Bids :</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" valign="top">
                                                            <p style="text-align: right;">
                                                                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="dsAuctionedItemBidHistory">
                                                                    <ItemTemplate>
                                                                        <%# GetLatestBid(Eval("Bid").ToString(), Eval("DateSubmitted").ToString()) %>
                                                                        <br />
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </p>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:SqlDataSource ID="dsAuctionedItemBidHistory" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetAuctionedItemBidHistory" SelectCommandType="StoredProcedure"
                                                    OnSelected="dsAuctionedItemBidHistory_Selected" OnSelecting="dsAuctionedItemBidHistory_Selecting">
                                                    <SelectParameters>
                                                        <asp:SessionParameter Name="Userid" SessionField="UserId" Type="Int32" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            </td>
                                        </tr>
                                        <tr id="trEmptyHistory" runat="server">
                                            <td align="center" >
                                                <asp:Label ID="Label4" runat="server" Text="No bid history for this item." ForeColor="Red" Font-Size="11px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <table id="creatorPanel" runat="server" cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td >
                                                <h2>My Workspace</h2>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" >
                                                <asp:ImageButton ID="btnStop" runat="server" ImageUrl="~/web/themes/images/emergencystop.JPG" ToolTip="Click to stop the auction." OnClientClick="return confirm('Are you sure you want to stop this auction?'); "
                                                    OnClick="btnStop_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;">
                                                <asp:Panel ID="pnlComments" runat="server">
                                                    <br />
                                                    <asp:Label ID="lblcomment" runat="server" Font-Size="11px">Comment</asp:Label><br />
                                                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Width="180px" Height="90px" Font-Names="Arial" MaxLength="999"></asp:TextBox><br />
                                                    <asp:CustomValidator ID="cvValidator" runat="server" ErrorMessage="* comments is required" Font-Size="11px" OnServerValidate="cvValidator_ServerValidate"></asp:CustomValidator>
                                                    <br />
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign="top" id="content" style="padding-right: 10px;">
                                    <br />
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <h1>
                                                    <asp:Label ID="lblUserName" runat="server" Font-Size="14px"></asp:Label></h1>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <h3>
                                                    Auction : &nbsp;<asp:Label ID="lblAuctionTitle" runat="server" Font-Size="12px"></asp:Label></h3>
                                               <asp:Timer ID="tmrAutoReload" runat="server" OnTick="tmrAutoReload_Tick" Interval="10000" />
                                                <asp:Timer ID="tmrRefreshTime" runat="server" Interval="2000" OnTick="tmrRefreshTime_Tick"></asp:Timer>     
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                    <asp:DetailsView ID="dvAuctionDetails" runat="server" Height="50px" Width="100%" AutoGenerateRows="False" CssClass="itemDetails" DataSourceID="dsAuctionItemDetails2" HeaderText="&nbsp;" HeaderStyle-BackColor="#10659E">
                                                        <Fields>
                                                            <asp:TemplateField HeaderText="Auction Type" SortExpression="AuctionType">
                                                                <HeaderStyle Width="130px" />
                                                                <ItemStyle BackColor="White" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("AuctionType") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Start Time" SortExpression="StartTime">
                                                                <HeaderStyle Width="130px" />
                                                                <ItemStyle BackColor="White" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Format(Eval("StartTime").ToString()) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="End Time" SortExpression="EndTime">
                                                                <ItemStyle BackColor="White" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Format(Eval("EndTime").ToString()) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Currency" SortExpression="Currency">
                                                                <ItemStyle BackColor="White" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCurrency" runat="server" Text='<%# Eval("Currency") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Duration" SortExpression="Duration">
                                                                <ItemStyle BackColor="White" />
                                                                <ItemTemplate>
                                                                    <font style="font-weight: bold;">
                                                                        <asp:label id="lblDuration" runat="server" Text='<%# Eval("Duration") %>' Font-Size="10">
                                                                        </asp:label>
                                                                    </font>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Time Elapsed" SortExpression="TimeElapsed">
                                                                <ItemStyle BackColor="White" />
                                                                <ItemTemplate>
                                                                    <font style="color: blue; font-weight: bold;">
                                                                        <asp:label id="lblElapsed" runat="server" Text='<%# Eval("TimeElapsed") %>' Font-Size="10" >
                                                                        </asp:label>
                                                                    </font>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Time Left" SortExpression="TimeLeft">
                                                                <ItemStyle BackColor="White" />
                                                                <ItemTemplate>
                                                                    <font style="color: Red; font-weight: bold;">
                                                                        <asp:label id="lblRemaining" runat="server" Text='<%# Eval("TimeLeft") %>' Font-Size="10">
                                                                        </asp:label>
                                                                    </font>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Fields>
                                                        <HeaderStyle BackColor="#10659E" />
                                                    </asp:DetailsView>
                                                    <asp:SqlDataSource ID="dsAuctionItemDetails2" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetAuctionedItemDetails" SelectCommandType="StoredProcedure"
                                                        OnSelected="dsAuctionItemDetails2_Selected">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="hdnAID" DefaultValue="0" Type="Int32" Name="AuctionRefNo" PropertyName="Value" />
                                                            <asp:SessionParameter DefaultValue="0" Name="UserId" SessionField="UserId" Type="Int32" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="tmrRefreshTime" EventName="tick" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <h3>
                                                    Auctioned Item(s)</h3>                                        
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:GridView Width="100%" runat="server" ID="gvAuctionEvent" CssClass="itemDetails" DataSourceID="dsAuctionItemDetails" OnRowCreated="gvAuctionEvent_RowCreated" OnRowDataBound="gvAuctionEvent_RowDataBound">
                                                        </asp:GridView>
                                                        <asp:SqlDataSource ID="dsAuctionItemDetails" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetAuctionItemsComparison" SelectCommandType="StoredProcedure">
                                                            <SelectParameters>
                                                                <asp:ControlParameter ControlID="hdnAID" DefaultValue="0" Type="Int32" Name="AuctionRefNo" PropertyName="Value" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="tmrAutoReload" EventName="tick" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <h3>
                                                    Bid Summary</h3>
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <asp:GridView Width="100%" runat="server" ID="gvAuctionLastBids" CssClass="itemDetails" DataSourceID="dsAuctionLatestBids" OnRowCreated="gvAuctionLastBids_RowCreated" AllowSorting="True">
                                                            <HeaderStyle ForeColor="White" />
                                                        </asp:GridView>
                                                        <asp:SqlDataSource ID="dsAuctionLatestBids" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetAuctionLatestBids" SelectCommandType="StoredProcedure">
                                                            <SelectParameters>
                                                                <asp:ControlParameter ControlID="hdnAID" DefaultValue="0" Type="Int32" Name="AuctionRefNo" PropertyName="Value" />
                                                                <asp:SessionParameter DefaultValue="2" Name="UserType" SessionField="UserType" Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                        <br />
                                                        <asp:GridView ID="gvAuctionHighestLowestBids" runat="server" Width="100%" CssClass="itemDetails" AllowSorting="True" DataSourceID="dsHighesLowestBids">
                                                            <HeaderStyle ForeColor="White" />
                                                            <RowStyle BackColor="White" />
                                                        </asp:GridView>
                                                        <asp:SqlDataSource ID="dsHighesLowestBids" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetHighestOrLowestBidAndBidder" SelectCommandType="StoredProcedure">
                                                            <SelectParameters>
                                                                <asp:ControlParameter ControlID="hdnAID" DefaultValue="0" Type="Int32" Name="AuctionRefNo" PropertyName="Value" />
                                                                <asp:SessionParameter DefaultValue="2" Name="UserType" SessionField="UserType" Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="tmrAutoReload" EventName="tick" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 14px">
                                                <p>
                                                    &nbsp;</p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="actions">
                                                <asp:LinkButton ID="lnkbreload" runat="server" Text='Reload' Width="100px" Visible="false"></asp:LinkButton>
                                                <br />
                                                <asp:CheckBox ID="chkAutoReload" Visible="false" EnableViewState="true" AutoPostBack="true" TextAlign="Left" ToolTip="Check to auto reload the page every 10 seconds." runat="server" Text="Auto Reload" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <p>
                                                    &nbsp;</p>
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
                        <uc3:auctionfooter ID="Auctionfooter1" runat="server" />
                        <asp:HiddenField ID="hdnAID" runat="server" Value="0" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
