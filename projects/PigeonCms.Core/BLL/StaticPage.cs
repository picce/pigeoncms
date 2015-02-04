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
using System.Threading;

namespace PigeonCms
{
    public class StaticPage: ITable
    {
        private string pageName = "";
        private bool visible = true;
        private bool showPageTitle = true;
        private Dictionary<string, string> pageTitleTranslations = new Dictionary<string, string>();
        private Dictionary<string, string> pageContentTranslations = new Dictionary<string, string>();

        public StaticPage()
        {
        }

        /// <summary>
        /// Name or achronim of the Page used as primary key
        /// </summary>
        [DataObjectField(true)]
        public string PageName
        {
            [DebuggerStepThrough()]
            get { return pageName; }
            [DebuggerStepThrough()]
            set { pageName = value; }
        }

        /// <summary>
        /// Title of the static page in current culture
        /// </summary>
        [DataObjectField(false)]
        public string PageTitle
        {
            get 
            {
                string pageTitle = "";
                PageTitleTranslations.TryGetValue(Thread.CurrentThread.CurrentCulture.Name, out pageTitle);
                if (Utility.IsEmptyFckField(pageTitle))
                    PageTitleTranslations.TryGetValue(Config.CultureDefault, out pageTitle);
                return pageTitle;
            }
        }

        /// <summary>
        /// html content of the static page in current culture
        /// </summary>
        [DataObjectField(false)]
        public string PageContent
        {
            get 
            {
                string pageContent = "";
                PageContentTranslations.TryGetValue(Thread.CurrentThread.CurrentCulture.Name, out pageContent);
                if (Utility.IsEmptyFckField(pageContent))
                    PageContentTranslations.TryGetValue(Config.CultureDefault, out pageContent);
                return pageContent;
            }
        }

        /// <summary>
        /// content parsed
        /// </summary>
        [DataObjectField(false)]
        public string PageContentParsed
        {
            get
            {
                string res = this.PageContent;
                res = Utility.Html.FillPlaceholders(res);
                return res;
            }
        }
        

        /// <summary>
        /// Show or not the the content
        /// </summary>
        public bool Visible
        {
            [DebuggerStepThrough()]
            get { return visible; }
            [DebuggerStepThrough()]
            set { visible = value; }
        }

        /// <summary>
        /// Show or not the page title
        /// </summary>
        public bool ShowPageTitle
        {
            [DebuggerStepThrough()]
            get { return showPageTitle; }
            [DebuggerStepThrough()]
            set { showPageTitle = value; }
        }

        /// <summary>
        /// Title of the static page in different culture
        /// </summary>
        [DataObjectField(false)]
        public Dictionary<string, string> PageTitleTranslations
        {
            [DebuggerStepThrough()]
            get { return pageTitleTranslations; }
            [DebuggerStepThrough()]
            set { pageTitleTranslations = value; }
        }

        /// <summary>
        /// Content of the static page in different culture
        /// </summary>
        [DataObjectField(false)]
        public Dictionary<string, string> PageContentTranslations
        {
            [DebuggerStepThrough()]
            get { return pageContentTranslations; }
            [DebuggerStepThrough()]
            set { pageContentTranslations = value; }
        }

        public bool IsPageTitleTranslated
        {
            get
            {
                bool res = true;
                string val = "";
                PageTitleTranslations.TryGetValue(Thread.CurrentThread.CurrentCulture.Name, out val);
                if (Utility.IsEmptyFckField(val))
                    res = false;
                return res;
            }
        }

        public bool IsPageContentTranslated
        {
            get
            {
                bool res = true;
                string val = "";
                PageContentTranslations.TryGetValue(Thread.CurrentThread.CurrentCulture.Name, out val);
                if (Utility.IsEmptyFckField(val))
                    res = false;
                return res;
            }
        }
    }

    /// <summary>
    /// Filter used in search
    /// </summary>
    /// <remarks></remarks>
    [Serializable]
    public class StaticPageFilter
    {
        #region fields definition

        private string pageName = "";
        private Utility.TristateBool visible = Utility.TristateBool.NotSet;

        public string PageName
        {
            [DebuggerStepThrough()]
            get { return pageName; }
            [DebuggerStepThrough()]
            set { pageName = value; }
        }

        public Utility.TristateBool Visible
        {
            [DebuggerStepThrough()]
            get { return visible; }
            [DebuggerStepThrough()]
            set { visible = value; }
        }

        #endregion

    }

}