using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PigeonCms
{
    public class AttributeValuesManager : TableManager<AttributeValue, AttributeValueFilter, int>, ITableManager
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
            string topItems = "";
            List<PigeonCms.AttributeValue> result = new List<PigeonCms.AttributeValue>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                if (filter.NumOfRecords > 0)
                {
                    topItems = "TOP " + filter.NumOfRecords.ToString();
                }

                sSql = "SELECT " + topItems + " Id, AttributeId, ValueString FROM " + this.TableName + " WHERE 1=1 ";
                if (filter.Id > 0)
                {
                    sSql += " AND Id = @Id ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Id", filter.Id));
                }
                if (filter.AttributeId > 0)
                {
                    sSql += " AND AttributeId = @AttributeId";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "AttributeId", filter.AttributeId));
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
            if (!Convert.IsDBNull(myRd["ValueString"]))
            {
                result.ValueString = (string)myRd["ValueString"];
                result.ValueTranslations = toDictionary((string)myRd["ValueString"]);
            }

        }
         
        public override PigeonCms.AttributeValue GetByKey(int id)
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

                sSql = "UPDATE " + this.TableName + " SET AttributeId=@AttributeId, ValueString=@ValueString "
                + " WHERE " + this.KeyFieldName + " = @Id";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", theObj.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "AttributeId", theObj.AttributeId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ValueString", toJson(theObj.ValueTranslations)));
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
                result.ValueTranslations = newObj.ValueTranslations;

                sSql = "INSERT INTO " + this.TableName + "(AttributeId, ValueString) "
                + "VALUES(@AttributeId, @ValueString) ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "AttributeId", result.AttributeId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ValueString", toJson(result.ValueTranslations)));
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

        /// <summary>
        /// Convert a json string into Dictionary<string, string>
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private Dictionary<string, string> toDictionary(string json)
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return serializer.Deserialize<Dictionary<string, string>>(json);
        }

        /// <summary>
        /// Convert a Dictionary<string,string> into Json string
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        private string toJson(Dictionary<string, string> dictionary)
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return serializer.Serialize(dictionary);
        }

    }
}
