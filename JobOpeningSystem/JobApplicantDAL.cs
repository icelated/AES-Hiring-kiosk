using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using System.Web;

using JobPositionSystem;



namespace JobOpeningSystem 
{
    /// <summary>
    /// This class is used to add applicants and their answers to the database
    /// </summary>
    public class JobApplicantDAL 
    {
        private const string _connectionstring = "Data Source=(DESCRIPTION="
             + "(ADDRESS=(PROTOCOL=TCP)(HOST=JESSE.VIOLATES.US)(PORT=1521))"
             + "(CONNECT_DATA=(SERVICE_NAME=XE)));"
             + "User Id=SYSTEM;Password=stupid;";
        private OracleConnection _connection;
        /// <summary>
        /// The constructor initializes the database connection
        /// </summary>
        public JobApplicantDAL()
        {
            try
            {
                _connection = new OracleConnection();
                _connection.ConnectionString = _connectionstring;
                _connection.Open();
            }
            catch (Exception e)
            {
                throw new Exception("Error connecting to SQL server. Consider nicely asking Jesse for help." + Environment.NewLine + e.Message);
            }
        }
        /// <summary>
        /// Creates a new applicant in the database
        /// </summary>
        /// <returns>Returns ApplicantID of created applicant</returns>
        public int AddApplicant(int JobID)
        {
            string sql = "insert into SUBMITTEDJOBAPPLICATION (JOBAPPLICATIONID) VALUES (" + JobID +  ")";
            string sql2 = "select max(SUBMITTEDJOBAPPLICATION) from SUBMITTEDJOBAPPLICATION";
            RunQuery(sql);
            return RunIntQuery(sql2);
        }


        /// <summary>
        /// Add job application
        /// </summary>
        /// <param name="JobDescription"></param>
        /// <param name="wage"></param>
        /// <param name="departmentID"></param>
        /// <param name="hasBeenFilled"></param>
        /// <param name="location"></param>
        /// <param name="jobname"></param>
        /// 

        public void AddJobApplication(List<String> answersfromsession)
        {
            
           // var value =  HttpContext.Current.Session["TextboxValue"];

           // string sql = "insert into ANSWEREDGENERICQUESTIONS (ANSWER) VALUES ( \'" + answersfromsession + " \' )";
          //  RunQuery(sql);
        }


        ///please keep for now

       // public void AddJobApplication(string JobDescription, string wage, int departmentID, int hasBeenFilled, string location, string jobname)
       // public void AddJobApplication()
       // {
          //  string sql = "insert into JOBQUESTIONS (
         // string sql = "insert into JOBAPPLICATION (JOBDESCRIPTION, WAGE, DEPARTMENTID, HASBEENFILLED, LOCATION, JOBNAME) VALUES (\'" + JobDescription + "\', \'" + wage + "\', " + departmentID + ", " + hasBeenFilled + ", \'" + location + "\', \'" + jobname + "\')";
         // RunNonQuery(sql);

       // }


        //////////////////////////////
        /// <summary>
        /// This function adds a new answer to a job question
        /// </summary>
        /// <param name="ApplicantID">Applicant ID obtained from running AddApplicant</param>
        /// <param name="QuestionID">QuestionID for current question</param>
        /// <param name="Response">The answer to submit</param>
        public void AddJobQuestion(int ApplicantID, int QuestionID, string Response)
        {
            string sql = "insert into ANSWEREDJOBQUESTIONS (QUESTIONID, SUBMITTEDAPPLICATIONID, ANSWER) VALUES (" + QuestionID + ", " + ApplicantID + ", " + Response + ")";
            RunQuery(sql);
        }
        /// <summary>
        /// This function adds a new answer to a generic question
        /// </summary>
        /// <param name="ApplicantID">Applicant ID obtained from running AddApplicant</param>
        /// <param name="QuestionID">QuestionID for current question</param>
        /// <param name="Response">The answer to submit</param>
        public void AddGenericQuestion(int ApplicantID, int QuestionID, string Response)
        {
            string sql = "insert into ANSWEREDGENERICQUESTIONS (QUESTIONID, SUBMITTEDAPPLICATIONID, ANSWER) VALUES (" + QuestionID + ", " + ApplicantID + ", " + Response + ")";
            RunQuery(sql);
        }

        /// <summary>
        /// Runs SQL query, returns object with results
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
        /// Runs an SQL query where the result is a single integer
        /// </summary>
        /// <param name="sql">SQL code to execute</param>
        /// <returns>Returns query results</returns>
        private int RunIntQuery(string sql)
        {
            OracleCommand cmd;
            OracleDataReader dr;
            int result;

            try
            {
                cmd = new OracleCommand(sql, _connection);

                cmd.CommandType = System.Data.CommandType.Text;
                dr = cmd.ExecuteReader();
                dr.Read();
                result = (int)dr.GetDecimal(0);
                dr.Close();
            }
            catch (Exception e)
            {
                throw new Exception("Error in sending SQL command to server. Check your parameters." + Environment.NewLine + e.Message);
            }
            return result;
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
        /// Gets all generic questions from the databse, returning QuestionID, QuestionText
        /// ,TypeID, HasCorrectAnswer, CorrectAnswer, IsRequired
        /// </summary>
        /// <returns>Dataset with columns from GenericQuestions table</returns>
        public DataSet GetGenericQuestions()
        {
            string sql = "select * from GENERICQUESTIONS";
            return RunQuery(sql);
        }
        /// <summary>
        /// The destructor closes the database connection
        /// </summary>
        ~JobApplicantDAL()
        {
            _connection.Close();
        }
    }
}
