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
    /// DAL for PigeonCms.TemplateBlock (in table TemplateBlocks)
    /// </summary>
    public class TemplateBlocksManager: TableManager<TemplateBlock, TemplateBlockFilter, string>, ITableManager
    {
        public TemplateBlocksManager()
        {
            this.TableName = "#__templateBlocks";
            this.KeyFieldName = "Name";
        }

        public override List<TemplateBlock> GetByFilter(TemplateBlockFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            List<TemplateBlock> result = new List<TemplateBlock>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT Name, Title, Enabled, OrderId FROM " + this.TableName + " WHERE 1=1 ";
                if (!string.IsNullOrEmpty(filter.Name))
                {
                    sSql += " AND Name = @Name ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Name", filter.Name));
                }
                if (filter.Enabled != Utility.TristateBool.NotSet)
                {
                    sSql += " AND Enabled = @Enabled ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Enabled", filter.Enabled));
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
                    TemplateBlock item = new TemplateBlock();
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

        protected override void FillObject(TemplateBlock result, DbDataReader myRd)
        {
            if (!Convert.IsDBNull(myRd["Name"]))
                result.Name = (string)myRd["Name"];
            if (!Convert.IsDBNull(myRd["Title"]))
                result.Title = (string)myRd["Title"];
            if (!Convert.IsDBNull(myRd["Enabled"]))
                result.Enabled = (bool)myRd["Enabled"];
            if (!Convert.IsDBNull(myRd["OrderId"]))
                result.OrderId = (int)myRd["OrderId"];
        }

        public override TemplateBlock GetByKey(string name)
        {
            TemplateBlock result =  new TemplateBlock();
            TemplateBlockFilter filter = new TemplateBlockFilter();
            filter.Name = name;
            result = GetByFilter(filter, "")[0];
            return result;
        }

        public override int Update(TemplateBlock theObj)
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

                sSql = "UPDATE "+ this.TableName 
                    + " SET Title=@Title, Enabled=@Enabled, OrderId=@OrderId "
                    + " WHERE "+ this.KeyFieldName +" = @Name";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Name", theObj.Name));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Title", theObj.Title));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Enabled", theObj.Enabled));
                myCmd.Parameters.Add(Database.Parameter(myProv, "OrderId", theObj.OrderId));
                result = myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override TemplateBlock Insert(TemplateBlock newObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            TemplateBlock result = new TemplateBlock();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                result.Name = newObj.Name;
                result.Title = newObj.Title;
                result.Enabled = newObj.Enabled;
                result.OrderId = this.GetNextOrderId();

                sSql = "INSERT INTO " + this.TableName + "(Name, Title, Enabled, OrderId) "
                    + "VALUES(@Name, @Title, @Enabled, @OrderId) ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Name", result.Name));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Title", result.Title));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Enabled", result.Enabled));
                myCmd.Parameters.Add(Database.Parameter(myProv, "OrderId", result.OrderId));
                myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }
    }
}