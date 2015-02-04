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
using PigeonCms;
using System.Net.Mail;
using System.Net;


namespace PigeonCms
{
    public class EmailContactFormControl : PigeonCms.BaseModuleControl
    {
        #region private fields
        private string emailAddressTo = "";
        private string emailAddressBcc = "";
        private string emailSubject = "";
        private string headerText = "";
        private string footerText = "";
        private string privacyText = "";
        private bool showPrivacyCheck = false;
        private bool showCaptcha = true;
        #endregion


        #region protected fields
        protected string LblErroreInfo = "";
        protected string LblSuccessInfo = "";
        #endregion


        #region public fields

        public string EmailAddressTo
        {
            get { return base.GetStringParam("EmailAddressTo", emailAddressTo); }
            set { emailAddressTo = value; }
        }

        public string EmailAddressBcc
        {
            get { return base.GetStringParam("EmailAddressBcc", emailAddressBcc); }
            set { emailAddressBcc = value; }
        }

        public string EmailSubject
        {
            get { return base.GetStringParam("EmailSubject", emailSubject); }
            set { emailSubject = value; }
        }

        public string HeaderText
        {
            get { return base.GetStringParam("HeaderText", headerText); }
            set { headerText = value; }
        }

        public string FooterText
        {
            get { return base.GetStringParam("FooterText", footerText); }
            set { footerText = value; }
        }

        public string PrivacyText
        {
            get { return base.GetStringParam("PrivacyText", privacyText); }
            set { privacyText = value; }
        }

        public bool ShowPrivacyCheck
        {
            get { return base.GetBoolParam("ShowPrivacyCheck", showPrivacyCheck); }
            set { showPrivacyCheck = value; }
        }

        public bool ShowCaptcha
        {
            get { return base.GetBoolParam("ShowCaptcha", showCaptcha); }
            set { showCaptcha = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void SendEmail(string body)
        {
            try
            {
                var smtp = new SmtpClient(AppSettingsManager.GetValue("SmtpServer"));
                using (smtp as IDisposable)
                {
                    smtp.EnableSsl = false;
                    if (!string.IsNullOrEmpty(AppSettingsManager.GetValue("SmtpUseSSL")))
                    {
                        bool useSsl = false;
                        bool.TryParse(AppSettingsManager.GetValue("SmtpUseSSL"), out useSsl);
                        smtp.EnableSsl = useSsl;
                    }
                    if (!string.IsNullOrEmpty(AppSettingsManager.GetValue("SmtpPort")))
                    {
                        int port = 25;
                        int.TryParse(AppSettingsManager.GetValue("SmtpPort"), out port);
                        smtp.Port = port;
                    }
                    if (!string.IsNullOrEmpty(AppSettingsManager.GetValue("SmtpUser")))
                    {
                        //smtp.Host = "88.86.167.150";
                        //smtp.UseDefaultCredentials = true;
                        smtp.Credentials = new NetworkCredential(
                            AppSettingsManager.GetValue("SmtpUser"),
                            AppSettingsManager.GetValue("SmtpPassword"));
                    }

                    MailMessage mail1 = new MailMessage();
                    mail1.From = new MailAddress(AppSettingsManager.GetValue("EmailSender"));
                    mail1.To.Add(this.EmailAddressTo);
                    if (!string.IsNullOrEmpty(this.EmailAddressBcc))
                        mail1.Bcc.Add(this.EmailAddressBcc);
                    mail1.Subject = this.EmailSubject;
                    mail1.IsBodyHtml = true;
                    mail1.Body = body;

                    smtp.Send(mail1);
                }
                LblSuccessInfo = base.GetLabel("LblGenericSuccess", "operation completed");
            }
            catch (Exception e1)
            {
                Tracer.Log("sendEmailInfo:" + e1.ToString(), TracerItemType.Error);
                LblErroreInfo = base.GetLabel("LblGenericError", "an error occured");
                throw e1;
            }
        }
    }
}