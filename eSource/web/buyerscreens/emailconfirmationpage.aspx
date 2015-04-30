<%@ Page Language="C#" AutoEventWireup="true" CodeFile="emailconfirmationpage.aspx.cs" Inherits="web_buyerscreens_EmailConfirmationPage" %>
<%@ Register  TagPrefix="EBid" TagName="LeftNav" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register  TagPrefix="EBid" TagName="TopNavBids" Src="~/WEB/usercontrol/Buyer/TopNavBids.ascx" %>
<%@ Register  TagPrefix="EBid" TagName="TopNav2" Src="~/WEB/usercontrol/Buyer/TopNav2.ascx" %>
<%@ Register  TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register  TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>


<html  >
<head runat="server">
    <title>Email Confirmation Page</title>
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_buyer.css" %>' />
</head>
<body style="height: 100%;">
<form runat="server" id=frm1>
<div>
<div align="left">
	<table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
		<tr>
			<td valign=top>
			<table border="0" cellpadding="0" cellspacing="0" width=100%>
				<tr>
					<td>
					<div align="left" id="masthead">
						<EBid:GlobalLinksNav runat="server" id=GlobalLinksNav />
					</div>
					</td>
				</tr>
				<tr>
					<td>
					<EBid:TopNav2 id="TopNav2"  runat="server"/>
					</td>
				</tr>
			</table>
			</td>
		</tr>
		<tr>
			<td>
			<table border="0" cellpadding="0" cellspacing="0" width="100%" >
				<tr>
					<td id="relatedInfo">
					<div align="left">
						<EBid:LeftNav id="LeftNav"  runat="server"/>
					</div>
					<div align="left">
						<table border="0" cellpadding="0" cellspacing="0" width="100%" id="table3">
							<tr>
								<td><!-- #BeginEditable "commentArea" -->&nbsp;<!-- #EndEditable --></td>
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
                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page0">
				    <tr>
					    <td id="content0">
					        <div align="left">
					            <h1><br />Email Successfully Sent</h1>
					             <p>You have successfully sent your email to the following email addresses&nbsp;<asp:Label runat="server" ID="lblAddresses"></asp:Label></p>
					            <p><asp:LinkButton runat="server" ID="btnHome" OnClick="btnHome_Click" >Home</asp:LinkButton></p>
					        </div>
					    </td>
				</tr>
					</table>&nbsp;
					<!-- #EndEditable --></td>
				</tr>
			</table>
			</td>
		</tr>
		<tr>
			<td id="footer"><EBid:Footer runat="server" ID="Footer1" /></td>
		</tr>
		</table>
		<asp:TextBox runat="server" ID="txtSuppliers" width=0 />
		<input type="hidden" runat="server" id="hdnBidRefNo" name="hdnBidRefNo" />
</div>
</div>
</form>
</body>
</html>
