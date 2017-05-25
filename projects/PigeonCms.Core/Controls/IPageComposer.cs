using PigeonCms;
using System;
using System.Web.UI;


namespace PigeonCms.Controls
{
    /// <summary>
    /// interface for pigeon\controls\PageComposer\PageComposer.ascx
    /// </summary>
    public interface IPageComposer
	{
        void RegisterScripts();
        void Load(IItem obj);
        void Store(IItem obj);
        void TranslateFileToEditor(IItem block);
        string TranslateFileFromEditor(IItem item, string fieldId);
        string ReverseMapPath(string path);
    }
}
