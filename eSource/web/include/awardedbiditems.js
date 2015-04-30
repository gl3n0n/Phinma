// Created By: Roberto Collantes Jr
// Last Update: 08042006
// **********************************************************************************************************
    function doSearch()
    {
        if (event.keyCode == 13)
        {
             // cancel the default submit
            event.returnValue=false;
            event.cancel = true;
            
            // submit the form by programmatically clicking the specified button
            lbSearch.click();
        }
    }
    function checkMonth()
    {
       var month = document.getElementById("ddlMonth").value;
        //alert(ddlMonth.value);
        if ((month==0) ||
            (month==null)) 
         alert("Please Select Month First");
         
        if( (event.which && event.which > 57) || ((event.keyCode && event.keyCode > 57) && 
            (event.keyCode && event.keyCode < 48)))
            alert("Please enter numeric values only!");
        
    }
    function setDay()
    {
        document.getElementById("tbDay").value = "dd";
    }
    function checkDay()
    {
        var month = document.getElementById("ddlMonth").value;
        var day = document.getElementById("tbDay").value;
        
        if(month != 0)
        {
            if(((month == 2 && day > 29) || (day > 31)) || 
               (day != parseInt(day) && day.length != 0))
            {
                alert("Please Enter a Valid Day!");
                setDay();
            }
        }    
        
        if(day == 0 ||
           day.length == 0)
        {
            setDay();
        }
        
    }
    function checkYear()
    {
        var year = document.getElementById("tbYear").value;
        
        if(year != parseInt(year) && year.length != 0)
        {
            alert("Please Enter a Valid Year!");
            document.getElementById("tbYear").value = "yyyy";
        }
        
        if(year.length == 0)
            document.getElementById("tbYear").value = "yyyy";
    }
// **********************************************************************************************************

