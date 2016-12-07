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

namespace PigeonCms
{

    /// <summary>
    /// DAL for Menu obj (table menu)
    /// </summary>
    public class MenuManager : TableManagerWithOrdering<Menu, MenuFilter, int>, ITableManagerWithPermission
    {
        private bool checkUserContext = false;
        private bool writeMode = false;
        private SeoProvider seoProvider;


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
        public MenuManager(): this(false, false)
        { }

        [DebuggerStepThrough()]
        public MenuManager(bool checkUserContext, bool writeMode)
        {
            this.TableName = "#__menu";
            this.KeyFieldName = "Id";
            this.checkUserContext = checkUserContext;
            this.writeMode = writeMode;
            if (this.writeMode) this.checkUserContext = true;    //forced

            seoProvider = new SeoProvider("menus");
        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Dictionary<string, string> GetList(string moduleFullName, string menuType)
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            MenuFilter filter = new MenuFilter();
            filter.ModuleFullName = moduleFullName;
            filter.MenuType = menuType;
            List<Menu> list = GetByFilter(filter, "");
            foreach (Menu item in list)
            {
                res.Add(item.Id.ToString(), item.Name + ": " + item.RoutePattern);
            }
            return res;
        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Dictionary<string, string> GetList(string moduleFullName)
        {
            return GetList(moduleFullName, "");
        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public override Dictionary<string, string> GetList()
        {
            return GetList("", "");
        }


        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public override List<Menu> GetByFilter(MenuFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            List<Menu> result = new List<Menu>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT t.Id, t.MenuType, t.Name, t.Alias, t.Link, "
                + " t.ContentType, t.Published, t.ParentId, "
                + " t.ModuleId, t.Ordering, t.AccessType, t.OverridePageTitle,t.ReferMenuId, "
                + " t.CurrMasterPage, t.CurrTheme, t.CssClass, t.Visible, "
                + " t.RouteId, t.PermissionId, t.AccessCode, t.AccessLevel, t.IsCore, "
                + " t.WriteAccessType, t.WritePermissionId, t.WriteAccessCode, t.WriteAccessLevel, "
                + " t.UseSsl, t.SeoId, "
                + " r.Name RouteName, r.Pattern RoutePattern, "
                + " r.CurrMasterPage RouteMasterPage, r.CurrTheme RouteTheme, r.UseSsl RouteUseSsl "
                + " FROM ["+ this.TableName +"] t "
                + " LEFT JOIN #__modules m ON t.moduleId = m.Id "
                + " LEFT JOIN #__routes r ON t.routeId = r.Id "
                + " WHERE t."+ this.KeyFieldName +" > 0 ";
                if (filter.Id > 0 || filter.Id == -1)
                {
                    sSql += " AND t.Id = @Id ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Id", filter.Id));
                }
                if (!string.IsNullOrEmpty(filter.MenuType))
                {
                    sSql += " AND t.MenuType = @MenuType ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "MenuType", filter.MenuType));
                }
                if (!string.IsNullOrEmpty(filter.Name))
                {
                    sSql += " AND t.Name = @Name ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Name", filter.Name));
                }
                if (!string.IsNullOrEmpty(filter.Alias))
                {
                    sSql += " AND t.Alias = @Alias ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Alias", filter.Alias));
                }
                if (filter.FilterContentType == true)
                {
                    sSql += " AND t.ContentType = @ContentType ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ContentType", (int)filter.ContentType));
                }
                if (filter.Published != Utility.TristateBool.NotSet)
                {
                    sSql += " AND t.Published = @Published ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Published", filter.Published));
                }
                if (filter.ParentId != -1)
                {
                    sSql += " AND t.ParentId = @ParentId ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ParentId", filter.ParentId));
                }
                if (filter.ModuleId > 0)
                {
                    sSql += " AND t.ModuleId = @ModuleId ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ModuleId", filter.ModuleId));
                }
                if (!string.IsNullOrEmpty(filter.ModuleFullName))
                {
                    string moduleName = "";
                    string moduleNamespace = "";
                    try
                    {
                        moduleNamespace = filter.ModuleFullName.Split('.')[0];
                        moduleName = filter.ModuleFullName.Split('.')[1];
                    }
                    finally{}
                    sSql += " AND m.ModuleName = @ModuleName ";
                    sSql += " AND m.ModuleNamespace = @ModuleNamespace ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ModuleName", moduleName));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ModuleNamespace", moduleNamespace));
                }

                if (filter.ReferMenuId > 0)
                {
                    sSql += " AND t.ReferMenuId = @ReferMenuId ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ReferMenuId", filter.ReferMenuId));
                }
                if (!string.IsNullOrEmpty(filter.CurrMasterPage))
                {
                    sSql += " AND t.CurrMasterPage = @CurrMasterPage ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CurrMasterPage", filter.CurrMasterPage));
                }
                if (!string.IsNullOrEmpty(filter.CurrTheme))
                {
                    sSql += " AND t.CurrTheme = @CurrTheme ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CurrTheme", filter.CurrTheme));
                }
                if (filter.Visible != Utility.TristateBool.NotSet)
                {
                    sSql += " AND t.Visible = @Visible ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Visible", filter.Visible));
                }
                if (filter.RouteId > 0)
                {
                    sSql += " AND t.RouteId = @RouteId ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "RouteId", filter.RouteId));
                }
                if (filter.IsCore != Utility.TristateBool.NotSet)
                {
                    sSql += " AND t.IsCore = @IsCore ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "IsCore", filter.IsCore));
                }
                if (!string.IsNullOrEmpty(filter.RoutePattern))
                {
                    sSql += " AND r.Pattern = @Pattern ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Pattern", filter.RoutePattern));
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    sSql += " ORDER BY t." + sort;
                }
                else
                {
                    sSql += " ORDER BY t.Ordering ";
                }

                myCmd.CommandText = Database.ParseSql(sSql);
                myRd = myCmd.ExecuteReader();
                while (myRd.Read())
                {
                    var item = new Menu();
                    FillObject(item, myRd);
                    result.Add(item);
                }
                myRd.Close();

                //(other loop to avoid multiple reader on same command)
                foreach (Menu item in result)
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

                //load culture specific 
                foreach (Menu item in result)
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


        public List<int> GetParentIdList(int menuId)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            List<int> parentList = new List<int>();

            try
            {
                if (menuId > 0)
                {
                    myConn.ConnectionString = Database.ConnString;
                    myConn.Open();

                    parentList.Add(menuId);
                    getParentId(menuId, ref parentList, myProv, myConn);
                }
            }
            finally
            {
                myConn.Dispose();
            }
            return parentList;
        }


        /// <summary>
        /// gets menuId level
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public int GetMenuLevel(int menuId)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            int level = 0;

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                getMenuLevel(menuId, ref level, myProv, myConn);
            }
            finally
            {
                myConn.Dispose();
            }

            return level;
        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public override Menu GetByKey(int id)
        {
            var result = new Menu();
            var list = new List<Menu>();
            var filter = new MenuFilter();
            filter.Id = id == 0 ? -1 : id;  //20121004
            list = GetByFilter(filter, "");
            if (list.Count > 0)
                result = list[0];
            return result;
        }


        public List<Menu> GetTree(MenuFilter filter, int level, string separatorText = ". . ")
        {
            List<PigeonCms.Menu> result = new List<Menu>();
            loadTree(result, filter, level, separatorText);
            return result;
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public override int DeleteById(int id)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbTransaction myTrans = null;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myProv.CreateCommand();
            string sSql;
            int res = 0;

            if (this.hasChilds(id))
            {
                throw new ArgumentException("current obj has childs");
            }

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;
                myTrans = myConn.BeginTransaction();
                myCmd.Transaction = myTrans;

                Menu currObj = GetByKey(id);
                new PermissionProvider().RemovePermissionById(currObj.ReadPermissionId);
                new PermissionProvider().RemovePermissionById(currObj.WritePermissionId);
                seoProvider.Remove(currObj);


                sSql = "DELETE FROM [" + this.TableName + "] WHERE Id = @Id ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", id));
                res = myCmd.ExecuteNonQuery();

                sSql = "DELETE FROM [" + this.TableName + "_Culture] WHERE MenuId = @MenuId ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "MenuId", id));
                myCmd.ExecuteNonQuery();

                if (currObj.ContentType == MenuContentType.Module)
                {
                    new ModulesManager().DeleteById(currObj.ModuleId);//delete associated module
                }
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

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public override int Update(Menu theObj)
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
                seoProvider.Save(theObj);

                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;
                myTrans = myConn.BeginTransaction();
                myCmd.Transaction = myTrans;

                sSql = "UPDATE ["+ this.TableName +"] "
                + " SET MenuType=@MenuType, Name=@Name, Alias=@Alias, Link=@Link, "
                + " ContentType=@ContentType, Published=@Published, ParentId=@ParentId, "
                + " ModuleId=@ModuleId, Ordering=@Ordering, AccessType=@AccessType, "
                + " OverridePageTitle=@OverridePageTitle, ReferMenuId=@ReferMenuId, "
                + " CurrMasterPage=@CurrMasterPage, CurrTheme=@CurrTheme, CssClass=@CssClass, "
                + " Visible=@Visible, RouteId=@RouteId, PermissionId=@PermissionId, "
                + " [AccessCode]=@AccessCode, AccessLevel=@AccessLevel, IsCore=@IsCore, "
                + " WriteAccessType=@WriteAccessType, WritePermissionId=@WritePermissionId, [WriteAccessCode]=@WriteAccessCode, "
                + " WriteAccessLevel=@WriteAccessLevel, UseSsl=@UseSsl, SeoId=@SeoId "
                + " WHERE Id = @Id";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", theObj.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "MenuType", theObj.MenuType));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Name", theObj.Name));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Alias", theObj.Alias));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Link", theObj.Link));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ContentType", theObj.ContentType));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Published", theObj.Published));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ParentId", theObj.ParentId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ModuleId", theObj.ModuleId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Ordering", theObj.Ordering));
                myCmd.Parameters.Add(Database.Parameter(myProv, "OverridePageTitle", theObj.OverridePageTitle));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ReferMenuId", theObj.ReferMenuId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CurrMasterPage", theObj.CurrMasterPageStored));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CurrTheme", theObj.CurrThemeStored));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CssClass", theObj.CssClass));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Visible", theObj.Visible));
                myCmd.Parameters.Add(Database.Parameter(myProv, "RouteId", theObj.RouteId));
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

                myCmd.Parameters.Add(Database.Parameter(myProv, "IsCore", theObj.IsCore));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UseSsl", (int)theObj.UseSsl));
                myCmd.Parameters.Add(Database.Parameter(myProv, "SeoId", theObj.SeoId));


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


        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public override Menu Insert(Menu newObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbTransaction myTrans = null;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;

            try
            {
                //create read/write permission
                new PermissionProvider().CreatePermissionObj(newObj);

                seoProvider.Save(newObj);

                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;
                myTrans = myConn.BeginTransaction();
                myCmd.Transaction = myTrans;

                newObj.Id = this.GetNextId();
                newObj.Ordering = this.GetNextOrdering();

                sSql = "INSERT INTO ["+ this.TableName +"] "
                    + " (Id, MenuType, Name, Alias, Link, ContentType, Published, ParentId, "
                    + " ModuleId, Ordering, AccessType, OverridePageTitle, ReferMenuId, "
                    + " CurrMasterPage, CurrTheme, CssClass, Visible, RouteId, "
                    + " PermissionId, t.AccessCode, t.AccessLevel, IsCore, "
                    + " WriteAccessType, WritePermissionId, WriteAccessCode, WriteAccessLevel, "
                    + " UseSsl, SeoId) "
                    + " VALUES(@Id, @MenuType, @Name, @Alias, @Link, @ContentType, @Published, @ParentId, "
                    + " @ModuleId, @Ordering, @AccessType, @OverridePageTitle, @ReferMenuId, "
                    + " @CurrMasterPage, @CurrTheme, @CssClass, @Visible, @RouteId, "
                    + " @PermissionId, @AccessCode, @AccessLevel, @IsCore, "
                    + " @WriteAccessType, @WritePermissionId, @WriteAccessCode, @WriteAccessLevel, "
                    + " @UseSsl, @SeoId)";

                myCmd.CommandText = Database.ParseSql(sSql);

                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", newObj.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "MenuType", newObj.MenuType));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Name", newObj.Name));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Alias", newObj.Alias));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Link", newObj.Link));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ContentType", (int)newObj.ContentType));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Published", newObj.Published));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ParentId", newObj.ParentId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ModuleId", newObj.ModuleId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Ordering", newObj.Ordering));
                myCmd.Parameters.Add(Database.Parameter(myProv, "OverridePageTitle", newObj.OverridePageTitle));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ReferMenuId", newObj.ReferMenuId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CurrMasterPage", newObj.CurrMasterPageStored));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CurrTheme", newObj.CurrThemeStored));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CssClass", newObj.CssClass));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Visible", newObj.Visible));
                myCmd.Parameters.Add(Database.Parameter(myProv, "RouteId", newObj.RouteId));
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

                myCmd.Parameters.Add(Database.Parameter(myProv, "IsCore", newObj.IsCore));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UseSsl", (int)newObj.UseSsl));
                myCmd.Parameters.Add(Database.Parameter(myProv, "SeoId", newObj.SeoId));

                myCmd.ExecuteNonQuery();
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


        #region protected methods

        protected override void FillObject(PigeonCms.Menu result, DbDataReader myRd)
        {
            if (!Convert.IsDBNull(myRd["Id"]))
                result.Id = (int)myRd["Id"];
            if (!Convert.IsDBNull(myRd["MenuType"]))
                result.MenuType = (string)myRd["MenuType"];
            if (!Convert.IsDBNull(myRd["Name"]))
                result.Name = (string)myRd["Name"];
            if (!Convert.IsDBNull(myRd["Alias"]))
                result.Alias = (string)myRd["Alias"];
            if (!Convert.IsDBNull(myRd["Link"]))
                result.Link = (string)myRd["Link"];
            if (!Convert.IsDBNull(myRd["ContentType"]))
                result.ContentType = (MenuContentType)int.Parse(myRd["ContentType"].ToString());
            if (!Convert.IsDBNull(myRd["Published"]))
                result.Published = (bool)myRd["Published"];
            if (!Convert.IsDBNull(myRd["ParentId"]))
                result.ParentId = (int)myRd["ParentId"];
            if (!Convert.IsDBNull(myRd["ModuleId"]))
                result.ModuleId = (int)myRd["ModuleId"];
            if (!Convert.IsDBNull(myRd["Ordering"]))
                result.Ordering = (int)myRd["Ordering"];
            if (!Convert.IsDBNull(myRd["OverridePageTitle"]))
                result.OverridePageTitle = (bool)myRd["OverridePageTitle"];
            if (!Convert.IsDBNull(myRd["ReferMenuId"]))
                result.ReferMenuId = (int)myRd["ReferMenuId"];
            if (!Convert.IsDBNull(myRd["CssClass"]))
                result.CssClass = (string)myRd["CssClass"];
            if (!Convert.IsDBNull(myRd["Visible"]))
                result.Visible = (bool)myRd["Visible"];
            if (!Convert.IsDBNull(myRd["RouteId"]))
                result.RouteId = (int)myRd["RouteId"];
            if (!Convert.IsDBNull(myRd["RouteName"]))
                result.RouteName = (string)myRd["RouteName"];
            if (!Convert.IsDBNull(myRd["RoutePattern"]))
                result.RoutePattern = (string)myRd["RoutePattern"];
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
            
            if (!Convert.IsDBNull(myRd["IsCore"]))
                result.IsCore = (bool)myRd["IsCore"];
            if (!Convert.IsDBNull(myRd["SeoId"]))
                result.SeoId = (int)myRd["SeoId"];

            //menu entry setting
            if (!Convert.IsDBNull(myRd["UseSsl"]))
                result.UseSsl = (Utility.TristateBool)int.Parse(myRd["UseSsl"].ToString());
            //route setting
            if (!Convert.IsDBNull(myRd["RouteUseSsl"]))
                result.RouteUseSsl = (bool)myRd["RouteUseSsl"];

            //routes hierarchy
            if (!Convert.IsDBNull(myRd["CurrMasterPage"]))
                result.CurrMasterPageStored = (string)myRd["CurrMasterPage"];
            if (!Convert.IsDBNull(myRd["CurrTheme"]))
                result.CurrThemeStored = (string)myRd["CurrTheme"];
            if (!Convert.IsDBNull(myRd["RouteMasterPage"]))
                result.RouteMasterPage = (string)myRd["RouteMasterPage"];
            if (!Convert.IsDBNull(myRd["RouteTheme"]))
                result.RouteTheme = (string)myRd["RouteTheme"];
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
                var o1 = new PigeonCms.Menu();
                o1 = this.GetByKey(currentRecordId);

                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT TOP 1 [" + this.KeyFieldName + "] FROM [" + this.TableName + "] "
                + " WHERE Ordering < @Ordering  "
                + " AND MenuType = @MenuType "
                + " AND ParentId = @ParentId "
                + " ORDER BY ordering DESC ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Ordering", ordering));
                myCmd.Parameters.Add(Database.Parameter(myProv, "MenuType", o1.MenuType));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ParentId", o1.ParentId));
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
                var o1 = new PigeonCms.Menu();
                o1 = this.GetByKey(currentRecordId);

                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT TOP 1 [" + this.KeyFieldName + "] FROM [" + this.TableName + "] "
                + " WHERE ordering > @ordering "
                + " AND MenuType = @MenuType "
                + " AND ParentId = @ParentId "
                + " ORDER BY ordering ASC ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Ordering", ordering));
                myCmd.Parameters.Add(Database.Parameter(myProv, "MenuType", o1.MenuType));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ParentId", o1.ParentId));
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

        private void updateCultureText(Menu theObj, DbCommand myCmd, DbProviderFactory myProv)
        {
            foreach (KeyValuePair<string, string> item in theObj.TitleTranslations)
            {
                string sSql = "";
                string titleWindowValue = "";
                theObj.TitleWindowTranslations.TryGetValue(item.Key, out titleWindowValue);
                if (string.IsNullOrEmpty(titleWindowValue))
                    titleWindowValue = "";

                //delete previous entry (if exists)
                sSql = "DELETE FROM [" + this.TableName + "_culture] WHERE CultureName=@CultureName AND MenuId=@MenuId ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "CultureName", item.Key));
                myCmd.Parameters.Add(Database.Parameter(myProv, "MenuId", theObj.Id));
                myCmd.ExecuteNonQuery();

                //insert current culture entry
                sSql = "INSERT INTO [" + this.TableName + "_culture](CultureName, MenuId, Title, TitleWindow) "
                + " VALUES(@CultureName, @MenuId, @Title, @WindowTitle) ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "CultureName", item.Key));
                myCmd.Parameters.Add(Database.Parameter(myProv, "MenuId", theObj.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Title", item.Value));
                myCmd.Parameters.Add(Database.Parameter(myProv, "WindowTitle", titleWindowValue));
                myCmd.ExecuteNonQuery();
            }
        }

        private bool hasChilds(int menuId)
        {
            bool res = false;
            var filter = new MenuFilter();
            filter.ParentId = menuId;
            if (this.GetByFilter(filter, "").Count > 0)
                res = true;
            return res;
        }

        private void getParentId(int menuId, ref List<int> parentList,
            DbProviderFactory myProv, DbConnection myConn)
        {
            DbCommand myCmd = myConn.CreateCommand();
            DbDataReader myRd = null;
            myCmd.Connection = myConn;
            string sSql;
            int parentId = 0;

            sSql = "SELECT t.Id, t.ParentId "
            + " FROM [" + this.TableName + "] t "
            + " WHERE t." + this.KeyFieldName + " > 0 "
            + " AND t.Id = @Id ";
            myCmd.Parameters.Add(Database.Parameter(myProv, "Id", menuId));
            myCmd.CommandText = Database.ParseSql(sSql);
            myRd = myCmd.ExecuteReader();
            if (myRd.Read())
            {
                if (!Convert.IsDBNull(myRd["ParentId"]))
                {
                    parentId = (int)myRd["ParentId"];
                    parentList.Add(parentId);
                }
            }
            myRd.Close();

            if (parentId > 0)
            {
                getParentId(parentId, ref parentList, myProv, myConn);
            }
        }

        private void getMenuLevel(int menuId, ref int level,
            DbProviderFactory myProv, DbConnection myConn)
        {
            DbCommand myCmd = myConn.CreateCommand();
            DbDataReader myRd = null;
            myCmd.Connection = myConn;
            string sSql;

            sSql = "SELECT t.Id, t.ParentId "
            + " FROM [" + this.TableName + "] t "
            + " WHERE t." + this.KeyFieldName + " > 0 "
            + " AND t.Id = @Id ";
            myCmd.Parameters.Add(Database.Parameter(myProv, "Id", menuId));
            myCmd.CommandText = Database.ParseSql(sSql);
            myRd = myCmd.ExecuteReader();
            if (myRd.Read())
            {
                if (!Convert.IsDBNull(myRd["ParentId"]))
                {
                    menuId = (int)myRd["ParentId"];
                }
            }
            myRd.Close();

            if (menuId > 0)
            {
                level++;
                getMenuLevel(menuId, ref level, myProv, myConn);
            }
        }

        private void getCultureSpecific(Menu result, DbDataReader myRd,
            DbCommand myCmd, DbProviderFactory myProv)
        {
            string sSql;
            //culture specific
            sSql = "SELECT CultureName, MenuId, Title, TitleWindow "
                + " FROM [" + this.TableName + "_culture] t "
                + " WHERE MenuId = @MenuId ";
            myCmd.CommandText = Database.ParseSql(sSql);
            myCmd.Parameters.Clear();
            myCmd.Parameters.Add(Database.Parameter(myProv, "MenuId", result.Id));
            myRd = myCmd.ExecuteReader();
            while (myRd.Read())
            {
                if (!Convert.IsDBNull(myRd["Title"]))
                    result.TitleTranslations.Add((string)myRd["cultureName"], (string)myRd["Title"]);
                if (!Convert.IsDBNull(myRd["TitleWindow"]))
                    result.TitleWindowTranslations.Add((string)myRd["cultureName"], (string)myRd["TitleWindow"]);
            }
            myRd.Close();
        }

        private void loadTree(List<Menu> result, MenuFilter menuFilter, int level, string separatorText)
        {
            const int MaxLevel = 10;
            level++;
            if (level < MaxLevel)
            {
                List<PigeonCms.Menu> recordList =
                    GetByFilter(menuFilter, "MenuType, t.ParentId, t.Ordering");
                foreach (PigeonCms.Menu record1 in recordList)
                {
                    string listText = "";
                    for (int i = 0; i < level; i++)
                        listText += separatorText;
                    record1.Name = listText + record1.Name;
                    result.Add(record1);
                    menuFilter.ParentId = record1.Id;
                    loadTree(result, menuFilter, level, separatorText);
                }
            }
        }

        #endregion
    }


    /// <summary>
    /// DAL for MenuType obj (table menuTypes)
    /// </summary>
    public class MenutypesManager : TableManager<Menutype, MenutypeFilter, int>, ITableManager
    {
        [DebuggerStepThrough()]
        public MenutypesManager()
        {
            this.TableName = "#__menuTypes";
            this.KeyFieldName = "Id";
        }

        public override List<Menutype> GetByFilter(MenutypeFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            List<Menutype> result = new List<Menutype>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT Id, MenuType, Title, Description "
                + " FROM [" + this.TableName + "] t "
                + " WHERE t." + this.KeyFieldName + " > 0 ";
                if (filter.Id > 0 || filter.Id == -1)
                {
                    sSql += " AND t.Id = @Id ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Id", filter.Id));
                }
                if (!string.IsNullOrEmpty(filter.MenuType))
                {
                    sSql += " AND t.MenuType = @MenuType ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "MenuType", filter.MenuType));
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    sSql += " ORDER BY " + sort;
                }

                myCmd.CommandText = Database.ParseSql(sSql);
                myRd = myCmd.ExecuteReader();
                while (myRd.Read())
                {
                    var item = new Menutype();
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

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public override Menutype GetByKey(int id)
        {
            var result = new Menutype();
            var list = new List<Menutype>();
            var filter = new MenutypeFilter();
            filter.Id = id;
            list = GetByFilter(filter, "");
            if (list.Count > 0)
                result = list[0];
            return result;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Menutype GetByMenuType(string menuType)
        {
            var result = new Menutype();
            var list = new List<Menutype>();
            var filter = new MenutypeFilter();
            filter.MenuType = menuType;
            list = GetByFilter(filter, "");
            if (list.Count > 0)
                result = list[0];
            return result;
        }

        /// <summary>
        /// dictionary list to use in module admin area (combo)
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, string> GetList()
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            MenutypeFilter filter = new MenutypeFilter();
            List<Menutype> list = GetByFilter(filter, "");
            foreach (Menutype item in list)
            {
                res.Add(item.MenuType, item.MenuType);
            }
            return res;
        }

        public override int Update(Menutype theObj)
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

                sSql = "UPDATE [" + this.TableName + "] "
                    + " SET Title=@Title, Description=@Description "
                    + " WHERE Id = @Id";
                //MenuType=@MenuType, 
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", theObj.Id));
                //myCmd.Parameters.Add(Database.Parameter(myProv, "MenuType", theObj.MenuType));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Title", theObj.Title));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Description", theObj.Description));

                result = myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override Menutype Insert(Menutype newObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbTransaction myTrans = null;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            Menutype result = new Menutype();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                myTrans = myConn.BeginTransaction();
                myCmd.Transaction = myTrans;

                //result.Id = GetNextId();     @@identity(1,1)
                result.MenuType = newObj.MenuType;
                result.Title = newObj.Title;
                result.Description = newObj.Description;

                sSql = "INSERT INTO [" + this.TableName + "] "
                    + " (MenuType, Title, Description) "
                    + " VALUES(@MenuType, @Title, @Description) ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "MenuType", newObj.MenuType));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Title", newObj.Title));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Description", newObj.Description));
                myCmd.ExecuteNonQuery();

                //creazione modulo
                Module m1 = new Module();
                m1.ModuleNamespace = "PigeonCms";
                m1.ModuleName = "TopMenu";
                m1.Published = false;
                m1.TemplateBlockName = "Toolbar";
                m1.ShowTitle = false;
                m1.MenuSelection = ModulesMenuSelection.AllPages;
                m1.ReadAccessType = MenuAccesstype.Public;
                m1.ModuleParams = "MenuType:="+ newObj.MenuType +"|ListClass:=menulist|ItemSelectedClass:=selected|ItemLastClass:=last";
                new ModulesManager().Insert(m1);

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

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public override int DeleteById(int id)
        {
            int res = 0;
            try
            {
                Menutype currObj = GetByKey(id);

                //delete all menu entries
                MenuFilter mnuFilter = new MenuFilter();
                mnuFilter.MenuType = currObj.MenuType;
                List<PigeonCms.Menu> mnuList = new PigeonCms.MenuManager().GetByFilter(mnuFilter, "");
                foreach (PigeonCms.Menu item in mnuList)
                {
                    new MenuManager().DeleteById(item.Id);
                }
                //delete menuType
                res = base.DeleteById(id);
            }
            finally
            {
            }
            return res;
        }

        protected override void FillObject(Menutype result, DbDataReader myRd)
        {
            if (!Convert.IsDBNull(myRd["Id"]))
                result.Id = (int)myRd["Id"];
            if (!Convert.IsDBNull(myRd["MenuType"]))
                result.MenuType = (string)myRd["MenuType"];
            if (!Convert.IsDBNull(myRd["Title"]))
                result.Title = (string)myRd["Title"];
            if (!Convert.IsDBNull(myRd["Description"]))
                result.Description = (string)myRd["Description"];
        }
    }
}