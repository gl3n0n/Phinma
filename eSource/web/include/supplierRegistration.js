//------Begin Brands------//
        function copyToBrandList()
        {
            fromList = document.getElementById("lstBrandsCarried");
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
           StoreToHiddenValue(selectedvalue);
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

        function  StoreToHiddenValue(selectedvalue)
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