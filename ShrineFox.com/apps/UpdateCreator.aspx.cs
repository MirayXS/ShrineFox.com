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
            new Game() { Name = "Persona 5 Royal", ID = "p5r", TitleID = "CUSA17416", Region = "usa" },
            new Game() { Name = "Persona 5 Royal", ID = "p5r", TitleID = "CUSA17419", Region = "eur" },
            new Game() { Name = "Persona 3 Dancing", ID = "p3d", TitleID = "CUSA12636", Region = "usa" },
            new Game() { Name = "Persona 4 Dancing", ID = "p4d", TitleID = "CUSA12811", Region = "eur" },
            new Game() { Name = "Persona 5 Dancing", ID = "p5d", TitleID = "CUSA12380", Region = "usa" }
        };

        public static List<Patch> P5RPatches = new List<Patch>()
        {
            new Patch() { ID = "mod_support", Name = "Mod Support", ShortDesc = "mod.cpk file replacement via PKG", Image = "https://66.media.tumblr.com/c3f99e21c7edb1df53e7f2fa02117621/tumblr_inline_pl680q6yWy1rp7sxh_500.gif",
                LongDesc = "Loads modded files from a <kbd>mod.cpk</kbd> file in the PKG's <code>USRDIR</code> directory." +
                            "<br>Only useful if you're downloading the patched eboot.bin and creating the PKG yourself." },
            new Patch() { ID = "mod_support2", Name = "Mod Support(Alt)", ShortDesc = "mod.cpk file replacement via FTP", Image = "https://66.media.tumblr.com/c3f99e21c7edb1df53e7f2fa02117621/tumblr_inline_pl680q6yWy1rp7sxh_500.gif",
                LongDesc = "Loads modded files from a <kbd>mod.cpk</kbd> file from <code>/data/p5r</code> on the PS4's internal memory via FTP.", Enabled = true },
            new Patch() { ID = "_0505", Name = "5.05 Backport", ShortDesc = "Run on firmware 5.05+", Image = "",
                LongDesc = "Allows the game to run on the lowest possible moddable PS4 firmware, and all those above it.", Enabled = true },
            new Patch() { ID = "intro_skip", Name = "Intro Skip", ShortDesc = "Bypass opening logos/movie", Image = "",
                LongDesc = "Skips boot logos and intro movie (can still be viewed in Thieves Den).", Enabled = true },
            new Patch() { ID = "all_dlc", Name = "Content Enabler", ShortDesc = "Enables on-disc content", Image = "",
                LongDesc = "<b>This will make saves created with this patch incompatible</b> with the game when the patch is disabled!"},
            new Patch() { ID = "dlc_msg", Name = "Skip DLC Messages", ShortDesc = "Skip DLC Messages on New Game", Image = "", 
                LongDesc = "Especially useful when using the Content Enabler patch together with a mod that skips the title screen and boots directly into a field.", Enabled = true },
            new Patch() { ID = "no_trp", Name = "Disable Trophies", ShortDesc = "Prevents the game from unlocking trophies", Image = "" },
            new Patch() { ID = "square", Name = "Global Square Menu", ShortDesc = "Square button menu usable everywhere", Image = "", 
                LongDesc = "Enables the square menu globally (e.g. in Thieves Den and in Velvet Room or during events or game sections which disable it).", Enabled = true },
            new Patch() { ID = "p5_save", Name = "P5 Save Bonus", ShortDesc = "Enables P5 save bonus without P5 saves present on system", Image = "", Enabled = true },
            new Patch() { ID = "env", Name = "ENV Tests", ShortDesc = "Test same ENV on all fields", Image = "", 
                LongDesc = "Maps all <code>env/env*.ENV</code> files to <code>env/env0000_000_000.ENV</code>." +
                "<br>Useful for testing custom/swapped ENV files on different fields without replacing them all manually." +
                "<br><b>Crashes the game</b> if <code>env/env0000_000_000.ENV</code> is not present in <kbd>mod.cpk</kbd>."},
            new Patch() { ID = "zzz", Name = "Random Tests", ShortDesc = "Only useful for very specific mod testing scenarios.", Image = "",
                LongDesc = "Only useful for very specific mod testing scenarios." },
            new Patch() { ID = "overlay", Name = "Disable Screenshot Overlay", ShortDesc = "Removes the annoying copyright overlay from in-game screenshots", Image = "", Enabled = true }
        };
        public static List<Patch> P3DPatches = new List<Patch>()
        {
            new Patch() { ID = "mod_support", Name = "Mod Support", ShortDesc = "mod.cpk file replacement via PKG or FTP", Image = "",
                LongDesc = "Loads modded files from a <kbd>mod.cpk</kbd> file in the PKG's <code>USRDIR</code> directory," +
                           $"<br>or placed in <code>/data/p3d</code> on the PS4's internal memory via FTP." +
                            "<br>The latter takes priority.", Enabled = true },
            new Patch() { ID = "intro_skip", Name = "Intro Skip", ShortDesc = "Bypass opening logos/movie", Image = "",
                LongDesc = "Skips boot logos and intro movie.", Enabled = true },
            new Patch() { ID = "no_trp", Name = "Disable Trophies", ShortDesc = "Prevents the game from unlocking trophies", Image = "" },
            new Patch() { ID = "overlay", Name = "Disable Screenshot Overlay", ShortDesc = "Removes the annoying copyright overlay from in-game screenshots", Image = "", Enabled = true }
        };
        public static List<Patch> P4DPatches = new List<Patch>()
        {
            new Patch() { ID = "mod_support", Name = "Mod Support", ShortDesc = "mod.cpk file replacement via PKG or FTP", Image = "",
                LongDesc = "Loads modded files from a <kbd>mod.cpk</kbd> file in the PKG's <code>USRDIR</code> directory," +
                           $"<br>or placed in <code>/data/p4d</code> on the PS4's internal memory via FTP." +
                            "<br>The latter takes priority.", Enabled = true },
            new Patch() { ID = "intro_skip", Name = "Intro Skip", ShortDesc = "Bypass opening logos/movie", Image = "",
                LongDesc = "Skips boot logos and intro movie.", Enabled = true },
            new Patch() { ID = "no_trp", Name = "Disable Trophies", ShortDesc = "Prevents the game from unlocking trophies", Image = "" },
        };
        public static List<Patch> P5DPatches = new List<Patch>()
        {
            new Patch() { ID = "mod_support", Name = "Mod Support", ShortDesc = "mod.cpk file replacement via PKG or FTP", Image = "",
                LongDesc = "Loads modded files from a <kbd>mod.cpk</kbd> file in the PKG's <code>USRDIR</code> directory," +
                           $"<br>or placed in <code>/data/p5d</code> on the PS4's internal memory via FTP." +
                            "<br>The latter takes priority.", Enabled = true },
            new Patch() { ID = "intro_skip", Name = "Intro Skip", ShortDesc = "Bypass opening logos/movie", Image = "",
                LongDesc = "Skips boot logos and intro movie.", Enabled = true },
            new Patch() { ID = "no_trp", Name = "Disable Trophies", ShortDesc = "Prevents the game from unlocking trophies", Image = "" },
            new Patch() { ID = "overlay", Name = "Disable Screenshot Overlay", ShortDesc = "Removes the annoying copyright overlay from in-game screenshots", Image = "", Enabled = true }
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            // Sidebar
            LiteralControl SidebarHtml = new LiteralControl();
            SidebarHtml.Text = ShrineFoxCom.Properties.Resources.IndexSidebar.Replace("<!--Accordions-->", ShrineFoxCom.Properties.Resources.Browse + ShrineFoxCom.Properties.Resources.Apps.Replace("ps4patchlink", "active"));
            Sidebar.Controls.Add(SidebarHtml);
            if (Page.IsPostBack)
                System.Threading.Thread.Sleep(200);
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
            // Disable radio buttons for unsupported regions
            var games = Games.Where(x => x.ID.Equals(selectedGame));
            usa.Enabled = false;
            eur.Enabled = false;
            if (games.Any(x => x.Region.Equals("usa")))
                usa.Enabled = true;
            if (games.Any(x => x.Region.Equals("eur")))
                eur.Enabled = true;
            // Maintain region selection if selected game supports the same region
            if (!games.Any(x => x.Region.Equals(selectedRegion)))
                selectedRegion = games.First().Region;
            // Check radio button of currently selected region
            usa.Checked = false;
            eur.Checked = false;
            if (selectedRegion == "usa")
                usa.Checked = true;
            if (selectedRegion == "eur")
                eur.Checked = true;
        }

        private void SetPatchTabs()
        {
            // Hide all tabs
            mod_support_tab.Attributes.Add("class", "tab-item d-none");
            mod_support2_tab.Attributes.Add("class", "tab-item d-none");
            _0505_tab.Attributes.Add("class", "tab-item d-none");
            intro_skip_tab.Attributes.Add("class", "tab-item d-none");
            all_dlc_tab.Attributes.Add("class", "tab-item d-none");
            dlc_msg_tab.Attributes.Add("class", "tab-item d-none");
            no_trp_tab.Attributes.Add("class", "tab-item d-none");
            square_tab.Attributes.Add("class", "tab-item d-none");
            p5_save_tab.Attributes.Add("class", "tab-item d-none");
            env_tab.Attributes.Add("class", "tab-item d-none");
            zzz_tab.Attributes.Add("class", "tab-item d-none");
            overlay_tab.Attributes.Add("class", "tab-item d-none");

            // Show tabs for available patches
            switch (selectedGame)
            {
                case "p5r":
                    mod_support_tab.Attributes.Add("class", "tab-item");
                    mod_support2_tab.Attributes.Add("class", "tab-item");
                    _0505_tab.Attributes.Add("class", "tab-item");
                    intro_skip_tab.Attributes.Add("class", "tab-item");
                    all_dlc_tab.Attributes.Add("class", "tab-item");
                    dlc_msg_tab.Attributes.Add("class", "tab-item");
                    no_trp_tab.Attributes.Add("class", "tab-item");
                    square_tab.Attributes.Add("class", "tab-item");
                    p5_save_tab.Attributes.Add("class", "tab-item");
                    env_tab.Attributes.Add("class", "tab-item");
                    zzz_tab.Attributes.Add("class", "tab-item");
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

            // Highlight selected patch tab
            switch (selectedPatch)
            {
                case "mod_support":
                    mod_support_tab.Attributes.Add("class", "tab-item active");
                    break;
                case "mod_support2":
                    mod_support2_tab.Attributes.Add("class", "tab-item active");
                    break;
                case "_0505":
                    _0505_tab.Attributes.Add("class", "tab-item active");
                    break;
                case "intro_skip":
                    intro_skip_tab.Attributes.Add("class", "tab-item active");
                    break;
                case "all_dlc":
                    all_dlc_tab.Attributes.Add("class", "tab-item active");
                    break;
                case "dlc_msg":
                    dlc_msg_tab.Attributes.Add("class", "tab-item active");
                    break;
                case "no_trp":
                    no_trp_tab.Attributes.Add("class", "tab-item active");
                    break;
                case "square":
                    square_tab.Attributes.Add("class", "tab-item active");
                    break;
                case "p5_save":
                    p5_save_tab.Attributes.Add("class", "tab-item active");
                    break;
                case "env":
                    env_tab.Attributes.Add("class", "tab-item active");
                    break;
                case "zzz":
                    zzz_tab.Attributes.Add("class", "tab-item active");
                    break;
                case "overlay":
                    overlay_tab.Attributes.Add("class", "tab-item active");
                    break;
            }

            // Show description of selected patch(es)
            SetDescription();
        }

        private void SetDescription()
        {
            var game = Games.First(x => x.ID.Equals(selectedGame) && x.Region.Equals(selectedRegion));
            titleID.InnerHtml = $"Patches applied to <b>{game.TitleID}</b> ({game.Name}, {game.Region.ToUpper()})";

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

            // Show image, long/short description, and whether it's enabled below tabs
            var patch = patches.First(x => x.ID.Equals(selectedPatch));
            patch_name.InnerText = patch.Name;
            image.Src = patch.Image;
            description.InnerHtml = patch.ShortDesc;
            description_long.InnerHtml = patch.LongDesc;
            enable.Checked = patch.Enabled;

            // Show applied patches & titleID near download button
            appliedPatches.InnerText = "";
            foreach (var enabledPatch in patches.Where(x => x.Enabled))
                appliedPatches.InnerText += $"{enabledPatch.Name}, ";
            appliedPatches.InnerText = appliedPatches.InnerText.TrimEnd(' ').TrimEnd(',');

            // Show relative path to downloaded file near download button
            dlPath.InnerText = $"{game.ID}/{game.TitleID}/";
            foreach (var enabledPatch in patches.Where(x => x.Enabled))
                dlPath.InnerText += $"{enabledPatch.ID}/";
            dlPath.InnerText += "eboot.bin";
            // Correct inconsistencies with actual patch names
            dlPath.InnerText.Replace("_0505","0505");
            if (selectedGame != "p5r")
                dlPath.InnerText.Replace("mod_support", "mod");
        }

        protected void PatchTab_Click(object sender, EventArgs e)
        {
            // Highlight clicked tab and select patch
            LinkButton clickedButton = (LinkButton)sender;
            selectedPatch = clickedButton.ID;
            SetPatchTabs();
        }

        protected void Patch_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkedBox = (CheckBox)sender;
            // Toggle whether patch is enabled
            switch (selectedGame)
            {
                case "p5r":
                    foreach (var patch in P5RPatches.Where(x => x.ID.Equals(selectedPatch)))
                        patch.Enabled = checkedBox.Checked;
                    break;
                case "p3d":
                    foreach (var patch in P3DPatches.Where(x => x.ID.Equals(selectedPatch)))
                        patch.Enabled = checkedBox.Checked;
                    break;
                case "p4d":
                    foreach (var patch in P4DPatches.Where(x => x.ID.Equals(selectedPatch)))
                        patch.Enabled = checkedBox.Checked;
                    break;
                case "p5d":
                    foreach (var patch in P5DPatches.Where(x => x.ID.Equals(selectedPatch)))
                        patch.Enabled = checkedBox.Checked;
                    break;
            }

            // Refresh description of selected patch(es)
            SetDescription();
        }

        protected void Radio_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton changedRadio = (RadioButton)sender;
            if (changedRadio.Checked)
                selectedRegion = changedRadio.ID;

            // Refresh description of selected patch(es)
            SetDescription();
        }

        protected void PKG_Click(object sender, EventArgs e)
        {

        }

        protected void EBOOT_Click(object sender, EventArgs e)
        {

        }
    }

    public class Game
    {
        public string Name { get; set; } = "";
        public string ID { get; set; } = "";
        public string TitleID { get; set; } = "";
        public string Region { get; set; } = "";
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