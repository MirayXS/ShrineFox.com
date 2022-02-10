<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ShrineFoxCom._Default" EnableEventValidation="false" EnableTheming="false" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="navipath">
		<a href="https://shrinefox.com/"><i class="fa fa-home" aria-hidden="true"></i> ShrineFox.com</a> 
		<i class="fa fa-angle-right" aria-hidden="true"></i> <%: Page.Title %>
	</div>
    <div class="text-center">
        <br>
        <h1 class="display-4">Welcome</h1>
        <h5>
            You've reached the internet's largest collection of <a href="https://shrinefox.com/browse/">SMT mods, tools & guides</a>.
        </h5>
        <p>
            I write about unofficial ways you can personalize your games, consoles and devices.
        </p>
    </div>
    <br>
    <br>
    <div class="container">
        <div class="columns">
            <!--News-->
            <div class="column col-4 col-sm-12">
                <div class="card">
                    <div class="card-header">
                        <div class="card-title h5"><a href="https://shrinefox.com/news/"><i class="fas fa-newspaper"></i> News</a></div>
                    </div>
                    <div class="card-footer">
                        <div id="news-latest"></div>
                    </div>
                </div>
            </div>
            <!--Guides-->
            <div class="column col-4 col-sm-12">
                <div class="card">
                    <div class="card-header">
                        <div class="card-title h5"><a href="https://shrinefox.com/guides/"><i class="fas fa-graduation-cap"></i> Guides</a></div>
                    </div>
                    <div class="card-footer">
                        <div id="guides-latest"></div>
                    </div>
                </div>
            </div>
            <!--Blog-->
            <div class="column col-4 col-sm-12">
                <div class="card">
                    <div class="card-header">
                        <div class="card-title h5"><a href="https://shrinefox.com/blog/"><i class="fab fa-wordpress"></i> Blog</a></div>
                    </div>
                    <div class="card-footer">
                        <div id="blog-latest"></div>
                    </div>
                </div>
            </div>
        </div>
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
        $('#news-latest').FeedEk({
            FeedUrl: 'https://shrinefox.com/news/feed',
            MaxCount: 1,
            ShowDesc: true,
            ShowPubDate: true,
            DescCharacterLimit: 0,
            TitleLinkTarget: '_blank',
            DateFormat: 'MMM d',
            DateFormatLang: 'en'
        });
        $('#guides-latest').FeedEk({
            FeedUrl: 'https://shrinefox.com/guides/feed',
            MaxCount: 1,
            ShowDesc: true,
            ShowPubDate: true,
            DescCharacterLimit: 0,
            TitleLinkTarget: '_blank',
            DateFormat: 'MMM d',
            DateFormatLang: 'en'
        });
        $('#blog-latest').FeedEk({
            FeedUrl: 'https://shrinefox.com/blog/feed',
            MaxCount: 1,
            ShowDesc: true,
            ShowPubDate: true,
            DescCharacterLimit: 0,
            TitleLinkTarget: '_blank',
            DateFormat: 'MMM d',
            DateFormatLang: 'en'
        });
    </script>
</asp:Content>
