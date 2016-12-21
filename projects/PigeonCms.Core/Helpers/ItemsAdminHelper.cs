using ImageResizer;
using PigeonCms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using System.Collections.Specialized;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;


namespace PigeonCms.Core.Helpers
{
	public class ItemsAdminHelper
    {
		public ItemsAdminHelper()
		{

		}

		public static void InsertJsIntoPageScriptManager(string path, Page page)
		{
			string url = path.StartsWith("~") || path.StartsWith("/") ? path : "~/Controls/" + path + ".js";
			ScriptManager.GetCurrent(page).Scripts.Add(new ScriptReference(url));
		}

		public static void RegisterCss(string path, Page page)
		{
			string url = path.StartsWith("~") || path.StartsWith("/") ? path : "~/Controls/" + path + ".css";
			Literal cssFile = new Literal() { Text = @"<link href=""" + page.ResolveUrl(url) + @""" type=""text/css"" rel=""stylesheet"" />" };
			page.Header.Controls.Add(cssFile);
		}

		public static void WriteImageToResponse(HttpResponse response, string filePath, int width, int height, OutputFormat outputFormat = OutputFormat.Jpeg, FitMode fitMode = FitMode.Pad, string padColor = "#FFFFFF")
		{
			Instructions resizeParams = new Instructions();
			resizeParams.Width = width;
			resizeParams.Height = height;
			resizeParams.IgnoreICC = true;
			resizeParams.Mode = fitMode;
			resizeParams.PaddingColor = "red"; // padColor;
			resizeParams.OutputFormat = outputFormat;

			ImageJob i = new ImageJob(filePath, Path.GetTempPath() + "<guid>.<ext>", resizeParams);
			i.Build();

			using (Bitmap img = new Bitmap(i.FinalPath))
			{
				using (MemoryStream ms = new MemoryStream())
				{
					response.Clear();
					response.ContentType = "image/" + (img.RawFormat == ImageFormat.Png ? "png" : "jpeg");
					response.Cache.SetCacheability(HttpCacheability.Public);
					img.Save(ms, img.RawFormat);
					ms.WriteTo(response.OutputStream);
					ms.Flush();
					response.Flush();
					ms.Close();
				}
			}

			File.Delete(i.FinalPath);
			//response.End();
		}

		public static IDictionary<string, object> GetSessionValues(HttpSessionState session)
		{
			IDictionary<string, object> result = new Dictionary<string, object>();

			foreach (string key in session.Keys)
				result[key] = session[key];

			return result;
		}

		public static string GetItemShortName(Item item)
		{
			if (item == null)
				return string.Empty;

			string[] itemType = item.ItemTypeName.Split('.');
			return itemType[itemType.Length - 1];
		}

		public static string ExceptionToString(Exception ex, string separator = "<br/>")
		{
			if (ex == null)
				return "";

			try
			{
				StringBuilder sb = new StringBuilder();

				sb.AppendLine("Inner exception: " + ex.InnerException + separator);
				sb.AppendLine("Message: " + ex.Message + separator);
				sb.AppendLine("Source: " + ex.Source + separator);
				sb.AppendLine("TargetSite: " + ex.TargetSite + separator);

				var st = new StackTrace(ex, true);
				foreach (StackFrame sf in st.GetFrames())
				{
					sb.AppendLine(sf.ToString());
				}

				return sb.ToString();
			}
			catch
			{
				return "Error " + ex.Message;
			}
		}

		public static string CreateUid(string lang)
		{
			return Regex.Replace(Guid.NewGuid().ToString(), @"\-", "0") + "-" + lang;
		}

    }

}
