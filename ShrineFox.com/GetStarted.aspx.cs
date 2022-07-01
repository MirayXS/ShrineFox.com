using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ShrineFoxCom
{
    public partial class GetStarted : Page
    {
        // List of platforms, their shorthand abbreviations and the games for each one
        List<Platform> Platforms = new List<Platform>() 
        {
            new Platform() { Name = "PlayStation 2",
                             ShortName = "PS2",
                             Games = Games.PS2Games
            },
            new Platform() { Name = "PlayStation 3",
                             ShortName = "PS3",
                             Games = Games.PS3Games
            },
            new Platform() { Name = "PlayStation 4", 
                             ShortName = "PS4", 
                             Games = Games.PS4Games 
            },
            new Platform() { Name = "PlayStation Vita", 
                             ShortName = "PSV",
                             Games = Games.PSVGames
            },
            new Platform() { Name = "PlayStation Portable",
                             ShortName = "PSP",
                             Games = Games.PSPGames
            },
            new Platform() { Name = "Nintendo 3DS",
                             ShortName = "3DS",
                             Games = Games.PQGames
            },
            new Platform() { Name = "PC",
                             ShortName = "PC",
                             Games = Games.PCGames
            },
        };

        // Current selection of game, platform and region
        public static string selectedGame = "";
        public static string selectedPlatform = "";
        public static string selectedRegion = "";

        protected void Page_Init(object sender, EventArgs e)
        {
            // Create hidden panels with dropdowns containing all possible default options
            GenerateDynamicControls();
            // Get queries from url
            if (!IsPostBack)
            {
                var queries = Asp.ParseQueryString(Request.Url.Query);
                foreach(var query in queries)
                {
                    if (query.Item1.ToLower() == "platform")
                        selectedPlatform = query.Item2.ToUpper();
                    if (query.Item1.ToLower() == "game")
                        selectedGame = query.Item2.ToUpper();
                    if (query.Item1.ToLower() == "region")
                        selectedRegion = query.Item2.ToUpper();
                }
            }
        }

        public void ErrorMsg(string msg)
        {
            var errorPanel = Asp.GetControlByType<Panel>(MainPlaceHolder, c => c.ID == "pnl_Error");
            errorPanel.Controls.Add(new LiteralControl() { Text = $"{Html.Notice("red", msg)}" });
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            // Update options if user has selected something
            if (IsPostBack || (selectedGame != "" || selectedPlatform != "" || selectedRegion != ""))
                UpdateDynamicControls();

            // Initiate download if button clicked
            string postBackId = AspExtension.GetPostBackControlId(Page);
            if (!string.IsNullOrEmpty(postBackId) && (postBackId == "PKG" || postBackId == "EBOOT" || postBackId == "PNACH" ))
                Download(postBackId);
        }

        public void GenerateDynamicControls()
        {
            // Get pairs of platform and abbreviation
            List<Tuple<string, string>> platformPairs = new List<Tuple<string, string>>();
            foreach (var platform in Platforms)
                platformPairs.Add(new Tuple<string, string>(platform.Name, platform.ShortName));

            // Platform panel and options (visible by default)
            MainPlaceHolder.Controls.Add(
                Asp.DropDownPanel("pnl_Platform",
                "1. Select Game Platform", "Choose which console or device to add Mod Support to.",
                Asp.DropDownList("platformList", platformPairs),
                true
                )
            );
            
            // Get all possible game and abbreviation pairs
            List<Tuple<string, string>> gamePairs = new List<Tuple<string, string>>();
            foreach (var pltfrm in Platforms)
                foreach (var game in pltfrm.Games)
                    if (!gamePairs.Any(x => x.Equals(new Tuple<string, string>(game.Name, game.ShortName))))
                        gamePairs.Add(new Tuple<string, string>(game.Name, game.ShortName));

            // Game panel and options (hidden by default)
            MainPlaceHolder.Controls.Add(
                Asp.DropDownPanel("pnl_Game",
                "2. Select Game Title", "Choose which title to add Mod Support to.",
                Asp.DropDownList("gameList", gamePairs),
                false
                )
            );
            
            // Panel for error messages
            MainPlaceHolder.Controls.Add(new Panel() { ID = "pnl_Error" });

            // Region panel and options (hidden by default)
            MainPlaceHolder.Controls.Add(
                Asp.DropDownPanel("pnl_Region",
                "3. Select Game Region", "Choose the supported region that matches your game.",
                Asp.DropDownList("regionList", new List<Tuple<string, string>>() {
                    new Tuple<string,string>("USA","USA"),
                    new Tuple<string, string>("EUR", "EUR")
                }),
                false
                )
            );

            // Results
            MainPlaceHolder.Controls.Add(new Panel() { ID = "pnl_Results" });


            // Patches panel and options (hidden by default)
            MainPlaceHolder.Controls.Add(
                Asp.SplitCardPanel("pnl_Patches",
                "4. Choose Patches", $"Mouse over each patch to view the description.",
                false
                )
            );

            var patchesPanelContentLeft = Asp.GetControlByType<Panel>(MainPlaceHolder, c => c.ID == "pnl_Patches_content_left");
            CheckBoxList chkBoxList_Patches = new CheckBoxList() { ID = "chkBoxList_Patches" };
            patchesPanelContentLeft.Controls.Add(chkBoxList_Patches);
        }

        private void UpdateDynamicControls()
        {
            // Get all dynamically generated controls
            var platformList = Asp.GetControlByType<DropDownList>(MainPlaceHolder, c => c.ID == "platformList");
            var gamePanel = Asp.GetControlByType<Panel>(MainPlaceHolder, c => c.ID == "pnl_Game");
            var gameList = Asp.GetControlByType<DropDownList>(MainPlaceHolder, c => c.ID == "gameList");
            var regionPanel = Asp.GetControlByType<Panel>(MainPlaceHolder, c => c.ID == "pnl_Region");
            var regionList = Asp.GetControlByType<DropDownList>(MainPlaceHolder, c => c.ID == "regionList");
            var patchesPanel = Asp.GetControlByType<Panel>(MainPlaceHolder, c => c.ID == "pnl_Patches");

            // Get previously selected Platform
            if (IsPostBack || string.IsNullOrEmpty(selectedPlatform))
                selectedPlatform = platformList.SelectedValue;

            // Re-select previously selected Platform in dropdown
            if (platformList.Items.FindByValue(selectedPlatform) != null)
                platformList.SelectedIndex = platformList.Items.IndexOf(platformList.Items.FindByValue(selectedPlatform));
            else
                selectedPlatform = "";

            // Make Game panel visible if Platform selected
            if (!string.IsNullOrEmpty(selectedPlatform))
            {
                gamePanel.Visible = true;

                // Get previously selected Game
                if (!string.IsNullOrEmpty(gameList.SelectedValue))
                    selectedGame = gameList.SelectedValue;

                // Update Game dropdown options based on selected Platform
                List<Tuple<string, string>> gamePairs = new List<Tuple<string, string>>();
                Platform platform = Platforms.Single(x => x.ShortName.Equals(selectedPlatform));
                foreach (var game in platform.Games)
                    if (!gamePairs.Any(x => x.Equals(new Tuple<string, string>(game.Name, game.ShortName))))
                        gamePairs.Add(new Tuple<string, string>(game.Name, game.ShortName));
                gameList.Items.Clear();
                foreach (var gamePair in gamePairs)
                    gameList.Items.Add(new ListItem { Text = gamePair.Item1, Value = gamePair.Item2 });
                // Re-select previously selected Game in dropdown
                if (gameList.Items.FindByValue(selectedGame) != null)
                    gameList.SelectedIndex = gameList.Items.IndexOf(gameList.Items.FindByValue(selectedGame));
                else
                    selectedGame = "";

                // Make Region panel visible if Game selected
                if (!string.IsNullOrEmpty(selectedGame))
                {
                    regionPanel.Visible = true;

                    // Get previously selected Region
                    if (!string.IsNullOrEmpty(regionList.SelectedValue))
                        selectedRegion = regionList.SelectedValue;

                    // Update Region dropdown options based on selected Game
                    List<Tuple<string, string>> regionPairs = new List<Tuple<string, string>>();
                    List<Game> selectedGames = platform.Games.Where(x => x.ShortName.Equals(selectedGame)).ToList();
                    foreach (var game in selectedGames)
                        if (!regionPairs.Any(x => x.Equals(new Tuple<string, string>(game.Region, game.Region))))
                            regionPairs.Add(new Tuple<string, string>(game.Region, game.Region));
                    regionList.Items.Clear();
                    foreach (var regionPair in regionPairs)
                        regionList.Items.Add(new ListItem { Text = regionPair.Item1, Value = regionPair.Item2 });
                    // Re-select previously selected Region in dropdown
                    if (regionList.Items.FindByValue(selectedRegion) != null)
                        regionList.SelectedIndex = regionList.Items.IndexOf(regionList.Items.FindByValue(selectedRegion));
                    else
                        selectedRegion = "";

                    // Make Patches panel visible if Region selected
                    if (!string.IsNullOrEmpty(selectedRegion))
                    {
                        patchesPanel.Visible = true;

                        var selectedGame = selectedGames.Single(x => x.Region.Equals(selectedRegion));
                        // Update Patches based on all prior selections
                        UpdatePatches(selectedGame);
                        // Update Results
                        UpdateResults(selectedGame);
                    }
                    else
                        patchesPanel.Visible = false;
                }
                else
                {
                    regionPanel.Visible = false;
                    patchesPanel.Visible = false;
                }
            }
            else
            {
                gamePanel.Visible = false;
                regionPanel.Visible = false;
                patchesPanel.Visible = false;
            }
        }

        private void UpdatePatches(Game game)
        {
            var patchesContentPanelLeft = Asp.GetControlByType<Panel>(MainPlaceHolder, c => c.ID == "pnl_Patches_content_left");
            var patchesContentPanelRight = Asp.GetControlByType<Panel>(MainPlaceHolder, c => c.ID == "pnl_Patches_content_right");
            var patchesContentPanelFooter = Asp.GetControlByType<Panel>(MainPlaceHolder, c => c.ID == "pnl_Patches_footer");
            var patchesCheckboxList = Asp.GetControlByType<CheckBoxList>(patchesContentPanelLeft, c => c.ID == "chkBoxList_Patches");

            // Patch Description placeholder for mouseover
            patchesContentPanelRight.Controls.Add(new LiteralControl() { Text = $"<span id=\"desc\"></span>" });

            // Recreate checklist items
            List<ListItem> items = new List<ListItem>();
            foreach (var patch in game.Patches)
            {
                // Re-check previously checked item
                var item = new ListItem() { Text = patch.Name, Value = patch.ShortName, Selected = patch.OnByDefault, Enabled = !patch.AlwaysOn };
                if (patchesCheckboxList.Items.FindByValue(item.Value) != null)
                    item.Selected = patchesCheckboxList.Items.FindByValue(item.Value).Selected;

                // Add javascript to show/hide description div
                item.Attributes.Add("onmouseover", $"document.getElementById('desc').innerHTML=\"<b>{patch.Name}</b> by {patch.Author}<br><br>{patch.Description}\";");

                // Add checkbox for each patch
                items.Add(item);
            }
            patchesCheckboxList.Items.Clear();
            patchesCheckboxList.Items.AddRange(items.ToArray());

            // Add download buttons
            if (selectedPlatform == "PS4")
                patchesContentPanelFooter.Controls.Add(new Button() { ID="PKG", CssClass = "btn btn-primary", Text = "Download Update PKG" });
            if (selectedPlatform == "PS4" || selectedPlatform == "PSV")
            {
                patchesContentPanelFooter.Controls.Add(new LiteralControl() { Text = " " });
                patchesContentPanelFooter.Controls.Add(new Button() { ID = "EBOOT", CssClass = "btn btn-secondary", Text = "Download EBOOT" });
            }
            if (selectedPlatform == "PS2")
                patchesContentPanelFooter.Controls.Add(new Button() { ID = "PNACH", CssClass = "btn btn-primary", Text = "Download PNACH" });
            // Mod Manager notice
            patchesContentPanelFooter.Controls.Add(new LiteralControl() { Text = "<br><br><i class=\"fa-solid fa-thumbs-up\"></i> Once you have installed these patches, you're ready to <a href=\"https://shrinefox.com/guides/2021/06/21/when-to-use-aemulus-or-mod-compendium/\">choose a Mod Manager!</a>" });
        }

        private void UpdateResults(Game game)
        {
            var resultsContentPanel = Asp.GetControlByType<Panel>(MainPlaceHolder, c => c.ID == "pnl_Results");

            // Show game boxart with link to wiki page
            Panel selectedGamePanel = Asp.SplitCardPanel("pnl_SelectedGame", $"TITLE ID: {game.TitleID}", $"{selectedGame} {selectedRegion} ({selectedPlatform})", true);
            var selectedGamePanelLeft = Asp.GetControlByType<Panel>(selectedGamePanel, c => c.ID == "pnl_SelectedGame_content_left");
            var selectedGamePanelRight = Asp.GetControlByType<Panel>(selectedGamePanel, c => c.ID == "pnl_SelectedGame_content_right");
            var selectedGamePanelFooter = Asp.GetControlByType<Panel>(selectedGamePanel, c => c.ID == "pnl_SelectedGame_footer");
            selectedGamePanelLeft.Controls.Add(new LiteralControl() { Text = $"<img src=\"{game.ImageUrl}\" style=\"max-height:150px;\" class=\"img-responsive img-fit-contain\">" });
            selectedGamePanelRight.Controls.Add(new LiteralControl()
            {
                Text = "<b>Need help installing these patches?</b> " +
                $"<a href=\"https://amicitia.miraheze.org/wiki/{game.Name}\">Learn more about {selectedGame} modding</a> on the Wiki!" +
                $"<br>While you're here, see also:<br><span style=\"font-size:8pt;\"><ul>" +
                $"<li><a href=\"https://shrinefox.com/browse?game={selectedGame}&type=mod\">{selectedGame} Mods</a></li>" +
                $"<li><a href=\"https://shrinefox.com/browse?game={selectedGame}&type=tool\">{selectedGame} Modding Tools</a></li>" +
                $"<li><a href=\"https://shrinefox.com/browse?game={selectedGame}&type=guide\">{selectedGame} Modding Tutorials</a></li>" +
                "</ul></span>"
            });
            if (selectedPlatform == "PS2")
                selectedGamePanelFooter.Controls.Add(new LiteralControl() { Text = "Read <a href=\"https://shrinefox.com/guides/2020/04/10/modding-using-hostfs-on-pcsx2-p3-p4-smt3/\">this guide</a> to install the PNACH on PCSX2." +
                    $"<br><span style=\"font-size:8pt;\"><b>Note:</b> Rename the downloaded .pnach file to <a href=\"https://i.imgur.com/5b2yURr.png\">match your game's CRC</a>. By default, for this game it's {game.CRC}.</span>"
                });
            if (selectedPlatform == "PS3")
                selectedGamePanelFooter.Controls.Add(new LiteralControl() { Text = "Read <a href=\"https://shrinefox.com/guides/2019/04/19/persona-5-rpcs3-modding-guide-1-downloads-and-setup/\">this guide if you're using RPCS3</a>, " +
                    "or <a href=\"https://shrinefox.com/guides/2019/06/12/persona-5-ps3-eboot-patching/\">this one for PS3 CFW.</a>" +
                    "<br><span style=\"font-size:8pt;\"><b>Note:</b> It's okay if this Title ID doesn't match yours.</span>" });
            else if (selectedPlatform == "PS4")
                selectedGamePanelFooter.Controls.Add(new LiteralControl() { Text = "Follow <a href=\"https://shrinefox.com/guides/2020/09/30/modding-persona-5-royal-jp-on-ps4-fw-6-72/\">this guide</a> to learn about modding your PS4 and installing the modded update." +
                    "<span style=\"font-size:8pt;\"><b>Note:</b> Update PKG will only work if your base game was installed from a specific <b>Base Game FPKG</b>. " +
                    "Compare one of the following hashes to yours using <a href=\"https://hashtab.en.softonic.com/\">HashTab</a>. If it doesn't match, you'll have to download the <b>EBOOT</b> " +
                    "instead and <a href=\"https://shrinefox.com/guides/2021/12/28/manually-patching-ps4-persona-games/\">generate your Update PKG manually</a>." +
                    "<table class=\"table table-striped table-hover\"><tbody>" +
                    $"<tr><td><b>CRC32</b></td><td id=\"crc32\">{game.CRC32}</td></tr>" +
                    $"<tr><td><bMD5</b></td><td id=\"md5\">{game.MD5}</td></tr>" +
                    $"<tr><td><b>SHA-1</b></td><td id=\"sha1\">{game.SHA1}</td></tr></tbody></table></span>" });
            resultsContentPanel.Controls.Add(selectedGamePanel);
        }

        private void Download(string type)
        {
            // Get list of selected patches
            var selectedPatches = new List<string>();
            var patchesCheckboxList = Asp.GetControlByType<CheckBoxList>(MainPlaceHolder, c => c.ID == "chkBoxList_Patches");
            foreach (ListItem patch in patchesCheckboxList.Items)
                if (patch.Selected)
                    selectedPatches.Add(patch.Value);

            if (selectedPlatform == "PS4" || selectedPlatform == "PSV")
                DownloadPS4PSV(selectedPatches, type);
            else if (selectedPlatform == "PS2" && type == "PNACH")
                DownloadPNACH(selectedPatches);
        }

        private void DownloadPNACH(List<string> selectedPatches)
        {
            Platform platform = Platforms.Single(x => x.ShortName.Equals(selectedPlatform));
            Game game = platform.Games.Single(x => x.Region.Equals(selectedRegion) && x.ShortName.Equals(selectedGame));

            StringBuilder sb = new StringBuilder();
            // Add enabled patches to text file
            sb.Append("// PNACH generated at https://shrinefox.com\n");
            foreach (GamePatch patch in game.Patches.Where(x => selectedPatches.Any(y => y.Equals(x.ShortName))))
                sb.Append($"\n// Title: {patch.Name}" +
                    $"\n// Author: {patch.Author}" +
                    $"\n// Game: {game.Name} ({game.Region})" +
                    $"\n// Notes: {patch.Description}" +
                    $"\n{patch.Text}\n");

            // Download file if it's not empty
            string text = sb.ToString();
            if (!string.IsNullOrEmpty(text))
            {
                Response.Clear();
                Response.ClearHeaders();
                Response.AppendHeader("Content-Length", text.Length.ToString());
                Response.ContentType = "text/plain";
                Response.AppendHeader("Content-Disposition", $"attachment;filename=\"{game.CRC}.pnach\"");
                Response.Write(text);
                Response.End();
            }
            else
                ErrorMsg($"Could not find a download for:<br>{selectedGame} {selectedPlatform} {selectedRegion}\t{String.Join("+", selectedPatches)}");

        }

        private void DownloadPS4PSV(List<string> selectedPatches, string type)
        {
            // Find download with selected patches
            bool foundDownload = false;
            foreach (string line in File.ReadAllLines($"{System.Web.Hosting.HostingEnvironment.MapPath("~/.")}//App_Data//patch_downloads.tsv"))
            {
                var splitLine = line.Split('\t');
                if (splitLine.Length > 3
                    && splitLine[0] == $"{selectedGame} {selectedPlatform} {selectedRegion}"
                    && splitLine[2] == type)
                {
                    var mods = splitLine[1].Split('+');
                    if (mods.All(x => selectedPatches.Contains(x)))
                    {
                        // Redirect browser to download URL
                        string downloadUrl = splitLine[3];
                        Response.Redirect(downloadUrl);
                        foundDownload = true;
                        break;
                    }
                }
            }
            if (!foundDownload)
                ErrorMsg($"Could not find a download for:<br>{selectedGame} {selectedPlatform} {selectedRegion}\t{String.Join("+", selectedPatches)}\t{type}");

        }
    }
}