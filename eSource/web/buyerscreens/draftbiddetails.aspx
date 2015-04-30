<%@ Page Language="c#" Inherits="EBid.WEB.buyer_screens.DraftBidDetails" CodeFile="draftbiddetails.aspx.cs"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="../usercontrol/commentlist.ascx" TagName="commentlist" TagPrefix="uc3" %>
<%--<%@ Register Src="../usercontrol/bids/biddetails_attachments.ascx" TagName="biddetails_attachments" TagPrefix="uc2" %>
--%>
<%@ Register Src="~/web/usercontrol/bids/draft_bid_attachment.ascx" TagName="draft_bid_attachment" TagPrefix="uc2" %>

<%@ Register Src="../usercontrol/bids/biddetails.ascx" TagName="biddetails" TagPrefix="uc1" %>
<%@ Register TagPrefix="EBid" TagName="LeftNav" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNavBids" Src="~/WEB/usercontrol/Buyer/TopNavBids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNav2" Src="~/WEB/usercontrol/Buyer/TopNav2.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<%@ Register TagPrefix="EBid" TagName="CommentBox" Src="~/web/usercontrol/CommentBox.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
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
    <div>
        <form id="Form1" runat="server" defaultbutton="btnOK">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
                <tr>
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
                                    <EBid:TopNav2 runat="server" ID="TopNav2" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr valign="top" height='100%'>
                    <td valign="top">
                        <br />
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
                            <tr>
                                <td id="relatedInfo">
                                    <div>
                                        <EBid:LeftNav runat="server" ID="LeftNav" />
                                    </div>
                                    <h2>
                                        Comments</h2>
                                    <div>
                                        <uc3:commentlist ID="Commentlist1" runat="server" />
                                    </div>
                                    <br />
                                </td>
                                <td id="content">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page0">
                                         <tr>
                                            <td id="content0">
                                                <h1>
                                                    <br />
                                                    Bid Event Details</h1>
                                                <p>
                                                    Click "Edit" to update this event. Click "Submit" to submit this event. If bid event
                                                    is equal or more than Php 2,000,000.00 estimated price, 
                                                    then it would require approval from
                                                    your immediate superior. Otherwise, it would automatically be approved.
                                                </p>
                                                <div align="left">
                                                    <uc1:biddetails ID="Biddetails1" runat="server" />
                                                </div>
                                                <br />
                                                <div align="left">
                                                    <%--<uc2:biddetails_attachments ID="Biddetails_attachments1" runat="server" />--%>
                                                     <uc2:draft_bid_attachment ID="draft_bid_attachment1" runat="server" />
                                                </div>
                                                <p style="text-align: center;">
                                                    <br />
                                                    Comment<br />
                                                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Width="360px" Height="90px"
                                                        Font-Names="Arial" MaxLength="999"></asp:TextBox><br />
                                                    <%--<asp:RequiredFieldValidator ID="rfvComment" runat="server" ErrorMessage="Include a comment please."
                                                        ControlToValidate="txtComment" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                    <br />
                                                     <asp:SqlDataSource ID="dsComments" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                        SelectCommand="sp_QueryBidItemsComments" SelectCommandType="StoredProcedure">
                                                        <SelectParameters>
                                                            <asp:SessionParameter Name="refNo" SessionField="BidRefNo" Type="String" />
                                                            <asp:SessionParameter Name="userId" SessionField="UserId" Type="String" />
                                                        </SelectParameters>
                                                     </asp:SqlDataSource>
                                                </p>                                                
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton runat="server" ID="btnEdit" CausesValidation="false" OnClick="btnEdit_Click">Edit</asp:LinkButton>
                                                            <asp:LinkButton runat="server" ID="btnSubmit" OnClientClick="return confirm('Are you sure you want to submit this bid event?');"
                                                                OnClick="btnSubmit_Click">Submit</asp:LinkButton>
                                                            <asp:LinkButton runat="server" ID="btnOK" CausesValidation="false" OnClick="btnOK_Click">OK</asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td id="footer">
                        <EBid:Footer runat="server" ID="Footer" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
