﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PersonaGameLib;
using ShrineFoxCom;

namespace ShrineFoxCom
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string head = File.ReadAllText(Server.MapPath("~/./App_Data/Resources/Html/head.html"));
            string body = File.ReadAllText(Server.MapPath("~/./App_Data/Resources/Html/body.html"));
            string footer = File.ReadAllText(Server.MapPath("~/./App_Data/Resources/Html/footer.html"));

            // Head Tags
            LiteralControl HeadHtml = new LiteralControl();
            HeadHtml.Text = head;
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