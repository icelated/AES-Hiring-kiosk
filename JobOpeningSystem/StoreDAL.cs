using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
namespace JobOpeningSystem
{
    public class StoreDAL
    {
        #region member variables and CTOR
        public enum ManagerType
        {
            store = 0, hiring, operational
        }
        private const string _connectionstring = "Data Source=(DESCRIPTION="
         + "(ADDRESS=(PROTOCOL=TCP)(HOST=JESSE.VIOLATES.US)(PORT=1521))"
         + "(CONNECT_DATA=(SERVICE_NAME=XE)));"
         + "User Id=SYSTEM;Password=stupid;";
        private OracleConnection _connection;

        public StoreDAL()
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
        ~StoreDAL()
        {
            _connection.Close();
        }
        #endregion
        #region common dal functions
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
        #endregion
        #region public functions

        public int AddStore(string Name, String Address)
        {
            string sql = String.Format("insert into STORES (ADDRESS, NAME) VALUES ('{0}', '{1}')", Address, Name);
            return RunIntQuery(sql);
        }

        /// <summary>
        /// Adds a manager of manager type
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Type"></param>
        public void AddManager(int UserID, ManagerType Type)
        {
            string sql = String.Format("insert into PERMISSIONENTRIES (USERID, PERMISSIONID, HASPERMISSION) values ('{0}', {1}, {2})", UserID, (int)Type, 1);
            RunNonQuery(sql);
        }

        /// <summary>
        /// I have no idea what this is supposed to do.
        /// </summary>
        public void UpdateStoreManager()
        {
            
        }
        /// <summary>
        /// Remove user's manager affilliation with store
        /// </summary>
        /// <param name="UserID">User to modify</param>
        public void RemoveManager(int UserID)
        {
            string sql = String.Format("delete from PERMISSIONENTRIES where USERID = {0}", UserID);
            RunNonQuery(sql);
        }
        /// <summary>
        /// Return all users that are store managers, operational managers, or hiring managers
        /// </summary>
        /// <param name="StoreID">StoreID to get managers from</param>
        /// <returns></returns>
        public DataSet GetManagersAtStore(int StoreID)
        {
            string sql = String.Format("select Users.UserID, Users.Name, PermissionEntries.EntryID, PermissionEntries.PermissionID, PermissionEntries.HasPermission from Users, PermissionEntries where Users.StoreID = {0} AND (PermissionID = 1 or PermissionID = 0 or PermissionID = 2)", StoreID);
            return RunQuery(sql);
        }
        #endregion
    }
}
