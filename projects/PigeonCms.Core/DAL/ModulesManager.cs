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
    /// DAL for Modules obj (table Modules)
    /// </summary>
    public class ModulesManager : TableManagerWithOrdering<Module, ModulesFilter, int>, ITableManagerWithPermission
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

        /// <summary>
        /// CheckUserContext=false
        /// WriteMode=false
        /// </summary>
        [DebuggerStepThrough()]
        public ModulesManager():this(false, false)
        { }

        [DebuggerStepThrough()]
        public ModulesManager(bool checkUserContext, bool writeMode)
        {
            this.TableName = "#__modules";
            this.KeyFieldName = "Id";
            this.checkUserContext = checkUserContext;
            this.writeMode = writeMode;
            if (this.writeMode) this.checkUserContext = true;    //forced
        }

        public override List<Module> GetByFilter(ModulesFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            List<Module> result = new List<Module>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT t.Id, t.Title, t.Content, t.Ordering, t.TemplateBlockName, t.Published, "
                + " t.ModuleName, t.ModuleNamespace, t.DateInserted, t.UserInserted, "
                + " t.DateUpdated, t.UserUpdated, t.AccessType, t.ShowTitle, t.ModuleParams, "
                + " t.IsCore, t.MenuSelection, t.CurrView, t.PermissionId, t.AccessCode, t.AccessLevel, "
                + " t.CssFile, t.CssClass, t.UseCache, t.UseLog, t.DirectEditMode, "
                + " t.WriteAccessType, t.WritePermissionId, t.WriteAccessCode, t.WriteAccessLevel, t.SystemMessagesTo "
                + " FROM #__modules t "
                + " LEFT JOIN #__modulesMenu m ON t.id = m.moduleId  "
                + " LEFT JOIN #__modulesMenuTypes mt ON t.id = mt.moduleId  "
                + " WHERE 1=1 ";
                
                if (filter.Id > 0 || filter.Id == -1)
                {
                    sSql += " AND t.Id = @Id ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Id", filter.Id));
                }
                if (!string.IsNullOrEmpty(filter.TemplateBlockName))
                {
                    sSql += " AND t.TemplateBlockName = @TemplateBlockName ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "TemplateBlockName", filter.TemplateBlockName));
                }
                if (filter.Published != Utility.TristateBool.NotSet)
                {
                    sSql += " AND t.Published = @Published ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Published", filter.Published));
                }
                if (!string.IsNullOrEmpty(filter.ModuleName))
                {
                    sSql += " AND t.ModuleName = @ModuleName ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ModuleName", filter.ModuleName));
                }
                if (!string.IsNullOrEmpty(filter.ModuleNamespace))
                {
                    sSql += " AND t.ModuleNamespace = @ModuleNamespace ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ModuleNamespace", filter.ModuleNamespace));
                }
                if (filter.MenuId > 0 || filter.MenuId == -1 || !string.IsNullOrEmpty(filter.MenuType))
                {
                    sSql += " AND (m.MenuId = @MenuId OR t.MenuSelection = @MenuSelection OR mt.MenuType = @MenuType) ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "MenuId", filter.MenuId));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "MenuSelection", (int)ModulesMenuSelection.AllPages));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "MenuType", filter.MenuType));
                }
                if (filter.IsContent == Utility.TristateBool.True)
                {
                    sSql += " AND (t.MenuSelection = @MenuSelection) ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "MenuSelection", (int)ModulesMenuSelection.MenuContent));
                }
                if (filter.IsContent == Utility.TristateBool.False)
                {
                    sSql += " AND (t.MenuSelection <> @MenuSelection) ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "MenuSelection", (int)ModulesMenuSelection.MenuContent));
                }
                if (filter.IsCore != Utility.TristateBool.NotSet)
                {
                    sSql += " AND t.IsCore = @IsCore ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "IsCore", filter.IsCore));
                }


                sSql += " GROUP BY t.Id, t.Title, t.Content, t.Ordering, t.TemplateBlockName, t.Published, "
                + " t.ModuleName, t.ModuleNamespace, t.DateInserted, t.UserInserted, "
                + " t.DateUpdated, t.UserUpdated, t.AccessType, t.ShowTitle, t.ModuleParams, "
                + " t.IsCore, t.MenuSelection, t.CurrView, t.PermissionId, t.AccessCode, t.AccessLevel, "
                + " t.CssFile, t.CssClass, t.UseCache, t.UseLog, t.DirectEditMode, "
                + " t.WriteAccessType, t.WritePermissionId, t.WriteAccessCode, t.WriteAccessLevel, t.SystemMessagesTo ";
                if (!string.IsNullOrEmpty(sort))
                {
                    sSql += " ORDER BY " + sort;
                }
                else
                {
                    sSql += " ORDER BY Ordering ";
                }
                myCmd.CommandText = Database.ParseSql(sSql);
                myRd = myCmd.ExecuteReader();
                while (myRd.Read())
                {
                    Module item = new Module();
                    FillObject(item, myRd);
                    result.Add(item);
                }
                myRd.Close();

                //(other loop to avoid multiple reader on same command)
                foreach (var item in result)
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

                //load culture specific and modulesMenu list
                foreach (Module item in result)
                {
                    getCultureSpecific(item, myRd, myCmd, myProv);
                    getModulesMenu(item, myRd, myCmd, myProv);
                }
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override Module GetByKey(int id)
        {
            var result = new Module();
            var list = new List<Module>();
            var filter = new ModulesFilter();
            filter.Id = id == 0 ? -1 : id;
            list = GetByFilter(filter, "");
            if (list.Count > 0)
                result = list[0];
            return result;
        }

        public override int Update(Module theObj)
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

                theObj.DateUpdated = DateTime.Now;
                theObj.UserUpdated = PgnUserCurrent.UserName;

                sSql = "UPDATE [" + this.TableName + "] "
                + " SET Content=@Content, Ordering=@Ordering, "
                + " TemplateBlockName=@TemplateBlockName, Published=@Published, "
                + " ModuleName=@ModuleName, ModuleNamespace=@ModuleNamespace, "
                + " DateUpdated=@DateUpdated, UserUpdated=@UserUpdated, AccessType=@AccessType, "
                + " ShowTitle=@ShowTitle, ModuleParams=@ModuleParams, IsCore=@IsCore, "
                + " MenuSelection=@MenuSelection, CurrView=@CurrView, "
                + " PermissionId=@PermissionId, [AccessCode]=@AccessCode, AccessLevel=@AccessLevel, "
                + " CssFile=@CssFile, CssClass=@CssClass, UseCache=@UseCache, UseLog=@UseLog, "
                + " DirectEditMode=@DirectEditMode, "
                + " WriteAccessType=@WriteAccessType, WritePermissionId=@WritePermissionId, [WriteAccessCode]=@WriteAccessCode, "
                + " WriteAccessLevel=@WriteAccessLevel, SystemMessagesTo=@SystemMessagesTo "
                + " WHERE Id = @Id";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", theObj.Id));
                //myCmd.Parameters.Add(Database.Parameter(myProv, "Title", theObj.Title));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Content", theObj.Content));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Ordering", theObj.Ordering));
                myCmd.Parameters.Add(Database.Parameter(myProv, "TemplateBlockName", theObj.TemplateBlockName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Published", theObj.Published));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ModuleName", theObj.ModuleName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ModuleNamespace", theObj.ModuleNamespace));
                //myCmd.Parameters.Add(Database.Parameter(myProv, "DateInserted", theObj.DateInserted));
                //myCmd.Parameters.Add(Database.Parameter(myProv, "UserInserted", theObj.UserInserted));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DateUpdated", theObj.DateUpdated));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UserUpdated", theObj.UserUpdated));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ShowTitle", theObj.ShowTitle));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ModuleParams", theObj.ModuleParams));
                myCmd.Parameters.Add(Database.Parameter(myProv, "IsCore", theObj.IsCore));
                myCmd.Parameters.Add(Database.Parameter(myProv, "MenuSelection", (int)theObj.MenuSelection));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CurrView", theObj.CurrView));
                //read permissions
                myCmd.Parameters.Add(Database.Parameter(myProv, "AccessType", (int)theObj.ReadAccessType));
                myCmd.Parameters.Add(Database.Parameter(myProv, "PermissionId", theObj.ReadPermissionId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "AccessCode", theObj.ReadAccessCode));
                myCmd.Parameters.Add(Database.Parameter(myProv, "AccessLevel", theObj.ReadAccessLevel));
                //write permissions
                myCmd.Parameters.Add(Database.Parameter(myProv, "WriteAccessType", theObj.WriteAccessType));
                myCmd.Parameters.Add(Database.Parameter(myProv, "WritePermissionId", theObj.WritePermissionId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "WriteAccessCode", theObj.WriteAccessCode));
                myCmd.Parameters.Add(Database.Parameter(myProv, "WriteAccessLevel", theObj.WriteAccessLevel));
                
                myCmd.Parameters.Add(Database.Parameter(myProv, "CssFile", theObj.CssFile));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CssClass", theObj.CssClass));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UseCache", (int)theObj.UseCache));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UseLog", (int)theObj.UseLog));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DirectEditMode", theObj.DirectEditMode));
                myCmd.Parameters.Add(Database.Parameter(myProv, "SystemMessagesTo", theObj.SystemMessagesTo));
                
                result = myCmd.ExecuteNonQuery();
                updateCultureText(theObj, myCmd, myProv);
                updateModulesMenu(theObj, myCmd, myProv);
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

        public override Module Insert(Module newObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbTransaction myTrans = null;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            Module result = new Module();

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
                result.Id = this.GetNextId();
                result.Ordering = this.GetNextOrdering();
                result.DateInserted = DateTime.Now;
                result.UserInserted = PgnUserCurrent.UserName;
                result.DateUpdated = DateTime.Now;
                result.UserUpdated = PgnUserCurrent.UserName;

                sSql = "INSERT INTO [" + this.TableName + "] "
                    + " (Id, Content, Ordering, TemplateBlockName, Published, "
                    + " ModuleName, ModuleNamespace, DateInserted, UserInserted, "
                    + " DateUpdated, UserUpdated, AccessType, ShowTitle, ModuleParams, IsCore, "
                    + " MenuSelection, CurrView, PermissionId, AccessCode, AccessLevel, "
                    + " CssFile, CssClass, UseCache, UseLog, DirectEditMode, "
                    + " WriteAccessType, WritePermissionId, WriteAccessCode, WriteAccessLevel, SystemMessagesTo) "
                    + " VALUES(@Id, @Content, @Ordering, @TemplateBlockName, @Published, "
                    + " @ModuleName, @ModuleNamespace, @DateInserted, @UserInserted, "
                    + " @DateUpdated, @UserUpdated, @AccessType, @ShowTitle, @ModuleParams, @IsCore, "
                    + " @MenuSelection, @CurrView, @PermissionId, @AccessCode, @AccessLevel, "
                    + " @CssFile, @CssClass, @UseCache, @UseLog, @DirectEditMode, "
                    + " @WriteAccessType, @WritePermissionId, @WriteAccessCode, @WriteAccessLevel, @SystemMessagesTo)";
                myCmd.CommandText = Database.ParseSql(sSql);

                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", result.Id));
                //##20100412 not saved
                //myCmd.Parameters.Add(Database.Parameter(myProv, "Title", result.Title));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Content", result.Content));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Ordering", result.Ordering));
                myCmd.Parameters.Add(Database.Parameter(myProv, "TemplateBlockName", result.TemplateBlockName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Published", result.Published));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ModuleName", result.ModuleName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ModuleNamespace", result.ModuleNamespace));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DateInserted", result.DateInserted));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UserInserted", result.UserInserted));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DateUpdated", result.DateUpdated));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UserUpdated", result.UserUpdated));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ShowTitle", result.ShowTitle));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ModuleParams", result.ModuleParams));
                myCmd.Parameters.Add(Database.Parameter(myProv, "IsCore", result.IsCore));
                myCmd.Parameters.Add(Database.Parameter(myProv, "MenuSelection", (int)result.MenuSelection));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CurrView", result.CurrView));
                //read permissions
                myCmd.Parameters.Add(Database.Parameter(myProv, "AccessType", (int)result.ReadAccessType));
                myCmd.Parameters.Add(Database.Parameter(myProv, "PermissionId", result.ReadPermissionId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "AccessCode", result.ReadAccessCode));
                myCmd.Parameters.Add(Database.Parameter(myProv, "AccessLevel", (int)result.ReadAccessLevel));
                //write permissions
                myCmd.Parameters.Add(Database.Parameter(myProv, "WriteAccessType", (int)result.WriteAccessType));
                myCmd.Parameters.Add(Database.Parameter(myProv, "WritePermissionId", result.WritePermissionId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "WriteAccessCode", (string)result.WriteAccessCode));
                myCmd.Parameters.Add(Database.Parameter(myProv, "WriteAccessLevel", (int)result.WriteAccessLevel));
                
                myCmd.Parameters.Add(Database.Parameter(myProv, "CssFile", result.CssFile));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CssClass", result.CssClass));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UseCache", (int)result.UseCache));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UseLog", (int)result.UseLog));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DirectEditMode", result.DirectEditMode));
                myCmd.Parameters.Add(Database.Parameter(myProv, "SystemMessagesTo", result.SystemMessagesTo));

                myCmd.ExecuteNonQuery();
                updateCultureText(result, myCmd, myProv);
                updateModulesMenu(result, myCmd, myProv);
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

        public override int DeleteById(int moduleId)
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

                var currObj = this.GetByKey(moduleId);
                new PermissionProvider().RemovePermissionById(currObj.ReadPermissionId);
                new PermissionProvider().RemovePermissionById(currObj.WritePermissionId);

                sSql = "DELETE FROM [" + this.TableName + "] WHERE Id=@Id ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", moduleId));
                res = myCmd.ExecuteNonQuery();

                sSql = "DELETE FROM #__modulesMenu WHERE ModuleId=@ModuleId ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "ModuleId", moduleId));
                myCmd.ExecuteNonQuery();

                sSql = "DELETE FROM [" + this.TableName + "_Culture] WHERE ModuleId = @ModuleId ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "ModuleId", moduleId));
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

        #region protected methods

        protected override void FillObject(Module result, DbDataReader myRd)
        {
            if (!Convert.IsDBNull(myRd["Id"]))
                result.Id = (int)myRd["Id"];
            if (!Convert.IsDBNull(myRd["Title"]))
                result.TitleOld = (string)myRd["Title"];
            if (!Convert.IsDBNull(myRd["Content"]))
                result.Content = (string)myRd["Content"];
            if (!Convert.IsDBNull(myRd["Ordering"]))
                result.Ordering = (int)myRd["Ordering"];
            if (!Convert.IsDBNull(myRd["TemplateBlockName"]))
                result.TemplateBlockName = (string)myRd["TemplateBlockName"];
            if (!Convert.IsDBNull(myRd["Published"]))
                result.Published = (bool)myRd["Published"];
            if (!Convert.IsDBNull(myRd["ModuleName"]))
                result.ModuleName = (string)myRd["ModuleName"];
            if (!Convert.IsDBNull(myRd["ModuleNamespace"]))
                result.ModuleNamespace = (string)myRd["ModuleNamespace"];
            if (!Convert.IsDBNull(myRd["DateInserted"]))
                result.DateInserted = (DateTime)myRd["DateInserted"];
            if (!Convert.IsDBNull(myRd["UserInserted"]))
                result.UserInserted = (string)myRd["UserInserted"];
            if (!Convert.IsDBNull(myRd["DateUpdated"]))
                result.DateUpdated = (DateTime)myRd["DateUpdated"];
            if (!Convert.IsDBNull(myRd["UserUpdated"]))
                result.UserUpdated = (string)myRd["UserUpdated"];
            if (!Convert.IsDBNull(myRd["ShowTitle"]))
                result.ShowTitle = (bool)myRd["ShowTitle"];
            if (!Convert.IsDBNull(myRd["ModuleParams"]))
                result.ModuleParams = (string)myRd["ModuleParams"];
            if (!Convert.IsDBNull(myRd["IsCore"]))
                result.IsCore = (bool)myRd["IsCore"];
            if (!Convert.IsDBNull(myRd["MenuSelection"]))
                result.MenuSelection = (ModulesMenuSelection)int.Parse(myRd["MenuSelection"].ToString());
            if (!Convert.IsDBNull(myRd["CurrView"]))
                result.CurrView = (string)myRd["CurrView"];
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

            if (!Convert.IsDBNull(myRd["CssFile"]))
                result.CssFile = (string)myRd["CssFile"];
            if (!Convert.IsDBNull(myRd["CssClass"]))
                result.CssClass = (string)myRd["CssClass"];
            if (!Convert.IsDBNull(myRd["UseCache"]))
                result.UseCache = (Utility.TristateBool)int.Parse(myRd["UseCache"].ToString());
            if (!Convert.IsDBNull(myRd["UseLog"]))
                result.UseLog = (Utility.TristateBool)int.Parse(myRd["UseLog"].ToString());
            if (!Convert.IsDBNull(myRd["DirectEditMode"]))
                result.DirectEditMode = (bool)myRd["DirectEditMode"];
            if (!Convert.IsDBNull(myRd["SystemMessagesTo"]))
                result.SystemMessagesTo = (string)myRd["SystemMessagesTo"];
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
                var o1 = new PigeonCms.Module();
                o1 = this.GetByKey(currentRecordId);

                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT TOP 1 [" + this.KeyFieldName + "] FROM [" + this.TableName + "] "
                + " WHERE Ordering < @Ordering  "
                + " AND TemplateBlockName = @TemplateBlockName "
                + " ORDER BY ordering DESC ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Ordering", ordering));
                myCmd.Parameters.Add(Database.Parameter(myProv, "TemplateBlockName", o1.TemplateBlockName));
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
                    sSql = "SELECT TOP 1 [" + KeyFieldName + "] FROM " + TableName
                        + " WHERE [" + KeyFieldName + "] < @currentRecordId ORDER BY ordering ASC ";
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
                var o1 = new PigeonCms.Module();
                o1 = this.GetByKey(currentRecordId);

                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT TOP 1 [" + this.KeyFieldName + "] FROM [" + this.TableName + "] "
                + " WHERE ordering > @ordering "
                + " AND TemplateBlockName = @TemplateBlockName "
                + " ORDER BY ordering ASC ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Ordering", ordering));
                myCmd.Parameters.Add(Database.Parameter(myProv, "TemplateBlockName", o1.TemplateBlockName));
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
                    sSql = "SELECT TOP 1 [" + KeyFieldName + "] FROM " + TableName
                        + " WHERE [" + KeyFieldName + "] > @currentRecordId ORDER BY ordering, [" + KeyFieldName + "] ";
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

        #endregion


        #region private methods

        //explain in which menu entries the module will appear
        private static void updateModulesMenu(Module theObj, DbCommand myCmd, DbProviderFactory myProv)
        {
            string sSql = "DELETE FROM #__modulesMenu WHERE ModuleId=@ModuleId ";
            myCmd.CommandText = Database.ParseSql(sSql);
            myCmd.Parameters.Clear();
            myCmd.Parameters.Add(Database.Parameter(myProv, "ModuleId", theObj.Id));
            myCmd.ExecuteNonQuery();

            sSql = "DELETE FROM #__modulesMenuTypes WHERE ModuleId=@ModuleId ";
            myCmd.CommandText = Database.ParseSql(sSql);
            myCmd.Parameters.Clear();
            myCmd.Parameters.Add(Database.Parameter(myProv, "ModuleId", theObj.Id));
            myCmd.ExecuteNonQuery();

            switch (theObj.MenuSelection)
            {
                case ModulesMenuSelection.List:
                    foreach (int menuId in theObj.ModulesMenu)
                    {
                        sSql = "INSERT INTO #__modulesMenu(ModuleId, MenuId) "
                            + "VALUES(@ModuleId, @MenuId) ";
                        myCmd.CommandText = Database.ParseSql(sSql);
                        myCmd.Parameters.Clear();
                        myCmd.Parameters.Add(Database.Parameter(myProv, "ModuleId", theObj.Id));
                        myCmd.Parameters.Add(Database.Parameter(myProv, "MenuId", menuId));
                        myCmd.ExecuteNonQuery();
                    }

                    foreach (string menuType in theObj.ModulesMenuTypes)
                    {
                        sSql = "INSERT INTO #__modulesMenuTypes(ModuleId, MenuType) "
                            + "VALUES(@ModuleId, @MenuType) ";
                        myCmd.CommandText = Database.ParseSql(sSql);
                        myCmd.Parameters.Clear();
                        myCmd.Parameters.Add(Database.Parameter(myProv, "ModuleId", theObj.Id));
                        myCmd.Parameters.Add(Database.Parameter(myProv, "MenuType", menuType));
                        myCmd.ExecuteNonQuery();
                    }
                    break;
            }
        }

        /// <summary>
        /// load modulesMenu list current module
        /// </summary>
        private void getModulesMenu(Module result, DbDataReader myRd,
            DbCommand myCmd, DbProviderFactory myProv)
        {
            string sSql = "";
            switch (result.MenuSelection)
            {
                case ModulesMenuSelection.AllPages:
                    //retrieve all menu voices
                    sSql = "SELECT Id FROM #__menu m ";
                    myCmd.CommandText = Database.ParseSql(sSql);
                    myRd = myCmd.ExecuteReader();
                    while (myRd.Read())
                    {
                        if (!Convert.IsDBNull(myRd["Id"]))
                            result.ModulesMenu.Add((int)myRd["Id"]);
                    }
                    myRd.Close();
                    break;

                case ModulesMenuSelection.NoPages:
                    break;

                case ModulesMenuSelection.List:
                    //retrieve detailed modulesMenu list
                    sSql = "SELECT ModuleId, MenuId FROM #__modulesMenu m "
                        + " WHERE ModuleId = @ModuleId ";
                    myCmd.CommandText = Database.ParseSql(sSql);
                    myCmd.Parameters.Clear();
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ModuleId", result.Id));
                    myRd = myCmd.ExecuteReader();
                    while (myRd.Read())
                    {
                        if (!Convert.IsDBNull(myRd["MenuId"]))
                            result.ModulesMenu.Add((int)myRd["MenuId"]);
                    }
                    myRd.Close();

                    sSql = "SELECT ModuleId, MenuType FROM #__modulesMenuTypes m "
                    + " WHERE ModuleId = @ModuleId ";
                    myCmd.CommandText = Database.ParseSql(sSql);
                    myCmd.Parameters.Clear();
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ModuleId", result.Id));
                    myRd = myCmd.ExecuteReader();
                    while (myRd.Read())
                    {
                        if (!Convert.IsDBNull(myRd["MenuType"]))
                            result.ModulesMenuTypes.Add((string)myRd["MenuType"]);
                    }
                    myRd.Close();
                    break;

                case ModulesMenuSelection.MenuContent:
                    break;

                default:
                    break;
            }

            if (result.ReadPermissionId > 0 && result.ReadAccessType != MenuAccesstype.Public)
            {
                result.ReadRolenames = new PermissionProvider().GetPermissionRoles(result.ReadPermissionId);
                result.WriteRolenames = new PermissionProvider().GetPermissionRoles(result.WritePermissionId);
            }
        }

        private void getCultureSpecific(Module result, DbDataReader myRd,
            DbCommand myCmd, DbProviderFactory myProv)
        {
            string sSql;
            //culture specific
            sSql = "SELECT CultureName, ModuleId, Title "
                + " FROM [" + this.TableName + "_culture] t "
                + " WHERE ModuleId = @ModuleId ";
            myCmd.CommandText = Database.ParseSql(sSql);
            myCmd.Parameters.Clear();
            myCmd.Parameters.Add(Database.Parameter(myProv, "ModuleId", result.Id));
            myRd = myCmd.ExecuteReader();
            while (myRd.Read())
            {
                if (!Convert.IsDBNull(myRd["Title"]))
                    result.TitleTranslations.Add((string)myRd["cultureName"], (string)myRd["Title"]);
            }
            myRd.Close();
        }

        private void updateCultureText(Module theObj, DbCommand myCmd, DbProviderFactory myProv)
        {
            foreach (KeyValuePair<string, string> item in theObj.TitleTranslations)
            {
                string sSql = "";

                //delete previous entry (if exists)
                sSql = "DELETE FROM [" + this.TableName + "_culture] WHERE CultureName=@CultureName AND ModuleId=@ModuleId ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "CultureName", item.Key));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ModuleId", theObj.Id));
                myCmd.ExecuteNonQuery();

                //insert current culture entry
                sSql = "INSERT INTO [" + this.TableName + "_culture](CultureName, ModuleId, Title) "
                + " VALUES(@CultureName, @ModuleId, @Title) ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "CultureName", item.Key));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ModuleId", theObj.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Title", item.Value));
                myCmd.ExecuteNonQuery();
            }
        }

        #endregion
    }
}