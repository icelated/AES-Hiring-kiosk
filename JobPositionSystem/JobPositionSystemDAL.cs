using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using System.Data.SqlClient;
namespace JobPositionSystem
{
    /// <summary>
    /// This class is used for adding and returning fields to and from a job application. Connnections to the database are automatically handled in the constructor and destructor.
    /// </summary>
    public class JobPositionSystemDAL
    {
        private const string _connectionstring = "Data Source=(DESCRIPTION="
             + "(ADDRESS=(PROTOCOL=TCP)(HOST=JESSE.VIOLATES.US)(PORT=1521))"
             + "(CONNECT_DATA=(SERVICE_NAME=XE)));"
             + "User Id=SYSTEM;Password=stupid;";
        private OracleConnection _connection;
        private int _jobid;
        /// <summary>
        /// Create connection to the database
        /// </summary>
        public JobPositionSystemDAL()
        {
            try
            {
                _connection = new OracleConnection();
                _connection.ConnectionString = _connectionstring;
                _connection.Open();
            }
            catch (Exception e)
            {
                throw new Exception("Error connecting to SQL server.hi " + Environment.NewLine + e.Message);
            }
        }
        /// <summary>
        /// Runs SQL commands that are not queries (ie you should expect nothing back)
        /// </summary>
        /// <param name="sql">SQL command to send</param>
        private void RunNonQuery(string sql)
        {
            OracleCommand cmd;

            try
            {
                cmd = new OracleCommand(sql, _connection);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception("Error in sending SQL command to server. Check your parameters." + Environment.NewLine + e.Message);
            }

        }
        /// <summary>
        /// Runs SQL query, returns DataSet object with results
        /// </summary>
        /// <param name="sql">SQL to run</param>
        /// <returns>DataSet object with results</returns>
        private DataSet RunQuery(string sql)
        {
            OracleCommand cmd;
            OracleDataReader dr;
            OracleDataAdapter da;
            OracleCommandBuilder cb;
            DataSet ds;

            try
            {
                cmd = new OracleCommand(sql, _connection);
                cmd.CommandType = System.Data.CommandType.Text;
                dr = cmd.ExecuteReader();
                da = new OracleDataAdapter(cmd);
                cb = new OracleCommandBuilder(da);
                ds = new DataSet();
                dr.Close();
            }
            catch (Exception e)
            {
                throw new Exception("Error in sending SQL command to server. Check your parameters." + Environment.NewLine + e.Message);
            }

            da.Fill(ds);
            return ds;
        }
        /// <summary>
        /// This function opens the JobOpening specified by JobID, and returns a DataSet containing columns
        /// JobApplicationID, JobDescription, Wage, DepartmentID, HasBeenFilled, Location, JobName in that order
        /// </summary>
        /// <param name="JobID">The JobID of the Job you want to connect to.</param>
        /// <returns>DataSet with JobQuestions for JobID</returns>
        public DataSet OpenJobOpeningByID(int JobID)
        {
            _jobid = JobID;
            string sql = " select* from JOBQUESTIONS where JOBAPPLICATIONID = " + _jobid;
            return RunQuery(sql);
        }

            /// <summary>
        /// Gets all generic questions from the databse, returning QuestionID, QuestionText
        /// ,TypeID, HasCorrectAnswer, CorrectAnswer, IsRequired
        /// </summary>
        /// <returns>Dataset with columns from GenericQuestions table</returns>
        public DataSet GetGenericQuestions()
        {
            string sql = "select * from GENERICQUESTIONS";
            return RunQuery(sql);
        }
        public JobApplication OpenJobApplication(int JobID)
        {
            _jobid = JobID;
            //string sql = " select* from JOBQUESTIONS where JOBAPPLICATIONID = " + _jobid;
            //DataSet set = RunQuery(sql);
            DataSet set = OpenJobOpeningByID(JobID);
            DataTable table = set.Tables[0];
            JobApplication app = new JobApplication(JobID);
            foreach (DataRow row in table.Rows)
            {
                int questionID = Int32.Parse(row["QUESTIONID"].ToString());
                //int jobApplicationID = Int32.Parse(row["JOBAPPLICATIONID"] as String);
                string questionText = row["QUESTIONTEXT"] as String;
                int type = Int32.Parse(row["TYPEID"].ToString());
                bool hasCorrectAnswer = (row["HASCORRECTANSWER"] as String == "0") ? false : true;
                string correctAnswer = row["CORRECTANSWER"] as String;
                bool required = ((row["ISREQUIRED"] as String) == "0") ? false : true;

                JobApplicationField field = new JobApplicationField(questionID, questionText, hasCorrectAnswer, correctAnswer, required);
                app.AddField(field);
            }

            //append generic questions
            /*set = GetGenericQuestions();
            table = set.Tables[0];
            foreach (DataRow row in table.Rows)
            {
                int questionID = Int32.Parse(row["QUESTIONID"].ToString());
                string questionText = row["QUESTIONTEXT"] as String;
                int type = Int32.Parse(row["TYPEID"].ToString());
                bool hasCorrectAnswer = (row["HASCORRECTANSWER"] as String == "0") ? false : true;
                string correctAnswer = row["CORRECTANSWER"] as String;
                bool required = ((row["ISREQUIRED"] as String) == "0") ? false : true;

                JobApplicationField field = new JobApplicationField(questionID, questionText, hasCorrectAnswer, correctAnswer, required);
                app.AddField(field);
            }*/

            return app;
        }

        /// <summary>
        /// This function gets the job details by jobID being passed in.
        /// Will return job application ID, job description, wage, loation name, etc...
        /// </summary>
        /// <param name="JobID"></param>
        /// <returns></returns>
        public DataSet GetJobOpeningByID(int JobID)
        {
            _jobid = JobID;
            string sql = " select* from JOBAPPLICATION where JOBAPPLICATIONID = " + _jobid;
            return RunQuery(sql);
        }


        /// <summary>
        /// Gets all open jobs
        /// </summary>
        /// <returns></returns>
        public DataSet GetJobs()
        {
            string sql = "select * from JOBAPPLICATION";
            return RunQuery(sql);
        }
        /// <summary>
        /// Adds a question for the opened job
        /// </summary>
        /// <param name="QuestionText">The prompt text for the question</param>
        /// <param name="TypeID">The type associated with the question</param>
        /// <param name="HasCorrectAnswer">A boolean value for whether or not there is a "correct" answer for this question</param>
        /// <param name="CorrectAnswer">Correct answer. Pass the string "" in case of not applicable</param>
        /// <param name="IsRequired">A boolean value for whether or not this question must be answered</param>
        public void AddField(int JobID, String QuestionText, int TypeID, int HasCorrectAnswer, String CorrectAnswer, int IsRequired)
        {
            string sql = "insert into JOBQUESTIONS (JOBAPPLICATIONID, QUESTIONTEXT, TYPEID, HASCORRECTANSWER, CORRECTANSWER, ISREQUIRED) VALUES (" + JobID + ", \'" + QuestionText + "\', " + TypeID + ", " + HasCorrectAnswer + ", \'" + CorrectAnswer + "\', " + IsRequired + ")";
            RunNonQuery(sql);
        }
        public void AddField(String QuestionText, int TypeID, int HasCorrectAnswer, String CorrectAnswer, int IsRequired)
        {
            string sql = "insert into JOBQUESTIONS (JOBAPPLICATIONID, QUESTIONTEXT, TYPEID, HASCORRECTANSWER, CORRECTANSWER, ISREQUIRED) VALUES (" + _jobid + ", \'" + QuestionText + "\', " + TypeID + ", " + HasCorrectAnswer + ", \'" + CorrectAnswer + "\', " + IsRequired + ")";
            RunNonQuery(sql);
        }

        /// <summary>
        /// Updates a question field. If you get an error regarding foreign keys, it is because the question has been answered(removal automatically planned)
        /// </summary>
        /// <param name="QuestionID">the primary key for the question</param>
        /// <param name="QuestionText">The prompt text for the question</param>
        /// <param name="TypeID">The type associated with the question</param>
        /// <param name="HasCorrectAnswer">A boolean value for whether or not there is a "correct" answer for this question</param>
        /// <param name="CorrectAnswer">Correct answer. Pass the string "" in case of not applicable</param>
        /// <param name="IsRequired">A boolean value for whether or not this question must be answered</param>
        public void UpdateJobApplicationField(int QuestionID, int JobID, String QuestionText, int TypeID, int HasCorrectAnswer, String CorrectAnswer, int IsRequired)
        {
            string sql = String.Format("update JOBQUESTIONS set QUESTIONTEXT='{0}', TYPEID={1}, HASCORRECTANSWER={2}, CORRECTANSWER='{3}', ISREQUIRED={4}, JOBAPPLICATIONID={6} where QUESTIONID = {5}", QuestionText, TypeID, HasCorrectAnswer, CorrectAnswer, IsRequired, QuestionID, JobID);
            RunNonQuery(sql);
        }
        /// <summary>
        /// Adds a new job position.
        /// </summary>
        /// <param name="JobDescription">Long description</param>
        /// <param name="Wage"></param>
        /// <param name="DepartmentID">DepartmentID of department to create job in. Be mindful of foreign key restraints.</param>
        /// <param name="HasBeenFilled"></param>
        /// <param name="Location"></param>
        /// <param name="JobName">Short title for job</param>
        public void AddJobPosition(String JobDescription, String Wage, int DepartmentID, int HasBeenFilled, String Location, String JobName)
        {
            string sql = String.Format("insert into JOBAPPLICATION (JOBDESCRIPTION, WAGE, DEPARTMENTID, HASBEENFILLED, LOCATION, JOBNAME) VALUES ('{0}', '{1}', {2}, {3}, '{4}', '{5}')", JobDescription, Wage, DepartmentID, HasBeenFilled, Location, JobName);
            RunNonQuery(sql);
        }
        /// <summary>
        /// Deletes a job position. Will throw an exception if job position has been applied for(pending further work).
        /// </summary>
        /// <param name="JobID"></param>
        public void RemoveJobPosition(int JobID)
        {
            string sql = String.Format("delete from JOBAPPLICATION where JOBAPPLICATIONID = {0}", JobID);
            RunNonQuery(sql);
        }
        public void RemoveJobPosition()
        {
            string sql = String.Format("delete from JOBAPPLICATION where JOBAPPLICATIONID = {0}", _jobid);
            RunNonQuery(sql);
        }

        /// <summary>
        /// Gets all screening questions for job
        /// </summary>
        /// <param name="JobID">Job ID to get questions for</param>
        /// <returns>Dataset containing columns </returns>
        public DataSet OpenScreeningQuestions(int JobID)
        {
            string sql = "select * from JOBQUESTIONS where JOBAPPLICATIONID = " + JobID;
            return RunQuery(sql);
        }
        public DataSet OpenScreeningQuestions()
        {
            string sql = "select * from JOBQUESTIONS where JOBAPPLICATIONID = " + _jobid;
            return RunQuery(sql);
        }
        /// <summary>
        /// Iterates through all fields in application, sending updates to the database. Will throw an exception if the question has been answered.
        /// </summary>
        /// <param name="Application">Application to update</param>
        /// <example>
        ///  JobPositionSystemDAL jps = new JobPositionSystemDAL();
        ///  JobApplication j = jps.OpenJobApplication(21);
        ///  j.Fields[0].FieldText += " test";
        ///  jps.UpdateScreeningQuestions(j);
        /// </example>
        public void UpdateScreeningQuestions(JobApplication Application)
        {
            for (int i = 0; i < Application.Fields.Count; i++)
            {
                UpdateJobApplicationField(Application.Fields[i].FieldID, Application.JobID() , Application.Fields[i].FieldText, Application.Fields[i].FieldType, Convert.ToInt32(Application.Fields[i].HasCorrectAnswer), Application.Fields[i].CorrectAnswer, Convert.ToInt32(Application.Fields[i].AnswerRequired));
            }
        }
        /// <summary>
        /// Close connection to the database
        /// </summary>
        ~JobPositionSystemDAL()
        {
            _connection.Close();
        }
    }
}
