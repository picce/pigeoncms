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
using PigeonCms;
using System.Collections.Generic;
using System.Threading;



namespace PigeonCms
{
    public class Category : 
        ITableWithOrdering, 
        ITableWithPermissions,
        ITableExternalId
    {
        const string imagesPath = "~/public/gallery/categories/";
        const string filesPath = "~/public/files/categories/";

        private int id = 0;
        private int sectionId = 0;
        private int parentId = 0;
        private bool enabled = true;
        private int ordering = 0;
        string defaultImageName = "";
        string cssClass = "";
        string extId = "";

        private Dictionary<string, string> titleTranslations = new Dictionary<string, string>();
        private Dictionary<string, string> descriptionTranslations = new Dictionary<string, string>();

        //read permissions
        MenuAccesstype readAccessType = MenuAccesstype.Public;
        private int readPermissionId = 0;
        List<string> readRolenames = new List<string>();
        private string readAccessCode = "";
        private int readAccessLevel = 0;

        //write permissions
        MenuAccesstype writeAccessType = MenuAccesstype.Public;
        private int writePermissionId = 0;
        List<string> writeRolenames = new List<string>();
        private string writeAccessCode = "";
        private int writeAccessLevel = 0;


        /// <summary>
        /// Automatic Id as PKey
        /// </summary>
        [DataObjectField(true)]
        public int Id
        {
            [DebuggerStepThrough()]
            get { return id; }
            [DebuggerStepThrough()]
            set { id = value; }
        }

        public int SectionId
        {
            [DebuggerStepThrough()]
            get { return sectionId; }
            [DebuggerStepThrough()]
            set { sectionId = value; }
        }

        public int ParentId
        {
            [DebuggerStepThrough()]
            get { return parentId; }
            [DebuggerStepThrough()]
            set { parentId = value; }
        }

        public bool Enabled
        {
            [DebuggerStepThrough()]
            get { return enabled; }
            [DebuggerStepThrough()]
            set { enabled = value; }
        }

        public int Ordering
        {
            [DebuggerStepThrough()]
            get { return ordering; }
            [DebuggerStepThrough()]
            set { ordering = value; }
        }

        public MenuAccesstype ReadAccessType
        {
            [DebuggerStepThrough()]
            get { return readAccessType; }
            [DebuggerStepThrough()]
            set { readAccessType = value; }
        }

        public int ReadPermissionId
        {
            [DebuggerStepThrough()]
            get { return readPermissionId; }
            [DebuggerStepThrough()]
            set { readPermissionId = value; }
        }

        /// <summary>
        /// roles allowed depending on this.PermissionId
        /// </summary>
        [DataObjectField(false)]
        public List<string> ReadRolenames
        {
            [DebuggerStepThrough()]
            get { return readRolenames; }
            [DebuggerStepThrough()]
            set { readRolenames = value; }
        }

        public string ReadAccessCode
        {
            [DebuggerStepThrough()]
            get { return readAccessCode; }
            [DebuggerStepThrough()]
            set { readAccessCode = value; }
        }

        public int ReadAccessLevel
        {
            [DebuggerStepThrough()]
            get { return readAccessLevel; }
            [DebuggerStepThrough()]
            set { readAccessLevel = value; }
        }

        public MenuAccesstype WriteAccessType
        {
            [DebuggerStepThrough()]
            get { return writeAccessType; }
            [DebuggerStepThrough()]
            set { writeAccessType = value; }
        }

        public int WritePermissionId
        {
            [DebuggerStepThrough()]
            get { return writePermissionId; }
            [DebuggerStepThrough()]
            set { writePermissionId = value; }
        }

        [DataObjectField(false)]
        public List<string> WriteRolenames
        {
            [DebuggerStepThrough()]
            get { return writeRolenames; }
            [DebuggerStepThrough()]
            set { writeRolenames = value; }
        }

        public string WriteAccessCode
        {
            [DebuggerStepThrough()]
            get { return writeAccessCode; }
            [DebuggerStepThrough()]
            set { writeAccessCode = value; }
        }

        public int WriteAccessLevel
        {
            [DebuggerStepThrough()]
            get { return writeAccessLevel; }
            [DebuggerStepThrough()]
            set { writeAccessLevel = value; }
        }

        //private string alias = "";
        ///// <summary>
        ///// item alias, used for url based categories
        ///// </summary>
        //[DataObjectField(false)]
        //public string Alias
        //{
        //    [DebuggerStepThrough()]
        //    get { return alias; }
        //    [DebuggerStepThrough()]
        //    set { alias = value; }
        //}

        /// <summary>
        /// Title in current culture
        /// </summary>
        [DataObjectField(false)]
        public string Title
        {
            get
            {
                string res = "";
                titleTranslations.TryGetValue(Thread.CurrentThread.CurrentCulture.Name, out res);
                if (string.IsNullOrEmpty(res))
                    titleTranslations.TryGetValue(Config.CultureDefault, out res);
                return res;
            }
        }

        /// <summary>
        /// Alias for the object in default culture
        /// </summary>
        [DataObjectField(false)]
        public string Alias
        {
            get
            {
                string res = "";
                titleTranslations.TryGetValue(Config.CultureDefault, out res);
                res = Utility.GetUrlAlias(res);
                return res;
            }
        }

        /// <summary>
        /// Title in different culture
        /// </summary>
        [DataObjectField(false)]
        public Dictionary<string, string> TitleTranslations
        {
            [DebuggerStepThrough()]
            get { return titleTranslations; }
            [DebuggerStepThrough()]
            set { titleTranslations = value; }
        }

        public bool IsTitleTranslated
        {
            get
            {
                bool res = true;
                string val = "";
                titleTranslations.TryGetValue(Thread.CurrentThread.CurrentCulture.Name, out val);
                if (string.IsNullOrEmpty(val))
                    res = false;
                return res;
            }
        }

        /// <summary>
        /// Description in current culture
        /// </summary>
        [DataObjectField(false)]
        public string Description
        {
            get
            {
                string res = "";
                descriptionTranslations.TryGetValue(Thread.CurrentThread.CurrentCulture.Name, out res);
                if (string.IsNullOrEmpty(res))
                    descriptionTranslations.TryGetValue(Config.CultureDefault, out res);
                return res;
            }
        }

        /// <summary>
        /// Title in different culture
        /// </summary>
        [DataObjectField(false)]
        public Dictionary<string, string> DescriptionTranslations
        {
            [DebuggerStepThrough()]
            get { return descriptionTranslations; }
            [DebuggerStepThrough()]
            set { descriptionTranslations = value; }
        }

        public bool IsDescriptionTranslated
        {
            get
            {
                bool res = true;
                string val = "";
                descriptionTranslations.TryGetValue(Thread.CurrentThread.CurrentCulture.Name, out val);
                if (string.IsNullOrEmpty(val))
                    res = false;
                return res;
            }
        }

        public string DefaultImageName
        {
            [DebuggerStepThrough()]
            get { return defaultImageName; }
            [DebuggerStepThrough()]
            set { defaultImageName = value; }
        }

        public string CssClass
        {
            [DebuggerStepThrough()]
            get { return cssClass; }
            [DebuggerStepThrough()]
            set { cssClass = value; }
        }

        /// <summary>
        /// external identifier to allow import/export from external datasource
        /// </summary>
        public string ExtId
        {
            [DebuggerStepThrough()]
            get { return extId; }
            [DebuggerStepThrough()]
            set { extId = value; }
        }


        private FileMetaInfo defaultImage = new FileMetaInfo();
        public FileMetaInfo DefaultImage
        {
            get
            {
                bool found = false;
                FileMetaInfo firstImg = new FileMetaInfo();
                int counter = 0;
                foreach (FileMetaInfo img in this.Images)
                {
                    if (counter == 0)
                        firstImg = img;
                    if (string.IsNullOrEmpty(defaultImageName))
                        break;
                    if (img.FileName == defaultImageName)
                    {
                        defaultImage = img;
                        found = true;
                        break;
                    }
                    counter++;
                }
                if (!found)
                    defaultImage = firstImg;
                return defaultImage;
            }
            [DebuggerStepThrough()]
            set { defaultImage = value; }
        }

        /// <summary>
        /// the number of images loaded
        /// </summary>
        public int NumOfImagesLoaded
        {
            get
            {
                return this.Images.Count;
            }
        }

        private List<FileMetaInfo> images = null;
        public List<FileMetaInfo> Images
        {
            get
            {
                if (images == null)
                {
                    images = new FilesGallery(imagesPath, this.Id.ToString(), "*.*").GetAll();
                }
                return images;
            }
        }

        public List<FileMetaInfo> ImagesNotDefault
        {
            get
            {
                List<FileMetaInfo> result = new List<FileMetaInfo>();
                foreach (FileMetaInfo item in this.Images)
                {
                    if (defaultImage.FileUrl != item.FileUrl)
                        result.Add(item);
                }
                return result;
            }
        }

        private List<FileMetaInfo> files = null;
        public List<FileMetaInfo> Files
        {
            get
            {
                if (files == null)
                {
                    files = new FilesGallery(filesPath, this.Id.ToString()).GetAll();
                }
                return files;
            }
        }

        private Section section = null;
        /// <summary>
        /// parent section of current category
        /// </summary>
        public Section Section
        {
            get
            {
                if (section == null)
                {
                    section = new Section();
                    if (this.SectionId > 0)
                        section = new SectionsManager().GetByKey(this.SectionId);
                }
                return section;
            }
        }

        private Category parent = null;
        /// <summary>
        /// parent category
        /// </summary>
        public Category Parent(CategoriesManager man)
        {
            if (parent == null)
            {
                parent = new Category();
                if (this.ParentId > 0)
                    parent = man.GetByKey(this.ParentId);
            }
            return parent;
        }

        private List<Category> childs = null;
        /// <summary>
        /// parent category
        /// </summary>
        public List<Category> Childs(CategoriesManager man)
        {
            if (childs == null)
            {
                var filter = new CategoriesFilter();
                filter.ParentId = this.Id;
                childs = man.GetByFilter(filter, "");
            }
            return childs;
        }

        #region public methods
        public Category(){}

        /// <summary>
        /// delete images folder content
        /// </summary>
        public void DeleteImages()
        {
            new FilesGallery(imagesPath, this.Id.ToString()).DeleteFolderContent();
            this.images = null;
        }

        /// <summary>
        /// delete files folder content
        /// </summary>
        public void DeleteFiles()
        {
            new FilesGallery(filesPath, this.Id.ToString()).DeleteFolderContent();
            this.files = null;
        }

        #endregion

    }


    [Serializable]
    public class CategoriesFilter : ITableExternalId
    {
        #region fields definition

        private int id = 0;
        private int sectionId = 0;
        private int parentId = -1;
        private int idToExpand = 0;
        private string alias = "";
        private Utility.TristateBool enabled = Utility.TristateBool.NotSet;
        private string extId = "";

        [DataObjectField(true)]
        public int Id
        {
            [DebuggerStepThrough()]
            get { return id; }
            [DebuggerStepThrough()]
            set { id = value; }
        }

        [DataObjectField(false)]
        public int SectionId
        {
            [DebuggerStepThrough()]
            get { return sectionId; }
            [DebuggerStepThrough()]
            set { sectionId = value; }
        }

        
        public int ParentId
        {
            [DebuggerStepThrough()]
            get { return parentId; }
            [DebuggerStepThrough()]
            set { parentId = value; }
        }

        /// <summary>
        /// category id to expand
        /// </summary>
        public int IdToExpand
        {
            [DebuggerStepThrough()]
            get { return idToExpand; }
            [DebuggerStepThrough()]
            set { idToExpand = value; }
        }

        [DataObjectField(false)]
        public string Alias
        {
            [DebuggerStepThrough()]
            get { return alias; }
            [DebuggerStepThrough()]
            set { alias = value; }
        }

        [DataObjectField(false)]
        public Utility.TristateBool Enabled
        {
            [DebuggerStepThrough()]
            get { return enabled; }
            [DebuggerStepThrough()]
            set { enabled = value; }
        }

        /// <summary>
        /// external identifier to allow import/export from external datasource
        /// </summary>
        [DataObjectField(false)]
        public string ExtId
        {
            [DebuggerStepThrough()]
            get { return extId; }
            [DebuggerStepThrough()]
            set { extId = value; }
        }

        #endregion
    }
}