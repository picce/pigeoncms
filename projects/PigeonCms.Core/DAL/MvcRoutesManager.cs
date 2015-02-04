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
using PigeonCms;
using System.Diagnostics;
using System.Web.Routing;

namespace PigeonCms
{
    /// <summary>
    /// DAL for MvcRoute
    /// see http://www.iansuttle.com/blog/post/ASPNET-MVC-Store-Routes-in-the-Database.aspx
    /// </summary>
    public class MvcRoutesManager : TableManagerWithOrdering<MvcRoute, MvcRoutesFilter, int>
    {
        [DebuggerStepThrough()]
        public MvcRoutesManager()
        {
            this.TableName = "#__routes";
            this.KeyFieldName = "Id";
        }

        public override Dictionary<string, string> GetList()
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            MvcRoutesFilter filter = new MvcRoutesFilter();
            List<MvcRoute> list = GetByFilter(filter, "");
            foreach (MvcRoute item in list)
            {
                res.Add(item.Id.ToString(), item.Name + " > " + item.Pattern);
            }
            return res;
        }

        public override List<MvcRoute> GetByFilter(MvcRoutesFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            List<MvcRoute> result = new List<MvcRoute>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT Id, Name, Pattern, Published, Ordering, "
                    + " CurrMasterPage, CurrTheme, IsCore, UseSsl " 
                    + " FROM ["+ this.TableName +"] t "
                    + " WHERE t.Id > 0 ";
                if (filter.Id > 0 || filter.Id == -1)
                {
                    sSql += " AND t.Id = @Id ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Id", filter.Id));
                }
                if (!string.IsNullOrEmpty(filter.Name))
                {
                    sSql += " AND (t.Name = @Name) ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Name", filter.Name));
                }
                if (!string.IsNullOrEmpty(filter.Pattern))
                {
                    sSql += " AND (t.Pattern = @Pattern) ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Pattern", filter.Pattern));
                }
                if (filter.Published != Utility.TristateBool.NotSet)
                {
                    sSql += " AND t.Published = @Published ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Published", filter.Published));
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    sSql += " ORDER BY " + sort;
                }
                else
                {
                    sSql += " ORDER BY t.Ordering ";
                }

                myCmd.CommandText = Database.ParseSql(sSql);
                myRd = myCmd.ExecuteReader();
                while (myRd.Read())
                {
                    MvcRoute item = new MvcRoute();
                    FillObject(item, myRd);
                    result.Add(item);
                }
                myRd.Close();

                foreach (var item in result)
                {
                    myCmd.Parameters.Clear();
                    setRouteParams(item, myCmd, myProv);
                }
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override MvcRoute GetByKey(int id)
        {
            var result = new MvcRoute();
            var list = new List<MvcRoute>();
            var filter = new MvcRoutesFilter();
            filter.Id = id;
            list = GetByFilter(filter, "");
            if (list.Count > 0)
                result = list[0];
            return result;
        }

        public override int Update(MvcRoute theObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbTransaction myTrans = null;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
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
                myCmd.Connection = myConn;

                myTrans = myConn.BeginTransaction();
                myCmd.Transaction = myTrans;

                sSql = "UPDATE [" + this.TableName + "] "
                + " SET Name=@Name, Pattern=@Pattern, Published=@Published, Ordering=@Ordering, "
                + " CurrMasterPage=@CurrMasterPage, CurrTheme=@CurrTheme, "
                + " IsCore=@IsCore, UseSsl=@UseSsl "
                + " WHERE Id = @Id";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", theObj.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Name", theObj.Name));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Pattern", theObj.Pattern));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Published", theObj.Published));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Ordering", theObj.Ordering));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CurrMasterPage", theObj.CurrMasterPage));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CurrTheme", theObj.CurrTheme));
                myCmd.Parameters.Add(Database.Parameter(myProv, "IsCore", theObj.IsCore));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UseSsl", theObj.UseSsl));

                result = myCmd.ExecuteNonQuery();
                //updateRouteParams(theObj, myCmd, myProv);

                myTrans.Commit();
            }
            catch (Exception e)
            {
                myTrans.Rollback();
                throw e;
            }
            finally
            {
                myTrans.Dispose();
                myConn.Dispose();
            }
            return result;
        }

        public void SetAppRoutes()
        {
            List<MvcRoute> routes = new List<MvcRoute>();
            var filter = new MvcRoutesFilter();
            filter.Published = Utility.TristateBool.True;
            try
            {
                routes = this.GetByFilter(filter, "");
            }
            catch (Exception ex)
            {
                PigeonCms.Tracer.Log("SetAppRoutes():" + ex.ToString(), TracerItemType.Error);
                //default route
                MvcRoute route1 = new MvcRoute();
                route1.Name = "pagesRoute";
                route1.Pattern = "pages/{pagename}";
                //route1.Pattern = "pages/{pagename}/{*pathInfo}";
                routes.Add(route1);
            }
            SetAppRoutes(routes);
        }

        public void SetAppRoutes(List<MvcRoute> routes)
        {
            RouteTable.Routes.Clear();

            var pageRouteHandler = new PigeonCms.PageRouteHandler();
            foreach (MvcRoute route in routes)
            {
                RouteValueDictionary constraints = new RouteValueDictionary();
                RouteValueDictionary defaults = new RouteValueDictionary();

                foreach (MvcRouteParam param in route.ParamsList)
                {
                    defaults.Add(param.Key, param.Value);
                    if (!string.IsNullOrEmpty(param.Constraint))
                    {
                        constraints.Add(param.Key, param.Constraint);
                    }
                }
                try
                {
                    RouteTable.Routes.Add(route.Name,
                        new Route(route.Pattern, pageRouteHandler)
                        {
                            Defaults = defaults,
                            Constraints = constraints
                        });
                }
                catch (Exception ex)
                {
                    PigeonCms.Tracer.Log("SetAppRoutes(List<MvcRoute> routes):" + ex.ToString(), TracerItemType.Error);
                }
            }
        }

        public override MvcRoute Insert(MvcRoute newObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbTransaction myTrans = null;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            MvcRoute result = new MvcRoute();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                myTrans = myConn.BeginTransaction();
                myCmd.Transaction = myTrans;

                result = newObj;
                result.Id = base.GetNextId();
                result.Ordering = base.GetNextOrdering();

                sSql = "INSERT INTO [" + this.TableName + "](Id, Name, Pattern, Published, Ordering, "
                + " CurrMasterPage, CurrTheme, IsCore, UseSsl) "
                + " VALUES(@Id, @Name, @Pattern, @Published, @Ordering, "
                + " @CurrMasterPage, @CurrTheme, @IsCore, @UseSsl) ";
                myCmd.CommandText = Database.ParseSql(sSql);

                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", result.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Name", result.Name));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Pattern", result.Pattern));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Published", result.Published));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Ordering", result.Ordering));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CurrMasterPage", result.CurrMasterPage));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CurrTheme", result.CurrTheme));
                myCmd.Parameters.Add(Database.Parameter(myProv, "IsCore", result.IsCore));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UseSsl", result.UseSsl));

                myCmd.ExecuteNonQuery();
                //updateRouteParams(newObj, myCmd, myProv);

                myTrans.Commit();
            }
            catch (Exception e)
            {
                myTrans.Rollback();
                throw e;
            }
            finally
            {
                myTrans.Dispose();
                myConn.Dispose();
            }
            return result;
        }

        public override int DeleteById(int id)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbTransaction myTrans = null;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myProv.CreateCommand();
            string sSql;
            int res = 0;

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                myTrans = myConn.BeginTransaction();
                myCmd.Transaction = myTrans;

                sSql = "DELETE FROM [" + this.TableName + "] WHERE Id = @Id ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", id));
                res = myCmd.ExecuteNonQuery();

                sSql = "DELETE FROM [#__routeParams] WHERE RouteId = @RouteId ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "RouteId", id));
                myCmd.ExecuteNonQuery();

                myTrans.Commit();
            }
            catch (Exception e)
            {
                myTrans.Rollback();
                throw e;
            }
            finally
            {
                myTrans.Dispose();
                myConn.Dispose();
            }
            return res;
        }

        protected override void FillObject(MvcRoute result, DbDataReader myRd)
        {
            if (!Convert.IsDBNull(myRd["Id"]))
                result.Id = (int)myRd["Id"];
            if (!Convert.IsDBNull(myRd["Name"]))
                result.Name = (string)myRd["Name"];
            if (!Convert.IsDBNull(myRd["Pattern"]))
                result.Pattern = (string)myRd["Pattern"];
            if (!Convert.IsDBNull(myRd["Published"]))
                result.Published = (bool)myRd["Published"];
            if (!Convert.IsDBNull(myRd["Ordering"]))
                result.Ordering = (int)myRd["Ordering"];
            if (!Convert.IsDBNull(myRd["CurrMasterPage"]))
                result.CurrMasterPage = (string)myRd["CurrMasterPage"];
            if (!Convert.IsDBNull(myRd["CurrTheme"]))
                result.CurrTheme = (string)myRd["CurrTheme"];
            if (!Convert.IsDBNull(myRd["IsCore"]))
                result.IsCore = (bool)myRd["IsCore"];
            if (!Convert.IsDBNull(myRd["UseSsl"]))
                result.UseSsl = (bool)myRd["UseSsl"];
        }

        private void setRouteParams(MvcRoute theRoute, DbCommand myCmd, DbProviderFactory myProv)
        {
            DbDataReader myRd = null;
            string sSql;
            var paramsList = new List<MvcRouteParam>();

            sSql = "SELECT RouteId, paramKey, paramValue, paramConstraint, paramDataType "
            + " FROM #__routeParams WHERE RouteId = @RouteId ";
            myCmd.CommandText = Database.ParseSql(sSql);
            myCmd.Parameters.Add(Database.Parameter(myProv, "RouteId", theRoute.Id));
            myRd = myCmd.ExecuteReader();
            while (myRd.Read())
            {
                MvcRouteParam param = new MvcRouteParam();
                if (!Convert.IsDBNull(myRd["RouteId"]))
                    param.RouteId = (int)myRd["RouteId"];
                if (!Convert.IsDBNull(myRd["paramKey"]))
                    param.Key = (string)myRd["paramKey"];
                if (!Convert.IsDBNull(myRd["paramValue"]))
                    param.Value = (string)myRd["paramValue"];
                if (!Convert.IsDBNull(myRd["paramConstraint"]))
                    param.Constraint = (string)myRd["paramConstraint"];

                theRoute.ParamsList.Add(param);
            }
            myRd.Close();
        }

        private static void updateRouteParams(MvcRoute theObj, DbCommand myCmd, DbProviderFactory myProv)
        {
            throw new NotImplementedException("updateRouteParams");

            //string sSql = "DELETE FROM #__routeParams WHERE RouteId=@RouteId ";
            //myCmd.CommandText = Database.ParseSql(sSql);
            //myCmd.Parameters.Clear();
            //myCmd.Parameters.Add(Database.Parameter(myProv, "RouteId", theObj.Id));
            //myCmd.ExecuteNonQuery();

            //foreach (MvcRouteParam param in theObj.ParamsList)
            //{
            //    sSql = "INSERT INTO #__routeParams(RouteId, ParamKey, ParamValue, ParamConstraint, ParamDataType) "
            //        + "VALUES(@RouteId, @ParamKey, @ParamValue, @ParamConstraint, @ParamDataType) ";
            //    myCmd.CommandText = Database.ParseSql(sSql);
            //    myCmd.Parameters.Clear();
            //    myCmd.Parameters.Add(Database.Parameter(myProv, "RouteId", theObj.Id));
            //    myCmd.Parameters.Add(Database.Parameter(myProv, "ParamKey", param.Key));
            //    myCmd.Parameters.Add(Database.Parameter(myProv, "ParamValue", param.Value));
            //    myCmd.Parameters.Add(Database.Parameter(myProv, "ParamConstraint", param.Constraint));
            //    myCmd.Parameters.Add(Database.Parameter(myProv, "ParamDataType", param.DataType));
            //    myCmd.ExecuteNonQuery();
            //}
        }
    }
}