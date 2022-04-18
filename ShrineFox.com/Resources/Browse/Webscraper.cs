﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace ShrineFoxCom.Resources.Browse
{
    public class Webscraper
    {
        public static List<Post> Posts = new List<Post>();

        public static void UpdateTSVs(PlaceHolder control)
        {
            // Load existing posts that aren't from gamebanana
            Posts = Post.Get();
            var NewPosts = new List<Post>();
            // Remove duplicate posts and exclusions
            var exclusions = Properties.Resources.exclude.Split('\n');
            for (int i = 0; i < Posts.Count(); i++)
                if (!NewPosts.Any(x => x.URL.Contains("gamebanana") && x.URL.TrimEnd('/').EndsWith("/" + Posts[i].URL.TrimEnd('/').Split('/').Last())))
                    if (!exclusions.Any(x => Posts[i].Authors.Any(y => y.Contains(x)) || x.Contains(Posts[i].Title)))
                        NewPosts.Add(Posts[i]);
                    
            Posts = NewPosts;

            LiteralControl notice = new LiteralControl();

            // For each game on Gamebanana...
            foreach (var gameTuple in Post.GameList)
            {
                string game = gameTuple.Item1;
                // For each type of Gamebanana submission...
                foreach (string type in Enum.GetNames(typeof(TypeFilter)))
                {
                    bool noneFound = false;
                    // Iterate over pages
                    for (int i = 1; i < 10; i++)
                    {
                        if (!noneFound)
                        {
                            Task.Run(async () =>
                            {
                                await FeedGenerator.GetFeed(i, game, (TypeFilter)Enum.Parse(typeof(TypeFilter), type));
                            }).GetAwaiter().GetResult();

                            ObservableCollection<GameBananaRecord> feed = FeedGenerator.CurrentFeed.Records;
                            // Print errors
                            if (FeedGenerator.error)
                            {
                                switch (Regex.Match(FeedGenerator.exception.Message, @"\d+").Value)
                                {
                                    case "443":
                                        notice.Text = Post.Notice("red", "<b>Error 443</b>: Unable to connect to Gamebanana.");
                                        control.Controls.Add(notice);
                                        break;
                                    case "500":
                                        notice.Text = Post.Notice("red", "<b>Error 500</b>: Unable to connect to Gamebanana.");
                                        control.Controls.Add(notice);
                                        break;
                                    case "503":
                                        notice.Text = Post.Notice("red", "<b>Error 503</b>: Unable to connect to Gamebanana.");
                                        control.Controls.Add(notice);
                                        break;
                                    case "504":
                                        notice.Text = Post.Notice("red", "<b>Error 504</b>: Gamebanana's servers are unavailable.");
                                        control.Controls.Add(notice);
                                        break;
                                    default:
                                        notice.Text = Post.Notice("red", $"<b>Exception</b>: {FeedGenerator.exception.Message}");
                                        control.Controls.Add(notice);
                                        break;
                                }
                                return;
                            }
                            // Add to TSV if not empty
                            if (feed == null)
                            {
                                notice.Text += Post.Notice("red", "<b>Exception</b>: Feed is null, Gamebanana fetch failed.");
                                control.Controls.Add(notice);
                            }
                            else if (feed.Count > 0)
                            {
                                notice.Text += $"<br>{feed.Count} item(s) found";
                                control.Controls.Add(notice);
                                // Add to TSV if not duplicate of existing post or exclusion
                                foreach (var item in feed)
                                {
                                    if (!Posts.Any(y => y.URL.TrimEnd('/').EndsWith("/" + item.Link.ToString().TrimEnd('/').Split('/').Last())))
                                    {
                                        if (!exclusions.Any(x => item.Owner.Name.Trim(' ').Contains(x) || item.Title.Contains(x)))
                                        {
                                            Post post = new Post();
                                            post.Authors = new List<string>() { item.Owner.Name.Trim(' ') };
                                            if (item.HasUpdates)
                                            {
                                                post.Date = item.DateUpdated.ToString("MM/dd/yyyy", new CultureInfo("en-US"));
                                                post.UpdateText = $"<b>Updated {post.Date}</b>";
                                            }
                                            else
                                                post.Date = item.DateAdded.ToString("MM/dd/yyyy", new CultureInfo("en-US"));
                                            if (item.HasDescription)
                                                post.Description = item.Description;
                                            else
                                                post.Description = Post.TruncateLongString(item.ConvertedText.Replace("\t", "").Replace("\r<br>", "").Replace("\r", ""), 150).TrimEnd('\n').TrimEnd('\\').Replace("\n", "<br>");
                                            post.EmbedURL = item.Image.ToString();
                                            post.URL = item.Link.ToString();
                                            if (post.URL.Contains("/tuts/"))
                                                post.Type = "guide";
                                            else if (post.URL.Contains("/mods/") || post.URL.Contains("/wips/") || post.URL.Contains("/sounds/"))
                                                post.Type = "mod";
                                            else if (post.URL.Contains("/tools/"))
                                                post.Type = "tool";
                                            else
                                                post.Type = "mod";
                                            post.Games = new List<string>() { game };
                                            post.Tags = new List<string>() { item.CategoryName.Replace("Other/", "").Replace("Game file", "") };
                                            post.Games = new List<string>() { game.Trim(' ') };
                                            post.Tags = new List<string>() { item.CategoryName.Replace("Other/", "").Replace("Game file", "").Trim(' ') };
                                            post.Title = item.Title;
                                            post.SourceURL = "";
                                            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
                                            post.Id = game.ToLower() + "-" + rgx.Replace(item.Title.ToLower().Replace(" ", ""), "");
                                            if (type.ToLower() == "wips" && !post.Title.ToLower().Contains("wip"))
                                                post.Title = "(WIP) " + post.Title;

                                            Posts.Add(post);
                                        }
                                    }
                                    else
                                    {
                                        // Update existing post
                                        try
                                        {
                                            int index = Posts.IndexOf(Posts.Single(x => x.URL.TrimEnd('/').EndsWith("/" + item.Link.ToString().TrimEnd('/').Split('/').Last())));
                                            if (item.HasUpdates)
                                            {
                                                Posts[index].Date = item.DateUpdated.ToString("MM/dd/yyyy", new CultureInfo("en-US"));
                                                Posts[index].UpdateText = $"<b>Updated {Posts[index].Date}</b>";
                                                // Download P5EX update
                                                if (item.Title == "Persona 5 EX")
                                                {
                                                    var lastUpdate = File.GetCreationTime($"{System.Web.Hosting.HostingEnvironment.MapPath("~/.")}//App_Data//yml_patches//p5_ex//patches//patch.yml");
                                                    if (item.DateUpdated > lastUpdate)
                                                    {
                                                        DownloadP5EXUpdate(item);
                                                    }
                                                }
                                            }
                                        }
                                        catch { }
                                    }
                                }
                            }
                            else
                            {
                                // No items found, skipping...
                                noneFound = true;
                            }
                        }
                    }
                }
            }
            // Save new TSVs
            List<string> lines = new List<string>();
            lines.Add($"ID\tType\tTitle\tGames\tAuthors\tDate\tTags\tDescription\tUpdate\tEmbed\tURL\tSourceURL");
            foreach (var post in Posts)
                lines.Add($"{post.Id}\t{post.Type}\t{post.Title}\t{String.Join(",", post.Games)}\t{String.Join(",", post.Authors)}\t{post.Date}\t{String.Join(",", post.Tags)}\t{post.Description}\t{post.UpdateText}\t{post.EmbedURL}\t{post.URL}\t");
            File.WriteAllLines($"{System.Web.Hosting.HostingEnvironment.MapPath("~/.")}//App_Data//amicitia.tsv", lines.ToArray());
            notice.Text = Post.Notice("green", "<b>Success</b>! Gamebanana database has been updated. Refresh to see changes.");
            control.Controls.Add(notice);
        }

        private static void DownloadP5EXUpdate(GameBananaRecord item)
        {
            foreach (var file in item.AllFiles)
            {
                if (file.FileName.Contains("p5ex_prx_patch") && file.FileName.EndsWith(".7z"))
                {
                    // Download .7z
                    string path = $"{System.Web.Hosting.HostingEnvironment.MapPath("~/.")}//App_Data//yml_patches//p5_ex";
                    string zip = $"{path}//{file.FileName}";
                    using (var client = new WebClient())
                        client.DownloadFile(file.DownloadUrl, zip);
                    // Unzip .7z
                    if (File.Exists(zip))
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.CreateNoWindow = true;
                        startInfo.FileName = $"{System.Web.Hosting.HostingEnvironment.MapPath("~/.")}//Resources//Dependencies//7z.exe";
                        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        startInfo.UseShellExecute = false;
                        startInfo.Arguments = $"x -y \"{zip}\" -o\"" + $"{Path.GetDirectoryName(zip)}\" *.yml -r -aoa";
                        using (Process process = new Process())
                        {
                            process.StartInfo = startInfo;
                            process.Start();
                            process.WaitForExit();
                        }
                    }
                }
            }
            
        }
    }

    public enum FeedFilter
    {
        Featured,
        Recent,
        Popular
    }
    public enum TypeFilter
    {
        Mods,
        WiPs,
        Sounds,
        Tools,
        Tutorials
    }
    public static class FeedGenerator
    {
        private static Dictionary<string, GameBananaModList> feed;
        public static bool error;
        public static Exception exception;
        public static GameBananaModList CurrentFeed = new GameBananaModList();
        public static double GetHeader(this HttpResponseMessage request, string key)
        {
            IEnumerable<string> keys = null;
            if (!request.Headers.TryGetValues(key, out keys))
                return -1;
            return Double.Parse(keys.First());
        }
        public static async Task GetFeed(int page, string game, TypeFilter type)
        {
            error = false;
            if (feed == null)
                feed = new Dictionary<string, GameBananaModList>();
            using (var httpClient = new HttpClient())
            {
                var requestUrl = GenerateUrl(page, game, type);
                if (feed.ContainsKey(requestUrl) && feed[requestUrl].IsValid)
                {
                    CurrentFeed = feed[requestUrl];
                    return;
                }
                try
                {
                    var response = await httpClient.GetAsync(requestUrl);
                    var responseString = await response.Content.ReadAsStringAsync();
                    responseString = responseString.Replace(@"""_aModManagerIntegrations"": []", @"""_aModManagerIntegrations"": {}");
                    var records = JsonConvert.DeserializeObject<ObservableCollection<GameBananaRecord>>(responseString);
                    CurrentFeed = new GameBananaModList();
                    CurrentFeed.Records = records;
                    // Get record count from header
                    var numRecords = response.GetHeader("X-GbApi-Metadata_nRecordCount");
                }
                catch (Exception e)
                {
                    error = true;
                    exception = e;
                    return;
                }
                if (!feed.ContainsKey(requestUrl))
                    feed.Add(requestUrl, CurrentFeed);
                else
                    feed[requestUrl] = CurrentFeed;
            }
        }
        private static string GenerateUrl(int page, string game, TypeFilter type)
        {
            // Base
            var url = "https://gamebanana.com/apiv4/";
            switch (type)
            {
                case TypeFilter.Mods:
                    url += "Mod/";
                    break;
                case TypeFilter.Sounds:
                    url += "Sound/";
                    break;
                case TypeFilter.WiPs:
                    url += "Wip/";
                    break;
                case TypeFilter.Tools:
                    url += "Tool/";
                    break;
                case TypeFilter.Tutorials:
                    url += "Tutorial/";
                    break;
            }
            // Different starting endpoint if requesting all mods instead of specific category
            url += $"ByGame?_aGameRowIds[]=";
            switch (game)
            {
                case "P3FES":
                    url += "8502&";
                    break;
                case "P3P":
                    url += "8583&";
                    break;
                case "P3D":
                    url += "8747&";
                    break;
                case "P4":
                    url += "8761&";
                    break;
                case "P4D":
                    url += "16093&";
                    break;
                case "P4G":
                    url += "8263&";
                    break;
                case "P4AU":
                    url += "16053&";
                    break;
                case "P5":
                    url += "7545&";
                    break;
                case "P5S":
                    url += "9099&";
                    break;
                case "P5R":
                    url += "8464&";
                    break;
                case "P5D":
                    url += "8615&";
                    break;
                case "PQ2":
                    url += "9561&";
                    break;
                case "CFB":
                    url += "8222&";
                    break;
                case "SMT3":
                    url += "10084&";
                    break;
                case "SMTV":
                    url += "14768&";
                    break;
            }
            // Consistent args
            url += $"&_aArgs[]=_sbIsNsfw = false&_sRecordSchema=FileDaddy&_nPerpage=50";
            // Get page number
            url += $"&_nPage={page}";
            return url;
        }
    }

    public class GameBananaAPIV4
    {
        [JsonProperty("_sName")]
        public string Title { get; set; }
        [JsonProperty("_aGame")]
        public GameBananaGame Game { get; set; }
        [JsonIgnore]
        public Uri Image => Media.Where(x => x.Type == "image").ToList().Count > 0 ? new Uri($"{Media[0].Base}/{Media[0].File}")
            : new Uri("https://images.gamebanana.com/static/img/DefaultEmbeddables/Sound.jpg");
        [JsonProperty("_aPreviewMedia")]
        public List<GameBananaMedia> Media { get; set; }
        [JsonProperty("_aSubmitter")]
        public GameBananaMember Owner { get; set; }
        [JsonProperty("_aFiles")]
        public List<GameBananaItemFile> Files { get; set; }
        [JsonProperty("_aAlternateFileSources")]
        public List<GameBananaAlternateFileSource> AlternateFileSources { get; set; }
    }
    public class GameBananaItem
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("Owner().name")]
        public string Owner { get; set; }
        [JsonProperty("Game().name")]
        public string Game { get; set; }
        [JsonProperty("Updates().bSubmissionHasUpdates()")]
        public bool HasUpdates { get; set; }

        [JsonProperty("Updates().aGetLatestUpdates()")]
        public GameBananaItemUpdate[] Updates { get; set; }

        [JsonProperty("Files().aFiles()")]
        public Dictionary<string, GameBananaItemFile> Files { get; set; }
        [JsonProperty("Preview().sSubFeedImageUrl()")]
        public Uri SubFeedImage { get; set; }
        [JsonProperty("Preview().sStructuredDataFullsizeUrl()")]
        public Uri EmbedImage { get; set; }

    }
    public class GameBananaCategory
    {
        [JsonProperty("_idRow")]
        public int? ID { get; set; }
        [JsonProperty("_idParentCategoryRow")]
        public int? RootID { get; set; }
        [JsonProperty("_sModelName")]
        public string Model { get; set; }
        [JsonProperty("_sName")]
        public string Name { get; set; }
        [JsonProperty("_sIconUrl")]
        public Uri Icon { get; set; }
        [JsonIgnore]
        public bool HasIcon => Icon.OriginalString.Length > 0;
    }
    public class GameBananaMember
    {
        [JsonProperty("_sName")]
        public string Name { get; set; }
        [JsonProperty("_sAvatarUrl")]
        public Uri Avatar { get; set; }
        [JsonProperty("_sUpicUrl")]
        public Uri Upic { get; set; }
        [JsonIgnore]
        public bool HasUpic => Upic.OriginalString.Length > 0;
    }
    public class GameBananaGame
    {
        [JsonProperty("_idRow")]
        public int ID { get; set; }
        [JsonProperty("_sName")]
        public string Name { get; set; }
    }
    public class GameBananaModManagerIntegration
    {
        [JsonProperty("_sInstallerName")]
        public string Name { get; set; }
        [JsonProperty("_sInstallerUrl")]
        public Uri Url { get; set; }
        [JsonProperty("_sIconClasses")]
        public string Icon { get; set; }
        [JsonProperty("_sDownloadUrl")]
        public string DownloadUrl { get; set; }
    }
    public class GameBananaRecord
    {
        [JsonProperty("_sName")]
        public string Title { get; set; }
        [JsonProperty("_aGame")]
        public GameBananaGame Game { get; set; }
        [JsonProperty("_sProfileUrl")]
        public Uri Link { get; set; }
        [JsonProperty("_aAlternateFileSources")]
        public List<GameBananaAlternateFileSource> AlternateFileSources { get; set; }
        [JsonIgnore]
        public bool HasAltLinks => AlternateFileSources != null;
        [JsonProperty("_aModManagerIntegrations")]
        public Dictionary<string, List<GameBananaModManagerIntegration>> ModManagerIntegrations { get; set; }
        [JsonIgnore]
        public Uri Image => Media.Where(x => x.Type == "image").ToList().Count > 0 ? new Uri($"{Media[0].Base}/{Media[0].File}")
            : SoundImage(Game.ID);
        [JsonProperty("_aPreviewMedia")]
        public List<GameBananaMedia> Media { get; set; }
        [JsonProperty("_sDescription")]
        public string Description { get; set; }
        [JsonIgnore]
        public bool HasDescription => Description.Length > 100;
        [JsonProperty("_sText")]
        public string Text { get; set; }
        [JsonIgnore]
        public string ConvertedText => ConvertHtmlToText(Text);
        [JsonProperty("_nViewCount")]
        public int Views { get; set; }
        [JsonProperty("_nLikeCount")]
        public int Likes { get; set; }
        [JsonProperty("_nDownloadCount")]
        public int Downloads { get; set; }
        [JsonIgnore]
        public string DownloadString => StringConverters.FormatNumber(Downloads);
        [JsonIgnore]
        public string ViewString => StringConverters.FormatNumber(Views);
        [JsonIgnore]
        public string LikeString => StringConverters.FormatNumber(Likes);
        [JsonProperty("_aSubmitter")]
        public GameBananaMember Owner { get; set; }
        [JsonProperty("_aFiles")]
        public List<GameBananaItemFile> AllFiles { get; set; }
        [JsonIgnore]
        public List<GameBananaItemFile> Files => AllFiles.Where(x => ModManagerIntegrations.ContainsKey(x.ID)
            && !x.Description.Contains(".disable-modbrowser")).ToList();
        [JsonProperty("_aCategory")]
        public GameBananaCategory Category { get; set; }
        [JsonProperty("_aRootCategory")]
        public GameBananaCategory RootCategory { get; set; }
        [JsonIgnore]
        public string CategoryName => StringConverters.FormatSingular(RootCategory.Name, Category.Name);
        [JsonIgnore]
        public bool HasLongCategoryName => CategoryName.Length > 30;
        [JsonIgnore]
        public bool HasDownloads => AllFiles != null && Files.Count > 0;
        [JsonIgnore]
        public bool Compatible => HasAltLinks || HasDownloads;

        [JsonProperty("_tsDateUpdated")]
        public long DateUpdatedLong { get; set; }
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1);

        [JsonIgnore]
        public DateTime DateUpdated => Epoch.AddSeconds(DateUpdatedLong);
        [JsonProperty("_tsDateAdded")]
        public long DateAddedLong { get; set; }

        [JsonIgnore]
        public DateTime DateAdded => Epoch.AddSeconds(DateAddedLong);
        [JsonIgnore]
        public string DateAddedFormatted => $"Added {StringConverters.FormatTimeAgo(DateTime.UtcNow - DateAdded)}";
        [JsonIgnore]
        public bool HasUpdates => DateAdded.CompareTo(DateUpdated) != 0;
        [JsonIgnore]
        public string DateUpdatedAgo => $"Updated {StringConverters.FormatTimeAgo(DateTime.UtcNow - DateUpdated)}";
        private Uri SoundImage(int game)
        {
            // Get different Sound thumbnail per game
            switch (game)
            {
                case 8502:
                    return new Uri("https://media.discordapp.net/attachments/792245872259235850/842426607712993351/P3FSound.png");
                case 8263:
                    return new Uri("https://media.discordapp.net/attachments/792245872259235850/842426608882679818/P4GSound.png");
                case 7545:
                    return new Uri("https://media.discordapp.net/attachments/792245872259235850/842426604789170236/P5Sound.png");
                case 9099:
                    return new Uri("https://media.discordapp.net/attachments/792245872259235850/842426607490170891/P5SSound.png");
                default:
                    return new Uri("https://images.gamebanana.com/static/img/DefaultEmbeddables/Sound.jpg");
            }
        }
        private string ConvertHtmlToText(string html)
        {
            // Newlines
            html = html.Replace("<br>", "\n");
            html = html.Replace(@"</li>", "\n");
            html = html.Replace(@"</h3>", "\n");
            html = html.Replace(@"</h2>", "\n");
            html = html.Replace(@"</h1>", "\n");
            html = html.Replace("<ul>", "\n");
            // Bullet point
            html = html.Replace("<li>", "• ");
            // Unique spaces
            html = html.Replace("&nbsp;", " ");
            html = html.Replace(@"\u00a0", " ");
            // Unique characters
            html = html.Replace("&amp;", "&");
            html = html.Replace("&gt;", ">");
            // Remove tabs
            html = html.Replace("\t", string.Empty);
            // Remove all unaccounted html tags
            html = Regex.Replace(html, "<.*?>", string.Empty);
            // Convert newlines of 3 or more to 2 newlines
            html = Regex.Replace(html, "[\\r\\n]{3,}", "\n\n", RegexOptions.Multiline);
            // Trim extra whitespace at start and end
            return html.Trim();
        }
    }
    public class GameBananaAlternateFileSource
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; } = "Mirror";
    }
    public class GameBananaModList
    {
        public ObservableCollection<GameBananaRecord> Records { get; set; }
        public int TotalPages { get; set; }
        public DateTime TimeFetched = DateTime.UtcNow;
        public bool IsValid => (DateTime.UtcNow - TimeFetched).TotalMinutes < 30;
    }
    public class GameBananaMedia
    {
        [JsonProperty("_sType")]
        public string Type { get; set; }
        [JsonProperty("_sUrl")]
        public Uri Audio { get; set; }
        [JsonProperty("_sBaseUrl")]
        public Uri Base { get; set; }
        [JsonProperty("_sFile")]
        public Uri File { get; set; }
        [JsonProperty("_sCaption")]
        public string Caption { get; set; }
    }

    public class GameBananaItemUpdate
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1);

        [JsonProperty("_sTitle")]
        public string Title { get; set; }

        [JsonProperty("_aChangeLog")]
        public GameBananaItemUpdateChange[] Changes { get; set; }

        [JsonProperty("_sText")]
        public string Text { get; set; }

        [JsonProperty("_tsDateAdded")]
        public long DateAddedLong { get; set; }

        [JsonIgnore]
        public DateTime DateAdded => Epoch.AddSeconds(DateAddedLong);
    }

    public class GameBananaItemUpdateChange
    {
        [JsonProperty("cat")]
        public string Category { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class GameBananaItemFile
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1);
        [JsonProperty("_idRow")]
        public string ID { get; set; }
        [JsonProperty("_sFile")]
        public string FileName { get; set; }

        [JsonProperty("_nFilesize")]
        public long Filesize { get; set; }
        [JsonIgnore]
        public string ConvertedFileSize => StringConverters.FormatSize(Filesize);

        [JsonProperty("_sDownloadUrl")]
        public string DownloadUrl { get; set; }

        [JsonProperty("_sDescription")]
        public string Description { get; set; }
        [JsonProperty("_bContainsExe")]
        public bool ContainsExe { get; set; }
        [JsonProperty("_nDownloadCount")]
        public int Downloads { get; set; }
        [JsonIgnore]
        public string DownloadString => StringConverters.FormatNumber(Downloads);

        [JsonProperty("_aMetadata")]
        [JsonExtensionData]
        public IDictionary<string, JToken> FileMetadata { get; set; }

        [JsonProperty("_tsDateAdded")]
        public long DateAddedLong { get; set; }

        [JsonIgnore]
        public DateTime DateAdded => Epoch.AddSeconds(DateAddedLong);

        [JsonIgnore]
        public string TimeSinceUpload => StringConverters.FormatTimeAgo(DateTime.UtcNow - DateAdded);

    }
    public class GithubFile
    {
        public string FileName { get; set; }
        public string DownloadUrl { get; set; }
        public int Downloads { get; set; }
        public string DownloadString => StringConverters.FormatNumber(Downloads);
        public long Filesize { get; set; }
        public string ConvertedFileSize => StringConverters.FormatSize(Filesize);
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string TimeSinceUpload => StringConverters.FormatTimeAgo(DateTime.UtcNow - DateAdded);
    }

    public static class StringConverters
    {
        // Load all suffixes in an array  
        static readonly string[] suffixes =
        { " Bytes", " KB", " MB", " GB", " TB", " PB" };
        public static string FormatSize(long bytes)
        {
            int counter = 0;
            decimal number = (decimal)bytes;
            while (Math.Round(number / 1024) >= 1)
            {
                number = number / 1024;
                counter++;
            }
            return bytes != 0 ? string.Format("{0:n2}{1}", number, suffixes[counter]) :
                string.Format("{0:n0}{1}", number, suffixes[counter]);
        }
        public static string FormatNumber(int number)
        {
            if (number > 1000000)
                return Math.Round((double)number / 1000000, 1).ToString() + "M";
            else if (number > 1000)
                return Math.Round((double)number / 1000, 1).ToString() + "K";
            else
                return number.ToString();
        }
        public static string FormatTimeSpan(TimeSpan timeSpan)
        {
            if (timeSpan.TotalMinutes < 60)
            {
                return Math.Floor(timeSpan.TotalMinutes).ToString() + "min";
            }
            else if (timeSpan.TotalHours < 24)
            {
                return Math.Floor(timeSpan.TotalHours).ToString() + "hr";
            }
            else if (timeSpan.TotalDays < 7)
            {
                return Math.Floor(timeSpan.TotalDays).ToString() + "d";
            }
            else if (timeSpan.TotalDays < 30.4)
            {
                return Math.Floor(timeSpan.TotalDays / 7).ToString() + "wk";
            }
            else if (timeSpan.TotalDays < 365.25)
            {
                return Math.Floor(timeSpan.TotalDays / 30.4).ToString() + "mo";
            }
            else
            {
                return Math.Floor(timeSpan.TotalDays % 365.25).ToString() + "yr";
            }
        }
        public static string FormatTimeAgo(TimeSpan timeSpan)
        {
            if (timeSpan.TotalMinutes < 60)
            {
                var minutes = Math.Floor(timeSpan.TotalMinutes);
                return minutes > 1 ? $"{minutes} minutes ago" : $"{minutes} minute ago";
            }
            else if (timeSpan.TotalHours < 24)
            {
                var hours = Math.Floor(timeSpan.TotalHours);
                return hours > 1 ? $"{hours} hours ago" : $"{hours} hour ago";
            }
            else if (timeSpan.TotalDays < 7)
            {
                var days = Math.Floor(timeSpan.TotalDays);
                return days > 1 ? $"{days} days ago" : $"{days} day ago";
            }
            else if (timeSpan.TotalDays < 30.4)
            {
                var weeks = Math.Floor(timeSpan.TotalDays / 7);
                return weeks > 1 ? $"{weeks} weeks ago" : $"{weeks} week ago";
            }
            else if (timeSpan.TotalDays < 365.25)
            {
                var months = Math.Floor(timeSpan.TotalDays / 30.4);
                return months > 1 ? $"{months} months ago" : $"{months} month ago";
            }
            else
            {
                var years = Math.Floor(timeSpan.TotalDays / 365.25);
                return years > 1 ? $"{years} years ago" : $"{years} year ago";
            }
        }
        public static string FormatSingular(string rootCat, string cat)
        {
            rootCat = rootCat.Replace("User Interface", "UI");

            if (cat == "Skin Packs")
                return cat.Substring(0, cat.Length - 1);

            if (rootCat[rootCat.Length - 1] == 's')
            {
                if (cat == rootCat)
                {
                    rootCat = rootCat.Replace("xes", "xs").Replace("xs/", "xes/");
                    return rootCat.Substring(0, rootCat.Length - 1);
                }
                else
                    return $"{cat} {rootCat.Substring(0, rootCat.Length - 1)}";
            }
            else
            {
                if (cat == rootCat)
                    return rootCat;
                else
                    return $"{cat} {rootCat}";
            }
        }
    }
}