<%@ Page Language="C#" AutoEventWireup="true" CodeFile="report_totalbidevents.aspx.cs" Inherits="web_purchasingscreens_report_totalbidevents" %>

<%@ Register Src="~/web/usercontrol/Purchasing/Purchasing_LeftNav_Bids.ascx" TagName="Purchasing_LeftNav_Reports" TagPrefix="uc1" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/WEB/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_TopNav_Reports" Src="~/web/usercontrol/Purchasing/Purchasing_TopNav_Reports.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Purchasing/Footer.ascx"%>
<%@ Register Assembly="Calendar" Namespace="CalendarControl" TagPrefix="cc1" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_ph.css" %>' />
    <script type="text/javascript" src="../include/util.js"></script>
</head>
<body style="height: 100%;">
    <div align="left">
        <form id="Form1" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
                <tr>
                    <td valign="top" height="137px">
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
                                    <EBid:TopDate runat="server" ID="TopDate" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="content">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="height: 100%">
                            <tr>
                                <td id="relatedInfo">
                                    <uc1:Purchasing_LeftNav_Reports ID="Purchasing_LeftNav_Reports1" runat="server" />
                                </td>
                                <td id="content">
                                    <div align="left">
                                        <br />
                                        <h1>
                                            Total Bid Events</h1>
                                        <div>
                                        <!-- contents here //-->


                                            <br />
                                            <table border="0" cellpadding="0" cellspacing="0" style="font-size: 11px; font-family: Arial; width: 100%">
                                                <tr>
                                                    <td valign="top" style="padding-top: 5px;">
                                                        Buyer</td>
                                                    <td>
                                                        <asp:ListBox ID="lbBuyers" runat="server" SelectionMode="Single" Width="300px" DataSourceID="sdsBuyers" DataTextField="BuyerFullName" DataValueField="BuyerId" Rows="1"></asp:ListBox><asp:SqlDataSource
                                                            ID="sdsBuyers" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetAllBuyers" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 113px;padding-top: 5px;" valign="top">
                                                        Date</td>
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
                                                        <asp:LinkButton ID="lnkShowDetails" runat="server" OnClick="lnkShowDetails_Click" Width="100px">Show Details</asp:LinkButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="height: 10px;">
                                                    </td>
                                                </tr>
                                                
                                                <asp:Panel ID="Panel1" runat="server">
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:GridView ID="gvBids" runat="server" SkinID="BidEvents" AllowPaging="True" AllowSorting="False"
                                                            AutoGenerateColumns="False" DataKeyNames="CompanyId" DataSourceID="dsTotalBidEvents"
                                                            OnRowCommand="gvBids_RowCommand" Width="450">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Company" InsertVisible="False" SortExpression="Company">
                                                                    <HeaderStyle HorizontalAlign="Center" Width="250" />
                                                                    <ItemTemplate>
                                                                        &nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Bind("Company") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Number of Bid Events" SortExpression="CntBidEvents">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="center" />
                                                                    <ItemTemplate>
                                                                        &nbsp;<asp:LinkButton ID="lnkCntBidEvents" runat="server" Text='<%# Bind("CntBidEvents") %>'
                                                                            CommandArgument='<%# Bind("CompanyId") %>' CommandName='<%# Bind("Company") %>'></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:SqlDataSource ID="dsTotalBidEvents" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                            SelectCommand="sp_GetBuyerCntApprovedBidEventsByCompany" SelectCommandType="StoredProcedure">
                                                            <SelectParameters>
                                                                <asp:SessionParameter Name="BuyerId" SessionField="totalbidevents_BuyerId" Type="Int32" />
                                                                <asp:SessionParameter Name="FromDate" SessionField="totalbidevents_FromDate" Type="DateTime" />
                                                                <asp:SessionParameter Name="ToDate" SessionField="totalbidevents_ToDate" Type="DateTime" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="height: 10px;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" id="actions1">
                                                        <asp:LinkButton ID="lnkViewReport" runat="server" OnClick="lnkViewReport_Click" Width="100px">View Report</asp:LinkButton>
                                                    </td>
                                                </tr>
                                                </asp:Panel>

                                                <tr>
                                                    <td colspan="2" style="height: 10px;">
                                                    </td>
                                                </tr>
                                            </table>


                                        <!-- contents here //-->
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td id="footer" height="50px">
                        <EBid:Footer runat="server" ID="Footer1" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
