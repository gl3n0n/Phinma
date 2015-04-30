<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<style>
.sidenav {
	width: 213px;
	padding: 40px 28px 25px 0;
	font-family: "CenturyGothicRegular", "Century Gothic", Arial, Helvetica, sans-serif;
}

ul.sidenav {
	padding: 0;
	margin: 0;
	font-size: 1em;
	line-height: 0.5em;
	list-style: none;
}

ul.sidenav li {}

ul.sidenav li a {
	line-height: 10px;
	font-size: 11px;
	padding: 10px 5px;
	color: #000;
	display: block;
	text-decoration: none;
	font-weight: bolder;
}

ul.sidenav li a:hover {
	background-color:#675C7C;
	color:white;
}

ul.sidenav ul {
	margin: 0;
	padding: 0;
	display: none;
}

ul.sidenav ul li {
	margin: 0;
	padding: 0;
	clear: both;
}

ul.sidenav ul li a {
	padding-left: 20px;
	font-size: 10px;
	font-weight: normal;
	outline:0;
}

ul.sidenav ul li a:hover {
	background-color:#D3C99C;
	color:#675C7C;
}

ul.sidenav ul ul li a {
	color:silver;
	padding-left: 40px;
}

ul.sidenav ul ul li a:hover {
	background-color:#D3CEB8;
	color:#675C7C;
}

ul.sidenav span{
	float:right;
}
</style>
<script type="text/javascript" src="~/jquery/ui/jquery-1.5.2.min.js"></script>
<script type="text/javascript" src="~/jquery/ui/scriptbreaker-multiple-accordion-1.js"></script>
<script language="JavaScript">

$(document).ready(function() {
	$(".sidenav").accordion({
		accordion:false,
		speed: 500,
		closedSign: '[+]',
		openedSign: '[-]'
	});
});

</script>
  	   
<ul class="sidenav">
	<li><a href="http://www.scriptbreaker.com" target="scriptbreaker">Home</a></li>
	<li><a href="#">JavaScript</a>
		<ul>
			 <li><a href="#">Cookies</a></li>
			 <li><a href="#">Events</a></li>
			 <li><a href="#">Forms</a></li>
			 <li><a href="#">Games</a></li>
			 <li><a href="#">Images</a></li>
			 <li><a href="#">Navigations</a>
				<ul>
					<li><a href="#">CSS</a></li>
					<li><a href="#">JavaScript</a></li>
					<li><a href="#">JQuery</a></li>
				</ul>
			</li>
			 <li><a href="#">Tabs</a></li>
		</ul>
	</li>
	<li><a href="#">Tutorials</a>
		<ul>
			 <li class="active"><a href="#">HTML</a></li>
			 <li><a href="#">CSS</a></li>
			 <li><a href="#">JavaScript</a></li>
			 <li><a href="#">Java</a>
				<ul>
					<li><a href="#">JSP</a></li>
					<li><a href="#">JSF</a></li>
					<li><a href="#">JPA</a></li>
					<li><a href="http://www.scriptbreaker.com/contact/" target="_blank">Contact</a></li>
				</ul>
			</li>
			 <li><a href="#">Tabs</a></li>
		</ul>
	</li>
	<li><a href="http://www.scriptbreaker.com/contact/" target="_blank">Contact</a></li>
	<li><a href="#">Upload script</a></li>
</ul>
More script and css style
: <a href="http://www.htmldrive.net/" title="HTML DRIVE - Free DHMTL Scripts,Jquery plugins,Javascript,CSS,CSS3,Html5 Library">www.htmldrive.net </a>