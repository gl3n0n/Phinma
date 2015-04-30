<%@ Page Language="c#" Inherits="EBid.web.vendor_screens.BidDetailsSubmitted" CodeFile="BidDetailsSubmitted.aspx.cs" %>

<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Vendor/Footer.ascx"  %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/web/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_TopNav_Bids" Src="~/web/usercontrol/Vendor/Vendor_TopNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_LeftNav_Bids" Src="~/web/usercontrol/Vendor/Vendor_LeftNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TendersCommentArea" Src="~/web/usercontrol/TendersCommentArea.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
    <%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_v.css" %>' />
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
                                    <EBid:TopDate runat="server" ID="TopDate" />
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
                                            <td>
                                                <EBid:Vendor_LeftNav_Bids runat="server" ID="Vendor_LeftNav_Bids" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                                <EBid:TendersCommentArea runat="server" ID="TendersCommentArea" />
                                                <p>
                                                    &nbsp;</p>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td id="content">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table5">
                                        <tr>
                                            <td id="content0">
                                                <h1>
                                                    <br />
                                                    Bid Tender Summary</h1>
                                                <p>
                                                    This Bid Item is still in Draft status. You may make edits to this document before
                                                    submitting it for Approval by the Purchasing head.</p>
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails" height="85">
                                                    <tr>
                                                        <th colspan="2">
                                                            Details</tr>
                                                    <tr>
                                                        <td width="133">
                                                            PR Number</td>
                                                        <td class="value">
                                                            <asp:Label runat="server" ID="lblPRNumber"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td width="133">
                                                            Reference Number</td>
                                                        <td class="value">
                                                            <asp:Label runat="server" ID="lblBidReferenceNumber"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td width="133">
                                                            Category</td>
                                                        <td class="value">
                                                            <asp:Label runat="server" ID="lblCategory"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td width="133">
                                                            Submission Deadline</td>
                                                        <td class="value">
                                                            <asp:Label runat="server" ID="lblBidSubmissionDeadline"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td width="133">
                                                            Delivery Date
                                                        </td>
                                                        <td class="value">
                                                            <asp:Label runat="server" ID="lblDeliveryDate"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td width="133">
                                                            Description
                                                        </td>
                                                        <td class="value">
                                                            <asp:Label runat="server" ID="lblBidItemDescription"></asp:Label></td>
                                                    </tr>
                                                </table>
                                                <asp:GridView runat="server" ID="gvBids" AutoGenerateColumns="false" Width="100%"
                                                    CssClass="itemDetails" OnRowDataBound="gvBids_RowDataBound">
                                                    <Columns>
                                                    </Columns>
                                                </asp:GridView>
                                                <br />
                                                <asp:GridView runat="server" ID="gvFileAttachments" CssClass="itemDetails" AutoGenerateColumns="False"
                                                    DataKeyNames="FileUploadId" DataSourceID="dsFileAttachments">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="File Attachment(s)" SortExpression="FileAttachment">
                                                            <ItemStyle BackColor="White" />
                                                            <ItemTemplate>
                                                                &nbsp;<asp:Label ID="lblFileName" runat="server" Text='<%# Bind("FileAttachment") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:SqlDataSource ID="dsFileAttachments" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                    SelectCommand="s3p_EBid_QueryVendorFileAttachments" SelectCommandType="StoredProcedure">
                                                    <SelectParameters>
                                                        <asp:SessionParameter Name="vendorId" SessionField="userid" Type="Int32" />
                                                        <asp:SessionParameter Name="bidRefNo" SessionField="BidRefNo" Type="Int32" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                                <br />
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton runat="server" ID="btnReCall" Text="Recall" OnClick="btnReCall_Click"></asp:LinkButton>
                                                            <asp:LinkButton runat="server" ID="btnOK" Text="OK" OnClick="btnOK_Click"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <p>&nbsp;</p>
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
                        <EBid:Footer runat="server" ID="Footer" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
