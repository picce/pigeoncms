using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PigeonCms
{
    class AttributesManager : TableManager<Attribute, AttributeFilter, int>, ITableManager
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
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            List<PigeonCms.Attribute> result = new List<PigeonCms.Attribute>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT Id, ItemType, Name, AttributeType, AllowCustomValue FROM " + this.TableName + " WHERE 1=1 ";
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
                    PigeonCms.Attribute item = new PigeonCms.Attribute();
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

        public PigeonCms.Attribute GetById(int id)
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

        protected override void FillObject(PigeonCms.Attribute result, DbDataReader myRd)
        {
            if (!Convert.IsDBNull(myRd["Id"]))
                result.Id = (int)myRd["Id"];
            if (!Convert.IsDBNull(myRd["ItemType"]))
                result.ItemType = (string)myRd["ItemType"];
            if (!Convert.IsDBNull(myRd["Name"]))
                result.Name = (string)myRd["Name"];
            if (!Convert.IsDBNull(myRd["AttributeType"]))
                result.AttributeType = (int)myRd["AttributeType"];
            if (!Convert.IsDBNull(myRd["AllowCustomValue"]))
                result.AllowCustomValue = (bool)myRd["AllowCustomValue"];
        }

        public override int Update(Attribute theObj)
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

                sSql = "UPDATE " + this.TableName + " SET ItemType=@ItemType, Name=@Name, AttributeType=@AttributeType, AllowCustomValue=@AllowCustomValue "
                + " WHERE " + this.KeyFieldName + " = @Id";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "ItemType", theObj.ItemType));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Name", theObj.Name));
                myCmd.Parameters.Add(Database.Parameter(myProv, "AttributeType", theObj.AttributeType));
                myCmd.Parameters.Add(Database.Parameter(myProv, "AllowCustomValue", theObj.AllowCustomValue));
                result = myCmd.ExecuteNonQuery();
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
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            Attribute result = new Attribute();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                result.ItemType = newObj.ItemType; 
                result.Name = newObj.Name;
                result.AttributeType = newObj.AttributeType;
                result.AllowCustomValue = newObj.AllowCustomValue;

                sSql = "INSERT INTO " + this.TableName + "(ItemType, Name, AttributeType,AllowCustomValue) "
                + "VALUES(@ItemType, @Name, @AttributeType, @AllowCustomValue) ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "ItemType", result.ItemType));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Name", result.Name));
                myCmd.Parameters.Add(Database.Parameter(myProv, "AttributeType", result.AttributeType));
                myCmd.Parameters.Add(Database.Parameter(myProv, "AllowCustomValue", result.AllowCustomValue));
                myCmd.ExecuteNonQuery();
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
            DbCommand myCmd = myProv.CreateCommand();

            string sSql;
            int res = 0;

            if (!deleteRelated && this.hasChilds(id))
            {
                throw new ArgumentException("current obj has childs");
            }

            try
            {

                var currObj = this.GetByKey(id);
                if (deleteRelated && id > 0)
                {
                    //delete all the attributeValues with AttributeId
                    var attributeValuesManager = new AttributeValuesManager();
                    var attributeValuesFilter = new AttributeValueFilter();
                    attributeValuesFilter.AttributeId = id;
                    var attributeValuesList = attributeValuesManager.GetByFilter(attributeValuesFilter, "");
                    foreach (var attributeValue in attributeValuesList)
                    {
                        attributeValuesManager.DeleteById(attributeValue.Id);
                    }
                }

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
