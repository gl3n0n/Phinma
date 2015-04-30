<%@ Page Language="C#" AutoEventWireup="true" CodeFile="endorsementconfirmation.aspx.cs" Inherits="web_buyer_screens_EndorsementConfirmation" %>
<%@ Register  TagPrefix="EBid" TagName="LeftNav" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register  TagPrefix="EBid" TagName="TopNavBids" Src="~/WEB/usercontrol/Buyer/TopNavBids.ascx" %>
<%@ Register  TagPrefix="EBid" TagName="TopNav2" Src="~/WEB/usercontrol/Buyer/TopNav2.ascx" %>
<%@ Register  TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register  TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<%@ Register  TagPrefix="EBid" TagName="CommentBox" Src="~/web/usercontrol/CommentBox.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">

<!-- #BeginTemplate "../templates/bids.dwt" -->

<head>
<meta http-equiv="Content-Language" content="en-us" />
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_buyer.css" %>' />
<style>

</style>
<!-- #BeginEditable "doctitle" -->
<!-- #EndEditable -->
</head>

<body style="height: 100%;">
<form id="Form1" runat="server" >
<div align="left">
<div align="left">
	<table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
	
		<tr>
			<td valign=top>
			<table border="0" cellpadding="0" cellspacing="0" width=100%>
				<tr>
					<td>
					<div align="left" id="masthead">
						<EBid:GlobalLinksNav runat="server" ID="GlobalLinksNav" />
					</div>
					</td>
				</tr>
				<tr>
					<td>
					<div id="tasks">
					<EBid:TopNav2 runat="server" ID="TopNav2" /></div>
					</td>
				</tr>
			</table>
			</td>
		</tr>
		<tr valign="top">
			<td valign="top">
			<table border="0" cellpadding="0" cellspacing="0" width="100%" >
				<tr>
					<td id="relatedInfo">
					<div align="left">
						<EBid:LeftNav runat="server" ID="LeftNav" />
					</div>
					<div align="left">
						<table border="0" cellpadding="0" cellspacing="0" width="100%" id="table3">
							<tr>
																<td><!-- #BeginEditable "commentArea" -->
											<EBid:CommentBox runat="server" ID="CommentBox" />
											
											<!-- #EndEditable --></td>

							</tr>
						</table>
					</div>
					<p>&nbsp;</p>
					<p>&nbsp;</p>
					<p>&nbsp;</p>
					<p>&nbsp;</p>
					<p>&nbsp;</p>
					<p>&nbsp;</p>
					<p>&nbsp;</p>
					<p>&nbsp;</p>
					<p>&nbsp;</p>
					<p>&nbsp;</p>
					<p>&nbsp;</p>
					<p>&nbsp;</p>
					<p>&nbsp;</p>
					<p>&nbsp;</p>
					<p>&nbsp;</p>
					<p>&nbsp;</p>
					<p>&nbsp;</p>
						</td>
					<td id="content">
					<!-- #BeginEditable "contentArea" -->

<div>
<div align="left">
	<table border="0" cellpadding="0" cellspacing="0" width="100%" id="page0">
		<tr>
			<td valign=top>
			<table border="0" cellpadding="0" cellspacing="0" width="100%" >
				<tr>
					<td id="content0">
					<div align="left">
					<h1><br />
					Endorsement Submitted Successfully</h1>
					<p>You have successfully submitted your endorsement to the 
					Purchasing Head for processing.</p>
					<p><asp:LinkButton runat="server" ID="btnHome" OnClick="btnHome_Click">Home</asp:LinkButton></p>
					</div></td>
				</tr>
			</table>
			<p>&nbsp;</p>
			<p>&nbsp;</p>
			<p>&nbsp;</p>
			<p>&nbsp;</p>
			<p>&nbsp;</p>
			<p>&nbsp;</p>
			<p>&nbsp;</p>
			<p>&nbsp;</p>
			<p>&nbsp;</p>
			<p>&nbsp;</p>
			<p>&nbsp;</p>
			<p>&nbsp;</td>
		</tr>
		</table>
</div>
&nbsp;</div>

					<!-- #EndEditable -->
					</td>
				</tr>
			</table>
			</td>
		</tr>
		<tr>
			<td id="footer"><EBid:Footer runat="server" ID="Footer" /></td>
		</tr>
	
	</table>
</div>
&nbsp;</div>
	</form>
</body>

<!-- #EndTemplate -->

</html>