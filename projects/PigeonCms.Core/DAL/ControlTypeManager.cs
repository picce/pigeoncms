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
    /// DAL for custom controls
    /// </summary>
    public class ControlTypeManager
    {
        public string Path { get; set; }

        public ControlTypeManager()
        {
            this.Path = "~/Controls/";
        }

        /// <summary>
        /// determine if a control is installed
        /// </summary>
        /// <param name="fullName">control namespace and name</param>
        /// <returns></returns>
        public bool Exist(string fullName)
        {
            string filePath = HttpContext.Current.Request.MapPath(this.Path + fullName) + "\\install.xml";
            return System.IO.File.Exists(filePath);
        }

        public Dictionary<string, string> GetList()
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            try
            {
                var controls = new FilesGallery(this.Path, "", "*.ascx").GetAll();
                foreach (var item in controls)
                {   
                    string controlName = "PigeonCms." + item.FileNameNoExtension;
                    res.Add(controlName, controlName);
                }
            }
            finally
            {
            }
            return res;
        }
    }
}