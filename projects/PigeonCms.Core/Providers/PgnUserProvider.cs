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
using System.Text.RegularExpressions;

namespace PigeonCms
{
    /// <summary>
    /// PigeonCms custom membership provider
    /// see http://msdn.microsoft.com/en-us/library/6tc47t75.aspx
    /// 20091217: removed sealed class def
    /// </summary>
    public class PgnUserProvider: MembershipProvider
    {
        #region private vars
		private string[] coreUsers = { "admin" };
        private int newPasswordLength = 8;
        private string eventSource = "PgnUserProvider";
        private string eventLog = "Application";
        private string exceptionMessage = "An exception occurred in PigeonCms.PgnUserProvider.";
        private string connectionString;

        // Used when determining encryption key values.
        private MachineKeySection machineKey;

        // If false, exceptions are thrown to the caller. If true,
        // exceptions are written to the event log.
        private bool pWriteExceptionsToEventLog;
        public bool WriteExceptionsToEventLog
        {
            get { return pWriteExceptionsToEventLog; }
            set { pWriteExceptionsToEventLog = value; }
        }
        #endregion


        #region private methods

        private bool checkPassword(string password, string dbpassword)
        {
            string pass1 = password;
            string pass2 = dbpassword;

            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Encrypted:
                    pass2 = unEncodePassword(dbpassword);
                    break;
                case MembershipPasswordFormat.Hashed:
                    pass1 = encodePassword(password);
                    break;
                default:
                    break;
            }

            if (pass1 == pass2)
            {
                return true;
            }

            return false;
        }

        private string encodePassword(string password)
        {
            string encodedPassword = password;

            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Clear:
                    break;
                case MembershipPasswordFormat.Encrypted:
                    encodedPassword =
                      Convert.ToBase64String(EncryptPassword(Encoding.Unicode.GetBytes(password)));
                    break;
                case MembershipPasswordFormat.Hashed:
                    HMACSHA1 hash = new HMACSHA1();
                    hash.Key = hexToByte(machineKey.ValidationKey);
                    encodedPassword =
                      Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));
                    break;
                default:
                    throw new ProviderException("Unsupported password format.");
            }

            return encodedPassword;
        }

        private string unEncodePassword(string encodedPassword)
        {
            string password = encodedPassword;

            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Clear:
                    break;
                case MembershipPasswordFormat.Encrypted:
                    try
                    {
                        password =
                          Encoding.Unicode.GetString(DecryptPassword(Convert.FromBase64String(password)));
                    }
                    catch (FormatException) {/* the password is in clear format */ }
                    break;
                case MembershipPasswordFormat.Hashed:
                    throw new ProviderException("Cannot unencode a hashed password.");
                default:
                    throw new ProviderException("Unsupported password format.");
            }

            return password;
        }

        // HexToByte
        //   Converts a hexadecimal string to a byte array. Used to convert encryption
        // key values from the configuration.
        private byte[] hexToByte(string hexString)
        {
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

		private bool isCoreUser(string username)
		{
			bool res = false;
			for (int i = 0; i < coreUsers.Length; i++)
			{
				if (username.ToLower() == coreUsers[i].ToLower())
				{
					res = true;
					break;
				}
			}
			return res;
		}

        // WriteToEventLog
        //   A helper function that writes exception detail to the event log. Exceptions
        // are written to the event log as a security measure to avoid private database
        // details from being returned to the browser. If a method does not return a status
        // or boolean indicating the action succeeded or failed, a generic exception is also 
        // thrown by the caller.
        private void writeToEventLog(Exception e, string action)
        {
            EventLog log = new EventLog();
            log.Source = eventSource;
            log.Log = eventLog;

            string message = "An exception occurred communicating with the data source.\n\n";
            message += "Action: " + action + "\n\n";
            message += "Exception: " + e.ToString();

            log.WriteEntry(message);
        }

        private void updateFailureCount(string username, string failureType)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;

            DateTime windowStart = new DateTime();
            int failureCount = 0;

            try
            {
                myConn.ConnectionString = connectionString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT FailedPasswordAttemptCount, "
                + " FailedPasswordAttemptWindowStart, "
                + " FailedPasswordAnswerAttemptCount, "
                + " FailedPasswordAnswerAttemptWindowStart "
                + " FROM " + tableName + " "
                + " WHERE Username = @Username AND ApplicationName = @ApplicationName ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Username", username));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ApplicationName", pApplicationName));

                myRd = myCmd.ExecuteReader();

                if (myRd.HasRows)
                {
                    myRd.Read();

                    if (failureType == "password")
                    {
                        if (!Convert.IsDBNull(myRd["FailedPasswordAttemptCount"]))
                            failureCount = (int)myRd["FailedPasswordAttemptCount"];
                        if (!Convert.IsDBNull(myRd["FailedPasswordAttemptWindowStart"]))
                            windowStart = (DateTime)myRd["FailedPasswordAttemptWindowStart"];
                    }

                    if (failureType == "passwordAnswer")
                    {
                        if (!Convert.IsDBNull(myRd["FailedPasswordAnswerAttemptCount"]))
                            failureCount = (int)myRd["FailedPasswordAnswerAttemptCount"];
                        if (!Convert.IsDBNull(myRd["FailedPasswordAnswerAttemptWindowStart"]))
                            windowStart = (DateTime)myRd["FailedPasswordAnswerAttemptWindowStart"];
                    }
                }
                myRd.Close();

                DateTime windowEnd = windowStart.AddMinutes(PasswordAttemptWindow);

                if (failureCount == 0 || DateTime.Now > windowEnd)
                {
                    // First password failure or outside of PasswordAttemptWindow. 
                    // Start a new password failure count from 1 and a new window starting now.

                    if (failureType == "password")
                        sSql = "UPDATE " + tableName + " " +
                                          "  SET FailedPasswordAttemptCount = @Count, " +
                                          "      FailedPasswordAttemptWindowStart = @WindowStart " +
                                          "  WHERE Username = @Username AND ApplicationName = @ApplicationName ";

                    if (failureType == "passwordAnswer")
                        sSql = "UPDATE " + tableName + " " +
                                          "  SET FailedPasswordAnswerAttemptCount = @Count, " +
                                          "      FailedPasswordAnswerAttemptWindowStart = @WindowStart " +
                                          "  WHERE Username = @Username AND ApplicationName = @ApplicationName ";

                    myCmd.CommandText = Database.ParseSql(sSql);
                    myCmd.Parameters.Clear();

                    myCmd.Parameters.Add(Database.Parameter(myProv, "Count", 1));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "WindowStart", DateTime.Now));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Username", username));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ApplicationName", pApplicationName));

                    if (myCmd.ExecuteNonQuery() < 0)
                        throw new ProviderException("Unable to update failure count and window start.");
                }
                else
                {
                    if (failureCount++ >= MaxInvalidPasswordAttempts && MaxInvalidPasswordAttempts > 0)
                    {
                        // Password attempts have exceeded the failure threshold. Lock out
                        // the user.

                        sSql = "UPDATE " + tableName + " " +
                          "  SET IsLockedOut=@IsLockedOut, LastLockedOutDate = @LastLockedOutDate, " +
                          "  FailedPasswordAttemptCount=0, FailedPasswordAnswerAttemptCount=0 " +
                          "  WHERE Username = @Username AND ApplicationName = @ApplicationName ";
                        myCmd.CommandText = Database.ParseSql(sSql);
                        myCmd.Parameters.Clear();

                        myCmd.Parameters.Add(Database.Parameter(myProv, "IsLockedOut", 1));
                        //myCmd.Parameters.Add(Database.Parameter(myProv, "Enabled", false));
                        myCmd.Parameters.Add(Database.Parameter(myProv, "LastLockedOutDate", DateTime.Now));
                        myCmd.Parameters.Add(Database.Parameter(myProv, "Username", username));
                        myCmd.Parameters.Add(Database.Parameter(myProv, "ApplicationName", pApplicationName));

                        if (myCmd.ExecuteNonQuery() < 0)
                            throw new ProviderException("Unable to lock out user.");
                    }
                    else
                    {
                        // Password attempts have not exceeded the failure threshold. Update
                        // the failure counts. Leave the window the same.
                        if (failureType == "password")
                            sSql = "UPDATE " + tableName + " "
                            + " SET FailedPasswordAttemptCount = @Count "
                            + " WHERE Username = @Username AND ApplicationName = @ApplicationName ";

                        if (failureType == "passwordAnswer")
                            sSql = "UPDATE " + tableName + " "
                            + " SET FailedPasswordAnswerAttemptCount = @Count "
                            + " WHERE Username = @Username AND ApplicationName = @ApplicationName ";

                        myCmd.CommandText = Database.ParseSql(sSql);
                        myCmd.Parameters.Clear();

                        myCmd.Parameters.Add(Database.Parameter(myProv, "Count", failureCount));
                        myCmd.Parameters.Add(Database.Parameter(myProv, "Username", username));
                        myCmd.Parameters.Add(Database.Parameter(myProv, "ApplicationName", pApplicationName));

                        if (myCmd.ExecuteNonQuery() < 0)
                            throw new ProviderException("Unable to update failure count.");
                    }
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    writeToEventLog(e, "UpdateFailureCount");
                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (myRd != null) { myRd.Close(); }
                myRd.Close();
            }
        }

        #endregion

        #region protected methods

        // helper function to retrieve config values from the configuration file.
        protected string GetConfigValue(string configValue, string defaultValue)
        {
            if (String.IsNullOrEmpty(configValue))
                return defaultValue;

            return configValue;
        }

        protected string GetSqlSelect()
        {
            string res = "SELECT [Id], Username, NickName, Email, PasswordQuestion," +
                 " Comment, IsApproved, IsLockedOut, CreationDate, LastLoginDate," +
                 " LastActivityDate, LastPasswordChangedDate, LastLockedOutDate, " +
                 " Enabled, AccessCode, AccessLevel, IsCore, " +
                 " Sex, CompanyName, Vat, Ssn, FirstName, SecondName, " +
                 " Address1, Address2, City, [State], ZipCode, " +
                 " Nation, Tel1, Mobile1, Website1, AllowMessages, AllowEmails, ValidationCode " +
                 " FROM " + tableName + " ";
            return res;
        }

        protected void SetLastActivityDate(DbConnection myConn, DbProviderFactory myProv, string username, bool userIsOnline)
        {
            string sSql = "";

            if (userIsOnline)
            {
                DbCommand updateCmd = myConn.CreateCommand();
                updateCmd.Connection = myConn;

                sSql = "UPDATE " + tableName + " " +
                  " SET LastActivityDate = @LastActivityDate " +
                  " WHERE Username = @Username AND Applicationname = @Applicationname ";
                updateCmd.CommandText = Database.ParseSql(sSql);
                updateCmd.Parameters.Add(Database.Parameter(myProv, "LastActivityDate", DateTime.Now));
                updateCmd.Parameters.Add(Database.Parameter(myProv, "Username", username));
                updateCmd.Parameters.Add(Database.Parameter(myProv, "Applicationname", pApplicationName));

                updateCmd.ExecuteNonQuery();
            }
        }

        protected PgnUser GetUserFromReader(DbDataReader myRd)
        {
            object providerUserKey = null; //= myRd.GetValue(0);
            int id = 0;
            string username = "";
            string nickName = "";//added 20170513
            string email = "";
            string passwordQuestion = "";
            string comment = "";
            bool isApproved = false;
            bool isLockedOut = false;
            DateTime creationDate = new DateTime();
            DateTime lastLoginDate = new DateTime();
            DateTime lastActivityDate = new DateTime();
            DateTime lastPasswordChangedDate = new DateTime();
            DateTime lastLockedOutDate = new DateTime();
            bool enabled = false;
            string accessCode = "";
            int accessLevel = 0;
            bool isCore = false;
            //20111118
            string sex = "";
            string companyName = "";
            string vat = "";
            string ssn = "";
            string firstName = "";
            string secondName = "";
            string address1 = "";
            string address2 = "";
            string city = "";
            string state = "";
            string zipCode = "";
            string nation = "";
            string tel1 = "";
            string mobile1 = "";
            string website1 = "";
            //20120306
            bool allowMessages = false;
            bool allowEmails = false;
            //20150403
            string validationCode = "";


            if (!Convert.IsDBNull(myRd["Id"]))
                id = (int)myRd["Id"];
            if (!Convert.IsDBNull(myRd["Username"]))
                username = (string)myRd["Username"];
            if (!Convert.IsDBNull(myRd["nickName"]))
                nickName = (string)myRd["nickName"];
            if (!Convert.IsDBNull(myRd["Email"]))
                email = (string)myRd["Email"];
            if (!Convert.IsDBNull(myRd["PasswordQuestion"]))
                passwordQuestion = (string)myRd["PasswordQuestion"];
            if (!Convert.IsDBNull(myRd["Comment"]))
                comment = (string)myRd["Comment"];
            if (!Convert.IsDBNull(myRd["IsApproved"]))
                isApproved = (bool)myRd["IsApproved"];
            if (!Convert.IsDBNull(myRd["IsLockedOut"]))
                isLockedOut = (bool)myRd["IsLockedOut"];
            if (!Convert.IsDBNull(myRd["creationDate"]))
                creationDate = (DateTime)myRd["creationDate"];
            if (!Convert.IsDBNull(myRd["lastLoginDate"]))
                lastLoginDate = (DateTime)myRd["lastLoginDate"];
            if (!Convert.IsDBNull(myRd["lastActivityDate"]))
                lastActivityDate = (DateTime)myRd["lastActivityDate"];
            if (!Convert.IsDBNull(myRd["lastPasswordChangedDate"]))
                lastPasswordChangedDate = (DateTime)myRd["lastPasswordChangedDate"];
            if (!Convert.IsDBNull(myRd["lastLockedOutDate"]))
                lastLockedOutDate = (DateTime)myRd["lastLockedOutDate"];
            if (!Convert.IsDBNull(myRd["enabled"]))
                enabled = (bool)myRd["enabled"];
            if (!Convert.IsDBNull(myRd["accessCode"]))
                accessCode = (string)myRd["accessCode"];
            if (!Convert.IsDBNull(myRd["AccessLevel"]))
                accessLevel = (int)myRd["AccessLevel"];
            if (!Convert.IsDBNull(myRd["isCore"]))
                isCore = (bool)myRd["isCore"];
            if (!Convert.IsDBNull(myRd["allowMessages"]))
                allowMessages = (bool)myRd["allowMessages"];
            if (!Convert.IsDBNull(myRd["allowEmails"]))
                allowEmails = (bool)myRd["allowEmails"];

            if (!Convert.IsDBNull(myRd["sex"]))
                sex = (string)myRd["sex"];
            if (!Convert.IsDBNull(myRd["companyName"]))
                companyName = (string)myRd["companyName"];
            if (!Convert.IsDBNull(myRd["vat"]))
                vat = (string)myRd["vat"];
            if (!Convert.IsDBNull(myRd["ssn"]))
                ssn = (string)myRd["ssn"];
            if (!Convert.IsDBNull(myRd["firstName"]))
                firstName = (string)myRd["firstName"];
            if (!Convert.IsDBNull(myRd["secondName"]))
                secondName = (string)myRd["secondName"];
            if (!Convert.IsDBNull(myRd["address1"]))
                address1 = (string)myRd["address1"];
            if (!Convert.IsDBNull(myRd["address2"]))
                address2 = (string)myRd["address2"];
            if (!Convert.IsDBNull(myRd["city"]))
                city = (string)myRd["city"];
            if (!Convert.IsDBNull(myRd["state"]))
                state = (string)myRd["state"];
            if (!Convert.IsDBNull(myRd["zipCode"]))
                zipCode = (string)myRd["zipCode"];
            if (!Convert.IsDBNull(myRd["nation"]))
                nation = (string)myRd["nation"];
            if (!Convert.IsDBNull(myRd["tel1"]))
                tel1 = (string)myRd["tel1"];
            if (!Convert.IsDBNull(myRd["mobile1"]))
                mobile1 = (string)myRd["mobile1"];
            if (!Convert.IsDBNull(myRd["website1"]))
                website1 = (string)myRd["website1"];
            if (!Convert.IsDBNull(myRd["validationCode"]))
                validationCode = (string)myRd["validationCode"];

            string name = "PgnUserProvider";
            if (this.Name != null) name = this.Name;

            PgnUser u = new PgnUser(name,
                                  id,
                                  username,
                                  nickName,
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
                                  lastLockedOutDate);
            u.Enabled = enabled;
            u.AccessCode = accessCode;
            u.AccessLevel = accessLevel;
            u.IsCore = isCore;
            u.AllowMessages = allowMessages;
            u.AllowEmails = allowEmails;
            u.NickName = nickName;

            u.Sex = sex;
            u.CompanyName = companyName;
            u.Vat = vat;
            u.Ssn = ssn;
            u.FirstName = firstName;
            u.SecondName = secondName;
            u.Address1 = address1;
            u.Address2 = address2;
            u.City = city;
            u.State = state;
            u.ZipCode = zipCode;
            u.Nation = nation;
            u.Tel1 = tel1;
            u.Mobile1 = mobile1;
            u.Website1 = website1;
            u.ValidationCode = validationCode;

            return u;
        }

        #endregion

        #region properties

        private string tableName = "";
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string pApplicationName = "";
        public override string ApplicationName
        {
            get { return pApplicationName; }
            set { pApplicationName = value; }
        }


        private bool pEnablePasswordReset = false;
        public override bool EnablePasswordReset
        {
            get { return pEnablePasswordReset; }
        }

        private bool pEnablePasswordRetrieval = false;
        public override bool EnablePasswordRetrieval
        {
            get { return pEnablePasswordRetrieval; }
        }

        private bool pRequiresQuestionAndAnswer = false;
        public override bool RequiresQuestionAndAnswer
        {
            get { return pRequiresQuestionAndAnswer; }
        }

        private bool pRequiresUniqueEmail = false;
        public override bool RequiresUniqueEmail
        {
            get { return pRequiresUniqueEmail; }
        }

        private int pMaxInvalidPasswordAttempts = 5;
        public override int MaxInvalidPasswordAttempts
        {
            get { return pMaxInvalidPasswordAttempts; }
        }

        private int pPasswordAttemptWindow = 10;
        public override int PasswordAttemptWindow
        {
            get { return pPasswordAttemptWindow; }
        }

        private MembershipPasswordFormat pPasswordFormat;
        public override MembershipPasswordFormat PasswordFormat
        {
            get { return pPasswordFormat; }
        }

        private int pMinRequiredNonAlphanumericCharacters = 0;
        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return pMinRequiredNonAlphanumericCharacters; }
        }

        private int pMinRequiredPasswordLength = 6;
        public override int MinRequiredPasswordLength
        {
            get { return pMinRequiredPasswordLength; }
        }

        private string pPasswordStrengthRegularExpression;
        public override string PasswordStrengthRegularExpression
        {
            get { return pPasswordStrengthRegularExpression; }
        }
        #endregion


        #region public methods

        // System.Configuration.Provider.ProviderBase.Initialize Method
        public override void Initialize(string name, NameValueCollection config)
        {
            // Initialize values from web.config
            if (config == null)
                throw new ArgumentNullException("config");

            if (name == null || name.Length == 0)
                name = "PgnUserProvider";

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "PigeonCms custom user provider");
            }

            // Initialize the abstract base class.
            base.Initialize(name, config);

            this.TableName = "#__memberUsers";

            pApplicationName = GetConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            pMaxInvalidPasswordAttempts = Convert.ToInt32(GetConfigValue(config["maxInvalidPasswordAttempts"], "5"));
            pPasswordAttemptWindow = Convert.ToInt32(GetConfigValue(config["passwordAttemptWindow"], "10"));
            pMinRequiredNonAlphanumericCharacters = Convert.ToInt32(GetConfigValue(config["minRequiredNonAlphanumericCharacters"], "1"));
            pMinRequiredPasswordLength = Convert.ToInt32(GetConfigValue(config["minRequiredPasswordLength"], "7"));
            pPasswordStrengthRegularExpression = Convert.ToString(GetConfigValue(config["passwordStrengthRegularExpression"], ""));
            pEnablePasswordReset = Convert.ToBoolean(GetConfigValue(config["enablePasswordReset"], "true"));
            pEnablePasswordRetrieval = Convert.ToBoolean(GetConfigValue(config["enablePasswordRetrieval"], "true"));
            pRequiresQuestionAndAnswer = Convert.ToBoolean(GetConfigValue(config["requiresQuestionAndAnswer"], "false"));
            pRequiresUniqueEmail = Convert.ToBoolean(GetConfigValue(config["requiresUniqueEmail"], "false"));
            pWriteExceptionsToEventLog = Convert.ToBoolean(GetConfigValue(config["writeExceptionsToEventLog"], "false"));

            string temp_format = config["passwordFormat"];
            if (temp_format == null)
            {
                //temp_format = "Hashed";
                temp_format = "Clear";
            }

            switch (temp_format)
            {
                case "Hashed":
                    pPasswordFormat = MembershipPasswordFormat.Hashed;
                    break;
                case "Encrypted":
                    pPasswordFormat = MembershipPasswordFormat.Encrypted;
                    break;
                case "Clear":
                    pPasswordFormat = MembershipPasswordFormat.Clear;
                    break;
                default:
                    throw new ProviderException("Password format not supported.");
            }

            // Initialize OdbcConnection.
            ConnectionStringSettings ConnectionStringSettings =
              ConfigurationManager.ConnectionStrings[config["connectionStringName"]];

            if (ConnectionStringSettings == null || ConnectionStringSettings.ConnectionString.Trim() == "")
                connectionString = Database.ConnString;
            else
                connectionString = ConnectionStringSettings.ConnectionString;


            // Get encryption and decryption key information from the configuration.
            //null instead of System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath
            Configuration cfg = WebConfigurationManager.OpenWebConfiguration(null);
            machineKey = (MachineKeySection)cfg.GetSection("system.web/machineKey");

            /*if (machineKey.ValidationKey.Contains("AutoGenerate"))
                if (PasswordFormat != MembershipPasswordFormat.Clear)
                    throw new ProviderException("Hashed or Encrypted passwords " +
                                                "are not supported with auto-generated keys.");*/
        }

        public override bool ChangePassword(string username, string oldPwd, string newPwd)
        {
            return ChangePassword(username, oldPwd, newPwd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="oldPwd"></param>
        /// <param name="newPwd"></param>
        /// <param name="reset">if true, oldPwd is not checked</param>
        /// <returns></returns>
        public bool ChangePassword(string username, string oldPwd, string newPwd, bool reset)
        {
            if (!reset)
            {
                if (!ValidateUser(username, oldPwd))
                    return false;
            }

            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, newPwd, true);

            OnValidatingPassword(args);

            if (args.Cancel)
                if (args.FailureInformation != null)
                    throw args.FailureInformation;
                else
                    throw new MembershipPasswordException("Change password canceled due to new password validation failure.");


            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql = "";
            int rowsAffected = 0;
            try
            {
                myConn.ConnectionString = connectionString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "UPDATE " + tableName + " "
                + " SET Password=@Password, LastPasswordChangedDate=@LastPasswordChangedDate "
                + " WHERE Username=@Username AND ApplicationName=@ApplicationName ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Password", encodePassword(newPwd)));
                myCmd.Parameters.Add(Database.Parameter(myProv, "LastPasswordChangedDate", DateTime.Now));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Username", username));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ApplicationName", pApplicationName));
                myCmd.CommandText = Database.ParseSql(sSql);
                rowsAffected = myCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    writeToEventLog(e, "ChangePassword");
                    throw new ProviderException(exceptionMessage);
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

            if (rowsAffected > 0)
                return true;
            else
                return false;
        }


        public override bool ChangePasswordQuestionAndAnswer(string username, string password,
                                                string newPwdQuestion, string newPwdAnswer)
        {
            if (!ValidateUser(username, password))
                return false;

            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql = "";
            int rowsAffected = 0;

            try
            {
                myConn.ConnectionString = connectionString;
                myConn.Open();
                myCmd.Connection = myConn;

                if (string.IsNullOrEmpty(newPwdQuestion)) newPwdQuestion = "";

                sSql = "UPDATE " + tableName + " "
                + " SET PasswordQuestion=@PasswordQuestion, PasswordAnswer=@PasswordAnswer "
                + " WHERE Username=@Username AND ApplicationName=@ApplicationName ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Question", newPwdQuestion));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Answer", encodePassword(newPwdAnswer)));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Username", username));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ApplicationName", pApplicationName));
                myCmd.CommandText = Database.ParseSql(sSql);
                rowsAffected = myCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    writeToEventLog(e, "ChangePasswordQuestionAndAnswer");
                    throw new ProviderException(exceptionMessage);
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

            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }


        public override MembershipUser CreateUser(string username,
                                          string password,
                                          string email,
                                          string passwordQuestion,
                                          string passwordAnswer,
                                          bool isApproved,
                                          object providerUserKey,
                                          out MembershipCreateStatus status)
        {
            var newUser = new PgnUser();

            return this.CreateUser(
                newUser,
                username,
                username/*as NickName*/,
                password, 
                email,
                passwordQuestion, 
                passwordAnswer, 
                true, 
                "",
                out status);
        }

        public PgnUser CreateUser(
                        PgnUser newUser,
                        string username,
                        string nickName,
                        string password,
                        string email,
                        string passwordQuestion,
                        string passwordAnswer,
                        bool isApproved,
                        object providerUserKey,
                        out MembershipCreateStatus status)
        {
            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, password, true);
            OnValidatingPassword(args);
            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            if (RequiresUniqueEmail && GetUserNameByEmail(email) != "")
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            MembershipUser u = GetUser(username, false);

            if (u == null)
            {
                DateTime createDate = DateTime.Now;

                //use identity as pkey
                //if (providerUserKey == null)
                //{
                //    providerUserKey = Guid.NewGuid();
                //}
                //else
                //{
                //    if (!(providerUserKey is Guid))
                //    {
                //        status = MembershipCreateStatus.InvalidProviderUserKey;
                //        return null;
                //    }
                //}

                DbProviderFactory myProv = Database.ProviderFactory;
                DbConnection myConn = myProv.CreateConnection();
                DbCommand myCmd = myConn.CreateCommand();
                string sSql = "";

                try
                {
                    myConn.ConnectionString = connectionString;
                    myConn.Open();
                    myCmd.Connection = myConn;

                    //PKID
                    sSql = "INSERT INTO " + tableName + " "
                    + " (/*[Id],*/ Username, NickName, ApplicationName, Email, Comment, "
                    + " Password, PasswordQuestion, PasswordAnswer, IsApproved, "
                    + " LastActivityDate, LastPasswordChangedDate, CreationDate, "
                    + " IsLockedOut, LastLockedOutDate,"
                    + " FailedPasswordAttemptCount, FailedPasswordAttemptWindowStart, "
                    + " FailedPasswordAnswerAttemptCount, FailedPasswordAnswerAttemptWindowStart, "
                    + " Enabled, AccessCode, AccessLevel, "
                    + " Sex, CompanyName, Vat, Ssn, FirstName, SecondName, "
                    + " Address1, Address2, City, [State], ZipCode, "
                    + " Nation, Tel1, Mobile1, Website1, AllowMessages, AllowEmails, ValidationCode) "
                    + " Values(@Username, @NickName, @ApplicationName, @Email, @Comment, "
                    + " @Password, @PasswordQuestion, @PasswordAnswer, @IsApproved, "
                    + " @LastActivityDate, @LastPasswordChangedDate, @CreationDate, "
                    + " @IsLockedOut, @LastLockedOutDate,"
                    + " @FailedPasswordAttemptCount, @FailedPasswordAttemptWindowStart, "
                    + " @FailedPasswordAnswerAttemptCount, @FailedPasswordAnswerAttemptWindowStart, "
                    + " @Enabled, @AccessCode, @AccessLevel, "
                    + " @Sex, @CompanyName, @Vat, @Ssn, @FirstName, @SecondName, "
                    + " @Address1, @Address2, @City, @State, @ZipCode, "
                    + " @Nation, @Tel1, @Mobile1, @Website1, @AllowMessages, @AllowEmails, @ValidationCode) ";
                    myCmd.CommandText = Database.ParseSql(sSql);

                    if (string.IsNullOrEmpty(username)) username = "";
                    if (string.IsNullOrEmpty(pApplicationName)) pApplicationName = "";
                    if (string.IsNullOrEmpty(email)) email = "";
                    if (string.IsNullOrEmpty(password)) password = "";
                    if (string.IsNullOrEmpty(passwordQuestion)) passwordQuestion = "";
                    if (string.IsNullOrEmpty(passwordAnswer)) passwordAnswer = "";

                    //cmd.Parameters.Add("@PKID", OdbcType.UniqueIdentifier).Value = providerUserKey;
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Username", username));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "NickName", nickName));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ApplicationName", pApplicationName));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Email", email));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Comment", ""));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Password", encodePassword(password)));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "PasswordQuestion", passwordQuestion));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "PasswordAnswer", encodePassword(passwordAnswer)));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "IsApproved", isApproved));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "LastActivityDate", createDate));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "LastPasswordChangedDate", createDate));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CreationDate", createDate));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "IsLockedOut", false));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "LastLockedOutDate", createDate));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "FailedPasswordAttemptCount", 0));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "FailedPasswordAttemptWindowStart", createDate));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "FailedPasswordAnswerAttemptCount", 0));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "FailedPasswordAnswerAttemptWindowStart", createDate));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Enabled", newUser.Enabled));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "AccessCode", newUser.AccessCode));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "AccessLevel", newUser.AccessLevel));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Sex", newUser.Sex));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CompanyName", newUser.CompanyName));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Vat", newUser.Vat));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Ssn", newUser.Ssn));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "FirstName", newUser.FirstName));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "SecondName", newUser.SecondName));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Address1", newUser.Address1));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Address2", newUser.Address2));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "City", newUser.City));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "State", newUser.State));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ZipCode", newUser.ZipCode));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Nation", newUser.Nation));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Tel1", newUser.Tel1));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Mobile1", newUser.Mobile1));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Website1", newUser.Website1));

                    myCmd.Parameters.Add(Database.Parameter(myProv, "AllowMessages", newUser.AllowMessages));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "AllowEmails", newUser.AllowEmails));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ValidationCode", newUser.ValidationCode));


                    int recAdded = myCmd.ExecuteNonQuery();
                    if (recAdded > 0)
                    {
                        status = MembershipCreateStatus.Success;
                    }
                    else
                    {
                        status = MembershipCreateStatus.UserRejected;
                    }
                }
                catch (Exception e)
                {
                    if (WriteExceptionsToEventLog)
                    {
                        writeToEventLog(e, "CreateUser");
                    }

                    status = MembershipCreateStatus.ProviderError;
                }
                finally
                {
                    myConn.Dispose();
                }

                return (PgnUser)GetUser(username, false);
            }
            else
            {
                status = MembershipCreateStatus.DuplicateUserName;
            }

            return null;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
			if (isCoreUser(username))
			{
				throw new ProviderException("Cannot delete core users");
			}

            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myProv.CreateCommand();
            string sSql;
            int rowsAffected = 0;

            try
            {
                myConn.ConnectionString = connectionString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "DELETE FROM [" + tableName + "] "
                + " WHERE Username = @Username AND Applicationname = @Applicationname ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Username", username));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Applicationname", pApplicationName));
                rowsAffected = myCmd.ExecuteNonQuery();

                if (deleteAllRelatedData)
                {
                    // Process commands to delete all data for the user in the database.
                    var usermetaMan = new PgnUserMetaManager();
                    usermetaMan.DeleteByUsername(username);
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    writeToEventLog(e, "DeleteUser");
                    throw new ProviderException(exceptionMessage);
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

            if (rowsAffected > 0)
                return true;

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize">0 to retrieve all records</param>
        /// <param name="totalRecords">not filled</param>
        /// <returns></returns>
        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            MembershipUserCollection users = new MembershipUserCollection();
            totalRecords = 0;    //not filled

            try
            {
                myConn.ConnectionString = connectionString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = GetSqlSelect() +
                     " WHERE ApplicationName = @ApplicationName " +
                     " ORDER BY Username Asc";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "ApplicationName", pApplicationName));
                myRd = myCmd.ExecuteReader();

                int counter = 0;
                int startIndex = pageSize * pageIndex;
                int endIndex = startIndex + pageSize - 1;

                while (myRd.Read())
                {
                    if (counter >= startIndex)
                    {
                        MembershipUser u = GetUserFromReader(myRd);
                        users.Add(u);
                    }
                    if (endIndex > 0)
                    {
                        if (counter >= endIndex) 
                        { 
                            myCmd.Cancel(); 
                        }
                    }
                    counter++;
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    writeToEventLog(e, "GetAllUsers ");
                    throw new ProviderException(exceptionMessage);
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
            return users;
        }

        public override int GetNumberOfUsersOnline()
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;

            TimeSpan onlineSpan = new TimeSpan(0, System.Web.Security.Membership.UserIsOnlineTimeWindow, 0);
            DateTime compareTime = DateTime.Now.Subtract(onlineSpan);
            int numOnline = 0;

            try
            {
                myConn.ConnectionString = connectionString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT Count(*) FROM "+ tableName +" " +
                " WHERE LastActivityDate > @CompareDate AND ApplicationName = @ApplicationName ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "CompareDate", compareTime));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ApplicationName", pApplicationName));
                
                myRd = myCmd.ExecuteReader();
                if (myRd.Read())
                {
                    if (!Convert.IsDBNull(myRd[0]))
                        numOnline = (int)myRd[0];
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    writeToEventLog(e, "GetNumberOfUsersOnline");
                    throw new ProviderException(exceptionMessage);
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
            return numOnline;
        }

        public override string GetPassword(string username, string answer)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;

            if (!EnablePasswordRetrieval)
            {
                throw new ProviderException("Password Retrieval Not Enabled.");
            }

            if (PasswordFormat == MembershipPasswordFormat.Hashed)
            {
                throw new ProviderException("Cannot retrieve Hashed passwords.");
            }

            string password = "";
            string passwordAnswer = "";

            try
            {
                myConn.ConnectionString = connectionString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT Password, PasswordAnswer, IsLockedOut FROM "+ tableName +" " +
                  " WHERE Username = @Username AND ApplicationName = @ApplicationName ";

                myCmd.Parameters.Add(Database.Parameter(myProv, "Username", username));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ApplicationName", pApplicationName));
                myCmd.CommandText = Database.ParseSql(sSql);
                myRd = myCmd.ExecuteReader();

                if (myRd.HasRows)
                {
                    myRd.Read();

                    if (!Convert.IsDBNull(myRd["IsLockedOut"]))
                    {
                        //20111129
                        //if ((bool)myRd["IsLockedOut"] == true)
                        //    throw new MembershipPasswordException("The supplied user is locked out.");
                    }
                    if (!Convert.IsDBNull(myRd["Password"]))
                        password = (string)myRd["Password"];
                    if (!Convert.IsDBNull(myRd["PasswordAnswer"]))
                        passwordAnswer = (string)myRd["PasswordAnswer"];
                }
                else
                {
                    throw new MembershipPasswordException("The supplied user name is not found.");
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    writeToEventLog(e, "GetPassword");
                    throw new ProviderException(exceptionMessage);
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

            if (RequiresQuestionAndAnswer && !checkPassword(answer, passwordAnswer))
            {
                updateFailureCount(username, "passwordAnswer");
                throw new MembershipPasswordException("Incorrect password answer.");
            }

            if (PasswordFormat == MembershipPasswordFormat.Encrypted)
            {
                password = unEncodePassword(password);
            }
            return password;
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            PgnUser u = null;

            try
            {
                myConn.ConnectionString = connectionString;
                myConn.Open();
                myCmd.Connection = myConn;

                //PKID
                sSql = GetSqlSelect() + " WHERE Username = @Username AND ApplicationName = @ApplicationName ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Username", username));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ApplicationName", pApplicationName));

                myRd = myCmd.ExecuteReader();

                if (myRd.HasRows)
                {
                    myRd.Read();
                    u = GetUserFromReader(myRd);
                    myRd.Close();
                    SetLastActivityDate(myConn, myProv, username, userIsOnline);
                }

            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    writeToEventLog(e, "GetUser(String, Boolean)");
                    throw new ProviderException(exceptionMessage);
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
            return u;
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            PgnUser u = null;

            try
            {
                myConn.ConnectionString = connectionString;
                myConn.Open();
                myCmd.Connection = myConn;

                //PKID = @PKID
                sSql = GetSqlSelect() + " WHERE [Id] = @Id ";
                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", (int)providerUserKey));
                //cmd.Parameters.Add("@PKID", OdbcType.UniqueIdentifier).Value = providerUserKey;
                myCmd.CommandText = Database.ParseSql(sSql);
                
                myRd = myCmd.ExecuteReader();

                if (myRd.HasRows)
                {
                    myRd.Read();
                    u = GetUserFromReader(myRd);

                    if (userIsOnline)
                    {
                        DbCommand updateCmd = myConn.CreateCommand();
                        updateCmd.Connection = myConn;

                        //PKID
                        sSql = "UPDATE "+ tableName +" " +
                          "SET LastActivityDate = @LastActivityDate " +
                          "WHERE [Id] = @Id ";
                        updateCmd.CommandText = Database.ParseSql(sSql);
                        updateCmd.Parameters.Add(Database.Parameter(myProv, "LastActivityDate", DateTime.Now));
                        updateCmd.Parameters.Add(Database.Parameter(myProv, "Id", (int)providerUserKey));

                        updateCmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    writeToEventLog(e, "GetUser(Object, Boolean)");
                    throw new ProviderException(exceptionMessage);
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
            return u;
        }

        public override bool UnlockUser(string username)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            int rowsAffected = 0;

            try
            {
                myConn.ConnectionString = connectionString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "UPDATE "+ tableName +" " +
                    " SET IsLockedOut = False, LastLockedOutDate = @LastLockedOutDate " +
                    " WHERE Username = @Username AND ApplicationName = @ApplicationName";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "LastLockedOutDate", DateTime.Now));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Username", username));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ApplicationName", pApplicationName));

                rowsAffected = myCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    writeToEventLog(e, "UnlockUser");
                    throw new ProviderException(exceptionMessage);
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

            if (rowsAffected > 0)
                return true;

            return false;
        }

        public override string GetUserNameByEmail(string email)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;

            string username = "";

            try
            {
                myConn.ConnectionString = connectionString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT Username "
                + " FROM "+ tableName +" WHERE Email = @Email AND ApplicationName = @ApplicationName";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Email", email));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ApplicationName", pApplicationName));
                myRd = myCmd.ExecuteReader();
                if (myRd.Read())
                {
                    if (!Convert.IsDBNull(myRd["Username"]))
                        username = (string)myRd["Username"];
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    writeToEventLog(e, "GetUserNameByEmail");
                    throw new ProviderException(exceptionMessage);
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
            return username;
        }

        //Override OnValidatingPassword
        protected override void OnValidatingPassword(ValidatePasswordEventArgs args)
        {
            //Any logic to process password validation conditions
            //e.g:
            if (args.Password.Length < MinRequiredPasswordLength)
                args.FailureInformation = new ArgumentException(String.Format("Password is too short, min password length {0}", MinRequiredPasswordLength.ToString()));

            if (args.UserName == args.Password)
                args.FailureInformation = new ArgumentException(String.Format("Password should not be equal to username"));

            //Also here could be any logic to throw an exception if needed
            //e.g:
            if (args.FailureInformation != null)
                throw args.FailureInformation;

            //Calling base
            base.OnValidatingPassword(args);

            if (args.Cancel)
            {
                if (args.FailureInformation == null)
                    args.FailureInformation = new ArgumentException(String.Format("Custom Password Validation Failure for password '{0}'", args.Password));

                throw args.FailureInformation;
            }
        }

        public override string ResetPassword(string username, string answer)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;

            if (!EnablePasswordReset)
            {
                throw new NotSupportedException("Password reset is not enabled.");
            }

            if (answer == null && RequiresQuestionAndAnswer)
            {
                updateFailureCount(username, "passwordAnswer");

                throw new ProviderException("Password answer required for password reset.");
            }

            string newPassword =
              System.Web.Security.Membership.GeneratePassword(newPasswordLength, MinRequiredNonAlphanumericCharacters);

            ValidatePasswordEventArgs args =
              new ValidatePasswordEventArgs(username, newPassword, true);

            OnValidatingPassword(args);

            if (args.Cancel)
                if (args.FailureInformation != null)
                    throw args.FailureInformation;
                else
                    throw new MembershipPasswordException("Reset password canceled due to password validation failure.");

            int rowsAffected = 0;
            string passwordAnswer = "";

            try
            {
                myConn.ConnectionString = connectionString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT PasswordAnswer, IsLockedOut FROM "+ tableName +" "
                + " WHERE Username = @Username AND ApplicationName = @ApplicationName ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Username", username));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ApplicationName", pApplicationName));
                myRd = myCmd.ExecuteReader();

                if (myRd.HasRows)
                {
                    myRd.Read();

                    if (!Convert.IsDBNull(myRd["IsLockedOut"]))
                        if ((bool)myRd["IsLockedOut"])
                            throw new MembershipPasswordException("The supplied user is locked out.");

                    if (!Convert.IsDBNull(myRd["PasswordAnswer"]))
                        passwordAnswer = (string)myRd["PasswordAnswer"];
                }
                else
                {
                    throw new MembershipPasswordException("The supplied user name is not found.");
                }

                if (RequiresQuestionAndAnswer && !checkPassword(answer, passwordAnswer))
                {
                    updateFailureCount(username, "passwordAnswer");
                    throw new MembershipPasswordException("Incorrect password answer.");
                }

                DbCommand updateCmd = myConn.CreateCommand();
                updateCmd.Connection = myConn;
                sSql = "UPDATE "+ tableName +" "
                + " SET Password = @Password, LastPasswordChangedDate = @LastPasswordChangedDate "
                + " WHERE Username = @Username AND ApplicationName = @ApplicationName  AND IsLockedOut = 0 ";
                updateCmd.CommandText = Database.ParseSql(sSql);
                updateCmd.Parameters.Add(Database.Parameter(myProv, "Password", encodePassword(newPassword)));
                updateCmd.Parameters.Add(Database.Parameter(myProv, "LastPasswordChangedDate", DateTime.Now));
                updateCmd.Parameters.Add(Database.Parameter(myProv, "Username", username));
                updateCmd.Parameters.Add(Database.Parameter(myProv, "ApplicationName", pApplicationName));

                rowsAffected = updateCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    writeToEventLog(e, "ResetPassword");
                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (myRd != null) { myRd.Close(); }
                myConn.Close();
            }

            if (rowsAffected > 0)
            {
                return newPassword;
            }
            else
            {
                throw new MembershipPasswordException("User not found, or user is locked out. Password not Reset.");
            }
        }

        public override void UpdateUser(MembershipUser user)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;

            PgnUser u = (PgnUser)user;

            try
            {
                myConn.ConnectionString = connectionString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "UPDATE "+ tableName +" "
                + " SET NickName = @NickName, Email = @Email, Comment = @Comment, IsApproved = @IsApproved, "
                + " Enabled = @Enabled, AccessCode = @AccessCode, AccessLevel = @AccessLevel, "
                + " Sex=@Sex, CompanyName=@CompanyName, Vat=@Vat, "
                + " Ssn=@Ssn, FirstName=@FirstName, SecondName=@SecondName, "
                + " Address1=@Address1, Address2=@Address2, City=@City, "
                + " [State]=@State, ZipCode=@ZipCode, "
                + " Nation=@Nation, Tel1=@Tel1, Mobile1=@Mobile1, Website1=@Website1, "
                + " AllowMessages=@AllowMessages, AllowEmails=@AllowEmails, ValidationCode=@ValidationCode "
                + " WHERE Username = @Username AND ApplicationName = @ApplicationName ";
                myCmd.CommandText = Database.ParseSql(sSql);

                myCmd.Parameters.Add(Database.Parameter(myProv, "NickName", u.NickName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Email", u.Email));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Comment", u.Comment));
                myCmd.Parameters.Add(Database.Parameter(myProv, "IsApproved", u.IsApproved));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Username", u.UserName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ApplicationName", pApplicationName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Enabled", u.Enabled));
                myCmd.Parameters.Add(Database.Parameter(myProv, "AccessCode", u.AccessCode));
                myCmd.Parameters.Add(Database.Parameter(myProv, "AccessLevel", u.AccessLevel));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Sex", u.Sex));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CompanyName", u.CompanyName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Vat", u.Vat));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Ssn", u.Ssn));
                myCmd.Parameters.Add(Database.Parameter(myProv, "FirstName", u.FirstName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "SecondName", u.SecondName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Address1", u.Address1));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Address2", u.Address2));
                myCmd.Parameters.Add(Database.Parameter(myProv, "City", u.City));
                myCmd.Parameters.Add(Database.Parameter(myProv, "State", u.State));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ZipCode", u.ZipCode));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Nation", u.Nation));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Tel1", u.Tel1));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Mobile1", u.Mobile1));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Website1", u.Website1));
                myCmd.Parameters.Add(Database.Parameter(myProv, "AllowMessages", u.AllowMessages));
                myCmd.Parameters.Add(Database.Parameter(myProv, "AllowEmails", u.AllowEmails));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ValidationCode", u.ValidationCode));

                myCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    writeToEventLog(e, "UpdateUser");
                    throw new ProviderException(exceptionMessage);
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

        public override bool ValidateUser(string username, string password)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;

            bool isValid = false;
            bool isApproved = false;
            string pwd = "";

            try
            {
                myConn.ConnectionString = connectionString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT Password, IsApproved FROM "+ tableName +" "
                + " WHERE Username = @Username AND ApplicationName = @ApplicationName AND IsLockedOut = 0";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Username", username));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ApplicationName", pApplicationName));

                myRd = myCmd.ExecuteReader();

                if (myRd.HasRows)
                {
                    myRd.Read();
                    if (!Convert.IsDBNull(myRd["Password"]))
                        pwd = (string)myRd["Password"];
                    if (!Convert.IsDBNull(myRd["IsApproved"]))
                        isApproved = (bool)myRd["IsApproved"];
                }
                else
                {
                    return false;
                }
                myRd.Close();

                if (checkPassword(password, pwd))
                {
                    if (isApproved)
                    {
                        isValid = true;

                        DbCommand updateCmd = myConn.CreateCommand();
                        updateCmd.Connection = myConn;
                        sSql = "UPDATE " + tableName + " SET LastLoginDate = @LastLoginDate, "
                        + " IsLockedOut = @IsLockedOut, "
                        + " FailedPasswordAttemptCount = @FailedPasswordAttemptCount, "
                        + " FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount "
                        + " WHERE Username = @Username AND ApplicationName = @ApplicationName ";
                        updateCmd.CommandText = Database.ParseSql(sSql);

                        updateCmd.Parameters.Add(Database.Parameter(myProv, "LastLoginDate", DateTime.Now));
                        updateCmd.Parameters.Add(Database.Parameter(myProv, "Username", username));
                        updateCmd.Parameters.Add(Database.Parameter(myProv, "ApplicationName", pApplicationName));
                        updateCmd.Parameters.Add(Database.Parameter(myProv, "IsLockedOut", 0)); //added 20091023
                        updateCmd.Parameters.Add(Database.Parameter(myProv, "FailedPasswordAttemptCount", 0)); //added 20091023
                        updateCmd.Parameters.Add(Database.Parameter(myProv, "FailedPasswordAnswerAttemptCount", 0)); //added 20091023

                        updateCmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    myConn.Dispose();
                    updateFailureCount(username, "password");
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    writeToEventLog(e, "ValidateUser");
                    throw new ProviderException(exceptionMessage);
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
            return isValid;
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            MembershipUserCollection users = new MembershipUserCollection();
            totalRecords = 0;   //not filled

            try
            {
                myConn.ConnectionString = connectionString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = GetSqlSelect()
                + " WHERE Username LIKE @UsernameSearch AND ApplicationName = @ApplicationName "
                + " ORDER BY Username Asc";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "UsernameSearch", "%"+ usernameToMatch +"%"));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ApplicationName", pApplicationName));
                myRd = myCmd.ExecuteReader();

                int counter = 0;
                int startIndex = pageSize * pageIndex;
                int endIndex = startIndex + pageSize - 1;

                while (myRd.Read())
                {
                    if (counter >= startIndex)
                    {
                        MembershipUser u = GetUserFromReader(myRd);
                        users.Add(u);
                    }
                    if (endIndex > 0)
                    {
                        if (counter >= endIndex) { myCmd.Cancel(); }
                    }
                    counter++;
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    writeToEventLog(e, "FindUsersByName");
                    throw new ProviderException(exceptionMessage);
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
            return users;
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            
            MembershipUserCollection users = new MembershipUserCollection();
            totalRecords = 0;   //not filled
            
            try
            {
                int counter = 0;
                int startIndex = pageSize * pageIndex;
                int endIndex = startIndex + pageSize - 1;

                sSql = GetSqlSelect()
                + " WHERE Email LIKE @EmailSearch AND ApplicationName = @ApplicationName "
                + " ORDER BY Username Asc";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "EmailSearch", "%" + emailToMatch + "%"));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ApplicationName", pApplicationName));

                myRd = myCmd.ExecuteReader();

                while (myRd.Read())
                {
                    if (counter >= startIndex)
                    {
                        MembershipUser u = GetUserFromReader(myRd);
                        users.Add(u);
                    }

                    if (endIndex > 0)
                    {
                        if (counter >= endIndex) { myCmd.Cancel(); }
                    }
                    counter++;
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    writeToEventLog(e, "FindUsersByEmail");
                    throw new ProviderException(exceptionMessage);
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
            return users;
        }

        #endregion
    }
}