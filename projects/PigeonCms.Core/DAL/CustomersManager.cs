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
    /// DAL for Customer obj (in table #__customers)
    /// </summary>
    public class CustomersManager : TableManager<Customer, CustomersFilter, int>, ITableManager
    {
        [DebuggerStepThrough()]
        public CustomersManager()
        {
            this.TableName = "#__customers";
            this.KeyFieldName = "Id";
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public override List<Customer> GetByFilter(CustomersFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            var result = new List<Customer>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT t.Id, t.CompanyName, t.Vat, "
                    + " t.DateInserted, t.UserInserted, "
                    + " t.DateUpdated, t.UserUpdated "
                    + " FROM ["+ this.TableName +"] t "
                    + " WHERE t.Id > 0 ";
                if (filter.Id > 0 || filter.Id == -1)
                {
                    sSql += " AND t.Id = @Id ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Id", filter.Id));
                }
                if (!string.IsNullOrEmpty(filter.CompanyNameLike))
                {
                    sSql += " AND t.CompanyName like @CompanyNameLike ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CompanyNameLike", "%" + filter.CompanyNameLike + "%"));
                }
                if (!string.IsNullOrEmpty(filter.Vat))
                {
                    sSql += " AND t.Vat = @Vat ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Vat", filter.Vat));
                }
                if (!string.IsNullOrEmpty(filter.VatLike))
                {
                    sSql += " AND t.Vat like @VatLike ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "VatLike", "%" + filter.VatLike + "%"));
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    sSql += " ORDER BY " + sort;
                }
                else
                {
                    sSql += " ORDER BY t.Id DESC ";
                }

                myCmd.CommandText = Database.ParseSql(sSql);
                myRd = myCmd.ExecuteReader();
                while (myRd.Read())
                {
                    var item = new Customer();
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
        public override Customer GetByKey(int id)
        {
            var result = new Customer();
            var list = new List<Customer>();
            var filter = new CustomersFilter();
            filter.Id = id == 0 ? -1 : id;
            list = GetByFilter(filter, "");
            if (list.Count > 0)
                result = list[0];
            return result;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Customer> GetByVat(string vat)
        {
            var filter = new CustomersFilter();
            if (string.IsNullOrEmpty(vat))
                filter.Id = -1;
            filter.Vat = vat;
            return GetByFilter(filter, "");
        }

        public override int Update(Customer theObj)
        {
            return this.Update(theObj, true);
        }

        public int Update(Customer theObj, bool checkVatKey)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            int result = 0;

            if (theObj.DateInserted == DateTime.MinValue)
                theObj.DateInserted = DateTime.Now;

            theObj.DateUpdated = DateTime.Now;
            theObj.UserUpdated = PgnUserCurrent.UserName;

            try
            {
                if (checkVatKey)
                {
                    var list = this.GetByVat(theObj.Vat);
                    if (list.Count > 0)
                    {
                        if (list[0].Id != theObj.Id)
                            throw new DuplicateNameException("Vat");
                    }
                }

                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "UPDATE [" + this.TableName + "] "
                + " SET CompanyName=@CompanyName, Vat=@Vat "
                + " WHERE Id = @Id";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", theObj.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CompanyName", theObj.CompanyName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Vat", theObj.Vat));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DateUpdated", theObj.DateUpdated));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UserUpdated", theObj.UserUpdated));

                result = myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public override Customer Insert(Customer newObj)
        {
            return this.Insert(newObj, true);
        }

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public Customer Insert(Customer newObj, bool checkVatKey)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            var result = new PigeonCms.Customer();

            try
            {
                if (checkVatKey)
                {
                    var list = this.GetByVat(newObj.Vat);
                    if (list.Count > 0)
                        throw new DuplicateNameException("Vat");
                }

                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                result = newObj;
                result.Id = base.GetNextId();
                result.DateInserted = DateTime.Now;
                result.UserInserted = PgnUserCurrent.UserName;
                result.DateUpdated = DateTime.Now;
                result.UserUpdated = PgnUserCurrent.UserName;

                sSql = "INSERT INTO [" + this.TableName + "]"
                    + "(Id, CompanyName, Vat, "
                    + " DateInserted, UserInserted, DateUpdated, UserUpdated) "
                    + " VALUES(@Id, @CompanyName, @Vat, "
                    + " @DateInserted, @UserInserted, @DateUpdated, @UserUpdated) ";
                myCmd.CommandText = Database.ParseSql(sSql);

                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", result.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CompanyName", result.CompanyName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Vat", result.Vat));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DateInserted", result.DateInserted));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UserInserted", result.UserInserted));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DateUpdated", result.DateUpdated));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UserUpdated", result.UserUpdated));

                myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public int DeleteByVat(string vat)
        {
            int res = 0;
            var list = this.GetByVat(vat);
            foreach (var item in list)
            {
                res += base.DeleteById(item.Id);
            }
            return res;
        }

        protected override void FillObject(Customer result, DbDataReader myRd)
        {
            if (!Convert.IsDBNull(myRd["Id"]))
                result.Id = (int)myRd["Id"];
            if (!Convert.IsDBNull(myRd["CompanyName"]))
                result.CompanyName = (string)myRd["CompanyName"];
            if (!Convert.IsDBNull(myRd["Vat"]))
                result.Vat = (string)myRd["Vat"];
            if (!Convert.IsDBNull(myRd["DateInserted"]))
                result.DateInserted = (DateTime)myRd["DateInserted"];
            if (!Convert.IsDBNull(myRd["UserInserted"]))
                result.UserInserted = (string)myRd["UserInserted"];
            if (!Convert.IsDBNull(myRd["DateUpdated"]))
                result.DateUpdated = (DateTime)myRd["DateUpdated"];
            if (!Convert.IsDBNull(myRd["UserUpdated"]))
                result.UserUpdated = (string)myRd["UserUpdated"];
        }
    }
}