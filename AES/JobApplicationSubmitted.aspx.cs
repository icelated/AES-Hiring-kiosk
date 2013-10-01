using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using JobOpeningSystem;
using JobPositionSystem;

namespace AES
{
    public partial class JobApplicationSubmitted : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              //TEST SESSION
                /*
                JobApplication application;
              JobApplicantSession sess = new JobApplicantSession();
              application = sess.ApplicationSession;
                */
                //register the javascript
              Page.ClientScript.RegisterStartupScript(this.GetType(),
                        "on", "DisplaySessionTimeout()", true);
               
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