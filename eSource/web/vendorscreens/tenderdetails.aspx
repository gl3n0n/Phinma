<%@ Page Language="c#" Inherits="EBid.web.vendor_screens.tenderDetails" CodeFile="tenderdetails.aspx.cs"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="../usercontrol/bids/biditemdetails.ascx" TagName="biditemdetails" TagPrefix="uc5" %>
<%@ Register Src="../usercontrol/bids/buyer_bidtenderdetails.ascx" TagName="buyer_bidtenderdetails" TagPrefix="uc6" %>
<%--<%@ Register Src="../usercontrol/bids/biddetails_attachments.ascx" TagName="biddetails_attachments" TagPrefix="uc3" %>--%>
<%@ Register Src="../usercontrol/bids/biddetails_w_bidtenderdetails.ascx" TagName="biddetails_w_bidtenderdetails" TagPrefix="uc4" %>
<%@ Register Src="~/web/usercontrol/auctionvendor/vendor_attachment_download.ascx" TagName="vendor_attachment_download" TagPrefix="uc3" %>

<%@ Register Src="../usercontrol/bids/bidtenderdetails.ascx" TagName="bidtenderdetails" TagPrefix="uc2" %>
<%@ Register Src="../usercontrol/commentlist_tender.ascx" TagName="commentlist_tender" TagPrefix="uc2" %>
<%@ Register Src="../usercontrol/bids/biddetails_details.ascx" TagName="biddetails_details" TagPrefix="uc1" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Vendor/Footer.ascx"  %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/web/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_TopNav_Bids" Src="~/web/usercontrol/Vendor/Vendor_TopNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_LeftNav_Bids" Src="~/web/usercontrol/Vendor/Vendor_LeftNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TendersCommentBox" Src="~/web/usercontrol/TendersCommentBox.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='../themes/<%= Session["configTheme"] %>/css/style_v.css' />
    <script type="text/javascript" src="../include/customValidation.js"></script>
    <script type="text/javascript" src="../include/util.js"></script>
    <script type="text/javascript" src="../include/events.js"></script>
    <script type="text/javascript" src="../include/generalJSFunctions.js"></script>

    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
    <style type="text/css">
        .rwDeliveryCost {
        display:none;
        }
    </style>
</head>
<body onLoad="FocusOn('dvBidTender_txtAmount');">
    <div align="left">
        <form runat="server" id="frmSubmitTender" defaultbutton="btnSubmit">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
                <tr>
                    <td valign="top" height="137px">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <div align="left" id="masthead">
                                        <EBid:GlobalLinksNav ID="GlobalLinksNav" runat="server" />
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
                                                <EBid:Vendor_LeftNav_Bids runat="server" ID="Vendor_LeftNav_Bids" />
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
	                                        <td>
		                                        <h2>Comments</h2>
	                                        </td>
                                        </tr>
                                        <tr>
	                                        <td>
		                                        <uc2:commentlist_tender ID="Commentlist_tender1" runat="server" />
	                                        </td>
                                        </tr>
                                    </table>
                                </td>
                                <td id="content">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table9">
                                        <tr>
                                            <td id="content0">
                                                <div align="left">
                                                    <h1>
                                                        <br />
                                                        Bid Tender Creation / Submission</h1>
                                                    <p>
                                                        Fill up the fields below then click <b>"Submit"</b> to submit your bid tender or <b>"Drafts"</b> to save and modify later.
                                                    </p>
                                                     <asp:Panel ID="pnlTenderDetails" runat="server" Width="100%" Visible="false">
                                                        <p>
                                                            <uc5:biditemdetails ID="Biditemdetails1" runat="server" />
                                                            <uc6:buyer_bidtenderdetails ID="Buyer_bidtenderdetails1" runat="server" />
                                                        </p> 
                                                   </asp:Panel>                                                    
                                                   <asp:Panel ID="pnlEditTenderDetails" runat="server" Width="100%" Visible="false"> 
                                                   <p> 
                                                        &nbsp;<uc2:bidtenderdetails ID="dvBidtenderdetails" runat="server" />     
                                                        <p>
                                                            <br />
                                                            <style type="text/css">
                                                                .hiddenRow {
                                                                    display:none;
                                                                }
                                                            </style>
                                                            <asp:DetailsView ID="dvBidTender" runat="server" AllowPaging="True" AutoGenerateRows="False" DataSourceID="dsTenderDetails" OnDataBound="dvBidTender_DataBound" SkinID="BidDetails" Width="100%">
                                                                <HeaderTemplate>
                                                                    SKU :
                                                                    <asp:Label ID="Label51" runat="server" Text='<%# Bind("SKU")%>'></asp:Label>
                                                                    &nbsp;-&nbsp;
                                                                    <asp:Label ID="lblItemDesc" runat="server" Text='<%#Bind("DetailDesc") %>'></asp:Label>
                                                                </HeaderTemplate>
                                                                <Fields>
                                                                    <asp:TemplateField>
                                                                    <HeaderStyle Width="120px" Height="20px" />
                                                                    <HeaderTemplate>
                                                                        &nbsp;<asp:Label ID="lblQuantity1" runat="server" Text="Quantity"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle BackColor="White" />
                                                                    <ItemTemplate>
                                                                        &nbsp;&nbsp;<asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("Qty")%>'></asp:Label>
                                                                        &nbsp;<asp:Label ID="lblUOM" runat="server" Text='<%# Bind("UnitOfMeasure")%>'></asp:Label>(s)
                                                                        <asp:HiddenField ID="hdnQuantity" runat="server" Value='<%# Bind("Qty")%>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        &nbsp;<asp:Label ID="lbl2" runat="server" Text="Unit Price (ex-VAT)"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle BackColor="White" />
                                                                    <ItemTemplate>
                                                                        &nbsp;
                                                                        <asp:TextBox runat="server" ID="txtAmount" Width="100" MaxLength="15" Text='<%# GetWholeNumberPart(Eval("Amount").ToString()) %>'></asp:TextBox>.
                                                                        <asp:TextBox runat="server" ID="txtAmountCents" Width="20" MaxLength="2" Text='<%# GetDecimalPart(Eval("Amount").ToString()) %>'></asp:TextBox><asp:Label ID="lblAmount" runat="server" Text="" ForeColor="Red" Font-Bold Font-Size="11px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle Width="120px" Height="20px" />
                                                                    <HeaderTemplate>
                                                                        &nbsp;<asp:Label ID="lbl11" runat="server" Text="Sub Total Price"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle BackColor="White" />
                                                                    <ItemTemplate>
                                                                        &nbsp;<asp:TextBox runat="server" ID="txtSubTotalPrice" Width="200px" ReadOnly="false" TabIndex="-1" BorderStyle="None" Font-Size="14px" Font-Bold="true" ForeColor="Black" onkeypress="return false;" onkeydown="return false;"
                                                                            onkeyup="return false;" Style="cursor: default; padding-top: 3px;">0.00</asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        &nbsp;<asp:Label ID="lbl3" runat="server" Text="Discount Per Unit Price"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle BackColor="White" />
                                                                    <ItemTemplate>
                                                                        &nbsp;
                                                                        <asp:TextBox runat="server" ID="txtDiscount" Width="100" MaxLength="15" Text='<%# GetWholeNumberPart(Eval("Discount").ToString()) %>'></asp:TextBox>.
                                                                        <asp:TextBox runat="server" ID="txtDiscountCents" Width="20" MaxLength="2" Text='<%# GetDecimalPart(Eval("Discount").ToString()) %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        &nbsp;<asp:Label ID="lbl31" runat="server" Text="Total % Discount"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle BackColor="White" />
                                                                    <ItemTemplate>
                                                                        &nbsp;
                                                                        <asp:TextBox runat="server" ID="txtTotalBidTenderDiscount" Width="70px" ReadOnly="false" TabIndex="-1" BorderStyle="None" Font-Size="14px" Font-Bold="true" ForeColor="Black" onkeypress="return false;" 
                                                                            onkeydown="return false;" onkeyup="return false;" Style="cursor: default; padding-top: 3px;">0.00</asp:TextBox>%
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-CssClass="rwDeliveryCost" ItemStyle-CssClass="rwDeliveryCost"   >
                                                                    <HeaderTemplate>
                                                                        &nbsp;<asp:Label ID="lbl4" runat="server" Text="Delivery Cost"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle BackColor="White" />
                                                                    <ItemTemplate>
                                                                        &nbsp;
                                                                        <asp:TextBox runat="server" ID="txtDeliveryCost" Width="100" MaxLength="15" Text='0'></asp:TextBox>.
                                                                        <asp:TextBox runat="server" ID="txtDeliveryCostCents" Width="20" MaxLength="2" Text='00'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle Width="120px" Height="20px" />
                                                                    <HeaderTemplate>
                                                                        &nbsp;<asp:Label ID="lbl5" runat="server" Text="Total Bid Tender Price"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle BackColor="White" />
                                                                    <ItemTemplate>
                                                                        &nbsp;<asp:TextBox runat="server" ID="txtTotalBidTenderPrice" Width="250px" ReadOnly="false" TabIndex="-1" BorderStyle="None" Font-Size="14px" Font-Bold="true" ForeColor="Black" onkeypress="return false;"
                                                                            onkeydown="return false;" onkeyup="return false;" Style="cursor: default; padding-top: 3px;">0.00</asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        &nbsp;<asp:Label ID="lbl41" runat="server" Text="Incoterm"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle BackColor="White" />
                                                                    <ItemTemplate>
                                                                        &nbsp;
                                                                        <%--<asp:TextBox runat="server" ID="txtIncoterm" Width="100" MaxLength="15" Text='<%# Bind("Incoterm")%>'></asp:TextBox>--%>
                                                                        
																		<asp:DropDownList runat="server" ID="txtIncoterm" Width="250px" DataSourceID="dsIncoterm" DataTextField="Incoterm" DataValueField="Id">
																		</asp:DropDownList>
                                                            <asp:SqlDataSource ID="dsIncoterm" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="s3p_EBid_GetIncoterm1" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        &nbsp;<asp:Label ID="lbl42" runat="server" Text="Currency"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle BackColor="White" />
                                                                    <ItemTemplate>
                                                                        &nbsp;
                                                                        <%--<asp:Dropdownlist runat="server" ID="txtCurrency" Width="100"><asp:Dropdownlist>--%>
                                                                            <asp:DropDownList runat="server" ID="txtCurrency" Width="250px" DataSourceID="dsCurrencies" DataTextField="Currency" DataValueField="IdRate">
																		</asp:DropDownList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        &nbsp;<asp:Label ID="lblPaymentTerms" runat="server" Text="Payment terms"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle BackColor="White" />
                                                                    <ItemTemplate>
                                                                        &nbsp;
                                                                        <%--<asp:TextBox ID="txtPaymentTerms" runat="server" Text='<%# Bind("PaymentTerms")%>' Width="200px" MaxLength="250"></asp:TextBox>--%><asp:DropDownList runat="server" ID="txtPaymentTerms" Width="250px" DataSourceID="dsPaymentTerms" DataTextField="PaymentTerm" DataValueField="ID">
																		</asp:DropDownList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        &nbsp;<asp:Label ID="lblLeadTime" runat="server" Text="Lead Time"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle BackColor="White" />
                                                                    <ItemTemplate>
                                                                        &nbsp;<asp:TextBox ID="txtLeadTime" runat="server" Text='<%# Bind("LeadTime")%>' Width="200px" MaxLength="250"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        &nbsp;<asp:Label ID="lbl9" runat="server" Text="Warranty"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle BackColor="White" />
                                                                    <ItemTemplate>
                                                                        &nbsp;<asp:TextBox ID="txtWarranty" runat="server" Text='<%# Bind("Warranty")%>' Width="200px" MaxLength="50"></asp:TextBox><asp:Label ID="lblWarranty" runat="server" Text="" ForeColor="Red" Font-Bold Font-Size="11px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        &nbsp;<asp:Label ID="lbl10" runat="server" Text="Remarks"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle BackColor="White" />
                                                                    <ItemTemplate>
                                                                        &nbsp;<asp:TextBox ID="txtRemarks" runat="server" Text='<%# Bind("Remarks")%>' Width="200px" MaxLength="250"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                </Fields>
                                                                <FooterStyle BackColor="White" />
                                                                <FooterTemplate>

                                                                </FooterTemplate>
                                                                <PagerSettings Visible="False" />
                                                                <HeaderStyle BackColor="#10659E" Font-Bold="True" ForeColor="White" Width="120px" />
                                                            </asp:DetailsView>
                                                            <asp:SqlDataSource ID="dsTenderDetails" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetBidTenderDetails" SelectCommandType="StoredProcedure">
                                                                <SelectParameters>
                                                                    <asp:SessionParameter Name="BidTenderNo" SessionField="BIDTENDERNO" Type="Int32" />
                                                                </SelectParameters>
                                                            </asp:SqlDataSource>
                                                            <asp:SqlDataSource ID="dsCurrencies" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetCurrencies" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                            
																		<asp:SqlDataSource ID="dsPaymentTerms" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetPaymentTerms" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                            <br />
                                                            <asp:SqlDataSource ID="dsFileAttachments" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetVendorBidEventFileAttachments" SelectCommandType="StoredProcedure">
                                                                <SelectParameters>
                                                                    <asp:SessionParameter Name="BidRefNo" SessionField="BidRefNo" Type="Int32" />
                                                                    <asp:SessionParameter DefaultValue="0" Name="VendorId" SessionField="UserId" Type="Int32" />
                                                                </SelectParameters>
                                                            </asp:SqlDataSource>
                                                            <asp:GridView ID="gvFileAttachment" runat="server" AutoGenerateColumns="false" CssClass="itemDetails_1" OnRowCommand="gvFileAttachment_RowCommand" ShowFooter="true">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="&nbsp;Bid Tender File Attachments">
                                                                        <ItemStyle CssClass="valueGridItem" Width="100%" />
                                                                        <HeaderStyle CssClass="itemDetails_th" />
                                                                        <ItemTemplate>
                                                                            <asp:Literal ID="litAttach" runat="server" Text="&nbsp;" Visible='<%# IsAttached(Eval("Attached").ToString()) %>' />
                                                                            <asp:Image ID="imgAttach" runat="server" Height="10px" ImageUrl="~/web/themes/images/paperclip.gif" Visible='<%# IsAttached(Eval("Attached").ToString()) %>' Width="10px" />
                                                                            <asp:LinkButton ID="lnkRemoveAttachment" runat="server" CausesValidation="false" CommandArgument='<% #Bind("ID")%>' CommandName="Remove" Font-Bold="true" ForeColor="red" Visible='<%# IsRemovable(Eval("Attached").ToString(), Eval("IsDetachable").ToString()) %>'>Remove</asp:LinkButton>
                                                                            <asp:Label ID="lblColon" runat="server" Text=":" Visible='<%# IsRemovable(Eval("Attached").ToString(), Eval("IsDetachable").ToString()) %>'></asp:Label>
                                                                            <asp:LinkButton ID="lnkFile" runat="server" CommandArgument='<%#Bind("FileAttachment") %>' CommandName="Download" Text='<% #Bind("Original", "{0}")%>' Visible='<%# IsAttached(Eval("Attached").ToString()) %>'></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <p style="color: Red;">
                                                                                &nbsp;<asp:FileUpload ID="fileUpload" runat="server" Width="330px" />
                                                                                <br />
                                                                                &nbsp;&nbsp;(Maximum File Size:&nbsp;<asp:Label ID="lblMaxFileSize" runat="server" Text="<%$ AppSettings:MaxFileSize %>"></asp:Label>
                                                                                KB)<br /> &nbsp;&nbsp;<asp:LinkButton ID="lnkAttach" runat="server" CausesValidation="false" CommandName="Attach">Add To Attachments List</asp:LinkButton>
                                                                                <asp:Literal ID="addAttachmentMsg" runat="server"></asp:Literal>
                                                                            </p>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <asp:Label ID="litMsg" runat="server" Font-Names="Arial" Font-Size="11px" ForeColor="Red"></asp:Label>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                    </p>
                                                    </asp:Panel>
                                                    <p style="text-align: center;">                                                        
                                                        Comment<br />
                                                        <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Width="360px" Height="90px"
                                                            Font-Names="Arial" MaxLength="999"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvComment" Enabled="false" runat="server" Display="Dynamic" ErrorMessage="<br />Include a comment please."
                                                            ControlToValidate="txtComment" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        <br />
                                                    </p>
                                                    <asp:Literal ID="litErrMsg" runat="server"></asp:Literal><br />                                                    
                                                    <div>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                            <tr>
                                                                <td>
                                                                    <asp:LinkButton runat="server" ID="btnDraft" OnClick="btnDraft_Click"  OnClientClick="return confirm('Are you sure you want to save this tender as Draft?');">Draft </asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="btnSubmit" OnClick="btnSubmit_Click">Submit</asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="btnCancel" OnClick="btnCancel_Click" CausesValidation="false">Cancel</asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <br />
                                                        <br />
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td id="footer" height="50px">
                        <EBid:Footer runat="server" ID="Footer" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
