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
    /// DAL for Section obj (in table #__sections)
    /// </summary>
    public class SectionsManager : 
        TableManager<Section, SectionsFilter, int>, 
        ITableManagerWithPermission,
        ITableManagerExternalId<Section>
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
        public SectionsManager(): this(false, false)
        { }

        public SectionsManager(bool checkUserContext, bool writeMode)
        {
            this.TableName = "#__sections";
            this.KeyFieldName = "Id";
            this.checkUserContext = checkUserContext;
            this.writeMode = writeMode;
            if (this.writeMode) this.checkUserContext = true;    //forced
        }

        public override Dictionary<string, string> GetList()
        {
            return GetListByItemType("");
        }

        public Dictionary<string, string> GetListByItemType(string itemType)
        {
            var res = new Dictionary<string, string>();
            var filter = new SectionsFilter();
            filter.ItemType = itemType;
            var list = GetByFilter(filter, "");
            foreach (Section item in list)
            {
                res.Add(item.Id.ToString(), item.Title);
            }
            return res;
        }

        public override List<Section> GetByFilter(SectionsFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            List<Section> result = new List<Section>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT t.Id, t.Enabled, " 
                    + " t.AccessType, t.PermissionId, t.AccessCode, t.AccessLevel, "
                    + " t.WriteAccessType, t.WritePermissionId, t.WriteAccessCode, t.WriteAccessLevel, "
                    + " t.MaxItems, t.MaxAttachSizeKB, t.CssClass, t.ItemType, t.ExtId "
                    + " FROM ["+ this.TableName +"] t "
                    + " WHERE t.Id > 0 ";
                if (filter.Id > 0 || filter.Id == -1)
                {
                    sSql += " AND t.Id = @Id ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Id", filter.Id));
                }
                if (filter.Enabled != Utility.TristateBool.NotSet)
                {
                    sSql += " AND t.Enabled = @Enabled ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Enabled", filter.Enabled));
                }
                if (!string.IsNullOrEmpty(filter.ItemType))
                {
                    sSql += " AND t.ItemType = @ItemType ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ItemType", filter.ItemType));
                }
                if (!string.IsNullOrEmpty(filter.ExtId))
                {
                    sSql += " AND t.ExtId = @ExtId ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ExtId", filter.ExtId));
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    sSql += " ORDER BY " + sort;
                }

                myCmd.CommandText = Database.ParseSql(sSql);
                myRd = myCmd.ExecuteReader();
                while (myRd.Read())
                {
                    var item = new Section();
                    FillObject(item, myRd);
                    result.Add(item);
                }
                myRd.Close();

                //(other loop to avoid multiple reader on same command)
                foreach (Section item in result)
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
                }

                //culture specifics
                foreach (Section item in result)
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

        public override Section GetByKey(int id)
        {
            var result = new Section();
            var resultList = new List<Section>();
            var filter = new SectionsFilter();
            filter.Id = id == 0 ? -1 : id;
            resultList = GetByFilter(filter, "");
            if (resultList.Count > 0)
                result = resultList[0];

            return result;
        }

        public Section GetByExtId(string extId)
        {
            var result = new Section();
            var resultList = new List<Section>();
            var filter = new SectionsFilter();
            
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

        public override int Update(Section theObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbTransaction myTrans = null;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            int result = 0;

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
                + " SET Enabled=@Enabled, "
                + " AccessType=@AccessType, PermissionId=@PermissionId, "
                + " [AccessCode]=@AccessCode, AccessLevel=@AccessLevel, "
                + " WriteAccessType=@WriteAccessType, WritePermissionId=@WritePermissionId, "
                + " [WriteAccessCode]=@WriteAccessCode, WriteAccessLevel=@WriteAccessLevel, "
                + " [MaxItems]=@MaxItems, MaxAttachSizeKB=@MaxAttachSizeKB, "
                + " CssClass=@CssClass, ItemType=@ItemType, ExtId=@ExtId "
                + " WHERE Id = @Id";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", theObj.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Enabled", theObj.Enabled));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CssClass", theObj.CssClass));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ItemType", theObj.ItemType));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ExtId", theObj.ExtId));
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
                //limits
                myCmd.Parameters.Add(Database.Parameter(myProv, "MaxItems", theObj.MaxItems));
                myCmd.Parameters.Add(Database.Parameter(myProv, "MaxAttachSizeKB", theObj.MaxAttachSizeKB));

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

        public override Section Insert(Section newObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbTransaction myTrans = null;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            //Section result = new Section();

            try
            {
                //create read/write permission
                new PermissionProvider().CreatePermissionObj(newObj);

                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                myTrans = myConn.BeginTransaction();
                myCmd.Transaction = myTrans;

                //result = newObj;
                //result.Id = base.GetNextId();

                sSql = "INSERT INTO [" + this.TableName + "](/*Id,*/ Enabled, "
                + " AccessType, PermissionId, AccessCode, AccessLevel, "
                + " WriteAccessType, WritePermissionId, WriteAccessCode, WriteAccessLevel, "
                + " MaxItems, MaxAttachSizeKB, CssClass, ItemType, ExtId) "
                + " VALUES(/*@Id,*/ @Enabled, "
                + " @AccessType, @PermissionId, @AccessCode, @AccessLevel, "
                + " @WriteAccessType, @WritePermissionId, @WriteAccessCode, @WriteAccessLevel, "
                + " @MaxItems, @MaxAttachSizeKB, @CssClass, @ItemType, @ExtId) "
                + " SELECT SCOPE_IDENTITY()";
                myCmd.CommandText = Database.ParseSql(sSql);

                //myCmd.Parameters.Add(Database.Parameter(myProv, "Id", result.Id));//identity
                myCmd.Parameters.Add(Database.Parameter(myProv, "Enabled", newObj.Enabled));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CssClass", newObj.CssClass));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ItemType", newObj.ItemType));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ExtId", newObj.ExtId));
                //read permissions
                myCmd.Parameters.Add(Database.Parameter(myProv, "AccessType", (int)newObj.ReadAccessType));
                myCmd.Parameters.Add(Database.Parameter(myProv, "PermissionId", newObj.ReadPermissionId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "AccessCode", (string)newObj.ReadAccessCode));
                myCmd.Parameters.Add(Database.Parameter(myProv, "AccessLevel", (int)newObj.ReadAccessLevel));
                //write permissions
                myCmd.Parameters.Add(Database.Parameter(myProv, "WriteAccessType", (int)newObj.WriteAccessType));
                myCmd.Parameters.Add(Database.Parameter(myProv, "WritePermissionId", newObj.WritePermissionId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "WriteAccessCode", (string)newObj.WriteAccessCode));
                myCmd.Parameters.Add(Database.Parameter(myProv, "WriteAccessLevel", (int)newObj.WriteAccessLevel));
                //limits
                myCmd.Parameters.Add(Database.Parameter(myProv, "MaxItems", (int)newObj.MaxItems));
                myCmd.Parameters.Add(Database.Parameter(myProv, "MaxAttachSizeKB", (int)newObj.MaxAttachSizeKB));

                newObj.Id = (int)(decimal)myCmd.ExecuteScalar();
                updateCultureText(newObj, myCmd, myProv);
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
            return newObj;
        }

        public int DeleteById(int id, bool deleteChilds)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbTransaction myTrans = null;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myProv.CreateCommand();
            string sSql;
            int res = 0;

            if (!deleteChilds && this.hasChilds(id))
            {
                throw new ArgumentException("current obj has childs");
            }

            try
            {
                var currObj = this.GetByKey(id);
                if (deleteChilds && id>0)
                {
                    //delete all its categories
                    var catman = new CategoriesManager();
                    var catfilter = new CategoriesFilter();
                    catfilter.SectionId = id;
                    var catList = catman.GetByFilter(catfilter, "");
                    foreach (var cat in catList)
                    {
                        catman.DeleteById(cat.Id, true);
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

                sSql = "DELETE FROM [" + this.TableName + "_Culture] WHERE SectionId = @SectionId ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "SectionId", id));
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

        protected override void FillObject(Section result, DbDataReader myRd)
        {
            if (!Convert.IsDBNull(myRd["Id"]))
                result.Id = (int)myRd["Id"];
            if (!Convert.IsDBNull(myRd["Enabled"]))
                result.Enabled = (bool)myRd["Enabled"];
            if (!Convert.IsDBNull(myRd["ItemType"]))
                result.ItemType = (string)myRd["ItemType"];
            if (!Convert.IsDBNull(myRd["ExtId"]))
                result.ExtId = (string)myRd["ExtId"];

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
            //limits
            if (!Convert.IsDBNull(myRd["MaxItems"]))
                result.MaxItems = (int)myRd["MaxItems"];
            if (!Convert.IsDBNull(myRd["MaxAttachSizeKB"]))
                result.MaxAttachSizeKB = (int)myRd["MaxAttachSizeKB"];
        }

        private bool hasChilds(int sectionId)
        {
            bool res = false;
            var man = new CategoriesManager();
            var filter = new CategoriesFilter();
            filter.SectionId = sectionId;
            if (man.GetByFilter(filter, "").Count > 0)
                res = true;
            return res;
        }

        private void getCultureSpecific(Section result, DbDataReader myRd,
        DbCommand myCmd, DbProviderFactory myProv)
        {
            string sSql;
            //culture specific
            sSql = "SELECT CultureName, SectionId, Title, Description "
                + " FROM [" + this.TableName + "_culture] t "
                + " WHERE SectionId = @SectionId ";
            myCmd.CommandText = Database.ParseSql(sSql);
            myCmd.Parameters.Clear();
            myCmd.Parameters.Add(Database.Parameter(myProv, "SectionId", result.Id));
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

        private void updateCultureText(Section theObj, DbCommand myCmd, DbProviderFactory myProv)
        {
            string sSql = "";

            foreach (KeyValuePair<string, string> item in theObj.TitleTranslations)
            {
                string descriptionValue = "";
                theObj.DescriptionTranslations.TryGetValue(item.Key, out descriptionValue);

                sSql = "DELETE FROM [" + this.TableName + "_culture] WHERE CultureName=@CultureName AND SectionId=@SectionId ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "CultureName", item.Key));
                myCmd.Parameters.Add(Database.Parameter(myProv, "SectionId", theObj.Id));
                myCmd.ExecuteNonQuery();

                sSql = "INSERT INTO [" + this.TableName + "_culture](CultureName, SectionId, Title, Description) "
                + " VALUES(@CultureName, @SectionId, @Title, @Description) ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "CultureName", item.Key));
                myCmd.Parameters.Add(Database.Parameter(myProv, "SectionId", theObj.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Title", item.Value));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Description", descriptionValue));
                myCmd.ExecuteNonQuery();
            }
        }

    }
}