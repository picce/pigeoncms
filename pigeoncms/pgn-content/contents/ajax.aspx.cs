using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PigeonCms;
using PigeonCms.Shop.PaymentsProvider;
using PigeonCms.Shop;
using System.Web.Services;

public partial class _ajax : Acme.BasePage
{
    [WebMethod]
    public static List<object> GetNextValues(string values, int productId)
    {
        var parentsAttributesValues = values.Split(',').Select(int.Parse).ToList();


        var prodProv = new PigeonCms.Shop.ProductsProvider.CurrentProduct<ProductItem, ProductItemsManager, ProductItemFilter>(productId);
        var attributeValues = prodProv.GetNextAttribute(parentsAttributesValues);

        var ret = new List<object>();

        foreach (var a in attributeValues)
        {
            var value = new
            {
                label = a.Value,
                value = a.Id.ToString()
            };
            ret.Add(value);
        }

        return ret;
    }

    [WebMethod]
    public static object GetThread(string values, int productId)
    {
        var parentsAttributesValues = values.Split(',').Select(int.Parse).ToList();


        var prodProv = new PigeonCms.Shop.ProductsProvider.CurrentProduct<ProductItem, ProductItemsManager, ProductItemFilter>(productId);
        var thread = prodProv.GetProductByAttributeValues(parentsAttributesValues);

        var obj = new
        {
            title = thread.Title,
            description = thread.Description,
            regPrice = thread.RegularPrice.ToString("C"),
            salePrice = thread.SalePrice.ToString("C")
        };

        return obj;

    }

}