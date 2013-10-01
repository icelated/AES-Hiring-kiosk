using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using JobPositionSystem;



namespace AES
{
    public partial class JobOpeningDetails : System.Web.UI.Page
    {
        /// <summary>
        /// Grab the job application ID being passed in by query string
        /// and add the job to a dataset. Then, bind that dataset to a repeater to display
        /// the job details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            if(!IsPostBack)
           {

            JobPositionSystemDAL jps = new JobPositionSystemDAL();
            DataSet ds = jps.GetJobOpeningByID(Convert.ToInt32(Server.UrlDecode(Request.QueryString["JobApplicationID"])));
            RepeaterJobDetails.DataSource = ds;
            RepeaterJobDetails.DataBind();
            //register the javascript
            Page.ClientScript.RegisterStartupScript(this.GetType(),
                          "on", "DisplaySessionTimeout()", true);
        }


            }

        protected void Button1_Click(object sender, EventArgs e)
        {

            Response.Redirect("KioskHome.aspx");

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //Get selected JobID and pass it to the application page
           int test = Convert.ToInt32(Server.UrlDecode(Request.QueryString["JobApplicationID"]));
           Response.Redirect("JobOpeningApplication.aspx?JobApplicationID=" + test);
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