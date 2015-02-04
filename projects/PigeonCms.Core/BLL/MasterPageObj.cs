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
    public class MasterPageObj: ITable
    {
        private string name = "";

        /// <summary>
        /// PKey
        /// </summary>
        [DataObjectField(true)]
        public string Name
        {
            [DebuggerStepThrough()]
            get { return name; }
            [DebuggerStepThrough()]
            set { name = value; }
        }



        #region public methods
        public MasterPageObj() { }

        public MasterPageObj(string name) 
        {
            this.Name = name;
        }
        #endregion
    }


    [Serializable]
    public class MasterPageObjFilter
    {
        #region fields definition

        private string name = "";

        [DataObjectField(true)]
        public string Name
        {
            [DebuggerStepThrough()]
            get { return name; }
            [DebuggerStepThrough()]
            set { name = value; }
        }

        #endregion
    }
}