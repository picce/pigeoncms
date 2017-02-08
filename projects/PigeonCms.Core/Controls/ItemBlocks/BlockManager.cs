using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using PigeonCms;
using PigeonCms.Controls.ItemFields;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace PigeonCms.Core.Controls.ItemBlocks
{
    public static class BlockManager
    {
        public static List<BaseBlockItem> DeserializeFromEditor(string json, Func<string, string> fileTranslator)
        {
            List<BaseBlockItem> result = new List<BaseBlockItem>();

            JObject root = JObject.Parse(json);

            if (root.HasValues && root["blocks"] != null)
            {
                result = JsonConvert.DeserializeObject<List<BaseBlockItem>>(root["blocks"].ToString());
                foreach (BaseBlockItem item in result)
                {
                    item.TranslateFileProperty(fileTranslator);
                }
            }

            return result;
        }

        public static BaseBlockItem CreateBlock(JObject block, Func<string, string> fileTranslator)
        {
            JToken token = null;
            if (!block.TryGetValue("type", out token))
                throw new Exception("Missing type");

            string type = token.Value<string>();

            if (!block.TryGetValue("data", out token))
                throw new Exception("Missing data");

            Type blockType = Type.GetType(string.Format("{0}.{1}BlockItem", typeof(BaseBlockItem).Namespace, type), false);

            if (blockType == null)
                throw new Exception("Invalid type" + type);

            BaseBlockItem blockObj = (BaseBlockItem)Activator.CreateInstance(blockType);
            blockObj.Deserialize((JObject)token, fileTranslator);
            return blockObj;
        }

        public static Translation GetTranslation(this JObject json, string property, Func<string, string> fileTranslator = null)
        {
            Translation result = new Translation();
            Dictionary<string, string> values = new Dictionary<string, string>();
            try
            {
                JToken token = null;
                if (json.TryGetValue(property, out token))
                {
                    values = JsonConvert.DeserializeObject<Dictionary<string, string>>(token.ToString());
                }
            }
            catch
            {

            }

            foreach (KeyValuePair<string, string> culture in Config.CultureList)
            {
                result[culture.Key] = values.ContainsKey(culture.Key) ? values[culture.Key] : string.Empty;
                if (fileTranslator != null)
                    result[culture.Key] = fileTranslator(result[culture.Key]);
            }

            return result;
        }

        public static string GetString(this JObject json, string property, string defaultValue = "")
        {
            JToken token = null;
            return json.TryGetValue(property, out token) ? token.Value<string>() : defaultValue;
        }

        public static void Deserialize(this BaseBlockItem block, JObject json, Func<string, string> fileTranslator)
        {
            ItemPropertiesDefs props = block.PropertiesList;
            Type type = props.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo property in properties)
            {
                ItemFieldAttribute attribute = (ItemFieldAttribute)property.GetCustomAttribute(typeof(ItemFieldAttribute));
                if (attribute == null)
                    continue;

                Func<string, string> useTranslator = (s) => { return s; };
                if (attribute is ImageFieldAttribute)
                    useTranslator = fileTranslator;

                if (attribute.Localized)
                {
                    property.SetValue(props, json.GetTranslation(property.Name.ToLower(), useTranslator), null);
                }
                else
                {
                    property.SetValue(props, useTranslator(json.GetString(property.Name.ToLower())), null);
                }
            }
        }

        public static void TranslateFileProperty(this BaseBlockItem block, Func<string, string> fileTranslator)
        {
            ItemPropertiesDefs props = block.PropertiesList;
            Type type = props.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo property in properties)
            {
                ImageFieldAttribute attribute = (ImageFieldAttribute)property.GetCustomAttribute(typeof(ImageFieldAttribute));
                if (attribute == null)
                    continue;

                if (attribute.Localized)
                {
                    Translation translation = (Translation)property.GetValue(props);
                    if (translation != null)
                    {
                        foreach (KeyValuePair<string, string> pair in translation)
                        {
                            translation[pair.Key] = fileTranslator(pair.Value);
                        }
                        property.SetValue(props, translation);
                    }
                }
                else
                {
                    property.SetValue(props, fileTranslator((string)property.GetValue(props)));
                }
            }
        }

        public static string SerializeForStore(List<BaseBlockItem> blocks)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            return JsonConvert.SerializeObject(blocks, settings);
        }

        public static string SerializeForEditor(List<BaseBlockItem> blocks)
        {
            string serializedBlocks = JsonConvert.SerializeObject(blocks, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            return "{ \"blocks\": " + serializedBlocks + " }";
        }
    }
}
