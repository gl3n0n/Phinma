<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true"
    CodeFile="createauction.aspx.cs" Inherits="web_buyer_screens_createauction" EnableEventValidation="false" %>

<%@ Register Assembly="Calendar" Namespace="CalendarControl" TagPrefix="cc1" %>
<%@ Register TagPrefix="EBid" TagName="TopNavAuction" Src="~/WEB/usercontrol/Buyer/TopNavAuctions.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNav2_Auction" Src="~/WEB/usercontrol/Buyer/TopNav2_Auction.ascx" %>
<%@ Register TagPrefix="EBid" TagName="LeftNavAuctions1" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="LeftNavNotifications" Src="~/web/usercontrol/Buyer/LeftNavNotifications.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<%@ Register TagPrefix="EBid" TagName="CommentBox" Src="~/web/usercontrol/CommentBox.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <script type="text/javascript" src="../../jquery/jquery-1.5.1.js"></script>
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_buyer.css" %>' />

    <script type="text/javascript"> 
    <!--
        var category_old="", subcatID_old=""; 
        var company_old="", group_old="";
        var _req; 
    //-->
    </script>
    <script type="text/javascript" src="../include/common.js"></script>
    <script type="text/javascript" src="../include/actb.js"></script>
    <script type="text/javascript" src="../include/customValidation.js"></script>
    <script type="text/javascript" src="../include/generalJSFunctions.js"></script>
    <script type="text/javascript" src="../include/util.js"></script>
    <script type="text/javascript" src="../include/events.js"></script>
    <script>
    <!--
    function GetPRDate()
    {
        document.getElementById("txtPRDate").value = document.getElementById("jscPRDate").value;  
    }
    function GetDeliveryDate()
    {
        document.getElementById("txtDeliveryDate").value = document.getElementById("jscDeliveryDate").value; 
    }
    function GetAuctionConfirmationDeadline()
    {
        document.getElementById("txtAuctionConfirmationDeadline").value = document.getElementById("jscAuctionConfirmationDeadline").value;
    }
    function GetAuctionDate()
    {
        document.getElementById("txtAuctionStartDate").value = document.getElementById("jscAuctionStartDate").value;
        document.getElementById("txtAuctionEndDate").value = document.getElementById("jscAuctionEndDate").value;
        CompleteAuctionStartTime();
        CompleteAuctionEndTime();
    }

    function CompleteAuctionEndTime()
    {
       switch (trim(document.getElementById("ddlEndAMPM").value))
        {
            case "PM":
                var EndHour = parseInt(document.getElementById("ddlEndHour").value) + 12;
                    document.getElementById("txtCompleteAuctionEndTime").value = document.getElementById("jscAuctionEndDate").value; + " " +
                    EndHour + ":" +
                    document.getElementById("txtEndMin").value + ":" +
                    document.getElementById("txtEndSec").value;    
            break;
            default:
                    document.getElementById("txtCompleteAuctionEndTime").value = document.getElementById("jscAuctionEndDate").value; + " " +
                    document.getElementById("ddlEndHour").value + ":" +
                    document.getElementById("txtEndMin").value + ":" +
                    document.getElementById("txtEndSec").value;    
            break;
        }
    }

    function  CompleteAuctionStartTime()
    {
        switch (trim(document.getElementById("ddlStartAMPM").value))
        {
            default:
                    document.getElementById("txtCompleteAuctionStartTime").value = document.getElementById("jscAuctionStartDate").value + " " + 
                    document.getElementById("ddlStartHour").value + ":" +
                    document.getElementById("txtStartMin").value + ":" +
                    document.getElementById("txtStartSec").value;
            break;
            case "PM":
                var starthour = parseInt(document.getElementById("ddlStartHour").value) + 12;
                document.getElementById("txtCompleteAuctionStartTime").value =  document.getElementById("jscAuctionStartDate").value + " " + 
                        starthour + ":" +
                        document.getElementById("txtStartMin").value + ":" +
                        document.getElementById("txtStartSec").value;
             break;
          }
    }

    function GetAuctionStartTime()
    {
        CompleteAuctionStartTime();
    }

    function GetAuctionEndTime()
    {
        CompleteAuctionEndTime();
    }

    function DisableSupplierRequired()
    {
        document.getElementById("rfvSuppliers").enabled = false; 
    }

    function EnableSupplierRequired()
    {
        document.getElementById("rfvSuppliers").enabled = true;
    }

    function AddSubItem()
    {
        DisableSupplierRequired();
       document.getElementById("hdnSetFocusToSubItem").value="1";
       
    }

    function Draft()
    {
        DisableSupplierRequired();
    }

    function Submit()
    {
        EnableSupplierRequired();
    }

    function SelectSupplier()
    {
        DisableSupplierRequired();
    }
    function AttachFile()
    {
        DisableSupplierRequired();
        document.getElementById("hdnSetFocusToNewFile").value="1";
    }

    function ShowProducts(index) {
        var chrom = navigator.userAgent;

        var index = parseInt(index) + 1;
        var grid = document.getElementById("gvAuctionItems");
        var selectedrow = grid.getElementsByTagName("tr")[index];
        var selectedcell = selectedrow.getElementsByTagName("td")[0];
        //lert(chrom);
        if (chrom.indexOf("Chrome") == -1 && chrom.indexOf("Firefox") == -1) {
        var text = selectedcell.childNodes[0];
    } else {
        var text = selectedcell.childNodes[1];
    }
        document.getElementById("hdnIndex").value = index;
        
        var vCategoryId = frmAuction.ddlCategory.options.item(frmAuction.ddlCategory.selectedIndex).value;
        var vSubCategoryId = frmAuction.ddlSubCategory.options.item(frmAuction.ddlSubCategory.selectedIndex).value;
        if (vCategoryId != "---- SELECT CATEGORY ----") {
            window.open('productitems.aspx?cid=' + vCategoryId + "&scid=" + vSubCategoryId + "&st=" + trim(text.value), 'x', 'toolbar=no, menubar=no, width=550; height=500, top=80, left=325, resizable=1, scrollbars=1');
        }
    }

    function RefreshMe(selectedProduct)
    {
        

            var ids = trim(selectedProduct);
            var ids1 = ids.split("|");
            var SKU = trim(ids1[0]);
            var ItemName = trim(ids1[1]);
            var ItemDesc = trim(ids1[2]);
            var UOM = trim(ids1[3]);

            var chrom = navigator.userAgent;
            if (chrom.indexOf("Chrome") == -1 && chrom.indexOf("Firefox") == -1) {
            var index = parseInt(document.getElementById("hdnIndex").value);
            var grid = document.getElementById("gvAuctionItems");
            var selectedrow = grid.getElementsByTagName("tr")[index];
            var ItemCell = selectedrow.getElementsByTagName("td")[0];
            var text = ItemCell.childNodes[0];
            text.value = SKU;

            var DescCell = selectedrow.getElementsByTagName("td")[1];
            var desc = DescCell.childNodes[0];
            desc.value = ItemDesc;

            var UOMCell = selectedrow.getElementsByTagName("td")[3];
            var uom = UOMCell.childNodes[0];
            uom.value = UOM;
            uom.readOnly = true;

            var SKUCell = selectedrow.getElementsByTagName("td")[5];
            var SKUtext = SKUCell.childNodes[6];
            SKUtext.value = SKU;
        }
        else {
            var index = parseInt(document.getElementById("hdnIndex").value);
            var grid = document.getElementById("gvAuctionItems");
            var selectedrow = grid.getElementsByTagName("tr")[index];
            var ItemCell = selectedrow.getElementsByTagName("td")[0];
            var text = ItemCell.childNodes[1];
            text.value = SKU;

            var DescCell = selectedrow.getElementsByTagName("td")[1];
            var desc = DescCell.childNodes[1];
            desc.value = ItemDesc;

            var UOMCell = selectedrow.getElementsByTagName("td")[3];
            var uom = UOMCell.childNodes[1];
            uom.value = UOM;
            uom.readOnly = true;

            var SKUCell = selectedrow.getElementsByTagName("td")[5];
            var SKUtext = SKUCell.childNodes[7];
            SKUtext.value = SKU;

        }
            var btnAddSubItem = document.getElementById("btnAddSubItem");
            btnAddSubItem.focus();
    }

     function Load()
     {
        setFocus();
        ChangeTitle();
     }
     
     function setFocus()
     {
       if (trim(document.getElementById("hdnSetFocusToSubItem").value)=="1")
       {
            
            var grid = document.getElementById("gvAuctionItems");
            var index = grid.rows.length-1;
            var selectedrow = grid.getElementsByTagName("tr")[index];
            var selectedcell = selectedrow.getElementsByTagName("td")[0];
            var txtItem = selectedcell.childNodes[0];
            txtItem.focus();
            document.getElementById("hdnSetFocusToSubItem").value = "";
       }
       if (trim(document.getElementById("hdnSetFocusToNewFile").value)=="1")
       {  
            var btnAttachFile = document.getElementById("btnAttachFile");
            btnAttachFile.focus();
            document.getElementById("hdnSetFocusToNewFile").value = "";
       }
     }
     
     function ConfirmRemoveSubItem()
     {
        frmAuction.hdnConfirmRemoveSubItem.value = window.confirm("Are you sure you want to delete this sub item?");
        frmAuction.hdnSetFocusToSubItem.value="1";
     }
     
      function ConfirmRemoveFileAttachment()
     {
        frmAuction.hdnConfirmRemoveFileAttachment.value = window.confirm("Are you sure you want to delete this file attachment?");
        frmAuction.hdnSetFocusToNewFile.value="1";
     }
     
      function CompareTime(source,  args)
     {    
        var AuctionStart = trim(document.getElementById("jscAuctionStartDate").value);   
        var AuctionEnd = trim(document.getElementById("jscAuctionEndDate").value);
        
        var startAMPM = trim(document.getElementById("ddlStartAMPM").value);
        var startHour = trim(document.getElementById("ddlStartHour").value);
        if (startAMPM == "PM")
            startHour = parseInt(startHour) + 12;
        var startMin = trim(document.getElementById("txtStartMin").value);
        var startSec = trim(document.getElementById("txtStartSec").value);

        var endAMPM = trim(document.getElementById("ddlEndAMPM").value);
        var endHour = trim(document.getElementById("ddlEndHour").value);
        if (endAMPM == "PM")
            endHour = parseInt(endHour) + 12;
        var endMin = trim(document.getElementById("txtEndMin").value);
        var endSec = trim(document.getElementById("txtEndSec").value);

        var aucStart = new Date(AuctionStart);
        var aucEnd = new  Date(AuctionEnd);
        
        var asy = aucStart.getFullYear();
        var asm = aucStart.getMonth();
        var asd = aucStart.getDate();
        
        var aey = aucEnd.getFullYear();
        var aem = aucEnd.getMonth();
        var aed = aucEnd.getDate();
        
        startDate = new Date(asy, asm, asd-1, startHour, startMin, startSec);
        endDate = new Date(aey, aem, aed-1, endHour, endMin, endSec);     
        args.IsValid=(startDate < endDate);
     } 
    
     function CompareConfirmDate(source,  args)
     {    
       
        var AuctionConfirmationDeadline = trim(document.getElementById("jscAuctionConfirmationDeadline").value);
        var AuctionStart = trim(document.getElementById("jscAuctionStartDate").value);         
        var now = new Date();
         
        var aucDeadline = new  Date(AuctionConfirmationDeadline);
        var aucStart = new Date(AuctionStart);
        
        var ady = aucDeadline.getFullYear();
        var adm = aucDeadline.getMonth();
        var add = aucDeadline.getDate();
         
        var asy = aucStart.getFullYear();
        var asm = aucStart.getMonth();
        var asd = aucStart.getDate();
        
        var ny  =  now.getFullYear();
        var nm = now.getMonth();
        var nd = now.getDate();

        startDate = new Date(asy, asm, asd-1);
        deadlineDate = new Date(ady, adm, add-1); 
        dateToday = new Date(ny, nm, nd-1);
        args.IsValid=((deadlineDate <= startDate) & (deadlineDate >= dateToday));
        //args.IsValid=(deadlineDate >= dateToday);
        
     }  
     
     function ChangeTitle()
     {
        var ddlAuctionType = trim(document.getElementById("ddlAuctionType").value);
        var grid = document.getElementById("gvAuctionItems");
        var headerrow = grid.getElementsByTagName("tr")[0];
        var selectedcell = headerrow.getElementsByTagName("th")[5];
        var txtTitle = document.getElementById("gvAuctionItems_ctl01_lblIncrementDecrement");
       
        if (ddlAuctionType == "0")
        {
            txtTitle.value = "Increment";
        }
        else if (ddlAuctionType == "1")
        {
            txtTitle.value = "Decrement";
        }
        else
        {
            txtTitle.value = "";
        }   
     }

    -->
    </script>
</head>
<body onload="Load();">
    <form runat="server" id="frmAuction">
        <div align="left">
            <div align="left">
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
                                        <EBid:TopNav2_Auction ID="TopNav2_Auction1" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%" height='100%'>
                                <tr>
                                    <td id="relatedInfo">
                                        <div align="left">
                                            <EBid:LeftNavAuctions1 runat="server" ID="LeftNavAuctions" />
                                        </div>
                                        <h2>
                                            Notifications</h2>
                                        <div align="left">
                                            <EBid:LeftNavNotifications runat="server" ID="LeftNavNotifications" />
                                        </div>
                                        <br />
                                        <div align="left">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table4">
                                                <tr>
                                                    <td>
                                                        <EBid:CommentBox runat="server" ID="CommentBox" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
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
                                                                    Create New Auction Event</h1>
                                                <script type="text/javascript">
                                                    $(document).ready(function() {
                                                        $('#relatedInfo').hide();
                                                    });
                                                 </script>
												<p><a href="javascript:void(0)" onclick="$('#relatedInfo').toggle()">Show/Hide Left Panel</a><br><asp:Label ID="Label1" runat="server" Font-Size="11px" ForeColor="red"></asp:Label></p>
                                                                <div>
                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails">
                                                                        <tr>
                                                                            <th colspan="2">
                                                                                &nbsp;Auction Event
                                                                                <asp:Label runat="server" ID="lblAuctionTitle" Text=""></asp:Label></th>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="133">
                                                                                Item Type:</td>
                                                                            <td class="value">
                                                                                <asp:Label runat="server" ID="lblItemType" Text=""></asp:Label></td>
                                                                        </tr>
                                                                       <tr>
                                                                            <td style="height: 26px">
                                                                                Reference No.:</td>
                                                                            <td class="evenCells" style="height: 26px">
                                                                                <asp:Label runat="server" ID="lblReferenceNumber" Text="NONE" ForeColor="Gray"></asp:Label>
                                                                                <asp:HiddenField ID="hdnDetailNo" runat="server" />
                                                                                <asp:HiddenField ID="hdnRefNo" runat="server" />
                                                                            </td>
                                                                        </tr> 
                                                                        <tr>
                                                                            <td>
                                                                                Requestor:</td>
                                                                            <td class="value">
                                                                                <asp:TextBox runat="server" ID="txtRequestor" Width="250px"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="rfvRequestor" runat="server" ErrorMessage="Requestor is a required field."
                                                                                    ControlToValidate="txtRequestor" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                PR Number:</td>
                                                                            <td class="evenCells">
                                                                                <asp:TextBox runat="server" ID="txtPRNumber" onKeyPress="return noNumbers(event);"
                                                                                    Width="250px" MaxLength="10"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="rfvPRNumber" runat="server" ErrorMessage="PR Number is a required field."
                                                                                    ControlToValidate="txtPRNumber" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                PR Date:</td>
                                                                            <td class="value">
                                                                                <cc1:JSCalendar ID="jscPRDate" runat="server" ScriptsBasePath="../../calendar/" EnableViewState="true"
                                                                                    ImageURL="../../calendar/img.gif" DateFormat="MM/dd/yyyy" Width="178px" ReadOnly="false"
                                                                                    MaxLength="10" TabIndex="-1" onBlur="GetPRDate();"></cc1:JSCalendar>
                                                                                    &nbsp;<asp:Label ID="lblExDate" runat="server" text="eg. 01/31/2007" ForeColor="red" Font-Size="10px"/>
                                                                                <asp:RequiredFieldValidator ID="rfvPRDate" runat="server" ErrorMessage="PR Date is a required field."
                                                                                    ControlToValidate="jscPRDate" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                <asp:CompareValidator ID="cvPRDate" runat="server" ControlToValidate="txtPRDate"
                                                                                    Display="None" ErrorMessage="PR date is an invalid date." Type="Date" Operator="DataTypeCheck"></asp:CompareValidator>
                                                                            </td>
                                                                        </tr>
                                                                       <tr>
                                                                            <td>
                                                                                Company:</td>
                                                                            <td class="evenCells">
                                                                                <asp:DropDownList runat="server" ID="ddlCompany" AutoPostBack="True"  Width="250px">
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="rfvCompany" runat="server" ErrorMessage="Company is a required field."
                                                                                    ControlToValidate="ddlCompany" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr> 
                                                                        <tr>
                                                                            <td>
                                                                                Group/Department/Section:</td>
                                                                            <td class="value">
                                                                                <asp:DropDownList runat="server" ID="ddlGroupDeptSec" Width="300px">
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="rfvGroupDeptSec" runat="server" ErrorMessage="Group/Deptartment/Section is a required field."
                                                                                    ControlToValidate="ddlGroupDeptSec" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Category:</td>
                                                                            <td class="evenCells">
                                                                                <asp:DropDownList runat="server" ID="ddlCategory" AutoPostBack="True" Width="250px">
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ErrorMessage="Category is a required field."
                                                                                    ControlToValidate="ddlCategory" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Sub Category:</td>
                                                                            <td class="value">
                                                                                <asp:DropDownList runat="server" ID="ddlSubCategory" Width="250px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Delivery Date:</td>
                                                                            <td class="evenCells">
                                                                                <cc1:JSCalendar ID="jscDeliveryDate" runat="server" ScriptsBasePath="../../calendar/"
                                                                                    EnableViewState="true" ImageURL="../../calendar/img.gif" DateFormat="MM/dd/yyyy"
                                                                                    Width="178px" ReadOnly="false" MaxLength="10" TabIndex="-1" onBlur="GetDeliveryDate();"></cc1:JSCalendar>
                                                                                    &nbsp;<asp:Label ID="lblExDate2" runat="server" text="eg. 01/31/2007" ForeColor="red" Font-Size="10px"/>
                                                                                <asp:RequiredFieldValidator ID="rfvDeliveryDate" runat="server" ErrorMessage="Delivery Date is a required field."
                                                                                    ControlToValidate="jscDeliveryDate" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                <asp:CompareValidator ID="cvjscDeliveryDate" runat="server" ControlToValidate="txtDeliveryDate"
                                                                                    Display="None" ErrorMessage="Delivery date is an invalid date." Type="Date" Operator="DataTypeCheck"></asp:CompareValidator>
                                                                            </td>
                                                                        </tr>
                                                                       <tr>
                                                                            <td>
                                                                               Description:</td>
                                                                            <td class="value">
                                                                                <asp:TextBox runat="server" ID="txtItemDescription"
                                                                                     Width="300px" Font-Names="Arial" Font-Size="11px" MaxLength="300"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="rfvItemDescription" runat="server" ErrorMessage="Item Description is a required field."
                                                                                    ControlToValidate="txtItemDescription" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>  
                                                                    </table>
                                                                    <div align="left">
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails">
                                                                            <tr>
                                                                                <th colspan="2">
                                                                                    &nbsp;Auction Event details</th>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="133">
                                                                                    Type:
                                                                                </td>
                                                                                <td class="value">
                                                                                    &nbsp;<asp:DropDownList runat="server" ID="ddlAuctionType" Width="145px">
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="rfvAuctionType" runat="server" ErrorMessage="Auction Type is a required field."
                                                                                        ControlToValidate="ddlAuctionType" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Confirmation Deadline:</td>
                                                                                <td class="evenCells">
                                                                                    <cc1:JSCalendar ID="jscAuctionConfirmationDeadline" runat="server" ScriptsBasePath="../../calendar/"
                                                                                        EnableViewState="true" ImageURL="../../calendar/img.gif" DateFormat="MM/dd/yyyy"
                                                                                        Width="145px" ReadOnly="false" MaxLength="10" TabIndex="-1" onBlur="GetAuctionConfirmationDeadline();"></cc1:JSCalendar>
                                                                                    &nbsp;<asp:Label ID="lblExDate3" runat="server" text="eg. 01/31/2007" ForeColor="red" Font-Size="10px"/>
                                                                                    <asp:RequiredFieldValidator ID="rfvAuctionConfirmationDeadline" runat="server" ErrorMessage="Auction Confirmation Deadline is a required field."
                                                                                        ControlToValidate="jscAuctionConfirmationDeadline" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                    <asp:CompareValidator ID="cvAuctionConfirmation" runat="server" ControlToValidate="txtAuctionConfirmationDeadline"
                                                                                        Display="None" ErrorMessage="Auction Confirmation Deadline is an invalid date."
                                                                                        Type="Date" Operator="DataTypeCheck"></asp:CompareValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Start Date:</td>
                                                                                <td class="value">
                                                                                    <cc1:JSCalendar ID="jscAuctionStartDate" runat="server" ScriptsBasePath="../../calendar/"
                                                                                        EnableViewState="true" ImageURL="../../calendar/img.gif" DateFormat="MM/dd/yyyy"
                                                                                        Width="145px" ReadOnly="false" MaxLength="10" TabIndex="-1" onBlur="GetAuctionDate();"></cc1:JSCalendar>
                                                                                    &nbsp;<asp:Label ID="lblExDate4" runat="server" text="eg. 01/31/2007" ForeColor="red" Font-Size="10px"/>
                                                                                    <asp:RequiredFieldValidator ID="rfvAuctionStartDate" runat="server" ErrorMessage="Auction Event Start Date is a required field."
                                                                                        ControlToValidate="jscAuctionStartDate" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                    <asp:CompareValidator ID="cvAuctionStartDate" runat="server" ControlToValidate="txtAuctionStartDate"
                                                                                        Display="None" ErrorMessage="Auction Event Start Date is an invalid date." Type="Date"
                                                                                        Operator="DataTypeCheck"></asp:CompareValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Start Time:</td>
                                                                                <td class="evenCells">
                                                                                    &nbsp;<asp:DropDownList runat="server" ID="ddlStartHour" OnBlur="GetAuctionStartTime();">
                                                                                    </asp:DropDownList>:
                                                                                    <asp:TextBox runat="server" ID="txtStartMin" Width="30px" OnBlur="GetAuctionStartTime();"
                                                                                        MaxLength="2" onKeyPress="return noNumbers(event);"></asp:TextBox>:
                                                                                    <asp:TextBox runat="server" ID="txtStartSec" Width="30px" OnBlur="GetAuctionStartTime();"
                                                                                        MaxLength="2" onKeyPress="return noNumbers(event);"></asp:TextBox>
                                                                                    <asp:DropDownList runat="server" ID="ddlStartAMPM" OnBlur="GetAuctionStartTime();">
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="rfvAuctionStartTimeHour" runat="server" ErrorMessage="Auction Start Time (Hours) is a required field."
                                                                                        ControlToValidate="ddlStartHour" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                    <asp:RequiredFieldValidator ID="rfvAuctionStartTimeMinutes" runat="server" ErrorMessage="Auction Start Time (Minutes) is a required field."
                                                                                        ControlToValidate="txtStartMin" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                    <asp:RangeValidator ID="rgeAuctionStartTimeMin" runat="server" ControlToValidate="txtStartMin"
                                                                                        Display="None" ErrorMessage="Valid Auction Start Time (Minutes) is between 00-59 inclusive."
                                                                                        MaximumValue="59" MinimumValue="00" SetFocusOnError="true"></asp:RangeValidator>
                                                                                    <asp:RequiredFieldValidator ID="rfvAuctionStartTimeSec" runat="server" ErrorMessage="Auction Start Time (Seconds) is a required field."
                                                                                        ControlToValidate="txtStartSec" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                    <asp:RangeValidator ID="rgeAuctionStartTimeSec" runat="server" ControlToValidate="txtStartSec"
                                                                                        Display="None" ErrorMessage="Valid Auction Start Time (Seconds) is between 00-59 inclusive."
                                                                                        MaximumValue="59" MinimumValue="00" SetFocusOnError="true"></asp:RangeValidator>
                                                                                    <asp:RequiredFieldValidator ID="rfvAuctionStartTimeAMPM" runat="server" ErrorMessage="Auction Start Time (AM/PM)  is a required field."
                                                                                        ControlToValidate="ddlStartAMPM" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                    <asp:CustomValidator ID="cvCompareConfirmDate" runat="server" ClientValidationFunction="CompareConfirmDate(this, args);"
                                                                                        Display="None" ErrorMessage="Auction Confirmation Deadline should be less than or equal to Auction Start Date and not less than Current Date."></asp:CustomValidator>  
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    End Date:</td>
                                                                                <td class="value">
                                                                                    <cc1:JSCalendar ID="jscAuctionEndDate" runat="server" ScriptsBasePath="../../calendar/"
                                                                                        EnableViewState="true" ImageURL="../../calendar/img.gif" DateFormat="MM/dd/yyyy"
                                                                                        Width="145px" ReadOnly="false" MaxLength="10" TabIndex="-1" onBlur="GetAuctionDate();"></cc1:JSCalendar>
                                                                                    &nbsp;<asp:Label ID="lblExDate5" runat="server" text="eg. 01/31/2007" ForeColor="red" Font-Size="10px"/>
                                                                                    <asp:RequiredFieldValidator ID="rfvAuctionEndDate" runat="server" ErrorMessage="Auction Event End Date is a required field."
                                                                                        ControlToValidate="jscAuctionEndDate" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtAuctionEndDate"
                                                                                        Display="None" ErrorMessage="Auction Event End Date is an invalid date." Type="Date"
                                                                                        Operator="DataTypeCheck"></asp:CompareValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    End Time:</td>
                                                                                <td class="evenCells">
                                                                                    &nbsp;<asp:DropDownList runat="server" ID="ddlEndHour" OnBlur="GetAuctionEndTime();">
                                                                                    </asp:DropDownList>:
                                                                                    <asp:TextBox runat="server" ID="txtEndMin" Width="30px" OnBlur="GetAuctionEndTime();"
                                                                                        MaxLength="2" onKeyPress="return noNumbers(event);"></asp:TextBox>:
                                                                                    <asp:TextBox runat="server" ID="txtEndSec" Width="30px" OnBlur="GetAuctionEndTime();"
                                                                                        MaxLength="2" onKeyPress="return noNumbers(event);"></asp:TextBox>
                                                                                    <asp:DropDownList runat="server" ID="ddlEndAMPM" OnBlur="GetAuctionEndTime();">
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="rfvAuctionEndTimeHour" runat="server" ErrorMessage="Auction End Time (Hours) is a required field."
                                                                                        ControlToValidate="ddlEndHour" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                    <asp:RequiredFieldValidator ID="rfvAuctionEndTimeMin" runat="server" ErrorMessage="Auction End Time (Minutes) is a required field."
                                                                                        ControlToValidate="txtEndMin" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                    <asp:RangeValidator ID="rgeAuctionEndTimeMin" runat="server" ControlToValidate="txtEndMin"
                                                                                        Display="None" ErrorMessage="Valid Auction End Time (Minutes) is between 00-59 inclusive."
                                                                                        MaximumValue="59" MinimumValue="00" SetFocusOnError="true"></asp:RangeValidator>
                                                                                    <asp:RequiredFieldValidator ID="rfvAuctionEndSec" runat="server" ErrorMessage="Auction End Time (Seconds) is a required field."
                                                                                        ControlToValidate="txtEndSec" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                    <asp:RangeValidator ID="rgeAuctionEndTimeSec" runat="server" ControlToValidate="txtEndSec"
                                                                                        Display="None" ErrorMessage="Valid Auction End Time (Seconds) is between 00-59 inclusive."
                                                                                        MaximumValue="59" MinimumValue="00" SetFocusOnError="true"></asp:RangeValidator>
                                                                                    <asp:RequiredFieldValidator ID="rfvEndAMPM" runat="server" ErrorMessage="Auction End Time (AM/PM) is a required field."
                                                                                        ControlToValidate="ddlEndAMPM" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                    <asp:CustomValidator ID="cvCompareTime" runat="server" ClientValidationFunction="CompareTime(this, args);"
                                                                                        Display="None" ErrorMessage="Auction Start Time should be less than Auction End Time."></asp:CustomValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 28px">
                                                                                    Bid Currency:
                                                                                </td>
                                                                                <td class="value" style="height: 28px">
                                                                                    &nbsp;<asp:DropDownList runat="server" ID="ddlBidCurrency" DataSourceID="sdsCurrecy" DataTextField="Currency" DataValueField="ID">
                                                                                    </asp:DropDownList><asp:SqlDataSource ID="sdsCurrecy" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetCurrencies" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                                                    <asp:RequiredFieldValidator ID="rfvBidCurrency" runat="server" ErrorMessage="Bid Currency is a required field."
                                                                                        ControlToValidate="ddlBidCurrency" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                    <br />
                                                                    <asp:GridView runat="server" ID="gvAuctionItems" AutoGenerateColumns="False" SkinID="AuctionedItems"
                                                                        OnRowDataBound="gvAuctionItems_RowDataBound" OnRowCommand="gvAuctionItems_RowCommand">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Item" FooterText="Estimated Value">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox runat="server" ID="txtItem" Width="140" Text='<%# Bind("PItemName2") %>'
                                                                                        ReadOnly='<%# IsReadOnly(Eval("ForConversion","{0}"))%>'></asp:TextBox>
                                                                                    <input disabled='<%# IsDisabled(Eval("ForConversion","{0}"))%>' type="button" id="btnSearch"
                                                                                        runat="server" value=" ... " width="80" />
                                                                                    <asp:RequiredFieldValidator ID="rfvItem" runat="server" ControlToValidate="txtItem"
                                                                                        Display="None" ErrorMessage="Item is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="185px" HorizontalAlign="Center" VerticalAlign="Top" />
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Description">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox runat="server" ID="txtDesc" TextMode="multiline" Wrap="true" Rows="4"
                                                                                        ReadOnly='<%# IsReadOnly(Eval("ForConversion","{0}"))%>' Text='<%# Bind("Description") %>'
                                                                                        Style="font-family: Arial,Verdana;" Width="95%"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator runat="server" ID="rfvDesc" ControlToValidate="txtDesc"
                                                                                        Display="None" ErrorMessage="Description is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="center" VerticalAlign="Top" />
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Quantity">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox runat="server" ID="txtQty" Width="70" Text='<%# Bind("Quantity") %>'
                                                                                        onKeyPress="AcceptDecimals(this);"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="rfvQty" runat="server" ControlToValidate="txtQty"
                                                                                        Display="None" ErrorMessage="Quantity is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                    <asp:RegularExpressionValidator ID="regQty" runat="server" ControlToValidate="txtQty"
                                                                                        Display="None" ErrorMessage="Quantity is a numeric field with 2 decimal places."
                                                                                        SetFocusOnError="True" ValidationExpression="^[-]?([1-9]{1}[0-9]{0,}(\.[0-9]{0,2})?|0(\.[0-9]{0,2})?|\.[0-9]{1,2})$"></asp:RegularExpressionValidator>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" Width="75px" VerticalAlign="Top" />
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Unit Of Measure">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox runat="server" ID="txtUOM" Width="50" Text='<%# Bind("UnitOfMeasure") %>'
                                                                                        ReadOnly='<%# IsReadOnly(Eval("ForConversion","{0}"))%>'></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="rfvUOM" runat="server" ControlToValidate="txtUOM"
                                                                                        Display="None" ErrorMessage="Unit of Measure is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" Width="55px" VerticalAlign="Top" />
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Starting Price">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox runat="server" ID="txtStartingPrice" Width="100px" Text='<%# Bind("StartingPrice") %>' onKeyPress="AcceptDecimals(this);" ></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="rfvtxtStartingPrice" runat="server" ControlToValidate="txtStartingPrice"
                                                                                        Display="None" ErrorMessage="Starting Price is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                    <asp:RegularExpressionValidator ID="regtxtStartingPrice" runat="server" ControlToValidate="txtStartingPrice"
                                                                                        Display="None" ErrorMessage="Starting Price is a numeric field with 2 decimal places."
                                                                                        SetFocusOnError="True" ValidationExpression="^[-]?([1-9]{1}[0-9]{0,}(\.[0-9]{0,2})?|0(\.[0-9]{0,2})?|\.[0-9]{1,2})$"></asp:RegularExpressionValidator>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" Width="105px" VerticalAlign="Top" />
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox runat="server" ID="lblIncrementDecrement" Text=""  BorderStyle="None" BackColor="#10659E" 
                                                                                            Font-Bold="true" ForeColor="#FFFFFF" width="80px" ReadOnly="true"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox runat="server" ID="txtIncrementDecrement" Width="80px" Text='<%# Bind("IncrementDecrement") %>'
                                                                                        onKeyPress="AcceptDecimals(this);"></asp:TextBox>
                                                                                    <br />
                                                                                    <asp:RequiredFieldValidator ID="rfvIncrementDecrement" runat="server" ControlToValidate="txtIncrementDecrement"
                                                                                        Display="None" ErrorMessage="Increment/Decrement is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                    <asp:RegularExpressionValidator ID="regIncDec" runat="server" ControlToValidate="txtIncrementDecrement"
                                                                                        Display="None" ErrorMessage="Increment/Decrement is a numeric field with 2 decimal places."
                                                                                        SetFocusOnError="True" ValidationExpression="^[-]?([1-9]{1}[0-9]{0,}(\.[0-9]{0,2})?|0(\.[0-9]{0,2})?|\.[0-9]{1,2})$"></asp:RegularExpressionValidator>
                                                                                    <asp:CompareValidator ID="cvIncrementDecrement" runat="server" ControlToValidate="txtIncrementDecrement"
                                                                                        ErrorMessage="Increment/Decrement must be a value greater than 0." Operator="GreaterThan"
                                                                                        SetFocusOnError="True" Type="Double" ValueToCompare="0" Display="None"></asp:CompareValidator>
                                                                                    <asp:TextBox Width="0" Height="0" BorderColor="White" BorderStyle="Solid" BorderWidth="1" runat="server" ID="txtSKU" Text='<%# Bind("SKU") %>'></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" Width="85px" VerticalAlign="Top" />
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton runat="server" ID="lnkRemove" Text="Remove" CommandName="Remove"
                                                                                        Visible='<%# IsVisible(Eval("ForConversion","{0}"))%>' CausesValidation="false"
                                                                                        CommandArgument='<%# Bind("RecNum") %>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="65px" HorizontalAlign="Center" VerticalAlign="Top" />
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label Visible="false" runat="server" ID="lblAuctionDetailNo" Text='<%# Bind("AuctionDetailNo") %>'></asp:Label>
                                                                                    <asp:Label Visible="false" runat="server" ID="lblRowIndex" Text='<%# Bind("RecNum") %>'></asp:Label>
                                                                                    <asp:Label Visible="false" runat="server" ID="lblForConversion" Text='<%# Bind("ForConversion") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                    <p>
                                                                        <asp:LinkButton runat="server" ID="btnAddSubItem" OnClick="btnAddSubItem_Click">Add Sub Item</asp:LinkButton></p>
                                                                    <p>
                                                                        <asp:GridView ID="gvFileAttachment" runat="server" CssClass="itemDetails_1" AutoGenerateColumns="false"
                                                                            OnRowCommand="gvFileAttachment_RowCommand" ShowFooter="true">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="&nbsp;File Attachment">
                                                                                    <ItemStyle CssClass="valueGridItem" Width="100%" />
                                                                                    <HeaderStyle CssClass="itemDetails_th" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Literal ID="litAttach" runat="server" Text="&nbsp;" Visible='<%# IsAttached(Eval("Attached").ToString()) %>' />
                                                                                        <asp:Image ID="imgAttach" runat="server" ImageUrl="~/web/themes/images/paperclip.gif" Width="10px"
                                                                                            Height="10px" Visible='<%# IsAttached(Eval("Attached").ToString()) %>' />
                                                                                        <asp:LinkButton ID="lnkRemoveAttachment" runat="server" CommandName="Remove" CommandArgument='<% #Bind("ID")%>'
                                                                                            Visible='<%# IsAttached(Eval("Attached").ToString()) %>' CausesValidation="false">Remove</asp:LinkButton>
                                                                                        <asp:Label ID="lblFile" runat="server" CssClass="discount" Text='<% #Bind("Original", ": {0}")%>'
                                                                                            Visible='<%# IsAttached(Eval("Attached").ToString()) %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <p style="color: Red;">
                                                                                            &nbsp;<asp:FileUpload ID="fileUpload" runat="server" Width="330px" /><br />
                                                                                            &nbsp;&nbsp;<asp:LinkButton ID="lnkAttach" runat="server" CausesValidation="false"
                                                                                                CommandName="Attach">Add To Attachments List</asp:LinkButton>
                                                                                            <asp:Literal ID="addAttachmentMsg" runat="server"></asp:Literal>
                                                                                        </p>
                                                                                    </FooterTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                        <asp:SqlDataSource ID="dsAttachments" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                            SelectCommand="sp_GetAuctionFileAttachments" SelectCommandType="StoredProcedure">
                                                                            <SelectParameters>
                                                                                <asp:SessionParameter Name="AuctionRefNo" SessionField="AuctionRefNo" Type="Int32" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                            </p>
                                                                        <p>                                                              </p>
                                                                    <p>
                                                                        <asp:HyperLink ID="lnkAddOTLS" runat="server">Add One-Time Supplier/Participant</asp:HyperLink>
                                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="itemDetails_1">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <th colspan="3" class="itemDetails_th">
                                                                                        &nbsp;Suppliers / Participants
                                                                                    </th>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <asp:ListBox Style="width: 250px; height: 200px" ID="lstSupplierA" runat="server"
                                                                                            DataSourceID="dsVendors" DataTextField="VendorName" DataValueField="VendorId"
                                                                                            SelectionMode="Multiple" OnDataBound="lstSupplierA_DataBound"></asp:ListBox>&nbsp;
                                                                                    </td>
                                                                                    <td align="center" style="width: 22px;">
                                                                                        <input style="width: 20px" id="btnSelectAll" type="button" value=">>" runat="server"
                                                                                            onserverclick="btnSelectAll_ServerClick" causesvalidation="false" /><br />
                                                                                        <input style="width: 20px" id="btnSelectOne" type="button" value=">" runat="server"
                                                                                            onserverclick="btnSelectOne_ServerClick" causesvalidation="false" /><br />
                                                                                        <input style="width: 20px" id="btnDeselectOne" type="button" value="<" runat="server"
                                                                                            onserverclick="btnDeselectOne_ServerClick" causesvalidation="false" /><br />
                                                                                        <input style="width: 20px" id="btnDeselectAll" type="button" value="<<" runat="server"
                                                                                            onserverclick="btnDeselectAll_ServerClick" causesvalidation="false" />
                                                                                    </td>
                                                                                    <td align="left">
                                                                                        <asp:ListBox Style="width: 250px; height: 200px" ID="lstSupplierB" runat="server"
                                                                                            SelectionMode="Multiple" ></asp:ListBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="3">
                                                                                        <asp:SqlDataSource ID="dsVendors" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                                            SelectCommand="sp_GetVendorsInCategoryAndSubCategory" SelectCommandType="StoredProcedure">
                                                                                            <SelectParameters>
                                                                                                <asp:ControlParameter ControlID="ddlCategory" DefaultValue=" " Name="CategoryId"
                                                                                                    PropertyName="SelectedValue" Type="String" />
                                                                                                <asp:ControlParameter ControlID="ddlSubCategory" DefaultValue=" " Name="SubCategoryId"
                                                                                                    PropertyName="SelectedValue" Type="String" />
                                                                                            </SelectParameters>
                                                                                        </asp:SqlDataSource>
                                                                                        <asp:SqlDataSource ID="dsParticipants" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                                            SelectCommand="s3p_EBid_GetAuctionItemSuppliers" SelectCommandType="StoredProcedure">
                                                                                            <SelectParameters>
                                                                                                <asp:SessionParameter Name="auctionRefno" SessionField="AuctionRefNo" Type="String" />
                                                                                            </SelectParameters>
                                                                                        </asp:SqlDataSource>
                                                                                        <asp:SqlDataSource ID="dsInvitedParticipants" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                                        SelectCommand="sp_GetAllBidEventParticipantsByBidDetailNo" SelectCommandType="StoredProcedure">
                                                                                        <SelectParameters>
                                                                                            <asp:ControlParameter Name="BidDetailNo" ControlID="hdnDetailNo" Type="String" />
                                                                                            <asp:Parameter Name="Status" DefaultValue="1" Type="String" />
                                                                                        </SelectParameters>
                                                                                        </asp:SqlDataSource>.
                                                                                        <asp:HiddenField ID="hdnVendor" runat="server" />
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </p>
                                                                        <asp:Label runat="server" ID="lblMessage" CssClass="discount"></asp:Label><div>
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:LinkButton runat="server" ID="btnSaveDraft" OnClick="btnSaveDraft_Click">Draft</asp:LinkButton>
                                                                                    <asp:LinkButton runat="server" ID="btnSubmit" OnClick="btnSubmit_Click">Submit</asp:LinkButton>
                                                                                    <asp:LinkButton runat="server" ID="lnkCancel" CausesValidation="false" OnClick="lnkCancel_Click">Cancel</asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                    <asp:TextBox runat="server" ID="txtSuppliers" Width="0" Height="0" BorderColor="White" BorderStyle="Solid" BorderWidth="1" ></asp:TextBox>
                                                                    <asp:TextBox runat="server" ID="txtPRDate" Width="0" Height="0" BorderColor="White" BorderStyle="Solid" BorderWidth="1" ></asp:TextBox>
                                                                    <asp:TextBox runat="server" ID="txtDeliveryDate" Width="0" Height="0" BorderColor="White" BorderStyle="Solid" BorderWidth="1" ></asp:TextBox>
                                                                    <asp:TextBox runat="server" ID="txtAuctionConfirmationDeadline" Width="0" Height="0" BorderColor="White" BorderStyle="Solid" BorderWidth="1" ></asp:TextBox>
                                                                    <asp:TextBox runat="server" ID="txtAuctionStartDate" Width="0" Height="0" BorderColor="White" BorderStyle="Solid" BorderWidth="1" ></asp:TextBox>
                                                                    <asp:TextBox runat="server" ID="txtAuctionEndDate" Width="0" Height="0" BorderColor="White" BorderStyle="Solid" BorderWidth="1" ></asp:TextBox>
                                                                    <asp:TextBox runat="server" ID="txtCompleteAuctionStartTime" Width="0" Height="0" BorderColor="White" BorderStyle="Solid" BorderWidth="1" ></asp:TextBox>
                                                                    <asp:TextBox runat="server" ID="txtCompleteAuctionEndTime" Width="0" Height="0" BorderColor="White" BorderStyle="Solid" BorderWidth="1" ></asp:TextBox>
                                                                    <asp:TextBox runat="server" ID="hdnItem" Width="0" Height="0" BorderColor="White" BorderStyle="Solid" BorderWidth="1" ></asp:TextBox>
                                                                    <asp:TextBox runat="server" ID="hdnDesc" Width="0" Height="0" BorderColor="White" BorderStyle="Solid" BorderWidth="1" ></asp:TextBox>
                                                                    <asp:TextBox runat="server" ID="hdnQty" Width="0" Height="0" BorderColor="White" BorderStyle="Solid" BorderWidth="1" ></asp:TextBox>
                                                                    <asp:TextBox runat="server" ID="hdnUOM" Width="0" Height="0" BorderColor="White" BorderStyle="Solid" BorderWidth="1" ></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <asp:SqlDataSource ID="dsEventDetails" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                SelectCommand="sp_GetBidEventDetails" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:SessionParameter Name="BidRefNo" SessionField="BidRefNo" Type="Int32" DefaultValue="0" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="dsItemDetails" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                SelectCommand="sp_GetBidItemDetails" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:SessionParameter Name="BidDetailNo" SessionField="BidDetailNo" Type="Int32"
                                            DefaultValue="0" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <input type="hidden" id="hdnAddNewRow" name="hdnAddNewRow" runat="server" />
                            <input type="hidden" id="hdnAddNewFile" name="hdnAddNewFile" runat="server" />
                            <input type="hidden" id="hdnAuctionRefNo" name="hdnAuctionRefNo" runat="server" />
                            <input type="hidden" id="hdnFileAttachments" name="hdnFileAttachments" runat="server" />
                            <input type="hidden" id="hdnActualFileNames" name="hdnActualFileNames" runat="server" />
                            <input type="hidden" id="hdnXMLFileName" name="hdnXMLFileName" runat="server" />
                            <input type="hidden" id="hdnSetFocusToSubItem" name="hdnSetFocusToSubItem" runat="server" />
                            <input type="hidden" id="hdnSetFocusToNewFile" name="hdnSetFocusToNewFile" runat="server" />
                            <input type="hidden" id="hdnIndex" name="hdnIndex" runat="server" />
                            <input type="hidden" id="hdnConfirmRemoveSubItem" name="hdnConfirmRemoveSubItem"
                                runat="server" />
                            <input type="hidden" id="hdnConfirmRemoveFileAttachment" name="hdnConfirmRemoveFileAttachment"
                                runat="server" />
                            <input id="__subcatID" type="hidden" name="__subcatID" runat="server" />
                            <input id="__groupID" type="hidden" name="__groupID" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvSuppliers" runat="server" ErrorMessage="Suppliers is a required field."
                                ControlToValidate="txtSuppliers" Display="None" SetFocusOnError="True" Enabled="False"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cuvValidate" runat="server" ClientValidationFunction="ValidatorIndividualAlert(this, args);"
                                Display="None"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td id="footer">
                            <EBid:Footer runat="server" ID="Footer" />
                            <script type="text/javascript">
                            <!--
                                function mainloop() 
                                { 
                                   try{ 
                                   
                                   if (!document.forms[0].ddlCompany) return; 
                                      var ddlCompany = document.forms[0].ddlCompany; 
                                      var company = ddlCompany.value; 
                                      var groupID = document.forms[0].ddlGroupDeptSec.value; 
                                      
                                   if (!document.forms[0].ddlCategory) return; 
                                      var ddlCategory = document.forms[0].ddlCategory; 
                                      var category = ddlCategory.value; 
                                      var subcatID = document.forms[0].ddlSubCategory.value; 
                                      
                                      
                                      if (company_old != company){ 
                                         company_old = company; 
                                         SendQuery("com",company,"c",category); 
                                      } 
                                         
                                      if (category_old != category){ 
                                         category_old = category; 
                                         SendQuery("com",company,"c",category); 
                                      } 
                                   } 
                                   catch (ex){} 
                                   setTimeout("mainloop();",10); 
                                } 

                                function Init() 
                                { 
                                   if (window.XMLHttpRequest) { // Non-IE browsers 
                                      _req = new XMLHttpRequest(); 
                                   } 
                                   else if (window.ActiveXObject){ // IE 
                                      _req = new ActiveXObject("Microsoft.XMLHTTP"); 
                                   } 
                                } 

                                SendQuery=function(key, val, key1, val1) //get data 
                                { 
                                   Init();    
                                   var url="../../GetAjaxData/GetAJAXData.aspx?" + key +"=" + val + "&" + key1 + "=" + val1; 
                                   if(_req!=null) { 
                                      _req.onreadystatechange = processStateChange; 
                                      _req.open("GET", url, true); _req.send(null); 
                                   } 
                                } 

                                clearSelection=function()
                                { 
                                   var _ddl = document.getElementById("ddlSubCategory"); 
                                   while (_ddl.childNodes.length >0){ 
                                      _ddl.removeChild(_ddl.childNodes[0]); 
                                   } 
                                   var o = document.createElement("Option"); 
                                   o.innerHTML = ""; //(All SubCategory)
                                   o.value =""; 
                                   _ddl.appendChild(o); 
                                   var _ddl1 = document.getElementById("ddlGroupDeptSec"); 
                                   while (_ddl1.childNodes.length >0){ 
                                      _ddl1.removeChild(_ddl1.childNodes[0]); 
                                   } 
                                   var o1 = document.createElement("Option"); 
                                   o1.innerHTML = "";//(All Group)
                                   o1.value =""; 
                                   _ddl1.appendChild(o1); 
                                   setHiddenfieldValue("__subcatID",""); 
                                   setHiddenfieldValue("__groupID",""); 
                                } 

                                processStateChange = function()
                                { 
                                   if (_req.readyState == 4){// only if "OK" 
                                      if (_req.status == 200) { 
                                         if(_req.responseText=="") 
                                            return false; 
                                         else{ 
                                            eval(_req.responseText); 
                                         } 
                                      } 
                                   } 
                                } 

                                populateDDL = function(v,t,ddlID,hiddenID)
                                { 
                                    _ddl = document.getElementById(ddlID); 
                                   while (_ddl.childNodes.length >0){ 
                                      _ddl.removeChild(_ddl.childNodes[0]); 
                                   } 
                                   var count = v.length; 
                                   var o = document.createElement("Option"); 
                                   o.innerHTML = ""; //(Select)
                                   o.value =""; 
                                   _ddl.appendChild(o); 
                                   var resetHiddenFldValue = true; 
                                   // alert(v.length); 
                                   for (var i=0; i<v.length; ++i){ 
                                      var op = document.createElement("Option"); 
                                      op.innerHTML = t[i]; 
                                      op.value = v[i]; 
                                      if (v[i] == document.getElementById(hiddenID).value){ 
                                         op.selected = true; 
                                         resetHiddenFldValue = false; 
                                      } 
                                      _ddl.appendChild(op); 
                                   } 
                                   if (resetHiddenFldValue) 
                                      document.getElementById(hiddenID).value=""; 
                                } 

                                var setHiddenfieldValue= function(fieldName,val)
                                { 
                                   document.getElementById(fieldName).value=val; 
                                } 

                                setTimeout("mainloop();",10); 
                            //-->
                           </script>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
