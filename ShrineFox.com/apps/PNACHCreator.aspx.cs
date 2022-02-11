using Humanizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShrineFoxCom
{
    public partial class PNACHCreator : Page
    {
        
        static List<Patch> patches = new List<Patch>();
        static List<Game> games = new List<Game>()
        {
            new Game() { Title = "Persona 3 FES", ShortName = "P3FES", ID = "SLUS_216.21", Region = "USA", CRC = "94A82AAA" },
            new Game() { Title = "Persona 4", ShortName = "P4", ID = "SLUS_217.82", Region = "USA", CRC = "DEDC3B71" },
            new Game() { Title = "Shin Megami Tensei III: Nocturne", ShortName = "SMT3", ID = "SLUS_209.11", Region = "USA", CRC = "E8FCF8EC" }
        };

        public class Game
        {
            public string Title { get; set; } = "";
            public string ShortName { get; set; } = "";
            public string Region { get; set; } = "";
            public string ID { get; set; } = "";
            public string CRC { get; set; } = "";
        }

        public class Patch
        {
            public Game Game { get; set; } = new Game();
            public string Title { get; set; } = "";
            public string Author { get; set; } = "";
            public string Notes { get; set; } = "";
            public string PatchCode { get; set; } = "";
            public bool Enabled { get; set; } = false;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (patches.Count == 0)
            {
                // Load Master PNACH contents once
                ParsePNACH(Properties.Resources.P3FES);
                ParsePNACH(Properties.Resources.P4);
                ParsePNACH(Properties.Resources.SMT3);
                SetGameDropDown();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Show last updated time for PNACH
            var lastWriteTime = File.GetLastWriteTime($"{System.Web.Hosting.HostingEnvironment.MapPath("~/.")}//App_Data\\pnach\\SMT3.pnach");
            lastUpdated.Controls.Add(new LiteralControl { Text = $"<i class=\"fas fa-history\" aria-hidden=\"true\"></i> Updated {lastWriteTime.Humanize()}" });
        }

        private void SetGameDropDown()
        {
            // Populate game dropdown
            gameList.Items.Clear();
            gameList.Items.Add("");
            foreach (var game in games)
                gameList.Items.Add(new ListItem() { Text = $"{game.Title} ({game.Region})", Value = game.ID });
            gameList.DataBind();
            gameList.SelectedIndex = 0;
            // Update patch dropdown list
            SetPatchDropDown();
        }

        private void SetPatchDropDown()
        {
            // Repopulate dropdown list, show enabled patches
            patchList.Items.Clear();
            patchList.Items.Add("");
            // Disable previously selected patches
            foreach (var patch in patches)
                patch.Enabled = false;

            if (games.Any(g => g.ID.Equals(gameList.SelectedValue)))
            {
                // Get selected game
                Game game = games.Single(g => g.ID.Equals(gameList.SelectedValue));
                // Update Default CRC
                txtBox_CRC.Text = game.CRC;
                // Add patches for selected game
                foreach (var patch in patches.Where(x => x.Game.Equals(game)))
                    patchList.Items.Add(new ListItem() { Text = patch.Title, Value = patch.Title });
            }
            patchList.SelectedIndex = 0;
            patchList.DataBind();
            SetDescription();
        }

        private void SetDescription()
        {
            // Get selected game
            Game game = new Game();
            if (games.Any(g => g.ID.Equals(gameList.SelectedValue)))
                game = games.Single(g => g.ID.Equals(gameList.SelectedValue));

            if (!string.IsNullOrEmpty(patchList.SelectedItem.Value))
            {
                // Update patch's description box
                Patch patch = patches.Where(x => x.Game.Equals(game)).First(x => x.Title.Equals(patchList.SelectedItem.Value));
                patchTitle.InnerText = patch.Title;
                patchInfo.InnerText = $"by {patch.Author}";
                patchNotes.InnerText = patch.Notes;
                enable.Enabled = true;
                if (patch.Enabled)
                    enable.Text = "<i class=\"fas fa-check-square\"></i> Enable This Patch";
                else
                    enable.Text = "<i class=\"far fa-square\"></i> Enable This Patch";
            }
            else
            {
                patchTitle.InnerText = "Select A Patch";
                patchInfo.InnerText = "Learn about a patch's functionality & toggle it";
                patchNotes.InnerText = "";
                enable.Enabled = false;
            }

            // Update enabled state of dropdownlist items
            for (int i = 0; i < patchList.Items.Count; i++)
            {
                if (patches.Where(x => x.Game.Equals(game)).Any(x => x.Title.Equals(patchList.Items[i].Value)))
                {
                    var patch = patches.Where(x => x.Game.Equals(game)).First(x => x.Title.Equals(patchList.Items[i].Value));
                    if (patch.Enabled)
                        patchList.Items[i].Text = $"✓ {patch.Title}";
                    else
                        patchList.Items[i].Text = patch.Title;
                }
            }
            
            // Show applied patches list near download button
            appliedPatches.InnerText = "";
            foreach (var enabledPatch in patches.Where(x => x.Enabled))
                appliedPatches.InnerText += $"{enabledPatch.Title}, ";
            appliedPatches.InnerText = appliedPatches.InnerText.TrimEnd(' ').TrimEnd(',');

            // Mod Requirement Notice
            StringBuilder sb = new StringBuilder();
            var enabledPatches = patches.Where(x => x.Enabled);
            if (enabledPatches.Any(x => x.Title.Equals("HostFS")))
                sb.Append(Post.Notice("yellow", "Please read <a href=\"https://shrinefox.com/guides/2020/04/10/modding-using-hostfs-on-pcsx2-p3-p4-smt3/\">these instructions</a> for using HostFS."));
            NoticePlaceHolder.Controls.Add(new LiteralControl { Text = sb.ToString() });

            // Compatibility Notice
            sb = new StringBuilder();
            NoticePlaceHolder2.Controls.Add(new LiteralControl { Text = sb.ToString() });
        }

        protected void SelectGame_Changed(object sender, EventArgs e)
        {
            SetPatchDropDown();
        }

        protected void SelectPatch_Changed(object sender, EventArgs e)
        {
            SetDescription();
        }

        protected void EnableAll_Click(object sender, EventArgs e)
        {
            foreach (var patch in patches)
                TogglePatch(patch.Title, true, false);
            SetDescription();
        }

        protected void DisableAll_Click(object sender, EventArgs e)
        {
            foreach (var patch in patches)
                TogglePatch(patch.Title, false, true);
            SetDescription();
        }

        protected void Enable_Click(object sender, EventArgs e)
        {
            TogglePatch(patchList.SelectedItem.Value);
            SetDescription();
        }

        private void TogglePatch(string patchTitle, bool forceEnable = false, bool forceDisable = false)
        {
            // Get selected game
            Game game = new Game();
            if (games.Any(g => g.ID.Equals(gameList.SelectedValue)))
                game = games.Single(g => g.ID.Equals(gameList.SelectedValue));

            // Toggle enabled state of selected patch
            if (patches.Where(x => x.Game.Equals(game)).Any(x => x.Title.Equals(patchTitle)))
            {
                if (forceEnable)
                    patches.Where(x => x.Game.Equals(game)).First(x => x.Title.Equals(patchTitle)).Enabled = true;
                else if (forceDisable)
                    patches.Where(x => x.Game.Equals(game)).First(x => x.Title.Equals(patchTitle)).Enabled = false;
                else
                    patches.Where(x => x.Game.Equals(game)).First(x => x.Title.Equals(patchTitle)).Enabled = !patches.Where(x => x.Game.Equals(game)).First(x => x.Title.Equals(patchTitle)).Enabled;
            }
        }

        protected void Download_Click(object sender, EventArgs e)
        {
            LinkButton clickedButton = (LinkButton)sender;
            StringBuilder sb = new StringBuilder();

            // Add enabled patches to text file
            sb.Append("// PNACH generated by https://shrinefox.com/apps/PNACHCreator\n");
            foreach (Patch patch in patches.Where(x => x.Enabled))
                sb.Append($"\n// Title: {patch.Title}" +
                    $"\n// Author: {patch.Author}" +
                    $"\n// Game: {patch.Game.Title} ({patch.Game.Region})" +
                    $"\n// Notes: {patch.Notes}" +
                    $"\n{patch.PatchCode}");

            // Download file if it's not empty
            string text = sb.ToString();
            if (!string.IsNullOrEmpty(text))
            {
                Response.Clear();
                Response.ClearHeaders();
                Response.AppendHeader("Content-Length", text.Length.ToString());
                Response.ContentType = "text/plain";
                Response.AppendHeader("Content-Disposition", $"attachment;filename=\"{txtBox_CRC.Text}.pnach\"");
                Response.Write(text);
                Response.End();
            }
        }

        public static void ParsePNACH(string pnachPath)
        {
            string[] pnachLines = pnachPath.Split('\n');

            for (int i = 0; i < pnachLines.Count(); i++)
            {
                // If line starts with // Title: begin reading patch
                if (pnachLines[i].Trim().StartsWith("// Title:"))
                {
                    // Continue serializing data until end of patch or yml file
                    var patch = new Patch();
                    int x = i;
                    patch.Title = pnachLines[x].Trim().Replace("// Title:", "").Trim();
                    x++;

                    while (x < pnachLines.Count() && !pnachLines[x].StartsWith("// Title:"))
                    {
                        switch (pnachLines[x].Trim())
                        {
                            case string s when s.StartsWith("// Author:"):
                                patch.Author = s.Replace("// Author:", "").Trim();
                                break;
                            case string s when s.StartsWith("// Game:"):
                                string game = s.Replace("// Game:", "").Split('(')[0].Trim();
                                string region = s.Split('(')[1].Replace(")","").Trim();
                                patch.Game = games.Single(g => g.Title.Equals(game) && g.Region.Equals(region));
                                break;
                            case string s when s.StartsWith("// Notes:"):
                                patch.Notes = s.Replace("// Notes:", "").Trim();
                                break;
                            default:
                                if (pnachLines[x].Trim() != "")
                                    patch.PatchCode += pnachLines[x].Trim() + "\n";
                                break;
                        }
                        x++;
                    }

                    // Add serialized patch to patch list
                    patches.Add(patch);
                }
            }
        }
    }
}