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
    <center><h2 class="display-4">Latest Videos</h2></center>
    <div class="card">
        <div id="YourPlayerID" style="height: 100%;"></div>
        <script src="https://apis.google.com/js/platform.js"></script>
    </div>

    <div class="notice green">
        <b>Current Project</b>
        <br>Learn more about the <b><a href="./vinesauce">Persona 5 Royal Vinesauce Mod</a></b>.
    </div>

    <br><br>
    <center><h2 class="display-4">Latest Blogposts</h2></center>
    <div class="card">
        <div class="card-header">
            <div class="card-title h5"><a href="/blog/"><i class="fab fa-wordpress"></i> See more blogposts</a></div>
        </div>
        <div class="card-footer">
            <div id="blog-latest"></div>
            <script>
                $('#blog-latest').FeedEk({
                    FeedUrl: 'https://shrinefox.com/blog/feed',
                    MaxCount: 10,
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
</asp:Content>
