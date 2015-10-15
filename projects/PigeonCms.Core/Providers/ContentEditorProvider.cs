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
using System.Web.Configuration;
using System.Collections.Specialized;
using System.Data.Common;
using System.Text;
using System.Collections.Generic;
using System.Web.Caching;
using PigeonCms.Core.Helpers;
using System.IO;

namespace PigeonCms
{
    /// <summary>
    /// static methods to integrate html editor in a moduleControl
    /// </summary>
    public static class ContentEditorProvider
    {
        public const string SystemReadMoreTag = "<hr class=\"system-readmore\" />";
        public const string SystemPagebreakTag = "<hr class=\"system-pagebreak\" />";

        /// <summary>
        /// content editor configuration class
        /// to use as param when init the editor
        /// </summary>
        public class Configuration
        {
            public enum EditorTypeEnum
            {
                Html = 0,
                BasicHtml = 1,
                Text = 2,
                Image = 3   /* used in labels resources only */
            }

            private bool readMoreButton = true;
            /// <summary>
            /// show readmore button
            /// </summary>
            public bool ReadMoreButton
            {
                get { return readMoreButton; }
                set { readMoreButton = value; }
            }

            private bool pageBreakButton = true;
            /// <summary>
            /// show pageBreak button
            /// </summary>
            public bool PageBreakButton
            {
                get { return pageBreakButton; }
                set { pageBreakButton = value; }
            }

            private bool fileButton = true;
            /// <summary>
            /// show file button to open filemanager 
            /// </summary>
            public bool FileButton
            {
                get { return fileButton; }
                set { fileButton = value; }
            }

            private string filesUploadUrl = "";
            /// <summary>
            /// url of filemanager page
            /// </summary>
            public string FilesUploadUrl
            {
                get { return filesUploadUrl; }
                set { filesUploadUrl = value; }
            }

            private EditorTypeEnum editorType = EditorTypeEnum.Html;
            /// <summary>
            /// html editor type
            /// </summary>
            public EditorTypeEnum EditorType
            {
                get { return editorType; }
                set { editorType = value; }
            }

            //private bool toggleEditor = true;
            ///// <summary>
            ///// show html content
            ///// </summary>
            //public bool ToggleEditor
            //{
            //    get { return toggleEditor; }
            //    set { toggleEditor = value; }
            //}
        }

        public static void InitEditor(PigeonCms.BaseModuleControl control, UpdatePanel upd1, Configuration config)
        {
            string extra = "";
            string editorTheme = "";
            string contentCss = "";
            string editorCss = ""; //control.ResolveUrl("~/Css/common.css");
            string initEditorText = "";
            var cssList = new DirectoryInfo(
                HttpContext.Current.Server.MapPath("~/App_Themes/" + PigeonCms.Config.CurrentTheme)).GetFiles("*.css");
            foreach (var file in cssList)
            {
                editorCss += control.ResolveUrl(
                    "~/App_Themes/" + PigeonCms.Config.CurrentTheme + "/" + file.Name) + ",";
            }
            if (editorCss.EndsWith(","))
                editorCss = editorCss.Substring(0, editorCss.Length - 1);
            if (!string.IsNullOrEmpty(editorCss))
                contentCss = "content_css: '" + editorCss + @"', ";


            switch (config.EditorType)
            {
                case Configuration.EditorTypeEnum.Html:
                    editorTheme = "advanced";
                    break;
                case Configuration.EditorTypeEnum.BasicHtml:
                    editorTheme = "simple";
                    //extra = "paste_text_sticky : true, paste_text_sticky_default : true,";
                    /*extra = @"
                    setup: function(ed) {
                        // Force Paste-as-Plain-Text
                        ed.onPaste.add( function(ed, e, o) {
                            ed.execCommand(‘mcePasteText’, true);
                            return tinymce.dom.Event.cancel(e);
                        });
                    }, ";*/
                    break;
                case Configuration.EditorTypeEnum.Text:
                default:
                    editorTheme = "";
                    break;
            }

            if (config.EditorType == Configuration.EditorTypeEnum.Text
                || config.EditorType == Configuration.EditorTypeEnum.Image)
            { 
                initEditorText = "function initEditor() {}"; 
            }
            else
            {
                initEditorText = @"
                var stylesheets = [];//not used
                $('link[rel=stylesheet]').each(function() {
                    stylesheets += $(this).attr('href') + ',';
                });
                function initEditor() {
                    tinymce.remove('textarea');
                    tinymce.init({
                        selector: 'textarea',
                        relative_urls: false,
                        plugins: [
                                 'advlist autolink link image lists charmap print preview hr anchor pagebreak ',
                                 'searchreplace wordcount visualblocks visualchars code fullscreen insertdatetime media nonbreaking',
                                 'save table contextmenu directionality emoticons template paste textcolor'
                           ],/*plugins-removed: spellchecker*/
                        image_advtab: true,
                        content_css: '" + editorCss + @"', 
						extended_valid_elements: 'iframe[class|src|frameborder=0|alt|title|width|height|align|name]'/*picce 20150811*/
                    });
                }
                ";

                //per version 3.x
                /*tinyMCE.init({
                    mode: 'textareas',
                    theme: '" + editorTheme + @"',
                    relative_urls: false, 
                    "+ contentCss + @"
                    "+ extra + @"
                    plugins: 'safari,pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template',
                    theme_advanced_buttons1: 'bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,styleselect,formatselect',
                    theme_advanced_buttons2: 'search,replace,|,bullist,numlist,|,outdent,indent,|,undo,redo,|,link,unlink,anchor,image,cleanup,help,code',
                    theme_advanced_buttons3: 'hr,removeformat,visualaid,|,sub,sup,|,charmap,|,fullscreen,|,template',
                    theme_advanced_toolbar_location: 'top',
                    theme_advanced_toolbar_align: 'left',
                    theme_advanced_statusbar_location: 'bottom',
                    theme_advanced_resizing: true,
                    extended_valid_elements: 'iframe[class|src|frameborder=0|alt|title|width|height|align|name]'
                });*/
            }


            //tinyMce editor
            Utility.Script.RegisterClientScriptInclude(control, "tinymce", 
                control.ResolveUrl(Config.VendorPath + "tiny_mce/tinymce.min.js"));

            Utility.Script.RegisterClientScriptBlock(control, "initEditorText", initEditorText);

            Utility.Script.RegisterClientScriptBlock(control, "insertEditorText", @"
            function insertEditorText(text) {
                tinyMCE.execCommand('mceInsertContent',false,text);
            }
            ");

            Utility.Script.RegisterClientScriptBlock(control, "insertReadmore()", @"
            function insertReadmore() {
                var content = tinyMCE.activeEditor.getContent();
                if (content.match(/<hr\s+class=(""|')system-readmore(""|')\s*\/*>/i)) {
                    //alert('There is already a Read more... link that has been inserted. Only one such link is permitted. Use {pagebreak} to split the page up further.');
                    return false;
                } else {
                    insertEditorText('"+ ContentEditorProvider.SystemReadMoreTag + @"');
                }
            }
            ");

            Utility.Script.RegisterClientScriptBlock(control, "insertPagebreak()", @"
            function insertPagebreak() {
                var content = tinyMCE.activeEditor.getContent();
                    insertEditorText('"+ ContentEditorProvider.SystemPagebreakTag + @"');
            }
            "); //use title->titolo e alt->alias

            Utility.Script.RegisterClientScriptBlock(control, "insertFile()", @"
            function insertFile() {
                $('<a href="""+ config.FilesUploadUrl + @"""></a>').fancybox({
                    'width': '80%',
                    'height': '80%',
                    'type': 'iframe',
                    'hideOnContentClick': false,
                    onClosed: function () { }
                }).click();
            }
            ");


            Utility.Script.RegisterClientScriptBlock(control, "toggleEditor()", @"
            function toggleEditor() {
                //tinyMCE.execCommand('mceToggleEditor',false,'<client.id here>');
                //tinyMCE.activeEditor.hide();
            }
            ");

            Utility.Script.RegisterStartupScript(upd1, "initEditor", @"
            try{
                if (typeof(initEditor) != 'undefined') {
                    setTimeout(function() { initEditor(); }, 200); }
                } 
            catch(err) {}
            ");
            control.Page.ClientScript.RegisterOnSubmitStatement(control.GetType(), "save", "tinyMCE.triggerSave();");            
        }
    }
}