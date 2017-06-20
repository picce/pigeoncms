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
    public class CountriesManager : TableManager<Country, CountriesFilter, string>
    {

        public CountriesManager()
        {
            this.TableName = "#__geoCountries";
            this.KeyFieldName = "Code";
        }

        public override Dictionary<string, string> GetList()
        {
            var res = new Dictionary<string, string>();
            var filter = new CountriesFilter();
            var list = GetByFilter(filter, "Code");
            foreach (var item in list)
            {
                res.Add(item.Code, item.Name);
            }
            return res;
        }

        public override List<Country> GetByFilter(CountriesFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql;
            var result = new List<Country>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "SELECT t.code, t.iso3, t.continent, t.name, t.custom1, t.custom2, t.custom3 "
                    + " FROM [" + this.TableName + "] t "
                    + " WHERE 1=1 ";

                if (!string.IsNullOrEmpty(filter.Code))
                {
                    sSql += " AND Code = @Code ";
                    p.Add("Code", filter.Code, null, null, null);
                }
                if (!string.IsNullOrEmpty(filter.Iso3))
                {
                    sSql += " AND Iso3 = @Iso3 ";
                    p.Add("Iso3", filter.Iso3, null, null, null);
                }
                if (!string.IsNullOrEmpty(filter.NameLike))
                {
                    sSql += " AND (Name like @NameLike) ";
                    p.Add("NameLike", "%" + filter.NameLike + "%", null, null, null);
                }
                if (!string.IsNullOrEmpty(sort))
                    sSql += " ORDER BY " + sort;
                else
                    sSql += " ORDER BY t.Code ";

                result = (List<Country>)myConn.Query<Country>(Database.ParseSql(sSql), p);
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override Country GetByKey(string code)
        {
            var result = new Country();
            var resultList = new List<Country>();
            var filter = new CountriesFilter();
            if (!string.IsNullOrEmpty(code))
            {
                filter.Code = code;
                resultList = GetByFilter(filter, "");
                if (resultList.Count > 0)
                    result = resultList[0];
            }
            return result;
        }

        public override int Update(Country theObj)
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
                + " SET Iso3=@Iso3, Continent=@Continent, Name=@Name, "
                + " Custom1=@Custom1, Custom2=@Custom2, Custom3=@Custom3 "
                + " WHERE Code = @Code";

                p.Add("Code", theObj.Code, null, null, null);
                p.Add("Iso3", theObj.Iso3, null, null, null);
                p.Add("Continent", theObj.Continent, null, null, null);
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

        public override Country Insert(Country theObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql;

            if (string.IsNullOrEmpty(theObj.Code))
                throw new ArgumentNullException("Invalid Country key field");

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "INSERT INTO [" + this.TableName + "]"
                + " (Code, Iso3, Continent, Name, Custom1, Custom2, Custom3) "
                + " VALUES(@Code, @Iso3, @Continent, @Name, @Custom1, @Custom2, @Custom3) ";

                p.Add("Code", theObj.Code, null, null, null);
                p.Add("Iso3", theObj.Iso3, null, null, null);
                p.Add("Continent", theObj.Continent, null, null, null);
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