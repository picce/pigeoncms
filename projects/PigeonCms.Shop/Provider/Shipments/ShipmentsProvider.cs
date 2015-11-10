using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using System.Data.Common;
using System.Diagnostics;
using PigeonCms;
using PigeonCms.Core.Helpers;
using System.Collections.Specialized;
using System.Web.UI;


namespace PigeonCms.Shop.ShipmentsProvider
{
    public abstract class BaseShipment
    {
        protected Shipment ShipmentData = null;

        public BaseShipment() { }

        public virtual BaseShipment SetParams(Shipment shipment)
        {
            this.ShipmentData = shipment;
            return this;
        }

        public abstract decimal CalculateShipAmount(
            IOrder order, 
            NameValueCollection data = null);
    }

    public class ShipmentFactory
    {
        public static BaseShipment Create(string shipCode)
        {
            if (string.IsNullOrEmpty(shipCode))
                throw new ArgumentException("Missing shipCode");

            var shipment = new Shipment();
            var man = new ShipmentsManager();
            shipment = man.GetByKey(shipCode);

            return create(shipment);
        }

        public static BaseShipment Create(Shipment shipment)
        {
            if (string.IsNullOrEmpty(shipment.ShipCode))
                throw new ArgumentException("Missing shipCode");

            return create(shipment);
        }

        private static BaseShipment create(Shipment shipment)
        {
            object[] parameters = new object[1];
            parameters[0] = shipment;

            object obj = PigeonCms.Reflection.Process(
                shipment.AssemblyName, "SetParams", parameters);

            return (BaseShipment)obj;
        }
    }
}