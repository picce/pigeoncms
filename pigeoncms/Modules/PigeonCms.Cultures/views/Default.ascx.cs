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
using System.Collections.Generic;
using PigeonCms;
using PigeonCms.Core.Helpers;
using System.Threading;


public partial class Controls_PigeonCms_Cultures : PigeonCms.BaseModuleControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string culture = Thread.CurrentThread.CurrentCulture.Name.ToLower();
            string res = "<ul class='section-flags'>";
            foreach(var d in Config.CultureList){
                string lanClass = "lang-" + d.Key.ToLower();
                bool selected = d.Key.ToLower() == culture;
                string selectedClass = "";
                if (selected)
                    selectedClass = "selected";
                res += "<li class='iconflag__item " + selectedClass + " " + lanClass + "'>"
                    + "<a href='?len=" + d.Key + "'></a>"
                    + "</li>";
            }
            res += "</ul>";
            LitList.Text = res;
        }
    }

}
