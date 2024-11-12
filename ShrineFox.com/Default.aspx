<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ShrineFoxCom._Default" EnableEventValidation="false" EnableTheming="false" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="navipath">
		<a href="/"><i class="fa fa-home" aria-hidden="true"></i> ShrineFox.com</a> 
		<i class="fa fa-angle-right" aria-hidden="true"></i> <%: Page.Title %>
	</div>

    <div class="text-center">
        <br>
        <h1 class="display-4">Welcome</h1>
        <h5>
            You've reached the internet's largest collection of <a href="/browse/">SMT mods, tools & guides</a>.
        </h5>
        <p>
            I write about unofficial ways you can personalize your games, consoles and devices.
        </p>
        <br><h2>New to modding? See how to <a href="/GetStarted">Get Started</a>.</h2>
    </div>

    <br><br>
    <center><h4 class="display-4">Latest Videos</h4></center>
    <div class="card">
        <div id="YourPlayerID" style="height: 100%;"></div>
        <script src="https://apis.google.com/js/platform.js"></script>
    </div>

    <div class="columns">
	    <div class="column col-6 col-sm-12">
            <center><h4>Popular Releases</h4></center>

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
            <br><a href="https://shrinefox.github.io/en/Adachi">
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
            <br><br><a href="/Projects" style="float: right;">
                <i class="fas fa-flask"></i> See more of My Projects</a>
        </div>
        <div class="column col-6 col-sm-12">
            <center><h4>Latest Blogposts</h4></center>
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
