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

namespace PigeonCms
{
    /// <summary>
    /// DAL for categoria obj (in table categorie)
    /// </summary>
    public class CategoriesManager : 
        TableManagerWithOrdering<Category, CategoriesFilter, int>, 
        ITableManagerWithPermission,
        ITableManagerExternalId<Category>

    {
        private bool checkUserContext = false;
        private bool writeMode = false;

        public bool CheckUserContext
        {
            get { return checkUserContext; }
        }

        public bool WriteMode
        {
            get { return writeMode; }
        }

        [DebuggerStepThrough()]
        public CategoriesManager(): this(false, false)
        { }

        public CategoriesManager(bool checkUserContext, bool writeMode)
        {
            this.TableName = "#__categories";
            this.KeyFieldName = "Id";
            this.checkUserContext = checkUserContext;
            this.writeMode = writeMode;
            if (this.writeMode) this.checkUserContext = true;    //forced
        }

        public override Dictionary<string, string> GetList()
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            CategoriesFilter filter = new CategoriesFilter();
            List<Category> list = GetByFilter(filter, "SectionId");
            foreach (Category item in list)
            {
                string sectionTitle = "";
                sectionTitle = new SectionsManager().GetByKey(item.SectionId).Title;
                res.Add(item.Id.ToString(), sectionTitle + " > " + item.Title);
            }
            return res;
        }

        public override List<Category> GetByFilter(CategoriesFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            List<Category> result = new List<Category>();
            var sectionsList = new List<Section>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT t.Id, t.SectionId, t.ParentId, t.Enabled, "
                    + " t.Ordering, t.DefaultImageName, "
                    + " t.AccessType, t.PermissionId, t.AccessCode, t.AccessLevel, "
                    + " t.WriteAccessType, t.WritePermissionId, t.WriteAccessCode, t.WriteAccessLevel, "
                    + " sect.AccessType sectAccessType, sect.PermissionId sectPermissionId, "
                    + " sect.AccessCode sectAccessCode, sect.AccessLevel sectAccessLevel, "
                    + " sect.WriteAccessType sectWriteAccessType, sect.WritePermissionId sectWritePermissionId, "
                    + " sect.WriteAccessCode sectWriteAccessCode, sect.WriteAccessLevel sectWriteAccessLevel, "
                    + " t.CssClass, t.ExtId "
                    + " FROM [" + this.TableName + "] t "
                    + " LEFT JOIN [" + this.TableName + "_Culture]c ON t.Id=c.CategoryId "
                    + " LEFT JOIN #__sections sect ON t.SectionId = sect.Id "
                    + " WHERE t.Id > 0 ";
                if (filter.Id > 0 || filter.Id == -1)
                {
                    sSql += " AND t.Id = @Id ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Id", filter.Id));
                }
                if (filter.SectionId > 0 || filter.SectionId == -1)
                {
                    sSql += " AND t.SectionId = @SectionId ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "SectionId", filter.SectionId));
                }
                if (filter.ParentId != -1)
                {
                    sSql += " AND t.ParentId = @ParentId ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ParentId", filter.ParentId));
                }
                if (filter.Enabled != Utility.TristateBool.NotSet)
                {
                    sSql += " AND t.Enabled = @Enabled ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Enabled", filter.Enabled));
                }
                if (!string.IsNullOrEmpty(filter.Alias))
                {
                    sSql += " AND (replace(lower(c.title),' ','-') = @Alias) ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Alias", filter.Alias));
                }
                if (!string.IsNullOrEmpty(filter.ExtId))
                {
                    sSql += " AND t.ExtId = @ExtId ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ExtId", filter.ExtId));
                }

                sSql += "GROUP BY t.Id, t.SectionId, t.ParentId, t.Enabled, "
                    + " t.Ordering, t.DefaultImageName, "
                    + " t.AccessType, t.PermissionId, t.AccessCode, t.AccessLevel, "
                    + " t.WriteAccessType, t.WritePermissionId, t.WriteAccessCode, t.WriteAccessLevel, "
                    + " sect.AccessType, sect.PermissionId, "
                    + " sect.AccessCode, sect.AccessLevel, "
                    + " sect.WriteAccessType, sect.WritePermissionId, "
                    + " sect.WriteAccessCode, sect.WriteAccessLevel,"
                    + " t.CssClass, t.ExtId ";
                if (!string.IsNullOrEmpty(sort))
                    sSql += " ORDER BY " + sort;
                else
                    sSql += " ORDER BY t.SectionId, t.Ordering ";

                myCmd.CommandText = Database.ParseSql(sSql);
                myRd = myCmd.ExecuteReader();
                while (myRd.Read())
                {
                    var item = new Category();
                    FillObject(item, myRd);
                    result.Add(item);

                    if (filter.IdToExpand > 0 && filter.IdToExpand == item.Id)
                    {
                        var expandFilter = new CategoriesFilter();
                        expandFilter.ParentId = filter.IdToExpand;
                        var childs = this.GetByFilter(expandFilter, sort);
                        result.AddRange(childs);
                    }

                    var sec = new Section();
                    fillSection(sec, myRd);
                    if (!sectionsList.Exists(
                        delegate(Section s)
                        {
                            return s.Id == sec.Id;
                        }))
                        sectionsList.Add(sec);

                }
                myRd.Close();

                //(other loop to avoid multiple reader on same command)
                foreach (Category item in result)
                {
                    //load read roles
                    if (item.ReadPermissionId > 0 && item.ReadAccessType != MenuAccesstype.Public)
                        item.ReadRolenames = new PermissionProvider().GetPermissionRoles(item.ReadPermissionId);
                    //load write roles
                    if (item.WritePermissionId > 0 && item.WriteAccessType != MenuAccesstype.Public)
                        item.WriteRolenames = new PermissionProvider().GetPermissionRoles(item.WritePermissionId);
                }
                if (this.CheckUserContext)
                {
                    result.RemoveAll(new PermissionProvider().IsItemNotAllowed);
                    if (this.WriteMode)
                        result.RemoveAll(new PermissionProvider().IsItemNotAllowedForWrite);

                    //check parent sections
                    loadSectionsRoles(sectionsList);
                    sectionsList.RemoveAll(new PermissionProvider().IsItemNotAllowed);
                    if (this.WriteMode)
                        sectionsList.RemoveAll(new PermissionProvider().IsItemNotAllowedForWrite);
                    //remove items for not allowed sections
                    result.RemoveAll(new SectionPredicate(sectionsList).IsItemNotInSection);
                }

                //culture specifics
                foreach (var item in result)
                {
                    getCultureSpecific(item, myRd, myCmd, myProv);
                }
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public Category GetByAlias(string alias)
        {
            var result = new Category();
            var resultList = new List<Category>();
            var filter = new CategoriesFilter();
            filter.Alias = alias;
            resultList = GetByFilter(filter, "");
            if (resultList.Count > 0)
                result = resultList[0];
            return result;
        }

        public override Category GetByKey(int id)
        {
            var result = new Category();
            var resultList = new List<Category>();
            var filter = new CategoriesFilter();

            if (id <= 0)
                return result;

            filter.Id = id;
            resultList = GetByFilter(filter, "");
            if (resultList.Count > 0)
                result = resultList[0];

            return result;
        }

        public Category GetByExtId(string extId)
        {
            var result = new Category();
            var resultList = new List<Category>();
            var filter = new CategoriesFilter();

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

        public override int Update(Category theObj)
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
                //fill ReadPermissionId and WritePermissionId before trans
                new PermissionProvider().UpdatePermissionObj(theObj);

                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                myTrans = myConn.BeginTransaction();
                myCmd.Transaction = myTrans;

                sSql = "UPDATE [" + this.TableName + "] "
                + " SET SectionId=@SectionId, ParentId=@ParentId, Enabled=@Enabled, "
                + " Ordering=@Ordering, DefaultImageName=@DefaultImageName, "
                + " AccessType=@AccessType, PermissionId=@PermissionId, "
                + " [AccessCode]=@AccessCode, AccessLevel=@AccessLevel, "
                + " WriteAccessType=@WriteAccessType, WritePermissionId=@WritePermissionId, "
                + " [WriteAccessCode]=@WriteAccessCode, WriteAccessLevel=@WriteAccessLevel, "
                + " CssClass=@CssClass, ExtId=@ExtId "
                + " WHERE Id = @Id";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", theObj.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "SectionId", theObj.SectionId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ParentId", theObj.ParentId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Enabled", theObj.Enabled));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Ordering", theObj.Ordering));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DefaultImageName", theObj.DefaultImageName));
                //read permissions
                myCmd.Parameters.Add(Database.Parameter(myProv, "AccessType", theObj.ReadAccessType));
                myCmd.Parameters.Add(Database.Parameter(myProv, "PermissionId", theObj.ReadPermissionId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "AccessCode", theObj.ReadAccessCode));
                myCmd.Parameters.Add(Database.Parameter(myProv, "AccessLevel", theObj.ReadAccessLevel));
                //write permissions
                myCmd.Parameters.Add(Database.Parameter(myProv, "WriteAccessType", theObj.WriteAccessType));
                myCmd.Parameters.Add(Database.Parameter(myProv, "WritePermissionId", theObj.WritePermissionId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "WriteAccessCode", theObj.WriteAccessCode));
                myCmd.Parameters.Add(Database.Parameter(myProv, "WriteAccessLevel", theObj.WriteAccessLevel));

                myCmd.Parameters.Add(Database.Parameter(myProv, "CssClass", theObj.CssClass));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ExtId", theObj.ExtId));

                result = myCmd.ExecuteNonQuery();
                updateCultureText(theObj, myCmd, myProv);
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

        public override Category Insert(Category newObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbTransaction myTrans = null;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            Category result = new Category();

            if (newObj.SectionId == 0)
                throw new ArgumentException("Category section missing");

            try
            {
                //create read/write permission
                new PermissionProvider().CreatePermissionObj(newObj);

                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                myTrans = myConn.BeginTransaction();
                myCmd.Transaction = myTrans;

                result = newObj;
                result.Ordering = base.GetNextOrdering();

                sSql = "INSERT INTO [" + this.TableName + "](/*Id,*/ SectionId, ParentId, Enabled, Ordering, DefaultImageName, "
                + " AccessType, PermissionId, AccessCode, AccessLevel, "
                + " WriteAccessType, WritePermissionId, WriteAccessCode, WriteAccessLevel, "
                + " CssClass, ExtId) "
                + " VALUES(/*@Id,*/ @SectionId, @ParentId, @Enabled, @Ordering, @DefaultImageName, "
                + " @AccessType, @PermissionId, @AccessCode, @AccessLevel, "
                + " @WriteAccessType, @WritePermissionId, @WriteAccessCode, @WriteAccessLevel, "
                + " @CssClass, @ExtId) "
                + " SELECT SCOPE_IDENTITY()";
                myCmd.CommandText = Database.ParseSql(sSql);

                //myCmd.Parameters.Add(Database.Parameter(myProv, "Id", result.Id));//identity
                myCmd.Parameters.Add(Database.Parameter(myProv, "SectionId", result.SectionId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ParentId", result.ParentId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Enabled", result.Enabled));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Ordering", result.Ordering));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DefaultImageName", result.DefaultImageName));
                //read permissions
                myCmd.Parameters.Add(Database.Parameter(myProv, "AccessType", (int)result.ReadAccessType));
                myCmd.Parameters.Add(Database.Parameter(myProv, "PermissionId", result.ReadPermissionId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "AccessCode", (string)result.ReadAccessCode));
                myCmd.Parameters.Add(Database.Parameter(myProv, "AccessLevel", (int)result.ReadAccessLevel));
                //write permissions
                myCmd.Parameters.Add(Database.Parameter(myProv, "WriteAccessType", (int)result.WriteAccessType));
                myCmd.Parameters.Add(Database.Parameter(myProv, "WritePermissionId", result.WritePermissionId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "WriteAccessCode", (string)result.WriteAccessCode));
                myCmd.Parameters.Add(Database.Parameter(myProv, "WriteAccessLevel", (int)result.WriteAccessLevel));

                myCmd.Parameters.Add(Database.Parameter(myProv, "CssClass", result.CssClass));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ExtId", result.ExtId));

                result.Id = (int)(decimal)myCmd.ExecuteScalar();
                updateCultureText(result, myCmd, myProv);
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

        public int DeleteById(int id, bool deleteChilds)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbTransaction myTrans = null;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myProv.CreateCommand();
            string sSql;
            int res = 0;

            if (!deleteChilds)
            {
                if (this.hasItems(id))
                    throw new ArgumentException("current obj has items");
                if (this.hasChilds(id))
                    throw new ArgumentException("current obj has childs");
            }

            try
            {
                var currObj = this.GetByKey(id);
                if (deleteChilds && id > 0)
                {
                    //delete all its items
                    var itemsManager = new ItemsManager<Item, ItemsFilter>();
                    var itemsFilter = new ItemsFilter();
                    itemsFilter.CategoryId = id;
                    var itemsList = itemsManager.GetByFilter(itemsFilter, "");
                    foreach (var item in itemsList)
                    {
                        itemsManager.DeleteById(item.Id);
                    }
                }

                currObj.DeleteImages();
                currObj.DeleteFiles();
                new PermissionProvider().RemovePermissionById(currObj.ReadPermissionId);
                new PermissionProvider().RemovePermissionById(currObj.WritePermissionId);

                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                myTrans = myConn.BeginTransaction();
                myCmd.Transaction = myTrans;

                sSql = "DELETE FROM [" + this.TableName + "] WHERE Id = @Id ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", id));
                res = myCmd.ExecuteNonQuery();

                sSql = "DELETE FROM [" + this.TableName + "_Culture] WHERE CategoryId = @CategoryId ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "CategoryId", id));
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

        public override int DeleteById(int id)
        {
            return this.DeleteById(id, false);
        }

        private class SectionPredicate
        {
            private List<Section> list = null;
            public SectionPredicate(List<Section> list)
            {
                this.list = list;
            }

            public bool IsItemNotInSection(Category item)
            {
                foreach (var s in list)
                    if (s.Id == item.SectionId) return false;
                return true;
            }
        }

        private void loadSectionsRoles(List<Section> list)
        {
            foreach (var item in list)
            {
                //load read roles
                if (item.ReadPermissionId > 0 && item.ReadAccessType != MenuAccesstype.Public)
                    item.ReadRolenames = new PermissionProvider().GetPermissionRoles(item.ReadPermissionId);
                //load write roles
                if (item.WritePermissionId > 0 && item.WriteAccessType != MenuAccesstype.Public)
                    item.WriteRolenames = new PermissionProvider().GetPermissionRoles(item.WritePermissionId);
            }
        }

        /// <summary>
        /// fill Section security context
        /// </summary>
        /// <param name="result"></param>
        /// <param name="myRd"></param>
        private void fillSection(Section result, DbDataReader myRd)
        {
            if (!Convert.IsDBNull(myRd["SectionId"]))
                result.Id = (int)myRd["SectionId"];
            //read permissions
            if (!Convert.IsDBNull(myRd["SectAccessType"]))
                result.ReadAccessType = (MenuAccesstype)int.Parse(myRd["SectAccessType"].ToString());
            if (!Convert.IsDBNull(myRd["SectPermissionId"]))
                result.ReadPermissionId = (int)myRd["SectPermissionId"];
            if (!Convert.IsDBNull(myRd["SectAccessCode"]))
                result.ReadAccessCode = (string)myRd["SectAccessCode"];
            if (!Convert.IsDBNull(myRd["SectAccessLevel"]))
                result.ReadAccessLevel = (int)myRd["SectAccessLevel"];
            //write permissions
            if (!Convert.IsDBNull(myRd["SectWriteAccessType"]))
                result.WriteAccessType = (MenuAccesstype)int.Parse(myRd["SectWriteAccessType"].ToString());
            if (!Convert.IsDBNull(myRd["SectWritePermissionId"]))
                result.WritePermissionId = (int)myRd["SectWritePermissionId"];
            if (!Convert.IsDBNull(myRd["SectWriteAccessCode"]))
                result.WriteAccessCode = (string)myRd["SectWriteAccessCode"];
            if (!Convert.IsDBNull(myRd["SectWriteAccessLevel"]))
                result.WriteAccessLevel = (int)myRd["SectWriteAccessLevel"];
        }

        protected override void FillObject(Category result, DbDataReader myRd)
        {
            if (!Convert.IsDBNull(myRd["Id"]))
                result.Id = (int)myRd["Id"];
            if (!Convert.IsDBNull(myRd["SectionId"]))
                result.SectionId = (int)myRd["SectionId"];
            if (!Convert.IsDBNull(myRd["ParentId"]))
                result.ParentId = (int)myRd["ParentId"];
            if (!Convert.IsDBNull(myRd["Enabled"]))
                result.Enabled = (bool)myRd["Enabled"];
            if (!Convert.IsDBNull(myRd["Ordering"]))
                result.Ordering = (int)myRd["Ordering"];
            if (!Convert.IsDBNull(myRd["DefaultImageName"]))
                result.DefaultImageName = myRd["DefaultImageName"].ToString();
            //read permissions
            if (!Convert.IsDBNull(myRd["AccessType"]))
                result.ReadAccessType = (MenuAccesstype)int.Parse(myRd["AccessType"].ToString());
            if (!Convert.IsDBNull(myRd["PermissionId"]))
                result.ReadPermissionId = (int)myRd["PermissionId"];
            if (!Convert.IsDBNull(myRd["AccessCode"]))
                result.ReadAccessCode = (string)myRd["AccessCode"];
            if (!Convert.IsDBNull(myRd["AccessLevel"]))
                result.ReadAccessLevel = (int)myRd["AccessLevel"];
            //write permissions
            if (!Convert.IsDBNull(myRd["WriteAccessType"]))
                result.WriteAccessType = (MenuAccesstype)int.Parse(myRd["WriteAccessType"].ToString());
            if (!Convert.IsDBNull(myRd["WritePermissionId"]))
                result.WritePermissionId = (int)myRd["WritePermissionId"];
            if (!Convert.IsDBNull(myRd["WriteAccessCode"]))
                result.WriteAccessCode = (string)myRd["WriteAccessCode"];
            if (!Convert.IsDBNull(myRd["WriteAccessLevel"]))
                result.WriteAccessLevel = (int)myRd["WriteAccessLevel"];
            
            if (!Convert.IsDBNull(myRd["CssClass"]))
                result.CssClass = (string)myRd["CssClass"];
            if (!Convert.IsDBNull(myRd["ExtId"]))
                result.ExtId = (string)myRd["ExtId"];
        }

        protected override int GetPreviousRecordInOrder(int ordering, int currentRecordId)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            int result = currentRecordId;

            try
            {
                var o1 = new PigeonCms.Category();
                o1 = this.GetByKey(currentRecordId);

                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT TOP 1 t.Id "
                + " FROM [" + this.TableName + "] t "
                + " LEFT JOIN #__sections sect ON t.SectionId = sect.Id "
                + " WHERE t.Ordering < @Ordering  "
                + " AND t.SectionId = @SectionId "
                + " ORDER BY t.Ordering DESC ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Ordering", ordering));
                myCmd.Parameters.Add(Database.Parameter(myProv, "SectionId", o1.Section.Id));
                myRd = myCmd.ExecuteReader();
                if (myRd.Read())
                {
                    if (myRd[0] != DBNull.Value)
                    {
                        result = (int)myRd[0];
                    }
                }
                myRd.Close();
                //se nn trovo un record prendo quello precedente per chiave (per init tabella)
                if (result == 0)
                {
                    sSql = "SELECT TOP 1 [Id] FROM " + TableName
                        + " WHERE [" + KeyFieldName + "] < @currentRecordId ORDER BY t.Ordering ASC ";
                    myCmd.CommandText = Database.ParseSql(sSql);
                    myCmd.Parameters.Add(Database.Parameter(myProv, "currentRecordId", currentRecordId));
                    myRd = myCmd.ExecuteReader();
                    if (myRd.Read())
                    {
                        if (myRd[0] != DBNull.Value)
                        {
                            result = (int)myRd[0];
                        }
                    }
                    myRd.Close();
                }
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        protected override int GetNextRecordInOrder(int ordering, int currentRecordId)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            int result = currentRecordId;

            try
            {
                var o1 = new PigeonCms.Category();
                o1 = this.GetByKey(currentRecordId);

                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT TOP 1 t.Id "
                + " FROM [" + this.TableName + "] t "
                + " LEFT JOIN #__sections sect ON t.SectionId = sect.Id "
                + " WHERE t.Ordering > @Ordering "
                + " AND t.SectionId = @SectionId "
                + " ORDER BY t.Ordering ASC ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Ordering", ordering));
                myCmd.Parameters.Add(Database.Parameter(myProv, "SectionId", o1.Section.Id));
                myRd = myCmd.ExecuteReader();
                if (myRd.Read())
                {
                    if (myRd[0] != DBNull.Value)
                    {
                        result = (int)myRd[0];
                    }
                }
                myRd.Close();
                //se nn trovo un record prendo quello successivo per chiave (per init tabella)
                if (result == currentRecordId)
                {
                    sSql = "SELECT TOP 1 [Id] FROM " + TableName
                        + " WHERE [" + KeyFieldName + "] > @currentRecordId ORDER BY Ordering, [" + KeyFieldName + "] ";
                    myCmd.CommandText = Database.ParseSql(sSql);
                    myCmd.Parameters.Add(Database.Parameter(myProv, "currentRecordId", currentRecordId));
                    myRd = myCmd.ExecuteReader();
                    if (myRd.Read())
                    {
                        if (myRd[0] != DBNull.Value)
                        {
                            result = (int)myRd[0];
                        }
                    }
                    myRd.Close();
                }
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        private bool hasChilds(int categoryId)
        {
            bool res = false;
            var filter = new CategoriesFilter();
            filter.ParentId = categoryId;
            if (this.GetByFilter(filter, "").Count > 0)
                res = true;
            return res;
        }

        private bool hasItems(int categoryId)
        {
            bool res = false;
            var man = new ItemsManager<Item, ItemsFilter>();
            var filter = new ItemsFilter();
            filter.CategoryId = categoryId;
            if (man.GetByFilter(filter, "").Count > 0)
                res = true;
            return res;
        }


        private void getCultureSpecific(Category result, DbDataReader myRd,
            DbCommand myCmd, DbProviderFactory myProv)
        {
            string sSql;
            //culture specific
            sSql = "SELECT CultureName, CategoryId, Title, Description "
                + " FROM [" + this.TableName + "_culture] t "
                + " WHERE CategoryId = @CategoryId ";
            myCmd.CommandText = Database.ParseSql(sSql);
            myCmd.Parameters.Clear();
            myCmd.Parameters.Add(Database.Parameter(myProv, "CategoryId", result.Id));
            myRd = myCmd.ExecuteReader();
            while (myRd.Read())
            {
                if (!Convert.IsDBNull(myRd["Title"]))
                    result.TitleTranslations.Add((string)myRd["cultureName"], (string)myRd["Title"]);
                if (!Convert.IsDBNull(myRd["Description"]))
                    result.DescriptionTranslations.Add((string)myRd["cultureName"], (string)myRd["Description"]);
            }
            myRd.Close();
        }

        private void updateCultureText(Category theObj, DbCommand myCmd, DbProviderFactory myProv)
        {
            string sSql = "";
            foreach (KeyValuePair<string, string> item in theObj.TitleTranslations)
            {
                string descriptionValue = "";
                theObj.DescriptionTranslations.TryGetValue(item.Key, out descriptionValue);
                if (descriptionValue == null)
                    descriptionValue = "";

                sSql = "DELETE FROM [" + this.TableName + "_culture] WHERE CultureName=@CultureName AND CategoryId=@CategoryId ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "CultureName", item.Key));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CategoryId", theObj.Id));
                myCmd.ExecuteNonQuery();

                sSql = "INSERT INTO [" + this.TableName + "_culture](CultureName, CategoryId, Title, Description) "
                + " VALUES(@CultureName, @CategoryId, @Title, @Description) ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "CultureName", item.Key));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CategoryId", theObj.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Title", item.Value));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Description", descriptionValue));
                myCmd.ExecuteNonQuery();
            }
        }

    }
}