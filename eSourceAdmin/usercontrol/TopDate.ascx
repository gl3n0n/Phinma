<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopDate.ascx.cs" Inherits="web_user_control_TopDate" %>
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
<div id="tasks" onload="DisplayTime();" style="text-align:left;">
	<span style="padding-left: 10px; text-align:left">
		Today is&nbsp;<asp:Label ID="lblDate" runat="server"></asp:Label>&nbsp;<label id="lblServerTime"></label>
	</span>
</div>
