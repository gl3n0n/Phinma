<%@ Control Language="C#" AutoEventWireup="true" CodeFile="buyer_bidtenderdetails.ascx.cs" Inherits="web_usercontrol_Buyer_buyer_bidtenderdetails" %>
<%@ Register Src="bidtender_attachments.ascx" TagName="bidtender_attachments" TagPrefix="uc1" %>

<asp:DetailsView ID="dvBidTender" runat="server" AllowPaging="True" AutoGenerateRows="False"
    DataSourceID="dsTenderDetails" SkinID="BidDetails" Width="100%" HeaderText="Bid Tender Details" OnDataBound="dvBidTender_DataBound" >    
    <EmptyDataRowStyle BackColor="white" HorizontalAlign="Center" />    
    <Fields>
        <asp:TemplateField>
            <HeaderStyle Width="120px" />
            <HeaderTemplate>
                &nbsp;Bidder
            </HeaderTemplate>
            <ItemStyle BackColor="White" />
            <ItemTemplate>
                &nbsp;
                <asp:Label ID="lblVendor" runat="server" Text='<%# Bind("VendorName")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderStyle Width="120px" />
            <HeaderTemplate>
                &nbsp;Quantity
            </HeaderTemplate>
            <ItemStyle BackColor="White" />
            <ItemTemplate>
                &nbsp;
                <asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("Qty", "{0:#,##0}")%>'></asp:Label>
                &nbsp;<asp:Label ID="lblUOM" runat="server" Text='<%# Bind("UnitOfMeasure")%>'></asp:Label>(s)                
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>            
            <HeaderTemplate>
                &nbsp;Unit Price
            </HeaderTemplate>
            <ItemStyle BackColor="White" />
            <ItemTemplate>
                &nbsp;
                <asp:Label ID="lblMount" runat="server" Text='<%# Bind("Amount", "{0:#,##0.00}")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>        
        <asp:TemplateField>            
            <HeaderTemplate>
                &nbsp;Discount
            </HeaderTemplate>
            <ItemStyle BackColor="White" />
            <ItemTemplate>
                &nbsp;
                <asp:Label ID="lblDiscount" runat="server" Text='<%# Bind("Discount", "{0:#,##0.00}") %>'></asp:Label>                
            </ItemTemplate>
        </asp:TemplateField>
        <%--<asp:TemplateField>            
            <HeaderTemplate>
                &nbsp;Delivery Cost
            </HeaderTemplate>
            <ItemStyle BackColor="White" />
            <ItemTemplate>
                &nbsp;
                <asp:Label ID="lblDeliveryCost" runat="server" Text='<%# Bind("DeliveryCost", "{0:#,##0.00}") %>'></asp:Label>                
            </ItemTemplate>
        </asp:TemplateField>--%>
        <asp:TemplateField>            
            <HeaderTemplate>
                &nbsp;Total Bid Tender Price
            </HeaderTemplate>
            <ItemStyle BackColor="White" />
            <ItemTemplate>
                &nbsp;
                <asp:Label ID="Label1" runat="server" Text='<%# Bind("BidPrice", "{0:#,##0.00}") %>' Font-Bold="true"></asp:Label>                
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>            
            <HeaderTemplate>
                &nbsp;Currency
            </HeaderTemplate>
            <ItemStyle BackColor="White" />
            <ItemTemplate>
                &nbsp;
                <asp:Label ID="Label1x" runat="server" Text='<%# Bind("Currency", "{0:#,##0.00}") %>' Font-Bold="true"></asp:Label>                
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>            
            <HeaderTemplate>
                &nbsp;PaymentTerms
            </HeaderTemplate>
            <ItemStyle BackColor="White" />
            <ItemTemplate>
                &nbsp;
                <asp:Label ID="Label1xxx" runat="server" Text='<%# Bind("PaymentTermsDesc") %>'></asp:Label>                
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>            
            <HeaderTemplate>
                &nbsp;Incoterm
            </HeaderTemplate>
            <ItemStyle BackColor="White" />
            <ItemTemplate>
                &nbsp;
                <asp:Label ID="Label1xx" runat="server" Text='<%# Bind("IncotermDesc") %>'></asp:Label>                
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>            
            <HeaderTemplate>
                &nbsp;Warranty
            </HeaderTemplate>
            <ItemStyle BackColor="White" />
            <ItemTemplate>
                &nbsp;
                <asp:Label ID="lblWarranty" runat="server" Text='<%# Bind("Warranty")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>            
            <HeaderTemplate>
                &nbsp;Lead Time
            </HeaderTemplate>
            <ItemStyle BackColor="White" />
            <ItemTemplate>
                &nbsp;
                <asp:Label ID="lblLeadTime" runat="server" Text='<%# Bind("LeadTime")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>            
            <HeaderTemplate>
                &nbsp;Remarks
            </HeaderTemplate>
            <ItemStyle BackColor="White" />
            <ItemTemplate>                
                &nbsp;
                <asp:Label ID="Label8" runat="server" Text='<%# Eval("Remarks").ToString().Trim().Length > 0 ? Eval("Remarks").ToString() : "No Remarks" %>'
                                                                            ForeColor='<%# Eval("Remarks").ToString().Trim().Length > 0 ? System.Drawing.Color.Black : System.Drawing.Color.Gray %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>            
            <HeaderTemplate>
                &nbsp;PO Number
            </HeaderTemplate>
            <ItemStyle BackColor="White" />
            <ItemTemplate>                
                &nbsp;
                <asp:Label ID="lblPONumber" runat="server" Text='<%# Eval("PONumber").ToString() %>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>            
            <HeaderTemplate>
                &nbsp;Status
            </HeaderTemplate>
            <ItemStyle BackColor="White" />
            <ItemTemplate>   
                <asp:Label ID="lblMessage1" runat="server" Text=''></asp:Label>       
                <asp:Label ID="lblStatus1" runat="server" Text='<%# Bind("AwardedStatus")%>'></asp:Label>
                <div style="display:block">
                <asp:DropDownList ID="ddlStatus1" runat="server" SelectedValue='<%# Bind("AwardedStatus")%>'>
                    <asp:ListItem Value="">Select status</asp:ListItem>
                    <asp:ListItem Value="For Delivery">For Delivery</asp:ListItem>
                    <asp:ListItem Value="Partially Delivered">Partially Delivered</asp:ListItem>
                    <asp:ListItem Value="Completed">Completed</asp:ListItem>
		    <asp:ListItem Value="Others">Others</asp:ListItem>
                </asp:DropDownList>
                    <br />
                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Width="360px" Height="40px" Font-Names="Arial" MaxLength="999" Text=''></asp:TextBox>
                    <br />
                <asp:Button ID="Button1" runat="server" Text="Update Status" OnClick="Button1_Click" /></div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderTemplate>
                &nbsp;<asp:Label ID="lblComments" runat="server" Text="Status Comments"></asp:Label>
                </HeaderTemplate>
                <ItemStyle BackColor="White" />
                <ItemTemplate>
                   <asp:DataList ID="dlStatusComments" DataSourceID="dsStatusComments" runat="server" Width="98%">
                        <ItemStyle Font-Names="Arial" Font-Size="11px" />
                        <SeparatorStyle HorizontalAlign="Center" />
                        <FooterStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text="Date Posted : " ForeColor="DimGray"></asp:Label>
                            <asp:Label ID="DatePostedLabel" runat="server" Text='<%# Eval("DatePosted") %>'></asp:Label><br />
                            <asp:Label ID="Label2" runat="server" Text="Author : " ForeColor="DimGray"></asp:Label>
                            <asp:Label ID="AuthorLabel" runat="server" Text='<%# Eval("FullName") %>'></asp:Label><br />
                            <asp:Label ID="Label1" runat="server" Text="Comment : " ForeColor="DimGray"></asp:Label><asp:Label ID="CommentLabel" runat="server" Text='<%# Eval("Comment") %>'></asp:Label>
                            <br />
                            <br />
                        </ItemTemplate>
                        <SeparatorTemplate>
                            - - -</SeparatorTemplate>
                        <FooterTemplate>
                            - - - - -</FooterTemplate>
                    </asp:DataList>
                </ItemTemplate>
        </asp:TemplateField>
    </Fields>
    <PagerSettings Visible="False" />    
</asp:DetailsView>
 
                    
<asp:SqlDataSource ID="dsTenderDetails" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
    SelectCommand="sp_GetBidTenderDetails" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:SessionParameter Name="BidTenderNo" SessionField="BidTenderNo" DefaultValue="0" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
                    
<asp:SqlDataSource ID="dsStatusComments" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
    SelectCommand="sp_GetBidTenderStatusComments" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:SessionParameter Name="BidTenderNo" SessionField="BidTenderNo" DefaultValue="0" Type="Int32" />
        <asp:SessionParameter Name="UserType" SessionField="UserType" DefaultValue="0" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<uc1:bidtender_attachments ID="Bidtender_attachments1" runat="server" />
