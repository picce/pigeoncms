using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PigeonCms.Shop;

public partial class _detail : Acme.BasePage
{

    int ProductId = 0;

    ProductItem product = null;
    protected ProductItem Product
    {
        get
        {
            if (product == null)
            {
                var man = new ProductItemsManager();
                product = man.GetByKey(ProductId);
            }
            return product;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        int.TryParse(Request["productId"], out ProductId);
        foreach (var thread in Product.ThreadItems)
        {
            string values = "";
            foreach(var value in thread.AttributeValues) {
                values += " " + value.Value;
            }
            LitVariants.Text += @"
            <div class='product-box'>
                <h2> " + thread.Title +  @"</h2>
                <p> " + values + @"</p>
            </div>
            ";
        }

        if ((bool)Product.HasThreads)
        {
            LitVariants.Visible = true;
        }

    }
}