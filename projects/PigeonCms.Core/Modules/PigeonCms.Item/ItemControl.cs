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
using System.Text;
using PigeonCms;
using PigeonCms.Core.Helpers;


namespace PigeonCms
{
    public class ItemControl<T, F>: PigeonCms.BaseModuleControl
        where T: Item, new()
        where F: ItemsFilter, new()
    {
        #region private fields
        private int itemId = 0;
        private string itemName = "";
        private bool showImages = true;
        private string previewSize = "s";
        private int customWidth = 0;
        private int customHeight = 0;
        private string headerText = "";
        private string footerText = "";
        #endregion

        #region public fields
        
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

        private int sectionId = 0;
        public int SectionId
        {
            get { return GetIntParam("SectionId", sectionId, "sectionid"); }
            set { sectionId = value; }
        }

        public int ItemId
        {
            get { return GetIntParam("ItemId", itemId, "itemid"); }
            set { itemId = value; }
        }

        public string ItemName
        {
            get { return GetStringParam("ItemName", itemName, "itemname"); }
            set { itemName = value; }
        }

        int currentPage = 0;
        /// <summary>
        /// in case of pagebreak
        /// </summary>
        public int CurrentPage
        {
            get
            {
                if (currentPage == 0)
                {
                    int res = 1;
                    if (!int.TryParse(Utility._QueryString("page"), out res))
                        res = 1;
                    if (res > this.CurrItem.DescriptionPages.Count)
                        res = 1;
                    if (res < 1)
                        res = 1;
                    currentPage = res;
                }
                return currentPage;
            }
        }

        private bool showFiles = false;
        public bool ShowFiles
        {
            get { return GetBoolParam("ShowFiles", showFiles); }
            set { showFiles = value; }
        }

        public bool ShowImages
        {
            get { return GetBoolParam("ShowImages", showImages); }
            set { showImages = value; }
        }

        public string PreviewSize
        {
            get { return GetStringParam("PreviewSize", previewSize); }
            set { previewSize = value; }
        }

        public int CustomWidth
        {
            get { return GetIntParam("CustomWidth", customWidth); }
            set { customWidth = value; }
        }

        public int CustomHeight
        {
            get { return GetIntParam("CustomHeight", customHeight); }
            set { customHeight = value; }
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

        private T currItem = null;
        /// <summary>
        /// current Item object
        /// </summary>
        public T CurrItem 
        {
            get
            {
                if (currItem == null)
                {
                    var filter = new F();
                    var man = new ItemsManager<T, F>(true, false);
                    var currItems = new List<T>();
                    var cache = new CacheManager<T>("PigeonCms.Item", true);

                    if (this.ItemId > 0)
                    {
                        cache.Remove(this.ItemId.ToString());//TODO see issue id:1 on codeplex-avoid read from cache

                        //try to catch by id
                        if (cache.IsEmpty(this.ItemId))
                        {
                            currItem = man.GetByKey(this.ItemId);
                            cache.Insert(this.ItemId, currItem);
                        }
                        else
                            currItem = cache.GetValue(this.ItemId);
                    }
                    else if (!string.IsNullOrEmpty(this.ItemName))
                    {
                        cache.Remove(this.ItemName);//TODO see issue id:1 on codeplex-avoid read from cache

                        //try to catch by alias
                        if (cache.IsEmpty(this.ItemName))
                        {
                            filter = new F();
                            filter.Alias = this.ItemName;
                            currItems = man.GetByFilter(filter, "");
                            if (currItems.Count > 0)
                                currItem = currItems[0];
                            else
                            {
                                //try to catch by default title
                                filter = new F();
                                filter.TitleSearch = this.ItemName;
                                currItems = man.GetByFilter(filter, "");
                                if (currItems.Count > 0)
                                    currItem = currItems[0];
                            }
                            cache.Insert(this.ItemName, currItem);
                        }
                        else
                            currItem = cache.GetValue(this.ItemName);
                    }

                    if (currItem == null)
                    {
                        currItem = new T();
                        currItem.TitleTranslations.Add(
                            Thread.CurrentThread.CurrentCulture.Name, "");
                        currItem.DescriptionTranslations.Add(
                            Thread.CurrentThread.CurrentCulture.Name, "");
                    }
                    else
                    {
                        if (!currItem.Enabled)
                            currItem = new T();
                        //edited on 20120124
                        if (this.SectionId > 0 && currItem.Category.SectionId != this.SectionId)
                            currItem = new T();
                    }

                    if (!this.ShowImages)
                        currItem.Images.Clear();
                    if (!this.ShowFiles)
                        currItem.Files.Clear();
                }
                return currItem;
            }
        }

        #endregion

        public void ForceCurrItemReload()
        {
            var cache = new CacheManager<T>("PigeonCms.Item", true);
            currItem = null;

            if (!cache.IsEmpty(this.ItemId))
                cache.Remove(this.ItemId.ToString());
            if (cache.IsEmpty(this.ItemName))
                cache.Remove(this.itemName);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}