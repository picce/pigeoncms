using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PigeonCms.Shop
{
    public class Shipment : ITable
    {
        public Shipment()
        {
        }

        private string shipCode = "";
        [DataObjectField(true)]
        public string ShipCode
        {
            [DebuggerStepThrough()]
            get { return shipCode; }
            [DebuggerStepThrough()]
            set { shipCode = value; }
        }

        private string name = "";
        public string Name
        {
            [DebuggerStepThrough()]
            get { return name; }
            [DebuggerStepThrough()]
            set { name = value; }
        }

        private string assemblyName = "";
        public string AssemblyName
        {
            [DebuggerStepThrough()]
            get { return assemblyName; }
            [DebuggerStepThrough()]
            set { assemblyName = value; }
        }

        private bool enabled = false;
        public bool Enabled
        {
            [DebuggerStepThrough()]
            get { return enabled; }
            [DebuggerStepThrough()]
            set { enabled = value; }
        }
    }

    [Serializable]
    public class ShipmentFilter
    {
        private string shipCode = "";
        public string ShipCode
        {
            [DebuggerStepThrough()]
            get { return shipCode; }
            [DebuggerStepThrough()]
            set { shipCode = value; }
        }

        bool? enabled = null;
        public bool? Enabled
        {
            [DebuggerStepThrough()]
            get { return enabled; }
            [DebuggerStepThrough()]
            set { enabled = value; }
        }
    }

}
