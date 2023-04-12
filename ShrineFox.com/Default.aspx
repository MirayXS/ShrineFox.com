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
    <br>
    <br>
    <div class="container">
        <div class="columns" id="slideshow">
            <!--News-->
            <div class="column">
                <div class="card">
                    <div class="card-image">
                        <img class="img-responsive" src="https://shrinefox.com/news/wp-content/uploads/2023/03/tabablend.png">
                    </div>
                    <div class="card-header">
                        <div class="card-title h5"><a href="https://shrinefox.com/news/blender-plugin-released-for-gfd/">Blender Plugin Released For GFD</a></div>
                    </div>
                    <div class="card-footer">
                        <a href="https://shrinefox.com/news/"><i class="fas fa-newspaper"></i> News</a>
                        <br>The first version of a Blender model importer/exporter for Persona 5 and the Dancing spinoffs has been released! Here’s what to expect and how to use it.
                    </div>
                </div>
            </div>
            <!--Guides-->
            <div class="column">
                <div class="card">
                    <div class="card-image">
                        <img class="img-responsive" src="https://shrinefox.com/guides/wp-content/uploads/2022/01/image-24.png">
                    </div>
                    <div class="card-header">
                        <div class="card-title h5"><a href="https://shrinefox.com/guides/2022/01/26/setting-up-persona-5-ex/">Setting Up Persona 5 EX</a></div>
                    </div>
                    <div class="card-footer">
                        <a href="https://shrinefox.com/guides/"><i class="fas fa-graduation-cap"></i> Guides</a>
                        <br>Learn how to set up DeathChaos’s Persona 5 EX, an overhaul mod the PS3 version of P5.
                    </div>
                </div>
            </div>
            <!--Blog-->
            <div class="column">
                <div class="card">
                    <div class="card-image">
                        <img class="img-responsive" src="https://shrinefox.com/blog/wp-content/uploads/2023/03/vinesona.png">
                    </div>
                    <div class="card-header">
                        <div class="card-title h5"><a href="https://shrinefox.com/blog/2023/03/02/p5r-vinesauce-mod-devlog-2/">P5R Vinesauce Mod – Devlog #2</a></div>
                    </div>
                    <div class="card-footer">
                        <a href="https://shrinefox.com/blog/"><i class="fab fa-wordpress"></i> Blog</a>
                        <br>Greener menus, cool glitchy effects, and talk of the eventual release– that’s the lowdown on the P5R Vinesauce mod’s current development!
                    </div>
                </div>
            </div>
        </div>
        <br>See <a href="https://shrinefox.com/articles">more articles</a>
    </div>
    <br>
    <br>
    <div style="margin-left: auto;margin-right:auto;max-width:800px;">
        <div id="YourPlayerID" style="height: 100%;"></div>
    </div>
    <script src="https://apis.google.com/js/platform.js"></script>
    <br>
    <br>
    <script>
        $("#slideshow > div:gt(0)").hide();

        setInterval(function () {
            $('#slideshow > div:first')
                .fadeOut(1000)
                .next()
                .fadeIn(2000)
                .end()
                .appendTo('#slideshow');
        }, 10000);
    </script>
</asp:Content>
