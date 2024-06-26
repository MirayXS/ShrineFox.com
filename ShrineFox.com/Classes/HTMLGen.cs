﻿using PersonaGameLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShrineFoxCom
{
    public class HTMLGen
    {
        public static void BlogForum(PlaceHolder control)
        {
            // Main
            string head = File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Data/Resources/Html/head.html"));
            string body = File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Data/Resources/Html/body.html"));
            string footer = File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Data/Resources/Html/footer.html"));
            // Forum
            string forum_overall_header = File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Data/Resources/Forum/template/overall_header.html"));
            string forum_overall_footer = File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Data/Resources/Forum/template/overall_footer.html"));
            string forum_colours = File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Data/Resources/Forum/theme/colours.css"));
            // Blog
            string blog_header = File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Data/Resources/Blog/header.php"));
            string blog_footer = File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Data/Resources/Blog/footer.php"));
            string blog_style = File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Data/Resources/Blog/style.css"));

            // Generate Directories
            string forumThemePath = $"{System.Web.Hosting.HostingEnvironment.MapPath("~/.")}//forum//styles//Milk_Edit";
            Directory.CreateDirectory($"{forumThemePath}//theme");
            Directory.CreateDirectory($"{forumThemePath}//template");
            string blogThemePath = $"{System.Web.Hosting.HostingEnvironment.MapPath("~/.")}//blog//wp-content//themes//primer";
            Directory.CreateDirectory(blogThemePath);

            // Generate Forum Header
            string forumHeader = forum_overall_header;
            string forumStyles = Between(head, "<!--ShrineFox Styles-->", "<!--End ShrineFox Styles-->");
            string videoSearch = Between(body, "<!--ShrineFox Header-->", "<!--End ShrineFox Header-->");
            forumHeader = forumHeader.Replace("<!--ShrineFox Styles-->", forumStyles)
                           .Replace("<!--ShrineFox Header-->", videoSearch);
            File.WriteAllText($"{forumThemePath}//template//overall_header.html", forumHeader);

            // Generate Forum Footer
            string forum_footer = Between(footer, "<!--ShrineFox Footer-->", "<!--End ShrineFox Footer-->");
            File.WriteAllText($"{forumThemePath}//template//overall_footer.html",
                forum_overall_footer.Replace("<!--ShrineFox Footer-->", forum_footer));

            // Generate Forum CSS
            File.WriteAllText($"{forumThemePath}//theme//colours.css", forum_colours);

            // Generate Blog .PHP/.CSS
            string blogStyles = Between(head, "<!--ShrineFox AllStyles-->", "<!--End ShrineFox Styles-->");
            string blogHeader = blog_header
                               .Replace("<!--ShrineFox Styles-->", blogStyles)
                               .Replace("<!--ShrineFox Header-->", videoSearch);

            foreach (string site in new string[] { "blog" })
            {
                string path = blogThemePath.Replace("blog", site);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                // Header
                File.WriteAllText(Path.Combine(path, "header.php"), blogHeader);
                // Footer
                File.WriteAllText(Path.Combine(path, "footer.php"), blog_footer.Replace("<!--ShrineFox Footer-->", footer));
                // CSS
                File.WriteAllText(Path.Combine(path, "style.css"), blog_style);
            }

            LiteralControl notice = new LiteralControl();
            notice.Text = Html.Notice("green", "<b>Success</b>! Forum/Blog HTML has been updated. Clear caches to see changes.");
            control.Controls.Add(notice);
        }

        public static string Between(string STR, string FirstString, string LastString)
        {
            string FinalString;
            int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
            int Pos2 = STR.IndexOf(LastString);
            FinalString = STR.Substring(Pos1, Pos2 - Pos1);
            return FinalString;
        }

        public static string FirstCharToUpper(string input)
        {
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}