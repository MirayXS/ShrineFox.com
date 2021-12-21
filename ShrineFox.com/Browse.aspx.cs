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
        protected void Page_Load(object sender, EventArgs e)
        {
            // If it's been more than half a day since an update, get Gamebanana data
            var lastWriteTime = File.GetLastWriteTime($"{System.Web.Hosting.HostingEnvironment.MapPath("~/.")}//App_Data//amicitia.tsv");
            if (DateTime.Now > lastWriteTime.AddHours(6))
                Webscraper.UpdateTSVs(Warning);

            // The number of posts on one page
            int maxPostsPerPage = 15;
            
            // Potential query values
            string game = "";
            string type = "";
            string author = "";
            string tag = "";
            string post = "";
            string page = "";

            // Split values from url
            if (Request.QueryString.Count > 0 && !String.IsNullOrEmpty(Request.QueryString[0]))
            {
                string[] queries = Request.Url.Query.Replace("?", "").Split('&');
                foreach (string query in queries)
                {
                    if (query.StartsWith("tag="))
                        tag = query.Replace("tag=", "");
                    else if (query.StartsWith("author="))
                        author = query.Replace("author=", "");
                    else if (query.StartsWith("type="))
                        type = query.Replace("type=", "");
                    else if (query.StartsWith("game="))
                        game = query.Replace("game=", "");
                    else if (query.StartsWith("post="))
                        post = query.Replace("post=", "");
                    else if (query.StartsWith("page="))
                        page = query.Replace("page=", "");
                }
            }

            // Validate Page Number
            // Can only be numerical, default to Page 1
            int pageNumber = 1;
            if (page != "")
                try { pageNumber = Convert.ToInt32(page); } catch { pageNumber = 1; }
                

            // Validate Game
            // Can only be a supported game, or default to none
            if (Post.GameList.Any(x => x.Item1.ToLower().Equals(game.ToLower())))
                game = game.ToLower();
            else
                game = "";

            // Validate Type
            // Can only be a supported game, or default to none
            if (Post.TypeList.Any(x => x.ToLower().Equals(type.ToLower())))
                type = type.ToLower();
            else
                type = "";

            // Set Up Sidebar
            // For the browser, only game/type accordions are shown, label is changed from browse to type (unlike home/other pages)
            LiteralControl SidebarHtml = new LiteralControl();
            SidebarHtml.Text = Properties.Resources.IndexSidebar.Replace("<!--Accordions-->", Properties.Resources.Browse.Replace("Browse</label>", "Type</label>") + Properties.Resources.Games);
            // For Game/Type queries, highlight the option in sidebar, adding current section to its url
            if (type != "")
                SidebarHtml.Text = SidebarHtml.Text.Replace("typeall","").Replace($"type{type}", "active").Replace("browse?game=", $"browse?type={type}&game=").Replace("/browse\" class=\"typeall\"", $"/browse?type={type}\" class=\"typeall\"");
            else
                SidebarHtml.Text = SidebarHtml.Text.Replace($"typeall", "active");
            if (game != "")
                SidebarHtml.Text = SidebarHtml.Text.Replace("gameall", "").Replace($"game{game}", "active").Replace("browse?type=", $"browse?game={game}&type=").Replace("/browse\" class=\"typeall\"", $"/browse?game={game}\" class=\"gameall\"");
            else
                SidebarHtml.Text = SidebarHtml.Text.Replace($"gameall", "active");
            // Add Sidebar to Page
            Sidebar.Controls.Add(SidebarHtml);

            // Serialize Post data from .tsv
            List<Post> posts = Post.Get();
            // Default value for total pages
            int totalPages = 1;

            // Show links to selected queries above results
            LiteralControl NavHtml = new LiteralControl();
            // Narrow down posts based on queries
            LiteralControl PostsHtml = new LiteralControl();
            // Generate Pagination
            LiteralControl Pagination1Html = new LiteralControl();
            LiteralControl Pagination2Html = new LiteralControl();

            try
            {
                if (post != "")
                {
                    // Individual posts have priority over all other queries
                    posts = new List<Post> { posts.Single(x => x.Id.ToLower().Equals(post.ToLower())) };
                    NavHtml.Text += $" <i class=\"fa fa-angle-right\" aria-hidden=\"true\"></i> Post: <a href=\"https://shrinefox.com/browse?post={post}\">{posts.First().Title}</a>";
                }
                else
                {
                    // Posts by type are further narrowed down by game, author, and tag in that order
                    if (type != "")
                    {
                        posts = posts.Where(x => x.Type.ToLower().Equals(type)).ToList();
                        NavHtml.Text += $" <i class=\"fa fa-angle-right\" aria-hidden=\"true\"></i> Type: <a href=\"https://shrinefox.com/browse?type={type}\">{Post.FirstCharToUpper(type)}</a>";
                    }
                    if (game != "")
                    {
                        posts = posts.Where(x => x.Games.Any(y => y.ToLower().Equals(game))).ToList();
                        NavHtml.Text += $" <i class=\"fa fa-angle-right\" aria-hidden=\"true\"></i> Game: <a href=\"https://shrinefox.com/browse?game={game}\">{game.ToUpper()}</a>";
                    }
                    if (author != "")
                    {
                        posts = posts.Where(x => x.Authors.Contains(author, StringComparer.OrdinalIgnoreCase)).ToList();
                        NavHtml.Text += $" <i class=\"fa fa-angle-right\" aria-hidden=\"true\"></i> Author: <a href=\"https://shrinefox.com/author?type={author}\">{author}</a>";
                    }
                    if (tag != "")
                    {
                        posts = posts.Where(x => x.Tags.Contains(tag, StringComparer.OrdinalIgnoreCase)).ToList();
                        NavHtml.Text += $" <i class=\"fa fa-angle-right\" aria-hidden=\"true\"></i> Tag: <a href=\"https://shrinefox.com/browse?tag={tag}\">{tag}</a>";
                    }

                    // Reverse Post Order (latest to oldest)
                    posts = posts.OrderBy(p => DateTime.Parse(p.Date, CultureInfo.CreateSpecificCulture("en-US"))).Reverse().ToList();
                    // Total Results Count
                    Pagination1Html.Text += $"<i class=\"fas fa-history\" aria-hidden=\"true\"></i> Updated {lastWriteTime.Humanize()}<br><b>{posts.Count}</b> results";
                    // Total number of pages in this query
                    totalPages = Convert.ToInt32(RoundUp(Convert.ToDecimal(posts.Count) / Convert.ToDecimal(maxPostsPerPage), 0));

                    // Include page in navigation if 2 or higher
                    if (pageNumber > 1)
                    {
                        NavHtml.Text += $" <i class=\"fa fa-angle-right\" aria-hidden=\"true\"></i> Page {pageNumber}</a>";
                    }
                }
            }
            catch { posts = new List<Post>(); totalPages = posts.Count;  }
            
            if (posts.Count > 0 && pageNumber >= 1)
            {
                // Write posts to page, starting from query page number
                for (int i = (pageNumber - 1) * maxPostsPerPage; i < posts.Count && i < ((pageNumber - 1) * maxPostsPerPage) + maxPostsPerPage; i++)
                    PostsHtml.Text += Post.Write(posts[i]);
            }
            else
            {
                // Show 404 if there's 0 posts, or if there's not enough posts to populate a given page number
                NavHtml.Text += $" <i class=\"fa fa-angle-right\" aria-hidden=\"true\"></i> 404 Not Found";
                PostsHtml.Text += Post.Notice("red", "No Posts Were Found!");
            }

            // Build Pagination URL
            string newQueryString = "";
            if (tag != "")
                newQueryString += $"&tag={tag}";
            if (author != "")
                newQueryString += $"&author={author}";
            if (game != "")
                newQueryString += $"&game={game}";
            if (type != "")
                newQueryString += $"&type={type}";
            newQueryString += "&page=";
            newQueryString = newQueryString.Remove(0, 1);
            newQueryString = "?" + newQueryString;
            
            // Generate and Add Pagination HTML
            string pagination = Pagination(pageNumber, totalPages, newQueryString);
            Pagination1Html.Text += $"<br>{pagination}";
            Pagination2Html.Text += $"<br>{pagination}";
            Pagination1.Controls.Add(Pagination1Html);
            Pagination2.Controls.Add(Pagination2Html);

            // Add Warnings/Tip Notices
            string[] aemulus = new string[] { "P5", "P5S", "P3FES", "P4G" };
            string[] hostFS = new string[] { "P3FES", "P4", "SMT3" };
            string[] modCpk = new string[] { "PQ", "PQ2", "P3D", "P5D", "P5" };
            // Cheats Construction
            if (type == "cheat")
            {
                Pagination1Html.Text += Post.Notice("red", "The cheats section is still under construction, sorry for the inconvenience.");
            }
            // Pan shoutout, P5R Modding Guide
            if (game.ToUpper().Equals("P5R") && type != "cheat")
            {
                Pagination1Html.Text += Post.Notice("green", "Special thanks to <a href=\"https://twitter.com/regularpanties\">@regularpanties</a> for the generous donation of a 6.72 PS4<br>and a plethora of documentation that made this section possible.");
                Pagination1Html.Text += Post.Notice("blue", "To learn how to install and run P5R mods on PS4, see <a href=\"https://shrinefox.com/guides/2020/09/30/modding-persona-5-royal-jp-on-ps4-fw-6-72\">this guide</a>.");
            }
            // SMTV Modding News Link
            if (game.ToUpper().Equals("SMTV") && type != "cheat")
            {
                Pagination1Html.Text += Post.Notice("green", "See <a href=\"https://shrinefox.com/news/smtv-modding-has-begun\">this article</a> for the latest info about creating and installing SMT V mods.");
            }
            // Mod install guides, P4G Vita/PC difference
            if (aemulus.Any(x => x.Equals(game.ToUpper())) && type != "tool" && type != "guide" && type != "cheat")
            {
                Pagination1Html.Text += Post.Notice("blue", "To learn how to install and run <b>Aemulus Package Manager</b> compatible mods, see <a href=\"https://shrinefox.com/guides/2021/06/21/when-to-use-aemulus-or-mod-compendium/\">this guide</a>.");
                if (game.ToUpper().Equals("P4G"))
                    Pagination1Html.Text += Post.Notice("yellow", "PC mods are not compatible with Vita. Installing Vita mods still requires the <a href=\"https://shrinefox.com/browse?post=p4g-mod-cpk\">mod.cpk patch</a> and <a href=\"https://shrinefox.com/browse?post=modcompendium\">Mod Compendium</a>. <a href=\"https://shrinefox.com/guides/2021/06/21/when-to-use-aemulus-or-mod-compendium/\">Read more</a> about the differences between mod managers.");
            }
            // HostFS, Nocturne difference
            if (hostFS.Any(x => x.Equals(game.ToUpper()) && type != "tool" && type != "guide" && type != "cheat"))
            {
                string additional = "";
                if (game.ToUpper().Equals("SMT3"))
                    additional += "<br><b>Note:</b> This only applies to PS2 mods. PC and PS2 mods are not compatible with the opposite platform.";
                Pagination1Html.Text += Post.Notice("yellow", $"Modding this game requires a <b>HostFS</b> patch on PCSX2. <a href=\"https://shrinefox.com/guides/2020/09/30/modding-persona-5-royal-jp-on-ps4-fw-6-72\">Read more here</a>.{additional}<br><br>If you have to build mods into as a PS2 ISO, see <a href=\"https://shrinefox.com/browse?post=p34-modding-guide\">this guide</a> instead.");
            }
            // Mod.cpk Patch Required for Mod
            if (modCpk.Any(x => x.Equals(game.ToUpper()) && type != "tool" && type != "guide" && type != "cheat"))
            {
                string link = "";
                switch (game.ToUpper()) 
                { 
                    case "P5":
                        link = "https://shrinefox.com/apps/PatchCreator";
                        break;
                    case "P3D":
                        link = "https://shrinefox.com/browse?post=p5d-p5dmodloading";
                        break;
                    case "P5D":
                        link = "https://shrinefox.com/browse?post=p5d-p5dmodloading";
                        break;
                    case "PQ":
                        link = "https://shrinefox.com/browse?post=pq2-patchesguide";
                        break;
                    case "PQ2":
                        link = "https://shrinefox.com/browse?post=pq2-patchesguide";
                        break;
                }

                Pagination1Html.Text += Post.Notice("yellow", $"Modding this game requires a <a href=\"{link}\">mod.cpk patch</a>.");
            }

            // Display Navigation and Post Contents
            Navigation.Controls.Add(NavHtml);
            Posts.Controls.Add(PostsHtml);
        }

        private static string Pagination(int currentPage, int totalPages, string query)
        {
            string html = "<ul class=\"pagination\">";
            int lastPage = 0;

            // Previous Button and First Page
            if (currentPage <= 1)
                html += "<li class=\"page-item disabled\"><a href=\"#\" tabindex=\"-1\">Prev</a></li>";
            else
                html += $"<li class=\"page-item\"><a href=\"https://shrinefox.com/browse{query}{currentPage - 1}\">Prev</a></li>";

            // First Page
            if (currentPage >= 2)
                html += $"<li class=\"page-item\"><a href=\"https://shrinefox.com/browse{query}1\">1</a></li>";

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
                        lastPage = p;
                        html += $"<li class=\"page-item\"><a href=\"https://shrinefox.com/browse{query}{p}\">{p}</a></li>";
                    }
                }
            }

            // Current Post Number
            html += $"<li class=\"page-item active\"><a href=\"https://shrinefox.com/browse{query}{currentPage}\">{currentPage}</a></li>";

            // Next Post Numbers
            for (int p = currentPage + 1; p < totalPages; ++p)
            {
                // Limit to the next 2 pages
                if (p < currentPage + 2)
                {
                    lastPage = p;
                    html += $"<li class=\"page-item\"><a href=\"https://shrinefox.com/browse{query}{p}\">{p}</a></li>";
                }   
            }

            if (currentPage < totalPages)
            {
                // ...
                if (currentPage + 1 < totalPages)
                    html += "<li class=\"page-item\"><span>...</span></li>";
                // Last Post Number & Next Button
                html += $"<li class=\"page-item\"><a href=\"https://shrinefox.com/browse{query}{totalPages}\">{totalPages}</a></li><li class=\"page-item\"><a href=\"https://shrinefox.com/browse{query}{totalPages}\">Next</a></li>";
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
    }
}