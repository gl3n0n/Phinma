<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bidprintpage.aspx.cs" Inherits="web_purchasingscreens_bidprintpage" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<script language="javascript" type="text/javascript" src="../include/util.js" />

<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style_ph.css" %>' rel="stylesheet" type="text/css" />
    <title>.:| Trans-Asia  eSourcing System | Print Bid Event Details Page |:.</title>
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
                        <asp:Label runat="server" ID="lblBidEventType"></asp:Label>
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
                                Bid reference Number:</td>
                            <td class="value">
                                <asp:Label runat="server" ID="lblBidReferenceNumber"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="133">
                                Sub Category:</td>
                            <td class="value">
                                <asp:Label runat="server" ID="lblSubCategory"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="133">
                                Bid Submission Deadline:</td>
                            <td class="value">
                                <asp:Label runat="server" ID="lblBidSubmissionDeadline"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="133">
                                Bid Item Description</td>
                            <td class="value">
                                <asp:Label runat="server" ID="lblBidItemDescription"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="133">
                                Deliver To:
                            </td>
                            <td class="value">
                                <asp:Label runat="server" ID="lblDeliverTo"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="132">
                                Incoterm</td>
                            <td class="value">
                                <asp:Label runat="server" ID="lblIncoterm"></asp:Label></td>
                        </tr>
                    </table>
                    <br />
                    <asp:GridView AutoGenerateColumns="False" runat="server" ID="gvBidItemDetails" CssClass="printItemDetails"
                        BorderColor="gray" BorderWidth="1px" DataSourceID="dsBidItemDetails">
                        <Columns>
                            <asp:TemplateField HeaderText="Item">
                                <ItemStyle CssClass="itemDetails_td" Width="80px" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblItem" Text='<%#Bind("Item") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="printItemDetails_th" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    &nbsp;<asp:Label runat="server" ID="lblDescription" Text='<%#Bind("DetailDesc") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="valueGridItem" Width="200px" />
                                <HeaderStyle CssClass="printItemDetails_th" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    &nbsp;<asp:Label runat="server" ID="lblQuantity" Text='<%#Bind("Qty") %>'></asp:Label>
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
                            <asp:TemplateField HeaderText="Delivery Date">
                                <ItemTemplate>
                                    &nbsp;<asp:Label runat="server" ID="lblDeliveryDate" Text='<%#Bind("DeliveryDate1") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="valueGridItemRight" Width="80px" />
                                <HeaderStyle CssClass="printItemDetails_th_right" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="dsBidItemDetails" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                        SelectCommand="s3p_EBid_GetBidItemDetails" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="bidRefNo" QueryStringField="BidRefNo" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <br />
                    <asp:GridView AutoGenerateColumns="False" runat="server" ID="gvInvitedSuppliers"
                        CssClass="printItemDetails" BorderColor="Gray" BorderWidth="1px" DataSourceID="dsSuppliersList">
                        <Columns>
                            <asp:TemplateField HeaderText="Invited Suppliers">
                                <ItemStyle CssClass="valueGridItem" Width="500px" BackColor="White" />
                                <HeaderStyle CssClass="printItemDetails_th" />
                                <ItemTemplate>
                                    &nbsp;<asp:Label runat="server" ID="lblVendors" Text='<%#Bind("Supplier") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="dsSuppliersList" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                        SelectCommand="s3p_EBid_GetSuppliers" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="bidRefno" QueryStringField="BidRefNo" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <br />
                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                        <tr>
                            <td align="center">
                                <asp:LinkButton runat="server" ID="lnkPrint" OnClientClick="window.print()">Print</asp:LinkButton>
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
