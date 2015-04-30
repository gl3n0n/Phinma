<%@ Page Language="C#" AutoEventWireup="true" CodeFile="auctionstopped.aspx.cs" Inherits="web_onlineAuction_AuctionStopped"
	Theme="default" %>

<%@ Register Src="../usercontrol/AuctionVendor/AuctionVendor_TopNav_Ongoing.ascx"
	TagName="AuctionVendor_TopNav_Ongoing" TagPrefix="uc2" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/web/usercontrol/TopDate.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
	<title id="PageTitle" runat="server"></title>
	<meta http-equiv="Content-Language" content="en-us" />
	<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
	<link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_oa.css" %>' />
</head>
<body style="height: 100%;">
	<div>
		<form runat="server">
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
									<uc2:AuctionVendor_TopNav_Ongoing ID="AuctionVendor_TopNav_Ongoing1" runat="server" />
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
					<td valign="top">
						<table border="0" cellpadding="0" cellspacing="0" width="100%">
							<tr>
								<td valign="top" id="content">
									<h1>
										<br />
										Auction Stopped</h1>
								</td>
							</tr>													
							<tr>
								<td style="padding-left: 20px; padding-right: 10px; padding-top: 5px;">
									<p>&nbsp;&nbsp;&nbsp;&nbsp;The auction was stopped.</p>
									<asp:Label ID="Label1" runat="server" Font-Size="11px" ForeColor="DimGray">Date Posted:&nbsp;</asp:Label><asp:Label ID="lblDatePosted" runat="server" Font-Size="11px"></asp:Label><br />
									<asp:Label ID="Label2" runat="server" Font-Size="11px" ForeColor="DimGray">Author:&nbsp;</asp:Label><asp:Label ID="lblAuthor" runat="server" Font-Size="11px"></asp:Label><br />
									<asp:Label ID="Label3" runat="server" Font-Size="11px" ForeColor="DimGray">Comments:&nbsp;</asp:Label><asp:Label ID="lblcomment" runat="server" Font-Size="11px"></asp:Label>
								</td>
							</tr>
							<tr>
								<td >
									<p>
										&nbsp;</p>
								</td>
							</tr>
							<tr>
								<td style="padding-left: 20px; padding-right: 10px" id="actions">
									<asp:LinkButton ID="lnkOk" runat="server" OnClick="lnkOk_Click">Ok</asp:LinkButton>
								</td>
							</tr>
							<tr>
								<td >
									<p>
										&nbsp;</p>
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
