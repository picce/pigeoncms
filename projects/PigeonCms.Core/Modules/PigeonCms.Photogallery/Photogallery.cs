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
using System.Data.Common;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Threading;
using PigeonCms;


namespace PigeonCms
{
    [DataObject()]
    public class Photogallery: FilesGallery
    {
        const string defaultPath = "~/Public/Gallery/";
        /// <summary>
        /// default virtual path: "~Public/Gallery"
        /// </summary>
        [DebuggerStepThrough()]
        public Photogallery()
            : base(defaultPath, "")
        { }

        [DebuggerStepThrough()]
        public Photogallery(string virtualPath, string folderName)
        {
            if (string.IsNullOrEmpty(virtualPath))
                virtualPath = defaultPath;

            base.VirtualPath = virtualPath;
            base.FolderName = folderName;
            base.SearchPattern = "*.jpg";
        }
    }
}