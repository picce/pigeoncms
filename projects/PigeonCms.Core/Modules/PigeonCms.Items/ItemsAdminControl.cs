using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PigeonCms.Core.Helpers;
using System.Web.UI.WebControls;

namespace PigeonCms
{
    public class ItemsAdminControl : PigeonCms.ItemControl<Item, ItemsFilter>
    {

        int targetImagesUpload = 0;
        //images attached
        protected int TargetImagesUpload
        {
            get { return GetIntParam("TargetImagesUpload", targetImagesUpload); }
            set { targetImagesUpload = value; }
        }

        int targetFilesUpload = 0;
        //files attached
        protected int TargetFilesUpload
        {
            get { return GetIntParam("TargetFilesUpload", targetFilesUpload); }
            set { targetFilesUpload = value; }
        }

        int targetDocsUpload = 0;
        //any file uploaded or linked in item description
        protected int TargetDocsUpload
        {
            get { return GetIntParam("TargetDocsUpload", targetDocsUpload); }
            set { targetDocsUpload = value; }
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

        string docsUploadUrl = "";
        protected string DocsUploadUrl
        {
            get
            {
                if (string.IsNullOrEmpty(docsUploadUrl) && this.TargetDocsUpload > 0)
                {
                    var menuMan = new MenuManager();
                    var menuTarget = new PigeonCms.Menu();
                    menuTarget = menuMan.GetByKey(this.TargetDocsUpload);
                    docsUploadUrl = Utility.GetRoutedUrl(menuTarget);
                }
                return docsUploadUrl;
            }
        }

        //restrictions params
        #region params restrictions

        private bool showSecurity = true;
        public bool ShowSecurity
        {
            get { return GetBoolParam("ShowSecurity", showSecurity); }
        }

        private bool showFieldsPanel = true;
        public bool ShowFieldsPanel
        {
            get { return GetBoolParam("ShowFieldsPanel", showFieldsPanel); }
        }

        private bool showParamsPanel = true;
        public bool ShowParamsPanel
        {
            get { return GetBoolParam("ShowParamsPanel", showParamsPanel); }
        }

        private bool showAlias = true;
        public bool ShowAlias
        {
            get { return GetBoolParam("ShowAlias", showAlias); }
        }

        private bool showType = true;
        public bool ShowType
        {
            get { return GetBoolParam("ShowType", showType); }
        }

        private bool showSectionColumn = true;
        public bool ShowSectionColumn
        {
            get { return GetBoolParam("ShowSectionColumn", showSectionColumn); }
        }

        private bool showEnabledFilter = true;
        public bool ShowEnabledFilter
        {
            get { return GetBoolParam("ShowEnabledFilter", showEnabledFilter); }
        }

        private bool showDates = true;
        public bool ShowDates
        {
            get { return GetBoolParam("ShowDates", showDates); }
        }

        private bool showItemsCount = true;
        public bool ShowItemsCount
        {
            get { return GetBoolParam("ShowItemsCount", showItemsCount); }
        }

        private bool allowOrdering = true;
        public bool AllowOrdering
        {
            get { return GetBoolParam("AllowOrdering", allowOrdering); }
        }

        private bool allowDelete = true;
        public bool AllowDelete
        {
            get { return GetBoolParam("AllowDelete", allowDelete); }
        }

        /*private bool showThread = false;
        public bool ShowThread
        {
            get { return GetBoolParam("ShowThread", showThread); }
        }*/

        int htmlEditorType = 0;
        protected ContentEditorProvider.Configuration.EditorTypeEnum HtmlEditorType
        {
            get
            {
                ContentEditorProvider.Configuration.EditorTypeEnum res;
                htmlEditorType = GetIntParam("HtmlEditorType", htmlEditorType);
                res = (ContentEditorProvider.Configuration.EditorTypeEnum)Enum.Parse(
                    typeof(ContentEditorProvider.Configuration.EditorTypeEnum),
                    htmlEditorType.ToString(), true);
                return res;
            }
        }

        private bool showEditorFileButton = true;
        public bool ShowEditorFileButton
        {
            get { return GetBoolParam("ShowEditorFileButton", showEditorFileButton); }
        }

        private bool showEditorPageBreakButton = true;
        public bool ShowEditorPageBreakButton
        {
            get { return GetBoolParam("ShowEditorPageBreakButton", showEditorPageBreakButton); }
        }

        private bool showEditorReadMoreButton = true;
        public bool ShowEditorReadMoreButton
        {
            get { return GetBoolParam("ShowEditorReadMoreButton", showEditorReadMoreButton); }
        }

        private bool showOnlyDefaultCulture = false;
        public bool ShowOnlyDefaultCulture
        {
            get { return GetBoolParam("ShowOnlyDefaultCulture", showOnlyDefaultCulture); }
        }

        #endregion

        #region filters

		//##20141205
		//filter per category
		private int categoryId = 0;
		public int CategoryId
		{
			get { return GetIntParam("CategoryId", categoryId, "categoryId"); }
			set { categoryId = value; }
		}

        private string itemType = "";
        public string ItemType
        {
            get { return GetStringParam("ItemType", itemType); }
            set { itemType = value; }
        }

        #endregion

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

        protected int LastSelectedCategoryId
        {
            get
            {
                var res = 0;
                var session = new SessionManager<int>("PigeonCms.ItemsAdminControl");
                if (!session.IsEmpty("LastSelectedCategoryId"))
                    res = session.GetValue("LastSelectedCategoryId");
                return res;
            }
            set
            {
                var session = new SessionManager<int>("PigeonCms.ItemsAdminControl");
                session.Insert("LastSelectedCategoryId", value);
            }
        }

        ContentEditorProvider.Configuration contentEditorConfig = null;
        protected ContentEditorProvider.Configuration ContentEditorConfig
        {
            get
            {
                if (contentEditorConfig == null)
                {
                    contentEditorConfig = new ContentEditorProvider.Configuration();
                    contentEditorConfig.FilesUploadUrl = this.DocsUploadUrl;
                    contentEditorConfig.EditorType = this.HtmlEditorType;
                    contentEditorConfig.FileButton = this.ShowEditorFileButton;
                    contentEditorConfig.PageBreakButton = this.ShowEditorPageBreakButton;
                    contentEditorConfig.ReadMoreButton = this.ShowEditorReadMoreButton;
                }
                return contentEditorConfig;
            }

        }


        public void GetTransText(string panelPrefix, Panel panel,
            Dictionary<string, string> translations,
            KeyValuePair<string, string> cultureItem)
        {
            TextBox t1 = new TextBox();
            t1 = (TextBox)panel.FindControl(panelPrefix + cultureItem.Value);
            translations.Add(cultureItem.Key, t1.Text);
        }


        public void SetTransText(string panelPrefix, Panel panel,
            Dictionary<string, string> translations,
            KeyValuePair<string, string> cultureItem)
        {
            string res = "";
            TextBox t1 = new TextBox();
            t1 = (TextBox)panel.FindControl(panelPrefix + cultureItem.Value);
            if (translations != null)
                translations.TryGetValue(cultureItem.Key, out res);
            t1.Text = res;
        }

        public void AddTransText(string panelPrefix, Panel panel,
            ContentEditorProvider.Configuration editorConfig,
            KeyValuePair<string, string> cultureItem, int maxLen, string cssClass)
        {
            var txt = new TextBox();
            txt.ID = panelPrefix + cultureItem.Value;
            txt.MaxLength = maxLen;
            txt.CssClass = cssClass;
            txt.ToolTip = cultureItem.Key;
            LabelsProvider.SetLocalizedControlVisibility(this.ShowOnlyDefaultCulture, cultureItem.Key, txt);
            var group = new Panel();
            group.CssClass = "form-group input-group";
            group.Controls.Add(txt);

            Literal lit = new Literal();
            if (!this.ShowOnlyDefaultCulture)
                lit.Text = "<div class=\"input-group-addon\"><span>" + cultureItem.Value.Substring(0, 3) + "</span></div>";
            group.Controls.Add(lit);
            panel.Controls.Add(group);
        }

    }
}
