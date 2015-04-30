<%@ Control Language="C#" AutoEventWireup="true" CodeFile="auctiondate.ascx.cs" Inherits="web_user_control_auctiondate" %>
<script type="text/javascript">
<!--
function DisplayTime()
{
	if (tis != null)
	{
		tis++;

		lblServerTime.innerHTML = ConvertSecondsToDateTimeString(tis);
		
		setTimeout("DisplayTime()",1000);
	}
}
//-->
</script>
<div id="tasks" onload="DisplayTime();">
	<span style="padding-left: 10px; float: left">
		Today is&nbsp;<asp:Label ID="lblDate" runat="server"></asp:Label>&nbsp;<label id="lblServerTime"></label>
	</span>
</div>
