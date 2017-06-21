using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Dapper;

namespace PigeonCms
{
    public class AttributesManager : TableManagerWithOrdering<Attribute, AttributeFilter, int>, ITableManager
    {
        [DebuggerStepThrough()]
        public AttributesManager()
        {
            this.TableName = "#__attributes";
            this.KeyFieldName = "id";
        }

        public override List<PigeonCms.Attribute> GetByFilter(AttributeFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql;
            var result = new List<PigeonCms.Attribute>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "SELECT Id, Name, AllowCustomValue, Ordering FROM " + this.TableName + " WHERE 1=1 ";

                if (filter.Id > 0)
                {
                    sSql += " AND Id = @Id ";
                    p.Add("Id", filter.Id, null, null, null);
                }
                if (filter.AllowCustomValue != Utility.TristateBool.NotSet)
                {
                    sSql += " AND AllowCustomValue = @AllowCustomValue ";
                    p.Add("AllowCustomValue", filter.AllowCustomValue, null, null, null);
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    sSql += " ORDER BY " + sort;
                }
                else
                {
                    sSql += " ORDER BY [Id] ";
                }

                result = (List<PigeonCms.Attribute>)myConn.Query<PigeonCms.Attribute>(Database.ParseSql(sSql), p);
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override PigeonCms.Attribute GetByKey(int id)
        {
            var result = new PigeonCms.Attribute();
            var list = new List<PigeonCms.Attribute>();
            PigeonCms.AttributeFilter filter = new AttributeFilter();
            if (id > 0)
            {
                filter.Id = id;
                list = this.GetByFilter(filter, "");
                if (list.Count > 0)
                    result = list[0];
            }
            return result;
        }

        public override int Update(Attribute theObj)
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

                sSql = "UPDATE " + this.TableName + " SET Name=@Name, AllowCustomValue=@AllowCustomValue, Ordering=@Ordering"
                + " WHERE " + this.KeyFieldName + " = @Id";
                p.Add("Id", theObj.Id, null, null, null);
                p.Add("Name", theObj.Name, null, null, null);
                p.Add("AllowCustomValue", theObj.AllowCustomValue, null, null, null);
                p.Add("Ordering", theObj.Ordering, null, null, null);

                result = myConn.Execute(Database.ParseSql(sSql), p);
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override Attribute Insert(Attribute newObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql;
            Attribute result = new Attribute();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                result.Name = newObj.Name;
                result.AllowCustomValue = newObj.AllowCustomValue;
                result.Ordering = base.GetNextOrdering();

                sSql = "INSERT INTO " + this.TableName + "(Name, AllowCustomValue, Ordering) "
                + " VALUES(@Name, @AllowCustomValue, @Ordering) "
                + " SELECT SCOPE_IDENTITY() ";

                p.Add("Name", result.Name, null, null, null);
                p.Add("AllowCustomValue", result.AllowCustomValue, null, null, null);
                p.Add("Ordering", result.Ordering, null, null, null);

                result.Id = (int)myConn.ExecuteScalar<decimal>(Database.ParseSql(sSql), p, null, null, null);
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        private bool hasChilds(int attributeId)
        {
            bool res = false;
            var man = new PigeonCms.AttributeValuesManager();
            var filter = new AttributeValueFilter();
            filter.AttributeId = attributeId;
            if (man.GetByFilter(filter, "").Count > 0)
                res = true;
            return res;
        }

        public int Delete(int id, bool deleteRelated = false)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();

            string sSql;
            int res = 0;

            if (!deleteRelated && this.hasChilds(id))
            {
                throw new ArgumentException("current obj has childs");
            }

            try
            {
                var currObj = this.GetByKey(id);

                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "DELETE FROM " + this.TableName + " WHERE " + this.KeyFieldName + " = @Id ";
                p.Add("Id", id, null, null, null);

                myConn.Execute(Database.ParseSql(sSql), p);
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
