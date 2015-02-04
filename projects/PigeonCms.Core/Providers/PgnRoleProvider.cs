using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using System.Web.Configuration;
using System.Collections.Specialized;
using System.Data.Common;
using System.Configuration.Provider;
using System.Security.Cryptography;
using System.Text;

namespace PigeonCms
{
    /// <summary>
    /// PigeonCms custom role provider
    /// see http://msdn.microsoft.com/en-us/library/317sza4k.aspx
    /// </summary>
    public class PgnRoleProvider: RoleProvider
    {
        #region private vars
        private string[] coreRoles = {"admin", "backend", "debug"};
        private string eventSource = "PgnRoleProvider";
        private string eventLog = "Application";
        private string exceptionMessage = "An exception occurred in PigeonCms.PgnRoleProvider.";
        private string connectionString;

        private bool pWriteExceptionsToEventLog = false;
        public bool WriteExceptionsToEventLog
        {
            get { return pWriteExceptionsToEventLog; }
            set { pWriteExceptionsToEventLog = value; }
        }

        private string pApplicationName;
        public override string ApplicationName
        {
            get { return pApplicationName; }
            set { pApplicationName = value; }
        }
        #endregion


        #region private methods
        // helper function to retrieve config values from the configuration file.
        private string getConfigValue(string configValue, string defaultValue)
        {
            if (String.IsNullOrEmpty(configValue))
                return defaultValue;

            return configValue;
        }

        private bool isCoreRole(string rolename)
        {
            bool res = false;
            for (int i = 0; i < coreRoles.Length; i++)
            {
                if (rolename.ToLower() == coreRoles[i].ToLower())
                {
                    res = true;
                    break;
                }
            }
            return res;
        }

        private void writeToEventLog(Exception e, string action)
        {
            EventLog log = new EventLog();
            log.Source = eventSource;
            log.Log = eventLog;

            string message = exceptionMessage + "\n\n";
            message += "Action: " + action + "\n\n";
            message += "Exception: " + e.ToString();

            log.WriteEntry(message);
        }
        #endregion


        #region public methods
        // System.Configuration.Provider.ProviderBase.Initialize Method
        public override void Initialize(string name, NameValueCollection config)
        {
            // Initialize values from web.config.
            if (config == null)
                throw new ArgumentNullException("config");

            if (name == null || name.Length == 0)
                name = "PgnRoleProvider";

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "PigeonCms custom role provider");
            }

            // Initialize the abstract base class.
            base.Initialize(name, config);

            pApplicationName = getConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            pWriteExceptionsToEventLog = Convert.ToBoolean(getConfigValue(config["writeExceptionsToEventLog"], "false"));


            // Initialize OdbcConnection.
            ConnectionStringSettings ConnectionStringSettings =
              ConfigurationManager.ConnectionStrings[config["connectionStringName"]];

            if (ConnectionStringSettings == null || ConnectionStringSettings.ConnectionString.Trim() == "")
                connectionString = Database.ConnString;
            else
                connectionString = ConnectionStringSettings.ConnectionString;
        }


        public override void AddUsersToRoles(string[] usernames, string[] rolenames)
        {
            foreach (string rolename in rolenames)
            {
                if (!RoleExists(rolename))
                {
                    throw new ProviderException("Role name not found.");
                }
            }

            foreach (string username in usernames)
            {
                if (username.Contains(","))
                {
                    throw new ArgumentException("User names cannot contain commas.");
                }

                //hidden on 20100104
                //foreach (string rolename in rolenames)
                //{
                //    if (IsUserInRole(username, rolename))
                //    {
                //        throw new ProviderException("User is already in role.");
                //    }
                //}
            }

            DbProviderFactory myProv = Database.ProviderFactory;
            //DbTransaction myTrans = null;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myProv.CreateCommand();
            string sSql = "";

            try
            {
                myConn.ConnectionString = connectionString;
                myConn.Open();
                myCmd.Connection = myConn;
                //myTrans = myConn.BeginTransaction();
                //myCmd.Transaction = myTrans;

                sSql = "INSERT INTO #__usersInRoles "
                + " (Username, Rolename, ApplicationName) "
                + " Values(@Username, @Rolename, @ApplicationName)";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "Username", ""));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Rolename", ""));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ApplicationName", ApplicationName));

                foreach (string username in usernames)
                {
                    foreach (string rolename in rolenames)
                    {
                        if (!IsUserInRole(username, rolename))  //added on 20100105 to allow external roles
                        {
                            //comment on 20111101
                            //if (username.ToLower() == "admin" && isCoreRole(rolename))
                            //{ /*do nothing*/ }
                            //else
                            //{
                            //}

                            myCmd.Parameters["Username"].Value = username;
                            myCmd.Parameters["Rolename"].Value = rolename;
                            myCmd.ExecuteNonQuery();
                        }
                    }
                }
                //myTrans.Commit();
            }
            catch (Exception e)
            {
                try
                {
                    //myTrans.Rollback();
                }
                catch { }


                if (WriteExceptionsToEventLog)
                {
                    writeToEventLog(e, "AddUsersToRoles");
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                myConn.Dispose();
            }
        }

        public override void CreateRole(string rolename)
        {
            if (rolename.Contains(","))
            {
                throw new ArgumentException("Role names cannot contain commas.");
            }

            if (RoleExists(rolename))
            {
                throw new ProviderException("Role name already exists.");
            }

            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql = "";

            try
            {
                myConn.ConnectionString = connectionString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "INSERT INTO #__roles "
                + " (Rolename, ApplicationName) "
                + " Values(@Rolename, @ApplicationName)";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Rolename", rolename));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ApplicationName", ApplicationName));
                myCmd.CommandText = Database.ParseSql(sSql);

                myCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    writeToEventLog(e, "CreateRole");
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                myConn.Dispose();
            }
        }

        public override bool DeleteRole(string rolename, bool throwOnPopulatedRole)
        {
            if (!RoleExists(rolename))
            {
                throw new ProviderException("Role does not exist.");
            }
            if (isCoreRole(rolename))
            {
                throw new ProviderException("Cannot delete core roles");
            }

            if (throwOnPopulatedRole && GetUsersInRole(rolename).Length > 0)
            {
                throw new ProviderException("Cannot delete a populated role.");
            }

            DbProviderFactory myProv = Database.ProviderFactory;
            DbTransaction myTrans = null;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myProv.CreateCommand();
            DbCommand myCmd2 = myProv.CreateCommand();
            string sSql = "";

            try
            {
                myConn.ConnectionString = connectionString;
                myConn.Open();
                myCmd.Connection = myConn;
                myCmd2.Connection = myConn;
                myTrans = myConn.BeginTransaction();
                myCmd.Transaction = myTrans;
                myCmd2.Transaction = myTrans;

                sSql = "DELETE FROM #__roles "
                + " WHERE Rolename = @Rolename AND ApplicationName = @ApplicationName ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Rolename", rolename));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ApplicationName", ApplicationName));

                sSql = "DELETE FROM #__usersInRoles "
                + " WHERE Rolename = @Rolename AND ApplicationName = @ApplicationName ";
                myCmd2.CommandText = Database.ParseSql(sSql);
                myCmd2.Parameters.Add(Database.Parameter(myProv, "Rolename", rolename));
                myCmd2.Parameters.Add(Database.Parameter(myProv, "ApplicationName", ApplicationName));

                myCmd.ExecuteNonQuery();
                myCmd2.ExecuteNonQuery();

                myTrans.Commit();
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback();
                }
                catch { }


                if (WriteExceptionsToEventLog)
                {
                    writeToEventLog(e, "DeleteRole");
                    return false;
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                myConn.Dispose();
            }
            return true;
        }

        public override string[] GetAllRoles()
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myProv.CreateCommand();
            DbDataReader myRd = null;
            string sSql = "";
            string tmpRoleNames = "";

            try
            {
                myConn.ConnectionString = connectionString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT Rolename FROM #__roles "
                + " WHERE ApplicationName = @ApplicationName ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "ApplicationName", ApplicationName));
                myRd = myCmd.ExecuteReader();

                while (myRd.Read())
                {
                    tmpRoleNames += myRd.GetString(0) + ",";
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    writeToEventLog(e, "GetAllRoles");
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (myRd != null) { myRd.Close(); }
                myConn.Dispose();
            }

            if (tmpRoleNames.Length > 0)
            {
                // Remove trailing comma.
                tmpRoleNames = tmpRoleNames.Substring(0, tmpRoleNames.Length - 1);
                return tmpRoleNames.Split(',');
            }
            return new string[0];
        }

        public override string[] GetRolesForUser(string username)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myProv.CreateCommand();
            DbDataReader myRd = null;
            string sSql = "";
            string tmpRoleNames = "";

            try
            {
                myConn.ConnectionString = connectionString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT Rolename FROM #__usersInRoles "
                + " WHERE Username = @Username AND ApplicationName = @ApplicationName ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Username", username));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ApplicationName", ApplicationName));
                myRd = myCmd.ExecuteReader();

                while (myRd.Read())
                {
                    tmpRoleNames += myRd.GetString(0) + ",";
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    writeToEventLog(e, "GetRolesForUser");
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (myRd != null) { myRd.Close(); }
                myConn.Dispose();
            }

            if (tmpRoleNames.Length > 0)
            {
                // Remove trailing comma.
                tmpRoleNames = tmpRoleNames.Substring(0, tmpRoleNames.Length - 1);
                return tmpRoleNames.Split(',');
            }
            return new string[0];
        }

        public override string[] GetUsersInRole(string rolename)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myProv.CreateCommand();
            DbDataReader myRd = null;
            string sSql = "";
            string tmpUserNames = "";

            try
            {
                myConn.ConnectionString = connectionString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT Username FROM #__usersInRoles "
                + " WHERE Rolename = @Rolename AND ApplicationName = @ApplicationName ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Rolename", rolename));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ApplicationName", ApplicationName));
                myRd = myCmd.ExecuteReader();

                while (myRd.Read())
                {
                    tmpUserNames += myRd.GetString(0) + ",";
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    writeToEventLog(e, "GetUsersInRole");
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (myRd != null) { myRd.Close(); }
                myConn.Dispose();
            }

            if (tmpUserNames.Length > 0)
            {
                // Remove trailing comma.
                tmpUserNames = tmpUserNames.Substring(0, tmpUserNames.Length - 1);
                return tmpUserNames.Split(',');
            }
            return new string[0];
        }

        public override bool IsUserInRole(string username, string rolename)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myProv.CreateCommand();
            DbDataReader myRd = null;
            string sSql = "";
            bool userIsInRole = false;

            try
            {
                myConn.ConnectionString = connectionString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT count(*) FROM #__usersInRoles "
                    + " WHERE Username = @Username AND Rolename = @Rolename  AND ApplicationName = @ApplicationName ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Username", username));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Rolename", rolename));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ApplicationName", ApplicationName));
                myRd = myCmd.ExecuteReader();

                int numRecs = 0;

                if (myRd.Read())
                {
                    if (!Convert.IsDBNull(myRd[0]))
                        numRecs = (int)myRd[0];
                }

                if (numRecs > 0)
                {
                    userIsInRole = true;
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    writeToEventLog(e, "IsUserInRole");
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                myConn.Dispose();
            }
            return userIsInRole;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] rolenames)
        {
            foreach (string rolename in rolenames)
            {
                if (!RoleExists(rolename))
                {
                    throw new ProviderException("Role name not found.");
                }
            }

            foreach (string username in usernames)
            {
                foreach (string rolename in rolenames)
                {
                    if (!IsUserInRole(username, rolename))
                    {
                        throw new ProviderException("User is not in role.");
                    }
                }
            }

            DbProviderFactory myProv = Database.ProviderFactory;
            DbTransaction myTrans = null;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myProv.CreateCommand();
            string sSql = "";

            try
            {
                myConn.ConnectionString = connectionString;
                myConn.Open();
                myCmd.Connection = myConn;
                myTrans = myConn.BeginTransaction();
                myCmd.Transaction = myTrans;

                sSql = "DELETE FROM #__usersInRoles "
                + " WHERE Username = @Username AND Rolename = @Rolename  AND ApplicationName = @ApplicationName ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Username", ""));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Rolename", ""));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ApplicationName", ApplicationName));

                foreach (string username in usernames)
                {
                    foreach (string rolename in rolenames)
                    {
                        if (username.ToLower() == "admin" && isCoreRole(rolename))
                        {
                            //do nothing
                        }
                        else
                        {
                            myCmd.Parameters["Username"].Value = username;
                            myCmd.Parameters["Rolename"].Value = rolename;
                            myCmd.ExecuteNonQuery();
                        }
                    }
                }
                myTrans.Commit();
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback();
                }
                catch { }


                if (WriteExceptionsToEventLog)
                {
                    writeToEventLog(e, "RemoveUsersFromRoles");
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                myConn.Dispose();
            }
        }

        public override bool RoleExists(string rolename)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myProv.CreateCommand();
            DbDataReader myRd = null;
            string sSql = "";
            bool exists = false;

            try
            {
                myConn.ConnectionString = connectionString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT COUNT(*) FROM #__roles "
                + " WHERE Rolename = @Rolename AND ApplicationName = @ApplicationName ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Rolename", rolename));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ApplicationName", ApplicationName));
                myRd = myCmd.ExecuteReader();

                int numRecs = 0;

                if (myRd.Read())
                {
                    if (!Convert.IsDBNull(myRd[0]))
                        numRecs = (int)myRd[0];
                }

                if (numRecs > 0)
                {
                    exists = true;
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    writeToEventLog(e, "RoleExists");
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                myConn.Dispose();
            }
            return exists;
        }

        public override string[] FindUsersInRole(string rolename, string usernameToMatch)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myProv.CreateCommand();
            DbDataReader myRd = null;
            string sSql = "";
            string tmpUserNames = "";

            try
            {
                sSql = "SELECT Username FROM #__usersInRoles  "
                + " WHERE Username LIKE @UsernameSearch AND RoleName = @Rolename AND ApplicationName = @ApplicationName ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "UsernameSearch", "%" + usernameToMatch + "%"));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Rolename", rolename));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ApplicationName", ApplicationName));
                myRd = myCmd.ExecuteReader();

                while (myRd.Read())
                {
                    tmpUserNames += myRd.GetString(0) + ",";
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    writeToEventLog(e, "FindUsersInRole");
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (myRd != null) { myRd.Close(); }
                myConn.Dispose();
            }

            if (tmpUserNames.Length > 0)
            {
                // Remove trailing comma.
                tmpUserNames = tmpUserNames.Substring(0, tmpUserNames.Length - 1);
                return tmpUserNames.Split(',');
            }
            return new string[0];
        }

        #endregion
    }
}