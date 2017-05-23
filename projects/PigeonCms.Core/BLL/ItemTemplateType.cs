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
using System.Collections.Generic;
using System.IO;
using PigeonCms;
using System.Linq;

namespace PigeonCms.Core
{
    /// <summary>
    /// used to parse item templates
    /// </summary>
    public class ItemTemplateType: XmlType
    {
        public ItemTemplateType() { }


        public void IncludeJsFilesContent(Page page)
        {
            foreach (var file in this.Files.Where(t => t.Type == "js"))
            {
                string url = Config.ItemsPath + this.FullName + "/templates/" + file.File;
                string content = File.ReadAllText(HttpContext.Current.Server.MapPath(url));

                Utility.Script.RegisterStartupScript(page, $"template-{this.FullName}-{file.File}", content);
            }
        }

        public string GetCssFilesContent()
        {
            string res = ""; 
            foreach (var file in this.Files.Where(t => t.Type == "css"))
            {
                string url = Config.ItemsPath + this.FullName + "/templates/" + file.File;
                string content = File.ReadAllText(HttpContext.Current.Server.MapPath(url));

                res += $@"
                <style>
                /*init template css include {file.File}*/
                {content}
                </style>
                ";
            }
            return res;
        }
    }


    [Serializable]
    public class ItemTemplateTypeFilter : XmlTypeFilter { }
}