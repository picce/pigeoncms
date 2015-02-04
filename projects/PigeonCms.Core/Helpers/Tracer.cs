//#define TRACER
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;

namespace PigeonCms
{
    /// <summary>
    /// custom trace
    /// tnx to http://codebetter.com/blogs/karlseguin/archive/2008/08/15/custom-asp-net-page-tracing.aspx
    /// </summary>

    public class TracerItem
    {
        TracerItemType type = TracerItemType.Info;
        DateTime dated;
        string message = "";
        int delta = 0;
        int elapsed = 0;

        public TracerItemType Type 
        {
            get { return type; }
            set { type = value; } 
        }
        public DateTime Dated 
        { 
            get { return dated; }
            set { dated = value; }
        }
        public string Message 
        {
            get { return message; }
            set { message = value; }
        }
        public int Delta 
        { 
            get { return delta; }
            set { delta = value; }
        }
        public int Elapsed 
        {
            get { return elapsed; }
            set { elapsed = value; }
        }

        public TracerItem() { }

        public TracerItem(DateTime dated, TracerItemType type, string message, int elapsed, int delta)
        {
            this.dated = dated;
            this.type = type;
            this.message = message;
            this.elapsed = elapsed;
            this.delta = delta;
        }
    }

    public enum TracerItemType
    {
        Debug = 0,
        Info,
        Warning,
        Alert,
        Error
    }

    public class Tracer
    {
        public static new string  ToString()
        {
            string res = "";
            foreach (TracerItem item in GetLogs())
            {
                res += "[" + item.Elapsed.ToString() + "ms]" + item.Message + Environment.NewLine;
 
            }
            return res;
        }

        public static List<TracerItem> GetLogs()
        {

            List<TracerItem> items = (List<TracerItem>)HttpContext.Current.Items["__tracer"];
            if (items == null)
            {
                items = new List<TracerItem>();
                HttpContext.Current.Items["__tracer"] = items;
            }
            return items;
        }

        public static void Log(PigeonCms.Module module, string message, params object[] arguments)
        {
            //inject module info
            message = "module(" + module.Id.ToString()
                + "|" + module.ModuleFullName
                + "|" + module.Title + "):" + message;
            Log(message, TracerItemType.Info, arguments);
        }

        public static void Log(string message, params object[] arguments)
        {
            //#if TRACER
            Log(message, TracerItemType.Info, arguments);
            //#endif
        }

        public static void Log(string message, TracerItemType type, params object[] arguments)
        {
            //#if TRACER
            DateTime now = DateTime.Now;
            List<TracerItem> items = GetLogs();
            int delta = items.Count == 0 ? 0 : (int)now.Subtract(items[items.Count - 1].Dated).TotalMilliseconds;
            int elasped = items.Count == 0 ? delta : items[items.Count - 1].Elapsed + delta;
            items.Add(new TracerItem(now, type, string.Format(message, arguments), elasped, delta));
            
            PigeonCms.Trace.Write("TRACER", message);
            //#endif
        }
    }

}