// Created By: GA Sacramento 
// Last Update: 08232006
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
	
	return numcheck.test(keychar)
}
// **********************************************************************************************************
// Restrict spaces in inputs, for passwords
function NoSpaces(e)
{	
	var keynum
	var keychar
	var spacecheck
    
	if(window.event) // IE
	{
		keynum = e.keyCode
	}
	else if(e.which) // Netscape/Firefox/Opera
	{
		keynum = e.which
	}
	
	keychar = String.fromCharCode(keynum)	
	spacecheck = /\S/	
		
	if (keynum == 13)
	{
		return keynum;
	}	
	
	return spacecheck.test(keychar);
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
	window.status = "GlobeTelecom";
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
// ----------------------------------------------------------------------------------------------------------
// Checks if the text, textarea, password, or file fields has no content.
function WithoutContent(ss) 
{
	if(ss.length > 0) { return false; }
	return true;
}
// ----------------------------------------------------------------------------------------------------------
// Checks if none of the set of text, textarea, password, or file fields have content.
function NoneWithContent(ss) 
{
	for(var i = 0; i < ss.length; i++) 
	{
		if(ss[i].value.length > 0) { return false; }
	}
	return true;
}
// ----------------------------------------------------------------------------------------------------------
// Checks if none of the set of radio buttons or checkboxes are checked.
function NoneWithCheck(ss) 
{
	for(var i = 0; i < ss.length; i++) 
	{
		if(ss[i].checked) { return false; }
	}
	return true;
}
// ----------------------------------------------------------------------------------------------------------
// Check if the single radio button or checkbox is unchecked.
function WithoutCheck(ss) 
{
	if(ss.checked) { return false; }
	return true;
}
// ----------------------------------------------------------------------------------------------------------
// Checks if selected drop-down list or select box entries have no value.
function WithoutSelectionValue(ss) 
{
	// NOTE: we'll start at index 1 since index 0 can't be chosen
	for(var i = 1; i < ss.length; i++) 
	{
		if(ss[i].selected) 
		{
			if(ss[i].value.length) { return false; }
		}
	}
	return true;
}
// ----------------------------------------------------------------------------------------------------------
// Checks if the selections in the two dropdown lists are the same
function SameSelection(ss1, ss2)
{
	// NOTE: we'll start at index 1 since index 0 can't be chosen
	for(var i = 1; i < ss1.length; i++) 
	{
		if(ss1[i].selected) 
		{
			if (ss2[i].selected) { return true; }
		}
	}
	return false;
}
// ----------------------------------------------------------------------------------------------------------
// Checks if the objects are the same
function IsMatch(ss1, ss2)
{
	if (ss1 = ss2) return true;
	return false;
}
// ----------------------------------------------------------------------------------------------------------
// Checks if email is valid
function IsValidEmail(str) 
{
	var x = str;
	var filter  = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;	
	return (filter.test(x));	
}
// ----------------------------------------------------------------------------------------------------------
// Verify if necessary fields are not empty
// For change password page
function VerifyData()
{	
	var errormessage = new String();
	var control = null;
	var currPwd = document.forms.form1.tbPassword;
	var newPwd = document.forms.form1.tbNewPassword;
	var confNewPwd = document.forms.form1.tbConfNewPassword;
	
	// Check if current password field is not blank
	if(WithoutContent(currPwd.value))
	{ 
		errormessage += "\n\nPlease enter your current password.";
		control = currPwd; 
	}
	
	// Check if new password field is not blank
	if(WithoutContent(newPwd.value))
	{ 
		errormessage += "\n\nPlease enter your new password."; 
		if (control == null) control = newPwd;
	}
	
	// Check if required password length
	if ((newPwd.value.length < 8) || (newPwd.value.length > 24))
	{
		errormessage += "\n\nPassword length should be from 8 to 24 characters.";	
		if (control == null) control = newPwd; 		
	}	
			
	// Check if confirmation password field is not blank
	if(WithoutContent(confNewPwd.value))
	{ 
		errormessage += "\n\nPlease confirm your new password."; 
		if (control == null) control = confNewPwd;
	}			
	
	//alert("hello");	
	// Check if passwords match
	if (newPwd.value != confNewPwd.value)
	{	
		errormessage += "\n\nPassword did not match.";	
		if (control == null) control = newPwd; 		
	}	
	
					
	// If there is an error/are errors.
	if(errormessage.length > 2) 
	{
		alert('NOTE:' + errormessage);
		control.focus(); // put focus to the first control which received the error
		control.select();
		return false;		
	}	
	return true;
}
// ----------------------------------------------------------------------------------------------------------
// Focus
function ChangePasswordFocus()
{	
	if (document.forms.form1.tbPassword != null)
	{
		if (document.forms.form1.tbPassword.value.length > 0)
		{
			if (document.forms.form1.tbNewPassword != null)
			{
				if (document.forms.form1.tbNewPassword.value.length > 0)
				{				
					if (document.forms.form1.tbConfNewPassword != null)					
						if (document.forms.form1.tbConfNewPassword.value.length = 0)
							document.forms.form1.tbConfNewPassword.focus();
				}
				else
					document.forms.form1.tbNewPassword.focus();
			}
		}
		else
			document.forms.form1.tbPassword.focus();
	}
}
// **********************************************************************************************************
function Compute(so, ta, re)
{
    var s = document.getElementById(so);
    var t = document.getElementById(ta);
    var r = document.getElementById(re);
    
    if ((s != null) && (t != null) && (r != null))
    {
        if ((s.value.length > 0) && (r.value.length > 0))
        {
            t.innerHTML = roundOff(r.value / s.value, 4);
        }
        else
            t.innerHTML = "0.0000";
    }
}

function DigitsOnly(e, usd)
{
    var keynum
    var keychar
    var numcheck
    var u = document.getElementById(usd);
    
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
    {
        if (u.value.length > 0)
        {
            var i = u.value.length - 1;	                
            if (u.value.charAt(i) == '.')
                hasDot = false;
            else if (u.value.indexOf('.') == -1)
                hasDot = false;
            else 
                hasDot = true;
        }
        else
        {
            hasDot = false;
        }
        return keynum;
    }
    if (keynum == 46) // delete
        return keynum;
    if (keynum == 9) // tab
        return keynum;
    if (keynum == 16) // shift
        return keynum;
    if (((keynum == 110) || (keynum == 190)) && (hasDot == false))
    {
        hasDot = true;
        if (u.value.length == 0)
            u.value += '0';
        return keynum;
    }
    if ((keynum >= 48) && (keynum <= 57)) // 0 - 9
        return keynum;
    if ((keynum >= 96) && (keynum <= 105)) // 0 - 9
        return keynum;
	
    return numcheck.test(keychar);
}

function roundOff(original_number, decimals) 
{
    var result1 = original_number * Math.pow(10, decimals)
    var result2 = Math.round(result1)
    var result3 = result2 / Math.pow(10, decimals)
    return padWithZeros(result3, decimals)
}

function padWithZeros(rounded_value, decimal_places) 
{
    // Convert the number to a string
    var value_string = rounded_value.toString()
   
    // Locate the decimal point
    var decimal_location = value_string.indexOf(".")

    // Is there a decimal point?
    if (decimal_location == -1) {
       
        // If no, then all decimal places will be padded with 0s
        decimal_part_length = 0
       
        // If decimal_places is greater than zero, tack on a decimal point
        value_string += decimal_places > 0 ? "." : ""
    }
    else {

        // If yes, then only the extra decimal places will be padded with 0s
        decimal_part_length = value_string.length - decimal_location - 1
    }
   
    // Calculate the number of decimal places that need to be padded with 0s
    var pad_total = decimal_places - decimal_part_length
   
    if (pad_total > 0) {
       
        // Pad the string with 0s
        for (var counter = 1; counter <= pad_total; counter++)
            value_string += "0"
        }
    return value_string
}