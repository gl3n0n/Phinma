// JScript File
//for select supplier pages
function UnderLine(object, state)
{
    switch (state)
    {
        case "over":
           object.style.textDecorationUnderline = false;
            break;
        default:
            object.style.textDecorationUnderline = true;
            break;
    }
}

var selectedList;
var availableList;
        
function createListObjects(lstSupplierA, lstSupplierB){
    availableList = lstSupplierA;
    selectedList = lstSupplierB
}

function delAttribute(){
   var selIndex = selectedList.selectedIndex;
   if(selIndex < 0)
      return;
   //check for one time supplier----------------------------//
   var selecteditem = selectedList.options.item(selIndex).value;
   var vendorsForACategoryId = document.getElementById("hdnVendorsForACategoryId").value;
   if (vendorsForACategoryId.indexOf(selecteditem) > -1)
        {
            
            RemoveFromSupplierList(selecteditem);
            availableList.appendChild(selectedList.options.item(selIndex))
        }
   else
        {
            RemoveFromOTLSList(selecteditem);   
            selectedList.options[selIndex]  = null;
        }
   selectNone(selectedList,availableList);
   setSize(availableList,selectedList);
}

function addAttribute(){
   var addIndex = availableList.selectedIndex;
   if(addIndex < 0)
      return;
   AddIntoSupplierList(trim(availableList.options.item(addIndex).value));
   selectedList.appendChild(availableList.options.item(addIndex));
   selectNone(selectedList,availableList);
   setSize(selectedList,availableList);
}

function setTop(top){
    document.getElementById('someLayer').style.top = top;
}

function setLayerTop(lyr,top){
    lyr.style.top = top;
}

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

function RemoveFromSupplierList(selecteditem)
{
        var SupplierList = trim(document.getElementById("hdnSuppliers").value);
        var SList = SupplierList.split(",");
        var NewSupplierList = "";
        for (var j = 0; j < SList.length; j++)
        {   
            if (SList[j] != selecteditem)
                {
                    if (NewSupplierList =="")
                            NewSupplierList = SList[j];
                    else
                            NewSupplierList = NewSupplierList + "," + SList[j];
                }
        }
        
        document.getElementById("hdnSuppliers").value = NewSupplierList;
        
}

function RemoveFromOTLSList(selecteditem)
{
    var OTLSList = trim(document.getElementById("hdnOTS").value);
    var SList = OTLSList.split(",");
    var NewOTLSList = "";
    for (var j = 0; j < SList.length; j++)
        {
            if (SList[j] != selecteditem)
                {
                    if (NewOTLSList =="")
                        NewOTLSList = SList[j];
                    else
                        NewOTLSList = NewOTLSList + "," + SList[j];
                }
         }
    document.getElementById("hdnOTS").value = NewOTLSList;
}

function delAll()
{
    var len = selectedList.length -1;
    for(i=len; i>=0; i--){
        var selecteditem = trim(selectedList.options.item(i).value);
        var vendorsForACategoryId = document.getElementById("hdnVendorsForACategoryId").value;
        if (vendorsForACategoryId.indexOf(selecteditem) > -1)
            {
                RemoveFromSupplierList(selecteditem);
                availableList.appendChild(selectedList.item(i));
            }
        else
            {
                RemoveFromOTLSList(selecteditem);       
                selectedList.options[i]  = null;
            }
    }
    selectNone(selectedList,availableList);
    setSize(selectedList,availableList);
    
}

function addAll(){
    var len = availableList.length -1;
    for(i=len; i>=0; i--)
    {
        AddIntoSupplierList(trim(availableList.item(i).value));
        selectedList.appendChild(availableList.item(i));
    }
    selectNone(selectedList,availableList);
    setSize(selectedList,availableList);
}

function AddIntoSupplierList(selecteditem)
{
       var SupplierList = trim(document.getElementById("hdnSuppliers").value);
       
       if (SupplierList == "")
            SupplierList = selecteditem;
       else
            SupplierList = SupplierList + "," + selecteditem;
            
       document.getElementById("hdnSuppliers").value = SupplierList;
}

function getSelected()
{
    var optionList = selectedList.options;
    var data = "";
    var len = optionList.length;
    for(i=0; i<len; i++)
    {
        if (data == "")
        {
            data = optionList.item(i).value;
        }
        else 
        {
            data += ",";
            data += optionList.item(i).value;
        }
    }
    return data;
}

function getSize(list)
{
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

function RefreshMe(otsIds, catid, suppliers)
{
    var url = window.location.href; 
    var index = url.indexOf("?");
    //concatenate url, remove querystring portion
    if (index != "-1")
        url = url.substr(0, index);
    url = url + "?catid=" + catid + "&suppliers=" + suppliers + "&ots=" + otsIds;  
       
    window.location=url;
}

function ShowOTLSDetails()
{
    var vCategoryId = document.getElementById("hdnCategoryId").value;
    var vOtsId = document.getElementById("hdnOTS").value;
    var vSuppliers = document.getElementById("hdnSuppliers").value;
    window.open("otlsdetails.aspx?CategoryId=" + vCategoryId + "&ots=" + vOtsId + "&Suppliers=" + vSuppliers, "x", "toolbar=no, menubar=no, width=800; height=375, top=250, left=275");
}

       

