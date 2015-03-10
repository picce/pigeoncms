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
    /// DAL for label obj (in table #__labels)
    /// </summary>
    public class LabelsManager : TableManager<ResLabel, LabelsFilter, int>, ITableManager
    {
        [DebuggerStepThrough()]
        public LabelsManager()
        {
            this.TableName = "#__labels";
            this.KeyFieldName = "Id";
        }

        public ResLabelTrans GetLabelTransByKey(string resourceSet, string resourceId)
        {
            var result = new ResLabelTrans();
            var list = new List<ResLabelTrans>();
            var filter = new LabelTransFilter();

            filter.ResourceSet = resourceSet;
            filter.ResourceId = resourceId;
            list = this.GetLabelTransByFilter(filter, "");
            if (list.Count > 0)
                result = list[0];
            return result;
        }

        public List<ResLabelTrans> GetLabelTransByFilter(LabelTransFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            var result = new List<ResLabelTrans>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT ResourceSet, ResourceId, TextMode, IsLocalized, ResourceParams "
                    + " FROM [" + this.TableName + "] t "
                    + " WHERE t.Id > 0 ";
                if (!string.IsNullOrEmpty(filter.ResourceSet))
                {
                    sSql += " AND t.ResourceSet = @ResourceSet ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ResourceSet", filter.ResourceSet));
                }
                if (!string.IsNullOrEmpty(filter.ResourceId))
                {
                    sSql += " AND t.ResourceId = @ResourceId ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ResourceId", filter.ResourceId));
                }
                sSql += " GROUP BY t.ResourceSet, t.ResourceId, TextMode, IsLocalized, ResourceParams ";
                if (!string.IsNullOrEmpty(sort))
                {
                    sSql += " ORDER BY " + sort;
                }
                else
                {
                    sSql += " ORDER BY t.ResourceSet, t.ResourceId ";
                }

                myCmd.CommandText = Database.ParseSql(sSql);
                myRd = myCmd.ExecuteReader();
                while (myRd.Read())
                {
                    var item = new ResLabelTrans();
                    if (!Convert.IsDBNull(myRd["ResourceSet"]))
                        item.ResourceSet = (string)myRd["ResourceSet"];
                    if (!Convert.IsDBNull(myRd["ResourceId"]))
                        item.ResourceId = (string)myRd["ResourceId"];
                    if (!Convert.IsDBNull(myRd["TextMode"]))
                        item.TextMode = (ContentEditorProvider.Configuration.EditorTypeEnum)myRd["TextMode"];
                    if (!Convert.IsDBNull(myRd["IsLocalized"]))
                        item.IsLocalized = (bool)myRd["IsLocalized"];
                    if (!Convert.IsDBNull(myRd["ResourceParams"]))
                        item.ResourceParams = (string)myRd["ResourceParams"];

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

        public List<string> GetResourceSetList(string resourceSetPart)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            var result = new List<string>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT ResourceSet "
                    + " FROM [" + this.TableName + "] t "
                    + " WHERE t.Id > 0 ";

                if (!string.IsNullOrEmpty(resourceSetPart))
                {
                    sSql += " AND t.ResourceSet like @resourceSetPart ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "resourceSetPart", "%" + resourceSetPart + "%"));
                }
                sSql += "GROUP BY t.ResourceSet ORDER BY t.ResourceSet ";

                myCmd.CommandText = Database.ParseSql(sSql);
                myRd = myCmd.ExecuteReader();
                while (myRd.Read())
                {
                    if (!Convert.IsDBNull(myRd["ResourceSet"]))
                        result.Add( (string)myRd["ResourceSet"] );
                }
                myRd.Close();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public override List<ResLabel> GetByFilter(LabelsFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            var result = new List<ResLabel>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT Id, CultureName, ResourceSet, ResourceId, Value, Comment, "
                    + " TextMode, IsLocalized, ResourceParams "
                    + " FROM [" + this.TableName + "] t "
                    + " WHERE t.Id > 0 ";
                if (filter.Id > 0 || filter.Id == -1)
                {
                    sSql += " AND t.Id = @Id ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Id", filter.Id));
                }
                if (!string.IsNullOrEmpty(filter.CultureName))
                {
                    sSql += " AND t.CultureName = @CultureName ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CultureName", filter.CultureName));
                }
                if (!string.IsNullOrEmpty(filter.ResourceSet))
                {
                    sSql += " AND t.ResourceSet = @ResourceSet ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ResourceSet", filter.ResourceSet));
                }
                if (!string.IsNullOrEmpty(filter.ResourceId))
                {
                    sSql += " AND t.ResourceId = @ResourceId ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ResourceId", filter.ResourceId));
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    sSql += " ORDER BY " + sort;
                }
                else
                {
                    sSql += " ORDER BY t.CultureName, t.ResourceSet, t.ResourceId ";
                }

                myCmd.CommandText = Database.ParseSql(sSql);
                myRd = myCmd.ExecuteReader();
                while (myRd.Read())
                {
                    ResLabel item = new ResLabel();
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
        public override ResLabel GetByKey(int id)
        {
            ResLabel result = new ResLabel();
            var list = new List<ResLabel>();
            LabelsFilter filter = new LabelsFilter();
            filter.Id = id;
            list = GetByFilter(filter, "");
            if (list.Count > 0)
                result = list[0];
            return result;
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public override int Update(ResLabel theObj)
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
                + " SET CultureName=@CultureName, ResourceSet=@ResourceSet, "
                + " ResourceId=@ResourceId, [Value]=@Value, Comment=@Comment, "
                + " TextMode=@TextMode, IsLocalized=@IsLocalized, ResourceParams=@ResourceParams "
                + " WHERE Id = @Id";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", theObj.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CultureName", theObj.CultureName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ResourceSet", theObj.ResourceSet));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ResourceId", theObj.ResourceId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Value", theObj.Value));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Comment", theObj.Comment));
                myCmd.Parameters.Add(Database.Parameter(myProv, "TextMode", (int)theObj.TextMode));
                myCmd.Parameters.Add(Database.Parameter(myProv, "IsLocalized", theObj.IsLocalized));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ResourceParams", theObj.ResourceParams));

                result = myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public override ResLabel Insert(ResLabel newObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            var result = new PigeonCms.ResLabel();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                result = newObj;
                //result.Id = base.GetNextId(); IDENTITY

                sSql = "INSERT INTO [" + this.TableName + "](CultureName, ResourceSet, ResourceId, Value, Comment, "
                + " TextMode, IsLocalized, ResourceParams) "
                + " VALUES(@CultureName, @ResourceSet, @ResourceId, @Value, @Comment, "
                + " @TextMode, @IsLocalized, @ResourceParams) ";
                myCmd.CommandText = Database.ParseSql(sSql);

                myCmd.Parameters.Add(Database.Parameter(myProv, "CultureName", result.CultureName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ResourceSet", result.ResourceSet));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ResourceId", result.ResourceId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Value", result.Value));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Comment", result.Comment));
                myCmd.Parameters.Add(Database.Parameter(myProv, "TextMode", (int)result.TextMode));
                myCmd.Parameters.Add(Database.Parameter(myProv, "IsLocalized", result.IsLocalized));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ResourceParams", result.ResourceParams));

                myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        protected override void FillObject(ResLabel result, DbDataReader myRd)
        {
            if (!Convert.IsDBNull(myRd["Id"]))
                result.Id = (int)myRd["Id"];
            if (!Convert.IsDBNull(myRd["CultureName"]))
                result.CultureName = (string)myRd["CultureName"];
            if (!Convert.IsDBNull(myRd["ResourceSet"]))
                result.ResourceSet = (string)myRd["ResourceSet"];
            if (!Convert.IsDBNull(myRd["ResourceId"]))
                result.ResourceId = (string)myRd["ResourceId"];
            if (!Convert.IsDBNull(myRd["Value"]))
                result.Value = (string)myRd["Value"];
            if (!Convert.IsDBNull(myRd["Comment"]))
                result.Comment = (string)myRd["Comment"];
            if (!Convert.IsDBNull(myRd["TextMode"]))
                result.TextMode = (ContentEditorProvider.Configuration.EditorTypeEnum)myRd["TextMode"];
            if (!Convert.IsDBNull(myRd["IsLocalized"]))
                result.IsLocalized = (bool)myRd["IsLocalized"];
            if (!Convert.IsDBNull(myRd["ResourceParams"]))
                result.ResourceParams = (string)myRd["ResourceParams"];
        }

        public int DeleteByResourceId(string resourceSet, string resourceId)
        {
            return DeleteByResourceId(resourceSet, resourceId, "");
        }

        public int DeleteByResourceId(string resourceSet, string resourceId, string cultureName)
        {
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

                sSql = "DELETE FROM #__labels "
                + " WHERE ResourceSet=@ResourceSet AND ResourceId=@ResourceId ";
                myCmd.Parameters.Add(Database.Parameter(myProv, "ResourceSet", resourceSet));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ResourceId", resourceId));
                if (!string.IsNullOrEmpty(cultureName))
                {
                    sSql += " AND CultureName=@CultureName";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CultureName", cultureName));
                }
                myCmd.CommandText = Database.ParseSql(sSql);
                res = myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return res;
        }
    }
}