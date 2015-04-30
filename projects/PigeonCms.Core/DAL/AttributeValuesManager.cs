using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PigeonCms
{
    class AttributeValuesManager : TableManager<AttributeValue, AttributeValueFilter, int>, ITableManager
    {
        [DebuggerStepThrough()]
        public AttributeValuesManager()
        {
            this.TableName = "#__attributesValues";
            this.KeyFieldName = "id";
        }

        public override List<PigeonCms.AttributeValue> GetByFilter(AttributeValueFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            List<PigeonCms.AttributeValue> result = new List<PigeonCms.AttributeValue>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT Id, AttributeId, Value, FROM " + this.TableName + " WHERE 1=1 ";
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
                    PigeonCms.AttributeValue item = new PigeonCms.AttributeValue();
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

        protected override void FillObject(PigeonCms.AttributeValue result, DbDataReader myRd)
        {
            if (!Convert.IsDBNull(myRd["Id"]))
                result.Id = (int)myRd["Id"];
            if (!Convert.IsDBNull(myRd["AttributeId"]))
                result.AttributeId = (int)myRd["AttributeId"];
            if (!Convert.IsDBNull(myRd["Value"]))
                result.ValueString = (string)myRd["Value"];
        }

        public PigeonCms.AttributeValue GetById(int id)
        {
            var result = new PigeonCms.AttributeValue();
            var list = new List<PigeonCms.AttributeValue>();
            PigeonCms.AttributeValueFilter filter = new AttributeValueFilter();
            if (id > 0)
            {
                filter.Id = id;
                list = this.GetByFilter(filter, "");
                if (list.Count > 0)
                    result = list[0];
            }
            return result;
        }

        public override int Update(AttributeValue theObj)
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

                sSql = "UPDATE " + this.TableName + " SET AttributeId=@AttributeId, Value=@Value "
                + " WHERE " + this.KeyFieldName + " = @Id";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "ItemType", theObj.AttributeId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Name", theObj.ValueString));
                result = myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override AttributeValue Insert(AttributeValue newObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            AttributeValue result = new AttributeValue();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                result.AttributeId = newObj.AttributeId;
                result.ValueString = newObj.ValueString;

                sSql = "INSERT INTO " + this.TableName + "(AttributeId, Value) "
                + "VALUES(@AttributeId, @Value) ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "AttributeId", result.AttributeId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Value", result.ValueString));
                myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        // TODO: delete records ItemAttributeValue rows
        public int Delete(int id, bool deleteRelated = false)
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

                sSql = "DELETE FROM " + this.TableName + " WHERE " + this.KeyFieldName + " = @Id ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", id));
                res = myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return res;
        }

        public override int DeleteById(int id)
        {
            return this.Delete(id);
        }

    }
}
