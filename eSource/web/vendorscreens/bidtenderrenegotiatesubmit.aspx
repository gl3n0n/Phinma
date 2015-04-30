<%@ Page Language="c#" Inherits="EBid.web.vendor_screens.BidTenderRenegotiateSubmit"
    CodeFile="bidtenderrenegotiatesubmit.aspx.cs" %>

<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Vendor/Footer.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/web/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_TopNav_Bids" Src="~/web/usercontrol/Vendor/Vendor_TopNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_LeftNav_Bids" Src="~/web/usercontrol/Vendor/Vendor_LeftNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_LeftNav_Comments" Src="~/web/usercontrol/Vendor/Vendor_LeftNav_Comments.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
    <%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title>.:| Trans-Asia  eSourcing System | |:.</title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='../themes/<%= Session["configTheme"] %>/css/style_v.css' />

    <script type="text/javascript">
	//<!--
	function DiscountFieldExists()
	{
		var elm = document.frmSubmitTender.elements;
		var strNames = "";
		for(i=0;i<elm.length;i++)
		{
			if((elm[i].type=="text"))
			{
				if (strNames == "") 
				{
					strNames = elm[i].id;
				}
				else
				{
					strNames = strNames + "|" + elm[i].id;
				}
			}
		}
		if (strNames.indexOf("txtDiscount") != -1)
		{
			return true;
		}
		else 
		{
			return false;
		}
	}

	function GetAllAmounts()
	{
		var elm = document.frmSubmitTender.elements;
		var TotalCost;
		var TotalExtendedCost;
	   
		for(i=0;i<elm.length;i++)
		{
			if((elm[i].type=="text"))
			{
				if (elm[i].id.indexOf("txtTotalCost") != -1) TotalCost = elm[i].id;
				if (elm[i].id.indexOf("txtTotalExtendedCost") != -1) TotalExtendedCost = elm[i].id;
			}
		}  
		
		if ((TotalCost != null) && (TotalExtendedCost != null))
		{    
		    
            document.getElementById(TotalCost).value = 0;  
            document.getElementById(TotalExtendedCost).value = 0;

            //check if there is no field with txtDiscount name
            if (DiscountFieldExists()== true)
            {
            for(i=0;i<elm.length;i++)
            {
                if((elm[i].type=="text"))
                {
               
	                if (elm[i].id.indexOf("txtAmount") != -1)
		                {
		                   if ((elm[i].value != "")&&(isNaN(elm[i].value)==false))
				                 document.getElementById(TotalCost).value = (parseFloat(document.getElementById(TotalCost).value)+parseFloat(elm[i].value));
		                 }
	                else if (elm[i].id.indexOf("txtDiscount") != -1)
		                {
			                if ((elm[i].value != "")&&(isNaN(elm[i].value)==false))
				                 {
					                document.getElementById(TotalCost).value = (parseFloat(document.getElementById(TotalCost).value)-parseFloat(elm[i].value));   
				                 }
		                }   
	                else if (elm[i].id.indexOf("txtDeliveryCost") != -1) 
	                    {
			                document.getElementById(TotalExtendedCost).value  = document.getElementById(TotalCost).value; 
			                if ((elm[i].value != "")&&(isNaN(elm[i].value)==false))
				                 {
					                document.getElementById(TotalExtendedCost).value = (parseFloat(document.getElementById(TotalCost).value)+parseFloat(elm[i].value));   
				                 }
                    
	                    }
                  }        
               }
            }
            else
            {
                for(i=0;i<elm.length;i++)
                    {
	                    if((elm[i].type=="text"))
	                    {
                       
		                    if (elm[i].id.indexOf("txtAmount") != -1)
			                    {
			                       if ((elm[i].value != "")&&(isNaN(elm[i].value)==false))
					                     document.getElementById(TotalCost).value = 0;
			                     }   
		                    else if (elm[i].id.indexOf("txtDeliveryCost") != -1) 
		                    {
				                    document.getElementById(TotalExtendedCost).value  = document.getElementById(TotalCost).value; 
				                    if ((elm[i].value != "")&&(isNaN(elm[i].value)==false))
					                     {
						                    document.getElementById(TotalExtendedCost).value = (parseFloat(document.getElementById(TotalCost).value)+parseFloat(elm[i].value));   
					                     }
                            
		                    }
	                    }        
                    }
            }    
		}           
	 }
	//-->
    </script>

</head>
<body runat="server" id="bdy">
    <div align="left">
        <form runat="server" id="frmSubmitTender">
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
                                                <EBid:Vendor_LeftNav_Comments runat="server" ID="Vendor_LeftNav_Comments" />
                                                <p>
                                                    &nbsp;</p>
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
                                                        Bid Tender Re-negotiate form</h1>
                                                    <p>
                                                        Fill up the fields below then click &quot;Submit&quot; to submit your Bid tender.</p>
                                                    <div align="left">
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails" height="85">
                                                            <tr>
                                                                <th colspan="2">
                                                                Details</tr>
                                                            <tr>
                                                                <td width="133">
                                                                    Reference Number</td>
                                                                <td class="value">
                                                                    <asp:Label runat="server" ID="lblBidReferenceNumber"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td width="133">
                                                                    Category</td>
                                                                <td class="value">
                                                                    <asp:Label runat="server" ID="lblCategory"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td width="133">
                                                                    Submission Deadline</td>
                                                                <td class="value">
                                                                    <asp:Label runat="server" ID="lblBidSubmissionDeadline"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td width="133">
                                                                    Delivery Date
                                                                </td>
                                                                <td class="value">
                                                                    <asp:Label runat="server" ID="lblDeliveryDate"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td width="133">
                                                                    Deliver To
                                                                </td>
                                                                <td class="value">
                                                                    <asp:Label runat="server" ID="lblDeliverTo"></asp:Label></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div align="left">
                                                        <asp:GridView runat="server" ID="gvBidDetails" AutoGenerateColumns="False" CssClass="itemDetails"
                                                            Width="100%">
                                                            <HeaderStyle CssClass="itemDetails_th" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Item" ItemStyle-Width="133px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Item") %>' Width="133px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="itemDetails_td" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Description" ItemStyle-CssClass="valueGridItem" ItemStyle-Width="650px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("DetailDesc") %>' Width="650px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Quantity" ItemStyle-CssClass="valueGridItem" ItemStyle-Width="75px">
                                                                    <ItemTemplate>
                                                                        <asp:Label font-color="#4D5B65;" ID="Label3" runat="server" Text='<%# Bind("Qty") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Unit of Measure" ItemStyle-CssClass="valueGridItem">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("UnitOfMeasure") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <div align="left">
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails">
                                                                <tr>
                                                                    <th width="138">
                                                                        Preferred Currency:</th>
                                                                    <td class="value">
                                                                        <asp:DropDownList runat="server" ID="ddlPreferredCurrency">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <asp:GridView runat="server" ID="gvBidDetails2" AutoGenerateColumns="False" DataKeyNames="BidDetailNo"
                                                            Width="100%" CssClass="itemDetails">
                                                            <Columns>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <br />
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                            <tr>
                                                                <td>
                                                                    <asp:LinkButton runat="server" ID="btnDraft" OnClick="btnDraft_Click">Draft </asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="btnSubmit" OnClick="btnSubmit_Click">Submit</asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="btnCancel" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
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
