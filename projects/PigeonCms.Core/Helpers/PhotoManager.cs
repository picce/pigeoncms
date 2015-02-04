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
    /// allowed image size
    /// </summary>
    public enum PhotoSize
    {
        Custom = 0,
        Small = 1,
        Medium = 2,
        Large = 3,
        XLarge = 4,
        Original = 5,
        Percentage = 6
    }


    /// <summary>
    /// static methods about images
    /// </summary>
    public class PhotoManager
    {

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imagePath">full path of image</param>
        /// <param name="keepOriginalSize">true: imageSize is not used</param>
        /// <param name="imageSize">set only width or height for automatic resize</param>
        /// <returns>a bitmap image</returns>
        //public static Stream GetImageStream(string imagePath, bool keepOriginalSize, Size imageSize)
        public static Bitmap GetImageBitmap(string imagePath, bool keepOriginalSize, Size imageSize)
        {
            Bitmap tmpImage = (Bitmap)System.Drawing.Image.FromFile(imagePath);
            if (!keepOriginalSize && (imageSize.Width + imageSize.Height) > 0)
            {
                if (imageSize.Height == 0)
                    imageSize.Height = Convert.ToInt32((imageSize.Width * tmpImage.Height) / tmpImage.Width);

                if (imageSize.Width == 0)
                    imageSize.Width = Convert.ToInt32((imageSize.Height * tmpImage.Width) / tmpImage.Height);

                tmpImage = new Bitmap(tmpImage, imageSize.Width, imageSize.Height);
            }
            return tmpImage;
        }

        public static Size GetPhotoSizeValue(PhotoSize imageSize)
        {
            Size returnValue = new System.Drawing.Size();
            int value = 0;
            switch (imageSize)
            {
                case PhotoSize.Custom:
                    break;
                case PhotoSize.Small:
                    int.TryParse(AppSettingsManager.GetValue("PhotoSize_Small"), out value);
                    if (value == 0) value = 90;
                    break;
                case PhotoSize.Medium:
                    int.TryParse(AppSettingsManager.GetValue("PhotoSize_Medium"), out value);
                    if (value == 0) value = 105;
                    break;
                case PhotoSize.Large:
                    int.TryParse(AppSettingsManager.GetValue("PhotoSize_Large"), out value);
                    if (value == 0) value = 250;
                    break;
                case PhotoSize.XLarge:
                    int.TryParse(AppSettingsManager.GetValue("PhotoSize_XLarge"), out value);
                    if (value == 0) value = 350;
                    break;
                case PhotoSize.Original:
                    break;
                default:
                    break;
            }
            returnValue.Width = value;
            return returnValue;
        }

        public static string GetPhotoSizeCode(PhotoSize imageSize)
        {
            string res = "";
            switch (imageSize)
            {
                case PhotoSize.Custom:
                    res = "custom";
                    break;
                case PhotoSize.Small:
                    res = "s";
                    break;
                case PhotoSize.Medium:
                    res = "m";
                    break;
                case PhotoSize.Large:
                    res = "l";
                    break;
                case PhotoSize.XLarge:
                    res = "xl";
                    break;
                case PhotoSize.Percentage:
                    res = "percentage";
                    break;
                case PhotoSize.Original:
                    break;
                default:
                    break;
            }
            return res;
        }

        /// <summary>
        /// use imageResizer
        /// </summary>
        public static string GetPreviewSrc2(string imageUrl, int customWidth, int customHeight)
        {
            string res = "";
            //check file exists
            string imagePath = "";
            string defaultImageUrl = VirtualPathUtility.ToAbsolute("~/Images/blank.jpg");
            try
            {
                imagePath = HttpContext.Current.Server.MapPath(imageUrl);
            }
            catch { }
            if (!System.IO.File.Exists(imagePath))
                imageUrl = defaultImageUrl;

            res = imageUrl
                + "?w=" + customWidth.ToString()
                + "&h=" + customHeight.ToString();
            return res;
        }

        public static string GetPreviewSrc2(string imageUrl, PhotoSize photoSize)
        {
            string res = "";
            int width = GetPhotoSizeValue(photoSize).Width;
            res = GetPreviewSrc2(imageUrl, width, width);
            return res;
        }

        public static string GetPreviewSrc(string imageUrl, string previewSize, int customWidth)
        {
            string res = "";
            res = VirtualPathUtility.ToAbsolute("~/")
                + "Handlers/ImageHandler.ashx"
                + "?size=" + previewSize
                + "&width=" + customWidth.ToString()
                + "&imageUrl=" + imageUrl;
            return res;
        }

        public static string GetPreviewSrc(string imageUrl, PhotoSize photoSize, int customWidth)
        {
            string res = "";
            res = GetPreviewSrc(imageUrl, GetPhotoSizeCode(photoSize), customWidth);
            return res;
        }

        public static string GetFileIconSrc(FileMetaInfo file)
        {
            return GetFileIconSrc(file, false);
        }

        public static string GetFileIconSrc(FileMetaInfo file, bool imageResize = false)
        {
            string res = "";
            switch (file.FileExtension.ToLower())
            {
                case ".jpg":
                case ".jpeg":
                case ".png":
                    //preview
                    if (imageResize)
                        res = PhotoManager.GetPreviewSrc2(file.FileUrl, PhotoSize.Small);
                    else
                        res = PhotoManager.GetPreviewSrc(file.FileUrl, PhotoSize.Small, 0);
                    break;

                default:
                    //icon
                    res = Utility.GetThemedImageSrc(
                        "explorer/file-" + file.FileExtension.ToLower().Replace(".", "") + ".png", "",
                        "explorer/file.png");
                    break;
            }
            return res;
        }

        #endregion
    }
}