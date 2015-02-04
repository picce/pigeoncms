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
    public enum CommentStatus
    {
        ToApprove = 0,
        Approved = 1,
        Unapproved = 2,
        Spam = 3
    }

    [DebuggerDisplay("Id={id}, Comment={comment}")]
    public class CommentItem: ITable
    {
        private int id = 0;
        private int groupId = 0;
        private DateTime dateInserted;
        private string userInserted = "";
        private string userHostAddress = "";
        private string name = "";
        private string email = "";
        private string comment = "";
        private CommentStatus status = CommentStatus.ToApprove;
        

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

        /// <summary>
        /// group of comments related to the same item
        /// </summary>
        public int GroupId
        {
            [DebuggerStepThrough()]
            get { return groupId; }
            [DebuggerStepThrough()]
            set { groupId = value; }
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

        public string UserHostAddress
        {
            [DebuggerStepThrough()]
            get { return userHostAddress; }
            [DebuggerStepThrough()]
            set { userHostAddress = value; }
        }

        public string Name
        {
            [DebuggerStepThrough()]
            get { return name; }
            [DebuggerStepThrough()]
            set { name = value; }
        }

        public string Email
        {
            [DebuggerStepThrough()]
            get { return email; }
            [DebuggerStepThrough()]
            set { email = value; }
        }

        public string Comment
        {
            [DebuggerStepThrough()]
            get { return comment; }
            [DebuggerStepThrough()]
            set { comment = value; }
        }

        public CommentStatus Status
        {
            [DebuggerStepThrough()]
            get { return status; }
            [DebuggerStepThrough()]
            set { status = value; }
        }

        public CommentItem() { }
    }


    [Serializable]
    public class CommentFilter
    {
        private int id = 0;
        private int groupId = 0;
        private string userInserted = "";
        private string name = "";
        private string email = "";
        private string userHostAddressPart = "";
        private bool filterStatus = false;
        private CommentStatus status = CommentStatus.ToApprove;


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

        public int GroupId
        {
            [DebuggerStepThrough()]
            get { return groupId; }
            [DebuggerStepThrough()]
            set { groupId = value; }
        }

        public string UserInserted
        {
            [DebuggerStepThrough()]
            get { return userInserted; }
            [DebuggerStepThrough()]
            set { userInserted = value; }
        }

        public string Name
        {
            [DebuggerStepThrough()]
            get { return name; }
            [DebuggerStepThrough()]
            set { name = value; }
        }

        public string Email
        {
            [DebuggerStepThrough()]
            get { return email; }
            [DebuggerStepThrough()]
            set { email = value; }
        }

        public string UserHostAddressPart
        {
            [DebuggerStepThrough()]
            get { return userHostAddressPart; }
            [DebuggerStepThrough()]
            set { userHostAddressPart = value; }
        }

        public bool FilterStatus
        {
            [DebuggerStepThrough()]
            get { return filterStatus; }
            [DebuggerStepThrough()]
            set { filterStatus = value; }
        }

        public CommentStatus Status
        {
            [DebuggerStepThrough()]
            get { return status; }
            [DebuggerStepThrough()]
            set { status = value; }
        }
    }
}