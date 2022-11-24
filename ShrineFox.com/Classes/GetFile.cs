using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace ShrineFoxCom
{
    public class GetFile
    {
        // Main
        public static string head { get; set; } = FromPath("./Resources/Html/head.html");
        public static string body { get; set; } = FromPath("./Resources/Html/body.html");
        public static string footer { get; set; } = FromPath("./Resources/Html/footer.html");
        // Forum
        public static string forum_overall_header { get; set; } = FromPath("./Resources/Forum/template/overall_header.html");
        public static string forum_overall_footer { get; set; } = FromPath("./Resources/Forum/template/overall_footer.html");
        public static string forum_colours { get; set; } = FromPath("./Resources/Forum/theme/colours.css");
        // Blog
        public static string blog_header { get; set; } = FromPath("./Resources/Blog/header.php");
        public static string blog_footer { get; set; } = FromPath("./Resources/Blog/footer.php");
        public static string blog_style { get; set; } = FromPath("./Resources/Blog/style.css");
        // Browse
        public static string browse_post { get; set; } = FromPath("./Resources/Browse/Post.html");
        public static string browse_pagination { get; set; } = FromPath("./Resources/Browse/Pagination.html");
        public static string browse_exclude { get; set; } = FromPath("./App_Data/exclude.txt");
        // Get Started
        public static string p3f_hostfs { get; set; } = FromPath("./pnach/94A82AAA.pnach");
        public static string p4_hostfs { get; set; } = FromPath("./pnach/DEDC3B71.pnach");
        // Etc
        public static string card { get; set; } = FromPath("./Resources/Card.html");

        private static string FromPath(string path)
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