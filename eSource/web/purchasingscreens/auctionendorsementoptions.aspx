<%@ Page Language="C#" AutoEventWireup="true" CodeFile="auctionendorsementoptions.aspx.cs" Inherits="web_purchasingscreens_auctionEndorsementOptions" %>

<%@ Register Src="../usercontrol/commentlist_auction.ascx" TagName="commentlist_auction" TagPrefix="uc1" %>
<%@ Register Src="../usercontrol/CommentBox.ascx" TagName="CommentBox" TagPrefix="uc2" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/WEB/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_TopNav_Auct" Src="~/web/usercontrol/Purchasing/Purchasing_TopNav_Auct.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_LeftNav" Src="~/web/usercontrol/Purchasing/Purchasing_LeftNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_LeftNav_Not" Src="~/web/usercontrol/Purchasing/Purchasing_LeftNav_Auct_Not.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Purchasing/Footer.ascx" %>
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
    <script language="javascript" type="text/javascript">
	<!--
	 function SelectAllCheckboxes(spanChk){

	   // Added as ASPX uses SPAN for checkbox
	   var oItem = spanChk.children;
	   var theBox= (spanChk.type=="checkbox") ? 
			spanChk : spanChk.children.item[0];
	   xState=theBox.checked;
	   elm=theBox.form.elements;
	   
	   if(xState == false)
	   {
			document.getElementById("lnkApprove").disabled=true;
			document.getElementById("lnkDisapprove").disabled=true;
	   }
	   else
	   {
		   document.getElementById("lnkApprove").disabled=false;
		   document.getElementById("lnkDisapprove").disabled=false;
	   }
	   
	   for(i=0;i<elm.length;i++)
		 if(elm[i].type=="checkbox" && 
				  elm[i].id!=theBox.id)
		 {
		   if(elm[i].checked!=xState)
			 elm[i].click();
		 }
	 }
	 
	 function VerifyCheckBox()
	 {
		var grdid = 'gvAuctionItems';
		var objid = 'cbSelected';
	//    var checkAll = 'CheckAll';
	    
		var chkbox; 
		var i=2; 
		var enabled = 0;
		var notall = 0;
	    
	    if(i<9)
		{
		   chkbox=document.getElementById(grdid + 
				   '_ctl0' + i + '_' + objid); 
		}
		else
		{
		    chkbox=document.getElementById(grdid + 
				   '_ctl' + i + '_' + objid); 
		}

        //chkbox=document.getElementById('gvAuctionItems_ctl02_cbSelected');
				   
		while(chkbox!=null)
		{ 
			if(chkbox.checked == true)
			{
				enabled = 1;
			}
	        
			i=i+1; 
			
			if(i<9)
			{
			   chkbox=document.getElementById(grdid + 
					   '_ctl0' + i + '_' + objid); 
			}
			else
			{
			    chkbox=document.getElementById(grdid + 
					   '_ctl' + i + '_' + objid); 
			}
			
		}
	    
		if(enabled == 1)        
		{
		   document.getElementById("lnkApprove").disabled=false;
		   document.getElementById("lnkDisapprove").disabled=false;
		}
		else
		{
		   document.getElementById("lnkApprove").disabled=true;
			document.getElementById("lnkDisapprove").disabled=true;
		}
	 }
	//-->
	</script>

    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
    <link href="../../css/style_ph.css' rel="stylesheet" type="text/css" />
</head>
<body onload="javascript:VerifyCheckBox();">
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
                <tr valign="top">
                    <td valign="top">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="height: 100%">
                            <tr height="100%" valign="top">
                                <td id="relatedInfo" valign="top">
                                    <div align="left">
                                        <EBid:Purchasing_LeftNav runat="server" ID="Purchasing_LeftNav" />
                                       
                                        
                                        <uc2:CommentBox ID="CommentBox1" runat="server" />
                                    </div>
                                </td>
                                <td id="content" valign="top" style="height: 100%">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page0">
                                        <tr>
                                            <td valign="top">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td id="content0">
                                                            <h1>
                                                                <br />
                                                                Auction Endorsement Summary</h1>
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails">
                                                                <tr>
                                                                    <th>
                                                                        Auction Event&nbsp;<asp:Label runat="server" ID="lblAuctionRefNo"></asp:Label></th>
                                                                </tr>
                                                            </table>
                                                            <asp:GridView Width="100%" runat="server" AutoGenerateColumns="False" ID="gvAuctionItems"
                                                                CssClass="itemDetails" DataSourceID="dsEndorsedAuctionItems" AllowSorting="True" EmptyDataText="No More Endorsed Auction Items to Approve/Disapprove.">
                                                                <HeaderStyle CssClass="itemDetails_th" />
                                                                <RowStyle CssClass="itemDetails_td" />
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                        <HeaderTemplate>
                                                                            &nbsp;<input id="CheckAll" runat="server" onclick="javascript:SelectAllCheckboxes(this);"
                                                                                type="checkbox" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="cbSelected" runat="server" onclick="javascript:VerifyCheckBox();" />
                                                                            <asp:HiddenField ID="hdEndorsedID" runat="server" Value='<%# Eval("AuctionEndorsementId") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Auction Item" SortExpression="ItemName">
                                                                        <HeaderStyle ForeColor="White" />
                                                                        <ItemTemplate>
                                                                            &nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Bind("ItemDesc") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Currency" SortExpression="Currency">
                                                                        <HeaderStyle ForeColor="White" />
                                                                        <ItemTemplate>
                                                                            &nbsp;<asp:Label ID="Label10" runat="server" Text='<%# Bind("Currency") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Bid Cost" SortExpression="Bid">
                                                                        <HeaderStyle ForeColor="White" />
                                                                        <ItemTemplate>
                                                                            &nbsp;<asp:Label ID="Label2" runat="server" Text='<%# Bind("Bid", "{0:#,##0.00}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle BackColor="White" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Supplier" SortExpression="VendorName">
                                                                        <HeaderStyle ForeColor="White" />
                                                                        <ItemTemplate>
                                                                            &nbsp;<asp:Label ID="Label3" runat="server" Text='<%# Bind("VendorName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle BackColor="White" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText=" Buyer Name">
                                                                        <HeaderStyle ForeColor="White" />
                                                                        <ItemTemplate>
                                                                            &nbsp;<asp:Label ID="Label4" runat="server" Text='<%# Bind("Buyer") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle BackColor="White" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EmptyDataRowStyle BorderWidth="0px" HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:GridView>
                                                            <asp:SqlDataSource ID="dsEndorsedAuctionItems" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                SelectCommand="s3p_EBid_GetEndorsedAuctionItems" SelectCommandType="StoredProcedure">
                                                                <SelectParameters>
                                                                    <asp:SessionParameter Name="auctionRefNo" SessionField="AuctionRefNo" Type="Int32" />
                                                                </SelectParameters>
                                                            </asp:SqlDataSource>
                                                            <br />
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions" runat="server">
                                                                <tr>
                                                                    <td>
                                                                        <asp:LinkButton ID="lnkApprove" Text="Approve" runat="server" OnClientClick="return confirm('Are you sure you want to approve this item?')"
                                                                            OnClick="Approve_Click" Width="100px"/>
                                                                        <asp:LinkButton ID="lnkDisapprove" Text="Disapprove" runat="server" OnClientClick="return confirm('Are you sure you want to disapprove this item?')"
                                                                            OnClick="Disapprove_Click" Width="100px"/>
                                                                        <asp:LinkButton ID="lnkFailed" Text="Failed" runat="server" OnClientClick="return confirm('Are you sure you want to declare this auction as failed?')" 
                                                                            OnClick="lnkFailed_Click" Width="100px"/> 
                                                                        <asp:LinkButton ID="lnkBack" runat="server" CausesValidation="false" OnClick="lnkBack_Click" Width="100px">Back</asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 20px;">
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
                        <EBid:Footer runat="server" ID="Footer1" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
