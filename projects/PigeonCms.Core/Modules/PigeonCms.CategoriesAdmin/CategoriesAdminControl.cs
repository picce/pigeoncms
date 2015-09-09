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
using System.Collections.Generic;
using System.Threading;
using PigeonCms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using PigeonCms.Core.Helpers;


namespace PigeonCms.Modules
{
    public class CategoriesAdminControl : PigeonCms.BaseModuleControl
    {
        public enum NodeClickCommandEnum
        {
            Select = 0, 
            Edit, 
            Enabled, 
            MoveUp, 
            MoveDown, 
            Delete,
            Custom
        }

        public class NodeClickEventArgs : EventArgs
        {
            private NodeClickCommandEnum command = 0;
            public NodeClickCommandEnum Command
            {
                get { return command; }
            }

            private int categoryId = 0;
            public int CategoryId
            {
                get { return categoryId; }
            }

            private string customCommand = "";
            public string CustomCommand
            {
                get { return customCommand; }
            }

            public NodeClickEventArgs(NodeClickCommandEnum command, int categoryId, string customCommand = "")
            {
                this.command = command;
                this.categoryId = categoryId;
                this.customCommand = customCommand;
            }
        }

        public delegate void NodeClickDelegate(object sender, NodeClickEventArgs e);
        public event NodeClickDelegate NodeClick; 


        #region public fields

        private int sectionId = 0;
        public int SectionId
        {
            get { return GetIntParam("SectionId", sectionId, "sectionid"); }
            set { sectionId = value; }
        }

        int targetImagesUpload = 0;
        public int TargetImagesUpload
        {
            get { return GetIntParam("TargetImagesUpload", targetImagesUpload); }
            set { targetImagesUpload = value; }
        }

        int targetFilesUpload = 0;
        public int TargetFilesUpload
        {
            get { return GetIntParam("TargetFilesUpload", targetFilesUpload); }
            set { targetFilesUpload = value; }
        }

        string imagesUploadUrl = "";
        protected string ImagesUploadUrl
        {
            get
            {
                if (string.IsNullOrEmpty(imagesUploadUrl) && this.TargetImagesUpload > 0)
                {
                    var menuMan = new MenuManager();
                    var menuTarget = new PigeonCms.Menu();
                    menuTarget = menuMan.GetByKey(this.TargetImagesUpload);
                    imagesUploadUrl = Utility.GetRoutedUrl(menuTarget);
                }
                return imagesUploadUrl;
            }
        }

        string filesUploadUrl = "";
        protected string FilesUploadUrl
        {
            get
            {
                if (string.IsNullOrEmpty(filesUploadUrl) && this.TargetFilesUpload > 0)
                {
                    var menuMan = new MenuManager();
                    var menuTarget = new PigeonCms.Menu();
                    menuTarget = menuMan.GetByKey(this.TargetFilesUpload);
                    filesUploadUrl = Utility.GetRoutedUrl(menuTarget);
                }
                return filesUploadUrl;
            }
        }

        //restrictions params
        #region params restrictions

        private bool showSecurity = true;
        public bool ShowSecurity
        {
            get { return GetBoolParam("ShowSecurity", showSecurity); }
            set { showSecurity = value; }
        }

        private bool showOnlyDefaultCulture = false;
        public bool ShowOnlyDefaultCulture
        {
            get { return GetBoolParam("ShowOnlyDefaultCulture", showOnlyDefaultCulture); }
            set { showOnlyDefaultCulture = value; }
        }

        private bool showItemsCount = true;
        public bool ShowItemsCount
        {
            get { return GetBoolParam("ShowItemsCount", showItemsCount); }
            set { showItemsCount = value; }
        }

        private bool allowOrdering = true;
        public bool AllowOrdering
        {
            get { return GetBoolParam("AllowOrdering", allowOrdering); }
            set { allowOrdering = value; }
        }

        private bool allowEdit = true;
        public bool AllowEdit
        {
            get { return GetBoolParam("AllowEdit", allowEdit); }
            set { allowEdit = value; }
        }

        private bool allowDelete = true;
        public bool AllowDelete
        {
            get { return GetBoolParam("AllowDelete", allowDelete); }
            set { allowDelete = value; }
        }

        private bool allowNew = true;
        public bool AllowNew
        {
            get { return GetBoolParam("AllowNew", allowNew); }
            set { allowNew = value; }
        }

        private bool allowSelection = true;
        public bool AllowSelection
        {
            get { return GetBoolParam("AllowSelection", allowSelection); }
            set { allowSelection = value; }
        }

        #endregion

        /// <summary>
        /// the same as ItemsAdminControl. keep synched selected section id
        /// </summary>
        protected int LastSelectedSectionId
        {
            get
            {
                var res = 0;
                var session = new SessionManager<int>("PigeonCms.ItemsAdminControl");
                if (!session.IsEmpty("LastSelectedSectionId"))
                    res = session.GetValue("LastSelectedSectionId");
                return res;
            }
            set
            {
                var session = new SessionManager<int>("PigeonCms.ItemsAdminControl");
                session.Insert("LastSelectedSectionId", value);
            }
        }


        #endregion

        public void NodeCommand(NodeClickCommandEnum command, int categoryId, string customCommand = "")
        {
            if (this.NodeClick != null)
            {
                var args = new NodeClickEventArgs(command, categoryId, customCommand);
                this.NodeClick(this, args);
            }
        }


    }
}