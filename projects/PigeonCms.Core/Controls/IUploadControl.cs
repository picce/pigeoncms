using PigeonCms;
using System.Web.UI;



namespace PigeonCms.Controls
{
	public interface IUploadControl
	{
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
