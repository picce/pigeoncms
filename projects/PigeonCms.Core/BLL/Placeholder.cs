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
    public class Placeholder: ITable
    {
        private string name = "";
        private bool visible = true;
        private string content = "";

        public Placeholder()
        {
        }

        /// <summary>
        /// Name used as primary key
        /// </summary>
        [DataObjectField(true)]
        public string Name
        {
            [DebuggerStepThrough()]
            get { return name; }
            [DebuggerStepThrough()]
            set { name = value; }
        }

        /// <summary>
        /// content of placeholder
        /// </summary>
        [DataObjectField(false)]
        public string Content
        {
            [DebuggerStepThrough()]
            get { return  content; }
            [DebuggerStepThrough()]
            set { content = value; }
        }

        /// <summary>
        /// Show or not the content in each istance
        /// </summary>
        [DataObjectField(false)]
        public bool Visible
        {
            [DebuggerStepThrough()]
            get { return visible; }
            [DebuggerStepThrough()]
            set { visible = value; }
        }

        public int OrderId
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

    }

    /// <summary>
    /// Filter used in search
    /// </summary>
    /// <remarks></remarks>
    [Serializable]
    public class PlaceholderFilter
    {
        #region fields definition

        private string name = "";
        private Utility.TristateBool visible = Utility.TristateBool.NotSet;

        public string Name
        {
            [DebuggerStepThrough()]
            get { return name; }
            [DebuggerStepThrough()]
            set { name = value; }
        }

        public Utility.TristateBool Visible
        {
            [DebuggerStepThrough()]
            get { return visible; }
            [DebuggerStepThrough()]
            set { visible = value; }
        }
        #endregion
    }

}