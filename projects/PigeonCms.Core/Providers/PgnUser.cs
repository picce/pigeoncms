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
using System.ComponentModel;

namespace PigeonCms
{
    public static class PgnUserCurrent
    {
        public static string UserName
        {
            get
            {
                string res = "";
                if (HttpContext.Current != null && HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    res = HttpContext.Current.User.Identity.Name;
                }
                return res;
            }
        }

        public static bool IsAuthenticated
        {
            get
            {
                bool res = false;
                if (HttpContext.Current != null && HttpContext.Current.User != null)
                    res = HttpContext.Current.User.Identity.IsAuthenticated;
                return res;
            }
        }

        public static PgnUser Current
        {
            get
            {
                PgnUser current = new PgnUser();
                if (PgnUserCurrent.IsAuthenticated)
                {
                    if (HttpContext.Current.Session["CurrentPgnUser"] == null)
                    {
                        current = (PgnUser)Membership.GetUser(HttpContext.Current.User.Identity.Name);
                        HttpContext.Current.Session["CurrentPgnUser"] = current;
                    }
                    else
                    {
                        current = (PgnUser)HttpContext.Current.Session["CurrentPgnUser"];
                    }
                }
                return current;
            }
        }

        /// <summary>
        /// default cast for Membership.GetUser method
        /// use to manage MembershipProvider that not returns PgnUser
        /// </summary>
        /// <param name="username"></param>
        /// <returns>PgnUser or null if not exists</returns>
        public static PgnUser GetUser(string username)
        {
            PgnUser user = null;
            var member = Membership.GetUser(username);
            if (member is PgnUser)
                user = (PgnUser)member;
            else
            {
                user = new PgnUser("PgnUserProvider", 
                    0, username, username, "", "", "", "", true, false,
                    DateTime.MinValue, DateTime.MinValue, DateTime.MinValue,
                    DateTime.MinValue, DateTime.MinValue);
            }
            return user;
        }
    }


    /// <summary>
    /// a PigeonCms authenticated user
    /// </summary>
    public class PgnUser: MembershipUser
    {
        #region public fields

        public int Id { get; set; }

        private bool enabled = true;
        public bool Enabled
        {
            [DebuggerStepThrough()]
            get { return enabled; }
            [DebuggerStepThrough()]
            set { enabled = value; }
        }

        private string nickName = "";
        /// <summary>
        /// default is the same as username
        /// </summary>
        public string NickName
        {
            [DebuggerStepThrough()]
            get
            {
                if (string.IsNullOrEmpty(nickName))
                    nickName = this.UserName;
                return nickName;
            }
            [DebuggerStepThrough()]
            set { nickName = value; }
        }

        private string accessCode = "";
        public string AccessCode
        {
            [DebuggerStepThrough()]
            get { return accessCode; }
            [DebuggerStepThrough()]
            set { accessCode = value; }
        }

        private int accessLevel = 0;
        public int AccessLevel
        {
            [DebuggerStepThrough()]
            get { return accessLevel; }
            [DebuggerStepThrough()]
            set { accessLevel = value; }
        }


        public bool IsCore { get; set; }


        private string sex = "";
        public string Sex
        {
            [DebuggerStepThrough()]
            get { return sex; }
            [DebuggerStepThrough()]
            set 
            {
                value = value.ToUpper();
                if (value != "M" && value != "F" && value != "")
                    throw new ArgumentException("sex not valid");
                sex = value; 
            }
        }

        private string companyName = "";
        public string CompanyName
        {
            [DebuggerStepThrough()]
            get { return companyName; }
            [DebuggerStepThrough()]
            set { companyName = value; }
        }

        private string vat = "";
        /// <summary>
        /// value added tax, piva in italy
        /// </summary>
        public string Vat
        {
            [DebuggerStepThrough()]
            get { return vat; }
            [DebuggerStepThrough()]
            set { vat = value; }
        }

        private string ssn = "";
        /// <summary>
        /// SocialSecurityNumber, codice fiscale in italy
        /// </summary>
        public string Ssn
        {
            [DebuggerStepThrough()]
            get { return ssn; }
            [DebuggerStepThrough()]
            set { ssn = value; }
        }


        private string firstName = "";
        public string FirstName
        {
            [DebuggerStepThrough()]
            get { return firstName; }
            [DebuggerStepThrough()]
            set { firstName = value; }
        }

        private string secondName = "";
        public string SecondName
        {
            [DebuggerStepThrough()]
            get { return secondName; }
            [DebuggerStepThrough()]
            set { secondName = value; }
        }

        private string address1 = "";
        public string Address1
        {
            [DebuggerStepThrough()]
            get { return address1; }
            [DebuggerStepThrough()]
            set { address1 = value; }
        }

        private string address2 = "";
        public string Address2
        {
            [DebuggerStepThrough()]
            get { return address2; }
            [DebuggerStepThrough()]
            set { address2 = value; }
        }

        private string city = "";
        public string City
        {
            [DebuggerStepThrough()]
            get { return city; }
            [DebuggerStepThrough()]
            set { city = value; }
        }

        private string state = "";
        /// <summary>
        /// state or prov in italy
        /// </summary>
        public string State
        {
            [DebuggerStepThrough()]
            get { return state; }
            [DebuggerStepThrough()]
            set { state = value; }
        }

        private string zipCode = "";
        /// <summary>
        /// zip or cap in italy
        /// </summary>
        public string ZipCode
        {
            [DebuggerStepThrough()]
            get { return zipCode; }
            [DebuggerStepThrough()]
            set { zipCode = value; }
        }

        private string nation = "";
        public string Nation
        {
            [DebuggerStepThrough()]
            get { return nation; }
            [DebuggerStepThrough()]
            set { nation = value; }
        }

        private string tel1 = "";
        public string Tel1
        {
            [DebuggerStepThrough()]
            get { return tel1; }
            [DebuggerStepThrough()]
            set { tel1 = value; }
        }

        private string mobile1 = "";
        public string Mobile1
        {
            [DebuggerStepThrough()]
            get { return mobile1; }
            [DebuggerStepThrough()]
            set { mobile1 = value; }
        }

        private string website1 = "";
        public string Website1
        {
            [DebuggerStepThrough()]
            get { return website1; }
            [DebuggerStepThrough()]
            set { website1 = value; }
        }

        public bool AllowMessages { get; set; }

        public bool AllowEmails { get; set; }


        private string validationCode = "";
        public string ValidationCode
        {
            [DebuggerStepThrough()]
            get { return validationCode; }
            [DebuggerStepThrough()]
            set { validationCode = value; }
        }

        #endregion


        public PgnUser(): base() { }

        public PgnUser(string providername,
                        int id,
                        string username,
                        string nickName,
                        object providerUserKey,
                        string email,
                        string passwordQuestion,
                        string comment,
                        bool isApproved,
                        bool isLockedOut,
                        DateTime creationDate,
                        DateTime lastLoginDate,
                        DateTime lastActivityDate,
                        DateTime lastPasswordChangedDate,
                        DateTime lastLockedOutDate) :
                        base(providername,
                           username,
                           providerUserKey,
                           email,
                           passwordQuestion,
                           comment,
                           isApproved,
                           isLockedOut,
                           creationDate,
                           lastLoginDate,
                           lastActivityDate,
                           lastPasswordChangedDate,
                           lastLockedOutDate)

        {
            this.Id = id;
        }
    }
}
