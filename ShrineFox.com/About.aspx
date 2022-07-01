<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="ShrineFoxCom.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<div class="navipath">
		<a href="https://shrinefox.com/"><i class="fa fa-home" aria-hidden="true"></i> ShrineFox.com</a> 
		<i class="fa fa-angle-right" aria-hidden="true"></i> <%: Page.Title %>
	</div>
	<h1><%: Page.Title %></h1>
ShrineFox.com is a fansite dedicated to <b>Amicitia</b>, a modding community close to my heart.
<br>
<br><h2>Mission</h2>
<br>Consumer electronics like smartphones, videogame consoles, and computers all offer some form of personalization. 
	I've always been interested in extending my control over devices I own, even in ways the manufacturers never intended. 
	Thanks to internet communities centered around modding, jailbreaking, and reverse engineering, almost any changes you can imagine are possible.
<br><br>Here on my site, I write about how you can use software to customize your experience for little to no cost. 
	Many of the methods I use are open-source and can already be found online. While these resources aren't new, 
	by compiling them in one place and demonstrating them myself, I hope to make modding more accessible for everybody.
	<br><br><h5><a href="https://shrinefox.com/Recommended">See here for my personal list of recommended resources</a>.</h5>
<br>
<br><h2>Support</h2>
If you would like to support what I do, please make a donation via <a href="https://ko-fi.com/shrinefox">Ko-Fi</a>, 
	which accepts PayPal and Stripe. Any contribution is greatly appreciated and helps keep the site online.
<br>You can message me there if you have any personal requests. You may also <a href="ryan@shrinefox.com">email me</a>, but please be aware I 
	don't frequently check/respond to these emails, nor am I obligated to respond.
<br>
<br><h2>Troubleshooting</h2>
To report a software bug, please <a href="https://docs.github.com/en/issues/tracking-your-work-with-issues/creating-an-issue">open an issue on Github</a>.
<br>If you have other modding related questions, ask on the <a href="https://shrinefox.com/forum">forum</a>.
<br>For concerns about anything linked here that I didn't make, contact the creator from wherever they posted it.
<br>I generally don't have control over content curated from <a href="https://gamebanana.com/">Gamebanana.com</a> 
	(appearing in the <a href="https://shrinefox.com/browse">Browse</a> section), but I do make an effort to keep it safe for work.
<br>
<br><h2>Source Code</h2>
This project is licensed under GPL-3.0, so it's open source and you can use any part of it to create your own projects, 
commercial or otherwise. See the <a href="https://github.com/ShrineFox/ShrineFox.com">repository on Github</a>.
<br>
<br>
<h2>Credits</h2>
<table class="table table-striped table-hover">
<tbody>
    <tr>
		<td>Scripts</td>
		<td>
			<a href="https://github.com/jakiestfu/Youtube-TV">YouTube TV</a> by Jacob Kelley
			<br><a href="https://jscolor.com/">jscolor</a> by Jan Odvárko
			<br><a href="https://github.com/enginkizil/FeedEk">FeedEk</a> by enginkizil
			<br><a href="https://github.com/TekkaGB/AemulusModManager">Aemulus</a> by TekkaGB 
			(<a href="https://gamebanana.com/">Gamebanana</a> Webscraper)
		</td>
	</tr>
	<tr>
		<td>Themes</td>
		<td>
			<a href="https://picturepan2.github.io/spectre">Spectre CSS Framework</a> by picturepan2
			<br><a href="https://gretathemes.com/wordpress-themes/justread/">JustRead</a> by GretaThemes
			<br><a href="https://themeforest.net/item/milk-multipurpose-responsive-phpbb-31-theme/">Milk (phpbb theme)</a> by PlanetStyles
		</td>
	</tr>
	<tr>
		<td>Icons</td>
		<td>
			<a href="https://fontawesome.com/icons"><i class="fa fa-font-awesome" aria-hidden="true"></i> <b>Font Awesome 5</b></a>
			<br><a href="https://www.deviantart.com/sylar399/art/Shin-Megami-Tensei-333408957">Favicon</a> by sylar399
		</td>
	</tr>
	<tr>
		<td>Special Thanks <i class="fas fa-heart"></i></td>
		<td>
			<br><b>TGEnigma</b>
			<br>Alan3D
			<br>Cherry Cream Soda
			<br>Crowpocalypse
			<br>DeathChaos25
			<br>regularpanties
			<br>Sierra
			<br>TekkaGB
			<br>Zarroboogs
		</td>
	</tr>
</table>
</asp:Content>
