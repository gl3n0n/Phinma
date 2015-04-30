// Created By: Roberto Collantes Jr
// Updated By: GA Sacramento 09022006
// **********************************************************************************************************
var selectedList;
var availableList;

function delAttribute(sourceList, targetList, listContainer)
{
   availableList = document.getElementById(sourceList);
   selectedList = document.getElementById(targetList);   

   for(var i=selectedList.length; i>0; i--)
   {
       if(selectedList.options[i-1].selected == true)
       {
           var selecteditem = selectedList.options.item(i-1).value;
           RemoveItemFromList(selecteditem,listContainer);
           availableList.appendChild(selectedList.options.item(i-1));
       }
   }
   
   selectNone(selectedList,availableList);
   setSize(availableList,selectedList);    
}

function addAttribute(sourceList, targetList, listContainer)
{    
    availableList = document.getElementById(sourceList);
    selectedList = document.getElementById(targetList);
 
    for(var i=availableList.length; i>0; i--)
    {
        if(availableList.options[i-1].selected == true)
        {
            AddItemIntoList(trim(availableList.options.item(i-1).value), listContainer);
            selectedList.appendChild(availableList.options.item(i-1));
        }
    }      
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


function RemoveItemFromList(selecteditem, listContainer)
{    
    var listHolder = trim(document.getElementById(listContainer).value);     
    //alert("S: " + selecteditem);
    var SList = listHolder.split(",");
    var newListHolder= "";
    for (var j = 0; j < SList.length; j++)
    {   
        if (SList[j] != selecteditem)
            {
                if (newListHolder =="")
                        newListHolder = SList[j];
                else
                        newListHolder = newListHolder + "," + SList[j];
            }
    }    
    //alert("N: " + newListHolder);
    document.getElementById(listContainer).value = newListHolder;    
}

function AddItemIntoList(selecteditem, listContainer)
{
   var listHolder = trim(document.getElementById(listContainer).value);
   //alert("S: " + selecteditem);
   if (listHolder == "")
        listHolder = selecteditem;
   else
        listHolder = listHolder + "," + selecteditem;
   //alert("N: " + listHolder);
   document.getElementById(listContainer).value = listHolder;
}

function delAll(sourceList, targetList, listContainer){
   availableList = document.getElementById(sourceList);
   selectedList = document.getElementById(targetList);
   
    var len = selectedList.length -1;
    
    for(i=len; i>=0; i--){
        var selecteditem = trim(selectedList.options.item(i).value);
        RemoveItemFromList(selecteditem,listContainer);
        availableList.appendChild(selectedList.item(i));
    }
    
    selectNone(selectedList,availableList);
    setSize(selectedList,availableList);   
}

function addAll(sourceList, targetList, listContainer){
    availableList = document.getElementById(sourceList);
    selectedList = document.getElementById(targetList);
    
    var len = availableList.length -1;
    for(i=len; i>=0; i--)
    {
        AddItemIntoList(trim(availableList.item(i).value), listContainer);
        selectedList.appendChild(availableList.item(i));
    }
    selectNone(selectedList,availableList);
    setSize(selectedList,availableList);
}

function getSelected()
{
    var optionList = document.getElementById("lbBrandsSelected").options;
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

function NumericValidator(kyCode)
{
    if((kyCode < 48) || (kyCode > 57))
    {
    //    alert("Please Enter Numeric Values Only!");
        return false;
    }
    
    return true;
}

function PhoneAndNoValidator(objName)
{
    //alert(tbName);
    var num = objName.value;
    var kyCode = window.event ? event.keyCode : event.which;
    
    if((kyCode != 45) && (kyCode != 40) && (kyCode != 41))
        if(!NumericValidator(kyCode))
            return false;
        //    objName.focus();
}

function NumberOnlyValidator(objName)
{
    var num = objName.value;
    var kyCode = window.event ? event.keyCode : event.which;
    
    if(!NumericValidator(kyCode))
        return false;
    //    objName.focus();
}

function ReturnAverage(tbName1, tbName2)
{
    var num1 = document.getElementById(tbName1).value;
    var num2;
    if((num1.length > 0) && (num1.length != 3))
    {
        num2 = 100 - parseInt(num1);
        document.getElementById(tbName2).value = num2;
    }
    else if(num1.length == 3)
    {
      document.getElementById(tbName1).value = "100";
      document.getElementById(tbName2).value = "0";
    }
    else
    {
       document.getElementById(tbName1).value = "0";
       document.getElementById(tbName2).value = "100";
    }
}

function currencyFormat(fld, milSep, decSep, e) 
{
    var sep = 0;
    var key = '';
    var i = j = 0;
    var len = len2 = 0;
    var strCheck = '0123456789';
    var aux = aux2 = '';
    var whichCode = (window.Event) ? e.which : e.keyCode;
    if (whichCode == 13) return true;  // Enter
    if (whichCode == 8) return true;  // Delete (Bug fixed)
    key = String.fromCharCode(whichCode);  // Get key value from key code
    if (strCheck.indexOf(key) == -1) return false;  // Not a valid key
    len = fld.value.length;
    for(i = 0; i < len; i++)
    if ((fld.value.charAt(i) != '0') && (fld.value.charAt(i) != decSep)) break;
    aux = '';
    for(; i < len; i++)
    if (strCheck.indexOf(fld.value.charAt(i))!=-1) aux += fld.value.charAt(i);
    aux += key;
    len = aux.length;
    if (len == 0) fld.value = '';
    if (len == 1) fld.value = '0'+ decSep + '0' + aux;
    if (len == 2) fld.value = '0'+ decSep + aux;
    if (len > 2) {
    aux2 = '';
    for (j = 0, i = len - 3; i >= 0; i--) {
    if (j == 3) {
    aux2 += milSep;
    j = 0;
    }
    aux2 += aux.charAt(i);
    j++;
    }
    fld.value = '';
    len2 = aux2.length;
    for (i = len2 - 1; i >= 0; i--)
    fld.value += aux2.charAt(i);
    fld.value += decSep + aux.substr(len - 2, len);
    }
    return false;
}

 function ValidateCompanyClass(source, args)
 {
    if ((document.getElementById("chkCompanyClassification_0").checked)||
        (document.getElementById("chkCompanyClassification_1").checked)||
        (document.getElementById("chkCompanyClassification_2").checked)||
        (document.getElementById("chkCompanyClassification_3").checked)||
        (document.getElementById("chkCompanyClassification_4").checked))
        args.IsValid = true;
    else
        args.IsValid = false;
 }
 
function CheckCompanyClass()
{    
    if (document.frmRegistration.Wizard1_chkCompanyClassification_4 != null)
    {   
        if (document.frmRegistration.Wizard1_chkCompanyClassification_4.checked)
          {
            document.frmRegistration.Wizard1_chkCompanyClassification_0.checked = false; 
            document.frmRegistration.Wizard1_chkCompanyClassification_0.disabled = true;
            document.frmRegistration.Wizard1_chkCompanyClassification_1.checked = false; 
            document.frmRegistration.Wizard1_chkCompanyClassification_1.disabled = true;
            document.frmRegistration.Wizard1_chkCompanyClassification_2.checked = false; 
            document.frmRegistration.Wizard1_chkCompanyClassification_2.disabled = true;
            document.frmRegistration.Wizard1_chkCompanyClassification_3.checked = false; 
            document.frmRegistration.Wizard1_chkCompanyClassification_3.disabled = true;
          }
         else
         {
            document.frmRegistration.Wizard1_chkCompanyClassification_0.disabled = false;
            document.frmRegistration.Wizard1_chkCompanyClassification_1.disabled = false;
            document.frmRegistration.Wizard1_chkCompanyClassification_2.disabled = false;
            document.frmRegistration.Wizard1_chkCompanyClassification_3.disabled = false;
         }
     }
}
// **********************************************************************************************************
