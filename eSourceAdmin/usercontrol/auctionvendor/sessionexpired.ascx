<%@ Control Language="C#" AutoEventWireup="true" CodeFile="sessionexpired.ascx.cs" Inherits="web_usercontrol_auctionvendor_sessionexpired" %>
<%@ Register Src="auctionfooter.ascx" TagName="auctionfooter" TagPrefix="uc1" %>
<%@ Register Src="../TopDate.ascx" TagName="TopDate" TagPrefix="uc2" %>
<table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
    <tr>
        <td valign="top" height="24">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <uc2:TopDate ID="TopDate1" runat="server" />
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
                            Session Expired</h1>
                    </td>
                </tr>
                <tr>
                    <td valign="top" style="padding-left: 20px; padding-right: 10px">
                        
                    </td>
                </tr>
                <tr>
                    <td valign="top" colspan="2" style="padding-left: 20px; padding-right: 10px">
                        <br />
                        <p>
                            Your session has expired. Click <b><i>"Close"</i></b> to close this window.
                            <br />
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
                        <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="false" OnClientClick='window.close();'>Close</asp:LinkButton>
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
            &nbsp;
            <uc1:auctionfooter ID="Auctionfooter1" runat="server" />
        </td>
    </tr>
</table>
