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
    public class FormFieldsManager : TableManager<FormField, FormFieldFilter, int>
    {
        [DebuggerStepThrough()]
        public FormFieldsManager()
        {
            this.TableName = "#__formFields";
            this.KeyFieldName = "Id";
        }

        public override Dictionary<string, string> GetList()
        {
            var res = new Dictionary<string, string>();
            var list = GetByFilter(new FormFieldFilter(), "");
            foreach (var item in list)
            {
                res.Add(item.Id.ToString(), item.Name);
            }
            return res;
        }

        public override List<FormField> GetByFilter(FormFieldFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            var result = new List<FormField>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT t.Id, t.FormId, t.Enabled, t.GroupName, "
                + " t.Name, t.DefaultValue, t.MinValue, t.MaxValue, " 
                + " t.RowsNo, t.ColsNo, t.CssClass, t.CssStyle, t.FieldType "
                + " FROM [" + this.TableName + "] t "
                + " WHERE 1=1 ";
                
                if (filter.Id > 0 || filter.Id == -1)
                {
                    sSql += " AND t.Id = @Id ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Id", filter.Id));
                }
                if (filter.FormId > 0 || filter.FormId == -1)
                {
                    sSql += " AND t.FormId = @FormId ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "FormId", filter.FormId));
                }
                if (!string.IsNullOrEmpty(filter.Name))
                {
                    sSql += " AND t.Name = @Name ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Name", filter.Name));
                }
                if (!string.IsNullOrEmpty(filter.NamePart))
                {
                    sSql += " AND (t.Name like @NamePart) ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "NamePart", "%" + filter.NamePart + "%"));
                }
                if (filter.Enabled != Utility.TristateBool.NotSet)
                {
                    sSql += " AND t.Enabled = @Enabled ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Enabled", filter.Enabled));
                }
                if (!string.IsNullOrEmpty(sort))
                    sSql += " ORDER BY " + sort;
                else
                    sSql += " ORDER BY t.Name ";

                myCmd.CommandText = Database.ParseSql(sSql);
                myRd = myCmd.ExecuteReader();
                while (myRd.Read())
                {
                    var item = new FormField();
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

        public override FormField GetByKey(int id)
        {
            var result = new FormField();
            var list = new List<FormField>();
            var filter = new FormFieldFilter();
            filter.Id = id == 0 ? -1 : id;
            list = GetByFilter(filter, "");
            if (list.Count > 0)
                result = list[0];
            return result;
        }

        public override int Update(FormField theObj)
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
                + " SET FormId=@FormId, Enabled=@Enabled, GroupName=@GroupName, "
                + " Name=@Name, DefaultValue=@DefaultValue, "
                + " MinValue=@MinValue, MaxValue=@MaxValue, "
                + " RowsNo=@RowsNo, ColsNo=@ColsNo, "
                + " CssClass=@CssClass, CssStyle=@CssStyle, FieldType=@FieldType "
                + " WHERE Id=@Id";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", theObj.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "FormId", theObj.FormId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Enabled", theObj.Enabled));
                myCmd.Parameters.Add(Database.Parameter(myProv, "GroupName", theObj.Group));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Name", theObj.Name));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DefaultValue", theObj.DefaultValue));
                myCmd.Parameters.Add(Database.Parameter(myProv, "MinValue", theObj.MinValue));
                myCmd.Parameters.Add(Database.Parameter(myProv, "MaxValue", theObj.MaxValue));
                myCmd.Parameters.Add(Database.Parameter(myProv, "RowsNo", theObj.Rows));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ColsNo", theObj.Cols));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CssClass", theObj.CssClass));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CssStyle", theObj.CssStyle));
                myCmd.Parameters.Add(Database.Parameter(myProv, "FieldType", theObj.Type.ToString()));

                result = myCmd.ExecuteNonQuery();
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

        public override FormField Insert(FormField newObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            var result = new FormField();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                result = newObj;
                result.Id = this.GetNextId();

                sSql = "INSERT INTO [" + this.TableName + "] "
                    + " (Id, FormId, Enabled, GroupName, Name, "
                    + " DefaultValue, MinValue, MaxValue, RowsNo, ColsNo, "
                    + " CssClass, CssStyle, FieldType) "
                    + " VALUES(@Id, @FormId, @Enabled, @GroupName, @Name, "
                    + " @DefaultValue, @MinValue, @MaxValue, @RowsNo, @ColsNo, "
                    + " @CssClass, @CssStyle, @FieldType)";
                myCmd.CommandText = Database.ParseSql(sSql);

                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", result.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "FormId", result.FormId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Enabled", result.Enabled));
                myCmd.Parameters.Add(Database.Parameter(myProv, "GroupName", result.Group));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Name", result.Name));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DefaultValue", result.DefaultValue));
                myCmd.Parameters.Add(Database.Parameter(myProv, "MinValue", result.MinValue));
                myCmd.Parameters.Add(Database.Parameter(myProv, "MaxValue", result.MaxValue));
                myCmd.Parameters.Add(Database.Parameter(myProv, "RowsNo", result.Rows));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ColsNo", result.Cols));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CssClass", result.CssClass));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CssStyle", result.CssStyle));
                myCmd.Parameters.Add(Database.Parameter(myProv, "FieldType", result.Type.ToString()));

                myCmd.ExecuteNonQuery();
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

        public override int DeleteById(int id)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myProv.CreateCommand();
            int res = 0;
            string sSql = "";

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "DELETE FROM #__formFieldOptions WHERE FormFieldId=@FormFieldId";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "FormFieldId", id));
                myCmd.ExecuteNonQuery();

                res = base.DeleteById(id);
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

        #region protected methods

        protected override void FillObject(FormField result, DbDataReader myRd)
        {
            if (!Convert.IsDBNull(myRd["Id"]))
                result.Id = (int)myRd["Id"];
            if (!Convert.IsDBNull(myRd["FormId"]))
                result.FormId = (int)myRd["FormId"];
            if (!Convert.IsDBNull(myRd["Enabled"]))
                result.Enabled = (bool)myRd["Enabled"];
            if (!Convert.IsDBNull(myRd["GroupName"]))
                result.Group = (string)myRd["GroupName"];
            if (!Convert.IsDBNull(myRd["Name"]))
                result.Name= (string)myRd["Name"];
            if (!Convert.IsDBNull(myRd["DefaultValue"]))
                result.DefaultValue = (string)myRd["DefaultValue"];
            if (!Convert.IsDBNull(myRd["MinValue"]))
                result.MinValue = (int)myRd["MinValue"];
            if (!Convert.IsDBNull(myRd["MaxValue"]))
                result.MaxValue = (int)myRd["MaxValue"];
            if (!Convert.IsDBNull(myRd["RowsNo"]))
                result.Rows = (int)myRd["RowsNo"];
            if (!Convert.IsDBNull(myRd["ColsNo"]))
                result.Cols = (int)myRd["ColsNo"];
            if (!Convert.IsDBNull(myRd["CssClass"]))
                result.CssClass = (string)myRd["CssClass"];
            if (!Convert.IsDBNull(myRd["CssStyle"]))
                result.CssStyle = (string)myRd["CssStyle"];
            if (!Convert.IsDBNull(myRd["FieldType"]))
            {
                FormFieldTypeEnum res = FormFieldTypeEnum.Text;
                try
                {
                    res = (FormFieldTypeEnum)Enum.Parse(
                    typeof(FormFieldTypeEnum), (string)myRd["FieldType"], true);
                }
                catch { }
                result.Type = res;
            }

            result.Options = null;  //will caus late binding
            result.LabelValue = result.Name;
        }

        #endregion
    }

    public class FormFieldOptionsManager : TableManagerWithOrdering<FormFieldOption, FormFieldOptionFilter, int>
    {
        [DebuggerStepThrough()]
        public FormFieldOptionsManager()
        {
            this.TableName = "#__formFieldOptions";
            this.KeyFieldName = "Id";
        }

        public Dictionary<string, string> GetList(int formFieldId)
        {
            var res = new Dictionary<string, string>();
            var filter = new FormFieldOptionFilter();
            filter.FormFieldId = formFieldId;
            var list = GetByFilter(filter, "");
            foreach (var item in list)
            {
                res.Add(item.Id.ToString(), item.Label);
            }
            return res;
        }

        public override List<FormFieldOption> GetByFilter(FormFieldOptionFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            var result = new List<FormFieldOption>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT t.Id, t.FormFieldId, t.Label, t.Value, t.Ordering "
                + " FROM [" + this.TableName + "] t "
                + " WHERE 1=1 ";

                if (filter.Id > 0 || filter.Id == -1)
                {
                    sSql += " AND t.Id = @Id ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Id", filter.Id));
                }
                if (filter.FormFieldId > 0 || filter.FormFieldId == -1)
                {
                    sSql += " AND t.FormFieldId = @FormFieldId ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "FormFieldId", filter.FormFieldId));
                }
                if (!string.IsNullOrEmpty(sort))
                    sSql += " ORDER BY " + sort;
                else
                    sSql += " ORDER BY t.Ordering, t.Label ";

                myCmd.CommandText = Database.ParseSql(sSql);
                myRd = myCmd.ExecuteReader();
                while (myRd.Read())
                {
                    var item = new FormFieldOption();
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

        public override FormFieldOption GetByKey(int id)
        {
            var result = new FormFieldOption();
            var list = new List<FormFieldOption>();
            var filter = new FormFieldOptionFilter();
            filter.Id = id == 0 ? -1 : id;
            list = GetByFilter(filter, "");
            if (list.Count > 0)
                result = list[0];
            return result;
        }

        public override int Update(FormFieldOption theObj)
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
                + " SET FormFieldId=@FormFieldId, Label=@Label, "
                + " Value=@Value, Ordering=@Ordering "
                + " WHERE Id = @Id";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", theObj.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "FormFieldId", theObj.FormFieldId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Label", theObj.Label));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Value", theObj.Value));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Ordering", theObj.Ordering));

                result = myCmd.ExecuteNonQuery();
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

        public override FormFieldOption Insert(FormFieldOption newObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            var result = new FormFieldOption();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                result = newObj;
                result.Id = this.GetNextId();
                result.Ordering = base.GetNextOrdering();

                sSql = "INSERT INTO [" + this.TableName + "] "
                    + " (Id, FormFieldId, Label, Value, Ordering) "
                    + " VALUES(@Id, @FormFieldId, @Label, @Value, @Ordering)";
                myCmd.CommandText = Database.ParseSql(sSql);

                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", result.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "FormFieldId", result.FormFieldId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Label", result.Label));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Value", result.Value));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Ordering", result.Ordering));

                myCmd.ExecuteNonQuery();
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

        #region protected methods

        protected override void FillObject(FormFieldOption result, DbDataReader myRd)
        {
            if (!Convert.IsDBNull(myRd["Id"]))
                result.Id = (int)myRd["Id"];
            if (!Convert.IsDBNull(myRd["FormFieldId"]))
                result.FormFieldId = (int)myRd["FormFieldId"];
            if (!Convert.IsDBNull(myRd["Label"]))
                result.Label = (string)myRd["Label"];
            if (!Convert.IsDBNull(myRd["Value"]))
                result.Value = (string)myRd["Value"];
            if (!Convert.IsDBNull(myRd["Ordering"]))
                result.Ordering = (int)myRd["Ordering"];
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
                var o1 = new PigeonCms.FormFieldOption();
                o1 = this.GetByKey(currentRecordId);

                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT TOP 1 t.Id "
                + " FROM [" + this.TableName + "] t "
                + " WHERE t.Ordering < @Ordering  "
                + " AND t.FormFieldId = @FormFieldId "
                + " ORDER BY t.Ordering DESC ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Ordering", ordering));
                myCmd.Parameters.Add(Database.Parameter(myProv, "FormFieldId", o1.FormFieldId));
                myRd = myCmd.ExecuteReader();
                if (myRd.Read())
                {
                    if (myRd[0] != DBNull.Value)
                    {
                        result = (int)myRd[0];
                    }
                }
                myRd.Close();
                //table init
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
                var o1 = new PigeonCms.FormFieldOption();
                o1 = this.GetByKey(currentRecordId);

                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT TOP 1 t.Id "
                + " FROM [" + this.TableName + "] t "
                + " WHERE t.Ordering > @Ordering "
                + " AND t.FormFieldId = @FormFieldId "
                + " ORDER BY t.Ordering ASC ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Ordering", ordering));
                myCmd.Parameters.Add(Database.Parameter(myProv, "FormFieldId", o1.FormFieldId));
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


        #endregion
    }

}