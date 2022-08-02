using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShrineFoxCom
{
    public class GamePatch
    {
        public string Name { get; set; } = "";
        public string ShortName { get; set; } = "";
        public string Author { get; set; } = "";
        public string Version { get; set; } = "";
        public string Description { get; set; } = "";
        public string Text { get; set; } = "";
        public bool AlwaysOn { get; set; } = false;
        public bool OnByDefault { get; set; } = false;
        public List<string> Conflicts { get; set; } = new List<string>();
        public string TargetPlatform { get; set; } = "";
    }
}