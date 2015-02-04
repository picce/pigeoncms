using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using System.Data.Common;
using PigeonCms;
using System.Diagnostics;

namespace PigeonCms
{
    public class ThemesObjManager
    {
        [DebuggerStepThrough()]
        public ThemesObjManager()
        {
        }

        public Dictionary<string, string> GetList()
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            ThemeObjFilter filter = new ThemeObjFilter();
            List<ThemeObj> list = GetByFilter(filter);
            foreach (ThemeObj item in list)
            {
                res.Add(item.Name, item.Name);
            }
            return res;
        }

        public List<ThemeObj> GetByFilter(ThemeObjFilter filter)
        {
            List<ThemeObj> result = new List<ThemeObj>();
            string path = HttpContext.Current.Request.MapPath("~/App_Themes");
            DirectoryInfo dir = new DirectoryInfo(path);
            DirectoryInfo[] dirs = dir.GetDirectories();

            foreach (DirectoryInfo currDir in dirs)
            {
                if (currDir.Name.ToLower() != ".svn")
                {
                    ThemeObj item = new ThemeObj(currDir.Name);
                    result.Add(item);
                }
            }
            return result;
        }

        public ThemeObj GetById(string name)
        {
            ThemeObj result = new ThemeObj();
            ThemeObjFilter filter = new ThemeObjFilter();
            filter.Name = name;
            List<ThemeObj> list = new ThemesObjManager().GetByFilter(filter);
            if (list.Count > 0)
                result = list[0];

            return result;
        }
    }
}