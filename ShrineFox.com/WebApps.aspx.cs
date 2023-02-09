using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShrineFoxCom
{
    public partial class WebApps : Page
    {
        public class Card
        {
            public string Title { get; set; } = "";
            public string Subtitle { get; set; } = "";
            public string Body { get; set; } = "";
            public string Footer { get; set; } = "";
            public string Image { get; set; } = "";
        }

        List<Card> cards = new List<Card>
        {
            new Card() {
                Title = "RPCS3 Patch Creator",
                Subtitle = "Generate a <kbd>patch.yml</kbd> to use for modding Persona 5 (PS3).",
                Body = "Automatically removes conflicting and unwanted patches so you only download what you need." +
                "<br>Read more about <a href=\"https://shrinefox.com/guides/2019/04/19/persona-5-rpcs3-modding-guide-1-downloads-and-setup/\">" +
                "how to mod Persona 5 on RPCS3</a> using these patches, or with a <a href=\"https://shrinefox.com/guides/2019/06/12/persona-5-ps3-eboot-patching/\">" +
                "CFW PS3 Console</a>.",
                Footer = "<a class=\"btn btn-primary float-right\" href=\"https://shrinefox.com/GetStarted\">View</a> " +
                "<a class=\"btn btn-secondary float-right\" href=\"https://github.com/ShrineFox/ShrineFox.com/blob/main/ShrineFox.com/GetStarted.aspx.cs\">Source Code</a>", 
                Image = "https://i.imgur.com/J1R3UTD.png" },
            new Card() { Title = "PS4 Update Creator", Subtitle = "Generate a pre-patched <kbd>EBOOT.bin</kbd> or <kbd>.PKG</kbd> for installing a modded PS4 Update.", 
                Body = "Choose a set of <a href=\"https://github.com/zarroboogs/ppp\">patches created by zarroboogs</a> for P5R, P3D, P4D and P5D." +
                "<br><a href=\"https://shrinefox.com/guides/2020/09/30/modding-persona-5-royal-on-ps4/\">Learn more about setting up and installing PS4 mods.</a>",
                Footer = "<a class=\"btn btn-primary float-right\" href=\"https://shrinefox.com/GetStarted\">View</a> " +
                "<a class=\"btn btn-secondary float-right\" href=\"https://github.com/ShrineFox/ShrineFox.com/blob/main/ShrineFox.com/GetStarted.aspx.cs\">Source Code</a>",
                Image = "https://i.imgur.com/Daf2ryg.png" },
            new Card() { Title = "Text Search",
                Subtitle = "Search for which file contains certain dialog and flowscript code.",
                Body = "If you're looking for a certain file in P3P, P3FES, P4(G) or P5(R), " +
                "you can enter part of the dialog or <a href=\"https://docs.shrinefox.com/flowscript/intro-to-scripting\">flowscript code</a> " +
                "and narrow down where it might be.",
                Footer = "<a class=\"btn btn-primary float-right\" href=\"https://shrinefox.com/apps/TextSearch\">View</a> " +
                "<a class=\"btn btn-secondary float-right\" href=\"https://github.com/ShrineFox/ShrineFox.com/blob/main/ShrineFox.com/apps/TextSearch.aspx.cs\">Source Code</a>",
                Image = "https://i.imgur.com/FhAiKde.png" },
            new Card() { Title = "Files", Subtitle = "View pre-dumped files from each game.",
                Body = "To save you time in case you accidentally lost or overwrote a copy of your own dumped files," +
                " you can look for a copy of the original to download here." +
                "<br><br><b>Important:</b> these are not \"complete\" dumps of the game, and cannot be used to install or run " +
                "any content that you don't own. These are provided for research and development purposes only.",
                Footer = "<a class=\"btn btn-primary float-right\" href=\"https://shrinefox.com/apps/Files\">View</a>",
                Image = "https://i.imgur.com/77HPhLh.png" }
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            LiteralControl cardsHtml = new LiteralControl();

            string cardHtml = GetFile.FromPath("./App_Data/Resources/Card.html");

            foreach (var card in cards)
            {
                cardsHtml.Text += cardHtml
                    .Replace("CARDTITLE", card.Title)
                    .Replace("CARDSUBTITLE", card.Subtitle)
                    .Replace("CARDBODY", card.Body)
                    .Replace("CARDFOOTER", card.Footer)
                    .Replace("CARDIMGSOURCE", card.Image);
            }

            CardsPlaceholder.Controls.Add(cardsHtml);
        }
    }
}