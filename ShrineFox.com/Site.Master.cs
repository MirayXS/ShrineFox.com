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
            string head = GetFile.FromPath("./App_Data/Resources/Html/head.html");
            string body = GetFile.FromPath("./App_Data/Resources/Html/body.html");
            string footer = GetFile.FromPath("./App_Data/Resources/Html/footer.html");

            // Head Tags
            LiteralControl HeadHtml = new LiteralControl();
            HeadHtml.Text = head;
            #if DEBUG
                // Use relative path
                HeadHtml.Text = HeadHtml.Text.Replace("https://shrinefox.com/", "./");
            #endif
            Head.Controls.Add(HeadHtml);

            // Body
            LiteralControl BodyHtml = new LiteralControl();
            BodyHtml.Text = body;
            Body.Controls.Add(BodyHtml);

            // Footer
            LiteralControl FootHtml = new LiteralControl();
            FootHtml.Text = footer;
            Footer.Controls.Add(FootHtml);
        }
    }
}