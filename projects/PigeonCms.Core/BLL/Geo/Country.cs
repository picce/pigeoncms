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
    public class Country: ITable
    {
        string 
            code = "",
            iso3 = "",
            continent = "",
            name = "",
            custom1 = "",
            custom2 = "",
            custom3 = "";

        /// <summary>
        /// country code as PKey (iso2 code)
        /// </summary>
        [DataObjectField(true)]
        public string Code
        {
            [DebuggerStepThrough()]
            get { return code; }
            [DebuggerStepThrough()]
            set { code = value; }
        }

        public string Iso3
        {
            [DebuggerStepThrough()]
            get { return iso3; }
            [DebuggerStepThrough()]
            set { iso3 = value; }
        }

        public string Continent
        {
            [DebuggerStepThrough()]
            get { return continent; }
            [DebuggerStepThrough()]
            set { continent = value; }
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
    public class CountriesFilter
    {
        private string code = "";
        private string iso3 = "";
        string nameLike = "";

        public string Code
        {
            [DebuggerStepThrough()]
            get { return code; }
            [DebuggerStepThrough()]
            set { code = value; }
        }

        public string Iso3
        {
            [DebuggerStepThrough()]
            get { return iso3; }
            [DebuggerStepThrough()]
            set { iso3 = value; }
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