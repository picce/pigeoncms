using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using StackExchange.Dapper;

namespace PigeonCms
{
    public class AttributeValuesManager : TableManagerWithOrdering<AttributeValue, AttributeValueFilter, int>, ITableManager
    {
        [DebuggerStepThrough()]
        public AttributeValuesManager()
        {
            this.TableName = "#__attributesValues";
            this.KeyFieldName = "id";
        }

        public override List<AttributeValue> GetByFilter(AttributeValueFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql;
            string topItems = "";
            var result = new List<AttributeValue>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                if (filter.NumOfRecords > 0)
                {
                    topItems = "TOP " + filter.NumOfRecords.ToString();
                }
                sSql = "SELECT " + topItems + " Id, AttributeId, ValueString, Ordering FROM " + this.TableName + " WHERE 1=1 ";
                if (filter.Id > 0)
                {
                    sSql += " AND Id = @Id ";
                    p.Add("Id", filter.Id, null, null, null);
                }
                if (filter.AttributeId > 0 || filter.AttributeId == -1)
                {
                    sSql += " AND AttributeId = @AttributeId";
                    p.Add("AttributeId", filter.AttributeId, null, null, null);
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    sSql += " ORDER BY " + sort;
                }
                else
                {
                    sSql += " ORDER BY [" + this.KeyFieldName + "] ";
                }
                result = (List<AttributeValue>)myConn.Query<AttributeValue>(Database.ParseSql(sSql), p);
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

         
        public override AttributeValue GetByKey(int id)
        {
            var result = new AttributeValue();
            var list = new List<AttributeValue>();
            var filter = new AttributeValueFilter();
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
            var p = new DynamicParameters();
            string sSql;
            int result = 0;

            if (theObj.Ordering == 0)
            {
                theObj.Ordering = this.GetNextOrdering();
            }

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "UPDATE " + this.TableName + " SET AttributeId=@AttributeId, ValueString=@ValueString, Ordering=@Ordering "
                + " WHERE " + this.KeyFieldName + " = @Id";

                p.Add("Id", theObj.Id, null, null, null);
                p.Add("AttributeId", theObj.AttributeId, null, null, null);
                p.Add("ValueString", theObj.ValueString, null, null, null);
                p.Add("Ordering", theObj.Ordering, null, null, null);

                result = myConn.Execute(Database.ParseSql(sSql), p);
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
            var p = new DynamicParameters();
            string sSql;
            AttributeValue result = new AttributeValue();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                result.AttributeId = newObj.AttributeId;
                result.ValueString = newObj.ValueString;
                result.Ordering = base.GetNextOrdering();

                sSql = "INSERT INTO " + this.TableName + "(AttributeId, ValueString, Ordering) "
                + "VALUES(@AttributeId, @ValueString, @Ordering) ";

                p.Add("AttributeId", result.AttributeId, null, null, null);
                p.Add("ValueString", newObj.ValueString, null, null, null);
                p.Add("Ordering", result.Ordering, null, null, null);

                myConn.Execute(Database.ParseSql(sSql), p);
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
            var p = new DynamicParameters();

            string sSql;
            int res = 0;

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "DELETE FROM " + this.TableName + " WHERE " + this.KeyFieldName + " = @Id ";
                p.Add("Id", id, null, null, null);
                res = myConn.Execute(Database.ParseSql(sSql), p);
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
