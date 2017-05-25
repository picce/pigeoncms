<%@ WebHandler Language="C#" Class="GetImageHandler" %>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using PigeonCms.Core;

public class GetImageHandler : IHttpHandler
{
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    public void ProcessRequest(HttpContext context)
    {
        // Recupero parametri da querystring e imposto valori default
        RequestParam param = ParseRequestParams(context);

        try
        {
            // Verifico se è stato ricevuto un nome file valido
            if (string.IsNullOrWhiteSpace(param.FileName))
            {
                RenderErrorImage(context, param);
                return;
            }

            // Lettura immagine (da locale o tramite HTTP)
            ReadSourceImage(context, param);

            CropScaleImage(param);

            RenderImage(context, param);

            context.Response.Cache.SetCacheability(HttpCacheability.Public);
            context.Response.Cache.SetExpires(DateTime.Now.AddMonths(1));

            if (param.DeleteAfter == 1)
            {
                try
                {
                    string filePath = param.FileName;
                    if (!File.Exists(filePath) && filePath.StartsWith("~"))
                        filePath = context.Server.MapPath(param.FileName);

                    if (File.Exists(filePath))
                        File.Delete(filePath);
                }
                catch { }
            }
        }
        catch (Exception ex)
        {
            RenderErrorImage(context, param);
        }
    }

    protected string GetRequestParamSafe(HttpContext context, string name)
    {
        object val = context.Request.Params[name];
        if (val == null)
            return string.Empty;

        return Convert.ToString(val).Trim();
    }

    protected string GetRequestParam(HttpContext context, string name)
    {
        string[] values = GetRequestParamSafe(context, name).Split(',');
        if (values.Length <= 0)
            return string.Empty;

        return values[0].Trim();
    }

    protected int GetRequestParam(HttpContext context, string name, int defaultValue)
    {
        int value;
        string[] values = GetRequestParamSafe(context, name).Split(',');
        if (values.Length <= 0)
            return defaultValue;

        bool converted = int.TryParse(values[0], out value);
        return converted ? value : defaultValue;
    }

    protected RequestParam ParseRequestParams(HttpContext context)
    {
        RequestParam result = new RequestParam()
        {
            FileName = GetRequestParam(context, "filename"),
            Format = GetRequestParam(context, "format"),
            Height = GetRequestParam(context, "height", 0),
            Width = GetRequestParam(context, "width", 0),
            CropCenter = GetRequestParam(context, "cropcenter"),
            Mode = GetRequestParam(context, "mode"),
            FitSide = GetRequestParam(context, "side"),
            DeleteAfter = GetRequestParam(context, "deleteafter", 0),
            Base64Encoded = GetRequestParam(context, "b64encoded", 0)
        };

        if (string.IsNullOrWhiteSpace(result.Mode) || !(new List<string>() { "fit", "crop", "scale", "fitcrop" }.Contains(result.Mode)))
            result.Mode = "fit";

        if (string.IsNullOrWhiteSpace(result.FitSide) || !(new List<string>() { "width", "height" }.Contains(result.FitSide)))
            result.FitSide = "width";

        if (string.IsNullOrWhiteSpace(result.CropCenter))
            result.CropCenter = "c";

        if (string.IsNullOrWhiteSpace(result.Format))
            result.Format = "png";

        return result;
    }

    protected void ReadSourceImage(HttpContext context, RequestParam param)
    {
        ReadSourceImage(context, param, param.FileName);
    }

    protected void ReadSourceImage(HttpContext context, RequestParam param, string fileName)
    {
        string _fileName = param.Base64Encoded == 1 ? PigeonCms.Core.Helpers.UrlUtils.Base64Decode(fileName) : fileName;

        if (!_fileName.StartsWith("http://") && !_fileName.StartsWith("https://"))
        {
            string path = _fileName;
            if (!File.Exists(path))
                path = context.Server.MapPath(_fileName);

            if (!File.Exists(path))
                return;

            MemoryStream ms = new MemoryStream();
            using (Stream input = File.OpenRead(path))
            {
                input.CopyTo(ms);
            }
            ms.Position = 0;
            param.SourceImage = Image.FromStream(ms);
        }
        else
        {
            HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create(_fileName);
            hwr.Timeout = 2500;
            WebResponse wr = hwr.GetResponse();
            StreamReader sr = new StreamReader(wr.GetResponseStream(), Encoding.Default);
            Stream stream = sr.BaseStream;
            param.SourceImage = Image.FromStream(stream);
            sr.Close();
        }

        param.InitialWidth = param.SourceImage.Width;
        param.InitialHeight = param.SourceImage.Height;
    }

    protected void RenderImage(HttpContext context, RequestParam param)
    {
        if (param.SourceImage == null)
            return;

        if (param.Format == "jpg" || param.Format == "jpeg")
        {
            context.Response.ContentType = "image/jpeg";
            param.SourceImage.Save(context.Response.OutputStream, ImageFormat.Jpeg);
        }
        else
        {
            context.Response.ContentType = "image/png";
            using (MemoryStream ms = new MemoryStream())
            {
                param.SourceImage.Save(ms, ImageFormat.Png);
                context.Response.Clear();
                context.Response.Buffer = true;
                context.Response.BinaryWrite(ms.ToArray());
                context.Response.Flush();
            }
        }
    }

    protected void RenderErrorImage(HttpContext context, RequestParam param)
    {
        try
        {
            int width = param.Width > 0 ? param.Width : (param.InitialWidth > 0 ? param.InitialWidth : 800);
            int height = param.Height > 0 ? param.Height : (param.InitialHeight > 0 ? param.InitialHeight : 600);

            ReadSourceImage(context, param, string.Format("http://placehold.it/{0}x{1}?text=Error!", width, height));
            RenderImage(context, param);
        }
        catch (Exception ex)
        {
            //ErrorUtils.Warning(ex);
        }
    }

    private Size CalculateCanvasSize(RequestParam param)
    {
        Size canvasSize = new Size();

        canvasSize.Width = param.Width > 0 ? param.Width : 0;
        canvasSize.Height = param.Height > 0 ? param.Height : 0;

        if (canvasSize.Width == 0)
        {
            switch (param.Mode)
            {
                case "scale":
                case "fit":
                case "fitcrop":
                    canvasSize.Width = param.InitialWidth * canvasSize.Height / param.InitialHeight;
                    break;
                case "crop":
                    canvasSize.Width = param.InitialWidth;
                    break;
            }
        }
        else if (canvasSize.Height == 0)
        {
            switch (param.Mode)
            {
                case "scale":
                case "fit":
                case "fitcrop":
                    canvasSize.Height = canvasSize.Width * param.InitialHeight / param.InitialWidth;
                    break;
                case "crop":
                    canvasSize.Height = param.InitialHeight;
                    break;
            }
        }

        return canvasSize;
    }

    private bool IsSameRatio(Size sizeA, Size sizeB)
    {
        int ratioA = sizeA.Width / sizeA.Height;
        int ratioB = sizeB.Width / sizeB.Height;

        return (ratioA >= 1 && ratioB >= 1) || (ratioA < 1 && ratioB < 1);
    }

    private Point GetCropCenter(RequestParam param, Size canvasSize, Size region)
    {
        Point result = new Point();
        int xDelta = region.Width - canvasSize.Width;
        int yDelta = region.Height - canvasSize.Height;

        switch (param.CropCenter)
        {
            case "tl": // Top Left
                result.X = 0;
                result.Y = 0;
                break;
            case "t": // Top
                result.X = -xDelta / 2;
                result.Y = 0;
                break;
            case "tr": // Top Right
                result.X = -xDelta;
                result.Y = 0;
                break;
            case "l": // Left
                result.X = 0;
                result.Y = -yDelta / 2;
                break;
            case "r": // Right
                result.X = -xDelta;
                result.Y = -yDelta / 2;
                break;
            case "bl": // Bottom Left
                result.X = 0;
                result.Y = -yDelta;
                break;
            case "b": // Bottom
                result.X = -xDelta / 2;
                result.Y = -yDelta;
                break;
            case "br": // Bottom Right
                result.X = -xDelta;
                result.Y = -yDelta;
                break;
            default:
            case "c": // Center
                result.X = -xDelta / 2;
                result.Y = -yDelta / 2;
                break;
        }

        return result;
    }

    private Rectangle CalculateImageRegion(RequestParam param, Size canvasSize)
    {
        Rectangle imageRegion = new Rectangle();

        if (param.Mode == "scale")
        {
            imageRegion.Location = new Point(0, 0);
            imageRegion.Size = canvasSize;
        }
        else if (param.Mode == "fit")
        {
            imageRegion.Width = canvasSize.Width;
            imageRegion.Height = canvasSize.Width * param.InitialHeight / param.InitialWidth;

            if (imageRegion.Height > canvasSize.Height)
            {
                imageRegion.Height = canvasSize.Height;
                imageRegion.Width = param.InitialWidth * canvasSize.Height / param.InitialHeight;
            }

            imageRegion.X = (canvasSize.Width - imageRegion.Width) / 2;
            imageRegion.Y = (canvasSize.Height - imageRegion.Height) / 2;
        }
        else if (param.Mode == "crop")
        {
            imageRegion.Size = new Size(param.InitialWidth, param.InitialHeight);
            imageRegion.Location = GetCropCenter(param, canvasSize, imageRegion.Size);
        }
        else if (param.Mode == "fitcrop")
        {
            if (param.FitSide == "width")
            {
                imageRegion.Width = canvasSize.Width;
                imageRegion.Height = canvasSize.Width * param.InitialHeight / param.InitialWidth;
                imageRegion.X = 0;
                imageRegion.Y = (canvasSize.Height - imageRegion.Height) / 2;
            }
            else
            {
                imageRegion.Height = canvasSize.Height;
                imageRegion.Width = param.InitialWidth * canvasSize.Height / param.InitialHeight;
                imageRegion.Y = 0;
                imageRegion.X = (canvasSize.Width - imageRegion.Width) / 2;
            }
        }

        return imageRegion;
    }

    private void CropScaleImage(RequestParam param)
    {
        if (param.Width <= 0 && param.Height <= 0)
            return;

        // Calcolo dimensioni canvas finale
        Size canvasSize = CalculateCanvasSize(param);

        // Calcolo dimensioni immagine finale
        Rectangle imageRegion = CalculateImageRegion(param, canvasSize);

        Bitmap scaledBitmap = new Bitmap(canvasSize.Width, canvasSize.Height, PixelFormat.Format32bppArgb);
        scaledBitmap.SetResolution(72, 72);
        scaledBitmap.MakeTransparent();

        Graphics scaledGraphics = Graphics.FromImage(scaledBitmap);
        scaledGraphics.Clear(Color.Transparent);
        scaledGraphics.CompositingQuality = CompositingQuality.HighQuality;
        scaledGraphics.SmoothingMode = SmoothingMode.HighQuality;
        scaledGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        scaledGraphics.DrawImage(param.SourceImage, imageRegion);
        scaledGraphics.Dispose();

        param.SourceImage = scaledBitmap;
    }

    public class RequestParam
    {
        public RequestParam()
        {
            FileName = string.Empty;
            Mode = string.Empty;
            InitialWidth = 0;
            InitialHeight = 0;
            FitSide = string.Empty;
            Format = "png";
            SourceImage = null;
            CropCenter = "c";
            DeleteAfter = 0;
            Base64Encoded = 0;
        }

        public Image SourceImage { get; set; }
        public string FileName { get; set; }

        public string FitSide { get; set; }
        public string Mode { get; set; }

        public int InitialWidth { get; set; }
        public int InitialHeight { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public string Format { get; set; }

        // bl = bottom-left, b = bottom, br = bottom-right, l = left, c = center; r = right; tl = top-left; t = top; tr = top-right
        public string CropCenter { get; set; }

        public int DeleteAfter { get; set; }
        public int Base64Encoded { get; set; }

        public string Uid
        {
            get
            {
                List<string> tokens = new List<string>();

                tokens.Add(PigeonCms.FilesHelper.GetUrlFileName(FileName));

                if (!string.IsNullOrWhiteSpace(Mode))
                    tokens.Add(Mode);

                if (Height > 0)
                    tokens.Add(Height.ToString());

                if (Width > 0)
                    tokens.Add(Width.ToString());

                tokens.Add(CropCenter.ToString());
                tokens.Add(FitSide.ToString());
                tokens.Add(Mode.ToString());

                return string.Join("_", tokens) + "." + Format;
            }
        }
    }
}