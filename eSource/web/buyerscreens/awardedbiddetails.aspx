<%@ Page Language="C#" AutoEventWireup="true" CodeFile="awardedbiddetails.aspx.cs" Inherits="WEB_buyer_screens_AwardedBidDetails" MaintainScrollPositionOnPostback="true" %>
<%@ Register Src="../usercontrol/bids/buyer_bidtenderdetails.ascx" TagName="buyer_bidtenderdetails" TagPrefix="uc4" %>
<%@ Register Src="../usercontrol/bids/bidtenderdetails.ascx" TagName="bidtenderdetails" TagPrefix="uc3" %>
<%@ Register Src="../usercontrol/bids/biditemdetails.ascx" TagName="biditemdetails" TagPrefix="uc2" %>
<%@ Register Src="../usercontrol/commentlist_tender.ascx" TagName="commentlist_tender" TagPrefix="uc1" %>
<%@ Register TagPrefix="EBid" TagName="LeftNav" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNavBids" Src="~/WEB/usercontrol/Buyer/TopNavBids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNav2" Src="~/WEB/usercontrol/Buyer/TopNav2.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
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
</head>
<body style="height: 100%;">
	<div align="left">
		<form runat="server" id="frm1">
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
									<h2>
									    Comments</h2>
									<div align="left">
									    <uc1:commentlist_tender ID="Commentlist_tender1" runat="server" />
									</div>
									<p>
                                        
										&nbsp;</p>
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
																Awarded Bid Tender Details</h1>
															<p>
																<uc2:biditemdetails ID="Biditemdetails1" runat="server" />
															</p>
															<p>
                                                                <uc4:buyer_bidtenderdetails ID="Buyer_bidtenderdetails1" runat="server" />
                                                            </p>
															<br />
															<div align="left">
																<table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
																	<tr>
																		<td>
																		    <asp:HyperLink ID="lnkComparison" runat="server" width="100px">Comparison</asp:HyperLink>
                                                                            <asp:HyperLink ID="lnkExport" runat="server" width="100px">Export</asp:HyperLink>
																			<asp:LinkButton runat="server" ID="lnkBack" OnClick="lnkBack_Click" width="100px">Back</asp:LinkButton>
																		</td>
																	</tr>
																</table>
															</div>
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
						<EBid:Footer runat="server" ID="Footer" />
					</td>
				</tr>
			</table>
		</form>
	</div>
</body>
</html>
