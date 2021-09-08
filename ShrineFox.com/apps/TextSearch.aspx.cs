using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShrineFoxcom
{
    public partial class TextSearch : Page
    {
        public static List<Tuple<int, string, string>> matches = new List<Tuple<int, string, string>>();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Sidebar
            LiteralControl SidebarHtml = new LiteralControl();
            SidebarHtml.Text = Properties.Resources.IndexSidebar.Replace("<!--Accordions-->", Properties.Resources.Browse + Properties.Resources.Apps.Replace("textsearchlink", "active"));
            Sidebar.Controls.Add(SidebarHtml);

            // Hide Next Occurence Button
            Next.Visible = false;
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            // Start with empty list of matches
            matches = new List<Tuple<int, string, string>>();
            // Get first matches
            GetMatches();
            // Show all matches so far
            ShowMatches();
        }

        private void GetMatches()
        {
            // Determine type from radio buttons
            string type = "";
            if (radioMsg.Checked)
                type = "msg";
            else
                type = "flow";

            // Get lines from text document
            string txtPath = Server.MapPath("..\\App_Data\\txt\\" + gameList.SelectedValue + $"{type}.txt");
            var txtLines = File.ReadAllLines(txtPath);

            // Use lowercase if search is not case sensitive
            string searchTerm = SearchBox.Text;
            if (!CaseSensitive.Checked)
                searchTerm = searchTerm.ToLower();

            // Keep track of matches found in this search
            int matchCount = 0;

            // Start from after the last matching line found
            int startingLine = 0;
            if (matches.Count > 0)
                startingLine = matches.Last().Item1 + 1;

            // For each line in text document...
            for (int i = startingLine; i < txtLines.Length; i++)
            {
                // Add line number, line and filename to match list if found
                if (txtLines[i].Contains(searchTerm) || (!CaseSensitive.Checked && txtLines[i].ToLower().Contains(searchTerm)))
                {
                    string textBlock = "";
                    // Get all text before line but after previous text block
                    for (int x = i - 1; x > 0; x--)
                    {
                        if (txtLines[x].Contains("\\") || txtLines[x].Contains("////") || txtLines[x].Contains("[msg") || txtLines[x].Contains("[sel") || txtLines[x].Equals(""))
                            break;
                        textBlock = txtLines[x] + "<br>" + textBlock;
                    }
                    // Add current line with search term highlighted
                    int removeIndex = txtLines[i].ToLower().IndexOf(searchTerm.ToLower());
                    textBlock += txtLines[i].Remove(removeIndex, searchTerm.Length).Insert(removeIndex, $"<span style=\"background:rgba(var(--link), 0.5);\">{SearchBox.Text}</span>");
                    // Get all text after line but before next text block
                    for (int x = i + 1; x < txtLines.Length; x++)
                    {
                        if (txtLines[x].Contains("\\") || txtLines[x].Contains("////") || txtLines[x].Contains("[msg") || txtLines[x].Contains("[sel") || txtLines[x].Equals(""))
                            break;
                        textBlock += "<br>" + txtLines[x];
                    }

                    // Add match to list
                    matches.Add(new Tuple<int, string, string>(i, textBlock, GetFilename(txtLines, i)));
                    matchCount++;
                }

                // Stop looking if 10 matches were found
                if (matchCount >= 10)
                    return;
            }
        }

        private void ShowMatches()
        {
            string result = "";

            // Show matches or notice if none found
            if (matches.Count > 0)
            {
                for (int i = 0; i < matches.Count; i++)
                {
                    result += $"<div class=\"card\"><p><b>{matches[i].Item3}</b><br>{matches[i].Item2}</p></div>";
                    Next.Visible = true;
                    SearchTip.InnerHtml = ShowTip(Path.GetExtension(matches[i].Item3).ToLower());
                }
            }
            else
            {
                result = Post.Notice("red", "No matches found!");
                Next.Visible = false;
            }

            results.InnerHtml = result;
        }

        string GetFilename(string[] lines, int lineNumber)
        {
            for (int x = lineNumber; x > 0; x--)
                if (!lines[x].Contains("[") && lines[x].Contains(".") && lines[x].Contains("\\"))
                    return lines[x].Replace("// File:", "");

            return "";
        }

        protected void Next_Click(object sender, EventArgs e)
        {
            // Get next matches
            GetMatches();
            // Show all matches so far
            ShowMatches();
        }

        public static string ShowTip(string fileName)
        {
            fileName = fileName.ToLower();
            string notice = "";
            if (fileName.Contains("eboot") || fileName.Contains("slus"))
                notice += Post.Notice("red", "Unfortunately, text contained in the executable cannot be edited by normal means.");
            if (fileName.Contains(".bf"))
                notice += Post.Notice("blue", "<b><a href=\"https://amicitia.miraheze.org/wiki/AtlusScriptCompiler\">AtlusScriptCompiler</a></b> can decompile BF files.");
            if (fileName.Contains(".bmd"))
                notice += Post.Notice("blue", "<b><a href=\"https://amicitia.miraheze.org/wiki/AtlusScriptCompiler\">AtlusScriptCompiler</a></b> can decompile BMD files.");
            if (fileName.Contains(".pak") || fileName.Contains(".pac"))
                notice += Post.Notice("blue", "<b><a href=\"https://shrinefox.com?tool=amicitia\">Amicitia</a></b> or <b><a href=\"https://shrinefox.com?tool=packtools\">PackTools</a></b> can open PAC files.");
            if (fileName.Contains(".pm1"))
                notice += Post.Notice("blue", "<b><a href=\"https://shrinefox.com?tool=pm1editor\">PM1 Message Script Editor</a></b> or <b><a href=\"https://github.com/Meloman19/PersonaEditor/releases\">PersonaEditor</a></b> can edit text in PM1 files.");
            if (fileName.Contains(".bvp"))
                notice += Post.Notice("blue", "<b><a href=\"https://github.com/Meloman19/PersonaEditor/releases\">PersonaEditor</a></b> can edit text in BVP files.");
            if (fileName.Contains(".tbl"))
                notice += Post.Notice("blue", "<b><a href=\"https://shrinefox.com/browse?post=binarytemplates\">010 Editor</a></b> can edit text in TBL files.");

            return notice;
        }
    }
}