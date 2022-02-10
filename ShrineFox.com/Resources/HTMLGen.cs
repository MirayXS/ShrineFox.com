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
            // Forum Theme HTML
            string forumThemePath = $"{System.Web.Hosting.HostingEnvironment.MapPath("~/.")}//forum//styles//prolight//template";
            Directory.CreateDirectory(forumThemePath);

            // Overall Header
            string header = Properties.Resources.overall_header;
            header = header.Replace("<!--INDEXHEADER-->", Properties.Resources.home_head);
            header = header.Replace("<!--INDEXBEFORECONTENT-->", Properties.Resources.home_body.Replace("forumlink", "active"));
            File.WriteAllText(Path.Combine(forumThemePath, "overall_header.html"), header);
            // Overall Footer
            File.WriteAllText(Path.Combine(forumThemePath, "overall_footer.html"), Properties.Resources.overall_footer.Replace("<!--INDEXFOOTER-->", Properties.Resources.home_foot));
            // CSS
            var forumCssPath = forumThemePath.Replace("template", "theme");
            Directory.CreateDirectory(forumCssPath);
            File.WriteAllText(Path.Combine(forumCssPath, "colours.css"), Properties.Resources.colours);
            
            // Blog Theme HTML
            string blogThemePath = $"{System.Web.Hosting.HostingEnvironment.MapPath("~/.")}//blog//wp-content//themes//justread";
            string[] sites = new string[] { "blog", "guides", "news" };
            
            foreach (string site in sites)
            {
                string path = blogThemePath.Replace("blog", site);
                Directory.CreateDirectory(path);

                // Header
                header = Properties.Resources.header;
                header = header.Replace("<!--INDEXHEADER-->", Properties.Resources.home_head);
                header = header.Replace("<!--INDEXBEFORECONTENT-->", Properties.Resources.home_body.Replace($"{site}link", "active").Replace("articleslink", "active"));
                header = header.Replace("<!--MainNavigation-->", $"<a href=\"https://shrinefox.com\"><i class=\"fa fa-home\" aria-hidden=\"true\"></i> ShrineFox.com</a> <i class=\"fa fa-angle-right\" aria-hidden=\"true\"></i><a href=\"https://shrinefox.com/{site}\">{FirstCharToUpper(site)}</a>");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                File.WriteAllText(Path.Combine(path, "header.php"), header);
                // Footer
                File.WriteAllText(Path.Combine(path, "footer.php"), Properties.Resources.home_foot);
                // CSS
                File.WriteAllText(Path.Combine(path, "style.css"), Properties.Resources.style);
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