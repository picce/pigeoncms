using Newtonsoft.Json.Linq;
using PigeonCms;
using PigeonCms.Controls.ItemFields;
using PigeonCms.Controls.ItemsAdmin;
using PigeonCms.Core.Controls.ItemBlocks;
using PigeonCms.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.UI;


public partial class Controls_PageComposer_PageComposer : 
    UserControl, PigeonCms.Controls.IPageComposer
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    public void Load(List<BaseBlockItem> blocks)
    {
        if (blocks == null)
            return;

        if (blocks != null && blocks.Count > 0)
        {
            blocks.ForEach((block) => 
            {
                TranslateFileToEditor(block);
            });
            aq_pagecomposer_value.Value = UrlUtils.Base64Encode(BlockManager.SerializeForEditor(blocks));
        }
    }

    public void Store(IItem obj)
    {
        IItem item = obj as IItem;
        if (item == null)
            return;

        //TOCHECK PropertiesList
        if (item.PropertiesList.Count > 0)
        {
            var props = item.PropertiesList[0];
            if (props == null)
                return;

            props.Blocks = new List<BaseBlockItem>();

            string blockB64Json = aq_pagecomposer_value.Value;
            if (!string.IsNullOrWhiteSpace(blockB64Json))
            {
                string source = UrlUtils.Base64Decode(blockB64Json);
                props.Blocks = BlockManager.DeserializeFromEditor(source, (fieldId) =>
                {
                    return TranslateFileFromEditor(item, fieldId);
                });
            }
        }

    }

    public void TranslateFileToEditor(IItem block)
    {
        try
        {
            if (block == null)
                return;

            //TOCHECK PropertiesList
            if (block.PropertiesList.Count > 0)
            {
                ItemPropertiesDefs props = block.PropertiesList[0];
                PropertyInfo[] properties = props.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                string uid = string.Empty;

                foreach (PropertyInfo property in properties)
                {
                    FileFormField attribute = (FileFormField)property.GetCustomAttribute(typeof(FileFormField));
                    if (attribute == null)
                        continue;

                    if (attribute.Localized)
                    {
                        Translation translation = (Translation)property.GetValue(props);
                        if (translation != null)
                        {
                            foreach (KeyValuePair<string, string> pair in translation)
                            {
                                uid = ItemsAdminHelper.CreateUid(pair.Key);
                                AbstractUploadHandler.SetFile(Context, "PageComposer", uid, pair.Value);
                                translation[pair.Key] = uid;
                            }
                            property.SetValue(props, translation);
                        }
                    }
                    else
                    {
                        uid = ItemsAdminHelper.CreateUid("no-lang");
                        AbstractUploadHandler.SetFile(Context, "PageComposer", uid, (string)property.GetValue(props));
                        property.SetValue(props, uid);
                    }
                }
            }

        }
        catch (Exception e)
        {

        }
    }

    public string TranslateFileFromEditor(IItem item, string fieldId)
    {
        if (AbstractUploadHandler.IsDeleted(Context, "PageComposer", fieldId))
        {
            AbstractUploadHandler.PerformDelete(Context, "PageComposer", fieldId);
            return null;
        }
        else if (AbstractUploadHandler.HasChanges(Context, "PageComposer", fieldId))
        {
            string fileName = Regex.Replace(AbstractUploadHandler.GetRealName(Context, "PageComposer", fieldId), @"[^a-zA-Z0-9\-_\.]", "-");
            if (string.IsNullOrWhiteSpace(fileName))
                return null;

            string newFileName;
            string filePath = FilesHelper.GetUniqueFilename(item.StaticImagesPath, fileName.ToLower(), out newFileName);
            AbstractUploadHandler.SaveTo(Context, "PageComposer", fieldId, filePath);
            return item.StaticImagesPath + newFileName;
        }
        else
        {
            return ReverseMapPath(AbstractUploadHandler.GetFilePath(Context, "PageComposer", fieldId));
        }
    }

    public string ReverseMapPath(string path)
    {
        string appPath = Server.MapPath("~");
        return string.Format("~/{0}", path.Replace(appPath, "").Replace("\\", "/"));
    }
}