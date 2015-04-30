// Created By: GA Sacramento 
// Last Update: 10062006
// **********************************************************************************************************
function ShowProducts(ctrlCategory, ctrlSubCategory, ctrlItem, ctrlId)
{    
    var selectedIndex;       
    
    selectedIndex = parseInt(ctrlCategory.selectedIndex);
    var vCategoryId = trim(ctrlCategory.options.item(selectedIndex).value);
    
    selectedIndex = parseInt(ctrlSubCategory.selectedIndex);
    var vSubCategoryId = trim(ctrlSubCategory.options.item(selectedIndex).value);
    
    var vSearchText = trim(ctrlItem.value);        
    
    newwindow = window.open('productitems.aspx?cid=' + vCategoryId + '&scid=' + vSubCategoryId + '&st=' + vSearchText + '&ctrlid=' + ctrlId , 'x', 'toolbar=no, menubar=no, width=550; height=500, top=80, left=325, resizable=1, scrollbars=1');
}
// **********************************************************************************************************
function AddOTLS(ctrlCategory, ctrlSubCategory)
{    
    var c = document.getElementById(ctrlCategory);
    var sc = document.getElementById(ctrlSubCategory);
    
    var selectedIndex; 
    var selectedIndex2;      
    
    selectedIndex = parseInt(c.selectedIndex);
    var vCategoryId = trim(c.options.item(selectedIndex).value); 
    
    selectedIndex2 = parseInt(sc.selectedIndex);
    
    var vSubCategoryId = trim(sc.options.item(selectedIndex2).value);
    
    if (vCategoryId != "")
        window.open("otlsdetails_add.aspx?cid=" + vCategoryId + "&scid=" + vSubCategoryId, "x", "toolbar=no, menubar=no, width=800; height=425, top=250, left=275");
    else
        alert('Please select a category first.');
}

// **********************************************************************************************************
// Compute total
function ComputeTotal(txtQty, txtPrice, txtPriceCents, txtTotal)
{   
    if ((txtQty != null) || (txtPrice != null) || (txtPriceCents != null) || (txtTotal != null))
        txtTotal.value = AddComma((Math.round(parseFloat(txtQty.value) *  parseFloat(txtPrice.value + '.' + txtPriceCents.value) * 100)/100).toFixed(2));
}
// Compute total
function ComputeGrossTotal(txtQty, txtPrice, txtPriceCents, txtDiscount, txtDiscountCents, txtDeliveryCost, txtDeliveryCostCents, txtSubTotal, txtTotal, txtTotalBidTenderDiscount)
{   
    var vQuantity = txtQty.value;
    var vPrice = parseFloat(txtPrice.value.replace(/,/g,"") + '.' + txtPriceCents.value);
    var vDiscount = parseFloat(txtDiscount.value.replace(/,/g, "") + '.' + txtDiscountCents.value);
    var vDeliveryCost = parseFloat(txtDeliveryCost.value.replace(/,/g,"") + '.' + txtDeliveryCostCents.value);
    var vSubTotal = (Math.ceil((vQuantity * vPrice) * 100) / 100).toFixed(2);
    var vDiscountPercent = ((vDiscount / vPrice) * 100).toFixed(2);
    var vTotal = (Math.ceil((parseFloat(vQuantity * vPrice) + parseFloat(vDeliveryCost - (vDiscount * vQuantity))) * 100) / 100).toFixed(2);
    
    var vNewPrice = parseFloat(txtPrice.value.replace(/,/g,"") + '.00');
    var vNewDiscount = parseFloat(txtDiscount.value.replace(/,/g,"") + '.00');
    var vNewDeliveryCost = parseFloat(txtDeliveryCost.value.replace(/,/g,"") + '.00');
    
    txtPrice.value = AddComma(vNewPrice);
    txtDiscount.value = AddComma(vNewDiscount);
    txtDeliveryCost.value = AddComma(vNewDeliveryCost);
    txtTotalBidTenderDiscount.value = vPrice!=0 ? AddComma(vDiscountPercent) + " %" : "0 %";
    txtSubTotal.value = AddComma(vSubTotal);
    txtTotal.value = AddComma(vTotal);
}
// **********************************************************************************************************
function RefreshMe(selectedProduct, ctrlId)
{    
    var ids = trim(selectedProduct).split("|");
    var SKU = trim(ids[0]);
    var ItemName = trim(ids[1]);
    var ItemDesc = trim(ids[2]);
    var UOM = trim(ids[3]);
    
    var tbItem = document.getElementById("gvBidItems_" + ctrlId + "_txtItem");
    var tbItemDesc = document.getElementById("gvBidItems_" + ctrlId + "_txtDescription");
    var tbUOM = document.getElementById("gvBidItems_" + ctrlId + "_txtUnitOfMeasure");
    var tbSKU = document.getElementById("gvBidItems_" + ctrlId + "_hdnSKU");
    
    if (tbItem != null)
        tbItem.value = SKU + " - " + ItemName;
    if (tbItemDesc != null)
        tbItemDesc.value = ItemDesc;
    if (tbUOM != null)
        tbUOM.value = UOM;
        tbUOM.readOnly = true;
    if (tbSKU != null)
        tbSKU.value = SKU;
    
    tbItem.focus();    
}
// **********************************************************************************************************
function Close()
{
    window.close();
}
// **********************************************************************************************************
function SelectProduct(ctrlId)
{
    var prods = document.getElementById("lstProducts");
    selectedIndex = parseInt(prods.selectedIndex);
    
    var vItem = trim(prods.item(selectedIndex).value);
   
    if (vItem != '')
    {
        if (window.opener != null)
        {
            window.opener.RefreshMe(vItem, ctrlId);
            window.close();
        }
    }
    else
    {
        alert("No product has been selected. Please click on an item in the listbox to select a product.");
    }	    
}
// **********************************************************************************************************
function Select(text, listbox)
{
    text.value = listbox.options.item(listbox.selectedIndex).text;
    document.getElementById("hdnSelectedProduct").value = listbox.options.item(listbox.selectedIndex).value;
}
// **********************************************************************************************************
function AutoComplete (field, select, property, forcematch) 
{
	var found = false;
	for (var i = 0; i < select.options.length; i++) 
	{
		if (select.options[i][property].toUpperCase().indexOf(field.value.toUpperCase()) == 0) 
		{
			found=true; break;
		}
	}
	if (found) { select.selectedIndex = i; }
	else { select.selectedIndex = -1; }
	if (field.createTextRange) {
		if (forcematch && !found) {
			field.value=field.value.substring(0,field.value.length-1); 
		return;
		}
	var cursorKeys ="8;46;37;38;39;40;33;34;35;36;45;";
	if (cursorKeys.indexOf(event.keyCode+";") == -1) {
		var r1 = field.createTextRange();
		var oldValue = r1.text;
		var newValue = found ? select.options[i][property] : oldValue;
		if (newValue != field.value) {
			field.value = newValue;
			var rNew = field.createTextRange();
			rNew.moveStart('character', oldValue.length) ;
			rNew.select();
			}
		}
	}
}
// **********************************************************************************************************
function FocusOn(ctrlId)
{
    //alert('hi');
    if (document.getElementById(ctrlId) != null)
    {
        var c = document.getElementById(ctrlId);
        c.focus();
        c.select();
    }
}
// **********************************************************************************************************
function RefreshMe1(vendorid, vendorname)
{     
    var lstSuppliersB = document.getElementById("lstSupplierB");
    var hdnVendor = document.getElementById("hdnVendor");
    var vendorIds =  document.getElementById("txtSuppliers");
    
    if (lstSuppliersB != null)
    {
       lstSuppliersB.add(new Option(vendorname,vendorid)); 
    }
    
    if (hdnVendor != null)
    {
        hdnVendor.value = vendorid + ';' + vendorname;
    }     
     
    if (vendorIds != null)
    {
        if (vendorIds.value == "")
        {
            vendorIds.value = vendorid;
        }
        else
        {
            vendorIds.value = vendorIds.value + "," + vendorid;
        }
    }
}
// **********************************************************************************************************
function AddSuppliersB(vendorid, vendorname)
{
    if (window.opener != null)
    {
       window.opener.RefreshMe1(vendorid, 'OTS - ' + vendorname);
       window.close();
    }
}