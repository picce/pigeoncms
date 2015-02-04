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
    [DebuggerDisplay("Id={id}, Name={name}, Pattern={pattern}")]
    public class MvcRoute: ITableWithOrdering
    {
        private int id = 0;
        private string name = "";
        private string pattern = "";
        private bool published = true;
        private int ordering = 0;
        private string currMasterPage = "";
        private string currTheme = "";
        private bool isCore = false;
        private bool useSsl = false;
        private List<MvcRouteParam> paramsList = new List<MvcRouteParam>();
        

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
        /// name of the mvc route
        /// </summary>
        public string Name
        {
            [DebuggerStepThrough()]
            get { return name; }
            [DebuggerStepThrough()]
            set { name = value; }
        }

        /// <summary>
        /// route pattern (ex. pages/{pagename})
        /// </summary>
        public string Pattern
        {
            [DebuggerStepThrough()]
            get { return pattern; }
            [DebuggerStepThrough()]
            set { pattern = value; }
        }

        public bool Published
        {
            [DebuggerStepThrough()]
            get { return published; }
            [DebuggerStepThrough()]
            set { published = value; }
        }

        public int Ordering
        {
            [DebuggerStepThrough()]
            get { return ordering; }
            [DebuggerStepThrough()]
            set { ordering = value; }
        }

        public string CurrMasterPage
        {
            [DebuggerStepThrough()]
            get { return currMasterPage; }
            [DebuggerStepThrough()]
            set { currMasterPage = value; }
        }

        public string CurrTheme
        {
            [DebuggerStepThrough()]
            get { return currTheme; }
            [DebuggerStepThrough()]
            set { currTheme = value; }
        }

        public bool IsCore
        {
            [DebuggerStepThrough()]
            get { return isCore; }
            [DebuggerStepThrough()]
            set { isCore = value; }
        }

        public bool UseSsl
        {
            [DebuggerStepThrough()]
            get { return useSsl; }
            [DebuggerStepThrough()]
            set { useSsl = value; }
        }

        public List<MvcRouteParam> ParamsList
        {
            [DebuggerStepThrough()]
            get { return paramsList; }
            [DebuggerStepThrough()]
            set { paramsList = value; }
        }

        #region public methods
        public MvcRoute(){}
        #endregion
    }


    [Serializable]
    public class MvcRoutesFilter
    {
        #region fields definition

        private int id = 0;
        private string name = "";
        private string pattern = "";
        private Utility.TristateBool published = Utility.TristateBool.NotSet;        

        [DataObjectField(true)]
        public int Id
        {
            [DebuggerStepThrough()]
            get { return id; }
            [DebuggerStepThrough()]
            set { id = value; }
        }

        public string Name
        {
            [DebuggerStepThrough()]
            get { return name; }
            [DebuggerStepThrough()]
            set { name = value; }
        }

        public string Pattern
        {
            [DebuggerStepThrough()]
            get { return pattern; }
            [DebuggerStepThrough()]
            set { pattern = value; }
        }

        [DataObjectField(false)]
        public Utility.TristateBool Published
        {
            [DebuggerStepThrough()]
            get { return published; }
            [DebuggerStepThrough()]
            set { published = value; }
        }
        #endregion
    }

    [DebuggerDisplay("RouteId={routeId}, Key={key}, Value={value}")]
    public class MvcRouteParam : ITable
    {
        private int routeId = 0;
        private string key = "";
        private string value = "";
        private string constraint = "";

        public int RouteId
        {
            [DebuggerStepThrough()]
            get { return routeId; }
            [DebuggerStepThrough()]
            set { routeId = value; }
        }

        public string Key
        {
            [DebuggerStepThrough()]
            get { return key; }
            [DebuggerStepThrough()]
            set { key = value; }
        }

        public string Value
        {
            [DebuggerStepThrough()]
            get { return this.value; }
            [DebuggerStepThrough()]
            set { this.value = value; }
        }

        /// <summary>
        /// </summary>
        public string Constraint
        {
            [DebuggerStepThrough()]
            get { return constraint; }
            [DebuggerStepThrough()]
            set { constraint = value; }
        }

        /// <summary>
        /// </summary>
        public string DataType
        {
            [DebuggerStepThrough()]
            get { return "string"; }
        }

        #region public methods
        public MvcRouteParam() { }

        public MvcRouteParam(string key, string value, string constraint)
        {
            this.Key = key;
            this.Value = value;
            this.Constraint = constraint;
        }

        #endregion
    }

    public class MvcRouteParamsFilter
    {
        #region fields definition

        private int routeId = 0;
        private string key = "";

        public int RouteId
        {
            [DebuggerStepThrough()]
            get { return routeId; }
            [DebuggerStepThrough()]
            set { routeId = value; }
        }

        public string Key
        {
            [DebuggerStepThrough()]
            get { return key; }
            [DebuggerStepThrough()]
            set { key = value; }
        }

        #endregion
    }
}