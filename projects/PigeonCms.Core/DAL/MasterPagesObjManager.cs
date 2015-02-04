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
    public class MasterPagesObjManager
    {
        [DebuggerStepThrough()]
        public MasterPagesObjManager()
        {
        }

        public Dictionary<string, string> GetList()
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            MasterPageObjFilter filter = new MasterPageObjFilter();
            List<MasterPageObj> list = GetByFilter(filter);
            foreach (MasterPageObj item in list)
            {
                res.Add(item.Name, item.Name);
            }
            return res;
        }

        public List<MasterPageObj> GetByFilter(MasterPageObjFilter filter)
        {
            List<MasterPageObj> result = new List<MasterPageObj>();
            string searchString = "*.master";
            string filespath = Config.MasterPagesPath;
            filespath = HttpContext.Current.Request.MapPath(filespath);
            DirectoryInfo dir = new DirectoryInfo(filespath);

            if (!string.IsNullOrEmpty(filter.Name))
                searchString = filter.Name + ".master";

            if (dir.Exists)
            {
                FileInfo[] files = dir.GetFiles(searchString);
                foreach (FileInfo file in files)
                {
                    MasterPageObj item = new MasterPageObj();
                    item.Name = file.Name.Replace(".master", "");
                    result.Add(item);
                }
            }
            return result;
        }

        public MasterPageObj GetById(string name)
        {
            MasterPageObj result = new MasterPageObj();
            MasterPageObjFilter filter = new MasterPageObjFilter();
            filter.Name = name;
            List<MasterPageObj> list = new MasterPagesObjManager().GetByFilter(filter);
            if (list.Count > 0)
                result = list[0];

            return result;
        }
    }
}