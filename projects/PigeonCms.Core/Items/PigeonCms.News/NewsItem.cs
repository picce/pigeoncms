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
using System.ComponentModel;
using System.IO;
using PigeonCms;
using System.Collections.Generic;
using System.Threading;



namespace PigeonCms
{
    public class NewsItem: Item
    {
        public NewsItem() { }

        public NewsItem(Item baseItem)
        { }
    }

    [Serializable]
    public class NewsItemFilter : ItemsFilter
    {
        public NewsItemFilter() 
        {
            //this.ItemType = "PigeonCms.NewsItem";
        }
    }

    //public class NewsItemsManager : ItemsManager<NewsItem, NewsItemFilter>
    //{
    //    public NewsItemsManager(bool checkUserContext)
    //        : base(checkUserContext)
    //    { }

    //    //public override List<NewsItem> GetByFilter(NewsItemFilter filter, string sort)
    //    //{
    //    //    filter.ItemType = "PigeonCms.NewsItem";
    //    //    var list = base.GetByFilter(filter, sort);
    //    //    return list;
    //    //}
    //}
}