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
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using PigeonCms;
using System.Web.Routing;


namespace PigeonCms
{
    public class MemberEditorControl: PigeonCms.BaseModuleControl
    {

        #region public fields

        private bool loginAfterCreate = false;
        public bool LoginAfterCreate
        {
            get { return GetBoolParam("LoginAfterCreate", loginAfterCreate); }
            set { loginAfterCreate = value; }
        }

        private bool needApprovation = false;
        public bool NeedApprovation
        {
            get { return GetBoolParam("NeedApprovation", needApprovation); }
            set { needApprovation = value; }
        }

        private bool newRoleAsUser = false;
        public bool NewRoleAsUser
        {
            get { return GetBoolParam("NewRoleAsUser", newRoleAsUser); }
            set { newRoleAsUser = value; }
        }

        private string redirectUrl = "";
        public string RedirectUrl
        {
            get { return GetStringParam("RedirectUrl", redirectUrl); }
            set { redirectUrl = value; }
        }

        private string newUserSuffix = "";
        public string NewUserSuffix
        {
            get { return GetStringParam("NewUserSuffix", newUserSuffix); }
            set { newUserSuffix = value; }
        }

        private string defaultRoles = "";
        public string DefaultRoles
        {
            get 
            { 
                return GetStringParam("DefaultRoles", "") + defaultRoles; 
            }
            set { defaultRoles = value; }
        }

        private string defaultAccessCode = "";
        public string DefaultAccessCode
        {
            get { return GetStringParam("DefaultAccessCode", defaultAccessCode); }
            set { defaultAccessCode = value; }
        }

        private int defaultAccessLevel = 0;
        public int DefaultAccessLevel
        {
            get { return GetIntParam("DefaultAccessLevel", defaultAccessLevel); }
            set { defaultAccessLevel = value; }
        }

        private bool sendEmailNotificationToUser = false;
        public bool SendEmailNotificationToUser
        {
            get { return GetBoolParam("SendEmailNotificationToUser", sendEmailNotificationToUser); }
            set { sendEmailNotificationToUser = value; }
        }

        private bool sendEmailNotificationToAdmin = false;
        public bool SendEmailNotificationToAdmin
        {
            get { return GetBoolParam("SendEmailNotificationToAdmin", sendEmailNotificationToAdmin); }
            set { sendEmailNotificationToAdmin = value; }
        }

        private string adminNotificationEmail = "";
        public string AdminNotificationEmail
        {
            get { return GetStringParam("AdminNotificationEmail", adminNotificationEmail); }
            set { adminNotificationEmail = value; }
        }

        private string notificationEmailPageName = "";
        public string NotificationEmailPageName
        {
            get { return GetStringParam("NotificationEmailPageName", notificationEmailPageName); }
            set { notificationEmailPageName = value; }
        }

        #endregion


        #region fields to show

        private bool showFieldSex = false;
        public bool ShowFieldSex
        {
            get { return GetBoolParam("ShowFieldSex", showFieldSex); }
            set { showFieldSex = value; }
        }

        private bool showFieldCompanyName = false;
        public bool ShowFieldCompanyName
        {
            get { return GetBoolParam("ShowFieldCompanyName", showFieldCompanyName); }
            set { showFieldCompanyName = value; }
        }

        private bool showFieldVat = false;
        public bool ShowFieldVat
        {
            get { return GetBoolParam("ShowFieldVat", showFieldVat); }
            set { showFieldVat = value; }
        }

        private bool showFieldSsn = false;
        public bool ShowFieldSsn
        {
            get { return GetBoolParam("ShowFieldSsn", showFieldSsn); }
            set { showFieldSsn = value; }
        }

        private bool showFieldFirstName = false;
        public bool ShowFieldFirstName
        {
            get { return GetBoolParam("ShowFieldFirstName", showFieldFirstName); }
            set { showFieldFirstName = value; }
        }

        private bool showFieldSecondName = false;
        public bool ShowFieldSecondName
        {
            get { return GetBoolParam("ShowFieldSecondName", showFieldSecondName); }
            set { showFieldSecondName = value; }
        }

        private bool showFieldAddress1 = false;
        public bool ShowFieldAddress1
        {
            get { return GetBoolParam("ShowFieldAddress1", showFieldAddress1); }
            set { showFieldAddress1 = value; }
        }

        private bool showFieldAddress2 = false;
        public bool ShowFieldAddress2
        {
            get { return GetBoolParam("ShowFieldAddress2", showFieldAddress2); }
            set { showFieldAddress2 = value; }
        }

        private bool showFieldCity = false;
        public bool ShowFieldCity
        {
            get { return GetBoolParam("ShowFieldCity", showFieldCity); }
            set { showFieldCity = value; }
        }

        private bool showFieldState = false;
        public bool ShowFieldState
        {
            get { return GetBoolParam("ShowFieldState", showFieldState); }
            set { showFieldState = value; }
        }

        private bool showFieldZipCode = false;
        public bool ShowFieldZipCode
        {
            get { return GetBoolParam("ShowFieldZipCode", showFieldZipCode); }
            set { showFieldZipCode = value; }
        }

        private bool showFieldNation = false;
        public bool ShowFieldNation
        {
            get { return GetBoolParam("ShowFieldNation", showFieldNation); }
            set { showFieldNation = value; }
        }

        private bool showFieldTel1 = false;
        public bool ShowFieldTel1
        {
            get { return GetBoolParam("ShowFieldTel1", showFieldTel1); }
            set { showFieldTel1 = value; }
        }

        private bool showFieldMobile1 = false;
        public bool ShowFieldMobile1
        {
            get { return GetBoolParam("ShowFieldMobile1", showFieldMobile1); }
            set { showFieldMobile1 = value; }
        }

        private bool showFieldWebsite1 = false;
        public bool ShowFieldWebsite1
        {
            get { return GetBoolParam("ShowFieldWebsite1", showFieldWebsite1); }
            set { showFieldWebsite1 = value; }
        }

        #endregion

        #region fields enabled or not

        private bool enabledFieldNation = true;
        public bool EnabledFieldNation
        {
            get { return GetBoolParam("EnabledFieldNation", enabledFieldNation); }
            set { enabledFieldNation = value; }
        }

        #endregion
    }
}