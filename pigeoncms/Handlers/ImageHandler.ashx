<%@ WebHandler Language="C#" Class="ImageHandler" %>

using System;
using System.IO;
using System.Web;
using PigeonCms;
using System.Drawing;
using System.Drawing.Imaging;

public class ImageHandler : IHttpHandler
{

    public bool IsReusable
    {
        get { return true; }
    }

    private bool ThumbCallback()
    {
        return false;
    }

    private void getThumb()
    {

    }
    
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "image/jpeg";
        context.Response.Cache.SetCacheability(HttpCacheability.Public);
        context.Response.BufferOutput = false;

        bool keepOriginalSize = true;
        string imagePath = "";
        string imageUrl = "";
        string defaultImagePath = HttpContext.Current.Server.MapPath("~/Images/blank.jpg");
        
        if (!string.IsNullOrEmpty(context.Request.QueryString["imageUrl"]))
        {
            imageUrl = context.Request.QueryString["imageUrl"];
        }
        try
        {
            imagePath = HttpContext.Current.Server.MapPath(imageUrl);
        }
        catch { }
        if (!System.IO.File.Exists(imagePath))
        {
            imagePath = defaultImagePath;
        }
        //if (imagePath.ToLower().EndsWith(".png"))
        //    context.Response.ContentType = "image/png";                

        //Setup the Size Parameter
        PhotoSize size = PhotoSize.Original;
        int customSize = 0;
        if (!string.IsNullOrEmpty(context.Request.QueryString["Size"]))
        {
            switch (context.Request.QueryString["size"].ToUpper())
            {
                case "CUSTOM":
                    size = PhotoSize.Custom;
                    keepOriginalSize = false;
                    break;
                case "S":
                    size = PhotoSize.Small;
                    keepOriginalSize = false;
                    break;
                case "M":
                    size = PhotoSize.Medium;
                    keepOriginalSize = false;
                    break;
                case "L":
                    size = PhotoSize.Large;
                    keepOriginalSize = false;
                    break;
                case "XL":
                    size = PhotoSize.XLarge;
                    keepOriginalSize = false;
                    break;
                case "PERCENTAGE":
                    size = PhotoSize.Percentage;
                    keepOriginalSize = false;
                    break;                    
                default:
                    size = PhotoSize.Original;
                    keepOriginalSize = true;
                    break;
            }
        }
        
        System.Drawing.Size imageSize = new System.Drawing.Size();
        imageSize = PhotoManager.GetPhotoSizeValue(size);
        if (!string.IsNullOrEmpty(context.Request.QueryString["width"]) 
            && (size == PhotoSize.Custom || size == PhotoSize.Percentage))
        {
            int.TryParse(context.Request.QueryString["width"].ToString(), out customSize);
            if (customSize > 0)
            {
                imageSize.Width = customSize;
            }
        }

        try
        {
            using (Stream fileStream = File.OpenRead(imagePath))
            {
                using (System.Drawing.Image tmpImage = Image.FromStream(fileStream))
                {
                    if (!keepOriginalSize && (imageSize.Width + imageSize.Height) > 0)
                    {
                        if (size == PhotoSize.Percentage)
                        {
                            //calculate percentage
                            if (imageSize.Height > 0)
                                imageSize.Height = Convert.ToInt32( imageSize.Height * tmpImage.Height / 100 );
                            if (imageSize.Width > 0)
                                imageSize.Width = Convert.ToInt32( tmpImage.Width * imageSize.Width / 100 );
                        }
                        //calculate reamaining side
                        if (imageSize.Height == 0)
                            imageSize.Height = Convert.ToInt32((imageSize.Width * tmpImage.Height) / tmpImage.Width);
                        if (imageSize.Width == 0)
                            imageSize.Width = Convert.ToInt32((imageSize.Height * tmpImage.Width) / tmpImage.Height);
                    }
                    
                    //tnx to http://www.glennjones.net/Post/799/Highqualitydynamicallyresizedimageswithnet.htm                 
                    Bitmap thumbnail = new Bitmap(imageSize.Width, imageSize.Height, PixelFormat.Format32bppArgb);
                    Graphics graphic = Graphics.FromImage(thumbnail);

                    graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    graphic.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    graphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    graphic.DrawImage(tmpImage, 0, 0, imageSize.Width, imageSize.Height);
                    

                    //contentType == "image/jpeg" 
                    //ImageCodecInfo[] info =  System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
                    //EncoderParameters encoderParameters;
                    //encoderParameters = new EncoderParameters(1);
                    //encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
                    //thumbnail.Save(context.Response.OutputStream, info[1], encoderParameters);
                    //thumbnail.Save(context.Response.OutputStream, ImageFormat.Png);

                    switch (Path.GetExtension(imagePath).ToLower())
                    {
                        case ".png":
                            //tnx to http://stackoverflow.com/questions/5629251/c-sharp-outputting-image-to-response-output-stream-giving-gdi-error
                            using (MemoryStream ms = new MemoryStream())
                            {
                                thumbnail.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                ms.WriteTo(context.Response.OutputStream);
                            }
                            break;
                        case ".jpg":
                        default:
                            thumbnail.Save(context.Response.OutputStream, ImageFormat.Jpeg);                            
                            break;
                    }
                }
            }
        }
        catch(Exception ex)
        {
            //var fakeModule = new PigeonCms.Module(); //used to log errors
            //int moduleId = 0;
            //int.TryParse(
            //    PigeonCms.AppSettingsManager.GetValue("DroidCatalogue.ActivationsAdmin_moduleId"), out moduleId);
            
            //fakeModule.Id = moduleId;
            //fakeModule.UseLog = PigeonCms.Utility.TristateBool.True;
            //PigeonCms.LogProvider.Write(fakeModule,
            //    "ImageHandler.ashx: " +
            //    "message:[" + ex.ToString() + "]; ",
            //    PigeonCms.TracerItemType.Error);
        }
        finally
        {
            context.Response.OutputStream.Dispose();
        }
    }
}