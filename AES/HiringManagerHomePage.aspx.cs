using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AES
{
    public partial class HiringManagerHomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void HomeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("KioskHome.aspx");
        }

        protected void JobOpeningButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("JobOpeningPage.aspx");
        }
    }
}