<%@ Page Language="C#" AutoEventWireup="true" CodeFile="report_bidhistorybyauctionevent.aspx.cs" Inherits="web_admin_report_bidhistorybyauctionevent" %>

<%@ Register Src="../usercontrol/reports/bidhistorybyauctionitem.ascx" TagName="bidhistorybyauctionitem" TagPrefix="uc2" %>

<%@ Register TagPrefix="EBid" TagName="AdminTopNav" Src="~/usercontrol/admin/adminTopNav.ascx" %>
<%@ Register Src="../usercontrol/admin/Admin_LeftNav_Reports.ascx" TagName="Admin_LeftNav_Reports" TagPrefix="uc1" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Admin_TopNav_Reports" Src="~/usercontrol/admin/Admin_TopNav_Reports.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/usercontrol/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link href="../css/style.css" rel="stylesheet" type="text/css" />    
	<link href="../css/style_ua.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div align="left">
        <form id="Form1" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
				<tr>
						<td valign="top" height="137px">
						  <table border="0" cellpadding="0" cellspacing="0" width="100%">
								<tr>
									<td>
										<div align="left" id="masthead">
											<EBid:AdminTopNav runat="server" ID="GlobalLinksNav" />
										</div>
									</td>
								</tr>
								<tr>
									<td>
										<EBid:Admin_TopNav_Reports runat="server" ID="Admin_TopNav_Reports" />
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
                    <td class="content">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="height: 100%">
                            <tr>
                                <td id="relatedInfo">
                                    <uc1:Admin_LeftNav_Reports ID="Admin_LeftNav_Reports1" runat="server" />
                                </td>
                                <td id="content">
                                    <div align="left">
                                        <br />
                                        <h1>
                                            Bid History By Auction Event</h1>
                                        <div>
                                            <br />
                                            <uc2:bidhistorybyauctionitem ID="Bidhistorybyauctionitem1" runat="server" />                                            
                                        </div>
                                    </div>
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
