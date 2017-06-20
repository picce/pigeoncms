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

namespace PigeonCms.Core.Controls.ItemBlocks
{
    [JsonConverter(typeof(BlockConverter))]
    public class BaseBlockItem : Item
    {
        public BaseBlockItem() : base() { }
        public BaseBlockItem(string itemType) : base(itemType) { }
    }

    public class BlockConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        //TOCHECK PropertiesList
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            BaseBlockItem result = null;
            Type blockType = null;

            if (reader.TokenType == JsonToken.StartObject)
                reader.Read();

            while (reader.TokenType == JsonToken.PropertyName)
            {
                string propertyName = reader.Value.ToString();

                switch (propertyName)
                {
                    case "type":
                        //TODO allow namespace from different Assembly
                        //example: "PigeonCms.Core.Controls.ItemBlocks.HeaderBlockItem"
                        string typeName = string.Format("{0}.{1}BlockItem",
                            typeof(BaseBlockItem).Namespace,
                            reader.ReadAsString());

                        blockType = Type.GetType(typeName, false);
                        result = (BaseBlockItem)Activator.CreateInstance(blockType);
                        break;
                    case "data":
                        Type propertiesType = Reflection.GetNestedType<ItemPropertiesDefs>(blockType);
                        reader.Read();
                        if (result != null)
                        {
                            result.PropertiesList[0] = (ItemPropertiesDefs)serializer.Deserialize(reader, propertiesType);
                            //result.PropertiesList = (List<ItemPropertiesDefs>)serializer.Deserialize(reader, propertiesType);

                        }
                        break;
                    default:
                        reader.Skip();
                        break;
                }

                reader.Read();
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            BaseBlockItem block = value as BaseBlockItem;
            if (block == null)
                return;

            serializer.Serialize(writer, new
            {
                type = block.GetType().Name.Replace("BlockItem", ""),
                data = block.PropertiesList[0]
            });
        }
    }
}
