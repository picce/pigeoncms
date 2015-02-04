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
using System.Web.Caching;
using PigeonCms.Core.Helpers;
using System.Net.Mail;
using System.Net;

namespace PigeonCms
{
    /// <summary>
    /// static class to manage messages between users
    /// </summary>
    public static class MessageProvider
    {

        public enum SendMessageEnum
        {
            Never, 
            UserSetting,
            Always
        }

        public const string USER_NOT_LOGGED_EXCEPTION = "not logged user";
        public const string USER_NOT_VALID_EXCEPTION = "invalid user";
        const string SYSTEM_USER = "SYSTEM";

        #region public methods

        public static void SendMessage(string toUsers, Message message, 
            SendMessageEnum sendLocalMessage, SendMessageEnum sendEmail)
        {

            SendMessage(Utility.String2List(toUsers, ";"), message, sendLocalMessage, sendEmail);
        }


        /// <summary>
        /// send internal message to toUsers list
        /// you need to be logged
        /// </summary>
        /// <param name="toUsers"></param>
        /// <param name="message"></param>
        public static void SendMessage(List<string> toUsers, Message message, 
            SendMessageEnum sendLocalMessage, SendMessageEnum sendEmail)
        {
            if (!PgnUserCurrent.IsAuthenticated)
                throw new CustomException(USER_NOT_LOGGED_EXCEPTION);

            sendMessage(PgnUserCurrent.UserName, toUsers, message, sendLocalMessage, sendEmail);
        }

        /// <summary>
        /// send system message
        /// no need to be logged
        /// </summary>
        /// <param name="toUsers"></param>
        /// <param name="message"></param>
        public static void SendSystemMessage(List<string> toUsers, Message message,
            SendMessageEnum sendLocalMessage, SendMessageEnum sendEmail)
        {
            sendMessage(SYSTEM_USER, toUsers, message, sendLocalMessage, sendEmail);
        }

        public static void SendSystemMessage(string toUsers, Message message,
            SendMessageEnum sendLocalMessage, SendMessageEnum sendEmail)
        {
            SendSystemMessage(Utility.String2List(toUsers), message, sendLocalMessage, sendEmail);
        }

        #endregion

        private static void sendMessage(string sender, List<string> toUsers, Message message,
            SendMessageEnum sendLocalMessage, SendMessageEnum sendEmail)
        {
            if (toUsers == null)
                throw new CustomException(USER_NOT_VALID_EXCEPTION);

            toUsers = Utility.RemoveDuplicatesFromList(toUsers);
            if (toUsers.Count == 0)
                throw new CustomException(USER_NOT_VALID_EXCEPTION);

            //sent folder
            var man = new MessagesManager();
            message.OwnerUser = sender;
            message.FromUser = sender;
            message.ToUser = Utility.List2String(toUsers, ";");
            message.IsRead = true;
            man.Insert(message);

            foreach (string user in toUsers)
            {
                bool bSendLocal = true;
                bool bSendEmail = true;

                if (sendLocalMessage == SendMessageEnum.Never)
                    bSendLocal = false;
                if (sendEmail == SendMessageEnum.Never)
                    bSendEmail = false;

                var member = Membership.GetUser(user.Trim());
                if (member == null)
                {
                    //check if user exists
                    bSendLocal = false;
                    bSendEmail = false;
                    var errMsg = new Message();
                    errMsg.OwnerUser = sender;
                    errMsg.FromUser = SYSTEM_USER;
                    errMsg.ToUser = sender;
                    errMsg.IsRead = false;
                    errMsg.Title = USER_NOT_VALID_EXCEPTION + " " + user.Trim();
                    errMsg.Description = message.GetOriginalMessageHeader();
                    man.Insert(errMsg);
                    //throw new CustomException(USER_NOT_VALID_EXCEPTION);
                }

                if (bSendLocal && sendLocalMessage == SendMessageEnum.UserSetting)
                    bSendLocal = ((PgnUser)member).AllowMessages;
                if (bSendEmail && sendEmail == SendMessageEnum.UserSetting)
                    bSendEmail = ((PgnUser)member).AllowEmails;

                if (bSendLocal && sender != user.Trim())
                {
                    //inbox of to users
                    message.OwnerUser = user.Trim();
                    message.FromUser = sender;
                    message.ToUser = user.Trim();
                    message.IsRead = false;
                    man.Insert(message);
                }

                if (bSendEmail && Utility.IsValidEmail(member.Email))
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
                            //smtp.Host = "62.149.128.218";
                            //smtp.UseDefaultCredentials = false;
                            smtp.Credentials = new NetworkCredential(
                                AppSettingsManager.GetValue("SmtpUser"),
                                AppSettingsManager.GetValue("SmtpPassword"));
                        }

                        MailMessage mail1 = new MailMessage();
                        mail1.From = new MailAddress(AppSettingsManager.GetValue("EmailSender"));
                        mail1.To.Add(member.Email);
                        mail1.Subject = message.Title;
                        mail1.IsBodyHtml = true;
                        mail1.Body = message.GetOriginalMessageHeader() + message.Description;

                        smtp.Send(mail1); 
                    }
                }

            }
        }
    }
}