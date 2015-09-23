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
using System.Collections.Generic;
using System.Threading;


namespace PigeonCms
{
    public class AppSetting: ITable
    {
        private string keySet = "";
        private string keyName = "";
        private string keyTitle = "";
        private string keyValue = "";
        private string keyInfo = "";

        public AppSetting()
        {
        }

        /// <summary>
        /// setting resource set PKey
        /// </summary>
        [DataObjectField(true)]
        public string KeySet
        {
            [DebuggerStepThrough()]
            get { return keySet; }
            [DebuggerStepThrough()]
            set { keySet = value; }
        }

        /// <summary>
        /// setting key PKey
        /// </summary>
        [DataObjectField(true)]
        public string KeyName
        {
            [DebuggerStepThrough()]
            get { return keyName; }
            [DebuggerStepThrough()]
            set { keyName = value; }
        }

        [DataObjectField(false)]
        public string KeyTitle
        {
            [DebuggerStepThrough()]
            get { return keyTitle; }
            [DebuggerStepThrough()]
            set { keyTitle = value; }
        }

        [DataObjectField(false)]
        public string KeyValue
        {
            [DebuggerStepThrough()]
            get { return keyValue; }
            [DebuggerStepThrough()]
            set { keyValue = value; }
        }

        /// <summary>
        /// info about using the key
        /// </summary>
        [DataObjectField(false)]
        public string KeyInfo
        {
            [DebuggerStepThrough()]
            get { return keyInfo; }
            [DebuggerStepThrough()]
            set { keyInfo = value; }
        }
    }

    [Serializable]
    public class AppSettingsFilter
    {
        private string keySet = "";
        private string keyName = "";

        public string KeySet
        {
            [DebuggerStepThrough()]
            get { return keySet; }
            [DebuggerStepThrough()]
            set { keySet = value; }
        }

        public string KeyName
        {
            [DebuggerStepThrough()]
            get { return keyName; }
            [DebuggerStepThrough()]
            set { keyName = value; }
        }

    }
}