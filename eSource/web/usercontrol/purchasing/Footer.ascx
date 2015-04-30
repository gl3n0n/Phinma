<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Footer.ascx.cs" Inherits="web_user_control_Footer" %>
<link type="text/css" href="../css/style.css" rel="stylesheet" />



<div id="footername">
        	<div id="footernamenav">
            	 <ul> 
            	    <li> 	<asp:HyperLink runat="server" ID="HyperLink1" NavigateUrl="~/web/purchasingscreens/index.aspx">Home |</asp:HyperLink> </li> 
            	    &nbsp;
            	    <li>	 <asp:HyperLink runat="server" ID="HyperLink2" NavigateUrl="~/web/purchasingscreens/bids.aspx">Bids |</asp:HyperLink></li> 
            	    &nbsp;
            	    <li>	<asp:HyperLink runat="server" ID="HyperLink3" NavigateUrl="~/web/purchasingscreens/auctions.aspx">Auctions |</asp:HyperLink> </li> 
            	    &nbsp;
            	    <li> 	<asp:HyperLink runat="server" ID="HyperLink4" NavigateUrl="~/web/purchasingscreens/report_awardedbiditemsbyitem.aspx">Reports |</asp:HyperLink> </li> &nbsp;
                     <li style="color:#FFF">  11th Floor, PHINMA Plaza, 39 Plaza Drive, Rockwell Center, Makati City, 1200, Philippines | Tel: +63 2 8700100 | Fax: +63 2 8700484 </li>
       	       </ul>
            </div>
        </div>

<%--<ul> 
            	    <li> 	<asp:HyperLink runat="server" ID="HyperLink1" NavigateUrl="~/web/purchasingscreens/index.aspx">Home |</asp:HyperLink> </li> 
            	    &nbsp;
            	    <li>	 <asp:HyperLink runat="server" ID="HyperLink2" NavigateUrl="~/web/purchasingscreens/bids.aspx">Bids |</asp:HyperLink></li> 
            	    &nbsp;
            	    <li>	<asp:HyperLink runat="server" ID="HyperLink3" NavigateUrl="~/web/purchasingscreens/auctions.aspx">Auctions |</asp:HyperLink> </li> 
            	    &nbsp;
            	    <li> 	<asp:HyperLink runat="server" ID="HyperLink4" NavigateUrl="~/web/purchasingscreens/report_awardedbiditemsbyitem.aspx">Reports |</asp:HyperLink> </li> &nbsp;
                     <li style="color:#FFF">  11th Floor, PHINMA Plaza, 39 Plaza Drive, Rockwell Center, Makati City, 1200, Philippines | Tel: +63 2 8700100 | Fax: +63 2 8700448 </li>
       	       </ul>--%>

<%--<asp:HyperLink ID="lnkAbout" runat="server" NavigateUrl="~/about.aspx" ForeColor="white">About Trans-Asa E-Sourcing System</asp:HyperLink>
				&nbsp;|&nbsp;				
				<asp:HyperLink ID="lnkHelp" runat="server" NavigateUrl="~/help.aspx" ForeColor="white">Help</asp:HyperLink>
				&nbsp;|&nbsp;				
				<asp:HyperLink ID="lnkFaqs" runat="server" NavigateUrl="~/faqs.aspx" ForeColor="white">FAQs</asp:HyperLink>
				&nbsp;|&nbsp;
				<asp:HyperLink ID="lnkLogout" runat="server" NavigateUrl="~/logout.aspx" ForeColor="white">Log Out</asp:HyperLink>	--%>
<span style="font-size:9px; font-weight:normal;">
E-Sourcing Portal System &nbsp;&nbsp;&bull;&nbsp;&nbsp;
Copyright © 2008 Trans-Asia All rights reserved.&bnsp;&nbsp;&bull;&nbsp;&nbsp;
<asp:HyperLink ID="hlTerms" runat="server">Terms of Use</asp:HyperLink>
- <asp:HyperLink ID="hlPrivacy" runat="server">Privacy Policy</asp:HyperLink><br />
    </span>
