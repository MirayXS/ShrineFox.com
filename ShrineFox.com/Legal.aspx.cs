using Humanizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShrineFoxCom
{
    public partial class Legal : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Show last updated time
            var lastWriteTime = File.GetLastWriteTime($"{System.Web.Hosting.HostingEnvironment.MapPath("~/.")}//Legal.aspx");
            lastUpdated.Controls.Add(new LiteralControl { Text = $"<i class=\"fas fa-history\" aria-hidden=\"true\"></i> Updated {lastWriteTime.Humanize()}" });
        }
    }
}