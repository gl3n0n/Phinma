// Created By: GA Sacramento 
// Last Update: 07112006
// **********************************************************************************************************
// Convert seconds to this format(1 day 1 hr. 1 min. 1 sec.)
function ConvertSecondsToCounterDateTimeString(sec)
{
	var days = parseInt(sec / 86400);
	var hours = parseInt((sec % 86400) / 3600);	
	var mins = parseInt(((sec % 86400) % 3600) / 60);
	var secs = parseInt((((sec % 86400) % 3600) % 60) % 60);
	
	return (days > 0 ? (days > 1 ? days + " days " : days + " day ") : "") + " " +
			(hours > 0 ? (hours > 1 ? hours + " hrs. " : hours + " hr. ") : "") + " " +
			(mins > 0 ? (mins > 1 ? mins + " mins. " : mins + " min. ") : "") + " " +
			(secs + " sec. ");
}
// **********************************************************************************************************
// Convert seconds to this format(12:01:01 AM)
function ConvertSecondsToDateTimeString(sec)
{	
	var hours = parseInt((sec % 86400) / 3600);	
	var mins = parseInt(((sec % 86400) % 3600) / 60);
	var secs = parseInt((((sec % 86400) % 3600) % 60) % 60);
	
	return ((hours < 10 ? "0" + hours : hours) + ":" + 
			(mins < 10 ? "0" + mins : mins) + ":" + 
			(secs < 10 ? "0" + secs : secs) + " " +
			(hours < 13 ? "AM" : "PM"));
}
// **********************************************************************************************************
// Restrict number inputs only
function NumbersOnly(e)
{
	var keynum
	var keychar
	var numcheck
    
	if(window.event) // IE
	{
		keynum = e.keyCode
	}
	else if(e.which) // Netscape/Firefox/Opera
	{
		keynum = e.which
	}
			
	keychar = String.fromCharCode(keynum)
	numcheck = /\d/	
	
	var lbl = document.getElementById("lblMessage");		
	if (keynum == 13)
	{
		return keynum;
	}
	
	if (numcheck.test(keychar) == false)
	{			
		if (lbl != null)				
			lbl.innerHTML = "Please input numbers only.";
	}		
	else
	{
		if (lbl != null)				
			lbl.innerHTML = "";
	}
	
	return numcheck.test(keychar);
}

// **********************************************************************************************************
// Restrict number inputs only
function NumbersOnly(e, nxtCtrl)
{
	var keynum
	var keychar
	var numcheck	
	var nxt = document.getElementById(nxtCtrl);
    
	if(window.event) // IE
	{
		keynum = e.keyCode
	}
	else if(e.which) // Netscape/Firefox/Opera
	{
		keynum = e.which
	}			
			
	keychar = String.fromCharCode(keynum)
	numcheck = /\d/	
	
	var lbl = document.getElementById("lblMessage");		
	if (keynum == 13)
	{
		return keynum;
	}		
	else if ((keynum == 46) && (nxt != null))
	{
	    nxt.focus();
	    nxt.select();	    
	    return false;
	}	
	else if ((numcheck.test(keychar) == false) && (keynum != 46))
	{			
		if (lbl != null)				
			lbl.innerHTML = "Please input numbers only.";		
		return false;
	}		
	else
	{
		if (lbl != null)				
			lbl.innerHTML = "";		
		return numcheck.test(keychar);
	}
}

// **********************************************************************************************************
function DigitsOnly(e)
{
    var keynum
	var keychar
	var numcheck
    
	if(window.event) // IE
	{
		keynum = e.keyCode
	}
	else if(e.which) // Netscape/Firefox/Opera
	{
		keynum = e.which
	}
			
	keychar = String.fromCharCode(keynum)
	numcheck = /\d/	
	
	if (keynum == 8) // backspace
	    return keynum;
	if (keynum == 46) // delete
	    return keynum;
	if (keynum == 9) // tab
	    return keynum;
	if (keynum == 16) // shift
	    return keynum;
	if ((keynum >= 48) && (keynum <= 57)) // 0 - 9
	    return keynum;
	if ((keynum >= 96) && (keynum <= 105)) // 0 - 9
	    return keynum;
	
	return numcheck.test(keychar);
}

function DigitsOnlyAndTransferFocus(e, nxtControl)
{
    var keynum
	var keychar
	var numcheck
    
	if(window.event) // IE
	{
		keynum = e.keyCode
	}
	else if(e.which) // Netscape/Firefox/Opera
	{
		keynum = e.which
	}
			
	keychar = String.fromCharCode(keynum)
		
	if ((keynum == 190) || (keynum == 110)) // if period
	{
	    if (nxtControl != null)
	        nxtControl.focus();
	    return false;
	}
	
	numcheck = /\d/	
	
	if (keynum == 8) // backspace
	    return keynum;
	if (keynum == 46) // delete
	    return keynum;
	if (keynum == 9) // tab
	    return keynum;
	if (keynum == 16) // shift
	    return keynum;
	if ((keynum >= 48) && (keynum <= 57)) // 0 - 9
	    return keynum;
	if ((keynum >= 96) && (keynum <= 105)) // 0 - 9
	    return keynum;
	
	return numcheck.test(keychar);		
}

// **********************************************************************************************************
// Auto-Reset textbox values if empty
function ResetIfEmpty(e)
{		
	var txt = e;
	if (txt != null)
	{			
		if ((txt.value == null) || (txt.value == ""))
			txt.value = "00";
		if (txt.value.length == 1)
			txt.value = "0" + txt.value;
	}
}

function ResetIfEmpty2(e)
{		
	var txt = e;
	if (txt != null)
	{			
		if ((txt.value == null) || (txt.value == ""))
			txt.value = "0";		
	}	
}

function ResetFloatIfEmpty(e)
{		
	var txt = e;
	if (txt != null)
	{			
		if ((txt.value == null) || (txt.value == ""))
			txt.value = "0.00";
		if (txt.indexOf(".") = -1)
			txt.value = txt.value + ".00";
	}
}
// **********************************************************************************************************
// The following functions are used for gridview checkboxes (Check All)
function ChangeHeaderAsNeeded()
{
	// Whenever a checkbox in the GridView is toggled, we need to
	// check the Header checkbox if ALL of the GridView checkboxes are
	// checked, and uncheck it otherwise
	if (CheckBoxIDs != null)
	{
		// check to see if all other checkboxes are checked
		for (var i = 1; i < CheckBoxIDs.length; i++)
		{
			var cb = document.getElementById(CheckBoxIDs[i]);
			if (!cb.checked)
			{
				// Whoops, there is an unchecked checkbox, make sure
				// that the header checkbox is unchecked
				ChangeCheckBoxState(CheckBoxIDs[0], false);
				return;
			}
		}
        
		// If we reach here, ALL GridView checkboxes are checked
		ChangeCheckBoxState(CheckBoxIDs[0], true);
	}
}
	
function ChangeCheckBoxState(id, checkState)
{
  var cb = document.getElementById(id);
  if (cb != null)
	 cb.checked = checkState;
}

function ChangeAllCheckBoxStates(checkState)
{
  // Toggles through all of the checkboxes defined in the CheckBoxIDs array
  // and updates their value to the checkState input parameter
  if (CheckBoxIDs != null)
  {
	 for (var i = 0; i < CheckBoxIDs.length; i++)
		ChangeCheckBoxState(CheckBoxIDs[i], checkState);
  }
}

function IsCheckBoxChecked()
{
	if (CheckBoxIDs != null)
	{
		var ctr = 0;
		
		// check to see if all other checkboxes are checked
		for (var i = 1; i < CheckBoxIDs.length; i++)
		{
			var cb = document.getElementById(CheckBoxIDs[i]);
			if (cb.checked)
				ctr++;
		}			
		
		if (ctr > 0)
			return true;
		else 
			return false;
	}
	else
		return false;	
}
// **********************************************************************************************************
// Display/Hide status
// Call SetStatus() on body onload event
function DisplayStatus(input)
{
	window.status=input;
    return false;
}

function HideStatus()
{
	window.status = "Globe";
	return true;
}		

function SetStatus()
{		
	document.onmouseover = HideStatus;
	document.onmouseout = HideStatus;	
	var i = 0;
	for (;i<document.anchors.length; i++)
	{
		var s = document.anchors[i];
		s.onmouseover = HideStatus;
		s.onmouseout = HideStatus;
	}
}
// **********************************************************************************************************
function addElement() 
{
	var ni = document.getElementById('myDiv');
	var numi = document.getElementById('theValue');
	var num = (document.getElementById('theValue').value -1) + 2;
	numi.value = num;
	
	var newdiv = document.createElement('div');
	var divIdName = 'div' +num;
	newdiv.setAttribute('id',divIdName);			

	newdiv.innerHTML = '<input type="file" id="input' + num + '"/>&nbsp;<a href="javascript:;" onclick="removeElement(\''+divIdName+'\')" style="font-size:11px">Remove</a>';
	ni.appendChild(newdiv);			
}
		
function removeElement(divNum) 
{
	var d = document.getElementById('myDiv');
	var olddiv = document.getElementById(divNum);
	d.removeChild(olddiv);
}
// **********************************************************************************************************
function AddComma(nStr)
{
	nStr += '';
	x = nStr.split('.');
	x1 = x[0];
	x2 = x.length > 1 ? '.' + x[1] : '';
	var rgx = /(\d+)(\d{3})/;
	while (rgx.test(x1)) {
		x1 = x1.replace(rgx, '$1' + ',' + '$2');
	}
	return x1 + x2;
}

// **********************************************************************************************************

function clickButton(e, buttonid)
{ 
    var bt = document.getElementById(buttonid); 
    if (typeof bt == 'object')
    { 
        if(navigator.appName.indexOf("Netscape")>(-1)){ 
              if (e.keyCode == 13){ 
                    bt.click(); 
                    return false; 
              } 
        } 
        if (navigator.appName.indexOf("Microsoft Internet Explorer")>(-1)){ 
              if (event.keyCode == 13){ 
                    bt.click(); 
                    return false; 
              } 
        } 
    }
} 
// **********************************************************************************************************
function roundOff(val, decimals) 
{
    var result1 = val * Math.pow(10, decimals);
    var result2 = Math.round(result1);
    var result3 = result2 / Math.pow(10, decimals);
    
    return padWithZeros(result3, decimals);
}

function padWithZeros(val, dec) 
{    
    var value_string = val.toString();
    var decimal_location = value_string.indexOf(".");
    
    if (decimal_location == -1) 
    {
        decimal_part_length = 0;
        value_string += dec > 0 ? "." : "";
    }
    else 
    {
        decimal_part_length = value_string.length - decimal_location - 1;
    }
    var pad_total = dec - decimal_part_length;
   
    if (pad_total > 0) 
    {
        for (var counter = 1; counter <= pad_total; counter++)
            value_string += "0";
    }
    return value_string;
}

function removeCommas(val)
{
    return val.replace(",", "");    
}