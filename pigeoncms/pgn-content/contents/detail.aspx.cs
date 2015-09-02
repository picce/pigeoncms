using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PigeonCms;
using PigeonCms.Shop;

public partial class _detail : Acme.BasePage
{

    int ProductId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        int.TryParse(Request["productId"], out ProductId);
        var man = new AttributeSetsManager();
        var parentsAttributes = new List<int>();
        var parentsAttributesValues = new List<int>();

        var prodProv = new PigeonCms.Shop.ProductsProvider.CurrentProduct<ProductItem, ProductItemsManager, ProductItemFilter>(ProductId);

        PanelDropVariants.Attributes.Add("data-product-id", ProductId.ToString());

        // variants
        for (int i = 0; i < prodProv.Product.Attributes.Count; i++)
        {
            DropDownList d = new DropDownList();
            d.ID = "DropDown" + prodProv.Product.Attributes[i].Name;
            d.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            d.Attributes.Add("data-select-id", prodProv.Product.Attributes[i].Id.ToString());
            if (i == 0)
            {
                d.Attributes.Add("data-select-parent", "");
                foreach (var v in prodProv.GetDefaultAttribute())
                {
                    d.Items.Add(new ListItem(v.Value, v.Id.ToString()));
                }
            }
            else
            {
                parentsAttributes.Add(prodProv.Product.Attributes[i - 1].Id);
                parentsAttributesValues.Add(prodProv.Product.AttributeValues[i - 1].Id);
                d.Attributes.Add("data-select-parent", string.Join(",", parentsAttributes.Select(parent => parent.ToString()).ToArray()));
                foreach (var v in prodProv.GetNextAttribute(parentsAttributesValues))
                {
                    d.Items.Add(new ListItem(v.Value, v.Id.ToString()));
                }
            }

            if (i == prodProv.Product.Attributes.Count - 1)
            {
                d.Attributes.Add("data-last", "");
            }

            var selected = prodProv.Product.AttributeValues.Find(x => x.AttributeId == prodProv.Product.Attributes[i].Id);
            d.SelectedValue = selected.Id.ToString();

            PanelDropVariants.Controls.Add(d);
        }

        LitTitle.Text = prodProv.Product.Title;
        LitDescription.Text = prodProv.Product.Description;
        LitRegPrice.Text = prodProv.Product.RegularPrice.ToString("C");
        LitSalePrice.Text = prodProv.Product.SalePrice.ToString("C");
    }

}