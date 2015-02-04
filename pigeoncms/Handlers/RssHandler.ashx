<%@ WebHandler Language="C#" Class="RssHandler" %>

using System;
using System.IO;
using System.Web;
using PigeonCms;
using PigeonCms.Core.Helpers;
using System.ServiceModel.Syndication;
using System.Collections.Generic;
using System.Xml;

public class RssHandler : IHttpHandler
{

    public bool IsReusable
    {
        get { return true; }
    }

    private SyndicationFeed getFeed()
    {
        var feed = new SyndicationFeed("Items", "Canale feeds - Still in test mode", new Uri("http://www.pigeoncms.com"));
        feed.Authors.Add(new SyndicationPerson("picce@yahoo.it", "picce", "www.pigeoncms.com"));
        feed.Categories.Add(new SyndicationCategory("categoria"));
        
        //using (var writer = System.Xml.XmlWriter.Create(Response.OutputStream))
        //{
        //}
        
        var items = new List<SyndicationItem>();

        var filter = new PigeonCms.ItemsFilter();
        filter.Enabled = Utility.TristateBool.True;
        filter.IsValidItem = Utility.TristateBool.True;
        var list = new PigeonCms.ItemsManager<Item, ItemsFilter>(true, false).GetByFilter(filter, "");
        foreach (var item in list)
        {
            var si = new SyndicationItem();
            si.Id = item.Id.ToString();
            si.Title = TextSyndicationContent.CreatePlaintextContent(item.Title);
            si.Content = SyndicationContent.CreateXhtmlContent(item.Description);
            si.Links.Add(new SyndicationLink(new Uri("http://www.pigeoncms.com")));
            si.PublishDate = item.DateInserted;

            items.Add(si);
        }
        feed.Items = items;

        return feed;
    }

    
    public void ProcessRequest(HttpContext context)
    {
        const string CacheKeyPrefix = "Rss";

        try
        {
            //string sTxt = "";
            var feed = new SyndicationFeed();
            var cache = new CacheManager<SyndicationFeed>(CacheKeyPrefix);
            if (cache.IsEmpty("channel"))
            {
                feed = getFeed();
                cache.Insert("channel", feed);
            }
            else
            {
                feed = cache.GetValue("channel");
            }

            context.Response.ContentType = "application/rss+xml";
            

            Rss20FeedFormatter rssFormatter = new Rss20FeedFormatter(feed);
            var sw = new StringWriter();
            var tw = new XmlTextWriter(sw);
            feed.SaveAsRss20(tw);
            
            context.Response.Write(sw.ToString());
        }
        catch(Exception e)
        {
        }
        finally
        {
            context.Response.OutputStream.Dispose();
        }
    }
}