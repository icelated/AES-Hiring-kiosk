using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JobPositionSystem;
using System.Data;
using System.Text;
using JobOpeningSystem;

namespace AES
{
    public partial class JobOpeningApplication : System.Web.UI.Page
    {
        List<Control> _controlsList;
        List<String> _session;         //this holds the session
        List<JobApplicationField> _fields;
       // List<JobApplication> _application;
        JobApplication _application;//this shit holds all genericquestions and answers
        JobApplicant _applicant;//this shit holds all jobquestions and answers


        JobApplicantSession _sessions = new JobApplicantSession();   //set an object to the session class

       

        protected void Page_Load(object sender, EventArgs e)
        {
            

            _controlsList = new List<Control>();     // object holds the controls
            _session = new List<string>();            // object holds the session    
            _fields = new List<JobApplicationField>();
            int jobID = Convert.ToInt32(Server.UrlDecode(Request.QueryString["JobApplicationID"])); //get passed in jobID of selected job
      
            _application = new JobApplication(jobID);
            _application.PopulateFromDatabase();

            _applicant = new JobApplicant();
            _applicant.PopulateFromDatabase();
            _fields = _applicant.Fields.Concat(_application.Fields).ToList();

            //call the fi=unction to create the html table and controls
            GenerateTable();

            //register the javascript session
            Page.ClientScript.RegisterStartupScript(this.GetType(),
                          "on", "DisplaySessionTimeout()", true);
        }

        /// <summary>
        /// create a dynamic html table
        /// and add the controls to it
        /// Add controls list toa ist
        /// </summary>
        private void GenerateTable()
        {
            foreach (JobApplicationField field in _fields)
            {
                TextBox textbox = new TextBox();

                TableRow tableRow = new TableRow();
                TableCell tableCell = new TableCell();
                TableCell tableCell2 = new TableCell();

                tableCell.Text = field.FieldText;


                tableCell2.Controls.Add(textbox);
                _controlsList.Add(textbox);

                tableRow.Cells.Add(tableCell);
                tableRow.Cells.Add(tableCell2);
                myTable.Rows.Add(tableRow);
            }
        }

        protected void RepeaterApplication_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void RepeaterApplication_ItemCommand1(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void RepeaterQuestions_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("KioskHome.aspx");
        }

        /// <summary>
        /// Validation checks
        /// If pass redirect to acceptance page
        /// If fail redirect to rejection page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _fields.Count; i++)
            {
                if (_fields[i].HasCorrectAnswer == true && ((_controlsList[i] as TextBox).Text == ""))
                { // Response.Redirect("JobApplicationRejected.aspx");
                    lblError.Text = "Failed to enter data into required fields.";
                    return;
                }
                //if required field and blank
                if (_fields[i].AnswerRequired == true && ((_controlsList[i] as TextBox).Text == ""))
                {
                    lblError.Text = "Failed to enter data into required fields.";
                    return;
                }


                if (i < _applicant.Fields.Count)
                    _applicant.AddAnswer((_controlsList[i] as TextBox).Text);
                else
                    _application.AddAnswer((_controlsList[i] as TextBox).Text);
            }
            if (_application.Verify() == false)
                Response.Redirect("JobApplicationRejected.aspx");
            if (_applicant.Verify() == false)
                Response.Redirect("JobApplicationRejected.aspx");
            //add the approved applicant to a session.
            _sessions.ApplicationSession = _application;

            //retrieve session application info
           // List<JobApplication> applications = new List<JobApplication>();
            JobApplication application;
            var jas = new JobApplicantSession();
            application = jas.ApplicationSession; //holds the answers

          

            List<String> answersfromsession = application.Answers;
           // JobApplicantDAL obj = new JobApplicantDAL();
            //obj.AddJobApplication(answersfromsession);

            Response.Redirect("JobApplicationSubmitted.aspx");
            
        }


        protected void Page_Init(object sender, EventArgs e)
        {
        }

    }
}