using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShrineFoxCom
{
    public class Asp
    {
        public static DropDownList DropDownList(string name, List<Tuple<string,string>> options)
        {
            // Create DropDownList
            var dropDownList = new DropDownList();
            dropDownList.ID = name;
            // Add items to DropDownList
            dropDownList.Items.Add(new ListItem() { Text = "", Value = "" });
            foreach (var option in options)
                dropDownList.Items.Add(new ListItem() { Text = option.Item1, Value = option.Item2 });
            // Create Postback Trigger
            dropDownList.AutoPostBack = true;
            dropDownList.CssClass = "form-select";

            return dropDownList;
        }

        public static Panel DropDownPanel(string name, string title, string subtitle, DropDownList ctrl, bool visible)
        {
            var panel = new Panel();
            panel.ID = name;
            panel.Controls.Add(new LiteralControl() { Text = Html.CardStart(title, subtitle) });
            panel.Controls.Add(new LiteralControl() { Text = "<div class=\"container\"><div class=\"columns\">" });
            ctrl.CssClass = "form-select column col-9";
            panel.Controls.Add(ctrl);
            panel.Controls.Add(new Button() { CssClass = "btn btn-primary float-right column col-3", Text = "Next" });
            panel.Controls.Add(new LiteralControl() { Text = "</div></div></div>" });
            panel.Controls.Add(new LiteralControl() { Text = Html.CardEnd() });
            panel.Visible = visible;

            return panel;
        }

        public static Panel CardPanel(string name, string title, string subtitle, bool visible)
        {
            var panel = new Panel();
            panel.ID = name;
            panel.Controls.Add(new LiteralControl() { Text = Html.CardStart(title, subtitle) });
            panel.Controls.Add(new Panel() { ID = name + "_content" });
            panel.Controls.Add(new LiteralControl() { Text = Html.CardEnd() });
            panel.Controls.Add(new Panel() { ID = name + "_footer" });
            panel.Controls.Add(new LiteralControl() { Text = Html.ContainerEnd() });
            panel.Visible = visible;

            return panel;
        }

        public static Panel SplitCardPanel(string name, string title, string subtitle, bool visible)
        {
            var panel = new Panel();
            panel.ID = name;
            panel.Controls.Add(new LiteralControl() { Text = Html.CardStart(title, subtitle) });
            panel.Controls.Add(new LiteralControl() { Text = $"{Html.ContainerStart()}{Html.ColumnStart(6)}" });
            panel.Controls.Add(new Panel() { ID = name + "_content_left" });
            panel.Controls.Add(new LiteralControl() { Text = $"{Html.ColumnEnd()}{Html.ColumnStart(6)}" });
            panel.Controls.Add(new Panel() { ID = name + "_content_right" });
            panel.Controls.Add(new LiteralControl() { Text = $"{Html.ColumnEnd()}{Html.ContainerEnd()}" });
            panel.Controls.Add(new LiteralControl() { Text = Html.CardEnd() });
            panel.Controls.Add(new Panel() { ID = name + "_footer" });
            panel.Controls.Add(new LiteralControl() { Text = Html.ContainerEnd() });
            panel.Visible = visible;

            return panel;
        }

        public static T GetControlByType<T>(Control root, Func<T, bool> predicate = null) where T : Control
        {
            if (root == null)
            {
                throw new ArgumentNullException("root");
            }

            var stack = new Stack<Control>(new Control[] { root });

            while (stack.Count > 0)
            {
                var control = stack.Pop();
                T match = control as T;

                if (match != null && (predicate == null || predicate(match)))
                {
                    return match;
                }

                foreach (Control childControl in control.Controls)
                {
                    stack.Push(childControl);
                }
            }

            return default(T);
        }

        public static List<Tuple<string, string>> ParseQueryString(string newQuery)
        {
            List<Tuple<string, string>> queries = new List<Tuple<string, string>>();

            foreach (var q in newQuery.Replace("?", "").Split('&'))
                queries.Add(new Tuple<string, string>(q.Split('=').First(), q.Split('=').Last()));

            return queries;
        }
    }

    public static class AspExtension
    {
        /// <summary>
        /// Gets the ID of the post back control.
        /// 
        /// See: http://geekswithblogs.net/mahesh/archive/2006/06/27/83264.aspx
        /// </summary>
        /// <param name = "page">The page.</param>
        /// <returns></returns>
        public static string GetPostBackControlId(this Page page)
        {
            if (!page.IsPostBack)
                return string.Empty;

            Control control = null;
            // first we will check the "__EVENTTARGET" because if post back made by the controls
            // which used "_doPostBack" function also available in Request.Form collection.
            string controlName = page.Request.Params["__EVENTTARGET"];
            if (!String.IsNullOrEmpty(controlName))
            {
                control = page.FindControl(controlName);
            }
            else
            {
                // if __EVENTTARGET is null, the control is a button type and we need to
                // iterate over the form collection to find it

                // ReSharper disable TooWideLocalVariableScope
                string controlId;
                Control foundControl;
                // ReSharper restore TooWideLocalVariableScope

                foreach (string ctl in page.Request.Form)
                {
                    // handle ImageButton they having an additional "quasi-property" 
                    // in their Id which identifies mouse x and y coordinates
                    if (ctl.EndsWith(".x") || ctl.EndsWith(".y"))
                    {
                        controlId = ctl.Substring(0, ctl.Length - 2);
                        foundControl = page.FindControl(controlId);
                    }
                    else
                    {
                        foundControl = page.FindControl(ctl);
                    }

                    if (!(foundControl is IButtonControl)) continue;

                    control = foundControl;
                    break;
                }
            }

            return control == null ? String.Empty : control.ID;
        }
    }
}