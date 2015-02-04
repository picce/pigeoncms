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
    public class Culture: ITableWithOrdering
    {
        private string cultureCode = "";
        private string displayName = "";
        private bool enabled = true;
        private int ordering = 0;


        /// <summary>
        /// PKey
        /// </summary>
        [DataObjectField(true)]
        public string CultureCode
        {
            [DebuggerStepThrough()]
            get { return cultureCode; }
            [DebuggerStepThrough()]
            set { cultureCode = value; }
        }

        [DataObjectField(false)]
        public string DisplayName
        {
            [DebuggerStepThrough()]
            get { return displayName; }
            [DebuggerStepThrough()]
            set { displayName = value; }
        }

        [DataObjectField(false)]
        public bool Enabled
        {
            [DebuggerStepThrough()]
            get { return enabled; }
            [DebuggerStepThrough()]
            set { enabled = value; }
        }

        [DataObjectField(false)]
        public int Ordering
        {
            [DebuggerStepThrough()]
            get { return ordering; }
            [DebuggerStepThrough()]
            set { ordering = value; }
        }



        #region public methods
        public Culture() { }
        #endregion
    }


    [Serializable]
    public class CulturesFilter
    {
        #region fields definition

        private string cultureCode = "";
        private Utility.TristateBool enabled = Utility.TristateBool.NotSet;

        [DataObjectField(true)]
        public string CultureCode
        {
            [DebuggerStepThrough()]
            get { return cultureCode; }
            [DebuggerStepThrough()]
            set { cultureCode = value; }
        }

        [DataObjectField(false)]
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