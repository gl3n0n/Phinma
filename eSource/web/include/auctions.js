// Created By: GA Sacramento 
// Last Update: 07122006
// **********************************************************************************************************
// Focus on upperleftmost textbox
function Focus()
{
	var usr = document.getElementById("gvBidInput_ctl02_txtLeft");		
	if (usr != null)
		usr.focus();		
}
// **********************************************************************************************************
// Set status
var message="Trans-Asia Ebid Portal"; 
var speed=400; 
var visible=0; 
function Flash() 
{ 
	if (visible == 0) 
	{ 
		window.status=message; visible=1; 
	} 
	else 
	{ 
		window.status=""; visible=0; 
	} 
	setTimeout('Flash()', speed); 
}
// **********************************************************************************************************
// Count down and count up
function Countdown()
{    
	if ((s != null) && (se != null))
	{
		s--;	
		se++;				
		
		lblRemaining.innerHTML = ConvertSecondsToCounterDateTimeString(s);					

		lblElapsed.innerHTML = ConvertSecondsToCounterDateTimeString(se);
	    
		if (s>0)
		{
			setTimeout("Countdown()",1000);
		}
		else
		{
			alert("Auction Finished!");
			window.close();
			location.href = 'finishedauctionevents.aspx';
		}
	}     		
}
// **********************************************************************************************************
// Display duration(in seconds) to DateString format
function LoadDuration()
{
	if (sd != null)
	{
		lblDuration.innerHTML = ConvertSecondsToCounterDateTimeString(sd);
	}
} 
// **********************************************************************************************************

