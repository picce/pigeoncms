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
    public class TemplateBlock: ITable
    {
        private string name = "";
        private string title = "";
        private bool enabled = true;
        private int orderId = 0;

        public TemplateBlock()
        {
        }

        /// <summary>
        /// Name of the block in masterpage
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
        /// Title or short description of the block content
        /// </summary>
        [DataObjectField(false)]
        public string Title
        {
            [DebuggerStepThrough()]
            get { return title; }
            [DebuggerStepThrough()]
            set { title = value; }
        }

        /// <summary>
        /// Enable or disable the block. When disabled it will not be shown in every page
        /// </summary>
        [DataObjectField(false)]
        public bool Enabled
        {
            [DebuggerStepThrough()]
            get { return enabled; }
            [DebuggerStepThrough()]
            set { enabled = value; }
        }

        /// <summary>
        /// Priority of the block.
        /// </summary>
        [DataObjectField(false)]
        public int OrderId
        {
            [DebuggerStepThrough()]
            get { return orderId; }
            [DebuggerStepThrough()]
            set { orderId = value; }
        }
    }

    /// <summary>
    /// Filter used in search
    /// </summary>
    /// <remarks></remarks>
    [Serializable]
    public class TemplateBlockFilter
    {
        #region fields definition

        private string name = "";
        private Utility.TristateBool enabled = Utility.TristateBool.NotSet;

        public string Name
        {
            [DebuggerStepThrough()]
            get { return name; }
            [DebuggerStepThrough()]
            set { name = value; }
        }

        public Utility.TristateBool Enabled
        {
            [DebuggerStepThrough()]
            get { return enabled; }
            [DebuggerStepThrough()]
            set { enabled = value; }
        }
        #endregion
    }
}