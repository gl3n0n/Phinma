<%@ Control Language="C#" AutoEventWireup="true" CodeFile="awardedbiditemsbycategory.ascx.cs" Inherits="web_usercontrol_reports_awardedbiditemsbycategory" %>
<%@ Register Assembly="Calendar" Namespace="CalendarControl" TagPrefix="cc1" %>
<table border="0" cellpadding="0" cellspacing="0" style="font-size: 11px; font-family: Arial; width: 100%">
    <tr>
        <td valign="top" style="padding-top: 5px;">
            Vendor(s)</td>
        <td>
            <asp:ListBox ID="lbVendors" runat="server" SelectionMode="Multiple" Width="300px" DataSourceID="sdsVendors" DataTextField="VendorName" DataValueField="VendorId" Rows="6"></asp:ListBox><asp:SqlDataSource
                ID="sdsVendors" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetVendors" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
        </td>
    </tr>
    <tr>
        <td valign="top" style="padding-top: 5px;">
            Category/Categories</td>
        <td>
            <asp:ListBox ID="lbCategories" runat="server" SelectionMode="Multiple" Width="300px" DataSourceID="sdsCategories" DataTextField="CategoryName" DataValueField="CategoryId" Rows="6"></asp:ListBox><asp:SqlDataSource
                ID="sdsCategories" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="s3p_EBid_GetAllProductCategory" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
        </td>
    </tr>
    <tr>
        <td valign="top" style="padding-top: 5px;">
            Company</td>
        <td>
            <asp:ListBox ID="lbCompanies" runat="server" SelectionMode="Multiple" Width="300px" DataSourceID="sdsCompanies" DataTextField="Company" DataValueField="CompanyId" Rows="3"></asp:ListBox><asp:SqlDataSource
                ID="sdsCompanies" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="s3p_EBid_QueryCompanies" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
        </td>
    </tr>
    <tr>
        <td style="width: 113px;padding-top: 5px;" valign="top">
            Date Awarded</td>
        <td>
            <table style="width: 272px; font-size: 11px; font-family: Arial">
                <tr>
                    <td style="width: 44px">
                        From</td>
                    <td>
                        <cc1:JSCalendar ID="clndrStartDate" runat="server" ImageURL="../../calendar/img.gif" EnableViewState="true" ScriptsBasePath="../../calendar/" DateFormat="MM/dd/yyyy" Width="178px" ReadOnly="false" MaxLength="10"
                            TabIndex="-1"></cc1:JSCalendar></td>
                </tr>
                <tr>
                    <td style="width: 44px; height: 16px;">
                        To</td>
                    <td style="height: 16px">
                        <cc1:JSCalendar ID="clndrEndDate" runat="server" ImageURL="../../calendar/img.gif" EnableViewState="true" ScriptsBasePath="../../calendar/" DateFormat="MM/dd/yyyy" Width="178px" ReadOnly="false" MaxLength="10"
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