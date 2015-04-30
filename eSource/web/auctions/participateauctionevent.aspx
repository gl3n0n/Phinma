<%@ Page Language="C#" AutoEventWireup="true" CodeFile="participateauctionevent.aspx.cs" Inherits="web_onlineAuction_participateauctionevent" Theme="default" %>

<%@ Register Src="../usercontrol/auctionvendor/auctionfooter.ascx" TagName="auctionfooter" TagPrefix="uc1" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/web/usercontrol/TopDate.ascx" %>
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
					alert('Ticket is required to participate this auction.');
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
				document.getElementById("lnkParticipate").disabled=false;				
			}
			else
			{
				document.getElementById("lnkParticipate").disabled=true;				
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
                    <td valign="top" height="21">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
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
                                        Participate Auction Event</h1>
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
                                                <asp:TemplateField HeaderText="&#160;Start Date &amp; Time" SortExpression="AuctionStartDateTime">
                                                    <ItemTemplate>
                                                        &nbsp;<asp:Label ID="Label3" runat="server" Text='<%# Format(Eval("AuctionStartDateTime").ToString()) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="&#160;End Date &amp; Time" SortExpression="AuctionEndDateTime">
                                                    <ItemTemplate>
                                                        &nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Format(Eval("AuctionEndDateTime").ToString()) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Fields>
                                        </asp:DetailsView>
                                        <asp:SqlDataSource ID="dsAuctionDetails" runat="server" SelectCommandType="StoredProcedure" SelectCommand="sp_GetAuctionDetails" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'>
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="hdnAuctionRefNo" DefaultValue="0" Name="AuctionRefNo" PropertyName="Value" Type="Int32" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                        <asp:HiddenField ID="hdnAuctionRefNo" runat="server" />
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
                                        <input type="checkbox" id="cbTermsAccept" name="itemdetails2" value="ON" onclick="EnableAcceptButton();">
                                        I have read, understood and accept the <asp:LinkButton ID="lnkTerms" runat="server" CausesValidation="false" OnClick="lnkTerms_Click">Terms and Conditions</asp:LinkButton> for Auction events hosted by Trans-Asia .
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <p>
                                        &nbsp;</p>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding-left: 20px; padding-right: 10px" id="actions">
                                    <asp:LinkButton ID="lnkParticipate" runat="server" CausesValidation="true" OnClientClick='if (IsTicketProvided()) return confirm("Are you sure you want to participate to this auction?"); else return false;'
                                        CommandName="Confirm" OnCommand="lnkParticipate_Command" Width="100px">Participate</asp:LinkButton>
                                    <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="false" OnClientClick='window.close();' Width="100px">Cancel</asp:LinkButton>
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
                        <uc1:auctionfooter ID="Auctionfooter1" runat="server" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
