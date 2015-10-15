using System;
using System.Data;
using System.Configuration;
using System.Linq;
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
using System.Web.Caching;
using PigeonCms.Core.Helpers;
using System.IO;
using System.Xml;

namespace PigeonCms
{
    /// <summary>
    /// manage database versions and updates
    /// </summary>
    public class DatabaseUpdateProvider
    {
        private DbVersionsManager dbVersionMan;
        private PigeonCms.Module fakeModule;


        private string componentFullName = "";
        public string ComponentFullName
        {
            get { return this.componentFullName; }
        }

        private List<DbVersion> updatesListFull = null;
        protected List<DbVersion> UpdatesListFull
        {
            get
            {
                if (updatesListFull == null)
                {
                    updatesListFull = getXmlUpdates();
                }
                return updatesListFull;
            }
        }

        private List<DbVersion> updatesListPending = null;
        public List<DbVersion> UpdatesListPending
        {
            get
            {
                if (updatesListPending == null)
                {
                    //filter on all updates
                    updatesListPending = (
                        from upd in this.UpdatesListFull
		                orderby upd.VersionId
                        where upd.VersionId > this.LastVersionInstalled.VersionId
                        select upd
                    ).ToList<DbVersion>(); 
                }
                return updatesListPending;
            }
        }


        private DbVersion lastVersionInstalled = null;
        /// <summary>
        /// last version of current component
        /// </summary>
        public DbVersion LastVersionInstalled
        {
            get
            {
                if (lastVersionInstalled == null)
                {
                    int lastVersionId = dbVersionMan.GetLastVersionId();
                    lastVersionInstalled = dbVersionMan.GetByKey(lastVersionId);
                }
                return lastVersionInstalled;
            }
        }

        public DatabaseUpdateProvider(string componentFullName = "PigeonCms.Core")
        {
            if (string.IsNullOrEmpty(componentFullName))
                throw new ArgumentException("Missing componentFullName", "componentFullName");

            this.componentFullName = componentFullName;

            dbVersionMan = new DbVersionsManager(this.ComponentFullName);

            fakeModule = new PigeonCms.Module();
            fakeModule.UseLog = Utility.TristateBool.True;
            fakeModule.ModuleNamespace = "PigeonCms.Core";
            fakeModule.ModuleName = "DatabaseUpdateProvider";
        }

        public bool ApplyPendingUpdates(out string logResult)
        {
            bool res = true;
            DbProviderFactory myProv = Database.ProviderFactory;
            DbTransaction myTrans = null;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql = "";
            string qryResult = "";

            var fromVersion = new DbVersion();
            var toVersion = new DbVersion();
            logResult = "Updating " + this.ComponentFullName
                + " @from version: [[fromVersion]]"
                + " @to version: [[toVersion]]"
                + " @res: [[res]]"
                + " @summary: [[summary]]";

            try
            {
                //retrieve sql
                int updatesCount = this.UpdatesListPending.Count;
                if (updatesCount > 0)
                {
                    fromVersion = this.UpdatesListPending[0];
                    toVersion = this.UpdatesListPending[updatesCount - 1];
                    
                    foreach (var item in this.UpdatesListPending)
                    {
                        sSql += item.SqlContent;
                    }
                }
            }
            catch (Exception ex)
            {
                res = false;
                qryResult = "err retrieving sql - " + ex.ToString();
            }

            if (res && !string.IsNullOrEmpty(sSql))
            {
                try
                {
                    //execute sql with transation
                    myConn.ConnectionString = Database.ConnString;
                    myConn.Open();
                    myCmd.Connection = myConn;

                    myTrans = myConn.BeginTransaction();
                    myCmd.Transaction = myTrans;

                    sSql = Database.ParseSql(sSql);
                    qryResult = Database.ExecuteQuery(myRd, myCmd, sSql);

                    myTrans.Commit();
                    myTrans.Dispose();
                    myConn.Dispose();
                    res = true;
                }
                catch (Exception ex)
                {
                    res = false;
                    myTrans.Rollback();
                    myTrans.Dispose();
                    myConn.Dispose();

                    qryResult = ex.ToString();
                }

                if (res)
                {
                    //update last version log in local db
                    if (dbVersionMan.GetByKey(toVersion.VersionId).VersionId == 0)
                        dbVersionMan.Insert(toVersion);
                    else
                        dbVersionMan.Update(toVersion);
                }
            }

            logResult = logResult
                .Replace("[[fromVersion]]", fromVersion.VersionId.ToString())
                .Replace("[[toVersion]]", toVersion.VersionId.ToString())
                .Replace("[[res]]", res.ToString())
                .Replace("[[summary]]", qryResult);

            LogProvider.Write(fakeModule, logResult, 
                (res ? TracerItemType.Info : TracerItemType.Error));

            return res;
        }

        private List<DbVersion> getXmlUpdates()
        {
            var res = new List<DbVersion>();
            XmlDocument doc = new XmlDocument();
            string xmlPath = Path.Combine(Config.SettingsPath, this.ComponentFullName);
            xmlPath = HttpContext.Current.Request.MapPath(xmlPath = Path.Combine(xmlPath, "updates.xml"));

            if (System.IO.File.Exists(xmlPath))
            {
                doc.Load(xmlPath);
                XmlNodeList updateNodes = doc.SelectNodes("//updates/item");
                foreach (XmlNode node in updateNodes)
                {
                    var dbVersionItem = new DbVersion();
                    dbVersionItem.ComponentFullName = this.ComponentFullName;
                    dbVersionItem.VersionId = getIntValue(node, "id");
                    dbVersionItem.VersionDate = getDateValue(node, "date");
                    dbVersionItem.VersionDev = getStringValue(node, "dev");
                    dbVersionItem.VersionNotes = getStringValue(node, "notes");
                    dbVersionItem.SqlContent = node.InnerText;

                    if (dbVersionItem.VersionId > 0)
                    {
                        res.Add(dbVersionItem);
                    }
                }

            }
            return res;
        }

        private string getStringValue(XmlNode node, string attrName, string defaultValue = "")
        {
            string res = "";
            if (node.Attributes[attrName] != null)
                res = node.Attributes[attrName].Value;

            if (string.IsNullOrEmpty(res))
                res = defaultValue;

            return res;
        }

        private DateTime getDateValue(XmlNode node, string attrName, DateTime? defaultValue = null)
        {
            DateTime res = DateTime.MinValue;
            string stringValue = getStringValue(node, attrName);
            if (!DateTime.TryParse(stringValue, out res))
            {
                if (defaultValue != null)
                    res = defaultValue.Value;
            }

            return res;
        }

        private int getIntValue(XmlNode node, string attrName, int defaultValue = 0)
        {
            int res = 0;
            string stringValue = getStringValue(node, attrName, defaultValue.ToString());
            int.TryParse(stringValue, out res);

            if (res == 0)
                res = defaultValue;

            return res;
        }

    }
}