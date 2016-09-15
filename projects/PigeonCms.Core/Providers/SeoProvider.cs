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

namespace PigeonCms
{
    /// <summary>
    /// manager seo info for  (modules,menus,item)
    /// and other classes that implements ITableWithSeo Interface
    /// </summary>
    public class SeoProvider
    {
		private SeoManager man;

        #region public methods

		public SeoProvider(string resourceSet)
		{
			man = new SeoManager(resourceSet);
		}

        /// <summary>
        /// update obj object in DAL class, before db update
        /// </summary>
        /// <param name="obj"></param>
        public void Save(ITableWithSeo obj)
        {
			if (obj.SeoId == 0)
			{
				obj.SeoId = man.Insert(obj.Seo).Id;
			}
			else
			{
				obj.SeoId = man.Update(obj.Seo);
			}
        }

		public void Remove(ITableWithSeo obj)
        {
			man.DeleteById(obj.SeoId);
			obj.SeoId = 0;
        }

		public Seo Get(ITableWithSeo obj)
        {
			var result = man.GetByKey(obj.SeoId);
            return result;
        }

        #endregion
    }
}