using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;


namespace JobOpeningSystem
{

  public class JobOpeningDAL
    {

        private const string _connectionstring = "Data Source=(DESCRIPTION="
             + "(ADDRESS=(PROTOCOL=TCP)(HOST=JESSE.VIOLATES.US)(PORT=1521))"
             + "(CONNECT_DATA=(SERVICE_NAME=XE)));"
             + "User Id=SYSTEM;Password=stupid;";
        private OracleConnection _connection;
        /// <summary>
        /// The constructor initializes the database connection
        /// </summary>
       public JobOpeningDAL()
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
        /// remove a job 
        /// </summary>
        /// <param name="JobID"></param>
       public void RemoveJobOpening(int JobID)
       {
           string sql = String.Format("delete from JOBAPPLICATION where JOBAPPLICATIONID = {0}", JobID);
           RunQuery(sql);
       }

      /// <summary>
      /// add a job
      /// </summary>
      /// <param name="JobDescription"></param>
      /// <param name="wage"></param>
      /// <param name="departmentID"></param>
      /// <param name="hasBeenFilled"></param>
      /// <param name="location"></param>
      /// <param name="jobname"></param>
       public void AddJobOpening(string JobDescription, string wage, int departmentID, int hasBeenFilled, string location, string jobname)
       {
           string sql = "insert into JOBAPPLICATION (JOBDESCRIPTION, WAGE, DEPARTMENTID, HASBEENFILLED, LOCATION, JOBNAME) VALUES (\'" + JobDescription + "\', \'" + wage + "\', " + departmentID + ", " + hasBeenFilled + ", \'" + location + "\', \'" + jobname + "\')";


           RunNonQuery(sql);

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


       ~JobOpeningDAL()
        {
            _connection.Close();
        }
    }
}
