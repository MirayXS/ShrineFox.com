using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShrineFoxCom;

namespace ShrineFoxCom
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Head Tags
            LiteralControl HeadHtml = new LiteralControl();
            HeadHtml.Text = Properties.Resources.home_head;
            #if DEBUG
                HeadHtml.Text = HeadHtml.Text.Replace("https://shrinefox.com/", "./"); // Use relative path
            #endif
            Head.Controls.Add(HeadHtml);

            // Body
            LiteralControl BodyHtml = new LiteralControl();
            BodyHtml.Text = Properties.Resources.home_body;
            // Highlight Current Page in Navbar
            switch (this.Page.Title)
            {
                case "Home Page":
                    BodyHtml.Text = BodyHtml.Text.Replace("text-dark home", "active");
                    break;
                case "Browse":
                    BodyHtml.Text = BodyHtml.Text.Replace("text-dark browse", "active");
                    break;
                case "Articles":
                    BodyHtml.Text = BodyHtml.Text.Replace("text-dark articles", "active");
                    break;
                case "Web Apps":
                    BodyHtml.Text = BodyHtml.Text.Replace("text-dark apps", "active");
                    break;
                case "Patch Creator":
                    BodyHtml.Text = BodyHtml.Text.Replace("text-dark patchcreator", "active");
                    BodyHtml.Text = BodyHtml.Text.Replace("text-dark apps", "active");
                    break;
                case "Update Creator":
                    BodyHtml.Text = BodyHtml.Text.Replace("text-dark updatecreator", "active");
                    BodyHtml.Text = BodyHtml.Text.Replace("text-dark apps", "active");
                    break;
                case "Text Search":
                    BodyHtml.Text = BodyHtml.Text.Replace("text-dark textsearch", "active");
                    BodyHtml.Text = BodyHtml.Text.Replace("text-dark apps", "active");
                    break;
                case "Files":
                    BodyHtml.Text = BodyHtml.Text.Replace("text-dark files", "active");
                    BodyHtml.Text = BodyHtml.Text.Replace("text-dark apps", "active");
                    break;
                case "About":
                    BodyHtml.Text = BodyHtml.Text.Replace("text-dark about", "active");
                    break;
            }
            #if DEBUG
                BodyHtml.Text = BodyHtml.Text.Replace("https://shrinefox.com/images/", "./images/"); // Use relative path
            #endif
            Body.Controls.Add(BodyHtml);

            // Footer
            LiteralControl FootHtml = new LiteralControl();
            FootHtml.Text = Properties.Resources.home_foot;
            Foot.Controls.Add(FootHtml);
        }
    }
}