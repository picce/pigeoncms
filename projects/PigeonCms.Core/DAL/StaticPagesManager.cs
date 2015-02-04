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
using System.Diagnostics;
using System.Threading;
using System.Data.Common;
using System.Text.RegularExpressions;


namespace PigeonCms
{
    /// <summary>
    /// DAL for StaticPage (in table staticPages)
    /// </summary>
    public class StaticPagesManager: TableManager<StaticPage, StaticPageFilter,string>, ITableManager
    {
        public const string DEFAULT_PAGE_NAME = "default";

        public StaticPagesManager()
        {
            this.TableName = "#__staticPages ";
            this.KeyFieldName = "PageName";
        }

        public override List<StaticPage> GetByFilter(StaticPageFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            List<StaticPage> result = new List<StaticPage>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT pageName FROM #__staticPages WHERE 1=1 ";
                if (!string.IsNullOrEmpty(filter.PageName))
                {
                    sSql += " AND pageName = @pageName ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "pageName", filter.PageName));
                }
                if (filter.Visible != Utility.TristateBool.NotSet)
                {
                    sSql += " AND Visible = @Visible ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Visible", filter.Visible));
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    sSql += " ORDER BY " + sort;
                }
                else
                {
                    sSql += " ORDER BY pageName ";
                }
                myCmd.CommandText = Database.ParseSql(sSql);
                myRd = myCmd.ExecuteReader();
                while (myRd.Read())
                {
                    StaticPage item = GetStaticPageByName((string)myRd["pageName"]);
                    result.Add(item);
                }
                myRd.Close();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        /// <summary>
        /// dictionary list to use in module admin area (combo)
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string,string> GetList()
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            StaticPageFilter filter = new StaticPageFilter();
            List<StaticPage>list = GetByFilter(filter, "");
            foreach (StaticPage item in list)
            {
                res.Add(item.PageName, item.PageName);
            }
            return res;
        }

        /// <summary>
        /// Indica se la pageName esiste o no
        /// </summary>
        /// <param name="pageName"></param>
        /// <returns>true or false</returns>
        public static bool ExistPage(string pageName)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            bool result = false;

            if (pageName == null) pageName = "";
            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT pageName "
                + " FROM #__staticPages m "
                + " WHERE pageName = @pageName ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "pageName", pageName));
                myRd = myCmd.ExecuteReader();
                if (myRd.Read())
                {
                    result = true;
                }
                myRd.Close();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public StaticPage GetStaticPageByName(string pageName)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            StaticPage result = new StaticPage();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT pageName, visible, showPageTitle "
                + " FROM #__staticPages m "
                + " WHERE pageName = @pageName ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "pageName", pageName));
                myRd = myCmd.ExecuteReader(CommandBehavior.SingleRow);
                if (myRd.Read())
                {
                    if (!Convert.IsDBNull(myRd["pageName"]))
                        result.PageName = (string)myRd["pageName"];
                    if (!Convert.IsDBNull(myRd["visible"]))
                        result.Visible = (bool)myRd["visible"];
                    if (!Convert.IsDBNull(myRd["showPageTitle"]))
                        result.ShowPageTitle = (bool)myRd["showPageTitle"];
                }
                myRd.Close();
                
                //culture specific texts
                sSql = "SELECT cultureName, pageName, pageTitle, pageContent "
                + " FROM #__staticPages_Culture m "
                + " WHERE pageName=@pageName ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "pageName", pageName));
                myRd = myCmd.ExecuteReader();
                while (myRd.Read())
                {
                    if (!Convert.IsDBNull(myRd["pageTitle"]))
                        result.PageTitleTranslations.Add((string)myRd["cultureName"], (string)myRd["pageTitle"]);
                    if (!Convert.IsDBNull(myRd["pageContent"]))
                        result.PageContentTranslations.Add((string)myRd["cultureName"], (string)myRd["pageContent"]);
                }
                myRd.Close();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override int Update(StaticPage theObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbTransaction myTrans = null;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            int result = 0;

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                myTrans = myConn.BeginTransaction();
                myCmd.Transaction = myTrans;

                sSql = "UPDATE #__staticPages SET visible=@visible, "
                + " ShowPageTitle=@ShowPageTitle "
                + " WHERE pageName = @pageName";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "pageName", theObj.PageName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "visible", theObj.Visible));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ShowPageTitle", theObj.ShowPageTitle));
                result = myCmd.ExecuteNonQuery();

                updateCultureText(theObj, myCmd, myProv);

                myTrans.Commit();
            }
            catch (Exception e)
            {
                myTrans.Rollback();
                throw e;
            }
            finally
            {
                myTrans.Dispose();
                myConn.Dispose();
            }
            return result;
        }

        public override StaticPage Insert(StaticPage newObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbTransaction myTrans = null;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            StaticPage result = new StaticPage();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                myTrans = myConn.BeginTransaction();
                myCmd.Transaction = myTrans;

                result.PageName = newObj.PageName;
                result.PageTitleTranslations = newObj.PageTitleTranslations;
                result.PageContentTranslations = newObj.PageContentTranslations;
                result.Visible = newObj.Visible;
                result.ShowPageTitle = newObj.ShowPageTitle;

                sSql = "INSERT INTO #__staticPages(PageName, Visible, ShowPageTitle) "
                + "VALUES(@PageName, @Visible, @ShowPageTitle) ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "PageName", result.PageName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Visible", result.Visible));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ShowPageTitle", result.ShowPageTitle));
                myCmd.ExecuteNonQuery();

                updateCultureText(result, myCmd, myProv);

                myTrans.Commit();
            }
            catch (Exception e)
            {
                myTrans.Rollback();
                throw e;
            }
            finally
            {
                myTrans.Dispose();
                myConn.Dispose();
            }
            return result;
        }

        public int Delete(string pageName)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myProv.CreateCommand();

            string sSql;
            int res = 0;

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "DELETE FROM #__staticPages WHERE pageName = @pageName ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "pageName", pageName));
                res = myCmd.ExecuteNonQuery();

                sSql = "DELETE FROM #__staticPages_Culture WHERE pageName = @pageName ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "pageName", pageName));
                myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return res;
        }

        private static void updateCultureText(StaticPage theObj, DbCommand myCmd, DbProviderFactory myProv)
        {
            string sSql = "";
            foreach (KeyValuePair<string, string> item in theObj.PageTitleTranslations)
            {
                string contentValue = "";
                theObj.PageContentTranslations.TryGetValue(item.Key, out contentValue);

                sSql = "DELETE FROM #__staticPages_Culture WHERE CultureName=@CultureName AND PageName=@PageName ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "CultureName", item.Key));
                myCmd.Parameters.Add(Database.Parameter(myProv, "PageName", theObj.PageName));
                myCmd.ExecuteNonQuery();

                sSql = "INSERT INTO #__staticPages_Culture(CultureName, PageName, PageTitle, PageContent) "
                + "VALUES(@CultureName, @PageName, @PageTitle, @PageContent) ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "CultureName", item.Key));
                myCmd.Parameters.Add(Database.Parameter(myProv, "PageName", theObj.PageName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "PageTitle", item.Value));
                myCmd.Parameters.Add(Database.Parameter(myProv, "PageContent", contentValue));
                myCmd.ExecuteNonQuery();
            }
        }
    }
}