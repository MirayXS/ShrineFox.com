using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShrineFoxCom
{
    public class Game
    {
        public string Name { get; set; } = "";
        public string ShortName { get; set; } = "";
        public string TitleID { get; set; } = "";
        public string Region { get; set; } = "";
        public string CRC32 { get; set; } = "";
        public string MD5 { get; set; } = "";
        public string SHA1 { get; set; } = "";
        public string CRC { get; set; } = "";
        public string ImageUrl { get; set; } = "";
        public List<GamePatch> Patches { get; set; } = new List<GamePatch>();
    }
}