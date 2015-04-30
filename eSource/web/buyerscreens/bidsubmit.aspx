<%@ Page Language="c#" Inherits="EBid.WEB.buyer_screens.bidSubmit" CodeFile="bidSubmit.aspx.cs"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register TagPrefix="EBid" TagName="LeftNav" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNavBids" Src="~/WEB/usercontrol/Buyer/TopNavBids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNav2" Src="~/WEB/usercontrol/Buyer/TopNav2.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<%@ Register TagPrefix="EBid" TagName="CommentBox" Src="~/web/usercontrol/CommentBox.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_buyer.css" %>' />
</head>
<body style="height: 100%;">
    <div align="left">
        <form id="Form1" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
                <tr>
                    <td valign="top">
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
                                    <EBid:TopNav2 ID="TopNav2" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr height='100%'>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" height='100%'>
                            <tr>
                                <td id="relatedInfo">
                                    <div align="left">
                                        <EBid:LeftNav ID="LeftNav" runat="server" />
                                    </div>
                                    <div>
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table3">
                                            <tr>
                                                <td>                                                    
                                                    <br />
                                                    <EBid:CommentBox runat="server" ID="CommentBox"></EBid:CommentBox>                                                    
                                                </td>
                                            </tr>
                                        </table>                                        
                                    </div>
                                </td>
                                <td id="content">                                    
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page0">
                                        <tr>
                                            <td valign="top">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%" id="Table2">
                                                    <tr>
                                                        <td id="content0">
                                                            <h1>
                                                                <br />
                                                                Bid Submission Confirmation</h1>
                                                            <p>
                                                                You have successfully submitted the following bid items</p>
                                                            <div align="left">
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails">
                                                                    <tr>
                                                                        <th>
                                                                            &nbsp;Bid Item&nbsp;<asp:Label runat="server" ID="lblBidRefNo2"></asp:Label></th>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <div align="left">
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails">
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
                                                                            <asp:Label runat="server" ID="lblBidReferenceNo"></asp:Label></td>
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
                                                                            Deliver to:
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
                                                            </div>
                                                            <div>
                                                                <asp:GridView AutoGenerateColumns="false" runat="server" ID="gvBidItemDetails" CssClass="itemDetails">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="itemDetails_th" ItemStyle-CssClass="itemDetails_td">
                                                                            <ItemStyle CssClass="itemDetails_td" />
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblItem" Text='<%#Bind("SKU") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Description" ItemStyle-CssClass="valueGridItem" HeaderStyle-CssClass="itemDetails_th">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblDescription" Text='<%#Bind("DetailDesc") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Quantity" ItemStyle-CssClass="valueGridItem" HeaderStyle-CssClass="itemDetails_th">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblQuantity" Text='<%#Bind("Qty") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Unit Of Measure" ItemStyle-CssClass="valueGridItem"
                                                                            HeaderStyle-CssClass="itemDetails_th">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblUnitOfMeasure" Text='<%#Bind("UnitOfMeasure") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Delivery Date" ItemStyle-CssClass="valueGridItemRight"
                                                                            HeaderStyle-CssClass="itemDetails_th_right">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblDeliveryDate" Text='<%#Bind("DeliveryDate1") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                            <div align="left">
                                                                <asp:GridView AutoGenerateColumns="false" runat="server" ID="gvFileAttachments" CssClass="itemDetails">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="File Attachments" ItemStyle-CssClass="valueGridItem"
                                                                            HeaderStyle-CssClass="itemDetails_th">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblDescription" Text='<%#Bind("FILES") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                            <div align="left">
                                                                <asp:GridView AutoGenerateColumns="false" runat="server" ID="gvSuppliers" CssClass="itemDetails">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Invited Suppliers" ItemStyle-CssClass="valueGridItem"
                                                                            HeaderStyle-CssClass="itemDetails_th">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblDescription" Text='<%#Bind("SUPPLIER") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                            <br />
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                                <tr>
                                                                    <td>
                                                                        <asp:LinkButton runat="server" ID="btnOk" OnClick="btnOk_Click">OK</asp:LinkButton></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td id="footer">
                        <EBid:Footer runat="server" ID="Footer" />
                        <input type="hidden" runat="server" id="hdnBidRefNo" name="hdnBidRefNo" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
