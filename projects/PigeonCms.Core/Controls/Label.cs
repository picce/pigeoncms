using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PigeonCms;

[assembly: TagPrefix("PigeonCms.Controls", "pgn")]
namespace PigeonCms.Controls
{
    [DefaultProperty("Text")]
    [ParseChildren(true, "Text")]//associate InnerHtml to Text prop
    [ToolboxData("<{0}:Label1 runat=server></{0}:Label1>")]
    public class Label : WebControl
    {
        private string text = "";
        private string resourceSet = "";
        private string resourceId = "";
        private bool translations = false;
        private ContentEditorProvider.Configuration.EditorTypeEnum textMode = ContentEditorProvider.Configuration.EditorTypeEnum.Text;


        protected override void AddedControl(Control control, int index)
        {
            //base.AddedControl(control, index);
        }

        //[Bindable(true)]
        //[Localizable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        public string Text
        {
            get
            {
                //String s = (String)ViewState["Text"];
                //return ((s == null)? "[" + this.ID + "]" : s);
                return text;
            }
 
            set
            {
                //ViewState["Text"] = value;
                text = value;
            }
        }


        [Category("Behavior")]
        [DefaultValue(false)]
        public string ResourceSet
        {
            get
            {
                //string res = "";
                //if (ViewState["ResourceSet"] != null)
                //    res = (string)ViewState["ResourceSet"];
                return resourceSet;
            }

            set
            {
                //ViewState["ResourceSet"] = value;
                resourceSet = value;
            }
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        public string ResourceId
        {
            get
            {
                return resourceId;
            }

            set
            {
                resourceId = value;
            }
        }

        //[Category("Behavior")]
        //[DefaultValue(false)]
        //public bool Translations
        //{
        //    get { return translations; }
        //    set { translations = value; }
        //}

        [Category("Behavior")]
        [DefaultValue(ContentEditorProvider.Configuration.EditorTypeEnum.Text)]
        public ContentEditorProvider.Configuration.EditorTypeEnum TextMode
        {
            get { return textMode; }
            set { textMode = value; }
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            writer.Write("");
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.Write("");
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            string res = this.Text;
            string resourceSet = this.ResourceSet;
            string resourceId = this.ResourceId;

            try
            {
                if (string.IsNullOrEmpty(resourceId) && this.ID != null)
                    resourceId = this.ID;

                if (this.Page is PigeonCms.Engine.BasePage)
                {
                    //engine page
                    var page = (PigeonCms.Engine.BasePage)this.Page;
                    
                    if (string.IsNullOrEmpty(resourceSet))
                        resourceSet = page.UniqueID;

                    res = page.GetLabel(resourceSet, resourceId, this.Text, this.TextMode);
                }
                else if (this.NamingContainer is PigeonCms.BaseModuleControl)
                {
                    //pigeoncms module
                    var module = (PigeonCms.BaseModuleControl)this.NamingContainer;
                    res = module.GetLabel(resourceId, this.Text, this.TextMode);
                }
            }
            catch (Exception ex)
            {
                Tracer.Log("PigeonCms.Controls.Label.RenderContents()>Label["
                    + resourceSet + "|" + resourceId + "=" + res + "] " 
                    + "ERR:" + ex.ToString(),
                    TracerItemType.Error);
            }

            writer.Write(res);
        }

    }

}
