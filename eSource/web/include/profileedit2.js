// Created By: GA Sacramento 
// Last Update: 07282006
// **********************************************************************************************************
var selectedList;
var availableList;

function delAttribute(sourceList, targetList, listContainer){
   availableList = document.getElementById(sourceList);
   selectedList = document.getElementById(targetList);
   
   for(var i=selectedList.length; i>0; i--)
   {
	   if(selectedList.options[i-1].selected == true)
	   {
		   var selecteditem = selectedList.options.item(i-1).value;
		   RemoveFromList(selecteditem,listContainer);
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
			AddIntoList(trim(availableList.options.item(i-1).value), listContainer);
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


function RemoveFromList(selecteditem, listContainer)
{
		var listHolder = trim(document.getElementById(listContainer).value);
		var SList = listHolder.split(",");
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
		document.getElementById(listContainer).value = NewSupplierList;
}

function delAll(sourceList, targetList, listContainer){
   availableList = document.getElementById(sourceList);
   selectedList = document.getElementById(targetList);
   
	var len = selectedList.length -1;
    
	for(i=len; i>=0; i--){
		var selecteditem = trim(selectedList.options.item(i).value);
		RemoveFromList(selecteditem,listContainer);
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
		AddIntoList(trim(availableList.item(i).value), listContainer);
		selectedList.appendChild(availableList.item(i));
	}
	selectNone(selectedList,availableList);
	setSize(selectedList,availableList);
}

function AddIntoList(selecteditem, listContainer)
{
	   var listHolder = trim(document.getElementById(listContainer).value);
       
	   if (listHolder == "")
			listHolder = selecteditem;
	   else
			listHolder = listHolder + "," + selecteditem;
       
	   document.getElementById(listContainer).value = listHolder;
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
// **********************************************************************************************************

