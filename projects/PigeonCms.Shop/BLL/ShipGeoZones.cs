using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PigeonCms.Shop
{
    public class ShipGeoZones : ITable
    {

        public ShipGeoZones()
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

        private string countryCode = "";
        [DataObjectField(false)]
        public string CountryCode
        {
            [DebuggerStepThrough()]
            get { return countryCode; }
            [DebuggerStepThrough()]
            set { countryCode = value; }
        }

        private string cityCode = "";
        [DataObjectField(true)]
        public string CityCode
        {
            [DebuggerStepThrough()]
            get { return cityCode; }
            [DebuggerStepThrough()]
            set { cityCode = value; }
        }

        private string continent = "";
        [DataObjectField(true)]
        public string Continent
        {
            [DebuggerStepThrough()]
            get { return continent; }
            [DebuggerStepThrough()]
            set { continent = value; }
        }

        public bool HasCity
        {
            get 
            {
                return (!string.IsNullOrEmpty(this.CityCode));
            }
        }

        public bool HasCountry
        {
            get
            {
                return (!string.IsNullOrEmpty(this.CountryCode));
            }
        }

        public bool HasContinent
        {
            get
            {
                return (!string.IsNullOrEmpty(this.Continent));
            }
        }

    }

    [Serializable]
    public class ShipGeoZonesFilter
    {
        private int id = 0;
        private string zoneCode = "";
        private string countryCode = "";
        private string cityCode = "";
        private string continent = "";

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

        public string CountryCode
        {
            [DebuggerStepThrough()]
            get { return countryCode; }
            [DebuggerStepThrough()]
            set { countryCode = value; }
        }

        public string CityCode
        {
            [DebuggerStepThrough()]
            get { return cityCode; }
            [DebuggerStepThrough()]
            set { cityCode = value; }
        }

        public string Continent
        {
            [DebuggerStepThrough()]
            get { return continent; }
            [DebuggerStepThrough()]
            set { continent = value; }
        }

    }

}
