<%@ Page Language="C#" AutoEventWireup="true" CodeFile="auctiondetailssubmitted.aspx.cs" Inherits="web_purchasing_screens_auctionDetailsSubmitted" %>

<%@ Register Src="../usercontrol/commentlist_auction.ascx" TagName="commentlist_auction" TagPrefix="uc5" %>

<%@ Register Src="../usercontrol/auctionvendor/auctiondetail.ascx" TagName="auctiondetail" TagPrefix="uc2" %>
<%@ Register Src="../usercontrol/auctionvendor/auctionitems.ascx" TagName="auctionitems" TagPrefix="uc3" %>
<%@ Register Src="../usercontrol/auctionvendor/auctionparticipants.ascx" TagName="auctionparticipants" TagPrefix="uc4" %>

<%@ Register Src="../usercontrol/AuctionVendor/auctionattachments.ascx" TagName="auctionattachments" TagPrefix="uc1" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/WEB/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_TopNav_Auct" Src="~/web/usercontrol/Purchasing/Purchasing_TopNav_Auct.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_LeftNav" Src="~/web/usercontrol/Purchasing/Purchasing_LeftNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_LeftNav_Not" Src="~/web/usercontrol/Purchasing/Purchasing_LeftNav_Auct_Not.ascx" %>
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
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />    
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
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
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="height: 100%;">
                            <tr>
                                <td id="relatedInfo">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <EBid:Purchasing_LeftNav runat="server" ID="Purchasing_LeftNav" />
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <h2>Comments</h2>
                                            </td>                                            
                                        </tr>
                                        <tr>
                                            <td>
                                                <uc5:commentlist_auction ID="Commentlist_auction1" runat="server" />
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
                                                                Auction Event Details</h1>                                                            
                                                            <div align="left">
                                                                <uc2:auctiondetail ID="Auctiondetail1" runat="server" />                                                                
                                                                <uc3:auctionitems ID="Auctionitems1" runat="server" />
                                                                <br />
                                                                <uc1:auctionattachments ID="Auctionattachments1" runat="server"></uc1:auctionattachments>
                                                                <br />
                                                            </div>
                                                            <div align="left">                                                                
                                                                <asp:GridView AutoGenerateColumns="false" runat="server" ID="gvInvitedSuppliers" CssClass="itemDetails">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="&nbsp;Invited Suppliers" ItemStyle-BackColor="#FFFFFF">
                                                                            <ItemStyle CssClass="itemDetails_td" />
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblVendors" Text='<%#Bind("Supplier") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
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
                                    <br />
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                        <tr>
                                            <td>
                                                <asp:LinkButton runat="server" ID="btnReedit" Text="Re-edit" Width="90px" OnClientClick="return confirm('Are you sure you want this auction event to be send back to the creator for re-editing ?');" OnClick="btnReedit_Click"></asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="btnApprove" Text="Approve" Width="90px" OnClick="btnApprove_Click" OnClientClick="return confirm('Are you sure you want to approve this auction event?');"></asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="btnReject" Text="Reject" Width="90px" OnClick="btnReject_Click" OnClientClick="return confirm('Are you sure you want to reject this auction event?');"></asp:LinkButton>
                                                <asp:LinkButton ID="lnkBack" runat="server" Width="90px" CausesValidation="false" OnClick="lnkBack_Click">Back</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                    <p>&nbsp;
                                        
                                    </p>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td id="footer" height="50px">
                        <EBid:Footer runat="server" ID="Footer1" />
                        <input type="hidden" id="hiddenID" name="hiddenID" runat="server" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
