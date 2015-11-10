using PigeonCms.Geo;
using PigeonCms.Shop.ShipmentsProvider;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace PigeonCms.Shop.ShipmentsProvider
{
    public class WeightZones : BaseShipment
    {
        public override decimal CalculateShipAmount(
            IOrder order, 
            NameValueCollection data = null)
        {
            decimal res = 0;
            var minShop = new PigeonCms.Shop.Settings().FreeShippingMinValue;

            if (order.TotalAmount <= minShop || minShop < 0)
            {
                // ship prices
                string myZoneCode = "";
                var zonePrices = new ShipZonesWeightManager().GetByFilter(new ShipZonesWeightFilter(), "");
                string nationCode = (string.IsNullOrEmpty(order.ShipNation) ? order.OrdNation : order.ShipNation);
                int cityCode = -1;
                if(string.IsNullOrEmpty(order.ShipState)) 
                {
                    int.TryParse(order.OrdState, out cityCode);
                } 
                else 
                {
                    int.TryParse(order.ShipState, out cityCode);
                }

                var country = new CountriesManager().GetByKey(nationCode);
                var f = new ShipGeoZonesFilter();
                // check continent
                f.Continent = country.Continent;
                var list = new ShipGeoZonesManager().GetByFilter(f, "");
                if (list.Count > 0)
                {
                    myZoneCode = list[0].ZoneCode;
                }
                // check countries
                f = new ShipGeoZonesFilter();
                f.CountryCode = country.Code;
                list = new ShipGeoZonesManager().GetByFilter(f, "");
                if (list.Count > 0)
                {
                    myZoneCode = list[0].ZoneCode;
                }
                if (cityCode > 0)
                {
                    var zone = new ZonesManager().GetByKey(cityCode);
                    // check city
                    f = new ShipGeoZonesFilter();
                    f.CityCode = zone.Code;
                    list = new ShipGeoZonesManager().GetByFilter(f, "");
                    if (list.Count > 0)
                    {
                        myZoneCode = list[0].ZoneCode;
                    }
                }

                var myZonePrices = zonePrices.Where(x => x.ZoneCode == myZoneCode).ToList();

                var ordersManager = new OrdersManager<Order, OrdersFilter, OrderRow, OrderRowsFilter>();
                var rows = ordersManager.GetByKey(order.Id).Rows;
                decimal sumOfWeights = ordersManager.GetOrderWeight(rows);


                foreach (var z in myZonePrices)
                {
                    if (z.WeightFrom <= sumOfWeights && z.WeightTo >= sumOfWeights)
                    {
                        res = z.ShippingPrice;
                    }
                }
            }
            else
            {
                res = 0m;
            }

            return res;
        }
    }
}
