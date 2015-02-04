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
using System.Text;
using System.Collections.Generic;

namespace PigeonCms
{
    /// <summary>
    /// manager permissions for modules,menus 
    /// and other classes that implements ITableWithPermissions Interface
    /// </summary>
    public class PermissionProvider
    {
        #region public methods

        public bool IsItemNotAllowed(ITableWithPermissions obj)
        {
            return !IsItemAllowed(obj, false);
        }

        public bool IsItemNotAllowedForWrite(ITableWithPermissions obj)
        {
            return !IsItemAllowed(obj, true);
        }

        /// <summary>
        /// check current user permissions for ITableWithPermissions obj
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>if obj is allowed or not for current user</returns>
        public bool IsItemAllowed(ITableWithPermissions obj, bool writeMode)
        {
            bool result = true;
            MenuAccesstype accessType;
            List<string> rolenames;
            string accessCode;
            int accessLevel;

            if (writeMode)
            {
                accessType = obj.WriteAccessType;
                rolenames = obj.WriteRolenames;
                accessCode = obj.WriteAccessCode;
                accessLevel = obj.WriteAccessLevel;
            }
            else
            {
                accessType = obj.ReadAccessType;
                rolenames = obj.ReadRolenames;
                accessCode = obj.ReadAccessCode;
                accessLevel = obj.ReadAccessLevel;
            }

            if (accessType != MenuAccesstype.Public)
            {
                //check current logged user permission
                result = false;
                if (PgnUserCurrent.IsAuthenticated)
                {
                    if (Roles.IsUserInRole("admin"))
                    {
                        //admin always granted
                        result = true;
                    }
                    else if (rolenames.Count > 0)
                    {
                        //check user roles
                        //obj.Rolenames uses roles cached in cookie
                        //Roles.GetRolesForUser() launch the method each time
                        foreach (string role in rolenames)  
                        {
                            if (Roles.IsUserInRole(role))
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        //all registered users
                        result = true;
                    }

                    if (result && !Roles.IsUserInRole("admin")/*20150127*/)
                    {
                        //check AccessCode
                        if (!string.IsNullOrEmpty(accessCode))
                        {
                            if (!string.IsNullOrEmpty(PgnUserCurrent.Current.AccessCode))
                            {
                                if (accessCode != PgnUserCurrent.Current.AccessCode)
                                    result = false;
                            }
                        }
                        //checl AccessLevel
                        if (accessLevel > 0)
                        {
                            if (accessLevel > PgnUserCurrent.Current.AccessLevel)
                                result = false;
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// update obj object in DAL class, before db update
        /// </summary>
        /// <param name="obj"></param>
        public void UpdatePermissionObj(ITableWithPermissions obj)
        {
            //read permission (before trans)
            if (obj.ReadAccessType == MenuAccesstype.Public)
                obj.ReadPermissionId = 0;
            else
                obj.ReadPermissionId =
                    new PermissionProvider().AddRolesToPermission(
                    obj.ReadPermissionId, obj.ReadRolenames, true);

            //write permission (before trans)
            if (obj.WriteAccessType == MenuAccesstype.Public)
                obj.WritePermissionId = 0;
            else
                obj.WritePermissionId =
                    new PermissionProvider().AddRolesToPermission(
                    obj.WritePermissionId, obj.WriteRolenames, true);
        }

        public void CreatePermissionObj(ITableWithPermissions obj)
        {
            //create read permission
            obj.ReadPermissionId =
                this.AddRolesToPermission(
                obj.ReadPermissionId, obj.ReadRolenames, true);
            //create write permission
            obj.WritePermissionId =
                this.AddRolesToPermission(
                obj.WritePermissionId, obj.WriteRolenames, true);
        }


        public int CreatePermission(List<string> rolenames)
        {
            return AddRolesToPermission(0, rolenames, true);
        }

        /// <summary>
        /// add rolenames to permission
        /// </summary>
        /// <param name="permissionId">the permission. If 0 a new permission will be created</param>
        /// <param name="rolenames">list of rolenames</param>
        /// <param name="removeExisting">if true, remove existing roles in permission</param>
        /// <returns>PermissionId value</returns>
        public int AddRolesToPermission(int permissionId, List<string> rolenames, bool removeExisting)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbTransaction myTrans = null;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myProv.CreateCommand();
            DbDataReader myRd = null;
            string sSql = "";

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;
                myTrans = myConn.BeginTransaction();
                myCmd.Transaction = myTrans;

                if (permissionId == 0 && rolenames.Count > 0)
                {
                    //new Id
                    sSql = "SELECT max(id) FROM #__permissions ";
                    myCmd.CommandText = Database.ParseSql(sSql);
                    myRd = myCmd.ExecuteReader();
                    if (myRd.Read())
                    {
                        if (myRd[0] != DBNull.Value)
                        {
                            permissionId = (int)myRd[0];
                        }
                    }
                    myRd.Close();
                    permissionId++;
                }
                else
                {
                    if (removeExisting)
                    {
                        sSql = "DELETE FROM #__permissions WHERE Id=@Id ";
                        myCmd.CommandText = Database.ParseSql(sSql);
                        myCmd.Parameters.Add(Database.Parameter(myProv, "Id", permissionId));
                        myCmd.ExecuteNonQuery();
                    }
                }

                if (permissionId > 0)
                {
                    sSql = "INSERT INTO #__permissions(Id, Rolename) "
                            + " VALUES(@Id, @Rolename)";
                    myCmd.CommandText = Database.ParseSql(sSql);
                    myCmd.Parameters.Clear();
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Id", permissionId));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Rolename", ""));

                    foreach (var rolename in rolenames)
                    {
                        if (!string.IsNullOrEmpty(rolename))
                        {
                            myCmd.Parameters["Rolename"].Value = rolename;
                            myCmd.ExecuteNonQuery();
                        }
                    } 
                }

                myTrans.Commit();
            }
            catch (Exception e)
            {
                myTrans.Rollback();
                throw e;
            }
            finally
            {
                myConn.Dispose();
            }
            return permissionId;
        }

        public void RemovePermissionRoles(int permissionId, List<string> rolenames)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbTransaction myTrans = null;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myProv.CreateCommand();
            string sSql = "";

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;
                myTrans = myConn.BeginTransaction();
                myCmd.Transaction = myTrans;

                sSql = "DELETE FROM #__permissions "
                + " WHERE Id=@Id AND Rolename=@Rolename";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", permissionId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Rolename", ""));

                foreach (var rolename in rolenames)
                {
                    myCmd.Parameters["Rolename"].Value = rolename;
                    myCmd.ExecuteNonQuery();
                }
                myTrans.Commit();
            }
            catch (Exception e)
            {
                myTrans.Rollback();
                throw e;
            }
            finally
            {
                myConn.Dispose();
            }
        }

        public void RemovePermissionById(int permissionId)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myProv.CreateCommand();
            string sSql = "";

            if (permissionId > 0)
            {
                try
                {
                    myConn.ConnectionString = Database.ConnString;
                    myConn.Open();
                    myCmd.Connection = myConn;

                    sSql = "DELETE FROM #__permissions WHERE Id=@Id ";
                    myCmd.CommandText = Database.ParseSql(sSql);
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Id", permissionId));
                    myCmd.ExecuteNonQuery();
                }
                finally
                {
                    myConn.Dispose();
                }
            }
        }

        public List<string> GetPermissionRoles(int permissionId)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myProv.CreateCommand();
            DbDataReader myRd = null;
            string sSql = "";
            List<string> result = new List<string>();

            if (permissionId > 0)
            {
                try
                {
                    myConn.ConnectionString = Database.ConnString;
                    myConn.Open();
                    myCmd.Connection = myConn;

                    sSql = "SELECT Rolename FROM #__permissions "
                    + " WHERE Id = @Id ";
                    myCmd.CommandText = Database.ParseSql(sSql);
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Id", permissionId));
                    myRd = myCmd.ExecuteReader();

                    while (myRd.Read())
                    {
                        result.Add(myRd.GetString(0));
                    }
                }
                finally
                {
                    if (myRd != null) { myRd.Close(); }
                    myConn.Dispose();
                }
            }
            return result;
        }

        public bool IsRoleInPermission(int permissionId, string rolename)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myProv.CreateCommand();
            DbDataReader myRd = null;
            string sSql = "";
            bool result = false;
            int numRecs = 0;

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT count(*) FROM #__permissions "
                    + " WHERE Id = @Id AND Rolename = @Rolename ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", permissionId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Rolename", rolename));
                myRd = myCmd.ExecuteReader();

                if (myRd.Read())
                {
                    if (!Convert.IsDBNull(myRd[0]))
                        numRecs = (int)myRd[0];
                }
                myRd.Close();

                if (numRecs > 0)
                {
                    result = true;
                }
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        #endregion
    }
}