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
using System.Data.OleDb;
using System.Linq;
using System.Web.Hosting;

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

        public static Dictionary<string, string> Read301CSV(string path)
        {
            if (File.Exists(path))
            {
                using (TextReader sr = new StreamReader(path))
                {
                    Dictionary<string, string> redirect_dict = new Dictionary<string, string>();
                    string line = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] columns = line.Split(',');
                        redirect_dict.Add(columns[0], columns[1]);
                    }
                    return redirect_dict;
                }
            }
            else
                return new Dictionary<string, string>();
        }

        public static string AddFilenameSuffix(string fileName, string suffix)
        {
            if (string.IsNullOrWhiteSpace(suffix))
                return fileName;

            List<string> fileNameTokens = new List<string>(fileName.Split('.'));
            string extension = fileNameTokens.Last();
            fileNameTokens.RemoveAt(fileNameTokens.Count - 1);
            return string.Join(".", fileNameTokens) + "_" + suffix + "." + extension;
        }

        public static string GetUniqueFilename(string path, string fileName, out string newFileName)
        {
            int suffix = 0;
            string basePath = path.StartsWith("~") ? HostingEnvironment.MapPath(path) : path;
            newFileName = fileName;

            while (true)
            {
                newFileName = AddFilenameSuffix(fileName, (suffix > 0 ? suffix.ToString() : ""));
                string fullPath = basePath + newFileName;
                if (!File.Exists(fullPath))
                    return fullPath;

                suffix++;
                if (suffix > 100)
                    throw new Exception("Cannot calculate unique filename");
            }
        }

        public static string GetDirectory(string path)
        {
            List<string> pathTokens = new List<string>(path.Split(@"\".ToCharArray()));
            pathTokens.RemoveAt(pathTokens.Count - 1);
            return string.Join(@"\", pathTokens.ToArray());
        }

        public static string GetExtensionFromMime(string mime)
        {
            if (string.IsNullOrWhiteSpace(mime))
                return "";

            switch (mime.ToLower())
            {
                case "image/jpeg":
                case "image/jpg":
                case "image/pjpeg":
                    return "jpg";
                case "image/png":
                    return "png";
                case "image/gif":
                    return "gif";
                case "image/svg+xml":
                    return "svg";
                case "application/pdf":
                    return "pdf";
                    // TODO: add other types
            }

            return "";
        }

        public static string GetMimeFromExtension(string extension)
        {
            if (string.IsNullOrWhiteSpace(extension))
                return "";

            switch (extension.ToLower())
            {
                case "jpeg":
                case "jpg":
                    return "image/jpeg";
                case "png":
                    return "image/png";
                case "gif":
                    return "image/gif";
                case "svg":
                    return "image/svg+xml";
                case "pdf":
                    return "application/pdf";
                    // TODO: add other types
            }

            return "";
        }

        public static string GetUrlFileName(string url)
        {
            string[] pathToken = url.Split('/');
            return pathToken[pathToken.Length - 1];
        }
    }

    public class ExportHelper
    {
        /// <summary>
        /// export grid to excel file
        /// returns destination file path
        /// tnx to http://www.siddharthrout.com/2014/07/20/creating-a-new-excel-file-and-adding-data-using-ace/
        /// </summary>
        public string GridToExcel(GridView ctrl, string filename, bool download = true, List<string>columns = null)
        {
            if (ctrl.Rows.Count == 0)
                return "";

            if (columns == null)
            {
                //ctrl.Columns is empty for grid with AutoGenerateColumns=true
                columns = new List<string>();
                foreach (DataControlField col in ctrl.Columns)
                {
                    //if (string.IsNullOrEmpty(col.HeaderText))
                    //    col.Visible = false;

                    //if (!col.Visible)
                    //    continue;

                    string title = col.HeaderText;
                    if (!col.Visible)
                        title = "";
                    columns.Add(title);
                }
            }

            //create file
            var olecon = new OleDbConnection();
            var olecmd = new OleDbCommand();
            string filePath = new FilesGallery().TempPhisicalPath;
            filename += ".xlsx";
            string connstring = "Provider=Microsoft.ACE.OLEDB.12.0;" +
                                "Data Source=" + Path.Combine(filePath, filename) + ";" +
                                "Extended Properties=\"Excel 12.0 Xml;HDR=YES;\"";

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            if (File.Exists(Path.Combine(filePath, filename)))
                File.Delete(Path.Combine(filePath, filename));

            olecon.ConnectionString = connstring;
            olecon.Open();
            olecmd.Connection = olecon;

            //create table
            olecmd.CommandText = getCreateTableSql(columns);
            olecmd.ExecuteNonQuery();

            //insert rows
            for (int i = 0; i < ctrl.Rows.Count; i++)
            {
                GridViewRow row = ctrl.Rows[i];
                olecmd.CommandText = getInsertSqlFromRow(row, columns);
                olecmd.ExecuteNonQuery();
            }
            olecon.Close();


            //download file
            FileInfo file = new FileInfo(Path.Combine(filePath, filename));
            if (download && file.Exists)
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                HttpContext.Current.Response.AddHeader("Content-Type", "application/Excel");
                HttpContext.Current.Response.ContentType = "application/vnd.xls";
                HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
                HttpContext.Current.Response.WriteFile(file.FullName);
                //HttpContext.Current.Response.End();
                HttpContext.Current.Response.Flush();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            return file.FullName;
            //return ctrl.Rows.Count;
        }

        /// <summary>
        /// sql to create table (sheet) in excel file
        /// </summary>
        private string getCreateTableSql(List<string> columns)
        {
            string res = "";
            string cols = "";
            foreach (var title in columns)
            {
                if (!string.IsNullOrEmpty(title))
                    cols += "[" + title + "] memo,";
            }
            if (cols.EndsWith(","))
                cols = cols.Substring(0, cols.Length - 1);

            res = "CREATE TABLE Sheet1(" + cols + ")";
            return res;
        }

        /// <summary>
        /// sql for insert single row in excel file
        /// </summary>
        private string getInsertSqlFromRow(GridViewRow row, List<string>columns /*GridView ctrl*/)
        {
            string res = "";
            string cols = "";
            string values = "";
            int colIdx = 0;
            //foreach (DataControlField col in ctrl.Columns)
            foreach (string title in columns)
            {
                //if (!col.Visible)
                //{
                //    colIdx++;
                //    continue;
                //}

                if (string.IsNullOrEmpty(title))
                {
                    colIdx++;
                    continue;
                }

                cols += "[" + title + "],";

                var LitValue = findFirstControlRecursive<Literal>(row.Cells[colIdx]);
                string value = row.Cells[colIdx].Text;
                if (LitValue != null)
                    value = LitValue.Text;

                //issue on newline that becomes _x000d_ in excel
                if (value.Contains("\r\n"))
                    value = value.Replace("\r\n", "");

                values += "'" + value.Replace("'", "''") + "',";

                colIdx++;
            }
            if (cols.EndsWith(","))
                cols = cols.Substring(0, cols.Length - 1);
            if (values.EndsWith(","))
                values = values.Substring(0, values.Length - 1);

            res = "INSERT INTO Sheet1(" + cols + ") VALUES(" + values + ")";
            return res;
        }

        private T findFirstControlRecursive<T>(Control parentControl) where T : Control
        {
            T ctrl = default(T);

            if ((parentControl is T) /*&& (parentControl.ID == id)*/)
                return (T)parentControl;

            foreach (Control c in parentControl.Controls)
            {
                ctrl = findFirstControlRecursive<T>(c);
                if (ctrl != null) break;
            }
            return ctrl;
        }

    }
}
