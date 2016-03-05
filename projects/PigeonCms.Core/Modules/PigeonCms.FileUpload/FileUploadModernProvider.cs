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
	public class FileUploadModernProvider
	{
		PigeonCms.Module uploadModule = new PigeonCms.Module();
		Dictionary<string, string> moduleParams = new Dictionary<string, string>();
		Dictionary<string, string> fileFoldersList = new Dictionary<string, string>();
		string customFolder = "";


		public enum FileNameTypeEnum
		{
			KeepOriginalName = 0,
			PrefixOriginalName = 1,
			PrefixCounter = 2,
			ForceFileName = 3
		}


		public enum UploadResultEnum
		{
			Success = 0,
			FileTooBig, 
			FileNotAllowed,
			GenericError
		}

		public static class IFolderTypes
		{
			public const string Custom = "custom";
			public const string Documents = "documents";
			public const string ItemsImages = "items-images";
			public const string ItemsFiles = "items-files";
			public const string CategoriesImages = "categories-images";
			public const string CategoriesFiles = "categories-files";
			public const string SectionsImages = "sections-images";
			public const string SectionsFiles = "sections-files";
			public const string Temp = "temp";
		}


		public FileUploadModernProvider(int fileManagerModuleId, string customFolder = "")
		{
			//sec checks
			try
			{
				if (fileManagerModuleId > 0)
					uploadModule = new PigeonCms.ModulesManager(true, false).GetByKey(fileManagerModuleId);

				if (uploadModule.Id > 0 && uploadModule.ModuleFullName == "PigeonCms.FilesManagerModern")
				{
					uploadModule.UseLog = Utility.TristateBool.True;
					this.customFolder = customFolder;
					this.moduleParams = uploadModule.Params;
					initFileFoldersList(fileFoldersList);

					if (string.IsNullOrEmpty(this.FileFolderType))
						throw new ArgumentException("Empty FileFolderType", "FileFolderType");

					if (!fileFoldersList.ContainsKey(this.FileFolderType))
						throw new ArgumentException("Invalid FileFolderType", "FileFolderType");

					if (!checkGrants(this.FileFolderType, this.customFolder))
						throw new ArgumentException("Path not allowed");

					this.allowed = true;
				}
				else
					throw new ArgumentException("Invalid file manager module");
			}
			catch (Exception ex)
			{
				var fakeModule = new Module();
				fakeModule.UseLog = Utility.TristateBool.True;
				fakeModule.ModuleNamespace = "PigeonCms";
				fakeModule.ModuleName = "FilesManagerModern";

				string logMessage = "FileUploadModernProvider() - "
					+ "fileManagerModuleId=" + fileManagerModuleId + "; "
					+ "customFolder=" + customFolder + "; "
					+ "FileFolderType=" + this.FileFolderType + "; "
					+ "EXC=" + ex.ToString() + "; ";

				LogProvider.Write(fakeModule, logMessage, TracerItemType.Alert);
			}

		}


		public bool Upload(HttpPostedFile file, out UploadResultEnum result)
		{
			bool res = true;
			result = UploadResultEnum.Success;
			string logMessage = "upload file;";
			TracerItemType logType = TracerItemType.Debug;
			string filename = file.FileName;
			

			switch (this.FileNameType)
			{
				case FileNameTypeEnum.PrefixOriginalName:
					filename = this.FilePrefix + file.FileName;
					break;
				//case FileNameTypeEnum.PrefixCounter:
				//	filename = this.FilePrefix + fileCounter.ToString() + Path.GetExtension(uploadField.FileName);
				//	break;
				case FileNameTypeEnum.ForceFileName:
					filename = this.ForcedFilename + Path.GetExtension(file.FileName);
					break;
				case FileNameTypeEnum.KeepOriginalName:
				default:
					break;
			}

			filename = sanitizeFilename(filename);
			logMessage += "filename=" + filename + "; ";

			try
			{
				string finalFolder = FilesHelper.MapPathWhenVirtual(this.GetFinalPath);
				logMessage += "finalFolder=" + finalFolder + "; ";

				DirectoryInfo dir = new DirectoryInfo(finalFolder);
				if (!dir.Exists)
				{
					logMessage += "folder created; ";
					dir.Create();
				}

				if (res && !checkExtensions(filename))
				{
					res = false;
					result = UploadResultEnum.FileNotAllowed;
					logType = TracerItemType.Warning;
					logMessage += "invalid extension; ";
				}

				if (res && !checkFileSize(file.ContentLength))
				{
					res = false;
					result = UploadResultEnum.FileTooBig;
					logType = TracerItemType.Warning;
					logMessage += "file too big; ";
				}

				if (res)
				{
					var fileFinalPath = Path.Combine(finalFolder, filename);
					logMessage += "fileFinalPath=" + fileFinalPath + "; ";
					switch (Utility.GetFileExt(filename).ToLower())
					{
						case "jpg":
						case "png":
						case "gif":
							saveImageFile(new Bitmap(file.InputStream), fileFinalPath);
							break;

						default:
							file.SaveAs(fileFinalPath);
							break;
					}
					result = UploadResultEnum.Success;
				}
			}
			catch (Exception ex)
			{
				res = false;
				result = UploadResultEnum.GenericError;
				logType = TracerItemType.Error;
				logMessage += "error="+ ex.ToString() +"; ";
			}

			LogProvider.Write(uploadModule, logMessage, logType);

			return res;
		}


		#region parameters

		private bool allowed = false;
		public bool Allowed
		{
			get { return allowed; }
		}

		/// <summary>
		/// Resize images to given size
		/// </summary>
		public int CustomWidth
		{
			get { return getIntParam(moduleParams, "CustomWidth"); }
		}

		/// <summary>
		/// Resize images to given size
		/// </summary>
		public int CustomHeight
		{
			get { return getIntParam(moduleParams, "CustomHeight"); }
		}

		/// <summary>
		/// Number of files
		/// </summary>
		public int NumOfFilesAllowed
		{
			get
			{
				return getIntParam(moduleParams, "NumOfFilesAllowed");
			}
		}

		public string FileFolderType
		{
			get
			{
				return getStringParam(moduleParams, "FileFolderType", "custom");
			}
		}

		public string FilePrefix
		{
			get
			{
				return getStringParam(moduleParams, "FilePrefix", "");
			}
		}

		/// <summary>
		/// forced file name, only FileTypeEnum.ForceFilename
		/// </summary>
		public string ForcedFilename
		{
			get
			{
				return getStringParam(moduleParams, "ForcedFilename", "");
			}
		}

		public FileNameTypeEnum FileNameType
		{
			get
			{
				FileNameTypeEnum fileNameType = FileNameTypeEnum.KeepOriginalName;
				int res = (int)fileNameType;
				res = getIntParam(moduleParams, "FileNameType");
				return (FileNameTypeEnum)res;
			}
		}

		/// <summary>
		/// allowed file extensions ex. jpg;jpeg;gif
		/// </summary>
		public List<string> ExtensionsList
		{
			get
			{
				var res = new List<string>();

				string fileExtensions = getStringParam(moduleParams, "FileExtensions", "").ToLower();
				if (!string.IsNullOrEmpty(fileExtensions))
					res = Utility.String2List(fileExtensions, ';');
				return res;
			}
		}

		/// <summary>
		/// max size in KB per each file uploaded
		/// 0 no limit (web server limit)
		/// </summary>
		public int FileSize
		{
			get
			{
				return getIntParam(moduleParams, "FileSize");
			}
		}

		public bool ShowWorkingPath
		{
			get
			{
				return getBoolParam(moduleParams, "ShowWorkingPath", true);
			}
		}

		public bool AllowFilesUpload
		{
			get
			{
				return getBoolParam(moduleParams, "AllowFilesUpload", false);
			}
		}

		public bool AllowFilesSelection
		{
			get
			{
				return getBoolParam(moduleParams, "AllowFilesSelection", false);
			}
		}

		public bool AllowFilesEdit
		{
			get
			{
				return getBoolParam(moduleParams, "AllowFilesEdit", false);
			}
		}

		public bool AllowFilesDel
		{
			get
			{
				return getBoolParam(moduleParams, "AllowFilesDel", false);
			}
		}

		public bool AllowFoldersNavigation
		{
			get
			{
				return getBoolParam(this.moduleParams, "AllowFoldersNavigation", false);
			}
		}

		public bool AllowNewFolder
		{
			get
			{
				return getBoolParam(this.moduleParams, "AllowNewFolder", false);
			}
		}

		/// <summary>
		/// where to store the uploaded file for custom and documents FileFolderType
		/// </summary>
		public string FilePath
		{
			get
			{
				string res = getStringParam(this.moduleParams, "FilePath", "/public/docs");
				return res;
			}
		}

		int maxFileSizeKB = -1;
		public int MaxFileSizeKB
		{
			get
			{
				if (this.maxFileSizeKB == -1)
				{
					maxFileSizeKB = this.FileSize;

					if (this.FileFolderType == IFolderTypes.ItemsImages || this.FileFolderType == IFolderTypes.ItemsFiles)
					{
						int customId = 0;
						int.TryParse(this.customFolder, out customId);
						if (customId > 0)
						{
							var man = new ItemsManager<Item, ItemsFilter>(true, true);
							var item = man.GetByKey(customId);
							if (item.Id > 0)
							{
								int remainSize = 0;
								int maxAttachSizeKB = 0;
								int sizeOfItemsKB = 0;

								maxAttachSizeKB = item.Category.Section.MaxAttachSizeKB;
								if (maxAttachSizeKB > 0)
								{
									try { sizeOfItemsKB = (int)(item.Category.Section.SizeOfItems / 1024); }
									catch { }
									remainSize = maxAttachSizeKB - sizeOfItemsKB;
									if (remainSize < maxFileSizeKB)
										maxFileSizeKB = remainSize;
								}
							}
						}
					}
				}
				return maxFileSizeKB;
			}
		}

		public string getFinalPath = null;
		public string GetFinalPath
		{
			get
			{
				if (getFinalPath == null)
				{
					if (!this.Allowed)
						return "";

					string fileFolderType = this.FileFolderType;
					string folder = this.customFolder;
					getFinalPath = fileFoldersList[fileFolderType];

					int customId = 0;
					int.TryParse(folder, out customId);

					if (fileFolderType == IFolderTypes.ItemsImages || fileFolderType == IFolderTypes.ItemsFiles)
					{
						getFinalPath = getFinalPath.Replace("#id", customId.ToString());
					}
					else if (fileFolderType == IFolderTypes.CategoriesImages || fileFolderType == IFolderTypes.CategoriesFiles)
					{
						getFinalPath = getFinalPath.Replace("#id", customId.ToString());
					}
					else if (fileFolderType == IFolderTypes.SectionsImages || fileFolderType == IFolderTypes.SectionsFiles)
					{
						getFinalPath = getFinalPath.Replace("#id", customId.ToString());
					}
					else if (fileFolderType == IFolderTypes.Temp)
					{
						getFinalPath = getFinalPath.Replace("#sessionid", Utility._SessionID().ToLower());
					}
					else if (fileFolderType == IFolderTypes.Documents)
					{
						getFinalPath = Path.Combine(getFinalPath, folder);
					}
					else if (fileFolderType == IFolderTypes.Custom)
					{
						getFinalPath = Path.Combine(getFinalPath, folder);
					}
				
				}
				getFinalPath = getFinalPath.Replace("\\", "/");
				if (getFinalPath.EndsWith("/"))
					getFinalPath = getFinalPath.Substring(0, getFinalPath.Length-1);

				return getFinalPath;
			}
		}

		#endregion


		#region private methods

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
			res = fileName
				.Replace(",", "-")
				.Replace(" ", "-")
				.Replace("+", "-");
			return res;
		}

		private void initFileFoldersList(Dictionary<string, string> list)
		{
			list.Add(IFolderTypes.Custom, "/public/docs");
			list.Add(IFolderTypes.Documents, "/public/docs");
			list.Add(IFolderTypes.ItemsImages, "/public/gallery/items/#id");
			list.Add(IFolderTypes.ItemsFiles, "/public/files/items/#id");
			list.Add(IFolderTypes.CategoriesImages, "/public/gallery/categories/#id");
			list.Add(IFolderTypes.CategoriesFiles, "/public/files/categories/#id");
			list.Add(IFolderTypes.SectionsImages, "/public/gallery/sections/#id");
			list.Add(IFolderTypes.SectionsFiles, "/public/files/sections/#id");
			list.Add(IFolderTypes.Temp, "/public/temp/#sessionid");
		}

		private bool checkGrants(string fileFolderType, string folder)
		{
			bool res = false;

			int customId = 0;
			int.TryParse(folder, out customId);

			if (fileFolderType == IFolderTypes.ItemsImages || fileFolderType == IFolderTypes.ItemsFiles)
			{
				if (customId > 0)
				{
					var man = new ItemsManager<Item, ItemsFilter>(true, true);
					var item = man.GetByKey(customId);
					if (item.Id > 0)
						res = true;
				}
			}
			else if (fileFolderType == IFolderTypes.CategoriesImages || fileFolderType == IFolderTypes.CategoriesFiles)
			{
				if (customId > 0)
				{
					var man = new CategoriesManager(true, true);
					var item = man.GetByKey(customId);
					if (item.Id > 0)
						res = true;
				}
			}
			else if (fileFolderType == IFolderTypes.SectionsImages || fileFolderType == IFolderTypes.SectionsFiles)
			{
				if (customId > 0)
				{
					var man = new SectionsManager(true, true);
					var item = man.GetByKey(customId);
					if (item.Id > 0)
						res = true;
				}
			}
			else if (fileFolderType == IFolderTypes.Temp)
			{
				if (!string.IsNullOrEmpty(folder))
				{
					if (folder == Utility._SessionID().ToLower())
						res = true;
				}
			}
			else if (fileFolderType == IFolderTypes.Documents)
			{
				res = true;
			}
			else if (fileFolderType == IFolderTypes.Custom)
			{
				//TODO
			}


			return res;
		}

		private string getStringParam(Dictionary<string, string> moduleParams, string paramName, string defaultValue)
		{
			string res = "";
			moduleParams.TryGetValue(paramName, out res);
			if (string.IsNullOrEmpty(res))
				res = defaultValue;
			return res;
		}

		private int getIntParam(Dictionary<string, string> moduleParams, string paramName)
		{
			int res = 0;
			string sValue = "";
			moduleParams.TryGetValue(paramName, out sValue);
			int.TryParse(sValue, out res);
			return res;
		}

		private bool getBoolParam(Dictionary<string, string> moduleParams, string paramName, bool defaultValue)
		{
			bool res = defaultValue;
			string parValue = "";
			if (moduleParams.TryGetValue(paramName, out parValue))
			{
				if (parValue == "0")
					res = false;
				if (parValue == "1")
					res = true;
			}
			return res;
		}

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
			if (this.MaxFileSizeKB > 0)
			{
				if (postedFileSize > this.MaxFileSizeKB * 1024)
					res = false;
			}
			else if (this.FileSize < 0)
			{
				res = false;
			}
			return res;
		}

		#endregion

	}
}