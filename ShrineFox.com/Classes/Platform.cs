using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShrineFoxCom
{
    public class Platform
    {
        public string Name { get; set; } = "";
        public string ShortName { get; set; } = "";
        public List<Game> Games { get; set; } = new List<Game>();
    }
}