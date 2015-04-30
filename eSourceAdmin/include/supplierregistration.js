// Updated By: GA Sacramento 09022006
// **********************************************************************************************************
//------Begin Brands------//
function copyToBrandList()
{
    fromList = document.getElementById("Wizard1_lstBrandsCarried");
    toList = document.getElementById("lstSelectedBrandsCarried");
    if (toList.options.length > 0 && toList.options[0].value == 'temp')
    {
        toList.options.length = 0;
    }
    var sel = false;
    for (i=0;i<fromList.options.length;i++)
    {
        var current = fromList.options[i];
        if (current.selected)
        {
            sel = true;
            if (current.value == 'temp')
            {
            alert ('You cannot move this text!');
            return;
            }
            txt = current.text;
            val = current.value;
            toList.options[toList.length] = new Option(txt,val);
            fromList.options[i] = null;
            StoreToHiddenValue(trim(val));
            i--;
        }
    }
    if (!sel) alert ('You haven\'t selected any options!');
}

function removeFromBrandList()
{
    fromList = document.getElementById("lstSelectedBrandsCarried");
    toList = document.getElementById("lstBrandsCarried");
    if (toList.options.length > 0 && toList.options[0].value == 'temp')
    {
        toList.options.length = 0;
    }
    var sel = false;
    for (i=0;i<fromList.options.length;i++)
    {
        var current = fromList.options[i];
        if (current.selected)
        {
            sel = true;
            if (current.value == 'temp')
            {
            alert ('You cannot move this text!');
            return;
            }
            txt = current.text;
            val = current.value;
            toList.options[toList.length] = new Option(txt,val);
            fromList.options[i] = null;
            RemoveFromHiddenValue(trim(val));
            i--;
        }
    }
    if (!sel) alert ('You haven\'t selected any options!');
}

function copyAllBrands(){
    
    fromList = document.getElementById("lstBrandsCarried");
    toList = document.getElementById("lstSelectedBrandsCarried");
    
    for(i=0; i<fromList.options.length; i++)
    {
        //get value before it is appended
        var selectedvalue = fromList.options.item(i).value;
        toList.options[toList.length] = new Option(fromList.options.item(i).text,fromList.options.item(i).value);
        fromList.options[i] = null;
        //store selectedvalue to hidden
        StoreToHiddenValue(selectedvalue);
        i--;
    }
    selectNone(toList,fromList);
    setSize(toList,fromList);
}

function removeAllBrands(){
    
    fromList = document.getElementById("lstSelectedBrandsCarried");
    toList = document.getElementById("lstBrandsCarried");
    for(i=0; i<fromList.options.length; i++)
    {
        var selectedvalue = fromList.options.item(i).value;
        toList.options[toList.length] = new Option(fromList.options.item(i).text,fromList.options.item(i).value);
        fromList.options[i] = null;
        RemoveFromHiddenValue(selectedvalue);
        i--;
    }
    selectNone(toList,fromList);
    setSize(toList,fromList);
}

function copyABrand()
{
   fromList = document.getElementById("lstBrandsCarried");
   toList = document.getElementById("lstSelectedBrandsCarried");
   var addIndex = fromList.selectedIndex;
   if(addIndex < 0)
      return;
   //get selected value before object is appended to destination list
   var selectedvalue = trim(fromList.options.item(addIndex).value);
   //object is appended
   toList.appendChild(fromList.options.item(addIndex));
   //store selected item to hidden value
   StoreToHiddenValueB(selectedvalue);
   selectNone(toList,fromList);
   setSize(toList,fromList);
}

function removeABrand()
{
   fromList = document.getElementById("lstSelectedBrandsCarried");
   toList = document.getElementById("lstBrandsCarried");
   var addIndex = fromList.selectedIndex;
   if(addIndex < 0)
      return;
   //get selected value before object is appended to destination list
   var selectedvalue = trim(fromList.options.item(addIndex).value);
   //object is appended
   toList.appendChild(fromList.options.item(addIndex));
   //remove selected item from hidden value
   RemoveFromHiddenValue(selectedvalue);
   selectNone(toList,fromList);
   setSize(toList,fromList);
} 

function CheckIfValueExists(selectedBrandsA, selectedvalue)
{
    for (var i=0; i<selectedBrandsA.length; i++)
    {
        if (trim(selectedBrandsA[i]) == trim(selectedvalue))
            return true;
    }
    return false;
}

function RemoveFromHiddenValue(selectedvalue)
{
   if (trim(document.getElementById("hdnBrands").value) != "")
   {
   
    var selectedBrands = trim(document.getElementById("hdnBrands").value);
    var selectedBrandsA = selectedBrands.split(",");
    var newString = "";
    for (var i=0; i<selectedBrandsA.length; i++)
    {
        if (selectedBrandsA[i]!=selectedvalue)
        {
            if (newString == "")
                newString = trim(selectedBrandsA[i]);
            else
                newString = newString + "," + trim(selectedBrandsA[i]);
        }
    }
    document.getElementById("hdnBrands").value = trim(newString);
   }
}
//------End Brands------//
//------Begin Item Carried------//
function copyToItemList()
{
    fromList = document.getElementById("lstItemsCarried");
    toList = document.getElementById("lstSelectedItemsCarried");
    if (toList.options.length > 0 && toList.options[0].value == 'temp')
    {
        toList.options.length = 0;
    }
    var sel = false;
    for (i=0;i<fromList.options.length;i++)
    {
        var current = fromList.options[i];
        if (current.selected)
        {
            sel = true;
            if (current.value == 'temp')
            {
            alert ('You cannot move this text!');
            return;
            }
            txt = current.text;
            val = current.value;
            toList.options[toList.length] = new Option(txt,val);
            fromList.options[i] = null;
            StoreItemToHiddenValue(trim(val));
            i--;
        }
    }
    if (!sel) alert ('You haven\'t selected any options!');
}

function removeFromItemList()
{
    fromList = document.getElementById("lstSelectedItemsCarried");
    toList = document.getElementById("lstItemsCarried");
    if (toList.options.length > 0 && toList.options[0].value == 'temp')
    {
        toList.options.length = 0;
    }
    var sel = false;
    for (i=0;i<fromList.options.length;i++)
    {
        var current = fromList.options[i];
        if (current.selected)
        {
            sel = true;
            if (current.value == 'temp')
            {
            alert ('You cannot move this text!');
            return;
            }
            txt = current.text;
            val = current.value;
            toList.options[toList.length] = new Option(txt,val);
            fromList.options[i] = null;
            RemoveItemFromHiddenValue(trim(val));
            i--;
        }
    }
    if (!sel) alert ('You haven\'t selected any options!');
}

function copyAllItems(){
    
    fromList = document.getElementById("lstItemsCarried");
    toList = document.getElementById("lstSelectedItemsCarried");
    for(i=0; i<fromList.options.length; i++)
    {
        var selectedvalue = trim(fromList.options.item(i).value);
        toList.options[toList.length] = new Option(fromList.options.item(i).text,fromList.options.item(i).value);
        fromList.options[i] = null;
        StoreItemToHiddenValue(selectedvalue);
        i--;
    }
    selectNone(toList,fromList);
    setSize(toList,fromList);
}

function removeAllItems(){
    
    fromList = document.getElementById("lstSelectedItemsCarried");
    toList = document.getElementById("lstItemsCarried");
    for(i=0; i<fromList.options.length; i++)
    {
        var selectedvalue = trim(fromList.options.item(i).value)
        toList.options[toList.length] = new Option(fromList.options.item(i).text,fromList.options.item(i).value);
        fromList.options[i] = null;
        RemoveItemFromHiddenValue(selectedvalue);
        i--;
    }
    selectNone(toList,fromList);
    setSize(toList,fromList);
}

function copyAnItem()
{
   fromList = document.getElementById("lstItemsCarried");
   toList = document.getElementById("lstSelectedItemsCarried");
   var addIndex = fromList.selectedIndex;
   if(addIndex < 0)
      return;
   var selectedvalue = trim(fromList.options.item(addIndex).value);
   toList.appendChild(fromList.options.item(addIndex));
   StoreItemToHiddenValue(selectedvalue);
   selectNone(toList,fromList);
   setSize(toList,fromList);
}

function removeAnItem()
{
   fromList = document.getElementById("lstSelectedItemsCarried");
   toList = document.getElementById("lstItemsCarried");
   var addIndex = fromList.selectedIndex;
   if(addIndex < 0)
      return;
   var selectedvalue = trim(fromList.options.item(addIndex).value);
   toList.appendChild(fromList.options.item(addIndex));
   RemoveItemFromHiddenValue(selectedvalue);
   selectNone(toList,fromList);
   setSize(toList,fromList);
} 

function CheckIfItemExists(selectedItemsA, selectedvalue)
{
    for (var i=0; i<selectedItemsA.length; i++)
    {
        if (trim(selectedItemsA[i]) == trim(selectedvalue))
            return true;
    }
    return false;
}

function  StoreItemToHiddenValue(selectedvalue)
{
   if (trim(document.getElementById("hdnItems").value) != "")
   {
    var selectedItems = trim(document.getElementById("hdnItems").value);
    var selectedItemsA =  selectedItems.split(",");
    var exists = CheckIfItemExists(selectedItemsA, selectedvalue);
    if  (!exists)
         document.getElementById("hdnItems").value = selectedItems + "," + trim(selectedvalue);               
   }
   else
        document.getElementById("hdnItems").value = trim(selectedvalue);
}

function RemoveItemFromHiddenValue(selectedvalue)
{
   if (trim(document.getElementById("hdnItems").value) != "")
   {
   
    var selectedItems = trim(document.getElementById("hdnItems").value);
    var selectedItemsA = selectedItems.split(",");
    var newString = "";
    for (var i=0; i<selectedItemsA.length; i++)
    {
        if (selectedItemsA[i]!=selectedvalue)
        {
            if (newString == "")
                newString = trim(selectedItemsA[i]);
            else
                newString = newString + "," + trim(selectedItemsA[i]);
        }
    }
    document.getElementById("hdnItems").value = trim(newString);
   }
}
//------End Item Carried------//
//------Begin Services Offered------//
function copyToServiceOfferedList()
{
    fromList = document.getElementById("lstServicesOffered");
    toList = document.getElementById("lstSelectedServicesOffered");
    if (toList.options.length > 0 && toList.options[0].value == 'temp')
    {
        toList.options.length = 0;
    }
    var sel = false;
    for (i=0;i<fromList.options.length;i++)
    {
        var current = fromList.options[i];
        if (current.selected)
        {
            sel = true;
            if (current.value == 'temp')
            {
            alert ('You cannot move this text!');
            return;
            }
            txt = current.text;
            val = current.value;
            toList.options[toList.length] = new Option(txt,val);
            fromList.options[i] = null;
            StoreServiceToHiddenValue(trim(val));
            i--;
        }
    }
    if (!sel) alert ('You haven\'t selected any options!');
}

function removeFromServiceOfferedList()
{
    fromList = document.getElementById("lstSelectedServicesOffered");
    toList = document.getElementById("lstServicesOffered");
    if (toList.options.length > 0 && toList.options[0].value == 'temp')
    {
        toList.options.length = 0;
    }
    var sel = false;
    for (i=0;i<fromList.options.length;i++)
    {
        var current = fromList.options[i];
        if (current.selected)
        {
            sel = true;
            if (current.value == 'temp')
            {
            alert ('You cannot move this text!');
            return;
            }
            txt = current.text;
            val = current.value;
            toList.options[toList.length] = new Option(txt,val);
            fromList.options[i] = null;
            RemoveServiceFromHiddenValue(trim(val));
            i--;
        }
    }
    if (!sel) alert ('You haven\'t selected any options!');
}

function copyAllServicesOffered(){
    
    fromList = document.getElementById("lstServicesOffered");
    toList = document.getElementById("lstSelectedServicesOffered");
    for(i=0; i<fromList.options.length; i++)
    {
        var selectedvalue = trim(fromList.options.item(i).value);
        toList.options[toList.length] = new Option(fromList.options.item(i).text,fromList.options.item(i).value);
        fromList.options[i] = null;
        StoreServiceToHiddenValue(selectedvalue);
        i--;
    }
    selectNone(toList,fromList);
    setSize(toList,fromList);
}

function removeAllServicesOffered(){
    
    fromList = document.getElementById("lstSelectedServicesOffered");
    toList = document.getElementById("lstServicesOffered");
    for(i=0; i<fromList.options.length; i++)
    {
        var selectedvalue = trim(fromList.options.item(i).value);
        toList.options[toList.length] = new Option(fromList.options.item(i).text,fromList.options.item(i).value);
        fromList.options[i] = null;
        RemoveServiceFromHiddenValue(selectedvalue);
        i--;
    }
    selectNone(toList,fromList);
    setSize(toList,fromList);
}

function copyAServiceOffered()
{
   fromList = document.getElementById("lstServicesOffered");
   toList = document.getElementById("lstSelectedServicesOffered");
   var addIndex = fromList.selectedIndex;
   if(addIndex < 0)
      return;
   var selectedvalue = trim(fromList.options.item(addIndex).value);
   toList.appendChild(fromList.options.item(addIndex));
   StoreServiceToHiddenValue(selectedvalue);
   selectNone(toList,fromList);
   setSize(toList,fromList);
}

function removeAServiceOffered()
{
   fromList = document.getElementById("lstSelectedServicesOffered");
   toList = document.getElementById("lstServicesOffered");
   var addIndex = fromList.selectedIndex;
   if(addIndex < 0)
      return;
   var selectedvalue = trim(fromList.options.item(addIndex).value);
   toList.appendChild(fromList.options.item(addIndex));
   RemoveServiceFromHiddenValue(selectedvalue);
   selectNone(toList,fromList);
   setSize(toList,fromList);
}

function CheckIfServiceExists(selectedServiceA, selectedvalue)
{
    for (var i=0; i<selectedServiceA.length; i++)
    {
        if (trim(selectedServiceA[i]) == trim(selectedvalue))
            return true;
    }
    return false;
}

function  StoreServiceToHiddenValue(selectedvalue)
{
   if (trim(document.getElementById("hdnServices").value) != "")
   {
    var selectedService = trim(document.getElementById("hdnServices").value);
    var selectedServiceA =  selectedService.split(",");
    var exists = CheckIfServiceExists(selectedServiceA, selectedvalue);
    if  (!exists)
         document.getElementById("hdnServices").value = selectedService + "," + trim(selectedvalue);               
   }
   else
        document.getElementById("hdnServices").value = trim(selectedvalue);
}

function RemoveServiceFromHiddenValue(selectedvalue)
{
   if (trim(document.getElementById("hdnServices").value) != "")
   {
   
    var selectedService = trim(document.getElementById("hdnServices").value);
    var selectedServiceA = selectedService.split(",");
    var newString = "";
    for (var i=0; i<selectedServiceA.length; i++)
    {
        if (selectedServiceA[i]!=selectedvalue)
        {
            if (newString == "")
                newString = trim(selectedServiceA[i]);
            else
                newString = newString + "," + trim(selectedServiceA[i]);
        }
    }
    document.getElementById("hdnServices").value = trim(newString);
   }
} 
//------End Services Offered------//
//------Begin Location------//
function copyToLocationList()
{
    fromList = document.getElementById("lstLocation");
    toList = document.getElementById("lstSelectedLocation");
    if (toList.options.length > 0 && toList.options[0].value == 'temp')
    {
        toList.options.length = 0;
    }
    var sel = false;
    for (i=0;i<fromList.options.length;i++)
    {
        var current = fromList.options[i];
        if (current.selected)
        {
            sel = true;
            if (current.value == 'temp')
            {
            alert ('You cannot move this text!');
            return;
            }
            txt = current.text;
            val = current.value;
            toList.options[toList.length] = new Option(txt,val);
            fromList.options[i] = null;
            StoreLocationToHiddenValue(trim(val));
            i--;
        }
    }
    if (!sel) alert ('You haven\'t selected any options!');
}

function removeFromLocationList()
{
    fromList = document.getElementById("lstSelectedLocation");
    toList = document.getElementById("lstLocation");
    if (toList.options.length > 0 && toList.options[0].value == 'temp')
    {
        toList.options.length = 0;
    }
    var sel = false;
    for (i=0;i<fromList.options.length;i++)
    {
        var current = fromList.options[i];
        if (current.selected)
        {
            sel = true;
            if (current.value == 'temp')
            {
            alert ('You cannot move this text!');
            return;
            }
            txt = current.text;
            val = current.value;
            toList.options[toList.length] = new Option(txt,val);
            fromList.options[i] = null;
            RemoveLocationFromHiddenValue(trim(val));
            i--;
        }
    }
    if (!sel) alert ('You haven\'t selected any options!');
}

function copyAllLocations(){
    
    fromList = document.getElementById("lstLocation");
    toList = document.getElementById("lstSelectedLocation");
    for(i=0; i<fromList.options.length; i++)
    {
        var selectedvalue = trim(fromList.options.item(i).value);
        toList.options[toList.length] = new Option(fromList.options.item(i).text,fromList.options.item(i).value);
        fromList.options[i] = null;
        StoreLocationToHiddenValue(selectedvalue);
        i--;
    }
    selectNone(toList,fromList);
    setSize(toList,fromList);
}

function removeAllLocations(){
    
    fromList = document.getElementById("lstSelectedLocation");
    toList = document.getElementById("lstLocation");
    for(i=0; i<fromList.options.length; i++)
    {
        var selectedvalue = trim(fromList.options.item(i).value);
        toList.options[toList.length] = new Option(fromList.options.item(i).text,fromList.options.item(i).value);
        fromList.options[i] = null;
        RemoveLocationFromHiddenValue(selectedvalue);
        i--;
    }
    selectNone(toList,fromList);
    setSize(toList,fromList);
}

function copyALocation()
{
   fromList = document.getElementById("lstLocation");
   toList = document.getElementById("lstSelectedLocation");
   var addIndex = fromList.selectedIndex;
   if(addIndex < 0)
      return;
   var selectedvalue =  trim(fromList.options.item(addIndex).value);
   toList.appendChild(fromList.options.item(addIndex));
   StoreLocationToHiddenValue(selectedvalue);
   selectNone(toList,fromList);
   setSize(toList,fromList);
}

function removeALocation()
{
   fromList = document.getElementById("lstSelectedLocation");
   toList = document.getElementById("lstLocation");
   var addIndex = fromList.selectedIndex;
   if(addIndex < 0)
      return;
   var selectedvalue =  trim(fromList.options.item(addIndex).value);
   toList.appendChild(fromList.options.item(addIndex));
   RemoveLocationFromHiddenValue(selectedvalue);
   selectNone(toList,fromList);
   setSize(toList,fromList);
} 
        
function CheckIfLocationExists(selectedLocationA, selectedvalue)
{
    for (var i=0; i<selectedLocationA.length; i++)
    {
        if (trim(selectedLocationA[i]) == trim(selectedvalue))
            return true;
    }
    return false;
}

function  StoreLocationToHiddenValue(selectedvalue)
{
   if (trim(document.getElementById("hdnLocation").value) != "")
   {
    var selectedLocation = trim(document.getElementById("hdnLocation").value);
    var selectedLocationA =  selectedLocation.split(",");
    var exists = CheckIfLocationExists(selectedLocationA, selectedvalue);
    if  (!exists)
         document.getElementById("hdnLocation").value = selectedLocation + "," + trim(selectedvalue);               
   }
   else
        document.getElementById("hdnLocation").value = trim(selectedvalue);
}
        
function RemoveLocationFromHiddenValue(selectedvalue)
{
   if (trim(document.getElementById("hdnLocation").value) != "")
   {
   
    var selectedLocation = trim(document.getElementById("hdnLocation").value);
    var selectedLocationA = selectedLocation.split(",");
    var newString = "";
    for (var i=0; i<selectedLocationA.length; i++)
    {
        if (selectedLocationA[i]!=selectedvalue)
        {
            if (newString == "")
                newString = trim(selectedLocationA[i]);
            else
                newString = newString + "," + trim(selectedLocationA[i]);
        }
    }
    document.getElementById("hdnLocation").value = trim(newString);
   }
}
//------End Location------//
        
function setSize(list1,list2){
    list1.size = getSize(list1);
    list2.size = getSize(list2);
}

function selectNone(list1,list2){
    list1.selectedIndex = -1;
    list2.selectedIndex = -1;
    addIndex = -1;
    selIndex = -1;
}

function getSize(list){
    /* Mozilla ignores whitespace, 
       IE doesn't - count the elements 
       in the list */
    var len = list.childNodes.length;
    var nsLen = 0;
    //nodeType returns 1 for elements
    for(i=0; i<len; i++){
        if(list.childNodes.item(i).nodeType==1)
            nsLen++;
    }
    if(nsLen<2)
        return 2;
    else
        return nsLen;
}        
//------Begin Category------//
function copyToCategoryList()
{
    fromList = document.getElementById("Wizard1_lstCategories");
    toList = document.getElementById("Wizard1_lstSelectedCategories");
    if (toList.options.length > 0 && toList.options[0].value == 'temp')
    {
        toList.options.length = 0;
    }
    var sel = false;
    var allval = "";
    for (i=0;i<fromList.options.length;i++)
    {
        var current = fromList.options[i];
        if (current.selected)
        {
            sel = true;
            if (current.value == 'temp')
            {
            alert ('You cannot move this text!');
            return;
            }
            txt = current.text;
            val = current.value;
            allval = ((trim(allval)=='') ? trim(val) : trim(allval) + ',' + trim(val));
            toList.options[toList.length] = new Option(txt,val);
            fromList.options[i] = null;
            StoreToHiddenValueC(trim(val));
            i--;
        }
    }
    SendQuery("cat",trim(allval)); 
    if (!sel) alert ('You haven\'t selected any options!');    
}
       
function removeFromCategoryList()
{
    fromList = document.getElementById("Wizard1_lstSelectedCategories");
    toList = document.getElementById("Wizard1_lstCategories");
    if (toList.options.length > 0 && toList.options[0].value == 'temp')
    {
        toList.options.length = 0;
    }
    var sel = false;
    var allval ="";
    for (i=0;i<fromList.options.length;i++)
    {
        var current = fromList.options[i];
        if (current.selected)
        {
            sel = true;
            if (current.value == 'temp')
            {
            alert ('You cannot move this text!');
            return;
            }
            txt = current.text;
            val = current.value;
            allval = ((trim(allval)=='') ? trim(val) : trim(allval) + ',' + trim(val));
            toList.options[toList.length] = new Option(txt,val);
            fromList.options[i] = null;
            RemoveFromHiddenValue(trim(val));
            i--;
        }
    }
     SendQuery("remcat",allval);
    if (!sel) alert ('You haven\'t selected any options!');
}
       
function copyAllCategories(){
    
    fromList = document.getElementById("Wizard1_lstCategories");
    toList = document.getElementById("Wizard1_lstSelectedCategories");
    var allval ="";
    for(i=0; i<fromList.options.length; i++)
    {
        //get value before it is appended
        var selectedvalue = fromList.options.item(i).value;
        //--store value to allval for SendQuery()
        allval = ((trim(allval)=='') ? trim(selectedvalue) : trim(allval) + ',' + trim(selectedvalue));
        //---
        toList.options[toList.length] = new Option(fromList.options.item(i).text,fromList.options.item(i).value);
        fromList.options[i] = null;
        //store selectedvalue to hidden
        StoreToHiddenValueC(selectedvalue);
        i--;
    }
    selectNone(toList,fromList);
    setSize(toList,fromList);
    SendQuery("cat",allval);   
}
        
function removeAllCategories(){
    var allval ="";
    fromList = document.getElementById("Wizard1_lstSelectedCategories");
    toList = document.getElementById("Wizard1_lstCategories");
    for(i=0; i<fromList.options.length; i++)
    {
        var selectedvalue = fromList.options.item(i).value;
        //--store value to allval for SendQuery()
        allval = ((trim(allval)=='') ? trim(selectedvalue) : trim(allval) + ',' + trim(selectedvalue));
        //---
        toList.options[toList.length] = new Option(fromList.options.item(i).text,fromList.options.item(i).value);
        fromList.options[i] = null;
        RemoveFromHiddenValue(selectedvalue);
        i--;
    }
    selectNone(toList,fromList);
    setSize(toList,fromList);
    SendQuery("remcat",allval);
}
    
function copyACategory()
{            
   fromList = document.getElementById("Wizard1_lstCategories");
   toList = document.getElementById("Wizard1_lstSelectedCategories");
   var addIndex = fromList.selectedIndex;
   if(addIndex < 0)
      return;
   //get selected value before object is appended to destination list
   var selectedvalue = trim(fromList.options.item(addIndex).value);
   //object is appended
   toList.appendChild(fromList.options.item(addIndex));
   //store selected item to hidden value
   StoreToHiddenValueC(selectedvalue);
   selectNone(toList,fromList);
   setSize(toList,fromList);
   SendQuery("cat",selectedvalue);
}
        
function removeACategory()
{
   fromList = document.getElementById("Wizard1_lstSelectedCategories");
   toList = document.getElementById("Wizard1_lstCategories");
   var addIndex = fromList.selectedIndex;
   if(addIndex < 0)
      return;
   //get selected value before object is appended to destination list
   var selectedvalue = trim(fromList.options.item(addIndex).value);
   //object is appended
   toList.appendChild(fromList.options.item(addIndex));
   //remove selected item from hidden value
   RemoveFromHiddenValue(selectedvalue);
   selectNone(toList,fromList);
   setSize(toList,fromList);
   SendQuery("remcat",selectedvalue);
} 
        
function CheckIfValueExists(selectedCategoryA, selectedvalue)
{
    for (var i=0; i<selectedCategoryA.length; i++)
    {
        if (trim(selectedCategoryA[i]) == trim(selectedvalue))
            return true;
    }
    return false;
}

function  StoreToHiddenValueC(selectedvalue)
{
   if (trim(document.getElementById("Wizard1_hdnCategories").value) != "")
   {
    var selectedCategory = trim(document.getElementById("Wizard1_hdnCategories").value);
    var selectedCategoryA =  selectedCategory.split(",");
    var exists = CheckIfValueExists(selectedCategoryA, selectedvalue);
    if  (!exists)
         document.getElementById("Wizard1_hdnCategories").value = selectedCategory + "," + selectedvalue;               
   }
   else
        document.getElementById("Wizard1_hdnCategories").value = selectedvalue;
}

function  StoreToHiddenValueB(selectedvalue)
{
   if (trim(document.getElementById("hdnBrands").value) != "")
   {
    var selectedBrands = trim(document.getElementById("hdnBrands").value);
    var selectedBrandsA =  selectedBrands.split(",");
    var exists = CheckIfValueExists(selectedBrandsA, selectedvalue);
    if  (!exists)
         document.getElementById("hdnBrands").value = selectedBrands + "," + selectedvalue;               
   }
   else
        document.getElementById("hdnBrands").value = selectedvalue;
}
        
function RemoveFromHiddenValue(selectedvalue)
{
   if (trim(document.getElementById("Wizard1_hdnCategories").value) != "")
   {
   
    var selectedCategory = trim(document.getElementById("Wizard1_hdnCategories").value);
    var selectedCategoryA = selectedCategory.split(",");
    var newString = "";
    for (var i=0; i<selectedCategoryA.length; i++)
    {
        if (selectedCategoryA[i]!=selectedvalue)
        {
            if (newString == "")
                newString = trim(selectedCategoryA[i]);
            else
                newString = newString + "," + trim(selectedCategoryA[i]);
        }
    }
    document.getElementById("Wizard1_hdnCategories").value = trim(newString);
   }
}
//------  End Categories     ------//
//------  Begin Subcategory  ------//
function copyToSubCategoryList()
{
    fromList = document.getElementById("Wizard1_lstSubCategory");    
    toList = document.getElementById("Wizard1_lstSelectedSubCategories");
    if (toList.options.length > 0 && toList.options[0].value == 'temp')
    {
        toList.options.length = 0;
    }
    var sel = false;
    for (i=0;i<fromList.options.length;i++)
    {
        var current = fromList.options[i];
        if (current.selected)
        {
            sel = true;
            if (current.value == 'temp')
            {
            alert ('You cannot move this text!');
            return;
            }
            txt = current.text;
            val = current.value;
            toList.options[toList.length] = new Option(txt,val);
            fromList.options[i] = null;
            StoreSubCategoryToHiddenValue(trim(val));
            i--;
        }
    }
    if (!sel) alert ('You haven\'t selected any options!');
}
       
function removeFromSubCategoryList()
{
    fromList = document.getElementById("Wizard1_lstSelectedSubCategories");    
    toList = document.getElementById("Wizard1_lstSubCategory");
    if (toList.options.length > 0 && toList.options[0].value == 'temp')
    {
        toList.options.length = 0;
    }
    var sel = false;
    for (i=0;i<fromList.options.length;i++)
    {
        var current = fromList.options[i];
        if (current.selected)
        {
            sel = true;
            if (current.value == 'temp')
            {
            alert ('You cannot move this text!');
            return;
            }
            txt = current.text;
            val = current.value;
            toList.options[toList.length] = new Option(txt,val);
            fromList.options[i] = null;
            RemoveSubCategoryFromHiddenValue(trim(val));
            i--;
        }
    }
    if (!sel) alert ('You haven\'t selected any options!');
}
       
function copyAllSubCategories()
{            
    fromList = document.getElementById("Wizard1_lstSubCategory");
    toList = document.getElementById("Wizard1_lstSelectedSubCategories");
    for(i=0; i<fromList.options.length; i++)
    {
        //get value before it is appended
        var selectedvalue = fromList.options.item(i).value;
        toList.options[toList.length] = new Option(fromList.options.item(i).text,fromList.options.item(i).value);
        fromList.options[i] = null;
        //store selectedvalue to hidden
        StoreSubCategoryToHiddenValue(selectedvalue);
        i--;
    }
    selectNone(toList,fromList);
    setSize(toList,fromList);
}
        
function removeAllSubCategories()
{
    fromList = document.getElementById("Wizard1_lstSelectedSubCategories");
    toList = document.getElementById("Wizard1_lstSubCategory");
    for(i=0; i<fromList.options.length; i++)
    {
        var selectedvalue = fromList.options.item(i).value;
        toList.options[toList.length] = new Option(fromList.options.item(i).text,fromList.options.item(i).value);
        fromList.options[i] = null;
        RemoveSubCategoryFromHiddenValue(selectedvalue);
        i--;
    }
    selectNone(toList,fromList);
    setSize(toList,fromList);
}
    
function copyASubCategory()
{
   fromList = document.getElementById("Wizard1_lstSubCategory");   
   toList = document.getElementById("Wizard1_lstSelectedSubCategories");
   var addIndex = fromList.selectedIndex;
   if(addIndex < 0)
      return;
   //get selected value before object is appended to destination list
   var selectedvalue = trim(fromList.options.item(addIndex).value);
   //object is appended
   toList.appendChild(fromList.options.item(addIndex));
   //store selected item to hidden value
   StoreSubCategoryToHiddenValue(selectedvalue);
   selectNone(toList,fromList);
   setSize(toList,fromList);
 }
        
function removeASubCategory()
{
   fromList = document.getElementById("Wizard1_lstSelectedSubCategories");
   toList = document.getElementById("Wizard1_lstSubCategory");
   var addIndex = fromList.selectedIndex;
   if(addIndex < 0)
      return;
   //get selected value before object is appended to destination list
   var selectedvalue = trim(fromList.options.item(addIndex).value);
   //object is appended
   toList.appendChild(fromList.options.item(addIndex));
   //remove selected item from hidden value
   RemoveSubCategoryFromHiddenValue(selectedvalue);
   selectNone(toList,fromList);
   setSize(toList,fromList);
 } 
        
function CheckIfSubCategoryValueExists(selectedSubCategoryA, selectedvalue)
{
    for (var i=0; i<selectedSubCategoryA.length; i++)
    {
        if (trim(selectedSubCategoryA[i]) == trim(selectedvalue))
            return true;
    }
    return false;
}

function StoreSubCategoryToHiddenValue(selectedvalue)
{
   if (trim(document.getElementById("Wizard1_hdnSubCategory").value) != "")
   {
    var selectedSubCategory = trim(document.getElementById("Wizard1_hdnSubCategory").value);
    var selectedSubCategoryA =  selectedSubCategory.split(",");
    var exists = CheckIfSubCategoryValueExists(selectedSubCategoryA, selectedvalue);
    if  (!exists)
         document.getElementById("Wizard1_hdnSubCategory").value = selectedSubCategory + "," + selectedvalue;               
   }
   else
        document.getElementById("Wizard1_hdnSubCategory").value = selectedvalue;
}
        
function RemoveSubCategoryFromHiddenValue(selectedvalue)
{    
    if (trim(document.getElementById("Wizard1_hdnSubCategory").value) != "")
    {        
        var selectedSubCategory = trim(document.getElementById("Wizard1_hdnSubCategory").value);
        var selectedSubCategoryA = selectedSubCategory.split(",");
        var newString = "";
        for (var i=0; i<selectedSubCategoryA.length; i++)
        {
            if (selectedSubCategoryA[i]!=selectedvalue)
            {
                if (newString == "")
                    newString = trim(selectedSubCategoryA[i]);
                else
                    newString = newString + "," + trim(selectedSubCategoryA[i]);
            }
        }
        document.getElementById("Wizard1_hdnSubCategory").value = trim(newString);
    }
}        
// ----------------------- //
// -------- AJAX --------- //
function Init() 
{ 
   if (window.XMLHttpRequest) { // Non-IE browsers 
      _req = new XMLHttpRequest(); 
   } 
   else if (window.ActiveXObject){ // IE 
      _req = new ActiveXObject("Microsoft.XMLHTTP"); 
   } 
} 

SendQuery = function(key, val) //get data 
{ 
   Init(); 
   //alert(key + "----" + val);  
   var url="../getajaxdata/getsubcategorydata.aspx?" + key +"=" + val; 
   //alert(url);
   if(_req!=null) 
   { 
      _req.onreadystatechange = processStateChange; 
      _req.open("GET", url, true); 
      _req.send(null); 
   } 
} 

processStateChange = function()
{ 
    //alert(_req.readyState);
    if (_req.readyState == 4)
    {// only if "OK" 
      //alert("STAT:" + _req.status);
      if (_req.status == 200) 
      { 
         if(_req.responseText=="") 
            return false; 
         else{ 
            //alert(_req.responseText);
            eval(_req.responseText); 
         } 
      } 
    } 
} 

populateDDL = function(v,t,ddlID)
{   
    _ddl = document.getElementById(ddlID); 
    var count = v.length;     
    for (var i=0; i<v.length; ++i)
    { 
        var op = document.createElement("Option"); 
        op.innerHTML = t[i]; 
        op.value = v[i];  
        _ddl.appendChild(op); 
    }     
//    setTimeout('copyAllSubCategories()',100);
} 

RemoveFromList = function(v,t,ddlID,hiddenID)
{     
    _ddl = document.getElementById(ddlID);     
    
    if (_ddl != null)
    {
        while (_ddl.childNodes.length >0)
        { 
          _ddl.removeChild(_ddl.childNodes[0]); 
        } 
        var count = v.length;         
        for (var i=0; i<v.length; ++i)
        { 
          var op = document.createElement("Option"); 
          op.innerHTML = t[i]; 
          op.value = v[i];  
          _ddl.appendChild(op); 
        } 

        var lstTempRemoveSubCategory = document.getElementById("lstTempRemoveSubCategory")        
        var tempLength = lstTempRemoveSubCategory.options.length;        
        var lstSubCategory = document.getElementById("Wizard1_lstSubCategory");        
        var lstSelectedSubCategories = document.getElementById("Wizard1_lstSelectedSubCategories");        
        
        for (var i=0; i<tempLength; ++i)
        {
            for (var j=0; j<lstSubCategory.options.length; j++)
            {
                if (lstSubCategory.options.item(j).value == lstTempRemoveSubCategory.options.item(i).value)
                {
                    lstSubCategory.options[j] = null;
                }
            }   
            for (var k=0; k<lstSelectedSubCategories.options.length; k++)
            {
                if (lstSelectedSubCategories.options.item(k).value == lstTempRemoveSubCategory.options.item(i).value)
                {                    
                    RemoveSubCategoryFromHiddenValue(lstSelectedSubCategories.options.item(k).value);
                    lstSelectedSubCategories.options[k] = null;
                }
            }            
        }
    }
} 

var setHiddenfieldValue = function(fieldName,val)
{ 
   document.getElementById(fieldName).value=val; 
} 