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
using System.IO;
using System.Data.Common;
using System.Diagnostics;
using StackExchange.Dapper;
using PigeonCms;


namespace PigeonCms
{
    public class TagTypesManager : TableManagerWithOrdering<TagType, TagTypesFilter, int>, 
        ITableManagerExternalId<TagType>
    {
        [DebuggerStepThrough()]
        public TagTypesManager()
        { 
            this.TableName = "#__tagTypes";
            this.KeyFieldName = "Id";        
        }

        public override Dictionary<string, string> GetList()
        {
            return GetListByItemType("");
        }

        public Dictionary<string, string> GetListByItemType(string itemType)
        {
            var res = new Dictionary<string, string>();
            var filter = new TagTypesFilter();
            filter.ItemType = itemType;
            var list = GetByFilter(filter, "");
            foreach (var item in list)
            {
                res.Add(item.Id.ToString(), item.Title);
            }
            return res;
        }

        public override List<TagType> GetByFilter(TagTypesFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql;
            var result = new List<TagType>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "SELECT t.Id, t.ItemType, t.Ordering, t.ExtId " 
                    + " FROM ["+ this.TableName +"] t "
                    + " WHERE t.Id > 0 ";
                if (filter.Id > 0 || filter.Id == -1)
                {
                    sSql += " AND t.Id = @Id ";
                    p.Add("Id", filter.Id, null, null, null);
                }
                if (!string.IsNullOrEmpty(filter.ItemType))
                {
                    sSql += " AND t.ItemType = @ItemType ";
                    p.Add("ItemType", filter.ItemType, null, null, null);
                }
                if (!string.IsNullOrEmpty(filter.ExtId))
                {
                    sSql += " AND t.ExtId = @ExtId ";
                    p.Add("ExtId", filter.ExtId, null, null, null);
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    sSql += " ORDER BY " + sort;
                }

                result = (List<TagType>)myConn.Query<TagType>(Database.ParseSql(sSql), p);

                //culture specifics
                foreach (var item in result)
                {
                    getCultureSpecific(item, myConn);
                }
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override TagType GetByKey(int id)
        {
            var result = new TagType();
            var resultList = new List<TagType>();
            var filter = new TagTypesFilter();

            filter.Id = id == 0 ? -1 : id;
            resultList = GetByFilter(filter, "");
            if (resultList.Count > 0)
                result = resultList[0];

            return result;
        }

        public TagType GetByExtId(string extId)
        {
            var result = new TagType();
            var resultList = new List<TagType>();
            var filter = new TagTypesFilter();
            
            if (string.IsNullOrEmpty(extId))
                return result;  

            filter.ExtId = extId;
            resultList = GetByFilter(filter, "");
            if (resultList.Count > 0)
                result = resultList[0];

            return result;
        }

        public int DeleteByExtId(string extId)
        {
            int res = 0;
            var item = GetByExtId(extId);
            if (item.Id > 0)
                res = this.DeleteById(item.Id);
            
            return res;
        }

        public override int Update(TagType theObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql;
            int result = 0;

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "UPDATE " + this.TableName
                + " SET ItemType = @ItemType, Ordering = @Ordering, "
                + " ExtId = @ExtId "
                + " WHERE " + this.KeyFieldName + " = @Id";
                
                p.Add("Id", theObj.Id, null, null, null);
                p.Add("ItemType", theObj.ItemType, null, null, null);
                p.Add("Ordering", theObj.Ordering, null, null, null);
                p.Add("ExtId", theObj.ExtId, null, null, null);
                
                result = myConn.Execute(Database.ParseSql(sSql), p);
                
                updateCultureText(theObj, myConn);

            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override TagType Insert(TagType theObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql;

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "INSERT INTO " + this.TableName
                    + "(ItemType, Ordering, ExtId) "
                    + " VALUES(@ItemType, @Ordering, @ExtId) "
                    + " SELECT SCOPE_IDENTITY() ";

                p.Add("ItemType", theObj.ItemType, null, null, null);
                p.Add("Ordering", theObj.Ordering, null, null, null);
                p.Add("ExtId", theObj.ExtId, null, null, null);
                
                theObj.Id = (int)(decimal) myConn.ExecuteScalar(Database.ParseSql(sSql), p, null, null, null);
                
                updateCultureText(theObj, myConn);
            }
            finally
            {
                myConn.Dispose();
            }
            return theObj;
        }

        public override int DeleteById(int id)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql;
            int res = 0;

            try
            {
                res = base.DeleteById(id);

                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "DELETE FROM [" + this.TableName + "_culture] WHERE TagTypeId = @TagTypeId ";
                p.Add("TagTypeId", id, null, null, null);

                myConn.Execute(Database.ParseSql(sSql), p);
            }
            finally
            {
                myConn.Dispose();
            }
            return res;
        }

        private void getCultureSpecific(TagType result, DbConnection myConn)
        {

            string sSql = "SELECT CultureName, TagTypeId, Title, Description "
                + " FROM [" + this.TableName + "_culture] t "
                + " WHERE TagTypeId = @TagTypeId ";

            var p = new DynamicParameters();
            p.Add("TagTypeId", result.Id, null, null, null);

            var rows = myConn.Query<dynamic>(Database.ParseSql(sSql), p);
            foreach (var row in rows)
            {
                var fields = row as IDictionary<string, object>;
                if (!Convert.IsDBNull(fields["Title"]))
                    result.TitleTranslations.Add((string)fields["CultureName"], (string)fields["Title"]);
                if (!Convert.IsDBNull(fields["Description"]))
                    result.DescriptionTranslations.Add((string)fields["CultureName"], (string)fields["Description"]);
            }
        }

        private void updateCultureText(TagType theObj, DbConnection myConn)
        {
            string sSql = "";

            foreach (KeyValuePair<string, string> item in theObj.TitleTranslations)
            {
                
                string descriptionValue = "";
                theObj.DescriptionTranslations.TryGetValue(item.Key, out descriptionValue);

                //delete previous entries
                sSql = "DELETE FROM [" + this.TableName + "_culture] WHERE CultureName = @CultureName AND TagTypeId = @TagTypeId ";
                var pd = new DynamicParameters();
                pd.Add("CultureName", item.Key, null, null, null);
                pd.Add("TagTypeId", theObj.Id, null, null, null);
                myConn.Execute(Database.ParseSql(sSql), pd);
                
                
                //re-insert
                sSql = "INSERT INTO [" + this.TableName + "_culture]"
                    + " (CultureName, TagTypeId, Title, Description) "
                    + " VALUES(@CultureName, @TagTypeId, @Title, @Description) ";

                var pi = new DynamicParameters();
                pi.Add("CultureName", item.Key, null, null, null);
                pi.Add("TagTypeId", theObj.Id, null, null, null);
                pi.Add("Title", item.Value, null, null, null);
                pi.Add("Description", descriptionValue, null, null, null);
                myConn.ExecuteScalar(Database.ParseSql(sSql), pi, null, null, null);
            }
        }

    }
}