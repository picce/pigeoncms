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
    /// <summary>
    /// DAL for ItemTag obj (in table #__itemTags)
    /// </summary>
    public class ItemTagsManager : TableManager<ItemTag, ItemTagsFilter, int>
    {
        //private int itemId = 0;
        //public int ItemId
        //{
        //    get 
        //    {
        //        if (this.itemId == 0)
        //            this.itemId = -1;
        //        return this.itemId; 
        //    }
        //}

        [DebuggerStepThrough()]
        public ItemTagsManager()
        { 
            this.TableName = "#__itemsTags";
            this.KeyFieldName = "ItemId|TagId";
            
            //this.itemId = itemId;
            //if (itemId <= 0)
            //    throw new ArgumentException("Invalid itemId", "itemId");
        }

        public override Dictionary<string, string> GetList()
        {
            return GetListByItemId(-1);
        }

        public Dictionary<string, string> GetListByItemId(int itemId)
        {
            var res = new Dictionary<string, string>();
            var filter = new ItemTagsFilter();
            filter.ItemId = itemId;
            var list = GetByFilter(filter, "");
            foreach (var item in list)
            {
                string value = item.ItemId.ToString() 
                    + "|" 
                    + item.TagId.ToString();

                res.Add(value, value);
            }
            return res;
        }

        public override List<ItemTag> GetByFilter(ItemTagsFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql;
            var result = new List<ItemTag>();

            try
            {
                //filter.ItemId = this.ItemId;

                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "SELECT t.ItemId, t.TagId " 
                    + " FROM ["+ this.TableName +"] t "
                    + " WHERE t.ItemId > 0 ";
                if (filter.ItemId > 0 || filter.ItemId == -1)
                {
                    sSql += " AND t.ItemId = @ItemId ";
                    p.Add("ItemId", filter.ItemId, null, null, null);
                }
                if (filter.TagId > 0 || filter.TagId == -1)
                {
                    sSql += " AND t.TagId = @TagId ";
                    p.Add("TagId", filter.TagId, null, null, null);
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    sSql += " ORDER BY " + sort;
                }

                result = (List<ItemTag>)myConn.Query<ItemTag>(Database.ParseSql(sSql), p);
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override ItemTag GetByKey(int id)
        {
            throw new NotSupportedException();
        }

        public ItemTag GetByKey(int itemId, int tagId)
        {
            var result = new ItemTag();
            var resultList = new List<ItemTag>();
            var filter = new ItemTagsFilter();

            filter.ItemId = itemId == 0 ? -1 : itemId;
            filter.TagId = tagId == 0 ? -1 : tagId;
            resultList = GetByFilter(filter, "");
            if (resultList.Count > 0)
                result = resultList[0];

            return result;
        }

        public override int Update(ItemTag theObj)
        {
            throw new NotSupportedException();
        }

        public override ItemTag Insert(ItemTag theObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql;

            if (theObj.ItemId <= 0)
                throw new ArgumentException("Invalid ItemId", "ItemId");

            if (theObj.TagId <= 0)
                throw new ArgumentException("Invalid TagId", "TagId");

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "INSERT INTO " + this.TableName
                    + "(ItemId, TagId) VALUES(@ItemId, @TagId) ";
                p.Add("ItemId", theObj.ItemId, null, null, null);
                p.Add("TagId", theObj.TagId, null, null, null);
                
                myConn.ExecuteScalar(Database.ParseSql(sSql), p, null, null, null);
            }
            finally
            {
                myConn.Dispose();
            }
            return theObj;
        }

        public override int DeleteById(int recordId)
        {
            throw new NotSupportedException();
        }

        public int DeleteById(int itemId, int tagId)
        {
            if (itemId <= 0)
                throw new ArgumentException("Invalid ItemId", "ItemId");

            if (tagId <= 0)
                throw new ArgumentException("Invalid TagId", "TagId");

            return delete(itemId, tagId);
        }

        public int DeleteByItemId(int itemId)
        {
            if (itemId <= 0)
                throw new ArgumentException("Invalid ItemId", "ItemId");

            return delete(itemId, 0);
        }

        public int DeleteByTagId(int tagId)
        {
            if (tagId <= 0)
                throw new ArgumentException("Invalid TagId", "TagId");

            return delete(0, tagId);
        }

        private int delete(int itemId, int tagId)
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

                sSql = "DELETE FROM [" + this.TableName + "] WHERE 1=1 ";
                if (itemId > 0)
                {
                    sSql += "AND ItemId = @ItemId ";
                    p.Add("ItemId", itemId, null, null, null);
                }
                if (tagId > 0)
                {
                    sSql += " AND TagId = @TagId ";
                    p.Add("TagId", tagId, null, null, null);
                }

                res = myConn.Execute(Database.ParseSql(sSql), p);
            }
            finally
            {
                myConn.Dispose();
            }
            return res;
        }
    }
}