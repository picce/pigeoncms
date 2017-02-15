using PigeonCms;
using System.Web.UI;



namespace PigeonCms.Controls
{
	public interface IContentEditorControl : ITextControl
	{
		ContentEditorProvider.Configuration Configuration { get; set; }
	}
}
