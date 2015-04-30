// Created By: GA Sacramento 
// Last Update: 07262006
// **********************************************************************************************************
// Focus
function Focus()
{
	var usr = document.getElementById("_ctl0_ContentPlaceHolder1_txtUserName");
	var usr2 = document.getElementById("_ctl0_ContentPlaceHolder1_txtUserName2");
	var pwd = document.getElementById("_ctl0_ContentPlaceHolder1_txtPassword");		
	if ((usr != null) && (pwd != null))
	{
		if ((usr.value == "")  || (usr.value == null))
			usr.focus();
		else 
			pwd.focus();
	}
	else if (usr2 != null)
	{
		usr2.focus();
	}
}
function Focus2()
{
	var usr = document.getElementById("_ctl0_ContentPlaceHolder1_txtUserName");		
	var pwd = document.getElementById("_ctl0_ContentPlaceHolder1_txtPassword");	
	if (pwd != null)
	{
		pwd.value ="";
	}	
	if (usr != null)
	{
		usr.value = "";
		usr.focus();			
	}		
}
// **********************************************************************************************************