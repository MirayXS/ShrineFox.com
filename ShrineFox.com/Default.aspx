<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ShrineFoxCom._Default" EnableEventValidation="false" EnableTheming="false" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="navipath">
		<a href="/"><i class="fa fa-home" aria-hidden="true"></i> ShrineFox.com</a> 
		<i class="fa fa-angle-right" aria-hidden="true"></i> <%: Page.Title %>
	</div>
    <!--Latest Videos from YouTube-->
    <div id="YourPlayerID" style="height: 100%;"></div>
    <script src="https://apis.google.com/js/platform.js"></script>
    <br>
    <br>
    <!--Welcome Text-->
    <div class="text-center">
        <br>
        <h1 class="display-4">Welcome</h1>
        <h5>
            You've reached the internet's largest collection of <a href="/browse/">SMT mods, tools & guides</a>.
        </h5>
        <p>
            I write about unofficial ways you can personalize your games, consoles and devices.
        </p>
        <br><h5 class="display-5"><a href="/GetStarted"><i class="fa-solid fa-circle-arrow-right"></i> Get Started</a> with modding Persona.</h5>
    </div>
    <br>
    <br>
    <hr>
    <br>
    <br>
    <!--Project/Blog Links-->
    <div class="columns">
	    <div class="column col-6 col-sm-12">
            <center><h5 class="display-5">Popular Releases</h5></center>

            <br><a href="https://github.com/ShrineFox/Persona-5-Mod-Menu">
                <div class="projbutton">
                    <h2>Persona 5 Mod Menu</h2>
                    <img src="./images/projects/ModMenu/p5rmodmenu.png" />
                </div>
            </a>
            <br><a href="/EarthBound">
                <div class="projbutton">
                    <h2>EarthBound Mod Menu</h2>
                    <img src="./images/projects/EarthBound/ebmodmenu_1.png" />
                </div>
            </a>
            <br><a href="https://shrinefox.github.io/en/adachi.html">
                <div class="projbutton">
                    <h2>P5 Adachi Mod (PS3)</h2>
                    <img src="https://shrinefox.github.io/en/adachi/images/magatsu.gif" />
                </div>
            </a>
            <br><a href="/Vinesauce">
                <div class="projbutton">
                    <h2>P5R Vinesauce Mod</h2>
                    <img src="./images/projects/Vinesauce/vine_logo.png" />
                </div>
            </a>
            <br><a href="/Mipha">
                <div class="projbutton">
                    <h2>BotW Mipha's Grace Mod</h2>
                    <img src="./images/projects/Mipha/LoMeephers.png">
                </div>
            </a>
            <br><br><a href="/Projects" class="display-5" style="float: right;">
                <i class="fas fa-flask"></i> View all Projects</a>
        </div>
        <div class="column col-6 col-sm-12">
            <center><h5 class="display-5">Blog Posts</h5></center>
            <div class="card">
                <div class="card-header">
                    <div class="card-title h5"><a href="/blog/"><i class="fab fa-wordpress"></i> See more blogposts</a></div>
                </div>
                <div class="card-footer">
                    <div id="blog-latest"></div>
                    <script>
                        $('#blog-latest').FeedEk({
                            FeedUrl: 'https://shrinefox.com/blog/feed',
                            MaxCount: 5,
                            ShowDesc: true,
                            ShowPubDate: true,
                            DescCharacterLimit: 0,
                            TitleLinkTarget: '_blank',
                            DateFormat: 'MMM d',
                            DateFormatLang: 'en'
                        });
                    </script>
                </div>
            </div>
        </div>
    </div>
    
    
</asp:Content>
