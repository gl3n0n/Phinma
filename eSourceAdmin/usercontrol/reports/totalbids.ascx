<%@ Control Language="C#" AutoEventWireup="true" CodeFile="totalbids.ascx.cs" Inherits="usercontrol_reports_totalbids" %>
<%@ Register Assembly="Calendar" Namespace="CalendarControl" TagPrefix="cc1" %>
<table border="0" cellpadding="0" cellspacing="0" style="font-size: 11px; font-family: Arial; width: 100%">
    <tr>
        <td>
            <table style="width: 300px; font-size: 11px; font-family: Arial">
                <tr>
                    <td style="width: 60px">
                        Start Date</td>
                    <td>
                        <cc1:JSCalendar ID="clndrStartDate" runat="server" ImageURL="../calendar/img.gif" EnableViewState="true" ScriptsBasePath="../calendar/" DateFormat="MM/dd/yyyy" Width="178px" ReadOnly="false" MaxLength="10"
                            TabIndex="-1"></cc1:JSCalendar></td>
                </tr>
                <tr>
                    <td style="width: 60px; height: 16px;">
                        End Date</td>
                    <td style="height: 16px">
                        <cc1:JSCalendar ID="clndrEndDate" runat="server" ImageURL="../calendar/img.gif" EnableViewState="true" ScriptsBasePath="../calendar/" DateFormat="MM/dd/yyyy" Width="178px" ReadOnly="false" MaxLength="10"
                            TabIndex="-1"></cc1:JSCalendar>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="height: 10px;">
        </td>
    </tr>
    <tr>
        <td colspan="2" id="actions">
            <asp:LinkButton ID="lnkViewReport" runat="server" OnClick="lnkViewReport_Click" Width="100px">View Report</asp:LinkButton>
        </td>
    </tr>
</table>