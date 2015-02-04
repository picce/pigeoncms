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


namespace PigeonCms
{
    /// <summary>
    /// DAL for PigeonCms.Placeholder (in table placeholders)
    /// </summary>
    public class PlaceholdersManager: TableManager<Placeholder, PlaceholderFilter,string>, ITableManager
    {
        public PlaceholdersManager()
        {
            this.TableName = "#__placeholders";
            this.KeyFieldName = "name";
        }

        public override List<PigeonCms.Placeholder> GetByFilter(PlaceholderFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            List<PigeonCms.Placeholder> result = new List<PigeonCms.Placeholder>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT Name, Content, Visible FROM " + this.TableName + " WHERE 1=1 ";
                if (!string.IsNullOrEmpty(filter.Name))
                {
                    sSql += " AND Name = @Name ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Name", filter.Name));
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
                    sSql += " ORDER BY [" + this.KeyFieldName + "] ";
                }
                myCmd.CommandText = Database.ParseSql(sSql);
                myRd = myCmd.ExecuteReader();
                while (myRd.Read())
                {
                    PigeonCms.Placeholder item = new PigeonCms.Placeholder();
                    FillObject(item, myRd);
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
        public override Dictionary<string, string> GetList()
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            PlaceholderFilter filter = new PlaceholderFilter();
            List<PigeonCms.Placeholder> list = GetByFilter(filter, "");
            foreach (PigeonCms.Placeholder item in list)
            {
                res.Add(item.Name, item.Name);
            }
            return res;
        }

        protected override void FillObject(PigeonCms.Placeholder result, DbDataReader myRd)
        {
            if (!Convert.IsDBNull(myRd["Name"]))
                result.Name = (string)myRd["Name"];
            if (!Convert.IsDBNull(myRd["Content"]))
                result.Content = (string)myRd["Content"];
            if (!Convert.IsDBNull(myRd["visible"]))
                result.Visible = (bool)myRd["visible"];
        }

        public PigeonCms.Placeholder GetByName(string name)
        {
            var result = new PigeonCms.Placeholder();
            var list = new List<PigeonCms.Placeholder>();
            PigeonCms.PlaceholderFilter filter = new PlaceholderFilter();
            if (!string.IsNullOrEmpty(name))
            {
                filter.Name = name;
                list = this.GetByFilter(filter, "");
                if (list.Count > 0)
                    result = list[0];
            }
            return result;
        }

        public override int Update(Placeholder theObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            int result = 0;

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "UPDATE "+ this.TableName +" SET Content=@Content, Visible=@Visible "
                + " WHERE "+ this.KeyFieldName +" = @Name";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Name", theObj.Name));
                myCmd.Parameters.Add(Database.Parameter(myProv, "visible", theObj.Visible));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Content", theObj.Content));
                result = myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override Placeholder Insert(Placeholder newObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            Placeholder result = new Placeholder();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                result.Name = newObj.Name;
                result.Visible = newObj.Visible;
                result.Content = newObj.Content;

                sSql = "INSERT INTO "+ this.TableName +"(Name, Visible, Content) "
                + "VALUES(@Name, @Visible, @Content) ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Name", result.Name));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Visible", result.Visible));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Content", result.Content));
                myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public int Delete(string name)
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

                sSql = "DELETE FROM " + this.TableName + " WHERE "+ this.KeyFieldName +" = @Name ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Name", name));
                res = myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return res;
        }
    }
}