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


namespace PigeonCms
{
    public class FileUploadControl: PigeonCms.BaseModuleControl
    {
        public class FileUploadEventArgs : EventArgs
        {
            private List<string> files =  new List<string>();
            private string message = "";
            private bool result = true;

            public List<string> Files
            {
                get { return files; }
            }

            public string Message
            {
                get { return message; }
            }

            public bool Result
            {
                get { return result; }
            }

            public FileUploadEventArgs(List<string> files, string message, bool result)
            {
                this.files = files;
                this.message = message;
                this.result = result;
            }
        }

        #region private fields

        private string fileExtensions = "";
        private int fileSize = 0;
        private PigeonCms.FileUploadControl.FileNameTypeEnum fileNameType = PigeonCms.FileUploadControl.FileNameTypeEnum.KeepOriginalName;
        private string filePrefix = "";
        private string forcedFilename = "";
        private string filePath = "~/Public";
        private int uploadFields = 1;
        private int numOfFilesAllowed = 0;
        private bool showWorkingPath = true;

        private int customWidth = 0;
        private int customHeight = 0;

        private string headerText = "";
        private string footerText = "";
        private string successText = "";
        private string errorText = "";
        private string buttonText = "Upload";
        
        private List<string> uploadedFiles = new List<string>();

        #endregion


        #region events

        public delegate void AfterUploadDelegate(object sender, FileUploadEventArgs e);
        public event AfterUploadDelegate AfterUpload; 

        #endregion

        public enum FileNameTypeEnum
        {
            KeepOriginalName = 0,
            PrefixOriginalName = 1,
            PrefixCounter = 2,
            ForceFileName = 3
        }

        #region public fields
        /// <summary>
        /// allowed file extensions ex. jpg;jpeg;gif
        /// </summary>
        public string FileExtensions
        {
            get { return GetStringParam("FileExtensions", fileExtensions).ToLower(); }
            set { fileExtensions = value; }
        }

        public List<string> ExtensionsList
        {
            get
            {
                var res = new List<string>();
                if (!string.IsNullOrEmpty(this.FileExtensions))
                    res = Utility.String2List(this.FileExtensions, ';');
                return res;
            }
        }

        /// <summary>
        /// max size in KB per each file uploaded
        /// 0 no limit (web server limit)
        /// </summary>
        public int FileSize
        {
            get { return GetIntParam("FileSize", fileSize); }
            set { fileSize = value; }
        }

        public PigeonCms.FileUploadControl.FileNameTypeEnum FileNameType
        {
            get 
            {
                int res = (int)fileNameType;
                res = GetIntParam("FileNameType", res);
                return (PigeonCms.FileUploadControl.FileNameTypeEnum)res;
            }
            set { fileNameType = value; }
        }

        /// <summary>
        /// file prefix
        /// </summary>
        public string FilePrefix
        {
            get { return GetStringParam("FilePrefix", filePrefix); }
            set { filePrefix = value; }
        }

        /// <summary>
        /// forced file name, only FileTypeEnum.ForceFilename
        /// </summary>
        public string ForcedFilename
        {
            get { return GetStringParam("ForcedFilename", forcedFilename); }
            set { forcedFilename = value; }
        }

        /// <summary>
        /// where to store the uploaded file
        /// </summary>
        public string FilePath
        {
            get 
            {
                string res = "";
                res = GetStringParam("FilePath", filePath);
                if (!res.EndsWith("/"))
                    res += "/";
                return res;
            }
            set { filePath = value; }
        }

        /// <summary>
        /// Number of upload fields
        /// </summary>
        public int UploadFields
        {
            get { return GetIntParam("UploadFields", uploadFields); }
            set { uploadFields = value; }
        }

        /// <summary>
        /// Number of files
        /// </summary>
        public int NumOfFilesAllowed
        {
            get { return GetIntParam("NumOfFilesAllowed", numOfFilesAllowed); }
            set { numOfFilesAllowed = value; }
        }

        public bool ShowWorkingPath
        {
            get { return GetBoolParam("ShowWorkingPath", showWorkingPath); }
            set { showWorkingPath = value; }
        }

        /// <summary>
        /// Resize images to given size
        /// </summary>
        public int CustomWidth
        {
            get { return GetIntParam("CustomWidth", customWidth); }
            set { customWidth = value; }
        }

        /// <summary>
        /// Resize images to given size
        /// </summary>
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

        public string SuccessText
        {
            get { return GetStringParam("SuccessText", successText); }
            set { successText = value; }
        }

        public string ErrorText
        {
            get { return GetStringParam("ErrorText", errorText); }
            set { errorText = value; }
        }

        public string ButtonText
        {
            get { return GetStringParam("ButtonText", buttonText); }
            set { buttonText = value; }
        }

        public List<string> UploadedFiles
        {
            [DebuggerStepThrough()]
            get { return uploadedFiles; }
        }

        #endregion


        #region public methods

        public void AddField(Panel container, string fieldId)
        {
            System.Web.UI.WebControls.FileUpload upload =
                new System.Web.UI.WebControls.FileUpload();
            upload.ID = fieldId;
            container.Controls.Add(upload);

            Literal litBr = new Literal();
            litBr.Text = "<br />";
            container.Controls.Add(litBr);
        }

        public void AddFields(Panel container)
        {
            for (int i = 0; i < this.UploadFields; i++)
            {
                AddField(container, "Upload" + i.ToString());
            }
        }

        public bool UploadFile(System.Web.UI.WebControls.FileUpload uploadField, int fileCounter)
        {
            string fileName = "";
            string path = "";
            bool res = false;

            path = FilesHelper.MapPathWhenVirtual(this.FilePath);
            DirectoryInfo dir = new DirectoryInfo(path);

            if (!dir.Exists)
                dir.Create();

            if (uploadField.HasFile)
            {
                if (checkExtensions(uploadField.FileName))
                {
                    switch (this.FileNameType)
                    {
                        case FileNameTypeEnum.PrefixOriginalName:
                            fileName = this.FilePrefix + uploadField.FileName;
                            break;
                        case FileNameTypeEnum.PrefixCounter:
                            fileName = this.FilePrefix + fileCounter.ToString() + Path.GetExtension(uploadField.FileName);
                            break;
                        case FileNameTypeEnum.ForceFileName:
                            fileName = this.ForcedFilename + Path.GetExtension(uploadField.FileName);
                            break;
                        case FileNameTypeEnum.KeepOriginalName:
                        default:
                            fileName = uploadField.FileName;
                            break;
                    }

                    fileName = sanitizeFilename(fileName);

                    if (checkFileSize(uploadField.PostedFile.ContentLength))
                    {
                        var filepath = Path.Combine(path, fileName);
                        saveFile(filepath, uploadField);
                        res = true;
                        uploadedFiles.Add(filepath);
                    }
                    else
                        throw new ArgumentException("file too big");
                }
                else
                    throw new ArgumentException("file type not allowed");
            }
            return res;
        }

        public bool UploadFile(System.Web.UI.WebControls.FileUpload uploadField)
        {
            return UploadFile(uploadField, 0);
        }

        public bool UploadFiles(Panel container)
        {
            bool res = false;
            int counter = 0;
            uploadedFiles.Clear();
            //loop through FileUploads controls in panel
            foreach (Control upload in container.Controls)
            {
                if (upload is System.Web.UI.WebControls.FileUpload)
                {
                    UploadFile((System.Web.UI.WebControls.FileUpload)upload, counter);
                    counter++;
                    res = true;
                }
            }
            if (this.AfterUpload != null)
            {
                var args = new FileUploadEventArgs(this.UploadedFiles, "", res);
                this.AfterUpload(this, args);
            }
            return res;
        }

        #endregion


        #region private methods

        private bool checkExtensions(string fileName)
        {
            bool res = true;
            string fileExt = Utility.GetFileExt(fileName);
            if (this.ExtensionsList.Count > 0)
            {
                res = this.ExtensionsList.Contains(fileExt.ToLower());
            }
            return res;
        }

        private bool checkFileSize(int postedFileSize)
        {
            bool res = true;
            if (this.FileSize > 0)
            {
                if (postedFileSize > this.FileSize * 1024)
                    res = false;
            }
            else if (this.FileSize < 0)
            {
                res = false;
            }
            return res;
        }

        private void saveFile(string filename, FileUpload clientFile)
        {
            switch (Utility.GetFileExt(filename).ToLower())
            {
                case "jpg":
                case "png":
                case "gif":
                    saveImageFile(new Bitmap(clientFile.PostedFile.InputStream), filename);
                    break;

                default:
                    clientFile.SaveAs(filename);
                    break;
            }
            if (this.CustomWidth > 0)
            {

            }
        }

        private void saveImageFile(Bitmap sourceImage, string filename)
        {
            //see http://www.nerdymusings.com/LPMArticle.asp?ID=32 for jit resize of images
            if (this.CustomWidth > 0 && sourceImage.Width > this.CustomWidth) 
            {
                int newImageHeight = (int)(sourceImage.Height * ((float)this.CustomWidth / (float)sourceImage.Width));

                Bitmap resizedImage = new Bitmap(this.CustomWidth, newImageHeight);
                Graphics gr = Graphics.FromImage(resizedImage);
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.DrawImage(sourceImage, 0, 0, this.CustomWidth, newImageHeight);
                // Save the resized image:
                resizedImage.Save(filename, fileExtensionToImageFormat(filename));
            } 
            else 
            {
                // Save the source image (no resizing necessary):
                sourceImage.Save(filename, fileExtensionToImageFormat(filename));
            }
        }

        private static ImageFormat fileExtensionToImageFormat(String filePath)
        {
            String ext = Utility.GetFileExt(filePath).ToLower();
            ImageFormat result = ImageFormat.Jpeg;
            switch (ext)
            {
                case "gif":
                    result = ImageFormat.Gif;
                    break;
                case "png":
                    result = ImageFormat.Png;
                    break;
            }
            return result;
        }

        private string sanitizeFilename(string fileName)
        {
            string res = fileName;
            res = fileName.Replace(",", "-").Replace(" ", "-");
            return res;
        }

        #endregion
    }
}