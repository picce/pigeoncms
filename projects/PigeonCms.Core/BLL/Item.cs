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
using System.Web.Compilation;
using System.Reflection;



namespace PigeonCms
{
    /// <summary>
    /// map inerithed class field to base class customizable field
    /// </summary>
    public class ItemFieldMapAttribute : System.Attribute
    {
        public enum CustomFields
        {
            CustomBool1,
            CustomBool2,
            CustomBool3,
            CustomBool4,
            CustomDate1,
            CustomDate2,
            CustomDate3,
            CustomDate4,
            CustomDecimal1,
            CustomDecimal2,
            CustomDecimal3,
            CustomDecimal4,
            CustomInt1,
            CustomInt2,
            CustomInt3,
            CustomInt4,
            CustomString1,
            CustomString2,
            CustomString3,
            CustomString4
            /*CustomTextTranslations1,
            CustomTextTranslations2,
            CustomTextTranslations3*/
        }

        public CustomFields FieldName { get; set; }

        public ItemFieldMapAttribute(CustomFields customFieldName)
        {
            this.FieldName = customFieldName;
        }
    }

    public class Item: ITableWithPermissions,
        ITableWithOrdering, ITableWithComments, ITableExternalId
    {
        const string DefaultItemType = "PigeonCms.Item";

        private string itemTypeName = "";
        private DateTime dateInserted;
        private string userInserted = "";
        private DateTime dateUpdated;
        private string userUpdated = "";
        string defaultImageName = "";
        private DateTime itemDate;
        private DateTime validFrom;
        private DateTime validTo;
        string cssClass = "";
        string extId = "";

        private Dictionary<string, string> titleTranslations = new Dictionary<string, string>();
        private Dictionary<string, string> descriptionTranslations = new Dictionary<string, string>();


        //custom fields
        private bool customBool1 = false;
        private bool customBool2 = false;
        private bool customBool3 = false;
        private bool customBool4 = false;
        private DateTime customDate1;
        private DateTime customDate2;
        private DateTime customDate3;
        private DateTime customDate4;
        private decimal customDecimal1 = 0.0m;
        private decimal customDecimal2 = 0.0m;
        private decimal customDecimal3 = 0.0m;
        private decimal customDecimal4 = 0.0m;
        private int customInt1 = 0;
        private int customInt2 = 0;
        private int customInt3 = 0;
        private int customInt4 = 0;
        private string customString1 = "";
        private string customString2 = "";
        private string customString3 = "";
        private string customString4 = "";
        //private Dictionary<string, string> customTextTranslations1 = new Dictionary<string, string>();
        //private Dictionary<string, string> customTextTranslations2 = new Dictionary<string, string>();
        //private Dictionary<string, string> customTextTranslations3 = new Dictionary<string, string>();
        private string itemParams = "";
        private int threadId = 0;

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


        #region fields

        public string ImagesPath
        {
            get { return "~/public/gallery/items/"; }
        }

        public virtual string FilesPath
        {
            get { return "~/public/files/items/"; }
        }

        /// <summary>
        /// Automatic Id as PKey
        /// </summary>
        [DataObjectField(true)]
        public int Id { get; set; }

        /// <summary>
        /// Item specific type name. Ex. PigeonCms.CustomItem
        /// </summary>
        [DataObjectField(false)]
        public string ItemTypeName
        {
            [DebuggerStepThrough()]
            get
            {
                if (!string.IsNullOrEmpty(itemTypeName))
                    return itemTypeName;
                else
                    return DefaultItemType;
            }
            [DebuggerStepThrough()]
            set { itemTypeName = value; }
        }

        public int CategoryId { get; set; }

        public int SectionId { get; set; }

        public bool Enabled { get; set; }

        public int Ordering { get; set; }

        public int CommentsGroupId { get; set; }


        /// <summary>
        /// record inserted date
        /// </summary>
        public DateTime DateInserted
        {
            [DebuggerStepThrough()]
            get { return dateInserted; }
            [DebuggerStepThrough()]
            set { dateInserted = value; }
        }

        /// <summary>
        /// record inserted user
        /// </summary>
        public string UserInserted
        {
            [DebuggerStepThrough()]
            get { return userInserted; }
            [DebuggerStepThrough()]
            set { userInserted = value; }
        }

        /// <summary>
        /// record updated date
        /// </summary>
        public DateTime DateUpdated
        {
            [DebuggerStepThrough()]
            get { return dateUpdated; }
            [DebuggerStepThrough()]
            set { dateUpdated = value; }
        }

        /// <summary>
        /// record updated user
        /// </summary>
        public string UserUpdated
        {
            [DebuggerStepThrough()]
            get { return userUpdated; }
            [DebuggerStepThrough()]
            set { userUpdated = value; }
        }

        /// <summary>
        /// item date
        /// </summary>
        public DateTime ItemDate
        {
            [DebuggerStepThrough()]
            get { return itemDate; }
            [DebuggerStepThrough()]
            set { itemDate = value; }
        }

        /// <summary>
        /// item init valid date
        /// </summary>
        public DateTime ValidFrom
        {
            [DebuggerStepThrough()]
            get { return validFrom; }
            [DebuggerStepThrough()]
            set { validFrom = value; }
        }

        /// <summary>
        /// item end valid date
        /// </summary>
        public DateTime ValidTo
        {
            [DebuggerStepThrough()]
            get { return validTo; }
            [DebuggerStepThrough()]
            set { validTo = value; }
        }

        public string CssClass
        {
            [DebuggerStepThrough()]
            get { return cssClass; }
            [DebuggerStepThrough()]
            set { cssClass = value; }
        }

        private string alias = "";
        /// <summary>
        /// item alias, used for url based items
        /// </summary>
        [DataObjectField(false)]
        public string Alias
        {
            [DebuggerStepThrough()]
            get { return alias; }
            [DebuggerStepThrough()]
            set { alias = value; }
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
                if (Utility.IsEmptyFckField(res))
                    titleTranslations.TryGetValue(Config.CultureDefault, out res);
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
                if (Utility.IsEmptyFckField(val))
                    res = false;
                return res;
            }
        }

        /// <summary>
        /// Raw Description in current culture
        /// </summary>
        [DataObjectField(false)]
        public string Description
        {
            get
            {
                string res = "";
                descriptionTranslations.TryGetValue(Thread.CurrentThread.CurrentCulture.Name, out res);
                if (Utility.IsEmptyFckField(res))
                    descriptionTranslations.TryGetValue(Config.CultureDefault, out res);
                if (res == null) res = "";
                return res;
            }
        }

        string descriptionParsed = null;
        /// <summary>
        /// Description parsed
        /// </summary>
        [DataObjectField(false)]
        public string DescriptionParsed
        {
            get
            {
                if (descriptionParsed == null)
                {
                    descriptionParsed = parseText(this.Description);
                }
                return descriptionParsed;
            }
        }

        //List<string> descriptionPages = null;
        /// <summary>
        /// Description paged if pagebreak is present,
        /// all desciption otherwise
        /// </summary>
        [DataObjectField(false)]
        public List<string> DescriptionPages
        {
            get
            {
                var res = new List<string>();
                res = Utility.String2List(this.Description, ContentEditorProvider.SystemPagebreakTag);
                for (int i = 0; i < res.Count; i++)
                {
                    res[i] = parseText(res[i]);
                }

                return res;
            }
        }

        /// <summary>
        /// Raw Description in different culture
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
                if (Utility.IsEmptyFckField(val))
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
                    if (this.Id > 0)
                        images = new FilesGallery(ImagesPath, this.Id.ToString(), "*.*").GetAll();
                    else
                        images = new List<FileMetaInfo>();
                }
                return images;
            }
        }

        /// <summary>
        /// images size in Bytes
        /// </summary>
        public long ImagesSize
        {
            get
            {
                return Utility.GetDirectorySize(
                    Path.Combine(ImagesPath, this.Id.ToString()));
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
                    if (this.Id > 0)
                        files = new FilesGallery(FilesPath, this.Id.ToString()).GetAll();
                    else
                        files = new List<FileMetaInfo>();
                }
                return files;
            }
        }

        /// <summary>
        /// files size in Bytes
        /// </summary>
        public long FilesSize
        {
            get
            {
                return Utility.GetDirectorySize(
                    Path.Combine(FilesPath, this.Id.ToString()));
            }
        }

        public ItemType ItemType
        {
            [DebuggerStepThrough()]
            get
            {
                return new ItemTypeManager().GetByFullName(this.ItemTypeName);
            }
        }

        /// <summary>
        /// inline serielized params for the item
        /// </summary>
        public string ItemParams
        {
            [DebuggerStepThrough()]
            get { return itemParams; }
            [DebuggerStepThrough()]
            set { itemParams = value; }
        }

        public Dictionary<string, string> Params
        {
            get
            {
                return Utility.GetParamsDictFromString(this.ItemParams);
            }
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

        /// <summary>
        /// thread id. For root items it is equal to Id
        /// </summary>
        public int ThreadId
        {
            [DebuggerStepThrough()]
            get { return threadId; }
            [DebuggerStepThrough()]
            set { threadId = value; }
        }

        private Category category = null;
        /// <summary>
        /// parent category of current item
        /// </summary>
        public Category Category
        {
            get
            {
                if (category == null)
                {
                    category = new Category();
                    if (this.CategoryId > 0)
                        category = new CategoriesManager().GetByKey(this.CategoryId);
                }
                return category;
            }
        }

        public bool IsThreadRoot
        {
            get { return this.Id == this.ThreadId; }
        }

        private Item threadRoot = null;
        public Item ThreadRoot
        {
            get
            {
                if (threadRoot == null)
                {
                    threadRoot = new Item();
                    if (!this.IsThreadRoot)
                        threadRoot = new ItemsManager<Item, ItemsFilter>().GetByKey(
                            this.ThreadId);
                    else
                        threadRoot = this;
                }
                return threadRoot;
            }
        }

        private List<Item> threadList = null;
        /// <summary>
        /// thread list
        /// </summary>
        public List<Item> ThreadList
        {
            get
            {
                if (threadList == null)
                {
                    threadList = new List<Item>();
                    if (this.ThreadId > 0 && this.IsThreadRoot)
                    {
                        var filter = new ItemsFilter();
                        filter.ThreadId = this.ThreadId;
                        filter.ShowOnlyRootItems = false;
                        threadList = new ItemsManager<Item, ItemsFilter>().GetByFilter(filter, "t.Id");
                    }
                }
                return threadList;
            }
        }

        Dictionary<int, string> formFields = null;
        /// <summary>
        /// form fields values linked to this item from db
        /// return pairs FormFieldId, Value
        /// </summary>
        [DataObjectField(false)]
        public Dictionary<int, string> FormFields
        {
            get
            {
                if (formFields == null)
                {
                    var man  = new ItemsManager<Item, ItemsFilter>();
                    formFields = man.GetFormFieldsDictionary(this.Id);
                }
                return formFields;
            }
            set { formFields = value; }
        }

        #endregion

        #region custom fields

        public bool CustomBool1
        {
            [DebuggerStepThrough()]
            get { return customBool1; }
            [DebuggerStepThrough()]
            set { customBool1 = value; }
        }

        public bool CustomBool2
        {
            [DebuggerStepThrough()]
            get { return customBool2; }
            [DebuggerStepThrough()]
            set { customBool2 = value; }
        }

        public bool CustomBool3
        {
            [DebuggerStepThrough()]
            get { return customBool3; }
            [DebuggerStepThrough()]
            set { customBool3 = value; }
        }

        public bool CustomBool4
        {
            [DebuggerStepThrough()]
            get { return customBool4; }
            [DebuggerStepThrough()]
            set { customBool4 = value; }
        }

        public DateTime CustomDate1
        {
            [DebuggerStepThrough()]
            get { return customDate1; }
            [DebuggerStepThrough()]
            set { customDate1 = value; }
        }

        public DateTime CustomDate2
        {
            [DebuggerStepThrough()]
            get { return customDate2; }
            [DebuggerStepThrough()]
            set { customDate2 = value; }
        }

        public DateTime CustomDate3
        {
            [DebuggerStepThrough()]
            get { return customDate3; }
            [DebuggerStepThrough()]
            set { customDate3 = value; }
        }

        public DateTime CustomDate4
        {
            [DebuggerStepThrough()]
            get { return customDate4; }
            [DebuggerStepThrough()]
            set { customDate4 = value; }
        }

        public decimal CustomDecimal1
        {
            [DebuggerStepThrough()]
            get { return customDecimal1; }
            [DebuggerStepThrough()]
            set { customDecimal1 = value; }
        }

        public decimal CustomDecimal2
        {
            [DebuggerStepThrough()]
            get { return customDecimal2; }
            [DebuggerStepThrough()]
            set { customDecimal2 = value; }
        }

        public decimal CustomDecimal3
        {
            [DebuggerStepThrough()]
            get { return customDecimal3; }
            [DebuggerStepThrough()]
            set { customDecimal3 = value; }
        }

        public decimal CustomDecimal4
        {
            [DebuggerStepThrough()]
            get { return customDecimal4; }
            [DebuggerStepThrough()]
            set { customDecimal4 = value; }
        }

        public int CustomInt1
        {
            [DebuggerStepThrough()]
            get { return customInt1; }
            [DebuggerStepThrough()]
            set { customInt1 = value; }
        }

        public int CustomInt2
        {
            [DebuggerStepThrough()]
            get { return customInt2; }
            [DebuggerStepThrough()]
            set { customInt2 = value; }
        }

        public int CustomInt3
        {
            [DebuggerStepThrough()]
            get { return customInt3; }
            [DebuggerStepThrough()]
            set { customInt3 = value; }
        }

        public int CustomInt4
        {
            [DebuggerStepThrough()]
            get { return customInt4; }
            [DebuggerStepThrough()]
            set { customInt4 = value; }
        }

        public string CustomString1
        {
            [DebuggerStepThrough()]
            get
            {
                return customString1;
                //string res = "";
                //customString1Translations.TryGetValue(Thread.CurrentThread.CurrentCulture.Name, out res);
                //if (Utility.IsEmptyFckField(res))
                //    customString1Translations.TryGetValue(Config.CultureDefault, out res);
                //return res;
            }
            [DebuggerStepThrough()]
            set { customString1 = value; }
        }

        public string CustomString2
        {
            [DebuggerStepThrough()]
            get
            {
                return customString2;
                //string res = "";
                //customString2Translations.TryGetValue(Thread.CurrentThread.CurrentCulture.Name, out res);
                //if (Utility.IsEmptyFckField(res))
                //    customString2Translations.TryGetValue(Config.CultureDefault, out res);
                //return res;
            }
            [DebuggerStepThrough()]
            set { customString2 = value; }
        }

        public string CustomString3
        {
            [DebuggerStepThrough()]
            get { return customString3; }
            [DebuggerStepThrough()]
            set { customString3 = value; }
        }

        public string CustomString4
        {
            [DebuggerStepThrough()]
            get { return customString4; }
            [DebuggerStepThrough()]
            set { customString4 = value; }
        }

        //public Dictionary<string, string> CustomTextTranslations1
        //{
        //    [DebuggerStepThrough()]
        //    get { return customTextTranslations1; }
        //    [DebuggerStepThrough()]
        //    set { customTextTranslations1 = value; }
        //}

        //public string CustomText1
        //{
        //    get
        //    {
        //        string res = "";
        //        customTextTranslations1.TryGetValue(Thread.CurrentThread.CurrentCulture.Name, out res);
        //        if (Utility.IsEmptyFckField(res))
        //            customTextTranslations1.TryGetValue(Config.CultureDefault, out res);
        //        if (res == null) res = "";
        //        return res;
        //    }
        //}

        #endregion

        #region methods

        public Item(){}

        /// <summary>
        /// delete images folder content
        /// </summary>
        public void DeleteImages()
        {
            new FilesGallery(ImagesPath, this.Id.ToString()).DeleteFolderContent();
            this.images = null;
        }

        /// <summary>
        /// delete files folder content
        /// </summary>
        public void DeleteFiles()
        {
            new FilesGallery(FilesPath, this.Id.ToString()).DeleteFolderContent();
            this.files = null;
        }

        /// <summary>
        /// fill custom fields from serialized string
        /// </summary>
        /// <param name="fieldsString"></param>
        public void LoadCustomFieldsFromString(string fieldsString)
        {
            //ItemType itemType = null;
            PropertyInfo pi = null;
            Type type = null;

            Dictionary<String, String> fieldsDict = new Dictionary<string, string>();
            fieldsDict = Utility.GetParamsDictFromString(fieldsString);
            if (fieldsDict.Count == 0) return;
            
            if (this.ItemTypeName == DefaultItemType)
                type = this.GetType();  //keep current type
            else
                type = BuildManager.GetType(this.ItemTypeName, false);  //derived type

            foreach (var item in fieldsDict)
            {
                //retrieve paramname and culture
                //string culture = "";
                string paramName = item.Key;
                //int sepIdx = paramName.LastIndexOf("__");
                //if (sepIdx > 0)
                //{
                //    culture = paramName.Substring(sepIdx + 2);
                //    paramName = paramName.Substring(0, sepIdx);
                //}

                pi = type.GetProperty(paramName);
                if (pi != null)
                {
                    if (this.ItemTypeName == DefaultItemType)
                    {
                        //set field value
                        setPropertyValue(pi, item.Value);
                    }
                    else
                    {
                        //set mapped field value
                        string baseClassFieldName = "";
                        foreach (ItemFieldMapAttribute attr in pi.GetCustomAttributes(typeof(ItemFieldMapAttribute), false))
                        {
                            if (attr != null)
                            {
                                baseClassFieldName = attr.FieldName.ToString(); //base class mapped field
                                break;
                            }
                        }
                        if (!string.IsNullOrEmpty(baseClassFieldName))
                        {
                            pi = type.GetProperty(baseClassFieldName); //base class field
                            if (pi != null)
                            {
                                setPropertyValue(pi, item.Value);
                            }
                        }
                    }
                }
            }
        }

        public int CompareTo(Item obj, string field)
        {
            int res = 0;

            switch (field.ToLower())
            {
                case "id":
                    res = this.Id.CompareTo(obj.Id);
                    break;
                case "title":
                    res = this.Title.CompareTo(obj.Title);
                    break;
                case "dateinserted":
                    res = this.DateInserted.CompareTo(obj.DateInserted);
                    break;
                case "dateupdated":
                    res = this.DateUpdated.CompareTo(obj.DateUpdated);
                    break;
                default:
                    res = this.Id.CompareTo(obj.Id);
                    break;
            }
            return res;
        }

        private void setPropertyValue(PropertyInfo pi, string value)
        {
            switch (pi.PropertyType.Name.ToLower())
            {
                case "boolean":
                    pi.SetValue(this, value == "1" ? true : false, null);
                    break;

                case "string":
                    pi.SetValue(this, value, null);
                    break;

                case "int32":
                    int intRes = 0;
                    int.TryParse(value, out intRes);
                    pi.SetValue(this, intRes, null);
                    break;

                case "decimal":
                    decimal decimalRes = 0.0m;
                    decimal.TryParse(value, out decimalRes);
                    pi.SetValue(this, decimalRes, null);
                    break;

                case "datetime":
                    DateTime datetimeRes = DateTime.MinValue;
                    DateTime.TryParse(value, out datetimeRes);
                    pi.SetValue(this, datetimeRes, null);
                    break;

                //case "dictionary`2":
                //    pi.SetValue(this, value, new[] { "Italiano" });
                //    break;
            }
        }

        private string parseText(string theText)
        {
            string res = theText;
            res = Utility.Html.FillPlaceholders(res);
            res = Utility.Html.RemoveSystemTags(res);
            return res;
        }

        #endregion

        public class ItemComparer : IComparer<Item>
        {
            private string sortExpression = "";
            private SortDirection sortDirection;


            public ItemComparer(string sortExpression, SortDirection sortDirection)
            {
                this.sortExpression = sortExpression;
                this.sortDirection = sortDirection;
            }

            public int Compare(Item lhs, Item rhs)
            {
                if (this.sortDirection == SortDirection.Descending)
                    return rhs.CompareTo(lhs, sortExpression);
                else
                    return lhs.CompareTo(rhs, sortExpression);
            }

            public bool Equals(Item lhs, Item rhs)
            {
                return this.Compare(lhs, rhs) == 0;
            }

            public int GetHashCode(Item e)
            {
                return e.GetHashCode();
            }
        }
    }


    [Serializable]
    public class ItemsFilter : ITableExternalId
    {
        #region fields definition

        private int id = 0;
        private string itemType = "";
        private int sectionId = 0;
        private int categoryId = 0;
        private Utility.TristateBool enabled = Utility.TristateBool.NotSet;
        private string userInserted = "";
        private string titleSearch = "";
        private string alias = "";
        private string descriptionSearch = "";
        private string fullSearch = "";
        private string accessCode = "";
        private int accessLevel = 0;
        private string writeAccessCode = "";
        private int writeAccessLevel = 0;
        private DatesRange itemDateRange = new DatesRange(DatesRange.RangeType.Always);
        private Utility.TristateBool isValidItem = Utility.TristateBool.NotSet;
        private int numOfRecords = 0;
        private int threadId = 0;
        private bool showOnlyRootItems = true;
        private string extId = "";

        //custom fields
        private Utility.TristateBool customBool1 = Utility.TristateBool.NotSet;
        private Utility.TristateBool customBool2 = Utility.TristateBool.NotSet;
        private Utility.TristateBool customBool3 = Utility.TristateBool.NotSet;
        private Utility.TristateBool customBool4 = Utility.TristateBool.NotSet;
        private int customInt1 = 0;
        private int customInt2 = 0;
        private int customInt3 = 0;
        private int customInt4 = 0;
        private string customString1 = "";
        private string customString2 = "";
        private string customString3 = "";
        private string customString4 = "";


        [DataObjectField(true)]
        public int Id
        {
            [DebuggerStepThrough()]
            get { return id; }
            [DebuggerStepThrough()]
            set { id = value; }
        }

        [DataObjectField(false)]
        public string ItemType
        {
            [DebuggerStepThrough()]
            get { return itemType; }
            [DebuggerStepThrough()]
            set { itemType = value; }
        }

        [DataObjectField(false)]
        public int SectionId
        {
            [DebuggerStepThrough()]
            get { return sectionId; }
            [DebuggerStepThrough()]
            set { sectionId = value; }
        }

        [DataObjectField(false)]
        public int CategoryId
        {
            [DebuggerStepThrough()]
            get { return categoryId; }
            [DebuggerStepThrough()]
            set { categoryId = value; }
        }

        [DataObjectField(false)]
        public Utility.TristateBool Enabled
        {
            [DebuggerStepThrough()]
            get { return enabled; }
            [DebuggerStepThrough()]
            set { enabled = value; }
        }

        [DataObjectField(false)]
        public string UserInserted
        {
            [DebuggerStepThrough()]
            get { return userInserted; }
            [DebuggerStepThrough()]
            set { userInserted = value; }
        }

        [DataObjectField(false)]
        public string TitleSearch
        {
            [DebuggerStepThrough()]
            get { return titleSearch; }
            [DebuggerStepThrough()]
            set { titleSearch = value; }
        }

        [DataObjectField(false)]
        public string Alias
        {
            [DebuggerStepThrough()]
            get { return Utility.GetUrlAlias(alias); }
            [DebuggerStepThrough()]
            set { alias = value; }
        }

        [DataObjectField(false)]
        public string DescriptionSearch
        {
            [DebuggerStepThrough()]
            get { return descriptionSearch; }
            [DebuggerStepThrough()]
            set { descriptionSearch = value; }
        }

        [DataObjectField(false)]
        public string FullSearch
        {
            [DebuggerStepThrough()]
            get { return fullSearch; }
            [DebuggerStepThrough()]
            set { fullSearch = value; }
        }

        public string AccessCode
        {
            [DebuggerStepThrough()]
            get { return accessCode; }
            [DebuggerStepThrough()]
            set { accessCode = value; }
        }

        public int AccessLevel
        {
            [DebuggerStepThrough()]
            get { return accessLevel; }
            [DebuggerStepThrough()]
            set { accessLevel = value; }
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

        public DatesRange ItemDateRange
        {
            [DebuggerStepThrough()]
            get { return itemDateRange; }
            [DebuggerStepThrough()]
            set { itemDateRange = value; }
        }

        /// <summary>
        /// check if current date is between ValidFrom and ValidTo dates
        /// </summary>
        [DataObjectField(false)]
        public Utility.TristateBool IsValidItem
        {
            [DebuggerStepThrough()]
            get { return isValidItem; }
            [DebuggerStepThrough()]
            set { isValidItem = value; }
        }

        /// <summary>
        /// number of records to load. 0 All records
        /// </summary>
        [DataObjectField(false)]
        public int NumOfRecords
        {
            [DebuggerStepThrough()]
            get { return numOfRecords; }
            [DebuggerStepThrough()]
            set { numOfRecords = value; }
        }

        [DataObjectField(false)]
        public int ThreadId
        {
            [DebuggerStepThrough()]
            get { return threadId; }
            [DebuggerStepThrough()]
            set { threadId = value; }
        }

        [DataObjectField(false)]
        public bool ShowOnlyRootItems
        {
            [DebuggerStepThrough()]
            get { return showOnlyRootItems; }
            [DebuggerStepThrough()]
            set { showOnlyRootItems = value; }
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


        //custom fields
        public Utility.TristateBool CustomBool1
        {
            [DebuggerStepThrough()]
            get { return customBool1; }
            [DebuggerStepThrough()]
            set { customBool1 = value; }
        }

        public Utility.TristateBool CustomBool2
        {
            [DebuggerStepThrough()]
            get { return customBool2; }
            [DebuggerStepThrough()]
            set { customBool2 = value; }
        }

        public Utility.TristateBool CustomBool3
        {
            [DebuggerStepThrough()]
            get { return customBool3; }
            [DebuggerStepThrough()]
            set { customBool3 = value; }
        }

        public Utility.TristateBool CustomBool4
        {
            [DebuggerStepThrough()]
            get { return customBool4; }
            [DebuggerStepThrough()]
            set { customBool4 = value; }
        }

        public int CustomInt1
        {
            [DebuggerStepThrough()]
            get { return customInt1; }
            [DebuggerStepThrough()]
            set { customInt1 = value; }
        }

        public int CustomInt2
        {
            [DebuggerStepThrough()]
            get { return customInt2; }
            [DebuggerStepThrough()]
            set { customInt2 = value; }
        }

        public int CustomInt3
        {
            [DebuggerStepThrough()]
            get { return customInt3; }
            [DebuggerStepThrough()]
            set { customInt3 = value; }
        }

        public int CustomInt4
        {
            [DebuggerStepThrough()]
            get { return customInt4; }
            [DebuggerStepThrough()]
            set { customInt4 = value; }
        }

        public string CustomString1
        {
            [DebuggerStepThrough()]
            get { return customString1; }
            [DebuggerStepThrough()]
            set { customString1 = value; }
        }

        public string CustomString2
        {
            [DebuggerStepThrough()]
            get { return customString2; }
            [DebuggerStepThrough()]
            set { customString2 = value; }
        }

        public string CustomString3
        {
            [DebuggerStepThrough()]
            get { return customString3; }
            [DebuggerStepThrough()]
            set { customString3 = value; }
        }

        public string CustomString4
        {
            [DebuggerStepThrough()]
            get { return customString4; }
            [DebuggerStepThrough()]
            set { customString4 = value; }
        }

        #endregion
    }
}