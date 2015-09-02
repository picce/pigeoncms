using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PigeonCms.Shop;

public partial class _products : Acme.BasePage
{

    List<ProductItem> products = null;
    protected List<ProductItem> Products
    {
        get
        {
            if (products == null)
            {
                var man = new ProductItemsManager();
                var filter = new ProductItemFilter();
                filter.ShowOnlyRootItems = true;
                filter.Enabled = PigeonCms.Utility.TristateBool.True;
                products = man.GetByFilter(filter, "");
            }
            return products;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        repProducts.DataSource = Products;
        repProducts.DataBind();
    }
    protected void repProducts_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        var item = (ProductItem)e.Item.DataItem;
        HyperLink HypTitle = e.Item.FindControl("HypTitle") as HyperLink;
        Literal LitDescription = e.Item.FindControl("LitDescription") as Literal;
        Literal LitPrice = e.Item.FindControl("LitPrice") as Literal;
        Literal LitSalePrice = e.Item.FindControl("LitSalePrice") as Literal;

        HypTitle.Text = item.Title + "--" + item.ProductType.ToString();
        HypTitle.NavigateUrl = "/contents/" + item.Alias + "/" + item.Id;
        LitDescription.Text = item.Description;
        LitPrice.Text = item.RegularPrice.ToString("C");
        LitSalePrice.Text = item.SalePrice.ToString("C");

    }
}