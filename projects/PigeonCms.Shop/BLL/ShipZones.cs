using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PigeonCms.Shop
{
    public class ShipZones : ITable
    {
        private string code = "";
        [DataObjectField(true)]
        public string Code
        {
            [DebuggerStepThrough()]
            get { return code; }
            [DebuggerStepThrough()]
            set { code = value; }
        }

        private string title = "";
        [DataObjectField(true)]
        public string Title
        {
            [DebuggerStepThrough()]
            get { return title; }
            [DebuggerStepThrough()]
            set { title = value; }
        }
    }

    [Serializable]
    public class ShipZonesFilter
    {
        string code = "";
        public string Code
        {
            [DebuggerStepThrough()]
            get { return code; }
            [DebuggerStepThrough()]
            set { code = value; }
        }
    }
}
