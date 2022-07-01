using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ShrineFoxCom
{
    public class Post
    {
        public Post()
        {
            this.Id = Id;
            this.Type = Type;
            this.Title = Title;
            this.Games = Games;
            this.Authors = Authors;
            this.Date = Date;
            this.Tags = Tags;
            this.Description = Description;
            this.EmbedURL = EmbedURL;
            this.URL = URL;
            this.UpdateText = UpdateText;
            this.SourceURL = SourceURL;
        }

        public string Id { get; set; } = "";
        public string Type { get; set; } = "";
        public string Title { get; set; } = "";
        public List<string> Games { get; set; } = new List<string>();
        public List<string> Authors { get; set; } = new List<string>();
        public string Date { get; set; } = "";
        public List<string> Tags { get; set; } = new List<string>();
        public string Description { get; set; } = "";
        public string EmbedURL { get; set; } = "";
        public string URL { get; set; } = "";
        public string UpdateText { get; set; } = "";
        public string SourceURL { get; set; } = "";
        public static List<Post> Get()
        {
            List<Post> posts = new List<Post>();
            // Get TSV lines...
            string[] tsvFile = File.ReadAllLines($"{System.Web.Hosting.HostingEnvironment.MapPath("~/.")}//App_Data//amicitia.tsv");
            // Skip first line (column names)
            for (int i = 1; i < tsvFile.Length; i++)
            {
                // Separate tabs into array
                var split = tsvFile[i].Split('\t');

                // Add to post list
                if (split.Any(x => !String.IsNullOrEmpty(x)))
                {
                    Post post = new Post();
                    post.Id = split[0].Trim('"');
                    post.Type = split[1].ToLower().Trim('"');
                    post.Title = split[2].Trim('"');
                    post.Games = split[3].ToLower().Split(',').ToList();
                    for (int x = 0; x < post.Games.Count; x++)
                        post.Games[x] = post.Games[x].Trim('"').Trim(' ');
                    post.Authors = split[4].Split(',').ToList();
                    for (int x = 0; x < post.Authors.Count; x++)
                        post.Authors[x] = post.Authors[x].Trim('"').Trim(' ');
                    post.Date = split[5].Trim('"');
                    post.Tags = split[6].Split(',').ToList();
                    for (int x = 0; x < post.Tags.Count; x++)
                        post.Tags[x] = post.Tags[x].Trim('"').Trim(' ');
                    post.Description = split[7].Trim('"');
                    post.UpdateText = split[8].Trim('"');
                    post.EmbedURL = split[9].Trim('"');
                    post.URL = split[10].Trim('"');
                    post.SourceURL = split[11].Trim('"');

                    posts.Add(post);
                }
            }
            
            return posts;
        }

        public static string Write(Post post)
        {
            string result;

            // Post Summary
            result = Properties.Resources.Post;
            
            // Download/Read/Clipboard Text
            if (post.Type == "cheat")
                result = result.Replace("POSTBUTTONTEXT", "<i class=\"fa fa-clipboard\"></i> Copy to Clipboard");
            else if (post.Type == "guide")
                result = result.Replace("POSTBUTTONTEXT", "<i class=\"fa fa-book\"></i> Read Guide");
            else
                result = result.Replace("POSTBUTTONTEXT", "<i class=\"fa fa-download\"></i> Download");
            
            // Thumbnail
            if (post.Type != "cheat")
            {
                // Use YouTube thumbnail as image and link to video
                if (post.EmbedURL.Contains("youtu"))
                {
                    string videoID = post.EmbedURL.Substring(post.EmbedURL.IndexOf("v=") + 2);
                    string ytThumb = $"https://img.youtube.com/vi/{videoID}/hqdefault.jpg";
                    result = result.Replace("POSTMEDIAURL", ytThumb).Replace("display:none","display:block");
                }
                else if (post.EmbedURL.Contains("streamable.com"))
                {
                    // Use Streamable thumbnail as image and link to video
                    string videoID = post.EmbedURL.Replace("https://streamable.com/", "");
                    result = result.Replace("POSTMEDIAURL", $"https:///cdn-cf-east.streamable.com/image/{videoID}.jpg\">").Replace("display:none", "display:block");
                }
                else if (post.EmbedURL != null && post.EmbedURL.Trim() != "") // Use provided image link
                    result = result.Replace("POSTMEDIAURL", post.EmbedURL);
                else // If no URL, use default Amicitia icon and hide image link
                    result = result.Replace("POSTMEDIAURL", "https://shrinefox.com/images/logo.svg");
                result = result.Replace("EMBEDURL", post.EmbedURL);
            }
            else
            {
                // If cheat, put cheatcode in thumbnail spot
                result = result.Replace("POSTEMBED", $"<div id=\"cheat{post.Id}\" class=\"cheatcode\">{post.UpdateText}</div>");
                result = result.Replace("POSTMEDIAURL", $"javascript:copyDivToClipboard('cheat{post.Id}')");
            }

            // Link to individual post
            result = result.Replace("POSTID", "<a href=\"https://shrinefox.com/browse?post=" + post.Id + "\"><i class=\"fa fa-link\"></i></a>");

            // Post Game/Type
            result = result.Replace("POSTTYPE", $"{post.Games.First().ToUpper()} {FirstCharToUpper(post.Type)}").Replace("POSTTITLE", post.Title);

            // Gamebanana Icon
            if (post.URL.Contains("gamebanana"))
                result = result.Replace("GAMEBANANA", "<img src=\"./images/gb.png\" alt=\"Gamebanana\">");
            else
                result = result.Replace("GAMEBANANA", "");

            // Authors
            string authors = "";
            foreach (string author in post.Authors.Where(x => !x.Equals("Unknown Author") && !String.IsNullOrWhiteSpace(x)))
            {
                authors += $"<a href=\"https://shrinefox.com/browse?author={author.Trim()}\">{author.Trim()}</a>";
                if (post.Authors.IndexOf(author) != post.Authors.Count() - 1)
                    authors += ", ";
            }
            result = result.Replace("POSTAUTHORS", authors);

            // Last Updated Date
            if (!String.IsNullOrEmpty(post.UpdateText) && post.UpdateText.Trim() != "")
                result = result.Replace("POSTDATE", post.Date + " (updated)");
            else
                result = result.Replace("POSTDATE", post.Date);

            // Source URL
            if (!String.IsNullOrEmpty(post.SourceURL) && post.SourceURL.Trim() != "")
                result = result.Replace("POSTSOURCE", $"<a class=\"btn btn-link float-right\" href=\"{post.SourceURL}\">Source</a>");
            else
                result = result.Replace("POSTSOURCE", "");

            // Download/Read/Copy URL
            result = result.Replace("POSTURL", post.URL);

            // Shortened Description
            result = result.Replace("POSTDESCRIPTION", post.Description);

            // Tags
            string tags = "";
            foreach (string tag in post.Tags.Where(x => !String.IsNullOrWhiteSpace(x)))
                tags += $"<div class=\"rh_tag\"><a href=\"https://shrinefox.com/browse?tag={tag.Trim()}\">{tag}</a></div>";
            result = result.Replace("POSTTAGS", tags);

            return result;
        }

        //Games in dropdown
        public static List<Tuple<string, string>> GameList = new List<Tuple<string, string>>() {
            new Tuple<string, string>("P5", "Persona 5"),
            new Tuple<string, string>("P5R", "Persona 5 Royal"),
            new Tuple<string, string>("P5S", "Persona 5 Strikers"),
            new Tuple<string, string>("P5D", "Persona 5 Dancing"),
            new Tuple<string, string>("P4", "Persona 4"),
            new Tuple<string, string>("P4G", "Persona 4 Golden"),
            new Tuple<string, string>("P4AU", "Persona 4 Arena Ultimax"),
            new Tuple<string, string>("P4D", "Persona 4 Dancing"),
            new Tuple<string, string>("P3FES", "Persona 3 FES"),
            new Tuple<string, string>("P3P", "Persona 3 Portable"),
            new Tuple<string, string>("P3D", "Persona 3 Dancing"),
            new Tuple<string, string>("PQ", "Persona Q"),
            new Tuple<string, string>("PQ2", "Persona Q2"),
            new Tuple<string, string>("CFB", "Catherine Full Body"),
            new Tuple<string, string>("SMT3", "SMT3: Nocturne"),
            new Tuple<string, string>("SMTV", "SMT V")
        };

        public static List<string> TypeList = new List<string>()
        {
            "Mod",
            "Tool",
            "Guide"
        };

        public static string FirstCharToUpper(string input)
        {
            return input.First().ToString().ToUpper() + input.Substring(1);
        }

        public static string TruncateLongString(string str, int maxLength)
        {
            if (string.IsNullOrEmpty(str) || str.Length < maxLength) return str;

            return str.Substring(0, Math.Min(str.Length, maxLength)) + " ... ";
        }
    }
}