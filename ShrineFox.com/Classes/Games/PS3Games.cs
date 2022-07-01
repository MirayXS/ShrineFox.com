using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShrineFoxCom
{
    public partial class Games
    {
        public static List<Game> PS3Games = new List<Game>()
        {
            new Game() { Name = "Persona 5", ShortName = "P5", TitleID = "NPUB31848", Region = "USA",
                Patches = Patches.PS3P5USAPatches,
                ImageUrl = "https://www.mobygames.com/images/covers/l/414569-persona-5-playstation-3-front-cover.jpg" },

            new Game() { Name = "Persona 5", ShortName = "P5", TitleID = "NPEB02436", Region = "EUR",
                Patches = Patches.PS3P5EURPatches,
                ImageUrl = "https://www.mobygames.com/images/covers/l/414569-persona-5-playstation-3-front-cover.jpg" }
        };
    }
}