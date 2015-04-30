<%@ Page Language="C#" AutoEventWireup="true" CodeFile="renegotiatedbiditemdetails.aspx.cs" Inherits="web_buyerscreens_renegotiatedbiditemdetails" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="../usercontrol/bids/bidtender_attachments.ascx" TagName="bidtender_attachments" TagPrefix="uc5" %>

<%@ Register Src="../usercontrol/bids/biddetails_attachments.ascx" TagName="biddetails_attachments" TagPrefix="uc4" %>

<%@ Register Src="../usercontrol/commentlist_tender.ascx" TagName="commentlist_tender" TagPrefix="uc3" %>
<%@ Reference Control="~/web/usercontrol/commentlist_tender.ascx" %>
<%@ Register Src="../usercontrol/bids/bidtenderdetails.ascx" TagName="bidtenderdetails" TagPrefix="uc1" %>
<%@ Register Src="../usercontrol/bids/biditemdetails.ascx" TagName="biditemdetails" TagPrefix="uc2" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/web/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="LeftNav" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNavBids" Src="~/WEB/usercontrol/Buyer/TopNavBids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNav2" Src="~/WEB/usercontrol/Buyer/TopNav2.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_buyer.css" %>' />
</head>
<body style="height: 100%;">
    <div align="left">
        <form id="Form1" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
                <tr>
                    <td valign="top" height="137px">
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
                                    <EBid:TopNav2 runat="server" ID="TopNav2" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
                            <tr>
                                <td id="relatedInfo">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td id="Td1">
                                                <div align="left">
                                                    <EBid:LeftNav runat="server" ID="LeftNav" />
                                                </div>
                                                <h2>
                                                    Comments</h2>
                                                <div align="center">
                                                    <div align="left" style="padding-left: 3px; text-align: justify;">
                                                        <asp:DataList ID="dlComments" runat="server" Width="100%">
                                                            <ItemStyle Font-Names="Arial" Font-Size="11px" />
                                                            <SeparatorStyle HorizontalAlign="Center" />
                                                            <FooterStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label3" runat="server" Text="Date Posted : " ForeColor="DimGray"></asp:Label>
                                                                <asp:Label ID="DatePostedLabel" runat="server" Text='<%# Eval("DatePosted") %>'></asp:Label><br />
                                                                <asp:Label ID="Label2" runat="server" Text="Author : " ForeColor="DimGray"></asp:Label>
                                                                <asp:Label ID="AuthorLabel" runat="server" Text='<%# Eval("FullName") %>'></asp:Label><br />
                                                                <asp:Label ID="Label1" runat="server" Text="Comment : " ForeColor="DimGray"></asp:Label><br />
                                                                &nbsp;&nbsp;<asp:Label ID="CommentLabel" runat="server" Text='<%# Eval("Comment") %>'></asp:Label>
                                                                <br />
                                                                <br />
                                                            </ItemTemplate>
                                                            <SeparatorTemplate>
                                                                - - -</SeparatorTemplate>
                                                            <FooterTemplate>
                                                                - - - - -</FooterTemplate>
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
                                                                <td>&nbsp;
                                                                    </td>
                                                                <td valign="bottom" align="center">
                                                                    <asp:ImageButton ID="btnFirstPage" ImageUrl="~/web/themes/images/pager/firstpage.gif" BorderWidth="0" runat="server" OnClick="btnFirstPage_Click" CausesValidation="false" ToolTip="First Page" />
                                                                    <asp:ImageButton ID="btnPreviousPage" ImageUrl="~/web/themes/images/pager/prevpage.gif" BorderWidth="0" runat="server" OnClick="btnPreviousPage_Click" CausesValidation="false" ToolTip="Previous Page" />
                                                                    <asp:ImageButton ID="btnNextPage" ImageUrl="~/web/themes/images/pager/nextpage.gif" BorderWidth="0" runat="server" OnClick="btnNextPage_Click" CausesValidation="false" ToolTip="Next Page" />
                                                                    <asp:ImageButton ID="btnLastPage" ImageUrl="~/web/themes/images/pager/lastpage.gif" BorderWidth="0" runat="server" OnClick="btnLastPage_Click" CausesValidation="false" ToolTip="Last Page" />
                                                                </td>
                                                                <td>&nbsp;
                                                                    </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lblRecordCount" Visible="False" runat="server" />
                                                        <asp:Label ID="lblCurrentIndex" Visible="False" Text="0" runat="server" />
                                                        <asp:Label ID="lblPageSize" Visible="False" Text="5" runat="server" />
                                                    </div>
                                                    <br />
                                                </div>
                                                <p>&nbsp;
                                                    </p>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td id="content" style="width: 100%">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table5">
                                        <tr>
                                            <td id="content0">
                                                <h1>
                                                    <br />
                                                    Clarified Bid Item Details</h1>
                                                <p>
                                                    These are the clarified tenders for this bid item.
                                                    <br />
                                                    <br />
                                                </p>
                                                <div align="left">
                                                    <p>
                                                        <%--&nbsp;<asp:LinkButton ID="lnkWithdraw" runat="server" Visible="false" Font-Bold="true" ToolTip="Click to withdraw this bid item." OnClientClick="return confirm('Are you sure you want to withdraw this bid item?');" OnClick="lnkWithdraw_Click">Withdraw Bid Item</asp:LinkButton>
                                                        --%>
                                                        <uc2:biditemdetails ID="ctrlBidItemDetails" runat="server" />
                                                    </p>
                                                    <asp:Panel ID="pnlFileUpload" runat="server" Height="50px" Width="100%" Visible="false">
                                                       <asp:GridView ID="gvFileAttachment" runat="server" CssClass="itemDetails_1" AutoGenerateColumns="false" OnRowCommand="gvFileAttachment_RowCommand" ShowFooter="true">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="&nbsp;File Attachment">
                                                                <ItemStyle CssClass="valueGridItem" Width="100%" />
                                                                <HeaderStyle CssClass="itemDetails_th" />
                                                                <ItemTemplate>
                                                                    <asp:Literal ID="litAttach" runat="server" Text="&nbsp;" Visible='<%# IsAttached(Eval("Attached").ToString()) %>' />
                                                                    <asp:Image ID="imgAttach" runat="server" ImageUrl="~/web/themes/images/paperclip.gif" Width="10px" Height="10px" Visible='<%# IsAttached(Eval("Attached").ToString()) %>' />
                                                                    <asp:LinkButton ID="lnkRemoveAttachment" runat="server" ForeColor="red" Font-Bold="true" CommandName="Remove" CommandArgument='<% #Bind("ID")%>' Visible='<%# IsRemovable(Eval("Attached").ToString(), Eval("IsDetachable").ToString()) %>' CausesValidation="false">Remove</asp:LinkButton>
                                                                    <asp:Label ID="lblColon" runat="server" Text=":" Visible='<%# IsRemovable(Eval("Attached").ToString(), Eval("IsDetachable").ToString()) %>' ></asp:Label>
                                                                    <asp:LinkButton ID="lnkFile" runat="server" CommandName="Download" CommandArgument='<%#Bind("FileAttachment") %>' Text='<% #Bind("Original", "{0}")%>' Visible='<%# IsAttached(Eval("Attached").ToString()) %>' CausesValidation="false"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p style="color: Red;">
                                                                        &nbsp;<asp:FileUpload ID="fileUpload" runat="server" Width="330px" /><br />
                                                                        &nbsp;&nbsp;<asp:LinkButton ID="lnkAttach" runat="server" CausesValidation="false" CommandName="Attach">Add To Attachments List</asp:LinkButton>
                                                                        <asp:Literal ID="addAttachmentMsg" runat="server"></asp:Literal>
                                                                    </p>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                       </asp:GridView>
                                                       <asp:SqlDataSource ID="dsAttachments" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetBidEventFileAttachments" SelectCommandType="StoredProcedure">
                                                            <SelectParameters>
                                                                <asp:SessionParameter Name="BidRefNo" SessionField="BidRefNo" Type="Int32" />
                                                            </SelectParameters>
                                                       </asp:SqlDataSource>
                                                    </asp:Panel>
                                                    <p>
                                                        <asp:GridView ID="gvBidItemTenders" runat="server" Width="100%" AutoGenerateColumns="False" SkinID="AuctionedItems" DataKeyNames="BidTenderNo" DataSourceID="dsBidItemTenders" OnRowCommand="gvBidItemTenders_RowCommand"
                                                            OnSelectedIndexChanged="gvBidItemTenders_SelectedIndexChanged" OnDataBound="gvBidItemTenders_DataBound">
                                                            <SelectedRowStyle BackColor="#50A4D1" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Tender No." InsertVisible="False" SortExpression="BidTenderNo">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle Width="80px" />
                                                                    <ItemTemplate>
                                                                        &nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Bind("BidTenderNo") %>'></asp:Label>
                                                                        <asp:HiddenField ID="hdnVendorId" runat="server" Value='<%# Bind("VendorId") %>'/>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Bidder" SortExpression="VendorName">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("VendorName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Bid Price" SortExpression="BidPrice">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("BidPrice", "{0:#,##0.00}") %>' Visible='<%# showBidPrice(Eval("RenegotiationStatus"), Eval("RenegotiationDeadline")) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Date Submitted" SortExpression="DateSubmitted">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Center" Width="180px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("DateSubmitted", "{0:D}<br />{0:T}") %>'></asp:Label>                                                                        
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Remarks" SortExpression="Remarks">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label8" runat="server" Text='<%# Eval("Remarks").ToString().Trim().Length > 0 ? Eval("Remarks").ToString() : "No Remarks" %>' ForeColor='<%# Eval("Remarks").ToString().Trim().Length > 0 ? System.Drawing.Color.Black : System.Drawing.Color.Gray %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemStyle HorizontalAlign="Center" Width="100px"/>
                                                                    <ItemTemplate>
                                                                        <asp:Panel runat="server" ID="pnlLinks">
                                                                            <asp:LinkButton ID="lnkViewDetails" runat="server" ToolTip="Click to view details and comments of this bid tender." CausesValidation="False" CommandName="Select" CommandArgument='<%# Bind("BidTenRef") %>' Width="95%" Enabled='<%# showBidPrice(Eval("RenegotiationStatus"), Eval("RenegotiationDeadline")) %>'>View Details</asp:LinkButton>
                                                                            <br />
                                                                            <asp:LinkButton ID="lnkEndorse" runat="server" Text="Re-Endorse" ToolTip="Click to re-endorse this bid tender." CommandArgument='<%# Bind("BidTenderNo") %>' CommandName="Re-endorse" Visible='<%# showReEndorseButton(Eval("RenegotiationStatus"), Eval("RenegotiationDeadline")) %>'></asp:LinkButton>
                                                                            <asp:LinkButton ID="lnkRenegotiate" runat="server" Text="Renegotiate" ToolTip="Click to renegotiate this bid tender." CommandArgument='<%# Bind("BidTenderNo") %>' CommandName="Renegotiate" Visible='<%# showRenegotiateButton(Eval("RenegotiationStatus"), Eval("RenegotiationDeadline")) %>'></asp:LinkButton>
                                                                            <asp:Label ID="lblSep" runat="server" Text="|" Visible='<%# showRenegotiateButton(Eval("RenegotiationStatus"), Eval("RenegotiationDeadline")) %>'></asp:Label>
                                                                            <asp:LinkButton ID="lnkClarify" runat="server" Text="Clarify" ToolTip="Click to clarify this bid tender." CommandArgument='<%# Bind("BidTenderNo") %>' CommandName="Clarify" Visible='<%# showRenegotiateButton(Eval("RenegotiationStatus"), Eval("RenegotiationDeadline")) %>'></asp:LinkButton>
                                                                            <asp:Label ID="lblStatus" runat="server" Text="<br />Re-Endorsed" Visible='<%# showReEndorsedLabel(Eval("RenegotiationStatus")) %>'></asp:Label>
                                                                            <asp:Label ID="lblStatus2" runat="server" Text="<br />Under Clarification" Visible='<%# showUnderRenegotiationLabel(Eval("RenegotiationStatus")) %>'></asp:Label>
                                                                        </asp:Panel>
                                                                        <asp:Panel runat="server" ID="pnlComments" Visible="false">
                                                                            Comments
                                                                            <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Font-Names="Arial" Width="100%" Height="45px"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfvComments" runat="server" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Comments Required.<br />" ControlToValidate="txtComment"></asp:RequiredFieldValidator>
                                                                            <asp:LinkButton ID="lnkOK" runat="server" CommandName="ContinueEndorsement" CommandArgument='<%# Bind("BidTenderNo") %>'>OK</asp:LinkButton>&nbsp;&nbsp;
                                                                            <asp:LinkButton ID="lnkCancel" runat="server" CommandName="CancelEndorsement" CausesValidation="false">Cancel</asp:LinkButton>
                                                                            <br /><br />
                                                                        </asp:Panel>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:SqlDataSource ID="dsBidItemTenders" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetBidItemTenders" SelectCommandType="StoredProcedure">
                                                            <SelectParameters>
                                                                <asp:SessionParameter DefaultValue="0" Name="BidDetailNo" SessionField="BidDetailNo" Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </p>
                                                    <center>
                                                        <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="red" Font-Size="11px"></asp:Label></center>
                                                    <p>
                                                        <asp:DetailsView ID="dvTenderDetails" runat="server" Width="100%" AutoGenerateRows="False" DataKeyNames="BidTenderNo" DataSourceID="dsBidItemTenders" SkinID="BidDetails" visible="false">
                                                            <HeaderTemplate>
                                                                &nbsp;Bid Tender Details
                                                            </HeaderTemplate>
                                                            <Fields>
                                                                <asp:TemplateField HeaderText="Tender No." InsertVisible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("BidTenderNo") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Bidder">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("VendorName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Amount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("Amount", "{0:#,##0.00}") %>' ></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Discount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("Discount", "{0:#,##0.00}") %>' ></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delivery Cost">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("DeliveryCost", "{0:#,##0.00}") %>' ></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Bid Price">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label2" runat="server" Font-Bold="true" Text='<%# Bind("BidPrice", "{0:#,##0.00}") %>' ></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Remarks">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label8" runat="server" Text='<%# Eval("Remarks").ToString().Trim().Length > 0 ? Eval("Remarks").ToString() : "No Remarks" %>' ForeColor='<%# Eval("Remarks").ToString().Trim().Length > 0 ? System.Drawing.Color.Black : System.Drawing.Color.Gray %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Date Submitted">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("DateSubmitted", "{0:D}") %>'></asp:Label>,
                                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("DateSubmitted", "{0:T}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Fields>
                                                        </asp:DetailsView>
                                                        <asp:Panel ID="pnlTenderAttachments" runat="server" Height="50px" Width="100%" Visible="false">
                                                            <uc5:bidtender_attachments ID="Bidtender_attachments1" runat="server" />
                                                        </asp:Panel>
                                                    </p>
                                                    <br />
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                        <tr>
                                                            <td style="height: 34px">
                                                                &nbsp;
                                                                <asp:LinkButton ID="lnkBack" runat="server" OnClick="lnkBack_Click">Back</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                    <br />
                                                </div>
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
