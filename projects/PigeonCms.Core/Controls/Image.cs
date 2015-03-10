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
using System.Text.RegularExpressions;

[assembly: TagPrefix("PigeonCms.Controls", "pgn")]
namespace PigeonCms.Controls
{
    [DefaultProperty("SrcAttr")]
    [ParseChildren(true, "Content")]//associate InnerHtml to Text prop
    [ToolboxData("<{0}:Image1 runat=server></{0}:Image1>")]
    public class Image : WebControl
    {
        private string content = "";
        private string srcAttr = "";
        private string src = "";
        private string resourceSet = "";
        private string resourceId = "";

        protected override void AddedControl(Control control, int index)
        {
        }

        [Category("Appearance")]
        [DefaultValue("")]
        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        /// <summary>
        /// source attribute
        /// default: src
        /// examples: src|data-image|url()
        /// </summary>
        [Category("Appearance")]
        [DefaultValue("")]
        public string SrcAttr
        {
            get 
            {
                if (string.IsNullOrEmpty(srcAttr))
                    srcAttr = "src";
                return srcAttr; 
            }
            set { srcAttr = value; }
        }

        /// <summary>
        /// current image files source
        /// </summary>
        [Category("Appearance")]
        [DefaultValue("")]
        public string Src
        {
            get { return src; }
        }


        [Category("Behavior")]
        [DefaultValue(false)]
        public string ResourceSet
        {
            get { return resourceSet; }
            set { resourceSet = value; }
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        public string ResourceId
        {
            get { return resourceId; }
            set { resourceId = value; }
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            writer.Write("");
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.Write("");
        }

        /// <summary>
        /// group[1] attribute name
        /// group[2] first delimiter
        /// group[3] attribute value
        /// group[4] second delimiter
        /// </summary>
        /// <param name="srcAttr">images source attr (ex. src|url|etc..)</param>
        /// <returns>the regex pattern</returns>
        private string getRegexPattern(string srcAttr)
        {
            string pattern = @"(ATTR=|ATTR)(""|'|[(])([^'"")]+)(""|'|[)])"
                .Replace("ATTR", srcAttr);

            return pattern;
        }

        private string getSrcValue(string content, string srcAttr)
        {
            string res = "";
            string pattern = getRegexPattern(srcAttr);

            foreach (Match match in Regex.Matches(content, pattern))
            {
                res = match.Groups[3].Value;
                break;
            }
            return res;
        }

        private string replaceSrc(string content, string srcAttr, string newSrc)
        {
            Regex rgx = new Regex(getRegexPattern(srcAttr));

            string res = rgx.Replace(content,
                delegate(Match match)
                {
                    string m = match.Groups[1].Value    /*attribute name*/
                        + match.Groups[2].Value         /*first separator*/
                        + newSrc                        /*content*/
                        + match.Groups[4].Value;        /*second separator*/
                    return m;
                });

            return res;
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            string res = this.Content;
            string resourceSet = this.ResourceSet;
            string resourceId = this.ResourceId;

            string defaultSrc = "";
            string newSrc = "";

            try
            {
                //default src of server control (on first time call)
                defaultSrc = getSrcValue(this.Content, this.SrcAttr);
                this.src = defaultSrc;

                if (string.IsNullOrEmpty(resourceId) && this.ID != null)
                    resourceId = this.ID;

                if (this.Page is PigeonCms.Engine.BasePage)
                {
                    //engine page
                    var page = (PigeonCms.Engine.BasePage)this.Page;
                    
                    if (string.IsNullOrEmpty(resourceSet))
                        resourceSet = page.UniqueID;

                    newSrc = page.GetLabel(resourceSet, resourceId, defaultSrc);
                }
                else if (this.NamingContainer is PigeonCms.BaseModuleControl)
                {
                    //pigeoncms module
                    var module = (PigeonCms.BaseModuleControl)this.NamingContainer;
                    newSrc = module.GetLabel(resourceId, defaultSrc);
                }

                if (!string.IsNullOrEmpty(newSrc))
                {
                    //replace src value in content html
                    res = replaceSrc(this.Content, this.SrcAttr, newSrc);
                    this.src = newSrc;
                }
            }
            catch (Exception ex)
            {
                Tracer.Log("PigeonCms.Controls.Image.RenderContents()>Image["
                    + resourceSet + "|" + resourceId + "=" + newSrc + "] " 
                    + "ERR:" + ex.ToString(),
                    TracerItemType.Error);
            }

            writer.Write(res);
        }

    }

}
