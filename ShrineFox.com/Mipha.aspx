<%@ Page Title="Mipha's Grace Mod" Language="C#" MasterPageFile="~/Blank.Master" AutoEventWireup="true" CodeBehind="Mipha.aspx.cs" Inherits="ShrineFoxCom.Mipha" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Header">
    <link rel='shortcut icon' href='./images/projects/EarthBound/favicon.ico' type='image/x-icon' />
	<meta charset="utf-8">
	<script src="https://kit.fontawesome.com/4c3075832a.js" crossorigin="anonymous"></script>
	<script src="./js/jquery.min.js" crossorigin="anonymous"></script>
	<link rel="stylesheet" href="./css/spectre.css" />
	<link rel="stylesheet" href="./css/projects/Mipha/mipha.css">
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
		<div class="corner top-left"><img src="../../../images/projects/Mipha/top-left.png"></div>
		<div class="corner top-right"><img src="../../../images/projects/Mipha/top-right.png"></div>
		<div class="container">
			<div class="columns">
				<div class="column col-6 col-sm-12">
					<div class="innercontainer">
						<center><img class="img-responsive" src="./images/projects/Mipha/MiphaLogo.png"></center>
						<h1><div class="hylian">Mipha's Grace is Ready.</div></h1>
						Replay the Legend of Zelda: Breath of the Wild as your favorite Zora champion!
						<br><a href="https://shrinefox.com/blog/2020/02/13/mipha-mod-dev-part-1-setup/">Originally 
							conceptualized</a> by ShrineFox in 2018, the mod has since been substantially 
							<a href="https://shrinefox.com/blog/2021/08/10/welcome-to-miphas-grace-2-0/">expanded on by PeregrineHBG</a>.
						<br>Enjoy a world of quality-of-life improvements that bring our fish hero (back) to life-- more than ever before!
					</div>
				</div>
				<div class="column col-6 col-sm-12 flex-centered">
					<!-- Trailer -->
					<div class="video-responsive">
						<iframe src="https://www.youtube.com/embed/OidUvlmWKQw" width="350" height="200" frameborder="0" allowfullscreen webkitallowfullscreen mozallowfullscreen scrolling="no"></iframe>
					</div>
				</div>
			</div>
		</div>
		<div class="corner bottom-left"><img src="../../../images/projects/Mipha/bottom-left.png"></div>
		<div class="corner bottom-right"><img src="../../../images/projects/Mipha/bottom-right.png"></div>
	</div>
	<div class="miphacontainer"><div class="miphadownarrow"></div></div>

	<!--Screenshots-->
	<div class="carousel">
		<div class="carousel-inner">
			<input class="carousel-open" type="radio" id="carousel-1" name="carousel" aria-hidden="true" hidden="" checked="checked">
			<div class="carousel-item">
				<img src="../../../images/projects/Mipha/MiphaScreen5.png">
			</div>
			<input class="carousel-open" type="radio" id="carousel-2" name="carousel" aria-hidden="true" hidden="">
			<div class="carousel-item">
				<img src="../../../images/projects/Mipha/MiphaScreen6.png">
			</div>
			<input class="carousel-open" type="radio" id="carousel-3" name="carousel" aria-hidden="true" hidden="">
			<div class="carousel-item">
				<img src="../../../images/projects/Mipha/MiphaScreen3.png">
			</div>
			<label for="carousel-3" class="carousel-control prev control-1">‹</label>
			<label for="carousel-2" class="carousel-control next control-1">›</label>
			<label for="carousel-1" class="carousel-control prev control-2">‹</label>
			<label for="carousel-3" class="carousel-control next control-2">›</label>
			<label for="carousel-2" class="carousel-control prev control-3">‹</label>
			<label for="carousel-1" class="carousel-control next control-3">›</label>

			<ol class="carousel-indicators">
				<li>
					<label for="carousel-1" class="carousel-bullet">•</label>
				</li>
				<li>
					<label for="carousel-2" class="carousel-bullet">•</label>
				</li>
				<li>
					<label for="carousel-3" class="carousel-bullet">•</label>
				</li>
			</ol>
		</div>
	</div>

	<div class="content">
		<div class="corner top-left"><img src="../../../images/projects/Mipha/top-left.png"></div>
		<div class="corner top-right"><img src="../../../images/projects/Mipha/top-right.png"></div>
		<div class="container">
			<div class="columns">
				<div class="column col-6 col-sm-12">
					<div class="innercontainer">
						<h1><div class="hylian">Returning Features</div></h1>
						<ul>
							<li>Link's model replaced with Mipha</li>
							<li>Faster swimming speed</li>
							<li>Recover stamina while swimming</li>
							<li>Swim up waterfalls & spin attack always enabled</li>
							<li>Increased weakness to electricity</li>
						</ul>
					</div>
				</div>
				<div class="column col-6 col-sm-12">
					<div class="innercontainer">
						<h1><div class="hylian">New Features in 2.0</div></h1>
						<ul>
							<li>Improved ragdoll physics</li>
							<li>Full dialogue edits</li>
							<li>New & tweaked animations</li>
							<li>Edited events and cutscenes</li>
							<li>Armor sets redesigned/re-fitted for Mipha</li>
						</ul>
					</div>
				</div>
			</div>
		</div>
		<div class="corner bottom-left"><img src="../../../images/projects/Mipha/bottom-left.png"></div>
		<div class="corner bottom-right"><img src="../../../images/projects/Mipha/bottom-right.png"></div>
	</div>
	<div class="miphacontainer"><div class="miphadownarrow"></div></div>

	<div class="content">
		<div class="corner top-left"><img src="../../../images/projects/Mipha/top-left.png"></div>
		<div class="corner top-right"><img src="../../../images/projects/Mipha/top-right.png"></div>
		<div class="innercontainer">
			<center>
				<b>Support Nintendo by playing Zelda: Breath of the Wild on <a href="https://www.nintendo.com/us/store/products/the-legend-of-zelda-breath-of-the-wild-switch//">Nintendo Switch</a>.</b>
				<br>
				<h1><div class="hylian">DOWNLOAD</div></h1>
				<u><b>Latest Version (v2.0)</b></u>
				<br><a href="#"><i class="fas fa-file" aria-hidden="true"></i> BCML .bnp (Wii U)</a>
				<br>
				<br>At this time there is no Switch port (aside from the <a href="https://gamebanana.com/mods/246646">old version</a> of the mod).
			</center>
		</div>
		<div class="corner bottom-left"><img src="../../../images/projects/Mipha/bottom-left.png"></div>
		<div class="corner bottom-right"><img src="../../../images/projects/Mipha/bottom-right.png"></div>
	</div>
	<div class="miphacontainer"><div class="miphadownarrow"></div></div>

	<div class="content">
		<div class="corner top-left"><img src="../../../images/projects/Mipha/top-left.png"></div>
		<div class="corner top-right"><img src="../../../images/projects/Mipha/top-right.png"></div>
		<center>
			<h1><div class="hylian">INSTALL INSTRUCTIONS</div></h1>
		</center>
		<div class="columns">
			<div class="column col-6 col-sm-12">
				<div class="innercontainer">
					<ul>
						<li>Download and install <a href="https://gamebanana.com/tools/6624">BCML</a></li>
						<li>Set up paths to BotW's base/update/DLC files in BCML settings</li>
						<li>Select downloaded .bnp file in BCML to install mod</li>
						<li>Launch game with <a href="https://cemu.info/">CEMU</a> and enjoy!</li>
						<li>You could also use <a href="https://gamebanana.com/tools/12110">UKMM</a> instead of BCML.</li>
					</ul>
				</div>
			</div>
			<div class="column col-6 col-sm-12 flex-centered">
				<img src="../../../images/projects/Mipha/LoMeephers.png" style="max-width: 95%;" />
			</div>
		</div>
		<div class="corner bottom-left"><img src="../../../images/projects/Mipha/bottom-left.png"></div>
		<div class="corner bottom-right"><img src="../../../images/projects/Mipha/bottom-right.png"></div>
	</div>
	<div class="miphacontainer"><div class="miphadownarrow"></div></div>

	<div class="content">
		<div class="corner top-left"><img src="../../../images/projects/Mipha/top-left.png"></div>
		<div class="corner top-right"><img src="../../../images/projects/Mipha/top-right.png"></div>
		<center>
			<div class="toggle">
				<div class="toggle-title hylian"><br><h1>CLICK TO VIEW CREDITS</h1></div>
				<div class="toggle-inner">
					<div class="hylian">Developed by</div>
					PeregrineHBG
					<br>ShrineFox
					<br>
					<div class="hylian">Modeling</div>
					Lynard Killer
					<br>Sofia Harley
					<br>Purple Wanderer
					<br>ToRiCoChi
					<br>
					<div class="hylian">Cutscenes</div>
					Ginger
					<br>
					<div class="hylian">Icons</div>
					Fazlox
					<br>
					<div class="hylian">Text</div>
					KingFeraligatr
					<br>
					<div class="hylian">Gerudo Set Textures</div>
					Mr. The
					<br>Esper
					<br>
					<div class="hylian">Playtesting</div>
					Eldin of Pharae
					<br>Only By the Stars
					<br>Tona the Zora
					<br>
					<div class="hylian">Ragdoll Information + Tools</div>
					Moonling
					<br>
					<div class="hylian">Titlescreen Schreenshots</div>
					Jupiter Sky
					<br>
					<div class="hylian">Additional Animation Tweaks</div>
					Gray
				</div>
			</div>
		</center>
		<div class="corner bottom-left"><img src="../../../images/projects/Mipha/bottom-left.png"></div>
		<div class="corner bottom-right"><img src="../../../images/projects/Mipha/bottom-right.png"></div>
	</div>
	<div class="miphacontainer"><div class="miphadownarrow"></div></div>

	<div class="miphacontainer" style="margin-top:60px"><div class="miphahealing"></div></div>


	<div class="content" style="margin-bottom:0px;padding-bottom: 0;border-radius:0px;margin-left:0;margin-right:0;margin-top:0">
		<div class="innercontainer">
			<center>
				ShrineFox 2024.
				<br>We are not affiliated, associated, authorized, endorsed by, or in any way officially connected with Nintendo, 
				or any of its subsidiaries or its affiliates.<br>The official Nintendo website can be found at 
				<a href="https://nintendo.com">https://nintendo.com</a>. "The Legend of Zelda" and "Breath of the Wild" are registered trademarks of Nintendo.
			</center>
		</div>
		<br>
	</div>

<div class="bubble-container" id="bubble-container"></div>
<script>
    document.addEventListener("DOMContentLoaded", function () {
		const bubbleContainer = document.getElementById('bubble-container');

        // Create bubbles
        for (let i = 0; i < 50; i++) {
            createBubble();
        }

        function createBubble() {
            const bubble = document.createElement('div');
            bubble.classList.add('bubble');
            bubble.style.left = `${Math.random() * 100}vw`;
            bubble.style.top = `${Math.random() * 100}vh`;
            bubble.style.setProperty('--rand', Math.random() * 2);
            bubbleContainer.appendChild(bubble);

            // Add hover event listener to make bubble poppable
            bubble.addEventListener('mouseenter', function (event) {
                const bubbleRect = bubble.getBoundingClientRect();
                const bubbleCenterX = bubbleRect.left + bubbleRect.width / 2;
                const bubbleCenterY = bubbleRect.top + bubbleRect.height / 2;
                const distance = Math.sqrt((event.clientX - bubbleCenterX) ** 2 + (event.clientY - bubbleCenterY) ** 2);

                if (distance <= 30) {
                    bubble.remove();
                }
            });
        }
    });
</script>

<script>
    if (jQuery(".toggle .toggle-title").hasClass('active')) {
        jQuery(".toggle .toggle-title.active").closest('.toggle').find('.toggle-inner').show();
    }
    jQuery(".toggle .toggle-title").click(function () {
        if (jQuery(this).hasClass('active')) {
            jQuery(this).removeClass("active").closest('.toggle').find('.toggle-inner').slideUp(200);
        }
        else {
            jQuery(this).addClass("active").closest('.toggle').find('.toggle-inner').slideDown(200);
        }
    });
</script>
<div class="wavescontainer">
	<svg class="waves" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink"
	viewBox="0 24 150 28" preserveAspectRatio="none" shape-rendering="auto">
	<defs>
	<path id="gentle-wave" d="M-160 44c30 0 58-18 88-18s 58 18 88 18 58-18 88-18 58 18 88 18 v44h-352z" />
	</defs>
	<g class="parallax">
	<use xlink:href="#gentle-wave" x="48" y="0" fill="rgba(0,121,216,0.7" />
	<use xlink:href="#gentle-wave" x="48" y="3" fill="rgba(0,121,216,0.5)" />
	<use xlink:href="#gentle-wave" x="48" y="5" fill="rgba(0,121,216,0.3)" />
	<use xlink:href="#gentle-wave" x="48" y="7" fill="#0079D8" />
	</g>
	</svg>
</div>

</asp:Content>
