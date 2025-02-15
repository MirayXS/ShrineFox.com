<%@ Page Title="EarthBound Mod Menu" Language="C#" MasterPageFile="~/Blank.Master" AutoEventWireup="true" CodeBehind="EarthBound.aspx.cs" Inherits="ShrineFoxCom.EarthBound" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Header">
    <link rel='shortcut icon' href='./images/projects/EarthBound/eb_present.ico' type='image/x-icon' />
	<meta charset="utf-8">
	<script src="https://kit.fontawesome.com/4c3075832a.js" crossorigin="anonymous"></script>
	<script src="./js/jquery.min.js" crossorigin="anonymous"></script>
	<link rel="stylesheet" href="./css/spectre.css" />
	<link rel="stylesheet" href="./css/projects/EarthBound/eb.css">
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
			<div class="ebborder top"> </div>
			<div class="container">
				<div class="columns">
					<div class="column col-6 col-sm-12">
						<div class="innercontainer">
							<img class="img-responsive" src="./images/projects/EarthBound/ebmodmenu_logo.png">
							<h1>Replay EarthBound your way!</h1>
							This mod collects several quality of life enhancements, some made by myself,
							<br>some made by the PK Hack community.
							<br><br>It's open source, so anyone can use parts of it for their own mods,
							<br>or create their own derivatives.
							<br>
							<br><a href="https://ko-fi.com/shrinefox"><i class="fa fa-coffee"></i> Leave a Tip on Ko-Fi!</a>
						</div>
					</div>
					<div class="column col-6 col-sm-12 flex-centered">
						<!-- Trailer -->
						<div class="video-responsive">
							<iframe src="https://www.youtube.com/embed/LBg1NGfGaH8" width="350" height="200" frameborder="0" allowfullscreen webkitallowfullscreen mozallowfullscreen scrolling="no"></iframe>
						</div>
					</div>
				</div>
			</div>
			<div class="ebborder bottom"> </div>
		</div>

		<div class="content">
			<div class="ebborder top"> </div>
			<div class="container">
				<div class="columns">
					<div class="column col-6 col-sm-12 flex-centered">
						<img class="img-responsive" src="./images/projects/EarthBound/ebmodmenu_1.png">
					</div>
					<div class="column col-6 col-sm-12">
						<div class="innercontainer">
							<h1>Built-In Features</h1>
							These cannot be toggled in-game.
							<ul>
								<li>Hold Y in the overworld to walk faster or automatically advance texboxes</li>
								<li>No periodic calls from your dad urging you to save and quit</li>
								<li>Faster swim/climbing speed and transitions between rooms</li>
								<li>Check/talk To by pressing A near NPCs and objects, like in MOTHER 3</li>
								<li>Equip items from the Goods menu</li>
								<li>Enemies can miss while crying</li>
								<li>PSI is renamed to PK, like MOTHER 3</li>
							</ul>
						</div>
					</div>
				</div>
			</div>
			<div class="ebborder bottom"> </div>
		</div>

		<div class="content">
			<div class="ebborder top"> </div>
			<div class="container">
				<div class="columns">
					<div class="column col-6 col-sm-12">
						<div class="innercontainer">
							<h1>Optional Features</h1>
							You can toggle these from the New Game/Load Save screen.
							<ul>
								<li>Key Items: Separate quest items from your inventory, just like MOTHER 3</li>
								<li>Mod Menu: Custom menu for changing party members, health, stats, location, items, BGM, starting battles or events, saving etc.</li>
								<li>No Photo Guy: Significantly shortened Photo Guy cutscenes, just Ness doing a peace sign and the shutter effect (takes ~2 seconds)</li>
								<li>Chaos Mode: Random effects can happen while playing. Warps, battles, visual distortions, or shuffled party members.</li>
								<li>Unrestricted Bike: Allows you to use the Bicycle item anywhere, even when you have party members following you (temporarily reverts party to Ness only)</li>
								<li>Skip Lvlup Text: Don't get notified of new moves unlocked or level increases at the end of battle.</li>
								<li>Easy Deaths: Fully revive and heal party after a game over, without taking half your money.</li>
								<li>Fast Saving: Shortens the dialogue when calling your dad on the phone.</li>
								<li>No Homesickness: Prevents Ness from becoming homesick.</li>
							</ul>
						</div>
					</div>
					<div class="column col-6 col-sm-12 flex-centered">
						<img class="img-responsive" src="./images/projects/EarthBound/ebmodmenu_2.png">
					</div>
				</div>
			</div>
			<div class="ebborder bottom"> </div>
		</div>

		<div class="content">
			<div class="ebborder top"> </div>
			<div class="innercontainer">
				<h1>FREQUENTLY ASKED QUESTIONS</h1>
				<div class="container">
					<div class="toggle">
						<div class="toggle-title"><h3>What Bugs/Known Issues Are There?</h3></div>
						<div class="toggle-inner">
							<p>
							Not everything is fully tested, so let me know if you discover more by <a href="https://github.com/ShrineFox/EarthBound-Mod-Menu/issues/new">opening a GitHub Issue</a>.
							<ul>
								<li>When using the bike, Ness will be shown as the character riding it regardless of party leader.</li>
								<li>The Attract Mode on the title screen does not play (this is because for some reason it hangs halfway through).</li>
								<li>Configuring optional mod settings may sometimes freeze the game when the third option is selected in a busy area.</li>
								<li>Chaos Mode has a mild potential to sometimes crash the game.</li>
								<li>Sometimes it says the wrong character received or used a key item, or the item's name displays incorrectly.</li>
								<li>When toggling off the Key Items mod, you may end up lacking the items required to advance. Use the Mod Menu to give yourself key items as needed.</li>
								<li>Sometimes the spacing between "PK" and the name of an attack may be incorrect in battle.</li>
							</ul>
							</p>
						</div>
					</div>
					<div class="toggle">
						<div class="toggle-title"><h3>Who Helped Make This Mod?</h3></div>
						<div class="toggle-inner">
							<p>
							Special thanks to the PK Hack Discord Server for all their kind assistance. You guys PK Rock <3
							<ul>
								<li>Mr. Tenda: Created <a href="https://github.com/pk-hack/CoilSnake">CoilSnake</a></li>
								<li>Mr. Accident: Created the <a href="https://github.com/tripped/ccscript_legacy">CCScript Compiler</a></li>
								<li>JTolmar: Y Button run with momentum, fast movement in water, Repel Sandwich, changing text speed on the fly, custom save/load/newgame menu</li>
								<li>phoenixbound: Name printing commands, helped design/debug custom menus with variable options and descriptions</li>
								<li>Catador: Toggle-able noclip option for Mod Menu, change bootup music</li>
								<li>phoenixbound, ShadowOne333, vince94, Chaz: Goods Equip Menu</li>
								<li>jtolmar, phoenixbound, and Catador: Fast Doors</li>
								<li>cooprocks123e: Ability to call battle backgrounds from anywhere, CCExpand for custom control codes, extended flags</li>
								<li>ShadowOne333: From <a href="https://github.com/ShadowOne333/MaternalBound-Redux">MaternalBound-Redux</a>: Changed controls, crying also affects enemies, lower HP/PP windows one tile, PSI => PK, restore spank sfx</li>
								<li>H.S.: asm65816 and ASMRef for assembly patch support via CCScript</li>
							</ul>
							</p>
						</div>
					</div>
					<div class="toggle">
						<div class="toggle-title"><h3>How Can I work on the project?</h3></div>
						<div class="toggle-inner"><p>Read the <a href="https://github.com/ShrineFox/EarthBound-Mod-Menu/wiki">Project Wiki</a> on GitHub for development information.</p></div>
					</div>
					<div class="toggle">
						<div class="toggle-title"><h3>Does This Work With Other Mods?</h3></div>
						<div class="toggle-inner"><p>Unfortunately not, this mod is based on vanilla EarthBound and cannot be easily combined with other patches.
							<br>If another mod is also open source and has a Coilsnake project, you would have to try merging the codebases.
						</p></div>
					</div>
				</div>
			</div>
			<div class="ebborder bottom"> </div>
		</div>
		<div class="content">
			<div class="ebborder top"> </div>
			<div class="innercontainer">
				<center>
					<b>Support Nintendo by playing EarthBound on <a href="https://www.nintendo.com/us/store/products/super-nintendo-entertainment-system-nintendo-switch-online-switch/">Nintendo Switch Online</a>.</b>
					<br><h1>DOWNLOAD</h1>
					<a href="https://github.com/ShrineFox/EarthBound-Mod-Menu/releases/"><img src="./images/projects/EarthBound/eb_present.png" style="height:32px;"> v0.9.1 .IPS Patch (SNES)</a> (Update #1)
					<br>
					<br>Use <a href="https://www.marcrobledo.com/RomPatcher.js/">an online ROM patcher</a> to apply the patch to your EarthBound ROM.
				</center>
			</div>
			<div class="ebborder bottom"> </div>
		</div>
		<div class="content" style="margin-bottom:0px;">
			<div class="ebborder top"> </div>
			<div class="innercontainer">
				<center>
					ShrineFox 2024.
					<br>We are not affiliated, associated, authorized, endorsed by, or in any way officially connected with Nintendo, 
					or any of its subsidiaries or its affiliates.<br>The official Nintendo website can be found at 
					<a href="https://nintendo.com">https://nintendo.com</a>. "EarthBound" / "MOTHER" are registered trademarks of <a href="https://1101.com/">SHIGESATO ITOI</a> & <a href="https://www.creatures.co.jp/en/company/">CREATURES Inc.</a>.
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
