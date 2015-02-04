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
using PigeonCms;

namespace PigeonCms
{
    /// <summary>
    /// a msg in messages table
    /// </summary>
    [DebuggerDisplay("Id={id}, OwnerUser={ownerUser}, FromUser={fromUser}, ToUser={ToUser}, Content={content}")]
    public class Message: ITable
    {
        public class MessageEventArgs : EventArgs
        {
            private int messageId = 0;
            private string operation = "";
            private string message = "";
            private bool result = true;

            public int MessageId
            {
                get { return messageId; }
            }

            public string Message
            {
                get { return message; }
            }

            public string Operation
            {
                get { return operation; }
            }

            public bool Result
            {
                get { return result; }
            }

            public MessageEventArgs(int messageId, string operation, string message, bool result)
            {
                this.messageId = messageId;
                this.operation = operation;
                this.message = message;
                this.result = result;
            }
        }

        private int id = 0;
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

        private string ownerUser = "";
        public string OwnerUser
        {
            [DebuggerStepThrough()]
            get { return ownerUser; }
            [DebuggerStepThrough()]
            set { ownerUser = value; }
        }

        private string fromUser = "";
        public string FromUser
        {
            [DebuggerStepThrough()]
            get { return fromUser; }
            [DebuggerStepThrough()]
            set { fromUser = value; }
        }

        private string toUser = "";
        public string ToUser
        {
            [DebuggerStepThrough()]
            get { return toUser; }
            [DebuggerStepThrough()]
            set { toUser = value; }
        }

        private string title = "";
        public string Title
        {
            [DebuggerStepThrough()]
            get { return title; }
            [DebuggerStepThrough()]
            set { title = value; }
        }

        private string description = "";
        public string Description
        {
            [DebuggerStepThrough()]
            get { return description; }
            [DebuggerStepThrough()]
            set { description = value; }
        }

        private DateTime dateInserted;
        public DateTime DateInserted
        {
            [DebuggerStepThrough()]
            get { return dateInserted; }
            [DebuggerStepThrough()]
            set { dateInserted = value; }
        }

        private int priority = 1;
        public int Priority
        {
            [DebuggerStepThrough()]
            get { return priority; }
            [DebuggerStepThrough()]
            set { priority = value; }
        }

        private bool isRead = false;
        public bool IsRead
        {
            [DebuggerStepThrough()]
            get { return isRead; }
            [DebuggerStepThrough()]
            set { isRead = value; }
        }

        private bool isStarred = false;
        public bool IsStarred
        {
            [DebuggerStepThrough()]
            get { return isStarred; }
            [DebuggerStepThrough()]
            set { isStarred = value; }
        }

        private bool visible = true;
        public bool Visible
        {
            [DebuggerStepThrough()]
            get { return visible; }
            [DebuggerStepThrough()]
            set { visible = value; }
        }

        private int itemId = 0;
        public int ItemId
        {
            [DebuggerStepThrough()]
            get { return itemId; }
            [DebuggerStepThrough()]
            set { itemId = value; }
        }

        private int moduleId = 0;
        public int ModuleId
        {
            [DebuggerStepThrough()]
            get { return moduleId; }
            [DebuggerStepThrough()]
            set { moduleId = value; }
        }

        public string GetOriginalMessageHeader()
        {
            string res = "";
            res = "<br /><br />----------Original message----------<br />"
                + "<table>"
                + "<tr>"
                + "<td>Subject:</td>"
                + "<td>" + this.Title + "</td>"
                + "</tr>"
                + "<tr>"
                + "<td>Date:</td>"
                + "<td>" + this.DateInserted + "</td>"
                + "</tr>"
                + "<tr>"
                + "<td>From:</td>"
                + "<td>" + this.FromUser + "</td>"
                + "</tr>"
                + "<tr>"
                + "<td>To:</td>"
                + "<td>" + this.ToUser + "</td>"
                + "</tr>"
                + "</table><br />";

            return res;
        }
    }


    [Serializable]
    public class MessagesFilter
    {
        private int id = 0;
        private string ownerUser = "";
        private string fromUser = "";
        private string toUserLike = "";
        private DatesRange dateInsertedRange = new DatesRange(DatesRange.RangeType.Always);
        private int itemId = 0;
        private int moduleId = 0;
        private Utility.TristateBool isRead = Utility.TristateBool.NotSet;
        private Utility.TristateBool isStarred = Utility.TristateBool.NotSet;
        private Utility.TristateBool visible = Utility.TristateBool.NotSet;
        private string titleSearch = "";
        private string descriptionSearch = "";
        private string fullSearch = "";
        private int topRows = 0;

        [DataObjectField(true)]
        public int Id
        {
            [DebuggerStepThrough()]
            get { return id; }
            [DebuggerStepThrough()]
            set { id = value; }
        }

        [DataObjectField(false)]
        public string OwnerUser
        {
            [DebuggerStepThrough()]
            get { return ownerUser; }
            [DebuggerStepThrough()]
            set { ownerUser = value; }
        }

        [DataObjectField(false)]
        public string FromUser
        {
            [DebuggerStepThrough()]
            get { return fromUser; }
            [DebuggerStepThrough()]
            set { fromUser = value; }
        }

        [DataObjectField(false)]
        public string ToUserLike
        {
            [DebuggerStepThrough()]
            get { return toUserLike; }
            [DebuggerStepThrough()]
            set { toUserLike = value; }
        }

        public DatesRange DateInsertedRange
        {
            [DebuggerStepThrough()]
            get { return dateInsertedRange; }
            [DebuggerStepThrough()]
            set { dateInsertedRange = value; }
        }

        [DataObjectField(false)]
        public int ItemId
        {
            [DebuggerStepThrough()]
            get { return itemId; }
            [DebuggerStepThrough()]
            set { itemId = value; }
        }

        [DataObjectField(false)]
        public int ModuleId
        {
            [DebuggerStepThrough()]
            get { return moduleId; }
            [DebuggerStepThrough()]
            set { moduleId = value; }
        }

        [DataObjectField(false)]
        public Utility.TristateBool IsRead
        {
            [DebuggerStepThrough()]
            get { return isRead; }
            [DebuggerStepThrough()]
            set { isRead = value; }
        }

        [DataObjectField(false)]
        public Utility.TristateBool IsStarred
        {
            [DebuggerStepThrough()]
            get { return isStarred; }
            [DebuggerStepThrough()]
            set { isStarred = value; }
        }

        [DataObjectField(false)]
        public Utility.TristateBool Visible
        {
            [DebuggerStepThrough()]
            get { return visible; }
            [DebuggerStepThrough()]
            set { visible = value; }
        }

        [DataObjectField(false)]
        public string TitleSearch
        {
            [DebuggerStepThrough()]
            get { return titleSearch; }
            [DebuggerStepThrough()]
            set { titleSearch = value; }
        }

        [DataObjectField(false)]
        public string DescriptionSearch
        {
            [DebuggerStepThrough()]
            get { return descriptionSearch; }
            [DebuggerStepThrough()]
            set { descriptionSearch = value; }
        }

        [DataObjectField(false)]
        public string FullSearch
        {
            [DebuggerStepThrough()]
            get { return fullSearch; }
            [DebuggerStepThrough()]
            set { fullSearch = value; }
        }

        /// <summary>
        /// number of records to load. 0 All records
        /// </summary>
        [DataObjectField(false)]
        public int TopRows
        {
            [DebuggerStepThrough()]
            get { return topRows; }
            [DebuggerStepThrough()]
            set { topRows = value; }
        }
    }
}
