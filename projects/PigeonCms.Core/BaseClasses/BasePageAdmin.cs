using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Threading;
using System.Globalization;
using PigeonCms;
using System.Collections.Generic;

/// <summary>
/// BasePage for the common funtionality in all admin pages
/// </summary>

namespace PigeonCms
{
    public class BasePageAdmin : Page
    {
        public BasePageAdmin()
        {
        }

        /// <summary>
        /// pkey, current record Id
        /// </summary>
        protected int CurrentId
        {
            get
            {
                int res = 0;
                if (ViewState["CurrentId"] != null)
                    res = (int)ViewState["CurrentId"];
                return res;
            }
            set { ViewState["CurrentId"] = value; }
        }

    }

}