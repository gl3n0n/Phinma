<%@ Page Language="C#" AutoEventWireup="true" CodeFile="confirmauctionevent.aspx.cs" Inherits="web_onlineAuction_confirmauctionevent" Theme="default" %>

<%@ Register Src="../usercontrol/AuctionVendor/AuctionVendor_TopNav.ascx" TagName="AuctionVendor_TopNav" TagPrefix="uc1" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/web/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AuctionVendor_TopNav_Upcoming" Src="~/web/usercontrol/AuctionVendor/AuctionVendor_TopNav_Upcoming.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
	<link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_oa.css" %>' />

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
    <div>
        <form runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
                <tr>
                    <td valign="top" height="137">
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
                                    <uc1:AuctionVendor_TopNav ID="AuctionVendor_TopNav1" runat="server" />
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
                                        Confirm Participation to Auction Event</h1>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="padding-left: 20px; padding-right: 10px">
                                    <div align="left">
                                        <br />
                                        <%--AUCTION HERE--%>
                                        <asp:DetailsView ID="dvEventDetails" runat="server" DataSourceID="dsAuctionDetails" DataKeyNames="AuctionRefNo" AutoGenerateRows="False" HeaderText="&nbsp;Auction Details" SkinID="EventDetails">
                                            <Fields>
                                                <asp:TemplateField HeaderText="&#160;Description :" SortExpression="ItemDesc">
                                                    <InsertItemTemplate>
                                                        &nbsp;<asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ItemDesc") %>'></asp:TextBox>
                                                    </InsertItemTemplate>
                                                    <ItemTemplate>
                                                        &nbsp;<asp:Label ID="Label5" runat="server" Text='<%# Bind("ItemDesc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="&#160;Confirmation Deadline : " SortExpression="AuctionDeadline">
                                                    <ItemTemplate>
                                                        &nbsp;<asp:Label ID="Label4" runat="server" Text='<%# Eval("AuctionDeadline", "{0:D}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="&nbsp;Start Date & Time : " SortExpression="AuctionStartDateTime">
                                                    <ItemTemplate>
                                                        &nbsp;<asp:Label ID="Label3" runat="server" Text='<%# Format(Eval("AuctionStartDateTime").ToString()) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="&nbsp;End Date & Time : " SortExpression="AuctionEndDateTime">
                                                    <ItemTemplate>
                                                        &nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Format(Eval("AuctionEndDateTime").ToString()) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="&nbsp;Currency : " SortExpression="Currency">
                                                    <ItemTemplate>
                                                        &nbsp;<asp:Label ID="Label10" runat="server" Text='<%# Bind("Currency") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Fields>
                                        </asp:DetailsView>
                                        <asp:SqlDataSource ID="dsAuctionDetails" runat="server" SelectCommandType="StoredProcedure" SelectCommand="sp_GetAuctionDetails" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'>
                                            <SelectParameters>
                                                <asp:SessionParameter DefaultValue="0" Name="AuctionRefNo" SessionField="AuctionRefNo" Type="Int32" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                        <asp:SqlDataSource ID="dsBidTender" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetTenderBidPrice" SelectCommandType="StoredProcedure">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="AuctionRefNo" SessionField="AuctionRefNo" Type="String" />
                                                <asp:SessionParameter Name="UserId" SessionField="UserId" Type="String" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" colspan="2" style="padding-left: 20px; padding-right: 10px">
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
                                <td colspan="2" align="center">
                                    <br />
                                    <p style="text-align: center;">
                                        Comment</p>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Width="360px" Height="90px" Font-Names="Arial" MaxLength="999"></asp:TextBox><br />
                                    <asp:CustomValidator ID="cvValidator" runat="server" ErrorMessage="* comments is required" OnServerValidate="cvValidator_ServerValidate" Font-Size="11px"></asp:CustomValidator></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding-left: 20px; padding-right: 10px" id="actions">
                                    <asp:LinkButton ID="lnkConfirm" runat="server" CausesValidation="true" OnClientClick='if (IsTicketProvided()) return confirm("Are you sure you want to confirm your participation to this auction?"); else return false;' CommandName="Confirm" OnCommand="lnkConfirm_Command">Confirm</asp:LinkButton>
                                    <asp:LinkButton ID="lnkDecline" runat="server" CausesValidation="true" OnClientClick='if (IsTicketProvided()) return confirm("Are you sure you want to decline your participation to this auction?"); else return false;' CommandName="Decline" OnCommand="lnkDecline_Command">Decline</asp:LinkButton>
                                    <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="false" OnClick="lnkCancel_Click">Cancel</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
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
