using AQuest.Cecchi.Data.Base;
using AQuest.Cecchi.Data.Base.Attributes;
using AQuest.Cecchi.Data.Items.Blocks;
using AQuest.Cecchi.Utils;
using AQuest.PigeonCMS.ItemsAdmin;
using AQuest.PigeonCMS.ItemsAdmin.Uploads;
using Newtonsoft.Json.Linq;
using PigeonCms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_PageComposer_PageComposer : UserControl
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        ItemsAdminHelper.RegisterCss("Pagecomposer/assets/css/application", Page);
        ItemsAdminHelper.InsertJsIntoPageScriptManager("Pagecomposer/assets/js/app", Page);
    }

    public void RegisterScripts()
    {
        JObject pageComposerSettings = new JObject
        {
            { "sources", new JArray { "element:input[id=aq_pagecomposer_value]" } },
            { "targets", new JArray { "element:input[id=aq_pagecomposer_value]" } },
            { "endpoints", new JObject {
                    { "getPreview", "/Controls/ImageUpload/PageComposerUploadHandler.ashx?action=previewurl" },
                    { "upload", "/Controls/ImageUpload/PageComposerUploadHandler.ashx" },
                    { "delete", "/Controls/ImageUpload/PageComposerUploadHandler.ashx?action=delete" },
                }
            }
        };

        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "aq_pagecomposer_start", @"
            try 
            {                 
                window.AQuest.PageComposer.init(" + pageComposerSettings.ToString() + @");
            }
            catch (exc)
            {
                document.addEventListener('DOMContentLoaded', function () {
                    window.AQuest.PageComposer.init(" + pageComposerSettings.ToString() + @");
                });
            }
        ", true);
    }

    public void Load(Item obj)
    {
        BaseItem item = obj as BaseItem;
        if (item == null)
            return;

        BlocksPropertiesDefs newsProps = item.Properties as BlocksPropertiesDefs;
        if (newsProps == null)
            return;

        if (newsProps.Blocks != null && newsProps.Blocks.Count > 0)
        {
            newsProps.Blocks.ForEach((block) => { TranslateFileToEditor(block); });
            aq_pagecomposer_value.Value = UrlUtils.Base64Encode(BlockManager.SerializeForEditor(newsProps.Blocks));
        }
    }

    public void Store(Item obj)
    {
        BaseItem item = obj as BaseItem;
        if (item == null)
            return;

        BlocksPropertiesDefs props = item.Properties as BlocksPropertiesDefs;
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

    public void TranslateFileToEditor(BaseItem block)
    {
        try
        {
            if (block == null)
                return;

            AbstractPropertiesDefs props = block.Properties;
            PropertyInfo[] properties = props.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            string uid = string.Empty;

            foreach (PropertyInfo property in properties)
            {
                ImageFieldAttribute attribute = (ImageFieldAttribute)property.GetCustomAttribute(typeof(ImageFieldAttribute));
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
        catch (Exception e)
        {

        }
    }

    public string TranslateFileFromEditor(BaseItem item, string fieldId)
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
            string filePath = FilesManipulationUtils.GetUniqueFilename(item.StaticImagesPath, fileName.ToLower(), out newFileName);
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