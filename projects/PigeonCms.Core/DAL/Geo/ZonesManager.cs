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
using PigeonCms;
using Dapper;


namespace PigeonCms.Geo
{
    /// <summary>
    /// DAL for categoria obj (in table categorie)
    /// </summary>
    public class ZonesManager : TableManager<Zone, ZonesFilter, int>
    {

        public ZonesManager()
        {
            this.TableName = "#__geoZones";
            this.KeyFieldName = "Id";
        }

        public override Dictionary<string, string> GetList()
        {
            var res = new Dictionary<string, string>();
            var filter = new ZonesFilter();
            var list = GetByFilter(filter, "");
            foreach (var item in list)
            {
                res.Add(item.Code, item.Name);
            }
            return res;
        }

        public override List<Zone> GetByFilter(ZonesFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql;
            var result = new List<Zone>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "SELECT t.Id, t.CountryCode, t.Code, t.Name, t.Custom1, t.Custom2, t.Custom3 "
                    + " FROM [" + this.TableName + "] t "
                    + " WHERE 1=1 ";

                if (filter.Id > 0 || filter.Id == -1)
                {
                    sSql += " AND Id = @Id ";
                    p.Add("Id", filter.Id, null, null, null);
                }

                if (!string.IsNullOrEmpty(filter.CountryCode))
                {
                    sSql += " AND CountryCode = @CountryCode ";
                    p.Add("CountryCode", filter.CountryCode, null, null, null);
                }

                if (!string.IsNullOrEmpty(filter.Code))
                {
                    sSql += " AND Code = @Code ";
                    p.Add("Code", filter.Code, null, null, null);
                }

                if (!string.IsNullOrEmpty(filter.NameLike))
                {
                    sSql += " AND (Name like @NameLike) ";
                    p.Add("NameLike", "%" + filter.NameLike + "%", null, null, null);
                }

                if (!string.IsNullOrEmpty(sort))
                    sSql += " ORDER BY " + sort;
                else
                    sSql += " ORDER BY t.CountryCode, t.Code ";

                result = (List<Zone>)myConn.Query<Zone>(Database.ParseSql(sSql), p);
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override int Update(Zone theObj)
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

                sSql = "UPDATE [" + this.TableName + "] "
                + " SET CountryCode=@CountryCode, Code=@Code, Name=@Name, "
                + " Custom1=@Custom1, Custom2@Custom2, Custom3=@Custom3 "
                + " WHERE Id = @Id";

                p.Add("Id", theObj.Id, null, null, null);
                p.Add("CountryCode", theObj.CountryCode, null, null, null);
                p.Add("Code", theObj.Code, null, null, null);
                p.Add("Name", theObj.Name, null, null, null);
                p.Add("Custom1", theObj.Custom1, null, null, null);
                p.Add("Custom2", theObj.Custom2, null, null, null);
                p.Add("Custom3", theObj.Custom3, null, null, null);

                result = myConn.Execute(Database.ParseSql(sSql), p);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override Zone Insert(Zone theObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql;

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "INSERT INTO [" + this.TableName + "]"
                + " (CountryCode, Code, Name, Custom1, Custom2, Custom3) "
                + " VALUES(@CountryCode, @Code, @Name, @Custom1, @Custom2, @Custom3) ";

                p.Add("Id", theObj.Id, null, null, null);
                p.Add("CountryCode", theObj.CountryCode, null, null, null);
                p.Add("Code", theObj.Code, null, null, null);
                p.Add("Name", theObj.Name, null, null, null);
                p.Add("Custom1", theObj.Custom1, null, null, null);
                p.Add("Custom2", theObj.Custom2, null, null, null);
                p.Add("Custom3", theObj.Custom3, null, null, null);

                myConn.Execute(Database.ParseSql(sSql), p);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                myConn.Dispose();
            }
            return theObj;
        }

    }
}