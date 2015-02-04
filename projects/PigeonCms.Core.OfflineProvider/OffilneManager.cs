using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Xml;

namespace PigeonCms.Core.Offline
{
    /// <summary>
    /// manage website offline settings
    /// </summary>
    public class OfflineManager
    {
        private string template = "";
        private bool offline = false;
        private DateTime offlineDateTime = DateTime.MinValue;
        private DateTime onlineDateTime = DateTime.MinValue;
        private string title = "";
        private string message = "";


        /// <summary>
        /// the name of template (without xsl extension)
        /// </summary>
        public string Template
        {
            [DebuggerStepThrough()]
            get 
            {
                if (string.IsNullOrEmpty(template))
                    template = "default";
                return template; 
            }
            [DebuggerStepThrough()]
            set { template = value; }
        }

        /// <summary>
        /// application offline or not
        /// </summary>
        public bool Offline
        {
            [DebuggerStepThrough()]
            get { return offline; }
            [DebuggerStepThrough()]
            set { offline = value; }
        }

        public DateTime OfflineDateTime
        {
            [DebuggerStepThrough()]
            get { return offlineDateTime; }
            [DebuggerStepThrough()]
            set { offlineDateTime = value; }
        }

        public DateTime OnlineDateTime
        {
            [DebuggerStepThrough()]
            get { return onlineDateTime; }
            [DebuggerStepThrough()]
            set { onlineDateTime = value; }
        }

        /// <summary>
        /// offline page Title
        /// </summary>
        public string Title
        {
            [DebuggerStepThrough()]
            get { return title; }
            [DebuggerStepThrough()]
            set { title = value; }
        }

        /// <summary>
        /// offline page message
        /// </summary>
        public string Message
        {
            [DebuggerStepThrough()]
            get { return message; }
            [DebuggerStepThrough()]
            set { message = value; }
        }

        public OfflineManager()
        {}

        /// <summary>
        /// save offline settings
        /// </summary>
        /// <returns></returns>
        public bool SaveData()
        {
            bool res = true;
            string filename = HttpContext.Current.Request.MapPath(OfflineProvider.XmlFilePath);
            MemoryStream ms = new MemoryStream();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            
            using (XmlWriter w = XmlWriter.Create(ms, settings))
            {
                w.WriteStartDocument();
                w.WriteProcessingInstruction("xml-stylesheet", "type='text/xsl' href='Templates/"+ this.Template +".xsl'"); 

                w.WriteStartElement("offline");

                w.WriteAttributeString("templateFileName", this.template);
                w.WriteAttributeString("offlineDateTime", this.offlineDateTime.ToString());
                w.WriteAttributeString("onlineDateTime", this.onlineDateTime.ToString());

                w.WriteStartElement("title");
                w.WriteCData(this.title);
                w.WriteEndElement();

                w.WriteStartElement("message");
                w.WriteCData(this.message);
                w.WriteEndElement();

                w.WriteEndElement();
                w.WriteEndDocument();
            }

            using (FileStream fs = File.Open(filename, FileMode.Create, FileAccess.Write))
            {
                ms.WriteTo(fs);
                ms.Dispose();
            }

            if (this.Offline)
                writeOfflineEventFile();
            else
                removeOfflineEventFile();

            return res;
        }

        /// <summary>
        /// retrieve offline settings
        /// </summary>
        /// <returns>true if settings file exists</returns>
        public bool GetData()
        {
            bool res = false;
            string filename = HttpContext.Current.Request.MapPath(OfflineProvider.XmlFilePath);
            XmlDocument doc = new XmlDocument();
            XmlNode node;

            if (System.IO.File.Exists(filename))
            {
                FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                doc.Load(fs);
                fs.Close();

                //doc.DocumentElement.
                node = doc.SelectSingleNode("//offline");

                if (node.Attributes["templateFileName"] != null)
                    this.template = node.Attributes["templateFileName"].Value;

                if (node.Attributes["offlineDateTime"] != null)
                {
                    DateTime.TryParse(
                        node.Attributes["offlineDateTime"].Value,
                        out this.offlineDateTime);
                }

                if (node.Attributes["onlineDateTime"] != null)
                {
                    DateTime.TryParse(
                        node.Attributes["onlineDateTime"].Value,
                        out this.onlineDateTime);
                }

                //title
                node = doc.SelectSingleNode("//offline/title");
                this.title = node.InnerText;

                //message
                node = doc.SelectSingleNode("//offline/message");
                this.message = node.InnerText;

                this.Offline = CheckOfflineEventFile();

                res = true;
            }
            return res;
        }

        /// <summary>
        /// check if offline.eve file exist
        /// </summary>
        /// <returns></returns>
        public bool CheckOfflineEventFile()
        {
            string filename = HttpContext.Current.Request.MapPath
                (OfflineProvider.OfflineFolder + "/" + OfflineProvider.OfflineFilename);

            return System.IO.File.Exists(filename);
        }

        /// <summary>
        /// create offline.eve file
        /// </summary>
        /// <returns></returns>
        private void writeOfflineEventFile()
        {
            string filename = HttpContext.Current.Request.MapPath
                (OfflineProvider.OfflineFolder + "/" + OfflineProvider.OfflineFilename);

            using (System.IO.File.Create(filename)) { };
        }

        /// <summary>
        /// remove offline.eve file
        /// </summary>
        /// <returns></returns>
        private void removeOfflineEventFile()
        {
            string filename = HttpContext.Current.Request.MapPath
                (OfflineProvider.OfflineFolder + "/" + OfflineProvider.OfflineFilename);

            System.IO.File.Delete(filename);
        }
    }

}