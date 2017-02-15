using Newtonsoft.Json;
using PigeonCms;
using PigeonCms.Controls.ItemsAdmin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Security.AccessControl;
using System.Web;
using System.Linq;
using PigeonCms.Core.Helpers;

namespace PigeonCms.Controls.ItemsAdmin
{
    public abstract class AbstractUploadHandler : IHttpHandler
    {
        protected abstract string Prefix { get; }
        protected abstract bool IsImage { get; }
        protected abstract bool IsEncrypted { get; }

        public virtual void ProcessRequest(HttpContext context)
        {
            UploadParameters parameters = null;
            if (IsEncrypted)
            {
                parameters = JsonConvert.DeserializeObject<UploadParameters>(Utility.Encryption.Decrypt(UrlUtils.Base64Decode(context.Request.Params["parameters"]), Config.EncryptKey));
            }
            else
            {
                parameters = JsonConvert.DeserializeObject<UploadParameters>(UrlUtils.Base64Decode(context.Request.Params["parameters"]));
            }

            if (context.Request["action"] == "preview")
            {
                Preview(context, parameters);
                return;
            }

            if (context.Request["action"] == "previewurl")
            {
                PreviewUrl(context, parameters);
                return;
            }

            FileUploadModernProvider.UploadResultEnum uploadResult = FileUploadModernProvider.UploadResultEnum.GenericError;
            UploadHandlerResult handlerResult = new UploadHandlerResult { Status = false };

            context.Response.ContentType = "text/javascript";

            try
            {
                if (context.Request.Files.Count <= 0)
                {
                    handlerResult.Status = false;
                    handlerResult.Message = Utility.GetLabel("UploadFile_FileNotReceived", "No file received");
                }
                else
                {
                    HttpPostedFile file = context.Request.Files[0];
                    uploadResult = Upload(context, file, parameters);
                    if (uploadResult == FileUploadModernProvider.UploadResultEnum.Success)
                    {
                        handlerResult.Status = true;
                        handlerResult.FileName = file.FileName;
                    }

                    switch (uploadResult)
                    {
                        case FileUploadModernProvider.UploadResultEnum.FileTooBig:
                            handlerResult.Message = Utility.GetLabel("UploadFile_FileTooBig", "File exceed size limits");
                            break;
                        case FileUploadModernProvider.UploadResultEnum.FileNotAllowed:
                            handlerResult.Message = Utility.GetLabel("UploadFile_FileNotAllowed", "File type is not allowed");
                            break;
                        case FileUploadModernProvider.UploadResultEnum.GenericError:
                            handlerResult.Message = Utility.GetLabel("UploadFile_GenericError", "Generic error occured");
                            break;
                        case FileUploadModernProvider.UploadResultEnum.Success:
                            handlerResult.Preview = GetPreviewUrl(context, parameters);
                            break;
                        default:
                            break;
                    }
                }

                context.Response.Write(JsonConvert.SerializeObject(handlerResult));
            }
            catch (Exception ex)
            {
                context.Response.Write(JsonConvert.SerializeObject(new UploadHandlerResult
                {
                    Status = false,
                    Message = ex.Message
                }));
            }
            finally
            {
                context.Response.OutputStream.Dispose();
            }
        }

        public FileUploadModernProvider.UploadResultEnum Upload(HttpContext context, HttpPostedFile file, UploadParameters parameters)
        {
            string extension = FilesHelper.GetExtensionFromMime(file.ContentType);
            string tmpFileName = Guid.NewGuid().ToString() + "." + extension;

            try
            {
                if (!string.IsNullOrWhiteSpace(parameters.AllowedFileTypes))
                {
                    List<string> fileTypes = new List<string>(parameters.AllowedFileTypes.Split(','));
                    if (!fileTypes.Contains("*") && !fileTypes.Contains(extension))
                        return FileUploadModernProvider.UploadResultEnum.FileNotAllowed;
                }

                if (parameters.MaxFileSize > 0 && file.ContentLength > (parameters.MaxFileSize * 1024))
                {
                    return FileUploadModernProvider.UploadResultEnum.FileTooBig;
                }

                string tmpBasePath = ConfigurationManager.AppSettings["tmpUploadPath"];

                if (string.IsNullOrWhiteSpace(tmpBasePath) || context.Request.IsLocal)
                    tmpBasePath = Path.GetTempPath();

                if (tmpBasePath.StartsWith("~"))
                    tmpBasePath = context.Server.MapPath(tmpBasePath);

                string tmpFilePath = Path.Combine(tmpBasePath, tmpFileName);
                file.SaveAs(tmpFilePath);
                context.Session[Prefix + "_" + parameters.UniqueID] = tmpFilePath;
                context.Session[Prefix + "_" + parameters.UniqueID + "_RealName"] = file.FileName;
                context.Session[Prefix + "_" + parameters.UniqueID + "_Changes"] = "__changed__";
                context.Session.Remove(Prefix + "_" + parameters.UniqueID + "_Deleted");
                return FileUploadModernProvider.UploadResultEnum.Success;
            }
            catch
            {
                return FileUploadModernProvider.UploadResultEnum.GenericError;
            }
        }

        public string GetPreviewUrl(HttpContext context, UploadParameters parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters.UniqueID))
                return string.Empty;

            string tmpFileName = Convert.ToString(context.Session[Prefix + "_" + parameters.UniqueID]);
            if (string.IsNullOrWhiteSpace(tmpFileName))
                return string.Empty;

            string tmpBasePath = ConfigurationManager.AppSettings["tmpUploadPath"];

            if (string.IsNullOrWhiteSpace(tmpBasePath) || context.Request.IsLocal)
                tmpBasePath = Path.GetTempPath();

            if (tmpBasePath.StartsWith("~"))
                tmpBasePath = context.Server.MapPath(tmpBasePath);

            string tmpFilePath = Path.Combine(tmpBasePath, tmpFileName);
            if (!File.Exists(tmpFilePath))
                return string.Empty;

            int thumbWidth = Convert.ToInt32(ConfigurationManager.AppSettings["thumbWidth"]);
            int thumbHeight = Convert.ToInt32(ConfigurationManager.AppSettings["thumbHeight"]);

            if (!string.IsNullOrWhiteSpace(context.Request["width"]))
                int.TryParse(context.Request["width"], out thumbWidth);

            if (!string.IsNullOrWhiteSpace(context.Request["height"]))
                int.TryParse(context.Request["height"], out thumbHeight);

            return context.Request.Url.GetLeftPart(UriPartial.Authority) + "/Handlers/GetImageHandler.ashx?b64encoded=1&filename=" + UrlUtils.Base64Encode(tmpFilePath) + "&ts=" + DateTime.Now.Ticks;
        }

        public static bool HasChanges(HttpContext context, string prefix, string uniqueID)
        {
            string key = prefix + "_" + uniqueID + "_Changes";
            object value = context.Session[key];
            return (value != null && Convert.ToString(value) == "__changed__");
        }

        public static string GetFilePath(HttpContext context, string prefix, string uniqueID)
        {
            string key = prefix + "_" + uniqueID;
            return Convert.ToString(context.Session[key]);
        }

        public void PreviewUrl(HttpContext context, UploadParameters parameters)
        {
            context.Response.ContentType = "text/javascript";

            try
            {
                context.Response.Write(JsonConvert.SerializeObject(new UploadHandlerResult
                {
                    Status = true,
                    Preview = GetPreviewUrl(context, parameters)
                }));
            }
            catch (Exception ex)
            {
                context.Response.Write(JsonConvert.SerializeObject(new UploadHandlerResult
                {
                    Status = false,
                    Message = ex.Message
                }));
            }
            finally
            {
                context.Response.OutputStream.Dispose();
            }
        }

        // OLD METHOD
        public void Preview(HttpContext context, UploadParameters parameters)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(parameters.UniqueID))
                    return;

                string tmpFileName = Convert.ToString(context.Session[Prefix + "_" + parameters.UniqueID]);
                if (tmpFileName == null)
                    return;

                string tmpBasePath = ConfigurationManager.AppSettings["tmpUploadPath"];

                if (string.IsNullOrWhiteSpace(tmpBasePath) || context.Request.IsLocal)
                    tmpBasePath = Path.GetTempPath();

                if (tmpBasePath.StartsWith("~"))
                    tmpBasePath = context.Server.MapPath(tmpBasePath);

                if (!Directory.Exists(tmpBasePath))
                {
                    Directory.CreateDirectory(tmpBasePath);
                    DirectorySecurity ds = Directory.GetAccessControl(tmpBasePath);
                    ds.SetAccessRuleProtection(false, true);
                    Directory.SetAccessControl(tmpBasePath, ds);
                }

                string tmpFilePath = Path.Combine(tmpBasePath, tmpFileName);
                FileInfo tmpFile = new FileInfo(tmpFilePath);

                if (IsImage)
                {
                    int thumbWidth = Convert.ToInt32(ConfigurationManager.AppSettings["thumbWidth"]);
                    int thumbHeight = Convert.ToInt32(ConfigurationManager.AppSettings["thumbHeight"]);

                    if (!string.IsNullOrWhiteSpace(context.Request["width"]))
                        int.TryParse(context.Request["width"], out thumbWidth);

                    if (!string.IsNullOrWhiteSpace(context.Request["height"]))
                        int.TryParse(context.Request["height"], out thumbHeight);

                    if (tmpFile.Extension == ".svg")
                    {
                        context.Response.Clear();
                        context.Response.ContentType = "image/svg+xml";
                        context.Response.Cache.SetCacheability(HttpCacheability.Public);
                        context.Response.WriteFile(tmpFilePath);
                        context.Response.Flush();
                        return;
                    }

                    ItemsAdminHelper.WriteImageToResponse(context.Response, tmpFilePath, thumbWidth, thumbHeight, ImageResizer.OutputFormat.Jpeg, ImageResizer.FitMode.Pad);
                }
                else
                {
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "inline; filename=" + context.Session[Prefix + "_" + parameters.UniqueID + "_RealName"]);
                    context.Response.AddHeader("Content-Length", tmpFile.Length.ToString());
                    context.Response.ContentType = FilesHelper.GetMimeFromExtension(tmpFile.Extension);
                    context.Response.WriteFile(tmpFilePath);
                }
            }
            catch (Exception ex)
            {
                context.Response.Clear();
                context.Response.ContentType = "text/plain";
                context.Response.Write(ex.Message);
                return;
            }
        }

        public bool IsReusable
        {
            get { return false; }
        }

        public static string GetExtension(HttpContext context, string prefix, string uniqueID)
        {
            string tmpFilePath = /*Path.GetTempPath() +*/ Convert.ToString(context.Session[prefix + "_" + uniqueID]);
            if (!File.Exists(tmpFilePath))
                return string.Empty;

            return tmpFilePath.Split('.').Last();
        }

        public static bool IsDeleted(HttpContext context, string prefix, string uniqueID)
        {
            return (context.Session[prefix + "_" + uniqueID + "_Deleted"] != null);
        }

        public static string GetRealName(HttpContext context, string prefix, string uniqueID)
        {
            return Convert.ToString(context.Session[prefix + "_" + uniqueID + "_RealName"]);
        }

        public static void PerformDelete(HttpContext context, string prefix, string uniqueID)
        {
            if (context.Session[prefix + "_" + uniqueID] != null)
            {
                string tmpFilePath = /*Path.GetTempPath() + */Convert.ToString(context.Session[prefix + "_" + uniqueID]);
                if (File.Exists(tmpFilePath))
                    File.Delete(tmpFilePath);

                context.Session.Remove(prefix + "_" + uniqueID);
            }/*
            else
            {
                if (string.IsNullOrWhiteSpace(FilePath))
                    return;

                string filePath = FilePath;
                if (filePath.StartsWith("~") || filePath.StartsWith("/"))
                    filePath = Server.MapPath(filePath);

                if (File.Exists(filePath))
                    File.Delete(filePath);
            }*/
        }

        public static void SaveTo(HttpContext context, string prefix, string uniqueID, string filePath)
        {
            string tmpFilePath = /*Path.GetTempPath() +*/ Convert.ToString(context.Session[prefix + "_" + uniqueID]);
            if (File.Exists(tmpFilePath))
            {
                string directory = FilesHelper.GetDirectory(filePath);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                File.Move(tmpFilePath, filePath);

                FileSecurity fs = File.GetAccessControl(filePath);
                fs.SetAccessRuleProtection(false, false);
                File.SetAccessControl(filePath, fs);

                context.Session.Remove(prefix + "_" + uniqueID);
                context.Session.Remove(prefix + "_" + uniqueID + "_RealName");
                context.Session.Remove(prefix + "_" + uniqueID + "_Deleted");
                context.Session.Remove(prefix + "_" + uniqueID + "_Changes");
            }
        }

        public static void SetFile(HttpContext context, string prefix, string uniqueID, string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                context.Session.Remove(prefix + "_" + uniqueID);
                context.Session.Remove(prefix + "_" + uniqueID + "_RealName");
                context.Session.Remove(prefix + "_" + uniqueID + "_Deleted");
                context.Session.Remove(prefix + "_" + uniqueID + "_Changes");
                return;
            }

            string _filePath = filePath.StartsWith("~") ? context.Server.MapPath(filePath) : filePath;
            _filePath = _filePath.StartsWith("/public") ? context.Server.MapPath("~" + _filePath) : _filePath;
            if (File.Exists(_filePath))
            {
                context.Session[prefix + "_" + uniqueID] = _filePath;
                context.Session[prefix + "_" + uniqueID + "_RealName"] = new FileInfo(_filePath).Name;
                context.Session.Remove(prefix + "_" + uniqueID + "_Deleted");
                context.Session.Remove(prefix + "_" + uniqueID + "_Changes");
            }
        }
    }
}
