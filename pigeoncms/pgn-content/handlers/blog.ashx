<%@ WebHandler Language="C#" Class="BlogHandler" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Runtime.Serialization;
using System.Collections.Generic;
using PigeonCms;


public class BlogHandler : IHttpHandler
{

    public bool IsReusable
    {
        get { return true; }
    }


    public void ProcessRequest(HttpContext context)
    {

        try
        {
            string res = "";

            string lang = "en";
            try { lang = HttpContext.Current.Request["lng"].ToLower(); }
            catch
            {
                lang = "en";
            }

            //string culture = Aquest.Assos.BasePage.Lang2Culture(lang);
            
            var items = getItems();
            var alsResult = new BlogResult(lang, items);
            res = new JavaScriptSerializer().Serialize(alsResult);

            context.Response.ContentType = "text/javascript";
            context.Response.Write(res);
        }
        catch (Exception e)
        {
        }
        finally
        {
            context.Response.OutputStream.Dispose();
        }
    }

    private List<PigeonCms.Item> getItems()
    {
        var man = new ItemsManager<Item, ItemsFilter>(true, false);
        var filter = new ItemsFilter();
        filter.SectionId = 2; //blog id
        filter.CategoryId = 3; //archivio - category id
        filter.Enabled = PigeonCms.Utility.TristateBool.True;

        var res = man.GetByFilter(filter, "");
        return res;
    }

    [DataContract]
    private class BlogResult
    {
        private string culture = "";
        
        public class BlogItemAdapter
        {
            public int PostId = 0;
            public string Alias = "";
            public string CategoryTitle = "";
            public string Title = "";
            public string DescriptionAbstract = "";
            public string DefaultImg = "";
            public string CssClass = "";

            public BlogItemAdapter(PigeonCms.Item item, string culture, string cssClass)
            {
                this.PostId = item.Id;
                this.Alias = item.Alias;
                this.CssClass = cssClass;

                this.CategoryTitle = LabelsProvider.GetLocalizedTextFromDictionary( 
                    item.Category.TitleTranslations, culture);
                
                this.Title = LabelsProvider.GetLocalizedTextFromDictionary( 
                    item.TitleTranslations, culture);
                
                this.DescriptionAbstract = Utility.Html.StripTagsRegex(
                    Utility.Html.GetTextIntro(
                    LabelsProvider.GetLocalizedTextFromDictionary(
                    item.DescriptionTranslations, culture)));
                
                this.DefaultImg = "http://placehold.it/400x280";
                if (!string.IsNullOrEmpty(item.DefaultImage.FileName))
                    this.DefaultImg = item.DefaultImage.FileUrl; ;
            }
        }

        public List<BlogItemAdapter> List = new List<BlogItemAdapter>();


        public BlogResult(string culture, List<PigeonCms.Item> list = null)
        {
            this.culture = culture;
            
            string[] postCssClasses = {
                "o-blog-single--purple", 
                "o-blog-single--pink",
                "o-blog-single--green"
            };

            int counter = 0;
            foreach (var i in list)
            {
                var post = new BlogItemAdapter(i, culture, postCssClasses[counter % 3]);
                List.Add(post);
                counter++;
            }
                
        }


    }

}