using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;


namespace PigeonCms
{
    public static class FilesHelper
    {
        /// <summary>
        /// Write a secure file out to the response stream. Writes piece-meal in 4K chunks to
        /// help prevent problems with large files.
        /// <example>
        /// <code>WriteFileToResponse(@"secureFolder/mysecurefile.pdf", @"test.pdf",
        /// @"application/pdf");</code>
        /// </example>
        /// <example>
        /// <code>WriteFileToResponse(@"secureFolder/mysecurefile.pdf", @"test.pdf");</code>
        /// </example>
        /// </summary>
        /// <param name="secureFilePath">>Relative path to the file to download from our 
        /// secure folder</param>
        /// <param name="userFilename">Name of file the user will see</param>
        /// <param name="contentType">MIME type of the file for Response.ContentType, 
        /// "application/octet-stream" is a good catch all. A list of other possible values 
        /// can be found at http://msdn.microsoft.com/en-us/library/ms775147.aspx </param>

        public static void WriteFileToResponse(string secureFilePath, string userFilename, string contentType)
        {
            // Process the file in 4K blocks
            byte[] dataBlock = new byte[0x1000];
            long fileSize;
            int bytesRead;
            long totalBytesRead = 0;

            if (string.IsNullOrEmpty(contentType))
                contentType = @"application/octet-stream";

            using (var fs = new FileStream(secureFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                fileSize = fs.Length;

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = contentType;
                HttpContext.Current.Response.AddHeader("Content-Disposition",
                    "attachment; filename=" + userFilename);

                while (totalBytesRead < fileSize)
                {
                    if (!HttpContext.Current.Response.IsClientConnected)
                        break;

                    bytesRead = fs.Read(dataBlock, 0, dataBlock.Length);
                    HttpContext.Current.Response.OutputStream.Write(dataBlock, 0, bytesRead);
                    HttpContext.Current.Response.Flush();
                    totalBytesRead += bytesRead;
                }

                HttpContext.Current.Response.Close();
            }
        }

        public static string MapPathWhenVirtual(string path)
        {
            string res = path;
            if (res.StartsWith("~") || res.StartsWith("/"))
            {
                //relative path
                res = HttpContext.Current.Request.MapPath(res);
            }
            else
            {
                //physical path
                res = res.Replace("/", "\\");
            }
            return res;
        }
    }
}
