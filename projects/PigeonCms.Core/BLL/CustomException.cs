using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace PigeonCms
{
    /// <summary>
    /// Severity level of Exception
    /// </summary>
    public enum CustomExceptionSeverity
    {
        Info, 
        Warning,
        Critical,
        Fatal
    }

    public enum CustomExceptionLogLevel
    {
        Debug,
        Log
    }
    
    public class CustomException: Exception
    {
        private CustomExceptionSeverity severity = CustomExceptionSeverity.Warning;
        private CustomExceptionLogLevel logLevel = CustomExceptionLogLevel.Debug;
        private string customMessage = "";

        public CustomExceptionSeverity Severity
        {
            get { return severity; }
        }

        public CustomExceptionLogLevel LogLevel
        {
            get { return logLevel; }
        }

        public string CustomMessage
        {
            get { return customMessage; }
        }

        public override string Message
        {
            get { return this.CustomMessage; }
        }

        public CustomException()
        {
        }

        public CustomException(string customMessage)
        {
            this.customMessage = customMessage;
        }

        public CustomException(string customMessage, CustomExceptionSeverity severity, CustomExceptionLogLevel logLevel)
        {
            this.customMessage = customMessage;
            this.severity = severity;
            this.logLevel = logLevel;
            PigeonCms.Tracer.Log("CustomException: "+ this.ToString(), TracerItemType.Error, this);
            //PigeonCms.Debug.Write("CustomException", this);
        }

        public override string ToString()
        {
            //return base.ToString();
            return "Message:" + this.CustomMessage + "; "+ 
                "Severity:" + this.Severity.ToString()+ "; " +
                "LogLevel:" + this.LogLevel.ToString() + "; ";
        }

    }

}