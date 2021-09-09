using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShrineFox.com
{
    public partial class Files : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Sidebar
            LiteralControl SidebarHtml = new LiteralControl();
            SidebarHtml.Text = Properties.Resources.IndexSidebar.Replace("<!--Accordions-->", Properties.Resources.Browse + Properties.Resources.Apps.Replace("fileslink", "active"));
            Sidebar.Controls.Add(SidebarHtml);

            Page.MaintainScrollPositionOnPostBack = true;
        }
    }
}