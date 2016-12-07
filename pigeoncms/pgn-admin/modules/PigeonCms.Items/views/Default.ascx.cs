using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Caching;
using PigeonCms;
using System.Collections.Generic;
using System.Text;

public partial class Controls_Default : PigeonCms.ItemsControl<Item, ItemsFilter>
{
    public StringBuilder ListString = new StringBuilder();
    public StringBuilder PathString = new StringBuilder();

    
    protected void Page_Load(object sender, EventArgs e)
    {
        int cols = this.RepeatColumns;
        int currCol = 0;
        int counter = 0;
        List<Item> items = this.ItemsList;
        int colWidth = 100;

        if (this.CategoryId > 0)
        {
            Category cat = new CategoriesManager().GetByKey(this.CategoryId);
            PathString.Append(new SectionsManager().GetByKey(cat.SectionId).Title + " > " + cat.Title);
        }
        else
        {
            if (!string.IsNullOrEmpty(this.SearchString))
            {
                PathString.Append(Utility.GetLabel("LblSearch", "cerca") + ": " + this.SearchString);
            }
        }

        if (cols > 0)
        {
            colWidth = 100 / cols;
        }

        foreach (PigeonCms.Item item in items)
        {
            string imgUrl = "";
            string link = "";

            if (!string.IsNullOrEmpty(item.DefaultImage.FileUrl))
            {
                imgUrl = VirtualPathUtility.ToAbsolute("~/Handlers/ImageHandler.ashx") + "?imageUrl=" + item.DefaultImage.FileUrl;
                imgUrl += "&size=m";
            }
            else
            {
                imgUrl = Utility.GetThemedImageSrc("spacer.gif");
            }

            if (currCol == 0)
            {
                ListString.Append("<tr>");
            }

            link = base.GetLinkAddress(item);

            ListString.Append("<td class='" + base.BaseModule.CssClass + "' width='" + colWidth + "%'>"
                + "<a href='" + link + "' class='" + base.BaseModule.CssClass + "'>");
            if (this.ShowImages)
            {
                ListString.Append("<img class='" + base.BaseModule.CssClass + "' src='" + imgUrl + "' alt='" + item.DefaultImage.Title + "' /><br />");
            }
            ListString.Append(item.Title 
                + "</a>"
                + "</td>");

            currCol++;
            counter++;

            //fill remain cols
            if (counter == items.Count)
            {
                for (int i = currCol; i < cols; i++)
                {
                    ListString.Append("<td class='" + base.BaseModule.CssClass + "' width='" + colWidth + "%'>&nbsp;</td>");
                }
            }

            if (currCol == cols)
            {
                ListString.Append("</tr>");
                currCol = 0;
            }
        }
    }
}
