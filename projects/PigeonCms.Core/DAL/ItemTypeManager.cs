using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using System.Data.Common;
using System.Xml;
using PigeonCms;


namespace PigeonCms
{
    /// <summary>
    /// DAL for ItemType obj (from Items folder)
    /// </summary>
    public class ItemTypeManager : XmlTypeManager<ItemType, ItemTypeFilter>
    {
        public ItemTypeManager() : base(Config.ItemsPath)
        {
            //specific xml parse steps
            base.ParseSteps += this.parseTagFields;
            //base.ParseSteps += this.parseTagInstallAttributes;
            //specific getByFilter steps
            //base.FilterSteps += this.baseFilterStep;
        }

        private void parseTagFields(ItemType result, XmlDocument doc)
        {
            try
            {
                XmlNodeList memberNodes = doc.SelectNodes("//install//fields");
                foreach (XmlNode nodeParams in memberNodes)
                {
                    XmlNodeList paramsList = nodeParams.SelectNodes("field");
                    foreach (XmlNode nodeParam in paramsList)
                    {
                        if (nodeParam.Attributes["type"] != null)
                        {
                            FormField item = FormBuilder.GetFormFieldFromXmlNode(nodeParam, nodeParams);
                            //if (item.Type == FormFieldTypeEnum.TextTranslated)
                            //{
                            //    //virtual fields for each culture
                            //    foreach (KeyValuePair<string, string> culture in Config.CultureList)
                            //    {
                            //        var tItem = Utility.ObjectCopier.Clone<FormField>(item);
                            //        tItem.Name += "__ " + culture.Value;
                            //        tItem.LabelValue += "&nbsp;[<i>"+ culture.Value +"</i>]";
                            //        tItem.IsTranslationField = true;
                            //        tItem.Type = FormFieldTypeEnum.Text;
                            //        result.Fields.Add(tItem);
                            //    }
                            //}
                            //else
                            result.Fields.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Parsing " + result.FullName + " params", ex);
            }
        }
    }

}