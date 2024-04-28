using PersonaGameLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShrineFoxCom
{
    public partial class Projects : Page
    {
        public class Card
        {
            public string Title { get; set; } = "";
            public string Subtitle { get; set; } = "";
            public string Trello { get; set; } = "";
            public string Body { get; set; } = "";
            public string Footer { get; set; } = "";
            public string TrelloCard { get; set; } = "";
        }

        List<Card> cards = new List<Card>
        {
            new Card() {
                Title = "ShrineFox.com",
                Subtitle = "This website!",
                Trello = "https://trello.com/c/HTLOsjKh/52-shrinefoxcom",
                Body = "I'm always looking for ways to improve this site as an educational resource, as well as to showcase my own work." +
                "<br>Suggestions are always welcome <a href=\"/Forum/\">on the forum</a>.",
                Footer = "<a class=\"btn btn-secondary float-right\" " +
                "href=\"https://github.com/ShrineFox/ShrineFox.com/blob/main/ShrineFox.com\">Source Code</a>", 
                },
            new Card() { 
                Title = "Persona 5(R) Mod Menu", 
                Subtitle = "In-game trainer for P5 and P5R.", 
                Body = "Custom scripts for Persona 5 that replace the square button function with a fully featured trainer." +
                "<br>Currently working on overhauling the repository to support PC, Switch, PS4, and PS3 from the same codebase.",
                Trello = "https://trello.com/c/On4NnqmQ/54-persona-5r-mod-menu-update",
                Footer = "<a class=\"btn btn-primary float-right\" href=\"https://github.com/ShrineFox/Persona-5-Mod-Menu/releases\">Download</a> " +
                "<a class=\"btn btn-secondary float-right\" href=\"https://github.com/ShrineFox/Persona-5-Mod-Menu\">Source Code</a>",
                },
            new Card() {
                Title = "Persona 4 Golden Mod Menu",
                Subtitle = "In-game trainer for P4G PC.",
                Body = "Custom scripts for P4G (PC) that replace the square button function with a fully featured trainer." +
                "<br>Eventually I'd like to make this compatible with P4 (PS2) and P4G (Vita) in a similar fashion to the P5(R) one.",
                Trello = "https://trello.com/c/LH6ss9z7/61-p4g-mod-menu",
                Footer = "<a class=\"btn btn-primary float-right\" href=\"https://github.com/ShrineFox/Persona-4-Golden-Mod-Menu/releases\">Download</a> " +
                "<a class=\"btn btn-secondary float-right\" href=\"https://github.com/ShrineFox/Persona-4-Golden-Mod-Menu\">Source Code</a>",
                },
            new Card() {
                Title = "EarthBound Mod Menu",
                Subtitle = "In-game trainer and QoL mod for EarthBound.",
                Body = "A collection of toggle-able quality of life enhancements for the SNES cult classic RPG EarthBound.<br>" +
                    "Improves inventory space by moving key items to a dedicated menu, lets you use your bike anywhere, even with a full party," +
                    "adds a run button, prevents your dad from calling you, access in-game cheats, and much much more.",
                Trello = "https://trello.com/c/ZL0zqy02/92-earthbound-mod-menu",
                Footer = "<a class=\"btn btn-primary float-right\" href=\"https://github.com/ShrineFox/EarthBound-Mod-Menu/releases\">Download</a> " +
                "<a class=\"btn btn-secondary float-right\" href=\"https://github.com/ShrineFox/EarthBound-Mod-Menu\">Source Code</a>",
                },
            new Card() {
                Title = "P5R Vinesauce Mod",
                Subtitle = "Mod that replaces Joker with Vinny from Vinesauce.",
                Body = "A group project commemorating many years of Vinesauce's variety livestreams. More info in <a href=\"/blog/2023/02/03/p5r-vinesauce-mod-devlog-1/\">this blogpost</a>.",
                Trello = "https://trello.com/c/8DM6pdUf/101-p5r-vinesauce-mod",
                Footer = ""
                },
            new Card() {
                Title = "P5 Adachi Mod",
                Subtitle = "Mod that replaces Joker with Adachi from Persona 4.",
                Body = "A group project aimed at fans of the cabbage detective. More info in this <a href=\"/blog/category/persona-5/adachi-mod/\">series of blogposts</a>." +
                "<br>Looking forward to eventually porting this to P5R on all platforms.",
                Trello = "https://trello.com/c/FT2nZ7wh/58-p5-adachi-mod-update",
                Footer = "<a class=\"btn btn-primary float-right\" href=\"<a class=\"btn btn-primary float-right\" href=\"https://github.com/ShrineFox/JackFrost-Bot/releases\">Download</a>"
                },
            new Card() {
                Title = "FrostBot",
                Subtitle = "Personal Discord Bot.",
                Body = "Made this to automate moderation of Amicitia's private Discord server." +
                "<br>Its most used features are creating/managing roles and role colors.",
                Trello = "https://trello.com/c/tt06Fq1k/53-frostbot-refactor",
                Footer = "<a class=\"btn btn-primary float-right\" href=\"https://github.com/ShrineFox/JackFrost-Bot/releases\">Download</a> " +
                "<a class=\"btn btn-secondary float-right\" href=\"https://github.com/ShrineFox/JackFrost-Bot\">Source Code</a>",
                },
            new Card() {
                Title = "Flowscript Docs",
                Subtitle = "Documentation on using Flowscript.",
                Body = "An online text-based guide on using AtlusScriptCompiler to create custom .BF and .BMD for Persona/SMT games." +
                "<br>Aimed at complete programming beginners to experts alike who want to get into creating scripting mods.",
                Trello = "https://trello.com/c/zJH95Pqi/90-flowscript-docs",
                Footer = "<a class=\"btn btn-primary float-right\" href=\"https://docs.shrinefox.com/\">View</a>",
                },
            new Card() {
                Title = "AtlusScriptCompiler GUI",
                Subtitle = "User interface for compiling and decompiling Atlus scripts.",
                Body = "Uses simple drag and drop operations to make it easier to create .BF and .BMD from .FLOW and .MSG respectively, and vice versa.",
                Trello = "https://trello.com/c/eLm8SFUI/91-atlusscriptcompilergui",
                Footer = "<a class=\"btn btn-primary float-right\" href=\"https://github.com/ShrineFox/AtlusScriptCompiler-GUI/releases\">Download</a> " +
                "<a class=\"btn btn-secondary float-right\" href=\"https://github.com/ShrineFox/AtlusScriptCompiler-GUI\">Source Code</a>",
                },
            new Card() {
                Title = "PersonaVoiceClipEditor",
                Subtitle = "GUI tool for working with sound files in Persona games.",
                Body = "A program made for batch encrypting audio, unpacking/repacking archives, and quickly renaming files (useful for dual-language audio mods).",
                Trello = "https://trello.com/c/14r6yAS3/102-personavoiceclipeditor-update",
                Footer = "<a class=\"btn btn-primary float-right\" href=\"https://github.com/ShrineFox/PersonaVoiceClipEditor/releases\">Download</a> " +
                "<a class=\"btn btn-secondary float-right\" href=\"https://github.com/ShrineFox/PersonaVoiceClipEditor\">Source Code</a>",
                }
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            LiteralControl cardsHtml = new LiteralControl();
            
            string cardHtml = File.ReadAllText(Server.MapPath("~/./App_Data/Resources/Card.html"));

            foreach (var card in cards)
            {
                cardsHtml.Text += cardHtml
                    .Replace("CARDTITLE", card.Title)
                    .Replace("CARDSUBTITLE", card.Subtitle)
                    .Replace("CARDBODY", $"<blockquote class=\"trello-card\"><a href=\"{card.Trello}\">Trello Card</a></blockquote><br>{card.Body}")
                    .Replace("CARDFOOTER", card.Footer)
                    .Replace("CARDIMGSOURCE", "");
            }

            CardsPlaceholder.Controls.Add(cardsHtml);
        }
    }
}