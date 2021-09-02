<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ShrineFoxcom._Default" EnableEventValidation="false" EnableTheming="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<asp:PlaceHolder ID="Sidebar" runat="server"></asp:PlaceHolder>
<a href="https://shrinefox.com/"><i class="fa fa-home" aria-hidden="true"></i> ShrineFox.com</a> <i class="fa fa-angle-right" aria-hidden="true"></i> <%: Page.Title %>
<h1><%: Page.Title %></h1>
You've found the internet's <a href="https://shrinefox.com/browse">largest collection</a> of SMT mods, tools, and guides.
What started as our humble Skype group has now spawned multiple modding communities. 
    Ours is named <i>Amicitia</i>, after the latin word for friendship (and TGE's <a href="https://shrinefox.com/browse?post=amicitia">premiere tool</a>).

<br><br>This site's goal is to feature all resources in a convenient place. If you're just getting started, 
    I recommend browsing our <a href="https://shrinefox.com/guides">guides</a>.

<br><br>I also mod other games and systems, as seen on my YouTube channel below. 
    Read about my miscellaneous projects on <a href="https://shrinefox.com/blog">my blog</a>, or catch up on <a href="https://shrinefox.com/news">Amicitia news</a>.
<br><br>

<hr>
<!--YouTube Playlist-->
<h2>Latest Videos</h2>
From time to time, I showcase my mods in action via cinematic highlight reels. 
    I plan to make more of them in the future. You can expect video tutorials as well!
<br><br>
<div style="margin-left: auto;margin-right:auto;max-width:800px;">
    <div id="YourPlayerID" style="height: 100%;"></div>
</div>
<script>
    document.addEventListener("DOMContentLoaded", function (event) {
        var controller = new YTV('YourPlayerID', {
			channelId: 'UCrB3t1zAQPwAeWtI8RZIOvQ',
			playlist: 'PLU6By7bu-RSsXcvqRkDdh3R4OGva8_No4',
            responsive: true
        });
    });
</script>
<br><br>
<hr>
<!--Latest Articles-->
<h2>Latest Articles</h2>
<div class="notices blue">
    <p>
        This section will be updated as new articles are published.
    </p>
</div>
<table>
	<tr>
		<td style="vertical-align: top;">
			<a href="https://shrinefox.com/news/">
				<h4>
					<i class="fas fa-newspaper"></i> News
				</h4>
			</a>
			<div id="news-latest"></div>
		</td>
		<td style="vertical-align: top;padding-left: 2em;">
			<a href="https://shrinefox.com/guides/">
				<h4>
					<i class="fas fa-graduation-cap"></i> Guides
				</h4>
			</a>
			<div id="guides-latest"></div>
		</td>
		<td style="vertical-align: top;padding-left: 2em;">
			<a href="https://shrinefox.com/blog/">
				<h4>
					<i class="fab fa-wordpress"></i> Blog
				</h4>
			</a>
			<div id="blog-latest"></div>
		</td>
	</tr>
</table>
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
<br><br>
</asp:Content>
