<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="ShrineFoxcom.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<asp:PlaceHolder ID="Sidebar" runat="server"></asp:PlaceHolder>
<a href="https://shrinefox.com/"><i class="fa fa-home" aria-hidden="true"></i> ShrineFox.com</a> <i class="fa fa-angle-right" aria-hidden="true"></i> <a href="https://shrinefox.com/browse"><%: Page.Title %></a> <asp:PlaceHolder ID="Navigation" runat="server"></asp:PlaceHolder>
<h1><%: Page.Title %></h1>
I'm ShrineFox. I describe myself as a content creator that loves modding games, customizing devices, 
	and sometimes making buggy utilities.
Ever since I was introduced to modding,
	I've never lost my passion for personalizing everything that I possibly can.
<br><br>I've proudly overseen the creation and growth of Amicitia, an SMT modding community. 
	I owe everything to my incredible friends who continue to inspire me. Much of the content on this site
	was made possible by them.
<br><br><hr>
<h2>Contact</h2>
If you would like to report a bug, please open an issue on Github.
<br>If you have a modding related question, ask on the forum or subreddit.
<br>For concerns about anything linked here that I didn't make, contact the creator where they uploaded it.
<br><br>If you still need to get in touch with me, contact <code>rуan＠shrinefox¸com</code> (type it manually).
<br><br><hr>
<h2>Credits</h2>
<table class="table table-striped table-hover">
<tbody>
    <tr>
		<td>Scripts</td>
		<td>
			<a href="https://github.com/jakiestfu/Youtube-TV">YouTube TV</a> by Jacob Kelley
			<br><a href="https://jscolor.com/">jscolor</a> by Jan Odvárko
			<br><a href="https://github.com/enginkizil/FeedEk">FeedEk</a> by enginkizil
			<br><a href="https://github.com/TekkaGB/AemulusModManager">Aemulus</a> by TekkaGB (Gamebanana Webscraper)
		</td>
	</tr>
	<tr>
		<td>Themes</td>
		<td>
			<a href="https://picturepan2.github.io/spectre">Spectre CSS Framework</a> by picturepan2
			<br><a href="https://quarktheme.com/">Quark</a> by Anthony Hortin (modified by <a href="https://worproject.ml/">worproject</a>)
			<br><a href="https://www.phpbb.com/customise/db/style/prolight/">Prolight (phpbb)</a> by eeji
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
			<br>DeathChaos
			<br>regularpanties
			<br>Sierra
			<br>TekkaGB
		</td>
	</tr>
</table>
</asp:Content>
