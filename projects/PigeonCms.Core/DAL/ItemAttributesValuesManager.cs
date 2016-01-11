using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PigeonCms
{
    public class ItemAttributesValuesManager : 
        TableManager<ItemAttributeValue, ItemAttributeValueFilter, int>, 
        ITableManager
    {
        [DebuggerStepThrough()]
        public ItemAttributesValuesManager()
        {
            this.TableName = "#__itemsAttributesValues";
            this.KeyFieldName = "ItemId|AttributeId|AttributeValueId";
        }


        public override List<PigeonCms.ItemAttributeValue> GetByFilter(ItemAttributeValueFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            var result = new List<PigeonCms.ItemAttributeValue>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT ItemId, AttributeId, AttributeValueId, CustomValueString "
                + " FROM " + this.TableName + " WHERE 1=1 ";
                if (filter.ItemId > 0)
                {
                    sSql += " AND ItemId = @ItemId ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ItemId", filter.ItemId));
                }
                if (filter.AttributeId > 0)
                {
                    sSql += " AND AttributeId = @AttributeId ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "AttributeId", filter.AttributeId));
                }
                if (filter.AttributeValueId > 0)
                {
                    sSql += " AND AttributeValueId = @AttributeValueId ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "AttributeValueId", filter.AttributeValueId));
                }
                if (filter.ValuesFields)
                {
                    sSql += " AND AttributeValueId > 0 ";
                }
                if (filter.CustomFields)
                {
                    sSql += " AND AttributeValueId = 0 ";
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    sSql += " ORDER BY " + sort;
                }
                else
                {
                    sSql += " ORDER BY ItemId ";
                }
                myCmd.CommandText = Database.ParseSql(sSql);
                myRd = myCmd.ExecuteReader();
                while (myRd.Read())
                {
                    var item = new PigeonCms.ItemAttributeValue();
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

        protected override void FillObject(PigeonCms.ItemAttributeValue result, DbDataReader myRd)
        {
            if (!Convert.IsDBNull(myRd["ItemId"]))
                result.ItemId = (int)myRd["ItemId"];
            if (!Convert.IsDBNull(myRd["AttributeId"]))
                result.AttributeId = (int)myRd["AttributeId"];
            if (!Convert.IsDBNull(myRd["AttributeValueId"]))
                result.AttributeValueId = (int)myRd["AttributeValueId"];
            if (!Convert.IsDBNull(myRd["CustomValueString"]))
                result.CustomValueString = (string)myRd["CustomValueString"];
        }

        public override ItemAttributeValue GetByKey(int id)
        {
            throw new NotSupportedException();
        }

        public PigeonCms.ItemAttributeValue GetById(int itemId, int attributeId, int attributeValueId)
        {
            var result = new PigeonCms.ItemAttributeValue();
            var list = new List<PigeonCms.ItemAttributeValue>();
            var filter = new ItemAttributeValueFilter();
            if (itemId > 0 && attributeId > 0 && attributeValueId > 0)
            {
                filter.ItemId = itemId;
                filter.AttributeId = attributeId;
                filter.AttributeValueId = attributeValueId;
                list = this.GetByFilter(filter, "");
                if (list.Count > 0)
                    result = list[0];
            }
            return result;
        }

        public List<PigeonCms.ItemAttributeValue> GetByItemId(int itemId)
        {
            var list = new List<PigeonCms.ItemAttributeValue>();
            var filter = new ItemAttributeValueFilter();
            if (itemId > 0)
            {
                filter.ItemId = itemId;
                list = this.GetByFilter(filter, "");
            }
            return list;
        }

        public override int Update(ItemAttributeValue theObj)
        {
            throw new NotSupportedException();
        }


        public override ItemAttributeValue Insert(ItemAttributeValue newObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            var result = new ItemAttributeValue();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                result.ItemId = newObj.ItemId;
                result.AttributeId = newObj.AttributeId;
                result.AttributeValueId = newObj.AttributeValueId;
                result.Referred = newObj.Referred;
                result.CustomValueString = string.IsNullOrEmpty(newObj.CustomValueString) ? "" : newObj.CustomValueString;

                sSql = "INSERT INTO " + this.TableName + " (ItemId, AttributeId, AttributeValueId, CustomValueString) "
                + "VALUES(@ItemId, @AttributeId, @AttributeValueId, @CustomValueString) ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "ItemId", result.ItemId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "AttributeId", result.AttributeId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "AttributeValueId", result.AttributeValueId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomValueString", result.CustomValueString));
                myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public int Delete(int itemId, int attributeId, int attributeValueId)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myProv.CreateCommand();

            string sSql;
            int res = 0;

            if (itemId <= 0)
                throw new ArgumentException("Warning: invalid itemId", "itemId");

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "DELETE FROM " + this.TableName + " WHERE 1 = 1 ";
                if (itemId > 0)
                {
                    sSql += " AND ItemId = @ItemId ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ItemId", itemId));
                }
                if (attributeId > 0)
                {
                    sSql += " AND AttributeId = @AttributeId ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "AttributeId", attributeId));
                }
                if (attributeValueId > 0)
                {
                    sSql += " AND AttributeValueId = @AttributeValueId ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "AttributeValueId", attributeValueId));
                }
                myCmd.CommandText = Database.ParseSql(sSql);

                res = myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return res;
        }

        public int DeleteByItemId(int itemId)
        {
            return this.Delete(itemId, 0, 0);
        }

    }
}
