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
using System.Web.Routing;


namespace PigeonCms
{
    public class ItemsControl<T, F>: PigeonCms.BaseModuleControl
        where T: Item, new()
        where F: ItemsFilter, new()
    {
        private PigeonCms.Menu menuTarget = null;

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

        private string sourceType = "category";
        public string SourceType
        {
            get { return GetStringParam("SourceType", sourceType); }
            set { sourceType = value; }
        }

        private int sectionId = 0;
        public int SectionId
        {
            get { return GetIntParam("SectionId", sectionId, "section"); }
            set { sectionId = value; }
        }

        private int categoryId = 0;
        public int CategoryId
        {
            get { return GetIntParam("CategoryId", categoryId, "categoryid"); }
            set { categoryId = value; }
        }

        private string categoryName = "";
        /// <summary>
        /// search by category alias
        /// </summary>
        public string CategoryName
        {
            get { return GetStringParam("CategoryName", categoryName, "categoryname"); }
            set { categoryName = value; }
        }

        private string searchString = "";
        public string SearchString
        {
            get { return GetStringParam("SearchString", searchString, "search"); }
            set { searchString = value; }
        }

        private int numOfItems = 0;
        public int NumOfItems
        {
            get { return GetIntParam("NumOfItems", numOfItems); }
            set { numOfItems = value; }
        }

        private int repeatColumns = 0;
        public int RepeatColumns
        {
            get { return GetIntParam("RepeatColumns", repeatColumns); }
            set { repeatColumns = value; }
        }

        private bool allowSearch = false;
        /// <summary>
        /// allow to filter results depending on query search params
        /// </summary>
        public bool AllowSearch
        {
            get { return GetBoolParam("AllowSearch", allowSearch); }
            set { allowSearch = value; }
        }

        private int minSearchChars = 0;
        public int MinSearchChars
        {
            get { return GetIntParam("MinSearchChars", minSearchChars); }
            set { minSearchChars = value; }
        }

        private bool showImages = false;
        public bool ShowImages
        {
            get { return GetBoolParam("ShowImages", showImages); }
            set { showImages = value; }
        }

        private string previewSize = "s";
        public string PreviewSize
        {
            get { return GetStringParam("PreviewSize", previewSize); }
            set { previewSize = value; }
        }

        private int customWidth = 0;
        public int CustomWidth
        {
            get { return GetIntParam("CustomWidth", customWidth); }
            set { customWidth = value; }
        }

        private int customHeight = 0;
        public int CustomHeight
        {
            get { return GetIntParam("CustomHeight", customHeight); }
            set { customHeight = value; }
        }

        private bool showFiles = false;
        public bool ShowFiles
        {
            get { return GetBoolParam("ShowFiles", showFiles); }
            set { showFiles = value; }
        }

        bool showDescription = false;
        public bool ShowDescription
        {
            get { return GetBoolParam("ShowDescription", showDescription); }
            set { showDescription = value; }
        }

        private int shortDescLen = 0;
        public int ShortDescLen
        {
            get { return GetIntParam("ShortDescLen", shortDescLen); }
            set { shortDescLen = value; }
        }

        private int itemTarget = 0;
        public int ItemTarget
        {
            get { return GetIntParam("ItemTarget", itemTarget); }
            set { itemTarget = value; }
        }

        private string headerText = "";
        public string HeaderText
        {
            get { return GetStringParam("HeaderText", headerText); }
            set { headerText = value; }
        }

        private string footerText = "";
        public string FooterText
        {
            get { return GetStringParam("FooterText", footerText); }
            set { footerText = value; }
        }

        private string sortDirection = "ASC";
        public string SortDirection
        {
            get { return GetStringParam("SortDirection", sortDirection); }
            set { sortDirection = value; }
        }

        private string sortParam = "t.Ordering";
        public string SortParam
        {
            get { return GetStringParam("SortParam", sortParam); }
            set { sortParam = value; }
        }

        private List<T> itemsList = null;
        public virtual List<T> ItemsList
        {
            get
            {
                if (itemsList == null)
                {
                    string sort = this.SortParam + " " + this.SortDirection;
                    itemsList = new List<T>();
                    var filter = new F();
                    filter.Enabled = Utility.TristateBool.True;
                    filter.IsValidItem = Utility.TristateBool.True;
                    filter.NumOfRecords = this.NumOfItems;
                    if (this.AllowSearch)
                    {
                        if (this.SearchString.Length < this.MinSearchChars)
                            filter.Id = -1;
                        filter.FullSearch = this.SearchString;
                    }

                    if (this.SourceType.ToLower() == "section")
                        filter.SectionId = this.SectionId;
                    else
                    {
                        if (this.CategoryId > 0)
                            filter.CategoryId = this.CategoryId;
                        else if (!string.IsNullOrEmpty(this.CategoryName))
                            filter.CategoryId = new CategoriesManager().GetByAlias(this.CategoryName).Id;
                    }

                    itemsList = new ItemsManager<T, F>(true, false).GetByFilter(filter, sort);
                    foreach (var item in itemsList)
                    {
                        if (!this.ShowImages)
                            item.Images.Clear();
                        if (!this.ShowFiles)
                            item.Files.Clear();
                        if (!this.ShowDescription)
                            item.DescriptionTranslations.Clear();
                    }
                }
                return itemsList;
            }
        }

        #endregion

        protected string GetShortDescription(T item)
        {
            string res = ""; 
            res = Utility.Html.GetTextIntro(item.Description);
            if (string.IsNullOrEmpty(res))
            {
                //obsolete way
                int len = this.ShortDescLen;
                res = Utility.Html.GetTextPreview(item.DescriptionParsed, len, "");
            }
            return res;
        }

        protected string GetLinkAddress(T item)
        {
            string res = "javascript:void(0);";

            if (this.ItemTarget > 0)
            {
                if (menuTarget == null)
                {
                    menuTarget = new MenuManager().GetByKey(this.ItemTarget);
                }

                try
                {
                    if (menuTarget.RoutePattern.Contains("itemname"))
                    {
                        string itemname = item.Alias;
                        if (string.IsNullOrEmpty(itemname))
                            item.TitleTranslations.TryGetValue(Config.CultureDefault, out itemname);
                        res = Utility.GetRoutedUrl(
                        menuTarget, new RouteValueDictionary { { "itemname", itemname } }, "", Config.AddPageSuffix);
                    }
                    else
                    {
                        res = Utility.GetRoutedUrl(
                        menuTarget, new RouteValueDictionary { { "itemid", item.Id } }, "", Config.AddPageSuffix);
                    }
                }
                catch (Exception ex)
                {
                    Tracer.Log("GetLinkAddress(): " + ex.ToString(), TracerItemType.Error);
                }
            }
            return res;
        }
    }
}