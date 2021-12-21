using Humanizer;
using ShrineFoxCom.Resources.Browse;
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

namespace ShrineFoxCom
{
    public partial class Admin : Page
    {
        // Placeholders
        LiteralControl Notice = new LiteralControl();
        PlaceHolder Login = new PlaceHolder();
        PlaceHolder GameBanana = new PlaceHolder();
        PlaceHolder DiscordBot = new PlaceHolder();
        PlaceHolder Html = new PlaceHolder();
        PlaceHolder PostForm = new PlaceHolder();
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
        // New Browse Post form
        Label postIdLbl = new Label();
        Label postTitleLbl = new Label();
        Label postGamesLbl = new Label();
        Label postTypeLbl = new Label();
        Label postAuthorsLbl = new Label();
        Label postDateLbl = new Label();
        Label postDescriptionLbl = new Label();
        Label postTagsLbl = new Label();
        Label postDownloadLbl = new Label();
        Label postThumbUrlLbl = new Label();
        LiteralControl postTxt = new LiteralControl();
        TextBox postIdTxt = new TextBox();
        TextBox postTitleTxt = new TextBox();
        TextBox postGamesTxt = new TextBox();
        TextBox postTypeTxt = new TextBox();
        TextBox postAuthorsTxt = new TextBox();
        TextBox postDateTxt = new TextBox();
        TextBox postDescriptionTxt = new TextBox();
        TextBox postTagsTxt = new TextBox();
        TextBox postDownloadTxt = new TextBox();
        TextBox postThumbUrlTxt = new TextBox();
        Button submitPostBtn = new Button();

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
            submitPostBtn.Click += new EventHandler(submitPostBtn_Click);
        }

        private void submitPostBtn_Click(object sender, EventArgs e)
        {
            Warning.Controls.Clear();
            if (postIdTxt.Text != "" && postTypeTxt.Text != "" && postTitleTxt.Text != "" && postGamesTxt.Text != "" && postAuthorsTxt.Text != "" 
                && postDateTxt.Text != "" && postDownloadTxt.Text != "")
            {
                File.AppendAllText($"{System.Web.Hosting.HostingEnvironment.MapPath("~/.")}//App_Data//amicitia.tsv", $"\n{postIdTxt.Text}\t{postTypeTxt.Text}\t{postTitleTxt.Text}\t{postGamesTxt.Text}\t{postAuthorsTxt.Text}\t{postDateTxt.Text}\t{postTagsTxt.Text}\t{postDescriptionTxt.Text}\t\t{postThumbUrlTxt.Text}\t{postDownloadTxt.Text}\t");
                Notice.Text = Post.Notice("green", "Updated TSV! Refresh the Browse page to see changes.");
            }
            else
                Notice.Text = Post.Notice("red", "Failed to update TSV because one or more text fields were blank.");
            Warning.Controls.Add(Notice);
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
            string password = File.ReadAllText($"{System.Web.Hosting.HostingEnvironment.MapPath("~/.")}//App_Data//pw.txt");
            if (loginTxtBox.Text != password)
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
            PostControls(PostForm);
            // Add Controls to page 
            control.Controls.Add(GameBanana);
            control.Controls.Add(DiscordBot);
            control.Controls.Add(Html);
            control.Controls.Add(PostForm);
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

        public void PostControls(PlaceHolder control)
        {
            control.Controls.Clear();

            postTxt.Text = "<h2>Add Browse Post</h2>" +
                    "Remotely create a new Browse entry.<br><br>";
            submitPostBtn.Text = "Add Post";
            submitPostBtn.Attributes.Add("class", "btn btn-primary");
            postIdLbl.Text = "Post ID:";
            postTitleLbl.Text = "Title:";
            postGamesLbl.Text = "Games:";
            postTypeLbl.Text = "Type:";
            postAuthorsLbl.Text = "Authors:";
            postDateLbl.Text = "Date:";
            postDescriptionLbl.Text = "Description:";
            postTagsLbl.Text = "Tags:";
            postDownloadLbl.Text = "Download:";
            postThumbUrlLbl.Text = "Thumbnail:";
            postIdTxt.Attributes.Add("class", "form-input");
            postTitleTxt.Attributes.Add("class", "form-input");
            postGamesTxt.Attributes.Add("class", "form-input");
            postTypeTxt.Attributes.Add("class", "form-input");
            postAuthorsTxt.Attributes.Add("class", "form-input");
            postDateTxt.Attributes.Add("class", "form-input");
            postDescriptionTxt.Attributes.Add("class", "form-input");
            postTagsTxt.Attributes.Add("class", "form-input");
            postDownloadTxt.Attributes.Add("class", "form-input");
            postThumbUrlTxt.Attributes.Add("class", "form-input");

            control.Controls.Add(postTxt);
            control.Controls.Add(postIdLbl); control.Controls.Add(postIdTxt);
            control.Controls.Add(postTitleLbl); control.Controls.Add(postTitleTxt);
            control.Controls.Add(postGamesLbl); control.Controls.Add(postGamesTxt);
            control.Controls.Add(postTypeLbl); control.Controls.Add(postTypeTxt);
            control.Controls.Add(postAuthorsLbl); control.Controls.Add(postAuthorsTxt);
            control.Controls.Add(postDateLbl); control.Controls.Add(postDateTxt);
            control.Controls.Add(postDescriptionLbl); control.Controls.Add(postDescriptionTxt);
            control.Controls.Add(postTagsLbl); control.Controls.Add(postTagsTxt);
            control.Controls.Add(postDownloadLbl); control.Controls.Add(postDownloadTxt);
            control.Controls.Add(postThumbUrlLbl); control.Controls.Add(postThumbUrlTxt);
            control.Controls.Add(submitPostBtn);
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