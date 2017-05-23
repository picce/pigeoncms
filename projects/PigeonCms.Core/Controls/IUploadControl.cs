using PigeonCms;
using System;
using System.Web.UI;



namespace PigeonCms.Controls
{
    public delegate void UploadControlFileDeletedDelegate(object sender, EventArgs e);

    public interface IUploadControl
	{
        event UploadControlFileDeletedDelegate FileDeleted;
        string Name { get; set; }
        string AllowedFileTypes { get; set; }
		bool Deleted { get; }
		string FilePath { get; set; }
		bool HasChanged { get; }
		bool HasFile { get; }
		int MaxFileSize { get; set; }

		void CleanSession();
		string GetExtension();
		void PerformDelete();
		void SaveTo(string filePath);
	}
}
