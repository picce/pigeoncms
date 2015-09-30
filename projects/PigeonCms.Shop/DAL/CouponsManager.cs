using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PigeonCms.Shop
{
    //TOTEST
    public class CouponsManager : TableManager<Coupon, CouponsFilter, int>, ITableManager
    {

        [DebuggerStepThrough()]
        public CouponsManager()
        {
            this.TableName = "#__shop_coupons";
            this.KeyFieldName = "Id";
        }

        public override List<Coupon> GetByFilter(CouponsFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            var result = new List<Coupon>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT t.Id, t.Code, t.DateInserted, t.UserInserted, t.DateUpdated, t.UserUpdated, "
                    + " t.ValidFrom, t.ValidTo, t.Enabled, t.Amount, t.IsPercentage, "
                    + " t.MinOrderAmount, t.CategoriesIdList, t.ItemsIdList, t.ItemType, "
                    + " t.MaxUses, t.UsesCounter "
                    + " FROM [" + this.TableName + "] t "
                    + " WHERE t.Id > 0 ";
                if (filter.Id > 0 || filter.Id == -1)
                {
                    sSql += " AND t.Id = @Id ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Id", filter.Id));
                }
                if (!string.IsNullOrEmpty((filter.Code)))
                {
                    sSql += " AND t.Code = @Code ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Code", filter.Code));
                }
                if (!string.IsNullOrEmpty((filter.ItemType)))
                {
                    sSql += " AND t.ItemType = @ItemType ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ItemType", filter.ItemType));
                }
                if (filter.Enabled != Utility.TristateBool.NotSet)
                {
                    sSql += " AND t.Enabled = @Enabled ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Enabled", filter.Enabled));
                }

                //daterange
                sSql += " AND ("
                     + Database.AddDatesRangeParameters(myCmd.Parameters, myProv, "t.ValidFrom", filter.ValidFromRange)
                     + ")";

                sSql += " AND ("
                     + Database.AddDatesRangeParameters(myCmd.Parameters, myProv, "t.ValidTo", filter.ValidToRange)
                     + ")";

                if (!string.IsNullOrEmpty(sort))
                {
                    sSql += " ORDER BY " + sort;
                }
                else
                {
                    sSql += " ORDER BY t.DateInserted ";
                }

                myCmd.CommandText = Database.ParseSql(sSql);
                myRd = myCmd.ExecuteReader();
                while (myRd.Read())
                {
                    bool bAdd = true;
                    var item = new Coupon();
                    FillObject(item, myRd);

                    if (filter.IsValid != Utility.TristateBool.NotSet)
                    {
                        bAdd = false;
                        if (filter.IsValid != Utility.TristateBool.True && item.IsValid)
                            bAdd = true;
                        if (filter.IsValid != Utility.TristateBool.False && !item.IsValid)
                            bAdd = true;
                    }

                    if (bAdd)
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

        public override Coupon GetByKey(int id)
        {
            var result = new Coupon();
            var resultList = new List<Coupon>();
            var filter = new CouponsFilter();

            if (id <= 0)
                return result;

            filter.Id = id;
            resultList = GetByFilter(filter, "");
            if (resultList.Count > 0)
                result = resultList[0];

            return result;
        }

        public Coupon GetByCode(string code)
        {
            var result = new Coupon();
            var resultList = new List<Coupon>();
            var filter = new CouponsFilter();

            if (string.IsNullOrEmpty(code))
                return result;

            filter.Code = code;
            resultList = GetByFilter(filter, "");
            if (resultList.Count > 0)
                result = resultList[0];

            return result;
        }

        public override int Update(Coupon theObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            int result = 0;

            theObj.DateUpdated = DateTime.Now;
            theObj.UserUpdated = PgnUserCurrent.UserName;

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "UPDATE [" + this.TableName + "] "
                + " SET Code=@Code, "
                + " DateUpdated=@DateUpdated, UserUpdated=@UserUpdated, "
                + " ValidFrom=@ValidFrom, ValidTo=@ValidTo, Enabled=@Enabled, Amount=@Amount, "
                + " IsPercentage=@IsPercentage, MinOrderAmount=@MinOrderAmount, ItemType=@ItemType, "
                + " CategoriesIdList=@CategoriesIdList, ItemsIdList=@ItemsIdList, "                
                + " MaxUses=@MaxUses, UsesCounter=@UsesCounter "
                + " WHERE Id = @Id";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", theObj.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Code", theObj.Code));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DateUpdated", DateTime.Now));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UserUpdated", PgnUserCurrent.UserName));

                if (theObj.ValidFrom == DateTime.MinValue)
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ValidFrom", DBNull.Value));
                else
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ValidFrom", theObj.ValidFrom));

                if (theObj.ValidTo == DateTime.MinValue)
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ValidTo", DBNull.Value));
                else
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ValidTo", theObj.ValidTo));

                myCmd.Parameters.Add(Database.Parameter(myProv, "Enabled", theObj.Enabled));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Amount", theObj.Amount));
                myCmd.Parameters.Add(Database.Parameter(myProv, "IsPercentage", theObj.IsPercentage));
                myCmd.Parameters.Add(Database.Parameter(myProv, "MinOrderAmount", theObj.MinOrderAmount));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ItemType", theObj.ItemType));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CategoriesIdList", theObj.CategoriesIdListString));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ItemsIdList", theObj.ItemsIdListString));
                myCmd.Parameters.Add(Database.Parameter(myProv, "MaxUses", theObj.MaxUses));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UsesCounter", theObj.UsesCounter));

                result = myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override Coupon Insert(Coupon newObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            var result = new Coupon();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                result = newObj;

                sSql = "INSERT INTO [" + this.TableName + "](t.Code, t.DateInserted, t.UserInserted, t.DateUpdated, t.UserUpdated, "
                    + " t.ValidFrom, t.ValidTo, t.Enabled, t.Amount, t.IsPercentage, t.MinOrderAmount, t.ItemType, "
                    + " t.CategoriesIdList, t.ItemsIdList, t.MaxUses, t.UsesCounter ) "
                    + " VALUES(@Code, @DateInserted, @UserInserted, @DateUpdated, "
                    + " @UserUpdated, @ValidFrom, @ValidTo, @Enabled, @Amount, @IsPercentage, @MinOrderAmount, @ItemType, "
                    + " @CategoriesIdList, @ItemsIdList, @MaxUses, @UsesCounter) ";
                myCmd.CommandText = Database.ParseSql(sSql);

                myCmd.Parameters.Add(Database.Parameter(myProv, "Code", result.Code));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DateInserted", DateTime.Now));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UserInserted", PgnUserCurrent.UserName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DateUpdated", DateTime.Now));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UserUpdated", PgnUserCurrent.UserName));
                if (result.ValidFrom == DateTime.MinValue)
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ValidFrom", DBNull.Value));
                else
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ValidFrom", result.ValidFrom));

                if (result.ValidTo == DateTime.MinValue)
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ValidTo", DBNull.Value));
                else
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ValidTo", result.ValidTo));

                myCmd.Parameters.Add(Database.Parameter(myProv, "Enabled", result.Enabled));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Amount", result.Amount));
                myCmd.Parameters.Add(Database.Parameter(myProv, "IsPercentage", result.IsPercentage));
                myCmd.Parameters.Add(Database.Parameter(myProv, "MinOrderAmount", result.MinOrderAmount));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CategoriesIdList", result.CategoriesIdListString));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ItemsIdList", result.ItemsIdListString));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ItemType", result.ItemType));
                myCmd.Parameters.Add(Database.Parameter(myProv, "MaxUses", result.MaxUses));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UsesCounter", result.UsesCounter));

                myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        protected override void FillObject(Coupon result, DbDataReader myRd)
        {
            if (!Convert.IsDBNull(myRd["Id"]))
                result.Id = (int)myRd["Id"];
            if (!Convert.IsDBNull(myRd["Code"]))
                result.Code = (string)myRd["Code"];
            if (!Convert.IsDBNull(myRd["DateInserted"]))
                result.DateInserted = (DateTime)myRd["DateInserted"];
            if (!Convert.IsDBNull(myRd["UserInserted"]))
                result.UserInserted = (string)myRd["UserInserted"];
            if (!Convert.IsDBNull(myRd["DateUpdated"]))
                result.DateUpdated = (DateTime)myRd["DateUpdated"];
            if (!Convert.IsDBNull(myRd["UserUpdated"]))
                result.UserUpdated = (string)myRd["UserUpdated"];
            if (!Convert.IsDBNull(myRd["ValidFrom"]))
                result.ValidFrom = (DateTime)myRd["ValidFrom"];
            if (!Convert.IsDBNull(myRd["ValidTo"]))
                result.ValidTo = (DateTime)myRd["ValidTo"];
            if (!Convert.IsDBNull(myRd["Enabled"]))
                result.Enabled = (bool)myRd["Enabled"];
            if (!Convert.IsDBNull(myRd["Amount"]))
                result.Amount = (decimal)myRd["Amount"];
            if (!Convert.IsDBNull(myRd["IsPercentage"]))
                result.IsPercentage = (bool)myRd["IsPercentage"];
            if (!Convert.IsDBNull(myRd["MinOrderAmount"]))
                result.MinOrderAmount = (decimal)myRd["MinOrderAmount"];
            if (!Convert.IsDBNull(myRd["CategoriesIdList"]))
                result.CategoriesIdListString = (string)myRd["CategoriesIdList"];
            if (!Convert.IsDBNull(myRd["ItemsIdList"]))
                result.ItemsIdListString = (string)myRd["ItemsIdList"];
            if (!Convert.IsDBNull(myRd["ItemType"]))
                result.ItemType = (string)myRd["ItemType"];
            if (!Convert.IsDBNull(myRd["MaxUses"]))
                result.MaxUses = (int)myRd["MaxUses"];
            if (!Convert.IsDBNull(myRd["UsesCounter"]))
                result.UsesCounter = (int)myRd["UsesCounter"];
        }

    }
}
