using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShrineFoxCom
{
    public partial class UpdateCreatorRedirect : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("https://shrinefox.com/GetStarted");
        }
    }
}