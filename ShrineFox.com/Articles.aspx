<%@ Page Title="Articles" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Articles.aspx.cs" Inherits="ShrineFoxCom.Articles" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<div class="navipath">
		<a href="/"><i class="fa fa-home" aria-hidden="true"></i> ShrineFox.com</a> 
		<i class="fa fa-angle-right" aria-hidden="true"></i> <%: Page.Title %>
	</div>
	<h1><%: Page.Title %></h1>
    <div class="container">
        <div class="columns">
            <!--News-->
            <div class="column col-4 col-sm-12">
                <div class="card">
                    <div class="card-header">
                        <div class="card-title h5"><a href="/news/"><i class="fas fa-newspaper"></i> News</a></div>
                    </div>
                    <div class="card-footer">
                        <div id="news-latest"></div>
                        <br><br>
                        <h5><a href="/news/">See more News articles</a></h5>
                    </div>
                </div>
            </div>
            <!--Guides-->
            <div class="column col-4 col-sm-12">
                <div class="card">
                    <div class="card-header">
                        <div class="card-title h5"><a href="/guides/"><i class="fas fa-graduation-cap"></i> Guides</a></div>
                    </div>
                    <div class="card-footer">
                        <div id="guides-latest"></div>
                        <br><br>
                        <h5><a href="/guides/">See more Guides</a></h5>
                    </div>
                </div>
            </div>
            <!--Blog-->
            <div class="column col-4 col-sm-12">
                <div class="card">
                    <div class="card-header">
                        <div class="card-title h5"><a href="/blog/"><i class="fab fa-wordpress"></i> Blog</a></div>
                    </div>
                    <div class="card-footer">
                        <div id="blog-latest"></div>
                        <br><br>
                        <h5><a href="/blog/">See more Blogposts</a></h5>
                    </div>
                </div>
            </div>
        </div>
    </div>
	<script>
        $('#news-latest').FeedEk({
            FeedUrl: '/news/feed',
            MaxCount: 10,
            ShowDesc: true,
            ShowPubDate: true,
            DescCharacterLimit: 0,
            TitleLinkTarget: '_blank',
            DateFormat: 'MMM d',
            DateFormatLang: 'en'
        });
        $('#guides-latest').FeedEk({
            FeedUrl: '/guides/feed',
            MaxCount: 10,
            ShowDesc: true,
            ShowPubDate: true,
            DescCharacterLimit: 0,
            TitleLinkTarget: '_blank',
            DateFormat: 'MMM d',
            DateFormatLang: 'en'
        });
        $('#blog-latest').FeedEk({
            FeedUrl: '/blog/feed',
            MaxCount: 10,
            ShowDesc: true,
            ShowPubDate: true,
            DescCharacterLimit: 0,
            TitleLinkTarget: '_blank',
            DateFormat: 'MMM d',
            DateFormatLang: 'en'
        });
    </script>
</asp:Content>
