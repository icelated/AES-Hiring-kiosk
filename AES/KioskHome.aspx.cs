using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JobPositionSystem;
using System.Data;
using JobOpeningSystem;



namespace AES
{
    public partial class KioskHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //get jobs list and bind it to a gridview
                JobPositionSystemDAL jps = new JobPositionSystemDAL();
                DataSet ds = jps.GetJobs();

                GridView2.DataSource = ds;
                GridView2.DataBind();
                //start a session
                Session["ASP.NET_SessionId"] = 1;

                // TEST
              // JobApplicantDAL app = new JobApplicantDAL();//test
               // JobOpeningDAL app = new JobOpeningDAL();

               // app.AddJobOpening("wow", "lol", 2, 0, "OR", "monkey");
               // app.RemoveJobOpening(64);

              // app.AddJobApplication( "Bananna peeler", "1 monkey", 2, 0, "WA", "Bananna man");
                
            }

        }
    }
}