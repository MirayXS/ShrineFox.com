using Humanizer;
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
                             Games = Games.PS2Games,
                             EmulatorName = "PCSX2"
            },
            new Platform() { Name = "PlayStation 3",
                             ShortName = "PS3",
                             Games = Games.PS3Games,
                             EmulatorName = "RPCS3"
            },
            new Platform() { Name = "PlayStation 4", 
                             ShortName = "PS4", 
                             Games = Games.PS4Games
            },
            new Platform() { Name = "PlayStation Vita", 
                             ShortName = "PSV",
                             Games = Games.PSVGames,
                             EmulatorName = "Vita3K"
            },
            new Platform() { Name = "PlayStation Portable",
                             ShortName = "PSP",
                             Games = Games.PSPGames,
                             EmulatorName = "PPSSPP"
            },
            new Platform() { Name = "Nintendo 3DS",
                             ShortName = "3DS",
                             Games = Games.PQGames,
                             EmulatorName = "Citra"
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
        public static string selectedTarget = "";

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
                    if (query.Item1.ToLower() == "target")
                        selectedTarget = query.Item2.ToUpper();
                }
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            // Update options if user has selected something
            if (IsPostBack || (selectedGame != "" || selectedPlatform != "" || selectedRegion != ""))
                UpdateDynamicControls();
            UpdateNavigation();
        }

        private void UpdateNavigation()
        {
            lastUpdated.Controls.Clear();
            lastUpdated.Controls.Add(new LiteralControl()
            {
                Text = "Before you can install mods from <a href=\"https://shrinefox.com/browse\">ShrineFox.com/Browse</a>, " +
                        "you must patch your game." +
                        "<br>The fan-made patch for loading modded files is called <b>Mod Support</b>." +
                        "<br><br>This page will walk you through the setup process." +
                        $"<br>{Html.Notice("yellow", "This section is still under construction, so not everything works as expected.<br><br>")}"
            });
            // Show last updated time for P5 EX
            var lastWriteTime = File.GetCreationTime(System.Web.Hosting.HostingEnvironment.MapPath("~/yml/p5_ex/patches/patch.yml"));
            lastUpdated.Controls.Add(new LiteralControl { Text = $"<i class=\"fas fa-history\" aria-hidden=\"true\"></i> Updated {lastWriteTime.Humanize()}" });
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
                "Game Platform", "Choose which console or device the game was released on.",
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
                "Game Title", "Choose which title to add Mod Support to.",
                Asp.DropDownList("gameList", gamePairs),
                false
                )
            );

            // Region panel and options (hidden by default)
            MainPlaceHolder.Controls.Add(
                Asp.DropDownPanel("pnl_Region",
                "Game Region", "Choose a supported region that matches your game.",
                Asp.DropDownList("regionList", new List<Tuple<string, string>>() {
                    new Tuple<string,string>("USA","USA"),
                    new Tuple<string, string>("EUR", "EUR")
                }),
                false
                )
            );

            // Emulated panel and options (hidden by default)
            MainPlaceHolder.Controls.Add(
                Asp.DropDownPanel("pnl_Emulator",
                "Target Platform", "Choose whether you're emulating the game on PC or playing it on a console.",
                Asp.DropDownList("emuList", new List<Tuple<string, string>>() { 
                    new Tuple<string,string>("Emulator", "emulator"), 
                    new Tuple<string, string>("Console", "console"), 
                }),
                false
                )
            );

            // PPU Panel (hidden by default)
            MainPlaceHolder.Controls.Add(
                Asp.TextBoxPanel("pnl_PPU",
                "Enter PPU Hash", "Required for RPCS3. Launch the game and search for \"PPU executable hash\" in rpcs3.log.",
                false, "PPU-b8c34f774adb367761706a7f685d4f8d9d355426"
                )
            );

            // Results
            MainPlaceHolder.Controls.Add(new Panel() { ID = "pnl_Results" });

            // Panel for error messages
            MainPlaceHolder.Controls.Add(new Panel() { ID = "pnl_Error" });

            // Patches panel and options (hidden by default)
            MainPlaceHolder.Controls.Add(
                Asp.SplitCardPanel("pnl_Patches",
                "Patches", $"Mouse over each patch to view the description.",
                false
                )
            );

            var patchesPanelContentLeft = Asp.GetControlByType<Panel>(MainPlaceHolder, c => c.ID == "pnl_Patches_content_left");
            LinkButton linkBtn_ChkAll = new LinkButton() { ID = "chkAll", Text = "Check All" };
            LinkButton linkBtn_UnchkAll = new LinkButton() { ID = "unchkAll", Text = "Uncheck All" };
            CheckBoxList chkBoxList_Patches = new CheckBoxList() { ID = "chkBoxList_Patches" };
            patchesPanelContentLeft.Controls.Add(linkBtn_ChkAll);
            patchesPanelContentLeft.Controls.Add(new LiteralControl() { Text = " | " });
            patchesPanelContentLeft.Controls.Add(linkBtn_UnchkAll);
            patchesPanelContentLeft.Controls.Add(chkBoxList_Patches);
        }

        private void UpdateDynamicControls()
        {
            // Get all dynamically generated controls
            var platformList = Asp.GetControlByType<DropDownList>(MainPlaceHolder, c => c.ID == "platformList");
            var gamePanel = Asp.GetControlByType<Panel>(MainPlaceHolder, c => c.ID == "pnl_Game");
            var gameList = Asp.GetControlByType<DropDownList>(MainPlaceHolder, c => c.ID == "gameList");
            var regionPanel = Asp.GetControlByType<Panel>(MainPlaceHolder, c => c.ID == "pnl_Region");
            var emuPanel = Asp.GetControlByType<Panel>(MainPlaceHolder, c => c.ID == "pnl_Emulator");
            var emuList = Asp.GetControlByType<DropDownList>(MainPlaceHolder, c => c.ID == "emuList");
            var regionList = Asp.GetControlByType<DropDownList>(MainPlaceHolder, c => c.ID == "regionList");
            var ppuPanel = Asp.GetControlByType<Panel>(MainPlaceHolder, c => c.ID == "pnl_PPU");
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
                    if (selectedPlatform != "PC")
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
                    if (!string.IsNullOrEmpty(selectedRegion) || selectedPlatform == "PC")
                    {
                        patchesPanel.Visible = true;

                        if (selectedPlatform == "PS3")
                            ppuPanel.Visible = true;

                        // If platform has an emulator, allow user to choose between that and console
                        if (platform.EmulatorName != "")
                        {
                            emuPanel.Visible = true;

                            // Get previously selected target platform
                            if (!string.IsNullOrEmpty(emuList.SelectedValue))
                                selectedTarget = emuList.SelectedValue;
                            else
                                selectedTarget = "emulator";
                            // Re-select previously selected target in dropdown
                            if (emuList.Items.FindByValue(selectedTarget) != null)
                                emuList.SelectedIndex = emuList.Items.IndexOf(emuList.Items.FindByValue(selectedTarget));
                            else
                                selectedTarget = "";
                            // Update name of emulator based on selected platform
                            if (selectedTarget != "console")
                                emuList.Items.FindByValue("emulator").Text = platform.EmulatorName;
                        }

                        var Game = selectedGames.Single(x => x.Region.Equals(selectedRegion));
                        // Update Patches based on all prior selections
                        UpdatePatches(Game);
                        // Update Results
                        UpdateResults(Game);
                    }
                    else
                    {
                        patchesPanel.Visible = false;
                        ppuPanel.Visible = false;
                    }
                }
                else
                {
                    regionPanel.Visible = false;
                    patchesPanel.Visible = false;
                    ppuPanel.Visible = false;
                }
            }
            else
            {
                gamePanel.Visible = false;
                regionPanel.Visible = false;
                patchesPanel.Visible = false;
                ppuPanel.Visible = false;
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
                item.Attributes.Add("onmouseover", $"document.getElementById('desc').innerHTML=\"<b>{patch.Name}</b> (v{patch.Version}) by {patch.Author}<br><br>{patch.Description}\";");

                // Add checkbox for each patch
                items.Add(item);
            }
            patchesCheckboxList.Items.Clear();
            patchesCheckboxList.Items.AddRange(items.ToArray());

            UpdateDownloadButtons();

            if (items.Count == 0)
                patchesContentPanelLeft.Controls.Add(new LiteralControl { Text = "<br><br>There are no patches for this title!<br>Read the instructions above for more information." });

            patchesContentPanelFooter.Controls.Add(new LiteralControl() { Text = "<br><br><i class=\"fa-solid fa-thumbs-up\"></i> Once you have set up mod support, you're ready to <a href=\"https://shrinefox.com/guides/2021/06/21/when-to-use-aemulus-or-mod-compendium/\">choose a Mod Manager!</a>" });
        }

        private void UpdateDownloadButtons()
        {
            var patchesContentPanelFooter = Asp.GetControlByType<Panel>(MainPlaceHolder, c => c.ID == "pnl_Patches_footer");

            // Add download buttons
            if (selectedPlatform == "PS4")
                patchesContentPanelFooter.Controls.Add(new Button() { ID = "PKG", CssClass = "btn btn-primary", Text = "Download Update PKG", OnClientClick = "target ='_blank';" });
            if (selectedPlatform == "PS4" || selectedPlatform == "PSV" || selectedPlatform == "PSP")
            {
                patchesContentPanelFooter.Controls.Add(new LiteralControl() { Text = " " });
                patchesContentPanelFooter.Controls.Add(new Button() { ID = "EBOOT", CssClass = "btn btn-primary", Text = "Download EBOOT", OnClientClick = "target ='_blank';" });
            }
            if (selectedPlatform == "PS3")
            {
                if (selectedTarget != "console")
                    patchesContentPanelFooter.Controls.Add(new Button() { ID = "YML", CssClass = "btn btn-primary", Text = "Download .YML", OnClientClick = "target ='_blank';" });
                else
                    patchesContentPanelFooter.Controls.Add(new Button() { ID = "YML_Old", CssClass = "btn btn-secondary", Text = "Download .YML (Old Format)", OnClientClick = "target ='_blank';" });
            }
            if (selectedPlatform == "PS2")
                patchesContentPanelFooter.Controls.Add(new Button() { ID = "PNACH", CssClass = "btn btn-primary", Text = "Download PNACH", OnClientClick = "target ='_blank';" });
            if (selectedPlatform == "3DS" || selectedPlatform == "PSP" || selectedPlatform == "PC")
                patchesContentPanelFooter.Controls.Add(new Button() { ID = "7Z", CssClass = "btn btn-primary", Text = "Download .7z", OnClientClick = "target ='_blank';" });

            // Initiate download if button clicked
            string postBackId = AspExtension.GetPostBackControlId(Page);
            if (!string.IsNullOrEmpty(postBackId))
            {
                if (postBackId == "PKG" || postBackId == "EBOOT" || postBackId == "PNACH" ||
                postBackId == "YML" || postBackId == "YML_Old" || postBackId == "7Z")
                    Download(postBackId);
                else if (postBackId == "chkAll")
                    CheckAll();
                else if (postBackId == "unchkAll")
                    UncheckAll();
            }

        }

        private void UpdateResults(Game game)
        {
            var resultsContentPanel = Asp.GetControlByType<Panel>(MainPlaceHolder, c => c.ID == "pnl_Results");

            // Show game boxart with link to wiki page and other resources
            Panel selectedGamePanel = Asp.SplitCardPanel("pnl_SelectedGame", $"TITLE ID: {game.TitleID}", $"{selectedGame} {selectedRegion} ({selectedPlatform})", true);
            var selectedGamePanelLeft = Asp.GetControlByType<Panel>(selectedGamePanel, c => c.ID == "pnl_SelectedGame_content_left");
            var selectedGamePanelRight = Asp.GetControlByType<Panel>(selectedGamePanel, c => c.ID == "pnl_SelectedGame_content_right");
            var selectedGamePanelFooter = Asp.GetControlByType<Panel>(selectedGamePanel, c => c.ID == "pnl_SelectedGame_footer");
            selectedGamePanelLeft.Controls.Add(new LiteralControl() { Text = $"<img src=\"{game.ImageUrl}\" style=\"max-height:150px;\" class=\"img-responsive img-fit-contain\">" });
            
            string gameShortName = selectedGame.Replace("EX","");
            string gameName = game.Name.Replace(" EX", "");
            selectedGamePanelRight.Controls.Add(new LiteralControl()
            {
                Text = "<b>Need help installing these patches?</b> " +
                $"<br><a target='_blank' href=\"https://amicitia.miraheze.org/wiki/{gameName}\">Learn about {gameShortName} modding</a> on the Wiki!" +
                $"<br><br><span style=\"font-size:8pt;\">While you're here, see also:<br><ul>" +
                $"<li><a target='_blank' href=\"https://shrinefox.com/browse?game={gameName}&type=mod\">{gameShortName} Mods</a></li>" +
                $"<li><a target='_blank' href=\"https://shrinefox.com/browse?game={gameName}&type=tool\">{gameShortName} Modding Tools</a></li>" +
                $"<li><a target='_blank' href=\"https://shrinefox.com/browse?game={gameName}&type=guide\">{gameShortName} Modding Guides</a></li>" +
                "</ul></span>"
            });
            if (selectedGame == "P5")
            {
                selectedGamePanelRight.Controls.Add(new LiteralControl()
                {
                    Text = "Consider using DeathChaos25's <a target='_blank' href=\"https://shrinefox.com/guides/2022/01/26/setting-up-persona-5-ex/\">Persona 5 EX mod</a>!" +
                    "<br>Select <b>Persona 5 EX</b> in the \"Game Title\" section above."
                });
            }

            UpdateInstallInstructions(game, selectedGamePanelFooter);

            resultsContentPanel.Controls.Add(selectedGamePanel);
        }

        private void UpdateInstallInstructions(Game game, Panel selectedGamePanelFooter)
        {
            // Show install instructions
            selectedGamePanelFooter.Controls.Add(new LiteralControl()
            {
                Text = "<b>Instructions:</b><br><ol><li>Choose which patches you'd like to include.</li><li>Click the button at the bottom of the page to download the file.</li>"
            });
            if (selectedPlatform == "PS2")
                selectedGamePanelFooter.Controls.Add(new LiteralControl()
                {
                    Text = "<li>Read <a target='_blank' href=\"https://shrinefox.com/guides/2020/04/10/modding-using-hostfs-on-pcsx2-p3-p4-smt3/\">this guide</a> " +
                    "to install the PNACH file on PCSX2, or <a target='_blank' href=\"https://shrinefox.com/guides/2020/03/29/loading-modded-files-in-persona-3-4-ps2/\">this one</a> to apply modded files to an ISO (for other platforms).</li>" +
                    $"<span style=\"font-size:8pt;\"><b>Note:</b> Rename the downloaded .pnach file to <a target='_blank' href=\"https://i.imgur.com/5b2yURr.png\">match your game's CRC</a>. By default, for this game it's {game.CRC}.</span>"
                });
            if (selectedPlatform == "3DS" || selectedPlatform == "PSP" || selectedPlatform == "PC")
                selectedGamePanelFooter.Controls.Add(new LiteralControl() { Text = "<li>Use <a target='_blank' href=\"https://www.7-zip.org/\">7zip</a> to extract the .7Z file.</li>" });
            if (selectedPlatform == "3DS")
                selectedGamePanelFooter.Controls.Add(new LiteralControl() { Text = "<li>Read <a target='_blank' href=\"https://shrinefox.com/guides/2021/08/15/using-pq2-patches/\">this guide</a> to apply the patch to the game.</li>" });
            if (selectedPlatform == "PS3")
            {
                if (selectedTarget != "" && selectedTarget != "console")
                    {
                    selectedGamePanelFooter.Controls.Add(new LiteralControl()
                    {
                        Text = "<li>Read <a target='_blank' href=\"https://shrinefox.com/guides/2019/04/19/persona-5-rpcs3-modding-guide-1-downloads-and-setup/\">this guide to install the YML file on RPCS3</a>."
                    });
                }
                else
                {
                    selectedGamePanelFooter.Controls.Add(new LiteralControl()
                    {
                        Text = "<li>Follow <a target='_blank' href=\"https://shrinefox.com/guides/2019/06/12/persona-5-ps3-eboot-patching/\">this guide to learn how to install mods with PS3 Custom Firmware.</a></li>"
                    });
                }
                selectedGamePanelFooter.Controls.Add(new LiteralControl()
                {
                    Text = "<br><span style=\"font-size:8pt;\"><b>Note:</b> It's okay if this Title ID doesn't match yours, as long as your region is EUR or USA.</span>"
                });
            }
            if (selectedPlatform == "PS4")
                selectedGamePanelFooter.Controls.Add(new LiteralControl()
                {
                    Text = "<li>Follow <a target='_blank' href=\"https://shrinefox.com/guides/2020/09/30/modding-persona-5-royal-jp-on-ps4-fw-6-72/\">this guide</a> " +
                    "to learn about modding your PS4 and installing the modded update.</li>" +
                    "<span style=\"font-size:8pt;\"><b>Note:</b> Update PKG will only work if your base game was installed from a specific <b>Base Game FPKG</b>. " +
                    "<br><br>Compare one of the following hashes to yours using <a target='_blank' href=\"https://hashtab.en.softonic.com/\">HashTab</a>. " +
                    "<br>If it doesn't match, you'll have to download the <b>EBOOT</b> " +
                    "instead and <a target='_blank' href=\"https://shrinefox.com/guides/2021/12/28/manually-patching-ps4-persona-games/\">generate your Update PKG manually</a>." +
                    "<table class=\"table table-striped table-hover\"><tbody>" +
                    $"<tr><td><b>CRC32</b></td><td id=\"crc32\">{game.CRC32}</td></tr>" +
                    $"<tr><td><b>MD5</b></td><td id=\"md5\">{game.MD5}</td></tr>" +
                    $"<tr><td><b>SHA-1</b></td><td id=\"sha1\">{game.SHA1}</td></tr></tbody></table></span>"
                });
            if (selectedGame == "P5EX")
            {
                selectedGamePanelFooter.Controls.Add(new LiteralControl()
                {
                    Text = "<hr><li><b>IMPORTANT:</b> Follow <a target='_blank' href=\"https://shrinefox.com/guides/2022/01/26/setting-up-persona-5-ex/\">this guide</a> to set up the P5 EX mod by DeathChaos25 on RPCS3." +
                    $"<br><span style=\"font-size:8pt;\">The following patches aren't included because they are reimplemented as part of the P5EX mod: {string.Join(",", Games.disabledEXPatches)}</span></li>"
                });
                if (selectedTarget != "console")
                {
                    selectedGamePanelFooter.Controls.Add(new LiteralControl()
                    {
                        Text = "</ul><br><br><b>TLDR Install Instructions:</b><ul>" +
                        "<li>Generate the <b>patch.yml</b> from this page and move it to your RPCS3/patches folder. Go to <b>Manage > Game Patches</b> and enable Mod SPRX and P5EX patches.</li>" +
                        "<li>Right click your game in the game list in RPCS3, select Open Install Folder, this will bring you inside the game's USRDIR folder.</li>" +
                        "<li>Download <a target='_blank' href=\"https://mega.nz/file/F5cGhDjC#DpMaU3iCfXeAF0NqbEU9p6aPkg1rTFYzCpZpE1rCjhc\">BGM cpk</a></li>" +
                        "<li>Download <a target='_blank' href=\"https://mega.nz/file/1p8wRCpa#-Ivf-55b2hU_3Y5ZTymi75C7tACExIskjxqZPIBxlE8\">P5R Bustups cpk</a></li>" +
                        "<li>Download <a target='_blank' href =\"https://shrinefox.com//yml//p5_ex//USRDIR//config.yml\">config.yml</a> " +
                            "and <a target='_blank' href=\"https://shrinefox.com////yml//p5_ex//USRDIR//mod.sprx\">mod.sprx</a></li>" +
                        "<li>Move those two files to the USRDIR folder in game's install folder</li>" +
                        "<li>Also move downloaded CPK files to the USRDIR folder</li>" +
                        "<li>Download <a target='_blank' href=\"https://drive.google.com/file/d/1VzLwyBq5d6WcJzMz1a_NEEC5-0fCbIRL/view\">this mod (P5EX)</a> " +
                        "with <a target='_blank' href=\"https://gamebanana.com/dl/767116\">this mod (softlock fix)</a>, build mod.cpk with both selected in Aemulus and enjoy!"
                    });
                }
                else
                {
                    selectedGamePanelFooter.Controls.Add(new LiteralControl()
                    {
                        Text = "</ul><br><br><b>TLDR Install Instructions:</b><ul>" +
                        "<li>Connect your console to the same network as your PC. Use homebrew like <a target='_blank' href=\"http://www.enstoneworld.com/articles\">CCAPI</a> or <a href=\"https://github.com/aldostools/webMAN-MOD/releases\">WebMAN MOD</a> to enable FTP (File Transfer Protocol).</li>" +
                        "<li>Using a program like <a target='_blank' href=\"https://filezilla-project.org/download.php?platform=win64\">FileZilla</a>, connect to your PS3's IP and navigate to the game's Install Folder (something like dev_hdd0\\game\\NPEB02436\\USDIR).</li>" +
                        "<li>Transfer your <b>eboot.bin</b> to PC and <a target='_blank' href=\"https://shrinefox.com/guides/2019/06/12/persona-5-ps3-eboot-patching/\">patch it with the <b>patch.yml</b></a> generated by this page, then transfer it back (overwrite).</li>" +
                        "<li>Download <a target='_blank' href=\"https://mega.nz/file/F5cGhDjC#DpMaU3iCfXeAF0NqbEU9p6aPkg1rTFYzCpZpE1rCjhc\">BGM cpk</a></li>" +
                        "<li>Download <a target='_blank' href=\"https://mega.nz/file/1p8wRCpa#-Ivf-55b2hU_3Y5ZTymi75C7tACExIskjxqZPIBxlE8\">P5R Bustups cpk</a></li>" +
                        "<li>Move the downloaded CPK files to the USRDIR folder in game's install folder</li>" +
                        "<li>Download <a target='_blank' href =\"https://shrinefox.com////yml//p5_ex//hardware//p5ex//conf.yml\">conf.yml</a> " +
                            "and <a target='_blank' href=\"https://shrinefox.com////yml//p5_ex//hardware//p5ex//mod.sprx\">mod.sprx</a></li>" +
                        "<li>Move those two files to the root of your PS3's dev_hdd0 folder</li>" +
                        "<li>Download <a target='_blank' href=\"https://drive.google.com/file/d/1VzLwyBq5d6WcJzMz1a_NEEC5-0fCbIRL/view\">this mod (P5EX)</a> " +
                        "with <a target='_blank' href=\"https://gamebanana.com/dl/767116\">this mod (softlock fix)</a>, build mod.cpk with both selected in Aemulus and transfer output to USRDIR folder on PS3."
                    });
                }
            }
            if (selectedGame == "P4AU" && selectedPlatform == "PC")
                selectedGamePanelFooter.Controls.Add(new LiteralControl()
                {
                    Text = "<li><b>IMPORTANT:</b> Follow <a target='_blank' href=\"https://gamebanana.com/mods/376984\">this guide</a> to set up mod support.</li>"
                });
            if (selectedGame == "P4G" && selectedPlatform == "PC")
                selectedGamePanelFooter.Controls.Add(new LiteralControl()
                {
                    Text = "<li><b>IMPORTANT:</b> Follow <a target='_blank' href=\"https://gamebanana.com/tuts/13379\">this guide</a> to set up mod support.</li>"
                });
            if (selectedGame == "P5S" && selectedPlatform == "PC")
                selectedGamePanelFooter.Controls.Add(new LiteralControl()
                {
                    Text = "<li>Run Aemulus Package Manager and configure the settings to add mod support.</li>"
                });
            if (selectedGame == "P3P" && selectedPlatform == "PSP")
                selectedGamePanelFooter.Controls.Add(new LiteralControl()
                {
                    Text = "<li><b>IMPORTANT:</b> Follow <a target='_blank' href=\"https://shrinefox.com/news/?p=266\">this guide</a> to set up mod support.</li>"
                });
        }

        private void Download(string type)
        {
            // Get list of selected patches
            var selectedPatches = new List<string>();
            var patchesCheckboxList = Asp.GetControlByType<CheckBoxList>(MainPlaceHolder, c => c.ID == "chkBoxList_Patches");
            foreach (ListItem patch in patchesCheckboxList.Items)
                if (patch.Selected)
                    selectedPatches.Add(patch.Value);

            if (!CheckForConflicts())
            {
                if (selectedPlatform == "PS2" && type == "PNACH")
                    DownloadPNACH(selectedPatches);
                else if (selectedPlatform == "PS3" && type == "YML")
                    DownloadYML(selectedPatches);
                else if (selectedPlatform == "PS3" && type == "YML_Old")
                    DownloadYMLOld(selectedPatches);
                else if (selectedPlatform == "PS4" || selectedPlatform == "PSV")
                    DownloadPS4PSV(selectedPatches, type);
                else
                    Download7Z(selectedPatches);
            }
        }

        private bool CheckForConflicts()
        {
            var patchesCheckboxList = Asp.GetControlByType<CheckBoxList>(MainPlaceHolder, c => c.ID == "chkBoxList_Patches");
            Platform platform = Platforms.Single(x => x.ShortName.Equals(selectedPlatform));
            Game game = platform.Games.Single(x => x.Region.Equals(selectedRegion) && x.ShortName.Equals(selectedGame));
            
            // Deselect list item if it conflicts with another selected item 
            foreach (ListItem listItem in patchesCheckboxList.Items)
            {
                if (listItem.Selected)
                {
                    foreach (GamePatch conflictingPatch in game.Patches.Where(x => x.Conflicts.Any(y => y.Equals(listItem.Value))))
                    {
                        var conflict = patchesCheckboxList.Items.FindByValue(conflictingPatch.ShortName);
                        if (conflict != null && conflict.Selected)
                        {
                            conflict.Selected = false;
                            ErrorMsg($"The patch \"{conflict.Text}\" has been deselected because it conflicts with \"{listItem.Text}\".");
                        }
                    }
                }
            }
            
            return false;
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

        private void DownloadYML(List<string> selectedPatches)
        {
            Platform platform = Platforms.Single(x => x.ShortName.Equals(selectedPlatform));
            Game game = platform.Games.Single(x => x.Region.Equals(selectedRegion) && x.ShortName.Equals(selectedGame));
            StringBuilder sb = new StringBuilder();
            var ppuBox = Asp.GetControlByType<TextBox>(MainPlaceHolder, c => c.ID == "pnl_PPU_txt");
            string ppuHash = ppuBox.Text;

            // Add enabled patches to text file
            sb.Append("Version: 1.2" +
                "\n# Patch.yml generated at https://shrinefox.com");
            foreach (GamePatch patch in game.Patches.Where(x => selectedPatches.Any(y => y.Equals(x.ShortName))))
                sb.Append($"\n\n{ppuHash}:" +
                    $"\n  {patch.Name}:" +
                    $"\n    Games:" +
                    $"\n      \"Persona 5\":" +
                    $"\n        BLES02247: [ All ]" +
                    $"\n        NPEB02436: [ All ]" +
                    $"\n        BLUS31604: [ All ]" +
                    $"\n        NPUB31848: [ All ]" +
                    $"\n    Author: {patch.Author}" +
                    $"\n    Notes: {patch.Description}" +
                    $"\n    Patch Version: {patch.Version}" +
                    $"\n    Patch:" +
                    $"\n    {patch.Text.Replace("\n  ", "\n      ")}");

            // Download file if it's not empty
            string text = sb.ToString();
            if (!string.IsNullOrEmpty(text))
            {
                Response.Clear();
                Response.ClearHeaders();
                Response.AppendHeader("Content-Length", text.Length.ToString());
                Response.ContentType = "text/plain";
                Response.AppendHeader("Content-Disposition", "attachment;filename=\"patch.yml\"");
                Response.Write(text);
                Response.End();
            }
        }

        private void DownloadYMLOld(List<string> selectedPatches)
        {
            Platform platform = Platforms.Single(x => x.ShortName.Equals(selectedPlatform));
            Game game = platform.Games.Single(x => x.Region.Equals(selectedRegion) && x.ShortName.Equals(selectedGame));
            StringBuilder sb = new StringBuilder();
            var ppuBox = Asp.GetControlByType<TextBox>(MainPlaceHolder, c => c.ID == "pnl_PPU_txt");
            string ppuHash = ppuBox.Text;

            // Add enabled patches to text file
            sb.Append("# Patch.yml generated at https://shrinefox.com");
            foreach (GamePatch patch in game.Patches.Where(x => selectedPatches.Any(y => y.Equals(x.ShortName))))
            {
                string patchID = "p5_" + patch.Name.ToLower().Replace(" ", "_");
                sb.Append($"# {patch.Name} v{patch.Version} by {patch.Author}" +
                    $"\n# {patch.Description}" +
                    $"\n{patchID}: &{patchID}");

                // Update file path to SPRX for hardware
                if (patch.Name.Equals("Mod SPRX") && (selectedTarget == "" || selectedTarget == "console"))
                    sb.Append($"\n{patch.Text.Replace("- [ be32, 0xa3bed0, 0x2f617070 ]", "- [ be32, 0xa3bed0, 0x2F646576 ]").Replace("- [ be32, 0xa3bed4, 0x5f686f6d ]", "- [ be32, 0xa3bed4, 0x5F686464 ]").Replace("- [ be32, 0xa3bed8, 0x652f6d6f ]", "- [ be32, 0xa3bed8, 0x302F7035 ]").Replace("- [ be32, 0xa3bedc, 0x642e7370 ]", "- [ be32, 0xa3bedc, 0x65782F6D ]").Replace("- [ be32, 0xa3bee0, 0x72780000 ]", "- [ be32, 0xa3bee0, 0x6F642E73 ]").Replace("- [ be32, 0xa3bee4, 0x0 ]", "- [ be32, 0xa3bee4, 0x70727800 ]")}");
                else
                    sb.Append($"\n{patch.Text}");
            }
            sb.Append($"{ppuHash}:");
            foreach (GamePatch patch in game.Patches.Where(x => selectedPatches.Any(y => y.Equals(x.ShortName))))
            {
                string patchID = "p5_" + patch.Name.ToLower().Replace(" ", "_");
                sb.Append($"\n- [ load, {patchID} ]");
            }

            // Download file if it's not empty
            string text = sb.ToString();
            if (!string.IsNullOrEmpty(text))
            {
                Response.Clear();
                Response.ClearHeaders();
                Response.AppendHeader("Content-Length", text.Length.ToString());
                Response.ContentType = "text/plain";
                Response.AppendHeader("Content-Disposition", "attachment;filename=\"patch.yml\"");
                Response.Write(text);
                Response.End();
            }
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
                    if (Enumerable.SequenceEqual(selectedPatches.OrderBy(e => e), mods.OrderBy(e => e)))
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

        private void Download7Z(List<string> selectedPatches)
        {
            string url = "";
            if (selectedGame == "PQ" || selectedGame == "PQ2")
            {
                if (selectedPatches.Any(x => x.Equals("canonNames")))
                    url = "https://github.com/DeathChaos25/PQ_Patches/releases/download/v1.0.2/PQ_Patches-v1.0.2.7z";
                else
                    url = "https://github.com/DeathChaos25/PQ_Patches/releases/download/v1.0.2/PQ_Patches-v1.0.2-ModCpk.7z";
            }
            else if (selectedGame == "P3P")
                url = "https://github.com/zarroboogs/p3p-patches/archive/refs/heads/master.zip";
            else if (selectedGame == "P4AU")
                url = "https://gamebanana.com/dl/797848";
            else if (selectedGame == "P4G")
                url = "https://github.com/Reloaded-Project/Reloaded-II/releases/download/1.19.2/Release.zip";
            if (selectedGame == "P5S")
                url = "https://gamebanana.com/tools/6878";

            Response.Redirect(url);
        }

        public void ErrorMsg(string msg)
        {
            var errorPanel = Asp.GetControlByType<Panel>(MainPlaceHolder, c => c.ID == "pnl_Error");
            errorPanel.Controls.Clear();
            errorPanel.Controls.Add(new LiteralControl() { Text = $"{Html.Notice("red", msg)}" });
        }

        private void CheckAll()
        {
            var patchesCheckboxList = Asp.GetControlByType<CheckBoxList>(MainPlaceHolder, c => c.ID == "chkBoxList_Patches");
            Platform platform = Platforms.Single(x => x.ShortName.Equals(selectedPlatform));
            Game game = platform.Games.Single(x => x.Region.Equals(selectedRegion) && x.ShortName.Equals(selectedGame));

            foreach (ListItem patch in patchesCheckboxList.Items)
                patch.Selected = true;
        }

        private void UncheckAll()
        {
            var patchesCheckboxList = Asp.GetControlByType<CheckBoxList>(MainPlaceHolder, c => c.ID == "chkBoxList_Patches");
            Platform platform = Platforms.Single(x => x.ShortName.Equals(selectedPlatform));
            Game game = platform.Games.Single(x => x.Region.Equals(selectedRegion) && x.ShortName.Equals(selectedGame));

            foreach (ListItem listItem in patchesCheckboxList.Items)
                if (listItem.Selected && !game.Patches.Single(x => x.ShortName.Equals(listItem.Value)).AlwaysOn)
                        listItem.Selected = false;
        }
    }
}