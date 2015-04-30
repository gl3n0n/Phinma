// Created By: GA Sacramento 
// Last Update: 07282006
// **********************************************************************************************************
function processText(n)
{
	TBox = document.getElementById("TblAcct" + n);
	var data = TBox.value;
	callserver("11"+n+data);
}

function processText1(n)
{
	TBox = document.getElementById("TblCredit" + n);
	var data = TBox.value;
	callserver("12"+n+data);
}

function processEquipments(col,n)
{
	var TBox, data;
	switch(col)
	{
		case 1:
			TBox = document.getElementById("TblEquipment" + n);
			data = TBox.value;
			callserver("21"+n+data);
			break;
		case 2:
			TBox = document.getElementById("TblUnits" + n);
			data = TBox.value;
			callserver("22"+n+data);
			break;
		case 3:
			TBox = document.getElementById("TblRemarks" + n);
			data = TBox.value;
			callserver("23"+n+data);
			break;
	}
}

function processRelatives(col,n)
{
	var TBox, data;
	switch(col)
	{
		case 1:
			TBox = document.getElementById("TbRelative" + n);
			data = TBox.value;
			callserver("31"+n+data);
			break;
		case 2:
			TBox = document.getElementById("TbTitle" + n);
			data = TBox.value;
			callserver("32"+n+data);
			break;
		case 3:
			TBox = document.getElementById("TbRelation" + n);
			data = TBox.value;
			callserver("33"+n+data);
			break;
	}
}

function processSelect(n)
{
	//alert(n);

	DDList = document.getElementById("ddlPlanType" + n);
	var dList = DDList.selectedIndex;
  //  alert(dList);
	var selValue = DDList.options[dList].value;
    
	callserver("13"+n+selValue);
}

function processReference(refType, col, n)
{
	var tbl;
    
	switch(refType)
	{
		case 1:
			tbl = ((col == 1) ? "TblCol11" : "TblCol21")
			break;
		case 2:
			tbl = ((col == 1) ? "TblCol12" : "TblCol22")
			break;
		case 3:
			tbl = ((col == 1) ? "TblCol13" : "TblCol23")
			break;
		case 4:
			tbl = ((col == 1) ? "TblCol14" : "TblCol24")
			break;
	}
    
	processReference2(refType, col, tbl, n)
}

function processReference2(refType, col, tblName, n)
{
	TBox = document.getElementById(tblName + n);
	data = TBox.value;
	callserver("4"+ refType + col + n + data);
}

function ReceiveServerData(arg, context)
{
	//alert(arg);
}

function NumericValidator(kyCode)
{
	if((kyCode < 48) || (kyCode > 57))
	{
		alert("Please Enter Numeric Values Only!");
		return false;
	}
    
	return true;
}

function PhoneAndNoValidator(objName)
{
	//alert(tbName);
	var num = objName.value;
	var kyCode = window.event ? event.keyCode : event.which;
    
	if(kyCode != 45)
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

function RemoveChar(objName)
{
	var num = objName.value;
	var i = 0;
    
	for( i = 0; i < num.length; i++)
		if(num.charCodeAt(i) != 45)
			if((num.charCodeAt(i) < 48) || (num.charCodeAt(i) > 57))
				objName.value = num.substring(0,i);
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

function RemoveElement(tbName, divNum) 
{
	alert(tbName+divNum);
	var d = document.getElementById(tbName);
	var olddiv = document.getElementById(divNum);
	d.removeChild(olddiv);
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
	//alert(document.frmRegistration.chkCompanyClass[4]);
	if (document.Form1.chkCompanyClassification_4.checked)
	  {
		document.Form1.chkCompanyClassification_0.checked = false; 
		document.Form1.chkCompanyClassification_0.disabled = true;
		document.Form1.chkCompanyClassification_1.checked = false; 
		document.Form1.chkCompanyClassification_1.disabled = true;
		document.Form1.chkCompanyClassification_2.checked = false; 
		document.Form1.chkCompanyClassification_2.disabled = true;
		document.Form1.chkCompanyClassification_3.checked = false; 
		document.Form1.chkCompanyClassification_3.disabled = true;
	  }
	 else
	 {
		document.Form1.chkCompanyClassification_0.disabled = false;
		document.Form1.chkCompanyClassification_1.disabled = false;
		document.Form1.chkCompanyClassification_2.disabled = false;
		document.Form1.chkCompanyClassification_3.disabled = false;
	 }
}
// **********************************************************************************************************

