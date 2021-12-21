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
            // Header
            LiteralControl HeaderHtml = new LiteralControl();
            HeaderHtml.Text = Properties.Resources.IndexHeader;
            #if DEBUG
                HeaderHtml.Text = HeaderHtml.Text.Replace("https://shrinefox.com/","./../");
            #endif

            // Ensure path to resources is correct
            if (Request.Url.AbsoluteUri.ToLower().Contains("/apps"))
                HeaderHtml.Text = HeaderHtml.Text;
            else
                HeaderHtml.Text = HeaderHtml.Text;

            // Body Before Content
            LiteralControl BeforeContentHtml = new LiteralControl();
            BeforeContentHtml.Text = Properties.Resources.IndexBeforeContent;
            // Ensure path to resources is correct
            if (Request.Url.AbsoluteUri.ToLower().Contains("/apps"))
                BeforeContentHtml.Text = BeforeContentHtml.Text;
            else
                BeforeContentHtml.Text = BeforeContentHtml.Text;
            // Mark active section in navbar
            if (Request.Url.AbsoluteUri.ToLower().Contains("/browse"))
                BeforeContentHtml.Text = BeforeContentHtml.Text.Replace("browselink","active");
            else if (Request.Url.AbsoluteUri.ToLower().Contains("/about"))
                BeforeContentHtml.Text = BeforeContentHtml.Text.Replace("aboutlink", "active");
            else
                BeforeContentHtml.Text = BeforeContentHtml.Text.Replace("homelink", "active");

            // Footer
            LiteralControl FooterHtml = new LiteralControl();
            FooterHtml.Text = Properties.Resources.IndexFooter;


            Header.Controls.Add(HeaderHtml);
            BeforeContent.Controls.Add(BeforeContentHtml);
            Footer.Controls.Add(FooterHtml);
        }
    }
}