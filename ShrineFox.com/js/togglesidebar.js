function toggleSidebar() {
	if (getCookie("sidebar") == "true") {
		document.getElementById("toggle-accordions").classList.toggle('active');
		document.getElementsByClassName("maincontent")[0].classList.toggle('active');
    }
}

function toggleCookie(ref) {
	document.getElementById("toggle-accordions").classList.toggle('active');
	document.getElementsByClassName("maincontent")[0].classList.toggle('active');

	if (getCookie("sidebar") == "true") {
		setCookie("sidebar", "false", 999);
	}
	else
		setCookie("sidebar", "true", 999);
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

window.onload = toggleSidebar;