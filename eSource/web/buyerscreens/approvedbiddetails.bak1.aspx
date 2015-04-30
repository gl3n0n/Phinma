<%@ Page Language="C#" AutoEventWireup="true" CodeFile="approvedbiddetails.aspx.cs" Inherits="web_buyerscreens_ApprovedBidDetails" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="../usercontrol/CommentBox.ascx" TagName="CommentBox" TagPrefix="uc4" %>
<%@ Register Src="../usercontrol/commentlist.ascx" TagName="commentlist" TagPrefix="uc3" %>
<%@ Register Src="../usercontrol/bids/biddetails_attachments.ascx" TagName="biddetails_attachments" TagPrefix="uc2" %>
<%@ Register Src="../usercontrol/bids/biddetails.ascx" TagName="biddetails" TagPrefix="uc1" %>
<%@ Register TagPrefix="EBid" TagName="LeftNav" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNavBids" Src="~/WEB/usercontrol/Buyer/TopNavBids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNav2" Src="~/WEB/usercontrol/Buyer/TopNav2.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<%@ Register Src="../usercontrol/bids/biddetails_items.ascx" TagName="biddetails_items" TagPrefix="uc1" %>
<%@ Register Src="../usercontrol/bids/biddetails_suppliers.ascx" TagName="biddetails_suppliers" TagPrefix="uc2" %>
<%@ Register Src="../usercontrol/bids/biddetails_details.ascx" TagName="biddetails_details" TagPrefix="uc3" %>
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

    <script type="text/javascript" src="../include/util.js"></script>

</head>
<body style="height: 100%;">
    <form runat="server" id="frm1" defaultbutton="lnkOk">
        <div align="left">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
                <tr height="137px">
                    <td valign="top">
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
                                    <EBid:TopNav2 ID="TopNav2" runat="server" />
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
                                    <div align="left">
                                        <EBid:LeftNav ID="LeftNav" runat="server" />
                                    </div>
                                    <br />
                                    <uc4:CommentBox ID="CommentBox1" runat="server" />
                                </td>
                                <td id="content">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page0" height="100%">
                                        <tr>
                                            <td valign="top">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td id="content0">
                                                            <h1>
                                                                <br />
                                                                Bid Event Details</h1>
                                                            <div align="left">
                                                                <uc3:biddetails_details ID="Biddetails_details1" runat="server" />
                                                                <uc1:biddetails_items ID="Biddetails_items1" runat="server" />
                                                                <asp:GridView runat="server" ID="gvSuppliers" AutoGenerateColumns="False" SkinID="AuctionedItems" AllowSorting="True" DataSourceID="dsBidParticipants">
                                                                    <HeaderStyle ForeColor="White" BackColor="Blue" HorizontalAlign="Center" Height="20px" />
                                                                    <RowStyle BackColor="White" />
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <ItemStyle Width="10px" />
                                                                            <HeaderTemplate>
                                                                                <asp:CheckBox ID="chkHeader" runat="server" />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkRow" runat="server" />
                                                                                <asp:HiddenField ID="hdnvendorid" runat="server" Value='<%# Bind("VendorId") %>' />
                                                                                <asp:HiddenField ID="hdnname" runat="server" Value='<%# Bind("Supplier") %>' />
                                                                                <asp:HiddenField ID="hdnemail" runat="server" Value='<%# Bind("VendorEmail") %>' />
                                                                                <asp:HiddenField ID="hdnmobileno" runat="server" Value='<%# Bind("MobileNo") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Supplier" SortExpression="Supplier">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemTemplate>
                                                                                &nbsp;<asp:Label ID="Label4" runat="server" Text='<%# Bind("Supplier") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Email Address" SortExpression="VendorEmail">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                            <ItemTemplate>
                                                                                &nbsp;<asp:Label ID="Label2" runat="server" Text='<%# Bind("VendorEmail") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Mobile No." SortExpression="MobileNo">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                            <ItemTemplate>
                                                                                &nbsp;<asp:Label ID="lblMobileNo" runat="server" Text='<%# Bind("MobileNo") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="No. of Invitations Sent" SortExpression="EmailSent">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("EmailSent") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="No. of Tenders Submitted" SortExpression="TenderCount">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label11" runat="server" Text='<%# Bind("TenderCount") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                                <asp:SqlDataSource ID="dsBidParticipants" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="s3p_EBid_GetSuppliers_2" SelectCommandType="StoredProcedure">
                                                                    <SelectParameters>
                                                                        <asp:SessionParameter DefaultValue="0" Name="BidRefNo" SessionField="BidRefNo" Type="Int32" />
                                                                    </SelectParameters>
                                                                </asp:SqlDataSource>
                                                            <uc2:biddetails_attachments ID="Biddetails_attachments1" runat="server" />
                                                                     
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Label ID="lblMessage" runat="server" Text="" Font-Size="11px" ForeColor="red"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                                <tr>
                                                                    <td>
                                                                        <asp:LinkButton runat="server" ID="btnSendEmailToVendors" OnClick="btnSendEmailToVendors_Click" Width="150">Send Invitation</asp:LinkButton>
                                                                        <asp:LinkButton runat="server" ID="lnkOk" OnClick="lnkOk_Click">OK</asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
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
        </div>
    </form>
</body>
</html>
