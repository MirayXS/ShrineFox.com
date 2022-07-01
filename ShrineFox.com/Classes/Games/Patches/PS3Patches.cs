using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShrineFoxCom
{
    public partial class Patches
    {
        public static List<GamePatch> PS3P5USAPatches = new List<GamePatch>()
        {
            new GamePatch() { Name = "Intro Skip", ShortName = "intro_skip", Author = "zarroboogs",
                Description = "Skips boot logos and intro movie (can still be viewed in Thieves Den)",
                AlwaysOn = true,
                OnByDefault = true
            }
        };

        public static List<GamePatch> PS3P5EURPatches = PS3P5USAPatches;
    }
}