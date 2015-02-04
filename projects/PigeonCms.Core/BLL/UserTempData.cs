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
using System.Web.Compilation;
using System.Reflection;



namespace PigeonCms
{
    public class UserTempData: ITable
    {
        public UserTempData()
        {
            this.columns.Clear();
            for (int i = 0; i < UserTempDataManager.NO_OF_COLS; i++)
            {
                this.columns.Add("");
            }
        }

        /// <summary>
        /// Automatic Id as PKey
        /// </summary>
        [DataObjectField(true)]
        public int Id { get; set; }

        private string username= "";
        /// <summary>
        /// record inserted user
        /// </summary>
        public string Username
        {
            [DebuggerStepThrough()]
            get { return username; }
            [DebuggerStepThrough()]
            set { username = value; }
        }

        private string sessionId = "";
        /// <summary>
        /// sessionId
        /// </summary>
        public string SessionId
        {
            [DebuggerStepThrough()]
            get { return sessionId; }
            [DebuggerStepThrough()]
            set { sessionId = value; }
        }

        private DateTime dateInserted;
        /// <summary>
        /// record inserted date
        /// </summary>
        public DateTime DateInserted
        {
            [DebuggerStepThrough()]
            get { return dateInserted; }
            [DebuggerStepThrough()]
            set { dateInserted = value; }
        }

        private DateTime dateExpiration;
        /// <summary>
        /// record updated date
        /// </summary>
        public DateTime DateExpiration
        {
            [DebuggerStepThrough()]
            get { return dateExpiration; }
            [DebuggerStepThrough()]
            set { dateExpiration = value; }
        }

        private bool enabled = true;
        /// <summary>
        /// record enabled or not
        /// </summary>
        public bool Enabled
        {
            [DebuggerStepThrough()]
            get { return enabled; }
            [DebuggerStepThrough()]
            set { enabled = value; }
        }

        private List<string> columns = new List<string>();
        public List<string> Columns
        {
            [DebuggerStepThrough()]
            get { return columns; }
            [DebuggerStepThrough()]
            set { columns = value; }
        }

    }


    [Serializable]
    public class UserTempDataFilter
    {
        private int id = 0;
        private string username = "";
        private string sessionId = "";
        private Utility.TristateBool isExpired = Utility.TristateBool.NotSet;
        private Utility.TristateBool enabled = Utility.TristateBool.NotSet;
        private int numOfRecords = 0;


        [DataObjectField(true)]
        public int Id
        {
            [DebuggerStepThrough()]
            get { return id; }
            [DebuggerStepThrough()]
            set { id = value; }
        }

        [DataObjectField(false)]
        public string Username
        {
            [DebuggerStepThrough()]
            get { return username; }
            [DebuggerStepThrough()]
            set { username = value; }
        }

        [DataObjectField(false)]
        public string SessionId
        {
            [DebuggerStepThrough()]
            get { return sessionId; }
            [DebuggerStepThrough()]
            set { sessionId = value; }
        }

        [DataObjectField(false)]
        public Utility.TristateBool IsExpired
        {
            [DebuggerStepThrough()]
            get { return isExpired; }
            [DebuggerStepThrough()]
            set { isExpired = value; }
        }

        [DataObjectField(false)]
        public Utility.TristateBool Enabled
        {
            [DebuggerStepThrough()]
            get { return enabled; }
            [DebuggerStepThrough()]
            set { enabled = value; }
        }

        /// <summary>
        /// number of records to load. 0 All records
        /// </summary>
        [DataObjectField(false)]
        public int NumOfRecords
        {
            [DebuggerStepThrough()]
            get { return numOfRecords; }
            [DebuggerStepThrough()]
            set { numOfRecords = value; }
        }
    }
}