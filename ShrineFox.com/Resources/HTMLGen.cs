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
            string forumThemePath = $"{System.Web.Hosting.HostingEnvironment.MapPath("~/.")}//forum//styles//Milk_v2";

            Directory.CreateDirectory($"{forumThemePath}//theme");
            Directory.CreateDirectory($"{forumThemePath}//template");

            // Forum Header
            string forumHeader = Properties.Resources.overall_header;
            string forumStyles = Between(Properties.Resources.home_head, "<!--ShrineFox Styles-->", "<!--End ShrineFox Styles-->");
            string videoSearch = Between(Properties.Resources.home_body, "<!--ShrineFox Header-->", "<!--End ShrineFox Header-->");
            forumHeader = forumHeader.Replace("<!--ShrineFox Styles-->", forumStyles)
                           .Replace("<!--ShrineFox Header-->", videoSearch);
            File.WriteAllText($"{forumThemePath}//template//overall_header.html", forumHeader);

            // Forum Footer
            string footer = Between(Properties.Resources.home_foot, "<!--ShrineFox Footer-->", "<!--End ShrineFox Footer-->");
            File.WriteAllText($"{forumThemePath}//template//overall_footer.html", 
                Properties.Resources.overall_footer.Replace("<!--ShrineFox Footer-->", footer));

            // Forum CSS
            File.WriteAllText($"{forumThemePath}//theme//colours.css", Properties.Resources.colours);
            
            // Blog Theme
            string blogThemePath = $"{System.Web.Hosting.HostingEnvironment.MapPath("~/.")}//blog//wp-content//themes//primer";

            string blogStyles = Between(Properties.Resources.home_head, "<!--ShrineFox AllStyles-->", "<!--End ShrineFox Styles-->");
            string blogNavbar = Between(Properties.Resources.home_body, "<!--ShrineFox NavBar-->", "<!--End ShrineFox NavBar-->");
            string blogHeader = Properties.Resources.header
                               .Replace("<!--ShrineFox Styles-->", blogStyles)
                               .Replace("<!--ShrineFox NavBar-->", blogNavbar)
                               .Replace("<!--ShrineFox Header-->", videoSearch);

            foreach (string site in new string[] { "blog", "guides", "news" })
            {
                string path = blogThemePath.Replace("blog", site);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                // Header
                File.WriteAllText(Path.Combine(path, "header.php"), blogHeader);
                // Footer
                File.WriteAllText(Path.Combine(path, "footer.php"), Properties.Resources.footer.Replace("<!--ShrineFox Footer-->", footer));
                // CSS
                File.WriteAllText(Path.Combine(path, "style.css"), Properties.Resources.style);
            }

            LiteralControl notice = new LiteralControl();
            notice.Text = Post.Notice("green", "<b>Success</b>! Forum/Blog HTML has been updated. Clear caches to see changes.");
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