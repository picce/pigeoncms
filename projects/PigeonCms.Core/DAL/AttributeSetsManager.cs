using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PigeonCms
{
    public class AttributeSetsManager : TableManager<AttributeSet, AttributeSetFilter, int>, ITableManager
    {
        [DebuggerStepThrough()]
        public AttributeSetsManager()
        {
            this.TableName = "#__attributeSet";
            this.KeyFieldName = "id";
        }

        public override List<AttributeSet> GetByFilter(AttributeSetFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            var result = new List<AttributeSet>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT Id, AttributesList, Name FROM " + this.TableName + " WHERE 1=1 ";
                if (filter.Id > 0)
                {
                    sSql += " AND Id = @Id ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Id", filter.Id));
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
                    var item = new AttributeSet();
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

        public override AttributeSet GetByKey(int id)
        {
            var result = new AttributeSet();
            var list = new List<AttributeSet>();
            var filter = new AttributeSetFilter();
            if (id > 0)
            {
                filter.Id = id;
                list = this.GetByFilter(filter, "");
                if (list.Count > 0)
                    result = list[0];
            }
            return result;
        }

        protected override void FillObject(AttributeSet result, DbDataReader myRd)
        {
            if (!Convert.IsDBNull(myRd["Id"]))
                result.Id = (int)myRd["Id"];
            if (!Convert.IsDBNull(myRd["Name"]))
                result.Name = (string)myRd["Name"];
            if (!Convert.IsDBNull(myRd["AttributesList"]))
                result.AttributesString = (string)myRd["AttributesList"];
        }

        public override int Update(AttributeSet theObj)
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

                sSql = "UPDATE " + this.TableName + " SET Name=@Name, AttributesList=@AttributesList "
                + " WHERE " + this.KeyFieldName + " = @Id";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", theObj.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Name", theObj.Name));
                myCmd.Parameters.Add(Database.Parameter(myProv, "AttributesList", theObj.AttributesString));
                result = myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override AttributeSet Insert(AttributeSet newObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            var result = new AttributeSet();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                result.AttributesString = newObj.AttributesString;
                result.Name = newObj.Name;

                sSql = "INSERT INTO " + this.TableName + "(Name, AttributesList) "
                + "VALUES(@Name, @AttributesList) ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "AttributesList", result.AttributesString));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Name", result.Name));
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
