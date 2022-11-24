using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace ShrineFoxCom
{
    public class GetFile
    {
        public static string FromPath(string path)
        {
            string text = "";
            path = path.Replace("./", System.Web.Hosting.HostingEnvironment.MapPath("~/."));
            if (File.Exists(path))
            {
                text = File.ReadAllText(path);
            }
            return text;
        }
    }
}