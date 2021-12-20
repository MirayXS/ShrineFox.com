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

        protected void Page_Load(object sender, EventArgs e)
        {
            // Sidebar
            LiteralControl SidebarHtml = new LiteralControl();
            SidebarHtml.Text = Properties.Resources.IndexSidebar.Replace("<!--Accordions-->", Properties.Resources.Browse + Properties.Resources.Apps.Replace("ps4patchlink", "active"));
            Sidebar.Controls.Add(SidebarHtml);
            if (!Page.IsPostBack)
            {
                
            }
        }

        private void Radio_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void GameTab_Click(object sender, EventArgs e)
        {
            LinkButton clickedButton = (LinkButton)sender;

            // Un-highlight tabs
            p5rtab.Attributes.Add("class", "tab-item");
            p3dtab.Attributes.Add("class", "tab-item");
            p4dtab.Attributes.Add("class", "tab-item");
            p5dtab.Attributes.Add("class", "tab-item");

            // Highlight selected tab
            switch (clickedButton.ID)
            {
                case "p5r":
                    selectedGame = "p5r";
                    p5rtab.Attributes.Add("class", "tab-item active");
                    break;
                case "p3d":
                    selectedGame = "p3d";
                    p3dtab.Attributes.Add("class", "tab-item active");
                    break;
                case "p4d":
                    selectedGame = "p4d";
                    p4dtab.Attributes.Add("class", "tab-item active");
                    break;
                case "p5d":
                    selectedGame = "p5d";
                    p5dtab.Attributes.Add("class", "tab-item active");
                    break;
                default:
                    break;
            }
        }

        private void PatchTab_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Checkbox_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void Download_Click(object sender, EventArgs e)
        {

        }
    }

    public static class P5RPatches
    {
        static bool _0505 { get; set; } = true;
        static bool all_dlc { get; set; } = false;
        static bool dlc_msg { get; set; } = true;
        static bool intro_skip { get; set; } = true;
        static bool mod_support { get; set; } = false;
        static bool mod_support2 { get; set; } = true;
        static bool no_trp { get; set; } = false;
        static bool p5_save { get; set; } = false;
        static bool square { get; set; } = true;
        static bool env { get; set; } = false;
        static bool zzz { get; set; } = false;
    }

    public static class P3DPatches
    {
        static bool intro_skip { get; set; } = true;
        static bool mod { get; set; } = false;
        static bool no_trp { get; set; } = false;
        static bool overlay { get; set; } = false;
    }

    public static class P5DPatches
    {
        static bool intro_skip { get; set; } = true;
        static bool mod { get; set; } = false;
        static bool no_trp { get; set; } = false;
        static bool overlay { get; set; } = false;
    }

    public static class P4DPatches
    {
        static bool intro_skip { get; set; } = true;
        static bool mod { get; set; } = false;
        static bool no_trp { get; set; } = false;
    }
}