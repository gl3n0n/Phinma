<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GlobalLinksNav.ascx.cs"
	Inherits="web_user_control_GlobalLinksNav" %>
<link type="text/css" href="../css/style.css" rel="stylesheet" />
<script language="javascript" src="../include/generalJSFunctions.js"></script>
<script type="text/javascript">
<!--	
	function ShowResults(usertype)
	{
		var searchstring = document.getElementById("GlobalLinksNav_tbSearch").value;
		var DDList = document.getElementById("GlobalLinksNav_ddlSearchOpt");
	    
		var dList = DDList.selectedIndex;
		var selValue = DDList.options[dList].value;

		if(trim(searchstring) != '')        
			//window.open('../../searchResults.aspx?searchstring=' + trim(searchstring) + '&usertype=' + usertype + '&searchType='+selValue , 'x', 'toolbar=no, statusbar = 1, menubar=no, width=450; height=650, top=150, left=325, resizable = 1');
			window.open('../../searchResults.aspx?searchstring=' + trim(searchstring) + '&usertype=' + usertype + '&searchType='+selValue , 'x', 'toolbar=no, status = 1, menubar=no, width=400, height=400, resizable = 1, scrollbars=1, left=200, top=50');
		else        
			//window.open('../../searchResults.aspx?searchstring=%' + '&usertype=' + usertype + '&searchType='+selValue , 'x', 'toolbar=no, statusbar = 1, menubar=no, width=450; height=650, top=150, left=325, resizable=1' );
			window.open('../../searchResults.aspx?searchstring=%' + '&usertype=' + usertype + '&searchType='+selValue , 'x', 'toolbar=no, status = 1, menubar=no, width=300, height=400, resizable=1, scrollbars=1, left=200, top=50' );
	}
//-->
</script>

<style type="text/css">
    .auto-style1 {
        width: 150px;
    }
</style>

<table border="0" cellpadding="0" cellspacing="0" width="100%" id="table2" >
	<tr>
		<td align="left" class="auto-style1">
			<img border="0" src="../../clients/<%= HttpContext.Current.Session["clientid"] %>/images/logo.jpg" style="margin-left:20px; margin-bottom:5px;" >
		</td>
		<td>
			&nbsp;
		</td>
		<td class="globalLinks" align="right" width="390px">
			<p>
                <table style="" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="text-align:left">
                    <asp:DropDownList ID="ddlSearchOpt" runat="server"  
                    style="margin:0px; padding:0px; width:160px;" EnableTheming="False" >
					<asp:ListItem Value="1">Bid Events</asp:ListItem>
					<asp:ListItem Value="2">Bid Reference No.</asp:ListItem>
					<asp:ListItem Value="3">Bid Event PR Number</asp:ListItem>
					<asp:ListItem Value="4">Auction Events</asp:ListItem>
					<asp:ListItem Value="5">Auction Reference No.</asp:ListItem>
					<asp:ListItem Value="6">Auction Event PR Number</asp:ListItem>
					<asp:ListItem Value="7">Product</asp:ListItem>
					<asp:ListItem Value="8">Supplier</asp:ListItem>
				    </asp:DropDownList></td>
                        <td style="text-align:right; width:185px;">
                            <asp:TextBox ID="tbSearch" runat="server" Height="20px" 
                    style="margin:0px; padding:0px; width:125px;" EnableTheming="False" />
                            <asp:Button ID="btnSearch" runat="server" UseSubmitBehavior="false" 
                    Text="Search" style="margin:0px; padding:0px;" EnableTheming="False" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"><asp:HyperLink ID="lnkAbout" runat="server" NavigateUrl="~/about.aspx" ForeColor="white">About Trans-Asia e-Sourcing System</asp:HyperLink>
				&nbsp;|&nbsp;				
				<asp:HyperLink ID="lnkHelp" runat="server" NavigateUrl="~/help.aspx" ForeColor="white">Help</asp:HyperLink>
				&nbsp;|&nbsp;				
				<asp:HyperLink ID="lnkFaqs" runat="server" NavigateUrl="~/faqs.aspx" ForeColor="white">FAQs</asp:HyperLink>
				&nbsp;|&nbsp;
				<asp:HyperLink ID="lnkLogout" runat="server" NavigateUrl="~/logout.aspx" ForeColor="white">Log Out</asp:HyperLink>	</td>
                    </tr>
                </table>
				
				
					
				<%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <div style="clear:both"></div>	--%>			
							
			</p>
		</td>
	</tr>
</table>
