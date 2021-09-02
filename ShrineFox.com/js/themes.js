var waves = '9,54,109';
var waves2 = '0,159,255';
var link = '20,116,232';
var text = '230,230,230';
var searchtext = '230,230,230';
var bg = '28,30,33';

document.addEventListener("DOMContentLoaded", () => {
	/* Set Theme from Cookie or Dropdown */
	SetTheme();
});

function ThemeToggle() {
	var theme = document.getElementById("theme").value.toLowerCase();
	setCookie("theme", theme, 999);

	SetTheme();
}

function SetTheme() {
	var theme = getCookie("theme");
	selectElement("theme", theme);
	HideColorPicker();

	/* Override amicitia.github.io game themeing with selection */
	if (theme == "blue") {
		waves = '9,54,109';
		waves2 = '0,159,255';
		link = '20,116,232';
		text = '28,30,33';
		searchtext = '230,230,230';
		bg = '230,230,230';
	}
	else if (theme == "blue dark") {
		waves = '9,54,109';
		waves2 = '0,159,255';
		link = '20,116,232';
		text = '230,230,230';
		searchtext = '230,230,230';
		bg = '28,30,33';
	}
	else if (theme == "red") {
		waves = '125,0,0';
		waves2 = '250,0,0';
		link = '200,0,0';
		text = '28,30,33';
		searchtext = '230,230,230';
		bg = '230,230,230';
	}
	else if (theme == "red dark") {
		waves = '125,0,0';
		waves2 = '250,0,0';
		link = '200,0,0';
		text = '230,230,230';
		searchtext = '230,230,230';
		bg = '28,30,33';
	}
	else if (theme == "green") {
		waves = '0,100,0';
		waves2 = '0,250,0';
		link = '0,200,0';
		text = '28,30,33';
		searchtext = '230,230,230';
		bg = '230,230,230';
	}
	else if (theme == "green dark") {
		waves = '0,100,0';
		waves2 = '0,250,0';
		link = '0,200,0';
		text = '230,230,230';
		searchtext = '230,230,230';
		bg = '28,30,33';
	}
	else if (theme == "yellow") {
		waves = '255,230,0';
		waves2 = '255,168,0';
		link = '255,215,0';
		text = '255,255,255';
		searchtext = '23,10,0';
		bg = '54,40,31';
	}
	else if (theme == "berry") {
		waves = '204,37,37';
		waves2 = '0,39,194';
		link = '255,0,128';
		text = '255,255,255';
		searchtext = '255,255,255';
		bg = '87,0,54';
    }
	else if (theme == "custom") {
		ShowColorPicker();
		/* Load color values from cookie */
		if (getCookie("color_link") == "") {
			setCookie("color_link", "200,200,200", 999);
			setCookie("color_waves", "50,50,50", 999);
			setCookie("color_waves2", "100,100,100", 999);
			setCookie("color_text", '150,150,150', 999);
			setCookie("color_searchtext", '255,255,255', 999);
			setCookie("color_bg", '0,0,0', 999);
		}
		link = getCookie("color_link");
		waves = getCookie("color_waves");
		waves2 = getCookie("color_waves2");
		text = getCookie("color_text");
		searchtext = getCookie("color_searchtext");
		bg = getCookie('color_bg');
	}
	else {
		/* Default Theme */
		waves = '9,54,109';
		waves2 = '0,159,255';
		link = '20,116,232';
		text = '230,230,230';
		searchtext = '230,230,230';
		bg = '28,30,33';
	}

	/* Override CSS color values */
	document.documentElement.style.setProperty('--waves', waves);
	document.documentElement.style.setProperty('--waves2', waves2);
	document.documentElement.style.setProperty('--link', link);
	document.documentElement.style.setProperty('--text', text);
	document.documentElement.style.setProperty('--searchtext', searchtext);
	document.documentElement.style.setProperty('--bg', bg);
}

function updateLink(picker) {
	var rgb = picker.toRGBString().replace("rgb(", "").replace(")", "");
	setCookie("color_link", rgb, 999);
	SetTheme();
}

function updateWaves(picker) {
	var rgb = picker.toRGBString().replace("rgb(", "").replace(")", "");
	setCookie("color_waves", rgb, 999);
	SetTheme();
}

function updateWaves2(picker) {
	var rgb = picker.toRGBString().replace("rgb(", "").replace(")", "");
	setCookie("color_waves2", rgb, 999);
	SetTheme();
}

function updateBg(picker) {
	var rgb = picker.toRGBString().replace("rgb(", "").replace(")", "");
	setCookie("color_bg", rgb, 999);
	SetTheme();
}

function updateText(picker) {
	var rgb = picker.toRGBString().replace("rgb(", "").replace(")", "");
	setCookie("color_text", rgb, 999);
	SetTheme();
}

function updateSearchText(picker) {
	var rgb = picker.toRGBString().replace("rgb(", "").replace(")", "");
	setCookie("color_searchtext", rgb, 999);
	SetTheme();
}

function updateColorPicker() {
	wavesstring = 'rgba(' + waves + ',1);'
	waves2string = 'rgba(' + waves2 + ',1);'
	linkstring = 'rgba(' + link + ',1);'
	textstring = 'rgba(' + text + ',1);'
	earchtextstring = 'rgba(' + searchtext + ',1);'
	bgstring = 'rgba(' + bg + ',1);'
	
	document.querySelector('#customwaves').jscolor.fromString(wavesstring);
	document.querySelector('#customwaves2').jscolor.fromString(waves2string);
	document.querySelector('#customlink').jscolor.fromString(linkstring);
	document.querySelector('#customtext').jscolor.fromString(textstring);
	document.querySelector('#customsearchtext').jscolor.fromString(textstring);
	document.querySelector('#custombg').jscolor.fromString(bgstring);
}

function getCookie(cname) {
	var name = cname + "=";
	var decodedCookie = decodeURIComponent(document.cookie);
	var ca = decodedCookie.split(';');
	for (var i = 0; i < ca.length; i++) {
		var c = ca[i];
		while (c.charAt(0) == ' ') {
			c = c.substring(1);
		}
		if (c.indexOf(name) == 0) {
			return c.substring(name.length, c.length);
		}
	}
	return "";
}

function setCookie(cname, val, exdays) {
	var exdate = new Date();
	exdate.setDate(exdate.getDate() + exdays);
	var c_value = escape(val) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
	document.cookie = cname + "=" + c_value + ";path=/";
}

function selectElement(id, valueToSelect) {
	let element = document.getElementById(id);
	element.value = valueToSelect;
}

function ShowColorPicker() {
	var c = document.getElementById('colorpicker');
	c.setAttribute("style", "display: block;");
}

function HideColorPicker() {
	var c = document.getElementById('colorpicker');
	c.setAttribute("style", "display: none;");
}