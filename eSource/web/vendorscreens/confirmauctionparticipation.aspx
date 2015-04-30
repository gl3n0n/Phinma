<%@ Page Language="C#" AutoEventWireup="true" CodeFile="confirmauctionparticipation.aspx.cs" Inherits="web_vendorscreens_confirmauctionparticipation" %>

<%@ Register Src="../usercontrol/AuctionVendor/auctionattachments.ascx" TagName="auctionattachments" TagPrefix="uc5" %>

<%@ Register Src="../usercontrol/commentlist_auction.ascx" TagName="commentlist_auction" TagPrefix="uc4" %>

<%@ Register Src="../usercontrol/auctionvendor/auctionitems.ascx" TagName="auctionitems" TagPrefix="uc3" %>
<%@ Register Src="../usercontrol/vendor/Vendor_LeftNav_Notifications.ascx" TagName="Vendor_LeftNav_Notifications" TagPrefix="uc2" %>
<%@ Register Src="../usercontrol/auctionvendor/auctiondetail.ascx" TagName="auctiondetail" TagPrefix="uc1" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Vendor/Footer.ascx"  %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/web/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_TopNav_Auction" Src="~/web/usercontrol/Vendor/Vendor_TopNav_Auction.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_LeftNav_Auctions" Src="~/web/usercontrol/Vendor/Vendor_LeftNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="CommentArea" Src="~/web/usercontrol/CommentArea.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
    <%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='../themes/<%= Session["configTheme"] %>/css/style_v.css' />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../include/util.js"></script>
    <script type="text/javascript">
	<!--
		function IsTicketProvided()
		{
			var ticket = document.getElementById('txtTicket');
			if (ticket != null)
			{
				if (ticket.value != '')
					return true;
				else
				{
					alert('Ticket is required to confirm/decline your participation.');
					ticket.focus();
					return false;
				}
			}
			else
				return false;
		}	
		
		function EnableAcceptButton()
		{
			if(document.getElementById("cbTermsAccept").checked)
			{
				document.getElementById("lnkConfirm").disabled=false;
				//document.getElementById("lnkDecline").disabled=false;
			}
			else
			{
				document.getElementById("lnkConfirm").disabled=true;
				//document.getElementById("lnkDecline").disabled=true;
			}
		}	
	//-->
	</script>
</head>
<body onload="EnableAcceptButton()">
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
                                    <div>
                                        <EBid:Vendor_LeftNav_Auctions runat="server" ID="Vendor_LeftNav_Bids" />
                                    </div>
                                    <h2>Comments</h2> 
                                    <div>                                   
                                    <uc4:commentlist_auction ID="Commentlist_auction1" runat="server" />
                                    </div>
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
                                                                Confirm Auction Event Participation</h1>
                                                            <uc1:auctiondetail ID="Auctiondetail1" runat="server" />
                                                            <uc3:auctionitems ID="Auctionitems1" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <uc5:auctionattachments ID="Auctionattachments1" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" colspan="2">
                                                <br />
                                                <p>
                                                    <b>Ticket&nbsp;:&nbsp;</b><asp:TextBox ID="txtTicket" runat="server" Font-Size="12px" Width="160px"></asp:TextBox>
                                                    &nbsp;
                                                    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label><br />
                                                    &nbsp;&nbsp;Please input the alpha-numeric ticket to confirm/decline your participation to this auction.<br />
                                                    <input type="checkbox" id="cbTermsAccept" name="itemdetails2" value="ON" onclick="EnableAcceptButton();" />
                                                    I have read, understood and accept the <asp:LinkButton ID="lnkTerms" runat="server" CausesValidation="false" OnClick="lnkTerms_Click">Terms and Conditions</asp:LinkButton> for Auction events hosted by Trans-Asia .
                                                </p>
                                            </td>
                                        </tr>
                                       <tr>
                                            <td colspan="2" align="center"><br />
                                                    <p  style="text-align: center;">Comment</p>
                                            </td>
                                        </tr> 
                                        <tr>
                                            <td colspan="2" align="center">
                                                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Width="360px" Height="90px" Font-Names="Arial" MaxLength="999"></asp:TextBox><br />
                                                <asp:CustomValidator ID="cvValidator" runat="server" ErrorMessage="* comments is required" OnServerValidate="cvValidator_ServerValidate" Font-Size="11px"></asp:CustomValidator></td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="lnkConfirm" runat="server" CausesValidation="true" OnClientClick='if (IsTicketProvided()) return confirm("Are you sure you want to confirm your participation to this auction?"); else return false;'
                                                    CommandName="Confirm" OnCommand="lnkConfirm_Command">Confirm</asp:LinkButton>
                                                <asp:LinkButton ID="lnkDecline" runat="server" CausesValidation="true" OnClientClick='if (IsTicketProvided()) return confirm("Are you sure you want to decline your participation to this auction?"); else return false;'
                                                    CommandName="Decline" OnCommand="lnkDecline_Command">Decline</asp:LinkButton>
                                                <asp:LinkButton ID="lnkBack" runat="server" CausesValidation="false" OnClick="lnkBack_Click">Back</asp:LinkButton>
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
