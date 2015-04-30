<%@ Page Language="C#" AutoEventWireup="true" CodeFile="awardedauctionitems.aspx.cs"
    Inherits="web_purchasing_screens_awardedAuctionItems" %>

<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/WEB/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_TopNav_Auct" Src="~/web/usercontrol/Purchasing/Purchasing_TopNav_Auct.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_LeftNav" Src="~/web/usercontrol/Purchasing/Purchasing_LeftNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_LeftNav_Not" Src="~/web/usercontrol/Purchasing/Purchasing_LeftNav_Auct_Not.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Purchasing/Footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title>.:| Trans-Asia  eSourcing System | Awarded Auctions Events |:.</title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_ph.css" %>' />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../include/awardedbiditems.js"></script>

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
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
                            <tr>
                                <td id="relatedInfo">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <EBid:Purchasing_LeftNav runat="server" ID="Purchasing_LeftNav" />
                                              
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td id="content">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page0">
                                        <tr>
                                            <td valign="top">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td id="content0">
                                                            <h1>
                                                                <br />
                                                                Awarded Auction Items</h1>
                                                            <div align="left">
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%" id="pageDetails0">
                                                                    <tr>
                                                                        <th>
                                                                            Auction Event Name</th>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <input id="tbSearch" type="text" name="txtPost" runat="server" onkeypress="doSearch()"></td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="pageDetails1">
                                                                <tr>
                                                                    <th>
                                                                        Sub-category</th>
                                                                    <th>
                                                                        Date Awarded</th>
                                                                    <th>
                                                                        Company</th>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:DropDownList ID="ddlSubCat" runat="server" Width="98px">
                                                                        </asp:DropDownList></td>
                                                                    <td>
                                                                        <div align="left">
                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table4">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        <select name=".bmon" id="ddlMonth" runat="server" onchange="setDay()">
                                                                                            <option value="0" selected>[Month]</option>
                                                                                            <option value="1">Jan </option>
                                                                                            <option value="2">Feb </option>
                                                                                            <option value="3">Mar</option>
                                                                                            <option value="4">Apr</option>
                                                                                            <option value="5">May</option>
                                                                                            <option value="6">Jun</option>
                                                                                            <option value="7">Jul</option>
                                                                                            <option value="8">Aug </option>
                                                                                            <option value="9">Sep </option>
                                                                                            <option value="10">Oct </option>
                                                                                            <option value="11">Nov </option>
                                                                                            <option value="12">Dec </option>
                                                                                        </select>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <input id="tbDay" runat="server" onkeypress="checkMonth()" onblur="checkDay()" onfocus="this.value=''"
                                                                                            maxlength="2" size="2" value="dd" name=".bday" autocomplete="off" style="font-family: Arial, Helvetica, sans-serif;
                                                                                            font-size: 12px">
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <input id="tbYear" runat="server" onblur="checkYear()" onfocus="this.value=''" maxlength="4"
                                                                                            size="4" value="yyyy" name=".byear" autocomplete="off" style="font-family: Arial, Helvetica, sans-serif;
                                                                                            font-size: 12px">
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:DropDownList ID="ddlCompanies" runat="server">
                                                                        </asp:DropDownList></td>
                                                                </tr>
                                                            </table>
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                                <tr>
                                                                    <td>
                                                                        <asp:LinkButton ID="lbSearch" runat="server" Text="Search" OnClick="lbSearch_Click"></asp:LinkButton></td>
                                                                </tr>
                                                            </table>
                                                            <p>
                                                                &nbsp;</p>
                                                            <div align="left" id="d3Search" runat="server" visible="false">
                                                                <h3>
                                                                    Search Results</h3>
                                                            </div>
                                                            <br />
                                                            <p>
                                                                <asp:Label ID="lblDataIsEmpty" runat="server" /></p>
                                                            <asp:GridView runat="server" ID="gvSearchedItems" AllowPaging="True" PageSize="20"
                                                                AutoGenerateColumns="false" OnRowCommand="gvBids_RowCommand" OnPageIndexChanging="gridView_PageIndexChanging"
                                                                CssClass="pageDetails" BorderColor="gainsboro">
                                                                <PagerSettings Mode="NumericFirstLast" FirstPageText="First" LastPageText="Last"
                                                                    NextPageText="Next" PreviousPageText="Prev" />
                                                                <PagerStyle HorizontalAlign="Right" />
                                                                <AlternatingRowStyle CssClass="evenCells" />
                                                                <HeaderStyle CssClass="pageDetails_th" ForeColor="#FFFFFF" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Bid Event">
                                                                        <ItemTemplate>
                                                                            &nbsp;<asp:LinkButton CommandName="Select" ID="lbBidItems" runat="server" Text='<%# Bind("ItemDesc") %>'
                                                                                CommandArgument='<%# Bind("AuctionRefNo") %>'></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Item Description">
                                                                        <ItemTemplate>
                                                                            &nbsp;<asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Date Awarded">
                                                                        <HeaderStyle HorizontalAlign="center" />
                                                                        <ItemStyle Width="100px" HorizontalAlign="center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDate" runat="server" Text='<%# Bind("DateAwarded1") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Company">
                                                                        <HeaderStyle HorizontalAlign="center" />
                                                                        <ItemStyle Width="100px" HorizontalAlign="center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCompany" runat="server" Text='<%# Bind("VendorName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <p>
                                        &nbsp;
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
