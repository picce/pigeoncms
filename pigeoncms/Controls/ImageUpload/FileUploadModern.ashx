<%@ WebHandler Language="C#" Class="FileUploadModernHandler" %>

using System;
using System.Configuration;
using System.Web;
using PigeonCms;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.IO;
using AQuest.Cecchi.Utils;
using AQuest.PigeonCMS.ItemsAdmin;
using System.Web.SessionState;
using AQuest.PigeonCMS.ItemsAdmin.Uploads;

public class FileUploadModernHandler : IHttpHandler, IRequiresSessionState
{
    [DataContract]
    public class UploadHandlerResult
    {
        [DataMember(Name = "status")]
        public bool Status = true;

        [DataMember(Name = "message")]
        public string Message = "";

        [DataMember(Name = "preview")]
        public string Preview = "";

        [DataMember(Name = "fileName")]
        public string FileName = "";
    }

    public void ProcessRequest(HttpContext context)
    {
        UploadParameters parameters = JsonConvert.DeserializeObject<UploadParameters>(Utility.Encryption.Decrypt(UrlUtils.Base64Decode(context.Request.Params["parameters"]), Config.EncryptKey));

        if (context.Request["action"] == "preview")
        {
            Preview(context, parameters);
            return;
        }

        FileUploadModernProvider.UploadResultEnum uploadResult = FileUploadModernProvider.UploadResultEnum.GenericError;
        UploadHandlerResult handlerResult = new UploadHandlerResult { Status = false };

        context.Response.ContentType = "text/javascript";

        try
        {
            if (context.Request.Files.Count > 0)
            {
                try
                {
                    HttpPostedFile file = context.Request.Files[0];
                    uploadResult = Upload(context, file, parameters);
                    if (uploadResult == FileUploadModernProvider.UploadResultEnum.Success)
                    {
                        handlerResult.Status = true;
                        handlerResult.FileName = file.FileName;
                    }
                }
                catch
                {

                }
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
                default:
                    break;
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

    public void Preview(HttpContext context, UploadParameters parameters)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(parameters.UniqueID))
                return;

            string tmpFileName = Convert.ToString(context.Session["FileUpload_" + parameters.UniqueID]);
            if (tmpFileName == null)
                return;

            string tmpFilePath = Path.GetTempPath() + tmpFileName;
            FileInfo tmpFile = new FileInfo(tmpFilePath);
            context.Response.Clear();
            context.Response.AddHeader("Content-Disposition", "inline; filename=" + context.Session["FileUpload_" + parameters.UniqueID + "_RealName"]);
            context.Response.AddHeader("Content-Length", tmpFile.Length.ToString());
            context.Response.ContentType = FilesManipulationUtils.GetMimeFromExtension(tmpFile.Extension);
            context.Response.WriteFile(tmpFilePath);
        }
        catch
        {
            return;
        }
    }

    public FileUploadModernProvider.UploadResultEnum Upload(HttpContext context, HttpPostedFile file, UploadParameters parameters)
    {
        string extension = FilesManipulationUtils.GetExtensionFromMime(file.ContentType);
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

            string tmpFilePath = Path.Combine(Path.GetTempPath(), tmpFileName);
            file.SaveAs(tmpFilePath);
            context.Session["FileUpload_" + parameters.UniqueID] = tmpFileName;
            context.Session["FileUpload_" + parameters.UniqueID + "_RealName"] = file.FileName;
            context.Session.Remove("FileUpload_" + parameters.UniqueID + "_Deleted");
            return FileUploadModernProvider.UploadResultEnum.Success;
        }
        catch (Exception ex)
        {
            return FileUploadModernProvider.UploadResultEnum.GenericError;
        }
    }

    public bool IsReusable
    {
        get { return true; }
    }
}