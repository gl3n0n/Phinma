<%@ Page Language="C#" AutoEventWireup="true" CodeFile="biddetailssubmitted.aspx.cs" Inherits="web_purchasing_screens_draftBidDetails" %>

<%@ Register Src="../usercontrol/bids/biddetails_attachments.ascx" TagName="biddetails_attachments" TagPrefix="uc3" %>

<%@ Register Src="../usercontrol/commentlist.ascx" TagName="commentlist" TagPrefix="uc2" %>

<%@ Register Src="../usercontrol/bids/biddetails.ascx" TagName="biddetails" TagPrefix="uc1" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/WEB/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_TopNav_Bids" Src="~/web/usercontrol/Purchasing/Purchasing_TopNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_LeftNav" Src="~/web/usercontrol/Purchasing/Purchasing_LeftNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Purchasing/Footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_ph.css" %>' />
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
                                                <EBid:Purchasing_LeftNav runat="server" ID="LeftNav" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>                                                
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <h2>
                                                    Comments</h2>
                                                <uc2:commentlist ID="Commentlist1" runat="server" />                                                
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td id="content">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page0">
                                        <tr>
                                            <td valign="top">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td id="content0">
                                                            <h1>
                                                                <br />
                                                                Bid Events For Approval -  Details</h1>
                                                            <asp:Label ID="lblMessage" runat="server" Font-Size="11px" ForeColor="red"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <uc1:biddetails ID="Biddetails1" runat="server" />
                                                <uc3:biddetails_attachments ID="Biddetails_attachments1" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <p style="text-align: center;">
                                                    <br />
                                                    Comment<br />
                                                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Width="360px" Height="90px" Font-Names="Arial" MaxLength="999"></asp:TextBox><br />
                                                    <asp:RequiredFieldValidator ID="rfvComment" runat="server" ErrorMessage="Include a comment please." ControlToValidate="txtComment" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                                    <br />                                                    
                                                </p>
                                            </td>
                                        </tr>
                                    </table>                                    
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                        <tr>
                                            <td>
                                                <asp:LinkButton runat="server" ID="btnReedit" OnClick="btnReedit_Click" OnClientClick="return confirm('Are you sure you want this bid event to be sent back to the creator for re-editing?');">Re-Edit</asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="btnReject" OnClick="btnReject_Click" OnClientClick="return confirm('Are you sure you want to reject this bid event?');">Reject</asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="btnApprove" OnClick="btnApprove_Click" OnClientClick="return confirm('Are you sure you want to approve this bid event?');">Approve</asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="btnCancel" OnClick="btnCancel_Click" CausesValidation="false">Cancel</asp:LinkButton>
                                            </td>
                                        </tr>                                        
                                    </table> 
                                    <br /><br />                                   
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
