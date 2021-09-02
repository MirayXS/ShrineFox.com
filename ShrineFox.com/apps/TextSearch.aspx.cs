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
        protected void Page_Load(object sender, EventArgs e)
        {
            // Sidebar
            LiteralControl SidebarHtml = new LiteralControl();
            SidebarHtml.Text = Properties.Resources.IndexSidebar.Replace("<!--Accordions-->", Properties.Resources.Browse + Properties.Resources.Apps.Replace("textsearchlink", "active"));
            Sidebar.Controls.Add(SidebarHtml);

            Page.MaintainScrollPositionOnPostBack = true;
            HiddenPostsLink.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            results.InnerHtml = "";
            string txtPath = Server.MapPath("txt\\" + gameList.SelectedValue + ".txt");
            var txtLines = File.ReadAllLines(txtPath);
            int lineNumber = 0;
            string match = GetLine(txtLines, 0, out lineNumber);
            if (match != "")
            {
                Hidden1.Value = lineNumber.ToString();
                string filename = GetFilename(txtLines, lineNumber);
                results.InnerHtml += $"<div class=\"card\"><p><b>{filename}</b><br>{match}</p></div><br>";
                HiddenPostsLink.Visible = true;
            }
            else
            {
                results.InnerHtml += $"<div class=\"notices red\"><p>No matches found!</p></div>";
                tip.InnerHtml = "";
            }
        }

        protected void HiddenPostsLink_Click(object sender, EventArgs e)
        {
            string txtPath = Server.MapPath("txt\\" + gameList.SelectedValue + ".txt");
            var txtLines = File.ReadAllLines(txtPath);
            int lineNumber;
            string match = GetLine(txtLines, Convert.ToInt32(Hidden1.Value) + 1, out lineNumber);
            if (match != "")
            {
                Hidden1.Value = lineNumber.ToString();
                string filename = GetFilename(txtLines, lineNumber);
                results.InnerHtml += $"<div class=\"card\"><p><b>{filename}</b><br>{match}</p></div><br>";
                HiddenPostsLink.Visible = true;
            }
            else
            {
                HiddenPostsLink.Visible = false;
            }
        }

        string GetLine(string[] lines, int startingLineNumber, out int lineNumber)
        {
            string result = "";
            for (int x = startingLineNumber; x < lines.Count(); x++)
            {
                if (CaseSensitive.Checked)
                {
                    if (lines[x].Contains(TextBox1.Text.Replace("[n]", " ")))
                    {
                        lineNumber = x;
                        result = lines[x];
                        for (int y = lineNumber + 1; lineNumber < lines.Count(); y++)
                        {
                            if (!lines[y].Equals(""))
                            {
                                result = result + "\n" + lines[y];
                            }
                        }
                        for (int y = lineNumber - 1; lineNumber > 0; y--)
                        {
                            if (!lines[y].Equals(""))
                            {
                                result = lines[y] + "\n" + result;
                            }
                        }
                        return result;
                    }
                }
                else
                {
                    if (lines[x].ToLower().Contains(TextBox1.Text.Replace("[n]", " ").ToLower()))
                    {
                        lineNumber = x;
                        result = lines[x];

                        for (int y = lineNumber - 1; lineNumber > 0; y--)
                        {
                            if (!String.IsNullOrEmpty(lines[y]))
                            {
                                result = lines[y] + "<br>" + result;
                            }
                            else
                            {
                                break;
                            }
                        }

                        for (int y = lineNumber + 1; lineNumber < lines.Count(); y++)
                        {
                            if (!String.IsNullOrEmpty(lines[y]))
                            {
                                result = result + "<br>" + lines[y];
                            }
                            else
                            {
                                break;
                            }
                        }
                        
                        return result;
                    }
                }
            }

            lineNumber = 0;
            return "";
        }

        string GetFilename(string[] lines, int lineNumber)
        {
            tip.InnerHtml = $"<div class=\"notices blue\"><p>";
            for (int x = lineNumber; x > 0; x--)
                if (!lines[x].Contains("[") && lines[x].Contains(".") && lines[x].Contains("\\"))
                {
                    if (lines[x].ToLower().Contains("eboot") || lines[x].ToLower().Contains("slus"))
                        tip.InnerHtml += "Unfortunately, text contained in the executable cannot be edited by normal means.";
                    else
                    {
                        if (lines[x].ToLower().Contains(".bf"))
                            tip.InnerHtml += "<b><a href=\"https://amicitia.miraheze.org/wiki/AtlusScriptCompiler\">AtlusScriptCompiler</a></b> can decompile BF files. ";
                        if (lines[x].ToLower().Contains(".bmd"))
                            tip.InnerHtml += "<b><a href=\"https://amicitia.miraheze.org/wiki/AtlusScriptCompiler\">AtlusScriptCompiler</a></b> can decompile BMD files. ";
                        if (lines[x].ToLower().Contains(".pak") || lines[x].ToLower().Contains(".pac"))
                            tip.InnerHtml += "<b><a href=\"https://shrinefox.com?tool=amicitia\">Amicitia</a></b> or <b><a href=\"https://shrinefox.com?tool=packtools\">PackTools</a></b> can open PAC files. ";
                        if (lines[x].ToLower().Contains(".pm1"))
                            tip.InnerHtml += "<b><a href=\"https://shrinefox.com?tool=pm1editor\">PM1 Message Script Editor</a></b> or <b><a href=\"https://github.com/Meloman19/PersonaEditor/releases\">PersonaEditor</a></b> can edit text in PM1 files. ";
                        if (lines[x].ToLower().Contains(".bvp"))
                            tip.InnerHtml += "<b><a href=\"https://github.com/Meloman19/PersonaEditor/releases\"> PersonaEditor</a></b> can edit text in BVP files. ";
                    }
                    tip.InnerHtml += "</p></div>";
                    return lines[x];
                }
                    
            return "";
        }
    }
}