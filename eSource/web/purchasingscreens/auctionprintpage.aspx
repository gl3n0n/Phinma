<%@ Page Language="C#" AutoEventWireup="true" CodeFile="auctionprintpage.aspx.cs"
    Inherits="web_purchasingscreens_auctionprintpage" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<script language="javascript" type="text/javascript" src="../include/util.js" />

<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style_ph.css" %>' rel="stylesheet" type="text/css" />
    <title>.:| Trans-Asia  eSourcing System | Print Auction Event Details Page |:.</title>
</head>
<body onload="SetStatus()">
    <form id="form1" runat="server">
        <div align="left">
            &nbsp;<img border="0" src="../images/logo.jpg" width="151" height="63">
        </div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="center" valign="top" height="50px">
                    <h1>
                        <br />
                        <asp:Label runat="server" ID="lblAuctEventType"></asp:Label>
                        &nbsp;Details
                    </h1>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table border="1" cellpadding="0" cellspacing="0" width="450px" id="printItemDetails"
                        height="85">
                        <tr>
                            <th colspan="2">
                                Details</tr>
                        <tr>
                            <td width="133">
                                Company:</td>
                            <td class="value">
                                <asp:Label runat="server" ID="lblCompany"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="133">
                                Requestor:</td>
                            <td class="value">
                                <asp:Label runat="server" ID="lblRequestor"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="133">
                                PR Number:</td>
                            <td class="value">
                                <asp:Label runat="server" ID="lblPRNumber"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="133">
                                PR Date</td>
                            <td class="value">
                                <asp:Label runat="server" ID="lblPRDate"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="133">
                                Group/Department/Section</td>
                            <td class="value">
                                <asp:Label runat="server" ID="lblGroup"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="133">
                                Reference Number:</td>
                            <td class="value">
                                <asp:Label runat="server" ID="lblAuctionReferenceNumber"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="133">
                                Sub Category:</td>
                            <td class="value">
                                <asp:Label runat="server" ID="lblSubCategory"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="133">
                                Delivery Date:</td>
                            <td class="value">
                                <asp:Label runat="server" ID="lblDeliveryDate"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="133">
                                Item Description:</td>
                            <td class="value">
                                <asp:Label runat="server" ID="lblItemDesc"></asp:Label></td>
                        </tr>
                    </table>
                    <br />
                    <table border="1" cellpadding="0" cellspacing="0" width="450px" id="printItemDetails1"
                        height="85">
                        <tr>
                            <th colspan="2">
                                Auction Event Details</tr>
                        <tr>
                            <td width="25%">
                                Auction Type:</td>
                            <td class="value">
                                <asp:Label runat="server" ID="lblAuctionType"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="25%">
                                Auction Confirmation Deadline:</td>
                            <td class="value">
                                <asp:Label runat="server" ID="lblAuctionDeadline"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="25%">
                                Auction Event Date:</td>
                            <td class="value">
                                <asp:Label runat="server" ID="lblAuctionDate"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="25%">
                                Auction Start Time:</td>
                            <td class="value">
                                <asp:Label runat="server" ID="lblAuctionStartTime"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="25%">
                                Auction End Time:</td>
                            <td class="value">
                                <asp:Label runat="server" ID="lblAuctionEndTime"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="25%">
                                Bid Currency</td>
                            <td class="value">
                                <asp:Label runat="server" ID="lblBidCurrency"></asp:Label></td>
                        </tr>
                    </table>
                    <br />
                    <asp:GridView AutoGenerateColumns="False" runat="server" ID="gvAuctionItemDetails"
                        CssClass="printItemDetails" BorderColor="gray" BorderWidth="1px" DataSourceID="dsAuctionItems">
                        <Columns>
                            <asp:TemplateField HeaderText="Item">
                                <ItemTemplate>
                                    &nbsp;<asp:Label runat="server" ID="lblItem" Text='<%#Bind("PItemName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="valueGridItem" Width="100px" />
                                <HeaderStyle CssClass="printItemDetails_th" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemStyle Width="200px" CssClass="valueGridItem" />
                                <ItemTemplate>
                                    &nbsp;<asp:Label runat="server" ID="lblDescription" Text='<%#Bind("Description") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="printItemDetails_th" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    &nbsp;<asp:Label runat="server" ID="lblQuantity" Text='<%#Bind("Quantity") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="valueGridItem" Width="80px" />
                                <HeaderStyle CssClass="printItemDetails_th" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit Of Measure">
                                <ItemTemplate>
                                    &nbsp;<asp:Label runat="server" ID="lblUnitOfMeasure" Text='<%#Bind("UnitOfMeasure") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="valueGridItem" Width="65px" />
                                <HeaderStyle CssClass="printItemDetails_th" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="dsAuctionItems" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                        SelectCommand="s3p_EBid_GetAuctionItems" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="auctionRefno" QueryStringField="AuctionRefNo" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <br />
                    <asp:GridView AutoGenerateColumns="False" runat="server" ID="gvInvitedSuppliers"
                        CssClass="printItemDetails" Width="450px" BorderColor="Gray" BorderWidth="1px" DataSourceID="dsAuctionSuppliers">
                        <Columns>
                            <asp:TemplateField HeaderText="Invited Suppliers">
                                <ItemStyle CssClass="valueGridItem" BackColor="White" />
                                <HeaderStyle CssClass="printItemDetails_th" />
                                <ItemTemplate>
                                    &nbsp;<asp:Label runat="server" ID="lblVendors" Text='<%#Bind("Supplier") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="dsAuctionSuppliers" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                        SelectCommand="s3p_EBid_GetAuctionItemSuppliers" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="auctionRefno" QueryStringField="AuctionRefNo" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
           <%-- <tr>
                <td>
                    <iframe src="../../rules.htm" width="100%" height="600px"></iframe>
                </td>
            </tr>--%>
            <tr>
                <td align="center">
                    <br />
                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                        <tr>
                            <td align="center">
                                <asp:LinkButton runat="server" ID="lnkPrint" OnClientClick="window.print();">Print</asp:LinkButton>
                                <asp:LinkButton runat="server" ID="lnkClose" OnClientClick="window.close()">Cancel</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>            
        </table>        
    </form>
</body>
</html>
