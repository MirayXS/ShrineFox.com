using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShrineFoxCom
{
    public partial class Games
    {
        public static List<Game> PCGames = new List<Game>()
        {
            new Game() { Name = "Persona 4 Golden", ShortName = "P4G", TitleID = "", Region = "USA",
                Patches = Patches.PCP4GPatches,
                ImageUrl = "https://www.mobygames.com/images/covers/l/276137-persona-4-golden-ps-vita-front-cover.png" }
        };
    }
}