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
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;


namespace PigeonCms
{
    /// <summary>
    /// Use Active Directory auth system with LDAP in PigeonCms
    /// </summary>
    public sealed class ActiveDirectoryUserProvider : PigeonCms.PgnUserProvider
    {
        const string providerFullName = "PigeonCms.ActiveDirectoryUserProvider";
        const string providerName = "ActiveDirectoryUserProvider";

        private string pApplicationName = "";
        public override string ApplicationName
        {
            get { return pApplicationName; }
            set { pApplicationName = value; }
        }

        private string contextUsername = "";
        private string contextPassword = "";
        private string domain = "";


        #region public methods

        // System.Configuration.Provider.ProviderBase.Initialize Method
        public override void Initialize(string name, NameValueCollection config)
        {
            // Initialize the base class.
            base.Initialize(name, config);

            pApplicationName = GetConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            contextUsername = GetConfigValue(config["contextUsername"], "guest");
            contextPassword = GetConfigValue(config["contextPassword"], "");
            domain = GetConfigValue(config["domain"], "local");
        }

        //not supported
        public override bool ChangePassword(string username, string oldPwd, string newPwd)
        {
            throw new NotSupportedException("ChangePassword not supported by " + providerFullName);
        }


        //not supported
        public override bool ChangePasswordQuestionAndAnswer(string username, string password,
                                                string newPwdQuestion, string newPwdAnswer)
        {
            throw new NotSupportedException("ChangePasswordQuestionAndAnswer not supported by " + providerFullName);
        }

        //not supported
        public override MembershipUser CreateUser(string username,
                                          string password,
                                          string email,
                                          string passwordQuestion,
                                          string passwordAnswer,
                                          bool isApproved,
                                          object providerUserKey,
                                          out MembershipCreateStatus status)
        {
            throw new NotSupportedException("CreateUser() not supported by " + providerFullName);
        }

        //not supported
        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotSupportedException("DeleteUser() not supported by " + providerFullName);
        }

        //not supported
        public override int GetNumberOfUsersOnline()
        {
            throw new NotSupportedException("GetNumberOfUsersOnline() not supported by " + providerFullName);
        }

        //not supported
        public override string GetPassword(string username, string answer)
        {
            throw new NotSupportedException("GetPassword() not supported by " + providerFullName);
        }

        //CHECK
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var res = new PgnUser(providerName, 0, username, username, "", "", "", "", true, false,
                DateTime.MinValue, DateTime.MinValue, DateTime.MinValue,
                DateTime.MinValue, DateTime.MinValue);
            return res;
        }

        //not supported
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotSupportedException("GetUser(object, bool) not supported " + providerFullName);
        }

        //not supported
        public override bool UnlockUser(string username)
        {
            throw new NotSupportedException("UnlockUser() not supported in " + providerFullName);
        }

        //not supported
        public override string ResetPassword(string username, string answer)
        {
            throw new NotSupportedException("ResetPassword() not supported in " + providerFullName);
        }

        //not supported
        public override void UpdateUser(MembershipUser user)
        {
            throw new NotSupportedException("UpdateUser() not supported in " + providerFullName);
        }

        //OK
        public override bool ValidateUser(string username, string password)
        {
            bool result = false;
            try
            {
                var ad = new ADAuthenticationHelper(
                    this.domain, this.contextUsername, this.contextPassword);
                result = ad.IsAuthenticated(this.domain, username, password);
            }
            catch (Exception ex)
            {
                throw new ProviderException("ValidateUser(string, string) error in " + providerFullName, ex);
            }
            return result;
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            var res = new MembershipUserCollection();
            totalRecords = 0;   //not filled
            res = FindUsersByName("", pageIndex, pageSize, out totalRecords);
            return res;
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            var res = new MembershipUserCollection();
            totalRecords = 0;   //not filled

            try
            {
                var ad = new ADAuthenticationHelper(
                    this.domain, this.contextUsername, this.contextPassword);
                
                var users = ad.FindUsers(usernameToMatch);
                int counter = 0;
                int startIndex = pageSize * pageIndex;
                int endIndex = startIndex + pageSize - 1;

                foreach (var user in users)
                {
                    if (counter >= startIndex)
                    {
                        MembershipUser u = GetUser(user, true);
                        res.Add(u);
                    }
                    if (endIndex > 0)
                    {
                        if (counter >= endIndex) { break; }
                    }
                    counter++;
                }
            }
            catch (Exception ex)
            {
                throw new ProviderException("FindUsersByName() error in " + providerName, ex);
            }
            return res;
        }

        #endregion
    }


    public sealed class ADAuthenticationHelper
    {
        private string domain = "";
        private string ctxUsername = "";
        private string ctxPassword = "";

        public ADAuthenticationHelper(string domain, string ctxUsername, string ctxPassword)
        {
            this.domain = domain;
            this.ctxUsername = ctxUsername;
            this.ctxPassword = ctxPassword;
        }

        //OK
        public bool IsAuthenticated(string domain, string username, string password)
        {
            bool result = false;
            using (var context = new PrincipalContext(
                ContextType.Domain, this.domain, this.ctxUsername, this.ctxPassword))
            {
                result = context.ValidateCredentials(username, password);
            }
            return result;
        }

        //OK
        public List<string> GetUserGroups(string username)
        {
            var result = new List<string>();
            try
            {
                PrincipalContext context = new PrincipalContext(
                    ContextType.Domain, this.domain, this.ctxUsername, this.ctxPassword);
                UserPrincipal user = UserPrincipal.FindByIdentity(context, username);
                if (user != null)
                {

                    PrincipalSearchResult<Principal> groups = user.GetAuthorizationGroups();
                    foreach (Principal p in groups)
                    {
                        if (p is GroupPrincipal)
                                result.Add(p.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error obtaining user groups. " + ex.ToString());
            }
            return result;
        }


        //OK
        public List<string> FindUsers(string usernameLike = "")
        {
            var result = new List<string>();
            try
            {
                PrincipalContext context = new PrincipalContext(
                    ContextType.Domain, this.domain, this.ctxUsername, this.ctxPassword);
                PrincipalSearcher ps = new PrincipalSearcher(new UserPrincipal(context));

                usernameLike = usernameLike.ToLower();
                foreach (var user in ps.FindAll())
                {
                    if (string.IsNullOrEmpty(usernameLike))
                        result.Add(user.SamAccountName);
                    else
                    {
                        if (user.Name.ToLower().Contains(usernameLike))
                        {
                            result.Add(user.SamAccountName);
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error obtaining all groups. " + e.ToString());
            }

            return result;
        }

        //OK
        public List<string> GetAllGroups(string groupname = "")
        {
            var result = new List<string>();
            try
            {
                PrincipalContext context = new PrincipalContext(
                    ContextType.Domain, this.domain, this.ctxUsername, this.ctxPassword);
                GroupPrincipal findAllGroups = new GroupPrincipal(context, "*");
                PrincipalSearcher ps = new PrincipalSearcher(findAllGroups);
                foreach (var group in ps.FindAll())
                {
                    if (string.IsNullOrEmpty(groupname))
                        result.Add(group.Name);
                    else
                    {
                        if (group.Name.ToLower() == groupname.ToLower())
                        {
                            result.Add(group.Name);
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error obtaining all groups. " + e.ToString());
            }

            return result;
        }

        //OK
        public List<string> GetUsersInGroup(string groupName, bool recursive = false, string username = "")
        {
            var result = new List<string>();
            using (var context = new PrincipalContext(ContextType.Domain, 
                this.domain, this.ctxUsername, this.ctxPassword))
            {
                using (var group = GroupPrincipal.FindByIdentity(context, IdentityType.Name, groupName))
                {
                    if (group != null)
                    {
                        var users = group.GetMembers(recursive);
                        foreach (Principal item in users)
                        {
                            string user = item.SamAccountName;
                            if (string.IsNullOrEmpty(username))
                                result.Add(user);
                            else
                            {
                                if (username.ToLower() == user.ToLower())
                                {
                                    //used from Roles.IsUserInRole
                                    result.Add(user);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}
