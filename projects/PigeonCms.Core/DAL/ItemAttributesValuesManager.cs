using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PigeonCms
{
    class ItemAttributesValuesManager : ITableManager
    {
        [DebuggerStepThrough()]
        public ItemAttributesValuesManager()
        {
            
        }

        public List<PigeonCms.ItemAttributeValue> GetByFilter(ItemAttributeValueFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            List<PigeonCms.ItemAttributeValue> result = new List<PigeonCms.ItemAttributeValue>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT ItemId, AttributeId, AttributeValueId, CustomValue FROM #__itemsAttributesValues WHERE 1=1 ";
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
                if (!string.IsNullOrEmpty(sort))
                {
                    sSql += " ORDER BY " + sort;
                }
                else
                {
                    sSql += " ORDER BY [ ItemId ] ";
                }
                myCmd.CommandText = Database.ParseSql(sSql);
                myRd = myCmd.ExecuteReader();
                while (myRd.Read())
                {
                    PigeonCms.ItemAttributeValue item = new PigeonCms.ItemAttributeValue();
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

        protected void FillObject(PigeonCms.ItemAttributeValue result, DbDataReader myRd)
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

        public PigeonCms.ItemAttributeValue GetById(int itemId, int attributeId)
        {
            var result = new PigeonCms.ItemAttributeValue();
            var list = new List<PigeonCms.ItemAttributeValue>();
            PigeonCms.ItemAttributeValueFilter filter = new ItemAttributeValueFilter();
            if (itemId > 0 && attributeId > 0)
            {
                filter.ItemId = itemId;
                filter.AttributeId = attributeId;
                list = this.GetByFilter(filter, "");
                if (list.Count > 0)
                    result = list[0];
            }
            return result;
        }

        public int Update(ItemAttributeValue theObj)
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

                sSql = "UPDATE #__itemsAttributesValues SET AttributeId=@AttributeId, AttributeValueId=@AttributeValueId CustomValueString=@CustomValueString"
                + " WHERE ItemId = @ItemId";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "AttributeId", theObj.AttributeId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "AttributeValueId", theObj.AttributeValueId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomValueString", theObj.CustomValueString));
                result = myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public ItemAttributeValue Insert(ItemAttributeValue newObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            ItemAttributeValue result = new ItemAttributeValue();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                result.ItemId = newObj.ItemId;
                result.AttributeId = newObj.AttributeId;
                result.AttributeValueId = newObj.AttributeValueId;
                result.CustomValueString = newObj.CustomValueString;

                sSql = "INSERT INTO  #__itemsAttributesValues (ItemId, AttributeId, AttributeValueId, CustomValueString) "
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

        public int Delete(int itemId, int attributeId)
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

                sSql = "DELETE FROM #__itemsAttributesValues WHERE ItemId = @ItemId AND AttributeId = @AttributeId";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "ItemId", itemId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "AttributeId", attributeId));
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
