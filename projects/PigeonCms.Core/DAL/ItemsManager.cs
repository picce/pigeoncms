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
    public class ItemsManager<T, F> : 
        TableManagerWithOrdering<T, F, int>,
        ITableManagerWithPermission, ITableManagerExternalId<T>
        where T: Item, new()
        where F: ItemsFilter, new()
    {
        public const string MaxItemsException = "PigeonCms.MaxItemsException";
        public const string ItemAliasInUseException = "PigeonCms.ItemAliasInUseException";

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
        public ItemsManager(): this(false, false)
        { }

        public ItemsManager(bool checkUserContext, bool writeMode)
        {
            this.TableName = "#__items";
            this.KeyFieldName = "Id";
            this.checkUserContext = checkUserContext;
            this.writeMode = writeMode;
            if (this.writeMode) this.checkUserContext = true;    //forced
        }

        public override Dictionary<string, string> GetList()
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            F filter = new F();
            Category cat = new Category();
            List<T> list = GetByFilter(filter, "");
            foreach (T item in list)
            {
                string sectionTitle = "";
                string categoryTitle = "";
                cat = new CategoriesManager().GetByKey(item.CategoryId);
                categoryTitle = cat.Title;
                sectionTitle = new SectionsManager().GetByKey(cat.SectionId).Title;
                res.Add(item.Id.ToString(), sectionTitle + " > " + categoryTitle + " > " + item.Title);
            }
            return res;
        }

        public override List<T> GetByFilter(F filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            List<T> result = new List<T>();
            var sectionsList = new List<Section>();
            var categoriesList = new List<Category>();

            string topItems = "";

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                if (filter.NumOfRecords > 0)
                {
                    topItems = "TOP " + filter.NumOfRecords.ToString();
                }

                sSql = "SELECT " + topItems + " t.Id, t.ItemType, t.CategoryId, categ.SectionId, "
                    + " t.Enabled, t.Alias, t.Ordering, t.DefaultImageName, "
                    + " t.[DateInserted], t.[UserInserted], t.[DateUpdated], t.[UserUpdated], "
                    + " t.ItemDate, t.ValidFrom, t.ValidTo, "
                    + " t.CustomBool1, t.CustomBool2, t.CustomBool3, t.CustomBool4, "
                    + " t.CustomDate1, t.CustomDate2, t.CustomDate3, t.CustomDate4, "
                    + " t.CustomDecimal1, t.CustomDecimal2, t.CustomDecimal3, t.CustomDecimal4, "
                    + " t.CustomInt1, t.CustomInt2, t.CustomInt3, t.CustomInt4, "
                    + " t.CustomString1, t.CustomString2, t.CustomString3, t.CustomString4, "
                    + " t.ItemParams, t.AccessType, t.PermissionId, t.AccessCode, t.AccessLevel, "
                    + " t.CommentsGroupId, t.WriteAccessType, t.WritePermissionId, t.WriteAccessCode, t.WriteAccessLevel, "
                    + " t.ThreadId, t.CssClass, t.ExtId, "
                    + " categ.AccessType categAccessType, categ.PermissionId categPermissionId, "
                    + " categ.AccessCode categAccessCode, categ.AccessLevel categAccessLevel, "
                    + " categ.WriteAccessType categWriteAccessType, categ.WritePermissionId categWritePermissionId, "
                    + " categ.WriteAccessCode categWriteAccessCode, categ.WriteAccessLevel categWriteAccessLevel, "
                    + " sect.AccessType sectAccessType, sect.PermissionId sectPermissionId, "
                    + " sect.AccessCode sectAccessCode, sect.AccessLevel sectAccessLevel, "
                    + " sect.WriteAccessType sectWriteAccessType, sect.WritePermissionId sectWritePermissionId, "
                    + " sect.WriteAccessCode sectWriteAccessCode, sect.WriteAccessLevel sectWriteAccessLevel "
                    + " FROM [" + this.TableName + "] t "
                    + " LEFT JOIN [" + this.TableName + "_Culture] c ON t.Id = c.ItemId "
                    + " LEFT JOIN #__itemFieldValues attr ON t.Id = attr.ItemId "
                    + " LEFT JOIN #__categories categ ON t.CategoryId = categ.Id "
                    + " LEFT JOIN #__sections sect ON categ.SectionId = sect.Id "
                    + " WHERE t.Id > 0 ";
                if (filter.ShowOnlyRootItems)
                {
                    //default
                    sSql += " AND (t.ThreadId = t.Id) ";
                }

                if (filter.Id > 0 || filter.Id == -1)
                {
                    sSql += " AND t.Id = @Id ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Id", filter.Id));
                }
                if (!string.IsNullOrEmpty(filter.ItemType))
                {
                    sSql += " AND (t.ItemType = @ItemType) ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ItemType", filter.ItemType));
                }
                if (filter.CategoryId > 0 || filter.CategoryId == -1)
                {
                    sSql += " AND t.CategoryId = @CategoryId ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CategoryId", filter.CategoryId));
                }
                if (filter.SectionId > 0 || filter.SectionId == -1)
                {
                    sSql += " AND categ.SectionId = @SectionId ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "SectionId", filter.SectionId));
                }
                if (!string.IsNullOrEmpty(filter.TitleSearch))
                {
                    sSql += " AND (c.Title like @TitleSearch) ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "TitleSearch", "%" + filter.TitleSearch + "%"));
                }
                if (!string.IsNullOrEmpty(filter.Alias))
                {
                    sSql += " AND t.Alias = @Alias ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Alias", filter.Alias));
                }
                if (!string.IsNullOrEmpty(filter.DescriptionSearch))
                {
                    sSql += " AND (c.Description like @DescriptionSearch) ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "DescriptionSearch", "%" + filter.DescriptionSearch + "%"));
                }
                if (!string.IsNullOrEmpty(filter.FullSearch))
                {
                    sSql += " AND (c.Title like @TitleSearch OR c.Description like @DescriptionSearch) ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "TitleSearch", "%" + filter.FullSearch + "%"));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "DescriptionSearch", "%" + filter.FullSearch + "%"));
                }
                if (filter.Enabled != Utility.TristateBool.NotSet)
                {
                    sSql += " AND t.Enabled = @Enabled ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Enabled", filter.Enabled));
                }
                if (!string.IsNullOrEmpty(filter.UserInserted))
                {
                    sSql += " AND t.UserInserted = @UserInserted ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "UserInserted", filter.UserInserted));
                }
                if (!string.IsNullOrEmpty(filter.AccessCode))
                {
                    sSql += " AND t.AccessCode = @AccessCode ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "AccessCode", filter.AccessCode));
                }
                if (filter.AccessLevel > 0)
                {
                    sSql += " AND t.AccessLevel <= @AccessLevel ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "AccessLevel", filter.AccessLevel));
                }
                if (!string.IsNullOrEmpty(filter.WriteAccessCode))
                {
                    sSql += " AND t.WriteAccessCode = @WriteAccessCode ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "WriteAccessCode", filter.WriteAccessCode));
                }
                if (filter.WriteAccessLevel > 0)
                {
                    sSql += " AND t.WriteAccessLevel <= @WriteAccessLevel ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "WriteAccessLevel", filter.WriteAccessLevel));
                }
                if (filter.ThreadId > 0 || filter.ThreadId == -1)
                {
                    sSql += " AND t.ThreadId = @ThreadId ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ThreadId", filter.ThreadId));                   
                }

                //datesrange filter
                sSql += " AND ("
                     + Database.AddDatesRangeParameters(myCmd.Parameters, myProv, "t.ItemDate", filter.ItemDateRange)
                     + ")";

                if (filter.IsValidItem != Utility.TristateBool.NotSet)
                {
                    DateTime getDate = DateTime.Now.Date;
                    if (filter.IsValidItem == Utility.TristateBool.True)
                    {
                        sSql += " AND (t.ValidFrom <= @GetDate OR t.ValidFrom is null) ";
                        sSql += " AND (t.ValidTo   >= @GetDate OR t.ValidTo   is null) ";
                        myCmd.Parameters.Add(Database.Parameter(myProv, "GetDate", getDate));
                    }
                    else
                    {
                        sSql += " AND (t.ValidFrom >= @GetDate OR t.ValidTo <= @GetDate) ";
                        myCmd.Parameters.Add(Database.Parameter(myProv, "GetDate", getDate));
                    }
                }

                if (!string.IsNullOrEmpty(filter.ExtId))
                {
                    sSql += " AND t.ExtId = @ExtId ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ExtId", filter.ExtId));
                }

                //custom fields
                if (filter.CustomBool1 != Utility.TristateBool.NotSet)
                {
                    sSql += " AND t.CustomBool1 = @CustomBool1 ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CustomBool1", filter.CustomBool1));
                }
                if (filter.CustomBool2 != Utility.TristateBool.NotSet)
                {
                    sSql += " AND t.CustomBool2 = @CustomBool2 ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CustomBool2", filter.CustomBool2));
                }
                if (filter.CustomBool3 != Utility.TristateBool.NotSet)
                {
                    sSql += " AND t.CustomBool3 = @CustomBool3 ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CustomBool3", filter.CustomBool3));
                }
                if (filter.CustomBool4 != Utility.TristateBool.NotSet)
                {
                    sSql += " AND t.CustomBool4 = @CustomBool4 ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CustomBool4", filter.CustomBool4));
                }
                if (filter.CustomInt1 > 0)
                {
                    sSql += " AND t.CustomInt1 = @CustomInt1 ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CustomInt1", filter.CustomInt1));
                }
                if (filter.CustomInt2 > 0)
                {
                    sSql += " AND t.CustomInt2 = @CustomInt2 ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CustomInt2", filter.CustomInt2));
                }
                if (filter.CustomInt3 > 0)
                {
                    sSql += " AND t.CustomInt3 = @CustomInt3 ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CustomInt3", filter.CustomInt3));
                }
                if (filter.CustomInt4 > 0)
                {
                    sSql += " AND t.CustomInt4 = @CustomInt4 ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CustomInt4", filter.CustomInt4));
                }
                if (!string.IsNullOrEmpty(filter.CustomString1))
                {
                    sSql += " AND t.CustomString1 = @CustomString1 ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CustomString1", filter.CustomString1));
                }
                if (!string.IsNullOrEmpty(filter.CustomString2))
                {
                    sSql += " AND t.CustomString2 = @CustomString2 ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CustomString2", filter.CustomString2));
                }
                if (!string.IsNullOrEmpty(filter.CustomString3))
                {
                    sSql += " AND t.CustomString3 = @CustomString3 ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CustomString3", filter.CustomString3));
                }
                if (!string.IsNullOrEmpty(filter.CustomString4))
                {
                    sSql += " AND t.CustomString4 = @CustomString4 ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CustomString4", filter.CustomString4));
                }

                sSql += " GROUP BY t.Id, t.ItemType, t.CategoryId, categ.SectionId, "
                    + " t.Enabled, t.Alias, t.Ordering, t.DefaultImageName, "
                    + " t.[DateInserted], t.[UserInserted], t.[DateUpdated], t.[UserUpdated], "
                    + " t.ItemDate, t.ValidFrom, t.ValidTo, "
                    + " t.CustomBool1, t.CustomBool2, t.CustomBool3, t.CustomBool4, "
                    + " t.CustomDate1, t.CustomDate2, t.CustomDate3, t.CustomDate4, "
                    + " t.CustomDecimal1, t.CustomDecimal2, t.CustomDecimal3, t.CustomDecimal4, "
                    + " t.CustomInt1, t.CustomInt2, t.CustomInt3, t.CustomInt4, "
                    + " t.CustomString1, t.CustomString2, t.CustomString3, t.CustomString4, "
                    + " t.ItemParams, t.AccessType, t.PermissionId, t.AccessCode, t.AccessLevel, "
                    + " t.CommentsGroupId, t.WriteAccessType, t.WritePermissionId, t.WriteAccessCode, t.WriteAccessLevel, "
                    + " t.ThreadId, t.CssClass, t.ExtId, "
                    + " categ.AccessType, categ.PermissionId, "
                    + " categ.AccessCode, categ.AccessLevel, "
                    + " categ.WriteAccessType, categ.WritePermissionId, "
                    + " categ.WriteAccessCode, categ.WriteAccessLevel, "
                    + " sect.AccessType, sect.PermissionId, "
                    + " sect.AccessCode, sect.AccessLevel, "
                    + " sect.WriteAccessType, sect.WritePermissionId, "
                    + " sect.WriteAccessCode, sect.WriteAccessLevel";
                if (!string.IsNullOrEmpty(sort))
                {
                    sSql += " ORDER BY " + sort;
                }
                else
                {
                    sSql += " ORDER BY categ.SectionId, t.CategoryId, t.Ordering ";
                }

                myCmd.CommandText = Database.ParseSql(sSql);
                myRd = myCmd.ExecuteReader();
                while (myRd.Read())
                {
                    T item = new T();
                    FillObject(item, myRd);
                    result.Add(item);

                    var sec = new Section();
                    fillSection(sec, myRd);
                    if (!sectionsList.Exists(
                        delegate(Section s)
                        {
                            return s.Id == sec.Id;
                        }))
                        sectionsList.Add(sec);

                    var cat = new Category();
                    fillCategory(cat, myRd);
                    if (!categoriesList.Exists(
                        delegate(Category s)
                        {
                            return s.Id == cat.Id;
                        }))
                        categoriesList.Add(cat);
                }
                myRd.Close();

                //(other loop to avoid multiple reader on same command)
                foreach (T item in result)
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

                    //check parent categories
                    loadCategoriesRoles(categoriesList);
                    categoriesList.RemoveAll(new PermissionProvider().IsItemNotAllowed);
                    if (this.WriteMode)
                        categoriesList.RemoveAll(new PermissionProvider().IsItemNotAllowedForWrite);
                    //remove items for not allowed categories
                    result.RemoveAll(new CategoryPredicate(categoriesList).IsItemNotInCategory);
                }

                //culture specifics
                foreach (T item in result)
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

        public override T GetByKey(int id)
        {
            T result = new T();
            List<T> resultList = new List<T>();
            F filter = new F();
            filter.Id = id==0 ? -1 : id;
            filter.ShowOnlyRootItems = false;
            resultList = GetByFilter(filter, "");
            if (resultList.Count > 0)
                result = resultList[0];
            return result;
        }

        /// <summary>
        /// default method to retrieve items
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public T GetByAlias(int categoryId, string alias)
        {
            T result = new T();
            var resultList = new List<T>();
            F filter = new F();

            if (categoryId <= 0)
                return result;

            if (string.IsNullOrEmpty(alias))
                return result;
            
            filter.CategoryId = categoryId;
            filter.Alias = alias;
            filter.ShowOnlyRootItems = false;
            
            resultList = GetByFilter(filter, "");
            if (resultList.Count > 0)
                result = resultList[0];
            return result;
        }

        public T GetByExtId(string extId)
        {
            T result = new T();
            var resultList = new List<T>();
            F filter = new F();

            if (string.IsNullOrEmpty(extId))
                return result;

            filter.ExtId = extId;
            filter.ShowOnlyRootItems = false;
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

        public override int Update(T theObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbTransaction myTrans = null;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            int result = 0;

            //check alias in category
            if (!string.IsNullOrEmpty(theObj.Alias))
            {
                int existingItemId = GetByAlias(theObj.CategoryId, theObj.Alias).Id;
                if (existingItemId > 0 && existingItemId != theObj.Id)
                    throw new CustomException(
                        "Item alias in use", 
                        CustomExceptionSeverity.Warning, CustomExceptionLogLevel.Log,
                        ItemAliasInUseException);
            }

            if (theObj.Ordering == 0)
            {
                theObj.Ordering = this.GetNextOrdering();
            }
            theObj.DateUpdated = DateTime.Now;
            if (string.IsNullOrEmpty(theObj.UserUpdated))
                theObj.UserUpdated = PgnUserCurrent.UserName;
            if (theObj.DateInserted == DateTime.MinValue)
                theObj.DateInserted = DateTime.Now;
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
                + " SET ItemType=@ItemType, CategoryId=@CategoryId, Enabled=@Enabled, "
                + " Alias=@Alias, Ordering=@Ordering, DefaultImageName=@DefaultImageName, "
                + " [DateInserted]=@DateInserted, [UserInserted]=@UserInserted, "
                + " [DateUpdated]=@DateUpdated, [UserUpdated]=@UserUpdated, "
                + " ItemDate=@ItemDate, ValidFrom=@ValidFrom, ValidTo=@ValidTo, "
                + " CustomBool1=@CustomBool1, CustomBool2=@CustomBool2, CustomBool3=@CustomBool3, CustomBool4=@CustomBool4, "
                + " CustomDate1=@CustomDate1, CustomDate2=@CustomDate2, CustomDate3=@CustomDate3, CustomDate4=@CustomDate4, "
                + " CustomDecimal1=@CustomDecimal1, CustomDecimal2=@CustomDecimal2, CustomDecimal3=@CustomDecimal3, CustomDecimal4=@CustomDecimal4, "
                + " CustomInt1=@CustomInt1, CustomInt2=@CustomInt2, CustomInt3=@CustomInt3, CustomInt4=@CustomInt4, "
                + " CustomString1=@CustomString1, CustomString2=@CustomString2, CustomString3=@CustomString3, CustomString4=@CustomString4, "
                + " [ItemParams]=@ItemParams, AccessType=@AccessType, PermissionId=@PermissionId, "
                + " [AccessCode]=@AccessCode, AccessLevel=@AccessLevel, CommentsGroupId=@CommentsGroupId, "
                + " WriteAccessType=@WriteAccessType, WritePermissionId=@WritePermissionId, [WriteAccessCode]=@WriteAccessCode, "
                + " WriteAccessLevel=@WriteAccessLevel, ThreadId=@ThreadId, CssClass=@CssClass, ExtId=@ExtId "
                + " WHERE Id = @Id";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", theObj.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ItemType", theObj.ItemTypeName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CategoryId", theObj.CategoryId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Enabled", theObj.Enabled));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Alias", theObj.Alias));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Ordering", theObj.Ordering));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DefaultImageName", theObj.DefaultImageName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DateInserted", theObj.DateInserted));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UserInserted", theObj.UserInserted));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DateUpdated", theObj.DateUpdated));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UserUpdated", theObj.UserUpdated));
                if (theObj.ItemDate == DateTime.MinValue)
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ItemDate", DBNull.Value));
                else
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ItemDate", theObj.ItemDate));

                if (theObj.ValidFrom == DateTime.MinValue)
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ValidFrom", DBNull.Value));
                else
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ValidFrom", theObj.ValidFrom));

                if (theObj.ValidTo == DateTime.MinValue)
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ValidTo", DBNull.Value));
                else
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ValidTo", theObj.ValidTo));

                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomBool1", theObj.CustomBool1));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomBool2", theObj.CustomBool2));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomBool3", theObj.CustomBool3));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomBool4", theObj.CustomBool4));

                if (theObj.CustomDate1 == DateTime.MinValue)
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CustomDate1", DBNull.Value));
                else
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CustomDate1", theObj.CustomDate1));

                if (theObj.CustomDate2 == DateTime.MinValue)
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CustomDate2", DBNull.Value));
                else
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CustomDate2", theObj.CustomDate2));

                if (theObj.CustomDate3 == DateTime.MinValue)
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CustomDate3", DBNull.Value));
                else
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CustomDate3", theObj.CustomDate3));

                if (theObj.CustomDate4 == DateTime.MinValue)
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CustomDate4", DBNull.Value));
                else
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CustomDate4", theObj.CustomDate4));

                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomDecimal1", theObj.CustomDecimal1));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomDecimal2", theObj.CustomDecimal2));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomDecimal3", theObj.CustomDecimal3));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomDecimal4", theObj.CustomDecimal4));

                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomInt1", theObj.CustomInt1));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomInt2", theObj.CustomInt2));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomInt3", theObj.CustomInt3));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomInt4", theObj.CustomInt4));

                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomString1", theObj.CustomString1));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomString2", theObj.CustomString2));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomString3", theObj.CustomString3));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomString4", theObj.CustomString4));

                myCmd.Parameters.Add(Database.Parameter(myProv, "ItemParams", theObj.ItemParams));
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

                myCmd.Parameters.Add(Database.Parameter(myProv, "CommentsGroupId", theObj.CommentsGroupId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ThreadId", theObj.ThreadId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CssClass", theObj.CssClass));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ExtId", theObj.ExtId));

                result = myCmd.ExecuteNonQuery();
                updateCultureText(theObj, myCmd, myProv);
                updateFormFields(theObj, myCmd, myProv);
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

        public override T Insert(T newObj)
        {
            return this.Insert(newObj, true);
        }

        public T Insert(T newObj, bool checkMaxItems)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbTransaction myTrans = null;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            T result = new T();

            if (checkMaxItems && !checkMaxItemsAllowed(newObj))
                throw new CustomException(
                    MaxItemsException, CustomExceptionSeverity.Info, CustomExceptionLogLevel.Log);

            //check alias in category
            if (!string.IsNullOrEmpty(newObj.Alias))
            {
                if (GetByAlias(newObj.CategoryId, newObj.Alias).Id > 0)
                    throw new CustomException(
                        "Item alias in use",
                        CustomExceptionSeverity.Warning, CustomExceptionLogLevel.Log,
                        ItemAliasInUseException);
            }

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

                //20151910 IDENTITY
                //result.Id = base.GetNextId();
                //if (result.ThreadId == 0)
                //    result.ThreadId = result.Id;

                result.Ordering = base.GetNextOrdering();
                result.DateInserted = DateTime.Now;
                if (string.IsNullOrEmpty(result.UserInserted))
                    result.UserInserted = PgnUserCurrent.UserName;
                result.DateUpdated = DateTime.Now;
                if (string.IsNullOrEmpty(result.UserUpdated))
                    result.UserUpdated = PgnUserCurrent.UserName;

                sSql = "INSERT INTO [" + this.TableName + "](/*Id,*/ ItemType, CategoryId, Enabled, "
                + " Alias, Ordering, DefaultImageName, "
                + " DateInserted, UserInserted, DateUpdated, UserUpdated, "
                + " ItemDate, ValidFrom, ValidTo, "
                + " CustomBool1, CustomBool2, CustomBool3, CustomBool4, "
                + " CustomDate1, CustomDate2, CustomDate3, CustomDate4, "
                + " CustomDecimal1, CustomDecimal2, CustomDecimal3, CustomDecimal4, "
                + " CustomInt1, CustomInt2, CustomInt3, CustomInt4, "
                + " CustomString1, CustomString2, CustomString3, CustomString4, "
                + " ItemParams, AccessType, PermissionId, AccessCode, AccessLevel, CommentsGroupId, "
                + " WriteAccessType, WritePermissionId, WriteAccessCode, WriteAccessLevel, "
                + " ThreadId, CssClass, ExtId) "
                + " VALUES(/*@Id,*/ @ItemType, @CategoryId, @Enabled, "
                + " @Alias, @Ordering, @DefaultImageName, "
                + " @DateInserted, @UserInserted, @DateUpdated, @UserUpdated, "
                + " @ItemDate, @ValidFrom, @ValidTo, "
                + " @CustomBool1, @CustomBool2, @CustomBool3, @CustomBool4, "
                + " @CustomDate1, @CustomDate2, @CustomDate3, @CustomDate4, "
                + " @CustomDecimal1, @CustomDecimal2, @CustomDecimal3, @CustomDecimal4, "
                + " @CustomInt1, @CustomInt2, @CustomInt3, @CustomInt4, "
                + " @CustomString1, @CustomString2, @CustomString3, @CustomString4, "
                + " @ItemParams, @AccessType, @PermissionId, @AccessCode, @AccessLevel, @CommentsGroupId, "
                + " @WriteAccessType, @WritePermissionId, @WriteAccessCode, @WriteAccessLevel, "
                + " @ThreadId, @CssClass, @ExtId) "
                + " SELECT SCOPE_IDENTITY()";
                myCmd.CommandText = Database.ParseSql(sSql);

                //myCmd.Parameters.Add(Database.Parameter(myProv, "Id", result.Id));//IDENTITY
                myCmd.Parameters.Add(Database.Parameter(myProv, "ItemType", result.ItemTypeName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CategoryId", result.CategoryId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Enabled", result.Enabled));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Alias", result.Alias));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Ordering", result.Ordering));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DefaultImageName", result.DefaultImageName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DateInserted", result.DateInserted));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UserInserted", result.UserInserted));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DateUpdated", result.DateUpdated));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UserUpdated", result.UserUpdated));

                if (result.ItemDate == DateTime.MinValue)
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ItemDate", DBNull.Value));
                else
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ItemDate", result.ItemDate));

                if (result.ValidFrom == DateTime.MinValue)
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ValidFrom", DBNull.Value));
                else
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ValidFrom", result.ValidFrom));

                if (result.ValidTo == DateTime.MinValue)
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ValidTo", DBNull.Value));
                else
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ValidTo", result.ValidTo));

                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomBool1", result.CustomBool1));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomBool2", result.CustomBool2));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomBool3", result.CustomBool3));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomBool4", result.CustomBool4));

                if (result.CustomDate1 == DateTime.MinValue)
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CustomDate1", DBNull.Value));
                else
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CustomDate1", result.CustomDate1));

                if (result.CustomDate2 == DateTime.MinValue)
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CustomDate2", DBNull.Value));
                else
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CustomDate2", result.CustomDate2));

                if (result.CustomDate3 == DateTime.MinValue)
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CustomDate3", DBNull.Value));
                else
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CustomDate3", result.CustomDate3));

                if (result.CustomDate4 == DateTime.MinValue)
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CustomDate4", DBNull.Value));
                else
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CustomDate4", result.CustomDate4));

                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomDecimal1", result.CustomDecimal1));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomDecimal2", result.CustomDecimal2));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomDecimal3", result.CustomDecimal3));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomDecimal4", result.CustomDecimal4));

                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomInt1", result.CustomInt1));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomInt2", result.CustomInt2));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomInt3", result.CustomInt3));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomInt4", result.CustomInt4));

                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomString1", result.CustomString1));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomString2", result.CustomString2));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomString3", result.CustomString3));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomString4", result.CustomString4));

                myCmd.Parameters.Add(Database.Parameter(myProv, "ItemParams", result.ItemParams));
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

                myCmd.Parameters.Add(Database.Parameter(myProv, "CommentsGroupId", result.CommentsGroupId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ThreadId", result.ThreadId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CssClass", result.CssClass));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ExtId", result.ExtId));

                result.Id = (int)(decimal)myCmd.ExecuteScalar();

                if (result.ThreadId == 0)
                {
                    //20150910 set item as thread root
                    result.ThreadId = result.Id;

                    sSql = "UPDATE [" + this.TableName + "] SET ThreadId=@ThreadId WHERE Id=@Id ";
                    myCmd.CommandText = Database.ParseSql(sSql);
                    myCmd.Parameters.Clear();
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Id", result.Id));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ThreadId", result.ThreadId));
                    myCmd.ExecuteNonQuery();
                }


                updateCultureText(result, myCmd, myProv);
                updateFormFields(result, myCmd, myProv);
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
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myProv.CreateCommand();
            int res = 0;

            try
            {
                T currObj = this.GetByKey(id);
                var list = new List<Item>();

                if (currObj.IsThreadRoot)
                    list = currObj.ThreadList;
                else
                    list.Add(currObj);


                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                foreach (var item in list)
                {
                    deleteObj(item, myProv, myConn, myCmd);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                myConn.Dispose();
            }
            return res;
        }

        private void deleteObj(Item currObj, 
            DbProviderFactory myProv, DbConnection myConn, DbCommand myCmd)
        {
            string sSql;

            currObj.DeleteImages();
            currObj.DeleteFiles();
            new PermissionProvider().RemovePermissionById(currObj.ReadPermissionId);
            new PermissionProvider().RemovePermissionById(currObj.WritePermissionId);

            var iman = new ItemAttributesValuesManager();
            iman.DeleteByItemId(currObj.Id);


            //item
            sSql = "DELETE FROM [" + this.TableName + "] WHERE Id = @Id ";
            myCmd.CommandText = Database.ParseSql(sSql);
            myCmd.Parameters.Clear();
            myCmd.Parameters.Add(Database.Parameter(myProv, "Id", currObj.Id));
            myCmd.ExecuteNonQuery();

            //culture
            sSql = "DELETE FROM [" + this.TableName + "_Culture] WHERE ItemId = @ItemId ";
            myCmd.CommandText = Database.ParseSql(sSql);
            myCmd.Parameters.Clear();
            myCmd.Parameters.Add(Database.Parameter(myProv, "ItemId", currObj.Id));
            myCmd.ExecuteNonQuery();

            //optional fields
            sSql = "DELETE FROM [#__itemFieldValues] WHERE ItemId = @ItemId ";
            myCmd.CommandText = Database.ParseSql(sSql);
            myCmd.Parameters.Clear();
            myCmd.Parameters.Add(Database.Parameter(myProv, "ItemId", currObj.Id));
            myCmd.ExecuteNonQuery();

            //related
            sSql = @"DELETE FROM #__itemsRelated 
            WHERE ItemId = @ItemId  OR RelatedId = @itemId ";
            myCmd.CommandText = Database.ParseSql(sSql);
            myCmd.Parameters.Clear();
            myCmd.Parameters.Add(Database.Parameter(myProv, "ItemId", currObj.Id));
            myCmd.ExecuteNonQuery();
        }

        private class SectionPredicate
        {
            private List<Section> list = null;
            public SectionPredicate(List<Section> list)
            {
                this.list = list;
            }

            public bool IsItemNotInSection(T item)
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

        private class CategoryPredicate
        {
            private List<Category> list = null;
            public CategoryPredicate(List<Category> list)
            {
                this.list = list;
            }

            public bool IsItemNotInCategory(T item)
            {
                foreach (var s in list)
                    if (s.Id == item.CategoryId) return false;
                return true;
            }
        }

        private void loadCategoriesRoles(List<Category> list)
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

        private void fillCategory(Category result, DbDataReader myRd)
        {
            if (!Convert.IsDBNull(myRd["CategoryId"]))
                result.Id = (int)myRd["CategoryId"];

            //read permissions
            if (!Convert.IsDBNull(myRd["CategAccessType"]))
                result.ReadAccessType = (MenuAccesstype)int.Parse(myRd["CategAccessType"].ToString());
            if (!Convert.IsDBNull(myRd["CategPermissionId"]))
                result.ReadPermissionId = (int)myRd["CategPermissionId"];
            if (!Convert.IsDBNull(myRd["CategAccessCode"]))
                result.ReadAccessCode = (string)myRd["CategAccessCode"];
            if (!Convert.IsDBNull(myRd["CategAccessLevel"]))
                result.ReadAccessLevel = (int)myRd["CategAccessLevel"];

            //write permissions
            if (!Convert.IsDBNull(myRd["CategWriteAccessType"]))
                result.WriteAccessType = (MenuAccesstype)int.Parse(myRd["CategWriteAccessType"].ToString());
            if (!Convert.IsDBNull(myRd["CategWritePermissionId"]))
                result.WritePermissionId = (int)myRd["CategWritePermissionId"];
            if (!Convert.IsDBNull(myRd["CategWriteAccessCode"]))
                result.WriteAccessCode = (string)myRd["CategWriteAccessCode"];
            if (!Convert.IsDBNull(myRd["CategWriteAccessLevel"]))
                result.WriteAccessLevel = (int)myRd["CategWriteAccessLevel"];
        }

        protected override void FillObject(T result, DbDataReader myRd)
        {
            if (!Convert.IsDBNull(myRd["Id"]))
                result.Id = (int)myRd["Id"];
            if (!Convert.IsDBNull(myRd["ItemType"]))
                result.ItemTypeName = (string)myRd["ItemType"];
            if (!Convert.IsDBNull(myRd["CategoryId"]))
                result.CategoryId = (int)myRd["CategoryId"];
            if (!Convert.IsDBNull(myRd["SectionId"]))
                result.SectionId = (int)myRd["SectionId"];
            if (!Convert.IsDBNull(myRd["Enabled"]))
                result.Enabled = (bool)myRd["Enabled"];
            if (!Convert.IsDBNull(myRd["Ordering"]))
                result.Ordering = (int)myRd["Ordering"];
            if (!Convert.IsDBNull(myRd["Alias"]))
                result.Alias = (string)myRd["Alias"];
            if (!Convert.IsDBNull(myRd["DefaultImageName"]))
                result.DefaultImageName = myRd["DefaultImageName"].ToString();
            if (!Convert.IsDBNull(myRd["DateInserted"]))
                result.DateInserted = (DateTime)myRd["DateInserted"];
            if (!Convert.IsDBNull(myRd["UserInserted"]))
                result.UserInserted = (string)myRd["UserInserted"];
            if (!Convert.IsDBNull(myRd["DateUpdated"]))
                result.DateUpdated = (DateTime)myRd["DateUpdated"];
            if (!Convert.IsDBNull(myRd["UserUpdated"]))
                result.UserUpdated = (string)myRd["UserUpdated"];
            if (!Convert.IsDBNull(myRd["ItemDate"]))
                result.ItemDate = (DateTime)myRd["ItemDate"];
            if (!Convert.IsDBNull(myRd["ValidFrom"]))
                result.ValidFrom = (DateTime)myRd["ValidFrom"];
            if (!Convert.IsDBNull(myRd["ValidTo"]))
                result.ValidTo = (DateTime)myRd["ValidTo"];

            //custom fields
            if (!Convert.IsDBNull(myRd["CustomBool1"]))
                result.CustomBool1 = (bool)myRd["CustomBool1"];
            if (!Convert.IsDBNull(myRd["CustomBool2"]))
                result.CustomBool2 = (bool)myRd["CustomBool2"];
            if (!Convert.IsDBNull(myRd["CustomBool3"]))
                result.CustomBool3 = (bool)myRd["CustomBool3"];
            if (!Convert.IsDBNull(myRd["CustomBool4"]))
                result.CustomBool4 = (bool)myRd["CustomBool4"];

            if (!Convert.IsDBNull(myRd["CustomDate1"]))
                result.CustomDate1 = (DateTime)myRd["CustomDate1"];
            if (!Convert.IsDBNull(myRd["CustomDate2"]))
                result.CustomDate2 = (DateTime)myRd["CustomDate2"];
            if (!Convert.IsDBNull(myRd["CustomDate3"]))
                result.CustomDate3 = (DateTime)myRd["CustomDate3"];
            if (!Convert.IsDBNull(myRd["CustomDate4"]))
                result.CustomDate4 = (DateTime)myRd["CustomDate4"];

            if (!Convert.IsDBNull(myRd["CustomDecimal1"]))
                result.CustomDecimal1 = (decimal)myRd["CustomDecimal1"];
            if (!Convert.IsDBNull(myRd["CustomDecimal2"]))
                result.CustomDecimal2 = (decimal)myRd["CustomDecimal2"];
            if (!Convert.IsDBNull(myRd["CustomDecimal3"]))
                result.CustomDecimal3 = (decimal)myRd["CustomDecimal3"];
            if (!Convert.IsDBNull(myRd["CustomDecimal4"]))
                result.CustomDecimal4 = (decimal)myRd["CustomDecimal4"];

            if (!Convert.IsDBNull(myRd["CustomInt1"]))
                result.CustomInt1 = (int)myRd["CustomInt1"];
            if (!Convert.IsDBNull(myRd["CustomInt2"]))
                result.CustomInt2 = (int)myRd["CustomInt2"];
            if (!Convert.IsDBNull(myRd["CustomInt3"]))
                result.CustomInt3 = (int)myRd["CustomInt3"];
            if (!Convert.IsDBNull(myRd["CustomInt4"]))
                result.CustomInt4 = (int)myRd["CustomInt4"];

            if (!Convert.IsDBNull(myRd["CustomString1"]))
                result.CustomString1 = (string)myRd["CustomString1"];
            if (!Convert.IsDBNull(myRd["CustomString2"]))
                result.CustomString2 = (string)myRd["CustomString2"];
            if (!Convert.IsDBNull(myRd["CustomString3"]))
                result.CustomString3 = (string)myRd["CustomString3"];
            if (!Convert.IsDBNull(myRd["CustomString4"]))
                result.CustomString4 = (string)myRd["CustomString4"];

            if (!Convert.IsDBNull(myRd["ItemParams"]))
                result.ItemParams = (string)myRd["ItemParams"];
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

            if (!Convert.IsDBNull(myRd["ThreadId"]))
                result.ThreadId = (int)myRd["ThreadId"];
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
            var manCateg = new CategoriesManager();

            try
            {
                var o1 = new PigeonCms.Item();
                o1 = this.GetByKey(currentRecordId);
                var cat1 = new PigeonCms.Category();
                cat1 = manCateg.GetByKey(o1.CategoryId);

                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT TOP 1 t.Id "
                + " FROM [" + this.TableName + "] t "
                + " LEFT JOIN #__categories categ ON t.CategoryId = categ.Id "
                + " WHERE t.Ordering < @Ordering  "
                + " AND t.CategoryId = @CategoryId "
                + " AND categ.SectionId = @SectionId "
                + " ORDER BY t.Ordering DESC ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Ordering", ordering));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CategoryId", o1.CategoryId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "SectionId", cat1.SectionId));
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
            var manCateg = new CategoriesManager();

            try
            {
                var o1 = new PigeonCms.Item();
                o1 = this.GetByKey(currentRecordId);
                var cat1 = new PigeonCms.Category();
                cat1 = manCateg.GetByKey(o1.CategoryId);

                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT TOP 1 t.Id "
                + " FROM [" + this.TableName + "] t "
                + " LEFT JOIN #__categories categ ON t.CategoryId = categ.Id "
                + " WHERE t.Ordering > @Ordering "
                + " AND t.CategoryId = @CategoryId "
                + " AND categ.SectionId = @SectionId "
                + " ORDER BY t.Ordering ASC ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Ordering", ordering));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CategoryId", o1.CategoryId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "SectionId", cat1.SectionId));
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

        private bool checkMaxItemsAllowed(T item)
        {
            bool res = true;
            int maxItems = item.Category.Section.MaxItems;
            if (maxItems > 0 &&
                item.Category.Section.NumOfItems >= maxItems)
                res = false;
            return res;
        }

        private void getCultureSpecific(T result, DbDataReader myRd,
                DbCommand myCmd, DbProviderFactory myProv)
        {
            string sSql;
            //culture specific
            sSql = "SELECT CultureName, ItemId, Title, Description "
                + " FROM [" + this.TableName + "_culture] t "
                + " WHERE ItemId = @ItemId ";
            //CustomString1, CustomString2, CustomString3, CustomText1, CustomText2, CustomText3
            myCmd.CommandText = Database.ParseSql(sSql);
            myCmd.Parameters.Clear();
            myCmd.Parameters.Add(Database.Parameter(myProv, "ItemId", result.Id));
            myRd = myCmd.ExecuteReader();
            while (myRd.Read())
            {
                if (!Convert.IsDBNull(myRd["Title"]))
                    result.TitleTranslations.Add((string)myRd["cultureName"], (string)myRd["Title"]);
                if (!Convert.IsDBNull(myRd["Description"]))
                    result.DescriptionTranslations.Add((string)myRd["cultureName"], (string)myRd["Description"]);
                
                //custom fields
                //if (!Convert.IsDBNull(myRd["CustomString1"]))
                //    result.CustomString1Translations.Add((string)myRd["cultureName"], (string)myRd["CustomString1"]);
                //if (!Convert.IsDBNull(myRd["CustomString2"]))
                //    result.CustomString2Translations.Add((string)myRd["cultureName"], (string)myRd["CustomString2"]);
                //if (!Convert.IsDBNull(myRd["CustomString3"]))
                //    result.CustomString3Translations.Add((string)myRd["cultureName"], (string)myRd["CustomString3"]);
                //if (!Convert.IsDBNull(myRd["CustomText1"]))
                //    result.CustomText1Translations.Add((string)myRd["cultureName"], (string)myRd["CustomText1"]);
                //if (!Convert.IsDBNull(myRd["CustomText2"]))
                //    result.CustomText2Translations.Add((string)myRd["cultureName"], (string)myRd["CustomText2"]);
                //if (!Convert.IsDBNull(myRd["CustomText3"]))
                //    result.CustomText3Translations.Add((string)myRd["cultureName"], (string)myRd["CustomText3"]);
            }
            myRd.Close();
        }

        private void updateCultureText(T theObj, DbCommand myCmd, DbProviderFactory myProv)
        {
            foreach (KeyValuePair<string, string> item in theObj.TitleTranslations)
            {
                string sSql = "";
                string descriptionValue = "";
                //string customString1 = "";
                //string customString2 = "";
                //string customString3 = "";
                //string customText1 = "";
                //string customText2 = "";
                //string customText3 = "";

                theObj.DescriptionTranslations.TryGetValue(item.Key, out descriptionValue);
                if (string.IsNullOrEmpty(descriptionValue))
                    descriptionValue = "";
                //theObj.CustomString1Translations.TryGetValue(item.Key, out customString1);
                //theObj.CustomString2Translations.TryGetValue(item.Key, out customString2);
                //theObj.CustomString3Translations.TryGetValue(item.Key, out customString3);
                //theObj.CustomText1Translations.TryGetValue(item.Key, out customText1);
                //theObj.CustomText2Translations.TryGetValue(item.Key, out customText2);
                //theObj.CustomText3Translations.TryGetValue(item.Key, out customText3);

                //delete previous entry (if exists)
                sSql = "DELETE FROM [" + this.TableName + "_culture] WHERE CultureName=@CultureName AND ItemId=@ItemId ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "CultureName", item.Key));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ItemId", theObj.Id));
                myCmd.ExecuteNonQuery();

                //insert current culture entry
                sSql = "INSERT INTO [" + this.TableName + "_culture](CultureName, ItemId, Title, Description) "
                + " VALUES(@CultureName, @ItemId, @Title, @Description) ";
                //CustomString1, CustomString2, CustomString3, CustomText1, CustomText2, CustomText3
                //@CustomString1, @CustomString2, @CustomString3, @CustomText1, @CustomText2, @CustomText3
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "CultureName", item.Key));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ItemId", theObj.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Title", item.Value));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Description", descriptionValue));
                //myCmd.Parameters.Add(Database.Parameter(myProv, "CustomString1", customString1));
                //myCmd.Parameters.Add(Database.Parameter(myProv, "CustomString2", customString2));
                //myCmd.Parameters.Add(Database.Parameter(myProv, "CustomString3", customString3));
                //myCmd.Parameters.Add(Database.Parameter(myProv, "CustomText1", customText1));
                //myCmd.Parameters.Add(Database.Parameter(myProv, "CustomText2", customText2));
                //myCmd.Parameters.Add(Database.Parameter(myProv, "CustomText3", customText3));

                myCmd.ExecuteNonQuery();
            }
        }

        private void updateFormFields(T theObj, DbCommand myCmd, DbProviderFactory myProv)
        {
            DbDataReader myRd = null;
            if (theObj.FormFields == null)
                return;
            foreach (KeyValuePair<int, string> item in theObj.FormFields)
            {
                string sSql = "";

                if (item.Key == 0)
                    continue;

                sSql = "SELECT * FROM #__itemFieldValues WHERE FormFieldId=@FormFieldId AND ItemId=@ItemId ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "FormFieldId", item.Key));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ItemId", theObj.Id));
                myRd = myCmd.ExecuteReader();
                if (myRd.Read())
                    sSql = "UPDATE #__itemFieldValues SET Value=@Value "
                    + " WHERE FormFieldId=@FormFieldId AND ItemId=@ItemId ";
                else
                    sSql = "INSERT INTO #__itemFieldValues(FormFieldId, ItemId, Value)"
                    + " VALUES(@FormFieldId, @ItemId, @Value)";
                myRd.Close();
                
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "FormFieldId", item.Key));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ItemId", theObj.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Value", item.Value));
                myCmd.ExecuteNonQuery();
            }
        }

        public Dictionary<int, string> GetFormFieldsDictionary(int itemId)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            var res = new Dictionary<int, string>();
            string sSql;

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT FormFieldId, ItemId, Value "
                + " FROM #__itemFieldValues t "
                + " WHERE ItemId = @ItemId ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "ItemId", itemId));
                myRd = myCmd.ExecuteReader();
                while (myRd.Read())
                {
                    int formFieldId = 0;
                    string value = "";

                    if (!Convert.IsDBNull(myRd["FormFieldId"]))
                        formFieldId = (int)myRd["FormFieldId"];
                    if (!Convert.IsDBNull(myRd["Value"]))
                        value = (string)myRd["Value"];

                    res.Add(formFieldId, value);
                }
                myRd.Close();
            }
            finally
            {
                myConn.Dispose();
            }
            return res;
        }

		//TOCHECK-LOLLO
        //ottimizzare qry (magari includere in ItemsFilter) - includere prop Related (List<T>) in Item; renamed
        public List<T> GetRelatedItems(int itemId)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            var relatedIdList = new List<int>();
            string sSql;

            myConn.ConnectionString = Database.ConnString;
            myConn.Open();
            myCmd.Connection = myConn;

            try {
                sSql = "SELECT RelatedId "
                  + " FROM #__itemsRelated r "
                  + " WHERE ItemId = @ItemId ";

                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "ItemId", itemId));
                myRd = myCmd.ExecuteReader();
                while (myRd.Read())
                {
                    int RelatedId = 0;

                    if (!Convert.IsDBNull(myRd["RelatedId"]))
                        RelatedId = (int)myRd["RelatedId"];

                    relatedIdList.Add(RelatedId);
                }
                myRd.Close();
            }
            finally
            {
                myConn.Dispose();
            }

            //TODO add in filter List<int>
            var relatedItemList = new List<T>();
            foreach(var Id in relatedIdList) {
                relatedItemList.Add(this.GetByKey(Id));
            }

            return relatedItemList;
        }

		//TOCHECK-LOLLO - edit picce (in SetRelated) TODO: check if exists
        //TODO check if exists
        public void SetRelated(int itemId, int RelatedId)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();

            myConn.ConnectionString = Database.ConnString;
            myConn.Open();
            myCmd.Connection = myConn;

            try
            {
                string sSql = "";
                sSql = "INSERT INTO #__itemsRelated (ItemId, RelatedId) "
                      + " VALUES(@ItemId, @RelatedId) ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "ItemId", itemId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "RelatedId", RelatedId));
                myCmd.ExecuteNonQuery();

            }
            finally
            {
                myConn.Dispose();
            }
            
        }
        
        //TOCHECK-LOLLO - edit picce (in DeleteRelated) TODO: check if exists
        public void DeleteRelated(int itemId, int relatedId)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();

            myConn.ConnectionString = Database.ConnString;
            myConn.Open();
            myCmd.Connection = myConn;

            try
            {
                string sSql = "";
                sSql = "DELETE FROM #__itemsRelated "
                      + " WHERE ItemId=@ItemId AND RelatedId=@RelatedId ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "ItemId", itemId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "RelatedId", relatedId));
                myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }

        }
    }
}