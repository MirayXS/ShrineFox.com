using Humanizer;
using ShrineFoxCom.Resources.Browse;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShrineFoxCom
{
    public partial class Browse : Page
    {
        public static string url = "/browse";
        protected void Page_Load(object sender, EventArgs e)
        {
            // If it's been more than half a day since an update, get Gamebanana data
            var lastWriteTime = File.GetLastWriteTime($"{System.Web.Hosting.HostingEnvironment.MapPath("~/.")}//App_Data//amicitia.tsv");
#if DEBUG
            if (DateTime.Now > lastWriteTime.AddHours(6))
                Webscraper.UpdateTSVs(Notices);
#endif

            // The number of posts on one page
            int maxPostsPerPage = 15;
            // Default value for total pages
            int totalPages = 1;

            // Get queries from url
            QueryCollection queries = ParseQueries(Asp.ParseQueryString(Request.Url.Query));
            // Show options to add queries to URL and reload
            GenerateQueryOptions(queries);

            // Serialize Post data from .tsv
            List<Post> posts = Post.Get();
            
            // Create HTML Collections for page elements
            LiteralControl PostsHtml = new LiteralControl();
            LiteralControl NoticeHtml = new LiteralControl();
            LiteralControl LastUpdatedHtml = new LiteralControl();
            LiteralControl PaginationHtml = new LiteralControl();
            LiteralControl Pagination2Html = new LiteralControl();

            // Narrow down posts based on queries
            try
            {
                if (queries != new QueryCollection())
                {
                    if (queries.PostID != "")
                    {
                        // Individual posts have priority over all other queries
                        posts = new List<Post> { posts.Single(x => x.Id.ToLower().Equals(queries.PostID.ToLower())) };
                    }
                    else
                    {
                        // Posts by type are further narrowed down by game, author, and tag
                        foreach (var game in queries.Games)
                            if (Post.GameList.Any(x => x.Item1.ToUpper().Equals(game.ToUpper())))
                                posts = posts.Where(p => p.Games.Any(g => g.ToUpper().Equals(game.ToUpper()))).ToList();
                        foreach (var type in queries.Types)
                            if (Post.TypeList.Any(x => x.ToLower().Equals(type.ToLower())))
                                posts = posts.Where(p => p.Type.ToLower().Equals(type.ToLower())).ToList();
                        foreach (var author in queries.Authors)
                            if (author != "")
                                posts = posts.Where(p => p.Authors.Any(a => a.ToLower().Equals(author.ToLower()))).ToList();
                        foreach (var tag in queries.Tags)
                            if (tag != "")
                                posts = posts.Where(p => p.Tags.Any(t => t.ToLower().Equals(tag.ToLower()))).ToList();
                    }
                }

                // Reverse Post Order (latest to oldest) and remove exclusions
                posts = posts.Where(x => !File.ReadAllLines(HttpContext.Current.Server.MapPath("~/App_Data/exclude.txt")).Any(y => x.Authors.Contains(y)))
                    .OrderBy(p => DateTime.Parse(p.Date, CultureInfo.CreateSpecificCulture("en-US"))).Reverse().ToList();
                // Total Results Count
                LastUpdatedHtml.Text += $"<i class=\"fas fa-history\" aria-hidden=\"true\"></i> Updated {lastWriteTime.Humanize()}<br><b>{posts.Count}</b> results";
                // Total number of pages in this query
                totalPages = Convert.ToInt32(RoundUp(Convert.ToDecimal(posts.Count) / Convert.ToDecimal(maxPostsPerPage), 0));
            }
            catch { posts = new List<Post>(); totalPages = posts.Count; }
            
            if (posts.Count > 0 && queries.PageNumber >= 1)
            {
                // Write posts to page, starting from query page number
                for (int i = (queries.PageNumber - 1) * maxPostsPerPage; i < posts.Count && i < ((queries.PageNumber - 1) * maxPostsPerPage) + maxPostsPerPage; i++)
                    PostsHtml.Text += Post.Write(posts[i]);
            }
            else
            {
                // Show 404 if there's 0 posts, or if there's not enough posts to populate a given page number
                PostsHtml.Text = Html.Notice("red", "No Posts Were Found!");
            }
            
            // Generate and Add Pagination HTML
            string pagination = Pagination(queries, totalPages);
            PaginationHtml.Text += $"<br>{pagination}";
            Pagination1.Controls.Add(PaginationHtml);
            Pagination2Html.Text += $"<br>{pagination}";
            Pagination2.Controls.Add(Pagination2Html);

            // Add Warnings/Tip Notices
            string[] aemulus = new string[] { "P5", "P5S", "P3FES", "P4G" };
            string[] hostFS = new string[] { "P3FES", "P4", "SMT3" };
            string[] modCpk = new string[] { "PQ", "PQ2", "P3D", "P5D", "P5" };
            // Pan shoutout, P5R Modding Guide
            if (queries.Games.Any(x => x.ToUpper().Equals("P5R")))
            {
                NoticeHtml.Text += Html.Notice("green", "Special thanks to <a href=\"https://twitter.com/regularpanties\">@regularpanties</a> for the generous donation of a 6.72 PS4<br>and a plethora of documentation that made this section possible.");
                if (queries.Types.Any(y => y.Equals("mod") || y.Equals("tool") || y.Equals("guide")))
                    NoticeHtml.Text += Html.Notice("blue", "To learn how to install and run P5R mods on PS4, see <a href=\"/guides/2020/09/30/modding-persona-5-royal-jp-on-ps4-fw-6-72\">this guide</a>.");
            }
            // SMTV Modding News Link
            if (queries.Games.Any(x => x.ToUpper().Equals("SMTV")) && queries.Types.Any(y => y.Equals("mod")))
                NoticeHtml.Text += Html.Notice("green", "See <a href=\"/news/smtv-modding-has-begun\">this article</a> for the latest info about creating and installing SMT V mods.");
            // Mod install guides, P4G Vita/PC difference
            if (aemulus.Any(y => queries.Games.Any(x => x.ToUpper().Equals(y.ToUpper()))) && queries.Types.Any(y => y.Equals("mod")))
            {
                NoticeHtml.Text += Html.Notice("blue", "To learn how to install and run <b>Aemulus Package Manager</b> compatible mods, see <a href=\"/guides/2021/06/21/when-to-use-aemulus-or-mod-compendium/\">this guide</a>.");
                if (queries.Games.Any(x => x.ToUpper().Equals("P4G")))
                    NoticeHtml.Text += Html.Notice("yellow", "PC mods are not compatible with Vita (or vice versa). Installing Vita mods still requires the <a href=\"/browse?post=p4g-mod-cpk\">mod.cpk patch</a> and <a href=\"/browse?post=modcompendium\">Mod Compendium</a>. <a href=\"/guides/2021/06/21/when-to-use-aemulus-or-mod-compendium/\">Read more</a> about the differences between mod managers.");
            }
            // HostFS, Nocturne difference
            if (hostFS.Any(y => queries.Games.Any(x => x.ToUpper().Equals(y.ToUpper()))) && queries.Types.Any(y => y.Equals("mod")))
            {
                string additional = "";
                if (queries.Games.Any(x => x.ToUpper().Equals("SMT3")))
                    additional += "<br><b>Note:</b> This only applies to PS2 mods. PC and PS2 mods are not compatible with the opposite platform.";
                NoticeHtml.Text += Html.Notice("yellow", $"Modding this game requires a <b>HostFS</b> patch on PCSX2. <a href=\"/guides/2020/09/30/modding-persona-5-royal-jp-on-ps4-fw-6-72\">Read more here</a>.{additional}<br><br>If you have to build mods into as a PS2 ISO, see <a href=\"/browse?post=p34-modding-guide\">this guide</a> instead.");
            }
            // Mod.cpk Patch Required for Mod
            if (modCpk.Any(y => queries.Games.Any(x => x.ToUpper().Equals(y.ToUpper()))) && queries.Types.Any(y => y.Equals("mod")))
            {
                foreach (var game in modCpk.Where(y => queries.Games.Any(x => x.ToUpper().Equals(y.ToUpper()))))
                {
                    string link = "";
                    switch (game.ToUpper())
                    {
                        case "P5":
                            link = "/apps/PatchCreator";
                            break;
                        case "P3D":
                            link = "/browse?post=p5d-p5dmodloading";
                            break;
                        case "P5D":
                            link = "/browse?post=p5d-p5dmodloading";
                            break;
                        case "PQ":
                            link = "/browse?post=pq2-patchesguide";
                            break;
                        case "PQ2":
                            link = "/browse?post=pq2-patchesguide";
                            break;
                    }
                    NoticeHtml.Text += Html.Notice("yellow", $"Modding this game requires a <a href=\"{link}\">mod.cpk patch</a>.");
                }
            }

            // Display Posts
            Posts.Controls.Add(PostsHtml);
            // Display Notices
            Notices.Controls.Add(NoticeHtml);
            // Display Last Updated Time
            LastUpdated.Controls.Add(LastUpdatedHtml);
        }

        private void GenerateQueryOptions(QueryCollection queries)
        {
            LiteralControl optionsHtml = new LiteralControl();
            QueryCollection newQuery = new QueryCollection();
            foreach (var author in queries.Authors)
                newQuery.Authors.Add(author);
            foreach (var game in queries.Games)
                newQuery.Games.Add(game);
            foreach (var type in queries.Types)
                newQuery.Types.Add(type);
            foreach (var tag in queries.Tags)
                newQuery.Tags.Add(tag);

            // Add game or type to query string from list
            foreach (var game in Post.GameList.Where(x => !newQuery.Games.Any(y => y.ToLower().Equals(x.Item1.ToLower()))))
            {
                QueryCollection tempQuery = new QueryCollection();
                foreach (var author in newQuery.Authors)
                    tempQuery.Authors.Add(author);
                foreach (var g in newQuery.Games)
                    tempQuery.Games.Add(g);
                foreach (var type in newQuery.Types)
                    tempQuery.Types.Add(type);
                foreach (var tag in newQuery.Tags)
                    tempQuery.Tags.Add(tag);

                tempQuery.Games.Add(game.Item1.ToUpper());
                optionsHtml.Text += $"<div class=\"rh_tag\"><a href=\"{url}{BuildQueryString(tempQuery)}\">{game.Item1.ToUpper()}</a></div>";
            }
            foreach (var type in Post.TypeList.Where(x => !newQuery.Types.Any(y => y.ToLower().Equals(x.ToLower()))))
            {
                QueryCollection tempQuery = new QueryCollection();
                foreach (var author in newQuery.Authors)
                    tempQuery.Authors.Add(author);
                foreach (var g in newQuery.Games)
                    tempQuery.Games.Add(g);
                foreach (var t in newQuery.Types)
                    tempQuery.Types.Add(t);
                foreach (var tag in newQuery.Tags)
                    tempQuery.Tags.Add(tag);

                tempQuery.Types.Add(type);
                optionsHtml.Text += $"<div class=\"rh_tag\"><a href=\"{url}{BuildQueryString(tempQuery)}\">{Post.FirstCharToUpper(type)}</a></div>";
            }

            optionsHtml.Text += "<br><div class=\"tags\"><ul class=\"tag-list\">";

            // Remove query from existing query string
            foreach (var game in newQuery.Games)
            {
                QueryCollection tempQuery = new QueryCollection();
                foreach (var author in newQuery.Authors)
                    tempQuery.Authors.Add(author);
                foreach (var g in newQuery.Games)
                    tempQuery.Games.Add(g);
                foreach (var type in newQuery.Types)
                    tempQuery.Types.Add(type);
                foreach (var tag in newQuery.Tags)
                    tempQuery.Tags.Add(tag);

                tempQuery.Games.Remove(game);
                optionsHtml.Text += $"<li class=\"tag-item\"><span>Game: {game.ToUpper()}</span> <a href=\"{url}{BuildQueryString(tempQuery)}\" class=\"remove-button ng-binding\">x</a></li>";
            }
            foreach (var type in newQuery.Types)
            {
                QueryCollection tempQuery = new QueryCollection();
                foreach (var author in newQuery.Authors)
                    tempQuery.Authors.Add(author);
                foreach (var g in newQuery.Games)
                    tempQuery.Games.Add(g);
                foreach (var t in newQuery.Types)
                    tempQuery.Types.Add(t);
                foreach (var tag in newQuery.Tags)
                    tempQuery.Tags.Add(tag);

                tempQuery.Types.Remove(type);
                optionsHtml.Text += $"<li class=\"tag-item\"><span>Type: {Post.FirstCharToUpper(type)}</span> <a href=\"{url}{BuildQueryString(tempQuery)}\" class=\"remove-button ng-binding\">x</a></li>";
            }
            foreach (var tag in newQuery.Tags)
            {
                QueryCollection tempQuery = new QueryCollection();
                foreach (var author in newQuery.Authors)
                    tempQuery.Authors.Add(author);
                foreach (var g in newQuery.Games)
                    tempQuery.Games.Add(g);
                foreach (var type in newQuery.Types)
                    tempQuery.Types.Add(type);
                foreach (var t in newQuery.Tags)
                    tempQuery.Tags.Add(t);

                tempQuery.Tags.Remove(tag);
                optionsHtml.Text += $"<li class=\"tag-item\"><span>Tag: {Post.FirstCharToUpper(tag)}</span> <a href=\"{url}{BuildQueryString(tempQuery)}\" class=\"remove-button ng-binding\">x</a></li>";
            }
            foreach (var author in newQuery.Authors)
            {
                QueryCollection tempQuery = new QueryCollection();
                foreach (var a in newQuery.Authors)
                    tempQuery.Authors.Add(a);
                foreach (var g in newQuery.Games)
                    tempQuery.Games.Add(g);
                foreach (var type in newQuery.Types)
                    tempQuery.Types.Add(type);
                foreach (var tag in newQuery.Tags)
                    tempQuery.Tags.Add(tag);

                tempQuery.Authors.Remove(author);
                optionsHtml.Text += $"<li class=\"tag-item\"><span>Author: {Post.FirstCharToUpper(author)}</span> <a href=\"{url}{BuildQueryString(tempQuery)}\" class=\"remove-button ng-binding\">x</a></li>";
            }

            optionsHtml.Text += "</ul></div>";

            QueryOptions.Controls.Add(optionsHtml);
        }

        public static string BuildQueryString(QueryCollection queries)
        {
            string queryString = "";

            foreach (var tag in queries.Tags)
                queryString += $"&tag={tag.Replace(" ","_")}";
            foreach (var author in queries.Authors)
                queryString += $"&author={author}";
            foreach (var game in queries.Games)
                queryString += $"&game={game}";
            foreach (var type in queries.Types)
                queryString += $"&type={type}";
            if (queries.PostID != "")
                queryString += $"&post={queries.PostID}";
            if (queries.PageNumber > 1)
                queryString += $"&page={queries.PageNumber}";
            if (queryString.Length > 0)
                queryString = queryString.Remove(0, 1);
            queryString = "?" + queryString;

            return queryString;
        }

        private static string Pagination(QueryCollection queries, int totalPages)
        {
            string html = "<ul class=\"pagination\">";
            int currentPage = queries.PageNumber;

            // Previous Button and First Page
            if (currentPage <= 1)
                html += "<li class=\"page-item disabled\"><a href=\"#\" tabindex=\"-1\">Prev</a></li>";
            else
            {
                queries.PageNumber -= 1;
                html += $"<li class=\"page-item\"><a href=\"{url}{BuildQueryString(queries)}\">Prev</a></li>";
            }

            // First Page
            if (currentPage >= 2)
            {
                queries.PageNumber = 1;
                html += $"<li class=\"page-item\"><a href=\"{url}{BuildQueryString(queries)}\">1</a></li>";
            }

            // Previous Post Numbers
            if (currentPage > 2)
            {
                // ...
                html += "<li class=\"page-item\"><span>...</span></li>";

                for (int p = 2; p < currentPage; ++p)
                {
                    // Limit to the last 2 pages
                    if (p > currentPage - 2 && currentPage - 2 > 0)
                    {
                        queries.PageNumber = p;
                        html += $"<li class=\"page-item\"><a href=\"{url}{BuildQueryString(queries)}\">{p}</a></li>";
                    }
                }
            }

            // Current Post Number
            queries.PageNumber = currentPage;
            html += $"<li class=\"page-item active\"><a href=\"{url}{BuildQueryString(queries)}\">{currentPage}</a></li>";

            // Next Post Numbers
            for (int p = currentPage + 1; p < totalPages; ++p)
            {
                // Limit to the next 2 pages
                if (p < currentPage + 2)
                {
                    queries.PageNumber = p;
                    html += $"<li class=\"page-item\"><a href=\"{url}{BuildQueryString(queries)}\">{p}</a></li>";
                }   
            }

            if (currentPage < totalPages)
            {
                // ...
                if (currentPage + 1 < totalPages)
                    html += "<li class=\"page-item\"><span>...</span></li>";
                // Last Post Number & Next Button
                queries.PageNumber = totalPages;
                html += $"<li class=\"page-item\"><a href=\"{url}{BuildQueryString(queries)}\">{totalPages}</a></li>";
                queries.PageNumber = currentPage + 1;
                html += $"<li class=\"page-item\"><a href=\"{url}{BuildQueryString(queries)}\">Next</a></li>";
            }
            else
            {
                // Disabled Next Button
                html += $"<li class=\"page-item disabled\"><a href=\"#\" tabindex=\"-1\">Next</a></li>";
            }

            return html + "</ul>";
        }

        public static decimal RoundUp(decimal numero, int numDecimales)
        {
            decimal valorbase = Convert.ToDecimal(Math.Pow(10, numDecimales));
            decimal resultado = Decimal.Round(numero * 1.00000000M, numDecimales + 1, MidpointRounding.AwayFromZero) * valorbase;
            decimal valorResiduo = 10M * (resultado - Decimal.Truncate(resultado));

            if (valorResiduo > 0)
            {
                if (valorResiduo >= 5)
                {
                    var ajuste = Convert.ToDecimal(Math.Pow(10, -(numDecimales + 1)));
                    numero += ajuste;
                    return Decimal.Round(numero * 1.00000000M, numDecimales, MidpointRounding.AwayFromZero);
                }
                else
                    return Decimal.Round(numero * 1.00M, numDecimales, MidpointRounding.AwayFromZero) + 1;
            }
            else
            {
                return Decimal.Round(numero * 1.00M, numDecimales, MidpointRounding.AwayFromZero);
            }
        }

        public static QueryCollection ParseQueries(List<Tuple<string, string>> queries)
        {
            QueryCollection qCollection = new QueryCollection();

            if (queries.Any(x => x.Item1.Equals("page")))
                try { qCollection.PageNumber = Convert.ToInt32(queries.First(x => x.Item1.Equals("page")).Item2); } catch { }
            foreach (var query in queries.Where(x => x.Item1.Equals("game") && Post.GameList.Any(y => y.Item1.ToLower().Equals(x.Item2.ToLower()))))
                qCollection.Games.Add(query.Item2.ToUpper());
            foreach (var query in queries.Where(x => x.Item1.Equals("type") && Post.TypeList.Any(y => y.ToLower().Equals(x.Item2.ToLower()))))
                qCollection.Types.Add(query.Item2.ToLower());
            foreach (var query in queries.Where(x => x.Item1.Equals("author")))
                qCollection.Authors.Add(Post.FirstCharToUpper(query.Item2).Replace("%20", " "));
            foreach (var query in queries.Where(x => x.Item1.Equals("tag")))
                qCollection.Tags.Add(Post.FirstCharToUpper(query.Item2).Replace("%20", " "));
            foreach (var query in queries.Where(x => x.Item1.Equals("post")))
                qCollection.PostID = query.Item2.ToLower();

            return qCollection;
        }

        public class QueryCollection
        {
            public List<string> Types { get; set; } = new List<string>();
            public List<string> Games { get; set; } = new List<string>();
            public List<string> Authors { get; set; } = new List<string>();
            public List<string> Tags { get; set; } = new List<string>();
            public int PageNumber { get; set; } = 1;
            public string PostID { get; set; } = "";
        }

        

    }
}