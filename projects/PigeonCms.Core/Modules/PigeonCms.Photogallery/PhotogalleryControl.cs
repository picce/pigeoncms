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


namespace PigeonCms
{
    public class PhotogalleryControl: PigeonCms.BaseModuleControl
    {
        #region private fields
        private PhotogalleryControl.SourceTypeEnum sourceType = SourceTypeEnum.FolderSource;
        private bool showChildList = false;
        private string basePath = "";
        private string folderName = "";
        private int sectionId = 0;
        private int categoryId = 0;
        private int itemId = 0;
        private string layoutCssFile = "";
        private string headerText = "";
        private string footerText = "";
        private string errorText = "";
        List<FileMetaInfo> imagesList = new List<FileMetaInfo>();
        #endregion


        public struct LinksList
        {
            public int Id;
            public string Title;

            public LinksList(int id, string title)
            {
                this.Id = id;
                this.Title = title;
            }
        }

        public enum SourceTypeEnum
        {
            FolderSource = 0,
            SectionSource = 1,
            CategorySource = 2, 
            ItemSource = 3
        }

        #region public fields

        public PhotogalleryControl.SourceTypeEnum SourceType
        {
            get 
            {
                int res = 0;
                res = GetIntParam("SourceType", res);
                return (SourceTypeEnum)res;
            }
            set { sourceType = value; }
        }

        public bool StaticFilesTracking
        {
            get
            {
                int staticFilesTracking = (int)Utility.TristateBool.NotSet;
                bool res = false;
                staticFilesTracking = GetIntParam("StaticFilesTracking", staticFilesTracking);
                if ((Utility.TristateBool)staticFilesTracking == Utility.TristateBool.True)
                    res = true;
                else if ((Utility.TristateBool)staticFilesTracking == Utility.TristateBool.NotSet)
                {
                    bool.TryParse(AppSettingsManager.GetValue("StaticFilesTracking"), out res);
                }
                return res;
            }
        }

        public bool ShowChildList
        {
            get { return GetBoolParam("ShowChildList", showChildList); }
            set { showChildList = value; }
        }

        /// <summary>
        /// folder taht contains images
        /// </summary>
        public string BasePath
        {
            get { return GetStringParam("BasePath", basePath); }
            set { basePath = value; }
        }

        public string FolderName
        {
            get { return GetStringParam("FolderName", folderName); }
            set { folderName = value; }
        }

        public int SectionId
        {
            get { return GetIntParam("SectionId", sectionId); }
            set { sectionId = value; }
        }

        public int CategoryId
        {
            get { return GetIntParam("CategoryId", categoryId); }
            set { categoryId = value; }
        }

        public int ItemId
        {
            get { return GetIntParam("ItemId", itemId); }
            set { itemId = value; }
        }

        public string HeaderText
        {
            get { return GetStringParam("HeaderText", headerText); }
            set { headerText = value; }
        }

        public string FooterText
        {
            get { return GetStringParam("FooterText", footerText); }
            set { footerText = value; }
        }

        public string ErrorText
        {
            get { return GetStringParam("ErrorText", errorText); }
            set { errorText = value; }
        }

        public List<FileMetaInfo> ImagesList
        {
            get
            {
                switch (this.SourceType)
                {
                    case SourceTypeEnum.FolderSource:
                        imagesList = new Photogallery(this.BasePath, this.FolderName).GetAll();
                        break;

                    case SourceTypeEnum.SectionSource:
                        if (this.ShowChildList)
                            imagesList = new Photogallery("", "categories/" + this.CategoryId).GetAll();
                        else
                            imagesList = new Photogallery("", "sections/"+ this.SectionId).GetAll();
                        break;

                    case SourceTypeEnum.CategorySource:
                        if (this.ShowChildList)
                            imagesList = new Photogallery("", "items/" + this.ItemId).GetAll();
                        else
                            imagesList = new Photogallery("", "categories/" + this.CategoryId).GetAll();
                        break;

                    case SourceTypeEnum.ItemSource:
                        imagesList = new Photogallery("", "items/" + this.ItemId).GetAll();
                        break;
                    default:
                        break;
                }
                
                return imagesList;
            }
        }

        public List<LinksList> ChildList
        {
            get
            {
                List<LinksList> res = new List<LinksList>();
                if (this.ShowChildList)
                {
                    switch (this.SourceType)
                    {
                        case SourceTypeEnum.FolderSource:
                            break;
                        case SourceTypeEnum.SectionSource:
                            res = getCategoriesList();
                            break;
                        case SourceTypeEnum.CategorySource:
                            res = getItemsList();
                            break;
                        case SourceTypeEnum.ItemSource:
                            break;
                        default:
                            break;
                    }
                }
                return res;
            }
        }

        
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        { }

        private List<LinksList> getCategoriesList()
        {
            List<LinksList> res = new List<LinksList>();
            List<Category>list = new List<Category>();
            CategoriesFilter filter = new CategoriesFilter();

            filter.Enabled = Utility.TristateBool.True;
            filter.SectionId = this.SectionId;

            list = new CategoriesManager().GetByFilter(filter, "");
            foreach (Category item in list)
            {
                res.Add(new LinksList(item.Id, item.Title));
                //res.Add(item.Title);
            }
            return res;
        }

        private List<LinksList> getItemsList()
        {
            List<LinksList> res = new List<LinksList>();
            List<Item> list = new List<Item>();
            ItemsFilter filter = new ItemsFilter();

            filter.Enabled = Utility.TristateBool.True;
            filter.CategoryId = this.CategoryId;

            list = new ItemsManager<Item,ItemsFilter>().GetByFilter(filter, "");
            foreach (Item item in list)
            {
                res.Add(new LinksList(item.Id, item.Title));
                //res.Add(item.Title);
            }
            return res;
        }
    }
}