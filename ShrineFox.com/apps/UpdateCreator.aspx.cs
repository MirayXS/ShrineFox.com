using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShrineFoxcom
{
    public partial class UpdateCreator : Page
    {
        CheckBoxList cbList = new CheckBoxList();
        string titleID = "";
        string path = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            // Sidebar
            LiteralControl SidebarHtml = new LiteralControl();
            SidebarHtml.Text = Properties.Resources.IndexSidebar.Replace("<!--Accordions-->", Properties.Resources.Browse + Properties.Resources.Apps.Replace("ps4patchlink", "active"));
            Sidebar.Controls.Add(SidebarHtml);
        }

        protected void SelectionChanged()
        {
            if (radio_CUSA17416.Checked)
                titleID = "CUSA17416";
            else
                titleID = "CUSA17419";

            RadioButton selRB = radioButtonsContainer2.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked);
            if (selRB != null)
                path = selRB.ToolTip;
        }


        protected void Download_Click(object sender, EventArgs e)
        {
            // Ensure valid options selected
            SelectionChanged();
            FileInfo fileInfo = new FileInfo(System.AppDomain.CurrentDomain.BaseDirectory + $"ppp\\{path}\\eboot.bin");

            //Download file
            if (File.Exists(fileInfo.FullName))
            {
                Response.Clear();
                Response.BufferOutput = false; // for large files...
                System.Web.HttpContext c = System.Web.HttpContext.Current;
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("content-disposition", "attachment;filename=" + "eboot.bin");
                Response.Flush();
                Response.WriteFile(fileInfo.FullName);
                Response.Close();
            }
        }

        protected void Select_Click(object sender, EventArgs e)
        {
            foreach (ListItem li in cbList.Items)
                li.Selected = true;
        }

        protected void Deselect_Click(object sender, EventArgs e)
        {
            foreach (ListItem li in cbList.Items)
                li.Selected = false;
        }
    }
}