using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AES
{
    public partial class JobApplicationRejected : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("KioskHome.aspx");
        }


        protected void Page_Init(object sender, EventArgs e)
        {
           // CheckSession();
        }


        private void CheckSession()
        {
            if (Session["ASP.NET_SessionId"] == null)
            {
                Response.Redirect("KioskHome.aspx");
            }
        }


    }
}