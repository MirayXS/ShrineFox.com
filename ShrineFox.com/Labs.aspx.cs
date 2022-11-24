using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShrineFoxCom
{
    public partial class Labs : Page
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
                "<br>Suggestions are always welcome <a href=\"https://shrinefox.com/Forum/\">on the forum</a>.",
                Footer = "<a class=\"btn btn-secondary float-right\" " +
                "href=\"https://github.com/ShrineFox/ShrineFox.com/blob/main/ShrineFox.com\">Source Code</a>", 
                },
            new Card() { 
                Title = "Persona 5(R) Mod Menu", 
                Subtitle = "In-game trainer for P5 and P5R.", 
                Body = "Custom scripts for Persona 5 that replace the square button function with a fully featured trainer." +
                "<br>Currently working on overhauling the repository.",
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
                Title = "FrostBot",
                Subtitle = "Personal Discord Bot.",
                Body = "Made this to automate moderation of Amicitia's private Discord server." +
                "<br>Its most used features are creating/managing roles and role colors. " +
                "Eventually I'd like to host it online by integrating it with this website.",
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
                Title = "P4GMOdelConverter",
                Subtitle = "User interface for importing and exporting custom P4G/P3P GMO models.",
                Body = "Automatically makes changes to custom models to make them compatible with P4G." +
                "<br>Kind of a mess right now, started working on a WIP refactor, but I don't know when I'll finish it.",
                Trello = "https://trello.com/c/aXPZ0DH1/42-p4gmodel",
                Footer = "<a class=\"btn btn-primary float-right\" href=\"https://github.com/ShrineFox/P4GMOdelConverter/releases\">Download</a> " +
                "<a class=\"btn btn-secondary float-right\" href=\"https://github.com/ShrineFox/P4GMOdelConverter\">Source Code</a>",
                },
            new Card() {
                Title = "PersonaRandomizer",
                Subtitle = "User interface for generating \"randomized\" Persona mods.",
                Body = "Very WIP and unstable. Uses old code by TGE for some support randomizing the TBL files of some games." +
                "<br>Also randomizes models and BGM for P3/4. In the future I'd like to completely redo it and make features work across all games.",
                Trello = "https://trello.com/c/GycdxuUs/86-personarandomizer",
                Footer = "<a class=\"btn btn-primary float-right\" href=\"https://github.com/ShrineFox/P4GMOdelConverter/releases\">Download</a> " +
                "<a class=\"btn btn-secondary float-right\" href=\"https://github.com/ShrineFox/P4GMOdelConverter\">Source Code</a>",
                },
            new Card() {
                Title = "P-Studio",
                Subtitle = "Experimental user interface for all things Persona modding.",
                Body = "Early concept for a workspace that conveniently unifies all the tools you need for modding Persona games." +
                "<br>Right now it manages projects and files, and docks other applications like Amicitia, GFDStudio, and Notepad++ in the window for editing." +
                "<br>In the future I'd like to add support for more programs and maybe integrate a mod manager. Needs lots more testing.",
                Trello = "https://trello.com/c/eY6RXEIj/83-p-studio-v01",
                Footer = "" +
                "<a class=\"btn btn-secondary float-right\" href=\"https://github.com/ShrineFox/P-Studio\">Source Code</a>",
                }
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            LiteralControl cardsHtml = new LiteralControl();
            
            foreach (var card in cards)
            {
                cardsHtml.Text += GetFile.card
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