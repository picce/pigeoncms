using AjaxControlToolkit;
using AQuest.Cecchi.Utils;
using ImageResizer;
using PigeonCms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AQuest.PigeonCMS.Controls
{
    public partial class ImageUpload : UserControl
    {
        public const int DefaultThumbWidth = 70;
        public const int DefaultThumbHeight = 70;

        public string PageScriptManager { get; set; }
        public string AllowedFileTypes { get; set; }
        public int MaxFileSize { get; set; }
        public int ThumbWidth { get; set; }
        public int ThumbHeight { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ThumbWidth <= 0)
                ThumbWidth = DefaultThumbWidth;

            if (ThumbHeight <= 0)
                ThumbHeight = DefaultThumbHeight;

            if (Request["action"] == "ImageUploadGetPreview")
            {
                string uploadId = Request["uploadId"];
                CheckPreview(uploadId, ThumbWidth, ThumbHeight);
                return;
            }

            litUploaderID.Text = UniqueID;

            btnRemove.Click += btnRemove_Click;

            asyncFileUpload.UploadedComplete += new EventHandler<AsyncFileUploadEventArgs>(asyncFileUpload_UploadedComplete);
            asyncFileUpload.UploadedFileError += new EventHandler<AsyncFileUploadEventArgs>(asyncFileUpload_UploadedFileError);

            lblLoader.Text = Utility.GetLabel("LblLoading", "Loading");
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            object oTmpFilePath = Session["ImageUpload_" + UniqueID];
            if (oTmpFilePath != null)
            {
                string tmpFilePath = Path.GetTempPath() + Convert.ToString(oTmpFilePath);
                if (File.Exists(tmpFilePath))
                    File.Delete(tmpFilePath);

                Session.Remove("ImageUpload_" + UniqueID);
            }
        }

        protected void asyncFileUpload_UploadedFileError(object sender, AsyncFileUploadEventArgs e)
        {

        }

        protected void asyncFileUpload_UploadedComplete(object sender, AsyncFileUploadEventArgs e)
        {
            string extension = asyncFileUpload.ContentType.Split('/')[1];
            if (!string.IsNullOrWhiteSpace(AllowedFileTypes))
            {
                List<string> fileTypes = new List<string>(AllowedFileTypes.Split(','));
                if (!fileTypes.Contains(extension))
                    throw new Exception("Invalid file type");
            }

            if (MaxFileSize > 0 && asyncFileUpload.PostedFile.ContentLength > MaxFileSize)
                throw new Exception("Invalid file size");

            string tmpFileName = Guid.NewGuid().ToString() + "." + extension;
            string tmpFilePath = Path.GetTempPath() + tmpFileName;

            Session["ImageUpload_" + UniqueID] = tmpFileName;

            asyncFileUpload.SaveAs(tmpFilePath);
        }

        public void SaveTo(string filePath)
        {
            string tmpFilePath = Path.GetTempPath() + Session["ImageUpload_" + UniqueID];
            if (File.Exists(tmpFilePath))
            {
                string directory = FilesManipulationUtils.GetDirectory(filePath);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                File.Move(tmpFilePath, filePath);
                Session.Remove("ImageUpload_" + UniqueID);
            }
        }

        public string GetExtension ()
        {
            string tmpFilePath = Path.GetTempPath() + Session["ImageUpload_" + UniqueID];
            if (!File.Exists(tmpFilePath))
                return string.Empty;

            return tmpFilePath.Split('.').Last();
        }

        public static void CheckPreview(string uploadId)
        {
            CheckPreview(uploadId, DefaultThumbWidth, DefaultThumbHeight);
        }

        public static void CheckPreview(string uploadId, int thumbWidth, int thumbHeight)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(uploadId))
                    return;

                string tmpFileName = Convert.ToString(HttpContext.Current.Session["ImageUpload_" + uploadId]);
                if (tmpFileName == null)
                    return;

                string tmpFilePath = Path.GetTempPath() + tmpFileName;

                Instructions resizeParams = new Instructions();
                resizeParams.Width = thumbWidth;
                resizeParams.Height = thumbHeight;
                resizeParams.IgnoreICC = true;
                resizeParams.Mode = FitMode.Pad;
                resizeParams.PaddingColor = "#362f4c";
                resizeParams.OutputFormat = OutputFormat.Jpeg;

                ImageJob i = new ImageJob(tmpFilePath, Path.GetTempPath() + "<guid>.<ext>", resizeParams);
                i.Build();

                using (Bitmap img = new Bitmap(i.FinalPath))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        HttpContext.Current.Response.Clear();
                        HttpContext.Current.Response.ContentType = "image/" + (img.RawFormat == ImageFormat.Png ? "png" : "jpeg");
                        img.Save(ms, img.RawFormat);
                        ms.WriteTo(HttpContext.Current.Response.OutputStream);
                        ms.Flush();
                        HttpContext.Current.Response.Flush();
                        ms.Close();
                    }
                }

                File.Delete(i.FinalPath);
                HttpContext.Current.Response.End();
            }
            catch
            {
                return;
            }
        }
    }
}