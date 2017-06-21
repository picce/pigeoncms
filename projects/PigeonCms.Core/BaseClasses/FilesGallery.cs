using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Xml;

namespace PigeonCms
{
    /// <summary>
    /// meta info about generic uploaded file
    /// </summary>
    public class FileMetaInfo
    {
        private bool dataLoaded = false;
        private Dictionary<string, string> titleTranslations = new Dictionary<string, string>();
        private Dictionary<string, string> descriptionTranslations = new Dictionary<string, string>();


        public FileMetaInfo(){}

        public FileMetaInfo(string fileUrl): this(fileUrl, false){}

        public FileMetaInfo(string fileUrl, bool isFolder)
        {
            this.FileUrl = fileUrl;
            this.isFolder = isFolder;
            if (!string.IsNullOrEmpty(this.FileUrl))
                GetData();
        }

        private bool isFolder = false;
        public bool IsFolder
        {
            [DebuggerStepThrough()]
            get { return isFolder; }
        }

        public string FileName
        {
            [DebuggerStepThrough()]
            get
            {
                string res = "";
                if (!string.IsNullOrEmpty(fileUrl))
                {
                    if (fileUrl.StartsWith("~") || fileUrl.StartsWith("/"))
                        res = VirtualPathUtility.GetFileName(fileUrl);
                    else
                        res = Path.GetFileName(fileUrl);
                }

                return res;
            }
        }

        public string FileNameNoExtension
        {
            [DebuggerStepThrough()]
            get 
            { 
                int dot = this.FileName.LastIndexOf(".");
                return this.FileName.Substring(0, dot); 
            }
        }

        private string fileUrl = "";
        //absolute path in local website eg:/public/filename.ext
        public string FileUrl
        {
            [DebuggerStepThrough()]
            get { return fileUrl; }
            [DebuggerStepThrough()]
            set { fileUrl = value; }
        }

        private string fileFullUrl = null;
        //absolute path eg:http://www.yoursite.com/public/filename.ext
        public string FileFullUrl
        {
            [DebuggerStepThrough()]
            get 
            {
                if (fileFullUrl == null)
                {
                    fileFullUrl = "";
                    if (!string.IsNullOrEmpty(this.FileUrl))
                        fileFullUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + this.FileUrl;
                }
                return fileFullUrl; 
            }
        }

        private long fileLength = 0;
        public long FileLength
        {
            [DebuggerStepThrough()]
            get { return fileLength; }
            [DebuggerStepThrough()]
            set { fileLength = value; }
        }

        /// <summary>
        /// file length in B, KB, MB, GB
        /// </summary>
        public string HumanLength
        {
            [DebuggerStepThrough()]
            get 
            {
                return Utility.GetFileHumanLength(this.FileLength);
            }
        }

        public string FileExtension
        {
            [DebuggerStepThrough()]
            get 
            {
                string res = "";
                try
                {
                    res = new FileInfo(this.FileName).Extension;
                }
                catch { }
                return res;
            }
        }

        /// <summary>
        /// Title in current culture
        /// </summary>
        public string Title
        {
            get
            {
                if (!dataLoaded) GetData();
                string res = "";
                titleTranslations.TryGetValue(Thread.CurrentThread.CurrentCulture.Name, out res);
                if (string.IsNullOrEmpty(res))
                    titleTranslations.TryGetValue(Config.CultureDefault, out res);
                return res;
            }
        }

        /// <summary>
        /// Title in different cultures
        /// </summary>
        public Dictionary<string, string> TitleTranslations
        {
            [DebuggerStepThrough()]
            get { return titleTranslations; }
            [DebuggerStepThrough()]
            set { titleTranslations = value; }
        }

        /// <summary>
        /// Description in current cultures
        /// </summary>
        public string Description
        {
            get
            {
                if (!dataLoaded) GetData();
                string res = "";
                descriptionTranslations.TryGetValue(Thread.CurrentThread.CurrentCulture.Name, out res);
                if (string.IsNullOrEmpty(res))
                    descriptionTranslations.TryGetValue(Config.CultureDefault, out res);
                return res;
            }
        }

        /// <summary>
        /// Description in different cultures
        /// </summary>
        public Dictionary<string, string> DescriptionTranslations
        {
            [DebuggerStepThrough()]
            get { return descriptionTranslations; }
            [DebuggerStepThrough()]
            set { descriptionTranslations = value; }
        }

        /// <summary>
        /// save file meta data (title, description, etc.)
        /// </summary>
        /// <returns></returns>
        public bool SaveData()
        {
            bool res = true;
            string filename = FilesHelper.MapPathWhenVirtual(this.FileUrl);
            FileStream fs = new FileStream(filename + ".xml", FileMode.Create);
            XmlWriter w = XmlWriter.Create(fs);

            w.WriteStartDocument();
            w.WriteStartElement("FileInfo");

            w.WriteStartElement("TitleTranslations");
            foreach (KeyValuePair<string, string> item in Config.CultureList)
            {
                string currVal = "";
                titleTranslations.TryGetValue(item.Key, out currVal);
                w.WriteStartElement("item");
                w.WriteAttributeString("len", item.Key);
                w.WriteCData(currVal);
                //w.WriteString(currTitle);
                //w.WriteElementString("title", currTitle);
                w.WriteEndElement();
            }
            w.WriteEndElement();

            w.WriteStartElement("DescriptionTranslations");
            foreach (KeyValuePair<string, string> item in Config.CultureList)
            {
                string currVal = "";
                descriptionTranslations.TryGetValue(item.Key, out currVal);
                w.WriteStartElement("item");
                w.WriteAttributeString("len", item.Key);
                w.WriteCData(currVal);
                w.WriteEndElement();
            }
            w.WriteEndElement();
            
            w.WriteEndElement();
            w.WriteEndDocument();
            w.Flush();
            fs.Close();

            return res;
        }

        /// <summary>
        /// get file data (Title, Description, etc.)
        /// </summary>
        /// <returns></returns>
        public bool GetData()
        {
            bool res = true;
            string filename = FilesHelper.MapPathWhenVirtual(this.FileUrl) + ".xml";
            XmlDocument doc = new XmlDocument();
            try
            {
                if (System.IO.File.Exists(filename))
                {
                    doc.Load(filename);

                    XmlNode rootNode;
                    rootNode = doc.SelectSingleNode("FileInfo");
                    if (rootNode == null)
                    {
                        //backward compatibility
                        rootNode = doc.SelectSingleNode("EmbeddedImage");
                    }

                    //titles
                    this.TitleTranslations.Clear();
                    XmlNodeList titleNodes = rootNode.SelectNodes("TitleTranslations/item");
                    foreach (XmlNode item in titleNodes)
                    {
                        string lenAttr = "";
                        string currValue = "";
                        if (item.Attributes["len"] != null)
                        {
                            lenAttr = item.Attributes["len"].Value;
                        }
                        currValue = item.InnerText;
                        this.TitleTranslations.Add(lenAttr, currValue);
                    }

                    //descriptions
                    this.DescriptionTranslations.Clear();
                    XmlNodeList descNodes = rootNode.SelectNodes("DescriptionTranslations/item");
                    foreach (XmlNode item in descNodes)
                    {
                        string lenAttr = "";
                        string currValue = "";
                        if (item.Attributes["len"] != null)
                        {
                            lenAttr = item.Attributes["len"].Value;
                        }
                        currValue = item.InnerText;
                        this.DescriptionTranslations.Add(lenAttr, currValue);
                    }
                }
            }
            catch
            {
                res = false;
            }
            finally
            {
            }
            this.dataLoaded = res;
            return res;
        }

    }

    /// <summary>
    /// generic file gallery
    /// </summary>
    [DataObject()]
    public class FilesGallery
    {
        #region fields

        public string TempPath
        {
            get
            {
                const string tempPath = "~/public/files/temp/";
                string res = "";
                if (!string.IsNullOrEmpty(Utility._SessionID()))
                {
                    res = VirtualPathUtility.ToAbsolute(tempPath + Utility._SessionID());
                }
                return res;
            }
        }

        public string TempPhisicalPath
        {
            get
            {
                string res = this.TempPath;
                res = FilesHelper.MapPathWhenVirtual(res);
                return res;
            }
        }

        private string searchPattern = "*.*";
        /// <summary>
        /// search pattern string used by GetAll() method
        /// </summary>
        public string SearchPattern
        {
            [DebuggerStepThrough()]
            get { return searchPattern; }
            [DebuggerStepThrough()]
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    searchPattern = value;
                }
            }
        }

        private string folderName = "";
        public string FolderName
        {
            [DebuggerStepThrough()]
            get { return this.folderName; }
            [DebuggerStepThrough()]
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (!value.EndsWith("/")) value += "/";
                    this.folderName = value;
                }
            }
        }

        private string virtualPath = "~/Public/files/";
        /// <summary>
        /// virtual base path for files gallery 
        /// example: "~/Public/files/"
        /// </summary>
        public string VirtualPath
        {
            [DebuggerStepThrough()]
            get { return this.virtualPath; }
            [DebuggerStepThrough()]
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (!value.EndsWith("/")) value += "/";
                    this.virtualPath = value;
                }
            }
        }

        /// <summary>
        /// absolute path
        /// </summary>
        public string AbsolutePath
        {
            [DebuggerStepThrough()]
            get
            {
                string res = this.virtualPath;
                if (!string.IsNullOrEmpty(this.folderName))
                    res += this.folderName;
                if (res.StartsWith("~") || res.StartsWith("/"))
                    res = VirtualPathUtility.ToAbsolute(res);

                return res;
            }
        }

        /// <summary>
        /// phisical path of foldername
        /// </summary>
        protected string PhisicalPath
        {
            //[DebuggerStepThrough()]
            get
            {
                string res = this.virtualPath;
                if (!string.IsNullOrEmpty(this.folderName))
                    res += this.folderName;
                res = FilesHelper.MapPathWhenVirtual(res);
                return res;
            }
        }

        #endregion


        [DebuggerStepThrough()]
        public FilesGallery(){ }

        [DebuggerStepThrough()]
        public FilesGallery(string virtualPath, string folderName)
        {
            this.VirtualPath = virtualPath;
            this.FolderName = folderName;
        }

        [DebuggerStepThrough()]
        public FilesGallery(string virtualPath, string folderName, string searchPattern)
        {
            this.VirtualPath = virtualPath;
            this.FolderName = folderName;
            this.SearchPattern = searchPattern;
        }

        /// <summary>
        /// get list of files filtered by searchPattern
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<FileMetaInfo> GetAll()
        {
            List<FileMetaInfo> result = new List<FileMetaInfo>();
            try
            {
                DirectoryInfo dir = new DirectoryInfo(this.PhisicalPath);
                if (dir.Exists)
                {
                    var folders = dir.GetDirectories(this.SearchPattern);
                    foreach (DirectoryInfo folder in folders)
                    {

                        if (folder.Name.ToLower() != ".svn" && folder.Name.ToLower() != "_vti_cnf")
                        {
                            var item = new FileMetaInfo("", true);
                            fillObject(item, folder);
                            result.Add(item);
                        } 
                    }

                    var files = dir.GetFiles(this.SearchPattern);
                    foreach (FileInfo file in files)
                    {
                        if (file.Extension.ToLower() != ".xml")
                        {
                            var item = new FileMetaInfo();
                            fillObject(item, file);
                            result.Add(item);
                        }
                    }
                }
            }
            finally
            {
            }
            return result;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public FileMetaInfo GetByFileName(string fileName)
        {
            var result = new FileMetaInfo();
            try
            {
                var file = new FileInfo(this.AbsolutePath + fileName);
                if (file.Exists)
                    fillObject(result, file);
            }
            finally
            {
            }
            return result;
        }

        public void RenameFile(string sourceFileName, string destFileName)
        {
            if (sourceFileName == destFileName)
                return;
            File.Move(Path.Combine(this.PhisicalPath, sourceFileName), Path.Combine(this.PhisicalPath, destFileName));
            File.Move(Path.Combine(this.PhisicalPath, sourceFileName + ".xml"), Path.Combine(this.PhisicalPath, destFileName + ".xml"));
        }

        public bool CreateFolder(string folderName)
        {
            bool res = false;
            try
            {
                var dir = new DirectoryInfo(this.PhisicalPath);
                if (dir.Exists)
                {
                    Directory.CreateDirectory(this.PhisicalPath + folderName);
                    res = true;
                }
            }
            finally
            {
            }
            return res;
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool DeleteByFileName(string fileName)
        {
            return DeleteByFileName(fileName, false);
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool DeleteByFileName(string fileName, bool isFolder)
        {
            bool res = false;
            try
            {
                if (isFolder)
                {
                    var dir = new DirectoryInfo(this.PhisicalPath + fileName);
                    if (dir.Exists)
                    {
                        dir.Delete();
                        res = true;
                    }
                }
                else
                {
                    var file = new FileInfo(this.PhisicalPath + fileName);
                    if (file.Exists)
                    {
                        file.Delete();
                        res = true;
                    }
                }
                //delete file with meta info
                var metafile = new FileInfo(this.PhisicalPath + fileName + ".xml");
                if (metafile.Exists)
                    metafile.Delete();
            }
            finally
            {
            }
            return res;
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool DeleteFolderContent()
        {
            bool res = false;
            try
            {
                Utility.DeleteFolderContent(this.PhisicalPath);
                res = true;
            }
            finally
            {
            }
            return res;
        }

        public bool DeleteFolder()
        {
            bool res = false;
            try
            {
                Utility.DeleteFolder(this.PhisicalPath);
                res = true;
            }
            finally
            {
            }
            return res;
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool RemoveSessionTempFolder()
        {
            bool res = false;
            try
            {
                if (string.IsNullOrEmpty(this.TempPhisicalPath))
                    return true;
                DirectoryInfo dir = new DirectoryInfo(this.TempPhisicalPath);
                if (dir.Exists)
                    dir.Delete(true);
                res = true;
            }
            catch { }
            return res;
        }

        public bool MoveTempFiles()
        {
            bool res = false;
            try
            {
                if (string.IsNullOrEmpty(this.TempPhisicalPath))
                    return true;
                DirectoryInfo dir = new DirectoryInfo(this.TempPhisicalPath);
                if (!dir.Exists)
                    return true;

                Utility.CopyFolder(this.TempPhisicalPath, this.PhisicalPath);
                dir.Delete(true);
                res = true;
            }
            catch { }
            return res;
        }

        private void fillObject(FileMetaInfo result, FileInfo item)
        {
            fillObject(result, item.Name, item.Length);
        }

        private void fillObject(FileMetaInfo result, DirectoryInfo item)
        {
            fillObject(result, item.Name, 0);
        }

        private void fillObject(FileMetaInfo result, string name, long fileLength)
        {
            //result.FileFullUrl = this.
            result.FileUrl = this.AbsolutePath + name;
            result.FileLength = fileLength;
        }
    }
}