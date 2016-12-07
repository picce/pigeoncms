<%@ WebHandler Language="C#" Class="UploadFile" %>

using System;
using System.Web;
using PigeonCms;
using PigeonCms.Core;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;


public class UploadFile : IHttpHandler
{
	[DataContract]
	public class UploadHadlerResult
	{
		[DataMember(Name = "status")]
		public bool Status = true;

		[DataMember(Name = "message")]
		public string Message = "";	
	}
	
    public void ProcessRequest(HttpContext context)
    {
        HttpRequest req = context.Request;
		string res = "";
		var handlerResult = new UploadHadlerResult();
		handlerResult.Status = false;
		var uploadResult = FileUploadModernProvider.UploadResultEnum.GenericError;
		
		//from FormData
		int fileManagerModuleId = 0;
		string sModuleId = Utility.Encryption.Decrypt(context.Request.Form["moduleid"], Config.EncryptKey);
		int.TryParse(sModuleId, out fileManagerModuleId);
		string customFolder = context.Request.Form["foldername"];
		try
		{
			var fileUploadProvider = new FileUploadModernProvider(fileManagerModuleId, customFolder);
			//file upload
			if (req.Files.Count > 0)
			{
				try
				{
					HttpPostedFile file = context.Request.Files[0];
					handlerResult.Status = fileUploadProvider.Upload(file, out uploadResult);
				}
				catch
				{
					handlerResult.Status = false;
					uploadResult = FileUploadModernProvider.UploadResultEnum.GenericError;
				}
			}
			switch (uploadResult)
			{
				case FileUploadModernProvider.UploadResultEnum.Success:
					handlerResult.Status = true;
					break;
				case FileUploadModernProvider.UploadResultEnum.FileTooBig:
					handlerResult.Message = "file too big";
					break;
				case FileUploadModernProvider.UploadResultEnum.FileNotAllowed:
					handlerResult.Message = "file not allowed";
					break;
				case FileUploadModernProvider.UploadResultEnum.GenericError:
					handlerResult.Message = "an error occured";
					break;
				default:
					break;
			}
			res = JsonConvert.SerializeObject(handlerResult);
			context.Response.ContentType = "text/javascript";
			context.Response.Write(res);			
		}
		catch (Exception e)
		{
		}
		finally
		{
			context.Response.OutputStream.Dispose();			
		}
    }


 
    public bool IsReusable 
	{
        get { return true; }
    }

}