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
using PigeonCms;
using System.Collections.Generic;
using System.Threading;


namespace PigeonCms
{
    [DebuggerDisplay("Id={id}, Description={description}")]
    public class LogItem: ITable
    {
        private int id = 0;
        private DateTime dateInserted;
        private string userInserted = "";
        private int moduleId = 0;
        TracerItemType type = TracerItemType.Info;
        private string userHostAddress = "";
        private string url = "";
        private string description = "";
        private string sessionId = "";
        

        /// <summary>
        /// IDENTITY Id as PKey
        /// </summary>
        [DataObjectField(true)]
        public int Id
        {
            [DebuggerStepThrough()]
            get { return id; }
            [DebuggerStepThrough()]
            set { id = value; }
        }

        public DateTime DateInserted
        {
            [DebuggerStepThrough()]
            get { return dateInserted; }
            [DebuggerStepThrough()]
            set { dateInserted = value; }
        }

        /// <summary>
        /// record inserted user
        /// </summary>
        public string UserInserted
        {
            [DebuggerStepThrough()]
            get { return userInserted; }
            [DebuggerStepThrough()]
            set { userInserted = value; }
        }

        public int ModuleId
        {
            [DebuggerStepThrough()]
            get { return moduleId; }
            [DebuggerStepThrough()]
            set { moduleId = value; }
        }

        public TracerItemType Type
        {
            [DebuggerStepThrough()]
            get { return type; }
            [DebuggerStepThrough()]
            set { type = value; }
        }

        public string UserHostAddress
        {
            [DebuggerStepThrough()]
            get { return userHostAddress; }
            [DebuggerStepThrough()]
            set { userHostAddress = value; }
        }

        public string Url
        {
            [DebuggerStepThrough()]
            get { return url; }
            [DebuggerStepThrough()]
            set { url = value; }
        }

        public string Description
        {
            [DebuggerStepThrough()]
            get { return description; }
            [DebuggerStepThrough()]
            set { description = value; }
        }

        public string SessionId
        {
            [DebuggerStepThrough()]
            get { return sessionId; }
            [DebuggerStepThrough()]
            set { sessionId = value; }
        }

        string moduleFullName = "";
        /// <summary>
        /// full name of associated modules (readonly)
        /// </summary>
        public string ModuleFullName
        {
            [DebuggerStepThrough()]
            get { return moduleFullName; }
            [DebuggerStepThrough()]
            set { moduleFullName = value; }
        }

        public LogItem() { }
    }


    [Serializable]
    public class LogItemsFilter
    {
        private int id = 0;
        private int topRows = 100;
        private string userInserted = "";
        private int moduleId = 0;
        private bool filterType = false;
        private TracerItemType type = TracerItemType.Info;
        private DatesRange dateInsertedRange = new DatesRange(DatesRange.RangeType.Today);
        private string urlPart = "";
        private string descriptionPart = "";
        private string moduleNamespace = "";
        private string moduleFullName = "";
        private string userHostAddressPart = "";
        private string sessionIdPart = "";


        /// <summary>
        /// Automatic Id as PKey
        /// </summary>
        [DataObjectField(true)]
        public int Id
        {
            [DebuggerStepThrough()]
            get { return id; }
            [DebuggerStepThrough()]
            set { id = value; }
        }

        /// <summary>
        /// Show top n rows. All if set to 0,
        /// </summary>
        public int TopRows
        {
            [DebuggerStepThrough()]
            get { return topRows; }
            [DebuggerStepThrough()]
            set { topRows = value; }
        }

        public string UserInserted
        {
            [DebuggerStepThrough()]
            get { return userInserted; }
            [DebuggerStepThrough()]
            set { userInserted = value; }
        }

        public int ModuleId
        {
            [DebuggerStepThrough()]
            get { return moduleId; }
            [DebuggerStepThrough()]
            set { moduleId = value; }
        }

        public bool FilterType
        {
            [DebuggerStepThrough()]
            get { return filterType; }
            [DebuggerStepThrough()]
            set { filterType = value; }
        }

        public TracerItemType Type
        {
            [DebuggerStepThrough()]
            get { return type; }
            [DebuggerStepThrough()]
            set { type = value; }
        }

        public DatesRange DateInsertedRange
        {
            [DebuggerStepThrough()]
            get { return dateInsertedRange; }
            [DebuggerStepThrough()]
            set { dateInsertedRange = value; }
        }

        public string UrlPart
        {
            [DebuggerStepThrough()]
            get { return urlPart; }
            [DebuggerStepThrough()]
            set { urlPart = value; }
        }

        public string DescriptionPart
        {
            [DebuggerStepThrough()]
            get { return descriptionPart; }
            [DebuggerStepThrough()]
            set { descriptionPart = value; }
        }

        public string ModuleNamespace
        {
            [DebuggerStepThrough()]
            get { return moduleNamespace; }
            [DebuggerStepThrough()]
            set { moduleNamespace = value; }
        }

        public string ModuleFullName
        {
            [DebuggerStepThrough()]
            get { return moduleFullName; }
            [DebuggerStepThrough()]
            set { moduleFullName = value; }
        }

        public string UserHostAddressPart
        {
            [DebuggerStepThrough()]
            get { return userHostAddressPart; }
            [DebuggerStepThrough()]
            set { userHostAddressPart = value; }
        }

        public string SessionIdPart
        {
            [DebuggerStepThrough()]
            get { return sessionIdPart; }
            [DebuggerStepThrough()]
            set { sessionIdPart = value; }
        }
    }
}