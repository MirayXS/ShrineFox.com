using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ShrineFox.com
{
    public partial class UpdateCreator : Page
    {
        CheckBoxList cbList = new CheckBoxList();
        public static bool regionIsUSA = true;
        public static bool downloadIsPKG = true;
        public static string selectedGame = "p5r";
        public static string selectedPatch = "mod_support";
        public static string selectedRegion = "usa";

        public static List<Game> Games = new List<Game>()
        {
            new Game() { Name = "Persona 5 Royal", ID = "p5r", TitleID = "CUSA17416", Region = "usa",
                CRC32 = "E2452B1C", MD5 = "E669D7F9F9AB3989A2ED9D8D615547BD", SHA1 = "25ABE8EFBD0D0CB7307927CD6AE6F1BB5ED506F4" },
            new Game() { Name = "Persona 5 Royal", ID = "p5r", TitleID = "CUSA17419", Region = "eur",
                CRC32 = "8221F3EE", MD5 = "9FA6741E1EC98F0DF6027DB553168B45", SHA1 = "98DE4F768453F32FFB17C57C2480518F80999EF8" },
            new Game() { Name = "Persona 3 Dancing", ID = "p3d", TitleID = "CUSA12636", Region = "usa",
                CRC32 = "FD46F56F", MD5 = "73482E870BEB91F5AD8BA0AEC4515F68", SHA1 = "A4889A43400DC4FD333B9B9C109816B47777C339" },
            new Game() { Name = "Persona 4 Dancing", ID = "p4d", TitleID = "CUSA12811", Region = "eur",
                CRC32 = "9B2CDA6E", MD5 = "7794E4C921AFB7B759F481C564AFF1CB", SHA1 = "176AF1377B97217E2E02DCC855E64FB730B80960" },
            new Game() { Name = "Persona 5 Dancing", ID = "p5d", TitleID = "CUSA12380", Region = "usa",
                CRC32 = "50A4FB33", MD5 = "D4E08AE0C5C7027B4FC01E55CDD23EF6", SHA1 = "521C247ADB8AA675C370257D53D60B77812E343A" }
        };

        public static List<Patch> P5RPatches = new List<Patch>()
        {
            new Patch() { ID = "mod_support", Name = "Mod Support", ShortDesc = "mod.cpk file replacement via FTP", Image = "https://66.media.tumblr.com/c3f99e21c7edb1df53e7f2fa02117621/tumblr_inline_pl680q6yWy1rp7sxh_500.gif",
                LongDesc = "Loads modded files from a <kbd>mod.cpk</kbd> file from <code>/data/p5r</code> on the PS4's internal memory via FTP." +
                "<br><b>Enabled by default.</b>", Enabled = true },
            new Patch() { ID = "_0505", Name = "5.05 Backport", ShortDesc = "Run on firmware 5.05+", Image = "https://i.postimg.cc/J44my5mT/505.png",
                LongDesc = "Allows the game to run on the lowest possible moddable PS4 firmware, and all those above it." +
                "<br><b>Enabled by default.</b>", Enabled = true },
            new Patch() { ID = "intro_skip", Name = "Intro Skip", ShortDesc = "Bypass opening logos/movie", Image = "https://i.postimg.cc/Jz4DKX80/p5rintro.png",
                LongDesc = "Skips boot logos and intro movie (can still be viewed in Thieves Den)." +
                "<br><b>Enabled by default.</b>", Enabled = true },
            new Patch() { ID = "all_dlc", Name = "Content Enabler", ShortDesc = "Enables on-disc content", Image = "https://i.postimg.cc/nrJMmjTH/p5rdlc.jpg",
                LongDesc = "<b>This will make saves created with this patch incompatible</b> with the game when the patch is disabled!" +
                "<br>Also hides DLC unlock messages when starting a new game."},
            new Patch() { ID = "no_trp", Name = "Disable Trophies", ShortDesc = "Prevents the game from unlocking trophies", Image = "https://i.postimg.cc/qMrChYZ8/notrophy.png" },
            new Patch() { ID = "square", Name = "Global Square Menu", ShortDesc = "Square button menu usable everywhere", Image = "https://i.postimg.cc/02Zr6NSs/square.png", 
                LongDesc = "Enables the square menu globally (e.g. in Thieves Den and in Velvet Room or during events or game sections which disable it)." +
                "<br><b>Enabled by default.</b>", Enabled = true },
            new Patch() { ID = "p5_save", Name = "P5 Save Bonus", ShortDesc = "Enables P5 save bonus without P5 saves present on system", 
                LongDesc = "><b>Enabled by default.</b>", Image = "https://i.postimg.cc/9MTrztd8/p5rsave.png", Enabled = true },
        };
        public static List<Patch> P3DPatches = new List<Patch>()
        {
            new Patch() { ID = "mod_support", Name = "Mod Support", ShortDesc = "mod.cpk file replacement via PKG or FTP", Image = "https://i.postimg.cc/GtxtxLSv/p3dmod.png",
                LongDesc = "Loads modded files from a <kbd>mod.cpk</kbd> file in the PKG's <code>USRDIR</code> directory," +
                           $"<br>or placed in <code>/data/p3d</code> on the PS4's internal memory via FTP." +
                            "<br>The latter takes priority." +
                            "<br><b>Enabled by default.</b>", Enabled = true },
            new Patch() { ID = "intro_skip", Name = "Intro Skip", ShortDesc = "Bypass opening logos/movie", Image = "https://i.postimg.cc/yNDJCzkz/p3dintro.png",
                LongDesc = "Skips boot logos and intro movie.<br><b>Enabled by default.</b>", Enabled = true },
            new Patch() { ID = "no_trp", Name = "Disable Trophies", ShortDesc = "Prevents the game from unlocking trophies", Image = "https://i.postimg.cc/qMrChYZ8/notrophy.png" },
            new Patch() { ID = "overlay", Name = "Disable Screenshot Overlay", ShortDesc = "Removes the annoying copyright overlay from in-game screenshots", 
                LongDesc = "<b>Enabled by default.</b>", Image = "https://i.postimg.cc/ZY8WSHK0/ps4.png", Enabled = true }
        };
        public static List<Patch> P4DPatches = new List<Patch>()
        {
            new Patch() { ID = "mod_support", Name = "Mod Support", ShortDesc = "mod.cpk file replacement via PKG or FTP", Image = "https://i.postimg.cc/7PSb1VFw/weedyosuke.gif",
                LongDesc = "Loads modded files from a <kbd>mod.cpk</kbd> file in the PKG's <code>USRDIR</code> directory," +
                           $"<br>or placed in <code>/data/p4d</code> on the PS4's internal memory via FTP." +
                            "<br>The latter takes priority.<br><b>Enabled by default.</b>", Enabled = true },
            new Patch() { ID = "intro_skip", Name = "Intro Skip", ShortDesc = "Bypass opening logos/movie", Image = "https://i.postimg.cc/527Hrt93/p4dintro.png",
                LongDesc = "Skips boot logos and intro movie.<br><b>Enabled by default.</b>", Enabled = true },
            new Patch() { ID = "no_trp", Name = "Disable Trophies", ShortDesc = "Prevents the game from unlocking trophies", Image = "https://i.postimg.cc/qMrChYZ8/notrophy.png" },
        };
        public static List<Patch> P5DPatches = new List<Patch>()
        {
            new Patch() { ID = "mod_support", Name = "Mod Support", ShortDesc = "mod.cpk file replacement via PKG or FTP", Image = "https://i.postimg.cc/TYc5r9ZB/Mod-Support.jpg",
                LongDesc = "Loads modded files from a <kbd>mod.cpk</kbd> file in the PKG's <code>USRDIR</code> directory," +
                           $"<br>or placed in <code>/data/p5d</code> on the PS4's internal memory via FTP." +
                            "<br>The latter takes priority.<br><b>Enabled by default.</b>", Enabled = true },
            new Patch() { ID = "intro_skip", Name = "Intro Skip", ShortDesc = "Bypass opening logos/movie", Image = "https://i.postimg.cc/W4xd2fkq/p5dintro.png",
                LongDesc = "Skips boot logos and intro movie.<br><b>Enabled by default.</b>", Enabled = true },
            new Patch() { ID = "no_trp", Name = "Disable Trophies", ShortDesc = "Prevents the game from unlocking trophies", Image = "https://i.postimg.cc/qMrChYZ8/notrophy.png" },
            new Patch() { ID = "overlay", Name = "Disable Screenshot Overlay", ShortDesc = "Removes the annoying copyright overlay from in-game screenshots",
                LongDesc = "<b>Enabled by default.</b>", Image = "https://i.postimg.cc/ZY8WSHK0/ps4.png", Enabled = true }
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            // Sidebar
            LiteralControl SidebarHtml = new LiteralControl();
            SidebarHtml.Text = ShrineFoxCom.Properties.Resources.IndexSidebar.Replace("<!--Accordions-->", ShrineFoxCom.Properties.Resources.Browse + ShrineFoxCom.Properties.Resources.Apps.Replace("ps4patchlink", "active"));
            Sidebar.Controls.Add(SidebarHtml);
            if (Page.IsPostBack)
            {

            }
        }

        protected void GameTab_Click(object sender, EventArgs e)
        {
            LinkButton clickedButton = (LinkButton)sender;

            // Un-highlight tabs, disable regions
            p5rtab.Attributes.Add("class", "tab-item");
            p3dtab.Attributes.Add("class", "tab-item");
            p4dtab.Attributes.Add("class", "tab-item");
            p5dtab.Attributes.Add("class", "tab-item");

            // Set selected game to ID of clicked tab and update region
            selectedGame = clickedButton.ID;
            SetRegion();

            // Highlight selected tab
            switch (selectedGame)
            {
                case "p5r":
                    p5rtab.Attributes.Add("class", "tab-item active");
                    break;
                case "p3d":
                    p3dtab.Attributes.Add("class", "tab-item active");
                    break;
                case "p4d":
                    p4dtab.Attributes.Add("class", "tab-item active");
                    break;
                case "p5d":
                    p5dtab.Attributes.Add("class", "tab-item active");
                    break;
                default:
                    break;
            }

            // Show available patches for selected game
            selectedPatch = "mod_support";
            SetPatchTabs();
        }

        private void SetRegion()
        {
            // Swap region if game doesn't support it
            if (!Games.Where(x => x.ID.Equals(selectedGame)).Any(y => y.Region.Equals(selectedRegion)))
            {
                if (selectedRegion == "usa")
                    selectedRegion = "eur";
                else
                    selectedRegion = "usa";
            }

            // Update radio icons
            if (selectedRegion == "usa")
            {
                usa.Text = "<i class=\"fas fa-circle\"></i> USA";
                eur.Text = "<i class=\"far fa-circle\"></i> EUR";
            }
            else
            {
                usa.Text = "<i class=\"far fa-circle\"></i> USA";
                eur.Text = "<i class=\"fas fa-circle\"></i> EUR";
            }
        }

        private void SetPatchTabs()
        {
            // Hide all tabs
            mod_support_tab.Attributes.Add("class", "tab-item d-none");
            _0505_tab.Attributes.Add("class", "tab-item d-none");
            intro_skip_tab.Attributes.Add("class", "tab-item d-none");
            all_dlc_tab.Attributes.Add("class", "tab-item d-none");
            no_trp_tab.Attributes.Add("class", "tab-item d-none");
            square_tab.Attributes.Add("class", "tab-item d-none");
            p5_save_tab.Attributes.Add("class", "tab-item d-none");
            overlay_tab.Attributes.Add("class", "tab-item d-none");
            // Enable Checkbox
            enable.Enabled = true;

            // Show tabs for available patches
            switch (selectedGame)
            {
                case "p5r":
                    mod_support_tab.Attributes.Add("class", "tab-item");
                    _0505_tab.Attributes.Add("class", "tab-item");
                    intro_skip_tab.Attributes.Add("class", "tab-item");
                    all_dlc_tab.Attributes.Add("class", "tab-item");
                    no_trp_tab.Attributes.Add("class", "tab-item");
                    square_tab.Attributes.Add("class", "tab-item");
                    p5_save_tab.Attributes.Add("class", "tab-item");
                    break;
                case "p3d":
                    mod_support_tab.Attributes.Add("class", "tab-item");
                    intro_skip_tab.Attributes.Add("class", "tab-item");
                    no_trp_tab.Attributes.Add("class", "tab-item");
                    overlay_tab.Attributes.Add("class", "tab-item");
                    break;
                case "p4d":
                    mod_support_tab.Attributes.Add("class", "tab-item");
                    intro_skip_tab.Attributes.Add("class", "tab-item");
                    no_trp_tab.Attributes.Add("class", "tab-item");
                    break;
                case "p5d":
                    mod_support_tab.Attributes.Add("class", "tab-item");
                    intro_skip_tab.Attributes.Add("class", "tab-item");
                    no_trp_tab.Attributes.Add("class", "tab-item");
                    overlay_tab.Attributes.Add("class", "tab-item");
                    break;
                default:
                    break;
            }

            // Highlight selected patch tab, disable non-optional mods checkbox
            switch (selectedPatch)
            {
                case "mod_support":
                    mod_support_tab.Attributes.Add("class", "tab-item active");
                    enable.Enabled = false;
                    break;
                case "_0505":
                    _0505_tab.Attributes.Add("class", "tab-item active");
                    enable.Enabled = false;
                    break;
                case "intro_skip":
                    intro_skip_tab.Attributes.Add("class", "tab-item active");
                    enable.Enabled = false;
                    break;
                case "all_dlc":
                    all_dlc_tab.Attributes.Add("class", "tab-item active");
                    break;
                case "no_trp":
                    no_trp_tab.Attributes.Add("class", "tab-item active");
                    break;
                case "square":
                    square_tab.Attributes.Add("class", "tab-item active");
                    enable.Enabled = false;
                    break;
                case "p5_save":
                    p5_save_tab.Attributes.Add("class", "tab-item active");
                    enable.Enabled = false;
                    break;
                case "overlay":
                    overlay_tab.Attributes.Add("class", "tab-item active");
                    enable.Enabled = false;
                    break;
            }

            // Show description of selected patch(es)
            SetDescription();
        }

        private void SetDescription()
        {
            var game = Games.First(x => x.ID.Equals(selectedGame) && x.Region.Equals(selectedRegion));
            titleID.InnerHtml = $"Patches applied to <b>{game.TitleID}</b> ({game.Name}, {game.Region.ToUpper()}):";

            // Get list of all patches for selected game
            List<Patch> patches = new List<Patch>();
            switch (selectedGame)
            {
                case "p5r":
                    patches = P5RPatches;
                    break;
                case "p3d":
                    patches = P3DPatches;
                    break;
                case "p4d":
                    patches = P4DPatches;
                    break;
                case "p5d":
                    patches = P5DPatches;
                    break;
            }

            // Show patch name, image & long/short description
            var patch = patches.First(x => x.ID.Equals(selectedPatch));
            patch_name.InnerText = patch.Name;
            image.Src = patch.Image;
            description.InnerHtml = patch.ShortDesc;
            description_long.InnerHtml = patch.LongDesc;

            // Update checkbox icon according to whether patch is enabled
            if (patch.Enabled)
                enable.Text = "<i class=\"fas fa-check-square\"></i> Enable This Patch";
            else
                enable.Text = "<i class=\"far fa-square\"></i> Enable This Patch";

            // Show FPKG hash
            crc32.InnerText = game.CRC32;
            md5.InnerText = game.MD5;
            sha1.InnerText = game.SHA1;

            // Update download links
            SetPKGLink();
            SetEBOOTLink();

            // Show applied patches & titleID near download button
            appliedPatches.InnerText = "";
            foreach (var enabledPatch in patches.Where(x => x.Enabled))
                appliedPatches.InnerText += $"{enabledPatch.Name}, ";
            appliedPatches.InnerText = appliedPatches.InnerText.TrimEnd(' ').TrimEnd(',');

            // Put checkmark next to enabled patch tabs
            foreach (var gamePatch in patches)
            {
                switch (gamePatch.ID)
                {
                    case "mod_support":
                        if (gamePatch.Enabled)
                            mod_support.Text = "Mod Support <i class=\"fas fa-check-square\"></i>";
                        else
                            mod_support.Text = "Mod Support";
                        break;
                    case "_0505":
                        if (gamePatch.Enabled)
                            _0505.Text = "5.05 Backport <i class=\"fas fa-check-square\"></i>";
                        else
                            _0505.Text = "5.05 Backport";
                        break;
                    case "intro_skip":
                        if (gamePatch.Enabled)
                            intro_skip.Text = "Intro Skip <i class=\"fas fa-check-square\"></i>";
                        else
                            intro_skip.Text = "Intro Skip";
                        break;
                    case "all_dlc":
                        if (gamePatch.Enabled)
                            all_dlc.Text = "Content Enabler <i class=\"fas fa-check-square\"></i>";
                        else
                            all_dlc.Text = "Content Enabler";
                        break;
                    case "no_trp":
                        if (gamePatch.Enabled)
                            no_trp.Text = "Disable Trophies <i class=\"fas fa-check-square\"></i>";
                        else
                            no_trp.Text = "Disable Trophies";
                        break;
                    case "square":
                        if (gamePatch.Enabled)
                            square.Text = "Global Square Menu <i class=\"fas fa-check-square\"></i>";
                        else
                            square.Text = "Global Square Menu";
                        break;
                    case "p5_save":
                        if (gamePatch.Enabled)
                            p5_save.Text = "P5 Save Bonus <i class=\"fas fa-check-square\"></i>";
                        else
                            p5_save.Text = "P5 Save Bonus";
                        break;
                    case "overlay":
                        if (gamePatch.Enabled)
                            overlay.Text = "Disable Screenshot Overlay <i class=\"fas fa-check-square\"></i>";
                        else
                            overlay.Text = "Disable Screenshot Overlay";
                        break;
                }
            }
        }

        protected void PatchTab_Click(object sender, EventArgs e)
        {
            // Highlight clicked tab and select patch
            LinkButton clickedButton = (LinkButton)sender;
            selectedPatch = clickedButton.ID;
            SetPatchTabs();
        }

        protected void Enable_Click(object sender, EventArgs e)
        {
            LinkButton clickedButton = (LinkButton)sender;
            // Toggle whether patch is enabled
            switch (selectedGame)
            {
                case "p5r":
                    foreach (var patch in P5RPatches.Where(x => x.ID.Equals(selectedPatch)))
                        if (patch.Enabled)
                            patch.Enabled = false;
                        else
                            patch.Enabled = true;
                    break;
                case "p3d":
                    foreach (var patch in P3DPatches.Where(x => x.ID.Equals(selectedPatch)))
                        if (patch.Enabled)
                            patch.Enabled = false;
                        else
                            patch.Enabled = true;
                    break;
                case "p4d":
                    foreach (var patch in P4DPatches.Where(x => x.ID.Equals(selectedPatch)))
                        if (patch.Enabled)
                            patch.Enabled = false;
                        else
                            patch.Enabled = true;
                    break;
                case "p5d":
                    foreach (var patch in P5DPatches.Where(x => x.ID.Equals(selectedPatch)))
                        if (patch.Enabled)
                            patch.Enabled = false;
                        else
                            patch.Enabled = true;
                    break;
            }

            // Refresh description of selected patch(es)
            SetDescription();
        }

        protected void Radio_Click(object sender, EventArgs e)
        {
            LinkButton clickedRadio = (LinkButton)sender;
            // If the selected game has the clicked region, and it isn't the currently selected region...
            if (clickedRadio.ID != selectedRegion && Games.Where(x => x.ID.Equals(selectedGame)).Any(y => y.Region.Equals(clickedRadio.ID)))
            {
                // Update selected region and radio button icons
                selectedRegion = clickedRadio.ID;
                SetRegion();

                // Update description of selected patch(es)
                SetDescription();
            }
                
        }

        protected void SetPKGLink()
        {
            string url = "http://up-4.net/d/";

            switch (selectedGame)
            {
                case "p5r":
                    switch (selectedRegion)
                    {
                        case "eur":
                            if (P5RPatches.Where(y => y.Enabled).Any(x => x.ID.Equals("all_dlc")))
                            {
                                if (P5RPatches.Where(y => y.Enabled).Any(x => x.ID.Equals("no_trp")))
                                    url += "QmFH";
                                else
                                    url += "QmCl";
                            }
                            else if (P5RPatches.Where(y => y.Enabled).Any(x => x.ID.Equals("no_trp")))
                                url += "QmFJ";
                            else
                                url += "QmFI";
                            break;
                        case "usa":
                            if (P5RPatches.Where(y => y.Enabled).Any(x => x.ID.Equals("all_dlc")))
                            {
                                if (P5RPatches.Where(y => y.Enabled).Any(x => x.ID.Equals("no_trp")))
                                    url += "QmCi";
                                else
                                    url += "QmCh";
                            }
                            else if (P5RPatches.Where(y => y.Enabled).Any(x => x.ID.Equals("no_trp")))
                                url += "QmCk";
                            else
                                url += "QmCj";
                            break;
                    }
                    break;
                case "p3d":
                    if (P3DPatches.Where(y => y.Enabled).Any(x => x.ID.Equals("no_trp")))
                        url += "QmAL";
                    else
                        url += "QmAG";
                    break;
                case "p4d":
                    if (P4DPatches.Where(y => y.Enabled).Any(x => x.ID.Equals("no_trp")))
                        url += "QmAh";
                    else
                        url += "QmAY";
                    break;
                case "p5d":
                    if (P5DPatches.Where(y => y.Enabled).Any(x => x.ID.Equals("no_trp")))
                        url += "QmAn";
                    else
                        url += "QmAk";
                    break;
            }

            pkg.NavigateUrl = url;
        }

        protected void SetEBOOTLink()
        {
            string url = "http://up-4.net/d/";

            switch (selectedGame)
            {
                case "p5r":
                    if (P5RPatches.Where(y => y.Enabled).Any(x => x.ID.Equals("all_dlc")))
                    {
                        if (P5RPatches.Where(y => y.Enabled).Any(x => x.ID.Equals("no_trp")))
                            url += "QmAq";
                        else
                            url += "QmAo";
                    }
                    else if (P5RPatches.Where(y => y.Enabled).Any(x => x.ID.Equals("no_trp")))
                        url += "QmAx";
                    else
                        url += "QmAs";
                    break;
                case "p3d":
                    if (P3DPatches.Where(y => y.Enabled).Any(x => x.ID.Equals("no_trp")))
                        url += "QmAJ";
                    else
                        url += "QmAD";
                    break;
                case "p4d":
                    if (P4DPatches.Where(y => y.Enabled).Any(x => x.ID.Equals("no_trp")))
                        url += "QmAa";
                    else
                        url += "QmAW";
                    break;
                case "p5d":
                    if (P5DPatches.Where(y => y.Enabled).Any(x => x.ID.Equals("no_trp")))
                        url += "QmAm";
                    else
                        url += "QmAi";
                    break;
            }

            eboot.NavigateUrl = url;
        }
    }

    public class Game
    {
        public string Name { get; set; } = "";
        public string ID { get; set; } = "";
        public string TitleID { get; set; } = "";
        public string Region { get; set; } = "";
        public string CRC32 { get; set; } = "";
        public string MD5 { get; set; } = "";
        public string SHA1 { get; set; } = "";
    }

    public class Patch
    {
        public string Name { get; set; } = "";
        public string ID { get; set; } = "";
        public string ShortDesc { get; set; } = "";
        public string LongDesc { get; set; } = "";
        public string Image { get; set; } = "";
        public bool Enabled { get; set; } = false;
    }
}