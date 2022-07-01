using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ShrineFoxCom
{
    public partial class DynamicCtrls : Page
    {
        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["count"] = 1;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            generateDynamicControls();
        }

        protected void btnRead_Click(object sender, EventArgs e)
        {
            switch (Convert.ToString(ViewState["control"]).ToLower())
            {
                case "textbox":
                    TextBox txt = (TextBox)PlaceHolder1.FindControl("TextBox");
                    lblValue.Text = txt.Text;
                    break;
                case "dropdownlist":
                    DropDownList ddl = (DropDownList)PlaceHolder1.FindControl("DropDownList");
                    lblValue.Text = ddl.SelectedValue;
                    break;
                case "radiobuttonlist":
                    string value = "";
                    RadioButtonList RadioButtonList = (RadioButtonList)PlaceHolder1.FindControl("RadioButtonList");
                    foreach (ListItem item in RadioButtonList.Items)
                    {
                        if (item.Selected)
                            value = value + item.Text + "<br>";
                    }
                    lblValue.Text = value;
                    break;
                case "checkboxlist":
                    string val = "";
                    CheckBoxList CheckBoxList = (CheckBoxList)PlaceHolder1.FindControl("CheckBoxList");
                    foreach (ListItem item in CheckBoxList.Items)
                    {
                        if (item.Selected)
                            val = val + item.Text + "<br>";
                    }
                    lblValue.Text = val;
                    break;
                default:
                    break;
            }
        }

        public void ddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ddl = (DropDownList)sender;
            if (ddl.SelectedIndex > 0)
            {
                lblValue.Text = ddl.SelectedValue;
            }
        }

        #endregion

        #region Dynamic Methods

        public void generateDynamicControls()
        {
            if (ddlDynamicControlList.SelectedIndex > 0)
            {
                lblValue.Text = string.Empty;
                switch (ddlDynamicControlList.SelectedValue.ToString().ToLower())
                {
                    case "textbox":
                        ViewState["control"] = "textbox";
                        createDynamicTextBox("TextBox");
                        break;
                    case "dropdownlist":
                        ViewState["control"] = "dropdownlist";
                        createDynamicDropDownList("DropDownList");
                        break;
                    case "radiobuttonlist":
                        ViewState["control"] = "radiobuttonlist";
                        createDynamicRadioButtonList("RadioButtonList");
                        break;
                    case "checkboxlist":
                        ViewState["control"] = "checkboxlist";
                        createDynamicCheckBoxList("CheckBoxList");
                        break;
                    default:
                        break;
                }
            }
        }

        public void createDynamicTextBox(string _TextBoxId)
        {
            HtmlGenericControl tr = new HtmlGenericControl("tr");
            HtmlGenericControl td1 = new HtmlGenericControl("td");

            Label lbl = new Label();
            lbl.ID = "lbl" + _TextBoxId.Replace(" ", "").ToLower();
            lbl.Text = _TextBoxId;
            td1.Controls.Add(lbl);
            tr.Controls.Add(td1);

            HtmlGenericControl td2 = new HtmlGenericControl("td");
            TextBox txtBox = new TextBox();
            txtBox.ID = _TextBoxId.Replace(" ", "").ToLower();
            td2.Controls.Add(txtBox);
            tr.Controls.Add(td2);
            PlaceHolder1.Controls.Add(tr);
        }

        public void createDynamicDropDownList(string _ddlId)
        {
            HtmlGenericControl tr = new HtmlGenericControl("tr");
            HtmlGenericControl td1 = new HtmlGenericControl("td");

            Label lbl = new Label();
            lbl.ID = "ddl" + _ddlId.Replace(" ", "").ToLower();
            lbl.Text = _ddlId;
            td1.Controls.Add(lbl);
            tr.Controls.Add(td1);

            HtmlGenericControl td2 = new HtmlGenericControl("td");
            DropDownList ddl = new DropDownList();
            ddl.ID = _ddlId.Replace(" ", "").ToLower();
            ddl.SelectedIndexChanged += ddl_SelectedIndexChanged;
            ddl.AutoPostBack = true;
            ddl.Items.Add(new ListItem("-- Select --", "-- Select --"));
            ddl.Items.Add(new ListItem("DropDown Option One", "Option One"));
            ddl.Items.Add(new ListItem("DropDown Option Two", "Option Two"));
            ddl.Items.Add(new ListItem("DropDown Option Three", "Option Three"));
            td2.Controls.Add(ddl);
            tr.Controls.Add(td2);
            PlaceHolder1.Controls.Add(tr);
        }

        public void createDynamicRadioButtonList(string _RadioButtonListID)
        {
            HtmlGenericControl tr = new HtmlGenericControl("tr");
            HtmlGenericControl td1 = new HtmlGenericControl("td");

            Label lbl = new Label();
            lbl.ID = "lbl" + _RadioButtonListID.Replace(" ", "").ToLower();
            lbl.Text = _RadioButtonListID;
            td1.Controls.Add(lbl);
            tr.Controls.Add(td1);

            HtmlGenericControl td2 = new HtmlGenericControl("td");
            RadioButtonList RadioButtonList = new RadioButtonList();
            RadioButtonList.ID = _RadioButtonListID.Replace(" ", "").ToLower();
            RadioButtonList.Items.Add(new ListItem("Radio Option One", "Option One"));
            RadioButtonList.Items.Add(new ListItem("Radio Option Two", "Option Two"));
            RadioButtonList.Items.Add(new ListItem("Radio Option Three", "Option Three"));
            td2.Controls.Add(RadioButtonList);
            tr.Controls.Add(td2);
            PlaceHolder1.Controls.Add(tr);
        }

        public void createDynamicCheckBoxList(string _CheckBoxListID)
        {
            HtmlGenericControl tr = new HtmlGenericControl("tr");
            HtmlGenericControl td1 = new HtmlGenericControl("td");

            Label lbl = new Label();
            lbl.ID = "Check Box List" + _CheckBoxListID.Replace(" ", "").ToLower();
            lbl.Text = _CheckBoxListID;
            td1.Controls.Add(lbl);
            tr.Controls.Add(td1);

            HtmlGenericControl td2 = new HtmlGenericControl("td");
            CheckBoxList CheckBoxList = new CheckBoxList();
            CheckBoxList.ID = _CheckBoxListID.Replace(" ", "").ToLower();
            CheckBoxList.Items.Add(new ListItem("CheckBox Option One", "Option One"));
            CheckBoxList.Items.Add(new ListItem("CheckBox Option Two", "Option Two"));
            CheckBoxList.Items.Add(new ListItem("CheckBox Option Three", "Option Three"));
            td2.Controls.Add(CheckBoxList);
            tr.Controls.Add(td2);
            PlaceHolder1.Controls.Add(tr);
        }

        #endregion
    }
}