using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;
using PigeonCms;
using System.Collections.Generic;
using System.Threading;
using PigeonCms.Controls.ItemFields;

namespace PigeonCms
{
    public class HelloWorldItem : Item
    {
        public HelloWorldItem() : base("PigeonCms.HelloWorldItem") { }

        
        [ItemPropertiesMap]
        public class CustomProp1 : ItemPropertiesDefs
        {

            [FormField]
            public string TextSimple { get; set; }

            [FormField(true)]
            public Translation TextSimpleLocalized { get; set; }

            [FormField(false, FormFieldTypeEnum.Html)]
            public string TextHtml { get; set; }

            [FormField(true, FormFieldTypeEnum.Html)]
            public Translation TextHtmlLocalized { get; set; }

            [FormField(false, FormFieldTypeEnum.Combo, "green;red;yellow;white;black")]
            public string SelectColor { get; set; }

            [ImageFormField(false, "jpg")]
            public string Image1 { get; set; }

            [FileFormField(false, "")]
            public string File1 { get; set; }

            [FormField(false, FormFieldTypeEnum.Check)]
            public bool Flag1 { get; set; }

            [FormField(false, FormFieldTypeEnum.Numeric)]
            public int Number1 { get; set; }
        }

        [ItemPropertiesMap(ItemPropertiesMapAttribute.MapTargetEnum.CustomString2)]
        public class CustomProp2 : ItemPropertiesDefs
        {
            [FormField]
            public string OtherSimpleText { get; set; }
        }

        public FileMetaInfo ImageHeader
        {
            get { return FindImage(""); }
        }
    }

    public class HelloWorldItemFilter : ItemsFilter
    {
        public HelloWorldItemFilter() 
        {
            this.ItemType = "PigeonCms.HelloWorldItem";
        }

        /*public int Status
        {
            [DebuggerStepThrough()]
            get { return base.CustomInt1; }
            [DebuggerStepThrough()]
            set { base.CustomInt1 = value; }
        }*/

    }

    public class HelloWorldItemsManager : ItemsManager<HelloWorldItem, HelloWorldItemFilter>
    {
        public HelloWorldItemsManager(bool checkUserContext, bool writeMode)
            : base(checkUserContext, writeMode) { }

        public override int DeleteById(int id)
        {
            return base.DeleteById(id);
        }

    }
}