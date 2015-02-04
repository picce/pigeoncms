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

public partial class Controls_Debug: PigeonCms.BaseModuleControl
{
    private string hideText = "";
    public string HideText
    {
        get 
        {
            if (string.IsNullOrEmpty(hideText))
            {
                hideText = this.Params["HideText"];
            }
            return hideText; 
        }
        set { hideText = value; }
    }


    public string Content
    {
        get 
        {
            string res = "";
            if (hideText != "0")
            {
                res = Tracer.ToString();
                res = res.Replace("--", "__");
                res = "<!--" + Environment.NewLine + res + "-->";
            }
            else
            {
                List<TracerItem> traceList = Tracer.GetLogs();
                res += "<div id='tracer'>" +
                    "<div class='item header'>" +
                    "<div class='index'>#</div>" +
                    "<div class='elapsed'>elapsed</div>" +
                    "<div class='delta'>delta</div>" +
                    "<div class='message'>message</div>" +
                    "</div>";
                for (int count = 0; count < traceList.Count; ++count)
                {
                    res += "" +
                    "<div class='item'>" + traceList[count].Type + "'>" +
                    "<div class='index'>" + count + 1 + "</div>" +
                    "<div class='elapsed'>" + traceList[count].Elapsed + "ms</div>" +
                    "<div class='delta'>" + traceList[count].Delta + "ms</div>" +
                    "<div class='message'>" + traceList[count].Message + "</div>" +
                    "</div>";
                }
            }
            return res;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }
}
