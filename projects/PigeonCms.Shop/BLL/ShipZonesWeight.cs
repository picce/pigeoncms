using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PigeonCms.Shop
{
    public class ShipZonesWeight : ITable
    {

        public ShipZonesWeight()
        {
        }

        /// <summary>
        /// Automatic Id as PKey
        /// </summary>
        [DataObjectField(true)]
        public int Id { get; set; }

        private string zoneCode = "";
        [DataObjectField(true)]
        public string ZoneCode
        {
            [DebuggerStepThrough()]
            get { return zoneCode; }
            [DebuggerStepThrough()]
            set { zoneCode = value; }
        }

        private decimal weightFrom = 0m;
        [DataObjectField(true)]
        public decimal WeightFrom
        {
            [DebuggerStepThrough()]
            get { return weightFrom; }
            [DebuggerStepThrough()]
            set { weightFrom = value; }
        }

        private decimal weightTo = 0m;
        [DataObjectField(true)]
        public decimal WeightTo
        {
            [DebuggerStepThrough()]
            get { return weightTo; }
            [DebuggerStepThrough()]
            set { weightTo = value; }
        }

        private decimal shippingPrice = 0m;
        [DataObjectField(true)]
        public decimal ShippingPrice
        {
            [DebuggerStepThrough()]
            get { return shippingPrice; }
            [DebuggerStepThrough()]
            set { shippingPrice = value; }
        }

    }

    [Serializable]
    public class ShipZonesWeightFilter
    {
        private int id = 0;
        private string zoneCode = "";
        private decimal weightFrom = 0m;
        private decimal weightTo = 0m;
        private decimal shippingPrice = 0m;

        /// <summary>
        /// PKey
        /// </summary>
        public int Id
        {
            [DebuggerStepThrough()]
            get { return id; }
            [DebuggerStepThrough()]
            set { id = value; }
        }

        public string ZoneCode
        {
            [DebuggerStepThrough()]
            get { return zoneCode; }
            [DebuggerStepThrough()]
            set { zoneCode = value; }
        }

        public decimal WeightFrom
        {
            [DebuggerStepThrough()]
            get { return weightFrom; }
            [DebuggerStepThrough()]
            set { weightFrom = value; }
        }

        public decimal WeightTo
        {
            [DebuggerStepThrough()]
            get { return weightTo; }
            [DebuggerStepThrough()]
            set { weightTo = value; }
        }

        public decimal ShippingPrice
        {
            [DebuggerStepThrough()]
            get { return shippingPrice; }
            [DebuggerStepThrough()]
            set { shippingPrice = value; }
        }

    }

}
