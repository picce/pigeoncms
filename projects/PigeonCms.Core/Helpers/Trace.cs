using System;
using System.Web;

namespace PigeonCms
{
    /// <summary>
    /// encapsulate trace functionality, avoid trace exception and time loss when tracing is disabled
    /// tnx to http://weblogs.asp.net/plip/articles/111130.aspx
    /// </summary>
    public class Trace
    {
        public static void Write(string Message)
        {
            if (HttpContext.Current.Trace.IsEnabled)
            {
                try
                {
                    HttpContext.Current.Trace.Write(Message.ToString());
                }
                catch (Exception e)
                {
                    HttpContext.Current.Trace.Warn("PigeonCms", "Trace.Write", e);
                }
            }
        }

        public static void Write(string Category, string Message)
        {
            if (HttpContext.Current.Trace.IsEnabled)
            {
                try
                {
                    HttpContext.Current.Trace.Write(Category.ToString(), Message.ToString());
                }
                catch (Exception e)
                {
                    HttpContext.Current.Trace.Warn("PigeonCms", "Trace.Write", e);
                }
            }
        }

        public static void Write(string Category, string Message, System.Exception exe)
        {
            if (HttpContext.Current.Trace.IsEnabled)
            {
                try
                {
                    HttpContext.Current.Trace.Write(Category.ToString(), Message.ToString(), exe);
                }
                catch (Exception e)
                {
                    HttpContext.Current.Trace.Warn("PigeonCms", "Trace.Write", e);
                }
            }
        }

        public static void Warn(string Message)
        {
            if (HttpContext.Current.Trace.IsEnabled)
            {
                try
                {
                    HttpContext.Current.Trace.Warn(Message.ToString());
                }
                catch (Exception e)
                {
                    HttpContext.Current.Trace.Warn("PigeonCms", "Trace.Warn", e);
                }
            }
        }

        public static void Warn(string Category, string Message)
        {
            if (HttpContext.Current.Trace.IsEnabled)
            {
                try
                {
                    HttpContext.Current.Trace.Warn(Category.ToString(), Message.ToString());
                }
                catch (Exception e)
                {
                    HttpContext.Current.Trace.Warn("PigeonCms", "Trace.Warn", e);
                }
            }
        }

        public static void Warn(string Category, string Message, System.Exception exe)
        {
            if (HttpContext.Current.Trace.IsEnabled)
            {
                try
                {
                    HttpContext.Current.Trace.Warn(Category.ToString(), Message.ToString(), exe);
                }
                catch (Exception e)
                {
                    HttpContext.Current.Trace.Warn("PigeonCms", "Trace.Warn", e);
                }
            }
        }
    }
}