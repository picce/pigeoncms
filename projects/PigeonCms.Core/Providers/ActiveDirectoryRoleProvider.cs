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
using System.DirectoryServices.AccountManagement;

namespace PigeonCms
{
    public sealed class ActiveDirectoryRoleProvider : PigeonCms.PgnRoleProvider
    {
        const string providerName = "PigeonCms.ActiveDirectoryRoleProvider";

        private string contextUsername = "";
        private string contextPassword = "";
        private string domain = "";


        #region public methods
        public override void Initialize(string name, NameValueCollection config)
        {
            // Initialize the base class.
            base.Initialize(name, config);

            contextUsername = GetConfigValue(config["contextUsername"], "guest");
            contextPassword = GetConfigValue(config["contextPassword"], "");
            domain = GetConfigValue(config["domain"], "local");
        }

        //OK
        public override string[] GetAllRoles()
        {
            string[] res = null;
            try
            {
                var ad = new ADAuthenticationHelper(this.domain, this.contextUsername, this.contextPassword);
                var groups = ad.GetAllGroups();
                if (groups.Count == 0)
                    res = new string[0];
                else
                    res = groups.ToArray();
            }
            catch (Exception e)
            {
                throw new ProviderException("GetAllRoles() error in "+ providerName , e);
            }
            return res;
        }

        //OK
        public override string[] GetRolesForUser(string username)
        {
            string[] res = null;
            try
            {
                var ad = new ADAuthenticationHelper(this.domain, this.contextUsername, this.contextPassword);
                var groups = ad.GetUserGroups(username);
                if (groups.Count == 0)
                    res = new string[0];
                else
                    res = groups.ToArray();
            }
            catch (Exception e)
            {
                throw new ProviderException("GetRolesForUser() error in " + providerName, e);
            }
            return res;
        }

        //OK
        public override string[] GetUsersInRole(string rolename)
        {
            string[] res = null;
            try
            {
                var ad = new ADAuthenticationHelper(this.domain, this.contextUsername, this.contextPassword);
                var list = ad.GetUsersInGroup(rolename, false);
                if (list.Count == 0)
                    res = new string[0];
                else
                    res = list.ToArray();
            }
            catch (Exception e)
            {
                throw new ProviderException("GetUsersInRole() error in " + providerName, e);
            }
            return res;
        }

        //OK
        public override bool IsUserInRole(string username, string rolename)
        {
            bool userIsInRole = false;
            try
            {
                var ad = new ADAuthenticationHelper(this.domain, this.contextUsername, this.contextPassword);
                var list = ad.GetUsersInGroup(rolename, true, username);
                //return PrincipalOperationException  #87 with some roles
                //see http://support.microsoft.com/kb/2585635

                if (list.Count > 0)
                    userIsInRole = true;
            }
            catch (Exception e)
            {
                throw new ProviderException("IsUserInRole(string, string) error in " + providerName, e);
            }
            return userIsInRole;
        }

        //OK
        public override bool RoleExists(string rolename)
        {
            bool exists = false;
            try
            {
                var ad = new ADAuthenticationHelper(this.domain, this.contextUsername, this.contextPassword);
                var list = ad.GetAllGroups(rolename);
                if (list.Count > 0)
                    exists = true;
            }
            catch (Exception e)
            {
                throw new ProviderException("RoleExists(string) error in SidRoleProvider", e);
            }
            return exists;
        }

        //TODO
        public override string[] FindUsersInRole(string rolename, string usernameToMatch)
        {
            string tmpUserNames = "";
            string[] tmpRes = null;
            string[] baseRes = null;

            try
            {
                //baseRes = base.FindUsersInRole(rolename, usernameToMatch);

                //while (myRd.Read())
                //{
                //    if (!Convert.IsDBNull(myRd[0]))
                //        tmpUserNames += myRd.GetString(0) + ",";
                //}
            }
            catch (Exception e)
            {
                throw new ProviderException("FindUsersInRole(string, string) error in SidRoleProvider", e);
            }

            if (tmpUserNames.Length > 0)
            {
                tmpUserNames = tmpUserNames.Substring(0, tmpUserNames.Length - 1);
                tmpRes = tmpUserNames.Split(',');
            }
            else
                tmpRes = new string[0];

            string[] res = new string[tmpRes.Length + baseRes.Length];
            baseRes.CopyTo(res, 0);
            tmpRes.CopyTo(res, baseRes.Length);
            return res;
        }

        #endregion

        // helper function to retrieve config values from the configuration file.
        private string GetConfigValue(string configValue, string defaultValue)
        {
            if (String.IsNullOrEmpty(configValue))
                return defaultValue;

            return configValue;
        }
    }
}