<%@ Page Title="P5R Vinesauce Mod" Language="C#" MasterPageFile="~/Blank.Master" AutoEventWireup="true" CodeBehind="Vinesauce.aspx.cs" Inherits="ShrineFoxCom.Vinesauce" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Header">
    <link rel='shortcut icon' href='./images/projects/Vinesauce/favicon.ico' type='image/x-icon' />
	<meta charset="utf-8">
	<script src="https://kit.fontawesome.com/4c3075832a.js" crossorigin="anonymous"></script>
	<script src="./js/jquery.min.js" crossorigin="anonymous"></script>
	<link rel="stylesheet" href="./css/spectre.css" />
	<link rel="stylesheet" href="./css/projects/Vinesauce/vine.css">
	<link rel="stylesheet" href="https://fonts.googleapis.com/css?family=News+Cycle|Open+Sans">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<div class="wrapper"> </div>
		<div class="logo">
			<center>
				<a href="https://shrinefox.com" style="z-index:1;position:relative;">
					<img class="img-responsive" src="./images/logo.svg" style="max-height:128px;">
				</a>
			</center>
		</div>
		<div class="content">
			<div class="vineborder top"> </div>
			<div class="container">
				<div class="columns">
					<div class="column col-6 col-sm-12">
						<div class="innercontainer">
							<img class="img-responsive" src="./images/projects/Vinesauce/vine_logo.png">
							<h1><b><div class="typesauce">It's Time to Scoot the Burbs, Everyone.</div></b></h1>
							Vinny finds himself stuck in the world of Persona 5, teaming up with and fighting against a large cast of familiar faces.
							<br>Together, will they find a way out?
							<br>
							<br>Join us in commemorating several years of fun and hilarious Vinesauce streams.
							Experience P5R like never before with new art, models, music, and tons of 
							easter eggs from Vinny’s past broadcasts.
						</div>
					</div>
					<div class="column col-6 col-sm-12 flex-centered">
						<!-- Trailer -->
						<div class="video-responsive">
							<iframe src="https://www.youtube.com/embed/cMvoJUaZ_fc" width="350" height="200" frameborder="0" allowfullscreen webkitallowfullscreen mozallowfullscreen scrolling="no"></iframe>
						</div>
					</div>
				</div>
			</div>
			<div class="vineborder bottom"> </div>
		</div>
		<div class="content">
			<div class="vineborder top"> </div>
			<div class="container">
				<div class="columns">
					<div class="column col-6 col-sm-12 flex-centered">
						<!--Slideshow-->
						<div class="video-responsive">
							<iframe src="https://www.youtube.com/embed/ZeEKjIHEm7s" width="350" height="200" frameborder="0" allowfullscreen webkitallowfullscreen mozallowfullscreen scrolling="no"></iframe>
						</div>
					</div>
					<div class="column col-6 col-sm-12">
						<div class="innercontainer">
							<h1><div class="typesauce">FEATURES</div></h1>
							Re-experience the acclaimed RPG from a fresh new angle!
							<ul>
								<li>Play as Vinny Vinesauce, complete with custom models and voice clips.</li>
								<li>9 fully reskinned party members inspired by Vinesauce lore.</li>
								<li>A variety of enemies to battle and recruit, all taken from Vinny's streams!</li>
								<li>Complete OST replacement from Vinny’s band <a href="https://vine.bandcamp.com/music">Red Vox</a>– a varied mix of instrumental and lyrical tracks.</li>
								<li>A fresh coat of green paint, with skillful UI art recreations by the community.</li>
								<li>Tons of easter eggs and references to Vinny’s Twitch livestreams!</li>
								<li>A trimmed-down story mode that allows you to focus on gameplay.</li>
								<li>See how far you can get in just 20 minutes!</li>
							</ul>
						</div>
					</div>
				</div>
			</div>
			<div class="vineborder bottom"> </div>
		</div>
		<div class="content">
			<div class="vineborder top"> </div>
			<div class="innercontainer">
				<h1><div class="typesauce">FREQUENTLY ASKED QUESTIONS</div></h1>
				<div class="container">
					<div class="toggle">
						<div class="toggle-title typesauce"><h3>What is Vinesauce?</h3></div>
						<div class="toggle-inner"><p>Vinesauce is a <a href="https://www.youtube.com/watch?v=oeRD_fGFhdA">variety streamer</a> named Vinny on <a href="https://www.twitch.tv/vinesauce">Twitch</a> & <a href="https://www.youtube.com/c/vinesauce">YouTube</a>. His stream's gimmick is <a href="https://www.youtube.com/playlist?list=PLHmR7c8zbVZRLORCvg7Thz-2PG126KmDA">corruptions</a>-- dismantling classic games by randomly editing RAM while playing. He's also known for his <a href="https://redvoxband.com/">music</a>, his commentary, and the community surrounding his streams.</p></div>
					</div>
					<div class="toggle">
						<div class="toggle-title typesauce"><h3>Why a Persona 5 mod?</h3></div>
						<div class="toggle-inner"><p>A <a href="https://www.youtube.com/watch?v=d-TBs4KwWF8">running gag</a> during Vinny's streams is that chat members will request that he play a Persona game, despite him having no interest in the franchise. Although this mod does not make it any more likely that he will change his mind, fans of his streams are sure to appreciate the irony.</p></div>
					</div>
					<div class="toggle">
						<div class="toggle-title typesauce"><h3>What platforms is it available for?</h3></div>
						<div class="toggle-inner"><p>Primarily, this mod is being developed for the PC (Steam/Xbox Game Pass) version of P5R.
							<br />It may potentially be ported to PS4 and Switch at a later date, although these plans take low priority.
							<br />The PC port will have certain exclusive features compared to any eventual console ports, such as configurable options.</p></div>
					</div>
					<div class="toggle">
						<div class="toggle-title typesauce"><h3>How do I play this mod?</h3></div>
						<div class="toggle-inner"><p>On PC, Extract and run Reloaded-II. Extract the contents of the .zip to the Reloaded-II/Mods folder. 
						<br>Enable the mods and launch the game through Reloaded-II. See <a href="https://docs.shrinefox.com/getting-started/persona-5-royal-pc-mod-support">here</a> for more detailed instructions.
						<br><br>For Switch, see <a href="https://docs.shrinefox.com/getting-started/persona-5-royal-switch-mod-support">here</a> for instructions on playing with mods. For PS4, see <a href="https://docs.shrinefox.com/getting-started/persona-5-royal-ps4-mod-support">here</a>.</p></div>
					</div>
				</div>
			</div>
			<div class="vineborder bottom"> </div>
		</div>
		<div class="content">
			<div class="vineborder top"> </div>
			<div class="innercontainer">
				<center>
					<b>Support Atlus by purchasing Persona 5 Royal on <a href="https://store.playstation.com/en-us/concept/10002771">Playstation Network</a>, <a href="https://www.nintendo.com/store/products/persona-5-royal-switch/">the Nintendo eShop</a> or <a href="https://store.steampowered.com/app/1687950/Persona_5_Royal/">Steam</a>.</b>
					<h1><div class="typesauce">DOWNLOAD</div></h1>
					<u>Latest Version (v0.0)</u>
					<br><a href="#"><i class="fas fa-folder-open" aria-hidden="true"></i> Reloaded II .zip (PC)</a>
					<br><a href="#"><i class="fas fa-folder-open" aria-hidden="true"></i> Aemulus .zip (Switch)</a>
					<br><a href="#"><i class="fas fa-folder-open" aria-hidden="true"></i> Aemulus .zip (PS4)</a>
					<br>
					<br>Note: This mod has not yet been released. Everything here is a placeholder!
				</center>
			</div>
			<div class="vineborder bottom"> </div>
		</div>
		<div class="content" style="margin-bottom:0px;">
			<div class="vineborder top"> </div>
			<div class="innercontainer">
				<center>
					ShrineFox 2023.
					<br>Special Thanks to: <a href="https://twitter.com/tgenigma">TGEnigma</a> (programming), 
					<a href="https://twitter.com/CheesyDraws">CheesyDraws</a>, <a href="https://twitter.com/NeonWillowLeaf">NeonWillowLeaf</a> 
					& <a href="https://protokyuu.newgrounds.com/">Protokyuu</a> (character & UI artwork), 
					<a href="https://twitter.com/kekulism">kekulism</a> (planning & bgm conversion), 
					<a href="https://twitter.com/CherryCreamSoda">CherryCreamSoda</a> (asset recolors & logo)
					<br>We are not affiliated, associated, authorized, endorsed by, or in any way officially connected with ATLUS, SEGA, 
					or any of its subsidiaries or its affiliates.<br>The official ATLUS website can be found at 
					<a href="https://atlus.com">https://atlus.com</a>. "Persona 5" is a registered trademarks of ATLUS.
				</center>
			</div>
			<br>
		</div>
		
		<script>
            if( jQuery(".toggle .toggle-title").hasClass('active') ){
				jQuery(".toggle .toggle-title.active").closest('.toggle').find('.toggle-inner').show();
			}
			jQuery(".toggle .toggle-title").click(function(){
				if( jQuery(this).hasClass('active') ){
					jQuery(this).removeClass("active").closest('.toggle').find('.toggle-inner').slideUp(200);
				}
				else{	jQuery(this).addClass("active").closest('.toggle').find('.toggle-inner').slideDown(200);
				}
			});
        </script>
</asp:Content>
