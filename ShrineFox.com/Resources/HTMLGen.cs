﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShrineFoxcom
{
    public class HTMLGen
    {

        public static void BlogForum(PlaceHolder control)
        {
            // Forum Theme HTML
            string forumThemePath = $"{System.Web.Hosting.HostingEnvironment.MapPath("~/.")}//forum//styles//prolight//template";
            Directory.CreateDirectory(forumThemePath);

            // Overall Header
            string header = Properties.Resources.overall_header;
            header = header.Replace("<!--INDEXHEADER-->", Properties.Resources.IndexHeader);
            header = header.Replace("<!--INDEXBEFORECONTENT-->", Properties.Resources.IndexBeforeContent.Replace("forumlink", "active"));
            header = header.Replace("<!--INDEXSIDEBAR-->", Properties.Resources.IndexSidebar.Replace("<!--Accordions-->", Properties.Resources.Forum + Properties.Resources.Browse + Properties.Resources.Apps) + $"<a href=\"https://shrinefox.com\"><i class=\"fa fa-home\" aria-hidden=\"true\"></i> ShrineFox.com</a> <i class=\"fa fa-angle-right\" aria-hidden=\"true\"></i><a href=\"https://shrinefox.com/forum\">Forum</a>");
            File.WriteAllText(Path.Combine(forumThemePath, "overall_header.html"), header);
            // Overall Footer
            File.WriteAllText(Path.Combine(forumThemePath, "overall_footer.html"), Properties.Resources.overall_footer.Replace("<!--INDEXFOOTER-->", Properties.Resources.IndexFooter));

            // Blog Theme HTML
            string blogThemePath = $"{System.Web.Hosting.HostingEnvironment.MapPath("~/.")}//blog//wp-content//themes//justread";
            string[] sites = new string[] { "blog", "guides", "news" };
            foreach (string site in sites)
            {
                string path = blogThemePath.Replace("blog", site);
                Directory.CreateDirectory(path);

                // Header
                header = Properties.Resources.header;
                header = header.Replace("<!--INDEXHEADER-->", Properties.Resources.IndexHeader);
                header = header.Replace("<!--INDEXBEFORECONTENT-->", Properties.Resources.IndexBeforeContent.Replace($"{site}link", "active").Replace("articleslink", "active"));
                header = header.Replace("<!--INDEXSIDEBAR-->", Properties.Resources.IndexSidebar.Replace("<!--Accordions-->", Properties.Resources.Blog.Replace("blog", site).Replace("Blog", FirstCharToUpper(site)) + "<!--Accordions-->"));
                foreach (var sitee in sites)
                {
                    if (sitee != site)
                        header = header.Replace("<!--Accordions-->", Properties.Resources.Blog.Replace("blog", sitee).Replace("Blog", FirstCharToUpper(sitee)).Replace("accordion\">", "accordion2\">") + "<!--Accordions-->");
                }
                header = header.Replace("<!--Accordions-->", Properties.Resources.Browse.Replace("accordion\">", "accordion2\">") + Properties.Resources.Apps.Replace("accordion\">", "accordion2\">") + $"</div>");
                header = header.Replace("<!--MainNavigation-->", $"<a href=\"https://shrinefox.com\"><i class=\"fa fa-home\" aria-hidden=\"true\"></i> ShrineFox.com</a> <i class=\"fa fa-angle-right\" aria-hidden=\"true\"></i><a href=\"https://shrinefox.com/{site}\">{FirstCharToUpper(site)}</a>");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                File.WriteAllText(Path.Combine(path, "header.php"), header);
                // Footer
                File.WriteAllText(Path.Combine(path, "footer.php"), Properties.Resources.IndexFooter);
            }

            LiteralControl notice = new LiteralControl();
            notice.Text = Post.Notice("green", "<b>Success</b>! Forum/Blog HTML has been updated. Clear caches to see changes.");
            control.Controls.Add(notice);
        }

        public static string FirstCharToUpper(string input)
        {
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}