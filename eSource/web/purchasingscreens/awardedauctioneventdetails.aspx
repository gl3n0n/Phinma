<%@ Page Language="C#" AutoEventWireup="true" CodeFile="awardedauctioneventdetails.aspx.cs"
    Inherits="web_purchasing_screens_AwardedAuctionEventDetails" %>

<%@ Register Src="../usercontrol/AuctionVendor/auctiondetail.ascx" TagName="auctiondetail" TagPrefix="uc2" %>
<%@ Register Src="../usercontrol/AuctionVendor/auctionitems.ascx" TagName="auctionitems" TagPrefix="uc3" %>
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
    <script type="text/javascript" src="../include/awardedbiditems.js"></script>
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
                                                <EBid:Purchasing_LeftNav runat="server" ID="Purchasing_LeftNav" />
                                                
                                            </td>
                                        </tr>
                                        
                                    </table>
                                </td>
                                <td id="content">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page0">
					                    <tr>
						                    <td valign=top>
						                    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
							                    <tr>
								                    <td id="content0">
								                        <h1><br />Auction Event Details</h1>										
								                        <div align="left">
                                                            <uc2:auctiondetail ID="Auctiondetail1" runat="server" />									            
                                                            <uc3:auctionitems ID="Auctionitems1" runat="server" />
                                                        </div>
                                                        <br />
								                        <table border="0" cellpadding="0" cellspacing="0" width="100%"  id="actions">
									                        <tr>
										                        <td><asp:LinkButton runat="server" ID="btnOK" OnClick="btnOK_Click" >Back</asp:LinkButton></td>
									                        </tr>
								                        </table>									        
								                    </td>
							                    </tr>
						                    </table></td>
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
