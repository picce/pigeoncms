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


namespace PigeonCms.Shop
{
    /// <summary>
    /// DAL for Customer obj (in table #__shop_customers)
    /// </summary>
    public class CustomersManager : TableManager<Customer, CustomersFilter, int>, ITableManager
    {
        private bool checkUserContext = true;

        private string ownerUser
        {
            get 
            {
                string res = "";
                if (this.checkUserContext)
                {
                    res = "USER__NOT__AUTH";    //invalidate query
                    if (PgnUserCurrent.IsAuthenticated)
                        res = PgnUserCurrent.UserName;
                }
                return res;
            }
        }


        /// <summary>
        /// checkUserContext = true;
        /// </summary>
        [DebuggerStepThrough()]
        public CustomersManager():this(true)
        { 
        }

        public CustomersManager(bool checkUserContext)
        {
            this.TableName = "#__shop_customers";
            this.KeyFieldName = "Id";

            this.checkUserContext = checkUserContext;
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

                sSql = "SELECT t.Id, t.OwnerUser, "
                    + " t.DateInserted, t.UserInserted, t.DateUpdated, t.UserUpdated, "
                    + " t.CompanyName, t.FirstName, t.SecondName, t.Ssn, t.Vat, t.Address, " 
                    + " t.City, t.State, t.ZipCode, t.Nation, "
                    + " t.Tel1, t.Mobile1, t.Website1, t.Email, t.Enabled, t.Notes, "
                    + " t.JsData, t.Custom1, t.Custom2, t.Custom3 "
                    + " FROM ["+ this.TableName +"] t "
                    + " WHERE t.Id > 0 ";
                if (filter.Id > 0 || filter.Id == -1)
                {
                    sSql += " AND t.Id = @Id ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Id", filter.Id));
                }
                //check user context if needed
                if (!string.IsNullOrEmpty(this.ownerUser))
                {
                    sSql += " AND t.OwnerUser = @ContextOwnerUser ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ContextOwnerUser", this.ownerUser));
                }
                if (!string.IsNullOrEmpty(filter.OwnerUser))
                {
                    sSql += " AND t.OwnerUser = @OwnerUser ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "OwnerUser", filter.OwnerUser));
                }
                if (!string.IsNullOrEmpty(filter.CompanyNameLike))
                {
                    sSql += " AND t.CompanyName like @CompanyNameLike ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CompanyNameLike", "%" + filter.CompanyNameLike + "%"));
                }
                if (!string.IsNullOrEmpty(filter.Ssn))
                {
                    sSql += " AND t.Ssn = @Ssn ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Ssn", filter.Ssn));
                }
                if (!string.IsNullOrEmpty(filter.SsnLike))
                {
                    sSql += " AND t.Ssn like @SsnLike ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "SsnLike", "%" + filter.SsnLike + "%"));
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
                if (filter.Enabled != Utility.TristateBool.NotSet)
                {
                    sSql += " AND t.Enabled = @Enabled ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Enabled", filter.Enabled));
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    sSql += " ORDER BY " + sort;
                }
                else
                {
                    sSql += " ORDER BY t.CompanyName ";
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
        public List<Customer> GetBySsn(string ssn)
        {
            var filter = new CustomersFilter();
            if (string.IsNullOrEmpty(ssn))
                filter.Id = -1;
            filter.Ssn = ssn;
            return GetByFilter(filter, "");
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
            return this.Update(theObj, false, false);
        }

        public int Update(Customer theObj, bool checkVatKey, bool checkSsnKey)
        {
            if (this.checkUserContext && !PgnUserCurrent.IsAuthenticated)
                throw new ArgumentException("User not authenticated");


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
                    //if (string.IsNullOrEmpty(theObj.Vat))
                    //    throw new ArgumentNullException("Vat");

                    var list = this.GetByVat(theObj.Vat);
                    if (list.Count > 0)
                    {
                        if (list[0].Id != theObj.Id)
                            throw new DuplicateNameException("Vat");
                    }
                }

                if (checkSsnKey)
                {
                    //if (string.IsNullOrEmpty(theObj.Ssn))
                    //    throw new ArgumentNullException("Ssn");

                    var list = this.GetBySsn(theObj.Ssn);
                    if (list.Count > 0)
                    {
                        if (list[0].Id != theObj.Id)
                            throw new DuplicateNameException("Ssn");
                    }
                }

                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "UPDATE [" + this.TableName + "] "
                + " SET DateUpdated=@DateUpdated, "
                + " UserUpdated=@UserUpdated, "
                + " CompanyName=@CompanyName, "
                + " FirstName=@FirstName, "
                + " SecondName=@SecondName, "
                + " Ssn=@Ssn, "
                + " Vat=@Vat, "
                + " Address=@Address, "
                + " City=@City, "
                + " State=@State, "
                + " ZipCode=@ZipCode, "
                + " Nation=@Nation, "
                + " Tel1=@Tel1, "
                + " Mobile1=@Mobile1, "
                + " Website1=@Website1, "
                + " Email=@Email, "
                + " Enabled=@Enabled, "
                + " Notes=@Notes, "
                + " JsData=@JsData, "
                + " Custom1=@Custom1, "
                + " Custom2=@Custom2, "
                + " Custom3=@Custom3 "
                + " WHERE Id=@Id ";
                if (this.checkUserContext)
                {
                    sSql += " AND OwnerUser=@OwnerUser ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "OwnerUser", this.ownerUser));
                }
                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", theObj.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DateUpdated", theObj.DateUpdated));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UserUpdated", theObj.UserUpdated));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CompanyName", theObj.CompanyName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "FirstName", theObj.FirstName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "SecondName", theObj.SecondName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "SSn", theObj.Ssn));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Vat", theObj.Vat));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Address", theObj.Address));
                myCmd.Parameters.Add(Database.Parameter(myProv, "City", theObj.City));
                myCmd.Parameters.Add(Database.Parameter(myProv, "State", theObj.State));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ZipCode", theObj.ZipCode));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Nation", theObj.Nation));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Tel1", theObj.Tel1));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Mobile1", theObj.Mobile1));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Website1", theObj.Website1));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Email", theObj.Email));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Enabled", theObj.Enabled));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Notes", theObj.Notes));
                myCmd.Parameters.Add(Database.Parameter(myProv, "JsData", theObj.JsData));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Custom1", theObj.Custom1));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Custom2", theObj.Custom2));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Custom3", theObj.Custom3));

                myCmd.CommandText = Database.ParseSql(sSql);
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
            return this.Insert(newObj, false, false);
        }

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public Customer Insert(Customer newObj, bool checkVatKey, bool checkSsnKey)
        {
            if (this.checkUserContext && !PgnUserCurrent.IsAuthenticated)
                throw new ArgumentException("User not authenticated");


            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            var result = new Customer();

            try
            {
                if (checkVatKey)
                {
                    var list = this.GetByVat(newObj.Vat);
                    if (list.Count > 0)
                        throw new DuplicateNameException("Vat");
                }
                if (checkSsnKey)
                {
                    var list = this.GetBySsn(newObj.Ssn);
                    if (list.Count > 0)
                        throw new DuplicateNameException("Ssn");
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
                    + "(Id, OwnerUser, "
                    + " DateInserted, UserInserted, DateUpdated, UserUpdated, "
                    + " CompanyName, FirstName, SecondName, Ssn, Vat, Address, "
                    + " City, State, ZipCode, Nation, "
                    + " Tel1, Mobile1, Website1, Email, Enabled, Notes, "
                    + " JsData, Custom1, Custom2, Custom3) "
                    + " VALUES(@Id, @OwnerUser, "
                    + " @DateInserted, @UserInserted, @DateUpdated, @UserUpdated, "
                    + " @CompanyName, @FirstName, @SecondName, @Ssn, @Vat, @Address, "
                    + " @City, @State, @ZipCode, @Nation, "
                    + " @Tel1, @Mobile1, @Website1, @Email, @Enabled, @Notes, "
                    + " @JsData, @Custom1, @Custom2, @Custom3) ";
                myCmd.CommandText = Database.ParseSql(sSql);

                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", result.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "OwnerUser", (this.checkUserContext ? this.ownerUser : result.OwnerUser) ));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DateInserted", result.DateInserted));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UserInserted", result.UserInserted));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DateUpdated", result.DateUpdated));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UserUpdated", result.UserUpdated));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CompanyName", result.CompanyName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "FirstName", result.FirstName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "SecondName", result.SecondName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Ssn", result.Ssn));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Vat", result.Vat));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Address", result.Address));
                myCmd.Parameters.Add(Database.Parameter(myProv, "City", result.City));
                myCmd.Parameters.Add(Database.Parameter(myProv, "State", result.State));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ZipCode", result.ZipCode));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Nation", result.Nation));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Tel1", result.Tel1));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Mobile1", result.Mobile1));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Website1", result.Website1));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Email", result.Email));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Enabled", result.Enabled));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Notes", result.Notes));
                myCmd.Parameters.Add(Database.Parameter(myProv, "JsData", result.JsData));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Custom1", result.Custom1));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Custom2", result.Custom2));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Custom3", result.Custom3));

                myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override int DeleteById(int id)
        {
            bool allowed = false;
            int res = 0;

            if (this.GetByKey(id).Id > 0)
                allowed = true;

            if (allowed)
                res = base.DeleteById(id);

            return res;
        }

        public int DeleteByOwnerUser()
        {
            if (this.checkUserContext && !PgnUserCurrent.IsAuthenticated)
                throw new ArgumentException("User not authenticated");

            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myProv.CreateCommand();
            string sSql;
            int res = 0;

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "DELETE FROM [" + this.TableName + "] WHERE OwnerUser=@OwnerUser ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "OwnerUser", this.ownerUser));
                res = myCmd.ExecuteNonQuery();
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


        protected override void FillObject(Customer result, DbDataReader myRd)
        {
            if (!Convert.IsDBNull(myRd["Id"]))
                result.Id = (int)myRd["Id"];
            if (!Convert.IsDBNull(myRd["OwnerUser"]))
                result.OwnerUser = (string)myRd["OwnerUser"];
            if (!Convert.IsDBNull(myRd["DateInserted"]))
                result.DateInserted = (DateTime)myRd["DateInserted"];
            if (!Convert.IsDBNull(myRd["UserInserted"]))
                result.UserInserted = (string)myRd["UserInserted"];
            if (!Convert.IsDBNull(myRd["DateUpdated"]))
                result.DateUpdated = (DateTime)myRd["DateUpdated"];
            if (!Convert.IsDBNull(myRd["UserUpdated"]))
                result.UserUpdated = (string)myRd["UserUpdated"];

            if (!Convert.IsDBNull(myRd["CompanyName"]))
                result.CompanyName = (string)myRd["CompanyName"];
            if (!Convert.IsDBNull(myRd["FirstName"]))
                result.FirstName = (string)myRd["FirstName"];
            if (!Convert.IsDBNull(myRd["SecondName"]))
                result.SecondName = (string)myRd["SecondName"];
            if (!Convert.IsDBNull(myRd["Ssn"]))
                result.Ssn = (string)myRd["Ssn"];
            if (!Convert.IsDBNull(myRd["Vat"]))
                result.Vat = (string)myRd["Vat"];
            if (!Convert.IsDBNull(myRd["Address"]))
                result.Address = (string)myRd["Address"];
            if (!Convert.IsDBNull(myRd["City"]))
                result.City = (string)myRd["City"];
            if (!Convert.IsDBNull(myRd["State"]))
                result.State = (string)myRd["State"];
            if (!Convert.IsDBNull(myRd["ZipCode"]))
                result.ZipCode = (string)myRd["ZipCode"];
            if (!Convert.IsDBNull(myRd["Nation"]))
                result.Nation = (string)myRd["Nation"];
            if (!Convert.IsDBNull(myRd["Tel1"]))
                result.Tel1 = (string)myRd["Tel1"];
            if (!Convert.IsDBNull(myRd["Mobile1"]))
                result.Mobile1 = (string)myRd["Mobile1"];
            if (!Convert.IsDBNull(myRd["Website1"]))
                result.Website1 = (string)myRd["Website1"];
            if (!Convert.IsDBNull(myRd["Email"]))
                result.Email = (string)myRd["Email"];
            if (!Convert.IsDBNull(myRd["Enabled"]))
                result.Enabled = (bool)myRd["Enabled"];
            if (!Convert.IsDBNull(myRd["Notes"]))
                result.Notes = (string)myRd["Notes"];
            if (!Convert.IsDBNull(myRd["JsData"]))
                result.JsData = (string)myRd["JsData"];
            if (!Convert.IsDBNull(myRd["Custom1"]))
                result.Custom1 = (string)myRd["Custom1"];
            if (!Convert.IsDBNull(myRd["Custom2"]))
                result.Custom2 = (string)myRd["Custom2"];
            if (!Convert.IsDBNull(myRd["Custom3"]))
                result.Custom3 = (string)myRd["Custom3"];
        }
    }
}