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

namespace PigeonCms
{
    /// <summary>
    /// static class to retrieve localized labels
    /// </summary>
    public static class LabelsProvider
    {
        public class UI
        {
            private Panel panel = null;
            private bool showOnlyDefaultCulture = false;
            
            public UI(bool showOnlyDefaultCulture, Panel panel)
            {
                this.showOnlyDefaultCulture = showOnlyDefaultCulture;
                this.panel = panel;
            }


            public void GetTransText(string controlPrefix, 
                Dictionary<string, string> translations,
                KeyValuePair<string, string> cultureItem)
            {
                TextBox t1 = new TextBox();
                t1 = (TextBox)panel.FindControl(controlPrefix + cultureItem.Value);
                translations.Add(cultureItem.Key, t1.Text);
            }


            public void SetTransText(string controlPrefix, 
                Dictionary<string, string> translations,
                KeyValuePair<string, string> cultureItem)
            {
                string res = "";
                TextBox t1 = new TextBox();
                t1 = (TextBox)panel.FindControl(controlPrefix + cultureItem.Value);
                if (translations != null)
                    translations.TryGetValue(cultureItem.Key, out res);
                if (t1 != null)
                    t1.Text = res;
            }

            public void AddTransText(string controlPrefix, 
                KeyValuePair<string, string> cultureItem, 
                int maxLen, string cssClass)
            {
                Panel pan1 = new Panel();
                pan1.CssClass = "form-group input-group";
                panel.Controls.Add(pan1);

                var txt = new TextBox();
                txt.ID = controlPrefix + cultureItem.Value;
                txt.MaxLength = maxLen;
                txt.CssClass = cssClass;
                txt.ToolTip = cultureItem.Key;
                LabelsProvider.SetLocalizedControlVisibility(showOnlyDefaultCulture, cultureItem.Key, txt);
                pan1.Controls.Add(txt);

                Literal lit = new Literal();
                if (!showOnlyDefaultCulture)
                    lit.Text = "<span class='input-group-addon'>" + cultureItem.Value + "</span>";
                pan1.Controls.Add(lit);
            }

        }

        const string CacheKeyPrefix = "Labels";

        #region public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceSet">clear all labels cache. Param not used</param>
        public static void ClearCacheByResourceSet(string resourceSet)
        {
            var cache = new CacheManager<List<ResLabel>>(CacheKeyPrefix);
            
            //last update 20150324
            //if (!cache.IsEmpty(resourceSet){}

            cache.Clear();
        }

        /// <summary>
        /// USE ONLY in static methods.
        /// This method doesn't use in page caching.
        /// Prefer use if BaseModuleControl.GetLabel
        /// </summary>
        /// <returns></returns>
        [Obsolete("USE ONLY in static methods. This method doesn't use in page caching. Prefer use if BaseModuleControl.GetLabel")]
        public static string GetLabel(
            string moduleFullName, 
            string resourceId, 
            string defaultValue,
            string forcedCultureCode = "")
        {
            string res = "";
            var labelsList = LabelsProvider.getLabelsByResourceId(moduleFullName, resourceId);
            
            res = LabelsProvider.GetLocalizedLabelFromList(
                moduleFullName,
                labelsList,
                resourceId, 
                defaultValue, 
                ContentEditorProvider.Configuration.EditorTypeEnum.Text,
                forcedCultureCode);

            if (string.IsNullOrEmpty(res))
            {
                res = defaultValue;
            }
            if (HttpContext.Current.Request.QueryString["tp"] == "1")
            {
                res = "[" + resourceId + "]" + res;
            }
            return res;
        }

        private static List<ResLabel> getLabelsByResourceId(string resourceSet, string resourceId)
        {
            var result = new List<ResLabel>();

            var cache = new CacheManager<List<ResLabel>>(CacheKeyPrefix);
            string key = resourceSet + "_" + resourceId;
            if (cache.IsEmpty(key))
            {
                var man = new LabelsManager();
                var filter = new LabelsFilter();
                filter.ResourceSet = resourceSet;
                filter.ResourceId = resourceId;
                result = man.GetByFilter(filter, "");

                cache.Insert(key, result);
            }
            else
            {
                result = cache.GetValue(key);
            }
            return result;
        }

        public static List<ResLabel> GetLabelsByResourceSet(string resourceSet)
        {
            var result = new List<ResLabel>();

            var cache = new CacheManager<List<ResLabel>>(CacheKeyPrefix);
            if (cache.IsEmpty(resourceSet))
            {
                var man = new LabelsManager();
                var filter = new LabelsFilter();
                filter.ResourceSet = resourceSet;
                result = man.GetByFilter(filter, "");

                cache.Insert(resourceSet, result);
            }
            else
            {
                result = cache.GetValue(resourceSet);
            }
            return result;
        }


        /// <summary>
        /// use this this method to retrieve localized text in items
        /// </summary>
        /// <param name="dict">dictionary that contains translations</param>
        /// <returns>the right value in current culture</returns>
        public static string GetLocalizedTextFromDictionary(Dictionary<string, string> dict, string forcedCultureCode = "")
        {
            string res = "";

            //20150512
            if (string.IsNullOrEmpty(forcedCultureCode))
                forcedCultureCode = Utility.GetCurrCultureName();

            dict.TryGetValue(forcedCultureCode, out res);
            if (Utility.IsEmptyFckField(res))
                dict.TryGetValue(Config.CultureDefault, out res);

            return res;
        }

        /// <summary>
        /// retrieve label result from cached list (in page or control var)
        /// try in order to retrieve:
        /// - current culture value
        /// - default culture value
        /// - defaultValue --> (firts request: insert in db) 
        /// </summary>
        /// <param name="resourceSet"></param>
        /// <param name="labelsList"></param>
        /// <param name="resourceId"></param>
        /// <param name="defaultValue"></param>
        /// <param name="textMode">default=EditorTypeEnum.Text</param>
        /// <returns>the label value</returns>
        public static string GetLocalizedLabelFromList(
            string resourceSet,
            List<ResLabel>labelsList, 
            string resourceId, 
            string defaultValue,
            ContentEditorProvider.Configuration.EditorTypeEnum textMode, 
            string forcedCultureCode)
        {
            string res = "";

            //20150512
            if (string.IsNullOrEmpty(forcedCultureCode))
                forcedCultureCode = Utility.GetCurrCultureName();

            if (labelsList != null)
            {
                //find the right value in labelsList
                try
                {
                    //try current culture
                    res = labelsList.Find(
                        delegate(ResLabel labelToFind)
                        {
                            if (labelToFind.ResourceId.ToLower() == resourceId.ToLower() &&
                                labelToFind.CultureName.ToLower() == forcedCultureCode.ToLower())
                                return true;
                            else
                                return false;
                        }).Value;

                    //added 20150701
                    //try default culture
                    if (string.IsNullOrEmpty(res) && Config.CultureDefault.ToLower() != forcedCultureCode.ToLower())
                    {
                        try
                        {
                            res = labelsList.Find(
                                delegate(ResLabel labelToFind)
                                {
                                    if (labelToFind.ResourceId.ToLower() == resourceId.ToLower() &&
                                        labelToFind.CultureName.ToLower() == Config.CultureDefault.ToLower())
                                        return true;
                                    else
                                        return false;
                                }).Value;
                        }
                        catch (NullReferenceException)
                        { res = ""; }
                    }
                }
                catch (NullReferenceException)
                {
                    //try default culture
                    if (Config.CultureDefault.ToLower() != forcedCultureCode.ToLower())
                    {
                        try
                        {
                            res = labelsList.Find(
                                delegate(ResLabel labelToFind)
                                {
                                    if (labelToFind.ResourceId.ToLower() == resourceId.ToLower() &&
                                        labelToFind.CultureName.ToLower() == Config.CultureDefault.ToLower())
                                        return true;
                                    else
                                        return false;
                                }).Value;
                        }
                        catch (NullReferenceException)
                        { res = ""; }
                    }

                    //##20140519
                    //auto insert new label with default value for current culture
                    if (string.IsNullOrEmpty(res)
                        && !string.IsNullOrEmpty(defaultValue)
                        && !string.IsNullOrEmpty(resourceSet))
                    {
                        if (insertDefaultValue(resourceSet, resourceId, defaultValue, textMode))
                        {
                            //cause label cache reload
                            ClearCacheByResourceSet(resourceSet);
                        }
                    }
                }
            }
            return res;
        }

        public static string GetLocalizedVarFromList(List<ResLabel> labelsList, string varId)
        {
            string res = varId;
            if (varId.StartsWith("$"))
            {
                string resourceId = varId.Substring(1);
                res = GetLocalizedLabelFromList("", labelsList, resourceId, "", 
                    ContentEditorProvider.Configuration.EditorTypeEnum.Text, "");
                if (string.IsNullOrEmpty(res))
                    res = varId;
            }
            return res;
        }

        public static void SetLocalizedControlVisibility(bool showOnlyDefaultCulture, string culture, Control ctrl)
        {
            if (showOnlyDefaultCulture)
            {
                if (culture.ToLower() != Config.CultureDefault.ToLower())
                    ctrl.Visible = false;
            }
        }

        #endregion

        /// <summary>
        /// automatically insert label in labels using CultureDev
        /// </summary>
        /// <returns>operation done sucessfully</returns>
        private static bool insertDefaultValue(
            string resourceSet,
            string resourceId,
            string defaultValue,
            ContentEditorProvider.Configuration.EditorTypeEnum textMode)
        {
            bool res = false;
            try
            {
                var man = new LabelsManager();
                var filter = new LabelsFilter();

                filter.ResourceSet = resourceSet;
                filter.ResourceId = resourceId;
                filter.CultureName = Config.CultureDev;

                var l = man.GetByFilter(filter, "");
                if (l.Count == 0)
                {
                    var o1 = new ResLabel();
                    o1.ResourceSet = resourceSet;
                    o1.ResourceId = resourceId;
                    o1.CultureName = Config.CultureDev;
                    o1.Value = defaultValue;
                    o1.Comment = "SYSTEM";
                    o1.TextMode = textMode;

                    //insert default value for current culture
                    man.Insert(o1);

                    Tracer.Log("LabelsProvider.GetLocalizedLabelFromList()>Insert new label["
                        + resourceSet + "|"
                        + resourceId + "|"
                        + o1.CultureName + "|"
                        + o1.TextMode.ToString() + "]=" + defaultValue,
                        TracerItemType.Debug);
                    res = true;
                }
            }
            catch (Exception ex)
            {
                Tracer.Log("LabelsProvider.GetLocalizedLabelFromList()>Insert new label ERR["
                    + resourceSet + "|" + resourceId + "|" + Config.CultureDefault + "]=" + defaultValue
                    + " ERR:" + ex.ToString(),
                    TracerItemType.Error);
            }
            return res;
        }
    }
}