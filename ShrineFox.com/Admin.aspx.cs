using Humanizer;
using ShrineFox.com.Resources.Browse;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShrineFox.com
{
    public partial class Admin : Page
    {
        // Placeholders
        LiteralControl Notice = new LiteralControl();
        PlaceHolder Login = new PlaceHolder();
        PlaceHolder GameBanana = new PlaceHolder();
        PlaceHolder DiscordBot = new PlaceHolder();
        PlaceHolder Html = new PlaceHolder();
        // Controls
        TextBox loginTxtBox = new TextBox();
        Button loginBtn = new Button();
        LiteralControl gbTxt = new LiteralControl();
        Button gbBtn = new Button();
        LiteralControl botTxt = new LiteralControl();
        LiteralControl botStatus = new LiteralControl();
        Button botActiveBtn = new Button();
        LiteralControl htmlTxt = new LiteralControl();
        Button htmlBtn = new Button();

        DateTime lastWriteTime;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Sidebar
            LiteralControl SidebarHtml = new LiteralControl();
            SidebarHtml.Text = Properties.Resources.IndexSidebar.Replace("<!--Accordions-->", Properties.Resources.Browse + Properties.Resources.Apps);
            Sidebar.Controls.Add(SidebarHtml);

            // Get Login State

            object value = Session["loggedIn"];
            if (value == null || !this.IsPostBack)
                Session["loggedIn"] = false;
            else if (value != null)
                Session["loggedIn"] = (bool)value;
            #if DEBUG
                Session["loggedIn"] = true;
            #endif

            // Last Browse Update Time
            lastWriteTime = File.GetLastWriteTime($"{System.Web.Hosting.HostingEnvironment.MapPath("~/.")}//App_Data//amicitia.tsv");

            // Show login screen or controls if already logged in
            if (Convert.ToBoolean(Session["loggedIn"]) == true)
                AddControls(Placeholder);
            else
                LoginControls(Placeholder);

            // Events
            loginBtn.Click += new EventHandler(loginBtn_Click);
            gbBtn.Click += new EventHandler(gbBtn_Click);
            botActiveBtn.Click += new EventHandler(botActiveBtn_Click);
            htmlBtn.Click += new EventHandler(htmlBtn_Click);
        }

        public void LoginControls(PlaceHolder control)
        {
            control.Controls.Clear();

            // Login Textbox
            loginTxtBox.Attributes.Add("class", "form-input");
            loginTxtBox.TextMode = TextBoxMode.Password;
            Login.Controls.Add(loginTxtBox);
            // Login Button
            loginBtn.Text = "Login";
            loginBtn.Attributes.Add("class", "btn btn-primary");
            Login.Controls.Add(loginBtn);

            // Add Login Control to Page
            control.Controls.Add(Login);
        }

        protected void loginBtn_Click(object sender, EventArgs e)
        {
            Warning.Controls.Clear();
            if (loginTxtBox.Text != Properties.Resources.pw)
                Notice.Text = Post.Notice("red", "Login failed! Your credentials were incorrect, please try again.");
            else
            {
                Session["loggedIn"] = true;
                Notice.Text = Post.Notice("green", "Login successful.");
                AddControls(Placeholder);
            }
            Warning.Controls.Add(Notice);
        }

        private void AddControls(PlaceHolder control)
        {
            control.Controls.Clear();
            // Create Controls
            GBControls(GameBanana);
            BotControls(DiscordBot);
            HtmlControls(Html);
            // Add Controls to page 
            control.Controls.Add(GameBanana);
            control.Controls.Add(DiscordBot);
            control.Controls.Add(Html);
        }

        public void GBControls(PlaceHolder control)
        {
            control.Controls.Clear();

            gbTxt.Text = "<h2>GameBanana</h2>" +
                    "Click the button below to update the contents on <a href=\"https://shrinefox.com/browse\">shrinefox.com/browse</a>." +
                    $"<br><i class=\"fas fa-history\" aria-hidden=\"true\"></i> Updated {lastWriteTime.Humanize()}<br><br>";
            gbBtn.Text = "Update Posts";
            gbBtn.Attributes.Add("class", "btn btn-primary");

            control.Controls.Add(gbTxt);
            control.Controls.Add(gbBtn);
        }

        public void BotControls(PlaceHolder control)
        {
            control.Controls.Clear();

            botTxt.Text = "<h2>FrostBot</h2>" +
                    "Remotely configure <a href=\"https://shrinefox.com/frostbot\">FrostBot</a>, Amicitia's Discord bot.<br><br>";
            botActiveBtn.Text = "Activate";
            botActiveBtn.Attributes.Add("class", "btn btn-primary");

            control.Controls.Add(botTxt);
            control.Controls.Add(botStatus);
            control.Controls.Add(botActiveBtn);
        }

        public void HtmlControls(PlaceHolder control)
        {
            control.Controls.Clear();

            htmlTxt.Text = "<h2>Update HTML</h2>" +
                    "Remotely update forum/blog layout HTML.<br><br>";
            htmlBtn.Text = "Update HTML";
            htmlBtn.Attributes.Add("class", "btn btn-primary");

            control.Controls.Add(htmlTxt);
            control.Controls.Add(botStatus);
            control.Controls.Add(htmlBtn);
        }

        private void gbBtn_Click(object sender, EventArgs e)
        {
            // Update TSVs
            Webscraper.UpdateTSVs(Warning);
        }

        private void botActiveBtn_Click(object sender, EventArgs e)
        {
            /*LiteralControl notice = new LiteralControl();
            if (!FrostBot.Program.active)
            {
                FrostBot.Program.Main();
                notice.Text = Post.Notice("green", "<b>Bot Activated</b> successfully!");
            }
            else
            {
                FrostBot.Program.Close();
                notice.Text = Post.Notice("red", "<b>Bot Deactivated</b>.");
            }

            botStatus.Controls.Clear();
            botStatus.Controls.Add(notice);*/
        }

        private void htmlBtn_Click(object sender, EventArgs e)
        {
            // Update Blog/Forum HTML
            HTMLGen.BlogForum(Warning);
        }
    }
}