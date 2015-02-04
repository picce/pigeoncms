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
using System.Web.Routing;


namespace PigeonCms
{
    public class CategoriesControl: PigeonCms.BaseModuleControl
    {
        private PigeonCms.Menu menuTarget = null;


        #region public fields

        private int sectionId = 0;
        public int SectionId
        {
            get
            {
                return GetIntParam("SectionId", sectionId);
            }
            set { sectionId = value; }
        }

        private int itemsListTarget = 0;
        public int ItemsListTarget
        {
            get { return GetIntParam("ItemsListTarget", itemsListTarget); }
            set { itemsListTarget = value; }
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

        public List<Category> CategoriesList
        {
            get
            {
                var res = new List<Category>();
                var filter = new CategoriesFilter();
                filter.Enabled = Utility.TristateBool.True;
                filter.SectionId = this.SectionId;
                res = new CategoriesManager().GetByFilter(filter, "");
                foreach (var item in res)
                {
                    if (!this.ShowImages)
                        item.Images.Clear();
                    if (!this.ShowDescription)
                        item.DescriptionTranslations.Clear();
                }
                return res;
            }
        }

        #endregion

        protected string GetLinkAddress(Category item)
        {
            string res = "javascript:void(0);";

            if (this.ItemsListTarget > 0)
            {
                if (menuTarget == null)
                {
                    menuTarget = new MenuManager().GetByKey(this.ItemsListTarget);
                }

                try
                {
                    if (menuTarget.RoutePattern.Contains("categoryname"))
                    {
                        string name = item.Alias;
                        if (string.IsNullOrEmpty(name))
                            item.TitleTranslations.TryGetValue(Config.CultureDefault, out name);
                        res = Utility.GetRoutedUrl(
                        menuTarget, new RouteValueDictionary { { "categoryname", name } }, "", Config.AddPageSuffix);
                    }
                    else
                    {
                        res = Utility.GetRoutedUrl(
                        menuTarget, new RouteValueDictionary { { "categoryid", item.Id } }, "", Config.AddPageSuffix);
                    }
                }
                catch (Exception ex)
                {
                    Tracer.Log("GetLinkAddress(): " + ex.ToString(), TracerItemType.Error);
                }
            }
            return res;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}