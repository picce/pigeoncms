using PigeonCms;
using PigeonCms.Controls.ItemFields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Acme.MyPrj
{
    public class TntItem : Item
    {
        public TntItem() : base("Acme.TntItem") { }

        //map subclass istance json serialization on field CustomString1
        [ItemPropertiesMap(ItemPropertiesMapAttribute.MapTargetEnum.CustomString1)]
        public class CustomProp1 : ItemPropertiesDefs
        {
            [ItemField]
            public string Brand { get; set; }

            [ItemField(ItemFieldEditorType.Number)]
            public int Sticks { get; set; }
        }

    }

    public class TntItemsFilter : ItemsFilter
    {
        public TntItemsFilter()
        {
            this.ItemType = "Acme.TntItem";
        }
    }

    public class TntItemsManager : ItemsManager<TntItem, TntItemsFilter>
    {
        public TntItemsManager(bool checkUserContext, bool writeMode)
            : base(checkUserContext, writeMode) { }

    }
}
