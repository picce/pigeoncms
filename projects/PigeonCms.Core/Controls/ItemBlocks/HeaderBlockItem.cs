using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using PigeonCms;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using PigeonCms.Controls.ItemFields;

namespace PigeonCms.Core.Controls.ItemBlocks
{
    public class HeaderBlockItem : BaseBlockItem
    {

        public HeaderBlockItem() : base("Pigeon.Core.Controls.ItemsBlocks.HeaderBlock") { }


        [ItemPropertiesMap]
        public class PropertiesDefs : ItemPropertiesDefs
        {
            [FormField(true)]
            public Translation Title { get; set; }

            public string TitleStyle { get; set; }

            [ImageFormField(false, "jpg")]
            public string Image { get; set; }

            [ImageFormField(false, "jpg")]
            public string MobileImage { get; set; }

            [FormField(true)]
            public Translation Subtitle { get; set; }

        }
    }
    
}
