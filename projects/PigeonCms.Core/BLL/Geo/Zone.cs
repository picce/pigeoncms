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



namespace PigeonCms.Geo
{
    public class Zone: ITable
    {
        int id  = 0;
        string
            countryCode,
            code,
            name,
            custom1,
            custom2,
            custom3 = "";

        /// <summary>
        /// Identity as pkey
        /// </summary>
        [DataObjectField(true)]
        public int Id
        {
            [DebuggerStepThrough()]
            get { return id; }
            [DebuggerStepThrough()]
            set { id = value; }
        }

        public string CountryCode
        {
            [DebuggerStepThrough()]
            get { return countryCode; }
            [DebuggerStepThrough()]
            set { countryCode = value; }
        }

        public string Code
        {
            [DebuggerStepThrough()]
            get { return code; }
            [DebuggerStepThrough()]
            set { code = value; }
        }

        public string Name
        {
            [DebuggerStepThrough()]
            get { return name; }
            [DebuggerStepThrough()]
            set { name = value; }
        }

        public string Custom1
        {
            [DebuggerStepThrough()]
            get { return custom1; }
            [DebuggerStepThrough()]
            set { custom1 = value; }
        }

        public string Custom2
        {
            [DebuggerStepThrough()]
            get { return custom2; }
            [DebuggerStepThrough()]
            set { custom2 = value; }
        }

        public string Custom3
        {
            [DebuggerStepThrough()]
            get { return custom3; }
            [DebuggerStepThrough()]
            set { custom3 = value; }
        }

    }


    [Serializable]
    public class ZonesFilter
    {
        int id = 0;
        string countryCode = "";
        string code = "";
        string nameLike = "";

        public int Id
        {
            [DebuggerStepThrough()]
            get { return id; }
            [DebuggerStepThrough()]
            set { id = value; }
        }

        public string CountryCode
        {
            [DebuggerStepThrough()]
            get { return countryCode; }
            [DebuggerStepThrough()]
            set { countryCode = value; }
        }

        public string Code
        {
            [DebuggerStepThrough()]
            get { return code; }
            [DebuggerStepThrough()]
            set { code = value; }
        }

        public string NameLike
        {
            [DebuggerStepThrough()]
            get { return nameLike; }
            [DebuggerStepThrough()]
            set { nameLike = value; }
        }

    }
}