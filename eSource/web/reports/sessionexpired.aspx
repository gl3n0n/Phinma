<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sessionexpired.aspx.cs" Inherits="web_onlineAuction_participateauctionevent" Theme="default" %>

<%@ Register Src="../usercontrol/auctionvendor/sessionexpired.ascx" TagName="sessionexpired" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
    <title>.:| Trans-Asia  eSourcing System | Participate Auction Event |:.</title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='../themes/<%= Session["configTheme"] %>/css/style_oa.css">    
</head>
<body style="height: 100%;">
    <div>
        <form runat="server">
            <uc1:sessionexpired ID="Sessionexpired1" runat="server" />            
        </form>
    </div>
</body>
</html>
