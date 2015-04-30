// JScript File
function ValidatorIndividualAlert(source, args)
{
    var i;
    var AllValid = true;
    for (i = 0; i < Page_Validators.length; i++) {
    
        if (!Page_Validators[i].isvalid) {
       
			var msg;
			msg = Page_Validators[i].errormessage;
			if(!msg)
				msg = Page_Validators[i].id
			alert(msg);
			AllValid = AllValid && false;
            break;
        }
    }
    args.IsValid = AllValid; 
}
