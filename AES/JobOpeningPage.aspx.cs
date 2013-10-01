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
    public partial class JobOpeningPage : System.Web.UI.Page
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //get jobs list and bind it to a gridview
                JobPositionSystemDAL jps = new JobPositionSystemDAL();
                DataSet ds = jps.GetJobs();
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            if (e.CommandName == "delete")
            {
                object idToDelete = e.CommandArgument;
                int ID = Convert.ToInt32(idToDelete);

                JobOpeningDAL app = new JobOpeningDAL();
                app.RemoveJobOpening(ID);

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Buttonjob_Click(object sender, EventArgs e)
        {
            string desc;
            string wage;
            int department_id;
            int has_been_filled;
            string location;
            string job_name;


            desc = this.TxtDescription.Text;
            wage = this.TxtWage.Text;
            department_id = Convert.ToInt32(this.TxtDepartmentID.Text);
            has_been_filled = Convert.ToInt32(this.TxtBeenFilled.Text);
            location = this.TxtLocation.Text;
            job_name = this.TxtJobName.Text;

            JobOpeningDAL app = new JobOpeningDAL();

            app.AddJobOpening(desc, wage, department_id, has_been_filled, location, job_name);
        }
    }
}